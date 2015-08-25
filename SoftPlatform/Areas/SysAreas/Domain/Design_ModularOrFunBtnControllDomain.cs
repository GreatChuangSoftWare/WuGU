
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Design_ModularOrFunBtnControllDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunBtnControll_Domain()
        {
            PKField = "Design_ModularOrFunBtnControllID";
            //PKFields = new List<string> { "Design_ModularOrFunBtnControllID" };
            TableName = "Design_ModularOrFunBtnControll";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunBtnControll_PKCheck()
        {
            if (Item.Design_ModularOrFunBtnControllID == null)
            {
                throw new Exception("功能模块字段主键不能为空！");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunBtnControll_GetByID()
        {
            Design_ModularOrFunBtnControll_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunBtnControll] A WHERE Design_ModularOrFunBtnControllID={0} ", Item.Design_ModularOrFunBtnControllID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 权限管理系统--缓存：获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunBtnControll_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            string sql = "SELECT * FROM V_Design_ModularOrFunBtnControll Order By Sort";
            var resp = Query16(sql, 2);
            return resp.Items;
        }

        /// <summary>
        /// 根据功能模块ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunBtnControll_GetByModularOrFunBtnID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
            var sql = string.Format(";SELECT * FROM [dbo].[V_Design_ModularOrFunBtnControll] A WHERE Design_ModularOrFunBtnID={0} Order By  Sort", Item.Design_ModularOrFunBtnID);
            var resp = Query16(sql);
            sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunBtn] A WHERE Design_ModularOrFunBtnID={0} Order By  Sort", Item.Design_ModularOrFunBtnID);
            var resptemp = Query16(sql, 4);
            resp.Item = resptemp.Item;
//Item.Design_ModularOrFunBtnID=149&Item.Design_ModularOrFunID=113&Item.Design_ModularOrFunParentID=107
            resp.Item.Design_ModularOrFunParentID = Item.Design_ModularOrFunParentID;
            return resp;
        }

        public MyResponseBase Design_ModularOrFunBtnControll_EditListSave()
        {
            Design_ModularOrFunBtnControll_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularOrFunBtnControll_GetByModularOrFunBtnID();
                    #endregion

                    #region (2)模块字段--数据整理
                    if (Item.Design_ModularOrFunBtnControlls == null)
                        Item.Design_ModularOrFunBtnControlls = new List<SoftProjectAreaEntity>();
                    Item.Design_ModularOrFunBtnControlls.ForEach(p =>
                    { p.Design_ModularOrFunBtnID = Item.Design_ModularOrFunBtnID; });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunBtnControllID).Except(from o in Item.Design_ModularOrFunBtnControlls select o.Design_ModularOrFunBtnControllID);
                    var updateItems = Item.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnControllID != null);
                    var addItems = Item.Design_ModularOrFunBtnControlls.Where(p => p.Design_ModularOrFunBtnControllID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFunBtnControll] WHERE  Design_ModularOrFunBtnControllID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFunBtnControll_Domain();
                        var DBFieldVals = "Design_ModularOrFunBtnID,Design_ModularOrFunControllID,Sort";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFunBtnControll_Domain();
                        var DBFieldVals = "Design_ModularOrFunBtnID,Design_ModularOrFunControllID,Sort";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

                    scope.Complete();
                    ProjectCache.Design_ModularOrFunBtnControlls_Clear();
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

        public MyResponseBase Design_ModularOrFunBtnControll_Rows()
        {
            MyResponseBase resp = new MyResponseBase();

            string sql = string.Format("SELECT * FROM Design_ModularOrFunControll WHERE  Design_ModularOrFunControllID IN({0}) ", Item.Design_ModularOrFunControllIDs);
            resp = Query16(sql, 2);

            ////(2)查询模块编码字段
            //var ModularFields = Design_ModularField_GetModularPageOrQueryField(Item.Design_ModularOrFunID);
            //resp.Item.Design_ModularFields = ModularFields;
            return resp;
        }

    }
}
