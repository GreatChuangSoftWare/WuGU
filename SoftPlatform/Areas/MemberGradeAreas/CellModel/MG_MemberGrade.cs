
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
    /// 表：MG_MemberGrade(会员等级)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
         //<summary>
         //会员等级
         //</summary>
        public  int?  MG_MemberGradeID{get;set;}

        /// <summary>
        /// 公司企业ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 等级名称
        /// </summary>
        public  string  MemberGradeName{get;set;}

        /// <summary>
        /// 等级序号
        /// </summary>
        public  int?  MemberGradeSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  MemberGradeStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  MemberGradeStatuName{get;set;}

        public SoftProjectAreaEntity MG_MemberGrade { get; set; }
        public List<SoftProjectAreaEntity> MG_MemberGrades { get; set; }
    }
}
