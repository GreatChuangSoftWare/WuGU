using Framework.Web.Mvc;
using SoftProject.CellModel;
using SoftProject.Domain;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;
using Framework.Core;

namespace SoftPlatform.Controllers
{
    public class AuthorizationAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AuthorizationAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AuthorizationAreas_default",
                "AuthorizationAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            //登录人菜单
            //ProjectCache.LoginModulerMenus.Add(1, LoginModulerMenu);

            //登录人模块按钮
            ProjectCache.LoginModulerBtns.Add(1, LoginModulerBtns);

            #region 角色：表单元素

            ProjectCache.QueryHtmlDropDownLists.Add("Pre_RoleID", SoftProjectAreaEntityDomain.QueryHtmlDropDownList_Pre_RoleID);
            ProjectCache.HtmlDropDownLiss.Add("Pre_RoleID", SoftProjectAreaEntityDomain.HtmlDropDownLiss_Pre_RoleID);

            #endregion

            //经营项目
            ProjectCache.HtmlDropDownListMultiSelects.Add("OperatingItemIDs", SoftProjectAreaEntityDomain.DropDownListMultiSelect_OperatingItemIDs);

            var load7 = SoftProjectAreaEntityDomain.Pre_UserRoleAll;
            var load8 = SoftProjectAreaEntityDomain.Pre_RolePremSetAll;
        }

        //<summary>
        //公司：用户页面按钮
        //</summary>
        //<param name="Pre_UserID"></param>
        //<param name="ModularOrFunCode">页面</param>
        //<param name="OperPos">位置</param>
        //<returns></returns>
        public static List<SoftProjectAreaEntity> LoginModulerBtns(int Pre_UserID, string ModularOrFunCode, int? OperPos)
        {
            var items = (from p in SoftProjectAreaEntityDomain.Pre_UserRoleAll
                         join o in SoftProjectAreaEntityDomain.Pre_RolePremSetAll on p.Pre_RoleID equals o.Pre_RoleID
                         join m in ProjectCache.Design_ModularOrFunRefBtns on o.Sys_PremSetID equals m.Design_PremSetID
                         where m.ModularOrFunCode == ModularOrFunCode && m.OperPos == OperPos && m.bValid == 1 && p.Pre_UserID == Pre_UserID
                         select m).Distinct().OrderBy(p => p.Sort).ToList();
            return items;
        }

        //<summary>
        //平台：获取菜单
        //</summary>
        //<param name="Pre_UserID"></param>
        //<param name="ModularOrFunCode">页面</param>
        //<param name="OperPos">位置</param>
        //<returns></returns>
        public static List<SoftProjectAreaEntity> LoginModulerMenu(int Pre_UserID, string MenuIdent=null)
        {
            var items = new List<SoftProjectAreaEntity>();
            if (MenuIdent == null)
            {
                 items = (from p in SoftProjectAreaEntityDomain.Pre_UserRoleAll
                             join o in SoftProjectAreaEntityDomain.Pre_RolePremSetAll on p.Pre_RoleID equals o.Pre_RoleID
                             join m in ProjectCache.Design_ModularOrFuns on o.Sys_PremSetID equals m.Design_ModularOrFunID
                             where p.Pre_UserID == Pre_UserID && m.BMenu == 1 && m.bValidModularOrFun != 0 && string.IsNullOrEmpty(m.MenuIdent)
                             select m).Distinct().OrderBy(p => p.Sort).ToList();
            }
            else
                items = (from p in SoftProjectAreaEntityDomain.Pre_UserRoleAll
                         join o in SoftProjectAreaEntityDomain.Pre_RolePremSetAll on p.Pre_RoleID equals o.Pre_RoleID
                         join m in ProjectCache.Design_ModularOrFuns on o.Sys_PremSetID equals m.Design_ModularOrFunID
                         where p.Pre_UserID == Pre_UserID && m.BMenu == 1 && m.bValidModularOrFun != 0 && m.MenuIdent == MenuIdent
                         select m).Distinct().OrderBy(p => p.Sort).ToList();

            var aaa = ProjectCache.Design_ModularOrFuns.Where(p => !string.IsNullOrEmpty(p.MenuIdent));
            //var items = ProjectCache.Design_ModularOrFunRefBtns.Where(p => p.ModularOrFunCode == ModularOrFunCode && (p.OperPos == OperPos)).OrderBy(p => p.Sort).ToList();
            return items;
        }

        #region 请求的url验证

        /// <summary>
        /// 判断用户是否具有权限码
        /// </summary>
        /// <param name="Pre_UsersID"></param>
        /// <param name="PremCodes"></param>
        /// <returns></returns>
        public static bool UserHasActionPath(int Pre_UserID, string ActionPath)
        {
            ////return true;
            ////if (PremCodesTemp.Where(p => p == PremCodes[0]).Count() == 0)
            ////    PremCodesTemp.Add(PremCodes[0]);
            //////return true;
            ////(1)判断是否需要验证
            //var temp = from p in Sys_PremSetsAll
            //           where p.PremActionPath == ActionPath
            //           select p;
            //if (temp.Count() == 0)
            //    return true;

            ////(2)验证
            //#region 测试代码
            ////var items = new List<int>();
            ////(from p in Sys_PremSetsAll
            //// join o in Pre_RolesPremCodes on p.Pre_RolesID equals o.Pre_RolesID
            //// join m in Sys_PremCodes on o.Sys_PremCodeID equals m.Sys_PremCodeID
            //// where p.Pre_UsersID == Pre_UsersID && PremCodes.Contains(m.PremCode)
            //// select o).ToList();

            ////var itemst1 = (from p in Pre_UserRoleAll
            ////               join o in Pre_RolePremSetAll on p.Pre_RoleID equals o.Pre_RoleID
            ////               join m in _Sys_PremSetsAll on o.Sys_PremSetID equals m.Sys_PremSetID //菜单
            ////               select o).ToList();

            ////var itemst2 = (from p in Pre_UserRoleAll
            ////               join o in Pre_RolePremSetAll on p.Pre_RoleID equals o.Pre_RoleID
            ////               join m in _Sys_PremSetsAll on o.Sys_PremSetID equals m.Sys_PremSetID //菜单
            ////               where ActionPath == m.PremActionPath
            ////               select o
            ////              ).ToList();
            //#endregion
            //var items = (from p in Pre_UserRoleAll
            //             join o in Pre_RolePremSetAll on p.Pre_RoleID equals o.Pre_RoleID
            //             join m in _Sys_PremSetsAll on o.Sys_PremSetID equals m.Sys_PremSetID //菜单
            //             where p.Pre_UserID == Pre_UserID && ActionPath == m.PremActionPath
            //             select m).ToList();
            //var count = items.Count();
            return true;// count > 0;
        }

        #endregion

    }
}
