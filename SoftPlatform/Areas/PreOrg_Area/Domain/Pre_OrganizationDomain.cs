
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

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Pre_Organization(组织机构管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 缓存、界面元素

        public  void Pre_Organization_AddCache()
        {
            #region 更新：企业用户缓存

            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.Pre_Organizations.Add(resp.Item);

            #endregion
        }

        public  void Pre_Organization_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var Pre_Organization = SoftProjectAreaEntityDomain.Pre_Organizations.Where(p => p.Pre_OrganizationID == Item.Pre_OrganizationID).FirstOrDefault();
            SoftProjectAreaEntityDomain.Pre_Organizations.Remove(Pre_Organization);
            SoftProjectAreaEntityDomain.Pre_Organizations.Add(resp.Item);

            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_Organization_GetAll()
        {
            #region 树数据
            string sql = "SELECT * FROM V_Pre_Organization ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 组织机构类别

        static List<SoftProjectAreaEntity> _Pre_Organizations = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Pre_Organizations
        {
            get
            {
                if (_Pre_Organizations.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Pre_Organizations = domain.Pre_Organization_GetAll().Items;
                }
                return _Pre_Organizations;
            }
        }

        public static void Pre_Organization_Clear()
        {
            _Pre_Organizations = new List<SoftProjectAreaEntity>();
        }

        #endregion

        /// <summary>
        /// 查询下拉树--组织机构父节点ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_ParentPre_OrganizationID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 组织机构
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Pre_Organizations.Where(p => p.LoginCategoryID == item.LoginCategoryID).ToList();

            var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentPre_OrganizationID", "Pre_OrganizationID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "ParentPre_OrganizationID___equal", tt, "==组织机构==");
            //sbHtml.AppendLine(str.ToString());
            var strDrop = str.ToString();
            //{
            //    var str = HtmlHelpers.DropDownForTree(null, "Pre_OrganizationID___equal", tt, "==组织机构==");
            //    strDrop = str.ToString();
            //}
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询下拉树--组织机构ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string QueryHtmlDropTrees_Pre_OrganizationID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 组织机构
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Pre_Organizations.Where(p => p.LoginCategoryID == item.LoginCategoryID).ToList();
            var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentPre_OrganizationID", "Pre_OrganizationID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Pre_OrganizationID___equal", tt, "==组织机构==");
            var strDrop = str.ToString();
            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面下拉树--公司组织机构父节点
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_ParentPre_OrganizationID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Pre_Organizations.Where(p => p.LoginCategoryID == item.LoginCategoryID).ToList(); 
            var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentPre_OrganizationID", "Pre_OrganizationID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.ParentPre_OrganizationID", tt, "");

            return str.ToHtmlString();
        }

        /// <summary>
        /// 编辑页面下拉树--公司组织机构ID
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string HtmlDropTrees_Pre_OrganizationID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Pre_Organizations.Where(p => p.LoginCategoryID == item.LoginCategoryID).ToList(); 
            var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentPre_OrganizationID", "Pre_OrganizationID", val, true, "");
            var str = HtmlHelpers.DropDownForTree(null, "Item.Pre_OrganizationID", tt, "");

            return str.ToHtmlString();
        }

        /// <summary>
        /// Jquery树
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <returns></returns>
        public static string JqTreeNs_Pre_OrganizationID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            List<SoftProjectAreaEntity> Items = SoftProjectAreaEntityDomain.Pre_Organizations.Where(p => p.LoginCategoryID == item.LoginCategoryID).ToList();
            var treeList = new TreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentPre_OrganizationID", "", "", "Pre_OrganizationID", "", "");
            var str = HtmlHelpersProject.JqTreeN(null, "Pre_OrganizationID", treeList, "", 2);
            return str.ToString();
        }
        #endregion
    }

}
