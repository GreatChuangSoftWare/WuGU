
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using System.IO;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Design_ModularFieldDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularField_Domain()
        {
            PKField = "Design_ModularFieldID";
            //PKFields = new List<string> { "Design_ModularFieldID" };
            TableName = "Design_ModularField";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularField_PKCheck()
        {
            if (Item.Design_ModularFieldID == null)
            {
                throw new Exception("功能模块字段主键不能为空！");
            }
        }

        public MyResponseBase Design_ModularField_GetByID()
        {
            Design_ModularField_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularField] A WHERE Design_ModularFieldID={0} ", Item.Design_ModularFieldID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 权限管理系统--缓存：获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularField_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            string sql = "SELECT * FROM V_Design_ModularField Order By Design_ModularFieldParentID";
            var resp = Query16(sql, 2);
            return resp.Items;
        }

        /// <summary>
        /// 根据功能模块ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularField_GetByModularOrFunID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularField] A WHERE Design_ModularOrFunID={0} ORDER BY Sort", Item.Design_ModularOrFunID);
            var resp = Query16(sql);
            return resp;
        }

        /// <summary>
        /// 根据功能模块ID查询：生成实体使用
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularField_GetByModularOrFunIDByEntity()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(";WITH T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT A.name");
            sb.AppendLine("	FROM [dbo].[Design_ModularField] A ");
            sb.AppendLine("	LEFT JOIN [dbo].[Design_ModularOrFun] B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID");
            sb.AppendLine("	WHERE A.Design_ModularOrFunID!={0} AND bValidModularOrFun=1");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("	(SELECT a.*");
            sb.AppendLine("	FROM Design_ModularField a");
            sb.AppendLine("	WHERE Design_ModularOrFunID={0} AND name NOT IN(	SELECT name FROM T1 )");
            sb.AppendLine(")");
            sb.AppendLine("SELECT A.*,CASE WHEN T2.Design_ModularFieldID IS NULL THEN 0 ELSE 1 END BEffective");
            sb.AppendLine("FROM Design_ModularField a");
            sb.AppendLine("LEFT JOIN T2 ON A.Design_ModularFieldID=T2.Design_ModularFieldID");
            sb.AppendLine("WHERE A.Design_ModularOrFunID={0}");
            sb.AppendLine("ORDER BY A.Sort");

            var sql = sb.ToString();
            sql = string.Format(sql, Item.Design_ModularOrFunID);
            //var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularField] A WHERE Design_ModularOrFunID={0} ORDER BY Sort", Item.Design_ModularOrFunID);
            var resp = Query16(sql);
            return resp;
        }

        public MyResponseBase Design_ModularField_EditListSave()
        {
            Design_ModularField_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularField_GetByModularOrFunID();
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Design_ModularFields.ForEach(p =>
                    { p.Design_ModularOrFunID = Item.Design_ModularOrFunID; });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularFieldID).Except(from o in Item.Design_ModularFields select o.Design_ModularFieldID);
                    var updateItems = Item.Design_ModularFields.Where(p => p.Design_ModularFieldID != null);
                    var addItems = Item.Design_ModularFields.Where(p => p.Design_ModularFieldID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularField] WHERE  Design_ModularFieldID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularField_Domain();
                        var DBFieldVals = "Design_ModularOrFunID,FieldTypeID,name,NameCn,xtype,length,QueryEn,QueryCn,Dicts,Calformula,Sort,Width,Align,DisFormat,NameCn2,Required,DefaultValue,bPrimaryKeyOrFK,bOperLog,bRepeat,ModularFieldRemark";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularField_Domain();
                        var DBFieldVals = "Design_ModularOrFunID,FieldTypeID,name,NameCn,xtype,length,QueryEn,QueryCn,Dicts,Calformula,Sort,Width,Align,DisFormat,NameCn2,Required,DefaultValue,bPrimaryKeyOrFK,bOperLog,bRepeat,ModularFieldRemark";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

                    scope.Complete();
                    ProjectCache.Design_ModularPageFields_Clear();
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

        public MyResponseBase Design_ModularField_CreateTable()
        {
            Design_ModularField_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    //return null;
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    //(1)保存或更新数据库数据
                    Design_ModularField_EditListSave();

                    //(2)创建表
                    #region 1)查询实体
                    var resptemp = Design_ModularOrFun_GetByID();
                    var ControllCode = resptemp.Item.ControllCode;
                    #endregion

                    #region 2)根据功能模块名(表名)

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("SELECT name,cast(xtype as int) xtype,cast(length as int) length,cast(sc.colid as int) colid,SM.TEXT AS 'DefaultValue'");
                    sb.AppendLine("FROM [syscolumns] SC ");
                    sb.AppendLine("LEFT JOIN dbo.syscomments SM ON SC.cdefault = SM.id");
                    sb.AppendLine("WHERE sc.[id] = object_id('{0}') ORDER BY colid");

                    //name,xtype,length,colid
                    //var sql = string.Format("SELECT name,cast(xtype as int) xtype,cast(length as int) length,cast(colid as int) colid  FROM [syscolumns] WHERE [id] = object_id('{0}') ORDER BY colid ", ControllCode);
                    var sql = sb.ToString();
                    sql = string.Format(sql, ControllCode);

                    //hOperControl = new SoftProjectAreaEntity
                    //{
                    //    DBTSql = sql,
                    //    DBOperType = 16,
                    //    DBSelectResultType = 8,
                    //};
                    //Sys_HOperControl = hOperControl;

                    //var respxxx = Execute();

                    resptemp = Query16(sql);
                    var tableColumns = resptemp.Items;
                    #endregion

                    #region 3)根据功能模块ID查询所有字段
                    resptemp = Design_ModularField_GetByModularOrFunID();
                    var Design_ModularFields = resptemp.Items.Where(p => p.FieldTypeID != 2).ToList();
                    //Design_ModularFields.ForEach(p =>
                    //{
                    //    if (!string.IsNullOrEmpty(p.QueryEn))
                    //    {
                    //        p.name = p.QueryEn;
                    //    }
                    //});
                    #endregion

                    #region 4)创建表

                    if (tableColumns.Count == 0)
                    {
                        #region 创建表
                        sql = string.Format("Create Table {0}(", ControllCode);

                        for (int i = 0; i < Design_ModularFields.Count; i++)
                        {
                            //数据类型：56：整数   106：小数    167：字符串   61：日期
                            #region 列
                            var item = Design_ModularFields[i];

                            if (!string.IsNullOrEmpty(item.QueryEn))//是否有查询字段
                            {
                                sql += string.Format(" {0} int,", item.QueryEn);
                            }
                            else
                            {
                                #region 字段定义
                                switch (item.xtype)
                                {
                                    case 56:
                                        if (item.bPrimaryKeyOrFK == 1)
                                            sql += string.Format(" {0} int  identity(1,1) primary key,", item.name);
                                        else
                                            sql += string.Format(" {0} int,", item.name);
                                        break;
                                    case 106:
                                        sql += string.Format(" {0} decimal(18,2),", item.name);
                                        break;
                                    case 167:
                                        if (item.length == 8000)
                                            sql += string.Format(" {0} varchar(max),", item.name, item.length == null ? 50 : item.length);
                                        else
                                            sql += string.Format(" {0} varchar({1}),", item.name, item.length == null ? 50 : item.length);
                                        break;
                                    case 61:
                                        sql += string.Format(" {0} datetime,", item.name, item.length);
                                        break;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        //CreateUserID、CreateDate、CreateUserName、UpdateUserID、UpdateDate、UpdateUserName
                        sql += " CreateUserID int,";
                        sql += " CreateDate datetime,";
                        sql += " CreateUserName varchar(50),";
                        sql += " UpdateUserID int,";
                        sql += " UpdateDate datetime,";
                        sql += " UpdateUserName varchar(50) ";

                        sql = sql.Substring(0, sql.Length - 1);
                        sql += ")";

                        Query16(sql, 1);
                        #endregion

                        #region 创建约束：默认值

                        #endregion

                        #region 添加约束
                        var DefaultValues = Design_ModularFields.Where(p => !string.IsNullOrEmpty(p.DefaultValue));
                        //var DefaultValues = Design_ModularFields.Where(p => p.DefaultValue != null);
                        foreach (var defaultValue in DefaultValues)
                        {
                            //var datetype = GetDataType(item);
                            sql = string.Format("ALTER TABLE {0} ADD CONSTRAINT {0}_{1} DEFAULT '{2}' FOR {1}", ControllCode, defaultValue.name, defaultValue.DefaultValue);
                            //sql = string.Format("ALTER TABLE {0} ALTER COLUMN {1} SET DEFAULT '{2}' ", ControllCode, defaultValue.name, defaultValue.DefaultValue);
                            Query16(sql, 1);
                        }

                        #endregion

                        #region 更新位置:Colid
                        //根据ID
                        sql = UpdateColidSql(Item.Design_ModularOrFunID);
                        Query16(sql, 1);
                        #endregion
                    }
                    else
                    {
                        //删除约束
                        var DefaultValues = tableColumns.Where(p => !string.IsNullOrEmpty(p.DefaultValue));
                        foreach (var defaultValue in DefaultValues)
                        {
                            //var datetype = GetDataType(item);
                            sql = string.Format("ALTER TABLE {0} ALTER COLUMN {1} DROP {0}_{1} ", ControllCode, defaultValue.name);
                            sql = string.Format("ALTER TABLE {0} DROP CONSTRAINT (0}_{1} ", ControllCode, defaultValue.name);
                            Query16(sql, 1);
                        }

                        //--位置相同：则修改：alter table Aa alter column AaNameCol1 varchar(200)
                        //--如果位置为空：则添加列 alter table 课程信息 add 上课信息 datatime
                        //--如果不存在：则删除列 alter table 课程信息 drop column 是否必修
                        #region 数据整理
                        var deleteIDsEnum = (from p in tableColumns select p.colid).Except(from o in Design_ModularFields select o.colid);
                        var updateItems = Design_ModularFields.Where(p => p.colid != null);
                        var addItems = Design_ModularFields.Where(p => p.colid == null);
                        #endregion
                        #region 删除列
                        if (deleteIDsEnum.Count() > 0)
                        {
                            var fields = new string[] { "CreateUserID", "CreateDate", "CreateUserName", "UpdateUserID", "UpdateDate", "UpdateUserName" };
                            foreach (var item in deleteIDsEnum)
                            {
                                if (item == null)
                                    continue;
                                var NameEn = tableColumns.Where(p => p.colid == item).First().name;
                                if (!fields.Contains(NameEn))
                                {
                                    sql = string.Format("alter table {0} drop column {1}", ControllCode, NameEn);
                                    Query16(sql, 1);
                                }
                            }
                        }
                        #endregion
                        #region 添加
                        if (addItems.Count() > 0)
                        {
                            foreach (var item in addItems)
                            {
                                var datetype = GetDataType(item);
                                sql = string.Format("alter table {0} add {1} {2} ", ControllCode, item.name, datetype);
                                Query16(sql, 1);
                            }
                        }
                        #endregion
                        #region 更新
                        if (updateItems.Count() > 0)
                        {
                            foreach (var item in updateItems)
                            {
                                //exec sp_rename '表明.原列名','新列名','column';
                                var tableColumn = tableColumns.Where(p => p.colid == item.colid).First();
                                if (item.name != tableColumn.name)
                                {
                                    sql = string.Format("exec sp_rename '{0}.{1}','{2}','column'; ", ControllCode, tableColumn.name, item.name);
                                    Query16(sql, 1);
                                }
                                if (item.xtype != tableColumn.xtype)
                                {
                                    var datetype = GetDataType(item);
                                    sql = string.Format("alter table {0} alter column {1} {2} ", ControllCode, item.name, datetype);
                                    Query16(sql, 1);
                                }
                                if (item.xtype == 167 && item.length != tableColumn.length)
                                {
                                    if (item.length == 8000 && tableColumn.length == -1)
                                    { }
                                    else
                                    {
                                        var datetype = GetDataType(item);
                                        sql = string.Format("alter table {0} alter column {1} {2} ", ControllCode, item.name, datetype);
                                        Query16(sql, 1);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 添加约束
                        DefaultValues = Design_ModularFields.Where(p => !string.IsNullOrEmpty(p.DefaultValue));
                        foreach (var defaultValue in DefaultValues)
                        {
                            //var datetype = GetDataType(item);
                            //alter table table_name add constraint 约束名 default '默认值' for column_name
                            sql = string.Format("ALTER TABLE {0} ADD CONSTRAINT {0}_{1} DEFAULT '{2}' FOR {1}", ControllCode, defaultValue.name, defaultValue.DefaultValue);
                            //sql = string.Format("ALTER TABLE {0} ALTER COLUMN {1} SET DEFAULT '{2}' ", ControllCode, defaultValue.name, defaultValue.DefaultValue);
                            Query16(sql, 1);
                        }

                        #endregion
                    }
                    #endregion
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

        public string GetDataType(SoftProjectAreaEntity item)
        {
            //数据类型：56：整数   106：小数    167：字符串   61：日期
            var datatype = "";
            switch (item.xtype)
            {
                case 56:
                    datatype = string.Format(" int ");
                    break;
                case 106:
                    datatype = " decimal(18,4) ";
                    break;
                case 167:
                    if (item.length == 8000)
                        datatype += string.Format("varchar(max)");
                    else
                        datatype = string.Format(" varchar({0}) ", item.length == null ? 50 : item.length);
                    break;
                case 61:
                    datatype = string.Format(" DateTime ", item.length);
                    break;
            }
            return datatype;
        }

        public string UpdateColidSql(int? Design_ModularOrFunID)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT object_id(ControllCode) id	FROM Design_ModularOrFun	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID);
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT A.*");
            sb.AppendLine("	FROM [syscolumns] A");
            sb.AppendLine("	JOIN T0 ON A.ID=T0.ID");
            sb.AppendLine(")");
            sb.AppendLine("UPDATE A SET A.colid=T1.colid");
            sb.AppendLine("FROM Design_ModularField A");
            sb.AppendLine("JOIN T1 ON A.name=T1.name");
            sb.AppendLine("WHERE Design_ModularOrFunID=" + Design_ModularOrFunID);
            return sb.ToString();
        }

        public MyResponseBase Design_ModularField_BulidEntity()
        {
            Design_ModularField_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    //(1)保存或更新数据库数据，更新表结构
                    Design_ModularField_CreateTable();

                    //(2)创建实体
                    #region 1)查询模块功能
                    var resptemp = Design_ModularOrFun_GetByID();
                    var Design_ModularOrFun = resptemp.Item;
                    #endregion

                    #region 3)根据功能模块ID查询所有字段
                    //resptemp = Design_ModularField_GetByModularOrFunID();
                    resptemp = Design_ModularField_GetByModularOrFunIDByEntity();

                    var Design_ModularFields = resptemp.Items;
                    #endregion
                    StringBuilder sbentity = new StringBuilder();
                    sbentity.AppendLine("");
                    sbentity.AppendLine("using Framework.Core;");
                    sbentity.AppendLine("using Framework.Web.Mvc;");
                    sbentity.AppendLine("using Framework.Web.Mvc.Sys;");
                    sbentity.AppendLine("using System;");
                    sbentity.AppendLine("using System.Collections.Generic;");
                    sbentity.AppendLine("using System.Linq;");
                    sbentity.AppendLine("using System.Text;");
                    sbentity.AppendLine("using System.Threading.Tasks;");
                    sbentity.AppendLine();
                    sbentity.AppendLine("namespace SoftProject.CellModel");
                    sbentity.AppendLine("{");
                    sbentity.AppendLine("    /// <summary>");
                    sbentity.AppendLine(string.Format("    /// 表：{0}({1})", Design_ModularOrFun.ControllCode, Design_ModularOrFun.ModularName));
                    sbentity.AppendLine("    /// </summary>");
                    sbentity.AppendLine("    public partial class SoftProjectAreaEntity");
                    sbentity.AppendLine("    {");

                    foreach (var item in Design_ModularFields)
                    {
                        if (!string.IsNullOrEmpty(item.QueryEn))//是否有查询字段
                        {
                            sbentity.AppendLine("        /// <summary>");
                            sbentity.AppendLine("        /// " + item.NameCn);
                            sbentity.AppendLine("        /// </summary>");
                            if (item.bPrimaryKeyOrFK != 2)
                                sbentity.AppendLine(string.Format("        public int? {0}", item.QueryEn) + "{get;set;}");
                            else
                                sbentity.AppendLine(string.Format("        //public int? {0}", item.QueryEn) + "{get;set;}");
                            sbentity.AppendLine();
                        }
                        var xtype = GetEntityDataType(item);
                        sbentity.AppendLine("        /// <summary>");
                        sbentity.AppendLine("        /// " + item.NameCn);
                        sbentity.AppendLine("        /// </summary>");
                        if (item.BEffective == 1)
                            sbentity.AppendLine(string.Format("        public {0} {1}", xtype, item.name) + "{get;set;}");
                        else
                            sbentity.AppendLine(string.Format("        //public {0} {1}", xtype, item.name) + "{get;set;}");
                        sbentity.AppendLine();
                    }
                    sbentity.AppendLine(string.Format("        public SoftProjectAreaEntity {0} ", Design_ModularOrFun.ControllCode) + "{ get; set; }");
                    sbentity.AppendLine(string.Format("        public List<SoftProjectAreaEntity> {0}s ", Design_ModularOrFun.ControllCode) + "{ get; set; }");

                    sbentity.AppendLine("    }");
                    sbentity.AppendLine("}");

                    //F:\软件项目\SoftPlatformProject\SoftPlatform\SoftPlatform\Areas\C_CustomerAreas\CellModel
                    //string filepath1=System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    //string filepath2=System.Environment.CurrentDirectory ;
                    //string filepath3=System.IO.Directory.GetCurrentDirectory();  
                    //string filepath4=System.AppDomain.CurrentDomain.BaseDirectory;
                    var path = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Areas\\{0}\\CellModel\\", Design_ModularOrFun.AreasCode);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //string filepath5 = string.Format(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Areas\\{0}\\CellModel\\{1}.cs", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
                    string filepath5 = path + Design_ModularOrFun.ControllCode + ".cs";

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

        public string GetEntityDataType(SoftProjectAreaEntity item)
        {
            //数据类型：56：整数   106：小数    167：字符串   61：日期
            var datatype = "";
            switch (item.xtype)
            {
                case 56:
                    datatype = string.Format(" int? ");
                    break;
                case 106:
                    datatype = " decimal? ";
                    break;
                case 167:
                    datatype = string.Format(" string ");
                    break;
                case 61:
                    datatype = string.Format(" DateTime? ");
                    break;
            }
            return datatype;
        }

        /// <summary>
        /// 获取页面或查询字段
        /// </summary>
        /// <param name="ModularOrFunCode"></param>
        /// <returns></returns>
        public MyResponseBase Design_ModularField_GetModularPageOrQueryField()
        {
            //Design_ModularOrFunID,ModularName
            StringBuilder sbsql1 = new StringBuilder();
            sbsql1.AppendLine("");
            sbsql1.AppendLine("SELECT DISTINCT A.Design_ModularOrFunID,ModularName");
            sbsql1.AppendLine("FROM Design_ModularOrFun A");
            sbsql1.AppendLine("JOIN Design_ModularField B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID");

            var resp1 = Query16(sbsql1.ToString());
            if (Item.Design_ModularOrFunID == null)
                Item.Design_ModularOrFunID = resp1.Items.First().Design_ModularOrFunID;

            StringBuilder sbsql = new StringBuilder();
            sbsql.AppendLine(";WITH T0 AS ");
            sbsql.AppendLine("(");
            //sbsql.AppendLine("  SELECT * ");
            //sbsql.AppendLine("  FROM [dbo].V_Design_ModularField");
            //sbsql.AppendLine("  WHERE Design_ModularOrFunID=6");
            //sbsql.AppendLine("  UNION ALL");
            sbsql.AppendLine("  SELECT *");
            sbsql.AppendLine("  FROM [dbo].V_Design_ModularField");
            sbsql.AppendLine("  WHERE Design_ModularOrFunID=" + Item.Design_ModularOrFunID);
            sbsql.AppendLine(")");
            sbsql.AppendLine("SELECT * ");
            sbsql.AppendLine("FROM T0");
            if (Item.Design_ModularFieldIDs != null)
                sbsql.AppendLine(string.Format("WHERE  Design_ModularFieldID NOT IN({0}) ", Item.Design_ModularFieldIDs));
            sbsql.AppendLine("Order By Sort");
            var resp = Query16(sbsql.ToString());

            resp.Item.Design_ModularOrFuns = resp1.Items;
            return resp;
        }
    }
}
