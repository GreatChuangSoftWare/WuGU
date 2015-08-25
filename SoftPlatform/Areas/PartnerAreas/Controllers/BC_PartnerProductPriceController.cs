using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class BC_PartnerProductPriceController : BaseController
    {
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
            ModularOrFunCode = "PartnerAreas.BC_PartnerProductPrice.IndexEdit";
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
            ModularOrFunCode = "PartnerAreas.BC_PartnerProductPrice.IndexEdit";
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

            ModularOrFunCode = "PartnerAreas.BC_PartnerProductPrice.IndexDetail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        ///// <summary>
        ///// 自动匹配用户名
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public JsonResult AutoCompletePartnerProduct(SoftProjectAreaEntityDomain domain)//string key, int Pre_UserID)
        //{
        //    //key = key.Trim().ToUpper();
        //    //var keyValueList = new List<Pre_UsersView>();
        //    //if (Pre_UsersDomain.Pre_UsersViews != null && Pre_UsersDomain.Pre_UsersViews.Count > 0)
        //    //{
        //    //    keyValueList = Pre_UsersDomain.Pre_UsersViews.Where(p => p.UserName.Contains(key)).ToList();
        //    //}
        //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
        //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "ProductNo__ProductName__Specifications___like", Value = domain.Item.ProductNo__ProductName__Specifications });

        //    var resp = domain.BC_PartnerProductPrice_AutoCompletePartnerProduct();

        //    List<AutocompleteItem> AutocompleteItems = new List<AutocompleteItem>();
        //    //foreach (var item in resp.Items)
        //    //{
        //    //    AutocompleteItems.Add(new AutocompleteItem
        //    //    {
        //    //        text = item.ProductNo,
        //    //        label = item.ProductNo,
        //    //        value = item.BC_PartnerProductPriceID.ToString()
        //    //    });
        //    //}
        //    AutocompleteItems.Add(new AutocompleteItem
        //    {
        //        text = "12345678",
        //        label = "12345678",
        //        value = "12345678"
        //    });
        //    AutocompleteItems.Add(new AutocompleteItem
        //    {
        //        text = "22345678",
        //        label = "22345678",
        //        value = "22345678"
        //    });
        //    AutocompleteItems.Add(new AutocompleteItem
        //    {
        //        text = "32345678",
        //        label = "32345678",
        //        value = "32345678"
        //    });

        //    return Json(AutocompleteItems, JsonRequestBehavior.AllowGet);
        //}

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
            //var MG_MemberGradeID = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(p => p.Pre_UserID == domain.Item.Pre_UserID).First().MG_MemberGradeID;

            ModularOrFunCode = "PartnerAreas.BC_PartnerProductPrice.IndexEdit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "MG_MemberGradeID___equal", Value = MG_MemberGradeID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "ProductNo__ProductName__Specifications___like", Value = domain.Item.ProductNo__ProductName__Specifications });
            if (domain.Item.Pre_UserID == null)
                throw new Exception("合作商ID不能为空");
            if (!domain.Querys.QueryDicts.ContainsKey("Pre_UserID___equal"))
            {
                domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_UserID___equal", Value = domain.Item.Pre_UserID.ToString() });
            }

            domain.PageQueryBase.PageSize = 10;
            var resp = domain.QueryIndex();

            List<AutocompleteItem> AutocompleteItems = new List<AutocompleteItem>();
            foreach (var item in resp.Items)
            {
                AutocompleteItems.Add(new AutocompleteItem
                {
                    text = "【" + item.ProductNo + "】" + "【" + item.ProductName + "】" + "【" + item.Specifications + "】",
                    label = "【" + item.ProductNo + "】" + "【" + item.ProductName + "】" + "【" + item.Specifications + "】",
                    value = item.BC_PartnerProductPriceID.ToString()
                });
            }

            return Json(AutocompleteItems, JsonRequestBehavior.AllowGet);
        }
    }
}
