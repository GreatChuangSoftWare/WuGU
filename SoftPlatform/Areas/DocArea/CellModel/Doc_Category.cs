
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
    /// 表：Doc_Category(文档类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 文档类别ID
        /// </summary>
        public  int?  Doc_CategoryID{get;set;}

        /// <summary>
        /// 公司ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 父节点
        /// </summary>
        public  int?  ParentDoc_CategoryID{get;set;}

        /// <summary>
        /// 父节点名称
        /// </summary>
        public  string  ParentDoc_CategoryName{get;set;}

        /// <summary>
        /// 文档类别名称
        /// </summary>
        public  string  DocCategoryName{get;set;}

        /// <summary>
        /// 排序序号
        /// </summary>
        public  int?  DocCategorySort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  StatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  StatuName{get;set;}

        public SoftProjectAreaEntity Doc_Category { get; set; }
        public List<SoftProjectAreaEntity> Doc_Categorys { get; set; }
    }
}
