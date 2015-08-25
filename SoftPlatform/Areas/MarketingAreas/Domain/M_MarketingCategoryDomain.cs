
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
    /// 业务层：M_MarketingCategory(营销类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 缓存、界面元素

        public void M_MarketingCategory_AddCache()
        {
            #region 更新：用户缓存

            ModularOrFunCode = "DocArea.M_MarketingCategory.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.M_MarketingCategorys.Add(resp.Item);

            #endregion
        }

        public void M_MarketingCategory_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "DocArea.M_MarketingCategory.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var M_MarketingCategory = SoftProjectAreaEntityDomain.M_MarketingCategorys.Where(p => p.M_MarketingCategoryID == Item.M_MarketingCategoryID).FirstOrDefault();
            
            SoftProjectAreaEntityDomain.M_MarketingCategorys.Remove(M_MarketingCategory);
            SoftProjectAreaEntityDomain.M_MarketingCategorys.Add(resp.Item);
            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase M_MarketingCategory_GetAll()
        {
            #region 树数据

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            //domain.Sys_HOperControl = new SoftProjectAreaEntity
            //{
            //    DBTSql = "SELECT * FROM M_MarketingCategory ",
            //    DBOperType = 16,
            //    DBSelectResultType = 2,
            //};

            //OperCode = "M_MarketingCategory.GetAll";
            //var resp = domain.Execute();
            //var resp = Execute();
            //resp.Item.M_MarketingCategorys = resptrees.Items;

            string sql = "SELECT * FROM V_M_MarketingCategory ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 营销类别

        static List<SoftProjectAreaEntity> _M_MarketingCategorys = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> M_MarketingCategorys
        {
            get
            {
                if (_M_MarketingCategorys.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _M_MarketingCategorys = domain.M_MarketingCategory_GetAll().Items;
                }
                return _M_MarketingCategorys;
            }
        }

        public static void M_MarketingCategory_Clear()
        {
            _M_MarketingCategorys = new List<SoftProjectAreaEntity>();
        }

        #endregion

        /// <summary>
        /// 查询下拉树--公司营销类型父节点ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_ParentM_MarketingCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 营销类别

            //var Pre_Company=HttpContext.Current.Session["LoginInfo"] as  SoftProjectAreaEntity;

            //var Items = SoftProjectAreaEntityDomain.M_MarketingCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.M_MarketingCategorys;
            var tt = new SelectTreeList(Items, "0", "MarketingCategoryName", "M_MarketingCategoryID", "ParentM_MarketingCategoryID", "M_MarketingCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "ParentM_MarketingCategoryID___equal", tt, "==营销类别==");
            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询下拉树--营销类型ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_M_MarketingCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 营销类型
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;

            //var Items = SoftProjectAreaEntityDomain.M_MarketingCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.M_MarketingCategorys;
            var tt = new SelectTreeList(Items, "0", "MarketingCategoryName", "M_MarketingCategoryID", "ParentM_MarketingCategoryID", "M_MarketingCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "M_MarketingCategoryID___equal", tt, "==营销类别==");

            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面下拉树--营销类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_ParentM_MarketingCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.M_MarketingCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.M_MarketingCategorys;
            var tt = new SelectTreeList(Items, "0", "MarketingCategoryName", "M_MarketingCategoryID", "ParentM_MarketingCategoryID", "M_MarketingCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.ParentM_MarketingCategoryID", tt, "==营销类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// 编辑页面下拉树--营销类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_M_MarketingCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.M_MarketingCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.M_MarketingCategorys;
            var tt = new SelectTreeList(Items, "0", "MarketingCategoryName", "M_MarketingCategoryID", "ParentM_MarketingCategoryID", "M_MarketingCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.M_MarketingCategoryID", tt, "==营销类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// Jquery树
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string JqTreeNs_M_MarketingCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Itemst = SoftProjectAreaEntityDomain.M_MarketingCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            var Itemst = SoftProjectAreaEntityDomain.M_MarketingCategorys;
            var treeList = new TreeList(Itemst, "0", "MarketingCategoryName", "M_MarketingCategoryID", "ParentM_MarketingCategoryID", "", "", "M_MarketingCategoryID", "", "");
                var str = HtmlHelpersProject.JqTreeN(null, "M_MarketingCategoryID", treeList, "", 2);
                return str.ToString();
        }

        #endregion
    }
}
