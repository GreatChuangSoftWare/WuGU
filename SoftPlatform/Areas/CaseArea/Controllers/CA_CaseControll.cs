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
    /// 控制器：CA_Case(我的案例)
    /// </summary>
    public class CA_CaseController : BaseController
    {
        public CA_CaseController()
        {
        }

        #region 加盟商

        /// <summary>
        /// 我的案例--查询
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

            ModularOrFunCode = "CaseArea.CA_Case.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的案例--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.CaseSharingPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.CaseSharingPerson = LoginInfo.UserName;
            resp.Item.CaseReleaseDate = DateTime.Now;

            ModularOrFunCode = "CaseArea.CA_Case.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的案例--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.CaseAuditStatuID = 1;
            domain.Item.CaseDescription = Server.UrlDecode(domain.Item.CaseDescription);
            domain.Item.CaseExperience = Server.UrlDecode(domain.Item.CaseExperience);

            ModularOrFunCode = "CaseArea.CA_Case.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 我的案例--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CaseArea.CA_Case.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "CaseArea.CA_Case.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的案例--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.CaseDescription = Server.UrlDecode(domain.Item.CaseDescription);
            domain.Item.CaseExperience = Server.UrlDecode(domain.Item.CaseExperience);

            ModularOrFunCode = "CaseArea.CA_Case.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 我的案例--提交
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Submit(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.CaseDescription = Server.UrlDecode(domain.Item.CaseDescription);
            domain.Item.CaseExperience = Server.UrlDecode(domain.Item.CaseExperience);
            domain.Item.CaseAuditStatuID = 4;
            ModularOrFunCode = "CaseArea.CA_Case.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 我的案例--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CaseArea.CA_Case.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 案例查询--列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexSearch(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "CaseAuditStatuID___equal", Value = "16" });

            ModularOrFunCode = "CaseArea.CA_Case.IndexSearch";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的案例--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail1(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CaseArea.CA_Case.Detail1";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 公司

        #region 待审核案例-查询

        /// <summary>
        /// 待审核案例--列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexWaitExamine(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "CaseAuditStatuID___equal", Value = "4" });

            ModularOrFunCode = "CaseArea.CA_Case.IndexWaitExamine";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 待审核列表--审核查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Examine(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CaseArea.CA_Case.Examine";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Item.CaseAuditDate = DateTime.Now;
            resp.Item.CaseAuditPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.CaseAuditPerson = LoginInfo.UserName;

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 待审核列表--审核保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult ExamineSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.CaseDescription = Server.UrlDecode(domain.Item.CaseDescription);
            domain.Item.CaseExperience = Server.UrlDecode(domain.Item.CaseExperience);
            domain.Item.CaseAuditStatuID = 16;
            ModularOrFunCode = "CaseArea.CA_Case.Examine";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 案例查询

        /// <summary>
        /// 案例查询--列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexSearch2(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "CaseAuditStatuID___equal", Value = "16" });

            ModularOrFunCode = "CaseArea.CA_Case.IndexSearch2";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 我的案例--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail2(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CaseArea.CA_Case.Detail2";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #endregion
    }
}
