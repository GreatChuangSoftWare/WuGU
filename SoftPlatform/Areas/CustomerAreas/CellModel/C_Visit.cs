
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
    /// 表：C_Visit(回访管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 回访ID
        /// </summary>
        public  int?  C_VisitID{get;set;}

        /// <summary>
        /// 顾客ID
        /// </summary>
        //public  int?  Pre_UserID{get;set;}

        /// <summary>
        /// 回访日期
        /// </summary>
        public  DateTime?  VisitDate{get;set;}

        /// <summary>
        /// 回访人
        /// </summary>
        public  string  VisitPerson{get;set;}

        ///// <summary>
        ///// 下次回访日期
        ///// </summary>
        //public  DateTime?  NextVisitDate{get;set;}

        /// <summary>
        /// 回访内容
        /// </summary>
        public  string  VisitContext{get;set;}

        public SoftProjectAreaEntity C_Visit { get; set; }
        public List<SoftProjectAreaEntity> C_Visits { get; set; }
    }
}
