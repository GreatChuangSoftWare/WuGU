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
    /// 控制器：Doc_Category(文档类别管理)
    /// </summary>
    public class Doc_CategoryController : BaseController
    {
        public Doc_CategoryController()
        {
            
        }

        /// <summary>
        /// 文档类别管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            if (domain.Querys.QueryDicts.ContainsKey("ParentDoc_CategoryID___equal"))
            {
                if (domain.Querys.QueryDicts["ParentDoc_CategoryID___equal"].Value == "1")
                {
                    domain.Querys.Remove(domain.Querys.QueryDicts["ParentDoc_CategoryID___equal"]);
                }
            }

            ModularOrFunCode = "DocArea.Doc_Category.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            //MenuCode = "AuthorizationPanel";

            if (Request.IsAjaxRequest())
                return View("TableContext", resp);
            resp.TreeID = "Doc_CategoryID";
            resp.TreeQueryType = 2;

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 文档类别管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            ModularOrFunCode = "DocArea.Doc_Category.Add";
            //
            //resp.FunNameEn = ActionName;
            return View("PopupEdit", resp);
        }

        /// <summary>
        /// 文档类别管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;

            if (domain.Item.ParentDoc_CategoryID == null)
                domain.Item.ParentDoc_CategoryID = 0;
            ModularOrFunCode = "DocArea.Doc_Category.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.AddSave();

            domain.Item.Doc_CategoryID = resp.Item.Doc_CategoryID;
            domain.Doc_Category_AddCache();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 文档类别管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_Category.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            ModularOrFunCode = "DocArea.Doc_Category.Edit";
            return View("PopupEdit", resp);
        }

        /// <summary>
        /// 文档类别管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.Doc_CategoryID == 1)
                domain.Item.ParentDoc_CategoryID = 0;
            ModularOrFunCode = "DocArea.Doc_Category.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.EditSave();

            //domain.Item.Doc_CategoryID = resp.Item.Doc_CategoryID;
            domain.Doc_Category_UpdateCache();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 文档类别管理--Row
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_Category.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Items.Add(resp.Item);
            //
            //resp.FunNameEn = ActionName;
            return View("Rows", resp);
        }

    }
}
