
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Pre_Role(角色管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 添加保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_Role_AddSave()
        {
            var resp = new MyResponseBase();

            resp = ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                Item.Pre_RoleID = resp.Item.Pre_RoleID;
                Pre_RolePremSet_AddUpdateDeleteSaves();
            }));
            Pre_Role_AddCache();//更新缓存
            return resp;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_Role_EditSave()
        {
            var resp = new MyResponseBase();

            resp = ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                Pre_RolePremSet_AddUpdateDeleteSaves();
            }));
            Pre_Role_UpdateCache();//更新缓存
            return resp;
        }

        #region 缓存、界面元素

        public void Pre_Role_AddCache()
        {
            #region 添加：角色缓存

            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.Pre_Roles.Add(resp.Item);

            #endregion
        }

        public void Pre_Role_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var Pre_Role = SoftProjectAreaEntityDomain.Pre_Roles.Where(p => p.Pre_RoleID == Item.Pre_RoleID).FirstOrDefault();

            SoftProjectAreaEntityDomain.Pre_Roles.Remove(Pre_Role);
            SoftProjectAreaEntityDomain.Pre_Roles.Add(resp.Item);

            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_Role_GetAll()
        {
            string sql = "SELECT * FROM V_Pre_Role ";
            var resp = Query16(sql, 2);
            return resp;
        }

        #region 角色

        static List<SoftProjectAreaEntity> _Pre_Roles = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Pre_Roles
        {
            get
            {
                if (_Pre_Roles.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Pre_Roles = domain.Pre_Role_GetAll().Items;
                }
                return _Pre_Roles;
            }
        }

        public static void Pre_Roles_Clear()
        {
            _Pre_Roles = new List<SoftProjectAreaEntity>();
        }

        #endregion

        public static string QueryHtmlDropDownList_Pre_RoleID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Roles = ProjectCache.Caches["Pre_RoleID"];
            var Pre_RolesTemp = Pre_Roles.Where(p => p.LoginCategoryID == item.LoginCategoryID);
            var str = HtmlHelpers.DropDownList(null, "Pre_RoleID___equal", Pre_RolesTemp, "Pre_RoleID", "RoleName", val, "", "==" + NameCn + "==");
            var strDrop = str.ToString();
            return strDrop;
        }

        public static string HtmlDropDownLiss_Pre_RoleID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //必须知道是公司、企业、顾客功能，从而进行过滤
            var Pre_RolesTemp = Pre_Roles.Where(p => p.LoginCategoryID == item.LoginCategoryID);
            var str = HtmlHelpers.DropDownList(null, "Item.Pre_RoleID", Pre_RolesTemp, "Pre_RoleID", "RoleName", val, "");
            return str.ToString();
        }

        #endregion
    }

    /// <summary>
    /// 业务层：Pre_RoleDomain
    /// </summary>
    //public partial class SoftProjectAreaEntityDomain
    //{
    //    #region 公共部分
    //    public void Pre_Role_Domain()
    //    {
    //        PKFields = new List<string> { "Pre_RoleID" };
    //        TableName = "Pre_Role";
    //    }

    //    /// <summary>
    //    /// 主键验证
    //    /// </summary>
    //    /// <returns></returns>
    //    public void Pre_Role_PKCheck()
    //    {
    //        if (Item.Pre_RoleID == null)
    //        {
    //            throw new Exception("角色主键不能为空！");
    //        }
    //    }

    //    public MyResponseBase Pre_Role_ByID()
    //    {
    //        Pre_Role_PKCheck();
    //        SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
    //        {
    //            DBTSql = string.Format(";SELECT * FROM [dbo].[V_Pre_Role] A WHERE Pre_RoleID={0} ", Item.Pre_RoleID),
    //            DBOperType = 16,
    //            DBSelectResultType = 4,
    //        };
    //        Sys_HOperControl = hOperControl;

    //        var resp = Execute();
    //        return resp;
    //    }

    //    #endregion

    //    /// <summary>
    //    /// 查询
    //    /// </summary>
    //    /// <returns></returns>
    //    public MyResponseBase Pre_Role_Index(int SelectType = 6)
    //    {
    //        if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
    //        {
    //            PageQueryBase.RankInfo = "SortID|1";
    //        }
    //        //StringBuilder sbsql =new StringBuilder( ";WITH T1000 AS ");
    //        //sbsql.AppendLine("(");
    //        //sbsql.AppendLine("  SELECT  A.*,B.DText StatusName ");
    //        //sbsql.AppendLine("  FROM Pre_Role A  ");
    //        //sbsql.AppendLine("  JOIN Sys_Dict B ON A.Status=B.DValue");
    //        //sbsql.AppendLine("WHERE 1=1  sqlplaceholder )");

    //        SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
    //        {
    //            DBTSql = ";WITH T1000 AS (SELECT * FROM V_Pre_Role A  WHERE 1=1  sqlplaceholder )",
    //            DBOperType = 8,//排序分页
    //            SelectSubType = SelectType,
    //            DBSelectResultType = 2,
    //            EqualQueryParam = ""
    //        };
    //        Sys_HOperControl = hOperControl;

    //        var resp = Execute();
    //        //resp = Execute();
    //        resp.Querys = Querys;
    //        resp.Item = Item;
    //        return resp;
    //    }

    //    public MyResponseBase Pre_Role_ByIDEdit()
    //    {
    //        //(1)检查主键ID
    //        Pre_Role_PKCheck();

    //        //(2)获取角色信息
    //        var resp = Pre_Role_ByID();

    //        //(3)获取所有角色权限:左连接
    //        var resptemp = Pre_RolePremSet_ByLoginCategoryAndRoleID();
    //        resp.Item.Pre_RolePremSets = resptemp.Items;
    //        return resp;
    //    }

    //    public MyResponseBase Pre_Role_Add()
    //    {
    //        var resp=Default();
    //        resp.Item = Item;
    //        //(1)获取所有角色权限
    //        var resptemp = Sys_PremSet_ByLoginCategory();
            
    //        resp.Item.Pre_RolePremSets = resptemp.Items;
    //        return resp;
    //    }

    //    public MyResponseBase Pre_Role_AddSave()
    //    {
    //        #region (1)添加：角色名称不能重复
    //        SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
    //        {
    //            DBTSql = string.Format("SELECT COUNT(*) FROM [dbo].[Pre_Role] WHERE RoleName='{0}'", Item.RoleName),
    //            DBOperType = 16,
    //            DBSelectResultType = 1,
    //        };
    //        Sys_HOperControl = hOperControl;
    //        var resptemp = Execute();
    //        if (Convert.ToInt32(resptemp.Obj) > 0)
    //            throw new Exception(string.Format("角色名称：【{0}】已经存在", Item.RoleName));
    //        #endregion

    //        Pre_Role_Domain();

    //        #region (2)添加角色
    //        using (var scope = new TransactionScope())
    //        {
    //            try
    //            {
    //                #region (1)添加角色
    //                hOperControl = new SoftProjectAreaEntity
    //                {
    //                    DBOperType = 1,
    //                    DBSelectResultType = 1,
    //                    DBFieldVals = "RoleName,Status,SortID,LoginCategory"
    //                };
    //                Sys_HOperControl = hOperControl;
    //                var resp = Execute();

    //                #endregion
    //                #region (2)添加角色-权限集关联
    //                if (Item.Pre_RolePremSets != null && Item.Pre_RolePremSets.Count > 0)
    //                {
    //                    Item.Pre_RolePremSets.ForEach(p => { p.Pre_RoleID = resp.Item.Pre_RoleID; });
    //                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = Item.Pre_RolePremSets };
    //                    domain.Pre_RolePremSet_AddSave();
    //                }
    //                #endregion
    //                //清空缓存：用户-角色-权限
    //                //ProjectCache.ClearPremByRoleChange();
    //                scope.Complete();
    //            }
    //            catch (Exception ex)
    //            {
    //                resp.RespAttachInfo.ValidationErrors.ErrorMessage = ex.Message;
    //            }
    //            finally
    //            {
    //                scope.Dispose();
    //            }
    //        }
    //        #endregion

    //        return resp;
    //    }

    //    public MyResponseBase Pre_Role_EditSave()
    //    {
    //        //(1)检查主键ID
    //        Pre_Role_PKCheck();

    //        #region (2)修改角色不能重复
    //        SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
    //        {
    //            DBTSql = string.Format("SELECT COUNT(*) FROM [dbo].[Pre_Role] WHERE RoleName='{0}' AND Pre_RoleID!={1}", Item.RoleName, Item.Pre_RoleID),
    //            DBOperType = 16,
    //            DBSelectResultType = 1,
    //        };
    //        Sys_HOperControl = hOperControl;
    //        var resptemp = Execute();
    //        if (Convert.ToInt32(resptemp.Obj) > 0)
    //            throw new Exception(string.Format("角色名称：【{0}】已经存在", Item.RoleName));
    //        #endregion

    //        Pre_Role_Domain();

    //        #region (2)修改角色
    //        using (var scope = new TransactionScope())
    //        {
    //            try
    //            {
    //                #region (1)修改角色
    //                hOperControl = new SoftProjectAreaEntity
    //                {
    //                    DBOperType = 2,
    //                    DBSelectResultType = 1,
    //                    DBFieldVals = "RoleName,Status,SortID,LoginCategory"
    //                };
    //                Sys_HOperControl = hOperControl;
    //                var resp = Execute();

    //                #endregion

    //                #region (3)根据角色查询所有角色权限集
    //                hOperControl = new SoftProjectAreaEntity
    //                {
    //                    DBTSql = string.Format("SELECT * FROM [dbo].[Pre_RolePremSet] WHERE  Pre_RoleID={0}", Item.Pre_RoleID),
    //                    DBOperType = 16,
    //                    DBSelectResultType = 2,
    //                };
    //                Sys_HOperControl = hOperControl;
    //                resptemp = Execute();
    //                #endregion

    //                #region (2)角色--权限集数据整理
    //                Item.Pre_RolePremSets.ForEach(p =>
    //                { p.Pre_RoleID = Item.Pre_RoleID; });

    //                var deleteIDsEnum = (from p in resptemp.Items select p.Pre_RolePremSetID).Except(from o in Item.Pre_RolePremSets select o.Pre_RolePremSetID);
    //                var updateItems = Item.Pre_RolePremSets.Where(p => p.Pre_RolePremSetID != null);
    //                var addItems = Item.Pre_RolePremSets.Where(p => p.Pre_RolePremSetID == null);
    //                #endregion

    //                #region (4)删除元素:执行删除，通过In进行删除
    //                //需要写专门语句？delete xxx where ID IN(XXX)
    //                if (deleteIDsEnum.Count() > 0)
    //                {
    //                    var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()

    //                    hOperControl = new SoftProjectAreaEntity
    //                    {
    //                        DBTSql = string.Format("DELETE [dbo].[Pre_RolePremSet] WHERE  Pre_RolePremSetID IN({0})", deleteIDs),
    //                        DBOperType = 16,
    //                        DBSelectResultType = 1,
    //                    };
    //                    Sys_HOperControl = hOperControl;
    //                    resptemp = Execute();
    //                }
    //                #endregion

    //                #region (5)更新角色-权限集关联

    //                if (updateItems.Count() > 0)
    //                {
    //                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
    //                    domain.Pre_RolePremSet_EditSave();
    //                }

    //                #endregion

    //                #region (6)添加

    //                if (addItems.Count() > 0)
    //                {
    //                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
    //                    domain.Pre_RolePremSet_AddSave();
    //                }

    //                #endregion
    //                //清空缓存：用户-角色-权限
    //                //ProjectCache.ClearPremByRoleChange();
    //                scope.Complete();
    //            }
    //            catch (Exception ex)
    //            {
    //                resp.RespAttachInfo.ValidationErrors.ErrorMessage = ex.Message;
    //            }
    //            finally
    //            {
    //                scope.Dispose();
    //            }
    //        }
    //        #endregion

    //        return resp;
    //    }


    //}

}
