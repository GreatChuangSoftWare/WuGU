
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：O_OrderDetail(订单明细)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public void O_OrderDetail_Domain()
        {
            //构造函数
            PKField = "O_OrderDetailID";
            TableName = "O_OrderDetail";


        }

        /// <summary>
        /// 主键不为空检查
        /// </summary>
        /// <returns></returns>
        public void O_OrderDetail_PKCheck()
        {
            //主键不为空检查
            if (Item.O_OrderDetailID == null)
            {
                throw new Exception("订单明细主键不能为空！");
            }
        }

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_OrderDetail_Index()
        {
            //列表查询
            var resp = new MyResponseBase();

            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                PageQueryBase.RankInfo = "UpdateDate|0";
            }

            Sys_HOperControl = null;
            ModularOrFunCode = "FranchiseeAreas.O_OrderDetail.EditIndex";
            OperCode = "O_OrderDetail.Index";
            SelectSubType = 6;
            bCal = 1;
            resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            resp.PageQueryBase = PageQueryBase;
            resp.FunNameEn = "Edit";
            return resp;
        }

        /// <summary>
        /// 添加保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_OrderDetail_AddSave()
        {
            //添加保存
            var resp = new MyResponseBase();
            #region (2)添加
            using (var scope = new TransactionScope())
            {
                try
                {
                    O_OrderDetail_Domain();
                    //string DBFieldVals = "添加的字段";
                    //resp = AddSave(DBFieldVals);
                    OperCode = "O_OrderDetail.AddSave";
                    Sys_HOperControl = null;
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
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_OrderDetail_EditSave()
        {
            //编辑保存
            var resp = new MyResponseBase();
            O_OrderDetail_PKCheck();

            #region (2)编辑
            using (var scope = new TransactionScope())
            {
                try
                {
                    O_OrderDetail_Domain();
                    //string DBFieldVals = "编辑的字段";
                    //resp = EditSave(DBFieldVals);
                    OperCode = "O_OrderDetail.EditSave";
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
        /// 根据页面ID查询页面字段
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_OrderDetail_ShoppingCart()
        {
            MyResponseBase resp = new MyResponseBase();

            //获取订单
            var sql = string.Format(";SELECT * FROM [dbo].[O_Order] A WHERE OrderStatuID<=2 AND Pre_CompanyID={0} ", LoginInfo.CompanyID);
            var resptemp = Query16(sql, 4);
            Item = resptemp.Item;

            Querys.Add(new Query { QuryType = 0, FieldName = "O_OrderID___equal", Value = Item.O_OrderID.ToString() });

            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                PageQueryBase.RankInfo = "UpdateDate|0";
            }

            Sys_HOperControl = null;
            OperCode = "O_OrderDetail.Index";
            //用于查找计算列
            ModularOrFunCode = "FranchiseeAreas.O_OrderDetail.ShoppingCart";// "FranchiseeAreas.O_OrderDetail.Index";
            SelectSubType = 1;
            bCal = 1;
            resp = Execute();
            resp.Querys = Querys;
            resp.PageQueryBase = PageQueryBase;
            resp.Item = Item;

            if (resp.ItemTotal == null)
                resp.ItemTotal = new SoftProjectAreaEntity { };
            return resp;
        }

        public MyResponseBase O_OrderDetail_AddEditDelete()
        {
            MyResponseBase resp = new MyResponseBase();

            //O_OrderDetail_Domain();

            #region (2)修改顾客
            using (var scope = new TransactionScope())
            {
                try
                {
                    #region (3)根据功能模块ID查询所有字段
                    var OldItems = O_OrderDetail_GetByOrderID().Items;
                    #endregion

                    #region (2)模块字段--数据整理

                    Item.Items.ForEach(p =>
                    {
                        p.PriceTotal = p.SpecificationsPrice * p.Number;
                        p.O_OrderID = Item.O_OrderID;
                    });

                    var deleteIDsEnum = (from p in OldItems select p.O_OrderDetailID).Except(from o in Item.Items select o.O_OrderDetailID);
                    var updateItems = Item.Items.Where(p => p.O_OrderDetailID != null && !deleteIDsEnum.Contains(p.O_OrderDetailID));
                    var addItems = Item.Items.Where(p => p.O_OrderDetailID == null);
                    #endregion

                    MyResponseBase resptemp = new MyResponseBase();
                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[O_OrderDetail] WHERE  O_OrderDetailID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    var DBFieldVals = "";
                    #region (5)更新模块字段
                    if (updateItems.Count() > 0)
                    {
                        Items = updateItems.ToList();
                        //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        //domain.O_OrderDetail_Domain();
                        //DBFieldVals = "C_CustomerOrderID,P_ProductID,Numer,Price,PriceTotal";
                        //domain.EditSaves(DBFieldVals);
                        //var operCode = "O_OrderDetail.EditSave";
                        //ExecuteEnums(operCode);
                        ModularOrFunCode = "OrderAreas.O_OrderDetail.Edit";
                        Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                        ExcuteEnumsNew();
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        //domain.O_OrderDetail_Domain();
                        //DBFieldVals = "C_CustomerOrderID,P_ProductID,Numer,Price,PriceTotal";
                        //domain.AddSaves(DBFieldVals);
                        //var operCode = "O_OrderDetail.AddSave";
                        Items = addItems.ToList();
                        ModularOrFunCode = "OrderAreas.O_OrderDetail.Add";
                        Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                        ExcuteEnumsNew(1);
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

        public MyResponseBase O_OrderDetail_GetByOrderID()
        {
            var sql = string.Format(";SELECT * FROM [dbo].[O_OrderDetail] A WHERE O_OrderID={0} ", Item.O_OrderID);
            var resp = Query16(sql, 2);
            return resp;
        }

        #region 弹窗-选择商品

        //public MyResponseBase O_OrderDetail_Popup(int SelectType = 6)
        //{
        //    var resp = new MyResponseBase();

        //    if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
        //    {
        //        PageQueryBase.RankInfo = "UpdateDate|0";
        //    }
        //    var sql = string.Format(";WITH T1000 AS ( SELECT * FROM [dbo].[V_P_Product] A Where 1=1 ");
        //    sql += " sqlplaceholder )";

        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
        //    {
        //        DBTSql = sql,
        //        DBOperType = 8,//排序分页
        //        SelectSubType = SelectType,
        //        DBSelectResultType = 2,
        //        EqualQueryParam = ""
        //    };
        //    Sys_HOperControl = hOperControl;

        //    resp = Execute();

        //    resp.Querys = Querys;
        //    resp.Item = Item;
        //    return resp;
        //}

        /// <summary>
        /// 根据页面ID查询页面字段
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_OrderDetail_Rows()
        {
            MyResponseBase resp = new MyResponseBase();
            //if (string.IsNullOrEmpty(Item.P_ProductIDs))
            //{
            //    throw new Exception("商品IDs主键不能为空！");
            //}
            if (Item.P_ProductID==null)
            {
                throw new Exception("商品ID主键不能为空！");
            }

            string sql = string.Format("SELECT * FROM V_P_Product  WHERE   P_ProductID={0} ", Item.P_ProductID);
            resp = Query16(sql, 2);

            //清空价格
            //	订单明细ID(tddata)、商品ID(tddata)、数量(text)、单价(只读)、规格价格(隐藏)、总价(标签：后台计算)、
            resp.Items.ForEach(p => { p.SalesCategoryID = 2; p.Number = 1; p.SpecificationsPrice = 0; p.PriceTotal = 0; });
            return resp;
        }

        #endregion
    }
}
