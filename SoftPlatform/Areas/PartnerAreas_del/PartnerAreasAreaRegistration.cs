using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class PartnerAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PartnerAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PartnerAreas_default",
                "PartnerAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
