using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class FranchiseeAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FranchiseeAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FranchiseeAreas_default",
                "FranchiseeAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
