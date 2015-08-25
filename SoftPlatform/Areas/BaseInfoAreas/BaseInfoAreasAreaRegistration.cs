using SoftProject.Domain;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class BaseInfoAreasAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BaseInfoAreas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BaseInfoAreas_default",
                "BaseInfoAreas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            #region 1级地区

            ProjectCache.QueryHtmlDropDownLists.Add("Ba_AreaID1", SoftProjectAreaEntityDomain.QueryHtmlDropDownList_Ba_AreaID1);
            ProjectCache.HtmlDropDownLiss.Add("Ba_AreaID1", SoftProjectAreaEntityDomain.HtmlDropDownLiss_Ba_AreaID1);

            #endregion

            #region 2级地区

            ProjectCache.QueryHtmlDropDownLists.Add("Ba_AreaID2", SoftProjectAreaEntityDomain.QueryHtmlDropDownList_Ba_AreaID2);
            ProjectCache.HtmlDropDownLiss.Add("Ba_AreaID2", SoftProjectAreaEntityDomain.HtmlDropDownLiss_Ba_AreaID2);

            #endregion

            #region 3级地区

            ProjectCache.QueryHtmlDropDownLists.Add("Ba_AreaID3", SoftProjectAreaEntityDomain.QueryHtmlDropDownList_Ba_AreaID3);
            ProjectCache.HtmlDropDownLiss.Add("Ba_AreaID3", SoftProjectAreaEntityDomain.HtmlDropDownLiss_Ba_AreaID3);

            #endregion

        }
    }
}
