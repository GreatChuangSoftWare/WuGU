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
    /// 控制器：M_Marketing(营销课堂)
    /// </summary>
    public class M_MarketingController : BaseController
    {
        public M_MarketingController()
        {
        }

        /// <summary>
        /// 营销课堂--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            //if (!domain.Querys.QueryDicts.ContainsKey("M_MarketingCategoryID___equal"))
            //{
            //    if (domain.Item.M_MarketingCategoryID == null)
            //throw new Exception("主键不能为空");
            //domain.Querys.Add(new Query { QuryType = 0, FieldName = "M_MarketingCategoryID___equal", Value = domain.Item.M_MarketingCategoryID.ToString() });
            //}

            ModularOrFunCode = "MarketingAreas.M_Marketing.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();
            //
            //resp.EditActions = new List<SoftProjectAreaEntity> { 
            //     new SoftProjectAreaEntity
            //    {
            //        LableTitle = "讲议",
            //        ActionNameEn = "IndexDownHandout",
            //        ControllName = "M_MarketingAttachment",
            //        ActionFieldNames = "HandoutFileNameGuid",
            //        //RefPKTableGuid = resp.Item.HandoutFileNameGuid,
            //    },
            //     new SoftProjectAreaEntity
            //    {
            //        LableTitle = "视频",
            //        ActionNameEn = "IndexDown",
            //        ControllName = "M_MarketingAttachment",
            //        ActionFieldNames = "VideoFileNameGuid",
            //        //RefPKTableGuid = resp.Item.VideoFileNameGuid,
            //    },
            //};

            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销课堂--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();

            resp.Item.HandoutFileNameGuid = Guid.NewGuid().ToString();
            resp.Item.VideoFileNameGuid = Guid.NewGuid().ToString();

            resp.EditActions = new List<SoftProjectAreaEntity> { 
                                new SoftProjectAreaEntity
                {
                    LableTitle="讲义",
                    ActionNameEn = "IndexEditHandout",
                    ControllName = "M_MarketingAttachment",
                    ActionFieldNames = "HandoutFileNameGuid",
                    HandoutFileNameGuid = resp.Item.HandoutFileNameGuid,
                },
                new SoftProjectAreaEntity
                {
                    LableTitle="视频",
                    ActionNameEn = "IndexEdit",
                    ControllName = "M_MarketingAttachment",
                    ActionFieldNames = "VideoFileNameGuid",
                    VideoFileNameGuid = resp.Item.VideoFileNameGuid,
                },

            };

            ModularOrFunCode = "MarketingAreas.M_Marketing.Add";
            resp.FunNameEn = "Add";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销课堂--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            #region 初始值
            #endregion
            ModularOrFunCode = "MarketingAreas.M_Marketing.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 营销课堂--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_Marketing.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.EditActions = new List<SoftProjectAreaEntity> { 
                                new SoftProjectAreaEntity
                {
                    LableTitle="讲义",
                    ActionNameEn = "IndexEditHandout",
                    ControllName = "M_MarketingAttachment",
                    ActionFieldNames = "HandoutFileNameGuid",
                    HandoutFileNameGuid = resp.Item.HandoutFileNameGuid,
                },
                new SoftProjectAreaEntity
                {
                    LableTitle="视频",
                    ActionNameEn = "IndexEdit",
                    ControllName = "M_MarketingAttachment",
                    ActionFieldNames = "VideoFileNameGuid",
                    VideoFileNameGuid = resp.Item.VideoFileNameGuid,
                },
            };
            //resp.EditAction = new SoftProjectAreaEntity
            //{
            //    LableTitle="视频",
            //    ActionNameEn = "IndexEdit",
            //    ControllName = "M_MarketingAttachment",
            //    ActionFieldNames = "M_MarketingID",
            //    M_MarketingID = resp.Item.M_MarketingID,
            //};

            ModularOrFunCode = "MarketingAreas.M_Marketing.Edit";
            resp.FunNameEn = "Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 营销课堂--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_Marketing.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 营销课堂--查看
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "MarketingAreas.M_Marketing.Detail";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

    }
}
