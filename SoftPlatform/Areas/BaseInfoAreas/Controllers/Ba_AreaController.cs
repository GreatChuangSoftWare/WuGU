using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Core;
using Framework.Web.Mvc.Sys;



using SoftProject.Domain;
using SoftProject.CellModel;

//namespace Framework.Web.Mvc
//SoftPlatform.Areas.Ba_AreaAreas
namespace SoftPlatform.Controllers
{
    public class Ba_AreaController : BaseController
    {
        public Ba_AreaController()
        {
        }

        /// <summary>
        /// AJax：区域下拉列表框改变
        /// </summary>BaAreas/Ba_Area/GetSubBa_AreaIDss
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public HJsonResult GetSubBa_AreaIDs1s(SoftProjectAreaEntityDomain domain)
        {
            var Items = SoftProjectAreaEntityDomain.Ba_Area_GetSubBa_AreaIDss(domain.Item.Ba_AreaID1);
            return new HJsonResult(new { Data = Items });
        }

        [HttpGet]
        public HJsonResult GetSubBa_AreaIDs2s(SoftProjectAreaEntityDomain domain)
        {
            var Items = SoftProjectAreaEntityDomain.Ba_Area_GetSubBa_AreaIDss(domain.Item.Ba_AreaID2);
            return new HJsonResult(new { Data = Items });
        }
    }
}
