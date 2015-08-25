
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
    /// 表：T_ToolCategory(工具类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 工具类别ID
        /// </summary>
        public  int?  T_ToolCategoryID{get;set;}

        /// <summary>
        /// 工具父节点
        /// </summary>
        public  int?  ParentT_ToolCategoryID{get;set;}

        /// <summary>
        /// 父节点名称
        /// </summary>
        public  string  ParentT_ToolCategoryName{get;set;}

        /// <summary>
        /// 工具类别名称
        /// </summary>
        public  string  ToolCategoryName{get;set;}

        /// <summary>
        /// 排序号
        /// </summary>
        public  int?  ToolCategorySort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  ToolCategoryStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  ToolCategoryStatuName{get;set;}

        public SoftProjectAreaEntity T_ToolCategory { get; set; }
        public List<SoftProjectAreaEntity> T_ToolCategorys { get; set; }
    }
}
