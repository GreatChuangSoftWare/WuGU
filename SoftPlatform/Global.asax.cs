
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SoftPlatform
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
             
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

        }

        protected void Session_Start()
        {
            //var loginInfo = new SoftProjectAreaEntity
            //{
            //    LoginID =1,//6,//1,//20,// 1,
            //    Name = "系统管理员-张三",
            //    //PasswordDigest = respv.Item.PasswordDigest,
            //    LoginCategoryID = 2,
            //    HomeConstrollName = "Home",
            //    HomeActionName = "Default",
            //};

            //var loginInfo = new SoftProjectAreaEntity
            //{
            //    LoginID = 1,
            //    Name = "公司100-登录名1",
            //    PasswordDigest = "",//;//.Item.PasswordDigest,
            //    CompanyID = 1005,
            //    CompanyName = "公司100",
            //    LoginCategoryID = 2,
            //    HomeConstrollName = "Home",
            //    HomeActionName = "Default",

            //};

            ////Session["Pre_Users"] = new Pre_Users { Pre_UsersID = 1, UserName = "系统管理员-张三", LoginName = "zs" };

            //Session["LoginInfo"] = loginInfo;

            //var loginInfo = new SoftProjectAreaEntity
            //{
            //    LoginID = 1,
            //    LoginName = "公司100-登录名1",
            //    PasswordDigest = "",//;//.Item.PasswordDigest,
            //    CompanyID = 1,
            //    CompanyName = "公司100",
            //    LoginCategoryID = 1,
            //    LoginNameCn = "公司100-登录名1",
            //    HomeConstrollName = "Home",
            //    HomeActionName = "Default",
            //    CurrNav = "Home",
            //    CurrMenuModularOrFunID = 71
            //};
            //loginInfo.CurrNavCategoryID = 1;
            //loginInfo.CurrNav = "加盟商仪表盘";
            //loginInfo.CurrNavIdent = 1005;

            //Session["LoginInfo"] = loginInfo;
        }

    }
}