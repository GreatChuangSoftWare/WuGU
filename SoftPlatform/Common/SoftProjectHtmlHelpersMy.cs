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

namespace Framework.Web.Mvc
{

    public static partial class HtmlHelpersProject
    {
        //filterContext.HttpContext.Session["CurrMenu"]
        public static MvcHtmlString Menu(this HtmlHelper helper, object obj)// MyResponseBase Model)// string ModularOrFunCode)
        {
            var Items = new List<SoftProjectAreaEntity>();
            //var past = new List<SoftProjectAreaEntity>();
            var conts = helper.ViewContext.Controller as BaseController;
            var CurrMenu = conts.CurrMenu;
            Items = conts.Menu();

            var roots = Items.Where(p => p.ParentPremID == 0).OrderBy(p => p.Sort).ToList();
            if (!string.IsNullOrEmpty( conts.MenuIdent))// != "")
                roots = Items;
            roots = roots.OrderBy(p => p.Sort).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in roots)
            {
                //如果有子节点
                var action = "";
                if (CurrMenu.IndexOf("/"+item.Design_ModularOrFunID)>=0)
                    action = "active";
                var childs = Items.Where(p => item.Design_ModularOrFunID == p.ParentPremID).OrderBy(p => p.Sort).ToList();
                if (childs.Count > 0)
                {
                    sb.AppendLine(string.Format("<li class='{0}'>",action));
                    sb.AppendLine("    <a href='#'>");
                    sb.AppendLine("        <i class='glyphicon glyphicon-th-large'></i>");
                    sb.AppendLine(string.Format("        <span>{0}</span>", item.ModularName));
                    sb.AppendLine("        <b class='icon-angle-down'></b></a>");
                    if(string.IsNullOrEmpty(action))
                        sb.AppendLine("        <ul style='display: none;' class='submenu'>");
                    else
                        sb.AppendLine("        <ul style='display: block;' class='submenu'>");

                    foreach (var item1 in childs)
                    {
                        action = "";
                        if (CurrMenu.IndexOf("/" + item.Design_ModularOrFunID+"/"+item1.Design_ModularOrFunID) >= 0)
                        {
                            action = "selected";
                        }
                        //if (item1.ActionPath == CurrMenu)
                        //    action = "active";
                        #region 参数
                        var strParam = "";
                        if (item1.ParamName != null && item1.ParamName.Length > 0 && obj != null)
                        {
                            #region 对象数据类型

                            Type type = obj.GetType();
                            #endregion

                            var paramNames = item1.ParamName.Split(',');
                            foreach (var param in paramNames)
                            {
                                PropertyInfo property = type.GetProperty(param);
                                var value = property.GetValue(obj, null);
                                strParam += "item1." + param + "=" + value;
                                //var val=item1.
                            }
                        }
                        if (strParam.Length > 0)
                            strParam = "?" + strParam;
                        #endregion
                        var ActionPath = item1.ActionPath + strParam;
                        //sb.AppendLine("            <li>");
                        //sb.AppendLine("                <a class='' href='/PurchaseOrderAreas/Pu_PurchaseOrder/WaitExamineIndex'>");
                        //sb.AppendLine("                    <span>待审核采购订单管理</span>");
                        //sb.AppendLine("                </a>");
                        //sb.AppendLine("            </li>");
                        if (item1.BUrlNva == 1)
                            sb.AppendLine(string.Format("<li ><a class='" + action + "' href='{0}'><span>{1}</span></a></li>", ActionPath, item1.ModularName));
                        else
                            sb.AppendLine(string.Format("<li ><a class='" + action + "' href='{0}'><span>{1}</span></a></li>", ActionPath, item1.MenuName));
                    }
                    sb.AppendLine("        </ul>");
                    sb.AppendLine("    </a>");
                    sb.AppendLine("</li>");
                }
                else
                {
                    #region 参数
                    var strParam = "";
                    if (item.ParamName != null && item.ParamName.Length > 0 && obj != null)
                    {
                        #region 对象数据类型

                        Type type = obj.GetType();
                        #endregion

                        var paramNames = item.ParamName.Split(',');
                        foreach (var param in paramNames)
                        {
                            PropertyInfo property = type.GetProperty(param);
                            var value = property.GetValue(obj, null);
                            strParam += "Item." + param + "=" + value;
                            //var val=item.
                        }
                    }
                    if (strParam.Length > 0)
                        strParam = "?" + strParam;
                    #endregion
                    var ActionPath = item.ActionPath + strParam;
                    if (item.BUrlNva == 1)
                        sb.AppendLine(string.Format("<li class='" + action + "'><a href='{0}'><i class='glyphicon glyphicon-th-large'></i><span>{1}</span></a></li>", ActionPath, item.ModularName));
                    else
                        sb.AppendLine(string.Format("<li class='" + action + "'><a href='{0}'><i class='glyphicon glyphicon-th-large'></i><span>{1}</span></a></li>", ActionPath, item.MenuName));
                }
            }
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        //public static MvcHtmlString Menu(this HtmlHelper helper, object obj)// MyResponseBase Model)// string ModularOrFunCode)
        //{
        //    var Items = new List<SoftProjectAreaEntity>();
        //    //var past = new List<SoftProjectAreaEntity>();
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    var CurrMenu = conts.CurrMenu;
        //    Items = conts.Menu();

