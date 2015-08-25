using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Core;
using Framework.Web.Mvc.Sys;
using SoftProject.Domain;
using SoftProject.CellModel;

namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：O_OrderDetail(订单明细)
    /// </summary>
    public class O_OrderDetailController : BaseController
    {
        public O_OrderDetailController()
        {
        }

        /// <summary>
        /// 订单明细--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "OrderAreas.O_OrderDetail.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            if (domain.Item.O_OrderID == null)
            {
                return View(Design_ModularOrFun.MainView, resp);
                //throw new Exception("主键不能为空！");
            }
            ModularOrFunCode = "OrderAreas.O_OrderDetail.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "O_OrderID___equal", Value = domain.Item.O_OrderID.ToString() });
            resp = domain.QueryIndex();

            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单明细--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            //在新建的加盟商：由于在加盟商仪表盘上的最后一次无订单，所以去掉了以下代码
            //if (domain.Item.O_OrderID == null)
            //    throw new Exception("主键不能为空！");

            ModularOrFunCode = "OrderAreas.O_OrderDetail.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.Querys.Add(new Query { QuryType = 0, FieldName = "O_OrderID___equal", Value = domain.Item.O_OrderID.ToString() });
            var resp = domain.QueryIndex();
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 订单明细--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexHandle(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.O_OrderID == null)
                throw new Exception("主键不能为空！");
            ModularOrFunCode = "OrderAreas.O_OrderDetail.IndexHandle";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.Querys.Add(new Query { QuryType = 0, FieldName = "O_OrderID___equal", Value = domain.Item.O_OrderID.ToString() });
            var resp = domain.QueryIndex();
            return View("IndexHandle", resp);
        }

        //[HttpGet]
        //public ActionResult ShoppingCart(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.O_OrderDetail_ShoppingCart();
        //    ModularOrFunCode = "OrderAreas.O_OrderDetail.ShoppingCart";
        //    ModularOrFunCodeEdit = "P_ProductAreas.O_OrderDetail.Fra_OrderPK";
        //    MenuCode = "OrderAreas.Fra_Franchisee.WantOrder";

        //    //base.TabModularOrFunCode = "C_CustomerAreas.C_Customer.Tab";
        //    //return View("ShoppingCart", resp);
        //    resp.ViewContextName = Design_ModularOrFun.PartialView;
        //    return View(Design_ModularOrFun.MainView, resp);
        //}

        [HttpGet]
        public ActionResult Popup(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "OrderAreas.O_OrderDetail.Popup";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.PageQueryBase.PageSize = 10000;
            var resp = domain.QueryIndex();
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        public ActionResult Rows(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.O_OrderDetail_Rows();
            ModularOrFunCode = "OrderAreas.O_OrderDetail.IndexHandle";
            return View("Rows", resp);
        }

    }
}
