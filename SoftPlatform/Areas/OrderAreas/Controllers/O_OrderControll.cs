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
    /// 控制器：O_Order(订单管理)
    /// </summary>
    public class O_OrderController : BaseController
    {
        public O_OrderController()
        {
        }

        #region 加盟商

        /// <summary>
        /// 订单管理--我的订单
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });

            ModularOrFunCode = "OrderAreas.O_Order.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 消费管理--添加到购物车
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddShoppingCart(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.O_Order_AddShoppingCart();

            return View("AddShoppingCartMessage");
        }

        /// <summary>
        /// 订单管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID",
                O_OrderID = resp.Item.O_OrderID,
            };

            ModularOrFunCode = "OrderAreas.O_Order.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.O_Order_EditSave();
            return new HJsonResult(new { Data = resp });
        }


        #region 购物车

        /// <summary>
        /// 订单管理--购物车
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult ShoppingCart(SoftProjectAreaEntityDomain domain)
        {
            //ModularOrFunCode = "OrderAreas.O_Order.Detail";
            //domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.O_Order_GetShoppingCart();
            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID",
                O_OrderID = resp.Item.O_OrderID,
            };

            ModularOrFunCode = "OrderAreas.O_Order.ShoppingCart";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 消费管理--提交保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult Submit(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.OrderStatuID = 4;
            ModularOrFunCode = "OrderAreas.O_Order.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.O_Order_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        /// <summary>
        /// 订单管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID",
                O_OrderID = resp.Item.O_OrderID,
            };

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 处理

        /// <summary>
        /// 待处理订单
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WaitHandleIndex(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "OrderStatuID___equal", Value = "4" });
            ModularOrFunCode = "OrderAreas.O_Order.WaitHandleIndex";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 加盟商订单--订单处理
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Handle(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.Item.OrderHandleDate = DateTime.Now;
            resp.Item.OrderHandlePerson = LoginInfo.UserName;
            resp.Item.OrderHandlePersonID = LoginInfo.Sys_LoginInfoID;
            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexHandle",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID",
                O_OrderID = resp.Item.O_OrderID
            };

            //处理并返回
            ModularOrFunCode = "OrderAreas.O_Order.Handle";
            resp.FunNameEn = "Handle";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 加盟商订单--处理保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        //[ActionName("HandleSave")]
        public HJsonResult HandleSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.OrderStatuID = 16;
            ModularOrFunCode = "OrderAreas.O_Order.Handle";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.O_Order_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        /// <summary>
        /// 订单管理--订单查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexSearch(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "OrderStatuID___equal", Value = "16" });

            ModularOrFunCode = "OrderAreas.O_Order.IndexSearch";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail2(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.Detail2";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID",
                O_OrderID = resp.Item.O_OrderID,
            };

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }


        /// <summary>
        /// 订单管理--订单查询
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

            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "OrderStatuID___equal", Value = "16" });

            ModularOrFunCode = "OrderAreas.O_Order.IndexDashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailDashboard(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.DetailDashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID",
                O_OrderID = resp.Item.O_OrderID,
            };

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单管理--最后1个订单
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult LastOrder(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_Order.LastOrder";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.O_Order_LastOrder();
            resp.Items.Add(resp.Item);

            resp.ChildAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "O_OrderDetail",
                ActionFieldNames = "O_OrderID"
            };

            resp.FunNameEn = "LastOrder";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }


    }
}
