using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.Mvc
{
    public class MyHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        // private Lazy<ILogger> logger = new Lazy<ILogger>(() => KernelContainer.Kernel.Get<ILogger>());

        public virtual void OnException(ExceptionContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["Controller"] as string;
            string actionName = filterContext.RouteData.Values["action"] as string;

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData,
                    //ViewData["aa"] = filterContext.Controller.ViewBag.asd
                };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                Controller controller = filterContext.Controller as Controller;
                Exception exception = filterContext.Exception;

                controller.Response.TrySkipIisCustomErrors = true;
                controller.Response.StatusCode = (int)HttpStatusCode.AjaxErrorResult;

                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult { Data = new { DisplayMessage = exception.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    filterContext.ExceptionHandled = true;
                }
            }


            //if (!filterContext.ExceptionHandled
            //|| TryRaiseErrorSignal(filterContext)
            //|| IsFiltered(filterContext))
            //    return;



            //if (filterContext.ExceptionHandled)
            //{
            //    if (TryRaiseErrorSignal(filterContext) || IsFiltered(filterContext))
            //        return;

            //    //LogException(filterContext);

            //    //自定义日志
            //    //Logging.ErrorLoggingEngine.Instance().Insert("action:" + actionName + ";sessionid:" + (filterContext.HttpContext.GetHttpSessionId()), filterContext.Exception);
            //}


        }

        //private static bool TryRaiseErrorSignal(ExceptionContext context)
        //{
        //    var httpContext = GetHttpContextImpl(context.HttpContext);
        //    if (httpContext == null)
        //        return false;
        //    var signal = ErrorSignal.FromContext(httpContext);
        //    if (signal == null)
        //        return false;
        //    signal.Raise(context.Exception, httpContext);
        //    return true;
        //}

        //private static bool IsFiltered(ExceptionContext context)
        //{
        //    var config = context.HttpContext.GetSection("elmah/errorFilter")
        //                    as ErrorFilterConfiguration;

        //    if (config == null)
        //        return false;

        //    var testContext = new ErrorFilterModule.AssertionHelperContext(
        //                          context.Exception,
        //                          GetHttpContextImpl(context.HttpContext));
        //    return config.Assertion.Test(testContext);
        //}

        //private static void LogException(ExceptionContext context)
        //{
        //    var httpContext = GetHttpContextImpl(context.HttpContext);
        //    var error = new Error(context.Exception, httpContext);
        //    ErrorLog.GetDefault(httpContext).Log(error);
        //}

        private static HttpContext GetHttpContextImpl(HttpContextBase context)
        {
            return context.ApplicationInstance.Context;
        }
    }

    public enum HttpStatusCode
    {
        AjaxErrorResult = 498,
        SessionExpired = 499,
    }
}