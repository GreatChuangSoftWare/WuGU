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
    /// 控制器：C_Recharge(顾客充值)
    /// </summary>
    public class C_RechargeController : BaseController
    {
        public C_RechargeController()
        {
        }

        #region 顾客充值

        /// <summary>
        /// 顾客充值--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            #region 查询
            //if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            //{
            //    if (domain.Item.Pre_UserID == null)
            //        throw new Exception("主键不能为空");
            //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            //}
            if (domain.Item.Pre_UserID == null)
                throw new Exception("主键不能为空");
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });

            ModularOrFunCode = "CustomerAreas.C_Recharge.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            #endregion

            #region 余额

            resp.Item.FundBalance= SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p=>p.Pre_UserID==domain.Item.Pre_UserID).FirstOrDefault().FundBalance;
            
            #endregion

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 合作商充值

        /// <summary>
        /// 合作商充值--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexCompCP(SoftProjectAreaEntityDomain domain)
        {
            ////domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            //if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            //{
            //    if (domain.Item.Pre_UserID == null)
            //        throw new Exception("主键不能为空");
            //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            //}
            if (domain.Item.Pre_UserID == null)
                throw new Exception("主键不能为空");
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });

            ModularOrFunCode = "CustomerAreas.C_Recharge.IndexCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            #region 余额

            resp.Item.FundBalance = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == domain.Item.Pre_UserID).FirstOrDefault().FundBalance;

            #endregion

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 充值：添加、编辑

        /// <summary>
        /// 顾客充值--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            resp.Item.Pre_UserID = domain.Item.Pre_UserID;
            ModularOrFunCode = "CustomerAreas.C_Recharge.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客充值--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            ModularOrFunCode = "CustomerAreas.C_Recharge.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Recharge_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 顾客充值--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Recharge.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "CustomerAreas.C_Recharge.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 顾客充值--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Recharge.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.C_Recharge_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 顾客充值--Row
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Recharge.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Items.Add(resp.Item);
            resp.FunNameEn = "Row";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View("Rows", resp);
        }

        #endregion

        /// <summary>
        /// 顾客充值--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Last(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "CustomerAreas.C_Recharge.Last";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.C_Recharge_Last();
            resp.Items.Add(resp.Item);

            ModularOrFunCode = "CustomerAreas.C_Recharge.Last";
            resp.FunNameEn = "Last";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
