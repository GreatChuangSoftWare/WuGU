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
    /// 控制器：C_Visit(回访管理)
    /// </summary>
    public class C_VisitController : BaseController
    {
        public C_VisitController()
        {
        }

        #region 顾客：回访管理

        /// <summary>
        /// 回访管理--查询
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

            ModularOrFunCode = "CustomerAreas.C_Visit.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 回访管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            #region 初始化代码
            #endregion
            ModularOrFunCode = "CustomerAreas.C_Visit.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        
        /// <summary>
        /// 回访管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult WaitVisitCompCUAdd(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            #region 初始化代码
            #endregion
            ModularOrFunCode = "CustomerAreas.C_Visit.WaitVisitCompCUAdd";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 回访管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            domain.Item.VisitContext = Server.UrlDecode(domain.Item.VisitContext);

            ModularOrFunCode = "CustomerAreas.C_Visit.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Visit_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 回访管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Visit.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "CustomerAreas.C_Visit.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 回访管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.VisitContext = Server.UrlDecode(domain.Item.VisitContext);
            ModularOrFunCode = "CustomerAreas.C_Visit.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 回访管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Last(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Visit.Last";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Visit_Last();
            resp.Items.Add(resp.Item);

            ModularOrFunCode = "CustomerAreas.C_Visit.Last";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 合作商：回访管理

        /// <summary>
        /// 回访管理--查询
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

            ModularOrFunCode = "CustomerAreas.C_Visit.IndexCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 回访管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCompCP(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            #region 初始化代码
            #endregion
            ModularOrFunCode = "CustomerAreas.C_Visit.AddCompCP";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 回访管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            domain.Item.VisitContext = Server.UrlDecode(domain.Item.VisitContext);

            ModularOrFunCode = "CustomerAreas.C_Visit.AddCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            //var resp = domain.AddSave();
            var resp = domain.C_Visit_AddSave();

            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 回访管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Visit.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "CustomerAreas.C_Visit.EditCompCP";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 回访管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.VisitContext = Server.UrlDecode(domain.Item.VisitContext);
            ModularOrFunCode = "CustomerAreas.C_Visit.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        
    }
}
