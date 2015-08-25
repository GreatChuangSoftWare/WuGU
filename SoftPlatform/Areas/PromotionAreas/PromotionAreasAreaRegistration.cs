using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class PromotionAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PromotionAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PromotionAreas_default",
                "PromotionAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
