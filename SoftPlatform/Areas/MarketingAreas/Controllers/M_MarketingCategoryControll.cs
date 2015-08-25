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
    /// 控制器：M_MarketingCategory(营销课堂)
    /// </summary>
    public class M_MarketingCategoryController : BaseController
    {
        public M_MarketingCategoryController()
        {
        }

        /// <summary>
        /// 营销课堂--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });

            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销课堂--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销课堂--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.ParentM_MarketingCategoryID == null)
                domain.Item.ParentM_MarketingCategoryID = 0;
            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            domain.M_MarketingCategory_AddCache();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 营销课堂--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销课堂--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.ParentM_MarketingCategoryID == null)
                domain.Item.ParentM_MarketingCategoryID = 0;
            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            domain.M_MarketingCategory_UpdateCache();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 营销课堂--Row
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingCategory.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Items.Add(resp.Item);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View("Rows", resp);
        }

    }
}
