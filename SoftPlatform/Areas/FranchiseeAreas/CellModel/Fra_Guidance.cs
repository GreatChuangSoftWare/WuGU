
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
    /// 表：Fra_Guidance(指导内容)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 指导内容ID
        /// </summary>
        public  int?  Fra_GuidanceID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 指导日期
        /// </summary>
        public  DateTime?  GuidanceDate{get;set;}

        /// <summary>
        /// 指导人
        /// </summary>
        public  string  GuidancePerson{get;set;}

        /// <summary>
        /// 录入人
        /// </summary>
        //public  string  EntryPerson{get;set;}

        /// <summary>
        /// 指导内容
        /// </summary>
        public  string  GuidanceContent{get;set;}

        public SoftProjectAreaEntity Fra_Guidance { get; set; }
        public List<SoftProjectAreaEntity> Fra_Guidances { get; set; }
    }
}
