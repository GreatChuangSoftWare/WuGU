
using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace SoftProject.Domain
{
    /// <summary>
    /// 软件项目：业务层
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        //登录人信息
        public SoftProjectAreaEntity LoginInfo
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
                return new SoftProjectAreaEntity();
            }
        }
        
        public static SoftProjectAreaEntity LoginInfostatic
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;
                return new SoftProjectAreaEntity();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SoftProjectAreaEntityDomain()
        {
            //排序
            RAInfo = new RespAttachInfo();
            //分页信息
            PageQueryBase = new PageQueryBase();
            //查询条件信息
            _Querys = new Querys();
            //单个实体初始化
            this.Item = new SoftProjectAreaEntity();
            //实体集合初始化
            this.Items = new List<SoftProjectAreaEntity>();
        }

        /// <summary>
        /// 默认Model对象
        /// </summary>
        public MyResponseBase Default()
        {
            var resp = new MyResponseBase { Item = new SoftProjectAreaEntity() };
            return resp;
        }

        /// <summary>
        /// 生成查询语句的数据库
        /// </summary>
        public void BulidHOperControl()
        {
            Sys_HOperControl = new SoftProjectAreaEntity
            {
                DBTSql = ProjectCache.GetQuerySql(TabViewName),
                DBOperType = 8,
                SelectSubType = 6,
                DBSelectResultType = 2,
                EqualQueryParam = ""
            };
        }

        /// <summary>
        /// 初始化排序字段
        /// </summary>
        public void BulidPageQueryBase()
        {
            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                if (string.IsNullOrEmpty(Design_ModularOrFun.TSqlDefaultSort))
                {
                    PageQueryBase.RankInfo = "UpdateDate|1";
                }
                else
                {
                    var sorts = Design_ModularOrFun.TSqlDefaultSort.Split('|');
                    if (sorts.Length == 2)
                        PageQueryBase.RankInfo = sorts[0] + "|" + sorts[1];
                    else
                        PageQueryBase.RankInfo = sorts[0] + "|1";
                }
            }
        }

        public MyResponseBase QueryIndex()
        {
            var resp = new MyResponseBase();

            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                if (string.IsNullOrEmpty(Design_ModularOrFun.TSqlDefaultSort))
                {
                    PageQueryBase.RankInfo = "UpdateDate|0";
                }
                else
                {
                    var sorts = Design_ModularOrFun.TSqlDefaultSort.Split('|');
                    if (sorts.Length == 2)
                        PageQueryBase.RankInfo = sorts[0] + "|" + sorts[1];
                    else
                        PageQueryBase.RankInfo = sorts[0] + "|1";
                }
            }
            SoftProjectAreaEntityDomain_Domain();

            Sys_HOperControl = new SoftProjectAreaEntity
            {
                DBTSql = ProjectCache.GetQuerySql(TabViewName),
                DBOperType = 8,
                SelectSubType = 6,
                DBSelectResultType = 2,
                EqualQueryParam = ""
            };
            if (Design_ModularOrFun.BCalCol==1)
            {
                bCal = 1;
                ModularOrFunCode = Design_ModularOrFun.ModularOrFunCode;
                //bCal,ModularOrFunCode
            }
            //BulidPageQueryBase();
            //BulidHOperControl();
            resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

        public void SoftProjectAreaEntityDomain_Domain()
        {
            ModularName = Design_ModularOrFun.ModularName;
            PKField = Design_ModularOrFun.PrimaryKey;// "Comp_CompanyID";
            //PKFields = new List<string> { "Comp_CompanyID" };
            TableName = Design_ModularOrFun.ControllCode;// "Comp_Company";
            TabViewName = Design_ModularOrFun.TabViewName;// "V_Comp_Company"; 
            if (string.IsNullOrEmpty(TabViewName))
                TabViewName = "V_" + TableName;
        }

        public MyResponseBase Execute()
        {
            //SoftProjectAreaEntity LoginInfo = HttpContext.Current.Session["LoginInfo"] as SoftProjectAreaEntity;

            #region 设置创建人、创建时间、更新人、更新时间
            if (LoginInfo != null)
            {
                Item.CreateUserID = LoginInfo.Sys_LoginInfoID;
                Item.CreateUserName = LoginInfo.UserName;

                Item.UpdateUserID = LoginInfo.Sys_LoginInfoID;
                Item.UpdateUserName = LoginInfo.UserName;
            }
            #endregion

            Item.CreateDate = DateTime.Now;
            Item.UpdateDate = DateTime.Now;

            MyResponseBase response = new MyResponseBase();

            if (!RAInfo.bError)
            {
                //数据库存操作
                MyADORepository dal = new MyADORepository(this);
                dal.Execute(response);
            }
            return response;
        }


        #endregion

        public SoftProjectAreaEntity Design_ModularOrFun { get; set; }

        private Querys _Querys;
        public Querys Querys
        {
            get
            {
                return _Querys;
            }
            set
            {
                _Querys = value;
                //var type = Item.GetType();
                //foreach (var item in Querys)
                //{
                //    var FieldName = item.FieldName;
                //    if (item.QuryType == 0)
                //        FieldName = Regex.Split(FieldName, "___", RegexOptions.IgnoreCase)[0];
                //    if (!string.IsNullOrEmpty(item.Value))
                //    {
                //        SetValue(Item, FieldName, item.Value);
                //        //PropertyInfo propertyInfo = type.GetProperty(FieldName);
                //        //propertyInfo.SetValue(Item, item.Value, null);
                //    }
                //}
            }
        }

        /// <summary>
        /// 设置相应属性的值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="fieldValue">属性值</param>
        public static void SetValue(object entity, string fieldName, string fieldValue)
        {
            Type entityType = entity.GetType();

            PropertyInfo propertyInfo = entityType.GetProperty(fieldName);
            if (propertyInfo == null)
                return;
            if (IsType(propertyInfo.PropertyType, "System.String"))
            {
                if (fieldValue == "")
                    fieldValue = "";
                propertyInfo.SetValue(entity, fieldValue, null);
            }
            else if (IsType(propertyInfo.PropertyType, "System.Boolean"))
            {
                propertyInfo.SetValue(entity, Boolean.Parse(fieldValue), null);

            }
            else if (IsType(propertyInfo.PropertyType, "System.Int32"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, int.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, 0, null);

            }
            else if (IsType(propertyInfo.PropertyType, "System.Decimal"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, Decimal.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, new Decimal(0), null);

            }
            else if (IsType(propertyInfo.PropertyType, "System.Nullable`1[System.DateTime]"))
            {
                if (fieldValue != "")
                {
                    try
                    {
                        if (fieldValue == "")
                            propertyInfo.SetValue(entity, null, null);
                        else
                        {
                            var datetemp = DateTime.Now;
                            DateTime.TryParse(fieldValue, out datetemp);
                            propertyInfo.SetValue(
                                entity, datetemp,
                                //(DateTime?)DateTime.ParseExact(fieldValue, "yyyy-MM-dd HH:mm:ss", null),
                            null);
                        }
                    }
                    catch
                    {
                        propertyInfo.SetValue(entity, (DateTime?)DateTime.ParseExact(fieldValue, "yyyy-MM-dd", null), null);
                    }
                }
                else
                    propertyInfo.SetValue(entity, null, null);
            }
            else if (IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Int32]"))
            {
                if (fieldValue == "")
                    propertyInfo.SetValue(entity, null, null);
                else
                {
                    var val = 0;
                    Int32.TryParse(fieldValue, out val);
                    propertyInfo.SetValue(entity, val, null);
                }
            }
        }

        /// <summary>
        /// 类型匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsType(Type type, string typeName)
        {
            if (type.ToString() == typeName)
                return true;
            if (type.ToString() == "System.Object")
                return false;

            return IsType(type.BaseType, typeName);
        }

        public PageQueryBase PageQueryBase
        {
            get;
            set;
        }


        /// <summary>
        /// 功能(英文)
        /// </summary>
        public string FunNameEn { get; set; }

        /// <summary>
        /// 功能(中文)
        /// </summary>
        public string FunNameCn { get; set; }

        /// <summary>
        /// 子查询
        /// </summary>
        public int? SelectSubType { get; set; }

        RespAttachInfo RAInfo { get; set; }

        /// <summary>
        /// 查询是否计算
        /// </summary>
        public int? bCal { get; set; }

        /// <summary>
        /// 页面编码
        /// </summary>
        public string ModularOrFunCode { get; set; }

        public string LogMessage { get; set; }

        #region 功能配置

        SoftProjectAreaEntity _Sys_HOperControl;

        public SoftProjectAreaEntity Sys_HOperControl
        {
            get
            {
                if (_Sys_HOperControl == null)
                {
                    var sys_HOperControls = ProjectCache.Sys_HOperControls.Where(p => p.OperCode == OperCode);
                    if (sys_HOperControls.Count() > 0)
                        _Sys_HOperControl = sys_HOperControls.FirstOrDefault();
                    else
                        _Sys_HOperControl = null;
                }
                return _Sys_HOperControl;
            }
            set
            {
                _Sys_HOperControl = value;
            }
        }

        #endregion

        public string OperCode { get; set; }

        public string ModularName { get; set; }
        public string TableName { get; set; }
        public string TabViewName { get; set; }
        public string PKField { get; set; }

        //public List<string> PKFields { get; set; }
        public string PKOperCode { get; set; }

        public SoftProjectAreaEntity Item { get; set; }

        public List<SoftProjectAreaEntity> Items { get; set; }

        public MyResponseBase Query16(string sql, int DBSelectResultType = 2)
        {
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
            {
                DBTSql = sql,
                DBOperType = 16,
                DBSelectResultType = DBSelectResultType,
            };
            Sys_HOperControl = hOperControl;

            var resp = Execute();
            resp.Querys = Querys;
            if (DBSelectResultType != 4)
                resp.Item = Item;
            return resp;
        }

        public MyResponseBase Query4(int? PKValue)
        {
            string sql = ProjectCache.GetQuerySqlByID(TabViewName, PKField, PKValue);

            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
            {
                DBTSql = sql,
                DBOperType = 16,
                DBSelectResultType = 4,
            };
            Sys_HOperControl = hOperControl;

            var resp = Execute();
            resp.Querys = Querys;
            return resp;
        }

        /// <summary>
        /// 根据主键查询--显示
        /// </summary>
        /// <returns></returns>
        public MyResponseBase ByID()
        {
            SoftProjectAreaEntityDomain_Domain();
            #region 获取主键值
            var PKFieldValue = 0;

            if (string.IsNullOrEmpty(PKField))
                throw new Exception(ModularName + "主键字段名：不能为空！");
            var type = Item.GetType(); //获取类型

            PropertyInfo propertyInfo = type.GetProperty(PKField);
            var PrimaryKeyItem = propertyInfo.GetValue(Item, null);
            if (PrimaryKeyItem == null || PrimaryKeyItem == DBNull.Value)
            {
                throw new Exception(ModularName + "主键值：不能为空！");
            }
            else
                PKFieldValue = Convert.ToInt32(PrimaryKeyItem);
            #endregion

            string sql = ProjectCache.GetQuerySqlByID(TabViewName, PKField, PKFieldValue);

            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
            {
                DBTSql = sql,
                DBOperType = 16,
                DBSelectResultType = 4,
            };
            Sys_HOperControl = hOperControl;

            var resp = Execute();
            resp.Querys = Querys;
            return resp;
        }

        /// <summary>
        /// 根据主键查询--显示
        /// </summary>
        /// <returns></returns>
        public MyResponseBase DeleteByID()
        {
            SoftProjectAreaEntityDomain_Domain();
            #region 获取主键值
            var PKFieldValue = 0;

            if (string.IsNullOrEmpty(PKField))
                throw new Exception(ModularName + "主键字段名：不能为空！");
            var type = Item.GetType(); //获取类型

            PropertyInfo propertyInfo = type.GetProperty(PKField);
            var PrimaryKeyItem = propertyInfo.GetValue(Item, null);
            if (PrimaryKeyItem == null || PrimaryKeyItem == DBNull.Value)
            {
                throw new Exception(ModularName + "主键值：不能为空！");
            }
            else
                PKFieldValue = Convert.ToInt32(PrimaryKeyItem);
            #endregion
            var sql = string.Format("DELETE  {0}  WHERE {1}={2} ", TableName, PKField, PKFieldValue);
            var resp = Query16(sql, 1);
            return resp;
        }


        #region 委托执行
        public MyResponseBase ExecuteDelegate(Action<SoftProjectAreaEntityDomain> DelegateMethod)
        {
            var resp = new MyResponseBase();

            #region (2)编辑
            using (var scope = new TransactionScope())
            {
                try
                {
                    DelegateMethod(null);
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
        #endregion

        #region 添加

        /// <summary>
        /// 参数由配置文件完成
        /// </summary>
        /// <returns></returns>
        public MyResponseBase AddSave()
        {
            var resp = new MyResponseBase();
            SoftProjectAreaEntityDomain_Domain();
            #region (2)添加
            using (var scope = new TransactionScope())
            {
                try
                {
                    var DBFieldVals = Design_ModularOrFun.TSql;
                    resp = AddSave(DBFieldVals);
                    //if (Item.NextVisitDate != null)
                    //{
                    //    var sql = string.Format("Update  Pre_User SET NextVisitDate='{0}' WHERE Pre_UserID={1}", Item.NextVisitDate.Format_yyyy_MM_dd(), Item.Pre_UserID);
                    //    Query16(sql, 1);
                    //}
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
        /// 参数由配置文件完成
        /// </summary>
        /// <returns></returns>
        public MyResponseBase AddSaveNotTran()
        {
            var resp = new MyResponseBase();
            SoftProjectAreaEntityDomain_Domain();
            try
            {
                var DBFieldVals = Design_ModularOrFun.TSql;
                resp = AddSave(DBFieldVals);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }

            return resp;
        }

        public MyResponseBase AddSave(string DBFieldVals)
        {
            var resp = new MyResponseBase();
            var hOperControl = new SoftProjectAreaEntity
            {
                DBOperType = 1,
                DBSelectResultType = 1,
                DBSqlParam = DBFieldVals
            };
            Sys_HOperControl = hOperControl;
            resp = Execute();
            return resp;
        }

        public MyResponseBase AddSaves()
        {
            var resp = new MyResponseBase();
            Sys_HOperControl = null;

            foreach (var item in Items)
            {
                Item = item;
                resp = Execute();
            }
            return resp;
        }

        public MyResponseBase AddSaves(string DBFieldVals)
        {
            var resp = new MyResponseBase();
            var hOperControl = new SoftProjectAreaEntity
            {
                DBOperType = 1,
                DBSelectResultType = 1,
                DBSqlParam = DBFieldVals
            };
            Sys_HOperControl = hOperControl;

            foreach (var item in Items)
            {
                Item = item;
                resp = Execute();
            }
            return resp;
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑单个对象，参数由配置文件完成
        /// </summary>
        /// <param name="DBFieldVals"></param>
        /// <returns></returns>
        public MyResponseBase EditSave()
        {
            MyResponseBase resp = new MyResponseBase();

            SoftProjectAreaEntityDomain_Domain();

            #region 主键检查
            //var PKFieldValue = 0;

            if (string.IsNullOrEmpty(PKField))
                throw new Exception(ModularName + "主键字段名：不能为空！");
            var type = Item.GetType(); //获取类型

            PropertyInfo propertyInfo = type.GetProperty(PKField);
            var PrimaryKeyItem = propertyInfo.GetValue(Item, null);
            if (PrimaryKeyItem == null || PrimaryKeyItem == DBNull.Value)
            {
                throw new Exception(ModularName + "主键值：不能为空！");
            }
            #endregion

            #region (2)编辑
            using (var scope = new TransactionScope())
            {
                try
                {
                    //Comp_Company_Domain();
                    var DBFieldVals = Design_ModularOrFun.TSql;
                    var hOperControl = new SoftProjectAreaEntity
                    {
                        DBOperType = 2,
                        DBSelectResultType = 1,
                        DBSqlParam = DBFieldVals
                    };
                    Sys_HOperControl = hOperControl;
                    resp = Execute();

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
        /// 编辑单个对象，参数由配置文件完成，非事务
        /// </summary>
        /// <param name="DBFieldVals"></param>
        /// <returns></returns>
        public MyResponseBase EditSaveNotTran()
        {
            MyResponseBase resp = new MyResponseBase();

            SoftProjectAreaEntityDomain_Domain();

            #region 主键检查
            //var PKFieldValue = 0;

            if (string.IsNullOrEmpty(PKField))
                throw new Exception(ModularName + "主键字段名：不能为空！");
            var type = Item.GetType(); //获取类型

            PropertyInfo propertyInfo = type.GetProperty(PKField);
            var PrimaryKeyItem = propertyInfo.GetValue(Item, null);
            if (PrimaryKeyItem == null || PrimaryKeyItem == DBNull.Value)
            {
                throw new Exception(ModularName + "主键值：不能为空！");
            }
            #endregion

            #region (2)编辑
            try
            {
                //Comp_Company_Domain();
                var DBFieldVals = Design_ModularOrFun.TSql;
                var hOperControl = new SoftProjectAreaEntity
                {
                    DBOperType = 2,
                    DBSelectResultType = 1,
                    DBSqlParam = DBFieldVals
                };
                Sys_HOperControl = hOperControl;
                resp = Execute();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
            #endregion
            return resp;
        }


        /// <summary>
        /// 编辑单个对象
        /// </summary>
        /// <param name="DBFieldVals"></param>
        /// <returns></returns>
        public MyResponseBase EditSave(string DBFieldVals)
        {
            MyResponseBase resp = new MyResponseBase();
            var hOperControl = new SoftProjectAreaEntity
                    {
                        DBOperType = 2,
                        DBSelectResultType = 1,
                        DBSqlParam = DBFieldVals
                    };
            Sys_HOperControl = hOperControl;
            resp = Execute();
            return resp;
        }

        /// <summary>
        /// sql语句由数据库年提供
        /// </summary>
        /// <returns></returns>
        public MyResponseBase EditSaves()
        {
            MyResponseBase resp = new MyResponseBase();
            Sys_HOperControl = null;

            foreach (var item in Items)
            {
                Item = item;
                resp = Execute();
            }
            return resp;
        }

        /// <summary>
        /// 编辑对象集
        /// </summary>
        /// <param name="DBFieldVals"></param>
        /// <returns></returns>
        public MyResponseBase EditSaves(string DBFieldVals)
        {
            MyResponseBase resp = new MyResponseBase();

            var hOperControl = new SoftProjectAreaEntity
            {
                DBOperType = 2,
                DBSelectResultType = 1,
                DBSqlParam = DBFieldVals
            };
            Sys_HOperControl = hOperControl;

            foreach (var item in Items)
            {
                Item = item;
                resp = Execute();
            }
            return resp;
        }

        #endregion

        /// <summary>
        /// 编辑对象集--后台配置数据库
        /// </summary>
        /// <param name="DBFieldVals"></param>
        /// <returns></returns>
        public MyResponseBase ExecuteEnums(string operCode)
        {
            MyResponseBase resp = new MyResponseBase();
            Sys_HOperControl = null;

            OperCode = operCode;
            foreach (var item in Items)
            {
                Item = item;
                resp = Execute();
            }
            return resp;
        }

        public MyResponseBase ExcuteEnumsNew(int DBOperType=2)
        {
            MyResponseBase resp = new MyResponseBase();

            SoftProjectAreaEntityDomain_Domain();

            #region (2)编辑
            using (var scope = new TransactionScope())
            {
                try
                {
                    var DBFieldVals = Design_ModularOrFun.TSql;
                    var hOperControl = new SoftProjectAreaEntity
                    {
                        DBOperType = DBOperType,
                        DBSelectResultType = 1,
                        DBSqlParam = DBFieldVals
                    };
                    Sys_HOperControl = hOperControl;
                    foreach (var item in Items)
                    {
                        Item = item;
                        resp = Execute();
                    }
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
