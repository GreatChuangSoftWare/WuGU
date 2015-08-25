
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
    /// 表：Act_SurveyTemplate(活动调研模板)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 活动调研表模板ID
        /// </summary>
        public  int?  Act_SurveyTemplateID{get;set;}

        /// <summary>
        /// 标题
        /// </summary>
        public  string  SurveyTemplateTitle{get;set;}

        /// <summary>
        /// 序号
        /// </summary>
        public  int?  SurveyTemplateSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  SurveyTemplateStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  SurveyTemplateSortStatuName{get;set;}

        /// <summary>
        /// 内容
        /// </summary>
        public  string  SurveyTemplateTitleContext{get;set;}

        public SoftProjectAreaEntity Act_SurveyTemplate { get; set; }
        public List<SoftProjectAreaEntity> Act_SurveyTemplates { get; set; }
    }
}
