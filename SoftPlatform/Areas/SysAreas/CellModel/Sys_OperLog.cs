using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    public partial class SoftProjectAreaEntity// : EntityBase
    {

        /// <summary>
        /// 操作日志ID
        /// </summary>
        public int? Sys_OperLogID { get; set; }

        ///// <summary>
        ///// 登录类别
        ///// </summary>
        //public int? LoginCategory { get; set; }

        /// <summary>
        /// 类别标识英文
        /// </summary>
        public string OperLogCategoryIdentEn { get; set; }

        /// <summary>
        /// 类别标识中文
        /// </summary>
        public string OperLogCategoryIdentCn { get; set; }

        /// <summary>
        /// 类别标识主键
        /// </summary>
        public int? OperLogIdent { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; }
    }
}
