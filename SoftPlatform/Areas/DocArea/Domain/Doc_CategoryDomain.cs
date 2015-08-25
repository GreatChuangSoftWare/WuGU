
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
    /// 业务层：Doc_Category(文档类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 缓存、界面元素

        public void Doc_Category_AddCache()
        {
            #region 更新：用户缓存

            ModularOrFunCode = "DocArea.Doc_Category.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.Doc_Categorys.Add(resp.Item);

            #endregion
        }

        public void Doc_Category_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "DocArea.Doc_Category.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var Doc_Category = SoftProjectAreaEntityDomain.Doc_Categorys.Where(p => p.Doc_CategoryID == Item.Doc_CategoryID).FirstOrDefault();
            
            SoftProjectAreaEntityDomain.Doc_Categorys.Remove(Doc_Category);
            SoftProjectAreaEntityDomain.Doc_Categorys.Add(resp.Item);
            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Doc_Category_GetAll()
        {
            #region 树数据

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            //domain.Sys_HOperControl = new SoftProjectAreaEntity
            //{
            //    DBTSql = "SELECT * FROM Doc_Category ",
            //    DBOperType = 16,
            //    DBSelectResultType = 2,
            //};

            //OperCode = "Doc_Category.GetAll";
            //var resp = domain.Execute();
            //var resp = Execute();
            //resp.Item.Doc_Categorys = resptrees.Items;

            string sql = "SELECT * FROM V_Doc_Category ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 文档类别

        static List<SoftProjectAreaEntity> _Doc_Categorys = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Doc_Categorys
        {
            get
            {
                if (_Doc_Categorys.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Doc_Categorys = domain.Doc_Category_GetAll().Items;
                }
                return _Doc_Categorys;
            }
        }

        public static void Doc_Category_Clear()
        {
            _Doc_Categorys = new List<SoftProjectAreaEntity>();
        }

        #endregion

        /// <summary>
        /// 查询下拉树--公司文档类型父节点ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_ParentDoc_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 文档类别

            var Pre_Company=HttpContext.Current.Session["LoginInfo"] as  SoftProjectAreaEntity;

            var Items = SoftProjectAreaEntityDomain.Doc_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            //List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Doc_Categorys;
            var tt = new SelectTreeList(Items, "0", "DocCategoryName", "Doc_CategoryID", "ParentDoc_CategoryID", "Doc_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "ParentDoc_CategoryID___equal", tt, "==文档类别==");
            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询下拉树--文档类型ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_Doc_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 文档类型
            var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;

            var Items = SoftProjectAreaEntityDomain.Doc_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            //List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Doc_Categorys;
            var tt = new SelectTreeList(Items, "0", "DocCategoryName", "Doc_CategoryID", "ParentDoc_CategoryID", "Doc_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Doc_CategoryID___equal", tt, "==文档类别==");

            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面下拉树--文档类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_ParentDoc_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            var Items = SoftProjectAreaEntityDomain.Doc_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            //List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Doc_Categorys;
            var tt = new SelectTreeList(Items, "0", "DocCategoryName", "Doc_CategoryID", "ParentDoc_CategoryID", "Doc_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.ParentDoc_CategoryID", tt, "==文档类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// 编辑页面下拉树--文档类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_Doc_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            var Items = SoftProjectAreaEntityDomain.Doc_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            //List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Doc_Categorys;
            var tt = new SelectTreeList(Items, "0", "DocCategoryName", "Doc_CategoryID", "ParentDoc_CategoryID", "Doc_CategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.Doc_CategoryID", tt, "==文档类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// Jquery树
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string JqTreeNs_Doc_CategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            var Itemst = SoftProjectAreaEntityDomain.Doc_Categorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            //var Itemst = SoftProjectAreaEntityDomain.Doc_Categorys;
                var treeList = new TreeList(Itemst, "0", "DocCategoryName", "Doc_CategoryID", "ParentDoc_CategoryID", "", "", "Doc_CategoryID", "", "");
                var str = HtmlHelpersProject.JqTreeN(null, "Doc_CategoryID", treeList, "", 2);
                return str.ToString();
        }

        #endregion
    }
}
