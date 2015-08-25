
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
    /// 业务层：Pr_Notice(活动公告管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        
        /// <summary>
        /// 根据主键查询--本月促销
        /// </summary>
        /// <returns></returns>
        public static MyResponseBase Pr_Notice_ByCurrMonth()
        {
            var domain = new SoftProjectAreaEntityDomain();
            domain.Sys_HOperControl = null;
            string sql = string.Format("SELECT * FROM Pr_Notice WHERE YEAR(PromotionStartDate)=YEAR(GETDATE()) AND MONTH(PromotionStartDate)=MONTH(getdate()) ORDER BY PromotionStartDate DESC ");
            var resp = domain.Query16(sql);
            return resp;
        }

        ///// <summary>
        ///// 根据主键查询--本月促销
        ///// </summary>
        ///// <returns></returns>
        //public static List<SoftProjectAreaEntity> Pr_Notice_ByHome()
        //{
        //    var domain = new SoftProjectAreaEntityDomain();
        //    domain.Sys_HOperControl = null;
        //    string sql = string.Format("SELECT TOP 10 * FROM Pr_Notice WHERE YEAR(PromotionStartDate)=YEAR(GETDATE()) AND MONTH(PromotionStartDate)=MONTH(getdate()) ORDER BY PromotionStartDate DESC ");
        //    var resp = domain.Query16(sql);
        //    return resp.Items;
        //}

        
    }
}
