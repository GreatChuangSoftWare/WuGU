using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class SysAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SysAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SysAreas_default",
                "SysAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            var load1 = ProjectCache.Sys_Dicts;
            var load2 = ProjectCache.Sys_HOperControls;
            //var load3 = ProjectCache.Design_ModularOrFuns;
            //var load4 = ProjectCache.Design_ModularPageFields;
            //var load5 = ProjectCache.Design_ModularOrFunRefBtns;
            //var load6 = ProjectCache.Design_ModularOrFunBtnControlls;
        }
    }
}
