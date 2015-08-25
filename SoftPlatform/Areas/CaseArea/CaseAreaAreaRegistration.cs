using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class CaseAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CaseArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CaseArea_default",
                "CaseArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            ProjectCache.QueryHtmlDropTrees.Add("ParentCA_CaseCategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_ParentCA_CaseCategoryID);
            ProjectCache.QueryHtmlDropTrees.Add("CA_CaseCategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_CA_CaseCategoryID);

            ProjectCache.HtmlDropTrees.Add("ParentCA_CaseCategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_ParentCA_CaseCategoryID);
            ProjectCache.HtmlDropTrees.Add("CA_CaseCategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_CA_CaseCategoryID);

            ProjectCache.JqTreeNs.Add("CA_CaseCategoryID", SoftProjectAreaEntityDomain.JqTreeNs_CA_CaseCategoryID);
        }
    }
}
