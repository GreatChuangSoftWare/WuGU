
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
    /// 业务层：O_Order(加盟商订单)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public void O_Order_Domain()
        {
            PKField = "O_OrderID";
            TableName = "O_Order";
            TabViewName = "V_O_Order";
        }

        /// <summary>
        /// 主键不为空检查
        /// </summary>
        /// <returns></returns>
        public void O_Order_PKCheck()
        {
            //主键不为空检查
            if (Item.O_OrderID == null)
            {
                throw new Exception("加盟商订单主键不能为空！");
            }
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_EditSave()
        {
            //编辑保存
            var resp = new MyResponseBase();
            Item.OrderAmount = Item.Items.Sum(p => p.SpecificationsPrice * p.Number);
            //return resp;
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                O_OrderDetail_AddEditDelete();
            }));

            return resp;
        }

        #region 购物车：加入购物车、提交

        public MyResponseBase O_Order_GetShoppingCart()
        {
            var sql = string.Format(";SELECT * FROM [dbo].[V_O_Order] A WHERE OrderStatuID<=2 AND Pre_CompanyID={0} ", LoginInfo.CompanyID);
            var resp = Query16(sql, 4);
            return resp;            
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_AddShoppingCart()
        {
            //获取购物车
            var resp = O_Order_GetShoppingCart();
            //查询商品
            var ProductItem = P_Product_ByID().Item;

            if (resp.Item==null||resp.Item.O_OrderID == null)//无购物车
            {
                #region 添加订单

                Item = new SoftProjectAreaEntity { Pre_CompanyID = LoginInfo.CompanyID, OrderStatuID = 1 };
                Item.OrderAmount = ProductItem.ProductPrice;
                Item.OrderDate = DateTime.Now;
                Item.OrderStatuID = 1;
                ModularOrFunCode = "OrderAreas.O_Order.Add";
                Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                var resptemp=AddSave();
                #endregion

                #region 添加订单明细
                Item = new SoftProjectAreaEntity
                {
                    O_OrderID = resptemp.Item.O_OrderID,
                    P_ProductID = ProductItem.P_ProductID,
                    ProductPrice = ProductItem.ProductPrice,
                    SpecificationsPrice = ProductItem.SpecificationsPrice,
                    Number = 1,
                    PriceTotal = ProductItem.SpecificationsPrice * 1,
                    SalesCategoryID = 1
                };
                ModularOrFunCode = "OrderAreas.O_OrderDetail.Add";
                Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                var resptempx = AddSave();
                #endregion
            }
            else//有购物车
            {
                var sql = string.Format(";SELECT * FROM [dbo].[O_OrderDetail] A WHERE O_OrderID={0} AND P_ProductID={1} ", resp.Item.O_OrderID, Item.P_ProductID);
                var respdetail = Query16(sql, 4);
                if (respdetail.Item.O_OrderDetailID == null)//添加
                {
                    Item = new SoftProjectAreaEntity
                    {
                        O_OrderID = resp.Item.O_OrderID,
                        P_ProductID = ProductItem.P_ProductID,
                        ProductPrice = ProductItem.ProductPrice,
                        SpecificationsPrice = ProductItem.SpecificationsPrice,
                        Number = 1,
                        PriceTotal = ProductItem.SpecificationsPrice * 1,
                        SalesCategoryID = 1
                    };
                    ModularOrFunCode = "OrderAreas.O_OrderDetail.Add";
                    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                    var resptemp = AddSave();
                }
                else//更新记录
                {
                    respdetail.Item.Number += 1;
                    respdetail.Item.PriceTotal = ProductItem.SpecificationsPrice * respdetail.Item.Number;
                    Item = respdetail.Item;
                    ModularOrFunCode = "OrderAreas.O_OrderDetail.Edit";
                    Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                    resp = EditSave();
                }
            }

            return resp;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_Submit()
        {
            //编辑保存
            var resp = new MyResponseBase();
            Item.OrderAmount = Item.Items.Sum(p => p.SpecificationsPrice * p.Number);
            Item.OrderDate = DateTime.Now;
            Item.OrderStatuID = 4;

            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                O_OrderDetail_AddEditDelete();
            }));

            return resp;

            #region 原代码
            //var resp = new MyResponseBase();
            //#region (2)编辑
            //using (var scope = new TransactionScope())
            //{
            //    try
            //    {
            //        O_Order_Domain();
            //        Item.OrderAmount = Item.Items.Sum(p => p.SpecificationsPrice * p.Number);
            //        Item.Pre_CompanyID = LoginInfo.CompanyID;
            //        Item.OrderDate = DateTime.Now;
            //        Item.OrderStatuID = 4;

            //        ModularOrFunCode = "OrderAreas.O_Order.Edit";
            //        Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            //        var resptemp = EditSave();

            //        O_OrderDetail_AddEditDelete();
            //        scope.Complete();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //    finally
            //    {
            //        scope.Dispose();
            //    }
            //}
            //#endregion
            //return resp;
            #endregion
        }

        #endregion

        #region 订单处理

        /// <summary>
        /// 促销商品查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_PromotionProduct()
        {
            O_Order_PKCheck();
            Item.OrderDate = DateTime.Now;
            OperCode = "O_Order.PromotionProduct";
            var resp = Execute();
            return resp;
        }


        /// <summary>
        /// 处理保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_HandleSave()
        {
            //编辑保存
            var resp = new MyResponseBase();
            O_Order_PKCheck();

            #region (2)编辑
            using (var scope = new TransactionScope())
            {
                try
                {
                    O_Order_Domain();
                    Item.OrderAmount = Item.Items.Sum(p => p.SpecificationsPrice * p.Number);
                    Item.OrderHandleDate = DateTime.Now;
                    Item.OrderHandlePersonID = LoginInfo.Sys_LoginInfoID;
                    Item.OrderHandlePerson = LoginInfo.LoginNameCn;
                    //
                    Item.OrderStatuID = 8;
                    OperCode = "O_Order.HandleSave";
                    resp = Execute();

                    O_OrderDetail_AddEditDelete();

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

        #region 公司查询

        #endregion

        /// <summary>
        /// 列表查询:进货排名
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_OrderRanking()
        {
            //列表查询
            var resp = new MyResponseBase();

            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                PageQueryBase.RankInfo = "OrderAmount|0";
            }
            Sys_HOperControl = null;
            ModularOrFunCode = "FranchiseeAreas.O_Order.OrderRanking";
            OperCode = "O_Order.OrderRanking";
            SelectSubType = 6;
            bCal = 1;
            resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            resp.PageQueryBase = PageQueryBase;
            return resp;
        }

        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase O_Order_LastOrder()
        {
            var sql = string.Format("SELECT TOP 1  * FROM O_Order Where  Pre_CompanyID={0} ORDER BY OrderDate DESC", Item.Pre_CompanyID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
