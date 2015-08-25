
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
    /// 表：Pre_Role(角色管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public  int?  Pre_RoleID{get;set;}

        /// <summary>
        /// 角色名称
        /// </summary>
        public  string  RoleName{get;set;}

        /// <summary>
        /// 首页名称
        /// </summary>
        public  string  RoleHomePageName{get;set;}

        /// <summary>
        /// 首页Url
        /// </summary>
        public  string  RoleHomePageUrl{get;set;}

        /// <summary>
        /// 排序
        /// </summary>
        public  int?  RoleSort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  RoleStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  RoleStatuName{get;set;}

        /// <summary>
        /// 角色类别
        /// </summary>
        public  int?  LoginCategoryID{get;set;}

        /// <summary>
        /// 角色类别
        /// </summary>
        public  string  LoginCategoryName{get;set;}

        /// <summary>
        /// 是否管理员角色
        /// </summary>
        public int? BAdmin { get; set; }

        public string BAdminName { get; set; }

        public SoftProjectAreaEntity Pre_Role { get; set; }
        public List<SoftProjectAreaEntity> Pre_Roles { get; set; }
    }
}
