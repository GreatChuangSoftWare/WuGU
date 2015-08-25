using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class MemberGradeAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MemberGradeAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MemberGradeAreas_default",
                "MemberGradeAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            var load10 = SoftProjectAreaEntityDomain.MG_MemberGrades;

            ProjectCache.QueryHtmlDropDownLists.Add("MG_MemberGradeID", SoftProjectAreaEntityDomain.QueryHtmlDropDownList_MG_MemberGradeID);
            ProjectCache.HtmlDropDownLiss.Add("MG_MemberGradeID", SoftProjectAreaEntityDomain.HtmlDropDownLiss_MG_MemberGradeID);

        }
    }
}
