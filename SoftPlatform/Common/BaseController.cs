using Framework.Web.Mvc;
using Healthcare.Framework.Web.Mvc;
using SoftProject.CellModel;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    [MyHandleError]
    [MyAuthorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
            ////this.ActionInvoker = new CustomActionInvoker();
            this.Breadcrumb = new Breadcrumb() { Items = new List<BreadcrumbItem>() };
            this.Menus = new Breadcrumb() { Items = new List<BreadcrumbItem>() };
        }

        public SoftProjectAreaEntity LoginInfo
        {
            get
            {
                SoftProjectAreaEntity loginInfo = Session["LoginInfo"] as SoftProjectAreaEntity;
                return loginInfo;
            }
        }

        public string CurrMenu
        {
            get
            {
                var currMenuActionPath = HttpContext.Session["CurrMenu"].ToString();
                return currMenuActionPath;
            }
        }

        public string ModularOrFunCode
        {
            get;
            set;
        }

        public string ModularOrFunCodeEdit
        {
            get;
            set;
        }

        public SoftProjectAreaEntity Design_ModularOrFun
        {
            get
            {
                //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == ActionName).FirstOrDefault();
                var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                return Design_ModularOrFun;
            }
        }

        /// <summary>
        /// 针对主从表：主表
        /// </summary>
        public SoftProjectAreaEntity Design_ModularOrFunEdit
        {
            get
            {
                //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == ActionName).FirstOrDefault();
                var Design_ModularOrFunEdit = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCodeEdit).FirstOrDefault();

                return Design_ModularOrFunEdit;
            }
        }

        public string TabModularOrFunCode { get; set; }

        public string MenuIdent { get; set; }

        /// <summary>
        /// 工具条按钮
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ModularOrFunCode"></param>
        /// <param name="OperPos"></param>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> ToolBarBtns(object item, string ModularOrFunCode, int? OperPos)
        {
            //var passbtns = new List<SoftProjectAreaEntity>();
            var btns = LoginModulerBtns(ModularOrFunCode, OperPos);

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

            //var type = item.GetType();
            //#region 按钮显示条件比较
            //for (var i = 0; i < btns.Count; i++)//var btn in btns)
            //{
            //    var btn = btns[i];
            //    if (!string.IsNullOrEmpty(btn.DispConditionsExpression))
            //    {
            //        var DispConditionsExpressionArr = btn.DispConditionsExpression.Split('|');
            //        #region 第1个数的值
            //        PropertyInfo property = type.GetProperty(DispConditionsExpressionArr[1]);
            //        var value1 = property.GetValue(item, null);
            //        if (value1 == null)
            //            throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
            //        var strValue1 = value1.ToString();
            //        #endregion

            //        #region 第2个数的值
            //        var strValue2 = DispConditionsExpressionArr[3];
            //        if (DispConditionsExpressionArr[0] == "2")
            //        {
            //            property = type.GetProperty(DispConditionsExpressionArr[3]);
            //            var value2 = property.GetValue(item, null);
            //            if (value2 == null)
            //                throw new Exception("按钮显示条件控制错误：【" + DispConditionsExpressionArr[1] + "】值不能为空!");
            //            strValue2 = value2.ToString();
            //        }
            //        #endregion
            //        #region 比较运算
            //        switch (DispConditionsExpressionArr[2])
            //        {
            //            case "equal":
            //                if (strValue1 != DispConditionsExpressionArr[3])
            //                    continue;
            //                break;
            //        }
            //        #endregion
            //        passbtns.Add(btn);
            //    }
            //}
            //#endregion
            btnsrows = btnsrows.Distinct().ToList();
            return btnsrows;
        }

        /// <summary>
        /// 登录人，某页面位置按钮
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ModularCodes"></param>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> LoginModulerBtns(string ModularOrFunCode, int? OperPos)// Design_ModularOrFunControllID)
        {
            //var type = item.GetType();
            //判断类型：是用户，还是客户等
            //如果为用户
            var past = new List<SoftProjectAreaEntity>();
            var LoginCategoryID=(int)LoginInfo.LoginCategoryID;

            past=ProjectCache.LoginModulerBtns[LoginCategoryID]((int)LoginInfo.Sys_LoginInfoID, ModularOrFunCode, OperPos);
            //if (LoginInfo.LoginCategoryID == 1)
            //{
            //    past = AuthorizationAreasAreaRegistration.LoginModulerBtns((int)LoginInfo.Sys_LoginInfoID, ModularOrFunCode, OperPos);
            //}
            //else if (LoginInfo.LoginCategoryID == 2)//如果为客户
            //{
            //    past = CompanyAreasAreaRegistration.LoginModulerBtnsByComp((int)LoginInfo.Sys_LoginInfoID, ModularOrFunCode, OperPos);
            //}
            var pastdists = past.Select(p => new
            {
                p.Design_ModularOrFunBtnID,
                p.Design_ModularOrFunID,
                p.Sort,
                p.BtnNameEn,
                p.BtnNameCn,
                p.OperPos,
                p.BtnBehavior,
                p.PopupAfterTableFun,
                p.MastEditArea,
                p.ChildtableSelect,
                //p.ParamName_del,
                p.DispConditionsExpression,
                p.OperConditionsExpression,
                p.TargetDom,
                p.TableSelect,
                p.PopupWidth,
                //p.Design_ModularPageID,
                p.RefDataArea,
                p.popupaddrepeat,
                p.BtnSearchMethod,
                p.bValid,
                p.BtnType,
                p.ModularOrFunBtnRemark
            }).Distinct().ToList();

            List<SoftProjectAreaEntity> results = new List<SoftProjectAreaEntity>();

            pastdists.ForEach(p =>
            {
                var item = new SoftProjectAreaEntity
                {
                    Design_ModularOrFunBtnID = p.Design_ModularOrFunBtnID,
                    Design_ModularOrFunID = p.Design_ModularOrFunID,
                    Sort = p.Sort,
                    BtnNameEn = p.BtnNameEn,
                    BtnNameCn = p.BtnNameCn,
                    OperPos = p.OperPos,
                    BtnBehavior = p.BtnBehavior,
                    PopupAfterTableFun = p.PopupAfterTableFun,
                    MastEditArea = p.MastEditArea,
                    ChildtableSelect = p.ChildtableSelect,
                    //p.ParamName_del,
                    DispConditionsExpression = p.DispConditionsExpression,
                    OperConditionsExpression = p.OperConditionsExpression,
                    TargetDom = p.TargetDom,
                    TableSelect = p.TableSelect,
                    PopupWidth = p.PopupWidth,
                    //p.Design_ModularPageID,
                    RefDataArea = p.RefDataArea,
                    popupaddrepeat = p.popupaddrepeat,
                    BtnSearchMethod = p.BtnSearchMethod,
                    bValid = p.bValid,
                    BtnType = p.BtnType,
                    ModularOrFunBtnRemark = p.ModularOrFunBtnRemark
                };
                results.Add(item);
            });

            return results;
        }

        public Breadcrumb Breadcrumb { get; set; }

        public string MenuCode { get; set; }

        public Breadcrumb Menus { get; set; }

        public SoftProjectAreaEntity MenuFun
        {
            get
            {
                //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == ActionName).FirstOrDefault();
                var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == MenuCode).FirstOrDefault();

                return Design_ModularOrFun;
            }
        }

        public int? MenuPostion()
        {
            if (MenuFun != null)
            {
                return MenuFun.MenuPostion;
            }
            return 0;
        }

        public List<SoftProjectAreaEntity> Menu()
        {
            var Items = new List<SoftProjectAreaEntity>();
            var LoginCategoryID = (int)LoginInfo.LoginCategoryID;

            //Items = ProjectCache.LoginModulerMenus[LoginCategoryID]((int)LoginInfo.Sys_LoginInfoID);
            
            if (Design_ModularOrFun != null)
                MenuIdent = Design_ModularOrFun.MenuIdent;
            //if(string.IsNullOrEmpty( MenuIdent))
            //    MenuIdent = "MainMenu";
            Items = AuthorizationAreasAreaRegistration.LoginModulerMenu((int)LoginInfo.Sys_LoginInfoID, MenuIdent);

            #region 原代码

            //var MenuPanelItem = MenuPanel(Design_ModularOrFun);
            //Items = ProjectCache.Design_ModularOrFuns.Where(p => p.ParentPremID == MenuFun.Design_ModularOrFunID && p.BMenu == 1).OrderBy(p => p.PremSort).ToList();

            //if (MenuFun.BMenu == 1)
            //{
            //    #region 参数
            //    var strParam = "";
            //    if (MenuFun.ParamName != null && MenuFun.ParamName.Length > 0 && obj != null)
            //    {
            //        #region 对象数据类型

            //        Type type = obj.GetType();
            //        #endregion

            //        var paramNames = MenuFun.ParamName.Split(',');
            //        foreach (var param in paramNames)
            //        {
            //            PropertyInfo property = type.GetProperty(param);
            //            var value = property.GetValue(obj, null);
            //            strParam += "Item." + param + "=" + value;
            //            //var val=item.
            //        }
            //    }
            //    if (strParam.Length > 0)
            //        strParam = "?" + strParam;
            //    #endregion
            //    var ActionPath = MenuFun.ActionPath + strParam;
            //    //strNav += string.Format("<li><a href='{0}{1}'>{2}</a></li>", modular.ActionPath, strParam, modular.ModularName);

            //    Menus.Items.Add(new BreadcrumbItem { Name = MenuFun.ModularName, URL = ActionPath });
            //}
            #endregion

            //var roots = Items.Where(p => p.ParentPremID == 0).ToList();
            //StringBuilder sb = new StringBuilder();
            //foreach (var item in roots)
            //{
            //    //如果有子节点

            //    var childs = Items.Where(p => item.Design_ModularOrFunID == p.ParentPremID).ToList();
            //    if (childs.Count > 0)
            //    {
            //        sb.AppendLine(string.Format("<li><font color='red'>{0}</font></li>",item.ModularName));
            //        foreach (var item1 in childs)
            //        {
            //            #region 参数
            //            var strParam = "";
            //            if (item.ParamName != null && item.ParamName.Length > 0 && obj != null)
            //            {
            //                #region 对象数据类型

            //                Type type = obj.GetType();
            //                #endregion

            //                var paramNames = item.ParamName.Split(',');
            //                foreach (var param in paramNames)
            //                {
            //                    PropertyInfo property = type.GetProperty(param);
            //                    var value = property.GetValue(obj, null);
            //                    strParam += "Item." + param + "=" + value;
            //                    //var val=item.
            //                }
            //            }
            //            if (strParam.Length > 0)
            //                strParam = "?" + strParam;
            //            #endregion
            //            var ActionPath = item.ActionPath + strParam;
            //            sb.AppendLine(string.Format("<li><a href='{0}'>{1}</a></li>", ActionPath, item.ModularName));
            //        }
            //    }
            //    else
            //    {
            //        #region 参数
            //        var strParam = "";
            //        if (item.ParamName != null && item.ParamName.Length > 0 && obj != null)
            //        {
            //            #region 对象数据类型

            //            Type type = obj.GetType();
            //            #endregion

            //            var paramNames = item.ParamName.Split(',');
            //            foreach (var param in paramNames)
            //            {
            //                PropertyInfo property = type.GetProperty(param);
            //                var value = property.GetValue(obj, null);
            //                strParam += "Item." + param + "=" + value;
            //                //var val=item.
            //            }
            //        }
            //        if (strParam.Length > 0)
            //            strParam = "?" + strParam;
            //        #endregion
            //        var ActionPath = item.ActionPath + strParam;
            //        //<li><a href="/AuthorizationAreas/Pre_User/Index">用户管理</a></li>
            //        sb.AppendLine(string.Format("<li><a href='{0}'>{1}</a></li>", ActionPath, item.ModularName));
            //    //<li><font color="red">权限管理</font></li>
            //    //<li><a href="/AuthorizationAreas/Pre_User/Index">用户管理</a></li>
            //    //<li><a href="/AuthorizationAreas/Pre_Role/Index">角色管理</a></li>
            //    }
            //    //Menus.Items.Add(new BreadcrumbItem { Name = item.ModularName, URL = ActionPath });
            //}
            //return sb.ToString();
            return Items;
        }


        //public static MvcHtmlString MainMenu(this HtmlHelper html)
        //{
        //    var controll = html.ViewContext.Controller as BaseController;
        //    //var html.GetRouteString("Controller").ToLower()
        //    //var area = "";
        //    //if (controll.RouteData.DataTokens["area"] != null)
        //    //    area = controll.RouteData.DataTokens["area"].ToString();
        //    //var constrollName = controll.RouteData.Values["controller"].ToString();
        //    //var actionName = controll.RouteData.Values["action"].ToString();
        //    List<Sys_PremSet> Sys_PremSets = controll.LoginMenu();


        //    var currpath = "";
        //    if (HttpContext.Current.Session["MenuUrl"] != null)
        //        currpath = HttpContext.Current.Session["MenuUrl"].ToString();
        //    ////根据Action，选中路径
        //    //var Sys_PremSetsAll = controll.Sys_PremSets;//所有角色权限权限集
        //    //var premSet = Sys_PremSetsAll.Where(p => p.Area == area && p.ConstrollName == constrollName && p.ActionName == actionName).First();
        //    //if (premSet.PermSetCategory == 0)
        //    //{
        //    //    var temp1 = Sys_PremSets.Where(p => p.Sys_PremCodeID == premSet.PermSetParentID).First();
        //    //    //var temp2 = Sys_PremSets.Where(p => p.Sys_PremCodeID == temp1.PermSetParentID).First();
        //    //    currpath = temp1.PermSetParentID + "/" + temp1.Sys_PermSetID + "/";
        //    //}
        //    //else
        //    //    currpath = premSet.PermSetParentID + "/" + premSet.Sys_PermSetID + "/";
        //    //var ActionSys_PremSet = Sys_PremSets.Where(p => p.Area == area && p.ConstrollName == constrollName && p.ActionName == actionName).First();
        //    //var ActionSys_PremSet = Sys_PremSets.Where(p => p.Area == area && p.ConstrollName == constrollName && p.ActionName == actionName).First();
        //    //var currpath =ActionSys_PremSet.PermSetParentID + "/" + ActionSys_PremSet.Sys_PermSetID+"/";//
        //    //获取路径
        //    var path = controll.Request.ApplicationPath;

        //    StringBuilder sb = new StringBuilder();//string.Format("<ul id='{0}' class='easyui-tree'>", name));

        //    sb.AppendLine("<ul id='mymenu' class='mymenu'>");

        //    //<li class='active'>
        //    //        <a href='/Home/Index'>
        //    //            <i class='glyphicon glyphicon-th-large'></i>
        //    //            <span>首页</span>
        //    //        </a>
        //    //        <ul class='submenu'>
        //    //        </ul>
        //    //    </li>

        //    if (currpath.Contains("/Home/Index"))
        //        sb.AppendLine("<li class='active'>");
        //    else
        //        sb.AppendLine("<li class=''>");
        //    sb.AppendLine("    <a href='/'>");
        //    sb.AppendLine("        <i class='glyphicon glyphicon-th-large'></i>");
        //    sb.AppendLine("        <span>首页</span>");
        //    sb.AppendLine("    <b class='icon-angle-down'></b></a>");
        //    sb.AppendLine("<ul class='submenu' style='display: block;'>");
        //    sb.AppendLine("</ul>");
        //    sb.AppendLine("</li>");

        //    var root = Sys_PremSets.Where(p => p.PermSetParentID == 0).OrderBy(p => p.OrderNum).ToList();
        //    var count = root.Count();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            var active = "";
        //            var bopen = false;
        //            if (currpath.Contains("/" + root[i].Sys_PermSetID + "/"))
        //            {
        //                active = "active";
        //                bopen = true;
        //            }
        //            sb.AppendLine("<li class='" + active + "'>");
        //            //sb.AppendLine("    <a class='" + selected + "'  href='" + path + root[i].ActionPath + "'>");
        //            if (root[i].ActionPath == "")
        //                sb.AppendLine("    <a href='#'>");
        //            else
        //                sb.AppendLine("    <a href='/" + root[i].ActionPath + "'>");

        //            sb.AppendLine("        <i class='glyphicon glyphicon-th-large'></i>");
        //            sb.AppendLine("        <span>" + root[i].PermSetName + "</span>");
        //            sb.AppendLine("    </a>");
        //            sb.AppendLine(BindMainMenu(Sys_PremSets, (int)root[i].Sys_PermSetID, currpath, path, bopen));
        //            sb.AppendLine("</li>");
        //        }
        //    }

        //    sb.AppendLine("</ul>");//结束

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindMainMenu(List<Sys_PremSet> Sys_PremSets, int Sys_PermSetID, string currpath, string path, bool bopen)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (bopen)
        //        sb.AppendLine("<ul class='submenu' style='display: block;'>");
        //    else
        //        sb.AppendLine("<ul class='submenu'>");
        //    var root = Sys_PremSets.Where(p => p.PermSetParentID == Sys_PermSetID).OrderBy(p => p.OrderNum).ToList();
        //    int count = root.Count;
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            var selected = "";
        //            if (currpath == "/" + Sys_PermSetID + "/" + root[i].Sys_PermSetID + "/")
        //                selected = "selected";

        //            sb.AppendLine("<li>");
        //            sb.AppendLine("    <a class='" + selected + "'  href='" + path + root[i].ActionPath + "'>");
        //            sb.AppendLine("        <span>" + root[i].PermSetName + "</span>");
        //            sb.AppendLine("    </a>");
        //            sb.AppendLine("</li>");

        //        }
        //    }
        //    sb.AppendLine("</ul>");

        //    return sb.ToString();
        //}



        //public Breadcrumb Menu(SoftProjectAreaEntity item)
        //{
        //    //var conts = ViewContext.Controller as BaseController;

        //    Menus.Items.Clear();
        //    if (MenuFun == null)
        //        return Menus;
        //    var Items = new List<SoftProjectAreaEntity>();

        //    var btns = LoginModulerBtns(MenuCode, 1);

        //    var sbhtml = new StringBuilder();
        //    foreach (var btn in btns)
        //    {
        //        var controllss = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
        //        var action = "";
        //        if (controllss[0].ActionPath == Request.Path)
        //            action = "active";
        //        var url = UrlByControll(item, controllss.First());
        //        //sbhtml.AppendLine("<li class='" + action + "'>");
        //        //sbhtml.AppendLine(string.Format("<a href='{0}'>{1}</a>", url, btn.BtnNameCn));
        //        //sbhtml.AppendLine("</li>");
        //        ////////////////////////////////////////////////////////
        //        Menus.Items.Add(new BreadcrumbItem { Name = btn.BtnNameCn, URL = url });
        //    }
        //    var strHtml = new MvcHtmlString(sbhtml.ToString());
        //    return Menus;
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
        //    //var url = "/" + controlls.AreasCode + "/" + controlls.ControllCode + "/" + controlls.ActionMethod + strParam;
        //    var url = controlls.ActionPath + strParam;
        //    return url;
        //}

        //public int? MenuPostion()
        //{
        //    if (Design_ModularOrFun != null)
        //    {
        //        var MenuPanelItem = MenuPanel(Design_ModularOrFun);
        //        //var MenuPanel = Design_ModularOrFun;
        //        //var Items = new List<SoftProjectAreaEntity>();
        //        //if (MenuPanel.BMenuPanel != 1)//是控制面板
        //        //{
        //        //    //MenuPanel = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == MenuPanel.Design_ModularOrFunParentID).FirstOrDefault();
        //        //    MenuPanel = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == MenuPanel.ParentPremID).FirstOrDefault();
        //        //}
        //        //var respItem = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == LoginInfo.CurrMenuModularOrFunID).FirstOrDefault();
        //        return MenuPanelItem.MenuPostion;
        //    }
        //    return 0;
        //}

        //public Breadcrumb Menu(SoftProjectAreaEntity obj)
        //{
        //    Menus.Items.Clear();
        //    if (Design_ModularOrFun == null)
        //        return Menus;
        //    var Items = new List<SoftProjectAreaEntity>();

        //    var MenuPanelItem = MenuPanel(Design_ModularOrFun);
        //    Items = ProjectCache.Design_ModularOrFuns.Where(p => p.ParentPremID == MenuPanelItem.Design_ModularOrFunID && p.BMenu == 1).OrderBy(p=>p.Sort).ToList();

        //    if (MenuPanelItem.BMenu == 1)
        //    {
        //        #region 参数
        //        var strParam = "";
        //        if (MenuPanelItem.ParamName != null && MenuPanelItem.ParamName.Length > 0 && obj != null)
        //        {
        //            #region 对象数据类型

        //            Type type = obj.GetType();
        //            #endregion

        //            var paramNames = MenuPanelItem.ParamName.Split(',');
        //            foreach (var param in paramNames)
        //            {
        //                PropertyInfo property = type.GetProperty(param);
        //                var value = property.GetValue(obj, null);
        //                strParam += "Item." + param + "=" + value;
        //                //var val=item.
        //            }
        //        }
        //        if (strParam.Length > 0)
        //            strParam = "?" + strParam;
        //        #endregion
        //        var ActionPath = MenuPanelItem.ActionPath + strParam;
        //        //strNav += string.Format("<li><a href='{0}{1}'>{2}</a></li>", modular.ActionPath, strParam, modular.ModularName);

        //        Menus.Items.Add(new BreadcrumbItem { Name = MenuPanelItem.ModularName, URL = ActionPath });
        //    }
        //    foreach (var item in Items)
        //    {
        //        #region 参数
        //        var strParam = "";
        //        if (item.ParamName != null && item.ParamName.Length > 0 && obj != null)
        //        {
        //            #region 对象数据类型

        //            Type type = obj.GetType();
        //            #endregion

        //            var paramNames = item.ParamName.Split(',');
        //            foreach (var param in paramNames)
        //            {
        //                PropertyInfo property = type.GetProperty(param);
        //                var value = property.GetValue(obj, null);
        //                strParam += "Item." + param + "=" + value;
        //                //var val=item.
        //            }
        //        }
        //        if (strParam.Length > 0)
        //            strParam = "?" + strParam;
        //        #endregion
        //        var ActionPath = item.ActionPath + strParam;
        //        Menus.Items.Add(new BreadcrumbItem { Name = item.ModularName, URL = ActionPath });
        //    }
        //    return Menus;
        //}

        public SoftProjectAreaEntity MenuPanel(SoftProjectAreaEntity MenuPanelItem)
        {
            if (MenuPanelItem.BMenuPanel == 1 || MenuPanelItem.ParentPremID == 0)
                return MenuPanelItem;
            MenuPanelItem = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == MenuPanelItem.ParentPremID).FirstOrDefault();
            return MenuPanel(MenuPanelItem);
        }

        //public Breadcrumb Menu()
        //{
        //    var respItem = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == LoginInfo.CurrMenuModularOrFunID).FirstOrDefault();
        //    var Items = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunParentID == LoginInfo.CurrMenuModularOrFunID && p.BMenu == 1);//.FirstOrDefault();

        //    Menus.Items.Clear();
        //    if (string.IsNullOrEmpty(ModularOrFunCode))
        //    {
        //        Menus.Items = new List<BreadcrumbItem> { 
        //            new BreadcrumbItem{Name=respItem.ModularName,URL="/Home/SubPanel?Design_ModularOrFunID="+LoginInfo.CurrMenuModularOrFunID,ActionCss="action selected"},
        //        };
        //    }
        //    else
        //        Menus.Items = new List<BreadcrumbItem> { 
        //            new BreadcrumbItem{Name=respItem.ModularName,URL="/Home/SubPanel?Design_ModularOrFunID="+LoginInfo.CurrMenuModularOrFunID},
        //        };

        //    foreach (var item in Items)
        //    {
        //        if (item.ModularOrFunCode == ModularOrFunCode)
        //            Menus.Items.Add(new BreadcrumbItem { Name = item.ModularName, URL = item.ActionPath, ActionCss = "action selected" });
        //        else
        //            Menus.Items.Add(new BreadcrumbItem { Name = item.ModularName, URL = item.ActionPath });
        //    }
        //    return Menus;
        //}

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.Breadcrumb.Root = new BreadcrumbItem() { Name = "首页", URL = Url.Content("~/") };
        }

    }
}