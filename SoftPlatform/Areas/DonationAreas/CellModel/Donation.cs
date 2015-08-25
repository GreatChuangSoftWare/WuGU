
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
    /// 表：Donation(捐助)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 捐助ID
        /// </summary>
        public  int?  DonationID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 捐助标题
        /// </summary>
        public  string  DonationTitle{get;set;}

        /// <summary>
        /// 捐助描述
        /// </summary>
        public  string  DonationContext{get;set;}

        /// <summary>
        /// 捐助开始日期
        /// </summary>
        public  DateTime?  DonationStartDate{get;set;}

        /// <summary>
        /// 捐助结束日期
        /// </summary>
        public  DateTime?  DonationEndDate{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  AuditStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  AuditStatuName{get;set;}

        /// <summary>
        /// 申请日期
        /// </summary>
        public  DateTime?  FillTabDate{get;set;}

        /// <summary>
        /// 填表人ID
        /// </summary>
        public  int?  FillTabPersonID{get;set;}

        /// <summary>
        /// 填表人
        /// </summary>
        public  string  FillTabPerson{get;set;}

        /// <summary>
        /// 审核人ID
        /// </summary>
        public  int?  AuditPersonID{get;set;}

        /// <summary>
        /// 审核人
        /// </summary>
        public  string  AuditPerson{get;set;}

        /// <summary>
        /// 审核日期
        /// </summary>
        public  DateTime?  AuditDate{get;set;}

        public SoftProjectAreaEntity Donation { get; set; }
        public List<SoftProjectAreaEntity> Donations { get; set; }
    }
}
