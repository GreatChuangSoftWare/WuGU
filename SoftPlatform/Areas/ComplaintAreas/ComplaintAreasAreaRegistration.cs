using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class ComplaintAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ComplaintAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ComplaintAreas_default",
                "ComplaintAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
