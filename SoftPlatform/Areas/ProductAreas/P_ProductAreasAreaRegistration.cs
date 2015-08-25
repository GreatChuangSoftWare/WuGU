using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class ProductAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ProductAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ProductAreas_default",
                "ProductAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            ProjectCache.QueryHtmlDropTrees.Add("ParentP_CategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_ParentP_CategoryID);
            ProjectCache.QueryHtmlDropTrees.Add("P_CategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_P_CategoryID);

            ProjectCache.HtmlDropTrees.Add("ParentP_CategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_ParentP_CategoryID);
            ProjectCache.HtmlDropTrees.Add("P_CategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_P_CategoryID);

            ProjectCache.JqTreeNs.Add("P_CategoryID", SoftProjectAreaEntityDomain.JqTreeNs_P_CategoryID);

        }
    }
}
