using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class DocAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DocArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DocArea_default",
                "DocArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            ProjectCache.QueryHtmlDropTrees.Add("ParentDoc_CategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_ParentDoc_CategoryID);
            ProjectCache.QueryHtmlDropTrees.Add("Doc_CategoryID", SoftProjectAreaEntityDomain.QueryHtmlDropTrees_Doc_CategoryID);

            ProjectCache.HtmlDropTrees.Add("ParentDoc_CategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_ParentDoc_CategoryID);
            ProjectCache.HtmlDropTrees.Add("Doc_CategoryID", SoftProjectAreaEntityDomain.HtmlDropTrees_Doc_CategoryID);

            ProjectCache.JqTreeNs.Add("Doc_CategoryID", SoftProjectAreaEntityDomain.JqTreeNs_Doc_CategoryID);

        }
    }
}
