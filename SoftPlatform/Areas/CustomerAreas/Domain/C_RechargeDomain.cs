
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
    /// 业务层：C_Recharge(充值)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Recharge_AddSave()
        {
            if (Item.Pre_UserID == null)
                throw new Exception("ID不能为空");
            //编辑保存
            var resp = new MyResponseBase();
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                //添加数据库中：用户帐户余额
                var sql = string.Format("UPDATE  Pre_User  SET FundBalance=ISNULL(FundBalance,0)+{0}  WHERE  Pre_UserID={1}", Item.Amount, Item.Pre_UserID);
                Query16(sql, 1);
                //更新缓存：用户账户余额
                var userItem = SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(o => o.Pre_UserID == Item.Pre_UserID).FirstOrDefault();
                userItem.FundBalance = (userItem.FundBalance == null ? 0 : userItem.FundBalance )+ Item.Amount;

            }));
            return resp;
        }

        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Recharge_EditSave()
        {
            if (Item.C_RechargeID == null)
                throw new Exception("充值记录ID不能为空");
            #region 查询原记录
            var sql = string.Format("SELECT * FROM C_Recharge  WHERE  C_RechargeID={0}", Item.C_RechargeID);
            var respOld = Query16(sql, 4);
            #endregion

            var resp = new MyResponseBase();
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                //更新数据库：用户帐户余额
                sql = string.Format("UPDATE  Pre_User  SET FundBalance=ISNULL(FundBalance,0)+{0}-{1}  WHERE  Pre_UserID={2}", Item.Amount,respOld.Item.Amount, respOld.Item.Pre_UserID);
                Query16(sql, 1);
                //更新缓存：用户账户余额
                var userItem=SoftProjectAreaEntityDomain.Pre_UserRoleAll.Where(o => o.Pre_UserID == respOld.Item.Pre_UserID).FirstOrDefault();
                userItem.FundBalance = (userItem.FundBalance!=null? userItem.FundBalance:0)+ Item.Amount - respOld.Item.Amount;
            }));

            return resp;
        }

        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Recharge_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM V_C_Recharge  Where  Pre_UserID={0} ORDER BY RechargeDate DESC", Item.Pre_UserID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
