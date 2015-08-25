
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
    /// 表：Act_ActivityCostRegulation(活动费用规定)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 活动费用规定ID
        /// </summary>
        public  int?  Act_ActivityCostRegulationID{get;set;}

        /// <summary>
        /// 标题
        /// </summary>
        public  string  ActivityCostRegulationHead{get;set;}

        /// <summary>
        /// 排序号
        /// </summary>
        public  int?  ActivityCostRegulationSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  ActivityCostRegulationStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  ActivityCostRegulationStatuName{get;set;}

        /// <summary>
        /// 内容
        /// </summary>
        public  string  ActivityCostRegulationContext{get;set;}

        public SoftProjectAreaEntity Act_ActivityCostRegulation { get; set; }
        public List<SoftProjectAreaEntity> Act_ActivityCostRegulations { get; set; }
    }
}
