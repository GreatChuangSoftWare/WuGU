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
    /// 控制器：C_Order(顾客订单)
    /// </summary>
    public class C_OrderController : BaseController
    {
        public C_OrderController()
        {
        }

        #region 顾客订单

        /// <summary>
        /// 顾客订单--查询
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

            ModularOrFunCode = "CustomerAreas.C_Order.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "C_OrderDetail",
                ActionFieldNames = "C_OrderID",
                C_OrderID = resp.Item.C_OrderID,
            };

            ModularOrFunCode = "CustomerAreas.C_Order.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            ModularOrFunCode = "CustomerAreas.C_Order.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Order_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 顾客订单--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "C_OrderDetail",
                ActionFieldNames = "C_OrderID",
                C_OrderID = resp.Item.C_OrderID,
            };

            ModularOrFunCode = "CustomerAreas.C_Order.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.C_Order_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 顾客订单--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 合作商订单

        /// <summary>
        /// 顾客订单--查询
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

            ModularOrFunCode = "CustomerAreas.C_Order.IndexCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCompCP(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "C_OrderDetail",
                ActionFieldNames = "C_OrderID",
                C_OrderID = resp.Item.C_OrderID,
            };

            ModularOrFunCode = "CustomerAreas.C_Order.AddCompCP";
            resp.FunNameEn = "AddCompCP";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            ModularOrFunCode = "CustomerAreas.C_Order.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Order_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 顾客订单--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "C_OrderDetail",
                ActionFieldNames = "C_OrderID",
                C_OrderID = resp.Item.C_OrderID,
            };

            ModularOrFunCode = "CustomerAreas.C_Order.EditCompCP";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.C_Order_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 顾客订单--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.DetailCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "DetailCompCP";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        /// <summary>
        /// 顾客订单--最近1次订单
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Last(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Order.Last";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Order_Last();
            resp.Items.Add(resp.Item);
            resp.ChildAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "C_OrderDetail",
                ActionFieldNames = "C_OrderID",
                C_OrderID = resp.Item.C_OrderID,
            };

            ModularOrFunCode = "CustomerAreas.C_Order.Last";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
