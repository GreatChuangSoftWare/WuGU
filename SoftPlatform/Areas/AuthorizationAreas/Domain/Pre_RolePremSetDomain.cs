
using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using SoftProject.CellModel;
using SoftPlatform.Controllers;

//namespace Framework.Web.Mvc.Authorization.Domain
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Pre_RolePremSetDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        public void Pre_RolePremSet_Domain()
        {
            PKField = "Pre_RolePremSetID";
            //PKFields = new List<string> { "Pre_RolePremSetID" };
            TableName = "Pre_RolePremSet";
        }

        /// <summary>
        /// 用户角色--编辑列表
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_RolePremSet_EditList()
        {
            MyResponseBase resp = new MyResponseBase();
            //(1)根据页面ID查询页面字段
            if (FunNameEn == "Add")
            {
                #region

                Sys_HOperControl = null;
                StringBuilder sbsql = new StringBuilder();
                sbsql.AppendLine("	SELECT Design_ModularOrFunID Sys_PremSetID,PremName PremSetName,ParentPremID PremSetParentID,PremSort OrderNum,LoginCategoryID,DataRightDropDown,0 bCheckSelect	");
                sbsql.AppendLine("	FROM Design_ModularOrFun");
                sbsql.AppendLine(string.Format("	WHERE  BPrem=1  AND (LoginCategoryID&{0})={0}",Item.LoginCategoryID));
                //sbsql.AppendLine(")");

                resp = Query16(sbsql.ToString(), 2);
                resp.FunNameEn = "Add";
                #endregion
            }
            else if (FunNameEn == "Edit")
            {
                #region 查询
                Sys_HOperControl = null;
                //OperCode = "Pre_RolePremSet.PremSetAllByRoleID";
                //resp = Execute();
                StringBuilder sbsql = new StringBuilder();
                sbsql.AppendLine(";WITH T0 AS");
                sbsql.AppendLine("(");
                sbsql.AppendLine("	SELECT Design_ModularOrFunID Sys_PremSetID,PremName PremSetName,ParentPremID PremSetParentID,Sort OrderNum,LoginCategoryID,DataRightDropDown");
                sbsql.AppendLine("	FROM Design_ModularOrFun");
                sbsql.AppendLine(string.Format("	WHERE  BPrem=1  AND (LoginCategoryID&{0})={0}", Item.LoginCategoryID));
                //sbsql.AppendLine("	WHERE  BPrem=1  AND (LoginCategoryID&1)=1");
                sbsql.AppendLine(")");
                sbsql.AppendLine("SELECT A.*,Pre_RolePremSetID,CASE WHEN B.Sys_PremSetID IS NULL THEN 0 ELSE 1 END AS bCheckSelect");
                sbsql.AppendLine("FROM T0  A");
                sbsql.AppendLine("LEFT JOIN Pre_RolePremSet B ON B.Sys_PremSetID=A.Sys_PremSetID");
                sbsql.AppendLine("AND Pre_RoleID=" + Item.Pre_RoleID);

                resp = Query16(sbsql.ToString());
                resp.FunNameEn = "Edit";
                #endregion
            }
            else if (FunNameEn == "Detail")
            {
                //Sys_HOperControl = null;
                //OperCode = "Pre_RolePremSet.PremSetAllByRoleID";
                //resp = Execute();

                StringBuilder sbsql = new StringBuilder();
                sbsql.AppendLine(";WITH T0 AS");
                sbsql.AppendLine("(");
                sbsql.AppendLine("	SELECT Design_ModularOrFunID Sys_PremSetID,PremName PremSetName,ParentPremID PremSetParentID,PremSort OrderNum,LoginCategoryID,DataRightDropDown");
                sbsql.AppendLine("	FROM Design_ModularOrFun");
                //sbsql.AppendLine("	WHERE  BPrem=1  AND (LoginCategoryID&1)=1");
                sbsql.AppendLine(string.Format("	WHERE  BPrem=1  AND (LoginCategoryID&{0})={0}", Item.LoginCategoryID));
                //sbsql.AppendLine("	WHERE  BPrem=1 AND (LoginCategoryID&1)=1");
                sbsql.AppendLine(")");
                sbsql.AppendLine("SELECT A.*,Pre_RolePremSetID,CASE WHEN B.Sys_PremSetID IS NULL THEN 0 ELSE 1 END AS bCheckSelect");
                sbsql.AppendLine("FROM T0  A");
                sbsql.AppendLine("LEFT JOIN Pre_RolePremSet B ON B.Sys_PremSetID=A.Sys_PremSetID");
                sbsql.AppendLine("AND Pre_RoleID=" + Item.Pre_RoleID);

                resp = Query16(sbsql.ToString());
                resp.FunNameEn = "Detail";
            }
            return resp;
        }

        /// <summary>
        /// 用户管理：添加、修改、删除
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_RolePremSet_AddUpdateDeleteSaves()
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    //(1)根据用户ID查询：用户角色
                    Sys_HOperControl = null;
                    OperCode = "Pre_RolePremSet.ByRoleID";
                    var OldItems = Execute().Items;

                    if (Item.Items == null)
                        Item.Items = new List<SoftProjectAreaEntity>();

                    #region (2)数据整理

                    Item.Items.ForEach(p => p.Pre_RoleID = Item.Pre_RoleID);
                    var deleteIDsEnum = (from p in OldItems select p.Pre_RolePremSetID).Except(from o in Item.Items select o.Pre_RolePremSetID);

                    var updateItems = Item.Items.Where(p => p.Pre_RolePremSetID != null && !deleteIDsEnum.Contains(p.Pre_RolePremSetID));

                    var addItems = Item.Items.Where(p => p.Pre_RolePremSetID == null);
                    #endregion
                    
                    //return null;

                    #region (3)删除
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);
                        var sql = string.Format("DELETE [dbo].[Pre_RolePremSet] WHERE  Pre_RolePremSetID IN({0})", deleteIDs);
                        var resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region 更新
                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Pre_RolePremSet_Domain();
                        var operCode = "Pre_RolePremSet.EditSave";
                        var resptemp = domain.ExecuteEnums(operCode);
                    }
                    #endregion

                    #region 添加
                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Pre_RolePremSet_Domain();
                        var operCode = "Pre_RolePremSet.AddSave";
                        var resptemp = domain.ExecuteEnums(operCode);
                    }
                    #endregion

                    scope.Complete();
                    SoftProjectAreaEntityDomain.Pre_RolePremSetAll_Clare();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    scope.Dispose();
                }
            }

            return resp;
        }

        #region 缓存

        #region 所有角色权限：保存角色时清空

        /// <summary>
        /// 所有角色权限管理：缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_RolePremSet_All()
        {
            StringBuilder sbSql = new StringBuilder();
            //sbSql.AppendLine("SELECT *  FROM [dbo].[V_AllPrem]");
            sbSql.AppendLine("SELECT *  FROM [dbo].[Pre_RolePremSet] A ");
            var sql = sbSql.ToString();
            var resp = Query16(sql);

            return resp;
        }


        static List<SoftProjectAreaEntity> _Pre_RolePremSetAll = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Pre_RolePremSetAll
        {
            get
            {
                if (_Pre_RolePremSetAll.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Pre_RolePremSetAll = domain.Pre_RolePremSet_All().Items;
                }
                return _Pre_RolePremSetAll;
            }
        }

        public static void Pre_RolePremSetAll_Clare()
        {
            _Pre_RolePremSetAll = new List<SoftProjectAreaEntity>();
        }

        #endregion

        #endregion

        ///// <summary>
        ///// 所有权限按钮：缓存
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Pre_RolePremSet_PremSetBtnsAll()
        //{
        //    StringBuilder sbSql = new StringBuilder();
        //    sbSql.AppendLine("SELECT *  ");
        //    sbSql.AppendLine("FROM [dbo].[V_Design_PremSet0510] A ");
        //    sbSql.AppendLine("JOIN Design_PremSetBtn B ON A.Sys_PremSetID=-B.Design_PremSetID  ");
        //    var sql = sbSql.ToString();
        //    var resp = Query16(sql);
        //    return resp;
        //}

        ///// <summary>
        ///// 所有权限Action：缓存
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Pre_RolePremSet_PremSetActionsAll()
        //{
        //    StringBuilder sbSql = new StringBuilder();
        //    sbSql.AppendLine("SELECT C.ActionPath,B.Design_ModularOrFunBtnID,A.* ");
        //    sbSql.AppendLine("FROM [dbo].[V_Design_PremSet0510] A ");
        //    sbSql.AppendLine("JOIN Design_PremSetBtn B ON A.Sys_PremSetID=-B.Design_PremSetID  ");
        //    sbSql.AppendLine("JOIN V_Design_ModularOrFunBtnControll0510 C ON B.Design_ModularOrFunBtnID=C.Design_ModularOrFunBtnID");

        //    var sql = sbSql.ToString();
        //    var resp = Query16(sql);
        //    return resp;
        //}
    }
}
