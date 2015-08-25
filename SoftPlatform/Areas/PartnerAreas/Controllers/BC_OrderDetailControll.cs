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
    /// 控制器：BC_OrderDetail(合作商订单明细)
    /// </summary>
    public class BC_OrderDetailController : BaseController
    {
        public BC_OrderDetailController()
        {
        }

        /// <summary>
        /// 合作商订单明细--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "PartnerAreas.BC_OrderDetail.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            if (domain.Item.BC_OrderID == null)
            {
                return View("IndexEdit", resp);
            }
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "BC_OrderID___equal", Value = domain.Item.BC_OrderID.ToString() });
            resp = domain.QueryIndex();
            //return View(Design_ModularOrFun.MainView, resp);
            return View("IndexEdit", resp);
        }

        /// <summary>
        /// 合作商订单明细--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            //由于仪表盘，去掉了验证
            //if (domain.Item.BC_OrderID == null)
            //    throw new Exception("主键不能为空！");

            ModularOrFunCode = "PartnerAreas.BC_OrderDetail.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.Querys.Add(new Query { QuryType = 0, FieldName = "BC_OrderID___equal", Value = domain.Item.BC_OrderID.ToString() });
            var resp = domain.QueryIndex();
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 合作商订单明细--Popup
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Popup(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PartnerAreas.BC_OrderDetail.Popup";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.PageQueryBase.PageSize = 10000;
            var resp = domain.QueryIndex();
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 合作商订单明细--Rows
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Rows(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.BC_OrderDetail_Rows();
            ModularOrFunCode = "PartnerAreas.BC_OrderDetail.IndexEdit";
            return View("Rows", resp);
        }
    }
}
