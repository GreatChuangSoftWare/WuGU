using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class CustomerAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CustomerAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CustomerAreas_default",
                "CustomerAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
