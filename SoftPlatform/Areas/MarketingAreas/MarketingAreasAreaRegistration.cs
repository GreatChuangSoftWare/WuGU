using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class MarketingAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MarketingAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MarketingAreas_default",
                "MarketingAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            ProjectCache.QueryHtmlDropTrees.Add("ParentM_MarketingCategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_ParentM_MarketingCategoryID);
            ProjectCache.QueryHtmlDropTrees.Add("M_MarketingCategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_M_MarketingCategoryID);

            ProjectCache.HtmlDropTrees.Add("ParentM_MarketingCategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_ParentM_MarketingCategoryID);
            ProjectCache.HtmlDropTrees.Add("M_MarketingCategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_M_MarketingCategoryID);

            ProjectCache.JqTreeNs.Add("M_MarketingCategoryID", SoftProjectAreaEntityDomain.JqTreeNs_M_MarketingCategoryID);
        }
    }
}
