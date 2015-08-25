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

namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：T_Tool(工具管理)
    /// </summary>
    public class T_ToolController : BaseController
    {
        public T_ToolController()
        {
        }

        /// <summary>
        /// 工具管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            //if (!domain.Querys.QueryDicts.ContainsKey("T_ToolCategoryID___equal"))
            //{
            //    if (domain.Item.T_ToolCategoryID == null)
            //throw new Exception("主键不能为空");
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "T_ToolCategoryID___equal", Value = domain.Item.T_ToolCategoryID.ToString() });
            //}

            ModularOrFunCode = "ToolArea.T_Tool.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.EditAction = new SoftProjectAreaEntity
            {
                LableTitle = "下载",
                ActionNameEn = "IndexDown",
                ControllName = "T_ToolAttachment",
                ActionFieldNames = "T_Tool",
                T_Tool = resp.Item.T_Tool,
            };

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            resp.Item.ToolUploadPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.ToolUploadPerson = LoginInfo.UserName;
            resp.Item.ToolUploadDate = DateTime.Now;
            resp.Item.ToolAttRefPKTableGuid = Guid.NewGuid().ToString();
            ModularOrFunCode = "ToolArea.T_Tool.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.ToolDesc = Server.UrlDecode(domain.Item.ToolDesc);
            ModularOrFunCode = "ToolArea.T_Tool.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 工具管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ToolArea.T_Tool.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "T_ToolAttachment",
                ActionFieldNames = "T_ToolID",
                T_ToolID = resp.Item.T_ToolID,
            };

            ModularOrFunCode = "ToolArea.T_Tool.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.ToolDesc = Server.UrlDecode(domain.Item.ToolDesc);
            ModularOrFunCode = "ToolArea.T_Tool.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 工具管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ToolArea.T_Tool.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
