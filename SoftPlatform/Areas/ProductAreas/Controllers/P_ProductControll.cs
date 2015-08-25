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
    /// 控制器：P_Product(商品管理)
    /// </summary>
    public class P_ProductController : BaseController
    {
        public P_ProductController()
        {
        }

        #region 公司：后台管理

        /// <summary>
        /// 商品管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_Product.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.TreeID = "P_CategoryID";
            resp.TreeQueryType = 1;
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 商品管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            #region 初始化代码
            #endregion
            ModularOrFunCode = "ProductAreas.P_Product.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 商品管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            domain.Item.ProductContext = Server.UrlDecode(domain.Item.ProductContext);
            ModularOrFunCode = "ProductAreas.P_Product.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 商品管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_Product.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            //resp.EditAction = new SoftProjectAreaEntity
            //{
            //    ActionNameEn = "IndexEdit",
            //    ControllName = "P_ProductAttachment",
            //    ActionFieldNames = "P_ProductID",
            //    P_ProductID = resp.Item.P_ProductID,
            //};

            ModularOrFunCode = "ProductAreas.P_Product.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 商品管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.ProductContext = Server.UrlDecode(domain.Item.ProductContext);

            ModularOrFunCode = "ProductAreas.P_Product.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 商品管理--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_Product.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditAction = new SoftProjectAreaEntity
            {
                ActionNameEn = "IndexDetail",
                ControllName = "P_ProductAttachment",
                ActionFieldNames = "P_ProductID",
                P_ProductID = resp.Item.P_ProductID,
            };

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 加盟商：我要订货

        /// <summary>
        /// 按类别查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductByCategory(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_Product.ProductByCategory";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.TreeID = "P_CategoryID";
            resp.TreeQueryType = 1;
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 新品上架
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductByNew(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { FieldName = "BNewID___equal", QuryType = 0, Value = "1" });
            ModularOrFunCode = "ProductAreas.P_Product.ProductByNew";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 精品推荐
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductByBoutique(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { FieldName = "BBoutiqueID___equal", QuryType = 0, Value = "1" });
            ModularOrFunCode = "ProductAreas.P_Product.ProductByBoutique";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;// "TableContext";
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        [HttpGet]
        public ActionResult ProductDetail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "ProductAreas.P_Product.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            return View("ProductDetail", resp);
        }

        /// <summary>
        /// 自动匹配用户名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonResult AutoCompleteProduct(SoftProjectAreaEntityDomain domain)//string key, int Pre_UserID)
        {
            ModularOrFunCode = "ProductAreas.P_Product.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
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
                    value = item.P_ProductID.ToString()
                });
            }

            return Json(AutocompleteItems, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// 商品管理--新品上市
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult IndexByNewHome(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.P_Product_ByNewHome();
        //    return View(Design_ModularOrFun.MainView, resp);
        //}
    }
}
