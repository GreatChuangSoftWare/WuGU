
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
    /// 表：Doc_BaseDocment(公司基本文档管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        public  int?  Doc_BaseDocmentID{get;set;}

        /// <summary>
        /// 公司ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 文档类型
        /// </summary>
        //public  int?  Doc_CategoryID{get;set;}

        /// <summary>
        /// 文档标题
        /// </summary>
        public  string  DocmentTitle{get;set;}

        /// <summary>
        /// 排序号
        /// </summary>
        public  int?  DocmentSort{get;set;}

        /// <summary>
        /// 位置
        /// </summary>
        public  int?  Position{get;set;}

        /// <summary>
        /// 创建人
        /// </summary>
        public  int?  DocmentPersonID{get;set;}

        /// <summary>
        /// 创建人
        /// </summary>
        public  string  DocmentPerson{get;set;}

        /// <summary>
        /// 创建日期
        /// </summary>
        public  DateTime?  DocmentDate{get;set;}

        /// <summary>
        /// 内容
        /// </summary>
        public  string  DocmentContext{get;set;}

        /// <summary>
        /// 审核内容
        /// </summary>
        public  string  DocmentAuditingContext{get;set;}

        /// <summary>
        /// 审核人
        /// </summary>
        public  int?  DocmentAuditingPersonID{get;set;}

        /// <summary>
        /// 审核人
        /// </summary>
        public  string  DocmentAuditingPerson{get;set;}

        /// <summary>
        /// 审核日期
        /// </summary>
        public  DateTime?  DocmentAuditingDate{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  DocmentStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  DocmentStatuName{get;set;}

        public SoftProjectAreaEntity Doc_BaseDocment { get; set; }
        public List<SoftProjectAreaEntity> Doc_BaseDocments { get; set; }
    }
}
