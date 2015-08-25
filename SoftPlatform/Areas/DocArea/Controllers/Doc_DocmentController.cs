using Framework.Core;
using Framework.Web.Mvc;
using SoftPlatform.Common;
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
    /// 控制器：Doc_Docment(文档管理)
    /// </summary>
    public class Doc_DocmentController : BaseController
    {
        public Doc_DocmentController()
        {
        }

        #region 文档管理

        /// <summary>
        /// 文档管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });

            ModularOrFunCode = "DocArea.Doc_Docment.Index"; 
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 文档管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            //DocmentPersonID,DocmentPerson,DocmentDate
            resp.Item.DocmentPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.DocmentPerson = LoginInfo.UserName;
            resp.Item.DocmentDate = DateTime.Now;

            resp.FunNameEn = "Add";
            ModularOrFunCode = "DocArea.Doc_Docment.Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 文档管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.DocmentContext = Server.UrlDecode(domain.Item.DocmentContext);
            var DocmentContextImg = Tool.GetHtmlImageUrlList(domain.Item.DocmentContext);
            if (DocmentContextImg.Count() > 0)
                domain.Item.DocmentThumbnailPath = DocmentContextImg.First();
            var DocmentOutline = Tool.HtmlToText(domain.Item.DocmentContext);
            var DocmentOutlineLength = 30;
            if (DocmentOutline.Length <= 30)
                DocmentOutlineLength = DocmentOutline.Length;
            domain.Item.DocmentOutline = DocmentOutline.Substring(0, DocmentOutlineLength);

            //domain.Item.DocmentOutline = domain.Item.DocmentOutline.Substring(0, 30);

            ModularOrFunCode = "DocArea.Doc_Docment.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 文档管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_Docment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            //resp.EditAction = new SoftProjectAreaEntity
            //{
            //    ActionNameEn = "IndexEdit",
            //    ControllName = "Doc_DocmentAttachment",
            //    ActionFieldNames = "Doc_DocmentID",
            //    Doc_DocmentID = resp.Item.Doc_DocmentID,
            //};

            ModularOrFunCode = "DocArea.Doc_Docment.Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 文档管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.DocmentContext = Server.UrlDecode(domain.Item.DocmentContext);
            var DocmentContextImg = Tool.GetHtmlImageUrlList(domain.Item.DocmentContext);
            if (DocmentContextImg.Count() > 0)
                domain.Item.DocmentThumbnailPath = DocmentContextImg.First();
            var DocmentOutline = Tool.HtmlToText(domain.Item.DocmentContext);
            var DocmentOutlineLength = 50;
            if (DocmentOutline.Length <= 50)
                DocmentOutlineLength = DocmentOutline.Length;
            domain.Item.DocmentOutline = DocmentOutline.Substring(0, DocmentOutlineLength);

            ModularOrFunCode = "DocArea.Doc_Docment.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 文档管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_Docment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.FunNameEn = "Detail";
            ModularOrFunCode = "DocArea.Doc_Docment.Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 文档管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailHome(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_Docment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            ModularOrFunCode = "DocArea.Doc_Docment.DetailHome";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #region 原代码

        ///// <summary>
        ///// 文档管理--浏览(根据类别ID)
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //public ActionResult IndexBrower(SoftProjectAreaEntityDomain domain)
        //{
        //    if (!domain.Querys.QueryDicts.ContainsKey("Doc_CategoryID___equal"))
        //    {
        //        if (domain.Item.Doc_CategoryID == null)
        //            throw new Exception("文档类别不能为空");
        //        domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = domain.Item.Doc_CategoryID.ToString() });
        //    }

        //    ModularOrFunCode = "DocArea.Doc_Docment.IndexBrower";
        //    domain.Design_ModularOrFun = Design_ModularOrFun;
        //    var resp = domain.QueryIndex();

        //    if (Request.IsAjaxRequest())
        //        return View("Doc_List",resp);
        //    resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
        //    return View("Doc_Frame", resp);
        //}

        ///// <summary>
        ///// 文档管理--浏览(根据类别ID)
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //public ActionResult IndexBrowerHome(SoftProjectAreaEntityDomain domain)
        //{
        //    //Doc_CategoryID
        //    if (domain.Item.Doc_CategoryID == null)
        //        throw new Exception("文档类别不能为空");
        //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = domain.Item.Doc_CategoryID.ToString() });

        //    ModularOrFunCode = "DocArea.Doc_Docment.Index";
        //    domain.Design_ModularOrFun = Design_ModularOrFun;
        //    var resp = domain.QueryIndex();

        //    if (Request.IsAjaxRequest())
        //        return View(Design_ModularOrFun.PartialView, resp);
        //    resp.ViewContextName = Design_ModularOrFun.PartialView;
        //    return View(Design_ModularOrFun.MainView, resp);
        //}

        #endregion

        /// <summary>
        /// 文档管理--浏览查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexBrowerDetail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "DocArea.Doc_Docment.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.FunNameEn = "Detail";
            ModularOrFunCode = "DocArea.Doc_Docment.Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View("Doc_Detail", resp);
        }

        /// <summary>
        /// 文档管理--最新通知
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexLatestNotification(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "17" });
            ModularOrFunCode = "DocArea.Doc_Docment.IndexLatestNotification";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }
        
        /// <summary>
        /// 文档管理--学习员地
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexStudyMember(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "18" });
            ModularOrFunCode = "DocArea.Doc_Docment.IndexStudyMember";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--员工风采
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexStaffStyle(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "19" });
            ModularOrFunCode = "DocArea.Doc_Docment.IndexStaffStyle";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--营销情报
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexMarketingInformation(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "20" });

            ModularOrFunCode = "DocArea.Doc_Docment.IndexMarketingInformation";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--总部动态
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexHeadquartersDynamics(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "21" });

            ModularOrFunCode = "DocArea.Doc_Docment.IndexHeadquartersDynamics";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--最新公告
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexLatestStatement(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "22" });

            ModularOrFunCode = "DocArea.Doc_Docment.IndexLatestStatement";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--培训动态
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexTrainingDynamics(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "23" });

            ModularOrFunCode = "DocArea.Doc_Docment.IndexTrainingDynamics";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--加盟商风采--FraStyle
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexFraStyle(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "24" });

            ModularOrFunCode = "DocArea.Doc_Docment.IndexFraStyle";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        /// <summary>
        /// 文档管理--激志人生
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexExcitingLife(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Doc_CategoryID___equal", Value = "25" });

            ModularOrFunCode = "DocArea.Doc_Docment.IndexExcitingLife";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View("Doc_List", resp);
            resp.ViewContextName = "Doc_List";// Design_ModularOrFun.PartialView;
            return View("NavPFrame", resp);
        }

        #endregion
    }

}
