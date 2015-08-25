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
    public class Design_ModularFieldController : BaseController
    {
        public Design_ModularFieldController()
        {
        }

        #region 编辑

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularField_GetByModularOrFunID();
            //resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("EditList", resp);
        }

        [HttpPost]
        public HJsonResult EditListSave(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularField_EditListSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpPost]
        public HJsonResult CreateTable(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularField_CreateTable();
            return new HJsonResult(new { Data = resp });
        }

        [HttpPost]
        public HJsonResult BulidEntity(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularField_BulidEntity();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            return View("Row", resp);
        }

        [HttpGet]
        public ActionResult Popup(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularField_GetModularPageOrQueryField();
            //resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("Popup", resp);
        }

    }
}