        //    var roots = Items.Where(p => p.ParentPremID == 0).OrderBy(p => p.Sort).ToList();
        //    if (conts.MenuIdent != "")
        //        roots = Items;
        //    roots = roots.OrderBy(p=>p.Sort).ToList();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var item in roots)
        //    {
        //        //如果有子节点
        //        var action = "";
        //        if (item.ActionPath == CurrMenu)
        //            action = "active";
        //        var childs = Items.Where(p => item.Design_ModularOrFunID == p.ParentPremID).OrderBy(p => p.PremSort).ToList();
        //        if (childs.Count > 0)
        //        {
        //            sb.AppendLine("<li class=''>");
        //            sb.AppendLine("    <a href='#'>");
        //            sb.AppendLine("        <i class='glyphicon glyphicon-th-large'></i>");
        //            sb.AppendLine(string.Format("        <span>{0}</span>",item.ModularName));
        //            sb.AppendLine("        <b class='icon-angle-down'></b></a>");
        //            sb.AppendLine("        <ul style='display: none;' class='submenu'>");
        //            foreach (var item1 in childs)
        //            {
        //                action = "";
        //                if (item1.ActionPath == CurrMenu)
        //                    action = "active";
        //                #region 参数
        //                var strParam = "";
        //                if (item1.ParamName != null && item1.ParamName.Length > 0 && obj != null)
        //                {
        //                    #region 对象数据类型

        //                    Type type = obj.GetType();
        //                    #endregion

