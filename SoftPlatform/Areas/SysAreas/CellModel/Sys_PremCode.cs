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
    /// 表:Sys_PremCode(权限码)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        #region 权限码

        /// <summary>
        /// 权限码ID
        /// </summary>
        public int? Sys_PremCodeID { get; set; }

        /// <summary>
        /// 权限码名称
        /// </summary>
        public string Sys_PremCodeName { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        public string PremCode { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器名
        /// </summary>
        public string ConstrollName { get; set; }

        ///// <summary>
        ///// Action名
        ///// </summary>
        //public string ActionName { get; set; }

        ///// <summary>
        ///// 权限路径
        ///// </summary>
        //public string ActionPath { get; set; }

        ///// <summary>
        ///// 是否验证
        ///// </summary>
        //public int? bValidate { get; set; }

        #endregion
        
        /// <summary>
        /// 权限码列表
        /// </summary>
        public List<SoftProjectAreaEntity> Sys_PremCodes { get; set; }
    }
}
