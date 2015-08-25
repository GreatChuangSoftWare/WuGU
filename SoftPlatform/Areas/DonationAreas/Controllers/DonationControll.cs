using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：Donation(捐助)
    /// </summary>
    public class DonationController : BaseController
    {
        public DonationController()
        {
        }

        /// <summary>
        /// 我的捐助--列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            }

            ModularOrFunCode = "DonationAreas.Donation.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的捐助--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.FillTabDate = DateTime.Now;
            resp.Item.FillTabPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.FillTabPerson = LoginInfo.UserName;

            ModularOrFunCode = "DonationAreas.Donation.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的捐助--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.AuditStatuID = 1;
            domain.Item.DonationContext = Server.UrlDecode(domain.Item.DonationContext);
            ModularOrFunCode = "DonationAreas.Donation.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 我的捐助--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DonationAreas.Donation.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "DonationAreas.Donation.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的捐助--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.DonationContext = Server.UrlDecode(domain.Item.DonationContext);
            ModularOrFunCode = "DonationAreas.Donation.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 我的捐助--提交保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Submit(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.DonationContext = Server.UrlDecode(domain.Item.DonationContext);
            domain.Item.AuditStatuID = 4;
            ModularOrFunCode = "DonationAreas.Donation.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 我的捐助--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DonationAreas.Donation.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 待审核捐助
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexWaitAudit(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "AuditStatuID___equal", Value = "4" });

            ModularOrFunCode = "DonationAreas.Donation.IndexWaitAudit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 待审核捐助--审核查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Audit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DonationAreas.Donation.Audit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.AuditDate = DateTime.Now;
            resp.Item.AuditPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.AuditPerson = LoginInfo.UserName;
            ModularOrFunCode = "DonationAreas.Donation.Audit";
            resp.FunNameEn = "Audit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 待审核捐助--审核保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AuditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DonationAreas.Donation.Audit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.Item.AuditStatuID = 16;
            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 捐助查询--列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexSearch(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "AuditStatuID___equal", Value = "16" });

            ModularOrFunCode = "DonationAreas.Donation.IndexSearch";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 捐助查询--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail2(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DonationAreas.Donation.Detail2";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
