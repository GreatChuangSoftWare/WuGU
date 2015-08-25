
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
    /// 表：CA_CaseCategory(案例类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 案例类别
        /// </summary>
        public  int?  CA_CaseCategoryID{get;set;}

        /// <summary>
        /// 案例父节点
        /// </summary>
        public  int?  ParentCA_CaseCategoryID{get;set;}

        /// <summary>
        /// 父节点名称
        /// </summary>
        public  string  ParentCA_CaseCategoryName{get;set;}

        /// <summary>
        /// 案例类别名称
        /// </summary>
        public  string  CaseCategoryName{get;set;}

        /// <summary>
        /// 排序号
        /// </summary>
        public  int?  CaseCategorySort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  CaseCategoryStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  CaseCategoryStatuName{get;set;}

        public SoftProjectAreaEntity CA_CaseCategory { get; set; }
        public List<SoftProjectAreaEntity> CA_CaseCategorys { get; set; }
    }
}
