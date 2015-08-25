
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
using System.Web;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：T_ToolCategory(工具类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 缓存、界面元素

        public void T_ToolCategory_AddCache()
        {
            #region 更新：用户缓存

            ModularOrFunCode = "DocArea.T_ToolCategory.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.T_ToolCategorys.Add(resp.Item);

            #endregion
        }

        public void T_ToolCategory_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "DocArea.T_ToolCategory.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var T_ToolCategory = SoftProjectAreaEntityDomain.T_ToolCategorys.Where(p => p.T_ToolCategoryID == Item.T_ToolCategoryID).FirstOrDefault();
            
            SoftProjectAreaEntityDomain.T_ToolCategorys.Remove(T_ToolCategory);
            SoftProjectAreaEntityDomain.T_ToolCategorys.Add(resp.Item);
            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase T_ToolCategory_GetAll()
        {
            #region 树数据

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            //domain.Sys_HOperControl = new SoftProjectAreaEntity
            //{
            //    DBTSql = "SELECT * FROM T_ToolCategory ",
            //    DBOperType = 16,
            //    DBSelectResultType = 2,
            //};

            //OperCode = "T_ToolCategory.GetAll";
            //var resp = domain.Execute();
            //var resp = Execute();
            //resp.Item.T_ToolCategorys = resptrees.Items;

            string sql = "SELECT * FROM V_T_ToolCategory ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 工具类别

        static List<SoftProjectAreaEntity> _T_ToolCategorys = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> T_ToolCategorys
        {
            get
            {
                if (_T_ToolCategorys.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _T_ToolCategorys = domain.T_ToolCategory_GetAll().Items;
                }
                return _T_ToolCategorys;
            }
        }

        public static void T_ToolCategory_Clear()
        {
            _T_ToolCategorys = new List<SoftProjectAreaEntity>();
        }

        #endregion

        /// <summary>
        /// 查询下拉树--公司工具类型父节点ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_ParentT_ToolCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 工具类别

            var Pre_Company=HttpContext.Current.Session["LoginInfo"] as  SoftProjectAreaEntity;

            var Items = SoftProjectAreaEntityDomain.T_ToolCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            //List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.T_ToolCategorys;
            var tt = new SelectTreeList(Items, "0", "ToolCategoryName", "T_ToolCategoryID", "ParentT_ToolCategoryID", "T_ToolCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "ParentT_ToolCategoryID___equal", tt, "==工具类别==");
            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询下拉树--工具类型ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_T_ToolCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 工具类型
            var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;

            var Items = SoftProjectAreaEntityDomain.T_ToolCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            //List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.T_ToolCategorys;
            var tt = new SelectTreeList(Items, "0", "ToolCategoryName", "T_ToolCategoryID", "ParentT_ToolCategoryID", "T_ToolCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "T_ToolCategoryID___equal", tt, "==工具类别==");

            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面下拉树--工具类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_ParentT_ToolCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.T_ToolCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.T_ToolCategorys;
            var tt = new SelectTreeList(Items, "0", "ToolCategoryName", "T_ToolCategoryID", "ParentT_ToolCategoryID", "T_ToolCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.ParentT_ToolCategoryID", tt, "==工具类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// 编辑页面下拉树--工具类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_T_ToolCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.T_ToolCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.T_ToolCategorys;
            var tt = new SelectTreeList(Items, "0", "ToolCategoryName", "T_ToolCategoryID", "ParentT_ToolCategoryID", "T_ToolCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.T_ToolCategoryID", tt, "==工具类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// Jquery树
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string JqTreeNs_T_ToolCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            var Itemst = SoftProjectAreaEntityDomain.T_ToolCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            //var Itemst = SoftProjectAreaEntityDomain.T_ToolCategorys;
                var treeList = new TreeList(Itemst, "0", "ToolCategoryName", "T_ToolCategoryID", "ParentT_ToolCategoryID", "", "", "T_ToolCategoryID", "", "");
                var str = HtmlHelpersProject.JqTreeN(null, "T_ToolCategoryID", treeList, "", 2);
                return str.ToString();
        }

        #endregion
    }
}
