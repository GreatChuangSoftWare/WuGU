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
    public class Design_ModularOrFunControllController : BaseController
    {
        public Design_ModularOrFunControllController()
        {
        }

        #region 编辑

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunControll_GetByModularOrFunID();
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
            var resp = domain.Design_ModularOrFunControll_EditListSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        /// <summary>
        /// 生成控制器名称
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidControll(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidControll();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成一般表控制器记录(新页)
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecord(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecord();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成一般表控制器记录(新页)--新建-提交-审核
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecord010416(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecord010416();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成一般表控制器记录(新页)--附件
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecordByAtt(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecordByAtt();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成一般表控制器记录(新页)--订单明细模板
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecordByOrderDetailTemplete(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecordByOrderDetailTemplete();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成一般表控制器记录(新页)--弹窗
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecordPopup(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecordPopup();

            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成领域的相关表控制器记录
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecordDomainRef(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecordDomainRef();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成领域的相关表控制器记录
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditListBulidRecordDomainRefPopup(SoftProjectAreaEntityDomain domain)
        {
            //return null;
            var resp = domain.Design_ModularOrFunControll_EditListBulidRecordDomainRefPopup();
            return new HJsonResult(new { Data = resp });
        }

        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunControll_Row();
            
            return View("Row", resp);
        }

        public ActionResult Popup(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFunControll_Popup();
            return View("Popup", resp);
        }

    }
}

