
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
    /// 表：CT_CollocationTips(搭配要诀)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 搭配要诀ID
        /// </summary>
        public  int?  CT_CollocationTipsID{get;set;}

        /// <summary>
        /// 列1
        /// </summary>
        public  string  CollocationTipsCol1{get;set;}

        /// <summary>
        /// 列3
        /// </summary>
        public  string  CollocationTipsCol2{get;set;}

        public SoftProjectAreaEntity CT_CollocationTips { get; set; }
        public List<SoftProjectAreaEntity> CT_CollocationTipss { get; set; }
    }
}
