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
    /// 控制器：BC_PartnerProductPrice(商品价格编辑)
    /// </summary>
    public class BC_PartnerProductPriceControll : BaseController
    {
        public BC_PartnerProductPriceControll()
        {
        }

        /// <summary>
        /// 商品价格--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            }

            if (domain.Item.Pre_UserID == null)
                throw new Exception("合作商ID不能为空");
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            }
            ModularOrFunCode = "FranchiseeAreas.BC_PartnerProductPrice.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 商品价格--保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult IndexEditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Items = domain.Item.Items;
            ModularOrFunCode = "FranchiseeAreas.BC_PartnerProductPrice.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ExcuteEnumsNew();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 商品价格--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            }

            if (domain.Item.Pre_UserID == null)
                throw new Exception("合作商ID不能为空");
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            }

            ModularOrFunCode = "FranchiseeAreas.BC_PartnerProductPrice.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
