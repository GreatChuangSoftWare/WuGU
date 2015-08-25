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
    /// 控制器：Pr_Notice(促销公告)
    /// </summary>
    public class Pr_NoticeController : BaseController
    {
        public Pr_NoticeController()
        {
        }

        /// <summary>
        /// 促销公告--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            //if (!domain.Querys.QueryDicts.ContainsKey("___equal"))
            //{
            //    if (domain.Item. == null)
            //throw new Exception("主键不能为空");
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "___equal", Value = domain.Item..ToString() });
            //}

            ModularOrFunCode = "PromotionAreas.Pr_Notice.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 促销公告--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            ModularOrFunCode = "PromotionAreas.Pr_Notice.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 促销公告--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.PromotionContext = Server.UrlDecode(domain.Item.PromotionContext);
            ModularOrFunCode = "PromotionAreas.Pr_Notice.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 促销公告--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PromotionAreas.Pr_Notice.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "PromotionAreas.Pr_Notice.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 促销公告--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.PromotionContext = Server.UrlDecode(domain.Item.PromotionContext);
            ModularOrFunCode = "PromotionAreas.Pr_Notice.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 促销公告--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PromotionAreas.Pr_Notice.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #region 本月促销
        
        /// <summary>
        /// 促销公告--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexByCurrMonth(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PromotionAreas.Pr_Notice.IndexByCurrMonth";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = SoftProjectAreaEntityDomain.Pr_Notice_ByCurrMonth();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 促销公告--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailByCurrMonth(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PromotionAreas.Pr_Notice.DetailByCurrMonth";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 促销公告--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexByHome(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PromotionAreas.Pr_Notice.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion
    }
}
