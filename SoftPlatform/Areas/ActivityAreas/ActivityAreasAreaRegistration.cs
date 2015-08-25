using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class ActivityAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ActivityAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ActivityAreas_default",
                "ActivityAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
