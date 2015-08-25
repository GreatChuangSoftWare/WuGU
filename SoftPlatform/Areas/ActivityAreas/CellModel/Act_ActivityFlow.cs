
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
    /// 表：Act_ActivityFlow(活动流程模板)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 活动流程ID
        /// </summary>
        public  int?  Act_ActivityFlowID{get;set;}

        /// <summary>
        /// 标题
        /// </summary>
        public  string  ActivityFlowTitle{get;set;}

        /// <summary>
        /// 排序号
        /// </summary>
        public  int?  ActivityFlowSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  ActivityFlowStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  ActivityFlowStatuName{get;set;}

        /// <summary>
        /// 内容
        /// </summary>
        public  string  ActivityFlowContext{get;set;}

        public SoftProjectAreaEntity Act_ActivityFlow { get; set; }
        public List<SoftProjectAreaEntity> Act_ActivityFlows { get; set; }
    }
}
