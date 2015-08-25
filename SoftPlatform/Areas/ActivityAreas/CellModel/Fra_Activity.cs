
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
    /// 表：Fra_Activity(活动管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public  int?  Fra_ActivityID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 活动状态ID
        /// </summary>
        public  int?  ActivityStatuID{get;set;}

        /// <summary>
        /// 活动状态
        /// </summary>
        public  string  ActivityStatuName{get;set;}

        /// <summary>
        /// 活动名称
        /// </summary>
        public  string  ActivityTitle{get;set;}

        /// <summary>
        /// 开始日期
        /// </summary>
        public  DateTime?  ActivityStartDate{get;set;}

        /// <summary>
        /// 结束日期
        /// </summary>
        public  DateTime?  ActivityEndDate{get;set;}

        /// <summary>
        /// 天数
        /// </summary>
        public  int?  DayLen{get;set;}

        /// <summary>
        /// 申请专家
        /// </summary>
        public  string  ApplicationSpecialist{get;set;}

        /// <summary>
        /// 申请日期
        /// </summary>
        public  DateTime?  ApplyDate{get;set;}

        /// <summary>
        /// 申请人ID
        /// </summary>
        public  int?  ApplyPersonID{get;set;}

        /// <summary>
        /// 申请人
        /// </summary>
        public  string  ApplyPerson{get;set;}

        /// <summary>
        /// 区域经理ID
        /// </summary>
        public  int?  RegionalManagerID{get;set;}

        /// <summary>
        /// 区域经理
        /// </summary>
        public  string  RegionalManager{get;set;}

        /// <summary>
        /// 审核日期
        /// </summary>
        public  DateTime?  ActivityAuditDate{get;set;}

        /// <summary>
        /// 活动流程ID
        /// </summary>
        //public  int?  Act_ActivityFlowID{get;set;}

        /// <summary>
        /// 活动调研模板ID
        /// </summary>
        //public  int?  Act_SurveyTemplateID{get;set;}

        /// <summary>
        /// 自我调研表
        /// </summary>
        public  string  ActivitySelfAtudyTable{get;set;}

        /// <summary>
        /// 活动总结
        /// </summary>
        public  string  ActivityConclusion{get;set;}

        public SoftProjectAreaEntity Fra_Activity { get; set; }
        public List<SoftProjectAreaEntity> Fra_Activitys { get; set; }
    }
}
