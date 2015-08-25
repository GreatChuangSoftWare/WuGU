
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
    /// 表：T_ToolAttachment(工具附件)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 文档附件ID
        /// </summary>
        public  int?  T_ToolAttachmentID{get;set;}

        /// <summary>
        /// 附件类别
        /// </summary>
        //public  int?  AttachmentCategoryID{get;set;}

        /// <summary>
        /// 文档名
        /// </summary>
        //public  string  AttachmentFileName{get;set;}

        /// <summary>
        /// 文档名
        /// </summary>
        //public  string  AttachmentFileNameGuid{get;set;}

        /// <summary>
        /// 文件类型
        /// </summary>
        //public  string  AttachmentFileType{get;set;}

        /// <summary>
        /// 文件大小
        /// </summary>
        //public  decimal?  AttachmentFileSize{get;set;}

        /// <summary>
        /// 文件大小
        /// </summary>
        //public  string  AttachmentFileSizeDisp{get;set;}

        /// <summary>
        /// 关联主表Guid
        /// </summary>
        //public  string  RefPKTableGuid{get;set;}

        public SoftProjectAreaEntity T_ToolAttachment { get; set; }
        public List<SoftProjectAreaEntity> T_ToolAttachments { get; set; }
    }
}
