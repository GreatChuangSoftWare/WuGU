
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
    /// 表：Fra_StoreTemplate(门店模板管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 门店调研模板ID
        /// </summary>
        public  int?  Fra_StoreTemplateID{get;set;}

        /// <summary>
        /// 题目
        /// </summary>
        public  string  StoreTemplateTitle{get;set;}

        /// <summary>
        /// 序号
        /// </summary>
        public  int?  StoreTemplateSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  StoreTemplateStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  StoreTemplateStatuName{get;set;}

        /// <summary>
        /// 内容
        /// </summary>
        public  string  StoreTemplateContext{get;set;}

        public SoftProjectAreaEntity Fra_StoreTemplate { get; set; }
        public List<SoftProjectAreaEntity> Fra_StoreTemplates { get; set; }
    }
}
