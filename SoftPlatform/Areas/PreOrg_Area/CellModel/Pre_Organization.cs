
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
    /// 表：Pre_Organization(组织机构管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 机构
        /// </summary>
        public  int?  Pre_OrganizationID{get;set;}

        /// <summary>
        /// 父节点机构
        /// </summary>
        public  int?  ParentPre_OrganizationID{get;set;}

        /// <summary>
        /// 父节点机构
        /// </summary>
        public  string  ParentPre_OrganizationName{get;set;}

        /// <summary>
        /// 机构名称
        /// </summary>
        public  string  OrganizationName{get;set;}

        /// <summary>
        /// 排序
        /// </summary>
        public  int?  OrganizationSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  OrganizationStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  OrganizationStatuName{get;set;}

        public SoftProjectAreaEntity Pre_Organization { get; set; }
        public List<SoftProjectAreaEntity> Pre_Organizations { get; set; }
    }
}
