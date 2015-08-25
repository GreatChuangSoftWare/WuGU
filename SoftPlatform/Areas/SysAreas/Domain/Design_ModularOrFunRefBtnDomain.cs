
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using Framework.Web.Mvc;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Design_ModularOrFunRefBtnDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunRefBtn_Domain()
        {
            PKField = "Design_ModularOrFunRefBtnID";
            //PKFields = new List<string> { "Design_ModularOrFunRefBtnID" };
            TableName = "Design_ModularOrFunRefBtn";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunRefBtn_PKCheck()
        {
            if (Item.Design_ModularOrFunRefBtnID == null)
            {
                throw new Exception("功能模块字段主键不能为空！");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunRefBtn_GetByID()
        {
            Design_ModularOrFunRefBtn_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunRefBtn] A WHERE Design_ModularOrFunRefBtnID={0} ", Item.Design_ModularOrFunRefBtnID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 权限管理系统--缓存：获取所有权限按钮
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunRefBtn_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            //string sql = "SELECT * FROM V_Design_ModularOrFunBtn Order By Design_ModularOrFunParentID";
            //V_Design_ModularOrFunBtnControll0510
            string sql = "SELECT * FROM V_Design_ModularOrFunRefBtn ";

            var resp = Query16(sql, 2);
            return resp.Items;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunRefBtn_GetByPremSetID()
        {
            if (Item.Design_PremSetID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
            var sql = string.Format(";SELECT * FROM [dbo].[V_Design_ModularOrFunRefBtn] A WHERE Design_PremSetID={0} Order  By  PremSetBtnSort", Item.Design_PremSetID);
            var resp = Query16(sql);
            sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE Design_ModularOrFunID={0}", Item.Design_PremSetID);
            var resptemp = Query16(sql, 4);
            resp.Item = resptemp.Item;
            resp.Item.Design_ModularOrFunID = Item.Design_ModularOrFunID;
            resp.Item.Design_PremSetID = Item.Design_PremSetID;
            return resp;
        }

        public MyResponseBase Design_ModularOrFunRefBtn_EditListSave()
        {
            Design_ModularOrFunRefBtn_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularOrFunRefBtn_GetByPremSetID();
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Items.ForEach(p =>
                    { p.Design_PremSetID = Item.Design_PremSetID; });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunRefBtnID).Except(from o in Item.Items select o.Design_ModularOrFunRefBtnID);//.ToList();
                    var updateItems = Item.Items.Where(p => p.Design_ModularOrFunRefBtnID != null && !deleteIDsEnum.Contains(p.Design_ModularOrFunRefBtnID));
                    var addItems = Item.Items.Where(p => p.Design_ModularOrFunRefBtnID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFunRefBtn] WHERE  Design_ModularOrFunRefBtnID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFunRefBtn_Domain();
                        var DBFieldVals = "Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFunRefBtn_Domain();
                        var DBFieldVals = "Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

                    scope.Complete();
                    ProjectCache.ClearDesign_ModularOrFunRefBtns();
                    //ProjectCache.Sys_PremSetsAll_Clear();
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

        public MyResponseBase Design_ModularOrFunRefBtn_Popup()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }

            #region 功能模块
            StringBuilder sbsql1 = new StringBuilder();
            sbsql1.AppendLine("");
            sbsql1.AppendLine("SELECT DISTINCT A.Design_ModularOrFunID,ModularName ");
            sbsql1.AppendLine("FROM Design_ModularOrFun A");
            sbsql1.AppendLine("WHERE GroupModularOrFun=2");

            var resp1 = Query16(sbsql1.ToString());
            if (Item.Design_ModularOrFunID == null)
                Item.Design_ModularOrFunID = resp1.Items.First().Design_ModularOrFunID;

            #endregion

            #region 功能模块下的页面
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS ");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunID={0}");
            sb.AppendLine("	UNION ALL");
            sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunParentID={0} AND GroupModularOrFun={1}");
            sb.AppendLine(")");
            sb.AppendLine("SELECT * FROM T0 ORDER BY Sort");
            var sql = sb.ToString();
            sql = string.Format(sql, Item.Design_ModularOrFunID, "3");
            var resp2 = Query16(sql);

            //var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE  GroupModularOrFun=3 AND Design_ModularOrFunParentID={0} ", Item.Design_ModularOrFunID);
            //var resp2 = Query16(sql);
            //var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunControll] A WHERE Design_ModularOrFunID={0} ", Item.Design_ModularOrFunID);
            //if (Item.Design_ModularOrFunBtnIDs != null)
            //    sql += string.Format(" AND Design_ModularOrFunControllID NOT IN({0})", Item.Design_ModularOrFunControllIDs);
            //var resp = Query16(sql);
            #endregion

            #region 功能模块下的页面

            if (Item.SubDesign_ModularOrFunID == null)
                Item.SubDesign_ModularOrFunID = resp2.Items.FirstOrDefault().Design_ModularOrFunID;

            sql = string.Format(";SELECT A.* FROM [dbo].[Design_ModularOrFunBtn] A ");
            //sql+="JOIN Design_ModularOrFun B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID";
            sql += string.Format(" WHERE [bValid]=1 AND Design_ModularOrFunID={0} ", Item.SubDesign_ModularOrFunID);
            if (Item.Design_ModularOrFunBtnIDs != null)
                sql += string.Format(" AND Design_ModularOrFunBtnID NOT IN({0})", Item.Design_ModularOrFunBtnIDs);
            var resp = Query16(sql);
            #endregion

            resp.Item.Design_ModularOrFuns = resp1.Items;
            resp.Item.SubDesign_ModularOrFuns = resp2.Items;
            return resp;
        }

        public MyResponseBase Design_ModularOrFunRefBtn_Rows()
        {
            MyResponseBase resp = new MyResponseBase();

            string sql = string.Format("SELECT B.ModularName,A.* FROM Design_ModularOrFunBtn A ");
            sql += " JOIN Design_ModularOrFun  B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID";
            sql += string.Format(" WHERE  Design_ModularOrFunBtnID IN({0}) ", Item.Design_ModularOrFunBtnIDs);
            resp = Query16(sql, 2);

            ////(2)查询模块编码字段
            //var ModularFields = Design_ModularField_GetModularPageOrQueryField(Item.Design_ModularOrFunID);
            //resp.Item.Design_ModularFields = ModularFields;
            return resp;
        }


        public MyResponseBase GetPageByModularOrFunParentID()
        {
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE  GroupModularOrFun=3 AND Design_ModularOrFunParentID={0} ", Item.Design_ModularOrFunID);
            var resp2 = Query16(sql);
            return resp2;
        }
    }
}
