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
    /// 控制器：T_ToolAttachment(工具附件)
    /// </summary>
    public class T_ToolAttachmentController : BaseController
    {
        public T_ToolAttachmentController()
        {
        }

        /// <summary>
        /// 工具附件--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "ToolArea.T_ToolAttachment.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            if (!string.IsNullOrEmpty(domain.Item.ToolAttRefPKTableGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ToolAttRefPKTableGuid });
                resp = domain.QueryIndex();
            }
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具附件--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "ToolArea.T_ToolAttachment.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            if (!string.IsNullOrEmpty(domain.Item.ToolAttRefPKTableGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ToolAttRefPKTableGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具附件--上传
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpLoad(SoftProjectAreaEntityDomain domain)
        {
            #region 保存文件
            if (domain.Item.RefPKTableGuid == null)
                throw new Exception("附件Guid不能为空");
            domain.Item.AttachmentFileSizeDisp = ProjectCommon.FileSizeDisp(domain.Item.AttachmentFileSize);
            ModularOrFunCode = "ToolArea.T_ToolAttachment.Upload";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var respadd = domain.AddSave();
            #endregion

            if (respadd.Item.T_ToolAttachmentID == null)
                throw new Exception("添加失败");
            domain.Item.T_ToolAttachmentID = respadd.Item.T_ToolAttachmentID;
            ModularOrFunCode = "ToolArea.T_ToolAttachment.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Items.Add(resp.Item);
            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;

            return View("Rows", resp);
        }

        /// <summary>
        /// 工具附件--删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Delete(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ToolArea.T_ToolAttachment.Delete";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.DeleteByID();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 工具附件--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ToolArea.T_ToolAttachment.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具附件--下载列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDown(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();

            ModularOrFunCode = "ToolArea.T_ToolAttachment.IndexDown";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            if (!string.IsNullOrEmpty(domain.Item.ToolAttRefPKTableGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ToolAttRefPKTableGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 工具附件--图片列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexImage(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();

            ModularOrFunCode = "ToolArea.T_ToolAttachment.IndexImage";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            if (!string.IsNullOrEmpty(domain.Item.ToolAttRefPKTableGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ToolAttRefPKTableGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
