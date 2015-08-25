
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
    /// 表：Pr_Notice(促销公告)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 促销公告ID
        /// </summary>
        public  int?  Pr_NoticeID{get;set;}

        /// <summary>
        /// 标题
        /// </summary>
        public  string  PromotionTitle{get;set;}

        /// <summary>
        /// 开始日期
        /// </summary>
        public  DateTime?  PromotionStartDate{get;set;}

        /// <summary>
        /// 结束日期
        /// </summary>
        public  DateTime?  PromotionEndDate{get;set;}

        /// <summary>
        /// 促销描述
        /// </summary>
        public  string  PromotionContext{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  PromotionStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  PromotionStatuName{get;set;}

        public SoftProjectAreaEntity Pr_Notice { get; set; }
        public List<SoftProjectAreaEntity> Pr_Notices { get; set; }
    }
}
