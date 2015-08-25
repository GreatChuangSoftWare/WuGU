
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
    /// 业务层：C_Visit(回访)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Visit_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM V_C_Visit  Where  Pre_UserID={0} ORDER BY VisitDate DESC", Item.Pre_UserID);
            var resp = Query16(sql, 4);
            return resp;
        }

        /// <summary>
        /// 参数由配置文件完成
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_Visit_AddSave()
        {
            var resp = new MyResponseBase();

            resp = ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                if (Item.NextVisitDate != null)
                {
                    var sql = string.Format("Update  Pre_User SET NextVisitDate='{0}' WHERE Pre_UserID={1}", Item.NextVisitDate.Format_yyyy_MM_dd(), Item.Pre_UserID);
                    Query16(sql, 1);
                }
            }));
            return resp;
        }

    }
}
