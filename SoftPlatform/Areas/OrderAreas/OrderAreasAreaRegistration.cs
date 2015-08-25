using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class OrderAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "OrderAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "OrderAreas_default",
                "OrderAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
