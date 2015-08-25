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
    /// 控制器：Doc_DocmentAttachment(基本文档附件)
    /// </summary>
    public class Doc_DocmentAttachmentController : BaseController
    {
        public Doc_DocmentAttachmentController()
        {
        }

        /// <summary>
        /// 基本文档附件--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_DocmentID___equal", Value = domain.Item.Doc_DocmentID.ToString() });
            ModularOrFunCode = "DocArea.Doc_DocmentAttachment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_DocmentID___equal", Value = domain.Item.Doc_DocmentID.ToString() });
            ModularOrFunCode = "DocArea.Doc_DocmentAttachment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpLoad(SoftProjectAreaEntityDomain domain)
        {
            #region 保存文件

            if (domain.Item.Doc_DocmentID == null)
                throw new Exception("文档ID不能为空");
            domain.Item.AttachmentFileSizeDisp = ProjectCommon.FileSizeDisp(domain.Item.AttachmentFileSize);
            ModularOrFunCode = "DocArea.Doc_DocmentAttachment.Upload";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var respadd = domain.AddSave();
            #endregion

            #region 查询
            if (respadd.Item.Doc_DocmentAttachmentID == null)
                throw new Exception("添加失败");
            domain.Item.Doc_DocmentAttachmentID = respadd.Item.Doc_DocmentAttachmentID;
            ModularOrFunCode = "DocArea.Doc_DocmentAttachment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Items.Add(resp.Item);
            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;

            #endregion
            return View("Rows", resp);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Delete(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_DocmentAttachment.Delete";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.DeleteByID();
            return new HJsonResult(new { Data = resp });
        }

    }
}
