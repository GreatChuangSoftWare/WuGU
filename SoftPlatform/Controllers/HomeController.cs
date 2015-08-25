using Aspose.Cells;
using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.CellModel;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class HomeController : BaseController
    {
        //public ActionResult IndexTemp()
        //{
        //    //return RedirectToAction("ProductByCategory", "P_ProductAreas/P_Product");//, new { DataTokens="P_ProductAreas" });
        //    //P_ProductAreas/P_Product/ProductByCategory
        //    //if (LoginInfo.LoginCategoryID == 1)
        //    //    return View("CompMagerIndex");
        //    //return View("FraMagerIndex");

        //    return View();
        //}


        public ActionResult Demo()
        {
            return View("Demo");
        }

        public ActionResult Index()
        {
            //return RedirectToAction("ProductByCategory", "P_ProductAreas/P_Product");//, new { DataTokens="P_ProductAreas" });
            //P_ProductAreas/P_Product/ProductByCategory
            //if (LoginInfo.LoginCategoryID == 1)
            //    return View("CompMagerIndex");
            //return View("FraMagerIndex");

            //HtmlHelpersProject.PageFormEleTypes
            //HtmlHelpersProject.SortFormEleTypes
            //HtmlHelpersProject.QueryFormEleTypes
            //Excel();

            if (!string.IsNullOrEmpty(LoginInfo.HomePageUrl))
                return Redirect(LoginInfo.HomePageUrl);
            return View();
        }

        public void Excel()
        {
            string outName = "";// table.TableName;
            Workbook workBook = new Workbook();
            workBook.Worksheets.Clear();
            workBook.Worksheets.Add(outName);//New Worksheet是Worksheet的name
            Worksheet ws = workBook.Worksheets[0];

            workBook.Worksheets[0].AutoFitColumns();
            var modulars = ProjectCache.Design_ModularOrFuns.Where(p => p.ParentPremID == 0).ToList();
            var k = 0;
            for (int i = 0; i < modulars.Count(); i++)
            {
                k++;
                var modular = modulars[i];
                #region 主表
                //(1)字段
                var ttt = HtmlHelpersProject.PageFormEleTypes(modular).Where(p => p.PageFormEleSort != 1 && p.PageFormEleSort != 100 && p.PageFormEleSort != null && p.FormEleType != 8)
                          .OrderBy(p => p.Page01FormEleSort);
                //PageFormEleSort = p.Page01FormEleSort; p.PageFormElePos = p.Page01FormElePos; p.FormEleType
                var DispNameCn = string.Join(",", ttt.Select(p => p.NameCn));
                //(2)查询条件
                var mmm = HtmlHelpersProject.QueryFormEleTypes(modular).Where(p => p.PageFormEleSort != null);
                var queryNameCn = string.Join(",", mmm.Select(p => p.NameCn));
                //按钮
                ws.Cells[k, 0].PutValue(modular.ModularName);
                ws.Cells[k, 1].PutValue(DispNameCn);
                ws.Cells[k, 2].PutValue(queryNameCn);
                #endregion
                #region 子模块

                var modularsChilds = ProjectCache.Design_ModularOrFuns.Where(p => p.ParentPremID == modular.Design_ModularOrFunID).ToList();
                for (var j = 0; j < modularsChilds.Count; j++)
                {
                    k++;
                    modular = modularsChilds[j];
                    ttt = HtmlHelpersProject.PageFormEleTypes(modular).Where(p => p.PageFormEleSort != 1 && p.PageFormEleSort != 100 && p.PageFormEleSort != null && p.FormEleType != 8)
                          .OrderBy(p => p.Page01FormEleSort);
                    //PageFormEleSort = p.Page01FormEleSort; p.PageFormElePos = p.Page01FormElePos; p.FormEleType
                    DispNameCn = string.Join(",", ttt.Select(p => p.NameCn));
                    //(2)查询条件
                    mmm = HtmlHelpersProject.QueryFormEleTypes(modular).Where(p => p.PageFormEleSort != null);
                    queryNameCn = string.Join(",", mmm.Select(p => p.NameCn));
                    //按钮
                    ws.Cells[k, 0].PutValue(modular.ModularName);
                    ws.Cells[k, 1].PutValue(DispNameCn);
                    ws.Cells[k, 2].PutValue(queryNameCn);
                }
                #endregion
            }
            workBook.Worksheets[0].AutoFitColumns();
            workBook.Save("C:\\aaa.xls");
        }

        public ActionResult CompMagerIndex()
        {
            MyResponseBase resp = new MyResponseBase();
            var domain=new SoftProjectAreaEntityDomain();
            resp = domain.Doc_Docment_Home();
            return View(resp);
        }

        public ActionResult FraMagerIndex()
        {
            MyResponseBase resp = new MyResponseBase();
            var domain = new SoftProjectAreaEntityDomain();
            resp = domain.Doc_Docment_Home();
            return View(resp);
        }

        /// <summary>
        /// 公司后台管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PlatformIndex()
        {
            var loginInfo = new SoftProjectAreaEntity
            {
                Sys_LoginInfoID = 1,
                LoginName = "平台-超级管理员",
                PasswordDigest = "",//;//.Item.PasswordDigest,
                CompanyID = 1,
                CompanyName = "平台-超级管理员",
                LoginCategoryID = 1,
                LoginNameCn = "平台-超级管理员",
                //HomeConstrollName = "Home",
                //HomeActionName = "Default",
                //CurrNav = "Home",
                //CurrMenuModularOrFunID = 71
            };
            //loginInfo.CurrNavCategoryID = 1;
            //loginInfo.CurrNav = "加盟商仪表盘";
            //loginInfo.CurrNavIdent = 1005;

            SoftProjectAreaEntityDomain.Sys_OperLogDetail_AddSave(1, 1, "登录", 1, 1, "平台-超级管理员", 1, "登录");

            Session["LoginInfo"] = loginInfo;
            return View("Index");
        }

        /// <summary>
        /// 加盟商后台管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CompIndex()
        {
            var loginInfo = new SoftProjectAreaEntity
            {
                Sys_LoginInfoID = 1,
                LoginName = "公司1-企业管理员",
                PasswordDigest = "",//;//.Item.PasswordDigest,
                CompanyID = 1,
                CompanyName = "公司",
                LoginCategoryID = 2,
                LoginNameCn = "公司-企业管理员1",
                //HomeConstrollName = "Home",
                //HomeActionName = "Default",
                //CurrNav = "Home",
                //CurrMenuModularOrFunID = 71
            };
            //loginInfo.CurrNavCategoryID = 1;
            //loginInfo.CurrNav = "加盟商仪表盘";
            //loginInfo.CurrNavIdent = 1005;

            //Session["Pre_Users"] = new Pre_Users { Pre_UsersID = 1, UserName = "系统管理员-张三", LoginName = "zs" };
            //SoftProjectAreaEntityDomain.Sys_OperLogDetail_AddSave(1, 1, "登录", 1, "登录");
            SoftProjectAreaEntityDomain.Sys_OperLogDetail_AddSave(2, 1, "登录", 1, 1, "公司1-企业管理员", 1, "登录");
            //SoftProjectAreaEntityDomain.Sys_OperLogDetail_AddSave(2, 1, "登录", 1005, "登录");
            Session["LoginInfo"] = loginInfo;

            return View("Index");
        }

        /// <summary>
        /// 子菜单面板
        /// </summary>
        /// <param name="Design_ModularOrFunID"></param>
        /// <returns></returns>
        public ActionResult SubPanel(int Design_ModularOrFunID)
        {
            //根据传入的Design_ModularOrFunID，查找子菜单，根据子菜单权限
            //(1)获取权限
            //如果只有1个则直接跳转

            //LoginInfo.CurrMenuModularOrFunID = Design_ModularOrFunID;

            var respItem = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunID).First();

            ModularOrFunCode = respItem.ModularOrFunCode;

            Session["LoginInfo"] = LoginInfo;

            ////ModularOrFunCode = "C_CustomerAreas.C_Customer.Index";

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain
            //{
            //    Item = new SoftProjectAreaEntity
            //    {
            //        Design_ModularOrFunID = Design_ModularOrFunID,
            //        Design_ModularOrFunParentID = Design_ModularOrFunID
            //    }
            //};
            //var respItem = domain.Design_ModularOrFun_GetByID();
            //var resp = domain.Design_ModularOrFun_GetByModularOrFunParentID(2);

            //base.Breadcrumb.Items.Clear();
            //base.Breadcrumb.Items = new List<BreadcrumbItem> { 
            //    new BreadcrumbItem{Name=respItem.Item.ModularName,URL=respItem.Item.ActionPath},
            //};
            //foreach (var item in resp.Items)
            //{
            //    base.Breadcrumb.Items.Add(new BreadcrumbItem { Name = item.ModularName, URL = item.ActionPath });
            //}
            ////(2)生成菜单
            ////(3)生成面板
            //ViewBag.Title = respItem.Item.ModularName;
            var resp = new MyResponseBase();
            return View(resp);
        }

        public ActionResult AuthorizationPanel()
        {
            MenuCode = "AuthorizationPanel";
            var resp = new MyResponseBase();
            return View("SubPanel", resp);
        }
    }
}
