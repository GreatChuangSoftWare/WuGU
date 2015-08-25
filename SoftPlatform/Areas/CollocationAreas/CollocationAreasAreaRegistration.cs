using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class CollocationAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CollocationAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CollocationAreas_default",
                "CollocationAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
