
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
    /// 业务层：C_Order(加盟商订单)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public void C_Order_Domain()
        {
            PKField = "C_OrderID";
            TableName = "C_Order";
            TabViewName = "V_C_Order";
        }

        /// <summary>
        /// 主键不为空检查
        /// </summary>
        /// <returns></returns>
        public void C_Order_PKCheck()
        {
            //主键不为空检查
            if (Item.C_OrderID == null)
            {
                throw new Exception("加盟商订单主键不能为空！");
            }
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Order_AddSave()
        {
            var Pre_UserID = Item.Pre_UserID;
            //编辑保存
            var resp = new MyResponseBase();
            Item.OrderAmount = Item.Items.Sum(p => p.CustomerSpecificationsPrice * p.Number);
            var OrderAmount = Item.OrderAmount;
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                C_OrderDetail_AddEditDelete();
                //添加数据库中：用户帐户余额
                var sql = string.Format("UPDATE  Pre_User  SET FundBalance=ISNULL(FundBalance,0)-{0}  WHERE  Pre_UserID={1}", OrderAmount, Pre_UserID);
                Query16(sql, 1);
                //更新缓存：用户账户余额
                var userItem = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(o => o.Pre_UserID == Pre_UserID).FirstOrDefault();
                userItem.FundBalance = (userItem.FundBalance == null ? 0 : userItem.FundBalance) - OrderAmount;
            }));

            return resp;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Order_EditSave()
        {
            if (Item.C_OrderID == null)
                throw new Exception("顾客订单ID：不能为空");
            #region 查询原订单金额
            var sql = string.Format("SELECT * FROM  C_Order  WHERE  C_OrderID={0}", Item.C_OrderID);
            var respOld = Query16(sql, 4);
            #endregion

            //编辑保存
            var resp = new MyResponseBase();
            Item.OrderAmount = Item.Items.Sum(p => p.CustomerSpecificationsPrice * p.Number);
            var OderAmountNew = Item.OrderAmount;
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                C_OrderDetail_AddEditDelete();

                //更新数据库：用户帐户余额
                sql = string.Format("UPDATE  Pre_User  SET FundBalance=ISNULL(FundBalance,0)-{0}+{1}  WHERE  Pre_UserID={2}", OderAmountNew, respOld.Item.OrderAmount, respOld.Item.Pre_UserID);
                Query16(sql, 1);
                //更新缓存：用户账户余额
                var userItem = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(o => o.Pre_UserID == respOld.Item.Pre_UserID).FirstOrDefault();
                var OrderAmount = (userItem.FundBalance != null ? userItem.FundBalance : 0) - OderAmountNew + respOld.Item.OrderAmount;
                userItem.FundBalance = OrderAmount;
            }));

            return resp;
        }

        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Order_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM V_C_Order  Where  Pre_UserID={0} ORDER BY OrderDate DESC", Item.Pre_UserID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
