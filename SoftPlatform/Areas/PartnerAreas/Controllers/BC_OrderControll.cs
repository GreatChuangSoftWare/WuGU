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
    /// 控制器：BC_Order(合作商订单)
    /// </summary>
    public class BC_OrderController : BaseController
    {
        public BC_OrderController()
        {
        }

        /// <summary>
        /// 合作商订单--查询
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

            ModularOrFunCode = "PartnerAreas.BC_Order.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 合作商订单--添加查询
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
                ControllName = "BC_OrderDetail",
                ActionFieldNames = "BC_OrderID",
                BC_OrderID = resp.Item.BC_OrderID,
            };

            ModularOrFunCode = "PartnerAreas.BC_Order.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 合作商订单--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PartnerAreas.BC_Order.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.BC_Order_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 合作商订单--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PartnerAreas.BC_Order.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexEdit",
                ControllName = "BC_OrderDetail",
                ActionFieldNames = "BC_OrderID",
                BC_OrderID = resp.Item.BC_OrderID,
            };

            ModularOrFunCode = "PartnerAreas.BC_Order.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 合作商订单--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PartnerAreas.BC_Order.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.BC_Order_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 合作商订单--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PartnerAreas.BC_Order.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();
            resp.ChildAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "BC_OrderDetail",
                ActionFieldNames = "BC_OrderID",
                BC_OrderID = resp.Item.BC_OrderID,
            };

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 合作商订单--最近1次订单
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Last(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "PartnerAreas.BC_Order.Last";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.BC_Order_Last();
            resp.Items.Add(resp.Item);
            resp.ChildAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "BC_OrderDetail",
                ActionFieldNames = "BC_OrderID",
                BC_OrderID = resp.Item.BC_OrderID,
            };

            ModularOrFunCode = "PartnerAreas.BC_Order.Last";
            resp.FunNameEn = "Last";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