        //                    var paramNames = item1.ParamName.Split(',');
        //                    foreach (var param in paramNames)
        //                    {
        //                        PropertyInfo property = type.GetProperty(param);
        //                        var value = property.GetValue(obj, null);
        //                        strParam += "item1." + param + "=" + value;
        //                        //var val=item1.
        //                    }
        //                }
        //                if (strParam.Length > 0)
        //                    strParam = "?" + strParam;
        //                #endregion
        //                var ActionPath = item1.ActionPath + strParam;
        //                //sb.AppendLine("            <li>");
        //                //sb.AppendLine("                <a class='' href='/PurchaseOrderAreas/Pu_PurchaseOrder/WaitExamineIndex'>");
        //                //sb.AppendLine("                    <span>待审核采购订单管理</span>");
        //                //sb.AppendLine("                </a>");
        //                //sb.AppendLine("            </li>");
        //                if (item1.BUrlNva == 1)
        //                    sb.AppendLine(string.Format("<li class='" + action + "'><a href='{0}'><span>{1}</span></a></li>", ActionPath, item1.ModularName));
        //                else
        //                    sb.AppendLine(string.Format("<li class='" + action + "'><a href='{0}'><span>{1}</span></a></li>", ActionPath, item1.MenuName));
        //            }
        //            sb.AppendLine("        </ul>");
        //            sb.AppendLine("    </a>");
        //            sb.AppendLine("</li>");
        //        }
        //        else
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
        //            if (item.BUrlNva == 1)
        //                sb.AppendLine(string.Format("<li class='" + action + "'><a href='{0}'><i class='glyphicon glyphicon-th-large'></i><span>{1}</span></a></li>", ActionPath, item.ModularName));
        //            else
        //                sb.AppendLine(string.Format("<li class='" + action + "'><a href='{0}'><i class='glyphicon glyphicon-th-large'></i><span>{1}</span></a></li>", ActionPath, item.MenuName));
        //        }
        //    }
        //    //            <li>
        //    //    <a href="/P_ProductAreas/P_Product/IndexTechnology">
        //    //        <i class="glyphicon glyphicon-th-large"></i>
        //    //        <span>待完善商品描述</span>
        //    //    </a>
        //    //</li>

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        #region 界面控件的辅助方法

        /// <summary>
        /// 查询的下拉列表框:1015-7-5
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <param name="item"></param>
        /// <param name="strDrop"></param>
        /// <returns></returns>
        private static string QueryHtmlDropDownList(HtmlHelper helper, Querys Querys,SoftProjectAreaEntity data, SoftProjectAreaEntity item, string strDrop)
        {
            #region 下拉列表框
            var val = Querys.GetValue(item.name + "___equal");

            var Dicts = item.name;
            if (!string.IsNullOrEmpty(item.Dicts))
            {
                Dicts = item.Dicts;
            }

            if (ProjectCache.IsExistyCategory(Dicts))
            {
                var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.GetByCategory(Dicts), "DValue", "DText", val, "", "==" + item.NameCn + "==");
                strDrop = str.ToString();
            }
            else
            {
                if (ProjectCache.QueryHtmlDropDownLists.ContainsKey(item.name))
                {
                    strDrop = ProjectCache.QueryHtmlDropDownLists[item.name](val, item.NameCn, data);
                }
            }
            //else if (item.name == "Pre_RoleID")
            //{
            //    var Pre_Roles = ProjectCache.Pre_Roles;
            //    var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", Pre_Roles, "Pre_RoleID", "RoleName", val, "", "==" + item.NameCn + "==");

            //    //var str = HtmlHelpers.DropDownList(helper, "Item." + Item.name, Pre_Roles, "Pre_RoleID", "RoleName", val, "");
            //    //sbHtml.AppendLine(str.ToString());
            //    strDrop = str.ToString();
            //}
            //else if (item.name == "Comp_RoleID")
            //{
            //    var Pre_Roles = ProjectCache.Comp_Roles;
            //    var str = HtmlHelpers.DropDownList(helper, item.name + "___equal", Pre_Roles, "Comp_RoleID", "CompRoleName", val, "", "==" + item.NameCn + "==");

            //    //var str = HtmlHelpers.DropDownList(helper, "Item." + Item.name, Pre_Roles, "Pre_RoleID", "RoleName", val, "");
            //    //sbHtml.AppendLine(str.ToString());
            //    strDrop = str.ToString();
            //}

            #endregion
            return strDrop;
        }

