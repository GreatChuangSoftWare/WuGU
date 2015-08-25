using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class PreOrg_AreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PreOrg_Area";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PreOrg_Area_default",
                "PreOrg_Area/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            #region 组织机构：表单元素

            ProjectCache.QueryHtmlDropTrees.Add("ParentPre_OrganizationID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_ParentPre_OrganizationID);
            ProjectCache.QueryHtmlDropTrees.Add("Pre_OrganizationID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_Pre_OrganizationID);

            ProjectCache.HtmlDropTrees.Add("ParentPre_OrganizationID", SoftProjectAreaEntityDomain.HtmlDropTrees_ParentPre_OrganizationID);
            ProjectCache.HtmlDropTrees.Add("Pre_OrganizationID", SoftProjectAreaEntityDomain.HtmlDropTrees_Pre_OrganizationID);

            ProjectCache.JqTreeNs.Add("Pre_OrganizationID", SoftProjectAreaEntityDomain.JqTreeNs_Pre_OrganizationID);

            #endregion
            var load=SoftProjectAreaEntityDomain.Pre_Organizations;
        }
    }
}
