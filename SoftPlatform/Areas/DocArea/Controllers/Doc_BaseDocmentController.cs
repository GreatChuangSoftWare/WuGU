using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.CellModel;
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
    /// 控制器：Doc_BaseDocment(基本文档管理)
    /// </summary>
    public class Doc_BaseDocmentController : BaseController
    {
        public Doc_BaseDocmentController()
        {
        }

        #region 基本文档管理

        /// <summary>
        /// 基本文档管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 基本文档管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.DocmentPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.DocmentPerson = LoginInfo.UserName;
            resp.Item.DocmentDate = DateTime.Now ;

            resp.FunNameEn = "Add";
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 基本文档管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.DocmentContext = Server.UrlDecode(domain.Item.DocmentContext);
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 基本文档管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            //resp.EditAction = new SoftProjectAreaEntity
            //{
            //    ActionNameEn = "Index",
            //    ControllName = "Doc_BaseDocmentAttachment",
            //    ActionFieldNames = "Doc_BaseDocmentID",
            //    Doc_BaseDocmentID = resp.Item.Doc_BaseDocmentID,
            //};
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 基本文档管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.DocmentContext = Server.UrlDecode(domain.Item.DocmentContext);
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 基本文档管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 基本文档管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailHome(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_BaseDocment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_BaseDocmentID___equal", Value = domain.Item.Doc_BaseDocmentID.ToString() });

            var resp = domain.QueryIndex();
            resp.Item = resp.Items.FirstOrDefault();

            ModularOrFunCode = "DocArea.Doc_BaseDocment.DetailHome";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View("Doc_DetailBase", resp);
        }

        #endregion
    }

}
