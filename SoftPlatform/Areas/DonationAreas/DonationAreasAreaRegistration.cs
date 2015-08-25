using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class DonationAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DonationAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DonationAreas_default",
                "DonationAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
