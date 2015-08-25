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
    public class Design_ModularOrFunDomainController : BaseController
    {
        public Design_ModularOrFunDomainController()
        {
        }

        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_Index();
            return View("Index", resp);
        }

        #region 添加

        [HttpGet]
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_Add();
            resp.FunNameEn = "Add";
            resp.FunNameCn = "编辑";
            resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("Edit", resp);
        }

        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpPost]
        public HJsonResult AddSaveMethod(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_AddSaveMethod();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 编辑

        [HttpGet]
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_GetByID();
            resp.FunNameEn = "Edit";
            resp.FunNameCn = "编辑";
            resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("Edit", resp);
        }

        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        public ActionResult BulidDomain(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_BulidDomainFile();
            //resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return null;
        }

        [HttpPost]
        public HJsonResult BulidRecord(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_BulidRecord();
            return new HJsonResult(new { Data = resp });
        }

        [HttpPost]
        public HJsonResult BulidRecordDomainRef(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_BulidRecordDomainRef();
            return new HJsonResult(new { Data = resp });
        }

        //?Item.Design_ModularOrFunDomainID
        [HttpPost]
        public HJsonResult Delete(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomain_Delete();
            return new HJsonResult(new { Data = resp });
        }

    }
}

