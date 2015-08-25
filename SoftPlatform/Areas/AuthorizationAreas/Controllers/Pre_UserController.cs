using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

//namespace Framework.Web.Mvc
namespace SoftPlatform.Controllers
{
    /// <summary>
    /// 控制器：Pre_User(用户管理)
    /// </summary>
    public class Pre_UserController : BaseController
    {
        public Pre_UserController()
        {
        }

        #region 修改密码

        /// <summary>
        /// 用户管理--显示修改修改密码页面
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult ChangePass(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ChangePass";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--修改密码保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult ChangePassSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_UserID = LoginInfo.Sys_LoginInfoID;
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ChangePass";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_ChangPass();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 重置密码

        /// <summary>
        /// 用户管理--重置密码
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult ResetPass(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.PasswordDigest = ConfigurationManager.AppSettings["InitialPass"];
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ResetPass";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_ChangPass();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 公司用户

        #region 公司用户管理

        /// <summary>
        /// 用户管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Index(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "1" });

            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Item.LoginCategoryID = 1;
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 1;
            resp.FunNameEn = "Add";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Add";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 1;//公司角色
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Add";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.LoginCategoryID = 1;
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Edit";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Edit";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult Detail(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Detail";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 个人信息修改

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditByMy(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_UserID = LoginInfo.Sys_LoginInfoID;
            ModularOrFunCode = "AuthorizationAreas.Pre_User.Index";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMy";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditByMySave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMy";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_User_EditByMySave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #endregion

        #region 公司客户管理

        #region 客户管理

        /// <summary>
        /// 用户管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "4" });

            ModularOrFunCode = "AuthorizationAreas.Pre_User.IndexCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Item.LoginCategoryID = 4;
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCU(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 4;
            resp.FunNameEn = "Add";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddCU";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCUSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 4;//企业角色
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.LoginCategoryID = 4;
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditCU";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCUSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCU";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 企业用户个人信息修改

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditByMyCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_UserID = LoginInfo.Sys_LoginInfoID;
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyCU";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditByMyCUSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_User_EditByMySave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #endregion

        #region 企业用户

        #region 企业用户管理

        /// <summary>
        /// 用户管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexComp(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "2" });
            ModularOrFunCode = "AuthorizationAreas.Pre_User.IndexComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Item.LoginCategoryID = 2;
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddComp(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 2;
            resp.FunNameEn = "Add";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddComp";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCompSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 2;//企业角色
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditComp(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.LoginCategoryID = 2;
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditComp";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCompSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailComp(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailComp";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 企业用户个人信息修改

        /// <summary>
        /// 用户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditByMyComp(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_UserID = LoginInfo.Sys_LoginInfoID;
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyComp";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 用户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditByMyCompSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyComp";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_User_EditByMySave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #endregion

        #region 企业客户

        #region 企业客户管理

        /// <summary>
        /// 企业客户管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexCompCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "8" });
            ModularOrFunCode = "AuthorizationAreas.Pre_User.IndexCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Item.LoginCategoryID = 8;
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业客户管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCompCU(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 8;
            resp.FunNameEn = "Add";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddCompCU";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业客户管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCompCUSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 8;//企业角色
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_AddSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 企业客户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCompCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.LoginCategoryID = 8;
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditCompCU";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业客户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCompCUSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 企业客户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailCompCU(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCU";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 企业客户个人信息修改

        /// <summary>
        /// 企业客户管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditByMyCompCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_UserID = LoginInfo.Sys_LoginInfoID;
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyCompCU";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业客户管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditByMyCompCUSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_User_EditByMySave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 仪表盘

        public ActionResult DashboardCompCU(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.Pre_UserID == null)
                throw new Exception("顾客ID不能为空");
            var resp = new MyResponseBase { Item = domain.Item };
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DashboardCompCU";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            return View("DashboardCompCU", resp);
        }

        #endregion

        #endregion

        #region 企业合作商

        #region 企业合作商管理

        /// <summary>
        /// 企业合作商管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult IndexCompCP(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "16" });
            ModularOrFunCode = "AuthorizationAreas.Pre_User.IndexCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.QueryIndex();

            resp.Item.LoginCategoryID = 16;
            if (Request.IsAjaxRequest())
                return View(Design_ModularOrFun.PartialView, resp);
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业合作商管理--添加查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult AddCompCP(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            resp.Item.LoginCategoryID = 16;
            resp.FunNameEn = "Add";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddCompCP";
            MenuCode = "AuthorizationPanel";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业合作商管理--添加保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult AddCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.Item.LoginCategoryID = 16;//企业角色
            ModularOrFunCode = "AuthorizationAreas.Pre_User.AddCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_AddCompCPSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 企业合作商管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.Item.LoginCategoryID = 16;
            resp.FunNameEn = "Edit";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditCompCP";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业合作商管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;

            var resp = domain.Pre_User_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 企业合作商管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult DetailCompCP(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            resp.FunNameEn = "Detail";
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCP";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 企业合作商个人信息修改

        /// <summary>
        /// 企业合作商管理--编辑查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ActionResult EditByMyCompCP(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Pre_UserID = LoginInfo.Sys_LoginInfoID;
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DetailCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.ByID();

            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyCompCP";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        /// <summary>
        /// 企业合作商管理--编辑保存
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public HJsonResult EditByMyCompCPSave(SoftProjectAreaEntityDomain domain)
        {
            ModularOrFunCode = "AuthorizationAreas.Pre_User.EditByMyCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            var resp = domain.Pre_User_EditByMySave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 仪表盘

        public ActionResult DashboardCompCP(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.Pre_UserID == null)
                throw new Exception("合作商ID不能为空");
            var resp = new MyResponseBase { Item = domain.Item };
            ModularOrFunCode = "AuthorizationAreas.Pre_User.DashboardCompCP";
            domain.Design_ModularOrFun = Design_ModularOrFun;
            return View("DashboardCompCP", resp);
        }

        #endregion

        #endregion

        #region 顾客统计：公司

        /// <summary>
        /// 按区域统计加盟商数，消费金额
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ByAreaTotalCompCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "8" });
            var resp = domain.Pre_User_ByAreaTotalCompCU();
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ByAreaTotalCompCU";

            if (Request.IsAjaxRequest())
                return View("TableContext", resp);

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);

            //ViewBag.Title = "顾客区域统计";
            //resp.ViewContextName = "ByAreaFraTotal";
            //return View("NavPFrame", resp);
        }

        /// <summary>
        /// 按区域统计加盟商数，消费金额
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ByAreaDetailCompCU(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "8" });
            var resp = domain.Pre_User_ByAreaDetailCompCU();
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ByAreaDetailCompCU";

            if (Request.IsAjaxRequest())
                return View("TableContext", resp);

            //ViewBag.Title = "顾客统计";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);

            //resp.ViewContextName = "ByAreaFraDetail";
            //return View("NavPFrame", resp);

        }

        #endregion

        #region 合作商统计：公司

        /// <summary>
        /// 按区域统计加盟商数，消费金额
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ByAreaTotalCompCP(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "16" });

            var resp = domain.Pre_User_ByAreaTotalCompCP();
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ByAreaTotalCompCP";

            if (Request.IsAjaxRequest())
                return View("TableContext", resp);

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);

            //ViewBag.Title = "顾客区域统计";
            //resp.ViewContextName = "ByAreaFraTotal";
            //return View("NavPFrame", resp);
        }

