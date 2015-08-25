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
    public class Design_ModularOrFunDomainDetailController : BaseController
    {
        public Design_ModularOrFunDomainDetailController()
        {
        }

        #region 编辑

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunDomainDetail_EditList();
            //resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("EditList", resp);
        }

        #endregion

        //public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.Design_ModularOrFunDomainDetail_AddSave();
        //    return new HJsonResult(new { Data = resp });
        //}

        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();//.Design_ModularOrFunDomainDetail_ByID();
            return View("Row", resp);
        }

    }
}

