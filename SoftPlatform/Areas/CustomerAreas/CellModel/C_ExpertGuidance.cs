
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
    /// 表：C_ExpertGuidance(专家指导)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 专家指导ID
        /// </summary>
        public  int?  C_ExpertGuidanceID{get;set;}

        /// <summary>
        /// 顾客ID
        /// </summary>
        //public  int?  Pre_UserID{get;set;}

        /// <summary>
        /// 专家姓名
        /// </summary>
        public  string  ExpertName{get;set;}

        /// <summary>
        /// 指导日期
        /// </summary>
        public  DateTime?  ExpertGuidanceDate{get;set;}

        /// <summary>
        /// 录入人
        /// </summary>
        public  string  EntryPerson{get;set;}

        /// <summary>
        /// 症状描述
        /// </summary>
        public  string  SymptomDesc{get;set;}

        /// <summary>
        /// 建议
        /// </summary>
        public  string  Suggestions{get;set;}

        /// <summary>
        /// 配方
        /// </summary>
        public  string  Formula{get;set;}

        public SoftProjectAreaEntity C_ExpertGuidance { get; set; }
        public List<SoftProjectAreaEntity> C_ExpertGuidances { get; set; }
    }
}
