
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
    /// 业务层：Design_ModularOrFunDomainDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunDomain_Domain()
        {
            PKField = "Design_ModularOrFunDomainID";
            //PKFields = new List<string> { "Design_ModularOrFunDomainID" };
            TableName = "Design_ModularOrFunDomain";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunDomain_PKCheck()
        {
            if (Item.Design_ModularOrFunDomainID == null)
            {
                throw new Exception("业务层主键不能为空！");
            }
        }

        public MyResponseBase Design_ModularOrFunDomain_GetByID()
        {
            Design_ModularOrFunDomain_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunDomain] A WHERE Design_ModularOrFunDomainID={0} ", Item.Design_ModularOrFunDomainID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        public MyResponseBase Design_ModularOrFunDomain_Index()
        {
            string sql = "SELECT * FROM Design_ModularOrFunDomain WHERE Design_ModularOrFunID=" + Item.Design_ModularOrFunID + " Order  By  Sort";
            var resp = Query16(sql, 2);
            resp.Item = new SoftProjectAreaEntity { Design_ModularOrFunID = Item.Design_ModularOrFunID };
            return resp;
        }

        public MyResponseBase Design_ModularOrFunDomain_Add()
        {
            var resp = new MyResponseBase();
            resp.Item = new SoftProjectAreaEntity { Design_ModularOrFunID = Item.Design_ModularOrFunID };
            resp.FunNameEn = "Add";
            return resp;
        }

        public MyResponseBase Design_ModularOrFunDomain_AddSave()
        {
            var resp = new MyResponseBase();
            Design_ModularOrFunDomain_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    var DBFieldVals = "";

                    #region (1)添加功能模块
                    //Design_ModularOrFunDomain_PKCheck();

                    Design_ModularOrFunDomain_Domain();
                    DBFieldVals = "ModularOrFunDomainName,MethodName,MethodReturnTypeID,Sort,Design_ModularOrFunID";
                    resp = AddSave(DBFieldVals);

                    #endregion
                    Design_ModularOrFunDomainDetail_EditSave();

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

        /// <summary>
        /// Row
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunDomain_AddSaveMethod()
        {
            MyResponseBase resp = new MyResponseBase();

            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            //Design_ModularOrFunID
            StringBuilder sbentity = new StringBuilder();
            if (Item.DomainTypeTemp == 1)
            {
                #region 构造函数
                sbentity.AppendLine(string.Format("PKFields = new List<string> {{ \"{0}ID\" }};", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("TableName = \"{0}\";", Design_ModularOrFun.ControllCode));
                //sbentity.AppendLine(string.Format("return resp;"));

                //主表
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = "构造函数",
                    MethodName = "Domain",
                    MethodReturnTypeID = 1,
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 1,
                };
                //var resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                //StringBuilder sbsql = new StringBuilder();

                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "构造函数",
                    //Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 1,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                Design_ModularOrFunDomain_AddSave();
                //Design_ModularOrFunDomainDetail_EditSave();

                #endregion
            }
            else if (Item.DomainTypeTemp == 2)
            {
                //new TextValue{Text="主键不为空检查",Value="2"},
                #region 主键不能为空
                //var MethodName = "PKCheck";
                //sbentity.AppendLine(string.Format("        public void {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
                //sbentity.AppendLine(string.Format("       {{"));
                sbentity.AppendLine(string.Format("           if (Item.{0}ID == null)", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("           {{"));
                sbentity.AppendLine(string.Format("               throw new Exception(\"{0}主键不能为空！\");", Design_ModularOrFun.ModularName));
                sbentity.AppendLine(string.Format("           }}"));
                //sbentity.AppendLine(string.Format("       }"));
                //sbentity.AppendLine(string.Format("     return resp;"));

                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = "主键不为空检查",
                    MethodName = "PKCheck",
                    MethodReturnTypeID = 1,
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 6,
                };
                //            resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "主键不为空检查",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 2,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });

                Design_ModularOrFunDomain_AddSave();
                #endregion
            }
            //new TextValue{Text="根据主键查询-显示",Value="13"},
            //new TextValue{Text="根据主键查询-编辑",Value="14"},
            else if (Item.DomainTypeTemp == 13)
            {
                #region 根据主键查询--显示
                sbentity.AppendLine(string.Format("{0}_PKCheck();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("OperCode = \"{0}.ByID\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("resp = Execute();"));
                sbentity.AppendLine(string.Format("return resp;"));

                //主表
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = "根据主键查询--显示",
                    MethodName = "ByID",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 11,
                };
                //resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "根据主键查询--显示",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 13,
                    ParamName = sbentity.ToString(),
                    DBOperCode = Design_ModularOrFun.ControllCode + ".ByID",
                    Serial = 1,
                });

                //Design_ModularOrFunDomainDetail_EditSave();
                Design_ModularOrFunDomain_AddSave();

                #endregion
            }
            else if (Item.DomainTypeTemp == 14)
            {
                #region 根据主键查询--编辑

                sbentity.AppendLine(string.Format("{0}_PKCheck();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("var sql = string.Format(\";SELECT * FROM [dbo].[{0}] A WHERE {0}ID={{0}} \", Item.{0}ID);", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("var resp = Query16(sql, 4);"));
                sbentity.AppendLine(string.Format("return resp;"));

                //主表
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = "根据主键查询--编辑",
                    MethodName = "ByIDEdit",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 11,
                };
                //resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "根据主键查询--编辑",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 14,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });

                //Design_ModularOrFunDomainDetail_EditSave();
                Design_ModularOrFunDomain_AddSave();

                #endregion
            }
            //new TextValue{Text="列表查询",Value="11"},
            //new TextValue{Text="列表查询--初始条件",Value="12"},
            else if (Item.DomainTypeTemp == 11)
            {
                #region 列表查询
                //主表
                sbentity.AppendLine(string.Format("var resp = new MyResponseBase();"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    PageQueryBase.RankInfo = \"UpdateDate|0\";"));
                sbentity.AppendLine(string.Format("}}"));
                sbentity.AppendLine(string.Format(""));
                //sbentity.AppendLine(string.Format("if (!Querys.QueryDicts.ContainsKey(\"{0}ID___equal\"))", Design_ModularOrFun.ControllCode));
                //sbentity.AppendLine(string.Format("{{"));
                //sbentity.AppendLine(string.Format("    Querys.Add(new Query { QuryType = 0, FieldName = \"{0}ID___equal\", Value = \"\" });", Design_ModularOrFun.ControllCode));
                //sbentity.AppendLine(string.Format("}}"));
                //sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("//SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity"));
                sbentity.AppendLine(string.Format("//{{"));
                sbentity.AppendLine(string.Format("//    DBTSql =string.Format(\";WITH T1000 AS (SELECT * FROM [dbo].[{0}] A  WHERE 1=1 sqlplaceholder )", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("//    DBOperType = 8,//排序分页"));
                sbentity.AppendLine(string.Format("//    SelectSubType = SelectType,"));
                sbentity.AppendLine(string.Format("//    DBSelectResultType = 2,"));
                sbentity.AppendLine(string.Format("//    EqualQueryParam = \"\""));
                sbentity.AppendLine(string.Format("//}};"));
                sbentity.AppendLine(string.Format("//Sys_HOperControl = hOperControl;"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("OperCode = \"{0}.Index\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("resp = Execute();"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("resp.Querys = Querys;"));
                sbentity.AppendLine(string.Format("resp.Item = Item;"));
                sbentity.AppendLine(string.Format("return resp;"));

                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = string.Format("列表查询"),
                    MethodName = "Index",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 11,
                };
                //resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("列表查询"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 11,
                    ParamName = sbentity.ToString(),
                    DBOperCode = Design_ModularOrFun.ControllCode + ".Index",
                    Serial = 1,
                });

                //Design_ModularOrFunDomainDetail_EditSave();
                Design_ModularOrFunDomain_AddSave();

                #endregion
            }
            else if (Item.DomainTypeTemp == 12)
            {
                #region 列表查询-带初值
                //主表
                sbentity.AppendLine(string.Format("var resp = new MyResponseBase();"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    PageQueryBase.RankInfo = \"UpdateDate|0\";"));
                sbentity.AppendLine(string.Format("}}"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("if (!Querys.QueryDicts.ContainsKey(\"{0}ID___equal\"))", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}ID___equal\", Value = \"\" }});", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("}}"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("//SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity"));
                sbentity.AppendLine(string.Format("//{{"));
                sbentity.AppendLine(string.Format("//    DBTSql =string.Format(\";WITH T1000 AS (SELECT * FROM [dbo].[{0}] A  WHERE 1=1 sqlplaceholder )", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("//    DBOperType = 8,//排序分页"));
                sbentity.AppendLine(string.Format("//    SelectSubType = SelectType,"));
                sbentity.AppendLine(string.Format("//    DBSelectResultType = 2,"));
                sbentity.AppendLine(string.Format("//    EqualQueryParam = \"\""));
                sbentity.AppendLine(string.Format("//}};"));
                sbentity.AppendLine(string.Format("//Sys_HOperControl = hOperControl;"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("OperCode = \"{0}.Index\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("resp = Execute();"));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("resp.Querys = Querys;"));
                sbentity.AppendLine(string.Format("resp.Item = Item;"));
                sbentity.AppendLine(string.Format("return resp;"));

                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = string.Format("列表查询"),
                    MethodName = "Index",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 11,
                };
                //resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("列表查询"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 12,
                    ParamName = sbentity.ToString(),
                    DBOperCode = Design_ModularOrFun.ControllCode + ".Index",
                    Serial = 1,
                });

                //Design_ModularOrFunDomainDetail_EditSave();
                Design_ModularOrFunDomain_AddSave();

                #endregion
            }

            #region 添加查询
            ////主表
            //Item = new SoftProjectAreaEntity
            //{
            //    ModularOrFunDomainDetailName = string.Format("添加查询--根据{0}初始化", FKFieldss.NameCn),
            //    MethodName = "Add",
            //    Design_ModularOrFunID = Design_ModularOrFunID,
            //    Sort = 31,
            //};
            //resp = Design_ModularOrFunDomain_Add();
            ////明细表
            //Item = resp.Item;
            //Item.Items.Add(new SoftProjectAreaEntity
            //{
            //    ModularOrFunDomainDetailName = string.Format("添加查询--根据{0}初始化", FKFieldss.NameCn),
            //    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
            //    DomainType = 22,
            //    ParamName = FKFieldss.name,
            //    DBOperCode = "",
            //    Serial = 1,
            //});

            //Design_ModularOrFunDomainDetail_EditSave();

            #endregion
            //new TextValue{Text="添加保存--事务",Value="23"},
            //new TextValue{Text="编辑保存--事务",Value="26"},

            else if (Item.DomainTypeTemp == 23)
            {
                #region 添加保存
                sbentity.AppendLine(string.Format("var resp = new MyResponseBase();"));
                sbentity.AppendLine(string.Format("#region (2)添加"));
                sbentity.AppendLine(string.Format("using (var scope = new TransactionScope())"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    try"));
                sbentity.AppendLine(string.Format("    {{"));
                sbentity.AppendLine(string.Format("        {0}_Domain();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("        //string DBFieldVals = \"添加的字段\";"));
                sbentity.AppendLine(string.Format("        //resp = AddSave(DBFieldVals);"));
                sbentity.AppendLine(string.Format("        OperCode = \"{0}.AddSave\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("        resp = Execute();"));
                sbentity.AppendLine(string.Format("        scope.Complete();"));
                sbentity.AppendLine(string.Format("    }}"));
                sbentity.AppendLine(string.Format("    catch (Exception ex)"));
                sbentity.AppendLine(string.Format("    {{"));
                sbentity.AppendLine(string.Format("        throw new Exception(ex.Message);"));
                sbentity.AppendLine(string.Format("    }}"));
                sbentity.AppendLine(string.Format("    finally"));
                sbentity.AppendLine(string.Format("    {{"));
                sbentity.AppendLine(string.Format("        scope.Dispose();"));
                sbentity.AppendLine(string.Format("    }}"));
                sbentity.AppendLine(string.Format("}}"));
                sbentity.AppendLine(string.Format("#endregion"));
                sbentity.AppendLine(string.Format("return resp;"));

                //主表
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = string.Format("添加保存"),
                    MethodName = "AddSave",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 31,
                };
                //resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("添加保存"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 23,
                    ParamName = sbentity.ToString(),
                    DBOperCode = Design_ModularOrFun.ControllCode + ".AddSave",
                    Serial = 1,
                });

                //Design_ModularOrFunDomainDetail_EditSave();
                Design_ModularOrFunDomain_AddSave();

                #endregion
            }
            else if (Item.DomainTypeTemp == 26)
            {
                #region 编辑保存
                sbentity.AppendLine(string.Format("var resp = new MyResponseBase();"));
                sbentity.AppendLine(string.Format("{0}_PKCheck();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format(""));
                sbentity.AppendLine(string.Format("#region (2)编辑"));
                sbentity.AppendLine(string.Format("using (var scope = new TransactionScope())"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    try"));
                sbentity.AppendLine(string.Format("    {{"));
                sbentity.AppendLine(string.Format("        {0}_Domain();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("        //string DBFieldVals = \"编辑的字段\";"));
                sbentity.AppendLine(string.Format("        //resp = EditSave(DBFieldVals);"));
                sbentity.AppendLine(string.Format("        OperCode = \"{0}.EditSave\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("        resp = Execute();"));
                sbentity.AppendLine(string.Format("        scope.Complete();"));
                sbentity.AppendLine(string.Format("    }}"));
                sbentity.AppendLine(string.Format("    catch (Exception ex)"));
                sbentity.AppendLine(string.Format("    {{"));
                sbentity.AppendLine(string.Format("        throw new Exception(ex.Message);"));
                sbentity.AppendLine(string.Format("    }}"));
                sbentity.AppendLine(string.Format("    finally"));
                sbentity.AppendLine(string.Format("    {{"));
                sbentity.AppendLine(string.Format("        scope.Dispose();"));
                sbentity.AppendLine(string.Format("    }}"));
                sbentity.AppendLine(string.Format("}}"));
                sbentity.AppendLine(string.Format("#endregion"));
                sbentity.AppendLine(string.Format("return resp;"));

                //主表
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = string.Format("编辑保存"),
                    MethodName = "EditSave",
                    MethodReturnTypeID = 2,
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 31,
                };
                //resp = Design_ModularOrFunDomain_AddSave();
                //明细表
                //Item = resp.Item;
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("编辑保存"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 26,
                    ParamName = sbentity.ToString(),
                    DBOperCode = Design_ModularOrFun.ControllCode + ".EditSave",
                    Serial = 1,
                });

                //Design_ModularOrFunDomainDetail_EditSave();
                Design_ModularOrFunDomain_AddSave();

                #endregion
            }

            //resp = Design_ModularOrFunDomain_AddSave();
            else if (Item.DomainTypeTemp == 51)
            {
                #region Add、Edit、Delete
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = string.Format("添加、编辑、删除"),
                    MethodName = "AddEditDelete",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 31,
                };
                #region 启动事务
                sbentity = new StringBuilder();
                sbentity.AppendLine("using (var scope = new TransactionScope())");
                sbentity.AppendLine("{");
                sbentity.AppendLine("    try");
                sbentity.AppendLine("    {");
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("启动事务及try"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 71,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion
                #region 查询原始值
                sbentity = new StringBuilder();
                sbentity.AppendLine(string.Format("var OldItems = {0}_GetByXXX();", Design_ModularOrFun.ControllCode));
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("根据{0}ID查询原始值", Design_ModularOrFun.ModularName),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 101,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion
                #region 数据整理
                sbentity = new StringBuilder();
                //sbentity.AppendLine("var OldItems = Pre_UserRole_GetByUserID();");
                sbentity.AppendLine("Item.Items.ForEach(p => p.XXXXID = Item.XXXXID);");
                sbentity.AppendLine(string.Format("var deleteIDsEnum = (from p in OldItems select p.{0}ID).Except(from o in Item.Items select o.{0}ID);", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("var updateItems = Item.Items.Where(p => p.{0}ID != null);", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("var addItems = Item.Items.Where(p => p.{0}ID == null);", Design_ModularOrFun.ControllCode));

                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("数据整理"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 101,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion
                #region 删除
                sbentity = new StringBuilder();
                //sbentity.AppendLine("var OldItems = Pre_UserRole_GetByUserID();");
                sbentity.AppendLine(string.Format("if (deleteIDsEnum.Count() > 0)"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    var deleteIDs = string.Join(\",\", deleteIDsEnum);"));
                sbentity.AppendLine(string.Format("    var sql = string.Format(\"DELETE [dbo].[{0}] WHERE  {0}ID IN({{0}})\", deleteIDs);", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("    var resptemp = Query16(sql, 1);"));
                sbentity.AppendLine(string.Format("}}"));
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("删除"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 101,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 更新
                sbentity = new StringBuilder();
                //sbentity.AppendLine("var OldItems = Pre_UserRole_GetByUserID();");

                sbentity.AppendLine(string.Format("if (updateItems.Count() > 0)"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain {{ Items = updateItems.ToList() }};"));
                sbentity.AppendLine(string.Format("    domain.{0}_Domain();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("    var operCode = \"{0}.EditSave\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("    domain.ExecuteEnums(operCode);"));
                sbentity.AppendLine(string.Format("}}"));

                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("更新"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 101,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 添加
                sbentity = new StringBuilder();
                //sbentity.AppendLine("var OldItems = Pre_UserRole_GetByUserID();");

                sbentity.AppendLine(string.Format("if (addItems.Count() > 0)"));
                sbentity.AppendLine(string.Format("{{"));
                sbentity.AppendLine(string.Format("    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain {{ Items = addItems.ToList() }};"));
                sbentity.AppendLine(string.Format("    domain.{0}_Domain();", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("    var operCode = \"{0}.AddSave\";", Design_ModularOrFun.ControllCode));
                sbentity.AppendLine(string.Format("    domain.ExecuteEnums(operCode);"));
                sbentity.AppendLine(string.Format("}}"));
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("添加"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 101,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 提交事务
                sbentity = new StringBuilder();
                sbentity.AppendLine("scope.Complete();");
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "提交事务",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 73,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 结束事务及try
                sbentity = new StringBuilder();
                sbentity.AppendLine("    }");
                sbentity.AppendLine("    catch (Exception ex)");
                sbentity.AppendLine("    {");
                sbentity.AppendLine("        throw new Exception(ex.Message);");
                sbentity.AppendLine("    }");
                sbentity.AppendLine("    finally");
                sbentity.AppendLine("    {");
                sbentity.AppendLine("        scope.Dispose();");
                sbentity.AppendLine("    }");
                sbentity.AppendLine("}");
                sbentity.AppendLine(string.Format("return resp;"));

                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "结束事务及try",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 72,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion
                Design_ModularOrFunDomain_AddSave();
                #endregion
            }
            else if (Item.DomainTypeTemp == 53)
            {
                #region 结束事务及try模板
                Item = new SoftProjectAreaEntity
                {
                    ModularOrFunDomainName = string.Format("结束事务及try模板"),
                    MethodName = "",
                    MethodReturnTypeID = 2,// "MyResponseBase",
                    Design_ModularOrFunID = Design_ModularOrFunID,
                    Sort = 31,
                };
                #region 启动事务
                sbentity = new StringBuilder();
                sbentity.AppendLine("using (var scope = new TransactionScope())");
                sbentity.AppendLine("{");
                sbentity.AppendLine("    try");
                sbentity.AppendLine("    {");
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = string.Format("启动事务及try"),
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 71,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 执行操作
                sbentity = new StringBuilder();
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "执行操作",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 101,
                    ParamName = "",
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 提交事务
                sbentity = new StringBuilder();
                sbentity.AppendLine("scope.Complete();");
                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "提交事务",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 73,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion

                #region 结束事务及try
                sbentity = new StringBuilder();
                sbentity.AppendLine("    }");
                sbentity.AppendLine("    catch (Exception ex)");
                sbentity.AppendLine("    {");
                sbentity.AppendLine("        throw new Exception(ex.Message);");
                sbentity.AppendLine("    }");
                sbentity.AppendLine("    finally");
                sbentity.AppendLine("    {");
                sbentity.AppendLine("        scope.Dispose();");
                sbentity.AppendLine("    }");
                sbentity.AppendLine("}");
                sbentity.AppendLine(string.Format("return resp;"));

                Item.Items.Add(new SoftProjectAreaEntity
                {
                    ModularOrFunDomainDetailName = "结束事务及try",
                    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                    DomainType = 72,
                    ParamName = sbentity.ToString(),
                    DBOperCode = "",
                    Serial = 1,
                });
                #endregion
                Design_ModularOrFunDomain_AddSave();
                #endregion
            }
            return resp;
        }

        public MyResponseBase Design_ModularOrFunDomain_EditSave()
        {
            Design_ModularOrFunDomain_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    var DBFieldVals = "";

                    #region (1)修改功能模块(无)
                    Design_ModularOrFunDomain_PKCheck();

                    Design_ModularOrFunDomain_Domain();
                    DBFieldVals = "ModularOrFunDomainName,MethodName,MethodReturnTypeID,Sort,Design_ModularOrFunID";
                    resp = EditSave(DBFieldVals);

                    #endregion

                    Design_ModularOrFunDomainDetail_EditSave();

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

        ///// <summary>
        ///// 生成控制器
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFunDomain_BulidDomainFile()
        //{
        //    #region (2)修改功能模块字段
        //    using (var scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //            //(2)创建业务层文件
        //            #region 1)查询模块功能
        //            var resptemp = Design_ModularOrFun_GetByID();
        //            var Design_ModularOrFun = resptemp.Item;
        //            #endregion

        //            #region 3)根据功能模块ID查询所有业务方法
        //            resptemp = Design_ModularOrFunDomain_Index();
        //            var Design_ModularOrFunDomains = resptemp.Items;
        //            var ModularOrFunDomainDetails = Design_ModularOrFunDomainDetail_GetByModularOrFunID();
        //            #endregion
        //            StringBuilder sbentity = new StringBuilder();
        //            #region 名称空间引用

        //            sbentity.AppendLine("");
        //            sbentity.AppendLine("using Framework.Core;");
        //            sbentity.AppendLine("using Framework.Web.Mvc;");
        //            sbentity.AppendLine("using Framework.Web.Mvc.Sys;");
        //            sbentity.AppendLine("using SoftProject.CellModel;");
        //            sbentity.AppendLine("using System;");
        //            sbentity.AppendLine("using System.Collections.Generic;");
        //            sbentity.AppendLine("using System.Linq;");
        //            sbentity.AppendLine("using System.Text;");
        //            sbentity.AppendLine("using System.Threading.Tasks;");
        //            sbentity.AppendLine("using System.Transactions;");

        //            #endregion

        //            sbentity.AppendLine();
        //            sbentity.AppendLine("namespace SoftProject.Domain");
        //            sbentity.AppendLine("{");
        //            sbentity.AppendLine("    /// <summary>");
        //            sbentity.AppendLine(string.Format("    /// 业务层：{0}({1})", Design_ModularOrFun.ControllCode, Design_ModularOrFun.ModularName));
        //            sbentity.AppendLine("    /// </summary>");
        //            sbentity.AppendLine("    public partial class SoftProjectAreaEntityDomain");
        //            sbentity.AppendLine("    {");
        //            foreach (var domain in Design_ModularOrFunDomains)
        //            {
        //                sbentity.AppendLine(string.Format("        /// <summary>"));
        //                sbentity.AppendLine(string.Format("        /// {0}", domain.ModularOrFunDomainName));
        //                sbentity.AppendLine(string.Format("        /// </summary>"));
        //                sbentity.AppendLine(string.Format("        /// <returns></returns>"));

        //                var ModularOrFunDomainDetailsTemps = ModularOrFunDomainDetails.Where(p => p.Design_ModularOrFunDomainID == domain.Design_ModularOrFunDomainID).ToList();
        //                for (int i = 0; i < ModularOrFunDomainDetailsTemps.Count; i++)
        //                {
        //                    var item = ModularOrFunDomainDetailsTemps[i];
        //                    if (item.DomainType == 1)
        //                    {
        //                        #region 构造函数
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "Domain" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public void {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("            PKFields = new List<string> {{ \"{0}ID\" }};", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("            TableName = \"{0}\";", Design_ModularOrFun.ControllCode));
        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 2)
        //                    {
        //                        #region 主键不能为空
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "PKCheck" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public void {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            sbentity.AppendLine(string.Format("       {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("           if (Item.{0}ID == null)", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("           {{"));
        //                        sbentity.AppendLine(string.Format("               throw new Exception(\"{0}主键不能为空！\");", Design_ModularOrFun.ModularName));
        //                        sbentity.AppendLine(string.Format("           }}"));
        //                        //sbentity.AppendLine(string.Format("       }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 11)
        //                    {
        //                        #region 列表查询--初始条件
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "Index" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            //                                    sbentity.AppendLine(string.Format("        public MyResponseBase BC_ExpertGuidance_Index(int SelectType = 6)"));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("            var resp = new MyResponseBase();"));
        //                        //var paramNames = item.ParamName.Split(',');

        //                        //foreach (var paramName in paramNames)
        //                        //{
        //                        //    sbentity.AppendLine(string.Format("            if (!Querys.QueryDicts.ContainsKey(\"{0}___equal\"))", paramName));
        //                        //    sbentity.AppendLine(string.Format("            {{"));
        //                        //    sbentity.AppendLine(string.Format("                if (Item.{0} == null)", paramName));
        //                        //    sbentity.AppendLine(string.Format("                    throw new Exception(\"不能为空\");", ""));
        //                        //    sbentity.AppendLine(string.Format("                else"));
        //                        //    sbentity.AppendLine(string.Format("                    Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = Item.{0}.ToString() }});", paramName));
        //                        //    sbentity.AppendLine(string.Format("            }}"));
        //                        //}

        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                PageQueryBase.RankInfo = \"UpdateDate|0\";"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            //SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity"));
        //                        sbentity.AppendLine(string.Format("            //{{"));
        //                        sbentity.AppendLine(string.Format("            //    DBTSql =string.Format(\";WITH T1000 AS (SELECT * FROM [dbo].[{0}] A  WHERE 1=1 sqlplaceholder )\"),", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("            //    DBOperType = 8,//排序分页"));
        //                        sbentity.AppendLine(string.Format("            //    SelectSubType = SelectType,"));
        //                        sbentity.AppendLine(string.Format("            //    DBSelectResultType = 2,"));
        //                        sbentity.AppendLine(string.Format("            //    EqualQueryParam = \"\""));
        //                        sbentity.AppendLine(string.Format("            //}};"));
        //                        sbentity.AppendLine(string.Format("            //Sys_HOperControl = hOperControl;"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            OperCode=\"{0}\";", DBOperCode));
        //                        sbentity.AppendLine(string.Format("            resp = Execute();"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            resp.Querys = Querys;"));
        //                        sbentity.AppendLine(string.Format("            resp.Item = Item;"));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 12)
        //                    {
        //                        #region 列表查询--初始条件
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "Index" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            //                                    sbentity.AppendLine(string.Format("        public MyResponseBase BC_ExpertGuidance_Index(int SelectType = 6)"));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("            var resp = new MyResponseBase();"));
        //                        var paramNames = item.ParamName.Split(',');

        //                        foreach (var paramName in paramNames)
        //                        {
        //                            sbentity.AppendLine(string.Format("            if (!Querys.QueryDicts.ContainsKey(\"{0}___equal\"))", paramName));
        //                            sbentity.AppendLine(string.Format("            {{"));
        //                            sbentity.AppendLine(string.Format("                if (Item.{0} == null)", paramName));
        //                            sbentity.AppendLine(string.Format("                    throw new Exception(\"不能为空\");", ""));
        //                            sbentity.AppendLine(string.Format("                else"));
        //                            sbentity.AppendLine(string.Format("                    Querys.Add(new Query {{ QuryType = 0, FieldName = \"{0}___equal\", Value = Item.{0}.ToString() }});", paramName));
        //                            sbentity.AppendLine(string.Format("            }}"));
        //                        }

        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                PageQueryBase.RankInfo = \"UpdateDate|0\";"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            //SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity"));
        //                        sbentity.AppendLine(string.Format("            //{{"));
        //                        sbentity.AppendLine(string.Format("            //    DBTSql =string.Format(\";WITH T1000 AS (SELECT * FROM [dbo].[{0}] A  WHERE 1=1 sqlplaceholder )\"),", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("            //    DBOperType = 8,//排序分页"));
        //                        sbentity.AppendLine(string.Format("            //    SelectSubType = SelectType,"));
        //                        sbentity.AppendLine(string.Format("            //    DBSelectResultType = 2,"));
        //                        sbentity.AppendLine(string.Format("            //    EqualQueryParam = \"\""));
        //                        sbentity.AppendLine(string.Format("            //}};"));
        //                        sbentity.AppendLine(string.Format("            //Sys_HOperControl = hOperControl;"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            OperCode=\"{0}\";", DBOperCode));
        //                        sbentity.AppendLine(string.Format("            resp = Execute();"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            resp.Querys = Querys;"));
        //                        sbentity.AppendLine(string.Format("            resp.Item = Item;"));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 13)
        //                    {
        //                        #region 根据主键查询--显示
        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;

        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "ByID" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("            {0}_PKCheck();", Design_ModularOrFun.ControllCode));
        //                        //sbentity.AppendLine(string.Format("            var sql = string.Format(\";SELECT * FROM [dbo].[{0}] A WHERE {0}ID={{0}} \", Item.{0}ID);", Design_ModularOrFun.ControllCode));
        //                        //sbentity.AppendLine(string.Format("            var resp = Query16(sql, 4);"));
        //                        sbentity.AppendLine(string.Format("             OperCode=\"{0}\";", DBOperCode));
        //                        sbentity.AppendLine(string.Format("             resp=Execute();"));

        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 14)
        //                    {
        //                        #region 根据主键查询--编辑
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "ByIDEdit" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("            {0}_PKCheck();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("            var sql = string.Format(\";SELECT * FROM [dbo].[{0}] A WHERE {0}ID={{0}} \", Item.{0}ID);", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("            var resp = Query16(sql, 4);"));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));
        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 22)
        //                    {
        //                        #region 添加查询-带初始值
        //                        //var opercode = "";
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "Add" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            //                                    sbentity.AppendLine(string.Format("        public MyResponseBase {0}_Add()", Design_ModularOrFun.ControllCode));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        sbentity.AppendLine(string.Format("            var resp = Default();"));
        //                        var paramNames = item.ParamName.Split(',');

        //                        foreach (var paramName in paramNames)
        //                        {
        //                            sbentity.AppendLine(string.Format("            resp.Item.{0} = Item.{0};", paramName));
        //                        }
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));
        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 23)
        //                    {
        //                        #region 添加保存

        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "AddSave" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));
        //                            //sbentity.AppendLine(string.Format("        public MyResponseBase {0}_AddSave()", Design_ModularOrFun.ControllCode));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;
        //                        sbentity.AppendLine(string.Format("            var resp = new MyResponseBase();"));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            #region (2)添加"));
        //                        sbentity.AppendLine(string.Format("            using (var scope = new TransactionScope())"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                try"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    {0}_Domain();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    //string DBFieldVals = \"添加的字段\";"));
        //                        sbentity.AppendLine(string.Format("                    //resp = AddSave(DBFieldVals);"));
        //                        sbentity.AppendLine(string.Format("                    OperCode=\"{0}\";", DBOperCode));
        //                        sbentity.AppendLine(string.Format("                    resp=Execute();"));
        //                        sbentity.AppendLine(string.Format("                    scope.Complete();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                catch (Exception ex)"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    throw new Exception(ex.Message);"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                finally"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    scope.Dispose();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format("            #endregion"));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 26)
        //                    {
        //                        #region 编辑保存
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "EditSave" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));

        //                            //sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode,item.MethodName));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;
        //                        sbentity.AppendLine(string.Format("            var resp = new MyResponseBase();"));
        //                        sbentity.AppendLine(string.Format("            {0}_PKCheck();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            #region (2)编辑"));
        //                        sbentity.AppendLine(string.Format("            using (var scope = new TransactionScope())"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                try"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    {0}_Domain();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    //string DBFieldVals = \"编辑的字段\";"));
        //                        sbentity.AppendLine(string.Format("                    //resp = EditSave(DBFieldVals);"));
        //                        sbentity.AppendLine(string.Format("                    OperCode=\"{0}\";", DBOperCode));
        //                        sbentity.AppendLine(string.Format("                    resp=Execute();"));
        //                        sbentity.AppendLine(string.Format("                    scope.Complete();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                catch (Exception ex)"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    throw new Exception(ex.Message);"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                finally"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    scope.Dispose();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format("            #endregion"));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 41)
        //                    {
        //                        #region 添加保存+子表
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "AddSave" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));

        //                            //sbentity.AppendLine(string.Format("        public MyResponseBase {0}_AddSave()", Design_ModularOrFun.ControllCode));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        //第1个为OperCode，第2个为子表方法。
        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;
        //                        sbentity.AppendLine(string.Format("            var resp = new MyResponseBase();"));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            #region (2)添加"));
        //                        sbentity.AppendLine(string.Format("            using (var scope = new TransactionScope())"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                try"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    {0}_Domain();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    //string DBFieldVals = \"添加的字段\";"));
        //                        sbentity.AppendLine(string.Format("                    //resp = AddSave(DBFieldVals);"));
        //                        sbentity.AppendLine(string.Format("                    OperCode=\"{0}\";", DBOperCode));
        //                        sbentity.AppendLine(string.Format("                    resp=Execute();"));
        //                        sbentity.AppendLine(string.Format("                    //调用子表方法"));
        //                        sbentity.AppendLine(string.Format("                    " + item.ParamName));
        //                        sbentity.AppendLine(string.Format("                    scope.Complete();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                catch (Exception ex)"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    throw new Exception(ex.Message);"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                finally"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    scope.Dispose();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format("            #endregion"));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 42)
        //                    {
        //                        #region 编辑保存+子表
        //                        if (i == 0)
        //                        {
        //                            var MethodName = domain.MethodName == null ? "EditSave" : domain.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));

        //                            //sbentity.AppendLine(string.Format("        public MyResponseBase {0}_EditSave()", Design_ModularOrFun.ControllCode));
        //                            sbentity.AppendLine(string.Format("        {{"));
        //                        }
        //                        var DBOperCode = item.DBOperCode;
        //                        if (string.IsNullOrEmpty(item.DBOperCode))
        //                            DBOperCode = Design_ModularOrFun.ControllCode + "." + domain.MethodName;
        //                        sbentity.AppendLine(string.Format("            var resp = new MyResponseBase();"));
        //                        sbentity.AppendLine(string.Format("            {0}_PKCheck();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            #region (2)编辑"));
        //                        sbentity.AppendLine(string.Format("            using (var scope = new TransactionScope())"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                try"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    {0}_Domain();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    //string DBFieldVals = \"编辑的字段\";"));
        //                        sbentity.AppendLine(string.Format("                    //resp = EditSave(DBFieldVals);"));
        //                        sbentity.AppendLine(string.Format("                    OperCode=\"{0}\";", Design_ModularOrFun.ControllCode, DBOperCode));
        //                        sbentity.AppendLine(string.Format("                    resp=Execute();"));
        //                        sbentity.AppendLine(string.Format("                    //调用子表方法"));
        //                        sbentity.AppendLine(string.Format("                    " + item.ParamName));

        //                        sbentity.AppendLine(string.Format("                    scope.Complete();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                catch (Exception ex)"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    throw new Exception(ex.Message);"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                finally"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    scope.Dispose();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format("            #endregion"));
        //                        sbentity.AppendLine(string.Format(" "));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));

        //                        #endregion
        //                    }
        //                    else if (item.DomainType == 52)
        //                    {
        //                        #region 子表Merger
        //                        if (i == 0)
        //                        {
        //                            var MethodName = item.MethodName == null ? "EditSave" : item.MethodName;
        //                            sbentity.AppendLine(string.Format("        public MyResponseBase {0}_{1}()", Design_ModularOrFun.ControllCode, MethodName));

        //                            //sbentity.AppendLine(string.Format("        public MyResponseBase BC_PartnerOrderDetail_EditSave()"));
        //                            sbentity.AppendLine(string.Format("        {"));
        //                        }
        //                        var opercode = item.ParamName;
        //                        if (item.ParamName == null)
        //                            opercode = item.MethodName;
        //                        sbentity.AppendLine(string.Format("            MyResponseBase resp = new MyResponseBase();"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            BC_PartnerOrderDetail_Domain();"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("            #region 处理"));
        //                        sbentity.AppendLine(string.Format("            using (var scope = new TransactionScope())"));
        //                        sbentity.AppendLine(string.Format("            {{"));
        //                        sbentity.AppendLine(string.Format("                try"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    #region (3)根据主表ID查询子表所有数据"));
        //                        sbentity.AppendLine(string.Format("                    var {0}s = {0}_GetByMastTableID();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    #endregion"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    #region (2)数据整理"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    Item.Items.ForEach(p => p.BCPartnerOrderDetailPriceTotal = p.BCPartnerOrderDetailPrice * p.BCPartnerOrderDetailNumer);"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    var deleteIDsEnum = (from p in {0}s select p.{0}ID).Except(from o in Item.Items select o.{0}ID);", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    var updateItems = Item.Items.Where(p => p.{0}ID != null);", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    var addItems = Item.Items.Where(p => p.{0}ID == null);", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                    #endregion"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    MyResponseBase resptemp = new MyResponseBase();"));
        //                        sbentity.AppendLine(string.Format("                    #region (4)删除元素:执行删除，通过In进行删除"));
        //                        sbentity.AppendLine(string.Format("                    if (deleteIDsEnum.Count() > 0)"));
        //                        sbentity.AppendLine(string.Format("                    {{"));
        //                        sbentity.AppendLine(string.Format("                        var deleteIDs = string.Join(\",\", deleteIDsEnum);"));
        //                        sbentity.AppendLine(string.Format("                        var sql = string.Format(\"DELETE [dbo].{0} WHERE  {0}ID IN(\\{0}\\})\", deleteIDs);", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                        resptemp = Query16(sql, 1);"));
        //                        sbentity.AppendLine(string.Format("                    }}"));
        //                        sbentity.AppendLine(string.Format("                    #endregion"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    var DBFieldVals = \"\";"));
        //                        sbentity.AppendLine(string.Format("                    #region (5)更新"));
        //                        sbentity.AppendLine(string.Format("                    if (updateItems.Count() > 0)"));
        //                        sbentity.AppendLine(string.Format("                    {{"));
        //                        sbentity.AppendLine(string.Format("                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };"));
        //                        sbentity.AppendLine(string.Format("                        domain.{0}_Domain();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                        //DBFieldVals = \"更新的字段\";"));
        //                        sbentity.AppendLine(string.Format("                        //domain.EditSaves(DBFieldVals);"));
        //                        sbentity.AppendLine(string.Format("                         OperCode=\"{0}.{1}\";", Design_ModularOrFun.ControllCode, item.MethodName));
        //                        sbentity.AppendLine(string.Format("                        domain.EditSaves();"));
        //                        sbentity.AppendLine(string.Format("                    }}"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    #endregion"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    #region (6)添加"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    if (addItems.Count() > 0)"));
        //                        sbentity.AppendLine(string.Format("                    {{"));
        //                        sbentity.AppendLine(string.Format("                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };"));
        //                        sbentity.AppendLine(string.Format("                        domain.{0}_Domain();", Design_ModularOrFun.ControllCode));
        //                        sbentity.AppendLine(string.Format("                        //DBFieldVals = \"\";"));
        //                        sbentity.AppendLine(string.Format("                        //domain.AddSaves(DBFieldVals);"));
        //                        sbentity.AppendLine(string.Format("                        OperCode=\"{0}.{1}\";", Design_ModularOrFun.ControllCode, item.ParamName));
        //                        sbentity.AppendLine(string.Format("                        domain.AddSaves();"));
        //                        sbentity.AppendLine(string.Format("                    }}"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    #endregion"));
        //                        sbentity.AppendLine(string.Format(""));
        //                        sbentity.AppendLine(string.Format("                    scope.Complete();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                catch (Exception ex)"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    throw new Exception(ex.Message);"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("                finally"));
        //                        sbentity.AppendLine(string.Format("                {{"));
        //                        sbentity.AppendLine(string.Format("                    scope.Dispose();"));
        //                        sbentity.AppendLine(string.Format("                }}"));
        //                        sbentity.AppendLine(string.Format("            }}"));
        //                        sbentity.AppendLine(string.Format("            #endregion"));
        //                        sbentity.AppendLine(string.Format("            return resp;"));
        //                        //sbentity.AppendLine(string.Format("        }"));
        //                        #endregion
        //                    }
        //                    if (ModularOrFunDomainDetailsTemps.Count - 1 == i)
        //                    {
        //                        sbentity.AppendLine("        }");
        //                        break;
        //                    }
        //                }
        //                sbentity.AppendLine();
        //            }

        //            sbentity.AppendLine("   }");
        //            sbentity.AppendLine("}");

        //            //F:\软件项目\SoftPlatformProject\SoftPlatform\SoftPlatform\Areas\C_CustomerAreas\CellModel
        //            //string filepath1=System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        //            //string filepath2=System.Environment.CurrentDirectory ;
        //            //string filepath3=System.IO.Directory.GetCurrentDirectory();  
        //            //string filepath4=System.AppDomain.CurrentDomain.BaseDirectory;
        //            var path = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\Domain\\", Design_ModularOrFun.AreasCode);
        //            if (!Directory.Exists(path))
        //                Directory.CreateDirectory(path);
        //            //string filepath5 = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\CellModel\\{1}.cs", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
        //            string filepath5 = path + Design_ModularOrFun.ControllCode + "Domain.cs";

        //            FileStream fs = new FileStream(filepath5, FileMode.Create);
        //            StreamWriter sw = new StreamWriter(fs);
        //            sw.Write(sbentity.ToString());
        //            sw.Flush();
        //            sw.Close();
        //            fs.Close();
        //            scope.Complete();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //        finally
        //        {
        //            scope.Dispose();
        //        }
        //    }
        //    #endregion

        //    return resp;
        //}

        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunDomain_BulidDomainFile()
        {
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    //(2)创建业务层文件
                    #region 1)查询模块功能
                    var resptemp = Design_ModularOrFun_GetByID();
                    var Design_ModularOrFun = resptemp.Item;
                    #endregion

                    #region 3)根据功能模块ID查询所有业务方法
                    resptemp = Design_ModularOrFunDomain_Index();
                    var Design_ModularOrFunDomains = resptemp.Items;
                    var ModularOrFunDomainDetails = Design_ModularOrFunDomainDetail_GetByModularOrFunID();
                    #endregion
                    StringBuilder sbentity = new StringBuilder();
                    #region 名称空间引用

                    sbentity.AppendLine("");
                    sbentity.AppendLine("using Framework.Core;");
                    sbentity.AppendLine("using Framework.Web.Mvc;");
                    sbentity.AppendLine("using Framework.Web.Mvc.Sys;");
                    sbentity.AppendLine("using SoftProject.CellModel;");
                    sbentity.AppendLine("using System;");
                    sbentity.AppendLine("using System.Collections.Generic;");
                    sbentity.AppendLine("using System.Linq;");
                    sbentity.AppendLine("using System.Text;");
                    sbentity.AppendLine("using System.Threading.Tasks;");
                    sbentity.AppendLine("using System.Transactions;");

                    #endregion

                    sbentity.AppendLine();
                    sbentity.AppendLine("namespace SoftProject.Domain");
                    sbentity.AppendLine("{");
                    sbentity.AppendLine("    /// <summary>");
                    sbentity.AppendLine(string.Format("    /// 业务层：{0}({1})", Design_ModularOrFun.ControllCode, Design_ModularOrFun.ModularName));
                    sbentity.AppendLine("    /// </summary>");
                    sbentity.AppendLine("    public partial class SoftProjectAreaEntityDomain");
                    sbentity.AppendLine("    {");
                    foreach (var domain in Design_ModularOrFunDomains)
                    {
                        var strMethodReturnTypeID = "";
                        if (domain.MethodReturnTypeID == null || domain.MethodReturnTypeID == 1)
                            strMethodReturnTypeID = "void";
                        else
                            strMethodReturnTypeID = " MyResponseBase ";
                        sbentity.AppendLine(string.Format("        /// <summary>"));
                        sbentity.AppendLine(string.Format("        /// {0}", domain.ModularOrFunDomainName));
                        sbentity.AppendLine(string.Format("        /// </summary>"));
                        sbentity.AppendLine(string.Format("        /// <returns></returns>"));
                        sbentity.AppendLine(string.Format("        public {0} {1}_{2}()", strMethodReturnTypeID, Design_ModularOrFun.ControllCode, domain.MethodName));
                        sbentity.AppendLine(string.Format("        {{"));

                        var ModularOrFunDomainDetailsTemps = ModularOrFunDomainDetails.Where(p => p.Design_ModularOrFunDomainID == domain.Design_ModularOrFunDomainID).ToList();
                        #region 具体步骤
                        for (int i = 0; i < ModularOrFunDomainDetailsTemps.Count; i++)
                        {
                            var item = ModularOrFunDomainDetailsTemps[i];
                            sbentity.AppendLine(string.Format("        //{0}", item.ModularOrFunDomainDetailName));
                            sbentity.AppendLine(string.Format("        {0}", HttpUtility.UrlDecode(item.ParamName)));
                        }
                        #endregion
                        sbentity.AppendLine("        }");

                        sbentity.AppendLine();
                    }

                    sbentity.AppendLine("   }");
                    sbentity.AppendLine("}");

                    //F:\软件项目\SoftPlatformProject\SoftPlatform\SoftPlatform\Areas\C_CustomerAreas\CellModel
                    //string filepath1=System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    //string filepath2=System.Environment.CurrentDirectory ;
                    //string filepath3=System.IO.Directory.GetCurrentDirectory();  
                    //string filepath4=System.AppDomain.CurrentDomain.BaseDirectory;
                    var path = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\Domain\\", Design_ModularOrFun.AreasCode);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //string filepath5 = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\CellModel\\{1}.cs", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
                    string filepath5 = path + Design_ModularOrFun.ControllCode + "Domain.cs";

                    FileStream fs = new FileStream(filepath5, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
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


        ///// <summary>
        ///// 生成业务层记录
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFunDomain_BulidRecord()
        //{
        //    var Design_ModularOrFunID = Item.Design_ModularOrFunID;
        //    Design_ModularOrFunDomain_Domain();
        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //    #region 功能模块对象
        //    var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
        //    #endregion

        //    #region 获取实体字段
        //    var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
        //    //var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).First();
        //    //var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

        //    var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
        //    var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

        //    #endregion

        //    #region 构造函数
        //    //主表
        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = "构造函数",
        //        MethodName = "Domain",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 1,
        //    };
        //    //var resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = "构造函数",
        //        //Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 1,
        //        ParamName = "",
        //        DBOperCode = "",
        //        Serial = 1,
        //    });
        //    Design_ModularOrFunDomain_AddSave();
        //    //Design_ModularOrFunDomainDetail_EditSave();

        //    #endregion

        //    #region 主键不能为空
        //    //主表
        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = "主键不为空检查",
        //        MethodName = "PKCheck",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 6,
        //    };
        //    //            resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = "主键不为空检查",
        //        Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 2,
        //        ParamName = "",
        //        DBOperCode = "",
        //        Serial = 1,
        //    });

        //    //Design_ModularOrFunDomainDetail_EditSave();
        //    Design_ModularOrFunDomain_AddSave();

        //    #endregion

        //    #region 根据主键查询--显示
        //    //主表
        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = "根据主键查询--显示",
        //        MethodName = "ByID",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 11,
        //    };
        //    //resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = "根据主键查询--显示",
        //        Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 13,
        //        ParamName = "",
        //        DBOperCode = Design_ModularOrFun.ControllCode + ".ByID",
        //        Serial = 1,
        //    });

        //    //Design_ModularOrFunDomainDetail_EditSave();
        //    Design_ModularOrFunDomain_AddSave();

        //    #endregion

        //    #region 根据主键查询--编辑
        //    //主表
        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = "根据主键查询--编辑",
        //        MethodName = "ByIDEdit",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 11,
        //    };
        //    //resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = "根据主键查询--编辑",
        //        Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 14,
        //        ParamName = "",
        //        DBOperCode = "",
        //        Serial = 1,
        //    });

        //    //Design_ModularOrFunDomainDetail_EditSave();
        //    Design_ModularOrFunDomain_AddSave();

        //    #endregion

        //    #region 列表查询
        //    //主表

        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = string.Format("列表查询"),
        //        MethodName = "Index",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 11,
        //    };
        //    //resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = string.Format("列表查询"),
        //        Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 11,
        //        ParamName = "",
        //        DBOperCode = Design_ModularOrFun.ControllCode + ".Index",
        //        Serial = 1,
        //    });

        //    //Design_ModularOrFunDomainDetail_EditSave();
        //    Design_ModularOrFunDomain_AddSave();

        //    #endregion

        //    //#region 添加查询
        //    ////主表
        //    //Item = new SoftProjectAreaEntity
        //    //{
        //    //    ModularOrFunDomainDetailName = string.Format("添加查询--根据{0}初始化", FKFieldss.NameCn),
        //    //    MethodName = "Add",
        //    //    Design_ModularOrFunID = Design_ModularOrFunID,
        //    //    Sort = 31,
        //    //};
        //    //resp = Design_ModularOrFunDomain_Add();
        //    ////明细表
        //    //Item = resp.Item;
        //    //Item.Items.Add(new SoftProjectAreaEntity
        //    //{
        //    //    ModularOrFunDomainDetailName = string.Format("添加查询--根据{0}初始化", FKFieldss.NameCn),
        //    //    Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //    //    DomainType = 22,
        //    //    ParamName = FKFieldss.name,
        //    //    DBOperCode = "",
        //    //    Serial = 1,
        //    //});

        //    //Design_ModularOrFunDomainDetail_EditSave();

        //    //#endregion

        //    #region 添加保存
        //    //主表
        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = string.Format("添加保存"),
        //        MethodName = "AddSave",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 31,
        //    };
        //    //resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = string.Format("添加保存"),
        //        Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 23,
        //        ParamName = "",
        //        DBOperCode = Design_ModularOrFun.ControllCode + ".AddSave",
        //        Serial = 1,
        //    });

        //    //Design_ModularOrFunDomainDetail_EditSave();
        //    Design_ModularOrFunDomain_AddSave();

        //    #endregion

        //    #region 编辑保存
        //    //主表
        //    Item = new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainName = string.Format("编辑保存"),
        //        MethodName = "EditSave",
        //        Design_ModularOrFunID = Design_ModularOrFunID,
        //        Sort = 31,
        //    };
        //    //resp = Design_ModularOrFunDomain_AddSave();
        //    //明细表
        //    //Item = resp.Item;
        //    Item.Items.Add(new SoftProjectAreaEntity
        //    {
        //        ModularOrFunDomainDetailName = string.Format("编辑保存"),
        //        Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
        //        DomainType = 26,
        //        ParamName = "",
        //        DBOperCode = Design_ModularOrFun.ControllCode + ".EditSave",
        //        Serial = 1,
        //    });

        //    //Design_ModularOrFunDomainDetail_EditSave();
        //    Design_ModularOrFunDomain_AddSave();

        //    #endregion
        //    return resp;
        //}

        /// <summary>
        /// 生成业务层记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunDomain_BulidRecord()
        {
            var Design_ModularOrFunID = Item.Design_ModularOrFunID;

            #region
            //构造函数
            SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 1, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            //主键不为空
            domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 2, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            //根据主键查询--显示
            domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 11, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            //根据主键查询--编辑
            domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 13, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            //列表查询
            domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 14, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            //添加保存
            domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 23, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            //编辑保存
            domain = new SoftProjectAreaEntityDomain();
            domain.Item = new SoftProjectAreaEntity { DomainTypeTemp = 26, Design_ModularOrFunID = Design_ModularOrFunID };
            domain.Design_ModularOrFunDomain_AddSaveMethod();

            #endregion

            return resp;
        }

        /// <summary>
        /// 生成业务层记录：领域模型的相关表
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunDomain_BulidRecordDomainRef()
        {
            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFunDomain_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var FKFieldss = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 2) == 2).First();
            //var FKFields = string.Join(",", FKFieldss);//deleteForecastIDsEnum.ToArray()

            var PKFields = Design_ModularFields.Where(p => p.bPrimaryKeyOrFK != null && ((int)p.bPrimaryKeyOrFK & 1) == 1).Select(p => p.name).ToList();
            var PKField = string.Join(",", PKFields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 构造函数
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainName = "构造函数",
                MethodName = "Domain",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 1,
            };
            var resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = "构造函数",
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 1,
                ParamName = "",
                DBOperCode = "",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 主键不能为空
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainName = "主键不为空检查",
                MethodName = "PKCheck",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 6,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = "主键不为空检查",
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 2,
                ParamName = "",
                DBOperCode = "",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 根据主键查询--显示
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainName = "根据主键查询--显示",
                MethodName = "ByID",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 11,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = "根据主键查询--显示",
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 13,
                ParamName = "",
                DBOperCode = "",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 根据主键查询--显示
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainName = "根据主键查询--编辑",
                MethodName = "ByIDEdit",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 11,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = "根据主键查询--编辑",
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 14,
                ParamName = "",
                DBOperCode = "",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 列表查询
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("列表查询--根据{0}初始化", FKFieldss.NameCn),
                MethodName = "Index",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 11,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("列表查询--根据{0}初始化", FKFieldss.NameCn),
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 12,
                ParamName = FKFieldss.name,
                DBOperCode = Design_ModularOrFun.ControllCode + ".Index",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 添加查询
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("添加查询--根据{0}初始化", FKFieldss.NameCn),
                MethodName = "Add",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 31,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("添加查询--根据{0}初始化", FKFieldss.NameCn),
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 22,
                ParamName = FKFieldss.name,
                DBOperCode = "",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 添加保存
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("添加保存", FKFieldss.NameCn),
                MethodName = "AddSave",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 31,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("添加保存"),
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 23,
                ParamName = "",
                DBOperCode = Design_ModularOrFun.ControllCode + ".AddSave",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            #region 编辑保存
            //主表
            Item = new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("编辑保存"),
                MethodName = "EditSave",
                Design_ModularOrFunID = Design_ModularOrFunID,
                Sort = 31,
            };
            resp = Design_ModularOrFunDomain_AddSave();
            //明细表
            Item = resp.Item;
            Item.Items.Add(new SoftProjectAreaEntity
            {
                ModularOrFunDomainDetailName = string.Format("编辑保存"),
                Design_ModularOrFunDomainID = resp.Item.Design_ModularOrFunDomainID,
                DomainType = 26,
                ParamName = "",
                DBOperCode = Design_ModularOrFun.ControllCode + ".EditSave",
                Serial = 1,
            });

            Design_ModularOrFunDomainDetail_EditSave();

            #endregion

            //var resp = Design_ModularOrFunDomain_EditListSave();
            return resp;
        }



        public MyResponseBase Design_ModularOrFunDomain_Delete()
        {
            Design_ModularOrFunDomain_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    var DBFieldVals = "";

                    #region (1)修改功能模块(无)
                    Design_ModularOrFunDomain_PKCheck();

                    var str = "DELETE Design_ModularOrFunDomain WHERE  Design_ModularOrFunDomainID=" + Item.Design_ModularOrFunDomainID;
                    resp = Query16(str, 1);

                    #endregion

                    //Design_ModularOrFunDomainDetail_EditSave();

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

    }
}
