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
    public class Design_ModularPageFieldController : BaseController
    {
        public Design_ModularPageFieldController()
        {
        }

        #region 编辑

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            //var resp = domain.Design_ModularPageField_EditList();
            var resp = domain.Design_ModularPageField_GetByModularOrFunID();
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
            var resp = domain.Design_ModularPageField_EditListSave();
            return new HJsonResult(new { Data = resp });
        }


        #endregion

        /// <summary>
        /// 生成字段
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult BulidRecord(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularPageField_BulidRecord();
            return new HJsonResult(new { Data = resp });
        }


        public ActionResult Rows(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularPageField_Rows();
            //resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("Rows", resp);
        }
    }
}

