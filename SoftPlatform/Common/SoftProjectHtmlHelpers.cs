using Framework.Core;
using Framework.Web.Mvc.Sys;
using SoftPlatform.Controllers;
using SoftProject.CellModel;
using SoftProject.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace Framework.Web.Mvc
{
    public static partial class HtmlHelpersProject
    {
        static string AbsolutePath = HttpContext.Current.Request.Url.AbsolutePath;

        #region 导航条

        public static MvcHtmlString Navigation(this HtmlHelper helper, object item, string ModularOrFunCode)// MyResponseBase Model)// string ModularOrFunCode)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            var modular = conts.Design_ModularOrFun;


            var strNav = "";// "<ul class='breadcrumb myjuxing'>";
            //strNav += "<li><span class='icon-home'></span><a href='/'>首页</a></li>";
            //if (modulars.Count() > 0)
            //{
            //var modular = modulars.FirstOrDefault();
            //strNav = NavChilds(modular.Design_ModularOrFunParentID, item) + strNav;
            //strNav = NavChilds(modular.Design_ModularOrFunParentID, item) + strNav;
            strNav = NavChilds(modular.ParentPremID, item) + strNav;
            if (modular.BUrlNva == 1)
            {
                #region 参数
                var strParam = "";
                if (modular.ParamName != null && modular.ParamName.Length > 0 && item != null)
                {
                    #region 对象数据类型

                    Type type = item.GetType();
                    #endregion

                    var paramNames = modular.ParamName.Split(',');
                    foreach (var param in paramNames)
                    {
                        PropertyInfo property = type.GetProperty(param);
                        var value = property.GetValue(item, null);
                        strParam += "Item." + param + "=" + value;
                        //var val=item.
                    }
                }
                if (strParam.Length > 0)
                    strParam = "?" + strParam;
                #endregion
                strNav += string.Format("<li><a href='{0}{1}'>{2}</a></li>", modular.ActionPath, strParam, modular.ModularName);
            }
            strNav += "<li>" + modular.MenuName + "</li>";
            //}
            strNav = "<li><span class='icon-home'></span><a href='/'>首页</a></li>" + strNav;
            strNav = "<ul class='breadcrumb myjuxing'>" + strNav;
            strNav += "</ul>";
            //conts.ViewBag.Title = modular.ModularName;
            MvcHtmlString mstr = new MvcHtmlString(strNav);
            return mstr;
        }

        public static string NavChilds(int? Design_ModularOrFunID, object item)//Design_ModularParentID)
        {
            string strNav = "";
            var modulars = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunID);
            if (modulars.Count() > 0)
            {
                var modular = modulars.FirstOrDefault();

                if (modular.bNavModularOrFun == 0 || modular.GroupModularOrFun == 1)
                    strNav = string.Format("<li>{0}</li>", modular.ModularName);
                else
                {
                    var strParam = "";
                    if (modular.ParamName != null && modular.ParamName.Length > 0 && item != null)
                    {
                        #region 对象数据类型

                        Type type = item.GetType();
                        #endregion

                        var paramNames = modular.ParamName.Split(',');
                        foreach (var param in paramNames)
                        {
                            PropertyInfo property = type.GetProperty(param);
                            var value = property.GetValue(item, null);
                            strParam += "Item." + param + "=" + value;
                            //var val=item.
                        }
                    }
                    if (strParam.Length > 0)
                        strParam = "?" + strParam;
                    strNav = string.Format("<li><a href='{0}{1}'>{2}</a></li>", modular.ActionPath, strParam, modular.ModularName);
                }
                strNav = NavChilds(modular.ParentPremID, item) + strNav;
            }
            return strNav;
        }

        #endregion

        /// <summary>
        /// 生成标题页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static IHtmlString HeadPageHtml(this HtmlHelper helper, object Item, string ModularOrFunCode, int? colcopies = 4, SoftProjectAreaEntity modulars = null)//List<SoftProjectAreaEntity> pagefields)// SoftProjectAreaEntity field, object data)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            if (modulars == null)
                modulars = conts.Design_ModularOrFun;

            //var xx=modulars.PageType;

            var pagefields = PageFormEleTypes(modulars);
            pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();
            PropertyInfo property = null;
            object value = null;
            var val = "";
            //TitleMul
            var MainTitle = pagefields.Where(p => p.PageFormElePos == 1)
                .OrderBy(p => p.PageFormEleSort).FirstOrDefault();

            #region 主标题
            if (MainTitle != null)
            {
                property = type.GetProperty(MainTitle.name);
                value = property.GetValue(Item, null);

                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (MainTitle.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(MainTitle.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(MainTitle.DisFormat);
                    }
                }

                sbHtml.AppendLine(string.Format("<h1>{0}</h1>", val));
            }
            #endregion

            #region 副标题

            var Subtitle = pagefields.Where(p => p.PageFormElePos == 2)
    .OrderBy(p => p.PageFormEleSort).FirstOrDefault();
            if (Subtitle != null)
            {
                property = type.GetProperty(Subtitle.name);
                value = property.GetValue(Item, null);
                val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (Subtitle.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(Subtitle.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(Subtitle.DisFormat);
                    }
                }

                sbHtml.AppendLine(string.Format("<h5>{0}</h5>", val));
            }
            #endregion

            #region 正文

            var Context = pagefields.Where(p => p.PageFormElePos == 3)
    .OrderBy(p => p.PageFormEleSort).FirstOrDefault();

            property = type.GetProperty(Context.name);
            value = property.GetValue(Item, null);
            val = "";
            if (value != null)
            {
                var strval = value.ToString();
                val = strval;
            }
            if (Context.xtype == 61)//日期类型
            {
                if (val != "")
                {
                    if (string.IsNullOrEmpty(Context.DisFormat))
                        val = val.ToDate().ToString("yyyy-MM-dd");
                    else
                        val = val.ToDate().ToString(Context.DisFormat);
                }
            }

            //sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");

            sbHtml.AppendLine("<div class=''>" + val + "&nbsp;</div>");

            #endregion

            var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
            return tempstr;
        }


        //public static MvcHtmlString Navigation(this HtmlHelper helper, object item, string ModularOrFunCode)// MyResponseBase Model)// string ModularOrFunCode)
        //{
        //    //<ul class="breadcrumb myjuxing"><li><span class="icon-home"></span><a href="/">首页</a></li>
        //    //<li>采购管理</li><li><a href="/PurchaseOrderAreas/Pu_PurchaseOrder/Index">采购订单管理</a></li><li>查看</li></ul>

        //    //var ModularOrFunCode = Model.ModularOrFunCode;
        //    //var item = Model.Item;
        //    //var modulars = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).ToList();
        //    //var conts = helper.ViewContext.Controller as BaseController;
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    var modulars = ProjectCache.Design_ModularOrFun;

        //    //var modulars = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();
        //    //var modulars = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();

        //    //var strParam = NavChilds(modulars.ParamName, item);

        //    //ViewBag.Title = Nav.ModularName;
        //    var strNav = "<ul class='breadcrumb myjuxing'>";
        //    strNav += "<li><span class='icon-home'></span><a href='/'>首页</a></li>";
        //    strNav += "<li>" + modulars.GroupName + "</li>";
        //    //strNav += string.Format("<li><a href='{0}{1}'>{2}</a></li>", modulars.ActionPath, strParam, modulars.ModularName);// modular.ActionPath, modular.ModularName);
        //    strNav += string.Format("<li>{0}</li>", modulars.PageName);// modular.ActionPath, modular.ModularName);
        //    strNav += "</ul>";
        //    MvcHtmlString mstr = new MvcHtmlString(strNav);

        //    return mstr;
        //}

        //public static string NavChilds(string ParamName, object item)//Design_ModularParentID)
        //{
        //    var strParam = "?";
        //    if (ParamName != null && ParamName.Length > 0 && item != null)
        //    {
        //        #region 对象数据类型

        //        Type type = item.GetType();
        //        #endregion

        //        var paramNames = ParamName.Split(',');
        //        foreach (var param in paramNames)
        //        {
        //            PropertyInfo property = type.GetProperty(param);
        //            var value = property.GetValue(item, null);
        //            strParam += "Item." + param + "=" + value;
        //            //var val=item.
        //        }
        //    }
        //    return strParam;
        //}

        //#endregion

        /// <summary>
        /// Tab页面
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <returns></returns>
        public static MvcHtmlString TabHtml(this HtmlHelper helper, object item, string ModularOrFunCode)
        {
            #region 模块信息
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            //if (Design_ModularOrFun.GroupModularOrFun == 3)
            //    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).FirstOrDefault();
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == "Add").FirstOrDefault();
            var conts = helper.ViewContext.Controller as BaseController;
            var Design_ModularOrFun = conts.Design_ModularOrFun;

            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == modulars.ModularID && p.Page02FormEleType != null).ToList();

            #endregion

            var btns = conts.LoginModulerBtns(conts.TabModularOrFunCode, 1);
            //var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == conts.TabModularOrFunCode).OrderBy(p => p.Sort).ToList();

            //<li class="@Html.IsActive("C_Customer", "Edit")">
            //    <a href="/C_CustomerAreas/C_Customer/Edit?Item.C_CustomerID=@Model.Item.C_CustomerID">顾客信息</a>
            //</li>
            var sbhtml = new StringBuilder();
            //var controll = helper.ViewContext.Controller as BaseController;
            //var controllName = conts.ControllName;
            //var actionName = conts.ActionName;

            foreach (var btn in btns)
            {
                var controllss = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                //if (!conts.LoginHasPremCode(btn.Design_ModularOrFunControllID))
                //    continue;
                var action = "";// Html.IsActive("C_Visit", "Index");
                if (controllss[0].ActionPath == conts.Request.Path)
                    action = "active";
                var url = UrlByControll(item, controllss.First());
                sbhtml.AppendLine("<li class='" + action + "'>");
                sbhtml.AppendLine(string.Format("<a href='{0}'>{1}</a>", url, btn.BtnNameCn));
                sbhtml.AppendLine("</li>");
            }
            var strHtml = new MvcHtmlString(sbhtml.ToString());
            return strHtml;
        }


        /// <summary>
        /// 页脚按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <returns></returns>
        public static MvcHtmlString BtnFooterHtml(this HtmlHelper helper, object item,
            string ModularOrFunCode,
            List<SoftProjectAreaEntity> btns,
            SoftProjectAreaEntity Design_ModularOrFun = null)
        {
            #region 模块信息
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            //if (Design_ModularOrFun.GroupModularOrFun == 3)
            //    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).FirstOrDefault();
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == "Add").FirstOrDefault();
            var conts = helper.ViewContext.Controller as BaseController;
            if (Design_ModularOrFun == null)
                Design_ModularOrFun = conts.Design_ModularOrFun;

            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == modulars.ModularID && p.Page02FormEleType != null).ToList();

            #endregion

            var type = item.GetType();
            var btnsrows = btns.Where(p => string.IsNullOrEmpty(p.DispConditionsExpression)).ToList();
            var DispConditionsExpressions = btns.Where(p => !string.IsNullOrEmpty(p.DispConditionsExpression)).ToList();

            for (var i = 0; i < DispConditionsExpressions.Count(); i++)//var btn in btns)
            {
                var btn = DispConditionsExpressions[i];
                #region 按钮显示条件比较
                if (!string.IsNullOrEmpty(btn.DispConditionsExpression))
                {
                    var DispConditionsExpressionArr = btn.DispConditionsExpression.Split('|');
                    #region 第1个数的值
                    PropertyInfo property = type.GetProperty(DispConditionsExpressionArr[1]);
                    var value1 = property.GetValue(item, null);
                    if (value1 == null)
                        throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
                    var strValue1 = value1.ToString();
                    #endregion

                    #region 第2个数的值
                    var strValue2 = DispConditionsExpressionArr[3];
                    if (DispConditionsExpressionArr[0] == "2")
                    {
                        property = type.GetProperty(DispConditionsExpressionArr[3]);
                        var value2 = property.GetValue(item, null);
                        if (value2 == null)
                            throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
                        strValue2 = value2.ToString();
                    }
                    #endregion
                    #region 比较运算
                    switch (DispConditionsExpressionArr[2])
                    {
                        case "equal":
                            if (strValue1 != DispConditionsExpressionArr[3])
                                continue;
                            break;
                    }
                    #endregion
                    btnsrows.Add(btn);
                }
                else
                    btnsrows.Add(btn);
                #endregion
            }
            //var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode && (p.OperPos == 3 && p.bValid != 0)).OrderBy(p => p.Sort).ToList();
            var strHtml = ButtonHtml(helper, item, Design_ModularOrFun, btnsrows, "", "", false);//calcols);
            //var strHtml = ButtonHtml(helper, item, Design_ModularOrFun, btns, "", "", false);
            return strHtml;
        }

        public static MvcHtmlString SortHtml(this HtmlHelper helper, List<RankInfo> RankInfos)
        {
            #region 模块信息
            var conts = helper.ViewContext.Controller as BaseController;
            var Design_ModularOrFun = conts.Design_ModularOrFun;
            #endregion

            var SortsFields = SortFormEleTypes(Design_ModularOrFun);// PageFormEleTypes

            StringBuilder strhtml = new StringBuilder("");
            //序号列：宽度
            //操作列：宽度
            int? width = null;
            string headTiele = null;
            string strOrderBy = null;
            for (var j = 0; j < SortsFields.Count; j++)
            {
                strOrderBy = null;
                #region 宽度
                //width = TableHeadInfos[j].HeadWidth;
                //if (width == null)
                //    width = TableHeadInfos[j].Width;
                #endregion
                headTiele = SortsFields[j].NameCn;
                if (SortsFields[j].Design_ModularFieldID > 100)
                {
                    strOrderBy = HtmlHelpers.OrderBy(RankInfos, SortsFields[j].name);
                }
                strhtml.Append(string.Format("<div  >{0} {1}</div>", headTiele, strOrderBy));
            }
            strhtml.Append("");
            var strmvc = new MvcHtmlString(strhtml.ToString());
            return strmvc;
        }

        /// <summary>
        /// 查询页面，表格显示字段
        /// </summary>
        /// <param name="Design_ModularOrFun"></param>
        public static List<SoftProjectAreaEntity> SortFormEleTypes(SoftProjectAreaEntity Design_ModularOrFun)
        {
            List<SoftProjectAreaEntity> SortFields = new List<SoftProjectAreaEntity>();
            var Design_ModularOrFunTemp = Design_ModularOrFun;
            if (Design_ModularOrFun.GroupModularOrFun == 3)
                Design_ModularOrFunTemp = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).First();
            switch (Design_ModularOrFun.SortCol)//QueryFormEleTypeName)
            {
                case "SortCol01":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort01 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort01).ToList();
                    break;
                case "SortCol02":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort02 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort02).ToList();
                    break;
                case "SortCol03":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort03 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort03).ToList();
                    break;
                case "SortCol04":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort04 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort04).ToList();
                    break;
                case "SortCol05":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort05 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort05).ToList();
                    break;
                case "SortCol06":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort06 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort06).ToList();
                    break;
                case "SortCol07":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort07 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort07).ToList();
                    break;
                case "SortCol08":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort08 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort08).ToList();
                    break;
                case "SortCol09":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort09 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort09).ToList();
                    break;
                case "SortCol10":
                    SortFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Sort10 != null).ToList();
                    SortFields = SortFields.OrderBy(p => p.Sort10).ToList();
                    break;

            }
            return SortFields;
        }

        public static MvcHtmlString ToolBarHtml(this HtmlHelper helper, object item, SoftProjectAreaEntity Design_ModularOrFun, List<SoftProjectAreaEntity> btns)
        {
            #region 模块信息
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            //if (Design_ModularOrFun.GroupModularOrFun == 3)
            //    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).FirstOrDefault();
            //var conts = helper.ViewContext.Controller as BaseController;
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFun;
            #endregion

            #region 生成calcols：用于表格的列计算，例如：批量删除

            //获取页面的所有字段
            var Fields = PageFormEleTypes(Design_ModularOrFun);
            //??
            var TableDispInfos = Fields.Where(p => p.FieldFunTypeID == null || ((((int)p.FieldFunTypeID) & 1) == 1)).ToList();// ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();

            var calcols = "";
            var kkkkxxx = TableDispInfos.Where(p => !string.IsNullOrEmpty(p.calcol)).ToList();

            for (var j = 0; j < kkkkxxx.Count; j++)
            {
                var field = kkkkxxx[j];
                calcols += field.calcol + ",";
            }
            if (calcols.Length > 0)
                calcols = calcols.Substring(0, calcols.Length - 1);
            #endregion

            var strHtml = ButtonHtml(helper, item, Design_ModularOrFun, btns, "", calcols);
            return strHtml;
        }


        //public static MvcHtmlString ToolBarHtml(this HtmlHelper helper, object item, string ModularOrFunCode)
        //{
        //    #region 模块信息
        //    //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
        //    //if (Design_ModularOrFun.GroupModularOrFun == 3)
        //    //    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).FirstOrDefault();
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();
        //    var Design_ModularOrFun = ProjectCache.Design_ModularOrFun;
        //    #endregion
        //    var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode && (p.OperPos == 1)).OrderBy(p => p.Sort).ToList();

        //    var Fields = PageFormEleTypes(Design_ModularOrFun);

        //    var TableDispInfos = Fields.Where(p => p.FieldFunTypeID == null || ((((int)p.FieldFunTypeID) & 1) == 1)).ToList();// ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();

        //    var calcols = "";
        //    for (var j = 0; j < TableDispInfos.Count; j++)
        //    {
        //        var field = TableDispInfos[j];
        //        if (!string.IsNullOrEmpty(field.calcol))
        //        {
        //            calcols += field.calcol + ",";// string.Format("data-calcol='{0}'", field.calcol);
        //        }
        //    }
        //    if (calcols.Length > 0)
        //        calcols = calcols.Substring(0, calcols.Length - 1);

        //    var strHtml = ButtonHtml(helper, item, Design_ModularOrFun, btns, "", calcols);
        //    return strHtml;
        //}

        public static MvcHtmlString ButtonHtml(this HtmlHelper helper, object item,
            SoftProjectAreaEntity Design_ModularOrFun,
            List<SoftProjectAreaEntity> btns,
            string btn_xs, string calcols, bool row = true)
        {
            StringBuilder sb = new StringBuilder();
            //resp.ModularOrFunCode
            //Design_ModularOrFunx = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).First();
            var type = item.GetType();
            //var controll = helper.ViewContext.Controller as BaseController;

            //针对Table中有多个按钮时，显示下拉
            var bdrop = false;
            if (btns.Count > 2 && row)
            {
                bdrop = true;
            }

            for (var i = 0; i < btns.Count; i++)//var btn in btns)
            {
                var btn = btns[i];
                #region 按钮显示条件比较
                //if (!string.IsNullOrEmpty(btn.DispConditionsExpression))
                //{
                //    var DispConditionsExpressionArr = btn.DispConditionsExpression.Split('|');
                //    #region 第1个数的值
                //    PropertyInfo property = type.GetProperty(DispConditionsExpressionArr[1]);
                //    var value1 = property.GetValue(item, null);
                //    if (value1 == null)
                //        throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
                //    var strValue1 = value1.ToString();
                //    #endregion

                //    #region 第2个数的值
                //    var strValue2 = DispConditionsExpressionArr[3];
                //    if (DispConditionsExpressionArr[0] == "2")
                //    {
                //        property = type.GetProperty(DispConditionsExpressionArr[3]);
                //        var value2 = property.GetValue(item, null);
                //        if (value2 == null)
                //            throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
                //        strValue2 = value2.ToString();
                //    }
                //    #endregion
                //    #region 比较运算
                //    switch (DispConditionsExpressionArr[2])
                //    {
                //        case "equal":
                //            if (strValue1 != DispConditionsExpressionArr[3])
                //                continue;
                //            break;
                //    }
                //    #endregion
                //}
                #endregion

                //用于生成按钮操作的Url
                var controllss = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                if ((btn.BtnBehavior == 1 || btn.BtnBehavior == 2 || btn.BtnBehavior == 3 || btn.BtnBehavior == 4 || btn.BtnBehavior == 10 || btn.BtnBehavior == 200 || btn.BtnBehavior == 400 || btn.BtnBehavior == 42 || btn.BtnBehavior == 70 || btn.BtnBehavior == 60) && controllss.Count == 0)
                    throw new Exception("按钮" + btn.BtnNameCn+":未有配置Action");
                else if (btn.BtnBehavior == 300 || btn.BtnBehavior == 301 || btn.BtnBehavior == 15 || btn.BtnBehavior == 101 || btn.BtnBehavior == 102 || btn.BtnBehavior == 105 || btn.BtnBehavior == 111 || btn.BtnBehavior == 310 || btn.BtnBehavior == 311)//保存并跳转
                {
                    if(controllss.Count==0)
                    throw new Exception("按钮" + btn.BtnNameCn+":未有配置Action");
                    else if(controllss.Count!=2)
                    throw new Exception("按钮" + btn.BtnNameCn+":需要2个Action");                
                }

                if (bdrop && i == 0)
                {
                    sb.AppendLine("<div class='btn-group'>");
                }
                else if (bdrop)
                    sb.AppendLine("<li>");

                if (btn.BtnType == 2)//上传
                {
                    var uploadifyName = "uploadify";
                    var ModularOrFunBtnRemark = btn.ModularOrFunBtnRemark;
                    var temparrs = btn.ModularOrFunBtnRemark.Split('|');
                    if (temparrs.Length > 1)
                    {
                        uploadifyName = temparrs[0];
                        ModularOrFunBtnRemark = temparrs[1];
                    }
                    //sb.AppendLine(string.Format("<input type='file' name='uploadify' class='uploadify uploadifyrow' id='uploadify' {0} /><div id='uploadifydiv'></div>", btn.ModularOrFunBtnRemark));
                    var strtemp = string.Format("<input type='file' name='{0}' class='uploadify uploadifyrow' id='{0}' {1} /><div id='{0}div'></div>", uploadifyName, ModularOrFunBtnRemark);
                    sb.AppendLine(strtemp);
                    //data-folder='/Files/ProductFiles' data-browerext='*.psd;*.jpeg;*.jpg' data-url='/P_ProductAreas/P_Attachment/Row' data-pkname='P_ProductID'  data-filename='PAttachmentFileName' data-fileguid='PAttachmentFileNameGuid' data-fileext='PAttachmentFileType' data-filesize='PAttachmentFileSize'/>
                    continue;
                }

                if (btn.BtnBehavior == 1)
                {
                    #region 1个url：拼接url
                    //1:通用：Url(跳转)
                    //2:通用：UrlDom查询(Ajax)
                    //var url = BulidUrl(item, btn);
                    
                    var url = UrlByControll(item, controllss.First());
                    //var url = UrlByBtn(item, btn);
                    //sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary btn-FwBtnSubmit' data-fun='{0}' data-posturl='{1}'  data-targeturlparamname='{2}'>{3}</a>",
                    //    btn.BtnBehavior, url, btn.TargetDom, btn.BtnNameCn));
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary ' href='{0}'  data-targeturlparamname='{1}'>{2}</a>",
                        url, btn.TargetDom, btn.BtnNameCn));
                    #endregion
                }
                if (btn.BtnBehavior == 2)
                {
                    #region 1个url：拼接url
                    //1:通用：Url(跳转)
                    //2:通用：UrlDom查询(Ajax)
                    //var url = BulidUrl(item, btn);
                    var url = UrlByControll(item, controllss.First());
                    //var url = UrlByBtn(item, btn);
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary btn-FwBtnSubmit' data-fun='{0}' data-posturl='{1}'  data-targeturlparamname='{2}'>{3}</a>",
                        btn.BtnBehavior, url, btn.TargetDom, btn.BtnNameCn));
                    #endregion
                }
                else if (btn.BtnBehavior == 3 || btn.BtnBehavior == 4)
                {
                    #region 1个url：url参数由targeturlparamname指定
                    //1:通用：Url-TargerParam查询(跳转)
                    //通用：Url-TargerParam查询-Dom查询(Ajax)
                    //var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).First();
                    //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls.ActionMethod;

                    var posturl = UrlByControll(item, controllss[0]);

                    sb.AppendLine(string.Format("<button class='btn  btn-primary btn-FwBtn' data-fun='{0}' data-posturl='{1}'  data-targeturlparamname='{2}' data-targetdom='{3}'>{4}</button>",
                        btn.BtnBehavior, posturl, btn.ParamName, btn.TargetDom, btn.BtnNameCn));
                    #endregion
                }
                else if (btn.BtnBehavior == 10)//弹窗-Url
                {
                    //var url = BulidUrl(item, btn, Design_ModularOrFun);
                    var url = UrlByControll(item, controllss.First());
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary btn-FwPopupGet' data-fun='10' data-posturl='{0}' data-popupwidth='{1}' data-modularname='{2}' data-btnnamecn='{3}' >{4}</a>",
                        url, btn.PopupWidth, Design_ModularOrFun.ModularName, btn.BtnNameCn, btn.BtnNameCn));
                }
                else if (btn.BtnBehavior == 15)//弹窗选择   //btn.BtnBehavior == 15 || btn.BtnBehavior == 16)
                {
                    #region 弹窗选择
                    //Design_ModularOrFun.ModularName不对
                    //var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                    var pkname = "P_ProductID";
                    var masteditarea = "";
                    var popupaddrepeat = "0";
                    var paramname = "";
                    //masteditarea  pkname popupaddrepeat paramname
                    //ParamName
                    if (!string.IsNullOrEmpty(btn.ModularOrFunBtnRemark))
                    {
                        var arrs = btn.ModularOrFunBtnRemark.Split(',');
                        foreach (var arr in arrs)
                        {
                            var arrtemps = arr.Split('|');
                            if (arrtemps[0] == "pkname")
                            { pkname = arrtemps[1]; }
                            else if (arrtemps[0] == "masteditarea")
                                masteditarea = arrtemps[1];
                            else if (arrtemps[0] == "popupaddrepeat")
                                popupaddrepeat = arrtemps[1];
                            else if (arrtemps[0] == "paramname")
                                paramname = arrtemps[1];
                        }
                        //pkname = btn.ModularOrFunBtnRemark;
                        //masteditarea = "";
                    }
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + "  btn-primary btn-FwPopup'  data-masteditarea='{0}'  data-tableselect='{1}'  data-btnnamecn='{3}' data-popupwidth='{4}' data-modularname='{5}' data-popupaddrepeat='{6}'  data-popuppkname='{7}' data-pkname='{8}'  data-paramname='{9}'",
                     masteditarea, btn.TableSelect, controllss[1].ParamName, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, popupaddrepeat, "", pkname, paramname));

                    //var url = BulidUrl(item, btn, Design_ModularOrFun);
                    //sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='' ", url));
                    //弹窗：本控制器，考虑查询的情况
                    //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[0].ActionMethod;
                    //var targeturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[1].ActionMethod;
                    var posturl = controllss[0].ActionPath;
                    var targeturl = controllss[1].ActionPath;
                    sb.AppendLine(string.Format(" data-popupurl='{0}' data-targeturl='{1}' ", posturl, targeturl));

                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");
                    #endregion
                }
                else if (btn.BtnBehavior == 101 || btn.BtnBehavior == 102 || btn.BtnBehavior == 105 || btn.BtnBehavior == 111 || btn.BtnBehavior == 310 || btn.BtnBehavior == 311)
                {
                    var btnClassDI = "btn-FwBtnSubmit";
                    if (btn.BtnBehavior == 101 || btn.BtnBehavior == 102 || btn.BtnBehavior == 111)
                        btnClassDI = "";
                    #region 2个url，url数据由targeturlparamname指定
                    //101：//弹窗:添加-保存并关闭  ,关闭窗体
                    //102：弹窗:添加-保存并添加，清空窗体
                    //105：//新页：添加-保存并添加(提交-Targer-插入表格Row)，清空范围不同。评论
                    //111：//弹窗:编辑-保存并关闭
                    //310：新页:提交+Url-TargerParam查询(跳转)
                    //311：新页:提交+Url-TargerParam-Dom查询(Ajax)
                    var controlls = controllss;
                    var targeturlparamname = controlls[1].ParamName;// Design_ModularOrFun.ControllCode + "ID";
                    if (btn.BtnBehavior == 310 || btn.BtnBehavior == 311)
                        targeturlparamname = controlls[1].ParamName;
                    ///////////////////////////
                    //var controlls = controllss;// ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                    //               sb.AppendLine(string.Format("<button class='btn btn-xs btn-primary btn-FwPopupGet' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}'",
                    //btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName));

                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary " + btnClassDI + "' data-fun='{0}' data-targeturlparamname='{1}' data-masteditarea='{2}' data-childtableselect='{3}' data-targetdom='{4}'  data-tableselect='{5}' data-pkname='{6}' data-btnnamecn='{7}'",
                            btn.BtnBehavior, targeturlparamname, btn.MastEditArea, btn.ChildtableSelect, btn.TargetDom, btn.TableSelect, Design_ModularOrFun.ControllCode + "ID", btn.BtnNameCn));

                    //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[0].ActionMethod;
                    //var targeturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[1].ActionMethod;
                    var posturl = controlls[0].ActionPath;// UrlByControll(item, controllss[0]);
                    var targeturl = controlls[1].ActionPath;// UrlByControll(item, controllss[1]);

                    sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='{1}'  ", posturl, targeturl));//, controlls[1].ParamName));
                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");

                    #endregion
                }
                else if (btn.BtnBehavior == 300 || btn.BtnBehavior == 301)//新页:提交+Url查询(跳转)  新页:提交+Url-Dom查询(Ajax)
                {
                    #region 2个url，url数据拼接而成
                    var controlls = controllss;// ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + "  btn-primary btn-FwBtnSubmit' data-fun='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-targetdom='{3}' ",
                             btn.BtnBehavior, btn.MastEditArea, btn.ChildtableSelect, btn.TargetDom, btn.TableSelect));

                    //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[0].ActionMethod;
                    var posturl = UrlByControll(item, controlls[0]);
                    var targeturl = UrlByControll(item, controlls[1]);
                    sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='{1}'  ", posturl, targeturl));
                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");

                    #endregion
                }
                else if (btn.BtnBehavior == 200)//新页：Url查询(Ajax)-插入表格Row
                {
                    var controlls = controllss.First();// ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).First();
                    //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls.ActionMethod;
                    var posturl = UrlByControll(item, controllss[0]);
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary btn-FwBtnSubmit' data-fun='200' data-posturl='{0}' ",
                     posturl));
                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");
                }
                else if (btn.BtnBehavior == 400)//新页：Url查询(Ajax)-插入表格Row
                {
                    var controlls = controllss.First();// ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).First();
                    //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls.ActionMethod;
                    var posturl = UrlByControll(item, controllss[0]);
                    //sb.AppendLine(string.Format("<button class='btn " + btn_xs + " btn-primary btn-FwBtnSubmit' data-fun='200' data-posturl='{0}' ",
                    //posturl));
                    if (string.IsNullOrEmpty(btn.BtnSearchMethod))
                        btn.BtnSearchMethod = "Framework.FwSearch";
                    sb.AppendLine(string.Format("<a class='btn btn-primary btn-FwSearch' data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'", btn.BtnSearchMethod, posturl, btn.TargetDom));

                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");
                }
                else if (btn.BtnBehavior == 41)
                {
                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='41' data-btnnamecn='删除' class='btn btn-primary btn-xs   btn-FwRowUIPopup' data-calcols='" + calcols + "'><span class='glyphicon glyphicon-trash'></span>删除</a>");
                    //sb.AppendLine(string.Format("<button class='btn btn-xs  btn-primary btn-FwRowUIPopup' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}' data-calcols='{10}'",
                    //        btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, calcols));
                    //sb.AppendLine(">" + btn.BtnNameCn + "</button>");
                }
                else if (btn.BtnBehavior == 42)
                {
                    var controlls = controllss.First();
                    var posturl = UrlByControll(item, controllss[0]);

                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='42' data-posturl='" + posturl + "' data-btnnamecn='删除' class='btn btn-primary btn-xs   btn-FwRowUIPopup' data-calcols='" + calcols + "'><span class='glyphicon glyphicon-trash'></span>删除</a>");
                    //sb.AppendLine(string.Format("<button class='btn btn-xs  btn-primary btn-FwRowUIPopup' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}' data-calcols='{10}'",
                    //        btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, calcols));

                    //sb.AppendLine(">" + btn.BtnNameCn + "</button>");
                }
                else if (btn.BtnBehavior == 70)
                {
                    var controlls = controllss.First();
                    var posturl = UrlByControll(item, controllss[0]);
                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='70' data-posturl='" + posturl + "' data-btnnamecn='" + btn.BtnNameCn + "' class='btn btn-primary btn-xs   btn-FwRowUIPopup' data-calcols='" + calcols + "'>" + btn.BtnNameCn + "</a>");
                }
                else if (btn.BtnBehavior == 51)
                {
                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='51' data-btnnamecn='删除' class='btn btn-primary  btn-BatchUI' data-calcols='" + calcols + "'><span class='glyphicon glyphicon-trash'></span>删除</a>");
                }
                //data-url
                else if (btn.BtnBehavior == 60)
                {
                    var controlls = controllss.First();
                    var posturl = UrlByControll(item, controllss[0]);

                    sb.AppendLine(string.Format("<a href='javascript:void(0);'  data-fun='60' data-url='" + posturl + "' class='btn btn-primary  btn-Export' data-calcols='" + calcols + "'>{0}</a>", btn.BtnNameCn));
                }
                if (bdrop && i == 0)
                {
                    //sb.AppendLine("    <button class='btn btn-xs btn-primary btn-FwBtnSubmit' data-fun='1' data-posturl='/FranchiseeAreas/Fra_Franchisee/Edit?Item.Fra_FranchiseeID=1005' data-targeturlparamname=''>编辑加盟商</button>");
                    sb.AppendLine("    <button type='button' class='btn btn-xs btn-danger dropdown-toggle' data-toggle='dropdown' aria-expanded='false'>");
                    sb.AppendLine("        <span class='caret'></span>");
                    sb.AppendLine("        <span class='sr-only'>Toggle Dropdown</span>");
                    sb.AppendLine("    </button>");
                    sb.AppendLine("    <ul class='dropdown-menu' role='menu'>");
                }
                else if (bdrop)
                    sb.AppendLine("    </li>");
            }
            if (bdrop)
                sb.AppendLine("</ul>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        /// <summary>
        /// 按钮超链接：
        /// 根据按钮url：模块区域+"/"+模块控制器+"/"+控制器方法名
        /// 参数：根据控制器需要的参数
        /// </summary>
        /// <param name="item"></param>
        /// <param name="btn_xs"></param>
        /// <param name="sb"></param>
        /// <param name="btn"></param>
        /// <param name="Design_ModularOrFun"></param>
        private static void btnLink(object item, string btn_xs, StringBuilder sb, SoftProjectAreaEntity btn, SoftProjectAreaEntity Design_ModularOrFun)
        {
            var url = BulidUrl(item, btn, Design_ModularOrFun);
            sb.AppendLine(string.Format("<a href='{0}' class='btn " + btn_xs + " btn-success'>{1}</a>", url, btn.BtnNameCn));
        }

        private static string BulidUrl(object item, SoftProjectAreaEntity btn, SoftProjectAreaEntity Design_ModularOrFun)
        {
            var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).First();

            var url = UrlByControll(item, controlls);
            return url;
        }

        private static string BulidUrl(object item, SoftProjectAreaEntity btn)
        {
            var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).First();

            var url = UrlByControll(item, controlls);
            return url;
        }

        private static string UrlByControll(object item, SoftProjectAreaEntity controlls)
        {
            var strParam = "?";

            if (controlls.ParamName != null && controlls.ParamName.Length > 0 && item != null)
            {
                #region 对象数据类型

                Type type = item.GetType();
                #endregion

                var paramNames = controlls.ParamName.Split(',');
                foreach (var param in paramNames)
                {
                    PropertyInfo property = type.GetProperty(param);
                    var value = property.GetValue(item, null);
                    strParam += "Item." + param + "=" + value;
                    //var val=item.
                }
            }
            //var url = "/" + controlls.AreasCode + "/" + controlls.ControllCode + "/" + controlls.ActionMethod + strParam;
            var url = controlls.ActionPath + strParam;
            return url;
        }

        //private static string UrlByBtn(object item, SoftProjectAreaEntity btn)
        //{
        //    var strParam = "?";

        //    if (btn.ParamName != null && btn.ParamName.Length > 0 && item != null)
        //    {
        //        #region 对象数据类型

        //        Type type = item.GetType();
        //        #endregion

        //        var paramNames = btn.ParamName.Split(',');
        //        foreach (var param in paramNames)
        //        {
        //            PropertyInfo property = type.GetProperty(param);
        //            var value = property.GetValue(item, null);
        //            strParam += "Item." + param + "=" + value;
        //            //var val=item.
        //        }
        //    }
        //    //var url = "/" + controlls.AreasCode + "/" + controlls.ControllCode + "/" + controlls.ActionMethod + strParam;
        //    var url = btn.ActionPath + strParam;
        //    return url;
        //}

        //private static string UrlByControll(object item, SoftProjectAreaEntity controlls)
        //{
        //    var strParam = "?";

        //    if (controlls.ParamName != null && controlls.ParamName.Length > 0 && item != null)
        //    {
        //        #region 对象数据类型

        //        Type type = item.GetType();
        //        #endregion

        //        var paramNames = controlls.ParamName.Split(',');
        //        foreach (var param in paramNames)
        //        {
        //            PropertyInfo property = type.GetProperty(param);
        //            var value = property.GetValue(item, null);
        //            strParam += "Item." + param + "=" + value;
        //            //var val=item.
        //        }
        //    }
        //    var url = "/" + controlls.AreasCode + "/" + controlls.ControllCode + "/" + controlls.ActionMethod + strParam;
        //    return url;
        //}

        //public static MvcHtmlString ToolBarHtml(this HtmlHelper helper, object item, string ModularOrFunCode) // MyResponseBase model)
        //{
        //    //var html = BtnHtml();
        //    //var item = model.Item;
        //    //var ModularOrFunCode = "";// model.ModularOrFunCode;//AuthorizationAreas.De_Member.DeleteGet
        //    var OperPos = 1;
        //    string btn_xs = "";
        //    StringBuilder sb = new StringBuilder();
        //    //resp.ModularOrFunCode
        //    #region 模块信息
        //    var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
        //    if (Design_ModularOrFun.GroupModularOrFun == 3)
        //        Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).FirstOrDefault();
        //    #endregion
        //    var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == ModularOrFunCode && (p.OperPos == OperPos)).OrderBy(p => p.Sort).ToList();

        //    //(4)模块列表页的Table字段信息
        //    var Fields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
        //    //显示字段
        //    var TableDispInfos = Fields.Where(p => p.FieldFunTypeID == null || ((((int)p.FieldFunTypeID) & 1) == 1)).ToList();// ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();

        //    //var PrimaryKey = "Pre_UserID";// ProjectCache.Design_ModularPageFields.Where(p => p.bPrimaryKey == 1).FirstOrDefault().NameEn;
        //    foreach (var btn in btns)
        //    {
        //        //<a href="~/AuthorizationAreas/Pre_User/Add" class='btn btn-success'><span class='glyphicon glyphicon-plus'></span>新增</a>
        //        if (btn.BtnBehavior == 100)
        //            continue;
        //        if (btn.BtnBehavior == 13)//跳转
        //        {
        //            btnLink(item, btn_xs, sb, btn, Design_ModularOrFun);
        //        }
        //        if (btn.BtnBehavior == 14)//导出
        //        {
        //            //查询的地址、区域等
        //            //btnLink(item, btn_xs, sb, btn, Design_ModularOrFun);
        //        }
        //        else if (btn.BtnBehavior == 15)//弹窗选择   //btn.BtnBehavior == 15 || btn.BtnBehavior == 16)
        //        {
        //            //Design_ModularOrFun.ModularName不对
        //            var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
        //            sb.AppendLine(string.Format("<button class='btn  btn-primary btn-FwPopup'  data-masteditarea='{0}'  data-tableselect='{1}' data-pkname='{2}'  data-btnnamecn='{3}' data-popupwidth='{4}' data-modularname='{5}' data-popupaddrepeat={6}  data-popuppkname={7}",
        //             btn.MastEditArea, btn.TableSelect, controlls[1].ParamName, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, btn.popupaddrepeat, controlls[0].ParamName));

        //            //var url = BulidUrl(item, btn, Design_ModularOrFun);
        //            //sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='' ", url));
        //            //弹窗：本控制器，考虑查询的情况
        //            var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[0].ActionMethod;
        //            var targeturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[1].ActionMethod;
        //            sb.AppendLine(string.Format(" data-popupurl='{0}' data-targeturl='{1}' ", posturl, targeturl));

        //            sb.AppendLine(">" + btn.BtnNameCn + "</button>");

        //        }
        //        else if (btn.BtnBehavior == 16)//批量删除
        //        {
        //            #region 所有列计算:用于删除行时计算列名

        //            var calcols = "";
        //            for (var j = 0; j < TableDispInfos.Count; j++)
        //            {
        //                var field = TableDispInfos[j];
        //                if (!string.IsNullOrEmpty(field.calcol))
        //                {
        //                    calcols += field.calcol + ",";// string.Format("data-calcol='{0}'", field.calcol);
        //                }
        //            }
        //            if (calcols.Length > 0)
        //                calcols = calcols.Substring(0, calcols.Length - 1);
        //            #endregion

        //            //var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
        //            sb.AppendLine(string.Format("<button class='btn  btn-primary btn-BatchUI'  data-masteditarea='{0}'  data-tableselect='{1}'   data-btnnamecn='{2}' data-popupwidth='{3}' data-modularname='{4}' data-popupaddrepeat='{5}'   data-fun='{6}' data-calcols='{7}'",
        //             btn.MastEditArea, btn.TableSelect, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, btn.popupaddrepeat, btn.PopupAfterTableFun, calcols));
        //            sb.AppendLine(">" + btn.BtnNameCn + "</button>");
        //        }
        //        else if (btn.BtnBehavior == 12)//弹窗(Get)：主表区域的data-targeturlparamname参数查询
        //        {
        //            #region 弹窗(Get)
        //            //11：保存并关闭
        //            //12：保存并添加
        //            //目前应用：添加，有主表，例如顾客回访记录、充值等的添加--弹窗(Get获取)
        //            //在页面中设置主表区域：用于存放主表信息
        //            //添加按钮：事件触发：btn-FwToolPopup
        //            //          获取添加页面url：data-posturl
        //            //          获取添加页面url的数据：data-targeturlparamname提交
        //            //var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
        //            sb.AppendLine(string.Format("<button class='btn  btn-primary btn-FwPopupGet' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}'",
        //                     btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName));

        //            var url = BulidUrl(item, btn, Design_ModularOrFun);
        //            sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='' ", url));

        //            //var posturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[0].ActionMethod;
        //            //if (controlls.Count == 1)
        //            //{
        //            //    sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='' data-targeturlparamname='{1}' ", posturl, controlls[0].ParamName));
        //            //}
        //            //else
        //            //{
        //            //    var targeturl = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls[1].ActionMethod;
        //            //    sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='{1}' data-targeturlparamname='{2}' ", posturl, targeturl, controlls[1].ParamName));
        //            //}

        //            sb.AppendLine(">" + btn.BtnNameCn + "</button>");

        //            //提交的后台posturl，
        //            //后续操作：0
        //            //弹窗，对表格的操作：1:首行插入行，2:替换行，3:删除行，4：重载页面 5:界面删除
        //            //查看：0
        //            //界面删除：5
        //            //删除：3或4：posturl
        //            //提交：2：posturl      rowurl
        //            //审核：2: posturl......rowurl
        //            //DB功能：  1添加：         1、保存并添加=>添加到表格 2、保存并关闭=>添加到表格
        //            //          2编辑：         11、保存继续编辑=>替换行   12、保存并关闭=>替换行
        //            //              提交(编辑)：11、提交继续编辑=>替换行   12、提交并关闭=>替换行     13、提交并下一条    14、上一条  15、下一条
        //            //              提交(确定)：                           12、提交并关闭=>替换行     13、提交并下一条    14、上一条  15、下一条
        //            //              审核(编辑)：11、审核继续编辑=>替换行   12、审核并关闭=>替换行     13、审核并下一条    14、上一条  15、下一条
        //            //              审核(确定)：                           12、审核并关闭=>替换行     13、审核并下一条    14、上一条  15、下一条
        //            //          3删除：  3、删除=>删除表格行      4、删除=>页面重载  5、界面删除
        //            //          4查看：
        //            #endregion
        //        }
        //        else if (btn.BtnBehavior == 11)
        //        {
        //            sb.AppendLine(string.Format("<button class='btn  btn-primary btn-FwToolPopup' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}'",
        //                     btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName));

        //            var url = BulidUrl(item, btn, Design_ModularOrFun);
        //            sb.AppendLine(string.Format(" data-posturl='{0}' ", url));
        //            sb.AppendLine(">" + btn.BtnNameCn + "</button>");
        //        }
        //    }
        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        /// <summary>
        /// 工具条代码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <returns></returns>List<SoftProjectAreaEntity> DynReportDefineDetails, object item, Type type, int j
        public static string BtnRowHtml(this HtmlHelper helper, object item, SoftProjectAreaEntity Design_ModularOrFun,
            List<SoftProjectAreaEntity> btns, string calcols1)//int OperPos = 1, string btn_xs = "",
        {
            var OperPos = 2;
            string btn_xs = "btn-xs";
            #region 主键值
            Type type = item.GetType();
            #endregion
            StringBuilder sb = new StringBuilder();

            var strbutHtml = ButtonHtml(helper, item, Design_ModularOrFun, btns, "btn-xs", calcols1);

            #region
            //   foreach (var btn in btns)
            //   {
            //       #region 按钮显示条件比较
            //       if (!string.IsNullOrEmpty(btn.DispConditionsExpression))
            //       {
            //           var DispConditionsExpressionArr = btn.DispConditionsExpression.Split('|');
            //           #region 第1个数的值
            //           PropertyInfo property = type.GetProperty(DispConditionsExpressionArr[1]);
            //           var value1 = property.GetValue(item, null);
            //           if (value1 == null)
            //               throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
            //           var strValue1 = value1.ToString();
            //           #endregion

            //           #region 第2个数的值
            //           var strValue2 = DispConditionsExpressionArr[3];
            //           if (DispConditionsExpressionArr[0] == "2")
            //           {
            //               property = type.GetProperty(DispConditionsExpressionArr[3]);
            //               var value2 = property.GetValue(item, null);
            //               if (value2 == null)
            //                   throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
            //               strValue2 = value2.ToString();
            //           }
            //           #endregion
            //           #region 比较运算
            //           switch (DispConditionsExpressionArr[2])
            //           {
            //               case "equal":
            //                   if (strValue1 != DispConditionsExpressionArr[3])
            //                       continue;
            //                   break;
            //           }
            //           #endregion
            //       }

            //       #endregion
            //       if (btn.BtnBehavior == 13)//跳转
            //       {
            //           btnLink(item, btn_xs, sb, btn, Design_ModularOrFun);
            //       }
            //       else if (btn.BtnBehavior == 11 || btn.BtnBehavior == 12)
            //       {
            //           #region 主键值
            //           //Type type = item.GetType();
            //           PropertyInfo propertyPrimaryKey = type.GetProperty(Design_ModularOrFun.PrimaryKey);
            //           object value = propertyPrimaryKey.GetValue(item, null);
            //           string pkvalue = "";
            //           if (value != null)
            //               pkvalue = value.ToString();
            //           #endregion

            //           if (btn.BtnBehavior == 11)//弹窗：UI
            //           {
            //               #region 弹窗UI
            //               sb.AppendLine(string.Format("<button class='btn btn-xs  btn-primary btn-FwRowUIPopup' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}' data-calcols='{10}'",
            //btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, calcols));

            //               //var url = BulidUrl(item, btn, Design_ModularOrFun);
            //               //sb.AppendLine(string.Format(" data-posturl='{0}' ", url));
            //               sb.AppendLine(">" + btn.BtnNameCn + "</button>");
            //               //#endregion
            //               #endregion
            //           }
            //           else if (btn.BtnBehavior == 12)//弹窗：Get，值由pkname、pkval进行查询
            //           {
            //               #region 弹窗Get
            //               sb.AppendLine(string.Format("<button class='btn btn-xs btn-primary btn-FwPopupGet' data-targetdom='{0}'  data-masteditarea='{1}' data-childtableselect='{2}' data-fun='{3}' data-tableselect='{4}' data-pkname='{5}' data-btnbehavior='{6}' data-btnnamecn='{7}' data-popupwidth='{8}' data-modularname='{9}'",
            //btn.TargetDom, btn.MastEditArea, btn.ChildtableSelect, btn.PopupAfterTableFun, btn.TableSelect, Design_ModularOrFun.PrimaryKey, btn.BtnBehavior, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName));

            //               var url = BulidUrl(item, btn, Design_ModularOrFun);
            //               //var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).First();

            //               //var strParam = "?";
            //               //PropertyInfo property = type.GetProperty(Design_ModularOrFun.PrimaryKey);
            //               //        var valuet = property.GetValue(item, null);
            //               //        strParam += "Item." + Design_ModularOrFun.PrimaryKey + "=" + valuet;
            //               //var url = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/" + controlls.ActionMethod + strParam;

            //               sb.AppendLine(string.Format(" data-posturl='{0}' data-targeturl='' ", url));

            //               sb.AppendLine(">" + btn.BtnNameCn + "</button>");

            //               #endregion
            //           }
            //       }
            //   }
            #endregion

            return strbutHtml.ToString();
        }


        #region 查询条件--列表页面

        /// <summary>
        /// 新：查询条件位置：由参数确定
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <param name="ModularOrFunCode"></param>
        /// <returns></returns>
        public static MvcHtmlString QueryHtml(this HtmlHelper helper, Querys Querys, string ModularOrFunCode, SoftProjectAreaEntity data)// MyResponseBase model)// Querys Querys)
        {
            //var ModularOrFunCode = "";
            //var Querys = model.Querys;
            StringBuilder sbQuery = new StringBuilder();
            //基础类
            var conts = helper.ViewContext.Controller as BaseController;
            var modulars = conts.Design_ModularOrFun;// ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();

            var QueryFields = QueryFormEleTypes(modulars);

            #region 快速、高级查询

            var fastQueryFields = QueryFields.Where(p => p.QueryPos == 1).ToList();
            var advQueryFields = QueryFields.Where(p => p.QueryPos == 2).ToList();
            if (QueryFields.Count == 0)
                return new MvcHtmlString(sbQuery.ToString());

            sbQuery.AppendLine("<div class='fastWhere'>");
            for (int i = 0; i < fastQueryFields.Count(); i++)
            {
                var item = fastQueryFields[i];
                var strDrop = "";
                if (item.FormEleType == 4)
                {
                    strDrop = QueryHtmlDropDownList(helper, Querys, data, item, strDrop);
                }
                else if (item.FormEleType == 128)
                {
                    strDrop = QueryHtmlDropTree(Querys, data, item, strDrop);
                }
                else if (item.FormEleType == 8192)//下拉复选框(位)
                {
                    #region 下拉复选框
                    var val = Querys.GetValue(item.name + "___bitand");//位与

                    if (ProjectCache.IsExistyCategory(item.name))
                    {
                        //NameCn	"状态"	string
                        //var str = HtmlHelpers.DropDownList(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name),          "DValue", "DText", val, "", "==" + item.NameCn + "==");
                        var str = HtmlHelpers.DropDownListMultiSelect(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name), "DValue", "DText", val, "");

                        //sbHtml.AppendLine(str.ToString());
                        strDrop = str.ToString();
                    }
                    #endregion
                }

                //56：整数   106：小数    167：字符串   61：日期
                #region 快速查询
                if (strDrop.Length > 0)
                {
                    sbQuery.AppendLine(strDrop);
                }
                else
                {
                    if (item.FormEleType == 8)
                    {
                        var val = Querys.GetValue(item.name + "___equal");
                        sbQuery.AppendLine("<input type='hidden' class='form-control' id='" + item.name + "___equal' name='" + item.name + "___equal'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='int' value='" + val + "' />");
                        continue;
                    }
                    #region 文本框绘制
                    var classcss = "";
                    var datatype = "int";
                    if (item.xtype == 106)
                    {
                        datatype = "decimal";
                    }
                    else if (item.xtype == 167)
                    {
                        datatype = "string";
                    }
                    if (item.xtype == 61)
                    {
                        datatype = "date";
                        classcss = "datepicker";
                    }

                    if (item.QueryType == 1)
                    {
                        var val = Querys.GetValue(item.name + "___like");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='" + val + "' />");
                    }
                    else if (item.QueryType == 2)
                    {
                        var val = Querys.GetValue(item.name + "___equal");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___equal' name='" + item.name + "___equal'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='" + val + "' />");
                    }
                    else if (item.QueryType == 3 || item.QueryType == null)
                    {
                        var val1 = Querys.GetValue(item.name + "___greaterequal");
                        var val2 = Querys.GetValue(item.name + "___lessequal");
                        sbQuery.AppendLine("<input type='text' class=' form-control " + classcss + "' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='" + datatype + "' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
                        sbQuery.AppendLine("<input type='text' class=' form-control " + classcss + "' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='" + datatype + "' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
                    }
                    #endregion
                }
                #endregion

            #endregion
            }
            var hiddcount = fastQueryFields.Where(p => p.FormEleType == 8).Count();
            if (hiddcount < fastQueryFields.Count())
            {
                sbQuery.AppendLine(string.Format("<button class='btn btn-primary btn-FwSearch' data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'><span class='glyphicon glyphicon-search'></span>查询</button>", modulars.SearchMethod, modulars.ActionPath, ".targetdom"));
                if (advQueryFields.Count > 0)
                {
                    sbQuery.AppendLine("<button class='btn btn-primary btn-AdvSearch' id='advSearch' data-module='advSearchArea' data-parents='SearchArea' >");
                    sbQuery.AppendLine("<span class='glyphicon glyphicon-search'></span>高级查询</button>");
                }
            }
            sbQuery.AppendLine("</div>");

        #endregion

            #region 高级查询

            if (advQueryFields.Count > 0)
            {
                sbQuery.AppendLine("<div style=\"width: 700px; display: none; background-color: rgb(255, 255, 255); box-shadow: 3px 1px 24px rgb(136, 136, 136); padding: 10px; z-index: 9999;position:'absolute'; right: 30px;\"");
                sbQuery.AppendLine("    class='SearchAreaDetail' id='advSearchArea' >");
                sbQuery.AppendLine("    <a style='top: 5px; right: 10px; position: absolute; cursor: pointer;' id='module_close'><i class='glyphicon glyphicon-remove'></i></a>");
                sbQuery.AppendLine("    <div class='moreWhere'>");
                sbQuery.AppendLine("        <ul style='margin-bottom: 0px; margin-top: 10px; list-style: outside none none;' class='container-fluid'>");

                for (var i = 0; i < advQueryFields.Count; i++)
                {
                    var item = advQueryFields[i];
                    var strDrop = "";
                    if (item.FormEleType == 4)
                    {
                        strDrop = QueryHtmlDropDownList(helper, Querys, data, item, strDrop);
                    }
                    else if (item.FormEleType == 128)
                    {
                        strDrop = QueryHtmlDropTree(Querys, data, item, strDrop);
                    }
                    else if (item.FormEleType == 8192)//下拉复选框(位)
                    {
                        #region 下拉复选框
                        var val = Querys.GetValue(item.name + "___bitand");//位与
                        if (ProjectCache.IsExistyCategory(item.name))
                        {
                            //NameCn	"状态"	string
                            //var str = HtmlHelpers.DropDownList(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name),          "DValue", "DText", val, "", "==" + item.NameCn + "==");
                            var str = HtmlHelpers.DropDownListMultiSelect(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name), "DValue", "DText", val, "");

                            //sbHtml.AppendLine(str.ToString());
                            strDrop = str.ToString();
                        }
                        #endregion
                    }
                    //56：整数   106：小数    167：字符串   61：日期
                    #region 查询条件
                    if (strDrop.Length > 0)
                    {
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine(strDrop);
                        sbQuery.AppendLine("</li>");
                    }
                    #region 绘制文本框
                    var classcss = "";
                    var datatype = "int";
                    if (item.xtype == 106)
                    {
                        datatype = "decimal";
                    }
                    else if (item.xtype == 167)
                    {
                        datatype = "string";
                    }
                    if (item.xtype == 61)
                    {
                        datatype = "date";
                        classcss = "datepicker";
                    }

                    if (item.QueryType == 1)
                    {
                        var val = Querys.GetValue(item.name + "___like");
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='" + val + "' />");
                        sbQuery.AppendLine("</li>");
                    }
                    else if (item.QueryType == 2)
                    {
                        var val = Querys.GetValue(item.name + "___equal");
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='" + val + "' />");
                        sbQuery.AppendLine("</li>");
                    }
                    else if (item.QueryType == 3)
                    {
                        var val1 = Querys.GetValue(item.name + "___greaterequal");
                        var val2 = Querys.GetValue(item.name + "___lessequal");
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine("<input type='text' class=' form-control " + classcss + "' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='" + datatype + "' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
                        sbQuery.AppendLine("</li>");
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine("<input type='text' class=' form-control " + classcss + "' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='" + datatype + "' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
                        sbQuery.AppendLine("</li>");
                    }
                    #endregion
                    #endregion
                }

                sbQuery.AppendLine("<li class='col-sm-4 text-right pull-right'>");
                sbQuery.AppendLine(string.Format("<button class='btn btn-primary pull-righ btn-FwSearch'  data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'><span class='glyphicon glyphicon-search'></span>查询</button>", modulars.SearchMethod, modulars.ActionPath, ".targetdom"));
                //sbQuery.AppendLine(string.Format("<button class='btn btn-primary           btn-FwSearch'  data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'><span class='glyphicon glyphicon-search'></span>查询</button>", modulars.SearchMethod, modulars.ActionPath, ".targetdom"));
                sbQuery.AppendLine("</li>");
                sbQuery.AppendLine("   </ul>");
                sbQuery.AppendLine("  </div>");
                sbQuery.AppendLine("  </div>");
            }
            #endregion

            #region 自定义查询条件

            //if (Design_Modular.bCustWhere == 1)
            //{
            //    sbQuery.AppendLine("<div style='width: 100%;'>");
            //    sbQuery.AppendLine("   <table class='table table-bordered table-hover table-striped custWhere' id='tabsearch' style='width: 100%;'>");
            //    sbQuery.AppendLine("       <thead>");
            //    sbQuery.AppendLine("           <tr>");
            //    sbQuery.AppendLine("               <th style='width: 80px;'>操作</th>");
            //    sbQuery.AppendLine("               <th style='width: 50px;'>与/或</th>");
            //    sbQuery.AppendLine("               <th style='width: 100px;'>字段</th>");
            //    sbQuery.AppendLine("               <th style='width: 50px;'>运算符</th>");
            //    sbQuery.AppendLine("               <th style='width: 100px;'>值</th>");
            //    sbQuery.AppendLine("           </tr>");
            //    sbQuery.AppendLine("       </thead>");
            //    sbQuery.AppendLine("       <tbody>");
            //    if (Querys.Count == 0)
            //    {
            //        Querys.Add(new Query { });
            //    }
            //    for (var i = 0; i < Querys.Count; i++)
            //    {
            //        var query = Querys[i];
            //        sbQuery.AppendLine("               <tr>");
            //        sbQuery.AppendLine("                   <td class='align-left valign-middle '>");
            //        sbQuery.AppendLine("                       <a href='javascript:void(0);' class='btn btn-primary btn-xs btn-FwCopy'>复</a>");
            //        sbQuery.AppendLine("                       <a href='javascript:void(0);' class='btn btn-primary btn-xs btn-FwDeleteNotHint' style='display: none;'>删</a>");
            //        sbQuery.AppendLine("                   </td>");
            //        sbQuery.AppendLine("                   <td>");
            //        var strdrop = HtmlHelpers.DropDownList(helper, "Querys[{0}].AndOr", ProjectCache.AndOrs, "Value", "Text", query.Oper, "", "=与/或=", "form-control", "style='width:80px;'");
            //        sbQuery.AppendLine(strdrop.ToString());
            //        sbQuery.AppendLine("                   </td>");
            //        sbQuery.AppendLine("                   <td>");
            //        //strdrop=HtmlHelpers.DropDownList(helper,"Querys[{0}].FieldName", FieldNames, "Value", "Text",query.FieldName, "", "=字段=","form-control queryfieldname", " data-datatype='date' data-bdic='1'");
            //        //    sbQuery.AppendLine(strdrop.ToString());
            //        sbQuery.AppendLine("                   </td>");
            //        sbQuery.AppendLine("                   <td>");
            //        strdrop = HtmlHelpers.DropDownList(helper, "Querys[{0}].Oper", ProjectCache.QueryOpers, "Value", "Text", query.Oper, "", "=运算符=", "form-control", "style='width:90px;'");
            //        sbQuery.AppendLine(strdrop.ToString());
            //        sbQuery.AppendLine("                   </td>");
            //        sbQuery.AppendLine("                   <td class='targetdom align-left'>");
            //        sbQuery.AppendLine("                       <input type='text' style='width:100px;' class='form-control' name='Querys[{0}].Value' value='" + @query.Value + "' />");
            //        sbQuery.AppendLine("                   </td>");
            //        sbQuery.AppendLine("               </tr>");

            //    }
            //    sbQuery.AppendLine("       </tbody>");
            //    sbQuery.AppendLine("   </table>");
            //    sbQuery.AppendLine("</div>");
            //    sbQuery.AppendLine("<div class='margin-top-5'>");
            //    sbQuery.AppendLine("    <button class='btn btn-primary pull-righ btn-FwSearch'><span class='glyphicon glyphicon-search'></span>查询</button>");
            //    sbQuery.AppendLine("</div>");
            //}
            #endregion

            MvcHtmlString mstr = new MvcHtmlString(sbQuery.ToString());
            return mstr;
        }

        //自动根据字段个数确定位置
        //public static MvcHtmlString QueryHtml(this HtmlHelper helper, Querys Querys, string ModularOrFunCode)// MyResponseBase model)// Querys Querys)
        //{
        //    //var ModularOrFunCode = "";
        //    //var Querys = model.Querys;
        //    StringBuilder sbQuery = new StringBuilder();
        //    //基础类
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    var modulars = ProjectCache.Design_ModularOrFun;// ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();

        //    //var Design_Modular = ProjectCache.Design_ModularPages.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();// "AuthorizationAreas.De_Member"

        //    //var QueryFields = ProjectCache.Design_ModularPageQueryFields.Where(p => p.Design_ModularOrFunID == modulars.ModularID && p.Query01FormEleType != null).ToList();
        //    //var QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == modulars.ModularID && p.Query01FormEleType != null).ToList();
        //    //QueryFields.ForEach(p => { p.FormEleType = p.Query01FormEleType; p.QueryType = p.Query01QueryType; });

        //    var QueryFields = QueryFormEleTypes(modulars);// PageFormEleTypes

        //    //var Design_Modular = ProjectCache.Design_ModularPages.Where(p => p.PageCode == "UserManger").FirstOrDefault();
        //    //var QueryFields = ProjectCache.Design_ModularPageQueryFields.Where(p => p.PageCode == "UserManger").OrderBy(p => p.QuerySort).ToList();
        //    #region 快速、高级查询

        //    int k = 0;
        //    bool bmorewhere = false;
        //    for (var i = 0; i < QueryFields.Count; i++)
        //    {
        //        var item = QueryFields[i];
        //        var strDrop = "";
        //        if (item.FormEleType == 4)
        //        {
        //            #region 下拉列表框
        //            var val = Querys.GetValue(item.name + "___equal");

        //            if (ProjectCache.IsExistyCategory(item.name))
        //            {
        //                //NameCn	"状态"	string
        //                var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.GetByCategory(item.name), "DValue", "DText", val, "", "==" + item.NameCn + "==");
        //                //sbHtml.AppendLine(str.ToString());
        //                strDrop = str.ToString();
        //            }
        //            else if (item.name == "Ba_AreaID1")//省  data-optionlabel='市(区、县)'
        //            {//==省(市)==
        //                var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.Ba_AreaID1s, "Ba_AreaID", "AreaName",
        //                    val, "", "==省(市)==", "form-control",
        //                    "  data-changeurl='/BaAreas/Ba_Area/GetSubBa_AreaIDs1s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Ba_AreaID2___equal' data-optionlabel='市(区、县)' ");
        //                strDrop = str.ToString();
        //            }
        //            else if (item.name == "Ba_AreaID2")//2级区域
        //            {
        //                #region 2级区域
        //                if (!string.IsNullOrEmpty(val))
        //                {//data-optionlabel='市(区、县)'
        //                    var Ba_AreaID = Convert.ToInt32(val);
        //                    var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.GetBrotherBa_AreaIDss(Ba_AreaID), "Ba_AreaID", "AreaName",
        //                        val, "", "==市(区、县)==", "form-control",
        //                        "  data-changeurl='/BaAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Ba_AreaID3___equal' data-optionlabel='市(区、县)' ");
        //                    strDrop = str.ToString();
        //                }
        //                else
        //                {
        //                    //var str = string.Format("<select id='Item_{0}' name='Item.{0}' class='form-control'  data-changeurl='/BaAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Item_Ba_AreaID3' ><option value='' selected=''>==市(区、县)==</option></select>", field.QueryEn);
        //                    var str = string.Format("<select id='{0}___equal' name='{0}___equal' class='form-control'  data-changeurl='/BaAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Ba_AreaID3___equal' data-optionlabel='市(区、县)'></select>", item.name);
        //                    strDrop = str.ToString();
        //                }
        //                #endregion
        //            }
        //            else if (item.name == "Ba_AreaID3")//3级区域
        //            {
        //                #region Ba_AreaID3
        //                if (!string.IsNullOrEmpty(val))
        //                {//==市(区、县)==
        //                    var Ba_AreaID = Convert.ToInt32(val);
        //                    var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.GetBrotherBa_AreaIDss(Ba_AreaID), "Ba_AreaID", "AreaName",
        //                        val, "", "==市(区、县)==", "form-control");
        //                    strDrop = str.ToString();
        //                }
        //                else
        //                {//<option value='' selected=''>==市(区、县)==</option>
        //                    var str = string.Format("<select id='{0}___equal' name='{0}___equal' class='form-control' ></select>", item.name);
        //                    strDrop = str.ToString();
        //                }
        //                #endregion
        //            }
        //            #endregion
        //        }
        //        else if (item.FormEleType == 128)
        //        {
        //            #region 下拉树
        //            var field = item;
        //            var val = Querys.GetValue(item.name + "___equal");

        //            if (field.name == "ParentProductCategoryID" || field.name == "P_ProductCategoryID")
        //            {
        //                #region 商品型号
        //                List<SoftProjectAreaEntity> Items = ProjectCache.P_ProductCategorys;
        //                var tt = new SelectTreeList(Items, "0", "ProductCategoryName", "P_ProductCategoryID", "ParentProductCategoryID", "P_ProductCategoryID", val, true, "");
        //                if (field.name == "ParentProductCategoryID")
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "ParentProductCategoryID___equal", tt, "==商品类别==");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                else
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "P_ProductCategoryID___equal", tt, "==商品类别==");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                #endregion
        //            }
        //            else if (field.name == "ParentOrganizationID" || field.name == "Pre_OrganizationID")
        //            {
        //                #region 组织机构
        //                List<SoftProjectAreaEntity> Items = ProjectCache.Pre_Organizations;
        //                var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentOrganizationID", "Pre_OrganizationID", val, true, "");
        //                if (field.name == "ParentOrganizationID")
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "ParentOrganizationID___equal", tt, "==组织机构==");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                else
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "Pre_OrganizationID___equal", tt, "==组织机构==");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                #endregion
        //            }
        //            else if (field.name == "ParentCaseCategoryID" || field.name == "CA_CaseCategoryID")
        //            {
        //                #region 案例类别
        //                List<SoftProjectAreaEntity> Items = ProjectCache.CA_CaseCategorys;
        //                var tt = new SelectTreeList(Items, "0", "CaseCategoryName", "CA_CaseCategoryID", "ParentCaseCategoryID", "CA_CaseCategoryID", val, true, "");
        //                if (field.name == "ParentCaseCategoryID")
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "ParentCaseCategoryID___equal", tt, "");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                else
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "CA_CaseCategoryID___equal", tt, "");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                #endregion
        //            }
        //            else if (field.name == "ParentToolCategoryID" || field.name == "T_ToolCategoryID")
        //            {
        //                #region 工具类别
        //                List<SoftProjectAreaEntity> Items = ProjectCache.T_ToolCategorys;
        //                var tt = new SelectTreeList(Items, "0", "ToolCategoryName", "T_ToolCategoryID", "ParentToolCategoryID", "T_ToolCategoryID", val, true, "");
        //                if (field.name == "ParentToolCategoryID")
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "ParentToolCategoryID___equal", tt, "");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                else
        //                {
        //                    var str = HtmlHelpers.DropDownForTree(null, "T_ToolCategoryID___equal", tt, "");
        //                    //sbHtml.AppendLine(str.ToString());
        //                    strDrop = str.ToString();
        //                }
        //                #endregion
        //            }
        //            #endregion
        //        }
        //        if (k < 3)//快速查找
        //        {
        //            //56：整数   106：小数    167：字符串   61：日期
        //            #region 快速查询
        //            if (i == 0)
        //            {
        //                sbQuery.AppendLine("<div class='fastWhere'>");
        //            }
        //            if (strDrop.Length > 0)
        //            {
        //                sbQuery.AppendLine(strDrop);
        //                k++;
        //                //continue;
        //            }
        //            else if (item.xtype == 56)//整数
        //            {
        //                #region 整数
        //                if (item.QueryType == 1)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='int' value='" + val + "' />");
        //                }
        //                else if (item.QueryType == 2 || item.QueryType == null)
        //                {
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    if (item.FormEleType != 8)
        //                    {
        //                        sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___equal' name='" + item.name + "___equal'  placeholder='" + item.NameCn + "' "
        //                            + " data-datatype='int' value='" + val + "' />");
        //                        k++;
        //                    }
        //                    else
        //                    {
        //                        sbQuery.AppendLine("<input type='hidden' class='form-control' id='" + item.name + "___equal' name='" + item.name + "___equal'  placeholder='" + item.NameCn + "' "
        //                            + " data-datatype='int' value='" + val + "' />");
        //                    }
        //                }
        //                else if (item.QueryType == 3)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='int' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='int' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                }
        //                #endregion
        //            }
        //            else if (item.xtype == 106)//小数
        //            {
        //                #region
        //                if (item.QueryType == 1)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='decimal' value='" + val + "' />");
        //                }
        //                else if (item.QueryType == 2 || item.QueryType == null)
        //                {
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='decimal' value='" + val + "' />");
        //                    k++;
        //                }
        //                else if (item.QueryType == 3)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='decimal' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='decimal' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                }

        //                //var val = Querys.GetValue(item.name + "___equal");
        //                //sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                //    + " data-datatype='decimal' value='" + val + "' />");
        //                #endregion
        //            }
        //            else if (item.xtype == 167)//字符串
        //            {
        //                #region
        //                if (item.QueryType == 1 || item.QueryType == null)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='string' value='" + val + "' />");
        //                }
        //                else if (item.QueryType == 2)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='string' value='" + val + "' />");
        //                }
        //                else if (item.QueryType == 3)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='string' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='string' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                }
        //                #endregion
        //            }
        //            else if (item.xtype == 61)//日期类型
        //            {//<input type='text' class=' form-control datepicker ' id='StorageDate___greaterequal' name='StorageDate___greaterequal' value='@Model.Querys.GetValue("StorageDate__greaterequal")' placeholder='入库起始日期' data-datatype='date' data-fieldnamecn='入库起始日期' /></li>
        //                #region
        //                if (item.QueryType == 1)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<input type='text' class='form-control datepicker' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='date' value='" + val + "' />");
        //                }
        //                else if (item.QueryType == 2)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    sbQuery.AppendLine("<input type='text' class='form-control datepicker' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='date' value='" + val + "' />");
        //                }
        //                else if (item.QueryType == 3 || item.QueryType == null)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control datepicker' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='date' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control datepicker' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='date' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                }
        //                #endregion
        //            }
        //            if (k >= 3 || i == QueryFields.Count - 1)
        //            {
        //                if (QueryFields.Count == 1 && item.FormEleType == 8)
        //                { }
        //                else
        //                {
        //                    sbQuery.AppendLine(string.Format("<button class='btn btn-primary btn-FwSearch' data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'><span class='glyphicon glyphicon-search'></span>查询</button>", modulars.SearchMethod, modulars.ActionPath, ".targetdom"));
        //                    if (QueryFields.Count > i + 1)
        //                    {
        //                        sbQuery.AppendLine("<button class='btn btn-primary btn-AdvSearch' id='advSearch' data-module='advSearchArea' data-parents='SearchArea'>");
        //                        sbQuery.AppendLine("<span class='glyphicon glyphicon-search'></span>高级查询</button>");
        //                    }
        //                }
        //                sbQuery.AppendLine("</div>");
        //            }
        //            #endregion
        //        }
        //        else//高级查询
        //        {
        //            if (k >= 3 && bmorewhere == false)
        //            {
        //                bmorewhere = true;
        //                sbQuery.AppendLine("<div style='width: 700px; display: none; background-color: rgb(255, 255, 255); top: 163px; right: 30px; box-shadow: 3px 1px 24px rgb(136, 136, 136); padding: 10px; z-index: 9999;'");
        //                sbQuery.AppendLine("    class='SearchAreaDetail' id='advSearchArea'>");
        //                sbQuery.AppendLine("    <a style='top: 5px; right: 10px; position: absolute; cursor: pointer;' id='module_close'><i class='glyphicon glyphicon-remove'></i></a>");
        //                sbQuery.AppendLine("    <div class='moreWhere'>");
        //                sbQuery.AppendLine("        <ul style='margin-bottom: 0px; margin-top: 10px; list-style: outside none none;' class='container-fluid'>");
        //            }
        //            //56：整数   106：小数    167：字符串   61：日期
        //            #region 查询条件
        //            if (strDrop.Length > 0)
        //            {
        //                sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                sbQuery.AppendLine(strDrop);
        //                sbQuery.AppendLine("</li>");
        //                k++;
        //            }
        //            else if (item.xtype == 56)//整数
        //            {
        //                //var val = Querys.GetValue(item.name + "___equal");
        //                //sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                //sbQuery.AppendLine(string.Format("<input type='text' class='form-control' id='{0}___equal' name='{0}___equal'  placeholder='{1}' "
        //                //    + " data-datatype='int' value='{2}' />", item.name, item.NameCn, val));
        //                //sbQuery.AppendLine("</li>");
        //                #region 整数
        //                if (item.QueryType == 1)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='int' value='" + val + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                else if (item.QueryType == 2 || item.QueryType == null)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='int' value='" + val + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                else if (item.QueryType == 3)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal' placeholder='起始" + item.NameCn + "' data-datatype='int' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='int' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                #endregion
        //            }
        //            else if (item.xtype == 106)//小数
        //            {
        //                //var val = Querys.GetValue(item.name + "___equal");
        //                //sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                //sbQuery.AppendLine(string.Format("<input type='text' class='form-control' id='{0}___equal' name='{0}___equal'  placeholder='{1}' "
        //                //    + " data-datatype='decimal' value='{2}' />", item.name, item.NameCn, val));
        //                //sbQuery.AppendLine("</li>");
        //                if (item.QueryType == 1)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='decimal' value='" + val + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                else if (item.QueryType == 2 || item.QueryType == null)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='decimal' value='" + val + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                else if (item.QueryType == 3)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='decimal' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='decimal' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //            }
        //            else if (item.xtype == 167)//字符串
        //            {
        //                //var val = Querys.GetValue(item.name + "___like");
        //                //sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                //sbQuery.AppendLine(string.Format("<input type='text' class='form-control' id='{0}___like' name='{0}___like'  placeholder='{1}' "
        //                //    + " data-datatype='string' value='{2}' />", item.name, item.NameCn, val));
        //                //sbQuery.AppendLine("</li>");
        //                if (item.QueryType == 1 || item.QueryType == null)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___like");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='string' value='" + val + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                else if (item.QueryType == 2)
        //                {
        //                    k++;
        //                    var val = Querys.GetValue(item.name + "___equal");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class='form-control' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
        //                        + " data-datatype='string' value='" + val + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //                else if (item.QueryType == 3)
        //                {
        //                    k += 2;
        //                    var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                    var val2 = Querys.GetValue(item.name + "___lessequal");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='string' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                    sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                    sbQuery.AppendLine("<input type='text' class=' form-control ' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='string' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
        //                    sbQuery.AppendLine("</li>");
        //                }
        //            }
        //            else if (item.xtype == 61)//日期类型
        //            {//<input type='text' class=' form-control datepicker ' id='StorageDate___greaterequal' name='StorageDate___greaterequal' value='@Model.Querys.GetValue("StorageDate__greaterequal")' placeholder='入库起始日期' data-datatype='date' data-fieldnamecn='入库起始日期' /></li>
        //                k += 2;
        //                var val1 = Querys.GetValue(item.name + "___greaterequal");
        //                var val2 = Querys.GetValue(item.name + "___lessequal");
        //                sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                sbQuery.AppendLine(string.Format("<input type='text' class=' form-control datepicker ' id='{0}___greaterequal' name='{0}___greaterequal' "
        //                    + " placeholder='起始{1}' data-datatype='date' data-fieldnamecn='起始{1}' value='{2}' />", item.name, item.NameCn, val1));
        //                sbQuery.AppendLine("</li>");
        //                sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
        //                sbQuery.AppendLine(string.Format("<input type='text' class=' form-control datepicker ' id='{0}___lessequal' name='{0}___lessequal' "
        //                    + " placeholder='结束{1}' data-datatype='date' data-fieldnamecn='结束{1}' value='{2}' />", item.name, item.NameCn, val1));
        //                sbQuery.AppendLine("</li>");
        //            }
        //            #endregion
        //            if (i == QueryFields.Count - 1)
        //            {
        //                #region 高级查询结束
        //                //if (Design_Modular.bCustWhere != 1)
        //                //{
        //                sbQuery.AppendLine("<li class='col-sm-4 text-right pull-right'>");
        //                sbQuery.AppendLine(string.Format("<button class='btn btn-primary pull-righ btn-FwSearch'  data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'><span class='glyphicon glyphicon-search'></span>查询</button>", modulars.SearchMethod, modulars.ActionPath, ".targetdom"));
        //                //sbQuery.AppendLine(string.Format("<button class='btn btn-primary           btn-FwSearch'  data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'><span class='glyphicon glyphicon-search'></span>查询</button>", modulars.SearchMethod, modulars.ActionPath, ".targetdom"));
        //                sbQuery.AppendLine("</li>");
        //                sbQuery.AppendLine("   </ul>");
        //                sbQuery.AppendLine("  </div>");
        //                sbQuery.AppendLine("  </div>");
        //                //}
        //                //else
        //                //{
        //                //    sbQuery.AppendLine("   </ul>");
        //                //    sbQuery.AppendLine("  </div>");
        //                //}
        //                #endregion
        //            }
        //        }
        //    }
        //    #endregion

        //    #region 自定义查询条件

        //    //if (Design_Modular.bCustWhere == 1)
        //    //{
        //    //    sbQuery.AppendLine("<div style='width: 100%;'>");
        //    //    sbQuery.AppendLine("   <table class='table table-bordered table-hover table-striped custWhere' id='tabsearch' style='width: 100%;'>");
        //    //    sbQuery.AppendLine("       <thead>");
        //    //    sbQuery.AppendLine("           <tr>");
        //    //    sbQuery.AppendLine("               <th style='width: 80px;'>操作</th>");
        //    //    sbQuery.AppendLine("               <th style='width: 50px;'>与/或</th>");
        //    //    sbQuery.AppendLine("               <th style='width: 100px;'>字段</th>");
        //    //    sbQuery.AppendLine("               <th style='width: 50px;'>运算符</th>");
        //    //    sbQuery.AppendLine("               <th style='width: 100px;'>值</th>");
        //    //    sbQuery.AppendLine("           </tr>");
        //    //    sbQuery.AppendLine("       </thead>");
        //    //    sbQuery.AppendLine("       <tbody>");
        //    //    if (Querys.Count == 0)
        //    //    {
        //    //        Querys.Add(new Query { });
        //    //    }
        //    //    for (var i = 0; i < Querys.Count; i++)
        //    //    {
        //    //        var query = Querys[i];
        //    //        sbQuery.AppendLine("               <tr>");
        //    //        sbQuery.AppendLine("                   <td class='align-left valign-middle '>");
        //    //        sbQuery.AppendLine("                       <a href='javascript:void(0);' class='btn btn-primary btn-xs btn-FwCopy'>复</a>");
        //    //        sbQuery.AppendLine("                       <a href='javascript:void(0);' class='btn btn-primary btn-xs btn-FwDeleteNotHint' style='display: none;'>删</a>");
        //    //        sbQuery.AppendLine("                   </td>");
        //    //        sbQuery.AppendLine("                   <td>");
        //    //        var strdrop = HtmlHelpers.DropDownList(helper, "Querys[{0}].AndOr", ProjectCache.AndOrs, "Value", "Text", query.Oper, "", "=与/或=", "form-control", "style='width:80px;'");
        //    //        sbQuery.AppendLine(strdrop.ToString());
        //    //        sbQuery.AppendLine("                   </td>");
        //    //        sbQuery.AppendLine("                   <td>");
        //    //        //strdrop=HtmlHelpers.DropDownList(helper,"Querys[{0}].FieldName", FieldNames, "Value", "Text",query.FieldName, "", "=字段=","form-control queryfieldname", " data-datatype='date' data-bdic='1'");
        //    //        //    sbQuery.AppendLine(strdrop.ToString());
        //    //        sbQuery.AppendLine("                   </td>");
        //    //        sbQuery.AppendLine("                   <td>");
        //    //        strdrop = HtmlHelpers.DropDownList(helper, "Querys[{0}].Oper", ProjectCache.QueryOpers, "Value", "Text", query.Oper, "", "=运算符=", "form-control", "style='width:90px;'");
        //    //        sbQuery.AppendLine(strdrop.ToString());
        //    //        sbQuery.AppendLine("                   </td>");
        //    //        sbQuery.AppendLine("                   <td class='targetdom align-left'>");
        //    //        sbQuery.AppendLine("                       <input type='text' style='width:100px;' class='form-control' name='Querys[{0}].Value' value='" + @query.Value + "' />");
        //    //        sbQuery.AppendLine("                   </td>");
        //    //        sbQuery.AppendLine("               </tr>");

        //    //    }
        //    //    sbQuery.AppendLine("       </tbody>");
        //    //    sbQuery.AppendLine("   </table>");
        //    //    sbQuery.AppendLine("</div>");
        //    //    sbQuery.AppendLine("<div class='margin-top-5'>");
        //    //    sbQuery.AppendLine("    <button class='btn btn-primary pull-righ btn-FwSearch'><span class='glyphicon glyphicon-search'></span>查询</button>");
        //    //    sbQuery.AppendLine("</div>");
        //    //}
        //    #endregion
        //    MvcHtmlString mstr = new MvcHtmlString(sbQuery.ToString());
        //    return mstr;
        //}

        //#endregion

        //public static MvcHtmlString TableLiquidHtml(this HtmlHelper helper, MyResponseBase model)// IEnumerable<object> Items, List<RankInfo> RankInfos)
        //{
        //    var Items = model.Items;
        //    //var RankInfos = model.PageQueryBase.RankInfos;
        //    var ModularOrFunCode = model.ModularOrFunCode;

        //    #region 获取相关数据:已抽象
        //    //var ModularOrFunCode = model.ModularOrFunCode;// "AuthorizationAreas.De_Member";
        //    //(1)模块对象
        //    var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
        //    if (Design_ModularOrFun.GroupModularOrFun == 3)
        //        Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).First();
        //    //(2)行的DataXX属性：功能、按钮的类型为11=>页面=>字段       "AuthorizationAreas.De_Member"
        //    //(3)模块行按钮
        //    var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == ModularOrFunCode && (p.OperPos == 2)).OrderBy(p => p.Sort).ToList();

        //    //(4)模块列表页的Table字段信息
        //    var Fields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
        //    //显示字段
        //    var TableDispInfos = Fields.Where(p => p.FieldFunTypeID == null || ((((int)p.FieldFunTypeID) & 1) == 1)).ToList();// ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
        //    //thead-data
        //    var thead_datas = Fields.Where(p => p.FieldFunTypeID != null && (((int)p.FieldFunTypeID) & 2) == 2);

        //    //tbody-data
        //    var tbody_datas = Fields.Where(p => p.FieldFunTypeID != null && (((int)p.FieldFunTypeID) & 4) == 4);

        //    //tr-data
        //    var tr_datas = thead_datas.Union(tbody_datas);

        //    #endregion

        //    StringBuilder sbHtml = new StringBuilder();

        //    #region 表头--无

        //    //StringBuilder sbHead = new StringBuilder("<thead ");

        //    //#region (1)Thead的data-XXX中文提示
        //    ////var count = Items.Count();
        //    //foreach (var theaddata in thead_datas)
        //    //{
        //    //    var datalower = theaddata.name.ToLower();
        //    //    sbHead.Append(string.Format(" data-{0}='{1}' ", datalower, theaddata.NameCn));
        //    //}
        //    //sbHead.Append(">");
        //    #endregion

        //    #region (2)显示合计行 -无
        //    //if (posTotal == 0)//开始处
        //    //{
        //    //    if (count > 0)
        //    //    {
        //    //        var item = Items.Last();//[count - 1];
        //    //        Type type = item.GetType();
        //    //        var strhead = WriteRowThTotalHtml(TableHeadInfos, item, type);
        //    //        sbHead.AppendLine(strhead);
        //    //    }
        //    //}
        //    //BCalCol
        //    //if (Design_ModularOrFun.BCalCol == 1)
        //    //{
        //    //    var item = Items[0];
        //    //    Type type = item.GetType();
        //    //    var tds = WriteRowThTotalHtml(TableDispInfos, tr_datas.ToList(), item, 0, type, Design_ModularOrFun, btns);
        //    //    //var tds = WriteRowTdHtml(TableDispInfos, theadDatas, item, x, type, Design_ModularOrFun,btns);
        //    //    sbHead.AppendLine(tds);
        //    //}
        //    #endregion

        //    #region (3)表头

        //    //写入表头
        //    //var theads = WriteHead(TableDispInfos, helper, RankInfos);
        //    //sbHead.AppendLine(theads);
        //    //sbHead.AppendLine("</thead>");
        //    ////sbHtml.AppendLine(ths.ToString());
        //    //sbHtml.AppendLine(sbHead.ToString());
        //    #endregion

        //    //#endregion

        //    #region 主体

        //    #region tbody的data-XXX：由字段为HeadOrDataType确定

        //    StringBuilder sbBody = new StringBuilder("<tbody  ");
        //    //foreach (var item in tbody_datas)
        //    //{
        //    //    var datalower = item.name.ToLower();
        //    //    var NameEn = item.name;
        //    //    //if (BodyDataXXInfos[i].bOperLog == 1)
        //    //    //    NameEn = "OperLogIdent";
        //    //    sbBody.Append(string.Format(" data-{0}='{1}' ", datalower, NameEn));
        //    //}
        //    sbBody.Append(">");

        //    #endregion

        //    #region 所有列计算:用于删除行时计算列名

        //    //var calcols = "";
        //    //for (var j = 0; j < TableDispInfos.Count; j++)
        //    //{
        //    //    var field = TableDispInfos[j];
        //    //    if (!string.IsNullOrEmpty(field.calcol))
        //    //    {
        //    //        calcols += field.calcol + ",";// string.Format("data-calcol='{0}'", field.calcol);
        //    //    }
        //    //}
        //    //if (calcols.Length > 0)
        //    //    calcols = calcols.Substring(0, calcols.Length - 1);
        //    #endregion

        //    int x = 0;
        //    int row = 0;
        //    var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == model.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
        //    //@foreach (var item in Model.Items)
        //    //{
        //    //    <tr>
        //    //        <td>
        //    //            <div class="container-fluid">
        //    //                <div class='form-horizontal merge '>
        //    //                    <ul class="form-horizontal merge">
        //    //                        @Html.FormEleHtml(Model)
        //    //                    </ul>
        //    //                </div>
        //    //            </div>
        //    //        </td>
        //    //    </tr>
        //    //}

        //    foreach (var item in Items)
        //    {
        //        sbBody.AppendLine("<tr>");
        //        sbBody.AppendLine("    <td>");
        //        sbBody.AppendLine("        <div class='container-fluid'>");
        //        sbBody.AppendLine("            <div class='form-horizontal merge '>");
        //        sbBody.AppendLine("                <ul class='form-horizontal merge'>");
        //        sbBody.AppendLine("");
        //        //Type type = item.GetType();
        //        var struls = FormEleHtmlTable(helper, item, pagefields);
        //        //var tds = WriteRowTdHtml(TableDispInfos, tr_datas.ToList(), item, row, type, Design_ModularOrFun, btns, calcols);
        //        //var tds = WriteRowTdHtml(TableDispInfos, theadDatas, item, x, type, Design_ModularOrFun,btns);
        //        sbBody.AppendLine(struls.ToString());
        //        sbBody.AppendLine("                </ul>");
        //        sbBody.AppendLine("            </div>");
        //        sbBody.AppendLine("        </div>");
        //        sbBody.AppendLine("    </td>");
        //        sbBody.AppendLine("</tr>");
        //        x++;
        //        row++;
        //    }
        //    sbBody.AppendLine("</tbody>");
        //    sbHtml.AppendLine(sbBody.ToString());

        //    #endregion

        //    MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
        //    return mstr;
        //}

        public static MvcHtmlString TableHtml1(this HtmlHelper helper, MyResponseBase model)
        {
            var Items = model.Items;
            var str = TableHtml(helper, Items, model.ItemTotal, model.PageQueryBase.RankInfos, model.ModularOrFunCode);
            return str;
        }

        #region Table
        public static MvcHtmlString TableHtml(this HtmlHelper helper, IEnumerable Items1, object ItemTotal, List<RankInfo> RankInfos,
            string ModularOrFunCode)
        {
            //var Items = model.Items;
            //var RankInfos = model.PageQueryBase.RankInfos;
            //var ModularOrFunCode = model.ModularOrFunCode;
            var BSort = 0;
            var Items = Items1.Cast<object>();

            #region 获取相关数据:已抽象
            var conts = helper.ViewContext.Controller as BaseController;
            var Design_ModularOrFun = conts.Design_ModularOrFun;// ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();

            if (!string.IsNullOrEmpty(Design_ModularOrFun.SortCol))
                BSort = 1;

            //(2)行的DataXX属性：功能、按钮的类型为11=>页面=>字段       "AuthorizationAreas.De_Member"
            //(3)模块行按钮
            var btns = conts.LoginModulerBtns(Design_ModularOrFun.ModularOrFunCode, 2);
            //var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode && (p.OperPos == 2)).OrderBy(p => p.Sort).ToList();
            //btns = btns.Where(p => p.bValid ==1).ToList();
            //(4)模块列表页的Table字段信息

            var Fields = PageFormEleTypes(Design_ModularOrFun);
            //p.PageFormEleSort = p.Page01FormEleSort; p.PageFormElePos = p.Page01FormElePos;

            //td数据
            var TableDispInfos = new List<SoftProjectAreaEntity>();
            try
            {
                TableDispInfos = Fields.Where(p => (((int)p.PageFormElePos) & 1) == 1)
                    .OrderBy(p => p.PageFormEleSort).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("页面字段配置：有问题");
            }
            var HiddelTableDispInfos = TableDispInfos.Where(p => p.FormEleType == 8).ToList();
            var noHiddelTableDispInfos = TableDispInfos.Where(p => p.FormEleType != 8).ToList();

            //查询位置为thead-data字段
            var thead_datas = Fields.Where(p => (((int)p.PageFormElePos) & 2) == 2);

            //查询位置为tbody-data字段
            var tbody_datas = Fields.Where(p => (((int)p.PageFormElePos) & 4) == 4);

            //合并行字段：tr-data
            var tr_datas = thead_datas.Union(tbody_datas);

            #endregion

            #region 所有列计算:用于删除行时计算列名

            var calcols = "";
            var kkkkxxx = TableDispInfos.Where(p => !string.IsNullOrEmpty(p.calcol)).ToList();

            for (var j = 0; j < kkkkxxx.Count; j++)
            {
                var field = kkkkxxx[j];
                calcols += field.calcol + ",";
            }
            if (calcols.Length > 0)
                calcols = calcols.Substring(0, calcols.Length - 1);

            #endregion

            StringBuilder sbHtml = new StringBuilder();

            #region 表头

            StringBuilder sbHead = new StringBuilder("<thead ");

            #region (1)Thead的data-XXX中文提示
            //var count = Items.Count();
            foreach (var theaddata in thead_datas)
            {
                var datalower = theaddata.name.ToLower();
                sbHead.Append(string.Format(" data-{0}='{1}' ", datalower, theaddata.NameCn));
            }
            sbHead.Append(">");
            #endregion

            #region (2)显示合计行
            if (Design_ModularOrFun.BCalCol == 1)
            {
                var item = ItemTotal;// Items.First();//[0];
                Type type = item.GetType();
                var tds = WriteRowThTotalHtml(noHiddelTableDispInfos, ItemTotal, type);
                //var tds = WriteRowTdHtml(TableDispInfos, theadDatas, item, x, type, Design_ModularOrFun,btns);
                sbHead.AppendLine(tds);
            }
            #endregion

            #region (3)表头

            //写入表头
            var theads = WriteHead(noHiddelTableDispInfos, helper, RankInfos, BSort);
            sbHead.AppendLine(theads);
            sbHead.AppendLine("</thead>");
            //sbHtml.AppendLine(ths.ToString());
            sbHtml.AppendLine(sbHead.ToString());
            #endregion

            #endregion

            #region 主体

            #region tbody的data-XXX：由字段为HeadOrDataType确定

            StringBuilder sbBody = new StringBuilder("<tbody  ");
            foreach (var item in tbody_datas)
            {
                var datalower = item.name.ToLower();
                var NameEn = item.name;
                //if (BodyDataXXInfos[i].bOperLog == 1)
                //    NameEn = "OperLogIdent";
                sbBody.Append(string.Format(" data-{0}='{1}' ", datalower, NameEn));
            }
            sbBody.Append(">");

            #endregion

            int x = 0;
            int row = 0;
            foreach (var item in Items)
            {
                Type type = item.GetType();
                var tds = WriteRowTdHtml(helper, noHiddelTableDispInfos, HiddelTableDispInfos, tr_datas.ToList(), item, row, type, Design_ModularOrFun, btns, calcols);
                //var tds = WriteRowTdHtml(TableDispInfos, theadDatas, item, x, type, Design_ModularOrFun,btns);
                sbBody.AppendLine(tds);
                //if (posTotal == 0 && x == count - 2)
                //    break;
                x++;
                row++;
            }
            sbBody.AppendLine("</tbody>");
            sbHtml.AppendLine(sbBody.ToString());

            #endregion

            MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            return mstr;
        }

        #endregion

        ///// <summary>
        ///// 表头：合计行
        ///// </summary>
        ///// <param name="TableHeadInfos"></param>
        ///// <param name="item"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //private static string WriteRowThTotalHtml(List<Sys_PageFunTableAreaEntity> TableHeadInfos, object item, Type type)
        //{
        //    StringBuilder strhtml = new StringBuilder("<tr>");
        //    //strhtml.AppendLine(string.Format("<th class='lockhead' ></th>"));//序号列

        //    //var tabheads=
        //    for (var j = 0; j < TableHeadInfos.Count; j++)
        //    {
        //        //#region 表头标题
        //        //headTiele = TableHeadInfos[j].NameCn;
        //        //if (TableHeadInfos[j].Sys_DynReportDefineDetailID == 2)//复选框
        //        //{
        //        //    headTiele = "<input type='checkbox' class='checkbox1 jq-checkall-switch' alt='全选/反选' title='全选/反选' />";
        //        //}
        //        //else if (TableHeadInfos[j].NameCnType == 2)
        //        //{
        //        //    headTiele = TableHeadInfos[j].NameCn2;
        //        //    strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}<th>", TableHeadInfos[j].Width, TableHeadInfos[j].NameCn));
        //        //}
        //        //#endregion
        //        var val = "";
        //        if (!string.IsNullOrEmpty(TableHeadInfos[j].NameEn))
        //        {
        //            PropertyInfo property = type.GetProperty(TableHeadInfos[j].NameEn);
        //            object value = property.GetValue(item, null);
        //            if (value != null)
        //            {
        //                var strval = value.ToString();
        //                if (TableHeadInfos[j].DataType == 5)//货币类型
        //                {
        //                    val = strval.ToDecimalNull().MoneyNum();
        //                }
        //                else if (TableHeadInfos[j].DataType == 4)//日期类型
        //                {
        //                    val = strval.ToDateNull().Format_yyyy_MM_dd();
        //                }
        //                else
        //                    val = strval;
        //            }
        //        }
        //        strhtml.Append(string.Format("<th class='lockhead'>{0}</th>", val));
        //    }
        //    strhtml.Append("</tr>");
        //    return strhtml.ToString();
        //}

        ///// <summary>
        ///// 单行Html
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static MvcHtmlString TableRowHtml(this HtmlHelper helper, MyResponseBase model)
        //{
        //    //model.Items.Add(model.Item);
        //    //var rowhtml = TableRows(helper, model);
        //    //MvcHtmlString mstr = new MvcHtmlString(rowhtml);
        //    return null;// mstr;
        //}

        /// <summary>
        /// 行Html
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MvcHtmlString TableRowsHtml(this HtmlHelper helper, IEnumerable Items1)// MyResponseBase model)
        {
            //model.Items.Add(model.Item);
            var rowhtml = TableRows(helper, Items1);
            MvcHtmlString mstr = new MvcHtmlString(rowhtml);
            return mstr;
        }


        /// <summary>
        /// 查询页面，表格显示字段
        /// </summary>
        /// <param name="Design_ModularOrFun"></param>
        public static string GetPageSaveEleTypes(SoftProjectAreaEntity Design_ModularOrFun)
        {
            var SaveFields = PageFormEleTypes(Design_ModularOrFun);
            SaveFields = SaveFields.Where(p => p.bPrimaryKeyOrFK != 1 & (p.FormEleType == 1 || p.FormEleType == 8 || p.FormEleType == 4 || p.FormEleType == 10 || p.FormEleType == 64 || p.FormEleType == 128 || p.FormEleType == 512 || p.FormEleType == 32768)).ToList();

            var strFields = "";
            if (SaveFields.Count > 0)
                SaveFields.ForEach(p => strFields += p.name + ",");
            if (strFields.Length > 0)
                strFields = strFields.Substring(0, strFields.Length - 1);
            return strFields;
        }


        /// <summary>
        /// 查询页面，表格显示字段
        /// </summary>
        /// <param name="Design_ModularOrFun"></param>
        public static List<SoftProjectAreaEntity> PageFormEleTypes(SoftProjectAreaEntity Design_ModularOrFun)
        {
            List<SoftProjectAreaEntity> Fields = new List<SoftProjectAreaEntity>();

            var Design_ModularOrFunTemp = Design_ModularOrFun;
            if (Design_ModularOrFun.GroupModularOrFun == 3)
                Design_ModularOrFunTemp = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).First();

            switch (Design_ModularOrFun.PageFormEleTypeName)
            {
                case "Page01FormEleType"://Page01FormEleSort Page01FormElePos Page01FormEleType
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page01FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page01FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page01FormEleSort; p.PageFormElePos = p.Page01FormElePos; p.FormEleType = p.Page01FormEleType; });
                    break;
                case "Page02FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page02FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page02FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page02FormEleSort; p.PageFormElePos = p.Page02FormElePos; p.FormEleType = p.Page02FormEleType; });
                    break;
                case "Page03FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page03FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page03FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page03FormEleSort; p.PageFormElePos = p.Page03FormElePos; p.FormEleType = p.Page03FormEleType; });
                    break;
                case "Page04FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page04FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page04FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page04FormEleSort; p.PageFormElePos = p.Page04FormElePos; p.FormEleType = p.Page04FormEleType; });
                    break;
                case "Page05FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page05FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page05FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page05FormEleSort; p.PageFormElePos = p.Page05FormElePos; p.FormEleType = p.Page05FormEleType; });
                    break;
                case "Page06FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page06FormEleType != null).ToList();
                    else //if(Design_ModularOrFun.PageType == 6)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page06FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page06FormEleSort; p.PageFormElePos = p.Page06FormElePos; p.FormEleType = p.Page06FormEleType; });
                    break;
                case "Page07FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page07FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page07FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page07FormEleSort; p.PageFormElePos = p.Page07FormElePos; p.FormEleType = p.Page07FormEleType; });
                    break;
                case "Page08FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page08FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page08FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page08FormEleSort; p.PageFormElePos = p.Page08FormElePos; p.FormEleType = p.Page08FormEleType; });
                    break;
                case "Page09FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page09FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page09FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page09FormEleSort; p.PageFormElePos = p.Page09FormElePos; p.FormEleType = p.Page09FormEleType; });
                    break;
                case "Page10FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page10FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page10FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page10FormEleSort; p.PageFormElePos = p.Page10FormElePos; p.FormEleType = p.Page10FormEleType; });
                    break;
                case "Page11FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page11FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page11FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page11FormEleSort; p.PageFormElePos = p.Page11FormElePos; p.FormEleType = p.Page11FormEleType; });
                    break;
                case "Page12FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page12FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page12FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page12FormEleSort; p.PageFormElePos = p.Page12FormElePos; p.FormEleType = p.Page12FormEleType; });
                    break;
                case "Page13FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page13FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page13FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page13FormEleSort; p.PageFormElePos = p.Page13FormElePos; p.FormEleType = p.Page13FormEleType; });
                    break;
                case "Page14FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page14FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page14FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page14FormEleSort; p.PageFormElePos = p.Page14FormElePos; p.FormEleType = p.Page14FormEleType; });
                    break;
                case "Page15FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page15FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page15FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page15FormEleSort; p.PageFormElePos = p.Page15FormElePos; p.FormEleType = p.Page15FormEleType; });
                    break;
                case "Page16FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page16FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page16FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page16FormEleSort; p.PageFormElePos = p.Page16FormElePos; p.FormEleType = p.Page16FormEleType; });
                    break;
                case "Page17FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page17FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page17FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page17FormEleSort; p.PageFormElePos = p.Page17FormElePos; p.FormEleType = p.Page17FormEleType; });
                    break;
                case "Page18FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page18FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page18FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page18FormEleSort; p.PageFormElePos = p.Page18FormElePos; p.FormEleType = p.Page18FormEleType; });
                    break;
                case "Page19FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page19FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page19FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page19FormEleSort; p.PageFormElePos = p.Page19FormElePos; p.FormEleType = p.Page19FormEleType; });
                    break;
                case "Page20FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page20FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page20FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page20FormEleSort; p.PageFormElePos = p.Page20FormElePos; p.FormEleType = p.Page20FormEleType; });
                    break;
                case "Page21FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page21FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page21FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page21FormEleSort; p.PageFormElePos = p.Page21FormElePos; p.FormEleType = p.Page21FormEleType; });
                    break;
                case "Page22FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page22FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page22FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page22FormEleSort; p.PageFormElePos = p.Page22FormElePos; p.FormEleType = p.Page22FormEleType; });
                    break;
                case "Page23FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page23FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page23FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page23FormEleSort; p.PageFormElePos = p.Page23FormElePos; p.FormEleType = p.Page23FormEleType; });
                    break;
                case "Page24FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page24FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page24FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page24FormEleSort; p.PageFormElePos = p.Page24FormElePos; p.FormEleType = p.Page24FormEleType; });
                    break;
                case "Page25FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page25FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page25FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page25FormEleSort; p.PageFormElePos = p.Page25FormElePos; p.FormEleType = p.Page25FormEleType; });
                    break;
                case "Page26FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page26FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page26FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page26FormEleSort; p.PageFormElePos = p.Page26FormElePos; p.FormEleType = p.Page26FormEleType; });
                    break;
                case "Page27FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page27FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page27FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page27FormEleSort; p.PageFormElePos = p.Page27FormElePos; p.FormEleType = p.Page27FormEleType; });
                    break;
                case "Page28FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page28FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page28FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page28FormEleSort; p.PageFormElePos = p.Page28FormElePos; p.FormEleType = p.Page28FormEleType; });
                    break;
                case "Page29FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page29FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page29FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page29FormEleSort; p.PageFormElePos = p.Page29FormElePos; p.FormEleType = p.Page29FormEleType; });
                    break;
                case "Page30FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page30FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page30FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page30FormEleSort; p.PageFormElePos = p.Page30FormElePos; p.FormEleType = p.Page30FormEleType; });
                    break;
                case "Page31FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page31FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page31FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page31FormEleSort; p.PageFormElePos = p.Page31FormElePos; p.FormEleType = p.Page31FormEleType; });
                    break;
                case "Page32FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page32FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page32FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page32FormEleSort; p.PageFormElePos = p.Page32FormElePos; p.FormEleType = p.Page32FormEleType; });
                    break;
                case "Page33FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page33FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page33FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page33FormEleSort; p.PageFormElePos = p.Page33FormElePos; p.FormEleType = p.Page33FormEleType; });
                    break;
                case "Page34FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page34FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page34FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page34FormEleSort; p.PageFormElePos = p.Page34FormElePos; p.FormEleType = p.Page34FormEleType; });
                    break;
                case "Page35FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page35FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page35FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page35FormEleSort; p.PageFormElePos = p.Page35FormElePos; p.FormEleType = p.Page35FormEleType; });
                    break;
                case "Page36FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page36FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page36FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page36FormEleSort; p.PageFormElePos = p.Page36FormElePos; p.FormEleType = p.Page36FormEleType; });
                    break;
                case "Page37FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page37FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page37FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page37FormEleSort; p.PageFormElePos = p.Page37FormElePos; p.FormEleType = p.Page37FormEleType; });
                    break;
                case "Page38FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page38FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page38FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page38FormEleSort; p.PageFormElePos = p.Page38FormElePos; p.FormEleType = p.Page38FormEleType; });
                    break;
                case "Page39FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page39FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page39FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page39FormEleSort; p.PageFormElePos = p.Page39FormElePos; p.FormEleType = p.Page39FormEleType; });
                    break;
                case "Page40FormEleType":
                    if (Design_ModularOrFun.PageType == 2)
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page40FormEleType != null).ToList();
                    else
                        Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page40FormEleSort != null).ToList();

                    Fields.ForEach(p => { p.PageFormEleSort = p.Page40FormEleSort; p.PageFormElePos = p.Page40FormElePos; p.FormEleType = p.Page40FormEleType; });
                    break;
            }

            return Fields;
        }

        /// <summary>
        /// 查询页面，表格显示字段
        /// </summary>
        /// <param name="Design_ModularOrFun"></param>
        public static List<SoftProjectAreaEntity> QueryFormEleTypes(SoftProjectAreaEntity Design_ModularOrFun)
        {
            List<SoftProjectAreaEntity> QueryFields = new List<SoftProjectAreaEntity>();
            var Design_ModularOrFunTemp = Design_ModularOrFun;// new SoftProjectAreaEntity();
            if (Design_ModularOrFun.GroupModularOrFun == 3)
                Design_ModularOrFunTemp = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).First();

            switch (Design_ModularOrFun.QueryFormEleTypeName)
            {
                case "Query01":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query01FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query01Pos; p.FormEleType = p.Query01FormEleType; p.QueryType = p.Query01QueryType; });
                    break;
                case "Query02":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query02FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query02Pos; p.FormEleType = p.Query02FormEleType; p.QueryType = p.Query02QueryType; });
                    break;
                case "Query03":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query03FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query03Pos; p.FormEleType = p.Query03FormEleType; p.QueryType = p.Query03QueryType; });
                    break;
                case "Query04":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query04FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query04Pos; p.FormEleType = p.Query04FormEleType; p.QueryType = p.Query04QueryType; });
                    break;
                case "Query05":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query05FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query05Pos; p.FormEleType = p.Query05FormEleType; p.QueryType = p.Query05QueryType; });
                    break;
                case "Query06":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query06FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query06Pos; p.FormEleType = p.Query06FormEleType; p.QueryType = p.Query06QueryType; });
                    break;
                case "Query07":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query07FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query07Pos; p.FormEleType = p.Query07FormEleType; p.QueryType = p.Query07QueryType; });
                    break;
                case "Query08":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query08FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query08Pos; p.FormEleType = p.Query08FormEleType; p.QueryType = p.Query08QueryType; });
                    break;
                case "Query09":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query09FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query09Pos; p.FormEleType = p.Query09FormEleType; p.QueryType = p.Query09QueryType; });
                    break;
                case "Query10":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query10FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query10Pos; p.FormEleType = p.Query10FormEleType; p.QueryType = p.Query10QueryType; });
                    break;
                case "Query11":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query11FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query11Pos; p.FormEleType = p.Query11FormEleType; p.QueryType = p.Query11QueryType; });
                    break;
                case "Query12":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query12FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query12Pos; p.FormEleType = p.Query12FormEleType; p.QueryType = p.Query12QueryType; });
                    break;
                case "Query13":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query13FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query13Pos; p.FormEleType = p.Query13FormEleType; p.QueryType = p.Query13QueryType; });
                    break;
                case "Query14":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query14FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query14Pos; p.FormEleType = p.Query14FormEleType; p.QueryType = p.Query14QueryType; });
                    break;
                case "Query15":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query15FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query15Pos; p.FormEleType = p.Query15FormEleType; p.QueryType = p.Query15QueryType; });
                    break;
                case "Query16":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query16FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query16Pos; p.FormEleType = p.Query16FormEleType; p.QueryType = p.Query16QueryType; });
                    break;
                case "Query17":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query17FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query17Pos; p.FormEleType = p.Query17FormEleType; p.QueryType = p.Query17QueryType; });
                    break;
                case "Query18":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query18FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query18Pos; p.FormEleType = p.Query18FormEleType; p.QueryType = p.Query18QueryType; });
                    break;
                case "Query19":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query19FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query19Pos; p.FormEleType = p.Query19FormEleType; p.QueryType = p.Query19QueryType; });
                    break;
                case "Query20":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query20FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query20Pos; p.FormEleType = p.Query20FormEleType; p.QueryType = p.Query20QueryType; });
                    break;
                case "Query21":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query21FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query21Pos; p.FormEleType = p.Query21FormEleType; p.QueryType = p.Query21QueryType; });
                    break;
                case "Query22":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query22FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query22Pos; p.FormEleType = p.Query22FormEleType; p.QueryType = p.Query22QueryType; });
                    break;
                case "Query23":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query23FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query23Pos; p.FormEleType = p.Query23FormEleType; p.QueryType = p.Query23QueryType; });
                    break;
                case "Query24":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query24FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query24Pos; p.FormEleType = p.Query24FormEleType; p.QueryType = p.Query24QueryType; });
                    break;
                case "Query25":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query25FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query25Pos; p.FormEleType = p.Query25FormEleType; p.QueryType = p.Query25QueryType; });
                    break;
                case "Query26":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query26FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query26Pos; p.FormEleType = p.Query26FormEleType; p.QueryType = p.Query26QueryType; });
                    break;
                case "Query27":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query27FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query27Pos; p.FormEleType = p.Query27FormEleType; p.QueryType = p.Query27QueryType; });
                    break;
                case "Query28":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query28FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query28Pos; p.FormEleType = p.Query28FormEleType; p.QueryType = p.Query28QueryType; });
                    break;
                case "Query29":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query29FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query29Pos; p.FormEleType = p.Query29FormEleType; p.QueryType = p.Query29QueryType; });
                    break;
                case "Query30":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query30FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query30Pos; p.FormEleType = p.Query30FormEleType; p.QueryType = p.Query30QueryType; });
                    break;
                case "Query31":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query31FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query31Pos; p.FormEleType = p.Query31FormEleType; p.QueryType = p.Query31QueryType; });
                    break;
                case "Query32":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query32FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query32Pos; p.FormEleType = p.Query32FormEleType; p.QueryType = p.Query32QueryType; });
                    break;
                case "Query33":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query33FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query33Pos; p.FormEleType = p.Query33FormEleType; p.QueryType = p.Query33QueryType; });
                    break;
                case "Query34":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query34FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query34Pos; p.FormEleType = p.Query34FormEleType; p.QueryType = p.Query34QueryType; });
                    break;
                case "Query35":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query35FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query35Pos; p.FormEleType = p.Query35FormEleType; p.QueryType = p.Query35QueryType; });
                    break;
                case "Query36":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query36FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query36Pos; p.FormEleType = p.Query36FormEleType; p.QueryType = p.Query36QueryType; });
                    break;
                case "Query37":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query37FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query37Pos; p.FormEleType = p.Query37FormEleType; p.QueryType = p.Query37QueryType; });
                    break;
                case "Query38":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query38FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query38Pos; p.FormEleType = p.Query38FormEleType; p.QueryType = p.Query38QueryType; });
                    break;
                case "Query39":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query39FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query39Pos; p.FormEleType = p.Query39FormEleType; p.QueryType = p.Query39QueryType; });
                    break;
                case "Query40":
                    QueryFields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Query40FormEleType != null).ToList();
                    QueryFields.ForEach(p => { p.QueryPos = p.Query40Pos; p.FormEleType = p.Query40FormEleType; p.QueryType = p.Query40QueryType; });
                    break;

            }
            return QueryFields;
        }

        public static string TableRows(this HtmlHelper helper, IEnumerable Items)// MyResponseBase model)
        {
            #region 获取相关控制数据

            //(1)模块对象
            var conts = helper.ViewContext.Controller as BaseController;
            var Design_ModularOrFun = conts.Design_ModularOrFun;

            //(3)模块行按钮
            var btns = conts.LoginModulerBtns(Design_ModularOrFun.ModularOrFunCode, 2);

            var Fields = PageFormEleTypes(Design_ModularOrFun);
            //p.PageFormEleSort = p.Page01FormEleSort; p.PageFormElePos = p.Page01FormElePos;
            var TableDispInfos = Fields.Where(p => (((int)p.PageFormElePos) & 1) == 1).OrderBy(p => p.PageFormEleSort).ToList();// 

            var HiddelTableDispInfos = TableDispInfos.Where(p => p.FormEleType == 8).ToList();
            var noHiddelTableDispInfos = TableDispInfos.Where(p => p.FormEleType != 8).ToList();

            //thead-data
            var thead_datas = Fields.Where(p => p.PageFormElePos != null && (((int)p.PageFormElePos) & 2) == 2);

            //tbody-data
            var tbody_datas = Fields.Where(p => p.PageFormElePos != null && (((int)p.PageFormElePos) & 4) == 4);

            //tr-data
            var tr_datas = thead_datas.Union(tbody_datas);

            #endregion

            #region 所有列计算:用于删除行时计算列名

            var calcols = "";
            var kkkkxxx = TableDispInfos.Where(p => !string.IsNullOrEmpty(p.calcol)).ToList();

            for (var j = 0; j < kkkkxxx.Count; j++)
            {
                var field = kkkkxxx[j];
                calcols += field.calcol + ",";
            }
            if (calcols.Length > 0)
                calcols = calcols.Substring(0, calcols.Length - 1);

            #endregion

            #region 主体

            StringBuilder sbRows = new StringBuilder();

            int x = 0;
            foreach (var item in Items)
            {
                Type type = item.GetType();
                var tds = WriteRowTdHtml(helper, noHiddelTableDispInfos, HiddelTableDispInfos, tr_datas.ToList(), item, x, type, Design_ModularOrFun, btns, calcols);
                sbRows.AppendLine(tds);
                x++;
            }

            #endregion

            return sbRows.ToString();
        }

        #region 表格
        public static string WriteHead(List<SoftProjectAreaEntity> TableHeadInfos, HtmlHelper helper, List<RankInfo> RankInfos, int? BSort = null)
        {
            StringBuilder strhtml = new StringBuilder("<tr>");
            //序号列：宽度
            //操作列：宽度
            int? width = null;
            string headTiele = null;
            string strOrderBy = null;
            for (var j = 0; j < TableHeadInfos.Count; j++)
            {
                strOrderBy = null;
                #region 宽度
                width = TableHeadInfos[j].HeadWidth;
                if (width == null)
                    width = TableHeadInfos[j].Width;
                #endregion
                #region 表头标题
                headTiele = TableHeadInfos[j].NameCn;
                if (TableHeadInfos[j].Design_ModularFieldID == 2)//复选框
                {
                    headTiele = "<input type='checkbox' class='checkbox1 jq-checkall-switch' alt='全选/反选' title='全选/反选' />";
                }
                else if (TableHeadInfos[j].NameCnType == 2)
                {
                    headTiele = TableHeadInfos[j].NameCn2;
                    strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}<th>", TableHeadInfos[j].Width, TableHeadInfos[j].NameCn));
                }
                #endregion
                if (TableHeadInfos[j].Design_ModularFieldID > 100 && BSort != 0)
                {
                    strOrderBy = HtmlHelpers.OrderBy(RankInfos, TableHeadInfos[j].name);
                }
                if (width != null)
                    strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}{2}</th>", width, headTiele, strOrderBy));
                else
                    strhtml.Append(string.Format("<th class='lockhead' >{0} {1}</th>", headTiele, strOrderBy));
            }
            strhtml.Append("</tr>");
            return strhtml.ToString();
        }

        /// <summary>
        /// 写行表头：总计
        /// </summary>
        /// <param name="func"></param>
        /// <param name="conts"></param>
        /// <param name="TableDispInfos">显示的列</param>
        /// <param name="tr_datas">每行tr的Data-XXX属性数据</param>
        /// <param name="ItemTotal"></param>
        /// <param name="row"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string WriteRowThTotalHtml(List<SoftProjectAreaEntity> TableDispInfos, object ItemTotal, Type type)
        {
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == "AuthorizationAreas.De_Member").FirstOrDefault();
            //var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == "AuthorizationAreas.De_Member" && (p.OperPos == OperPos)).OrderBy(p => p.Sort).ToList();

            StringBuilder strhtml = new StringBuilder("<tr>");

            var val = "";
            for (var j = 0; j < TableDispInfos.Count; j++)
            {
                //return val;
                val = "";
                if (TableDispInfos[j].Design_ModularFieldID == 1 || TableDispInfos[j].Design_ModularFieldID == 2 || TableDispInfos[j].Design_ModularFieldID == 3)
                    val = "";
                else
                    val = GetHtmlVal(TableDispInfos, ItemTotal, type, j);
                if (val != "")
                {
                    strhtml.Append(string.Format("<td class='lockhead'><lable class='{0}Total'>{1}</lable></td>", TableDispInfos[j].name, val));
                }
                else
                    strhtml.Append(string.Format("<td class='lockhead'>{0}</td>", val));
            }
            strhtml.Append("</tr>");
            return strhtml.ToString();
        }

        /// <summary>
        /// 写行
        /// </summary>
        /// <param name="func"></param>
        /// <param name="conts"></param>
        /// <param name="TableDispInfos">显示的列</param>
        /// <param name="tr_datas">每行tr的Data-XXX属性数据</param>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string WriteRowTdHtml(this HtmlHelper helper, List<SoftProjectAreaEntity> TableDispInfosKKK,
            List<SoftProjectAreaEntity> HiddelTableDispInfos,
            List<SoftProjectAreaEntity> tr_datas, object item, int row, Type type, SoftProjectAreaEntity Design_ModularOrFun,
            List<SoftProjectAreaEntity> btns, string calcols)
        {
            StringBuilder strhtml = new StringBuilder("<tr");

            #region 生成tr的data-XXX属性
            var datas = new List<SoftProjectAreaEntity>();
            for (var i = 0; i < tr_datas.Count; i++)
            {
                var datalower = tr_datas[i].name.ToLower();
                var valdata = GetHtmlVal(tr_datas, item, type, i);
                strhtml.AppendLine(string.Format(" data-{0}='{1}' ", datalower, valdata));
            }
            strhtml.Append(">");
            #endregion

            var val = "";
            var align = "";
            //拆分为隐藏字段、非隐藏字段
            //var HiddelTableDispInfos = TableDispInfos.Where(p => p.FormEleType==8).ToList();
            //var noHiddelTableDispInfos = TableDispInfos.Where(p => p.FormEleType != 8).ToList();
            //PageFormEleSort
            var xxx = TableDispInfosKKK.Select(p => p.PageFormEleSort).Distinct().OrderBy(p => p).ToList();

            for (var z = 0; z < xxx.Count; z++)
            {
                align = "";
                //return val;
                var L = xxx[z];
                val = "";
                var TableDispInfos = TableDispInfosKKK.Where(p => p.PageFormEleSort == L).ToList();
                #region 单列
                for (var j = 0; j < TableDispInfos.Count; j++)
                {
                    if (j != 0)
                    {
                        if (TableDispInfos[j].AdditionalInfo != null)
                            val += TableDispInfos[j].AdditionalInfo;// "AdditionalInfo";
                    }
                    if (TableDispInfos[j].Design_ModularFieldID == 1)//序号列
                    {
                        val += (row + 1).ToString();
                        //隐藏字段
                        for (var m = 0; m < HiddelTableDispInfos.Count; m++)
                        {
                            var valtemp = GetHtmlVal(HiddelTableDispInfos, item, type, m);
                            var fieldhiddle = HiddelTableDispInfos[m];
                            val += string.Format("<input type='hidden'  id='{0}' name='{0}' value='{1}' />", fieldhiddle.name, valtemp);
                        }
                    }
                    else if (TableDispInfos[j].Design_ModularFieldID == 2)//复选框
                        val += string.Format("<input type='checkbox' class='checkbox1 jq-checkall-item'  />");
                    else if (TableDispInfos[j].Design_ModularFieldID == 3)//操作
                    {
                        var btnsrows = btns.Where(p => string.IsNullOrEmpty(p.DispConditionsExpression)).ToList();
                        var DispConditionsExpressions = btns.Where(p => !string.IsNullOrEmpty(p.DispConditionsExpression)).ToList();

                        for (var i = 0; i < DispConditionsExpressions.Count(); i++)//var btn in btns)
                        {
                            var btn = DispConditionsExpressions[i];
                            #region 按钮显示条件比较
                            if (!string.IsNullOrEmpty(btn.DispConditionsExpression))
                            {
                                var DispConditionsExpressionArr = btn.DispConditionsExpression.Split('|');
                                #region 第1个数的值
                                PropertyInfo property = type.GetProperty(DispConditionsExpressionArr[1]);
                                var value1 = property.GetValue(item, null);
                                if (value1 == null)
                                    throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
                                var strValue1 = value1.ToString();
                                #endregion

                                #region 第2个数的值
                                var strValue2 = DispConditionsExpressionArr[3];
                                if (DispConditionsExpressionArr[0] == "2")//第3位为每2个对象
                                {
                                    property = type.GetProperty(DispConditionsExpressionArr[3]);
                                    var value2 = property.GetValue(item, null);
                                    if (value2 == null)
                                        throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
                                    strValue2 = value2.ToString();
                                }
                                #endregion
                                //1|BEffective|equal|1
                                #region 比较运算
                                switch (DispConditionsExpressionArr[2])
                                {
                                    case "equal":
                                        if (strValue1 != strValue2)
                                            continue;
                                        break;
                                    case "notequal":
                                        if (strValue1 == strValue2)
                                            continue;
                                        break;
                                }
                                #endregion
                                btnsrows.Add(btn);
                            }
                            else
                                btnsrows.Add(btn);
                            #endregion
                        }
                        var strval = ButtonHtml(helper, item, Design_ModularOrFun, btnsrows, "btn-xs", calcols);
                        val += strval.ToString();
                    }
                    else if (TableDispInfos[j].Design_ModularFieldID == 5)//上传控件
                        val += string.Format("<input type='file' name='file{{0}}' id='uploadify' />");
                    else
                    {
                        #region 其它字段
                        var valtemp = GetHtmlVal(TableDispInfos, item, type, j);

                        //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                        //56：整数   106：小数    167：字符串   61：日期
                        var field = TableDispInfos[j];
                        var fieldtype = "";

                        if (field.FormEleType == null)//
                        {
                        }
                        else if (field.FormEleType == 1)//文本框
                        {
                            #region 文本框
                            if (field.xtype == 61)//日期类型
                            {
                                val += string.Format("<input type='text' class=' form-control datetimepicker1 ' data-datatype='date' id='{0}' name='{0}' value='{1}' placeholder='{2}' data-fieldnamecn='{2}'  />",
                                    field.name, valtemp, field.NameCn);
                            }
                            else
                            {
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                else if (field.xtype == 167)//字符串
                                {
                                    fieldtype = "string";
                                }

                                var calcol = "";
                                var calrow = "";
                                var calele = "";
                                if (!string.IsNullOrEmpty(field.calcol))
                                {
                                    calcol = string.Format("data-calcol='{0}'", field.calcol);
                                    calele = "  calele";
                                }
                                if (!string.IsNullOrEmpty(field.calrow))
                                {
                                    calrow = string.Format("data-calrow='{0}'", field.calrow);
                                    if (calele.Length == 0)
                                        calele = "  calele";
                                }
                                //data-tabnextcol="Month01"
                                var tabnextcol = "";
                                //回车垂直方向
                                if (field.bTabVer == 1)
                                    tabnextcol = string.Format("data-tabnextcol='{0}'", field.name);

                                //sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' />",
                                //                    disabled, Required, field.name, val, field.NameCn, fieldtype));
                                val += string.Format("<input type='text' class=' form-control " + field.name + " {0} '  {1} {2} {3} id='{4}' name='{4}' value='{5}' placeholder='{6}' data-fieldnamecn='{6}'  data-datatype='{7}' />",
                                                    calele, calcol, calrow, tabnextcol, field.name, valtemp, field.NameCn, fieldtype);
                            }
                            #endregion
                        }
                        else if (field.FormEleType == 64)
                        {
                            //sbHtml.AppendLine(string.Format("<input type='text' disabled='disabled' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='string' />",
                            //disabled, Required, field.name, val, field.NameCn, fieldtype));
                            val += string.Format("<input type='text' disabled='disabled' class=' form-control ' id='{0}' name='{0}' value='{1}' placeholder='{2}' data-fieldnamecn='{2}'  data-datatype='{3}' />",
                                 field.name, valtemp, field.NameCn, fieldtype);
                        }
                        else if (field.FormEleType == 2)//标签
                        {
                            //sbHtml.AppendLine("<label control-label'>" + val + "</label>");
                            //<span id="Item_PriceTotalFm">90,000.00</span>
                            val += string.Format("<label class='{0}'>{1}</label>", field.name, valtemp);
                        }
                        else if (field.FormEleType == 4)//下拉列表框
                        {
                            SoftProjectAreaEntity data = item as SoftProjectAreaEntity;
                            StringBuilder sbHtml = new StringBuilder();

                            PropertyInfo property = type.GetProperty(field.name);
                            object value = property.GetValue(data, null);
                            //var val = "";
                            if (value != null)
                            {
                                var strval = value.ToString();
                                val = strval;
                            }

                            HtmlDropDownLisByTable(helper, data, sbHtml, type, field, ref property, ref value, ref val);
                            val = sbHtml.ToString();
                            //val = QueryHtmlDropDownList(helper, Querys, data, item, strDrop);
                            //val = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        }
                        else if (field.FormEleType == 16)
                        {
                        }
                        else if (field.FormEleType == 32)
                        {
                        }
                        else if (field.FormEleType == 256)//图片
                        {//@item.PAttachmentFileNameGuid
                            val += string.Format("<img src='{0}' />", valtemp);
                        }
                        else if (field.FormEleType == 65536)//超链接
                        {
                            #region 超链接
                            if (!string.IsNullOrEmpty(field.AdditionalInfo))
                            {
                                //在功能模块中查找url和参数
                                var aitem = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == field.AdditionalInfo).FirstOrDefault();

                                var strParam = "";
                                if (aitem.ParamName != null && aitem.ParamName.Length > 0 && item != null)
                                {
                                    #region 对象数据类型
                                    //type = data.GetType();
                                    //Type type = item.GetType();
                                    #endregion

                                    var paramNames = aitem.ParamName.Split(',');
                                    foreach (var param in paramNames)
                                    {
                                        var property = type.GetProperty(param);
                                        var value = property.GetValue(item, null);
                                        strParam += "Item." + param + "=" + value;
                                        //var val=item.
                                    }
                                }
                                if (strParam.Length > 0)
                                    strParam = "?" + strParam;
                                string url = string.Format("<a href='{0}{1}' target='_blank'>{2}&nbsp;</a>", aitem.ActionPath, strParam, valtemp);
                                //sbHtml.AppendLine(url);
                                val = url;
                            }
                            else
                                val = "<a href='#' target='_blank'>" + valtemp + "&nbsp;</a>";
                            #endregion
                        }
                        else
                            val += valtemp;
                        #endregion
                    }
                }
                strhtml.Append(string.Format("<td>{0}</td>", val));
                #endregion
            }

            strhtml.Append("</tr>");
            return strhtml.ToString();
        }

        private static string GetHtmlVal(List<SoftProjectAreaEntity> DynReportDefineDetails, object item, Type type, int j)
        {
            PropertyInfo property = type.GetProperty(DynReportDefineDetails[j].name);
            object value = property.GetValue(item, null);
            var val = "";
            if (value != null)
            {
                var strval = value.ToString();
                if (DynReportDefineDetails[j].xtype == 5)//货币类型
                {
                    val = strval.ToDecimalNull().MoneyNum();
                }
                else if (DynReportDefineDetails[j].xtype == 61)//日期类型
                {
                    if (strval != "")
                    {
                        //val = strval.ToDateNull().Format_yyyy_MM_dd();
                        if (string.IsNullOrEmpty(DynReportDefineDetails[j].DisFormat))
                            val = strval.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = strval.ToDate().ToString(DynReportDefineDetails[j].DisFormat);
                    }

                }
                else
                    val = strval;

            }
            return val;
        }

        #endregion



        /// <summary>
        /// 生成表单元素
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static MvcHtmlString FormEleHtmlHead(this HtmlHelper helper, object Item, string ModularOrFunCode, int? colcopies = 4)//List<SoftProjectAreaEntity> pagefields)// SoftProjectAreaEntity field, object data)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            var modulars = conts.Design_ModularOrFun;

            var pagefields = PageFormEleTypes(modulars);
            //pagefields1 = pagefields1.Where(p => p.PageFormElePos == 1).ToList();
            //pagefields = pagefields.Where(p => p.PageFormEleSort == 2).OrderBy(p => p.PageFormEleSort).ToList();

            pagefields = pagefields.Where(p => p.PageFormElePos == 1).OrderBy(p => p.PageFormEleSort).ToList();

            //pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();

            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();
            //if (colcopies == 6 && pagefields.Count <= 4)
            //    colcopies = 12;
            colcopies = 12 / pagefields.Count;
            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理
                if (string.IsNullOrEmpty(field.name))
                    continue;
                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                //<li class="col-lg-3">专家姓名:李医生</li>
                sbHtml.AppendLine(string.Format("<li class='col-lg-3'>{0}：{1}</li>", field.NameCn, val));
                #endregion
            }
            MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            return mstr;
        }

        /// <summary>
        /// 生成表单元素
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static MvcHtmlString FormEleHtmlHeadContext(this HtmlHelper helper, object Item, string ModularOrFunCode, int? colcopies = 4)//List<SoftProjectAreaEntity> pagefields)// SoftProjectAreaEntity field, object data)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            var modulars = conts.Design_ModularOrFun;

            var pagefields = new List<SoftProjectAreaEntity>();
            pagefields = PageFormEleTypes(modulars);
            //pagefields = pagefields.Where(p => p.PageFormEleSort == 2).OrderBy(p => p.PageFormEleSort).ToList();
            //if (pagefields1.Count > 3)
            //{
            //    pagefields = pagefields1.Skip(3).ToList();
            //}
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            pagefields = pagefields.Where(p => p.PageFormElePos == 2).OrderBy(p => p.PageFormEleSort).ToList();

            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();
            if (colcopies == 6 && pagefields.Count <= 4)
                colcopies = 12;
            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理
                if (string.IsNullOrEmpty(field.name))
                    continue;
                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                var disabled = "";
                if (field.FormEleType == 64)
                    disabled = "disabled='disabled'";

                var fieldtype = "";
                var Required = "";
                if (field.Required == 1 && field.FormEleType == 1)
                    Required = "data-required='true'";

                //if (field.bHidden == 1)
                //    { 
                //    <input type='hidden' id=Item_@field.NameEn  name='Item.@field.NameEn' value='" + val + "' />";
                //    }
                //    <li class='col-lg-6'>
                //        <div class='form-group'>
                //            <label class='col-lg-3 control-label'>@field.NameCn
                //                @if (field.Required == 1)
                //                { 
                //                    <font color="red">*</font>
                //                }
                //            </label>
                //            <div class='col-lg-9'>
                //                @Html.FormEleHtml(field, Model.Item)
                //            </div>
                //        </div>
                //    </li>
                //bHidden
                #endregion

                if (field.FormEleType == 8)
                {
                    sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                }
                //else if (field.FormEleType == 64) //文本框(只读)
                //{
                //    sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                //    sbHtml.AppendLine("<div class='form-group'>");
                //    sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                //    sbHtml.AppendLine("</label>");
                //    sbHtml.AppendLine("<div class='col-lg-9'>");
                //    sbHtml.AppendLine(string.Format("<input type='text' disabled='disabled' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='string' />",
                //    disabled, Required, field.name, val, field.NameCn, fieldtype));

                //    //sbHtml.AppendLine(string.Format("<input type='text'  id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                //    sbHtml.AppendLine("</div>");
                //    sbHtml.AppendLine("</div>");
                //    sbHtml.AppendLine("</li>");
                //}
                else
                {
                    if (field.xtype != 167)
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 100)
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 200)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-8 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }
                    else if (field.xtype == 167 && field.length > 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }

                    //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                    //56：整数   106：小数    167：字符串   61：日期
                    if (field.FormEleType == 1 || field.FormEleType == 64)//文本框、文本框(只读)
                    {
                        #region 文本框
                        if (field.xtype == 61)//日期类型
                        {
                            sbHtml.AppendLine(string.Format("<input type='text' class=' form-control datetimepicker1 ' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  />",
                                disabled, Required, field.name, val, field.NameCn));
                        }
                        else
                        {
                            if (field.xtype == 167)//字符串
                            {
                                #region 字符串
                                if (field.length <= 300)
                                {
                                    //<input alt="订单编号：不能为空！" title="订单编号：不能为空！" data-original-title="" class="form-control validaterrorpro" data-datatype="string" data-len="0|50" data-fieldnamecn="订单编号" data-required="true" id="Item_PurchaseOrderNo" name="Item.PurchaseOrderNo" value="" type="text">

                                    sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' data-len='0|{6}' />",
                                                        disabled, Required, field.name, val, field.NameCn, fieldtype, field.length / 2));
                                }
                                else
                                {
                                    sbHtml.AppendLine(string.Format("<textarea class='form-control' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' placeholder='{2}' data-fieldnamecn='{2}' data-datatype='string' data-len='0|{3}' {4} >{5}</textarea>",
                                                         Required, field.name, field.NameCn, field.length / 2, disabled, val));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 整数、小数
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' />",
                                                    disabled, Required, field.name, val, field.NameCn, fieldtype));
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (field.FormEleType == 2)//标签
                    {
                        sbHtml.AppendLine("<label class='control-label'>" + val + "</label>");
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        //var str = QueryHtmlDropTree(Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 16)
                    {

                    }
                    else if (field.FormEleType == 32)
                    {

                    }
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
            }
            MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            return mstr;
        }

        //
        public static bool BDispFormEleHtml(this HtmlHelper helper, SoftProjectAreaEntity modulars = null)//List<SoftProjectAreaEntity> pagefields)// SoftProjectAreaEntity field, object data)
        {
            var pagefields = PageFormEleTypes(modulars);
            var pagefieldstemp = pagefields.Where(p => p.FormEleType != 8);
            return pagefieldstemp.Count() > 0 ? true : false;
        }

        /// <summary>
        /// 生成表单元素:调整为混列(最后没有调整)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static IHtmlString FormEleHtml(this HtmlHelper helper, SoftProjectAreaEntity Item, string ModularOrFunCode, int? colcopies = 4, SoftProjectAreaEntity modulars = null)//List<SoftProjectAreaEntity> pagefields)// SoftProjectAreaEntity field, object data)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            if (modulars == null)
                modulars = conts.Design_ModularOrFun;

            //var xx=modulars.PageType;

            var pagefields = PageFormEleTypes(modulars);
            pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();
            if (colcopies == 6 && pagefields.Count <= 4)
                colcopies = 12;

            if (modulars.PageType != 2)
            {
                //pagefields.ForEach(p => p.FormEleType = 2);
            }



            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理
                if(string.IsNullOrEmpty( field.name))
                    continue;
                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                var disabled = "";
                if (field.FormEleType == 64)
                    disabled = "disabled='disabled'";

                var fieldtype = "";
                var Required = "";
                if (field.Required == 1 && field.FormEleType == 1)
                    Required = "data-required='true'";

                #endregion

                if (field.FormEleType == 8)
                {
                    sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                }
                else
                {
                    if (field.FormEleType == 32768)//上传--编辑页面
                    {
                        //ProductMainImage|data-folder="/Files/ProductFiles" data-browerext="*.psd;*.jpeg;*.jpg"  data-img="ProductMainImage"  data-filename="ProductMainImageFileName"  data-filepath="ProductMainImageFilePath"

                        var AdditionalInfoArrs = field.AdditionalInfo.Split('|');

                        #region 上传--编辑页面
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\" style='text-align: right;'>");
                        sbHtml.AppendLine(field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                        sbHtml.AppendLine("<div class='row'>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine(string.Format("        <input type='file' name='{0}' class='uploadify uploadifyedit'  id='{0}' {1} /><div id='{0}div'></div>", field.name, AdditionalInfoArrs[1]));
                        if (AdditionalInfoArrs[0] == "1")
                        {
                            sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                            sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}Img'>", val, field.name));// AdditionalInfoArrs[0]));
                        }
                        //sbHtml.AppendLine(string.Format("<input type='file' name='uploadify' class='uploadify uploadifyedit' id='uploadify' {0} />", field.AdditionalInfo));

                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine("        <div id='uploadifydiv'></div>");
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</li>");
                        #endregion
                        continue;
                    }

                    if (field.FormEleType == 256)//图片
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }
                    else if (field.xtype != 167)
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 100)
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 200)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-8 '>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }
                    else if (field.xtype == 167 && field.length > 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }

                    //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                    //56：整数   106：小数    167：字符串   61：日期
                    if (field.FormEleType == 1 || field.FormEleType == 64)//文本框、文本框(只读)
                    {
                        #region 文本框
                        if (field.xtype == 61)//日期类型
                        {
                            sbHtml.AppendLine(string.Format("<input type='text' class=' form-control datetimepicker1 ' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}'  />",
                                disabled, Required, field.name, val));
                        }
                        else
                        {
                            if (field.xtype == 167)//字符串
                            {
                                #region 字符串
                                if (field.length <= 300)
                                {
                                    sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                        disabled, Required, field.name, val, fieldtype, field.length / 2));
                                }
                                else
                                {
                                    sbHtml.AppendLine(string.Format("<textarea class='form-control' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' data-datatype='string' data-len='0|{2}' {3} >{4}</textarea>",
                                                         Required, field.name, field.length / 2, disabled, val));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 整数、小数
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' />",
                                                    disabled, Required, field.name, val, fieldtype));
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (field.FormEleType == 2)//标签
                    {
                        sbHtml.AppendLine(string.Format("<div class='control-label {0}'>{1}&nbsp;</div>", field.name, val));
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                        HtmlDropDownLis(helper, data, sbHtml, type, field, ref property, ref value, ref val);
                    }
                    else if (field.FormEleType == 10)//密码
                    {
                        //sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
                        sbHtml.AppendLine(string.Format("<input type='password' style='width:50%;' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='string' />",
                                            disabled, Required, field.name, val));
                    }
                    else if (field.FormEleType == 12)//子查询
                    {
                        //string actionEn = "", controllName = "";
                        if (!string.IsNullOrEmpty(field.AdditionalInfo))
                        {
                            var arrs = field.AdditionalInfo.Split(',');
                            data.ActionNameEn = arrs[0];
                            data.ControllName = arrs[1];
                        }

                        //var strmvc = System.Web.Mvc.Html.ChildActionExtensions.Action(data.ActionNameEn, data.ControllName, new { Item = data });
                        var strmvc = helper.Action(data.ActionNameEn, data.ControllName, new { Item = data });
                        sbHtml.AppendLine(strmvc.ToHtmlString() + "<br/>");
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        HtmlDropTree(sbHtml, data, field, val);
                    }
                    else if (field.FormEleType == 256)//图片
                    {
                        #region 图片
                        var filepath = val;
                        //if (field.ModularFieldRemark != null)
                        //{
                        //    var imgfield = field.ModularFieldRemark;
                        //    property = type.GetProperty(imgfield);
                        //    value = property.GetValue(data, null);
                        //    //val = "";
                        //    if (value != null)
                        //    {
                        //        var strval = value.ToString();
                        //        filepath = strval;
                        //    }
                        //}

                        sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}'>", filepath, field.name));
                        #endregion
                    }
                    else if (field.FormEleType == 512)//Html编辑器
                    {
                        sbHtml.AppendLine(string.Format("<script class='ueEdit' id='{0}' data-name='{0}' type='text/plain'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</script>");
                    }
                    else if (field.FormEleType == 1024)//Html元素
                    {
                        sbHtml.AppendLine(string.Format("<div class='frmHtml' data-name='{0}'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</div>");
                    }
                    else if (field.FormEleType == 2048)//单个复选框
                    {
                        var str = HtmlHelpers.ChecksButton(helper, field.name, field.NameCn, val);
                        sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 8192)
                    {
                        #region
                        property = type.GetProperty(field.name);
                        value = property.GetValue(data, null);
                        if (value != null)
                        {
                            var strval = value.ToString();
                            val = strval;
                        }

                        var dict = field.name;
                        if (!string.IsNullOrEmpty(field.Dicts))
                            dict = field.Dicts;

                        var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        sbHtml.AppendLine(str.ToString());
                        #endregion
                    }
                    else if (field.FormEleType == 8193)//下拉复选框(','分隔)
                    {
                        #region
                        DropDownListMultiSelects(helper, data, sbHtml, type, field, ref property, ref value, ref val);
                        //var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        //sbHtml.AppendLine(str.ToString());
                        #endregion
                    }
                    else if (field.FormEleType == 65536)//超链接
                    {
                        #region 超链接
                        if (!string.IsNullOrEmpty(field.AdditionalInfo))
                        {
                            //在功能模块中查找url和参数
                            var aitem = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == field.AdditionalInfo).FirstOrDefault();

                            var strParam = "";
                            if (aitem.ParamName != null && aitem.ParamName.Length > 0 && data != null)
                            {
                                #region 对象数据类型
                                //type = data.GetType();
                                //Type type = item.GetType();
                                #endregion

                                var paramNames = aitem.ParamName.Split(',');
                                foreach (var param in paramNames)
                                {
                                    property = type.GetProperty(param);
                                    value = property.GetValue(data, null);
                                    strParam += "Item." + param + "=" + value;
                                    //var val=item.
                                }
                            }
                            if (strParam.Length > 0)
                                strParam = "?" + strParam;
                            string url = string.Format("<a href='{0}{1}' target='_blank'>{2}&nbsp;</a>", aitem.ActionPath, strParam, val);
                            sbHtml.AppendLine(url);
                            //val = url;
                        }
                        else
                            sbHtml.AppendLine("<a href='#' target='_blank'>" + val + "&nbsp;</a>");
                        #endregion
                    }
                    else if (field.FormEleType == 16)
                    {

                    }
                    else if (field.FormEleType == 32)
                    {

                    }
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
            }
            //IHtmlString ss=new MvcHtmlString(sbHtml.ToString());
            //MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
            return tempstr;
        }

        /// <summary>
        /// 生成表单元素:调整为混列(最后没有调整)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static IHtmlString FormEleHtml1(this HtmlHelper helper, SoftProjectAreaEntity Item, string ModularOrFunCode, int? colcopies = 4, SoftProjectAreaEntity modulars = null)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            if (modulars == null)
                modulars = conts.Design_ModularOrFun;

            //var xx=modulars.PageType;

            var pagefields = PageFormEleTypes(modulars);
            pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();

            if (modulars.PageType != 2)
            {
                pagefields.ForEach(p => p.FormEleType = 2);
            }
            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理
                if (string.IsNullOrEmpty(field.name))
                {
                    if (field.FormEleType == 32768)//上传--编辑页面
                    {
                        #region 上传--编辑页面
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">");
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                        sbHtml.AppendLine("<div class='row'>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine(string.Format("        <input type='file' name='uploadify' class='uploadify uploadifyEdit' id='uploadify' {0} />", field.AdditionalInfo));
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine("        <div id='uploadifydiv'></div>");
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</li>");
                        #endregion
                    }
                    continue;
                }
                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                var disabled = "";
                if (field.FormEleType == 64)
                    disabled = "disabled='disabled'";

                var fieldtype = "";
                var Required = "";
                if (field.Required == 1 && field.FormEleType == 1)
                    Required = "data-required='true'";

                #endregion

                if (field.FormEleType == 8)
                {
                    sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                }
                else
                {
                    if (field.FormEleType == 256)//图片
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }
                    else if (field.xtype != 167)
                    {
                        sbHtml.AppendLine("<li class='col-lg-12'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }
                    else if (field.xtype == 167 && field.length <= 100)
                    {
                        sbHtml.AppendLine("<li class='col-lg-12'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }
                    else if (field.xtype == 167 && field.length <= 200)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 '>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2  control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }
                    else if (field.xtype == 167 && field.length <= 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }
                    else if (field.xtype == 167 && field.length > 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2  control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }

                    //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                    //56：整数   106：小数    167：字符串   61：日期
                    if (field.FormEleType == 1 || field.FormEleType == 64)//文本框、文本框(只读)
                    {
                        #region 文本框
                        if (field.xtype == 61)//日期类型
                        {
                            sbHtml.AppendLine(string.Format("<input type='text' style='width:50%;' class=' form-control datetimepicker1' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}'  />",
                                disabled, Required, field.name, val, field.NameCn));
                        }
                        else
                        {
                            if (field.xtype == 167)//字符串
                            {
                                #region 字符串
                                if (field.length <= 300)
                                {
                                    if (field.length <= 100)
                                        sbHtml.AppendLine(string.Format("<input type='text' style='width:50%;' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                        disabled, Required, field.name, val, fieldtype, field.length / 2));
                                    else
                                        sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                            disabled, Required, field.name, val, fieldtype, field.length / 2));
                                }
                                else
                                {
                                    sbHtml.AppendLine(string.Format("<textarea class='form-control ' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' data-datatype='string' data-len='0|{2}' {3} >{4}</textarea>",
                                                         Required, field.name, field.length / 2, disabled, val));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 整数、小数
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                sbHtml.AppendLine(string.Format("<input type='text' style='width:50%;' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' data-datatype='{4}' />",
                                                    disabled, Required, field.name, val, fieldtype));
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (field.FormEleType == 2)//标签
                    {
                        sbHtml.AppendLine(string.Format("<div class='control-label {0}'>{1}&nbsp;</div>", field.name, val));
                    }
                    else if (field.FormEleType == 10)//密码
                    {
                        //sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
                        sbHtml.AppendLine(string.Format("<input type='password' style='width:50%;' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='string' />",
                                            disabled, Required, field.name, val));
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                        HtmlDropDownLis(helper, data, sbHtml, type, field, ref property, ref value, ref val, "style='width:50%;'");
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        HtmlDropTree(sbHtml, data, field, val);
                    }
                    else if (field.FormEleType == 256)//图片
                    {
                        #region 图片
                        var filepath = val;
                        if (field.ModularFieldRemark != null)
                        {
                            var imgfield = field.ModularFieldRemark;
                            property = type.GetProperty(imgfield);
                            value = property.GetValue(data, null);
                            //val = "";
                            if (value != null)
                            {
                                var strval = value.ToString();
                                filepath = strval;
                            }
                        }
                        sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}'>", filepath, field.name));
                        #endregion
                    }
                    else if (field.FormEleType == 512)//Html编辑器
                    {
                        sbHtml.AppendLine(string.Format("<script class='ueEdit' id='{0}' data-name='{0}' type='text/plain'>", field.name));

                        //sbHtml.AppendLine(string.Format("<script id='ueEdit' data-name='{0}' type='text/plain'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</script>");
                    }
                    else if (field.FormEleType == 1024)//Html元素
                    {
                        sbHtml.AppendLine(string.Format("<div class='frmHtml' data-name='{0}'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</div>");
                    }
                    else if (field.FormEleType == 2048)//单个复选框
                    {
                        var str = HtmlHelpers.ChecksButton(helper, field.name, field.NameCn, val);
                        sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 8192)
                    {
                        #region
                        property = type.GetProperty(field.name);
                        value = property.GetValue(data, null);
                        if (value != null)
                        {
                            var strval = value.ToString();
                            val = strval;
                        }

                        var dict = field.name;
                        if (!string.IsNullOrEmpty(field.Dicts))
                            dict = field.Dicts;

                        var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        sbHtml.AppendLine(str.ToString());
                        #endregion
                    }
                    else if (field.FormEleType == 65536)//超链接
                    {
                        #region 超链接
                        if (!string.IsNullOrEmpty(field.AdditionalInfo))
                        {
                            //在功能模块中查找url和参数
                            var aitem = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == field.AdditionalInfo).FirstOrDefault();

                            var strParam = "";
                            if (aitem.ParamName != null && aitem.ParamName.Length > 0 && data != null)
                            {
                                #region 对象数据类型
                                //type = data.GetType();
                                //Type type = item.GetType();
                                #endregion

                                var paramNames = aitem.ParamName.Split(',');
                                foreach (var param in paramNames)
                                {
                                    property = type.GetProperty(param);
                                    value = property.GetValue(data, null);
                                    strParam += "Item." + param + "=" + value;
                                    //var val=item.
                                }
                            }
                            if (strParam.Length > 0)
                                strParam = "?" + strParam;
                            string url = string.Format("<a href='{0}{1}' target='_blank'>{2}&nbsp;</a>", aitem.ActionPath, strParam, val);
                            sbHtml.AppendLine(url);
                        }
                        else
                            sbHtml.AppendLine("<a href='#' target='_blank'>" + val + "&nbsp;</a>");
                        #endregion
                    }
                    else if (field.FormEleType == 16)
                    {
                    }
                    else if (field.FormEleType == 32)
                    {

                    }
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
            }
            //IHtmlString ss=new MvcHtmlString(sbHtml.ToString());
            //MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
            return tempstr;
        }

        /// <summary>
        /// 生成表单元素:调整为混列
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static IHtmlString FormEleHtml1N(this HtmlHelper helper, SoftProjectAreaEntity Item, string ModularOrFunCode, int? colcopies = 4, SoftProjectAreaEntity modulars = null)
        {
            var conts = helper.ViewContext.Controller as BaseController;
            if (modulars == null)
                modulars = conts.Design_ModularOrFun;

            //var xx=modulars.PageType;

            var pagefields = PageFormEleTypes(modulars);
            pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();

            if (modulars.PageType != 2)
            {
                pagefields.ForEach(p => p.FormEleType = 2);
            }
            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理
                if (string.IsNullOrEmpty(field.name))
                {
                    if (field.FormEleType == 32768)//上传--编辑页面
                    {
                        #region 上传--编辑页面
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">");
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                        sbHtml.AppendLine("<div class='row'>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine(string.Format("        <input type='file' name='uploadify' class='uploadify uploadifyEdit' id='uploadify' {0} />", field.AdditionalInfo));
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine("        <div id='uploadifydiv'></div>");
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</li>");
                        #endregion
                    }
                    continue;
                }
                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                var disabled = "";
                if (field.FormEleType == 64)
                    disabled = "disabled='disabled'";

                var fieldtype = "";
                var Required = "";
                if (field.Required == 1 && field.FormEleType == 1)
                    Required = "data-required='true'";

                #endregion

                if (field.FormEleType == 8)
                {
                    sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                }
                else
                {
                    if (field.FormEleType == 256)//图片
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }
                    else if (field.xtype != 167)
                    {
                        sbHtml.AppendLine("<li class='col-lg-12'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 100)
                    {
                        sbHtml.AppendLine("<li class='col-lg-12'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 200)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 '>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3  control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length > 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2  control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }

                    //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                    //56：整数   106：小数    167：字符串   61：日期
                    if (field.FormEleType == 1 || field.FormEleType == 64)//文本框、文本框(只读)
                    {
                        #region 文本框
                        if (field.xtype == 61)//日期类型
                        {
                            sbHtml.AppendLine(string.Format("<input type='text' style=';' class=' form-control datetimepicker1' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}'  />",
                                disabled, Required, field.name, val, field.NameCn));
                        }
                        else
                        {
                            if (field.xtype == 167)//字符串
                            {
                                #region 字符串
                                if (field.length <= 300)
                                {
                                    if (field.length <= 100)
                                        sbHtml.AppendLine(string.Format("<input type='text' style=';' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                        disabled, Required, field.name, val, fieldtype, field.length / 2));
                                    else
                                        sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                            disabled, Required, field.name, val, fieldtype, field.length / 2));
                                }
                                else
                                {
                                    sbHtml.AppendLine(string.Format("<textarea class='form-control ' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' data-datatype='string' data-len='0|{2}' {3} >{4}</textarea>",
                                                         Required, field.name, field.length / 2, disabled, val));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 整数、小数
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                sbHtml.AppendLine(string.Format("<input type='text' style=';' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' data-datatype='{4}' />",
                                                    disabled, Required, field.name, val, fieldtype));
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (field.FormEleType == 2)//标签
                    {
                        sbHtml.AppendLine(string.Format("<div class='control-label {0}'>{1}&nbsp;</div>", field.name, val));
                    }
                    else if (field.FormEleType == 10)//密码
                    {
                        //sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
                        sbHtml.AppendLine(string.Format("<input type='password' style=';' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='string' />",
                                            disabled, Required, field.name, val));
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                        HtmlDropDownLis(helper, data, sbHtml, type, field, ref property, ref value, ref val);
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        HtmlDropTree(sbHtml, data, field, val);
                    }
                    else if (field.FormEleType == 256)//图片
                    {
                        #region 图片
                        var filepath = val;
                        if (field.ModularFieldRemark != null)
                        {
                            var imgfield = field.ModularFieldRemark;
                            property = type.GetProperty(imgfield);
                            value = property.GetValue(data, null);
                            //val = "";
                            if (value != null)
                            {
                                var strval = value.ToString();
                                filepath = strval;
                            }
                        }
                        sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}'>", filepath, field.name));
                        #endregion
                    }
                    else if (field.FormEleType == 512)//Html编辑器
                    {
                        sbHtml.AppendLine(string.Format("<script class='ueEdit' id='{0}' data-name='{0}' type='text/plain'>", field.name));

                        //sbHtml.AppendLine(string.Format("<script id='ueEdit' data-name='{0}' type='text/plain'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</script>");
                    }
                    else if (field.FormEleType == 1024)//Html元素
                    {
                        sbHtml.AppendLine(string.Format("<div class='frmHtml' data-name='{0}'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</div>");
                    }
                    else if (field.FormEleType == 2048)//单个复选框
                    {
                        var str = HtmlHelpers.ChecksButton(helper, field.name, field.NameCn, val);
                        sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 8192)
                    {
                        #region
                        property = type.GetProperty(field.name);
                        value = property.GetValue(data, null);
                        if (value != null)
                        {
                            var strval = value.ToString();
                            val = strval;
                        }

                        var dict = field.name;
                        if (!string.IsNullOrEmpty(field.Dicts))
                            dict = field.Dicts;

                        var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        sbHtml.AppendLine(str.ToString());
                        #endregion
                    }
                    else if (field.FormEleType == 65536)//超链接
                    {
                        #region 超链接
                        if (!string.IsNullOrEmpty(field.AdditionalInfo))
                        {
                            //在功能模块中查找url和参数
                            var aitem = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == field.AdditionalInfo).FirstOrDefault();

                            var strParam = "";
                            if (aitem.ParamName != null && aitem.ParamName.Length > 0 && data != null)
                            {
                                #region 对象数据类型
                                //type = data.GetType();
                                //Type type = item.GetType();
                                #endregion

                                var paramNames = aitem.ParamName.Split(',');
                                foreach (var param in paramNames)
                                {
                                    property = type.GetProperty(param);
                                    value = property.GetValue(data, null);
                                    strParam += "Item." + param + "=" + value;
                                    //var val=item.
                                }
                            }
                            if (strParam.Length > 0)
                                strParam = "?" + strParam;
                            string url = string.Format("<a href='{0}{1}' target='_blank'>{2}&nbsp;</a>", aitem.ActionPath, strParam, val);
                            sbHtml.AppendLine(url);
                        }
                        else
                            sbHtml.AppendLine("<a href='#' target='_blank'>" + val + "&nbsp;</a>");
                        #endregion
                    }
                    else if (field.FormEleType == 16)
                    {
                    }
                    else if (field.FormEleType == 32)
                    {

                    }
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
            }
            //IHtmlString ss=new MvcHtmlString(sbHtml.ToString());
            //MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
            return tempstr;
        }

        /// <summary>
        /// 生成表单元素:调整为混列(最后没有调整)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public static IHtmlString FormEleHtml2(this HtmlHelper helper, SoftProjectAreaEntity Item,
            string ModularOrFunCode, int? colcopies = 4,
Action<string, SoftProjectAreaEntity> action = null)
        {
            //    public void BulidChilds(string strhtml, SoftProjectAreaEntity item, string ActionNameEn, string ControllName)
            var conts = helper.ViewContext.Controller as BaseController;

            //if (modulars == null)
            SoftProjectAreaEntity modulars = conts.Design_ModularOrFun;

            //var xx=modulars.PageType;

            var pagefields = PageFormEleTypes(modulars);
            pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = Item;// model.Item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();

            if (modulars.PageType != 2)
            {
                pagefields.ForEach(p => p.FormEleType = 2);
            }
            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理
                if (string.IsNullOrEmpty(field.name))
                {
                    if (field.FormEleType == 32768)//上传--编辑页面
                    {
                        #region 上传--编辑页面
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">");
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                        sbHtml.AppendLine("<div class='row'>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine(string.Format("        <input type='file' name='uploadify' class='uploadify uploadifyEdit' id='uploadify' {0} />", field.AdditionalInfo));
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("    <div class='col-sm-6'>");
                        sbHtml.AppendLine("        <div id='uploadifydiv'></div>");
                        sbHtml.AppendLine("    </div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</div>");
                        sbHtml.AppendLine("</li>");
                        #endregion
                    }
                    continue;
                }
                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                var disabled = "";
                if (field.FormEleType == 64)
                    disabled = "disabled='disabled'";

                var fieldtype = "";
                var Required = "";
                if (field.Required == 1 && field.FormEleType == 1)
                    Required = "data-required='true'";

                #endregion

                if (field.FormEleType == 8)
                {
                    sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                }
                else
                {
                    if (field.FormEleType == 256)//图片
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2 control-label\" style='text-align: right;width:12.5%;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10'>");
                    }
                    else if (field.xtype != 167)
                    {
                        sbHtml.AppendLine("<li class='col-lg-6'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 100)
                    {
                        sbHtml.AppendLine("<li class='col-lg-6'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\" style='text-align: right;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 200)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 '>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2  control-label\" style='text-align: right;width:12.5%;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10' style='width:87.5%;'>");
                    }
                    else if (field.xtype == 167 && field.length <= 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2 control-label\" style='text-align: right;width:12.5%;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10' style='width:87.5%;'>");
                    }
                    else if (field.xtype == 167 && field.length > 300)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-2  control-label\" style='text-align: right;width:12.5%;'>" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-10' style='width:87.5%;'>");
                    }

                    //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                    //56：整数   106：小数    167：字符串   61：日期
                    if (field.FormEleType == 1 || field.FormEleType == 64)//文本框、文本框(只读)
                    {
                        #region 文本框
                        if (field.xtype == 61)//日期类型
                        {
                            sbHtml.AppendLine(string.Format("<input type='text'  class=' form-control datetimepicker1 ' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}'  />",
                                disabled, Required, field.name, val));
                        }
                        else
                        {
                            if (field.xtype == 167)//字符串
                            {
                                #region 字符串
                                if (field.length <= 300)
                                {
                                    if (field.length <= 100)
                                        sbHtml.AppendLine(string.Format("<input type='text'  class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                        disabled, Required, field.name, val, fieldtype, field.length / 2));
                                    else
                                        sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' data-len='0|{5}' />",
                                                            disabled, Required, field.name, val, fieldtype, field.length / 2));
                                }
                                else
                                {
                                    sbHtml.AppendLine(string.Format("<textarea class='form-control' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' data-datatype='string' data-len='0|{2}' {3} >{4}</textarea>",
                                                         Required, field.name, field.length / 2, disabled, val));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 整数、小数
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                sbHtml.AppendLine(string.Format("<input type='text'  class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='{4}' />",
                                                    disabled, Required, field.name, val, fieldtype));
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (field.FormEleType == 2)//标签
                    {
                        sbHtml.AppendLine(string.Format("<div class='control-label {0}'>{1}&nbsp;</div>", field.name, val));
                        //sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                        HtmlDropDownLis(helper, data, sbHtml, type, field, ref property, ref value, ref val);
                    }
                    else if (field.FormEleType == 10)//密码
                    {
                        //sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
                        sbHtml.AppendLine(string.Format("<input type='password' style='width:50%;' class=' form-control' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}'  data-datatype='string' />",
                                            disabled, Required, field.name, val));
                    }
                    else if (field.FormEleType == 12)//子查询
                    {
                        //string actionEn = "", controllName = "";
                        if (!string.IsNullOrEmpty(field.AdditionalInfo))
                        {
                            var arrs = field.AdditionalInfo.Split(',');
                            data.ActionNameEn = arrs[0];
                            data.ControllName = arrs[1];
                        }

                        //var strmvc = System.Web.Mvc.Html.ChildActionExtensions.Action(data.ActionNameEn, data.ControllName, new { Item = data });
                        var strmvc = helper.Action(data.ActionNameEn, data.ControllName, new { Item = data });
                        sbHtml.AppendLine(strmvc.ToHtmlString() + "<br/>");
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        HtmlDropTree(sbHtml, data, field, val);
                    }
                    else if (field.FormEleType == 256)//图片
                    {
                        #region 图片
                        var filepath = val;
                        if (field.ModularFieldRemark != null)
                        {
                            var imgfield = field.ModularFieldRemark;
                            property = type.GetProperty(imgfield);
                            value = property.GetValue(data, null);
                            //val = "";
                            if (value != null)
                            {
                                var strval = value.ToString();
                                filepath = strval;
                            }
                        }
                        sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}'>", filepath, field.name));
                        #endregion
                    }
                    else if (field.FormEleType == 512)//Html编辑器
                    {
                        sbHtml.AppendLine(string.Format("<script class='ueEdit' id='{0}' data-name='{0}' type='text/plain'>", field.name));

                        //sbHtml.AppendLine(string.Format("<script id='ueEdit' data-name='{0}' type='text/plain'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</script>");
                    }
                    else if (field.FormEleType == 1024)//Html元素
                    {
                        sbHtml.AppendLine(string.Format("<div class='frmHtml' data-name='{0}'>", field.name));
                        sbHtml.AppendLine(val);
                        sbHtml.AppendLine("</div>");
                    }
                    else if (field.FormEleType == 2048)//单个复选框
                    {
                        var str = HtmlHelpers.ChecksButton(helper, field.name, field.NameCn, val);
                        sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 8192)
                    {
                        #region
                        property = type.GetProperty(field.name);
                        value = property.GetValue(data, null);
                        if (value != null)
                        {
                            var strval = value.ToString();
                            val = strval;
                        }

                        var dict = field.name;
                        if (!string.IsNullOrEmpty(field.Dicts))
                            dict = field.Dicts;

                        var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        sbHtml.AppendLine(str.ToString());
                        #endregion
                    }
                    else if (field.FormEleType == 8193)//下拉复选框(','分隔)
                    {
                        #region
                        DropDownListMultiSelects(helper, data, sbHtml, type, field, ref property, ref value, ref val);
                        //var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        //sbHtml.AppendLine(str.ToString());
                        #endregion
                    }
                    else if (field.FormEleType == 65536)//超链接
                    {
                        #region 超链接
                        if (!string.IsNullOrEmpty(field.AdditionalInfo))
                        {
                            //在功能模块中查找url和参数
                            var aitem = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == field.AdditionalInfo).FirstOrDefault();

                            var strParam = "";
                            if (aitem.ParamName != null && aitem.ParamName.Length > 0 && data != null)
                            {
                                #region 对象数据类型
                                //type = data.GetType();
                                //Type type = item.GetType();
                                #endregion

                                var paramNames = aitem.ParamName.Split(',');
                                foreach (var param in paramNames)
                                {
                                    property = type.GetProperty(param);
                                    value = property.GetValue(data, null);
                                    strParam += "Item." + param + "=" + value;
                                    //var val=item.
                                }
                            }
                            if (strParam.Length > 0)
                                strParam = "?" + strParam;
                            string url = string.Format("<a href='{0}{1}' target='_blank'>{2}&nbsp;</a>", aitem.ActionPath, strParam, val);
                            sbHtml.AppendLine(url);
                        }
                        else
                            sbHtml.AppendLine("<a href='#' target='_blank'>" + val + "&nbsp;</a>");
                        #endregion
                    }
                    else if (field.FormEleType == 16)
                    {
                    }
                    else if (field.FormEleType == 32)
                    {

                    }
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
            }
            //IHtmlString ss=new MvcHtmlString(sbHtml.ToString());
            //MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            //sbHtml.AppendLine("<li class='col-lg-12'>");
            //sbHtml.AppendLine("    <div class='form-group'>");
            //sbHtml.AppendLine("        <label class='col-lg-2 control-label' style='text-align: right;width:12.5%;'>");
            //sbHtml.AppendLine("            图片");
            //sbHtml.AppendLine("        </label>");
            //sbHtml.AppendLine("        <div class='col-lg-10' style='width:87.5%;'>");
            ////sbHtml.AppendLine("            @if (Model.EditAction != null)");
            ////sbHtml.AppendLine("            {");
            //sbHtml.AppendLine("                @Html.Action(Model.EditAction.ActionNameEn, Model.EditAction.ControllName, request)");
            ////sbHtml.AppendLine("            }");
            //sbHtml.AppendLine("            <br />");
            //sbHtml.AppendLine("        </div>");
            //sbHtml.AppendLine("    </div>");
            //sbHtml.AppendLine("</li>");

            var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
            //var tempstr = sbHtml.ToString();
            return tempstr;
        }

        ///// <summary>
        ///// 生成表单元素
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="item">显示的数据项定义</param>
        ///// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        ///// <returns></returns>
        //public static IHtmlString FormEleHtml(this HtmlHelper helper, object Item, string ModularOrFunCode, int? colcopies = 4, SoftProjectAreaEntity modulars = null)//List<SoftProjectAreaEntity> pagefields)// SoftProjectAreaEntity field, object data)
        //{
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    if (modulars == null)
        //        modulars = conts.Design_ModularOrFun;

        //    //var xx=modulars.PageType;

        //    var pagefields = PageFormEleTypes(modulars);
        //    pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
        //    //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
        //    var data = Item;// model.Item;
        //    if (data == null)
        //        return new MvcHtmlString("没有数据！");
        //    StringBuilder sbHtml = new StringBuilder();
        //    var type = data.GetType();
        //    if (colcopies == 6 && pagefields.Count <= 4)
        //        colcopies = 12;

        //    if (modulars.PageType != 2)
        //    {
        //        pagefields.ForEach(p => p.FormEleType = 2);
        //    }

        //    foreach (var field in pagefields)
        //    {
        //        #region 基本数据、控制字段处理
        //        if (string.IsNullOrEmpty(field.name))
        //        {
        //            if (field.FormEleType == 32768)//上传--编辑页面
        //            {
        //                #region 上传--编辑页面
        //                sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">");
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-11'>");
        //                sbHtml.AppendLine("<div class='row'>");
        //                sbHtml.AppendLine("    <div class='col-sm-6'>");
        //                sbHtml.AppendLine(string.Format("        <input type='file' name='uploadify' class='uploadify uploadifyEdit' id='uploadify' {0} />", field.AdditionalInfo));
        //                sbHtml.AppendLine("    </div>");
        //                sbHtml.AppendLine("    <div class='col-sm-6'>");
        //                sbHtml.AppendLine("        <div id='uploadifydiv'></div>");
        //                sbHtml.AppendLine("    </div>");
        //                sbHtml.AppendLine("</div>");
        //                sbHtml.AppendLine("</div>");
        //                sbHtml.AppendLine("</div>");
        //                sbHtml.AppendLine("</li>");
        //                #endregion
        //            }
        //            continue;
        //        }
        //        PropertyInfo property = type.GetProperty(field.name);
        //        object value = property.GetValue(data, null);
        //        var val = "";
        //        if (value != null)
        //        {
        //            var strval = value.ToString();
        //            val = strval;
        //        }
        //        if (field.xtype == 61)//日期类型
        //        {
        //            if (val != "")
        //            {
        //                if (string.IsNullOrEmpty(field.DisFormat))
        //                    val = val.ToDate().ToString("yyyy-MM-dd");
        //                else
        //                    val = val.ToDate().ToString(field.DisFormat);
        //            }
        //        }
        //        var disabled = "";
        //        if (field.FormEleType == 64)
        //            disabled = "disabled='disabled'";

        //        var fieldtype = "";
        //        var Required = "";
        //        if (field.Required == 1 && field.FormEleType == 1)
        //            Required = "data-required='true'";

        //        #endregion

        //        if (field.FormEleType == 8)
        //        {
        //            sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
        //        }
        //        else
        //        {
        //            if (field.FormEleType == 256)//图片
        //            {
        //                sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-11'>");
        //            }
        //            else if (field.xtype != 167)
        //            {
        //                sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-9'>");
        //            }
        //            else if (field.xtype == 167 && field.length <= 100)
        //            {
        //                sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-9'>");
        //            }
        //            else if (field.xtype == 167 && field.length <= 200)//字符串
        //            {
        //                sbHtml.AppendLine("<li class='col-lg-8 '>");//overflow-auto
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-9'>");
        //            }
        //            else if (field.xtype == 167 && field.length <= 300)//字符串
        //            {
        //                sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");//overflow-auto
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-11'>");
        //            }
        //            else if (field.xtype == 167 && field.length > 300)//字符串
        //            {
        //                sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
        //                sbHtml.AppendLine("<div class='form-group'>");
        //                sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
        //                if (field.Required == 1)
        //                {
        //                    sbHtml.AppendLine("<font color='red'>*</font>");
        //                }
        //                sbHtml.AppendLine("</label>");
        //                sbHtml.AppendLine("<div class='col-lg-11'>");
        //            }

        //            //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
        //            //56：整数   106：小数    167：字符串   61：日期
        //            if (field.FormEleType == 1 || field.FormEleType == 64)//文本框、文本框(只读)
        //            {
        //                #region 文本框
        //                if (field.xtype == 61)//日期类型
        //                {
        //                    sbHtml.AppendLine(string.Format("<input type='text' class=' form-control datetimepicker1 ' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  />",
        //                        disabled, Required, field.name, val, field.NameCn));
        //                }
        //                else
        //                {
        //                    if (field.xtype == 167)//字符串
        //                    {
        //                        #region 字符串
        //                        if (field.length <= 300)
        //                        {
        //                            sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' data-len='0|{6}' />",
        //                                                disabled, Required, field.name, val, field.NameCn, fieldtype, field.length / 2));
        //                        }
        //                        else
        //                        {
        //                            sbHtml.AppendLine(string.Format("<textarea class='form-control' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' placeholder='{2}' data-fieldnamecn='{2}' data-datatype='string' data-len='0|{3}' {4} >{5}</textarea>",
        //                                                 Required, field.name, field.NameCn, field.length / 2, disabled, val));
        //                        }
        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        #region 整数、小数
        //                        if (field.xtype == 56)//整数
        //                        {
        //                            fieldtype = "int";
        //                        }
        //                        else if (field.xtype == 106)//小数
        //                        {
        //                            fieldtype = "dec";
        //                        }
        //                        sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' />",
        //                                            disabled, Required, field.name, val, field.NameCn, fieldtype));
        //                        #endregion
        //                    }
        //                }
        //                #endregion
        //            }
        //            else if (field.FormEleType == 2)//标签
        //            {
        //                sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
        //            }
        //            else if (field.FormEleType == 4)//下拉列表框
        //            {
        //                //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
        //                //sbHtml.AppendLine(str.ToString());
        //                HtmlDropDownLis(helper, data, sbHtml, type, field, ref property, ref value, ref val);
        //            }
        //            else if (field.FormEleType == 128)//下拉树
        //            {
        //                HtmlDropTree(sbHtml, field, val);
        //            }
        //            else if (field.FormEleType == 256)//图片
        //            {
        //                #region 图片
        //                var filepath = val;
        //                if (field.ModularFieldRemark != null)
        //                {
        //                    var imgfield = field.ModularFieldRemark;
        //                    property = type.GetProperty(imgfield);
        //                    value = property.GetValue(data, null);
        //                    //val = "";
        //                    if (value != null)
        //                    {
        //                        var strval = value.ToString();
        //                        filepath = strval;
        //                    }
        //                }
        //                sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}'>", filepath, field.name));
        //                #endregion
        //            }
        //            else if (field.FormEleType == 512)//Html编辑器
        //            {
        //                sbHtml.AppendLine(string.Format("<script id='ueEdit' data-name='{0}' type='text/plain'>", field.name));
        //                sbHtml.AppendLine(val);
        //                sbHtml.AppendLine("</script>");
        //            }
        //            else if (field.FormEleType == 1024)//Html元素
        //            {
        //                sbHtml.AppendLine(string.Format("<div class='frmHtml' data-name='{0}'>", field.name));
        //                sbHtml.AppendLine(val);
        //                sbHtml.AppendLine("</div>");
        //            }
        //            else if (field.FormEleType == 2048)//单个复选框
        //            {
        //                var str = HtmlHelpers.ChecksButton(helper, field.name, field.NameCn, val);
        //                sbHtml.AppendLine(str.ToString());
        //            }
        //            else if (field.FormEleType == 8192)
        //            {
        //                #region
        //                property = type.GetProperty(field.name);
        //                value = property.GetValue(data, null);
        //                if (value != null)
        //                {
        //                    var strval = value.ToString();
        //                    val = strval;
        //                }

        //                var dict = field.name;
        //                if (!string.IsNullOrEmpty(field.Dicts))
        //                    dict = field.Dicts;

        //                var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
        //                sbHtml.AppendLine(str.ToString());
        //                #endregion
        //            }
        //            else if (field.FormEleType == 16)
        //            {

        //            }
        //            else if (field.FormEleType == 32)
        //            {

        //            }
        //            sbHtml.AppendLine("</div>");
        //            sbHtml.AppendLine("</div>");
        //            sbHtml.AppendLine("</li>");
        //        }
        //    }
        //    //IHtmlString ss=new MvcHtmlString(sbHtml.ToString());
        //    //MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
        //    var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
        //    return tempstr;
        //}

        public static MvcHtmlString FormEleHtmlTable(this HtmlHelper helper,
            SoftProjectAreaEntity item, List<SoftProjectAreaEntity> pagefields, int? colcopies = 4)
        {
            //List<SoftProjectAreaEntity>

            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == model.ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            var data = item;
            if (data == null)
                return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            var type = data.GetType();
            if (colcopies == 6 && pagefields.Count <= 4)
                colcopies = 12;
            foreach (var field in pagefields)
            {
                #region 基本数据、控制字段处理

                PropertyInfo property = type.GetProperty(field.name);
                object value = property.GetValue(data, null);
                var val = "";
                if (value != null)
                {
                    var strval = value.ToString();
                    val = strval;
                }
                if (field.xtype == 61)//日期类型
                {
                    if (val != "")
                    {
                        if (string.IsNullOrEmpty(field.DisFormat))
                            val = val.ToDate().ToString("yyyy-MM-dd");
                        else
                            val = val.ToDate().ToString(field.DisFormat);
                    }
                }
                var disabled = "";
                if (field.bDisabled == 1)
                    disabled = "disabled='disabled'";

                var fieldtype = "";
                var Required = "";
                if (field.Required == 1)
                    Required = "data-required='true'";

                //if (field.bHidden == 1)
                //    { 
                //    <input type='hidden' id=Item_@field.NameEn  name='Item.@field.NameEn' value='" + val + "' />";
                //    }
                //    <li class='col-lg-6'>
                //        <div class='form-group'>
                //            <label class='col-lg-3 control-label'>@field.NameCn
                //                @if (field.Required == 1)
                //                { 
                //                    <font color="red">*</font>
                //                }
                //            </label>
                //            <div class='col-lg-9'>
                //                @Html.FormEleHtml(field, Model.Item)
                //            </div>
                //        </div>
                //    </li>
                //bHidden
                #endregion

                if (field.FormEleType == 8)
                {
                    sbHtml.AppendLine(string.Format("<input type='hidden' id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                }
                else if (field.FormEleType == 64)
                {
                    sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                    sbHtml.AppendLine("<div class='form-group'>");
                    sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                    sbHtml.AppendLine("</label>");
                    sbHtml.AppendLine("<div class='col-lg-9'>");
                    sbHtml.AppendLine(string.Format("<input type='text' disabled='disabled' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='string' />",
                    disabled, Required, field.name, val, field.NameCn, fieldtype));

                    //sbHtml.AppendLine(string.Format("<input type='text'  id='Item_{0}' name='Item.{0}' value='{1}' />", field.name, val));
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
                else
                {
                    if (field.xtype != 167)
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length <= 100)
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    else if (field.xtype == 167 && field.length > 200)//字符串
                    {
                        sbHtml.AppendLine("<li class='col-lg-12 overflow-auto'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-1 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-11'>");
                    }
                    else
                    {
                        sbHtml.AppendLine("<li class='col-lg-" + colcopies * 2 + "'>");
                        sbHtml.AppendLine("<div class='form-group'>");
                        sbHtml.AppendLine("<label class=\"col-lg-3 control-label\">" + field.NameCn);
                        if (field.Required == 1)
                        {
                            sbHtml.AppendLine("<font color='red'>*</font>");
                        }
                        sbHtml.AppendLine("</label>");
                        sbHtml.AppendLine("<div class='col-lg-9'>");
                    }
                    //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                    //56：整数   106：小数    167：字符串   61：日期
                    if (field.FormEleType == 1)//文本框
                    {
                        #region 文本框
                        if (field.xtype == 61)//日期类型
                        {
                            //if (val != "")
                            //{
                            //    if (field.DisFormat == null)
                            //        val = val.ToDate().ToString("yyyy-MM-dd");
                            //    else
                            //        val = val.ToDate().ToString(field.DisFormat);
                            //}
                            sbHtml.AppendLine(string.Format("<input type='text' class=' form-control datetimepicker1 ' {0}  {1} data-datatype='date' id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  />",
                                disabled, Required, field.name, val, field.NameCn));
                        }
                        else
                        {
                            if (field.xtype == 167)//字符串
                            {
                                #region 字符串
                                if (field.length <= 100)
                                {
                                    //<input alt="订单编号：不能为空！" title="订单编号：不能为空！" data-original-title="" class="form-control validaterrorpro" data-datatype="string" data-len="0|50" data-fieldnamecn="订单编号" data-required="true" id="Item_PurchaseOrderNo" name="Item.PurchaseOrderNo" value="" type="text">

                                    sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' data-len='0|{6}' />",
                                                        disabled, Required, field.name, val, field.NameCn, fieldtype, field.length / 2));
                                }
                                else
                                {
                                    sbHtml.AppendLine(string.Format("<textarea class='form-control' rows='3'  {0}  id='Item_{1}' name='Item.{1}' data-datatype='string' placeholder='{2}' data-fieldnamecn='{2}' data-datatype='string' data-len='0|{3}' >{4}</textarea>",
                                                         Required, field.name, field.NameCn, field.length / 2, val));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 整数、小数
                                if (field.xtype == 56)//整数
                                {
                                    fieldtype = "int";
                                }
                                else if (field.xtype == 106)//小数
                                {
                                    fieldtype = "dec";
                                }
                                sbHtml.AppendLine(string.Format("<input type='text' class=' form-control ' {0}  {1}  id='Item_{2}' name='Item.{2}' value='{3}' placeholder='{4}' data-fieldnamecn='{4}'  data-datatype='{5}' />",
                                                    disabled, Required, field.name, val, field.NameCn, fieldtype));
                                #endregion
                            }
                        }
                        #endregion
                    }
                    else if (field.FormEleType == 2)//标签
                    {
                        sbHtml.AppendLine("<label control-label>" + val + "</label>");
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        //var str = QueryHtmlDropTree(Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 16)
                    {

                    }
                    else if (field.FormEleType == 32)
                    {

                    }
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</div>");
                    sbHtml.AppendLine("</li>");
                }
            }
            MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            return mstr;
        }

        #region 树
        //@Html.JqTree("OrganizationsTree", ViewData["ListOrganizationsTree"] as TreeList, ViewData["currentPath"].ToString(), false)

        public static MvcHtmlString JqTreeN(this HtmlHelper helper, string ID, TreeList treeList, string selectTreeNode, int? paramNameType = 1, bool parentLink = true, bool bcontroll = false)
        {
            string urlpath = HttpContext.Current.Request.Url.AbsolutePath;
            //var urlpathcount = urlpath.Split('/').Count();
            //if (urlpathcount > 3)
            //{
            //    var pos=urlpath.LastIndexOf("/");
            //    urlpath = urlpath.Substring(0, pos);
            //}
            //urlpath += "/IndexTree";
            string PKField = treeList.ValueField;
            string ParentField = treeList.ParentField;//	"ParentCaseCategoryID"	string
            if (paramNameType == 1)
                ParentField = PKField;


            List<TreeNode> treeNodes = treeList.TreeNodes;
            StringBuilder sb = new StringBuilder("<div id='" + ID + "' class='my-tree-menu'>");
            sb.AppendLine("<ul>");
            var root = new List<TreeNode>();
            if (treeList.RootValue == null)
                root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
            else
                root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == treeList.RootValue).ToList();
            var count = root.Count();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    List<TreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
                    int SubCount = Sublist.Count();

                    if (SubCount > 0)//非叶节点
                    {
                        var icon = "glyphicon-folder-close";
                        var selectIselected = "";
                        var bopen = false;
                        if (selectTreeNode.Contains(treeList.RootValue + "/" + root[i].Value + "/"))
                        {
                            selectIselected = " class='selected' ";
                            icon = "glyphicon-folder-open";
                            bopen = true;
                        }
                        var liclass = "";
                        if (selectTreeNode == treeList.RootValue + "/" + root[i].Value + "/")
                            liclass = "active";

                        sb.AppendLine("<li class=''>");
                        //sb.AppendLine(string.Format("<a " + selectIselected + " href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'><i class='glyphicon " + icon + "'></i>{4}</a>", urlpath + treeList.ControllerAction, "Item." + PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));
                        sb.AppendLine(string.Format("<a class='selected btn-FwBtnSubmit' data-fun='2' " + selectIselected + " href='javascript:void(0);' data-posturl='{0}?Querys[0].FieldName={1}___equal&Querys[0].Value={2}'><i class='glyphicon glyphicon-folder-close'></i>{3}</a>", urlpath, ParentField, root[i].Value, root[i].Text));

                        sb.AppendLine(JqBindTreeN(root[i], treeNodes, treeList.ControllerAction, treeList.RootValue + "/" + root[i].TreeNodeID + "/", ParentField, selectTreeNode, 1, parentLink, i == count - 1, urlpath, bopen));
                        sb.AppendLine("</li>");
                    }
                    else//叶子节点
                    {
                        var selectIselected = "";
                        var liclass = "";
                        if (selectTreeNode == treeList.RootValue + "/" + root[i].Value + "/")
                        {
                            selectIselected = " class='selected' ";
                            liclass = "active";
                        }
                        //sb.AppendLine(string.Format("<li class='" + liclass + "'><a " + selectIselected + " href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a></li>", urlpath + "/" + treeList.ControllerAction, "Item." + PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));
                        sb.AppendLine(string.Format("<li class='" + liclass + "'>"));
                        sb.AppendLine(string.Format("<a class='btn-FwBtnSubmit' data-fun='2' " + selectIselected + " href='javascript:void(0);' data-posturl='{0}?Querys[0].FieldName={1}___equal&Querys[0].Value={2}'>{3}</a>", urlpath, ParentField, root[i].Value, root[i].Text));
                        sb.AppendLine("</li>");
                    }
                }
            }

            sb.AppendLine("</ul></div>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        private static string JqBindTreeN(TreeNode treeModel, IEnumerable<TreeNode> treeModels, string controllerAction, string currentPath, string ParentField, string selectTreeNode, int lev, bool parentLink, bool blast, string urlpath, bool bopen)
        {
            StringBuilder sb = new StringBuilder();
            //if (lev == 1)
            //    sb.AppendLine("<ul class='sub' style='display: block;'>");
            //else
            //    sb.AppendLine("<ul class='sub' style='display: none;'>");
            sb.AppendLine("<ul class='sub' style='display: none;'>");
            List<TreeNode> root = treeModels.Where(p => p.ParentTreeNodeID == treeModel.TreeNodeID).ToList();
            int count = root.Count;
            for (int i = 0; i < count; i++)
            {
                List<TreeNode> Sublist = treeModels.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
                int SubCount = Sublist.Count();
                if (SubCount > 0)//非叶节点
                {
                    var selectIselected = "";
                    var icon = "glyphicon-folder-close";
                    bopen = false;
                    if (selectTreeNode.Contains(currentPath + root[i].TreeNodeID + "/"))
                    {
                        selectIselected = " class='selected' ";
                        icon = "glyphicon-folder-open";
                        bopen = true;
                    }
                    var liclass = "";
                    if (selectTreeNode == currentPath + root[i].TreeNodeID + "/")
                        liclass = "active";
                    sb.AppendLine("<li class='" + liclass + "'>");

                    //sb.AppendLine(string.Format("<a " + selectIselected + " href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'><i class='glyphicon " + icon + "'></i>{4}</a>", urlpath + controllerAction, "Item." + PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
                    //sb.AppendLine(string.Format("<a class='btn-FwBtnSubmit' data-fun='9' " + selectIselected + " href='#' data-posturl='{0}?Querys[0].FieldName={1}___equal&Querys[0].Value={2}'><i class='glyphicon " + icon + "'></i>{3}</a>", urlpath, "Item." + PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
                    sb.AppendLine(string.Format("<a class='btn-FwBtnSubmit' data-fun='2' " + selectIselected + " href='javascript:void(0);' data-posturl='{0}?Querys[0].FieldName={1}___equal&Querys[0].Value={2}'><i class='glyphicon glyphicon-folder-close'></i>{3}</a>", urlpath, ParentField, root[i].Value, root[i].Text));

                    sb.AppendLine(JqBindTreeN(root[i], treeModels, controllerAction, currentPath + root[i].TreeNodeID + "/", ParentField, selectTreeNode, lev, parentLink, i == count - 1, urlpath, bopen));
                    sb.AppendLine("</li>");
                }
                else//叶子节点
                {
                    var selectIselected = "";
                    var liclass = "";
                    if (selectTreeNode == currentPath + root[i].TreeNodeID + "/")
                    {
                        selectIselected = " class='selected' ";
                        liclass = "active";
                    }
                    //sb.AppendLine(string.Format("<li class='" + liclass + "'><a  " + selectIselected + " href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a></li>", urlpath + controllerAction, "Item." + PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
                    sb.AppendLine(string.Format("<li class='" + liclass + "'>"));
                    //sb.AppendLine(string.Format("<a  class='btn-FwBtnSubmit' data-fun='9' " + selectIselected + " href='#' data-posturl='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a>", urlpath + controllerAction, "Item." + PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
                    sb.AppendLine(string.Format("<a class='btn-FwBtnSubmit' data-fun='2' " + selectIselected + " href='javascript:void(0);' data-posturl='{0}?Querys[0].FieldName={1}___equal&Querys[0].Value={2}'>{3}</a>", urlpath, ParentField, root[i].Value, root[i].Text));
                    sb.AppendLine("</li>");
                }
            }

            sb.AppendLine("</ul>");

            return sb.ToString();
        }

        #endregion

    }
}
