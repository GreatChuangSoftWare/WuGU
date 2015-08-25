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
    /// 控制器：P_ProductAttachment(商品附件)
    /// </summary>
    public class P_ProductAttachmentController : BaseController
    {
        public P_ProductAttachmentController()
        {
        }

        /// <summary>
        /// 商品附件--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "P_ProductID___equal", Value = domain.Item.P_ProductID.ToString() });
            //ModularOrFunCode = "ProductAreas.P_ProductAttachment.IndexEdit";
            //domain.Design_ModularOrFun = Design_ModularOrFun;
            //var resp = domain.QueryIndex();

            //if (Request.IsAjaxRequest())
            //    return View(Design_ModularOrFun.PartialView, resp);
            //resp.ViewContextName = Design_ModularOrFun.PartialView;
            //return View(Design_ModularOrFun.MainView, resp);

            //////////////////////////////////////////////////
            var resp = new MyResponseBase();
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            if (!string.IsNullOrEmpty(domain.Item.ImageFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ImageFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);

        }

        /// <summary>
        /// 商品附件--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "P_ProductID___equal", Value = domain.Item.P_ProductID.ToString() });
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.ImageFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ImageFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

            /// <summary>
        /// 商品附件--上传
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpLoad(SoftProjectAreaEntityDomain domain)
        {
            #region 原代码

            //#region 保存文件

            //if (domain.Item.P_ProductID == null)
            //    throw new Exception("ID不能为空");
            //domain.Item.AttachmentFileSizeDisp = ProjectCommon.FileSizeDisp(domain.Item.AttachmentFileSize);
            //ModularOrFunCode = "ProductAreas.P_ProductAttachment.Upload";
            //domain.Design_ModularOrFun = Design_ModularOrFun;
            //var respadd = domain.AddSave();
            //#endregion

            //#region 查询
            //if (respadd.Item.P_ProductAttachmentID == null)
            //    throw new Exception("添加失败");
            //domain.Item.Doc_DocmentAttachmentID = respadd.Item.Doc_DocmentAttachmentID;
            //ModularOrFunCode = "ProductAreas.P_ProductAttachment.Index";
            //domain.Design_ModularOrFun = Design_ModularOrFun;
            //var resp = domain.ByID();

            //resp.Items.Add(resp.Item);
            //resp.FunNameEn = "Detail";
            //resp.ViewContextName = Design_ModularOrFun.PartialView;

            //#endregion
            //return View("Rows", resp);
            #endregion
            ///////////////////////////////////////////
            #region 保存文件
            if (domain.Item.RefPKTableGuid == null)
                throw new Exception("附件Guid不能为空");
            domain.Item.AttachmentFileSizeDisp = ProjectCommon.FileSizeDisp(domain.Item.AttachmentFileSize);
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.Upload";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var respadd = domain.AddSave();
            #endregion

            #region 查询
            if (respadd.Item.P_ProductAttachmentID == null)
                throw new Exception("添加失败");
            domain.Item.P_ProductAttachmentID = respadd.Item.P_ProductAttachmentID;
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.IndexEdit"; 
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Items.Add(resp.Item);
            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;

            #endregion
            return View("Rows", resp);

        }

        /// <summary>
        /// 商品附件--删除
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Delete(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.Delete";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.DeleteByID();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 商品附件--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 商品附件--下载列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDown(SoftProjectAreaEntityDomain domain)
        {

            ModularOrFunCode = "ProductAreas.P_ProductAttachment.IndexDown";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.ImageFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ImageFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 商品附件--图片列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexImage(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_ProductAttachment.IndexImage";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = new MyResponseBase();
            if (!string.IsNullOrEmpty(domain.Item.ImageFileNameGuid))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "RefPKTableGuid___equal", Value = domain.Item.ImageFileNameGuid });
                resp = domain.QueryIndex();
            }

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
