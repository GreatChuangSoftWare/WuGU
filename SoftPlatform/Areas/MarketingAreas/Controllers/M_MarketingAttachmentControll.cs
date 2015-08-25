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
    /// 控制器：M_MarketingAttachment(营销视频附件)
    /// </summary>
    public class M_MarketingAttachmentController : BaseController
    {
        public M_MarketingAttachmentController()
        {
        }

        #region 视频附件

        /// <summary>
        /// 营销附件--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            if (!string.IsNullOrEmpty(domain.Item.VideoFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.VideoFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销附件--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销附件--下载列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDown(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexDown";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty( domain.Item.VideoFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.VideoFileNameGuid });
                resp = domain.QueryIndex();
            }
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销附件--图片列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexImage(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexImage";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.VideoFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.VideoFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销附件--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.VideoFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.VideoFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销附件--上传
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
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.Upload";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var respadd = domain.AddSave();
            #endregion

            #region 查询
            if (respadd.Item.M_MarketingAttachmentID == null)
                throw new Exception("添加失败");
            domain.Item.Doc_DocmentAttachmentID = respadd.Item.Doc_DocmentAttachmentID;
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Items.Add(resp.Item);
            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;

            #endregion
            return View("Rows", resp);
        }

        /// <summary>
        /// 营销附件--删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Delete(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.Delete";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.DeleteByID();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 讲义附件

        /// <summary>
        /// 营销讲义附件--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEditHandout(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexEditHandout";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            if (!string.IsNullOrEmpty(domain.Item.HandoutFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.HandoutFileNameGuid });
                resp = domain.QueryIndex();
            }
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销讲义附件--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetailHandout(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.HandoutFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.HandoutFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销讲义附件--上传
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpLoadHandout(SoftProjectAreaEntityDomain domain)
        {
            #region 保存文件
            if (domain.Item.RefPKTableGuid == null)
                throw new Exception("附件Guid不能为空");
            domain.Item.AttachmentFileSizeDisp = ProjectCommon.FileSizeDisp(domain.Item.AttachmentFileSize);
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.Upload";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var respadd = domain.AddSave();
            #endregion

            #region 查询
            if (respadd.Item.M_MarketingAttachmentID == null)
                throw new Exception("添加失败");
            domain.Item.M_MarketingAttachmentID = respadd.Item.M_MarketingAttachmentID;
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Items.Add(resp.Item);
            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;

            #endregion
            return View("Rows", resp);
        }

        /// <summary>
        /// 营销讲义附件--下载列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDownHandout(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexDown";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.HandoutFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.HandoutFileNameGuid });
                resp = domain.QueryIndex();
            }
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销讲义附件--图片列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexImageHandout(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_MarketingAttachment.IndexImage";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.HandoutFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.HandoutFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion
    }
}