        /// <summary>
        /// 按区域统计加盟商数，消费金额
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ByAreaDetailCompCP(SoftProjectAreaEntityDomain domain)
        {
            domain.Querys.Add(new Query { QuryType = 0, FieldName = "LoginCategoryID___equal", Value = "16" });

            var resp = domain.Pre_User_ByAreaDetailCompCP();
            ModularOrFunCode = "AuthorizationAreas.Pre_User.ByAreaDetailCompCP";

            if (Request.IsAjaxRequest())
                return View("TableContext", resp);

            //ViewBag.Title = "顾客统计";
            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);

            //resp.ViewContextName = "ByAreaFraDetail";
            //return View("NavPFrame", resp);

        }

        #endregion

        #region 顾客信息--待回访列表

        /// <summary>
        /// 顾客管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WaitVisitCompCU(SoftProjectAreaEntityDomain domain)
        {
            //if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            var resp = domain.Pre_User_WaitVisit();
            ModularOrFunCode = "AuthorizationAreas.Pre_User.WaitVisitCompCU";
            if (Request.IsAjaxRequest())
                return View("TableContext", resp);//TableContextIndexFra

            resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View(Design_ModularOrFun.MainView, resp);
        }

        #endregion

        #region 顾客信息--待回访顾客--首页

        /// <summary>
        /// 顾客管理--查询
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WaitVisitCompCUHome(SoftProjectAreaEntityDomain domain)
        {
            //if (!domain.Querys.QueryDicts.ContainsKey("Pre_CompanyID___equal"))
            //    domain.Querys.Add(new Query { QuryType = 0, FieldName = "Pre_CompanyID___equal", Value = LoginInfo.CompanyID.ToString() });
            domain.Item.Pre_CompanyID = LoginInfo.CompanyID;
            domain.PageQueryBase.PageSize = 10;
            var resp = domain.Pre_User_WaitVisit();
            //if (Request.IsAjaxRequest())
            //    return View(Design_ModularOrFun.PartialView, resp);//TableContextIndexFra
            //resp.ViewContextName = Design_ModularOrFun.PartialView;
            return View("WaitVisitHome", resp);
        }

        #endregion

    }

}
