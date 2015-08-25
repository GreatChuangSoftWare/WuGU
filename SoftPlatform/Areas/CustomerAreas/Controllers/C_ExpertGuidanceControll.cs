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
    /// 控制器：C_ExpertGuidance(专家指导)
    /// </summary>
    public class C_ExpertGuidanceController : BaseController
    {
        public C_ExpertGuidanceController()
        {
        }

        #region 顾客：专家指导

        /// <summary>
        /// 专家指导--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            {
                if (domain.Item.Pre_UserID == null)
                    throw new Exception("主键不能为空");
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            }

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 专家指导--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 专家指导--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            domain.Item.SymptomDesc = Server.UrlDecode(domain.Item.SymptomDesc);
            domain.Item.Suggestions = Server.UrlDecode(domain.Item.Suggestions);
            domain.Item.Formula = Server.UrlDecode(domain.Item.Formula);

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 专家指导--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 专家指导--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.SymptomDesc = Server.UrlDecode(domain.Item.SymptomDesc);
            domain.Item.Suggestions = Server.UrlDecode(domain.Item.Suggestions);
            domain.Item.Formula = Server.UrlDecode(domain.Item.Formula);
            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 专家指导--最近1次
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Last(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Last";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_ExpertGuidance_Last();
            resp.Items.Add(resp.Item);

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.Last";
            resp.FunNameEn = "Last";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 合作商：专家指导

        /// <summary>
        /// 专家指导--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexCompCP(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            {
                if (domain.Item.Pre_UserID == null)
                    throw new Exception("主键不能为空");
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            }

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.IndexCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 专家指导--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCompCP(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.AddCompCP";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 专家指导--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            domain.Item.SymptomDesc = Server.UrlDecode(domain.Item.SymptomDesc);
            domain.Item.Suggestions = Server.UrlDecode(domain.Item.Suggestions);
            domain.Item.Formula = Server.UrlDecode(domain.Item.Formula);

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.AddCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 专家指导--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.EditCompCP";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 专家指导--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.SymptomDesc = Server.UrlDecode(domain.Item.SymptomDesc);
            domain.Item.Suggestions = Server.UrlDecode(domain.Item.Suggestions);
            domain.Item.Formula = Server.UrlDecode(domain.Item.Formula);
            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 专家指导--最近1次
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult LastCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.LastCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_ExpertGuidance_Last();
            resp.Items.Add(resp.Item);

            ModularOrFunCode = "CustomerAreas.C_ExpertGuidance.LastCompCP";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

    }
}
