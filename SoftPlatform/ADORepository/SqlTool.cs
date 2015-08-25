using Framework.Core;
using Framework.Web.Mvc.ADORepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Text;
using SoftProject.CellModel;
using SoftProject.Domain;


namespace Framework.Web.Mvc
{
    public class SqlTool
    {
        /// <summary>
        /// Sql语句查询：获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sdr"></param>
        /// <returns></returns>
        public static T Read<T>(SafeDataReader sdr) where T : new()
        {
            T TargetItem = new T();
            Type type = TargetItem.GetType();
            //可设置为空
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in props)
            {
                if (p.Name == "DBCommandType")
                {

                }
                var propertieType = p.PropertyType;
                if (propertieType.IsClass && propertieType.Name != "String")
                    continue;
                string propertieTypeName = "";
                object val = null;
                var schema = sdr.GetSchemaTable().Select(string.Format("ColumnName='{0}'", p.Name));
                if (schema.Length > 0)
                {
                    var dbType = schema.First()["DataType"].ToString();
                    dbType = dbType.Substring(dbType.LastIndexOf('.') + 1).ToLower();

                    if (propertieType.IsGenericType && propertieType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        propertieType = p.PropertyType.GetGenericArguments()[0];
                        propertieTypeName = propertieType.Name.ToLower();
                        if (dbType == propertieTypeName)
                        {
                            #region 获取值
                            if (propertieTypeName == "int16")
                            {
                                val = sdr.GetNullInt16(p.Name);
                            }
                            else if (propertieTypeName == "int32")
                            {
                                val = sdr.GetNullInt32(p.Name);
                            }
                            else if (propertieTypeName == "int64")
                            {
                                val = sdr.GetNullInt64(p.Name);
                            }
                            else if (propertieTypeName == "decimal")
                            {
                                val = sdr.GetNullDecimal(p.Name);
                            }
                            else if (propertieTypeName == "datetime")
                            {
                                val = sdr.GetNullDateTime(p.Name);
                            }
                            else if (propertieTypeName == "boolean")
                            {
                                val = sdr.GetNullBoolean(p.Name);
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        #region 获取值
                        propertieTypeName = propertieType.Name.ToLower();
                        if (propertieType.IsEnum)
                            propertieTypeName = "int32";
                        if (dbType == propertieTypeName)
                        {
                            if (propertieTypeName == "int16")
                            {
                                val = sdr.GetInt16(p.Name);
                            }
                            else if (propertieTypeName == "int32")
                            {
                                val = sdr.GetInt32(p.Name);
                            }
                            else if (propertieTypeName == "int64")
                            {
                                val = sdr.GetInt64(p.Name);
                            }
                            else if (propertieTypeName == "decimal")
                            {
                                val = sdr.GetDecimal(p.Name);
                            }
                            else if (propertieTypeName == "datetime")
                            {
                                val = sdr.GetDateTime(p.Name);
                            }
                            else if (propertieTypeName == "boolean")
                            {
                                val = sdr.GetBoolean(p.Name);
                            }
                            else if (propertieTypeName == "string")
                            {
                                val = sdr.GetString(p.Name);
                            }
                        }
                        #endregion
                    }
                    p.SetValue(TargetItem, val, null);
                }
            }
            return TargetItem;
        }

        ///// <summary>
        ///// 获取对象中字段名列表信息
        ///// </summary>
        ///// <param name="Fields"></param>
        ///// <param name="Item"></param>
        ///// <returns></returns>
        //public static Tuple<string, SqlParameter[]> BulidPKQueryParas<T>(string[] Fields, T Item)
        //{
        //    List<SqlParameter> paras = new List<SqlParameter>();
        //    Type type = Item.GetType();

        //    string strsql = "select * from  " + type.Name + " ";
        //    string strsqlW = "Where 1=1 ";

        //    //var fieldinfos= type.GetFields();
        //    foreach (var field in Fields)
        //    {
        //        PropertyInfo props = type.GetProperty(field);
        //        var value = props.GetValue(Item, null);
        //        //string strVal = "";
        //        //if (value != null)
        //        //    strVal = value.ToString();

        //        var para = "@" + field;

        //        paras.Add(new SqlParameter(para, value));
        //        strsqlW += " AND " + field + "=" + para;
        //    }
        //    strsql += strsqlW;

        //    return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        //}

        /// <summary>
        /// 插入配置：
        /// ArrDBFieldVals：添加的字段名
        /// </summary>
        /// <typeparam name="CellT"></typeparam>
        /// <param name="DomainModel"></param>
        /// <returns></returns>
        public static Tuple<string, SqlParameter[]> BulidInsertParas(SoftProjectAreaEntityDomain DomainModel)
        {
            string strsql = "insert into " + DomainModel.TableName + "(";
            List<SqlParameter> paras = new List<SqlParameter>();

            Type type = DomainModel.Item.GetType();

            #region 添加

            string strsqlV = "VALUES(";

            #region 根据给定的属性名获取值
            var arrDBFieldVals = DomainModel.Sys_HOperControl.DBSqlParam.Split(',');
            foreach (string fieldn in arrDBFieldVals)
            {
                //"\r\n\r\n
                var field = fieldn.Replace("\r", "");
                field = field.Replace("\n", "");
                field = field.Replace(" ", "");

                PropertyInfo propertyInfo = type.GetProperty(field);
                if (propertyInfo == null)
                    continue;
                var para = "@" + field;
                var val = propertyInfo.GetValue(DomainModel.Item, null);
                if (val != null)
                {
                    paras.Add(new SqlParameter(para, val));
                    strsql += field + ",";
                    strsqlV += "@" + field + ",";
                }
                else
                {
                    strsql += field + ",";
                    strsqlV += "null,";
                }
            }
            #endregion

            #region 创建人ID、创建人、创建日期、更新人ID、更新人、更新日期

            strsql += "CreateUserID,CreateUserName,CreateDate,UpdateUserID,UpdateUserName,UpdateDate)";
            strsqlV += "@CreateUserID,@CreateUserName,@CreateDate,@UpdateUserID,@UpdateUserName,@UpdateDate)";

            PropertyInfo propertyInfofix = type.GetProperty("CreateUserID");
            paras.Add(new SqlParameter("@CreateUserID", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("CreateUserName");
            paras.Add(new SqlParameter("@CreateUserName", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("CreateDate");
            paras.Add(new SqlParameter("@CreateDate", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("UpdateUserID");
            paras.Add(new SqlParameter("@UpdateUserID", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("UpdateUserName");
            paras.Add(new SqlParameter("@UpdateUserName", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("UpdateDate");
            paras.Add(new SqlParameter("@UpdateDate", propertyInfofix.GetValue(DomainModel.Item, null)));

            #endregion

            strsql += strsqlV + ";";
            strsql += "select scope_identity() ";
            #endregion

            return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        }

        /// <summary>
        /// 更新配置：
        /// ArrDBFieldVals：更新的字段
        /// DBFieldWheresReplaces：条件
        /// </summary>
        /// <typeparam name="CellT"></typeparam>
        /// <param name="DomainModel"></param>
        /// <returns></returns>
        public static Tuple<string, SqlParameter[]> BulidUpdateParas(SoftProjectAreaEntityDomain DomainModel)
        {
            List<SqlParameter> paras = new List<SqlParameter>();

            Type type = DomainModel.Item.GetType();

            string strsql = "Update " + DomainModel.TableName + " Set "; ;

            #region 生成查询条件
            //PkField
            string PkField = DomainModel.PKField;// PKFields[0];
            //string strsqlWR = "  " + DomainModel.Sys_HOperControl.DBTSql;
            string strsqlWR = string.Format(" WHERE  {0}=@{0}", PkField);

            PropertyInfo propertyInfo = type.GetProperty(PkField);
            var val = propertyInfo.GetValue(DomainModel.Item, null);
            paras.Add(new SqlParameter("@" + PkField, val));

            #endregion
            //可设置为空
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            #region 修改

            #region 根据给定的属性名获取值
            //var arrDBFieldVals = DomainModel.Sys_HOperControl.DBSqlParam.Split(',');

            var ArrDBFieldVals = DomainModel.Sys_HOperControl.DBSqlParam.Split(',');
            foreach (string field1 in ArrDBFieldVals)
            {
                var field = field1;//.Trim();
                //field = field.Replace("\r", "");
                //field = field.Replace("\n", "");
                propertyInfo = type.GetProperty(field);
                if (propertyInfo == null)
                    continue;

                var para = "@" + field;
                val = propertyInfo.GetValue(DomainModel.Item, null);
                if (val != null)
                {
                    paras.Add(new SqlParameter(para, val));
                    strsql += field + "=" + "@" + field + ",";
                }
                else
                {
                    strsql += field + "=null,";
                }
            }

            //#region Sql语句占位符使用方式

            ////var strsqlWR = "  Where 1=1 ";
            //if (!string.IsNullOrEmpty(DomainModel.Sys_HOperControl.DBFieldWheresReplaces))
            //{
            //    var dBFieldWheresReplaces = DomainModel.Sys_HOperControl.DBFieldWheresReplaces.Split(',');
            //    foreach (var str in dBFieldWheresReplaces)
            //    {
            //        var arrItem = str.Split('|');
            //        if (arrItem.Length == 1)//静态，只加入参数
            //        {
            //            //获取字段的值
            //            string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
            //            PropertyInfo propertyInfo = type.GetProperty(field);
            //            var val = propertyInfo.GetValue(DomainModel.Item, null);
            //            paras.Add(new SqlParameter(arrItem[0], val));
            //        }
            //        else
            //        {
            //            string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
            //            PropertyInfo propertyInfo = type.GetProperty(field);
            //            var val = propertyInfo.GetValue(DomainModel.Item, null);
            //            if (val != null)//形如：And UserID=@UserID
            //            {
            //                if (arrItem.Length == 2)//动态等值，加入参数、替换语句
            //                {
            //                    var strsqlWR1 = " AND " + field + "=" + arrItem[1];
            //                    paras.Add(new SqlParameter(arrItem[1], val));
            //                    strsqlWR = strsqlWR.Replace(arrItem[1], strsqlWR1);//拼接语句，解决条件在开始或者中间问题
            //                }
            //                else
            //                {
            //                    if (arrItem[2].Contains(" IN"))//不需要参数，替换IN语句内容，替换占位符
            //                    {
            //                        arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
            //                        strsqlWR = strsqlWR.Replace(arrItem[1], arrItem[2]);
            //                    }
            //                    else//加入参数，替换占位符语句
            //                    {
            //                        paras.Add(new SqlParameter(arrItem[1], val));
            //                        strsqlWR = strsqlWR.Replace(arrItem[1], arrItem[2]);
            //                    }
            //                }
            //            }
            //            else
            //                strsqlWR = strsqlWR.Replace(arrItem[1], "");
            //        }
            //    }
            //}

            //#endregion

            #endregion

            strsql += "UpdateUserID=@UpdateUserID,UpdateUserName=@UpdateUserName,UpdateDate=@UpdateDate  ";

            PropertyInfo propertyInfofix = type.GetProperty("UpdateUserID");
            paras.Add(new SqlParameter("@UpdateUserID", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("UpdateUserName");
            paras.Add(new SqlParameter("@UpdateUserName", propertyInfofix.GetValue(DomainModel.Item, null)));

            propertyInfofix = type.GetProperty("UpdateDate");
            paras.Add(new SqlParameter("@UpdateDate", propertyInfofix.GetValue(DomainModel.Item, null)));

            //strsql = strsql.Substring(0, strsql.Length - 1);
            strsql += strsqlWR;

            #endregion

            return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        }

        /// <summary>
        /// 删除
        /// DBFieldWheresReplaces:条件
        /// </summary>
        /// <typeparam name="CellT"></typeparam>
        /// <param name="DomainModel"></param>
        /// <returns></returns>
        public static Tuple<string, SqlParameter[]> BulidDeleteParas(SoftProjectAreaEntityDomain DomainModel)        {
            List<SqlParameter> paras = new List<SqlParameter>();

            Type type = DomainModel.Item.GetType();

            #region 生成查询条件

            string PkField = DomainModel.PKField;// PKFields[0];
            //string strsqlWR = "  " + DomainModel.Sys_HOperControl.DBTSql;
            string strsqlWR = string.Format(" WHERE  {0}=@{0}", PkField);

            PropertyInfo propertyInfo = type.GetProperty(PkField);
            var val = propertyInfo.GetValue(DomainModel.Item, null);
            paras.Add(new SqlParameter("@" + PkField, val));
            #endregion

            string strsql = "DELETE " + DomainModel.TableName + strsqlWR;

            return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        }


        /////////////////////////////////////////
        /// <summary>
        /// 自定义语句
        /// </summary>
        /// <typeparam name="CellT"></typeparam>
        /// <param name="DomainModel"></param>
        /// <returns></returns>
        public static Tuple<string, SqlParameter[]> BulidSqlItemsParas16(SoftProjectAreaEntityDomain DomainModel)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            SoftProjectAreaEntity moci = DomainModel.Sys_HOperControl;
            Type type = DomainModel.Item.GetType();

            string strsql = moci.DBTSql;

            #region Sql语句占位符使用方式

            var strsqlWR = "";
            if (!string.IsNullOrEmpty(moci.DBSqlParam))
            {
                var dBFieldWheresReplaces = moci.DBSqlParam.Split(',');
                foreach (var str in dBFieldWheresReplaces)
                {
                    var arrItem = str.Split('|');
                    if (arrItem.Length == 1)//静态，只加入参数
                    {
                        string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
                        PropertyInfo propertyInfo = type.GetProperty(field);
                        if (propertyInfo == null)
                            throw new Exception("字段：【" + field + "】不存在");
                        var val = propertyInfo.GetValue(DomainModel.Item, null);
                        //if(val != null)
                        paras.Add(new SqlParameter(arrItem[0], val));
                    }
                    else
                    {
                        string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
                        field = field.Replace("\r\n", "");
                        PropertyInfo propertyInfo = type.GetProperty(field);
                        if (propertyInfo == null)
                            throw new Exception("字段：【" + field + "】不存在");
                        var val = propertyInfo.GetValue(DomainModel.Item, null);
                        if (arrItem[0] == "1")//形如：aaa=isnull(@aaa,aaa)，如果@aaa为null，将以NULL替换
                        {
                            if (val != null)//形如：And UserID=@UserID
                                paras.Add(new SqlParameter(arrItem[1], val));
                            else
                                strsql = strsql.Replace(arrItem[1], "NULL");
                        }
                        else
                        {
                            #region 类型2、3
                            if (val != null)
                            {
                                if (arrItem[0] == "2")
                                {////先替换表达式，再替换Sql语句，例如IN、或者表达式，(隐匿应用，表达式中可以没有替换值，功能3也可以实现此功能)
                                    arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
                                    strsql = strsql.Replace(arrItem[1], arrItem[2]);
                                }
                                else if (arrItem[0] == "3")//参数据传递：例如@DateBegin<BirthDate,形如：And UserID=@UserID
                                {
                                    paras.Add(new SqlParameter(arrItem[1], val));
                                    strsql = strsql.Replace(arrItem[1], arrItem[2]);
                                }
                            }
                            else
                                strsql = strsql.Replace(arrItem[1], "");
                            #endregion
                        }
                    }
                }
            }

            #endregion

            #region Sql语句占位符使用方式

            //var strsqlWR = "";
            //if (!string.IsNullOrEmpty(moci.DBSqlParam))
            //{
            //    var DBSqlParam = moci.DBSqlParam.Split(',');
            //    foreach (var str in DBSqlParam)
            //    {
            //        var arrItem = str.Split('|');
            //        if (arrItem.Length == 1)//静态，只加入参数
            //        {
            //            //获取字段的值
            //            string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
            //            if (field == "RankInfos")
            //            {
            //                string DBOrderBys = "";
            //                foreach (var rank in DomainModel.PageQueryBase.RankInfos)
            //                {
            //                    DBOrderBys += rank.Property + " " + (rank.Ascending ? "asc" : "desc") + ",";
            //                }

            //                if (DBOrderBys.Length > 0)
            //                {
            //                    DBOrderBys = DBOrderBys.Substring(0, DBOrderBys.Length - 1);
            //                }
            //                strsql = strsql.Replace(arrItem[0], DBOrderBys);
            //            }
            //            else
            //            {
            //                PropertyInfo propertyInfo = type.GetProperty(field);
            //                var val = propertyInfo.GetValue(DomainModel.Item, null);
            //                paras.Add(new SqlParameter(arrItem[0], val));
            //            }
            //        }
            //        else if (arrItem[0] == "3")
            //        {
            //            string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
            //            PropertyInfo propertyInfo = type.GetProperty(field);
            //            var val = propertyInfo.GetValue(DomainModel.Item, null);
            //            if (val != null)//形如：And UserID=@UserID
            //            {
            //                if (arrItem.Length == 2)//动态等值，加入参数、替换语句
            //                {
            //                    strsqlWR = " AND " + field + "=" + arrItem[1];
            //                    paras.Add(new SqlParameter(arrItem[1], val));
            //                    strsql = strsql.Replace(arrItem[1], strsqlWR);//拼接语句，解决条件在开始或者中间问题
            //                }
            //                else
            //                {
            //                    if (arrItem[2].Contains(" IN"))//不需要参数，替换IN语句内容，替换占位符
            //                    {
            //                        arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
            //                        strsql = strsql.Replace(arrItem[1], arrItem[2]);
            //                    }
            //                    else//加入参数，替换占位符语句
            //                    {
            //                        paras.Add(new SqlParameter(arrItem[1], val));
            //                        strsql = strsql.Replace(arrItem[1], arrItem[2]);
            //                    }
            //                }
            //            }
            //            else
            //                strsql = strsql.Replace(arrItem[1], "");
            //        }
            //        else if (arrItem[0] == "4")
            //        {
            //            string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
            //            field = field.Replace("\r\n", "");
            //            PropertyInfo propertyInfo = type.GetProperty(field);
            //            var val = propertyInfo.GetValue(DomainModel.Item, null);
            //            if (val != null)//形如：And UserID=@UserID
            //            {
            //                paras.Add(new SqlParameter(arrItem[1], val));
            //            }
            //            else
            //            {
            //                strsql = strsql.Replace(arrItem[1], "NULL");
            //            }
            //        }
            //    }
            //}

            #endregion

            return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        ///// <summary>
        ///// 生成基本Sql语句：替换参数等
        ///// </summary>
        ///// <typeparam name="CellT"></typeparam>
        ///// <param name="DomainModel"></param>
        ///// <returns></returns>
        //public static Tuple<string, SqlParameter[]> BulidBaseSqlParas<CellT>(IDomainBase<CellT> DomainModel)
        //    where CellT : new()
        //{
        //    List<SqlParameter> paras = new List<SqlParameter>();
        //    Sys_HOperControl moci = DomainModel.Sys_HOperControl;
        //    Type type = DomainModel.Item.GetType();

        //    string strsql = moci.DBTSql;

        //    #region Sql语句占位符使用方式

        //    var strsqlWR = "";
        //    if (!string.IsNullOrEmpty(moci.DBFieldWheresReplaces))
        //    {
        //        var dBFieldWheresReplaces = moci.DBFieldWheresReplaces.Split(',');
        //        foreach (var str in dBFieldWheresReplaces)
        //        {
        //            var arrItem = str.Split('|');
        //            if (arrItem.Length == 1)//静态，只加入参数
        //            {
        //                string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                if (propertyInfo == null)
        //                    throw new Exception("字段：【" + field + "】不存在");
        //                var val = propertyInfo.GetValue(DomainModel.Item, null);
        //                //if(val != null)
        //                paras.Add(new SqlParameter(arrItem[0], val));
        //            }
        //            else
        //            {
        //                string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
        //                field = field.Replace("\r\n", "");
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                if (propertyInfo == null)
        //                    throw new Exception("字段：【" + field + "】不存在");
        //                var val = propertyInfo.GetValue(DomainModel.Item, null);
        //                if (arrItem[0] == "1")//形如：aaa=isnull(@aaa,aaa)，如果@aaa为null，将以NULL替换
        //                {
        //                    if (val != null)//形如：And UserID=@UserID
        //                        paras.Add(new SqlParameter(arrItem[1], val));
        //                    else
        //                        strsql = strsql.Replace(arrItem[1], "NULL");
        //                }
        //                else
        //                {
        //                    #region 类型2、3
        //                    if (val != null)
        //                    {
        //                        if (arrItem[0] == "2")
        //                        {////先替换表达式，再替换Sql语句，例如IN、或者表达式，(隐匿应用，表达式中可以没有替换值，功能3也可以实现此功能)
        //                            arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
        //                            strsql = strsql.Replace(arrItem[1], arrItem[2]);
        //                        }
        //                        else if (arrItem[0] == "3")//参数据传递：例如@DateBegin<BirthDate,形如：And UserID=@UserID
        //                        {
        //                            paras.Add(new SqlParameter(arrItem[1], val));
        //                            strsql = strsql.Replace(arrItem[1], arrItem[2]);
        //                        }
        //                    }
        //                    else
        //                        strsql = strsql.Replace(arrItem[1], "");
        //                    #endregion
        //                }
        //            }
        //        }
        //    }

        //    #endregion

        //    return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        //}

        public static Tuple<string, SqlParameter[]> BulidBaseSqlParas128(SoftProjectAreaEntityDomain DomainModel)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            SoftProjectAreaEntity moci = DomainModel.Sys_HOperControl;
            Type type = DomainModel.Item.GetType();

            string strsql = moci.DBTSql;

            #region 动态select列名

            //if (!string.IsNullOrEmpty(DomainModel.DnyFunName))
            //{
            //    //TableHeadInfos[j].Sys_DynReportDefineDetailID
            //    //var sbSelect = new StringBuilder();
            //    var sbCal = new StringBuilder();
            //    var fields = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == DomainModel.DnyFunName && p.Sys_DynReportDefineDetailID > 100).OrderBy(p => p.TableInfoSort).ToList();
            //    var sbtemp = new StringBuilder();
            //    #region 查询
            //    sbtemp.AppendLine(";WITH T1000 AS");
            //    sbtemp.AppendLine("(");
            //    sbtemp.AppendLine("SELECT ");
            //    var sbSelect = string.Join(",", fields.Select(p => p.NameEn).ToArray());
            //    sbtemp.AppendLine(sbSelect);
            //    sbtemp.AppendLine(" FROM " + DomainModel.QueryViewName);
            //    sbtemp.AppendLine("WHERE 1=1 ");
            //    sbtemp.AppendLine("sqlplaceholder");
            //    sbtemp.AppendLine(")");
            //    sbtemp.AppendLine(",TCal1000 AS");
            //    sbtemp.AppendLine("(");
            //    sbtemp.AppendLine("    SELECT ");
            //    #endregion

            //    #region 计算列
            //    fields.ForEach(p =>
            //    {
            //        if (!string.IsNullOrEmpty(p.Calformula))
            //        {
            //            if (p.Calformula.ToLower() == "sum")
            //            {
            //                sbCal.Append(" Sum(" + p.NameEn + ") " + p.NameEn + ",");
            //            }
            //        }
            //        else
            //            sbCal.Append(" NULL " + p.NameEn + ",");
            //    });
            //    if (sbCal.Length > 0)
            //    {
            //        var sbCalTemp = sbCal.ToString();
            //        sbCalTemp = sbCalTemp.Substring(0, sbCalTemp.Length - 1);
            //        sbtemp.AppendLine(sbCalTemp);
            //    }
            //    sbtemp.AppendLine("    ,NULL R");
            //    sbtemp.AppendLine("    FROM T1000");
            //    sbtemp.AppendLine(")");
            //    #endregion
            //    strsql = sbtemp.ToString();
            //    //查询：C_ForecastAreaIndex
            //    //视图：名称
            //    //;WITH T1000 AS
            //    //(
            //    //    SELECT  C_ForecastID,CompanyName,P_ProductID,[ProductModel],Month01,Month02,Month03,Month04,Month05,Month06,Month07,Month08,Month09,Month10,Month11,Month12
            //    //    FROM V_C_ForecastArea
            //    //    WHERE 1=1
            //    //    sqlplaceholder
            //    //)
            //}

            #endregion

            #region Sql语句占位符使用方式

            var strsqlWR = "";
            if (!string.IsNullOrEmpty(moci.DBSqlParam))
            {
                var dBFieldWheresReplaces = moci.DBSqlParam.Split(',');
                foreach (var str in dBFieldWheresReplaces)
                {
                    var arrItem = str.Split('|');
                    if (arrItem.Length == 1)//静态，只加入参数
                    {
                        string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
                        PropertyInfo propertyInfo = type.GetProperty(field);
                        if (propertyInfo == null)
                            throw new Exception("字段：【" + field + "】不存在");
                        var val = propertyInfo.GetValue(DomainModel.Item, null);
                        //if(val != null)
                        paras.Add(new SqlParameter(arrItem[0], val));
                    }
                    else
                    {
                        string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
                        field = field.Replace("\r\n", "");
                        PropertyInfo propertyInfo = type.GetProperty(field);
                        if (propertyInfo == null)
                            throw new Exception("字段：【" + field + "】不存在");
                        var val = propertyInfo.GetValue(DomainModel.Item, null);
                        if (arrItem[0] == "1")//形如：aaa=isnull(@aaa,aaa)，如果@aaa为null，将以NULL替换
                        {
                            if (val != null)//形如：And UserID=@UserID
                                paras.Add(new SqlParameter(arrItem[1], val));
                            else
                                strsql = strsql.Replace(arrItem[1], "NULL");
                        }
                        else
                        {
                            #region 类型2、3
                            if (val != null)
                            {
                                if (arrItem[0] == "2")
                                {////先替换表达式，再替换Sql语句，例如IN、或者表达式，(隐匿应用，表达式中可以没有替换值，功能3也可以实现此功能)
                                    arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
                                    strsql = strsql.Replace(arrItem[1], arrItem[2]);
                                }
                                else if (arrItem[0] == "3")//参数据传递：例如@DateBegin<BirthDate,形如：And UserID=@UserID
                                {
                                    paras.Add(new SqlParameter(arrItem[1], val));
                                    strsql = strsql.Replace(arrItem[1], arrItem[2]);
                                }
                            }
                            else
                                strsql = strsql.Replace(arrItem[1], "");
                            #endregion
                        }
                    }
                }
            }

            #endregion

            string strquery = "";
            var ser = 0;

            #region 等值查询条件

            if (!string.IsNullOrEmpty(moci.EqualQueryParam))
            {
                var queryParams = moci.EqualQueryParam.Split(',');
                foreach (var field in queryParams)
                {
                    //string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
                    PropertyInfo propertyInfo = type.GetProperty(field);
                    if (propertyInfo == null)
                        throw new Exception("字段：【" + field + "】不存在");
                    var val = propertyInfo.GetValue(DomainModel.Item, null);
                    //if(val != null)
                    strquery += field + "=@" + field;
                    paras.Add(new SqlParameter("@" + field, val));
                }
            }

            #endregion

            #region 查询条件:Querys

            foreach (var item in DomainModel.Querys)
            {
                if (string.IsNullOrEmpty(item.FieldName) || string.IsNullOrEmpty(item.Value))
                    continue;
                if (item.QuryType == 1)
                {
                    var paramName = "@" + item.FieldName + ser;
                    ser++;
                    if (string.IsNullOrEmpty(item.AndOr) || string.IsNullOrEmpty(item.Oper))
                        continue;
                    #region 自定义查询条件
                    //添加参数
                    switch (item.Oper.ToLower())
                    {
                        case "in":
                            strquery += " " + item.AndOr + "  " + item.FieldName + " In(" + item.Value + ")";
                            break;
                        case "noin":
                            strquery += " " + item.AndOr + "  " + item.FieldName + " Not In(" + item.Value + ")";
                            break;
                        case "like":
                            strquery += " " + item.AndOr + "  " + item.FieldName + " Like '%'+" + paramName + "+'%'";
                            paras.Add(new SqlParameter(paramName, item.Value));
                            break;
                        case "nolike":
                            strquery += " " + item.AndOr + "  " + item.FieldName + " Not Like '%'+" + paramName + "+'%'";
                            paras.Add(new SqlParameter(paramName, item.Value));
                            break;
                        default:
                            strquery += " " + item.AndOr + "  " + item.FieldName + item.Oper + paramName;
                            paras.Add(new SqlParameter(paramName, item.Value));
                            break;
                    }
                    #endregion
                }
                else
                {
                    //string[] sArray=Regex.Split(str,"js",RegexOptions.IgnoreCase);
                    var regs = Regex.Split(item.FieldName, "___", RegexOptions.IgnoreCase);// item.FieldName.Split('__');
                    var FieldNames = Regex.Split(regs[0], "__", RegexOptions.IgnoreCase).ToList();
                    var Oper = regs[1].ToLower();
                    strquery += FieldNames.Count > 0 ? " AND (" : " AND ";
                    #region 查询条件
                    //添加参数
                    for (var i = 0; i < FieldNames.Count; i++)
                    {
                        var fieldname = FieldNames[i];
                        var paramName = "@" + fieldname + ser;
                        ser++;
                        if (i > 0 && FieldNames.Count > 1)
                            strquery += " OR ";
                        switch (Oper)
                        {
                            case "bitand":
                                var val = Convert.ToInt32(item.Value);
                                if (val >= 1)
                                    strquery += fieldname + "&1=1 OR ";
                                if (val >= 2)
                                    strquery += fieldname + "&2=2 OR ";
                                if (val >= 4)
                                    strquery += fieldname + "&4=4 OR ";
                                if (val >= 8)
                                    strquery += fieldname + "&8=8 OR ";
                                if (val >= 16)
                                    strquery += fieldname + "&16=16 OR ";
                                if (val >= 32)
                                    strquery += fieldname + "&32=32 OR ";
                                if (val >= 64)
                                    strquery += fieldname + "&64=64 OR ";
                                if (val >= 128)
                                    strquery += fieldname + "&128=128 OR ";
                                strquery = strquery.Substring(0, strquery.Length - 3);
                                break;
                            case "in":
                                strquery += fieldname + " In(" + item.Value + ")";
                                break;
                            case "noin":
                                strquery += fieldname + " Not In(" + item.Value + ")";
                                break;
                            case "like":
                                strquery += fieldname + " Like '%'+" + paramName + "+'%'";
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "nolike":
                                strquery += fieldname + " Not Like '%'+" + paramName + "+'%'";
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "less":
                                strquery += fieldname + "<" + paramName;
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "lessequal":
                                strquery += fieldname + "<=" + paramName;
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "equal":
                                strquery += fieldname + "=" + paramName;
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "notequal":
                                strquery += fieldname + "!=" + paramName;
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "greater":
                                strquery += fieldname + ">" + paramName;
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                            case "greaterequal":
                                strquery += fieldname + ">=" + paramName;
                                paras.Add(new SqlParameter(paramName, item.Value));
                                break;
                        }
                    }
                    #endregion
                    strquery += FieldNames.Count > 0 ? ")" : "";
                }
            }

            strsql = strsql.Replace("sqlplaceholder", strquery);
            #endregion

            return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static string AppendCount(string strsql)
        {
            strsql = strsql + " SELECT  COUNT(*) TotalItems From T1000 ";
            return strsql;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="strsql"></param>
        /// <param name="PageQueryBase"></param>
        /// <returns></returns>
        public static string AppendOrder(string strsql, PageQueryBase PageQueryBase)
        {
            string DBOrderBys = "";
            foreach (var rank in PageQueryBase.RankInfos)
            {
                DBOrderBys += rank.Property + " " + (rank.Ascending ? "asc" : "desc") + ",";
            }
            if (DBOrderBys.Length > 0)
            {
                DBOrderBys = " ORDER  BY " + DBOrderBys.Substring(0, DBOrderBys.Length - 1);
            }
            if (string.IsNullOrEmpty(DBOrderBys))
                DBOrderBys = " ORDER  BY (select 1)";
            strsql = strsql + string.Format("\n,TO1000 AS( SELECT *,ROW_NUMBER() OVER ({0}) R  From T1000 )", DBOrderBys);
            //string strPage = string.Format(",TP1000 AS (SELECT TOP {0} * FROM  TP01  WHERE R>{1})", PageQueryBase.PageSize, PageQueryBase.SkipNum);
            //strsql += strPage;
            return strsql;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="strsql"></param>
        /// <param name="PageQueryBase"></param>
        /// <returns></returns>
        public static string AppendPage(string strsql, PageQueryBase PageQueryBase)
        {
            string strPage = string.Format("\n,TP1000 AS (SELECT TOP {0} * FROM  TO1000  WHERE R>{1})", PageQueryBase.PageSize, PageQueryBase.SkipNum);
            strsql += strPage;
            return strsql;
        }

        public static string AppendTotal(string strsql, string ModularOrFunCode)
        {
            var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();

            var fields = PageFormEleTypes(Design_ModularOrFun);
            var fieldCals = fields.Where(p => !string.IsNullOrEmpty(p.Calformula)).ToList();

            #region 动态select列名

            ////TableHeadInfos[j].Sys_DynReportDefineDetailID
            ////var sbSelect = new StringBuilder();
            //var sbCal = new StringBuilder();
            //var fields = ProjectCache.Sys_PageFunTableInfos.Where(p => p.FunCode == DomainModel.DnyFunName && p.Sys_DynReportDefineDetailID > 100).OrderBy(p => p.TableInfoSort).ToList();
            var sbtemp = new StringBuilder();
            //#region 查询
            //sbtemp.AppendLine(";WITH T1000 AS");
            //sbtemp.AppendLine("(");
            //sbtemp.AppendLine("SELECT ");
            //var sbSelect = string.Join(",", fields.Select(p => p.NameEn).ToArray());
            //sbtemp.AppendLine(sbSelect);
            //sbtemp.AppendLine(" FROM " + DomainModel.QueryViewName);
            //sbtemp.AppendLine("WHERE 1=1 ");
            //sbtemp.AppendLine("sqlplaceholder");
            //sbtemp.AppendLine(")");
            //sbtemp.AppendLine(",TCal1000 AS");
            //sbtemp.AppendLine("(");
            sbtemp.AppendLine("    SELECT COUNT(*) AS TotalItems,");
            #endregion

            #region 计算列
            var sbCal = new StringBuilder();

            fieldCals.ForEach(p =>
            {
                if (p.Calformula == "Sum")
                {
                    sbCal.Append(" Sum(" + p.name + ") " + p.name + ",");
                }
            });
            if (sbCal.Length > 0)
            {
                var sbCalTemp = sbCal.ToString();
                sbCalTemp = sbCalTemp.Substring(0, sbCalTemp.Length - 1);
                sbtemp.AppendLine(sbCalTemp);
            }
            sbtemp.AppendLine("    FROM T1000");
            //sbtemp.AppendLine(")");
            #endregion
            strsql = strsql + sbtemp.ToString();
            return strsql;
        }

        /// <summary>
        /// 查询页面，表格显示字段
        /// </summary>
        /// <param name="Design_ModularOrFun"></param>
        public static List<SoftProjectAreaEntity> PageFormEleTypes(SoftProjectAreaEntity Design_ModularOrFun)
        {
            List<SoftProjectAreaEntity> Fields = new List<SoftProjectAreaEntity>();
            var Design_ModularOrFunTemp = ProjectCache.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == Design_ModularOrFun.Design_ModularOrFunParentID).First();

            switch (Design_ModularOrFun.PageFormEleTypeName)
            {
                case "Page01FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page01FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page01FormEleType; });
                    break;
                case "Page02FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page02FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page02FormEleType; });
                    break;
                case "Page03FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page03FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page03FormEleType; });
                    break;
                case "Page04FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page04FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page04FormEleType; });
                    break;
                case "Page05FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page05FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page05FormEleType; });
                    break;
                case "Page06FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page06FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page06FormEleType; });
                    break;
                case "Page07FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page07FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page07FormEleType; });
                    break;
                case "Page08FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page08FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page08FormEleType; });
                    break;
                case "Page09FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page09FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page09FormEleType; });
                    break;
                case "Page10FormEleType":
                    Fields = ProjectCache.Design_ModularPageFields.Where(p => p.Design_ModularOrFunID == Design_ModularOrFunTemp.Design_ModularOrFunID && p.Page10FormEleType != null).ToList();
                    Fields.ForEach(p => { p.FormEleType = p.Page10FormEleType; });
                    break;
            }

            return Fields;
        }


        ///////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////

        ////查询--分页
        //public static Tuple<string, SqlParameter[]> BulidSelectPageParas<CellT>(IDomainBase<CellT> DomainModel)
        //    where CellT : new()
        //{
        //    List<SqlParameter> paras = new List<SqlParameter>();

        //    Sys_HOperControl moci = DomainModel.Sys_HOperControl;
        //    Type type = DomainModel.Item.GetType();

        //    string strsql = moci.DBTSql;

        //    #region Sql语句占位符使用方式

        //    var strsqlWR = "";
        //    if (!string.IsNullOrEmpty(moci.DBFieldWheresReplaces))
        //    {
        //        var dBFieldWheresReplaces = moci.DBFieldWheresReplaces.Split(',');
        //        foreach (var str in dBFieldWheresReplaces)
        //        {
        //            var arrItem = str.Split('|');
        //            if (arrItem.Length == 1)//静态，只加入参数
        //            {
        //                //获取字段的值
        //                string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(DomainModel.Item, null);
        //                paras.Add(new SqlParameter(arrItem[0], val));
        //            }
        //            else if (arrItem[0] == "2")
        //            {
        //                string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
        //                field = field.Replace("\r\n", "");
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(DomainModel.Item, null);
        //                if (val != null)//替换--形如：And UserID=@UserID 2|@aaa|@aaa
        //                {
        //                    //strsqlWR = arrItem[2].Replace(arrItem[1], val.ToString());// --" AND " + field + "=" + arrItem[1];
        //                    strsql = strsql.Replace(arrItem[1], val.ToString());
        //                }
        //                else
        //                    strsql = strsql.Replace(arrItem[1], "");
        //            }
        //            else if (arrItem[0] == "3")
        //            {
        //                string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(DomainModel.Item, null);
        //                if (val != null)//形如：And UserID=@UserID
        //                {
        //                    if (arrItem.Length == 2)//动态等值，加入参数、替换语句
        //                    {
        //                        strsqlWR = " AND " + field + "=" + arrItem[1];
        //                        paras.Add(new SqlParameter(arrItem[1], val));
        //                        strsql = strsql.Replace(arrItem[1], strsqlWR);//拼接语句，解决条件在开始或者中间问题
        //                    }
        //                    else
        //                    {
        //                        if (arrItem[2].Contains(" IN"))//不需要参数，替换IN语句内容，替换占位符
        //                        {
        //                            arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
        //                            strsql = strsql.Replace(arrItem[1], arrItem[2]);
        //                        }
        //                        else//加入参数，替换占位符语句
        //                        {
        //                            paras.Add(new SqlParameter(arrItem[1], val));
        //                            strsql = strsql.Replace(arrItem[1], arrItem[2]);
        //                        }
        //                    }
        //                }
        //                else
        //                    strsql = strsql.Replace(arrItem[1], "");
        //            }
        //            else if (arrItem[0] == "4")
        //            {
        //                string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
        //                field = field.Replace("\r\n", "");
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(DomainModel.Item, null);
        //                if (val != null)//形如：And UserID=@UserID
        //                {
        //                    paras.Add(new SqlParameter(arrItem[1], val));
        //                }
        //                else
        //                {
        //                    strsql = strsql.Replace(arrItem[1], "NULL");
        //                }
        //            }
        //        }
        //    }

        //    #endregion


        //    string DBOrderBys = "";
        //    foreach (var rank in DomainModel.PageQueryBase.RankInfos)
        //    {
        //        DBOrderBys += rank.Property + " " + (rank.Ascending ? "asc" : "desc") + ",";
        //    }

        //    if (DBOrderBys.Length > 0)
        //    {
        //        DBOrderBys = " ORDER  BY " + DBOrderBys.Substring(0, DBOrderBys.Length - 1);
        //    }
        //    if (DomainModel.PageQueryBase.IsPagination == 0)
        //    {
        //        strsql = strsql + string.Format("SELECT * From T1000 {0} ", DBOrderBys);
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(DBOrderBys))
        //            DBOrderBys = "(select 1)";
        //        strsql = strsql + string.Format(",T1001 AS( SELECT *,ROW_NUMBER() OVER ({0}) R  From T1000 )", DBOrderBys);
        //        string strPage = string.Format(" SELECT TOP {0} * FROM  T1001  WHERE R>{1}", DomainModel.PageQueryBase.PageSize, DomainModel.PageQueryBase.SkipNum);
        //        strsql += strPage;
        //    }
        //    return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        //}

        ////查询--生成Sql语句
        //public static Tuple<string, SqlParameter[]> BulidTSql(object obj, string strsql, string DBFieldWheresReplaces)
        //{
        //    List<SqlParameter> paras = new List<SqlParameter>();

        //    Type type = obj.GetType();

        //    #region Sql语句占位符使用方式

        //    //var strsqlWR = "";
        //    if (!string.IsNullOrEmpty(DBFieldWheresReplaces))
        //    {
        //        var dBFieldWheresReplaces = DBFieldWheresReplaces.Split(',');
        //        foreach (var str in dBFieldWheresReplaces)
        //        {
        //            var arrItem = str.Split('|');
        //            if (arrItem.Length == 1)//静态，只加入参数，针对固定值，又不能使用IsNULL解决，如日期<XXX之类
        //            {
        //                //获取字段的值
        //                string field = arrItem[0].Substring(1, arrItem[0].Length - 1);
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(obj, null);
        //                paras.Add(new SqlParameter(arrItem[0], val));
        //            }
        //            else if (arrItem[0] == "10") //使用 表字段=ISNULL(@变量名,表字段)
        //            {
        //                string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
        //                field = field.Replace("\r\n", "");
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(obj, null);
        //                if (val != null)//形如：And UserID=@UserID
        //                {
        //                    paras.Add(new SqlParameter(arrItem[1], val));
        //                }
        //                else
        //                {
        //                    strsql = strsql.Replace(arrItem[1], "NULL");
        //                }
        //            }
        //            else if (arrItem[0] == "11")//处理值替换
        //            {
        //                string field = arrItem[1].Substring(1, arrItem[1].Length - 1);
        //                PropertyInfo propertyInfo = type.GetProperty(field);
        //                var val = propertyInfo.GetValue(obj, null);
        //                if (val != null)//形如：And UserID=@UserID
        //                {
        //                    arrItem[2] = arrItem[2].Replace(arrItem[1], val.ToString());
        //                    strsql = strsql.Replace(arrItem[1], arrItem[2]);
        //                }
        //                else
        //                    strsql = strsql.Replace(arrItem[1], "");
        //            }
        //        }
        //    }

        //    #endregion

        //    //string DBOrderBys = "";
        //    //foreach (var rank in DomainModel.PageQueryBase.RankInfos)
        //    //{
        //    //    DBOrderBys += rank.Property + " " + (rank.Ascending ? "asc" : "desc") + ",";
        //    //}

        //    //if (DBOrderBys.Length > 0)
        //    //{
        //    //    DBOrderBys = " ORDER  BY " + DBOrderBys.Substring(0, DBOrderBys.Length - 1);
        //    //}
        //    //if (DomainModel.PageQueryBase.IsPagination == 0)
        //    //{
        //    //    strsql = strsql + string.Format("SELECT * From T1000 {0} ", DBOrderBys);
        //    //}
        //    //else
        //    //{
        //    //    if (string.IsNullOrEmpty(DBOrderBys))
        //    //        DBOrderBys = "(select 1)";
        //    //    strsql = strsql + string.Format(",T1001 AS( SELECT *,ROW_NUMBER() OVER ({0}) R  From T1000 )", DBOrderBys);
        //    //    string strPage = string.Format(" SELECT TOP {0} * FROM  T1001  WHERE R>{1}", DomainModel.PageQueryBase.PageSize, DomainModel.PageQueryBase.SkipNum);
        //    //    strsql += strPage;
        //    //}
        //    return Tuple.Create<string, SqlParameter[]>(strsql, paras.ToArray());
        //}

        //public static string RecordCount(string strsql)
        //{
        //    strsql = strsql + " SELECT  COUNT(*)  From T1000 ";
        //    return strsql;
        //}

        //public static string OrderBy(string strsql, PageQueryBase PageQueryBase)
        //{
        //    string DBOrderBys = "";
        //    foreach (var rank in PageQueryBase.RankInfos)
        //    {
        //        DBOrderBys += rank.Property + " " + (rank.Ascending ? "asc" : "desc") + ",";
        //    }

        //    if (DBOrderBys.Length > 0)
        //    {
        //        DBOrderBys = " ORDER  BY " + DBOrderBys.Substring(0, DBOrderBys.Length - 1);
        //    }
        //    else
        //    {
        //        DBOrderBys = " SELECT 1 ";
        //    }
        //    strsql = strsql + string.Format(",T1100 AS( SELECT *,ROW_NUMBER() OVER ({0}) R  From T1000 )", DBOrderBys);

        //    //if (DomainModel.PageQueryBase.IsPagination == 0)
        //    //{
        //    //    strsql = strsql + string.Format("SELECT * From T1000 {0} ", DBOrderBys);
        //    //}
        //    //else
        //    //{
        //    //    if (string.IsNullOrEmpty(DBOrderBys))
        //    //        DBOrderBys = "(select 1)";
        //    //    strsql = strsql + string.Format(",T1001 AS( SELECT *,ROW_NUMBER() OVER ({0}) R  From T1000 )", DBOrderBys);
        //    //    string strPage = string.Format(" SELECT TOP {0} * FROM  T1001  WHERE R>{1}", DomainModel.PageQueryBase.PageSize, DomainModel.PageQueryBase.SkipNum);
        //    //    strsql += strPage;
        //    //}
        //    return strsql;
        //}

        //public static string Page(string strsql, PageQueryBase PageQueryBase)
        //{
        //    string strPage = string.Format(" SELECT TOP {0} * FROM  T1100  WHERE R>{1}", PageQueryBase.PageSize, PageQueryBase.SkipNum);
        //    strsql += strPage;
        //    return strsql;
        //}
    }

}