        private static void HtmlDropDownLis(HtmlHelper helper, SoftProjectAreaEntity data, StringBuilder sbHtml, 
            Type type, SoftProjectAreaEntity field, ref PropertyInfo property, ref object value, ref string val, string css = "")
        {
            #region 下拉列表框
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

            if (ProjectCache.IsExistyCategory(dict))
            {
                var str = HtmlHelpers.DropDownList(helper, "Item." + field.name, ProjectCache.GetByCategory(dict), "DValue", "DText", val, css);
                sbHtml.AppendLine(str.ToString());
            }
            else
            {
                if (ProjectCache.HtmlDropDownLiss.ContainsKey(field.name))
                {
                    var str = ProjectCache.HtmlDropDownLiss[field.name](val, field.NameCn, data);
                    sbHtml.AppendLine(str.ToString());
                }
            }
            //else if (field.name == "Pre_RoleID")
            //{
            //    var Pre_Roles = ProjectCache.Pre_Roles;
            //    var str = HtmlHelpers.DropDownList(helper, "Item." + field.name, Pre_Roles, "Pre_RoleID", "RoleName", val, "");
            //    sbHtml.AppendLine(str.ToString());
            //}
            //else if (field.name == "Comp_RoleID")
            //{
            //    var Comp_Roles = ProjectCache.Comp_Roles;
            //    var str = HtmlHelpers.DropDownList(helper, "Item." + field.name, Comp_Roles, "Comp_RoleID", "CompRoleName", val, "");
            //    sbHtml.AppendLine(str.ToString());
            //}

            #endregion
        }

        private static void HtmlDropDownLisByTable(HtmlHelper helper, SoftProjectAreaEntity data, StringBuilder sbHtml,
    Type type, SoftProjectAreaEntity field, ref PropertyInfo property, ref object value, ref string val, string css = "")
        {
            #region 下拉列表框
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

            if (ProjectCache.IsExistyCategory(dict))
            {
                var str = HtmlHelpers.DropDownList(helper, field.name, ProjectCache.GetByCategory(dict), "DValue", "DText", val, css);
                sbHtml.AppendLine(str.ToString());
            }
            else
            {
                if (ProjectCache.HtmlDropDownLiss.ContainsKey(field.name))
                {
                    var str = ProjectCache.HtmlDropDownLiss[field.name](val, field.NameCn, data);
                    sbHtml.AppendLine(str.ToString());
                }
            }
            //else if (field.name == "Pre_RoleID")
            //{
            //    var Pre_Roles = ProjectCache.Pre_Roles;
            //    var str = HtmlHelpers.DropDownList(helper, "Item." + field.name, Pre_Roles, "Pre_RoleID", "RoleName", val, "");
            //    sbHtml.AppendLine(str.ToString());
            //}
            //else if (field.name == "Comp_RoleID")
            //{
            //    var Comp_Roles = ProjectCache.Comp_Roles;
            //    var str = HtmlHelpers.DropDownList(helper, "Item." + field.name, Comp_Roles, "Comp_RoleID", "CompRoleName", val, "");
            //    sbHtml.AppendLine(str.ToString());
            //}

            #endregion
        }

        //多选下拉列表框
        private static void DropDownListMultiSelects(HtmlHelper helper, SoftProjectAreaEntity data, StringBuilder sbHtml, 
            Type type, SoftProjectAreaEntity field, ref PropertyInfo property, ref object value, ref string val, string css = "")
        {
            #region 下拉列表框
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

            if (ProjectCache.IsExistyCategory(dict))
            {
                var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                //var str = HtmlHelpers.DropDownList(helper, "Item." + field.name, ProjectCache.GetByCategory(dict), "DValue", "DText", val, css);
                sbHtml.AppendLine(str.ToString());
            }
            else
            {
                if (ProjectCache.HtmlDropDownListMultiSelects.ContainsKey(field.name))
                {
                    var str = ProjectCache.HtmlDropDownListMultiSelects[field.name](val, field.NameCn, data);
                    sbHtml.AppendLine(str.ToString());
                }
            }
            #endregion
        }

