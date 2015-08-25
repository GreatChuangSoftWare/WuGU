
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftPlatform.Controllers;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Pre_User(用户管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public void Pre_User_Domain()
        {
            PKField = "Pre_UserID";
            //PKFields = new List<string> { "Pre_UserID" };
            TableName = "Pre_User";
            TabViewName = "V_Pre_User";
        }

        public void ValidateMobilePhoneAdd()
        {
            if (SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.MobilePhone == Item.MobilePhone).Count() > 0)
                throw new Exception("手机号码重复：公司用户已使用些号码");
        }

        public void ValidateMobilePhoneUpdate()
        {
            if (SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.MobilePhone == Item.MobilePhone && p.Pre_UserID != Item.Pre_UserID).Count() > 0)
                throw new Exception("手机号码重复：公司用户已使用些号码");
        }

        #region 管理员

        /// <summary>
        /// 添加保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_AddSave()
        {
            var resp = new MyResponseBase();
            ValidateMobilePhoneAdd();
            resp = AddSave();
            Item.Pre_UserID = resp.Item.Pre_UserID;
            Pre_User_AddCache();
            return resp;
        }

        /// <summary>
        /// 添加保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_AddCompCPSave()
        {
            var resp = new MyResponseBase();
            var bAddProduct = true;//是否添加加盟商零售价格商品

            //(1)查询Sql语句
            var sqlBC_PartnerProductPriceAddProduct = ProjectCache.Sys_HOperControls.Where(p => p.OperCode == "BC_Partner.BC_PartnerProductPriceAddProductPrice").FirstOrDefault().DBTSql;
            var sql = string.Format("SELECT * FROM  Pre_Company  WHERE Pre_CompanyID={0}", LoginInfo.CompanyID);
            var OperatingItemIDs = Query16(sql, 4).Item.OperatingItemIDs;
            if (string.IsNullOrEmpty(OperatingItemIDs))
                bAddProduct = false;

            #region 添加加盟商

            ValidateMobilePhoneAdd();
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                if (bAddProduct)
                {
                    sqlBC_PartnerProductPriceAddProduct = string.Format(sqlBC_PartnerProductPriceAddProduct, Item.Pre_CompanyID, resp.Item.Pre_UserID, OperatingItemIDs);
                    Query16(sqlBC_PartnerProductPriceAddProduct, 1);
                }
            }));

            #endregion

            return resp;
        }


        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_EditSave()
        {
            var resp = new MyResponseBase();
            ValidateMobilePhoneUpdate();
            resp = EditSave();
            Pre_User_UpdateCache();
            return resp;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_EditByMySave()
        {
            var resp = new MyResponseBase();
            ValidateMobilePhoneUpdate();
            resp = EditSave();
            Pre_User_UpdateCacheByMy();
            return resp;
        }

        #endregion

        public MyResponseBase Pre_User_ChangPass()
        {
            var resp = new MyResponseBase();
            try
            {
                var sql = string.Format("Update  Pre_User  SET  PasswordDigest='{0}' WHERE  Pre_UserID={1}", Item.PasswordDigest, Item.Pre_UserID);
                resp = Query16(sql, 1);
                var item = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == LoginInfo.Sys_LoginInfoID).FirstOrDefault();
                item.PasswordDigest = Item.PasswordDigest;
            }
            catch
            {
                throw new Exception("修改密码失败");
            }
            return resp;
        }

        #region 企业管理时：对企业管理员的管理

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_AddAdminSave()
        {
            var resp = new MyResponseBase();
            //查询企业类型为2的管理员角色
            var Pre_RoleID = SoftProjectAreaEntityDomain.Pre_Roles.Where(p => p.LoginCategoryID == 2 && p.BAdmin == 1).FirstOrDefault().Pre_RoleID;
            if (Pre_RoleID == null)
                throw new Exception("企业管理员角色还不有添加");
            //(1)验证
            ValidateMobilePhoneAdd();
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                Pre_User_Domain();
                Sys_HOperControl = null;
                Item.Pre_RoleID = Pre_RoleID;
                Item.UserCategoryID = 1;
                Item.UserStatuID = 1;
                Item.LoginCategoryID = 2;
                OperCode = "Pre_User.AddAdminSave";
                Item.UserName = Item.PreCompanyName2;
                resp = Execute();
            }));

            //(3)添加到缓存
            Item.Pre_UserID = resp.Item.Pre_UserID;
            Pre_User_AddCache();
            return resp;
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_EditAdminSave()
        {
            var resp = new MyResponseBase();
            //查询企业类型为2的管理员角色
            var Pre_RoleID = SoftProjectAreaEntityDomain.Pre_Roles.Where(p => p.LoginCategoryID == 2 && p.BAdmin == 1).FirstOrDefault().Pre_RoleID;
            //(1) 验证
            ValidateMobilePhoneUpdate();
            resp = ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                Pre_User_Domain();
                Sys_HOperControl = null;
                Item.Pre_RoleID = Pre_RoleID;
                Item.UserName = Item.PreCompanyName2;
                OperCode = "Pre_User.EditAdminSave";
                resp = Execute();
            }));
            Pre_User_UpdateCache();
            return resp;
        }

        #endregion

        #region 缓存、界面元素

        #region 所有用户角色：保存用户时清空

        public void Pre_User_AddCache()
        {
            #region 更新：企业用户缓存

            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.Pre_UserRoleAll.Add(resp.Item);

            #endregion
        }

        public void Pre_User_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var Pre_User = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == Item.Pre_UserID).FirstOrDefault();
            SoftProjectAreaEntityDomain.Pre_UserRoleAll.Remove(Pre_User);
            SoftProjectAreaEntityDomain.Pre_UserRoleAll.Add(resp.Item);
            #endregion
        }


        public void Pre_User_UpdateCacheByMy()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var Pre_User = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == Item.Pre_UserID).FirstOrDefault();
            SoftProjectAreaEntityDomain.Pre_UserRoleAll.Remove(Pre_User);
            SoftProjectAreaEntityDomain.Pre_UserRoleAll.Add(resp.Item);
            //Pre_User = resp.Item;

            var user = resp.Item;
            var loginInfo = new SoftProjectAreaEntity
            {
                Sys_LoginInfoID = user.Pre_UserID,
                LoginCategoryID = 1,
                MobilePhone = user.MobilePhone,
                CompanyID = user.Pre_CompanyID,
                CompanyName = user.PreCompanyName,
                UserName = user.UserName,
                RoleID = user.Pre_RoleID,

                HomePageName = user.RoleHomePageName,
                HomePageUrl = user.RoleHomePageUrl,
            };
            HttpContext.Current.Session["LoginInfo"] = loginInfo;
            #endregion
        }

        /// <summary>
        /// 所有用户管理：缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_All()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT *  FROM [dbo].[V_Pre_User] A ");
            var sql = sbSql.ToString();
            var resp = Query16(sql);
            return resp;
        }

        static List<SoftProjectAreaEntity> _Pre_UserRoleAll = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Pre_UserRoleAll
        {
            get
            {
                if (_Pre_UserRoleAll.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    //_Pre_UserRoleAll = domain.Pre_UserRole_All().Items;
                    _Pre_UserRoleAll = domain.Pre_User_All().Items;
                }
                return _Pre_UserRoleAll;
            }
        }

        public static void Pre_UserRoleAll_Clare()
        {
            _Pre_UserRoleAll = new List<SoftProjectAreaEntity>();
        }

        #endregion

        #endregion

        #region 顾客统计

        /// <summary>
        /// 根据区域进行统计--顾客数量、消费额
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_ByAreaTotalCompCU()
        {
            //Sys_HOperControl = null;

            Sys_HOperControl = null;
            //OperCode = "C_CustomerOrderDetail.Index";
            //ModularOrFunCode = "C_CustomerAreas.C_CustomerOrderDetail.Index";
            //ModularOrFunCode = "Pre_User.ByAreaTotalCompCU.ByAreaFraTotal";
            SelectSubType = 6;
            //bCal = 1;
            //resp = Execute();

            //            -		domain.Querys	Count = 3	Framework.Core.Querys
            //-		[0]	{Framework.Core.Query}	Framework.Core.Query
            //        AndOr	null	string
            //        FieldName	"Ba_AreaID1___equal"	string
            //        Oper	null	string
            //        QuryType	0	int
            //        Value	"1"	string

            //if (!Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            //Querys.

            if (!Querys.QueryDicts.ContainsKey("Ba_AreaID1___equal")||
                string.IsNullOrEmpty(Querys.QueryDicts["Ba_AreaID1___equal"].Value))
            {
                OperCode = "C_Customer.ByProvince";
            }
            else if (Querys.QueryDicts.ContainsKey("Ba_AreaID2___equal") && string.IsNullOrEmpty(Querys.QueryDicts["Ba_AreaID2___equal"].Value))
            {
                OperCode = "C_Customer.ByCity";
            }
            else if (Querys.QueryDicts.ContainsKey("Ba_AreaID3___equal") && string.IsNullOrEmpty(Querys.QueryDicts["Ba_AreaID3___equal"].Value))
            {
                OperCode = "C_Customer.ByCounty";
            }
            var resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

        /// <summary>
        /// 根据区域进行统计--顾客数量、消费额
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_ByAreaDetailCompCU()
        {
            Sys_HOperControl = null;
            //OperCode = "C_CustomerOrderDetail.Index";
            //ModularOrFunCode = "C_CustomerAreas.C_CustomerOrderDetail.Index";
            //ModularOrFunCode = "C_CustomerAreas.C_Customer.ByAreaFraDetail";
            SelectSubType = 6;
            //bCal = 1;
            OperCode = "C_Customer.ByAreaFraDetail";
            var resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

        #endregion

        #region 合作商统计

        /// <summary>
        /// 根据区域进行统计--顾客数量、消费额
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_ByAreaTotalCompCP()
        {
            Sys_HOperControl = null;
            SelectSubType = 6;

            if (Item.Ba_AreaID1 == null)
            {
                OperCode = "BC_Partner.ByProvince";
            }
            else if (Item.Ba_AreaID2 == null)
            {
                OperCode = "BC_Partner.ByCity";
            }
            else if (Item.Ba_AreaID3 == null)
            {
                OperCode = "BC_Partner.ByCounty";
            }
            var resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

        /// <summary>
        /// 根据区域进行统计--顾客数量、消费额
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_ByAreaDetailCompCP()
        {
            Sys_HOperControl = null;
            //OperCode = "C_CustomerOrderDetail.Index";
            //ModularOrFunCode = "C_CustomerAreas.C_CustomerOrderDetail.Index";
            //ModularOrFunCode = "C_CustomerAreas.C_Customer.ByAreaFraDetail";
            SelectSubType = 6;
            //bCal = 1;
            OperCode = "BC_Partner.ByAreaFraDetail";
            var resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

        #endregion


        #region 合作商统计

        ///// <summary>
        ///// 根据区域进行统计--顾客数量、消费额
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Pre_User_ByAreaTotalCompCP()
        //{
        //    //Sys_HOperControl = null;

        //    Sys_HOperControl = null;
        //    //OperCode = "C_CustomerOrderDetail.Index";
        //    //ModularOrFunCode = "C_CustomerAreas.C_CustomerOrderDetail.Index";
        //    ModularOrFunCode = "BC_PartnerAreas.BC_Partner.ByAreaBaTotal";
        //    SelectSubType = 6;
        //    bCal = 1;
        //    //            resp = Execute();

        //    if (Item.Ba_AreaID1 == null)
        //    {
        //        OperCode = "BC_Partner.ByProvince";
        //        //;WITH T0 AS
        //        //(
        //        //    SELECT Ba_AreaID1,AreaName1,SUM(PriceTotal) PriceTotal
        //        //    FROM V_BC_PartnerOrderDetail A
        //        //    GROUP BY Ba_AreaID1,AreaName1
        //        //)
        //        //,T1 AS
        //        //(
        //        //    SELECT Ba_AreaID1,COUNT(*) Count
        //        //    FROM [dbo].V_BC_Partner
        //        //    GROUP BY Ba_AreaID1,AreaName1
        //        //)
        //        //,T1000 AS
        //        //(
        //        //    SELECT  AreaName1 AreaName,T1.Count,T0.PriceTotal
        //        //    FROM  T0 JOIN T1 ON T0.Ba_AreaID1=T1.Ba_AreaID1
        //        //)
        //    }
        //    else if (Item.Ba_AreaID2 == null)
        //    {
        //        OperCode = "BC_Partner.ByCity";
        //        //;WITH T0 AS
        //        //(
        //        //    SELECT Ba_AreaID1,AreaName1,Ba_AreaID2,AreaName2,SUM(PriceTotal) PriceTotal
        //        //    FROM V_BC_PartnerOrderDetail A
        //        //    WHERE 1=1 AND Ba_AreaID1=@Ba_AreaID1
        //        //    GROUP BY Ba_AreaID1,AreaName1,Ba_AreaID2,AreaName2
        //        //)
        //        //,T1 AS
        //        //(
        //        //    SELECT Ba_AreaID2,COUNT(*) Count
        //        //    FROM [dbo].V_BC_Partner
        //        //    WHERE 1=1 AND Ba_AreaID1=@Ba_AreaID1
        //        //    GROUP BY Ba_AreaID1,AreaName1,Ba_AreaID2,AreaName2
        //        //)
        //        //,T1000 AS
        //        //(
        //        //    SELECT  AreaName2 AreaName,T1.Count,T0.PriceTotal
        //        //    FROM  T0 JOIN T1 ON T0.Ba_AreaID2=T1.Ba_AreaID2
        //        //)

        //    }
        //    else if (Item.Ba_AreaID3 == null)
        //    {
        //        OperCode = "BC_Partner.ByCounty";
        //        //;WITH T0 AS
        //        //(
        //        //    SELECT Ba_AreaID1,AreaName1,Ba_AreaID2,AreaName2,Ba_AreaID3,AreaName3,SUM(PriceTotal) PriceTotal
        //        //    FROM V_BC_PartnerOrderDetail A
        //        //    WHERE 1=1 AND Ba_AreaID2=@Ba_AreaID2
        //        //    GROUP BY Ba_AreaID1,AreaName1,Ba_AreaID2,AreaName2,Ba_AreaID3,AreaName3
        //        //)
        //        //,T1 AS
        //        //(
        //        //    SELECT Ba_AreaID3,COUNT(*) Count
        //        //    FROM [dbo].V_BC_Partner
        //        //    WHERE 1=1 AND Ba_AreaID2=@Ba_AreaID2
        //        //    GROUP BY Ba_AreaID1,AreaName1,Ba_AreaID2,AreaName2,Ba_AreaID3,AreaName3
        //        //)
        //        //,T1000 AS
        //        //(
        //        //    SELECT  AreaName3 AreaName,T1.Count,T0.PriceTotal
        //        //    FROM  T0 JOIN T1 ON T0.Ba_AreaID3=T1.Ba_AreaID3
        //        //)
        //    }
        //    var resp = Execute();

        //    resp.Querys = Querys;
        //    resp.Item = Item;
        //    return resp;
        //}

        ///// <summary>
        ///// 根据区域进行统计--顾客数量、消费额
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Pre_User_ByDetailCompCP()
        //{
        //    Sys_HOperControl = null;
        //    //OperCode = "C_CustomerOrderDetail.Index";
        //    //ModularOrFunCode = "C_CustomerAreas.C_CustomerOrderDetail.Index";
        //    ModularOrFunCode = "BC_PartnerAreas.BC_Partner.ByBaDetail";
        //    SelectSubType = 6;
        //    bCal = 1;

        //    OperCode = "BC_Partner.ByBaDetail";
        //    //;WITH T0 AS
        //    //(
        //    //    SELECT BC_PartnerID,COUNT(*) Count,SUM(PriceTotal) PriceTotal
        //    //    FROM V_BC_PartnerOrderDetail A
        //    //    WHERE 1=1
        //    //    sqlplaceholder 
        //    //    GROUP BY BC_PartnerID
        //    //)
        //    //,T1000 AS
        //    //(
        //    //    SELECT  A.*,T0.Count,T0.PriceTotal
        //    //    FROM  V_BC_Partner A JOIN T0 ON A.BC_PartnerID=T0.BC_PartnerID
        //    //)

        //    var resp = Execute();

        //    resp.Querys = Querys;
        //    resp.Item = Item;
        //    return resp;
        //}

        #endregion

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_User_WaitVisit()
        {
            //列表查询
            var resp = new MyResponseBase();

            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                PageQueryBase.RankInfo = "UpdateDate|0";
            }

            OperCode = "C_Customer.WaitVisit";
            resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

    }
}
