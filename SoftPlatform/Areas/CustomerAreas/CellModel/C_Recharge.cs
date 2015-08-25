
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：C_Recharge(顾客充值)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 充值ID
        /// </summary>
        public  int?  C_RechargeID{get;set;}

        /// <summary>
        /// 顾客ID
        /// </summary>
        //public  int?  Pre_UserID{get;set;}

        /// <summary>
        /// 充值日期
        /// </summary>
        public  DateTime?  RechargeDate{get;set;}

        /// <summary>
        /// 充值金额
        /// </summary>
        public  decimal?  Amount{get;set;}

        /// <summary>
        /// 充值人
        /// </summary>
        public  string  RechargePerson{get;set;}

        public SoftProjectAreaEntity C_Recharge { get; set; }
        public List<SoftProjectAreaEntity> C_Recharges { get; set; }
    }
}
