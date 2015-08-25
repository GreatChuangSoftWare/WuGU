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
    /// 控制器：T_ToolCategory(工具类别管理)
    /// </summary>
    public class T_ToolCategoryController : BaseController
    {
        public T_ToolCategoryController()
        {
        }

        /// <summary>
        /// 工具类别管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
[HttpGet]
public ActionResult Index(SoftProjectAreaEntityDomain domain)
{
    //domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });

    ModularOrFunCode = "ToolArea.T_ToolCategory.Index";
    domain.Design_ModularOrFun = Design_ModularOrFun;
    var resp = domain.QueryIndex();

    if (Request.IsAjaxRequest())
        return View(Design_ModularOrFun.PartialView, resp);
    resp.ViewContextName = Design_ModularOrFun.PartialView;
    return View(Design_ModularOrFun.MainView, resp);
}

        /// <summary>
        /// 工具类别管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
    ModularOrFunCode = "ToolArea.T_ToolCategory.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具类别管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值 
            #endregion
    ModularOrFunCode = "ToolArea.T_ToolCategory.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 工具类别管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
    ModularOrFunCode = "ToolArea.T_ToolCategory.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

    ModularOrFunCode = "ToolArea.T_ToolCategory.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具类别管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
    ModularOrFunCode = "ToolArea.T_ToolCategory.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 工具类别管理--Row
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
    ModularOrFunCode = "ToolArea.T_ToolCategory.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Items.Add(resp.Item);
             resp.ViewContextName = Design_ModularOrFun.PartialView;
             return View("Rows", resp);
        }

   }
}
