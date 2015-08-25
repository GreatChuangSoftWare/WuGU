using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class ToolAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ToolArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ToolArea_default",
                "ToolArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            ProjectCache.QueryHtmlDropTrees.Add("ParentT_ToolCategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_ParentT_ToolCategoryID);
            ProjectCache.QueryHtmlDropTrees.Add("T_ToolCategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_T_ToolCategoryID);

            ProjectCache.HtmlDropTrees.Add("ParentT_ToolCategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_ParentT_ToolCategoryID);
            ProjectCache.HtmlDropTrees.Add("T_ToolCategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_T_ToolCategoryID);

            ProjectCache.JqTreeNs.Add("T_ToolCategoryID", SoftProjectAreaEntityDomain.JqTreeNs_T_ToolCategoryID);

        }
    }
}
