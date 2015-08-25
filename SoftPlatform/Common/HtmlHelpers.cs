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
    public static class HtmlHelpers
    {
        static string AbsolutePath = HttpContext.Current.Request.Url.AbsolutePath;

        #region 角色-权限分配

        /// <summary>
        /// 权限分配表
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString PermAssignTable(this HtmlHelper html, string idname, List<SoftProjectAreaEntity> Items, string classCss = "form-control", string disabled = "", string otherPro = "")
        {
            var controll = html.ViewContext.Controller as BaseController;
            var Pre_RolesPremSets = Items;//角色对应的
            StringBuilder sb = new StringBuilder();//string.Format("<ul id='{0}' class='easyui-tree'>", name));
            sb.AppendLine("<div class='mylist'>");
            sb.AppendLine("<table id='PermAssignTable' class='table table-hover table-striped table-condensed table-bordered'>");

            sb.AppendLine("<thead><tr><th>功能权限</th><th>数据权限</th></tr></thead>");
            //data-collsname='Pre_RolePremSets'
            sb.AppendLine("<tbody  data-pre_rolepremsetid='Pre_RolePremSetID'  data-pre_roleid='Pre_RoleID' data-sys_premsetid='Sys_PremSetID' >");
            //权限码根节点
            var root = Pre_RolesPremSets.Where(p => p.PremSetParentID == 0).OrderBy(p => p.OrderNum).ToList();
            var count = root.Count();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    //if (root[i].Sys_PermSetID == 1)
                    //    continue;
                    var Sublist = Pre_RolesPremSets.Where(p => p.PremSetParentID == root[i].Sys_PremSetID).ToList();
                    int SubCount = Sublist.Count();

                    #region 是否选中

                    var bcheck = "checked";
                    if (root[i].bCheckSelect == 0)
                    {
                        bcheck = "";
                    }
                    #endregion

                    #region 数据权限
                    #region 数据权限下拉列表框，判断此权限码节点--是否需要数据权限

                    var strDataRightstemp = "";
                    if (!string.IsNullOrEmpty(root[i].DataRightDropDown))
                    {
                        strDataRightstemp = DataRightDropDown(root[i].DataRightDropDown, root[i].Sys_PremCodeID, classCss, otherPro, disabled);
                    }

                    #endregion

                    if (root[i].DataRightValue != null)
                    {
                        var strreplace = string.Format("value='{0}'", root[i].DataRightValue);
                        strDataRightstemp = strDataRightstemp.Replace(strreplace, strreplace + " selected");
                    }

                    #endregion

                    if (SubCount > 0)
                    {//Pre_RolePremSetID,Pre_RoleID,Sys_PermSetID
                        sb.AppendLine(string.Format("<tr data-pre_rolepremsetid='{0}'  data-pre_roleid='{1}' data-sys_premsetid='{2}'>", root[i].Pre_RolePremSetID, root[i].Pre_RoleID, root[i].Sys_PremSetID));
                        //sb.AppendLine("<tr >");
                        var temp = string.Format("<td class='align-left'><i class='tabletreeplus btn-Expcoll '></i><label><input " + disabled + " name='{0}'  id='{0}' " + bcheck + " type='checkbox' value='{1}' data-parentid='{2}' class='jq-checkall-item'  />{3}</label></td>",
                            idname, root[i].Sys_PremSetID, root[i].PremSetParentID, root[i].PremSetName);
                        sb.AppendLine(temp);
                        //数据权限列
                        sb.AppendLine("<td class='align-left'>" + strDataRightstemp + "</td>");
                        sb.AppendLine("</tr>");
                        sb.AppendLine(BindPermAssignTable(Pre_RolesPremSets, (int)root[i].Sys_PremSetID, idname, 1, classCss, otherPro, disabled));
                    }
                    else
                    {//root[i].Value  checkbox//根据层：获取空格数       
                        //sb.AppendLine(string.Format("<li><a href='{0}'>{1}</a></li>", path + root[i].ActionPath, root[i].ModularName));
                        sb.AppendLine(string.Format("<tr data-pre_rolepremsetid='{0}'  data-pre_roleid='{1}' data-sys_premsetid='{2}'>", root[i].Pre_RolePremSetID, root[i].Pre_RoleID, root[i].Sys_PremSetID));
                        //sb.AppendLine("<tr >");
                        var temp = string.Format("<td class='align-left'><label><input " + disabled + " name='{0}' id='{0}' " + bcheck + " type='checkbox' value='{1}' data-parentid='{2}' class='jq-checkall-item'  />{3}</label></td>",
                            idname, root[i].Sys_PremSetID, root[i].PremSetParentID, root[i].PremSetName);
                        sb.AppendLine(temp);
                        sb.AppendLine("<td class='align-left'>" + strDataRightstemp + "</td>");
                        sb.AppendLine("</tr>");
                    }
                }
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        private static string BindPermAssignTable(List<SoftProjectAreaEntity> Pre_RolesPremSets, int Sys_PremSetID, string idname, int lev,
            string classCss, string otherPro, string disabled)
        {
            StringBuilder sb = new StringBuilder();

            lev++;//下一层
            var root = Pre_RolesPremSets.Where(p => p.PremSetParentID == Sys_PremSetID).OrderBy(p => p.OrderNum).ToList();
            int count = root.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        var Sublist = Pre_RolesPremSets.Where(p => p.PremSetParentID == root[i].Sys_PremSetID).ToList();
                        int SubCount = Sublist.Count();
                        #region 是否选中

                        var bcheck = "checked='checked'";
                        if (root[i].bCheckSelect == 0)
                        {
                            bcheck = "";
                        }
                        #endregion

                        #region 数据权限
                        #region 数据权限下拉列表框，判断此权限码节点--是否需要数据权限

                        var strDataRightstemp = "";
                        if (!string.IsNullOrEmpty(root[i].DataRightDropDown))
                        {
                            strDataRightstemp = DataRightDropDown(root[i].DataRightDropDown, root[i].Sys_PremCodeID, classCss, otherPro, disabled);
                        }

                        #endregion

                        if (root[i].DataRightValue != null)
                        {
                            var strreplace = string.Format("value='{0}'", root[i].DataRightValue);
                            strDataRightstemp = strDataRightstemp.Replace(strreplace, strreplace + " selected");
                        }

                        #endregion

                        if (SubCount > 0)//非叶节点
                        {
                            var width = 30 * lev;
                            var labwidth = "<label style='width:" + width + "px;'></label>";

                            sb.AppendLine(string.Format("<tr class='hide' data-pre_rolepremsetid='{0}'  data-pre_roleid='{1}' data-sys_premsetid='{2}'>", root[i].Pre_RolePremSetID, root[i].Pre_RoleID, root[i].Sys_PremSetID));
                            //sb.AppendLine("<tr class='hide'>");
                            var sss = string.Format("<td class='align-left'>" + labwidth + "<i class='tabletreeplus btn-Expcoll '></i><label><input " + disabled + " name='{0}' id='{0}' " + bcheck + " type='checkbox' value='{1}' data-parentid='{2}' class='jq-checkall-item'  />{3}</label></td>",
                                idname, root[i].Sys_PremSetID, root[i].PremSetParentID, root[i].PremSetName);
                            sb.AppendLine(sss);

                            sb.AppendLine("<td class='align-left'>" + strDataRightstemp + "</td>");
                            sb.AppendLine("</tr>");
                            sb.AppendLine(BindPermAssignTable(Pre_RolesPremSets, (int)root[i].Sys_PremSetID, idname, (lev), classCss, otherPro, disabled));
                        }
                        else
                        {
                            var width = 30 * lev + 19;
                            var labwidth = "<label style='width:" + width + "px;'></label>";

                            sb.AppendLine(string.Format("<tr class='hide' data-pre_rolepremsetid='{0}'  data-pre_roleid='{1}' data-sys_premsetid='{2}'>", root[i].Pre_RolePremSetID, root[i].Pre_RoleID, root[i].Sys_PremSetID));
                            //sb.AppendLine("<tr class='hide'>");
                            //sb.AppendLine("<tr class=''>");
                            var temp = string.Format("<td class='align-left'>" + labwidth + "<label><input " + disabled + " name='{0}' id='{0}' " + bcheck + " type='checkbox' value='{1}' data-parentid='{2}' class='jq-checkall-item'  />{3}</label></td>", idname,
                                root[i].Sys_PremSetID, root[i].PremSetParentID, root[i].PremSetName);
                            sb.AppendLine(temp);
                            sb.AppendLine("<td class='align-left'>" + strDataRightstemp + "</td>");
                            sb.AppendLine("</tr>");
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return sb.ToString();
        }

        private static string DataRightDropDown(string dictCategory, int? Sys_PremCodeID, string classCss, string otherPro, string disabled)
        {
            #region 数据权限下拉列表框

            //var dictCategory = root[i].DataRightDropDown;
            var DataRights = ProjectCache.GetByCategory(dictCategory);

            var strDataRights = string.Format("<select   name='DataRight' id='DataRight' data-premcodeid='" + Sys_PremCodeID + "'  class='{0}' {1} {2}  >", classCss, otherPro, disabled);
            strDataRights += "<option value=''>--请选择--</option>";

            foreach (var item in DataRights)
            {
                strDataRights += string.Format("<option value='{0}'>{1}</option>", item.DValue, item.DText);
            }
            strDataRights += "</select>";

            #endregion
            return strDataRights;
        }

        #endregion

        //#region 列表页面

        //public static MvcHtmlString ToHtml(this HtmlHelper helper, Func<object, BaseController, string> func, IEnumerable<object> Items, List<RankInfo> RankInfos, int posTotal = 1)
        //{
        //    //基础类
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    var TableHeadInfos = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == conts.PageFunNameEn && (p.HeadOrDataType & 2) == 2).OrderBy(p => p.TableInfoSort).ToList();
        //    //可以不排序
        //  //var TableTrDataInfos = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == conts.PageFunNameEn && (p.HeadOrDataType & 1) == 1).OrderBy(p => p.TableInfoSort).ToList();
        //    //var TableTrDataInfos1 = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == conts.PageFunNameEn).ToList();//.OrderBy(p => p.TableInfoSort).ToList();

        //    var TableTrDataInfos = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == conts.PageFunNameEn && ((p.HeadOrDataType & 1) == 1)).OrderBy(p => p.TableInfoSort).ToList();

        //    StringBuilder sbHtml = new StringBuilder();
        //    #region 表头
        //    StringBuilder sbHead = new StringBuilder("<thead id='tbhead'>");

        //    var count = Items.Count();

        //    #region 显示合计行

        //    if (posTotal == 0)//开始处
        //    {
        //        if (count > 0)
        //        {
        //            var item = Items.Last();//[count - 1];
        //            Type type = item.GetType();
        //            var strhead = WriteRowThTotalHtml(TableHeadInfos, item, type);
        //            sbHead.AppendLine(strhead);
        //        }
        //    }
        //    #endregion

        //    #region 表头

        //    //写入表头
        //    var ths = WriteRowThHtml(TableHeadInfos, helper, RankInfos);
        //    sbHead.AppendLine(ths);
        //    sbHead.AppendLine("</thead>");
        //    //sbHtml.AppendLine(ths.ToString());

        //    #endregion

        //    #endregion

        //    #region 主体

        //    StringBuilder sbBody = new StringBuilder("<tbody id='tbbody' ");

        //    for (var i = 0; i < TableTrDataInfos.Count; i++)
        //    {
        //        var datalower = TableTrDataInfos[i].NameEn.ToLower();
        //        var NameEn = TableTrDataInfos[i].NameEn;
        //        if (TableTrDataInfos[i].bOperLog == 1)
        //            NameEn = "OperLogIdent";
        //        sbBody.Append(string.Format(" data-{0}='{1}' ", datalower, NameEn));
        //    }
        //    sbBody.Append(">");

        //    int x = 0;
        //    if (count > 1)
        //    {
        //        foreach (var item in Items)
        //        {
        //            Type type = item.GetType();
        //            var tds = WriteRowTdHtml(func, conts, TableHeadInfos, TableTrDataInfos, item, x, type);
        //            sbBody.AppendLine(tds);
        //            if (posTotal == 0 && x == count - 2)
        //                break;
        //            x++;
        //        }
        //    }
        //    sbBody.AppendLine("</tbody>");

        //    #endregion

        //    sbHtml.AppendLine(sbHead.ToString());
        //    sbHtml.AppendLine(sbBody.ToString());

        //    MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
        //    return mstr;
        //}

        //public static MvcHtmlString ToHtmlNew(this HtmlHelper helper, Func<object, BaseController, string> func, IEnumerable<object> Items, List<RankInfo> RankInfos, int posTotal = 1)
        //{
        //    //基础类
        //    var conts = helper.ViewContext.Controller as BaseController;
        //    var TableHeadInfos = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == conts.PageFunNameEn && (p.HeadOrDataType & 2) == 2).OrderBy(p => p.TableInfoSort).ToList();
        //    //可以不排序
        //    var TableTrDataInfos = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == conts.PageFunNameEn && (p.HeadOrDataType & 1) == 1 && (p.HeadOrDataType & 2) == 0).OrderBy(p => p.TableInfoSort).ToList();

        //    StringBuilder sbHtml = new StringBuilder();
        //    #region 表头
        //    StringBuilder sbHead = new StringBuilder("<thead id='tbhead'>");

        //    var count = Items.Count();

        //    #region 显示合计行

        //    if (posTotal == 0)//开始处
        //    {
        //        if (count > 0)
        //        {
        //            var item = Items.Last();//[count - 1];
        //            Type type = item.GetType();
        //            var strhead = WriteRowThTotalHtml(TableHeadInfos, item, type);
        //            sbHead.AppendLine(strhead);
        //        }
        //    }
        //    #endregion

        //    #region 表头

        //    //写入表头
        //    var ths = WriteRowThHtmlNew(TableHeadInfos, helper, RankInfos);
        //    sbHead.AppendLine(ths);
        //    sbHead.AppendLine("</thead>");
        //    //sbHtml.AppendLine(ths.ToString());

        //    #endregion

        //    #endregion

        //    #region 主体

        //    StringBuilder sbBody = new StringBuilder("<tbody id='tbbody'>");

        //    int x = 0;
        //    if (count > 1)
        //    {
        //        foreach (var item in Items)
        //        {
        //            Type type = item.GetType();
        //            var tds = WriteRowTdHtml(func, conts, TableHeadInfos, TableTrDataInfos, item, x, type);
        //            sbBody.AppendLine(tds);
        //            if (posTotal == 0 && x == count - 2)
        //                break;
        //            x++;
        //        }
        //    }
        //    sbBody.AppendLine("</tbody>");

        //    #endregion

        //    sbHtml.AppendLine(sbHead.ToString());
        //    sbHtml.AppendLine(sbBody.ToString());

        //    MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
        //    return mstr;
        //}

        //private static string WriteRowThHtmlNew(List<Sys_PageFunTableAreaEntity> TableHeadInfos, HtmlHelper helper, List<RankInfo> RankInfos)
        //{
        //    StringBuilder strhtml = new StringBuilder("<tr>");
        //    //序号列：宽度
        //    //操作列：宽度
        //    int? width = null;
        //    string headTiele = null;
        //    string strOrderBy = null;
        //    for (var j = 0; j < TableHeadInfos.Count; j++)
        //    {
        //        strOrderBy = null;
        //        #region 宽度
        //        width = TableHeadInfos[j].HeadWidth;
        //        if (width == null)
        //            width = TableHeadInfos[j].Width;
        //        #endregion
        //        #region 表头标题
        //        headTiele = TableHeadInfos[j].NameCn;
        //        if (TableHeadInfos[j].Sys_DynReportDefineDetailID == 2)//复选框
        //        {
        //            headTiele = "<input type='checkbox' class='checkbox1 jq-checkall-switch' alt='全选/反选' title='全选/反选' />";
        //        }
        //        else if (TableHeadInfos[j].NameCnType == 2)
        //        {
        //            headTiele = TableHeadInfos[j].NameCn2;
        //            strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}<th>", TableHeadInfos[j].Width, TableHeadInfos[j].NameCn));
        //        }
        //        #endregion
        //        if (TableHeadInfos[j].Sys_DynReportDefineDetailID > 100)
        //        {
        //            strOrderBy = OrderBy(RankInfos, TableHeadInfos[j].NameEn);
        //        }
        //        if (width != null)
        //            strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}{2}</th>", width, headTiele, strOrderBy));
        //        else
        //            strhtml.Append(string.Format("<th class='lockhead' >{0} {1}</th>", headTiele, strOrderBy));
        //    }
        //    strhtml.Append("</tr>");
        //    return strhtml.ToString();
        //}

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
        ///// 表头：显示
        ///// </summary>
        ///// <param name="TableHeadInfos"></param>
        ///// <param name="helper"></param>
        ///// <param name="RankInfos"></param>
        ///// <returns></returns>
        //private static string WriteRowThHtml(List<Sys_PageFunTableAreaEntity> TableHeadInfos, HtmlHelper helper, List<RankInfo> RankInfos)
        //{
        //    StringBuilder strhtml = new StringBuilder("<tr>");
        //    //序号列：宽度
        //    //操作列：宽度
        //    int? width = null;
        //    string headTiele = null;
        //    string strOrderBy = null;
        //    for (var j = 0; j < TableHeadInfos.Count; j++)
        //    {
        //        strOrderBy = null;
        //        #region 宽度
        //        width = TableHeadInfos[j].HeadWidth;
        //        if (width == null)
        //            width = TableHeadInfos[j].Width;
        //        #endregion
        //        #region 表头标题
        //        headTiele = TableHeadInfos[j].NameCn;
        //        if (TableHeadInfos[j].Sys_DynReportDefineDetailID == 2)//复选框
        //        {
        //            headTiele = "<input type='checkbox' class='checkbox1 jq-checkall-switch' alt='全选/反选' title='全选/反选' />";
        //        }
        //        else if (TableHeadInfos[j].NameCnType == 2)
        //        {
        //            headTiele = TableHeadInfos[j].NameCn2;
        //            strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}<th>", TableHeadInfos[j].Width, TableHeadInfos[j].NameCn));
        //        }
        //        #endregion
        //        if (string.IsNullOrEmpty(TableHeadInfos[j].SortEn))
        //        {
        //            strOrderBy = OrderBy(RankInfos, TableHeadInfos[j].SortEn);
        //        }
        //        if (width != null)
        //            strhtml.Append(string.Format("<th class='lockhead' style='width: {0}px;'>{1}{2}</th>", width, headTiele, strOrderBy));
        //        else
        //            strhtml.Append(string.Format("<th class='lockhead' >{0} {1}</th>", headTiele, strOrderBy));
        //    }
        //    strhtml.Append("</tr>");
        //    return strhtml.ToString();
        //}

        //private static string WriteRowTdHtml(Func<object, BaseController, string> func, BaseController conts, 
        //    List<Sys_PageFunTableAreaEntity> TableHeadInfos, 
        //    List<Sys_PageFunTableAreaEntity> TableTrDataInfos, object item, int row, Type type)
        //{
        //    StringBuilder strhtml = new StringBuilder("<tr");
        //    //生成tr的data属性
        //    //var datas = new List<Sys_DynReportDefineDetailView>();
        //    for (var i = 0; i < TableTrDataInfos.Count; i++)
        //    {
        //        var datalower = TableTrDataInfos[i].NameEn.ToLower();
        //        var valdata = GetHtmlVal(TableTrDataInfos, item, type, i);
        //        strhtml.AppendLine(string.Format(" data-{0}='{1}' ", datalower, valdata));
        //    }

        //    strhtml.Append(">");

        //    var val = "";
        //    var align = "";
        //    for (var j = 0; j < TableHeadInfos.Count; j++)
        //    {
        //        align = "";
        //        if (TableHeadInfos[j].Sys_DynReportDefineDetailID == 1)
        //            val = (row + 1).ToString();
        //        else if (TableHeadInfos[j].Sys_DynReportDefineDetailID == 2)//复选框
        //            val = "<input type='checkbox' class='checkbox1 jq-checkall-item' />";
        //        else if (TableHeadInfos[j].Sys_DynReportDefineDetailID == 3)//操作
        //        {
        //            val = func(item, conts);
        //        }
        //        else
        //            val = GetHtmlVal(TableHeadInfos, item, type, j);
        //        strhtml.Append(string.Format("<td class='{0}'>{1}</td>", TableHeadInfos[j].Align, val));
        //    }
        //    strhtml.Append("</tr>");
        //    return strhtml.ToString();
        //}

        //private static string GetHtmlVal(List<Sys_PageFunTableAreaEntity> DynReportDefineDetails, object item, Type type, int j)
        //{
        //    PropertyInfo property = type.GetProperty(DynReportDefineDetails[j].NameEn);
        //    object value = property.GetValue(item, null);
        //    var val = "";
        //    if (value != null)
        //    {
        //        var strval = value.ToString();
        //        if (DynReportDefineDetails[j].DataType == 5)//货币类型
        //        {
        //            val = strval.ToDecimalNull().MoneyNum();
        //        }
        //        else if (DynReportDefineDetails[j].DataType == 4)//日期类型
        //        {
        //            val = strval.ToDateNull().Format_yyyy_MM_dd();
        //        }
        //        else
        //            val = strval;
        //    }
        //    return val;
        //}

        //#endregion

        #region 单选按钮

        public static MvcHtmlString RadioButtonList(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, object selectValue, string optionLabel)
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;

            if (optionLabel.ToString().Length > 0)
            {
                if (selectValue == null)
                    sb.AppendLine("<label class=\"radio\"><input data-val=\"true\"  id=\"" + name + (index++) + "\" name=\"" + name + "\" checked type=\"radio\" value=\"\" />" + optionLabel + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine("<label class=\"radio\"><input data-val=\"true\"  id=\"" + name + (index++) + "\" name=\"" + name + "\" type=\"radio\" value=\"\" />" + optionLabel + "</label>&nbsp;&nbsp;");
            }
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);

                property = type.GetProperty(DataTextField);
                object text = property.GetValue(item, null);
                if (selectValue == null)
                    sb.AppendLine("<label class=\"radio\"><input data-val=\"true\"  id=\"" + name + (index++) + "\" name=\"" + name + "\" type=\"radio\" value=\"" + value.ToString() + "\" />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else if (value.ToString() == selectValue.ToString())
                    sb.AppendLine("<label class=\"radio\"><input data-val=\"true\"  id=\"" + name + (index++) + "\" name=\"" + name + "\" checked type=\"radio\" value=\"" + value.ToString() + "\" />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine("<label class=\"radio\"><input data-val=\"true\"  id=\"" + name + (index++) + "\" name=\"" + name + "\" type=\"radio\" value=\"" + value.ToString() + "\" />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        #endregion

        #region 下拉列表框

        /// <summary>
        /// 下拉列表框
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">集合</param>
        /// <param name="DataValueField">值</param>
        /// <param name="DataTextField">文本</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="optionLabel">为空项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="classCss">class样式</param>
        /// <param name="otherPro">其它属性</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownList(this HtmlHelper helper, string name, IEnumerable items, int? selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            foreach (var item in items)
            {
                //Type type = item.GetType();
                //PropertyInfo property = type.GetProperty(DataValueField);
                //object value = property.GetValue(item, null);

                //property = type.GetProperty(DataTextField);
                //object text = property.GetValue(item, null);
                var value = item.ToString();
                var text = value;
                if (selectValues == item.ToString())
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        public static MvcHtmlString DropDownList(string name, IEnumerable items, int? selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            foreach (var item in items)
            {
                //Type type = item.GetType();
                //PropertyInfo property = type.GetProperty(DataValueField);
                //object value = property.GetValue(item, null);

                //property = type.GetProperty(DataTextField);
                //object text = property.GetValue(item, null);
                var value = item.ToString();
                var text = value;
                if (selectValues == item.ToString())
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        /// <summary>
        /// 下拉列表框
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">集合</param>
        /// <param name="DataValueField">值</param>
        /// <param name="DataTextField">文本</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="optionLabel">为空项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="classCss">class样式</param>
        /// <param name="otherPro">其它属性</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListT(this HtmlHelper helper, string name, IEnumerable items, string selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            foreach (var item in items)
            {
                //Type type = item.GetType();
                //PropertyInfo property = type.GetProperty(DataValueField);
                //object value = property.GetValue(item, null);

                //property = type.GetProperty(DataTextField);
                //object text = property.GetValue(item, null);
                var text = item;
                var value = item;
                if (selectValues == value.ToString())
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        /// <summary>
        /// 下拉列表框
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">集合</param>
        /// <param name="DataValueField">值</param>
        /// <param name="DataTextField">文本</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="optionLabel">为空项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="classCss">class样式</param>
        /// <param name="otherPro">其它属性</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownList(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, int? selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            if (items != null)
            {
                foreach (var item in items)
                {
                    Type type = item.GetType();
                    PropertyInfo property = type.GetProperty(DataValueField);
                    object value = property.GetValue(item, null);

                    property = type.GetProperty(DataTextField);
                    object text = property.GetValue(item, null);
                    if (selectValues == value.ToString())
                        sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                    else
                        sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                }
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        public static MvcHtmlString DropDownList(string name, IEnumerable items, string DataValueField, string DataTextField, int? selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            if (items != null)
            {
                foreach (var item in items)
                {
                    Type type = item.GetType();
                    PropertyInfo property = type.GetProperty(DataValueField);
                    object value = property.GetValue(item, null);

                    property = type.GetProperty(DataTextField);
                    object text = property.GetValue(item, null);
                    if (selectValues == value.ToString())
                        sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                    else
                        sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                }
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }
        //                                    <select style="display: none;" class="multiselect checkboxdrop" multiple="multiple" id="Item_FraOperatingItemIDs" 
        //    name="Item.FraOperatingItemIDs">
        //    <option value="1" selected  >香磨五谷</option>
        //    <option value="2" >华府茗芳</option>
        //    <option value="4" selected >御致茗芳</option>
        //</select>

        /// <summary>
        /// 下拉列表框
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">集合</param>
        /// <param name="DataValueField">值</param>
        /// <param name="DataTextField">文本</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="optionLabel">为空项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="classCss">class样式</param>
        /// <param name="otherPro">其它属性</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListMultiSelect(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, string selectValue1, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        { 
            int? selectValue=null;
            if (string.IsNullOrEmpty(selectValue1))
                selectValue = 0;
            else
                selectValue = Convert.ToInt32(selectValue1);
            //var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}' multiple='multiple'   class='multiselect {2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' multiple='multiple' class='multiselect {1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);

                int IntVal = Convert.ToInt32(value);
                //object text = property.GetValue(item, null);

                property = type.GetProperty(DataTextField);
                object text = property.GetValue(item, null);
                if ((selectValue & IntVal) == IntVal)
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }


        /// <summary>
        /// 下拉列表框
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">集合</param>
        /// <param name="DataValueField">值</param>
        /// <param name="DataTextField">文本</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="optionLabel">为空项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="classCss">class样式</param>
        /// <param name="otherPro">其它属性</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownList(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, string selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);

                property = type.GetProperty(DataTextField);
                object text = property.GetValue(item, null);
                if (selectValues == value.ToString())
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        public static MvcHtmlString DropDownList(string name, IEnumerable items, string DataValueField, string DataTextField, string selectValue, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            var selectValues = selectValue == null ? "" : selectValue.ToString();
            StringBuilder sb = new StringBuilder();
            var arr = name.Split('.');
            if (arr.Length > 1)
            {
                sb.AppendLine(string.Format("<select {3}  id='{0}' name='{1}'   class='{2}' " + otherPro + ">", arr[0] + "_" + arr[1], name, classCss, disabled));
            }
            else
                sb.AppendLine(string.Format("<select id='{0}' name='{0}' class='{1}'  " + otherPro + ">", name, classCss));
            if (optionLabel.Length > 0)
            {
                sb.AppendLine("<option value='' selected>" + optionLabel + "</option>");
            }
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);

                property = type.GetProperty(DataTextField);
                object text = property.GetValue(item, null);
                if (selectValues == value.ToString())
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", value, text));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", value, text)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        #endregion

        #region 下拉树

        public static MvcHtmlString DropDownForTree(this HtmlHelper helper, string name, SelectTreeList selectTreeList, string optionLab = "")
        {
            List<DropTreeNode> treeNodes = selectTreeList.TreeNodes;
            StringBuilder sb = new StringBuilder("<select id=\"" + name + "\" name=\"" + name + "\"  class='form-control'>");
            if (optionLab != "")
                sb.AppendLine("<option value='' selected class=\"dropDownTreeCss0\">" + optionLab + "</option>");

            var root = new List<DropTreeNode>();
            if (selectTreeList.RootValue == null)
                root = selectTreeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
            else
                root = selectTreeList.TreeNodes.Where(p => p.ParentTreeNodeID == selectTreeList.RootValue).ToList();

            var count = root.Count();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)// var item in root)
                {
                    List<DropTreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
                    int SubCount = Sublist.Count();

                    if (SubCount > 0)//非叶节点
                    {
                        sb.AppendLine(BindDropDownForTree(root[i], treeNodes, 0, selectTreeList.SelectValue, selectTreeList.IsFinalLevel));
                    }
                    else//叶子节点
                    {
                        if (selectTreeList.SelectValue == root[i].Value)
                            sb.AppendLine("<option value=\"" + root[i].Value + "\" selected class=\"dropDownTreeCss0\">" + root[i].Text + "</option>");
                        else
                            sb.AppendLine("<option value=\"" + root[i].Value + "\" class=\"dropDownTreeCss0\">" + root[i].Text + "</option>");
                    }
                }

            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());

            return mstr; ;
        }

        /// <summary>
        /// 下拉树
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">下拉树name名称</param>
        /// <param name="selectTreeList">数据集</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="optionLabel">为空项</param>
        /// <param name="classCss">class样式</param>
        /// <param name="otherPro">其它属性</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownForTree(this HtmlHelper helper, string name, SelectTreeList selectTreeList, string disabled = "", string optionLabel = "", string classCss = "form-control", string otherPro = "")
        {
            List<DropTreeNode> treeNodes = selectTreeList.TreeNodes;
            StringBuilder sb = new StringBuilder("<select id='" + name + "' name='" + name + "' " + disabled + " class='form-control' >");//form-control select2me
            var root = new List<DropTreeNode>();
            if (selectTreeList.RootValue == null)
                root = selectTreeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
            else
                root = selectTreeList.TreeNodes.Where(p => p.ParentTreeNodeID == selectTreeList.RootValue).ToList();

            var count = root.Count();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)// var item in root)
                {
                    List<DropTreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
                    int SubCount = Sublist.Count();

                    if (SubCount > 0)//非叶节点
                    {
                        sb.AppendLine(BindDropDownForTree(root[i], treeNodes, 0, selectTreeList.SelectValue, selectTreeList.IsFinalLevel));
                    }
                    else//叶子节点
                    {
                        if (selectTreeList.SelectValue == root[i].Value)
                            sb.AppendLine("<option value='" + root[i].Value + "'  selected " + root[i].Css + ">" + root[i].Text + "</option>");
                        else
                            sb.AppendLine("<option value='" + root[i].Value + "' " + root[i].Css + ">" + root[i].Text + "</option>");
                    }
                }

            }
            sb.AppendLine("</select>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());

            return mstr; ;
        }

        private static string BindDropDownForTree(DropTreeNode treeModel, IEnumerable<DropTreeNode> treeModels, int lev, string SelectValue, bool isFinalLevel)
        {
            string spNode = "";
            for (int k = 0; k < lev; k++)
            {
                spNode += "&nbsp;&nbsp;&nbsp;";
            }

            string spLeaf = spNode + "&nbsp;&nbsp;&nbsp;";

            StringBuilder sb = new StringBuilder();

            //if (isFinalLevel)
            //{
            //    sb.AppendLine("<option value=\"" + treeModel.Value + "\" " + treeModel.Css + ">" + spNode + treeModel.Text + "</option>");
            //}
            //else
            //{
            if (SelectValue == treeModel.Value)
                sb.AppendLine("<option value='" + treeModel.Value + "' selected   " + treeModel.Css + ">" + spNode + treeModel.Text + "</option>");
            else
                sb.AppendLine("<option value='" + treeModel.Value + "' " + treeModel.Css + ">" + spNode + treeModel.Text + "</option>");
            //}
            lev++;

            if (treeModel != null)
            {
                //获取子节点
                List<DropTreeNode> root = treeModels.Where(p => p.ParentTreeNodeID == treeModel.TreeNodeID).ToList();
                int count = root.Count;
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        List<DropTreeNode> Sublist = treeModels.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
                        int SubCount = Sublist.Count();

                        if (SubCount > 0)//非叶节点
                        {
                            sb.AppendLine(BindDropDownForTree(root[i], treeModels, lev, SelectValue, isFinalLevel));
                        }
                        else//叶子节点
                        {
                            //if (isFinalLevel)
                            //    sb.AppendLine("<option value=\"" + root[i].Value + "\" class=\"dropDownTreeCss" + lev + "\">" + spLeaf + root[i].Text + "</option>");
                            //else
                            //{
                            if (SelectValue == root[i].Value)
                                sb.AppendLine("<option value='" + root[i].Value + "' selected " + root[i].Css + ">" + spLeaf + root[i].Text + "</option>");
                            else
                                sb.AppendLine("<option value='" + root[i].Value + "' " + root[i].Css + ">" + spLeaf + root[i].Text + "</option>");
                            //}
                        }
                    }
                }
            }
            //MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return sb.ToString();
        }

        #endregion

        #region 复选框

        /// <summary>
        /// 单个复选框
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="items"></param>
        /// <param name="DataValueField"></param>
        /// <param name="DataTextField"></param>
        /// <param name="selectValues"></param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="myclass">自定义class</param>
        /// <returns></returns>
        public static MvcHtmlString ChecksButton(this HtmlHelper helper, string name, string DataTextField, string selectValues, string disabled = "", string myclass = "")
        {
            StringBuilder sb = new StringBuilder();
            if (selectValues == "1")
                sb.AppendLine("<label class='checkbox-inline " + myclass + "'><input class='ck-align' " + disabled + " name='Item." + name + "" + "' type='checkbox' value='1' checked='checked' />&nbsp;</label>&nbsp;&nbsp;");
            else
                sb.AppendLine("<label class='checkbox-inline " + myclass + "'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='1' />&nbsp;</label>&nbsp;&nbsp;");

            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        /// <summary>
        /// 复选框--按列表保存
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">数据集合</param>
        /// <param name="DataValueField">值字段</param>
        /// <param name="DataTextField">文本字段</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <returns></returns>
        public static MvcHtmlString ChecksButtonList(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, List<int> selectValues, string disabled = "")
        {
            StringBuilder sb = new StringBuilder();
            if (selectValues == null)
                selectValues = new List<int>();
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);
                int intValue = Convert.ToInt32(value);

                property = type.GetProperty(DataTextField);
                object text = property.GetValue(item, null);

                if (selectValues.Contains(intValue))
                    sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + " name='" + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='" + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        /// <summary>
        /// 复选框--按位保存
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="items"></param>
        /// <param name="DataValueField"></param>
        /// <param name="DataTextField"></param>
        /// <param name="selectValues"></param>
        /// <param name="disabled">是否允许编辑</param>
        /// <param name="myclass">自定义class</param>
        /// <returns></returns>
        public static MvcHtmlString ChecksButtonList(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, int? selectValues, string disabled = "", string myclass = "")
        {
            StringBuilder sb = new StringBuilder();
            var i = 0;
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);

                property = type.GetProperty(DataTextField);
                int IntVal = Convert.ToInt32(value);
                object text = property.GetValue(item, null);
                if ((selectValues & IntVal) == IntVal)
                    sb.AppendLine("<label class='checkbox-inline " + myclass + "'><input class='ck-align' " + disabled + " name='" + name + "" + "[{0}]' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine("<label class='checkbox-inline " + myclass + "'><input class='ck-align' " + disabled + "name='" + name + "[{0}]' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                i++;
            }
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        /// <summary>
        /// 复选框--根据字段表示选中
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">控制名</param>
        /// <param name="items">数据集合</param>
        /// <param name="DataValueField">值字段</param>
        /// <param name="DataTextField">文本字段</param>
        /// <param name="selectValues">选中项</param>
        /// <param name="disabled">是否允许编辑</param>
        /// <returns></returns>
        public static MvcHtmlString ChecksButtonList(this HtmlHelper helper, string name, IEnumerable items, string DataValueField, string DataTextField, string disabled = "")
        {
            StringBuilder sb = new StringBuilder();
            var i = 0;
            foreach (var item in items)
            {
                Type type = item.GetType();
                PropertyInfo property = type.GetProperty(DataValueField);
                object value = property.GetValue(item, null);
                int intValue = Convert.ToInt32(value);

                property = type.GetProperty(DataTextField);
                object text = property.GetValue(item, null);

                property = type.GetProperty("bCheckSelect");
                object valuebCheck = property.GetValue(item, null);
                int valuebCheckValue = Convert.ToInt32(valuebCheck);

                if (valuebCheckValue == 1)
                    sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + " name='" + name + "[{0}]' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='" + name + "[{0}]' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                i++;
            }
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        #endregion

        #region 分页

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pagingOption">分页对象</param>
        /// <returns></returns>
        public static MvcHtmlString Paging(this HtmlHelper helper, PageQueryBase pagingOption)
        {
            var PageSize = pagingOption.PageSize;
            var PageIndex = pagingOption.PageIndex;

            int intMin = (PageIndex - 1) / 5 * 5 + 1;
            int intMax = (PageIndex - 1) / 5 * 5 + 5;

            //int intMin = PageIndex / PageSize * PageSize + 1;
            //int intMax = PageIndex / PageSize * PageSize + 5;
            if (intMax > pagingOption.TotalPages)
                intMax = pagingOption.TotalPages;

            var sb = new StringBuilder();
            sb.Append("<ul class='pagination'>");
            //sb.Append("<li class=''><a>|&lt;</a></li>");
            sb.AppendFormat("<li ><a href='javascript:void(0);' data-val='{0}' class='pageIndex'>|&lt;</a></li>", 1);
            //sb.Append(string.Format("<a href='#' PageIndex={0}>&lt;&lt;</a>", intMin - 1));
            if (intMin > 5)//前5页
            {
                sb.AppendFormat("<li ><a href='javascript:void(0);' data-val='{0}' class='pageIndex'>&lt;&lt;</a></li>", intMin - 1);
            }
            for (int i = intMin; i <= intMax; i++)
            {
                //sb.Append(string.Format("<a href='#' PageIndex={0}>{0}</a>", i));
                sb.AppendFormat(GeneratePageLink(i, PageIndex));
                //sb.AppendFormat("<li ><a href='javascript:void(0);' data-val='{0}' class='pageIndex'>{0}</a></li>", i);
            }
            if (intMax < pagingOption.TotalPages)
            {
                //sb.Append(string.Format("<a href='#' PageIndex={0}>&gt;&gt;</a>", intMax + 1));
                sb.AppendFormat("<li ><a href='javascript:void(0);' data-val='{0}' class='pageIndex'>&gt;&gt;</a></li>", intMax + 1);
            }
            //sb.AppendFormat(string.Format("<li class='' PageIndex={0}><a>&gt;|</a></li>", pagingOption.TotalPages));

            sb.AppendFormat("<li ><a href='javascript:void(0);' data-val='{0}' class='pageIndex'>&gt;|</a></li>", pagingOption.TotalPages);
            //sb.AppendFormat("<li class=\"active\"><a>{0}</a></li>", i);

            sb.Append("</ul>");

            #region
            //var str = string.Format("<li ><a href='javascript:void(0);' data-val='{0}' class='pageIndex'>{0}</a></li>", linkText);

            //sb.Append(currentPage > 1 ? GeneratePageLink(1) : "<li class=\"first disabled\"><a>«</a></li>");
            //sb.Append(currentPage > 1 ? GeneratePageLink(currentPage - 1) : "<li class=\"previous disabled\"><a>‹</a></li>");


            //var pageCount = pagingOption.TotalPages;
            //if (pageCount == 0)
            //    pageCount = 1;
            //const int nrOfPagesToDisplay = 4;

            //var currentPage = pagingOption.PageIndex;

            ////var sb = new StringBuilder();

            //// Previous
            //sb.Append("<ul class='pagination'>");
            //sb.Append(currentPage > 1 ? GeneratePageLink(1) : "<li class=\"first disabled\"><a>«</a></li>");
            //sb.Append(currentPage > 1 ? GeneratePageLink(currentPage - 1) : "<li class=\"previous disabled\"><a>‹</a></li>");

            //var start = 1;
            //var end = pageCount;

            //if (pageCount > nrOfPagesToDisplay)
            //{
            //    var middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
            //    var below = (currentPage - middle);
            //    var above = (currentPage + middle);

            //    if (below < 4)
            //    {
            //        above = nrOfPagesToDisplay;
            //        below = 1;
            //    }
            //    else if (above > (pageCount - 4))
            //    {
            //        above = pageCount;
            //        below = (pageCount - nrOfPagesToDisplay);
            //    }

            //    start = below;
            //    end = above;
            //}

            //if (start > 3)
            //{
            //    sb.Append(GeneratePageLink(1));
            //    sb.Append(GeneratePageLink(2));
            //    sb.Append("<li class=\"disabled\"><a>...</a></li>");
            //}

            //for (var i = start; i <= end; i++)
            //{
            //    if (i == currentPage || (currentPage <= 0 && i == 0))
            //    {
            //        sb.AppendFormat("<li class=\"active\"><a>{0}</a></li>", i);
            //    }
            //    else
            //    {
            //        sb.Append(GeneratePageLink(i));
            //    }
            //}

            //if (end < pageCount)//后面还有其它页码
            //{
            //    int tempmin = pageCount - 3;

            //    sb.Append("<li class=\"disabled\"><a>...</a></li>");
            //    for (int k = tempmin; k <= pageCount; k++)
            //    {
            //        sb.Append(GeneratePageLink(k));
            //    }
            //}

            //// Next
            //sb.Append(currentPage < pageCount ? GeneratePageLink(currentPage + 1) : "<li class=\"next disabled\"><a>›</a></li>");
            //sb.Append(currentPage < pageCount ? GeneratePageLink(pageCount) : "<li class=\"last disabled\"><a>»</a></li>");
            //sb.Append("</ul>");
            #endregion

            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        public static string GeneratePageLink(int pageIndex, int currpage)
        {
            string str = "";
            if (pageIndex == currpage)
                str = string.Format("<li class=\"active\"><a>{0}</a></li>", pageIndex);
            else
                str = string.Format("<li ><a href='javascript:;' data-val='{0}' class='pageIndex'>{0}</a></li>", pageIndex);
            return str;
        }


        ///// <summary>
        ///// 分页
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="pagingOption">分页对象</param>
        ///// <returns></returns>
        //public static MvcHtmlString Paging(this HtmlHelper helper, PageQueryBase pagingOption)
        //{
        //    var pageCount = pagingOption.TotalPages;
        //    if (pageCount == 0) 
        //        pageCount = 1;
        //    const int nrOfPagesToDisplay = 4;

        //    var currentPage = pagingOption.PageIndex;

        //    var sb = new StringBuilder();

        //    // Previous
        //    sb.Append("<ul class='pagination'>");
        //    sb.Append(currentPage > 1 ? GeneratePageLink(1) : "<li class=\"first disabled\"><a>«</a></li>");
        //    sb.Append(currentPage > 1 ? GeneratePageLink(currentPage - 1) : "<li class=\"previous disabled\"><a>‹</a></li>");

        //    var start = 1;
        //    var end = pageCount;

        //    if (pageCount > nrOfPagesToDisplay)
        //    {
        //        var middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
        //        var below = (currentPage - middle);
        //        var above = (currentPage + middle);

        //        if (below < 4)
        //        {
        //            above = nrOfPagesToDisplay;
        //            below = 1;
        //        }
        //        else if (above > (pageCount - 4))
        //        {
        //            above = pageCount;
        //            below = (pageCount - nrOfPagesToDisplay);
        //        }

        //        start = below;
        //        end = above;
        //    }

        //    if (start > 3)
        //    {
        //        sb.Append(GeneratePageLink(1));
        //        sb.Append(GeneratePageLink(2));
        //        sb.Append("<li class=\"disabled\"><a>...</a></li>");
        //    }

        //    for (var i = start; i <= end; i++)
        //    {
        //        if (i == currentPage || (currentPage <= 0 && i == 0))
        //        {
        //            sb.AppendFormat("<li class=\"active\"><a>{0}</a></li>", i);
        //        }
        //        else
        //        {
        //            sb.Append(GeneratePageLink(i));
        //        }
        //    }

        //    if (end < pageCount)//后面还有其它页码
        //    {
        //        int tempmin = pageCount - 3;

        //        sb.Append("<li class=\"disabled\"><a>...</a></li>");
        //        for (int k = tempmin; k <= pageCount; k++)
        //        {
        //            sb.Append(GeneratePageLink(k));
        //        }
        //    }

        //    // Next
        //    sb.Append(currentPage < pageCount ? GeneratePageLink(currentPage + 1) : "<li class=\"next disabled\"><a>›</a></li>");
        //    sb.Append(currentPage < pageCount ? GeneratePageLink(pageCount) : "<li class=\"last disabled\"><a>»</a></li>");
        //    sb.Append("</ul>");

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //public static string GeneratePageLink(int linkText)
        //{
        //    var str = string.Format("<li ><a href='javascript:;' data-val='{0}' class='pageIndex'>{0}</a></li>", linkText);
        //    return str;
        //}

        #endregion

        //#region 组织机构树

        //public static MvcHtmlString Tree(this HtmlHelper helper, string name, TreeList treeList, string selectTreeNode, bool parentLink = true, bool bcontroll = false)
        //{
        //    string urlpath = HttpContext.Current.Request.Url.AbsolutePath;

        //    string PKField = treeList.ValueField;

        //    List<TreeNode> treeNodes = treeList.TreeNodes;
        //    StringBuilder sb = new StringBuilder("<div id=\"MyTree\" class=\"tree\">");
        //    var root = new List<TreeNode>();
        //    if (treeList.RootValue == null)
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
        //    else
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == treeList.RootValue).ToList();
        //    var count = root.Count();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            List<TreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //            int SubCount = Sublist.Count();

        //            if (SubCount > 0)//非叶节点
        //            {
        //                if (i == 0)
        //                    sb.AppendLine("<div class=\"tree-folder tree-folder-first\" style=\"display: block;\">");
        //                else
        //                    sb.AppendLine("<div class=\"tree-folder tree-folder-first\" style=\"display: block;\">");

        //                #region 头部,文件夹开和折叠...

        //                var selectIselected1 = "fa fa-folder";
        //                bool bopen = false;
        //                if (selectTreeNode.StartsWith(treeList.RootValue + "/" + root[i].TreeNodeID + "/"))
        //                {
        //                    selectIselected1 = "fa fa-folder-open";
        //                    bopen = true;
        //                }

        //                sb.AppendLine(" <div class=\"tree-folder-header\">");
        //                sb.AppendLine("<i class=\"" + selectIselected1 + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-folder-name\">");

        //                sb.AppendLine(string.Format("<a href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a>", urlpath + treeList.ControllerAction, PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));

        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");//头部结束

        //                #endregion

        //                #region
        //                //下一级
        //                sb.AppendLine(BindTree(root[i], treeNodes, treeList.ControllerAction, treeList.RootValue + "/" + root[i].TreeNodeID + "/", PKField, selectTreeNode, 1, parentLink, i == count - 1, urlpath, bopen));

        //                //sb.AppendLine(BindTreeNoSame(root[i], treeNodes, treeList.ControllerAction, treeList.RootValue + "/" + root[i].TreeNodeID + "/", PKField, selectTreeNode, 1, parentLink, i == count - 1));

        //                #endregion

        //                sb.AppendLine("<div class='tree-loader' style='display: none;'>");

        //                sb.AppendLine("</div>");

        //                sb.AppendLine("</div>");//文件夹结束


        //            }
        //            else//叶子节点
        //            {
        //                var selectDivSelect = "";
        //                var selectIselected = "tree-dot";
        //                if (selectTreeNode == treeList.RootValue + "/" + root[i].Value + "/")
        //                {
        //                    selectDivSelect = " tree-selected";
        //                    //selectIselected = "fa fa-check";
        //                }

        //                sb.AppendLine("<div class=\"tree-item" + selectDivSelect + "\" style=\"display: block;\">");
        //                //sb.AppendLine("<i class=\"" + selectIselected + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-item-name\">");

        //                sb.AppendLine(string.Format("<a href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a>", urlpath + "/" + treeList.ControllerAction, PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));

        //                //if (root[i].NodeUrl == "")
        //                //    sb.AppendLine(string.Format("<a href='javascript:void(0);'>{0}</a>", root[i].Text));
        //                //else
        //                //    sb.AppendLine(string.Format("<a  href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a></li>", root[i].NodeUrl, PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));
        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");
        //            }
        //        }
        //    }

        //    sb.AppendLine("</div>");
        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindTree(TreeNode treeModel, IEnumerable<TreeNode> treeModels, string controllerAction, string currentPath, string PKField, string selectTreeNode, int lev, bool parentLink, bool blast, string urlpath, bool bopen)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (bopen)
        //        sb.AppendLine("<div class=\"tree-folder-content\" style=\"display: block;\">");
        //    else
        //        sb.AppendLine("<div class=\"tree-folder-content\" style=\"display: none;\">");

        //    List<TreeNode> root = treeModels.Where(p => p.ParentTreeNodeID == treeModel.TreeNodeID).ToList();
        //    int count = root.Count;
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            List<TreeNode> Sublist = treeModels.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //            int SubCount = Sublist.Count();

        //            if (SubCount > 0)//非叶节点
        //            {
        //                sb.AppendLine("<div class=\"tree-folder\" style=\"display: block;\">");

        //                #region 头部,文件夹开和折叠...

        //                bopen = false;
        //                var selectIselected1 = "fa fa-folder";

        //                if (selectTreeNode.StartsWith(currentPath + root[i].TreeNodeID + "/"))
        //                {
        //                    selectIselected1 = "fa fa-folder-open";
        //                    bopen = true;
        //                }

        //                #region 样例
        //                //<div class="tree-folder-header">

        //                //    <i class="fa fa-folder"></i>
        //                //    <div class="tree-folder-name">

        //                //        Sales

        //                //    </div>

        //                //</div>
        //                //---------------------
        //                // div class="tree-folder-header">

        //                //    <i class="fa fa-folder-open"></i>
        //                //    <div class="tree-folder-name">

        //                //        Sales

        //                //    </div>

        //                //</div>
        //                #endregion

        //                sb.AppendLine(" <div class=\"tree-folder-header\">");
        //                sb.AppendLine("<i class=\"" + selectIselected1 + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-folder-name\">");

        //                sb.AppendLine(string.Format("<a  href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a>", urlpath + controllerAction, PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));

        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");//头部结束

        //                #endregion

        //                #region
        //                //下一级
        //                sb.AppendLine(BindTree(root[i], treeModels, controllerAction, currentPath + root[i].TreeNodeID + "/", PKField, selectTreeNode, lev, parentLink, i == count - 1, urlpath, bopen));

        //                //sb.AppendLine(BindTreeNoSame(root[i], treeNodes, treeList.ControllerAction, treeList.RootValue + "/" + root[i].TreeNodeID + "/", PKField, selectTreeNode, 1, parentLink, i == count - 1));

        //                #endregion

        //                sb.AppendLine("<div class='tree-loader' style='display: none;'>");

        //                sb.AppendLine("</div>");

        //                sb.AppendLine("</div>");//文件夹结束
        //            }
        //            else//叶子节点
        //            {
        //                var selectDivSelect = "";
        //                //var selectIselected = "tree-dot";

        //                if (selectTreeNode == currentPath + root[i].TreeNodeID + "/")
        //                {
        //                    selectDivSelect = " tree-selected";
        //                    //selectIselected = "fa fa-check";
        //                }

        //                sb.AppendLine("<div class=\"tree-item" + selectDivSelect + "\" style=\"display: block;\">");
        //                //sb.AppendLine("<i class=\"" + selectIselected + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-item-name\">");

        //                sb.AppendLine(string.Format("<a href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a>", urlpath + controllerAction, PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));

        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");
        //            }
        //        }
        //    }
        //    sb.AppendLine("</div>");

        //    return sb.ToString();
        //}

        //#endregion 组织机构树

        //#region 卡片树

        ///// <summary>
        ///// 每个节点的颜色和Url不同
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="name"></param>
        ///// <param name="treeList"></param>
        ///// <param name="selectTreeNode"></param>
        ///// <param name="parentLink"></param>
        ///// <param name="bcontroll"></param>
        ///// <returns></returns>
        //public static MvcHtmlString TreeNodeNoSame(this HtmlHelper helper, string name, TreeList treeList, string selectTreeNode, bool parentLink = true, bool bcontroll = false)
        //{
        //    string PKField = treeList.ValueField;

        //    List<TreeNode> treeNodes = treeList.TreeNodes;
        //    StringBuilder sb = new StringBuilder("<div id=\"MyTree\" class=\"tree\">");

        //    var root = new List<TreeNode>();
        //    if (treeList.RootValue == null)
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
        //    else
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == treeList.RootValue).ToList();
        //    var count = root.Count();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            List<TreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //            int SubCount = Sublist.Count();

        //            if (SubCount > 0)//非叶节点
        //            {
        //                if (i == 0)
        //                    sb.AppendLine("<div class=\"tree-folder tree-folder-first\" style=\"display: block;\">");
        //                else
        //                    sb.AppendLine("<div class=\"tree-folder\" style=\"display: block;\">");

        //                #region 头部,文件夹开和折叠...

        //                bool bopen = false;
        //                var selectIselected1 = "fa fa-folder";

        //                if (selectTreeNode.StartsWith(treeList.RootValue + "/" + root[i].TreeNodeID + "/"))
        //                {
        //                    selectIselected1 = "fa fa-folder-open";
        //                    bopen = true;
        //                }

        //                sb.AppendLine(" <div class=\"tree-folder-header\">");
        //                sb.AppendLine("<i class=\"" + selectIselected1 + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-folder-name\">");
        //                if (root[i].NodeUrl == "")
        //                {
        //                    sb.AppendLine(string.Format("<a  href='javascript:void(0);'>{0}</a>", root[i].Text));
        //                }
        //                else
        //                    sb.AppendLine(string.Format("<a href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a>", root[i].NodeUrl, PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));

        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");//头部结束

        //                #endregion

        //                #region
        //                //下一级

        //                sb.AppendLine(BindTreeNoSame(root[i], treeNodes, treeList.ControllerAction, treeList.RootValue + "/" + root[i].TreeNodeID + "/", PKField, selectTreeNode, 1, parentLink, i == count - 1, bopen));

        //                #endregion
        //                sb.AppendLine("<div class='tree-loader' style='display: none;'>");
        //                //sb.AppendLine("<img src='assets/img/input-spinner.gif'></img>");
        //                sb.AppendLine("</div>");

        //                sb.AppendLine("</div>");//文件夹结束
        //            }
        //            else//叶子节点
        //            {
        //                var selectDivSelect = "";
        //                //var selectIselected = "tree-dot";
        //                if (selectTreeNode.StartsWith(treeList.RootValue + "/" + root[i].Value + "/"))
        //                {
        //                    selectDivSelect = " tree-selected";
        //                    //selectIselected = "fa fa-check";
        //                }
        //                //if (root[i].NodeUrl == "")
        //                //{
        //                //    sb.AppendLine(string.Format("<li><span class='{0}'></span><a class='{1}' href='javascript:void(0);'>{6}</a></li>", show_line, active, root[i].Text));
        //                //}
        //                //else
        //                //    sb.AppendLine(string.Format("<li><span class='{0}'></span><a class='{1}' href='{2}?IsFinalLevel=0&{3}={4}&currentPath={5}'>{6}</a></li>", show_line, active, root[i].NodeUrl, PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));

        //                sb.AppendLine("<div class=\"tree-item" + selectDivSelect + "\" style=\"display: block;\">");
        //                //sb.AppendLine("<i class=\"" + selectIselected + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-item-name\">");
        //                if (root[i].NodeUrl == "")
        //                    sb.AppendLine(string.Format("<a href='javascript:void(0);'>{0}</a>", root[i].Text));
        //                else
        //                    sb.AppendLine(string.Format("<a  href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a>", root[i].NodeUrl, PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));
        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");
        //            }
        //        }
        //    }

        //    sb.AppendLine("</div>");//结束

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindTreeNoSame(TreeNode treeModel, IEnumerable<TreeNode> treeModels, string controllerAction, string currentPath, string PKField, string selectTreeNode, int lev, bool parentLink, bool blast, bool bopen)
        //{
        //    StringBuilder sb = new StringBuilder("<div class=\"tree-folder-content\" style=\"display: " + (bopen ? "block" : "none") + ";\">");
        //    //StringBuilder sb = new StringBuilder("<div class=\"tree-folder-content\" style=\"display:block;\">");
        //    List<TreeNode> root = treeModels.Where(p => p.ParentTreeNodeID == treeModel.TreeNodeID).ToList();
        //    int count = root.Count;
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            List<TreeNode> Sublist = treeModels.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //            int SubCount = Sublist.Count();

        //            if (SubCount > 0)//非叶节点
        //            {
        //                //string change = "change";
        //                //string active = active = root[i].Css;
        //                //if (selectTreeNode.StartsWith(currentPath + root[i].TreeNodeID + "/"))
        //                //{
        //                //    change = "";
        //                //}
        //                //if (selectTreeNode == currentPath + root[i].TreeNodeID + "/")
        //                //{
        //                //    active = "active";
        //                //}

        //                //sb.AppendLine("<li>");
        //                ////sb.AppendLine("<p class='jq-collapse-title'><span class='show_icon " + change + "'></span><a class='" + active + "' href='" + urlpath + controllerAction + "?IsFinalLevel=1&NodeID=" + root[i].Value + "&ParentCode=" + root[i].TreeNodeID + "&ParentName=" + root[i].Text + "&Other=" + root[i].Other + "&currentPath=" + currentPath + root[i].TreeNodeID + "/'>" + root[i].Text + "</a></p>");

        //                //if (root[i].NodeUrl == "")
        //                //{
        //                //    sb.AppendLine(string.Format("<p class='jq-collapse-title'><span class='show_icon {0}'></span><a class='{1}' href='javascript:void(0);'>{2}</a></p>", change, active, root[i].Text));
        //                //}
        //                //else
        //                //{
        //                //    sb.AppendLine(string.Format("<p class='jq-collapse-title'><span class='show_icon {0}'></span><a class='{1}' href='{2}?IsFinalLevel=1&{3}={4}&currentPath={5}'>{6}</a></p>", change, active, root[i].NodeUrl, PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
        //                //}

        //                //sb.AppendLine(BindTreeNoSame(root[i], treeModels, controllerAction, currentPath + root[i].TreeNodeID + "/", PKField, selectTreeNode, lev, parentLink, i == count - 1));
        //                //sb.AppendLine("</li>");

        //                sb.AppendLine("<div class=\"tree-folder\" style=\"display: block;\">");

        //                #region 头部,文件夹开和折叠...

        //                bopen = false;
        //                var selectIselected1 = "fa fa-folder";

        //                if (selectTreeNode.StartsWith(currentPath + root[i].TreeNodeID + "/"))
        //                {
        //                    selectIselected1 = "fa fa-folder-open";
        //                    bopen = true;
        //                }

        //                sb.AppendLine(" <div class=\"tree-folder-header\">");
        //                sb.AppendLine("<i class=\"" + selectIselected1 + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-folder-name\">");
        //                if (root[i].NodeUrl == "")
        //                {
        //                    sb.AppendLine(string.Format("<a  href='javascript:void(0);'>{0}</a>", root[i].Text));
        //                }
        //                else
        //                    sb.AppendLine(string.Format("<a href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a>", root[i].NodeUrl, PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));

        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");//头部结束

        //                #endregion

        //                #region //下一级

        //                sb.AppendLine(BindTreeNoSame(root[i], treeModels, controllerAction, currentPath + root[i].TreeNodeID + "/", PKField, selectTreeNode, lev, parentLink, i == count - 1, bopen));

        //                #endregion

        //                sb.AppendLine("<div class='tree-loader' style='display: none;'>");
        //                //sb.AppendLine("<img src='assets/img/input-spinner.gif'></img>");
        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");//文件夹结束
        //            }
        //            else//叶子节点
        //            {
        //                //var show_line = "show_line1";
        //                //var active = root[i].Css; ;
        //                //if (count - 1 == i)
        //                //{
        //                //    show_line = "show_line2";
        //                //}
        //                //if (selectTreeNode == currentPath + root[i].TreeNodeID + "/")
        //                //    active = "active";
        //                ////                        if (selectTreeNode == currentPath + root[i].TreeNodeID + "/")//show_line
        //                ////                            sb.AppendLine("<li><span class='" + show_line + "'></span><a class='active' href=\"" + urlpath + controllerAction + "?IsFinalLevel=1&NodeID=" + root[i].Value + "&ParentCode=" + root[i].TreeNodeID + "&ParentName=" + root[i].Text + "&Other=" + root[i].Other + "&currentPath=" + currentPath + root[i].TreeNodeID + "/\">" + root[i].Text + "</a></li>");

        //                //if (root[i].NodeUrl == "")
        //                //{
        //                //    sb.AppendLine(string.Format("<li><span class='{0}'></span><a class='{1}' href='javascript:void(0);'>{2}</a></li>", show_line, active, root[i].Text));
        //                //}
        //                //else
        //                //    sb.AppendLine(string.Format("<li><span class='{0}'></span><a class='{1}' href='{2}?IsFinalLevel=0&{3}={4}&currentPath={5}'>{6}</a></li>", show_line, active, root[i].NodeUrl, PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));

        //                var selectDivSelect = "";
        //                //var selectIselected = "tree-dot";
        //                if (selectTreeNode.StartsWith(currentPath + root[i].TreeNodeID + "/"))
        //                {
        //                    selectDivSelect = " tree-selected";
        //                    //selectIselected = "fa fa-check";
        //                }

        //                sb.AppendLine("<div class=\"tree-item" + selectDivSelect + "\" style=\"display: block;\">");
        //                //sb.AppendLine("<i class=\"" + selectIselected + "\"></i>");
        //                sb.AppendLine("<div class=\"tree-item-name\">");
        //                if (root[i].NodeUrl == "")
        //                    sb.AppendLine(string.Format("<a href='javascript:void(0);'>{0}</a>", root[i].Text));
        //                else
        //                    sb.AppendLine(string.Format("<a  href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a>", root[i].NodeUrl, PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
        //                sb.AppendLine("</div>");
        //                sb.AppendLine("</div>");

        //            }
        //        }
        //    }
        //    sb.AppendLine("</div>");

        //    return sb.ToString();
        //}

        //#endregion

        //public static MvcHtmlString TreeSelect(this HtmlHelper helper, string name, TreeList treeList, string ControllerAction, string selectTreeNode, bool parentLink = true)
        //{
        //    List<TreeNode> treeNodes = treeList.TreeNodes;
        //    StringBuilder sb = new StringBuilder();
        //    var root = new List<TreeNode>();
        //    if (treeList.RootValue == null)
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
        //    else
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == treeList.RootValue).ToList();

        //    var count = root.Count();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            List<TreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();
        //            int SubCount = Sublist.Count();

        //            if (SubCount > 0)//非叶节点
        //            {
        //                sb.AppendLine(BindTreeSelect(root[i], treeNodes, treeList.ControllerAction, root[i].TreeNodeID, selectTreeNode, 1, parentLink));
        //            }
        //            else//叶子节点
        //                if (selectTreeNode == root[i].Value)
        //                    sb.AppendLine("<li class='active'><img src='/Content/images/_line1.gif'/><a href='javascript:void(0)'" + "  IsFinalLevel='1'  NodeID='" + root[i].Value + "'  Code='" + root[i].TreeNodeID + "'  Name='" + root[i].Text + "'  Other='" + root[i].Other + "' currentPath='" + root[i].TreeNodeID + "'  >" + root[i].Text + "</a></li>");
        //                else
        //                    sb.AppendLine("<li><img src='/Content/images/_line1.gif'/><a href='javascript:void(0)'/" + "  IsFinalLevel='1'  NodeID='" + root[i].Value + "'  Code='" + root[i].TreeNodeID + "'  Name='" + root[i].Text + "'  Other='" + root[i].Other + "' currentPath='" + root[i].TreeNodeID + "' >" + root[i].Text + "</a></li>");
        //        }
        //    }
        //    //sb.AppendLine(" </ul>");
        //    //sb.AppendLine(" </li>");

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindTreeSelect(TreeNode treeModel, IEnumerable<TreeNode> treeModels, string controllerAction, string currentPath, string selectTreeNode, int lev, bool parentLink)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("<li class=\"text-sub" + lev + "\">");
        //    //sb.AppendLine("<a href=\"\" class=\"jq-collapse-title\"><span class=\"show_icon\"><a href=\"@Url.Content(\"~/" + controllerAction + "/" + treeModel.Value + "\")\">" + treeModel.Text + "</a></span></div>");
        //    //sb.AppendLine();
        //    if (selectTreeNode == currentPath)
        //    {
        //        if (parentLink)
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title active\"><span class=\"show_icon\"></span>");
        //            sb.AppendLine("<a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' >" + treeModel.Text + "</a>");
        //        }
        //        else
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title\"><span class=\"show_icon\"></span>");
        //            sb.AppendLine("<a href=\'javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' >" + treeModel.Text + "</a>");
        //        }
        //        sb.AppendLine("</p>");
        //        sb.AppendLine("<ul class=\"nav nav-pills nav-stacked jq-collapse-content text-sub\">");
        //    }
        //    else
        //    {
        //        if (selectTreeNode.StartsWith(currentPath))//展开图标
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title\"><span class=\"show_icon\"></span>");
        //            if (parentLink)
        //                sb.AppendLine("<a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' >" + treeModel.Text + "</a>");
        //            else
        //                sb.AppendLine("<a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' >" + treeModel.Text + "</a>");
        //            sb.AppendLine("</p>");
        //            sb.AppendLine("<ul class=\"nav nav-pills nav-stacked jq-collapse-content text-sub\">");
        //        }
        //        else//折叠
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title\"><span class=\"show_icon change\"></span>");

        //            if (parentLink)
        //            {
        //                sb.AppendLine("<a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' >" + treeModel.Text + "</a>");
        //            }
        //            else
        //            {
        //                sb.AppendLine("<a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' >" + treeModel.Text + "</a>");
        //            }
        //            sb.AppendLine("</p>");
        //            sb.AppendLine("<ul style='display: none;' class=\"nav nav-pills nav-stacked jq-collapse-content text-sub\">");
        //        }
        //    }
        //    lev++;
        //    if (treeModel != null)
        //    {
        //        //获取子节点
        //        List<TreeNode> root = treeModels.Where(p => p.ParentTreeNodeID == treeModel.TreeNodeID).ToList();
        //        int count = root.Count;
        //        if (count > 0)
        //        {
        //            for (int i = 0; i < count; i++)
        //            {
        //                List<TreeNode> Sublist = treeModels.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //                int SubCount = Sublist.Count();

        //                if (SubCount > 0)//非叶节点
        //                {
        //                    sb.AppendLine(BindTreeSelect(root[i], treeModels, controllerAction, currentPath + "/" + root[i].TreeNodeID, selectTreeNode, lev, parentLink));
        //                }
        //                else//叶子节点
        //                {//active
        //                    if (selectTreeNode == currentPath + "/" + root[i].TreeNodeID)
        //                        sb.AppendLine("<li class='active'><a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' ><img src='/Content/images/_line1.gif'/>" + root[i].Text + "</a></li>");
        //                    else
        //                        sb.AppendLine("<li><a href='javascript:void(0)'" + " IsFinalLevel='0'  NodeID='" + treeModel.Value + "'  Code='" + treeModel.TreeNodeID + "'  Other='" + treeModel.Other + "' Name='" + treeModel.Text + "' currentPath='" + currentPath + "' ><img src='/Content/images/_line1.gif'/>" + root[i].Text + "</a></li>");
        //                }
        //            }
        //        }
        //    }
        //    sb.AppendLine("</ul>");
        //    sb.AppendLine("</li>");
        //    return sb.ToString();
        //}

        //public static MvcHtmlString TreeAssets(this HtmlHelper helper, string name, TreeList treeList, string selectTreeNode, string ControllActionPath, bool parentLink = true)
        //{
        //    List<TreeNode> treeNodes = treeList.TreeNodes;
        //    StringBuilder sb = new StringBuilder();

        //    var root = new List<TreeNode>();
        //    if (treeList.RootValue == null)
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == null).ToList();
        //    else
        //        root = treeList.TreeNodes.Where(p => p.ParentTreeNodeID == treeList.RootValue).ToList();

        //    var count = root.Count();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)// var item in root)
        //        {
        //            List<TreeNode> Sublist = treeNodes.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //            int SubCount = Sublist.Count();

        //            if (SubCount > 0)//非叶节点
        //            {
        //                sb.AppendLine(BindTreeAssets(root[i], treeNodes, ControllActionPath, root[i].TreeNodeID, selectTreeNode, 1, parentLink));
        //            }
        //            else//叶子节点
        //                if (selectTreeNode == root[i].Value)
        //                    sb.AppendLine("<li class='active'><img src='/Content/images/_line1.gif'/><a href=\"/" + ControllActionPath + "?IsFinalLevel=1&NodeID=" + root[i].Value + "&ParentCode=" + root[i].TreeNodeID + "&ParentName=" + root[i].Text + "&Other=" + root[i].Other + "&currentPath=" + root[i].TreeNodeID + "\">" + root[i].Text + "</a></li>");
        //                else
        //                    sb.AppendLine("<li><img src='/Content/images/_line1.gif'/><a href=\"/" + ControllActionPath + "?IsFinalLevel=1&NodeID=" + root[i].Value + "&ParentCode=" + root[i].TreeNodeID + "&ParentName=" + root[i].Text + "&Other=" + root[i].Other + "&currentPath=" + root[i].TreeNodeID + "\">" + root[i].Text + "</a></li>");
        //        }
        //    }
        //    //sb.AppendLine(" </ul>");
        //    //sb.AppendLine(" </li>");

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindTreeAssets(TreeNode treeModel, IEnumerable<TreeNode> treeModels, string controllerAction, string currentPath, string selectTreeNode, int lev, bool parentLink)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("<li class=\"text-sub" + lev + "\">");
        //    //sb.AppendLine("<a href=\"\" class=\"jq-collapse-title\"><span class=\"show_icon\"><a href=\"@Url.Content(\"~/" + controllerAction + "/" + treeModel.Value + "\")\">" + treeModel.Text + "</a></span></div>");
        //    //sb.AppendLine();
        //    if (selectTreeNode == currentPath)
        //    {
        //        if (parentLink)
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title active\"><span class=\"show_icon\"></span>");
        //            sb.AppendLine("<a href=\"/" + controllerAction + "?IsFinalLevel=0&NodeID=" + treeModel.Value + "&ParentCode=" + treeModel.TreeNodeID + "&Other=" + treeModel.Other + "&ParentName=" + treeModel.Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "\">" + treeModel.Text + "</a>");
        //        }
        //        else
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title\"><span class=\"show_icon\"></span>");
        //            sb.AppendLine("<a href=\"/" + controllerAction + "?IsFinalLevel=0&NodeID=" + treeModel.Value + "&ParentCode=" + treeModel.TreeNodeID + "&Other=" + treeModel.Other + "&ParentName=" + treeModel.Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "#\">" + treeModel.Text + "</a>");
        //        }
        //        sb.AppendLine("</p>");
        //        sb.AppendLine("<ul class=\"nav nav-pills nav-stacked jq-collapse-content text-sub\">");
        //    }
        //    else
        //    {
        //        if (selectTreeNode.StartsWith(currentPath))//展开图标
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title\"><span class=\"show_icon\"></span>");
        //            if (parentLink)
        //                sb.AppendLine("<a href=\"/" + controllerAction + "?IsFinalLevel=0&NodeID=" + treeModel.Value + "&ParentCode=" + treeModel.TreeNodeID + "&ParentName=" + treeModel.Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "\">" + treeModel.Text + "</a>");
        //            else
        //                sb.AppendLine("<a href=\"/" + controllerAction + "?IsFinalLevel=0&NodeID=" + treeModel.Value + "&ParentCode=" + treeModel.TreeNodeID + "&ParentName=" + treeModel.Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "#\">" + treeModel.Text + "</a>");
        //            sb.AppendLine("</p>");
        //            sb.AppendLine("<ul class=\"nav nav-pills nav-stacked jq-collapse-content text-sub\">");
        //        }
        //        else//折叠
        //        {
        //            sb.AppendLine("<p class=\"jq-collapse-title\"><span class=\"show_icon change\"></span>");

        //            if (parentLink)
        //            {
        //                sb.AppendLine("<a href=\"/" + controllerAction + "?IsFinalLevel=0&NodeID=" + treeModel.Value + "&ParentCode=" + treeModel.TreeNodeID + "&ParentName=" + treeModel.Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "\">" + treeModel.Text + "</a>");
        //            }
        //            else
        //            {
        //                sb.AppendLine("<a href=\"/" + controllerAction + "?IsFinalLevel=0&NodeID=" + treeModel.Value + "&ParentCode=" + treeModel.TreeNodeID + "&ParentName=" + treeModel.Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "#\">" + treeModel.Text + "</a>");
        //            }
        //            sb.AppendLine("</p>");
        //            sb.AppendLine("<ul style='display: none;' class=\"nav nav-pills nav-stacked jq-collapse-content text-sub\">");
        //        }
        //    }
        //    lev++;
        //    if (treeModel != null)
        //    {
        //        //获取子节点
        //        List<TreeNode> root = treeModels.Where(p => p.ParentTreeNodeID == treeModel.TreeNodeID).ToList();
        //        int count = root.Count;
        //        if (count > 0)
        //        {
        //            for (int i = 0; i < count; i++)
        //            {
        //                List<TreeNode> Sublist = treeModels.Where(p => p.ParentTreeNodeID == root[i].TreeNodeID).ToList();//.ModularFuns.ToList();
        //                int SubCount = Sublist.Count();

        //                if (SubCount > 0)//非叶节点
        //                {
        //                    sb.AppendLine(BindTreeAssets(root[i], treeModels, controllerAction, currentPath + "/" + root[i].TreeNodeID, selectTreeNode, lev, parentLink));
        //                }
        //                else//叶子节点
        //                {//active
        //                    if (selectTreeNode == currentPath + "/" + root[i].TreeNodeID)
        //                        sb.AppendLine("<li class='active'><a  href=\"/" + controllerAction + "?IsFinalLevel=1&NodeID=" + root[i].Value + "&ParentCode=" + root[i].TreeNodeID + "&ParentName=" + root[i].Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "/" + root[i].TreeNodeID + "\"><img src='/Content/images/_line1.gif'/>" + root[i].Text + "</a></li>");
        //                    else
        //                        sb.AppendLine("<li><a  href=\"/" + controllerAction + "?IsFinalLevel=1&NodeID=" + root[i].Value + "&ParentCode=" + root[i].TreeNodeID + "&ParentName=" + root[i].Text + "&Other=" + treeModel.Other + "&currentPath=" + currentPath + "/" + root[i].TreeNodeID + "\"><img src='/Content/images/_line1.gif'/>" + root[i].Text + "</a></li>");
        //                }
        //            }
        //        }
        //    }
        //    sb.AppendLine("</ul>");
        //    sb.AppendLine("</li>");
        //    return sb.ToString();
        //}
        ////

        #region 排序

        /// <summary>
        /// 排序
        /// <returns></returns>
        public static MvcHtmlString OrderBy(this HtmlHelper helper, List<RankInfo> RankInfos, string FieldName)
        {
            var str = OrderBy(RankInfos, FieldName);
            MvcHtmlString mstr = new MvcHtmlString(str);
            return mstr;
        }

        /// <summary>
        /// 排序
        /// <returns></returns>
        public static string OrderBy(List<RankInfo> RankInfos, string FieldName)
        {
            var iconclass = "";
            var sb = new StringBuilder();
            if (RankInfos.Count > 0)
            {
                if (RankInfos[0].Property == FieldName)
                {
                    if (RankInfos[0].Ascending)
                    {
                        //iconclass = "ascorange";
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon '><i class='caret down'></i></a>", FieldName);
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon disabled'><i class='caret'></i></a>", FieldName);
                    }
                    else
                    {
                        //iconclass = "descorange";
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon disabled'><i class='caret down'></i></a>", FieldName);
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon '><i class='caret'></i></a>", FieldName);
                    }
                }
                else
                {
                    //iconclass = "ascwhite";
                    sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon disabled'><i class='caret down'></i></a>", FieldName);
                    sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon disabled'><i class='caret'></i></a>", FieldName);
                }
            }
            else
            {
                //iconclass = "ascwhite";
                sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon disabled'><i class='caret down'></i></a>", FieldName);
                sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon disabled'><i class='caret'></i></a>", FieldName);
            }
            //MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// 排序
        /// <returns></returns>
        public static MvcHtmlString OrderByNew(this HtmlHelper helper, List<RankInfo> RankInfos, string FieldName)
        {
            var iconclass = "";
            var sb = new StringBuilder();
            if (RankInfos.Count > 0)
            {
                FieldName = "[" + FieldName + "]";
                if (RankInfos[0].Property == FieldName)
                {
                    if (RankInfos[0].Ascending)
                    {
                        //iconclass = "ascorange";
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon '><i class='caret down'></i></a>", FieldName);
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon disabled'><i class='caret'></i></a>", FieldName);
                    }
                    else
                    {
                        //iconclass = "descorange";
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon disabled'><i class='caret down'></i></a>", FieldName);
                        sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon '><i class='caret'></i></a>", FieldName);
                    }
                }
                else
                {
                    //iconclass = "ascwhite";
                    sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby ascorange sort-icon disabled'><i class='caret down'></i></a>", FieldName);
                    sb.AppendFormat("<a href='javascript:void(0)' data-field='" + FieldName + "' class='fieldorderby descorange sort-icon disabled'><i class='caret'></i></a>", FieldName);
                }
            }
            else
            {
                //iconclass = "ascwhite";
                sb.AppendFormat("<a href='javascript:void(0)' data-field='[" + FieldName + "' class='fieldorderby ascorange sort-icon disabled'><i class='caret down'></i></a>", FieldName);
                sb.AppendFormat("<a href='javascript:void(0)' data-field='[" + FieldName + "' class='fieldorderby descorange sort-icon disabled'><i class='caret'></i></a>", FieldName);
            }
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        ///// <summary>
        ///// 排序
        ///// <returns></returns>
        //public static MvcHtmlString OrderBy(this HtmlHelper helper, List<RankInfo> RankInfos, string FieldName)
        //{
        //    var iconclass = ""; ;
        //    if (RankInfos.Count > 0)
        //    {
        //        if (RankInfos[0].Property == FieldName)
        //        {
        //            if (RankInfos[0].Ascending)
        //                iconclass = "ascorange";
        //            else
        //                iconclass = "descorange";
        //        }
        //        else
        //            iconclass = "ascwhite";
        //    }
        //    else
        //        iconclass = "ascwhite";
        //    MvcHtmlString mstr = new MvcHtmlString(iconclass);
        //    return mstr;
        //}

        #endregion
        

        #region 菜单

        #region 菜单

        //public static MvcHtmlString MainMenu(this HtmlHelper html)
        //{
        //    var controll = html.ViewContext.Controller as BaseController;
        //    //var html.GetRouteString("Controller").ToLower()
        //    //var area = "";
        //    //if (controll.RouteData.DataTokens["area"] != null)
        //    //    area = controll.RouteData.DataTokens["area"].ToString();
        //    //var constrollName = controll.RouteData.Values["controller"].ToString();
        //    //var actionName = controll.RouteData.Values["action"].ToString();
        //    List<SoftProjectAreaEntity> Sys_PremSets = controll.LoginMenu();

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
        //    //var path = controll.Request.ApplicationPath;
        //    var path = "";
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

        //    var root = Sys_PremSets.Where(p => p.PremSetParentID == 0).OrderBy(p => p.OrderNum).ToList();
        //    var count = root.Count();
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            var active = "";
        //            var bopen = false;
        //            if (currpath.Contains("/" + root[i].Sys_PremSetID + "/"))
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
        //            sb.AppendLine("        <span>" + root[i].PremSetName + "</span>");
        //            sb.AppendLine("    </a>");
        //            sb.AppendLine(BindMainMenu(Sys_PremSets, (int)root[i].Sys_PremSetID, currpath, path, bopen));
        //            sb.AppendLine("</li>");
        //        }
        //    }

        //    sb.AppendLine("</ul>");//结束

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindMainMenu(List<SoftProjectAreaEntity> Sys_PremSets, int Sys_PermSetID, string currpath, string path, bool bopen)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (bopen)
        //        sb.AppendLine("<ul class='submenu' style='display: block;'>");
        //    else
        //        sb.AppendLine("<ul class='submenu'>");
        //    var root = Sys_PremSets.Where(p => p.PremSetParentID == Sys_PermSetID).OrderBy(p => p.OrderNum).ToList();
        //    int count = root.Count;
        //    if (count > 0)
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            var selected = "";
        //            if (currpath == "/" + Sys_PermSetID + "/" + root[i].Sys_PremSetID + "/")
        //                selected = "selected";

        //            sb.AppendLine("<li>");
        //            sb.AppendLine("    <a class='" + selected + "'  href='" + path + root[i].ActionPath + "'>");
        //            sb.AppendLine("        <span>" + root[i].PremSetName + "</span>");
        //            sb.AppendLine("    </a>");
        //            sb.AppendLine("</li>");

        //        }
        //    }
        //    sb.AppendLine("</ul>");

        //    return sb.ToString();
        //}

        #endregion


        //public static MvcHtmlString MainMenu(this HtmlHelper html)
        //{
        //    var controll = html.ViewContext.Controller as BaseController;
        //    //var html.GetRouteString("Controller").ToLower()
        //    //var area = "";
        //    //if (controll.RouteData.DataTokens["area"] != null)
        //    //    area = controll.RouteData.DataTokens["area"].ToString();
        //    //var constrollName = controll.RouteData.Values["controller"].ToString();
        //    //var actionName = controll.RouteData.Values["action"].ToString();
        //    List<SoftProjectAreaEntity> Sys_PremSets = controll.LoginMenu();

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
        //    //var path = controll.Request.ApplicationPath;
        //    var path = "";

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

        //    //var root =  Sys_PremSets.Where(p => p.PremSetParentID == 0).OrderBy(p => p.OrderNum).ToList();
        //    //var count = root.Count();
        //    //if (count > 0)
        //    //{
        //    //    for (int i = 0; i < count; i++)
        //    //    {
        //    //        var active = "";
        //    //        var bopen = false;
        //    //        if (currpath.Contains("/" + root[i].Sys_PremSetID + "/"))
        //    //        {
        //    //            active = "active";
        //    //            bopen = true;
        //    //        }
        //    //        sb.AppendLine("<li class='" + active + "'>");
        //    //        //sb.AppendLine("    <a class='" + selected + "'  href='" + path + root[i].ActionPath + "'>");
        //    //        if (root[i].ActionPath == "")
        //    //            sb.AppendLine("    <a href='#'>");
        //    //        else
        //    //            sb.AppendLine("    <a href='/" + root[i].ActionPath + "'>");

        //    //        sb.AppendLine("        <i class='glyphicon glyphicon-th-large'></i>");
        //    //        sb.AppendLine("        <span>" + root[i].PremSetName + "</span>");
        //    //        sb.AppendLine("    </a>");
        //    //        sb.AppendLine(BindMainMenu(Sys_PremSets, (int)root[i].Sys_PremSetID, currpath, path, bopen));
        //    //        sb.AppendLine("</li>");
        //    //    }
        //    //}

        //    sb.AppendLine("</ul>");//结束

        //    MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
        //    return mstr;
        //}

        //private static string BindMainMenu(List<SoftProjectAreaEntity> Sys_PremSets, int Sys_PermSetID, string currpath, string path, bool bopen)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (bopen)
        //        sb.AppendLine("<ul class='submenu' style='display: block;'>");
        //    else
        //        sb.AppendLine("<ul class='submenu'>");
        //    //var root = Sys_PremSets.Where(p => p.PremSetParentID == Sys_PermSetID).OrderBy(p => p.OrderNum).ToList();
        //    //int count = root.Count;
        //    //if (count > 0)
        //    //{
        //    //    for (int i = 0; i < count; i++)
        //    //    {
        //    //        var selected = "";
        //    //        if (currpath == "/" + Sys_PermSetID + "/" + root[i].Sys_PremSetID + "/")
        //    //            selected = "selected";

        //    //        sb.AppendLine("<li>");
        //    //        sb.AppendLine("    <a class='" + selected + "'  href='" + path + root[i].ActionPath + "'>");
        //    //        sb.AppendLine("        <span>" + root[i].PremSetName + "</span>");
        //    //        sb.AppendLine("    </a>");
        //    //        sb.AppendLine("</li>");

        //    //    }
        //    //}
        //    sb.AppendLine("</ul>");

        //    return sb.ToString();
        //}

        #endregion

        #region 树

        public static MvcHtmlString JqTree(this HtmlHelper helper, string ID, TreeList treeList, string selectTreeNode, bool parentLink = true, bool bcontroll = false)
        {
            string urlpath = HttpContext.Current.Request.Url.AbsolutePath;

            string PKField = treeList.ValueField;

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

                        sb.AppendLine("<li class='" + liclass + "'>");
                        sb.AppendLine(string.Format("<a " + selectIselected + " href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'><i class='glyphicon " + icon + "'></i>{4}</a>", urlpath + treeList.ControllerAction, "Item." + PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));
                        sb.AppendLine(JqBindTree(root[i], treeNodes, treeList.ControllerAction, treeList.RootValue + "/" + root[i].TreeNodeID + "/", PKField, selectTreeNode, 1, parentLink, i == count - 1, urlpath, bopen));
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
                        sb.AppendLine(string.Format("<li class='" + liclass + "'><a " + selectIselected + " href='{0}?IsFinalLevel=0&{1}={2}&currentPath={3}'>{4}</a></li>", urlpath + "/" + treeList.ControllerAction, "Item." + PKField, root[i].Value, treeList.RootValue + "/" + root[i].TreeNodeID + "/", root[i].Text));
                    }
                }
            }

            sb.AppendLine("</ul></div>");
            MvcHtmlString mstr = new MvcHtmlString(sb.ToString());
            return mstr;
        }

        private static string JqBindTree(TreeNode treeModel, IEnumerable<TreeNode> treeModels, string controllerAction, string currentPath, string PKField, string selectTreeNode, int lev, bool parentLink, bool blast, string urlpath, bool bopen)
        {
            StringBuilder sb = new StringBuilder();
            if (bopen)
                sb.AppendLine("<ul class='sub' style='display: block;'>");
            else
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
                    sb.AppendLine(string.Format("<a " + selectIselected + " href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'><i class='glyphicon " + icon + "'></i>{4}</a>", urlpath + controllerAction, "Item." + PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
                    sb.AppendLine(JqBindTree(root[i], treeModels, controllerAction, currentPath + root[i].TreeNodeID + "/", PKField, selectTreeNode, lev, parentLink, i == count - 1, urlpath, bopen));
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
                    sb.AppendLine(string.Format("<li class='" + liclass + "'><a  " + selectIselected + " href='{0}?IsFinalLevel=1&{1}={2}&currentPath={3}'>{4}</a></li>", urlpath + controllerAction, "Item." + PKField, root[i].Value, currentPath + root[i].TreeNodeID + "/", root[i].Text));
                }
            }

            sb.AppendLine("</ul>");

            return sb.ToString();
        }


        #endregion

        public static string IsActive(this HtmlHelper html, string controllName1)
        {
            var controllName = html.ViewContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            return controllName == controllName1 ? "active" : "";
        }

        public static string IsActive(this HtmlHelper html, string controllName1, string ActionName)
        {
            var controllName = html.ViewContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            var actionName = html.ViewContext.Controller.ControllerContext.RouteData.Values["action"].ToString();

            if (controllName == controllName1 && actionName == ActionName)
                return "active";
            return "";
        }

        public static MvcHtmlString RenderBreadcrumb(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();

            BaseController controller = helper.ViewContext.Controller as BaseController;

            if (controller != null && controller.Breadcrumb != null)
            {
                var breadcrumb = controller.Breadcrumb;
                if (breadcrumb != null)
                {
                    //sb.Append("<ul class=\"page-breadcrumb breadcrumb\">");
                    //breadcrumb myjuxing
                    sb.Append("<ul class=\"breadcrumb myjuxing\">");
                    if (breadcrumb.Root != null)
                    {
                        //sb.Append("<i class=\"icon-home\"></i>" + breadcrumb.Root.Name + "" + breadcrumb.Separator);
                        //if (string.IsNullOrWhiteSpace(breadcrumb.Root.URL))
                        //{
                        //    sb.Append("" + breadcrumb.Root.Name + "");// + breadcrumb.Separator);
                        //}
                        //else
                        //{
                        //    //<li><i class=\"fa fa-angle-right\"></i></li>
                        //    //<span class="icon-home"></span>
                        //    sb.Append("<li><span class='icon-home'></span><a href=\"" + breadcrumb.Root.URL + "\">" + breadcrumb.Root.Name + "</a></li>");
                        //}
                        sb.Append("<li><span class='icon-home'></span><a href=\"/Home/Index\">" + breadcrumb.Root.Name + "</a></li>");
                    }

                    if (breadcrumb.Items != null)
                    {
                        breadcrumb.Items.ForEach(m =>
                        {
                            if (string.IsNullOrWhiteSpace(m.URL))
                            {
                                sb.Append("<li>" + m.Name + "</li>");//+ breadcrumb.Separator + "
                            }
                            else
                            {
                                sb.Append("<li><a href='" + m.URL + "'>" + m.Name + "</a></li>");
                            }

                        });
                    }
                    if (helper.ViewBag.Title == null)
                    {
                        sb.Append("<li>" + breadcrumb.CurrentName + "</li>");
                    }
                    else
                    {
                        sb.Append("<li>" + helper.ViewBag.Title + "</li>");
                    }
                    sb.Append("</ul>");
                }
            }

            return new MvcHtmlString(sb.ToString()); ;
        }

    }
}
