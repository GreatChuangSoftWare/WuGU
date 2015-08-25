
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Framework.Web.Mvc.Authorization
namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：Ba_Area(地区)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 地区ID
        /// </summary>
        public int? Ba_AreaID{ get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public string AreaCode{ get; set; }

        ///// <summary>
        ///// 地区名称
        ///// </summary>
        //public string AreaName{ get; set; }

        /// <summary>
        /// 父地区编码
        /// </summary>
        public string AreaParentCode { get; set; }

        public SoftProjectAreaEntity Ba_Area { get; set; }

        public List<SoftProjectAreaEntity> Ba_Areas { get; set; }
    }
}
