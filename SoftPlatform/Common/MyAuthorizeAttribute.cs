using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Healthcare.Framework.Web.Mvc
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //So now we are validating for secure part of the application
            //var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //filterContext.RouteData.DataTokens[];
            //controll.RouteData.DataTokens["area"].ToString()

            //var actionName = filterContext.ActionDescriptor.ActionName;

            //CurrentExecutionFilePath	"/AuthorizationAreas/De_Member"	string
            //CurrentExecutionFilePathExtension	""	string
            //FilePath	"/AuthorizationAreas/De_Member"	string
            var CurrentExecutionFilePath = filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath;
            //if (CurrentExecutionFilePath == "/")
            //    CurrentExecutionFilePath = "/Home/Index";

            //var AbsolutePath=filterContext.RequestContext.HttpContext.Request.AbsolutePath;
            //"/AuthorizationAreas/Pre_Organization/Index"	string

            //filterContext.ActionDescriptor.ActionName = "sdsd";
            //filterContext.RouteData.Values["action"]="Index";

            //var data = new RouteData(this, new MvcRouteHandler());
            //data.DataTokens["area"] = "AuthorizationAreas";
            //data.Values.Add("controller", "De_Member");
            //data.Values.Add("action", "Index");

            //var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "Error",
            //    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
            //    TempData = filterContext.Controller.TempData,
            //    //ViewData["aa"] = filterContext.Controller.ViewBag.asd
            //};

            //var area = "";
            //if (filterContext.RouteData.DataTokens["area"] != null)
            //    area = filterContext.RouteData.DataTokens["area"].ToString();
            //var controllerName = filterContext.RouteData.Values["controller"].ToString();
            //var actionName = filterContext.RouteData.Values["action"].ToString();

            //var controllerType = filterContext.Controller;

            //if ((controllerName + "/" + actionName).ToLower() == "/BaseInfoAreas/Sys_LoginInfo/Login".ToLower())
            //    return;
            ////skip authorization for specific part of application, which have deliberately marked with [SkipAuthorizaion] attribute
            //if (filterContext.ActionDescriptor.IsDefined(typeof(SkipAuthorizaionAttribute), true)
            //    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipAuthorizaionAttribute), true))
            //{
            //    return;
            //}

            //if (filterContext.HttpContext == null)
            //{
            //    //throw new MvcException("用户登录过期，请重新登录！");
            //    throw new Exception("用户登录过期，请重新登录！");
            //}

            if (CurrentExecutionFilePath != "/BaseInfoAreas/Sys_LoginInfo/Login")
            {
                if (filterContext.HttpContext == null
                    || filterContext.HttpContext.Session == null
                    || filterContext.HttpContext.Session["LoginInfo"] == null
                    || !(filterContext.HttpContext.Session["LoginInfo"] is SoftProjectAreaEntity)
                    || (filterContext.HttpContext.Session["LoginInfo"] as SoftProjectAreaEntity) == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        //throw new MvcException ("用户登录过期，请刷新窗口以后重新登录！");
                        throw new Exception("用户登录过期，请刷新窗口以后重新登录！");
                    }
                    else
                    {
                        //filterContext.HttpContext.Session["RequestOldUrl"] = filterContext.HttpContext.Request.Url;
                        filterContext.Result = new RedirectResult("/BaseInfoAreas/Sys_LoginInfo/Login");
                        return;
                    }
                }

                ////var user = filterContext.HttpContext.Session["Pre_Users"] as Pre_Users;

                ////获取用户权限列表
                //var ModularCode = (controllerName + "." + actionName);
                SoftProjectAreaEntity loginInfo = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;

                var LoginID = loginInfo.Sys_LoginInfoID;
                //if (CurrentExecutionFilePath != "/")
                //{
                //    if (loginInfo.LoginCategoryID == 1)
                //    {
                //        if (!ProjectCache.UserHasPremCode((int)loginInfo.LoginID, CurrentExecutionFilePath))
                //        {
                //            throw new Exception("用户无权进行操作！");
                //        }
                //    }
                //    else
                //    {
                //        if (!ProjectCache.CustomerHasPremCode((int)loginInfo.LoginID, CurrentExecutionFilePath))
                //        {
                //            throw new Exception("客户无权进行操作！");
                //        }
                //    }
                //}

                //是否是菜单 

                var currMenu = ProjectCache.HasMenu(CurrentExecutionFilePath);
                if (!string.IsNullOrEmpty(currMenu))
                {
                    filterContext.HttpContext.Session["CurrMenu"] = currMenu;
                }
                if (filterContext.HttpContext.Session["CurrMenu"] == null)
                    filterContext.HttpContext.Session["CurrMenu"] = "/Home/Index";
                var currpath = "";
                if (CurrentExecutionFilePath == "/Home/Index" || CurrentExecutionFilePath == "/")
                    currpath = "/Home/Index";
                //else
                //    currpath = ProjectCache.MenuAction(CurrentExecutionFilePath);
                //if (currpath.Length > 0 && !filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                //    HttpContext.Current.Session["MenuUrl"] = currpath;
                //if (loginInfo.LoginCategoryID == 1)
                //{
                //    if (!ProjectCache.UserHasActionPath((int)LoginID, CurrentExecutionFilePath))
                //    {
                //        throw new Exception("客户无权进行操作！");
                //    }
                //}
                //else
                //{
                //    if (!ProjectCache.Fra_FranchiseeHasActionPath((int)LoginID, CurrentExecutionFilePath))
                //    {
                //        throw new Exception("客户无权进行操作！");
                //    }                
                //}
                //if (pre_Users.LoginCategory == 2)
                //    pre_UsersID = 2;
                //PermissionOperControlDomain domain = new PermissionOperControlDomain();
                //ProjectCache.UserHasPremCode();
                //if (!domain.UserFunPermission((int)pre_UsersID, ModularCode))
                //{ 
                //}
                //MenuAction 
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SkipAuthorizaionAttribute : Attribute { }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //public class PermissionsAttribute : Attribute
    //{
    //    public PermissionsAttribute(params string[] allow)
    //    {
    //        Allow = allow ?? new string[0];
    //    }

    //    public string[] Allow { get; private set; }

    //    public static PermissionsAttribute Merge(params PermissionsAttribute[] permissions)
    //    {
    //        if (permissions == null)
    //        {
    //            return new PermissionsAttribute();
    //        }

    //        var allNotNullPermissions = permissions.Where(p => p != null);

    //        if (!allNotNullPermissions.Any())
    //        {
    //            return new PermissionsAttribute();
    //        }

    //        return new PermissionsAttribute
    //        {
    //            Allow = allNotNullPermissions.Aggregate(new List<string>(),
    //                                          (list, permissionsAttribute) =>
    //                                          {
    //                                              list.AddRange(permissionsAttribute.Allow);
    //                                              return list;
    //                                          }).ToArray()
    //        };
    //    }
    //}
}
