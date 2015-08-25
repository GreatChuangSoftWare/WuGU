
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
    /// 业务层：Design_ModularOrFunSqlDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunSql_Domain()
        {
            PKField = "Design_ModularOrFunSqlID";
            //PKFields = new List<string> { "Design_ModularOrFunSqlID" };
            TableName = "Design_ModularOrFunSql";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunSql_PKCheck()
        {
            if (Item.Design_ModularOrFunSqlID == null)
            {
                throw new Exception("DBSql主键不能为空！");
            }
        }

        public MyResponseBase Design_ModularOrFunSql_GetByID()
        {
            Design_ModularOrFunSql_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunSql] A WHERE Design_ModularOrFunSqlID={0} ", Item.Design_ModularOrFunSqlID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunSql_GetAll()
        {
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunSql] A ");
            var resp = Query16(sql);
            return resp;
        }

        /// <summary>
        /// 根据功能模块ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunSql_GetByModularOrFunID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunSql] A WHERE Design_ModularOrFunID={0} ORDER BY Sort", Item.Design_ModularOrFunID);
            var resp = Query16(sql);
            return resp;
        }

        public MyResponseBase Design_ModularOrFunSql_EditListSave()
        {
            Design_ModularOrFunSql_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularOrFunSql_GetByModularOrFunID();
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Items.ForEach(p =>
                    { p.Design_ModularOrFunID = Item.Design_ModularOrFunID; });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunSqlID).Except(from o in Item.Items select o.Design_ModularOrFunSqlID);
                    var updateItems = Item.Items.Where(p => p.Design_ModularOrFunSqlID != null);
                    var addItems = Item.Items.Where(p => p.Design_ModularOrFunSqlID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFunSql] WHERE  Design_ModularOrFunSqlID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFunSql_Domain();
                        var DBFieldVals = "SqlNameCn,OperName,OperCode,DBOperType,DBTSql,SelectSubType,DBSelectResultType,DBSqlParam,DBFieldVals,DefaultSort,FieldDesc,Sort,Design_ModularOrFunID";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFunSql_Domain();
                        var DBFieldVals = "SqlNameCn,OperName,OperCode,DBOperType,DBTSql,SelectSubType,DBSelectResultType,DBSqlParam,DBFieldVals,DefaultSort,FieldDesc,Sort,Design_ModularOrFunID";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

                    ProjectCache.ClearHOperControls();
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

        public MyResponseBase Design_ModularOrFunSql_BulidTSql()
        {
            Design_ModularOrFunSql_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            #region 获取实体字段
            var Design_ModularFields = Design_ModularField_GetByModularOrFunID().Items;
            var Fields = Design_ModularFields.Where(p => ((int)p.FieldTypeID & 1) == 1 && p.bPrimaryKeyOrFK != 1).Select(p => p.name).ToList();
            var tableFields = string.Join(",", Fields);//deleteForecastIDsEnum.ToArray()

            #endregion

            #region 生成添加
            var addItem = new SoftProjectAreaEntity
            {
                OperName = Design_ModularOrFun.ModularName + "--添加保存",
                OperCode = Design_ModularOrFun.ControllCode + ".AddSave",
                DBOperType = 1,
                DBTSql = null,
                SelectSubType = null,
                DBSelectResultType = 1,
                DBSqlParam = tableFields,
                DefaultSort = null,
                Sort = 1,
                Design_ModularOrFunID = Item.Design_ModularOrFunID
            };
            #endregion

            #region 生成编辑
            var editItem = new SoftProjectAreaEntity
            {
                OperName = Design_ModularOrFun.ModularName + "--编辑保存",
                OperCode = Design_ModularOrFun.ControllCode + ".EditSave",
                DBOperType = 2,
                DBTSql = null,
                SelectSubType = null,
                DBSelectResultType = 1,
                DBSqlParam = tableFields,
                DefaultSort = null,
                Sort = 3,
                Design_ModularOrFunID = Item.Design_ModularOrFunID
            };
            #endregion

            #region 生成查询

            StringBuilder sbselect = new StringBuilder();
            sbselect.AppendLine(";WITH T1000 AS ");
            sbselect.AppendLine("(");
            sbselect.AppendLine("	SELECT * ");
            sbselect.AppendLine(string.Format("	FROM [dbo].[{0}] A  ", Design_ModularOrFun.ControllCode));
            sbselect.AppendLine("	WHERE 1=1  ");
            sbselect.AppendLine("	sqlplaceholder ");
            sbselect.AppendLine(")");

            var selectItem = new SoftProjectAreaEntity
            {
                OperName = Design_ModularOrFun.ModularName + "--查询",
                OperCode = Design_ModularOrFun.ControllCode + ".Index",
                DBOperType = 8,
                DBTSql = sbselect.ToString(),
                SelectSubType = 6,//分页排序
                DBSelectResultType = 2,
                DBSqlParam = "",
                DefaultSort = "Update|0",
                Sort = 5,
                Design_ModularOrFunID = Item.Design_ModularOrFunID
            };

            #endregion

            #region 根据ID查询

            StringBuilder sbselectByID = new StringBuilder();
            sbselectByID.AppendLine("SELECT * ");
            sbselectByID.AppendLine(string.Format("FROM [dbo].[{0}] A ", Design_ModularOrFun.ControllCode));
            sbselectByID.AppendLine(string.Format("WHERE {0}=@{0} ", Design_ModularOrFun.ControllCode+"ID"));

            var ByIDItem = new SoftProjectAreaEntity
            {
                OperName = Design_ModularOrFun.ModularName + "--根据ID查询",
                OperCode = Design_ModularOrFun.ControllCode + ".ByID",
                DBOperType = 16,
                DBTSql = sbselectByID.ToString(),
                SelectSubType = null,//分页排序
                DBSelectResultType = 4,
                DBSqlParam = "@" + Design_ModularOrFun.ControllCode+"ID",
                DefaultSort = "",
                Sort = 7,
                Design_ModularOrFunID = Item.Design_ModularOrFunID
            };

            #endregion

            Item.Items.Clear();
            Item.Items.Add(addItem);
            Item.Items.Add(editItem);
            Item.Items.Add(selectItem);
            Item.Items.Add(ByIDItem);
            var resp = Design_ModularOrFunSql_EditListSave();
            return resp;
        }
    }
}
