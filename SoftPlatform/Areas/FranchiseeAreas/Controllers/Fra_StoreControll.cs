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
    /// 控制器：Fra_Store(门店管理)
    /// </summary>
    public class Fra_StoreController : BaseController
    {
        public Fra_StoreController()
        {
        }

        #region 加盟商管理

        /// <summary>
        /// 门店管理--查询
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

            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            resp.Items.ForEach(p => p.StoreContext = p.StoreContext.Replace("<input ", "<input disabled='disabled'"));

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 门店管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Fra_Store_Add();
            resp.Item.Pre_CompanyID = LoginInfo.CompanyID;

            //StoreContext
            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 门店管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.StoreContext = Server.UrlDecode(domain.Item.StoreContext);
            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 门店管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 门店管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.StoreContext = Server.UrlDecode(domain.Item.StoreContext);
            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 门店管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "FranchiseeAreas.Fra_Store.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.StoreContext = resp.Item.StoreContext.Replace("<input ", "<input disabled='disabled'");// (p => p.StoreContext = p.StoreContext.Replace("<input ", "<input disabled='disabled'"));
            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 加盟商仪表盘

        /// <summary>
        /// 门店管理--查询：加盟商仪表盘
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDashboard(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                if (domain.Item.Pre_CompanyID == null)
                    throw new Exception("加盟商ID不能为空");
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = domain.Item.Pre_CompanyID.ToString() });
            }

            ModularOrFunCode = "FranchiseeAreas.Fra_Store.IndexDashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            resp.Items.ForEach(p => p.StoreContext = p.StoreContext.Replace("<input ", "<input disabled='disabled'"));

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

    }
}
