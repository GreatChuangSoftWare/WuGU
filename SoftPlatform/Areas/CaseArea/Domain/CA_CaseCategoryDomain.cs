
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

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：CA_CaseCategory(案例类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 缓存、界面元素

        public void CA_CaseCategory_AddCache()
        {
            #region 更新：用户缓存

            ModularOrFunCode = "CaseArea.CA_CaseCategory.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.CA_CaseCategorys.Add(resp.Item);

            #endregion
        }

        public void CA_CaseCategory_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "CaseArea.CA_CaseCategory.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var CA_CaseCategory = SoftProjectAreaEntityDomain.CA_CaseCategorys.Where(p => p.CA_CaseCategoryID == Item.CA_CaseCategoryID).FirstOrDefault();
            
            SoftProjectAreaEntityDomain.CA_CaseCategorys.Remove(CA_CaseCategory);
            SoftProjectAreaEntityDomain.CA_CaseCategorys.Add(resp.Item);
            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase CA_CaseCategory_GetAll()
        {
            #region 树数据

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            //domain.Sys_HOperControl = new SoftProjectAreaEntity
            //{
            //    DBTSql = "SELECT * FROM CA_CaseCategory ",
            //    DBOperType = 16,
            //    DBSelectResultType = 2,
            //};

            //OperCode = "CA_CaseCategory.GetAll";
            //var resp = domain.Execute();
            //var resp = Execute();
            //resp.Item.CA_CaseCategorys = resptrees.Items;

            string sql = "SELECT * FROM V_CA_CaseCategory ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 案例类别

        static List<SoftProjectAreaEntity> _CA_CaseCategorys = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> CA_CaseCategorys
        {
            get
            {
                if (_CA_CaseCategorys.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _CA_CaseCategorys = domain.CA_CaseCategory_GetAll().Items;
                }
                return _CA_CaseCategorys;
            }
        }

        public static void CA_CaseCategory_Clear()
        {
            _CA_CaseCategorys = new List<SoftProjectAreaEntity>();
        }

        #endregion

        /// <summary>
        /// 查询下拉树--公司案例类型父节点ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_ParentCA_CaseCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 案例类别

            //var Pre_Company=HttpContext.Current.Session["LoginInfo"] as  SoftProjectAreaEntity;

            //var Items = SoftProjectAreaEntityDomain.CA_CaseCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.CA_CaseCategorys;
            var tt = new SelectTreeList(Items, "0", "CaseCategoryName", "CA_CaseCategoryID", "ParentCA_CaseCategoryID", "CA_CaseCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "ParentCA_CaseCategoryID___equal", tt, "==案例类别==");
            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询下拉树--案例类型ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_CA_CaseCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 案例类型
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;

            //var Items = SoftProjectAreaEntityDomain.CA_CaseCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.CA_CaseCategorys;
            var tt = new SelectTreeList(Items, "0", "CaseCategoryName", "CA_CaseCategoryID", "ParentCA_CaseCategoryID", "CA_CaseCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "CA_CaseCategoryID___equal", tt, "==案例类别==");

            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面下拉树--案例类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_ParentCA_CaseCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.CA_CaseCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.CA_CaseCategorys;
            var tt = new SelectTreeList(Items, "0", "CaseCategoryName", "CA_CaseCategoryID", "ParentCA_CaseCategoryID", "CA_CaseCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.ParentCA_CaseCategoryID", tt, "==案例类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// 编辑页面下拉树--案例类型父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_CA_CaseCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Items = SoftProjectAreaEntityDomain.CA_CaseCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);

            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.CA_CaseCategorys;
            var tt = new SelectTreeList(Items, "0", "CaseCategoryName", "CA_CaseCategoryID", "ParentCA_CaseCategoryID", "CA_CaseCategoryID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.CA_CaseCategoryID", tt, "==案例类型==");
            return str.ToHtmlString();
        }

        /// <summary>
        /// Jquery树
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string JqTreeNs_CA_CaseCategoryID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Company = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
            //var Itemst = SoftProjectAreaEntityDomain.CA_CaseCategorys.Where(p => p.Pre_CompanyID == Pre_Company.CompanyID);
            var Itemst = SoftProjectAreaEntityDomain.CA_CaseCategorys;
            var treeList = new TreeList(Itemst, "0", "CaseCategoryName", "CA_CaseCategoryID", "ParentCA_CaseCategoryID", "", "", "CA_CaseCategoryID", "", "");
                var str = HtmlHelpersProject.JqTreeN(null, "CA_CaseCategoryID", treeList, "", 2);
                return str.ToString();
        }

        #endregion
    }
}
