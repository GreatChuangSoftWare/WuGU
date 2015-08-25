using Framework.Web.Mvc;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftProject.Domain
{
    public delegate string Call(string val, string NameCn, SoftProjectAreaEntity item);

    public delegate string CallDrop(string val, string NameCn, SoftProjectAreaEntity item,int EditOrTable=1);

    
    public delegate List<SoftProjectAreaEntity> LoginModulerBtn(int Pre_UserID, string ModularOrFunCode, int? OperPos);

    public delegate List<SoftProjectAreaEntity> LoginModulerMenu(int Pre_UserID);

    public partial class ProjectCache
    {
        public ProjectCache()
        {
            //QueryHtmlDropDownLists.Add("Pre_RoleID", QueryHtmlDropDownList_Pre_RoleID);
            //QueryHtmlDropDownLists.Add("Comp_RoleID",QueryHtmlDropDownList_Comp_RoleID);
        }

        #region 登录人模块菜单

        static Dictionary<int, LoginModulerMenu> _LoginModulerMenus = new Dictionary<int, LoginModulerMenu>();

        public static Dictionary<int, LoginModulerMenu> LoginModulerMenus
        {
            get
            {
                return _LoginModulerMenus;
            }
        }

        public static void LoginModulerMenuss_Clear()
        {
            _LoginModulerMenus = new Dictionary<int, LoginModulerMenu>();
        }

        #endregion

        #region 登录人模块按钮

        static Dictionary<int, LoginModulerBtn> _LoginModulerBtns = new Dictionary<int, LoginModulerBtn>();

        public static Dictionary<int, LoginModulerBtn> LoginModulerBtns
        {
            get
            {
                return _LoginModulerBtns;
            }
        }

        public static void LoginModulerBtns_Clear()
        {
            _LoginModulerBtns = new Dictionary<int, LoginModulerBtn>();
        }

        #endregion

        //#region 缓冲集合

        //public static Dictionary<string, List<SoftProjectAreaEntity>> _Caches = new Dictionary<string, List<SoftProjectAreaEntity>>();

        //public static Dictionary<string, List<SoftProjectAreaEntity>> Caches
        //{
        //    get
        //    {
        //        return _Caches;
        //    }
        //}

        //public static void Caches_Clear()
        //{
        //    _Caches = new Dictionary<string, List<SoftProjectAreaEntity>>();
        //}

        //#endregion

        #region 查询下拉列表框

        static Dictionary<string, Call> _QueryHtmlDropDownLists = new Dictionary<string, Call>();

        public static Dictionary<string, Call> QueryHtmlDropDownLists
        {
            get
            {
                return _QueryHtmlDropDownLists;
            }
        }

        public static void QueryHtmlDropDownLists_Clear()
        {
            _QueryHtmlDropDownLists = new Dictionary<string, Call>();
        }

        #endregion

        #region 编辑下拉列表框

        static Dictionary<string, Call> _HtmlDropDownLiss = new Dictionary<string, Call>();

        public static Dictionary<string, Call> HtmlDropDownLiss
        {
            get
            {
                return _HtmlDropDownLiss;
            }
        }

        public static void HtmlDropDownLiss_Clear()
        {
            _HtmlDropDownLiss = new Dictionary<string, Call>();
        }

        #endregion

        #region 编辑多选下拉列表框
        //DropDownListMultiSelect
        static Dictionary<string, Call> _HtmlDropDownListMultiSelects = new Dictionary<string, Call>();

        public static Dictionary<string, Call> HtmlDropDownListMultiSelects
        {
            get
            {
                return _HtmlDropDownListMultiSelects;
            }
        }

        public static void HtmlDropDownListMultiSelects_Clear()
        {
            _HtmlDropDownListMultiSelects = new Dictionary<string, Call>();
        }

        #endregion

        #region 查询条件--下拉列表树

        static Dictionary<string, Call> _QueryHtmlDropTrees = new Dictionary<string, Call>();

        public static Dictionary<string, Call> QueryHtmlDropTrees
        {
            get
            {
                return _QueryHtmlDropTrees;
            }
        }

        public static void QueryHtmlDropTrees_Clear()
        {
            _QueryHtmlDropTrees = new Dictionary<string, Call>();
        }

        #endregion

        #region 编辑页面--下拉列表树

        static Dictionary<string, Call> _HtmlDropTrees = new Dictionary<string, Call>();

        public static Dictionary<string, Call> HtmlDropTrees
        {
            get
            {
                return _HtmlDropTrees;
            }
        }

        public static void HtmlDropTrees_Clear()
        {
            _HtmlDropTrees = new Dictionary<string, Call>();
        }

        #endregion

        #region Jquery树

        static Dictionary<string, Call> _JqTreeN = new Dictionary<string, Call>();

        public static Dictionary<string, Call> JqTreeNs
        {
            get
            {
                return _JqTreeN;
            }
        }

        public static void JqTreeNs_Clear()
        {
            _JqTreeN = new Dictionary<string, Call>();
        }

        #endregion


        //<summary>
        //公司：模块按钮
        //</summary>
        //<param name="Pre_UserID"></param>
        //<param name="ModularOrFunCode">页面</param>
        //<param name="OperPos">位置</param>
        //<returns></returns>
        public static List<SoftProjectAreaEntity> ModulerBtns(string ModularOrFunCode, int? OperPos)
        {
            var items = (from m in Design_ModularOrFunRefBtns
                         where m.ModularOrFunCode == ModularOrFunCode && m.OperPos == OperPos && m.bValid == 1
                         select m).Distinct().OrderBy(p => p.Sort).ToList();
            return items;
        }

        /// <summary>
        /// 判断加盟是否具有权限码
        /// </summary>
        /// <param name="Pre_UsersID"></param>
        /// <param name="PremCodes"></param>
        /// <returns></returns>
        public static bool Fra_FranchiseeHasActionPath(int Fra_FranchiseeID, string ActionPath)
        {
            ////(1)判断是否需要验证
            //var temp = from p in Sys_PremSetsAll
            //           where p.PremActionPath == ActionPath
            //           select p;
            //if (temp.Count() == 0)
            //    return true;

            ////(2)验证
            //#region 测试代码
            //#endregion
            //var items = (from p in Fra_FranchiseeRoleAll
            //             join o in Fra_RolePremSetAll on p.Fra_RoleID equals o.Fra_RoleID
            //             join m in Sys_PremSetsAll on o.Sys_PremSetID equals m.Sys_PremSetID //菜单
            //             where p.Fra_FranchiseeID == Fra_FranchiseeID && ActionPath == m.PremActionPath
            //             select m).ToList();
            //var count = items.Count();
            //return count > 0;
            return true;
        }

        //<summary>
        //公司：是否为菜单
        //</summary>
        //<param name="Pre_UserID"></param>
        //<param name="ModularOrFunCode">页面</param>
        //<param name="OperPos">位置</param>
        //<returns></returns>
        public static string HasMenu(string ActionPath)
        {
            var currMenu = "";
            var items = from m in Design_ModularOrFuns
                        where m.BMenu == 1 && m.ActionPath == ActionPath
                        select m;
            if (items.Count() > 0)
            {
                var Design_ModularOrFun = items.First();
                currMenu ="/"+ Design_ModularOrFun.Design_ModularOrFunID.ToString();
                if (Design_ModularOrFun.ParentPremID != 0)
                    currMenu = "/" + Design_ModularOrFun.ParentPremID + "/" + Design_ModularOrFun.Design_ModularOrFunID;
            }
            //var items = ProjectCache.Design_ModularOrFunRefBtns.Where(p => p.ModularOrFunCode == ModularOrFunCode && (p.OperPos == OperPos)).OrderBy(p => p.Sort).ToList();
            return currMenu;
        }



    }

}