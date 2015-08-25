using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Framework.Web.Mvc.Authorization
namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：Pre_RolePremSet(角色--权限集)
    /// </summary>
    public partial class SoftProjectAreaEntity 
    {
        /// <summary>
        /// 角色-权限集ID
        /// </summary>
        public int? Pre_RolePremSetID { get; set; }

        ///// <summary>
        ///// 权限ID
        ///// </summary>
        //public int? Pre_RoleID { get; set; }

        /// <summary>
        /// 权限集ID
        /// </summary>
        public int? Sys_PremSetID { get; set; }

        /// <summary>
        /// 数据权限ID
        /// </summary>
        public int? DataRightValue { get; set; }

        //public int? Design_PremSetID{get;set;}

        public string PremActionPath { get; set; }
        ////public string ModularName{get;set;}
        //public string ActionMethodCn { get; set; }

        /// <summary>
        /// 权限父节点ID
        /// </summary>
        public int? PremSetParentID { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public int? bCheckSelect { get; set; }

        public int? OrderNum { get; set; }

        public string PremSetName { get; set; }

        /// <summary>
        /// 角色-权限集列表
        /// </summary>
        public List<SoftProjectAreaEntity> Pre_RolePremSets { get; set; }
    }
}
