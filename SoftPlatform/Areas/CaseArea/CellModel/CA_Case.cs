
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
    /// 表：CA_Case(案例管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 案例ID
        /// </summary>
        public  int?  CA_CaseID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 案例类别
        /// </summary>
        //public  int?  CA_CaseCategoryID{get;set;}

        /// <summary>
        /// 案例名称
        /// </summary>
        public  string  CaseName{get;set;}

        /// <summary>
        /// 发布日期
        /// </summary>
        public  DateTime?  CaseReleaseDate{get;set;}

        /// <summary>
        /// 案例概述
        /// </summary>
        public  string  CaseOverview{get;set;}

        /// <summary>
        /// 案例描述
        /// </summary>
        public  string  CaseDescription{get;set;}

        /// <summary>
        /// 心得体会
        /// </summary>
        public  string  CaseExperience{get;set;}

        /// <summary>
        /// 分享人ID
        /// </summary>
        public  int?  CaseSharingPersonID{get;set;}

        /// <summary>
        /// 分享人
        /// </summary>
        public  string  CaseSharingPerson{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  CaseAuditStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  CaseAuditStatuName{get;set;}

        /// <summary>
        /// 审核日期
        /// </summary>
        public  DateTime?  CaseAuditDate{get;set;}

        /// <summary>
        /// 审核描述
        /// </summary>
        public  string  CaseAuditDescription{get;set;}

        /// <summary>
        /// 审核人ID
        /// </summary>
        public  int?  CaseAuditPersonID{get;set;}

        /// <summary>
        /// 审核人
        /// </summary>
        public  string  CaseAuditPerson{get;set;}

        public SoftProjectAreaEntity CA_Case { get; set; }
        public List<SoftProjectAreaEntity> CA_Cases { get; set; }
    }
}
