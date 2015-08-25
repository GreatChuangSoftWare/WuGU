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
    /// 控制器：C_OrderDetail(顾客订单明细)
    /// </summary>
    public class C_OrderDetailController : BaseController
    {
        public C_OrderDetailController()
        {
        }

        /// <summary>
        /// 顾客订单明细ID--编辑列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            ModularOrFunCode = "CustomerAreas.C_OrderDetail.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            if (domain.Item.C_OrderID == null)
            {
                return View(Design_ModularOrFun.MainView, resp);
            }
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "C_OrderID___equal", Value = domain.Item.C_OrderID.ToString() });
            resp = domain.QueryIndex();

            //return View(Design_ModularOrFun.MainView, resp);
            return View("IndexEdit", resp);
        }

        /// <summary>
        /// 顾客订单明细ID--查看列表
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            //由于仪表盘的原因，去掉以下代码
            //if (domain.Item.C_OrderID == null)
            //    throw new Exception("主键不能为空！");

            ModularOrFunCode = "CustomerAreas.C_OrderDetail.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.Querys.Add(new Query { QuryType = 0, FieldName = "C_OrderID___equal", Value = domain.Item.C_OrderID.ToString() });
            var resp = domain.QueryIndex();
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单明细ID--Popup
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Popup(SoftProjectAreaEntityDomain domain)
        {
            //企业ID
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });

            //根据顾客ID，获取顾客级别
            //Pre_UserID
            var MG_MemberGradeID = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == domain.Item.Pre_UserID).FirstOrDefault().MG_MemberGradeID;
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "MG_MemberGradeID___equal", Value = MG_MemberGradeID.ToString() });

            ModularOrFunCode = "CustomerAreas.C_OrderDetail.Popup";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            domain.PageQueryBase.PageSize = 10000;
            var resp = domain.QueryIndex();
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客订单明细ID--Rows
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Rows(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.C_OrderDetail_Rows();
            ModularOrFunCode = "CustomerAreas.C_OrderDetail.IndexEdit";
            return View("Rows", resp);
        }

        ///// <summary>
        ///// 顾客订单明细ID--Row
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //public ActionResult RowsCompCP(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.C_OrderDetail_RowsCompCP();
        //    ModularOrFunCode = "CustomerAreas.C_OrderDetail.IndexEdit";
        //    return View("Rows", resp);
        //}

    }
}
