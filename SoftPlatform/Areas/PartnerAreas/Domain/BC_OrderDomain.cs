
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
    /// 业务层：BC_Order(合作商订单)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public void BC_Order_Domain()
        {
            PKField = "BC_OrderID";
            TableName = "BC_Order";
            TabViewName = "V_BC_Order";
        }

        /// <summary>
        /// 主键不为空检查
        /// </summary>
        /// <returns></returns>
        public void BC_Order_PKCheck()
        {
            //主键不为空检查
            if (Item.BC_OrderID == null)
            {
                throw new Exception("合作商订单主键不能为空！");
            }
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase BC_Order_AddSave()
        {
            //编辑保存
            var resp = new MyResponseBase();
            Item.OrderAmount = Item.Items.Sum(p => p.BCSpecificationsPrice * p.Number);

            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                BC_OrderDetail_AddEditDelete();
            }));

            return resp;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase BC_Order_EditSave()
        {
            //编辑保存
            var resp = new MyResponseBase();
            Item.OrderAmount = Item.Items.Sum(p => p.BCSpecificationsPrice * p.Number);

            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                BC_OrderDetail_AddEditDelete();
            }));

            return resp;
        }

        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase BC_Order_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM V_BC_Order  Where  Pre_UserID={0} ORDER BY OrderDate DESC", Item.Pre_UserID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
