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
    /// 控制器：Fra_ProductPrice(商品价格编辑)
    /// </summary>
    public class Fra_ProductPriceController : BaseController
    {
        public Fra_ProductPriceController()
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
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "bValidate___equal", Value = "1" });

            ModularOrFunCode = "FranchiseeAreas.Fra_ProductPrice.IndexEdit";
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
            ModularOrFunCode = "FranchiseeAreas.Fra_ProductPrice.IndexEdit";
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
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "bValidate___equal", Value = "1" });

            ModularOrFunCode = "FranchiseeAreas.Fra_ProductPrice.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }


        /// <summary>
        /// 商品价格--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IndexDetailDashboard(SoftProjectAreaEntityDomain domain)
        {
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            {
                if (domain.Item.Pre_CompanyID == null)
                    throw new Exception("加盟商ID不能为空");
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = domain.Item.Pre_CompanyID.ToString() });
            }
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "bValidate___equal", Value = "1" });

            ModularOrFunCode = "FranchiseeAreas.Fra_ProductPrice.IndexDetailDashboard";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }


        /// <summary>
        /// 自动匹配用商品价格表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonResult AutoCompleteProductPrice(SoftProjectAreaEntityDomain domain)
        {
            //参数：用户ID+商品xxx
            //根据用户ID，查询用户会员等级,可以从缓存中获取
            //查询条件：加盟商ID+会员等级+商品XXX
            var MG_MemberGradeID = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == domain.Item.Pre_UserID).First().MG_MemberGradeID;

            ModularOrFunCode = "FranchiseeAreas.Fra_ProductPrice.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "MG_MemberGradeID___equal", Value = MG_MemberGradeID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "ProductNo__ProductName__Specifications___like", Value = domain.Item.ProductNo__ProductName__Specifications });

            domain.PageQueryBase.PageSize = 10;
            var resp = domain.QueryIndex();

            List<AutocompleteItem> AutocompleteItems = new List<AutocompleteItem>();
            foreach (var item in resp.Items)
            {
                AutocompleteItems.Add(new AutocompleteItem
                {
                    text = "【" + item.ProductNo + "】" + "【" + item.ProductName + "】" + "【" + item.Specifications + "】",
                    label = "【" + item.ProductNo + "】" + "【" + item.ProductName + "】" + "【" + item.Specifications + "】",
                    value = item.Fra_ProductPriceID.ToString()
                });
            }

            return Json(AutocompleteItems, JsonRequestBehavior.AllowGet);
        }

    }
}
