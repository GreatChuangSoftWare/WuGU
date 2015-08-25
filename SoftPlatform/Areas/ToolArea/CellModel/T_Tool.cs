
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
    /// 表：T_Tool(工具管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 工具ID
        /// </summary>
        public  int?  T_ToolID{get;set;}

        /// <summary>
        /// 工具类别
        /// </summary>
        //public  int?  T_ToolCategoryID{get;set;}

        /// <summary>
        /// 工具名称
        /// </summary>
        public  string  ToolName{get;set;}

        /// <summary>
        /// 工具排序
        /// </summary>
        public  int?  ToolSort{get;set;}

        /// <summary>
        /// 上传日期
        /// </summary>
        public  DateTime?  ToolUploadDate{get;set;}

        /// <summary>
        /// 上传人
        /// </summary>
        public  int?  ToolUploadPersonID{get;set;}

        /// <summary>
        /// 上传人
        /// </summary>
        public  string  ToolUploadPerson{get;set;}

        /// <summary>
        /// 工具描述
        /// </summary>
        public  string  ToolDesc{get;set;}

        /// <summary>
        /// 附件
        /// </summary>
        public  string  ToolAttRefPKTableGuid{get;set;}

        /// <summary>
        /// 附件
        /// </summary>
        public  string  ToolAttRefPKTableGuidSubQuery{get;set;}

        /// <summary>
        /// 附件下载
        /// </summary>
        public  string  ToolAttDown{get;set;}

        public SoftProjectAreaEntity T_Tool { get; set; }
        public List<SoftProjectAreaEntity> T_Tools { get; set; }
    }
}
