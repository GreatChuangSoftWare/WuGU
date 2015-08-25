
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
    /// 业务层：BC_OrderDetail(订单明细)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {

        public MyResponseBase BC_OrderDetail_AddEditDelete()
        {
            MyResponseBase resp = new MyResponseBase();

            //BC_OrderDetail_Domain();

            #region (2)修改顾客
            using (var scope = new TransactionScope())
            {
                try
                {
                    #region (3)根据功能模块ID查询所有字段
                    var OldItems = BC_OrderDetail_GetByOrderID().Items;
                    #endregion

                    #region (2)模块字段--数据整理

                    Item.Items.ForEach(p =>
                    {
                        p.PriceTotal = p.BCSpecificationsPrice * p.Number;
                        p.BC_OrderID = Item.BC_OrderID;
                    });

                    var deleteIDsEnum = (from p in OldItems select p.BC_OrderDetailID).Except(from o in Item.Items select o.BC_OrderDetailID);
                    var updateItems = Item.Items.Where(p => p.BC_OrderDetailID != null && !deleteIDsEnum.Contains(p.BC_OrderDetailID));
                    var addItems = Item.Items.Where(p => p.BC_OrderDetailID == null);
                    #endregion

                    MyResponseBase resptemp = new MyResponseBase();
                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[BC_OrderDetail] WHERE  BC_OrderDetailID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    var DBFieldVals = "";
                    #region (5)更新模块字段
                    if (updateItems.Count() > 0)
                    {
                        Items = updateItems.ToList();
                        //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        //domain.BC_OrderDetail_Domain();
                        //DBFieldVals = "C_CustomerOrderID,P_ProductID,Numer,Price,PriceTotal";
                        //domain.EditSaves(DBFieldVals);
                        //var operCode = "BC_OrderDetail.EditSave";
                        //ExecuteEnums(operCode);
                        ModularOrFunCode = "PartnerAreas.BC_OrderDetail.Edit";
                        Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
                        ExcuteEnumsNew();
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        //domain.BC_OrderDetail_Domain();
                        //DBFieldVals = "C_CustomerOrderID,P_ProductID,Numer,Price,PriceTotal";
                        //domain.AddSaves(DBFieldVals);
                        //var operCode = "BC_OrderDetail.AddSave";
                        Items = addItems.ToList();
                        ModularOrFunCode = "PartnerAreas.BC_OrderDetail.Add";
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

        public MyResponseBase BC_OrderDetail_GetByOrderID()
        {
            var sql = string.Format(";SELECT * FROM [dbo].[BC_OrderDetail] A WHERE BC_OrderID={0} ", Item.BC_OrderID);
            var resp = Query16(sql, 2);
            return resp;
        }

        #region AutoComplete选中商品

        /// <summary>
        /// 根据页面ID查询页面字段
        /// </summary>
        /// <returns></returns>
        public MyResponseBase BC_OrderDetail_Rows()
        {
            //Item.Fra_ProductPriceIDs
            MyResponseBase resp = new MyResponseBase();
            if (Item.BC_PartnerProductPriceID==null)
            {
                throw new Exception("合作商商品价格ID主键不能为空！");
            }
            string sql = string.Format("SELECT * FROM V_BC_PartnerProductPrice  WHERE   BC_PartnerProductPriceID={0} ", Item.BC_PartnerProductPriceID);
            resp = Query16(sql, 2);

            //清空价格
            //	订单明细ID(tddata)、商品ID(tddata)、数量(text)、单价(只读)、规格价格(隐藏)、总价(标签：后台计算)、
            resp.Items.ForEach(p =>
            {
                p.Number = 1;
                //p.CustomerProductPrice = p.CustomerProductPrice;
                //p.CustomerSpecificationsPrice = p.CustomerSpecificationsPrice;
                //p.BCProductPrice = p.BCProductPrice;
                //p.BCSpecificationsPrice=BCSpecificationsPrice;
                p.PriceTotal = p.Number * p.BCSpecificationsPrice;
            });

            return resp;
        }

        #endregion

    }
}
