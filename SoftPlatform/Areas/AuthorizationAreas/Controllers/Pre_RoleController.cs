using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Core;
using Framework.Web.Mvc.Sys;
using SoftProject.Domain;


//namespace Framework.Web.Mvc
namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：Pre_Role(角色管理)
    /// </summary>
    public class Pre_RoleController : BaseController
    {
        public Pre_RoleController()
        {
        }

        #region 公司角色

        /// <summary>
        /// 角色管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "1" });
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            //return View("IndexPFrame", resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 角色管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 1;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Add";
            //MenuCode = "AuthorizationPanel";
            resp.FunNameEn = "Add";
            return View("Edit", resp);
        }

        /// <summary>
        /// 角色管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 1;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_Role_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 角色管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Edit";
            resp.FunNameEn = "Edit";
            return View("Edit", resp);
        }

        /// <summary>
        /// 角色管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_Role_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpGet]
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            return View("Edit", resp);
        }

        #endregion

        #region 公司客户角色

        /// <summary>
        /// 角色管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "4" });
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.IndexCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            //return View("IndexPFrame", resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 角色管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCU(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 4;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.AddCU";
            //MenuCode = "AuthorizationPanel";
            resp.FunNameEn = "Add";
            return View("Edit", resp);
        }

        /// <summary>
        /// 角色管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCUSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.Pre_CompanyID;
            domain.Item.LoginCategoryID = 4;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.AddCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_Role_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 角色管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.DetailCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Role.EditCU";
            resp.FunNameEn = "Edit";
            return View("Edit", resp);
        }

        /// <summary>
        /// 角色管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCUSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.EditCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_Role_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpGet]
        public ActionResult DetailCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.DetailCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            return View("Edit", resp);
        }

        #endregion

        #region 企业角色

        /// <summary>
        /// 角色管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexComp(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "2" });
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.IndexComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            //return View("IndexPFrame", resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 角色管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddComp(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 2;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.AddComp";
            //MenuCode = "AuthorizationPanel";
            resp.FunNameEn = "Add";
            return View("Edit", resp);
        }

        /// <summary>
        /// 角色管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCompSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 2;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.AddComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_Role_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 角色管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditComp(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Role.EditComp";
            resp.FunNameEn = "Edit";
            return View("Edit", resp);
        }

        /// <summary>
        /// 角色管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCompSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.EditComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_Role_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpGet]
        public ActionResult DetailComp(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            return View("Edit", resp);
        }

        #endregion

        #region 企业客户角色

        /// <summary>
        /// 企业客户角色管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexCompCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "8" });
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.IndexCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            //return View("IndexPFrame", resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业客户角色管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCompCU(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 8;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.AddCompCU";
            //MenuCode = "AuthorizationPanel";
            resp.FunNameEn = "Add";
            return View("Edit", resp);
        }

        /// <summary>
        /// 企业客户角色管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCompCUSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 8;
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.AddCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_Role_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 企业客户角色管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCompCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.DetailCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Role.EditCompCU";
            resp.FunNameEn = "Edit";
            return View("Edit", resp);
        }

        /// <summary>
        /// 企业客户角色管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCompCUSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.EditCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_Role_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        [HttpGet]
        public ActionResult DetailCompCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Role.DetailCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            return View("Edit", resp);
        }

        #endregion

    }
}
