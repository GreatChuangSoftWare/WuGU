using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftPlatform.Controllers;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class Sys_LoginInfoController : BaseController
    {
        //(1)不同类别的登录人员：首页定义
        //(2)不同登录人员：菜单调用不同
        //(3)不同登录人员：按钮权限不同
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(SoftProjectAreaEntityDomain domain)
        {
            //LoginInfo ss = new SoftProject.CellModel.SoftProjectAreaEntity();
            var resp = new MyResponseBase();
            resp.Item = domain.Item;
            if (domain.Item.MobilePhone != null)
            {
                try
                {
                    resp = domain.Login();
                    Session["LoginInfo"] = resp.Item;
                    if (resp.Item.LoginCategoryID == 1)
                        SoftProjectAreaEntityDomain.Sys_OperLogDetail_AddSave(1, 1, "登录", resp.Item.CompanyID, resp.Item.Sys_LoginInfoID, resp.Item.UserName, resp.Item.Sys_LoginInfoID, "登录");
                    else
                        SoftProjectAreaEntityDomain.Sys_OperLogDetail_AddSave(2, 1, "登录", resp.Item.CompanyID, resp.Item.Sys_LoginInfoID, resp.Item.UserName, resp.Item.Sys_LoginInfoID, "登录");

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    resp.Item.ErrorMessage = e.Message;
                    return View(resp);
                }
            }
            return View(resp);
        }

        public ActionResult LogOut()
        {
            Session.Remove("LoginInfo");
            return RedirectToAction("Login", "Sys_LoginInfo", new { area = "BaseInfoAreas" });
        }

        //public ActionResult ChangePass(SoftProjectAreaEntityDomain domain)
        //{
        //    if (LoginInfo.LoginCategoryID == 1)
        //        return RedirectToAction("ChangePass", "Pre_User", new { area = "AuthorizationAreas" });
        //    return RedirectToAction("ChangePass", "Comp_User", new { area = "CompanyAreas" });
        //}

        ///// <summary>
        ///// 用户管理--编辑个人信息
        ///// </summary>
        ///// <param name="domain"></param>
        ///// <returns></returns>
        //public ActionResult EditByMy(SoftProjectAreaEntityDomain domain)
        //{
        //    if (LoginInfo.LoginCategoryID == 1)
        //        return RedirectToAction("EditByMy", "Pre_User", new { area = "AuthorizationAreas" });
        //    return RedirectToAction("EditByMy", "Comp_User", new { area = "CompanyAreas" });
        //}

    }
}
