
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
    /// 业务层：Design_ModularOrFunBtnDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunBtn_Domain()
        {
            PKField = "Design_ModularOrFunBtnID";
            //PKFields = new List<string> { "Design_ModularOrFunBtnID" };
            TableName = "Design_ModularOrFunBtn";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunBtn_PKCheck()
        {
            if (Item.Design_ModularOrFunBtnID == null)
            {
                throw new Exception("功能模块操作主键不能为空！");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunBtn_GetByID()
        {
            Design_ModularOrFunBtn_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunBtn] A WHERE Design_ModularOrFunBtnID={0} ", Item.Design_ModularOrFunBtnID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 权限管理系统--缓存：获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunBtn_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            //string sql = "SELECT * FROM V_Design_ModularOrFunBtn Order By Design_ModularOrFunParentID";
            //V_Design_ModularOrFunBtnControll0510
            string sql = "SELECT * FROM V_Design_ModularOrFunBtn0510 ";

            var resp = Query16(sql,2);
            return resp.Items;
        }

        /// <summary>
        /// 根据功能模块ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunBtn_GetByModularOrFunID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }

            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunBtn] A WHERE Design_ModularOrFunID={0} Order By Sort", Item.Design_ModularOrFunID);
            var resp = Query16(sql);

            var Design_ModularOrFun = Design_ModularOrFun_GetByID();
            resp.Item = Design_ModularOrFun.Item;
            //此处借助Design_ModularOrFunParentID作为返回参数
            resp.Item.Design_ModularOrFunParentID = Item.Design_ModularOrFunParentID;
            return resp;
        }

        public MyResponseBase Design_ModularOrFunBtn_EditListSave()
        {
            Design_ModularOrFunBtn_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularOrFunBtn_GetByModularOrFunID();
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Design_ModularOrFunBtns.ForEach(p =>
                    { p.Design_ModularOrFunID = Item.Design_ModularOrFunID; });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunBtnID).Except(from o in Item.Design_ModularOrFunBtns select o.Design_ModularOrFunBtnID);
                    var updateItems = Item.Design_ModularOrFunBtns.Where(p => p.Design_ModularOrFunBtnID != null);
                    var addItems = Item.Design_ModularOrFunBtns.Where(p => p.Design_ModularOrFunBtnID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFunBtn] WHERE  Design_ModularOrFunBtnID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFunBtn_Domain();
                        var DBFieldVals = "Sort,Design_ModularOrFunID,BtnNameEn,BtnNameCn,OperPos,BtnBehavior,PopupAfterTableFun,popupaddrepeat,PopupWidth,DispConditionsExpression,bValid,BtnType,ModularOrFunBtnRemark";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFunBtn_Domain();
                        var DBFieldVals = "Sort,Design_ModularOrFunID,BtnNameEn,BtnNameCn,OperPos,BtnBehavior,PopupAfterTableFun,popupaddrepeat,PopupWidth,DispConditionsExpression,bValid,BtnType,ModularOrFunBtnRemark";
                        domain.AddSaves(DBFieldVals);
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
    }
}
