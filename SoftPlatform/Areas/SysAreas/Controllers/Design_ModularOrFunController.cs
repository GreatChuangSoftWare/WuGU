using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;
using SoftProject.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SoftPlatform.Controllers
{
    public class Design_ModularOrFunController : BaseController
    {
        public Design_ModularOrFunController()
        {
        }

        #region 模块

        [HttpGet]
        public ActionResult IndexByModular(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_GetModular(1);
            //resp.FunNameEn = "EditList";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("IndexByModular", resp);
        }

        #endregion

        [HttpGet]
        public ActionResult Add(SoftProjectAreaEntityDomain domain)
        {
            var resp = new MyResponseBase();
            resp.FunNameEn = "Add";
            return View("Add", resp);
        }

        [HttpPost]
        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_AddSave();
            return new HJsonResult(new { Data = resp });
        }


        #region 编辑

        [HttpGet]
        public ActionResult Edit(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_GetByID();
            resp.FunNameEn = "Edit";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            //if (resp.Item.GroupModularOrFun == 3)
            //    return View("EditFun", resp);
            return View("Edit", resp);
        }

        [HttpPost]
        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_EditSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        #region 子功能

        [HttpGet]
        public ActionResult EditList(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_GetByModularOrFunParentID(3);
            resp.FunNameEn = "EditList";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("EditList", resp);
        }

        [HttpPost]
        public HJsonResult EditListSave(SoftProjectAreaEntityDomain domain)
        {
            domain.Item.Design_ModularOrFuns.ForEach(p => p.TSql = Server.UrlDecode(p.TSql));
            //domain.Item.TSql = Server.UrlDecode(domain.Item.TSql);
            var resp = domain.Design_ModularOrFun_EditListSave();
            return new HJsonResult(new { Data = resp });
        }

        #endregion

        public ActionResult Row(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Default();
            return View("Row", resp);
        }

        /// <summary>
        /// 生成记录
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult BulidRecord(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_BulidRecord();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成记录：添加-提交-审核
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult BulidRecord010416(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_BulidRecord010416();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成记录
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult BulidRecordByOrderDetailTemplete(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_BulidRecordByOrderDetailTemplete();
            return new HJsonResult(new { Data = resp });
        }

        /// <summary>
        /// 生成记录--附件
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        [HttpPost]
        public HJsonResult BulidRecordAtt(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_BulidRecordByAtt();
            return new HJsonResult(new { Data = resp });
        }

        [HttpPost]
        public HJsonResult BulidRecordPopup(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_BulidRecordPopup();
            return new HJsonResult(new { Data = resp });
        }

        #region 权限

        [HttpGet]
        public ActionResult EditListPrem(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_GetPremByModularOrFunParentID();
            resp.FunNameEn = "EditList";
            //resp.FunNameCn = "编辑";
            //resp.FunBtnNameCn = "保存";
            //resp.ModularOrFunCode = "AuthorizationAreas.De_MemberNewP.Edit";
            return View("EditListPrem", resp);
        }

        //[HttpPost]
        //public HJsonResult EditListPremSave(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.Design_ModularOrFun_EditListPremSave();
        //    return new HJsonResult(new { Data = resp });
        //}

        //public ActionResult RowPrem(SoftProjectAreaEntityDomain domain)
        //{
        //    var resp = domain.Default();
        //    return View("RowPrem", resp);
        //}


        #endregion

        [HttpPost]
        public HJsonResult BulidSql(SoftProjectAreaEntityDomain domain)
        {
            var resp = domain.Design_ModularOrFun_BulidSql();
            return new HJsonResult(new { Data = resp });
        }

        public ActionResult BulidPage(SoftProjectAreaEntityDomain domain)
        {
            if (domain.Item.Design_ModularOrFunID == null)
                throw new Exception("主键不能为空");
            var Design_ModularOrFunID = domain.Item.Design_ModularOrFunID;
            //domain.Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = domain.Design_ModularOrFun_GetByID().Item;
            #endregion

            if (Design_ModularOrFun.PageType == 1)//Table页面
            {
                BulidTable(Design_ModularOrFun);
            }
            else if (Design_ModularOrFun.PageType == 2)//Edit页面
            {
                BulidEditPage(Design_ModularOrFun);
            }
            //var resp = domain.Design_ModularOrFun_BulidPage();
            var url = string.Format("SysAreas/Design_ModularOrFun/EditList?Item.Design_ModularOrFunID=" + domain.Item.Design_ModularOrFunID);
            return Redirect(url);
        }

        #region 生成Edit页面
        public void BulidEditPage(SoftProjectAreaEntity Item)
        {
            //F:\软件项目\中国质量检验协会\SoftPlatform\SoftPlatform\Areas\SysAreas\Template
            //读取文件
            string tempfilepath = Server.MapPath("/") + "Areas\\SysAreas\\Template\\EditPContext.cshtml";
            string strFile = Read(tempfilepath);

            var strFormEleHtml = FormEleHtml(Item);
            strFile = strFile.Replace("<!--strFormEleHtml-->", strFormEleHtml);
            
            var btns = ProjectCache.ModulerBtns(Item.ModularOrFunCode, 3);
            //var btns = LoginModulerBtns(Item.ModularOrFunCode, 3);
            string strBtnFooterHtml=BtnFooterHtml(null,btns,Item);

            strFile = strFile.Replace("<!--strBtnFooterHtml-->", strBtnFooterHtml);


            FileStream fs = new FileStream("C:\\abc.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(strFile);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 页脚按钮
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <returns></returns>
        public  string BtnFooterHtml( object item,
            List<SoftProjectAreaEntity> btns,
            SoftProjectAreaEntity Design_ModularOrFun = null)
        {
            #region 模块信息
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            //if (Design_ModularOrFun.GroupModularOrFun == 3)
            //    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).FirstOrDefault();
            //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == "Add").FirstOrDefault();
            //var conts = helper.ViewContext.Controller as BaseController;
            //if (Design_ModularOrFun == null)
            //    Design_ModularOrFun = conts.Design_ModularOrFun;

            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == modulars.ModularID && p.Page02FormEleType != null).ToList();

            #endregion

            //var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode && (p.OperPos == 3 && p.bValid != 0)).OrderBy(p => p.Sort).ToList();
            //public string ButtonHtml(object item, SoftProjectAreaEntity Design_ModularOrFun, 
            //List<SoftProjectAreaEntity> btns, string btn_xs, string calcols, bool row = true)
            var strHtml = ButtonHtml(null, Design_ModularOrFun, btns, "", "");//calcols);
            //var strHtml = ButtonHtml(helper, item, Design_ModularOrFun, btns, "", "", false);
            return  strHtml;
        }


        /// <summary>
        /// 生成表单元素:调整为混列(最后没有调整)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="item">显示的数据项定义</param>
        /// <param name="pagetype">页面类型：1：新页 2：弹窗</param>
        /// <returns></returns>
        public string FormEleHtml(SoftProjectAreaEntity modulars = null)
        {
            //var Fields = HtmlHelpersProject.PageFormEleTypes(Design_ModularOrFun);
            var pagefields = HtmlHelpersProject.PageFormEleTypes(modulars);
            pagefields = pagefields.OrderBy(p => p.PageFormEleSort).ToList();
            //var pagefields = ProjectCache.Design_ModularPageFields.Where(p => p.ModularOrFunCode == ModularOrFunCode).OrderBy(p => p.TableInfoSort).ToList();
            //var data = Item;// model.Item;
            //if (data == null)
            //    return new MvcHtmlString("没有数据！");
            StringBuilder sbHtml = new StringBuilder();
            //var type = data.GetType();
            int colcopies = 4;
            if (colcopies == 6 && pagefields.Count <= 4)
                colcopies = 12;

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
                //PropertyInfo property = type.GetProperty(field.name);
                //object value = property.GetValue(data, null);
                var val = "@item." + field.name;
                //if (value != null)
                //{
                //    var strval = value.ToString();
                //    val = strval;
                //}
                //if (field.xtype == 61)//日期类型
                //{
                //    if (val != "")
                //    {
                //        if (string.IsNullOrEmpty(field.DisFormat))
                //            val = val.ToDate().ToString("yyyy-MM-dd");
                //        else
                //            val = val.ToDate().ToString(field.DisFormat);
                //    }
                //}
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
                        sbHtml.AppendLine("<li class='col-lg-8 '>");//overflow-auto
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
                        sbHtml.AppendLine("<div class='control-label'>" + val + "&nbsp;</div>");
                    }
                    else if (field.FormEleType == 4)//下拉列表框
                    {
                        //var str = QueryHtmlDropDownList(helper, Querys, item, strDrop);
                        //sbHtml.AppendLine(str.ToString());
                        HtmlDropDownLis(sbHtml, field);
                    }
                    else if (field.FormEleType == 128)//下拉树
                    {
                        HtmlDropTree(sbHtml, field);
                    }
                    else if (field.FormEleType == 256)//图片
                    {
                        #region 图片
                        var filepath = val;
                        sbHtml.AppendLine(string.Format("<img src='{0}' id='Item_{1}'>", field.name, field.name));
                        #endregion
                    }
                    else if (field.FormEleType == 512)//Html编辑器
                    {
                        sbHtml.AppendLine(string.Format("<script id='ueEdit' data-name='{0}' type='text/plain'>", field.name));
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
                        //var str = HtmlHelpers.ChecksButton(helper, field.name, field.NameCn, val);
                        //sbHtml.AppendLine(str.ToString());
                    }
                    else if (field.FormEleType == 8192)
                    {
                        #region
                        //property = type.GetProperty(field.name);
                        //value = property.GetValue(data, null);
                        //if (value != null)
                        //{
                        //    var strval = value.ToString();
                        //    val = strval;
                        //}

                        //var dict = field.name;
                        //if (!string.IsNullOrEmpty(field.Dicts))
                        //    dict = field.Dicts;

                        //var str = HtmlHelpers.DropDownListMultiSelect(helper, "Item." + field.name + "s", ProjectCache.GetByCategory(dict), "DValue", "DText", val, "");
                        //sbHtml.AppendLine(str.ToString());
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
            //var tempstr = helper.Raw(sbHtml.ToString());//sbHtml.ToString());
            return sbHtml.ToString();
        }

        private static void HtmlDropTree(StringBuilder sbHtml, SoftProjectAreaEntity field)
        {
            #region 下拉树
            var str = string.Format("@Html.DropDownForTree(null, \"Item.{0}\", new SelectTreeList(ProjectCache.XXXXXs, \"0\", \"XXXXName\", \"{0}\", \"ParentXXXXID\", \"{0}\", val, true, \"\"), \"\")", field.name);
            sbHtml.AppendLine(str.ToString());
            #endregion
        }


        private static void HtmlDropDownLis(StringBuilder sbHtml, SoftProjectAreaEntity field)
        {
            #region 下拉列表框
            //property = type.GetProperty(field.name);
            //value = property.GetValue(data, null);
            //if (value != null)
            //{
            //    var strval = value.ToString();
            //    val = strval;
            //}

            var dict = field.name;
            if (!string.IsNullOrEmpty(field.Dicts))
                dict = field.Dicts;

            if (ProjectCache.IsExistyCategory(dict))
            {
                var str = string.Format("@Html.DropDownList(\"Item.{0}\", CacheDomain.GetByCategory(\"{1}\"), \"DValue\", \"DText\", item.{0}, \"\", \"\")", field.name, dict);
                //var str = "@Html.";// HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.GetByCategory(Dicts), "DValue", "DText", val, "", "==" + item.NameCn + "==");
                sbHtml.AppendLine(str.ToString());
            }
            else
            {
                var str = string.Format("@Html.DropDownList(\"Item.{0}\", ProjectCache.{0}s, \"{0}\", \"XXXName\", Querys.GetValue({0}___equal), \"\", \"\")", field.name);
                sbHtml.AppendLine(str.ToString());
            }

            #endregion
        }


        #endregion
        #region 生成Table页面

        public void BulidTable(SoftProjectAreaEntity Item)
        {
            //读取文件
            string tempfilepath = Server.MapPath("/");
            string strFile = Read(tempfilepath);
            strFile = strFile.Replace("@modulars.SearchMethod", Item.SearchMethod);
            strFile = strFile.Replace("@modulars.ActionPath", Item.ActionPath);

            #region 工具条按钮

            var btns = ProjectCache.ModulerBtns(Item.ModularOrFunCode, 1);
            var strToolBarHtml = ToolBarHtml(null, Item, btns);
            strFile = strFile.Replace("<!--strToolBarHtml-->", strToolBarHtml);

            #endregion
            #region 工具条查询
            var strQueryHtml = QueryHtml(Item);
            strFile = strFile.Replace("<!--strQueryHtml-->", strQueryHtml);

            #endregion
            #region 表格
            //var strTableHtml = TableHtml(Item);
            //strFile = strFile.Replace("<!--strTableHtml-->", strTableHtml);

            #endregion
        }

        public string TableHtml(IEnumerable Items6, object ItemTotal, List<RankInfo> RankInfos, SoftProjectAreaEntity ModularOrFunCode)
        {
            //var Items = model.Items;
            //var RankInfos = model.PageQueryBase.RankInfos;
            //var ModularOrFunCode = model.ModularOrFunCode;
            var BSort = 0;
            //var Items = Items1.Cast<object>();

            #region 获取相关数据:已抽象
            //var conts = helper.ViewContext.Controller as BaseController;
            //var Design_ModularOrFun = conts.Design_ModularOrFun;// ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == conts.ActionName).FirstOrDefault();

            if (!string.IsNullOrEmpty(Design_ModularOrFun.SortCol))
                BSort = 1;

            //(2)行的DataXX属性：功能、按钮的类型为11=>页面=>字段       "AuthorizationAreas.De_Member"
            //(3)模块行按钮
            var btns = ProjectCache.ModulerBtns(Design_ModularOrFun.ModularOrFunCode, 2);
            //var btns = ModulerBtns(Design_ModularOrFun.ModularOrFunCode, 2);
            //var btns = ProjectCache.Design_ModularOrFunBtns.Where(p => p.ModularOrFunCode == Design_ModularOrFun.ModularOrFunCode && (p.OperPos == 2)).OrderBy(p => p.Sort).ToList();
            //btns = btns.Where(p => p.bValid ==1).ToList();
            //(4)模块列表页的Table字段信息

            var Fields = HtmlHelpersProject.PageFormEleTypes(Design_ModularOrFun);

            //p.PageFormEleSort = p.Page01FormEleSort; p.PageFormElePos = p.Page01FormElePos;

            //td数据
            var TableDispInfos = Fields.Where(p => (((int)p.PageFormElePos) & 1) == 1)
                .OrderBy(p => p.PageFormEleSort).ToList();

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
                var tds = "";// WriteRowThTotalHtml(noHiddelTableDispInfos, ItemTotal, type);
                //var tds = WriteRowTdHtml(TableDispInfos, theadDatas, item, x, type, Design_ModularOrFun,btns);
                sbHead.AppendLine(tds);
            }
            #endregion

            #region (3)表头

            //写入表头
            var theads = HtmlHelpersProject.WriteHead(noHiddelTableDispInfos, null, RankInfos, BSort);
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
            sbBody.AppendLine("@for(var i=0;i<Model.Items.Count;i++)");
            sbBody.AppendLine("{");
            sbBody.AppendLine("     var item=Model.Items[i];");

            //Type type = item.GetType();
            var tdsx = WriteRowTdHtml(noHiddelTableDispInfos, HiddelTableDispInfos, tr_datas.ToList(), null, row, null, Design_ModularOrFun, btns, calcols);
            //var tds = WriteRowTdHtml(TableDispInfos, theadDatas, item, x, type, Design_ModularOrFun,btns);
            sbBody.AppendLine(tdsx);
            //if (posTotal == 0 && x == count - 2)
            //    break;
            x++;
            row++;

            sbBody.AppendLine("}");
            sbBody.AppendLine("</tbody>");
            sbHtml.AppendLine(sbBody.ToString());

            #endregion

            //MvcHtmlString mstr = new MvcHtmlString(sbHtml.ToString());
            return sbHtml.ToString();
        }

        private string WriteRowTdHtml(List<SoftProjectAreaEntity> TableDispInfosKKK,
    List<SoftProjectAreaEntity> HiddelTableDispInfos,
    List<SoftProjectAreaEntity> tr_datas, object itemzz, int row, Type typez, SoftProjectAreaEntity Design_ModularOrFun,
    List<SoftProjectAreaEntity> btns, string calcols)
        {
            StringBuilder strhtml = new StringBuilder("<tr");

            #region 生成tr的data-XXX属性
            var datas = new List<SoftProjectAreaEntity>();
            for (var i = 0; i < tr_datas.Count; i++)
            {
                var datalower = tr_datas[i].name.ToLower();
                //var valdata = GetHtmlVal(tr_datas, item, type, i);
                strhtml.AppendLine(string.Format(" data-{0}='@item.{1}' ", datalower, tr_datas[i].name));
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
                            //var valtemp = GetHtmlVal(HiddelTableDispInfos, item, type, j);
                            var fieldhiddle = HiddelTableDispInfos[m];
                            val += string.Format("<input type='hidden'  id='{0}' name='{0}' value='item.{0}' />", fieldhiddle.name);
                        }
                    }
                    else if (TableDispInfos[j].Design_ModularFieldID == 2)//复选框
                        val += string.Format("<input type='checkbox' class='checkbox1 jq-checkall-item'  />");
                    else if (TableDispInfos[j].Design_ModularFieldID == 3)//操作
                    {
                        #region
                        //var btnsrows = btns.Where(p => string.IsNullOrEmpty(p.DispConditionsExpression)).ToList();
                        //var DispConditionsExpressions = btns.Where(p => !string.IsNullOrEmpty(p.DispConditionsExpression)).ToList();

                        //for (var i = 0; i < DispConditionsExpressions.Count(); i++)//var btn in btns)
                        //{
                        //    var btn = DispConditionsExpressions[i];
                        //    #region 按钮显示条件比较
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
                        //        btnsrows.Add(btn);
                        //    }
                        //    else
                        //        btnsrows.Add(btn);
                        //    #endregion
                        //}
                        #endregion
                        var strval = ButtonHtml(null, Design_ModularOrFun, btns, "btn-xs", calcols);
                        val += strval.ToString();
                    }
                    else if (TableDispInfos[j].Design_ModularFieldID == 5)//上传控件
                        val += string.Format("<input type='file' name='file{{0}}' id='uploadify' />");
                    else
                    {
                        #region 其它字段
                        //var valtemp = GetHtmlVal(TableDispInfos, item, type, j);
                        var valtemp = "";
                        //表单控件类型：1：文本框  2:标签  4：下拉列表框  8：Hidden  16:Radion  32:CheckBox
                        //56：整数   106：小数    167：字符串   61：日期
                        var field = TableDispInfos[j];
                        var fieldtype = "@item." + field.name;

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
                                if (aitem.ParamName != null && aitem.ParamName.Length > 0)//&& item != null)
                                {
                                    #region 对象数据类型
                                    //type = data.GetType();
                                    //Type type = item.GetType();
                                    #endregion

                                    var paramNames = aitem.ParamName.Split(',');
                                    foreach (var param in paramNames)
                                    {
                                        //var property = type.GetProperty(param);
                                        //var value = property.GetValue(item, null);
                                        strParam += "Item." + param + "=@item." + param;
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


        /// <summary>
        /// 新：查询条件位置：由参数确定
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <param name="ModularOrFunCode"></param>
        /// <returns></returns>
        public static string QueryHtml(SoftProjectAreaEntity modulars)// MyResponseBase model)// Querys Querys)
        {
            //var ModularOrFunCode = "";
            //var Querys = model.Querys;
            StringBuilder sbQuery = new StringBuilder();

            var QueryFields = HtmlHelpersProject.QueryFormEleTypes(modulars);

            #region 快速、高级查询

            var fastQueryFields = QueryFields.Where(p => p.QueryPos == 1).ToList();
            var advQueryFields = QueryFields.Where(p => p.QueryPos == 2).ToList();
            if (QueryFields.Count == 0)
                return "";

            sbQuery.AppendLine("<div class='fastWhere'>");
            for (int i = 0; i < fastQueryFields.Count(); i++)
            {
                var item = fastQueryFields[i];
                var strDrop = "";
                if (item.FormEleType == 4)
                {
                    strDrop = QueryHtmlDropDownList(item, strDrop);
                }
                else if (item.FormEleType == 128)
                {
                    strDrop = QueryHtmlDropTree(item);
                }
                else if (item.FormEleType == 8192)//下拉复选框(位)
                {
                    #region 下拉复选框
                    //var val = Querys.GetValue(item.name + "___bitand");//位与

                    if (ProjectCache.IsExistyCategory(item.name))
                    {
                        //NameCn	"状态"	string
                        //var str = HtmlHelpers.DropDownList(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name),          "DValue", "DText", val, "", "==" + item.NameCn + "==");
                        //var str = HtmlHelpers.DropDownListMultiSelect(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name), "DValue", "DText", val, "");

                        //sbHtml.AppendLine(str.ToString());
                        //strDrop = str.ToString();
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
                        //var val = Querys.GetValue(item.name + "___equal");
                        sbQuery.AppendLine("<input type='hidden' class='form-control' id='" + item.name + "___equal' name='" + item.name + "___equal'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='int' value='Querys.GetValue(" + item.name + " ___equal)' />");
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
                        //var val = Querys.GetValue(item.name + "___like");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='Querys.GetValue(" + item.name + " ___equal)' />");
                    }
                    else if (item.QueryType == 2)
                    {
                        //var val = Querys.GetValue(item.name + "___equal");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___equal' name='" + item.name + "___equal'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='Querys.GetValue(" + item.name + " ___equal)' />");
                    }
                    else if (item.QueryType == 3 || item.QueryType == null)
                    {
                        var val1 = "Querys.GetValue(" + item.name + "___greaterequal)";
                        var val2 = "Querys.GetValue(" + item.name + "___lessequal)";
                        sbQuery.AppendLine("<input type='text' class=' form-control " + classcss + "' id='" + item.name + "___greaterequal' name='" + item.name + "___greaterequal'  placeholder='起始" + item.NameCn + "' data-datatype='" + datatype + "' data-fieldnamecn='起始" + item.NameCn + "' value='" + val1 + "' />");
                        sbQuery.AppendLine("<input type='text' class=' form-control " + classcss + "' id='" + item.name + "___lessequal' name='" + item.name + "___lessequal'  placeholder='结束" + item.NameCn + "' data-datatype='" + datatype + "' data-fieldnamecn='结束" + item.NameCn + "' value='" + val2 + "' />");
                    }
                    #endregion
                }
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
                        strDrop = QueryHtmlDropDownList(item, strDrop);
                    }
                    else if (item.FormEleType == 128)
                    {
                        strDrop = QueryHtmlDropTree(item);
                    }
                    else if (item.FormEleType == 8192)//下拉复选框(位)
                    {
                        #region 下拉复选框
                        //var val = Querys.GetValue(item.name + "___bitand");//位与
                        if (ProjectCache.IsExistyCategory(item.name))
                        {
                            //NameCn	"状态"	string
                            //var str = HtmlHelpers.DropDownList(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name),          "DValue", "DText", val, "", "==" + item.NameCn + "==");
                            //var str = HtmlHelpers.DropDownListMultiSelect(helper, item.name + "___bitand", ProjectCache.GetByCategory(item.name), "DValue", "DText", val, "");

                            //sbHtml.AppendLine(str.ToString());
                            //strDrop = str.ToString();
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
                        var val = " Querys.GetValue(" + item.name + "___like)";
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='" + val + "' />");
                        sbQuery.AppendLine("</li>");
                    }
                    else if (item.QueryType == 2)
                    {
                        var val = "Querys.GetValue(" + item.name + "___equal)";
                        sbQuery.AppendLine("<li class='col-sm-4 text-left'>");
                        sbQuery.AppendLine("<input type='text' class='form-control " + classcss + "' id='" + item.name + "___like' name='" + item.name + "___like'  placeholder='" + item.NameCn + "' "
                            + " data-datatype='" + datatype + "' value='" + val + "' />");
                        sbQuery.AppendLine("</li>");
                    }
                    else if (item.QueryType == 3)
                    {
                        var val1 = "Querys.GetValue(" + item.name + "___greaterequal)";
                        var val2 = "Querys.GetValue(" + item.name + "___lessequal)";
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

            #endregion
            #region 自定义查询条件

            #endregion

            //MvcHtmlString mstr = new MvcHtmlString(sbQuery.ToString());
            return sbQuery.ToString();
        }

        /// <summary>
        /// 查询的下拉列表框:1015-7-5
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Querys"></param>
        /// <param name="item"></param>
        /// <param name="strDrop"></param>
        /// <returns></returns>
        private static string QueryHtmlDropDownList(SoftProjectAreaEntity item, string strDrop)
        {
            #region 下拉列表框
            //var val = Querys.GetValue(item.name + "___equal");

            var Dicts = item.name;
            if (!string.IsNullOrEmpty(item.Dicts))
            {
                Dicts = item.Dicts;
            }

            if (ProjectCache.IsExistyCategory(Dicts))
            {
                var str = string.Format("@Html.DropDownList(\"{0}___equal\", CacheDomain.GetByCategory(\"{1}\"), \"DValue\", \"DText\", Querys.GetValue({0}___equal), \"\", \"=={2}==\")", item.name, Dicts, item.NameCn);
                //var str = "@Html.";// HtmlHelpers.DropDownList(helper, item.name + "___equal", ProjectCache.GetByCategory(Dicts), "DValue", "DText", val, "", "==" + item.NameCn + "==");
                strDrop = str.ToString();
            }
            else
            {
                var str = string.Format("@Html.DropDownList(\"{0}___equal\", ProjectCache.{0}s, \"{0}\", \"XXXName\", Querys.GetValue({0}___equal), \"\", \"=={2}==\")", item.name, Dicts, item.NameCn);
                strDrop = str.ToString();
            }

            #endregion
            return strDrop;
        }

        /// <summary>
        /// 查询条件下拉树:2015-7-5
        /// </summary>
        /// <param name="Querys"></param>
        /// <param name="item"></param>
        /// <param name="strDrop"></param>
        /// <returns></returns>
        private static string QueryHtmlDropTree(SoftProjectAreaEntity item)
        {
            #region 下拉树

            var str = string.Format("@Html.DropDownForTree(null, \"ParentXXXXXID___equal\", new SelectTreeList(ProjectCache.TableName, \"0\", \"XXXXName\", \"{0}\", \"ParentXXXXID\", \"{0}\", Querys.GetValue({0}___equal), true, \"\"), \"==MMMM==\")", item.name);
            #endregion
            return str;
        }

        public string ToolBarHtml(object item, SoftProjectAreaEntity Design_ModularOrFun, List<SoftProjectAreaEntity> btns)
        {

            #region 生成calcols：用于表格的列计算，例如：批量删除

            //获取页面的所有字段
            var Fields = HtmlHelpersProject.PageFormEleTypes(Design_ModularOrFun);
            //??
            var TableDispInfos = Fields.Where(p => p.FieldFunTypeID == null || ((((int)p.FieldFunTypeID) & 1) == 1)).ToList();

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

            var strHtml = ButtonHtml(item, Design_ModularOrFun, btns, "", calcols);
            return strHtml;
        }

        public string ButtonHtml(object item, SoftProjectAreaEntity Design_ModularOrFun, List<SoftProjectAreaEntity> btns, string btn_xs, string calcols, bool row = true)
        {
            StringBuilder sb = new StringBuilder();
            //var type = item.GetType();

            //针对Table中有多个按钮时，显示下拉
            var bdrop = false;
            if (btns.Count > 2 && row)
            {
                bdrop = true;
            }

            for (var i = 0; i < btns.Count; i++)//var btn in btns)
            {
                var btn = btns[i];

                //用于生成按钮操作的Url
                var controllss = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                if (bdrop && i == 0)
                {
                    sb.AppendLine("<div class='btn-group'>");
                }
                else if (bdrop)
                    sb.AppendLine("<li>");

                if (btn.BtnType == 2)//上传
                {
                    sb.AppendLine(string.Format("<input type='file' name='uploadify' class='uploadify uploadifyrow' id='uploadify' {0} /><div id='uploadifydiv'></div>", btn.ModularOrFunBtnRemark));
                    continue;
                }

                if (btn.BtnBehavior == 1 || btn.BtnBehavior == 2)
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

                    var pkname = "P_ProductID";
                    var xx = btn.ModularOrFunBtnRemark;//ModularOrFunBtnRemark
                    if (!string.IsNullOrEmpty(btn.ModularOrFunBtnRemark))
                    {
                        pkname = btn.ModularOrFunBtnRemark;
                    }

                    //Design_ModularOrFun.ModularName不对
                    //var controlls = ProjectCache.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnID == btn.Design_ModularOrFunBtnID).ToList();
                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + "  btn-primary btn-FwPopup'  data-masteditarea='{0}'  data-tableselect='{1}'  data-btnnamecn='{3}' data-popupwidth='{4}' data-modularname='{5}' data-popupaddrepeat='{6}'  data-popuppkname='{7}'  data-pkname='{8}'",
                     btn.MastEditArea, btn.TableSelect, controllss[1].ParamName, btn.BtnNameCn, btn.PopupWidth, Design_ModularOrFun.ModularName, btn.popupaddrepeat, controllss[0].ParamName, pkname));

                    var posturl = controllss[0].ActionPath;
                    var targeturl = controllss[1].ActionPath;
                    sb.AppendLine(string.Format(" data-popupurl='{0}' data-targeturl='{1}' ", posturl, targeturl));

                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");
                    #endregion
                }
                else if (btn.BtnBehavior == 101 || btn.BtnBehavior == 102 || btn.BtnBehavior == 105 || btn.BtnBehavior == 111 || btn.BtnBehavior == 310 || btn.BtnBehavior == 311)
                {
                    #region 2个url，url数据由targeturlparamname指定
                    var controlls = controllss;
                    var targeturlparamname = controlls[1].ParamName;// Design_ModularOrFun.ControllCode + "ID";
                    if (btn.BtnBehavior == 310 || btn.BtnBehavior == 311)
                        targeturlparamname = controlls[1].ParamName;

                    sb.AppendLine(string.Format("<a class='btn " + btn_xs + " btn-primary btn-FwBtnSubmit' data-fun='{0}' data-targeturlparamname='{1}' data-masteditarea='{2}' data-childtableselect='{3}' data-targetdom='{4}'  data-tableselect='{5}' data-pkname='{6}' data-btnnamecn='{7}'",
                            btn.BtnBehavior, targeturlparamname, btn.MastEditArea, btn.ChildtableSelect, btn.TargetDom, btn.TableSelect, Design_ModularOrFun.ControllCode + "ID", btn.BtnNameCn));

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
                    var posturl = UrlByControll(item, controllss[0]);
                    if (string.IsNullOrEmpty(btn.BtnSearchMethod))
                        btn.BtnSearchMethod = "Framework.FwSearch";
                    sb.AppendLine(string.Format("<a class='btn btn-primary btn-FwSearch' data-searchmethod='{0}' data-url='{1}' data-targetdom='{2}'", btn.BtnSearchMethod, posturl, btn.TargetDom));

                    sb.AppendLine(">" + btn.BtnNameCn + "</a>");
                }
                else if (btn.BtnBehavior == 41)
                {
                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='41' data-btnnamecn='删除' class='btn btn-primary btn-xs   btn-FwRowUIPopup' data-calcols='" + calcols + "'><span class='glyphicon glyphicon-trash'></span>删除</a>");
                }
                else if (btn.BtnBehavior == 42)
                {
                    var controlls = controllss.First();
                    var posturl = UrlByControll(item, controllss[0]);

                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='42' data-posturl='" + posturl + "' data-btnnamecn='删除' class='btn btn-primary btn-xs   btn-FwRowUIPopup' data-calcols='" + calcols + "'><span class='glyphicon glyphicon-trash'></span>删除</a>");
                }
                else if (btn.BtnBehavior == 51)
                {
                    sb.AppendLine("<a href='javascript:void(0);'  data-fun='51' data-btnnamecn='删除' class='btn btn-primary  btn-BatchUI' data-calcols='" + calcols + "'><span class='glyphicon glyphicon-trash'></span>删除</a>");
                }
                if (bdrop && i == 0)
                {
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
            return sb.ToString();
        }

        #endregion

        private string UrlByControll(object item, SoftProjectAreaEntity controlls)
        {
            var strParam = "?";

            if (controlls.ParamName != null && controlls.ParamName.Length > 0)
            {
                #region 对象数据类型

                //Type type = item.GetType();
                #endregion

                var paramNames = controlls.ParamName.Split(',');
                foreach (var param in paramNames)
                {
                    strParam += "Item." + param + "=@Item." + param;
                }
            }
            var url = controlls.ActionPath + strParam;
            return url;
        }

        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            var str = sr.ReadToEnd();
            //while ((line = sr.ReadLine()) != null)
            //{
            //    Console.WriteLine(line.ToString());
            //}
            return str;
        }
    }
}

