
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using System.IO;
using System.Web;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Design_ModularOrFunControllDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunControll_Domain()
        {
            PKField = "Design_ModularOrFunControllID";
            //PKFields = new List<string> { "Design_ModularOrFunControllID" };
            TableName = "Design_ModularOrFunControll";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunControll_PKCheck()
        {
            if (Item.Design_ModularOrFunControllID == null)
            {
                throw new Exception("功能模块字段主键不能为空！");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_GetByID()
        {
            Design_ModularOrFunControll_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunControll] A WHERE Design_ModularOrFunControllID={0} ", Item.Design_ModularOrFunControllID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 权限管理系统--缓存：获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunControll_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            string sql = "SELECT * FROM V_Design_ModularOrFunControll";
            var resp = Query16(sql, 2);
            return resp.Items;
        }

        /// <summary>
        /// 根据功能模块ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_GetByModularOrFunID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunControll] A WHERE Design_ModularOrFunID={0} Order By Sort", Item.Design_ModularOrFunID);
            var resp = Query16(sql);

            return resp;
        }

        public MyResponseBase Design_ModularOrFunControll_EditListSave()
        {
            Design_ModularOrFunControll_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularOrFunControll_GetByModularOrFunID();
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Design_ModularOrFunControlls.ForEach(p =>
                    { p.Design_ModularOrFunID = Item.Design_ModularOrFunID; });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunControllID).Except(from o in Item.Design_ModularOrFunControlls select o.Design_ModularOrFunControllID);
                    var updateItems = Item.Design_ModularOrFunControlls.Where(p => p.Design_ModularOrFunControllID != null);
                    var addItems = Item.Design_ModularOrFunControlls.Where(p => p.Design_ModularOrFunControllID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFunControll] WHERE  Design_ModularOrFunControllID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFunControll_Domain();
                        //var DBFieldVals = "Sort,Design_ModularOrFunID,Design_ModularOrFunID1,Design_ModularOrFunDomainID,ControllName,ControllActionPath,ActionMethod,ActionName,ParamName,ViewName,ControllProgramCode,BPrem,ActionMethodCn,DataRightDict,BMenu,ToolbarButtonAreaWidth,TableWidth,ControllModularOrFunCode";
                        var DBFieldVals = "Sort,Design_ModularOrFunID,Design_ModularOrFunID1,Design_ModularOrFunDomainID,ControllName,ActionMethod,ParamName,BPrem,ActionMethodCn,DataRightDict,ControllModularOrFunCode,ControllProgramCode,ActionName,ViewName";

                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFunControll_Domain();
                        //var DBFieldVals = "Sort,Design_ModularOrFunID,Design_ModularOrFunID1,Design_ModularOrFunDomainID,ControllName,ControllActionPath,ActionMethod,ActionName,ParamName,ViewName,ControllProgramCode,BPrem,ActionMethodCn,DataRightDict,BMenu,ToolbarButtonAreaWidth,TableWidth";
                        var DBFieldVals = "Sort,Design_ModularOrFunID,Design_ModularOrFunID1,Design_ModularOrFunDomainID,ControllName,ActionMethod,ParamName,BPrem,ActionMethodCn,DataRightDict,ControllModularOrFunCode,ControllProgramCode,ActionName,ViewName";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

                    scope.Complete();
                    ProjectCache.Design_ModularOrFunBtnControlls_Clear();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    scope.Dispose();
                }
            }
            #endregion

            return resp;
        }

        ///// <summary>
        ///// 生成控制器记录
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFunControll_EditListBulidRecord()
        //{
        //    Design_ModularOrFunControll_Domain();
        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //    #region 功能模块对象
        //    var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
        //    #endregion

        //    #region 获取实体字段
        //    var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
        //    var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
        //    var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

        //    var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
        //    var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

        //    #endregion

        //    #region 查询
        //    var selectItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--查询",
        //        ActionMethod = "Index",
        //        ActionName = 1,
        //        ParamName = null,//主键名
        //        ViewName = 1,//IndexP
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 100
        //    };
        //    #endregion

        //    #region 添加查询
        //    var addItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--添加查询",
        //        ActionMethod = "Add",
        //        ActionName = 1,
        //        ParamName = null,//主键名
        //        ViewName = 20,//EditP
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 105
        //    };
        //    #endregion

        //    #region 添加保存
        //    var addSaveItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--添加保存",
        //        ActionMethod = "AddSave",
        //        ActionName = 2,
        //        ParamName = null,//主键名
        //        ViewName = null,//EditP
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 110
        //    };
        //    #endregion

        //    #region 编辑查询
        //    var editItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
        //        ActionMethod = "Edit",
        //        ActionName = 1,
        //        ParamName = PKField,//主键名
        //        ViewName = 20,//EditP
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 115
        //    };
        //    #endregion

        //    #region 编辑保存
        //    var editSaveItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
        //        ActionMethod = "EditSave",
        //        ActionName = 2,
        //        ParamName = null,
        //        ViewName = null,
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 120
        //    };
        //    #endregion

        //    //Item.Items.Clear();
        //    if (Item.Design_ModularOrFunControlls == null)
        //        Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
        //    Item.Design_ModularOrFunControlls.Add(selectItem);
        //    Item.Design_ModularOrFunControlls.Add(addItem);
        //    Item.Design_ModularOrFunControlls.Add(addSaveItem);
        //    Item.Design_ModularOrFunControlls.Add(editItem);
        //    Item.Design_ModularOrFunControlls.Add(editSaveItem);

        //    var resp = Design_ModularOrFunControll_EditListSave();
        //    return resp;
        //}

        public void Design_ModularOrFunControll_CheckBulid()
        {
            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            var sqltemp = "SELECT * FROM Design_ModularOrFunControll A WHERE Design_ModularOrFunID=" + Item.Design_ModularOrFunID;
            var controllTemps = Query16(sqltemp);
            if (controllTemps.Items.Count > 0)
                throw new Exception("已生成过控制器，只能生成1次");        
        }

        /// <summary>
        /// 生成控制器记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecord()
        {
            Design_ModularOrFunControll_CheckBulid();
            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            var sqltemp = "SELECT * FROM Design_ModularOrFunControll A WHERE Design_ModularOrFunID=" + Item.Design_ModularOrFunID;
            var controllTemps = Query16(sqltemp);
            if (controllTemps.Items.Count > 0)
                throw new Exception("已生成过控制器，只能生成1次");
            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 查询
            StringBuilder sbcode = new StringBuilder();

            ///// <summary>
            ///// 用户管理--查询
            ///// </summary>
            ///// <param name="domain"></param>
            ///// <returns></returns>
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult Index(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("//if (!domain.Querys.QueryDicts.ContainsKey(\"{0}___equal\"))", FKFields));
            sbcode.AppendLine("//{");
            sbcode.AppendLine(string.Format("//    if (domain.Item.{0} == null)", FKFields));
            sbcode.AppendLine("        //throw new Exception(\"主键不能为空\");");
            sbcode.AppendLine(string.Format("    //domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", FKFields));
            sbcode.AppendLine("//}");

            //sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Index\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");


            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查询",
                ActionMethod = "Index",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "查询",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 1
            };
            #endregion

            #region 添加查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Add(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            var resp = domain.Default();");
            sbcode.AppendLine("            #region 初始化代码");
            sbcode.AppendLine("            #endregion");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Add\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var addItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加查询",
                ActionMethod = "Add",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "添加",
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 3
            };
            #endregion

            #region 添加保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            #region 初始值 ");
            sbcode.AppendLine("            #endregion");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.AddSave();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var addSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加保存",
                ActionMethod = "AddSave",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 2,
                ParamName = null,//主键名
                ViewName = null,//EditP
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 5
            };
            #endregion

            #region 编辑查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Edit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            //sbcode.AppendLine("            ModularOrFunCode = "AuthorizationAreas.Pre_User.Detail";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Edit\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var editItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
                ActionMethod = "Edit",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "编辑",
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 7
            };
            #endregion

            #region 编辑保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine("            var resp = domain.EditSave();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var editSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
                ActionMethod = "EditSave",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 9
            };
            #endregion

            #region 查看查询
            sbcode = new StringBuilder();

            sbcode.AppendLine("        public ActionResult Detail(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Detail\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            resp.FunNameEn = \"Detail\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var DetailItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查看",
                ActionMethod = "Detail",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "查看",
                ControllModularOrFunCode = string.Format("{0}.{1}.Detail", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 11
            };
            #endregion

            //Item.Items.Clear();
            if (Item.Design_ModularOrFunControlls == null)
                Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(addItem);
            Item.Design_ModularOrFunControlls.Add(addSaveItem);
            Item.Design_ModularOrFunControlls.Add(editItem);
            Item.Design_ModularOrFunControlls.Add(editSaveItem);
            Item.Design_ModularOrFunControlls.Add(DetailItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        /// <summary>
        /// 生成控制器记录:新建-提交-审核
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecord010416()
        {
            Design_ModularOrFunControll_CheckBulid();

            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 我的查询
            StringBuilder sbcode = new StringBuilder();

            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult Index(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("//if (!domain.Querys.QueryDicts.ContainsKey(\"{0}___equal\"))", FKFields));
            sbcode.AppendLine("//{");
            sbcode.AppendLine(string.Format("//    if (domain.Item.{0} == null)", FKFields));
            sbcode.AppendLine("        //throw new Exception(\"主键不能为空\");");
            sbcode.AppendLine(string.Format("    //domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", FKFields));
            sbcode.AppendLine("//}");

            //sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Index\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");


            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = "我的"+Design_ModularOrFun.ModularName + "--列表",
                ActionMethod = "Index",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "我的" + Design_ModularOrFun.ModularName + "--列表",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 1
            };
            #endregion

            #region 添加查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Add(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            var resp = domain.Default();");
            sbcode.AppendLine("            resp.Item.FillTabDate= DateTime.Now;");
            sbcode.AppendLine("            resp.Item.FillTabPersonID= LoginInfo.Sys_LoginInfoID;");
            sbcode.AppendLine("            resp.Item.FillTabPerson= LoginInfo.UserName;");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Add\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var addItem = new SoftProjectAreaEntity
            {
                ControllName ="我的"+ Design_ModularOrFun.ModularName + "--添加查询",
                ActionMethod = "Add",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "添加",
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 3
            };
            #endregion

            #region 添加保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("             //domain.Item.Pre_CompanyID = LoginInfo.CompanyID;");
            sbcode.AppendLine("             domain.Item.AuditStatuID = 1;");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.AddSave();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var addSaveItem = new SoftProjectAreaEntity
            {
                ControllName ="我的"+ Design_ModularOrFun.ModularName + "--添加保存",
                ActionMethod = "AddSave",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 2,
                ParamName = null,//主键名
                ViewName = null,//EditP
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 5
            };
            #endregion

            #region 编辑查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Edit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            //sbcode.AppendLine("            ModularOrFunCode = "AuthorizationAreas.Pre_User.Detail";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Edit\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var editItem = new SoftProjectAreaEntity
            {
                ControllName ="我的"+ Design_ModularOrFun.ModularName + "--编辑查询",
                ActionMethod = "Edit",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "编辑",
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 7
            };
            #endregion

            #region 编辑保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine("            var resp = domain.EditSave();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var editSaveItem = new SoftProjectAreaEntity
            {
                ControllName = "我的" + Design_ModularOrFun.ModularName + "--编辑保存",
                ActionMethod = "EditSave",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 9
            };

            #endregion

            #region 提交保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult Submit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("         domain.Item.AuditStatuID = 4;");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine("            var resp = domain.EditSave();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var submitSaveItem = new SoftProjectAreaEntity
            {
                ControllName = "我的" + Design_ModularOrFun.ModularName + "--提交保存",
                ActionMethod = "Submit",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Submit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 11
            };

            #endregion

            #region 我的查询--查看
            sbcode = new StringBuilder();

            sbcode.AppendLine("        public ActionResult Detail(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Detail\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            resp.FunNameEn = \"Detail\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var DetailItem = new SoftProjectAreaEntity
            {
                ControllName ="我的"+ Design_ModularOrFun.ModularName + "--查看",
                ActionMethod = "Detail",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "查看",
                ControllModularOrFunCode = string.Format("{0}.{1}.Detail", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 13
            };
            #endregion

            #region 待审核查询
            sbcode = new StringBuilder();

            sbcode.AppendLine("        [HttpGet]");
            sbcode.AppendLine("        public ActionResult IndexWaitAudit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            domain.Querys.Add(new Query { QuryType = 0, FieldName = \"AuditStatuID___equal\", Value = \"4\" });");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexWaitAudit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));

            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            if (Request.IsAjaxRequest())");
            sbcode.AppendLine("                return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var indexWaitExamine = new SoftProjectAreaEntity
            {
                ControllName = "待审核" + Design_ModularOrFun.ModularName,
                ActionMethod = "IndexWaitExamine",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "待审核" + Design_ModularOrFun.ModularName,
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexWaitAudit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 20
            };
            #endregion

            #region 审核查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Audit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            //sbcode.AppendLine("            ModularOrFunCode = "AuthorizationAreas.Pre_User.Detail";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Audit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");

            sbcode.AppendLine("            resp.Item.AuditDate= DateTime.Now;");
            sbcode.AppendLine("            resp.Item.AuditPersonID= LoginInfo.Sys_LoginInfoID;");
            sbcode.AppendLine("            resp.Item.AuditPerson= LoginInfo.UserName;");

            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Audit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Audit\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var auditItem = new SoftProjectAreaEntity
            {
                ControllName ="待审核"+ Design_ModularOrFun.ModularName + "--审核查询",
                ActionMethod = "Audit",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "审核查询",
                ControllModularOrFunCode = string.Format("{0}.{1}.Audit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 22
            };
            #endregion

            #region 审核保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult AuditSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Audit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine("            domain.Item.AuditStatuID = 16;");
            sbcode.AppendLine("            var resp = domain.EditSave();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var auditSaveItem = new SoftProjectAreaEntity
            {
                ControllName = "待审核" + Design_ModularOrFun.ModularName + "--审核保存",
                ActionMethod = "AuditSave",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Audit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 24
            };

            #endregion


            #region 查询列表

            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpGet]");
            sbcode.AppendLine("        public ActionResult IndexSearch(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            domain.Querys.Add(new Query { QuryType = 0, FieldName = \"AuditStatuID___equal\", Value = \"16\" });");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexSearch\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            if (Request.IsAjaxRequest())");
            sbcode.AppendLine("                return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var indexSearchtem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "查询--列表",
                ActionMethod = "IndexSearch",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexSearch", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 26
            };

            #endregion

            #region 查询--查看

            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Detail2(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Detail2\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var detail2Item = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "查询--查看",
                ActionMethod = "Detail2",
                ControllProgramCode = sbcode.ToString(),
                //DonationID
                ActionName = 2,
                ParamName = PKField,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Detail2", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 28
            };

            #endregion


            //Item.Items.Clear();
            if (Item.Design_ModularOrFunControlls == null)
                Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(addItem);
            Item.Design_ModularOrFunControlls.Add(addSaveItem);
            Item.Design_ModularOrFunControlls.Add(editItem);
            Item.Design_ModularOrFunControlls.Add(editSaveItem);
            Item.Design_ModularOrFunControlls.Add(submitSaveItem);
            Item.Design_ModularOrFunControlls.Add(DetailItem);

            Item.Design_ModularOrFunControlls.Add(indexWaitExamine);
            Item.Design_ModularOrFunControlls.Add(auditItem);
            Item.Design_ModularOrFunControlls.Add(auditSaveItem);

            Item.Design_ModularOrFunControlls.Add(indexSearchtem);
            Item.Design_ModularOrFunControlls.Add(detail2Item);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        /// <summary>
        /// 生成控制器记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordByOrderDetailTemplete()
        {
            Design_ModularOrFunControll_CheckBulid();

            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            //var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            //var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()
            PKField = Design_ModularOrFun.PrimaryKey;
            var MainPKField = Design_ModularOrFun.MainPrimaryKey;
            #endregion

            #region 编辑列表

            StringBuilder sbcode = new StringBuilder();

            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine("    var resp = new MyResponseBase();");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexEdit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    if (domain.Item.{0} == null)", MainPKField));
            sbcode.AppendLine("    {");
            sbcode.AppendLine("        throw new Exception(\"主键不能为空\");");
            sbcode.AppendLine("    }");
            sbcode.AppendLine(string.Format("    domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", MainPKField));
            sbcode.AppendLine("    resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");

            var indexEditItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑列表",
                ActionMethod = ".C_OrderID.",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = MainPKField,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "编辑列表",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexEdit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 1
            };
            #endregion

            #region 查看列表
            sbcode = new StringBuilder();

            sbcode.AppendLine("        [HttpGet]");
            sbcode.AppendLine("        public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("            if (domain.Item.{0} == null)", MainPKField));
            sbcode.AppendLine("                throw new Exception(\"主键不能为空！\");");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexDetail\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("            domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", MainPKField));
            sbcode.AppendLine("            var resp = domain.QueryIndex();");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var indexDetailItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查看列表",
                ActionMethod = "IndexDetail",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = MainPKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "查看列表",
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexDetail", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 3
            };
            #endregion

            #region Popup
            sbcode = new StringBuilder();
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult Popup(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Popup\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine("    domain.PageQueryBase.PageSize = 10000;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");

            var popupItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--Popup",
                ActionMethod = "Popup",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                //ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "Popup",
                ControllModularOrFunCode = string.Format("{0}.{1}.Popup", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 7
            };
            #endregion

            #region Rows
            sbcode = new StringBuilder();
            sbcode.AppendLine("public ActionResult Rows(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    var resp = domain.{0}_Rows();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexEdit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    return View(\"Rows\", resp);");
            sbcode.AppendLine("}");

            var rowsItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--Rows",
                ActionMethod = "Rows",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Rows", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 9
            };
            #endregion

            //Item.Items.Clear();
            if (Item.Design_ModularOrFunControlls == null)
                Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
            Item.Design_ModularOrFunControlls.Add(indexEditItem);
            Item.Design_ModularOrFunControlls.Add(indexDetailItem);
            Item.Design_ModularOrFunControlls.Add(popupItem);
            Item.Design_ModularOrFunControlls.Add(rowsItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        /// <summary>
        /// 生成控制器记录--附件
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordByAtt()
        {
            Design_ModularOrFunControll_CheckBulid();
            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 编辑查询
            StringBuilder sbcode = new StringBuilder();

            ///// <summary>
            ///// 用户管理--查询
            ///// </summary>
            ///// <param name="domain"></param>
            ///// <returns></returns>
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult IndexEdit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", FKFields));
            //sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexEdit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");


            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑列表",
                ActionMethod = "IndexEdit",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = FKFields,//PKField,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "编辑列表",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexEdit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 1
            };
            #endregion

            #region 查看列表
            sbcode = new StringBuilder();

            ///// <summary>
            ///// 用户管理--查询
            ///// </summary>
            ///// <param name="domain"></param>
            ///// <returns></returns>
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult IndexDetail(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", FKFields));
            //sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexDetail\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");

            var indexDetailItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查看列表",
                ActionMethod = "IndexDetail",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = FKFields,//PKField,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "查看列表",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexDetail", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 1
            };
            #endregion

            #region 上传
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpGet]");
            sbcode.AppendLine("        public ActionResult UpLoad(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            #region 保存文件");
            sbcode.AppendLine("");
            sbcode.AppendLine("            if (domain.Item.RefPKTableGuid == null)");
            sbcode.AppendLine("                throw new Exception(\"附件Guid不能为空\")");
            sbcode.AppendLine("            domain.Item.AttachmentFileSizeDisp = ProjectCommon.FileSizeDisp(domain.Item.AttachmentFileSize);");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Upload\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var respadd = domain.AddSave();");
            sbcode.AppendLine("            #endregion");
            sbcode.AppendLine("");
            sbcode.AppendLine("            #region 查询");
            sbcode.AppendLine(string.Format("            if (respadd.Item.{0} == null)", PKField));
            sbcode.AppendLine("                throw new Exception(\"添加失败\");");
            sbcode.AppendLine("            domain.Item.Doc_DocmentAttachmentID = respadd.Item.Doc_DocmentAttachmentID;");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexEdit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            resp.Items.Add(resp.Item);");
            sbcode.AppendLine("            resp.FunNameEn = \"Detail\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("");
            sbcode.AppendLine("            #endregion");
            sbcode.AppendLine("            return View(\"Rows\", resp);");
            sbcode.AppendLine("        }");

            var uploadItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--上传",
                ActionMethod = "UpLoad",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "上传",
                ControllModularOrFunCode = string.Format("{0}.{1}.UpLoad", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 3
            };
            #endregion

            #region 删除
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult Delete(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Delete\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.DeleteByID();");
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var deleteItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--删除",
                ActionMethod = "Delete",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 2,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = null,//EditP
                ControllModularOrFunCode = string.Format("{0}.{1}.Delete", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 5
            };
            #endregion

            #region 查看查询
            sbcode = new StringBuilder();

            sbcode.AppendLine("        public ActionResult Detail(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Detail\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            resp.FunNameEn = \"Detail\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var DetailItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查看",
                ActionMethod = "Detail",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "查看",
                ControllModularOrFunCode = string.Format("{0}.{1}.Detail", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 11
            };
            #endregion

            #region 下载列表
            sbcode = new StringBuilder();

            ///// <summary>
            ///// 附件管理--下载列表
            ///// </summary>
            ///// <param name="domain"></param>
            ///// <returns></returns>
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult IndexDown(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", FKFields));
            //sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexDown\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");


            var downItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--下载列表",
                ActionMethod = "IndexDown",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = FKFields,//PKField,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "下载列表",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 11
            };
            #endregion

            #region 图片显示列表
            sbcode = new StringBuilder();

            ///// <summary>
            ///// 附件管理--下载列表
            ///// </summary>
            ///// <param name="domain"></param>
            ///// <returns></returns>
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult IndexImage(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            //sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine(string.Format("domain.Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = domain.Item.{0}.ToString() }});", FKFields));
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.IndexImage\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");

            var imageItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--图片列表",
                ActionMethod = "IndexImage",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = FKFields,//PKField,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "图片列表",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.IndexImage", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 13
            };
            #endregion

            //Item.Items.Clear();
            if (Item.Design_ModularOrFunControlls == null)
                Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(indexDetailItem);


            Item.Design_ModularOrFunControlls.Add(uploadItem);
            Item.Design_ModularOrFunControlls.Add(deleteItem);
            Item.Design_ModularOrFunControlls.Add(DetailItem);
            Item.Design_ModularOrFunControlls.Add(downItem);
            Item.Design_ModularOrFunControlls.Add(imageItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        /// <summary>
        /// 生成控制器记录-弹窗
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordPopup()
        {
            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 查询
            StringBuilder sbcode = new StringBuilder();

            ///// <summary>
            ///// 用户管理--查询
            ///// </summary>
            ///// <param name="domain"></param>
            ///// <returns></returns>
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult Index(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine("    //domain.Querys.Add(new Query { QuryType = 0, FieldName = \"Pre_CompanyID___equal\", Value = LoginInfo.CompanyID.ToString() });");
            sbcode.AppendLine("");
            //sbcode.AppendLine("    ModularOrFunCode = \"AuthorizationAreas.Pre_User.Index\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Index\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("    var resp = domain.QueryIndex();");
            sbcode.AppendLine("");
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(Design_ModularOrFun.PartialView, resp);");
            sbcode.AppendLine("    resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("    return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");


            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查询",
                ActionMethod = "Index",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "查询",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 1
            };
            #endregion

            #region 添加查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Add(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine("            var resp = domain.Default();");
            sbcode.AppendLine("            #region 初始化代码");
            sbcode.AppendLine("            #endregion");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Add\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var addItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加查询",
                ActionMethod = "Add",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "添加",
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 3
            };
            #endregion

            #region 添加保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("            if (domain.Item.Parent{0} == null)", PKField));
            sbcode.AppendLine(string.Format("                domain.Item.Parent{0} = 0;", PKField));

            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.AddSave();");
            sbcode.AppendLine(string.Format("             domain.{0}_AddCache();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var addSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加保存",
                ActionMethod = "AddSave",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 2,
                ParamName = null,//主键名
                ViewName = null,//EditP
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 5
            };
            #endregion

            #region 编辑查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("        public ActionResult Edit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            //sbcode.AppendLine("            ModularOrFunCode = "AuthorizationAreas.Pre_User.Detail";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            resp.FunNameEn = \"Edit\";");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("        }");

            var editItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
                ActionMethod = "Edit",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "编辑",
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 7
            };
            #endregion

            #region 编辑保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("        [HttpPost]");
            sbcode.AppendLine("        public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("            if (domain.Item.Parent{0} == null)", PKField));
            sbcode.AppendLine(string.Format("                domain.Item.Parent{0} = 0;", PKField));
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("");
            sbcode.AppendLine("            var resp = domain.EditSave();");
            sbcode.AppendLine(string.Format("             domain.{0}_UpdateCache();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("        }");

            var editSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
                ActionMethod = "EditSave",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 9
            };
            #endregion

            #region 查看查询--无此代码
            //sbcode = new StringBuilder();

            //sbcode.AppendLine("        public ActionResult Detail(SoftProjectAreaEntityDomain domain)");
            //sbcode.AppendLine("        {");
            //sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Detail\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            //sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            //sbcode.AppendLine("            var resp = domain.ByID();");
            //sbcode.AppendLine("");
            //sbcode.AppendLine("            resp.FunNameEn = \"Detail\";");
            //sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            //sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            //sbcode.AppendLine("        }");

            //var DetailItem = new SoftProjectAreaEntity
            //{
            //    ControllName = Design_ModularOrFun.ModularName + "--查看",
            //    ActionMethod = "Detail",
            //    ControllProgramCode = sbcode.ToString(),
            //    ActionName = 1,
            //    ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
            //    ViewName = 30,//PopupEdit
            //    BPrem = 1,
            //    ActionMethodCn = "查看",
            //    ControllModularOrFunCode = string.Format("{0}.{1}.Detail", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
            //    Design_ModularOrFunID = Item.Design_ModularOrFunID,
            //    Sort = 11
            //};
            #endregion

            #region Row
            sbcode = new StringBuilder();

            sbcode.AppendLine("        public ActionResult Row(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("        {");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Index\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.ByID();");
            sbcode.AppendLine("            resp.Items.Add(resp.Item);");

            sbcode.AppendLine("             resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("             return View(\"Rows\", resp);");
            sbcode.AppendLine("        }");

            var rowItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--Row",
                ActionMethod = "Row",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "Row",
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 13
            };
            #endregion

            //Item.Items.Clear();
            if (Item.Design_ModularOrFunControlls == null)
                Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(addItem);
            Item.Design_ModularOrFunControlls.Add(addSaveItem);
            Item.Design_ModularOrFunControlls.Add(editItem);
            Item.Design_ModularOrFunControlls.Add(editSaveItem);
            //Item.Design_ModularOrFunControlls.Add(DetailItem);
            Item.Design_ModularOrFunControlls.Add(rowItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        ///// <summary>
        ///// 生成控制器记录
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordPopup()
        //{
        //    Design_ModularOrFunControll_Domain();
        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //    #region 功能模块对象
        //    var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
        //    #endregion

        //    #region 获取实体字段
        //    var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
        //    var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
        //    var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

        //    var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
        //    var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

        //    #endregion

        //    #region 查询
        //    var selectItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--查询",
        //        ActionMethod = "Index",
        //        ActionName = 1,
        //        ParamName = null,//主键名
        //        ViewName = 1,//IndexP
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 100
        //    };
        //    #endregion

        //    #region 添加查询
        //    var addItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--添加查询",
        //        ActionMethod = "Add",
        //        ActionName = 1,
        //        ParamName = null,//主键名
        //        ViewName = 30,//PopupEdit
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 105
        //    };
        //    #endregion

        //    #region 添加保存
        //    var addSaveItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--添加保存",
        //        ActionMethod = "AddSave",
        //        ActionName = 2,
        //        ParamName = null,//主键名
        //        ViewName = null,//EditP
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 110
        //    };
        //    #endregion

        //    #region 编辑查询
        //    var editItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
        //        ActionMethod = "Edit",
        //        ActionName = 1,
        //        ParamName = PKField,//主键名
        //        ViewName = 30,//PopupEdit
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 115
        //    };
        //    #endregion

        //    #region 编辑保存
        //    var editSaveItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
        //        ActionMethod = "EditSave",
        //        ActionName = 2,
        //        ParamName = null,
        //        ViewName = null,
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 120
        //    };
        //    #endregion

        //    #region Row
        //    var rowItem = new SoftProjectAreaEntity
        //    {
        //        ControllName = Design_ModularOrFun.ModularName + "--Row",
        //        ActionMethod = "Row",
        //        ActionName = 1,
        //        ParamName = PKField,
        //        ViewName = 11,
        //        Design_ModularOrFunID = Item.Design_ModularOrFunID,
        //        Sort = 125
        //    };
        //    #endregion

        //    //Item.Items.Clear();
        //    if (Item.Design_ModularOrFunControlls == null)
        //        Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
        //    Item.Design_ModularOrFunControlls.Add(selectItem);
        //    Item.Design_ModularOrFunControlls.Add(addItem);
        //    Item.Design_ModularOrFunControlls.Add(addSaveItem);
        //    Item.Design_ModularOrFunControlls.Add(editItem);
        //    Item.Design_ModularOrFunControlls.Add(editSaveItem);
        //    Item.Design_ModularOrFunControlls.Add(rowItem);

        //    var resp = Design_ModularOrFunControll_EditListSave();
        //    return resp;
        //}

        /// <summary>
        /// 生成控制器记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordPopup_Old()
        {
            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 查询
            StringBuilder sbcode = new StringBuilder();
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult Index(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    var resp = domain.{0}_Index();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Index\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    if (Request.IsAjaxRequest())");
            sbcode.AppendLine("        return View(\"IndexPContext\", resp);");
            sbcode.AppendLine("    return View(\"IndexPFrame\", resp);");
            sbcode.AppendLine("}");

            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查询",
                ActionMethod = "Index",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 1,//IndexP
                BPrem = 1,
                ActionMethodCn = "查询",
                //DataRightDict="",
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 100
            };

            #endregion
            //return null;
            #region 添加查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("public ActionResult Add(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine("    var resp = domain.Default();");
            sbcode.AppendLine("    resp.FunNameEn = \"Add\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Add\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    return View(\"PopupEdit\", resp);");
            sbcode.AppendLine("}");

            var addItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加查询",
                ActionMethod = "Add",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = null,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "添加",
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 105
            };
            #endregion

            #region 添加保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("[HttpPost]");
            sbcode.AppendLine("[ActionName(\"AddSave\")]");
            sbcode.AppendLine("public HJsonResult AddSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    var resp = domain.{0}_AddSave();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("}");

            var addSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加保存",
                ActionMethod = "AddSave",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 2,
                ParamName = null,//主键名
                ViewName = null,//EditP
                ControllModularOrFunCode = string.Format("{0}.{1}.Add", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 110
            };
            #endregion

            #region 编辑查询
            sbcode = new StringBuilder();
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult Edit(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    var resp = domain.{0}_ByIDEdit();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Edit\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    return View(\"PopupEdit\", resp);");
            sbcode.AppendLine("}");

            var editItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
                ActionMethod = "Edit",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = Design_ModularOrFun.ControllCode + "ID",//PKField,//主键名
                ViewName = 30,//PopupEdit
                BPrem = 1,
                ActionMethodCn = "编辑",
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 115
            };
            #endregion

            #region 编辑保存
            sbcode = new StringBuilder();
            sbcode.AppendLine("[HttpPost]");
            sbcode.AppendLine("[ActionName(\"EditSave\")]");
            sbcode.AppendLine("public HJsonResult EditSave(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    var resp = domain.{0}_EditSave();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    return new HJsonResult(new { Data = resp });");
            sbcode.AppendLine("}");
            var editSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
                ActionMethod = "EditSave",
                ControllProgramCode = sbcode.ToString(),

                ActionName = 2,
                ParamName = null,
                ViewName = null,
                ControllModularOrFunCode = string.Format("{0}.{1}.Edit", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 120
            };
            #endregion

            #region Row
            sbcode = new StringBuilder();

            sbcode.AppendLine("public ActionResult Row(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    var resp = domain.{0}_ByID();", Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("     resp.Items.Add(resp.Item);");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.Index\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("    ");
            sbcode.AppendLine("    ");
            sbcode.AppendLine("    return View(\"Rows\", resp);");
            sbcode.AppendLine("}");

            var rowItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--Row",
                ActionMethod = "Row",
                ControllProgramCode = sbcode.ToString(),
                ActionName = 1,
                ParamName = PKField,
                ViewName = 11,
                ControllModularOrFunCode = string.Format("{0}.{1}.Index", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode),
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 125
            };
            #endregion

            //Item.Items.Clear();
            if (Item.Design_ModularOrFunControlls == null)
                Item.Design_ModularOrFunControlls = new List<SoftProjectAreaEntity>();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(addItem);
            Item.Design_ModularOrFunControlls.Add(addSaveItem);
            Item.Design_ModularOrFunControlls.Add(editItem);
            Item.Design_ModularOrFunControlls.Add(editSaveItem);
            Item.Design_ModularOrFunControlls.Add(rowItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }


        /// <summary>
        /// 生成控制器记录：领域模型的相关表
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordDomainRef()
        {
            Design_ModularOrFunControll_CheckBulid();

            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 查询
            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查询",
                ActionMethod = "Index",
                ActionName = 1,
                ParamName = FKFields,//主键名
                ViewName = 15,//IndexPTab
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 100
            };
            #endregion

            #region 添加查询
            var addItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加查询",
                ActionMethod = "Add",
                ActionName = 1,
                ParamName = FKFields,//主键名
                ViewName = 20,//EditP
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 105
            };
            #endregion

            #region 添加保存
            var addSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加保存",
                ActionMethod = "AddSave",
                ActionName = 2,
                ParamName = null,//主键名
                ViewName = null,//EditP
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 110
            };
            #endregion

            #region 编辑查询
            var editItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
                ActionMethod = "Edit",
                ActionName = 2,
                ParamName = FKFields + "," + PKField,//主键名
                ViewName = 20,//EditP
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 115
            };
            #endregion

            #region 编辑保存
            var editSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
                ActionMethod = "EditSave",
                ActionName = 2,
                ParamName = null,
                ViewName = null,
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 120
            };
            #endregion

            //Item.Items.Clear();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(addItem);
            Item.Design_ModularOrFunControlls.Add(addSaveItem);
            Item.Design_ModularOrFunControlls.Add(editItem);
            Item.Design_ModularOrFunControlls.Add(editSaveItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        /// <summary>
        /// 生成控制器记录：领域模型的相关表
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidRecordDomainRefPopup()
        {
            Design_ModularOrFunControll_CheckBulid();

            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).Select(p => p.name).ToList();
            var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 查询
            var selectItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--查询",
                ActionMethod = "Index",
                ActionName = 1,
                ParamName = FKFields,//主键名
                ViewName = 15,//IndexPTab
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 100
            };
            #endregion

            #region 添加查询
            var addItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加查询",
                ActionMethod = "Add",
                ActionName = 1,
                ParamName = FKFields,//主键名
                ViewName = 30,//EditP
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 105
            };
            #endregion

            #region 添加保存
            var addSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--添加保存",
                ActionMethod = "AddSave",
                ActionName = 2,
                ParamName = null,//主键名
                ViewName = null,//EditP
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 110
            };
            #endregion

            #region 编辑查询
            var editItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑查询",
                ActionMethod = "Edit",
                ActionName = 2,
                ParamName = FKFields + "," + PKField,//主键名
                ViewName = 30,//EditP
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 115
            };
            #endregion

            #region 编辑保存
            var editSaveItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--编辑保存",
                ActionMethod = "EditSave",
                ActionName = 2,
                ParamName = null,
                ViewName = null,
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 120
            };
            #endregion

            #region Row
            var rowItem = new SoftProjectAreaEntity
            {
                ControllName = Design_ModularOrFun.ModularName + "--Row",
                ActionMethod = "Row",
                ActionName = 1,
                ParamName = PKField,
                ViewName = 1,
                Design_ModularOrFunID = Item.Design_ModularOrFunID,
                Sort = 125
            };
            #endregion

            //Item.Items.Clear();
            Item.Design_ModularOrFunControlls.Add(selectItem);
            Item.Design_ModularOrFunControlls.Add(addItem);
            Item.Design_ModularOrFunControlls.Add(addSaveItem);
            Item.Design_ModularOrFunControlls.Add(editItem);
            Item.Design_ModularOrFunControlls.Add(editSaveItem);
            Item.Design_ModularOrFunControlls.Add(rowItem);

            var resp = Design_ModularOrFunControll_EditListSave();
            return resp;
        }

        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_EditListBulidControll()
        {
            Design_ModularField_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    //(1)保存或更新数据库数据，更新表结构
                    Design_ModularOrFunControll_EditListSave();

                    //(2)创建控制器
                    #region 1)查询模块功能
                    var resptemp = Design_ModularOrFun_GetByID();
                    var Design_ModularOrFun = resptemp.Item;
                    #endregion

                    #region 3)根据功能模块ID查询所有字段
                    resptemp = Design_ModularOrFunControll_GetByModularOrFunID();
                    var Design_ModularOrFunControlls = resptemp.Items;
                    #endregion
                    StringBuilder sbentity = new StringBuilder();
                    #region 名称空间引用

                    sbentity.AppendLine("using Framework.Core;");
                    sbentity.AppendLine("using Framework.Web.Mvc;");
                    sbentity.AppendLine("using SoftProject.Domain;");
                    sbentity.AppendLine("using System;");
                    sbentity.AppendLine("using System.Collections.Generic;");
                    sbentity.AppendLine("using System.Linq;");
                    sbentity.AppendLine("using System.Text;");
                    sbentity.AppendLine("using System.Web;");
                    sbentity.AppendLine("using System.Web.Mvc;");

                    #endregion

                    sbentity.AppendLine();
                    sbentity.AppendLine("namespace SoftPlatform.Controllers");
                    sbentity.AppendLine("{");
                    sbentity.AppendLine("    /// <summary>");
                    sbentity.AppendLine(string.Format("    /// 控制器：{0}({1})", Design_ModularOrFun.ControllCode, Design_ModularOrFun.ModularName));
                    sbentity.AppendLine("    /// </summary>");
                    sbentity.AppendLine("    public class " + Design_ModularOrFun.ControllCode + "Controller : BaseController");
                    sbentity.AppendLine("    {");
                    sbentity.AppendLine("        public " + Design_ModularOrFun.ControllCode + "Controller()");
                    sbentity.AppendLine("        {");
                    sbentity.AppendLine("        }");
                    sbentity.AppendLine();

                    //                    NewPage(Design_ModularOrFun, Design_ModularOrFunControlls, sbentity);
                    foreach (var item in Design_ModularOrFunControlls)
                    {
                        sbentity.AppendLine("        /// <summary>");
                        sbentity.AppendLine("        /// " + item.ControllName);
                        sbentity.AppendLine("        /// </summary>");
                        sbentity.AppendLine("        /// <param name=\"domain\"></param>");
                        sbentity.AppendLine("        /// <returns></returns>");
                        sbentity.AppendLine(HttpUtility.UrlDecode(item.ControllProgramCode));

                    }
                    sbentity.AppendLine("   }");
                    sbentity.AppendLine("}");

                    //F:\软件项目\SoftPlatformProject\SoftPlatform\SoftPlatform\Areas\C_CustomerAreas\CellModel
                    //string filepath1=System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    //string filepath2=System.Environment.CurrentDirectory ;
                    //string filepath3=System.IO.Directory.GetCurrentDirectory();  
                    //string filepath4=System.AppDomain.CurrentDomain.BaseDirectory;
                    var path = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\Controllers\\", Design_ModularOrFun.AreasCode);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //string filepath5 = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\CellModel\\{1}.cs", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
                    string filepath5 = path + Design_ModularOrFun.ControllCode + "Controll.cs";

                    FileStream fs = new FileStream(filepath5, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(65001));
                    sw.Write(sbentity.ToString());
                    sw.Flush();
                    sw.Close();
                    fs.Close();


                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    scope.Dispose();
                }
            }
            #endregion

            return resp;
        }

        private void NewPage(SoftProjectAreaEntity Design_ModularOrFun, List<SoftProjectAreaEntity> Design_ModularOrFunControlls, StringBuilder sbentity)
        {
            foreach (var item in Design_ModularOrFunControlls)
            {
                //public ActionResult Index(SoftProjectAreaEntityDomain domain)
                //{
                //    var resp = domain.BC_ExpertGuidance_Index();
                //    resp.ModularOrFunCode = "BC_PartnerAreas.BC_ExpertGuidance";
                //    ;
                //    
                //    return View("IndexPTab", resp);
                //}
                #region 获取ModularOrFunCode
                var ModularOrFunCode = "";
                if (item.Design_ModularOrFunID1 != null)
                {
                    if (item.Design_ModularOrFunID1 == Item.Design_ModularOrFunID)
                        ModularOrFunCode = Design_ModularOrFun.ModularOrFunCode;
                    else
                    {
                        SoftProjectAreaEntityDomain domintemp = new SoftProjectAreaEntityDomain();
                        domintemp.Item = new SoftProjectAreaEntity { Design_ModularOrFunID = item.Design_ModularOrFunID1 };
                        var resptemp1 = domintemp.Design_ModularOrFun_GetByID();
                        ModularOrFunCode = resptemp1.Item.ModularOrFunCode;
                    }
                }
                #endregion

                #region 视图名称

                var ViewName = "";
                if (item.ViewName == 1)
                    ViewName = "IndexP";
                else if (item.ViewName == 2)
                    ViewName = "IndexPFrame";
                else if (item.ViewName == 3)
                    ViewName = "IndexPContext";
                else if (item.ViewName == 4)
                    ViewName = "IndexPChild";

                else if (item.ViewName == 11)
                    ViewName = "Row";
                else if (item.ViewName == 12)
                    ViewName = "Rows";
                else if (item.ViewName == 15)
                    ViewName = "IndexPTab";

                else if (item.ViewName == 20)
                    ViewName = "EditP";
                else if (item.ViewName == 21)
                    ViewName = "EditPFrame";
                else if (item.ViewName == 22)
                    ViewName = "EditPContext";
                else if (item.ViewName == 26)
                    ViewName = "EditArea";

                else if (item.ViewName == 30)
                    ViewName = "PopupEdit";
                else if (item.ViewName == 31)
                    ViewName = "EditPartial";

                #endregion

                var ActionName = item.ActionName == 1 ? "ActionResult" : "HJsonResult";
                sbentity.AppendLine("        /// <summary>");
                sbentity.AppendLine("        /// " + item.ControllName);
                sbentity.AppendLine("        /// </summary>");
                sbentity.AppendLine("        /// <param name=\"domain\"></param>");
                sbentity.AppendLine("        /// <returns></returns>");
                if (item.ActionName == 1)
                {
                    sbentity.AppendLine(string.Format("        public ActionResult {0}(SoftProjectAreaEntityDomain domain)", item.ActionMethod));
                    sbentity.AppendLine("        {");

                    sbentity.AppendLine(string.Format("            var resp = domain.{0}_{1}();", Design_ModularOrFun.ControllCode, item.ActionMethod));
                    sbentity.AppendLine("            resp.ModularOrFunCode = \"" + ModularOrFunCode + "\";");
                    sbentity.AppendLine("            ");
                    sbentity.AppendLine("            ");
                    sbentity.AppendLine(string.Format("            return View(\"{0}\", resp);", ViewName));

                    sbentity.AppendLine("        }");
                }
                else
                {
                    sbentity.AppendLine(string.Format("        public HJsonResult {0}(SoftProjectAreaEntityDomain domain)", item.ActionMethod));
                    sbentity.AppendLine("        {");
                    sbentity.AppendLine(string.Format("            var resp = domain.{0}_{1}();", Design_ModularOrFun.ControllCode, item.ActionMethod));
                    sbentity.AppendLine("            return new HJsonResult(new { Data = resp });");
                    sbentity.AppendLine("        }");
                }
                sbentity.AppendLine();
            }
        }

        /// <summary>
        /// 根据功能模块ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunControll_Popup()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }

            var Design_ModularOrFun = Design_ModularOrFun_GetByID();
            if (Design_ModularOrFun.Item.GroupModularOrFun == 3)
            {
                Item.Design_ModularOrFunID = Design_ModularOrFun.Item.Design_ModularOrFunParentID;
            }

            StringBuilder sbsql1 = new StringBuilder();
            sbsql1.AppendLine("");
            sbsql1.AppendLine("SELECT DISTINCT A.Design_ModularOrFunID,ModularName ");
            sbsql1.AppendLine("FROM Design_ModularOrFun A");
            sbsql1.AppendLine("JOIN [dbo].[Design_ModularOrFunControll] B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID");

            var resp1 = Query16(sbsql1.ToString());
            if (Item.Design_ModularOrFunID == null)
                Item.Design_ModularOrFunID = resp1.Items.First().Design_ModularOrFunID;

            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunControll] A WHERE Design_ModularOrFunID={0} ", Item.Design_ModularOrFunID);
            if (Item.Design_ModularOrFunControllIDs != null)
                sql += string.Format(" AND Design_ModularOrFunControllID NOT IN({0})", Item.Design_ModularOrFunControllIDs);
            sql += " ORDER BY Sort";
            var resp = Query16(sql);
            resp.Items = resp.Items.OrderBy(p => p.Sort).ToList();
            resp.Item.Design_ModularOrFuns = resp1.Items;
            return resp;
        }

        public MyResponseBase Design_ModularOrFunControll_Row()
        {
            //Design_ModularOrFunID
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            var resp = Default();
            StringBuilder sbcode = new StringBuilder();
            sbcode.AppendLine("//[HttpPost]");
            sbcode.AppendLine("[HttpGet]");
            sbcode.AppendLine("public ActionResult|HJsonResult ZZZ(SoftProjectAreaEntityDomain domain)");
            sbcode.AppendLine("{");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.ZZZ\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            domain.Design_ModularOrFun = Design_ModularOrFun;");
            sbcode.AppendLine("            var resp = domain.MMM();");
            sbcode.AppendLine("");
            sbcode.AppendLine("            resp.FunNameEn = \"ZZZ\";");
            sbcode.AppendLine(string.Format("    ModularOrFunCode = \"{0}.{1}.ZZZ\";", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode));
            sbcode.AppendLine("            if (Request.IsAjaxRequest())");
            sbcode.AppendLine("                 resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            resp.ViewContextName = Design_ModularOrFun.PartialView;");
            sbcode.AppendLine("            return View(Design_ModularOrFun.MainView, resp);");
            sbcode.AppendLine("}");

            resp.Item.ControllProgramCode = sbcode.ToString();
            return resp;
        }
    }
}
