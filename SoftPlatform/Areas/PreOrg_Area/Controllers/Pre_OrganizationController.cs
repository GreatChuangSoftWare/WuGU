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
    /// 控制器：Pre_Organization(组织机构管理)
    /// </summary>
    public class Pre_OrganizationController : BaseController
    {
        public Pre_OrganizationController()
        {
        }

        #region 公司组织机构

        /// <summary>
        /// 组织机构管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Querys.QueryDicts.ContainsKey("ParentPre_OrganizationID___equal"))
            {
                if (domain.Querys.QueryDicts["ParentPre_OrganizationID___equal"].Value == "1")
                {
                    domain.Querys.Remove(domain.Querys.QueryDicts["ParentPre_OrganizationID___equal"]);
                    //Querys.Add(new Query { QuryType = 0, FieldName = "ParentToolCategoryID___equal", Value = "1" });
                }
            }
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "1" });
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Item.LoginCategoryID = 1;
            if (Request.IsAjaxRequest())
                return View("TableContext", resp);
            resp.TreeID = "Pre_OrganizationID";
            resp.TreeQueryType = 2;
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 组织机构管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 1;
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Add";
            return View("PopupEdit", resp);
        }

        /// <summary>
        /// 组织机构管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 1;
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();

            domain.Item.Pre_OrganizationID = resp.Item.Pre_OrganizationID;
            domain.Pre_Organization_AddCache();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 组织机构管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Item.LoginCategoryID = 1;
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Edit";
            return View("PopupEdit", resp);
        }

        /// <summary>
        /// 组织机构管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.Pre_OrganizationID == 1)
                domain.Item.ParentPre_OrganizationID = 0;
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            domain.Pre_Organization_UpdateCache();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 组织机构管理--Row
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Items.Add(resp.Item);
            ModularOrFunCode = "PreOrg_Area.Pre_Organization.Index";
            return View("Rows", resp);
        }

        #endregion

    }
}
