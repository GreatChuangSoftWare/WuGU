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
    /// 控制器：Fra_Activity(活动管理)
    /// </summary>
    public class Fra_ActivityController : BaseController
    {
        public Fra_ActivityController()
        {
        }

        #region 加盟商

        /// <summary>
        /// 活动管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            }

            ModularOrFunCode = "ActivityAreas.Fra_Activity.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Items.ForEach(p => p.ActivitySelfAtudyTable = p.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'"));

            resp.Items.ForEach(p => p.ActivityConclusion = p.ActivityConclusion.Replace("<input ", "<input disabled='disabled'"));
             
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            //查询最后1个状态为1的：审核流程
            StringBuilder sbsql = new StringBuilder();
            //            string sql = "

            //查询最后1个调研模板为1的：调研模板

            var resp = domain.Fra_Activity_Add();


            ModularOrFunCode = "ActivityAreas.Fra_Activity.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.ActivitySelfAtudyTable = Server.UrlDecode(domain.Item.ActivitySelfAtudyTable);
            domain.Item.ActivityConclusion = Server.UrlDecode(domain.Item.ActivityConclusion);
            domain.Item.ActivityStatuID = 1;

            if (domain.Item.ActivityStartDate != null && domain.Item.ActivityEndDate != null)
            {
                domain.Item.DayLen = domain.Item.ActivityStartDate.GetDay(domain.Item.ActivityEndDate);
            }

            ModularOrFunCode = "ActivityAreas.Fra_Activity.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 活动管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "ActivityAreas.Fra_Activity.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.ActivitySelfAtudyTable = Server.UrlDecode(domain.Item.ActivitySelfAtudyTable);
            domain.Item.ActivityConclusion = Server.UrlDecode(domain.Item.ActivityConclusion);
            if (domain.Item.ActivityStartDate != null && domain.Item.ActivityEndDate != null)
            {
                domain.Item.DayLen = domain.Item.ActivityStartDate.GetDay(domain.Item.ActivityEndDate);
            }

            ModularOrFunCode = "ActivityAreas.Fra_Activity.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }


        /// <summary>
        /// 活动管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Submit(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.ActivitySelfAtudyTable = Server.UrlDecode(domain.Item.ActivitySelfAtudyTable);
            domain.Item.ActivityConclusion = Server.UrlDecode(domain.Item.ActivityConclusion);
            if (domain.Item.ActivityStartDate != null && domain.Item.ActivityEndDate != null)
            {
                domain.Item.DayLen = domain.Item.ActivityStartDate.GetDay(domain.Item.ActivityEndDate);
            }
            domain.Item.ActivityStatuID = 4;
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 活动管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.ActivityFlowContext = resp.Item.ActivityFlowContext.Replace("<input ", "<input disabled='disabled'");
            resp.Item.ActivitySelfAtudyTable = resp.Item.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'");
            resp.Item.ActivityConclusion = resp.Item.ActivityConclusion.Replace("<input ", "<input disabled='disabled'");

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion


        #region 待审核活动

        /// <summary>
        /// 活动管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexWaitExamine(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "ActivityStatuID___equal", Value = "4" });
            ModularOrFunCode = "ActivityAreas.Fra_Activity.IndexWaitExamine";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Items.ForEach(p => p.ActivitySelfAtudyTable = p.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'"));
            resp.Items.ForEach(p => p.ActivityConclusion = p.ActivityConclusion.Replace("<input ", "<input disabled='disabled'"));

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Examine(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Examine";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Item.ActivityAuditDate = DateTime.Now;
            resp.Item.RegionalManagerID = LoginInfo.Sys_LoginInfoID;
            resp.Item.RegionalManager = LoginInfo.UserName;

            resp.Item.ActivitySelfAtudyTable= resp.Item.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'");
            //    resp.Items.ForEach(p => p.ActivityConclusion = p.ActivityConclusion.Replace("<input ", "<input disabled='disabled'"));


            ModularOrFunCode = "ActivityAreas.Fra_Activity.Examine";
            resp.FunNameEn = "Examine";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult ExamineSave(SoftProjectAreaEntityDomain domain)
        {
            //domain.Item.ActivitySelfAtudyTable = Server.UrlDecode(domain.Item.ActivitySelfAtudyTable);
            //domain.Item.ActivityConclusion = Server.UrlDecode(domain.Item.ActivityConclusion);
            //if (domain.Item.ActivityStartDate != null && domain.Item.ActivityEndDate != null)
            //{
            //    domain.Item.DayLen = domain.Item.ActivityStartDate.GetDay(domain.Item.ActivityEndDate);
            //}
            domain.Item.ActivityStatuID = 16;
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Examine";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        //#region 公司活动查询

        ///// <summary>
        ///// 活动管理--查询
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Index2(SoftProjectAreaEntityDomain domain)
        //{
        //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "ActivityStatuID___equal", Value = "16" });
        //    ModularOrFunCode = "ActivityAreas.Fra_Activity.Index2";
        //    domain.Design_ModularOrFun = Design_ModularOrFun;
        //    var resp = domain.QueryIndex();
        //    resp.Items.ForEach(p => p.ActivitySelfAtudyTable = p.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'"));
        //    resp.Items.ForEach(p => p.ActivityConclusion = p.ActivityConclusion.Replace("<input ", "<input disabled='disabled'"));

        //    if (Request.IsAjaxRequest())
        //        return View(Design_ModularOrFun.PartialView, resp);
        //    resp.ViewContextName = Design_ModularOrFun.PartialView;
        //    return View(Design_ModularOrFun.MainView, resp);
        //}

        ///// <summary>
        ///// 活动管理--查看
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //public ActionResult Detail2(SoftProjectAreaEntityDomain domain)
        //{
        //    ModularOrFunCode = "ActivityAreas.Fra_Activity.Detail2";
        //    domain.Design_ModularOrFun = Design_ModularOrFun;
        //    var resp = domain.ByID();

        //    resp.Item.ActivityFlowContext = resp.Item.ActivityFlowContext.Replace("<input ", "<input disabled='disabled'");
        //    resp.Item.ActivitySelfAtudyTable = resp.Item.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'");
        //    resp.Item.ActivityConclusion = resp.Item.ActivityConclusion.Replace("<input ", "<input disabled='disabled'");

        //    resp.FunNameEn = "Detail";
        //    resp.ViewContextName = Design_ModularOrFun.PartialView;
        //    return View(Design_ModularOrFun.MainView, resp);
        //}

        //#endregion

        #region 公司活动查询

        /// <summary>
        /// 活动管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index2(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "ActivityStatuID___equal", Value = "16" });
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Index2";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            //resp.Items.ForEach(p => p.ActivitySelfAtudyTable = p.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'"));
            //resp.Items.ForEach(p => p.ActivityConclusion = p.ActivityConclusion.Replace("<input ", "<input disabled='disabled'"));

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail2(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Detail2";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.ActivityFlowContext = resp.Item.ActivityFlowContext.Replace("<input ", "<input disabled='disabled'");
            resp.Item.ActivitySelfAtudyTable = resp.Item.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'");
            resp.Item.ActivityConclusion = resp.Item.ActivityConclusion.Replace("<input ", "<input disabled='disabled'");

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 公司活动查询：仪表盘

        /// <summary>
        /// 活动管理--查询：仪表盘
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDashboard(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                if (domain.Item.Pre_CompanyID == null)
                    throw new Exception("加盟商ID不有为空");
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = domain.Item.Pre_CompanyID.ToString() });
            }

            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "ActivityStatuID___equal", Value = "16" });
            ModularOrFunCode = "ActivityAreas.Fra_Activity.IndexDashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            //resp.Items.ForEach(p => p.ActivitySelfAtudyTable = p.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'"));
            //resp.Items.ForEach(p => p.ActivityConclusion = p.ActivityConclusion.Replace("<input ", "<input disabled='disabled'"));

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 活动管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailDashboard(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ActivityAreas.Fra_Activity.DetailDashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.ActivityFlowContext = resp.Item.ActivityFlowContext.Replace("<input ", "<input disabled='disabled'");
            resp.Item.ActivitySelfAtudyTable = resp.Item.ActivitySelfAtudyTable.Replace("<input ", "<input disabled='disabled'");
            resp.Item.ActivityConclusion = resp.Item.ActivityConclusion.Replace("<input ", "<input disabled='disabled'");

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单管理--最后1次指导
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Last(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ActivityAreas.Fra_Activity.Last";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Fra_Activity_Last();
            resp.Items.Add(resp.Item);

            resp.FunNameEn = "Last";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

    }
}
