using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class Sys_DictController : BaseController
    {
        ///// <summary>
        ///// 字典--能修改查询
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[ActionName("IndexByModi")]
        //public ActionResult IndexByModi(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.IndexByModi();
        //    return View("IndexByModi", resp);
        //}

        [HttpGet]
        [ActionName("GetByCategory")]
        public ActionResult ByCategory(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_Dict_ByCategory();
            return View("ByCategory", resp); ;
        }

        [HttpGet]
        [ActionName("Add")]
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_Dict_Add();
            return View("Edit", resp); ;
        }

        [HttpPost]
        [ActionName("Add")]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_Dict_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_Dict_Edit();
            return View("Edit", resp); ;
        }

        [HttpPost]
        [ActionName("Edit")]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_Dict_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpGet]
        [ActionName("Row")]
        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Sys_Dict_GetByID();
            return View("Row", resp); ;
        }

    }
}

