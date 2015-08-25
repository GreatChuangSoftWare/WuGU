using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    public partial class SoftProjectAreaEntity 
    {
        /// <summary>
        /// 操作日志明细ID
        /// </summary>
        public int? Sys_OperLogDetailID { get; set; }

        ///// <summary>
        ///// 登录类型：1：公司，2：加盟商
        ///// </summary>
        //public int? LoginCategoryID { get; set; }

        /// <summary>
        /// 日志类别ID
        /// </summary>
        public int? LogCategoryID { get; set; }

        /// <summary>
        /// 日志类别名称
        /// </summary>
        public string LogCategoryName { get; set; }


        ///// <summary>
        ///// 操作ID
        ///// </summary>
        //public int? Sys_OperLogID { get; set; }



        ///// <summary>
        ///// 操作标识
        ///// </summary>
        //public int? OperLogIdent { get; set; }

        ///// <summary>
        ///// 操作名称
        ///// </summary>
        //public string OperName { get; set; }

        public DateTime? OperDate { get; set; }

        public int? LogPersonID { get; set; }

        public string LogPerson { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string DetailDescription { get; set; }

    }
}