        /// <summary>
        /// 查询条件--下拉树:2015-7-5
        /// </summary>
        /// <param name="Querys"></param>
        /// <param name="item"></param>
        /// <param name="strDrop"></param>
        /// <returns></returns>
        private static string QueryHtmlDropTree(Querys Querys,SoftProjectAreaEntity data, SoftProjectAreaEntity item, string strDrop)
        {
            var field = item;
            var val = Querys.GetValue(item.name + "___equal");

            if (ProjectCache.QueryHtmlDropTrees.ContainsKey(item.name))
            {
                strDrop = ProjectCache.QueryHtmlDropTrees[item.name](val, item.NameCn, data);
            }
            #region 原代码
            //if (field.name == "ParentOrganizationID" || field.name == "Pre_OrganizationID")
            //{
            //    #region 组织机构
            //    List<SoftProjectAreaEntity> Items = ProjectCache.Pre_Organizations;
            //    var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentOrganizationID", "Pre_OrganizationID", val, true, "");
            //    if (field.name == "ParentOrganizationID")
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "ParentOrganizationID___equal", tt, "==组织机构==");
            //        //sbHtml.AppendLine(str.ToString());
            //        strDrop = str.ToString();
            //    }
            //    else
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "Pre_OrganizationID___equal", tt, "==组织机构==");
            //        strDrop = str.ToString();
            //    }
            //    #endregion
            //}
            //else if (field.name == "ParentPreDoc_CategoryID" || field.name == "PreDoc_CategoryID")
            //{
            //    #region 公司文档类别
            //    List<SoftProjectAreaEntity> Items = ProjectCache.PreDoc_Categorys;
            //    var tt = new SelectTreeList(Items, "0", "PreDocCategoryName", "PreDoc_CategoryID", "ParentPreDoc_CategoryID", "PreDoc_CategoryID", val, true, "");
            //    if (field.name == "ParentPreDoc_CategoryID")
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "ParentPreDoc_CategoryID___equal", tt, "==文档类别==");
            //        //sbHtml.AppendLine(str.ToString());
            //        strDrop = str.ToString();
            //    }
            //    else
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "PreDoc_CategoryID___equal", tt, "==文档类别==");
            //        //sbHtml.AppendLine(str.ToString());
            //        strDrop = str.ToString();
            //    }
            //    #endregion
            //}
            //else if (field.name == "ParentCompDoc_CategoryID" || field.name == "CompDoc_CategoryID")
            //{
            //    #region 企业文档类别
            //    List<SoftProjectAreaEntity> Items = ProjectCache.CompDoc_Categorys;
            //    var tt = new SelectTreeList(Items, "0", "DocCategoryName", "CompDoc_CategoryID", "ParentCompDoc_CategoryID", "CompDoc_CategoryID", val, true, "");
            //    if (field.name == "ParentCompDoc_CategoryID")
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "ParentCompDoc_CategoryID___equal", tt, "==文档类别==");
            //        strDrop = str.ToString();
            //    }
            //    else
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "CompDoc_CategoryID___equal", tt, "==文档类别==");
            //        strDrop = str.ToString();
            //    }
            //    #endregion
            //}

            #endregion
            return strDrop;
        }

        /// <summary>
        /// 编辑页面--下拉树
        /// </summary>
        /// <param name="sbHtml"></param>
        /// <param name="field"></param>
        /// <param name="val"></param>
        private static void HtmlDropTree(StringBuilder sbHtml, SoftProjectAreaEntity data, SoftProjectAreaEntity field, string val)
        {
            //var field = item;
            //var val = Querys.GetValue(item.name + "___equal");
            string str="";
            if (ProjectCache.HtmlDropTrees.ContainsKey(field.name))
            {
                str = ProjectCache.HtmlDropTrees[field.name](val, field.NameCn,data);
                sbHtml.Append(str);
            }

            #region 下拉树:现分布在各个业务类中
            //if (field.name == "ParentOrganizationID" || field.name == "Pre_OrganizationID")
            //{
            //    #region 组织机构
            //List<SoftProjectAreaEntity> Items = ProjectCache.Pre_Organizations;
            //var tt = new SelectTreeList(Items, "0", "OrganizationName", "Pre_OrganizationID", "ParentOrganizationID", "Pre_OrganizationID", val, true, "");
            //if (field.name == "ParentOrganizationID")
            //{
            //    var str = HtmlHelpers.DropDownForTree(null, "Item.ParentOrganizationID", tt, "");
            //    sbHtml.AppendLine(str.ToString());
            //}
            //    else
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "Item.Pre_OrganizationID", tt, "");
            //        sbHtml.AppendLine(str.ToString());
            //    }
            //    #endregion
            //}
            //else if (field.name == "ParentPreDoc_CategoryID" || field.name == "PreDoc_CategoryID")
            //{
            //    #region 公司文档类别
            //    List<SoftProjectAreaEntity> Items = ProjectCache.PreDoc_Categorys;
            //    var tt = new SelectTreeList(Items, "0", "PreDocCategoryName", "PreDoc_CategoryID", "ParentPreDoc_CategoryID", "PreDoc_CategoryID", val, true, "");
            //    if (field.name == "ParentPreDoc_CategoryID")
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "Item.ParentPreDoc_CategoryID", tt, "");
            //        sbHtml.AppendLine(str.ToString());
            //    }
            //    else
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "Item.PreDoc_CategoryID", tt, "");
            //        sbHtml.AppendLine(str.ToString());
            //    }
            //    #endregion
            //}
            //else if (field.name == "ParentCompDoc_CategoryID" || field.name == "CompDoc_CategoryID")
            //{
            //    #region 企业文档类别
            //    List<SoftProjectAreaEntity> Items = ProjectCache.CompDoc_Categorys;
            //    var tt = new SelectTreeList(Items, "0", "DocCategoryName", "CompDoc_CategoryID", "ParentCompDoc_CategoryID", "CompDoc_CategoryID", val, true, "");
            //    if (field.name == "ParentCompDoc_CategoryID")
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "Item.ParentCompDoc_CategoryID", tt, "");
            //        sbHtml.AppendLine(str.ToString());
            //    }
            //    else
            //    {
            //        var str = HtmlHelpers.DropDownForTree(null, "Item.CompDoc_CategoryID", tt, "");
            //        sbHtml.AppendLine(str.ToString());
            //    }
            //    #endregion
            //}

            #endregion
        }

        public static MvcHtmlString JqTreeN(this HtmlHelper helper,SoftProjectAreaEntity data, string ID, int? paramNameType = 2)
        {
            string str = "";
            if (ProjectCache.JqTreeNs.ContainsKey(ID))
            {
                str = ProjectCache.JqTreeNs[ID](paramNameType.ToString(), "", data);
                //sbHtml.Append(str);
            }
            #region 原代码

            //List<SoftProjectAreaEntity> Items = new List<SoftProjectAreaEntity>();
            //if (ID == "Pre_OrganizationID")
            //{
            //    var Itemst = ProjectCache.Pre_Organizations;
            //    var treeList = new TreeList(Itemst, "0", "OrganizationName", "Pre_OrganizationID", "ParentOrganizationID", "", "", "Pre_OrganizationID", "", "");
            //    var str = JqTreeN(helper, ID, treeList, "", 2);
            //    return str;
            //}
            //else if (ID == "PreDoc_CategoryID")
            //{
            //    var Itemst = ProjectCache.PreDoc_Categorys;
            //    var treeList = new TreeList(Itemst, "0", "PreDocCategoryName", "PreDoc_CategoryID", "ParentPreDoc_CategoryID", "", "", "PreDoc_CategoryID", "", "");
            //    var str = JqTreeN(helper, ID, treeList, "", 2);
            //    return str;
            //}
            //else if (ID == "CompDoc_CategoryID")
            //{
            //    var Itemst = ProjectCache.PreDoc_Categorys;
            //    var treeList = new TreeList(Itemst, "0", "DocCategoryName", "CompDoc_CategoryID", "ParentCompDoc_CategoryID", "", "", "CompDoc_CategoryID", "", "");
            //    var str = JqTreeN(helper, ID, treeList, "", 2);
            //    return str;
            //}
            #endregion
            return new MvcHtmlString(str);
        }

        #endregion
    }
}
