
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
    /// 表：Complaint(我的投诉)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 投诉ID
        /// </summary>
        public  int?  ComplaintID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 投诉类型
        /// </summary>
        public  int?  ComplaintCategoryID{get;set;}

        /// <summary>
        /// 投诉类型
        /// </summary>
        public  string  ComplaintCategoryName{get;set;}

        /// <summary>
        /// 投诉人
        /// </summary>
        public  string  FraDispComplaintPerson{get;set;}

        /// <summary>
        /// 投诉人ID
        /// </summary>
        public  int?  ComplaintPersonID{get;set;}

        /// <summary>
        /// 投诉人
        /// </summary>
        public  string  ComplaintPerson{get;set;}

        /// <summary>
        /// 投诉日期
        /// </summary>
        public  DateTime?  ComplaintDate{get;set;}

        /// <summary>
        /// 投诉对象
        /// </summary>
        public  string  ComplaintObject{get;set;}

        /// <summary>
        /// 投诉内容
        /// </summary>
        public  string  ComplaintContext{get;set;}

        /// <summary>
        /// 处理结果
        /// </summary>
        public  string  ComplaintHandleResult{get;set;}

        /// <summary>
        /// 处理人ID
        /// </summary>
        public  int?  ComplaintHandlePersonID{get;set;}

        /// <summary>
        /// 处理人
        /// </summary>
        public  string  ComplaintHandlePerson{get;set;}

        /// <summary>
        /// 处理日期
        /// </summary>
        public  DateTime?  ComplaintHandleDate{get;set;}

        /// <summary>
        /// 投诉状态
        /// </summary>
        public  int?  ComplaintStatuID{get;set;}

        /// <summary>
        /// 投诉状态
        /// </summary>
        public  string  ComplaintStatuName{get;set;}

        public SoftProjectAreaEntity Complaint { get; set; }
        public List<SoftProjectAreaEntity> Complaints { get; set; }
    }
}
