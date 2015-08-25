
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
    /// 业务层：C_ExpertGuidance(订单)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 最后1个订单
        /// </summary>
        /// <returns></returns>
        public MyResponseBase C_ExpertGuidance_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM V_C_ExpertGuidance  Where  Pre_UserID={0} ORDER BY ExpertGuidanceDate DESC", Item.Pre_UserID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
