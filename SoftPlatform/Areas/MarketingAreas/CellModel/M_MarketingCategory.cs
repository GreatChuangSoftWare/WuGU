
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
    /// 表：M_MarketingCategory(营销类别)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 营销类别ID
        /// </summary>
        public  int?  M_MarketingCategoryID{get;set;}

        /// <summary>
        /// 营销父节点
        /// </summary>
        public  int?  ParentM_MarketingCategoryID{get;set;}

        /// <summary>
        /// 父节点名称
        /// </summary>
        public  string  ParentM_MarketingCategoryName{get;set;}

        /// <summary>
        /// 工具类别名称
        /// </summary>
        public  string  MarketingCategoryName{get;set;}

        /// <summary>
        /// 排序号
        /// </summary>
        public  int?  MarketingCategorySort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  MarketingCategoryStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  MarketingCategoryStatuName{get;set;}

        public SoftProjectAreaEntity M_MarketingCategory { get; set; }
        public List<SoftProjectAreaEntity> M_MarketingCategorys { get; set; }
    }
}
