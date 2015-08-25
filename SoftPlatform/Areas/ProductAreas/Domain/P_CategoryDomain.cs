
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
    /// 业务层：P_Category(商品类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 缓存、界面元素
        
        public void P_Category_AddCache()
        {
            #region 更新：用户缓存

            ModularOrFunCode = "ProductAreas.P_Category.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.P_Categorys.Add(resp.Item);

            #endregion
        }

        public void P_Category_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "ProductAreas.P_Category.Detail";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var P_Category = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.P_CategoryID == Item.P_CategoryID).FirstOrDefault();
            SoftProjectAreaEntityDomain.P_Categorys.Remove(P_Category);
            SoftProjectAreaEntityDomain.P_Categorys.Add(resp.Item);
            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase P_Category_GetAll()
        {
            #region 树数据

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            //domain.Sys_HOperControl = new SoftProjectAreaEntity
            //{
            //    DBTSql = "SELECT * FROM P_Category ",
            //    DBOperType = 16,
            //    DBSelectResultType = 2,
            //};

            //OperCode = "P_Category.GetAll";
            //var resp = domain.Execute();
            //var resp = Execute();
            //resp.Item.P_Categorys = resptrees.Items;

            string sql = "SELECT * FROM V_P_Category ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 商品类别

        static List<SoftProjectAreaEntity> _P_Categorys = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> P_Categorys
        {
            get
            {
                if (_P_Categorys.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _P_Categorys = domain.P_Category_GetAll().Items;
                }
                return _P_Categorys;
            }
        }

        public static void P_Category_Clear()
        {
            _P_Categorys = new List<SoftProjectAreaEntity>();
        }

        #endregion

        /// <summary>
        /// 查询下拉树--公司商品类型父节点ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_ParentP_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 商品类别

            //var Pre_Company=HttpContext.Current.Session["LoginInfo"] as  SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.P_Categorys;
            var tt = new SelectTreeList(Items, "0", "PCategoryName", "P_CategoryID", "ParentP_CategoryID", "P_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "ParentP_CategoryID___equal", tt, "==商品类别==");
            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询下拉树--商品类型ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_P_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 商品类型
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.P_Categorys;
            var tt = new SelectTreeList(Items, "0", "PCategoryName", "P_CategoryID", "ParentP_CategoryID", "P_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "P_CategoryID___equal", tt, "==商品类别==");

            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面下拉树--商品类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_ParentP_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.P_Categorys;
            var tt = new SelectTreeList(Items, "0", "PCategoryName", "P_CategoryID", "ParentP_CategoryID", "P_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.ParentP_CategoryID", tt, "==商品类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// 编辑页面下拉树--商品类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_P_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.P_Categorys;
            var tt = new SelectTreeList(Items, "0", "PCategoryName", "P_CategoryID", "ParentP_CategoryID", "P_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.P_CategoryID", tt, "==商品类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// Jquery树
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string JqTreeNs_P_CategoryID(string paramNameType, string NameCn, SoftProjectAreaEntity item)
        {
            var TreeQueryType = Convert.ToInt32(paramNameType);
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Itemst = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            var Itemst = SoftProjectAreaEntityDomain.P_Categorys;
            var treeList = new TreeList(Itemst, "0", "PCategoryName", "P_CategoryID", "ParentP_CategoryID", "", "", "P_CategoryID", "", "");
            var str = HtmlHelpersProject.JqTreeN(null, "P_CategoryID", treeList, "", TreeQueryType);
            return str.ToString();

            //var Itemst = ProjectCache.P_Categorys;
            //var treeList = new TreeList(Itemst, "0", "PCategoryName", "P_ProductCategoryID", "ParentProductCategoryID", "", "", "P_ProductCategoryID", "", "");
            //    var str = JqTreeN(helper, ID, treeList, "", paramNameType);
            //    return str;
        }

        #endregion
    }
}
