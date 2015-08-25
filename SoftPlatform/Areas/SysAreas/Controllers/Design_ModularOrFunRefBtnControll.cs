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
    public class Design_ModularOrFunRefBtnController : BaseController
    {
        public Design_ModularOrFunRefBtnController()
        {
        }

        #region 编辑

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunRefBtn_GetByPremSetID();
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
            var resp = domain.Design_ModularOrFunRefBtn_EditListSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        public ActionResult Popup(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunRefBtn_Popup();
            return View("Popup", resp);
        }

        public HJsonResult GetPageByModularOrFunParentID(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.GetPageByModularOrFunParentID();
            return new HJsonResult(new { Data = resp.Items });
        }

        public ActionResult Rows(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunRefBtn_Rows();
            return View("Rows", resp);
        }

        //[HttpGet]
        //public ActionResult EditListFun(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.Design_ModularOrFunRefBtn_GetByModularOrFunBtnID();
        //    //resp.FunNameEn = "Edit";
        //    //resp.FunNameCn = "编辑";
        //    //resp.FunBtnNameCn = "保存";
        //    //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
        //    return View("EditListFun", resp);
        //}
    }
}

