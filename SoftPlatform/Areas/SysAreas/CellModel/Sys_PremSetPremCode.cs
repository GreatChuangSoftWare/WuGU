using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：Sys_PremSetPremCode(权限集-权限码关联列表)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        #region 权限集-权限码

        /// <summary>
        /// 权限集-权限码ID
        /// </summary>
        public string Sys_PremSetPremCodeID { get; set; }

        ///// <summary>
        ///// 权限集ID
        ///// </summary>
        //public int? Sys_PermSetID{get;set;}

        ///// <summary>
        ///// 权限码ID
        ///// </summary>
        //public int? Sys_PremCodeID { get; set; }

        /// <summary>
        /// 是否为菜单Action
        /// </summary>
        public int? bMenuAction { get; set; }

        #endregion

        /// <summary>
        /// 权限集-权限码关联列表
        /// </summary>
        public List<SoftProjectAreaEntity> Sys_PremSetPremCodes { get; set; }

    }
}
