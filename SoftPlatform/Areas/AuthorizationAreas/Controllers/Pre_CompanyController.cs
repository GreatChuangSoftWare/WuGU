using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

//namespace Framework.Web.Mvc
namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：Pre_Company(公司管理)
    /// </summary>
    public class Pre_CompanyController : BaseController
    {
        public Pre_CompanyController()
        {
        }
        #region 公司管理自己的公司信息

        /// <summary>
        /// 公司管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit1(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Edit1";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 公司管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult Edit1Save(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Edit1";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 公司管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail1(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Detail1";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 公司管理:企业(加盟商)

        /// <summary>
        /// 公司管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "CompanyCategoryID___equal", Value = "2" });
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 公司管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.FunNameEn = "Add";
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 公司管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            domain.Item.CompanyCategoryID = 2;
            var resp = domain.Pre_Company_AddSave();//保存企业信息时，保存管理员信息
            //var resp = domain.Default();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 公司管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 公司管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_Company_EditSave();////保存企业信息时，保存管理员信息
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 公司管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            //MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 企业管理自己的企业信息

        /// <summary>
        /// 公司管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditComp(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_Company.EditComp";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 公司管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCompSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.EditComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_Company_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 公司管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailComp(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        public ActionResult Dashboard(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.Pre_CompanyID == null)
                throw new Exception("加盟商ID不能为空");
            var resp = new MyResponseBase { Item=domain.Item};
            ModularOrFunCode = "AuthorizationAreas.Pre_Company.Dashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            return View("Dashboard", resp);
        }
    }
}
