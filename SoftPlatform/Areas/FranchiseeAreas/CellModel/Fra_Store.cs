
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
    /// 表：Fra_Store(门店管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 门店管理ID
        /// </summary>
        public  int?  Fra_StoreID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 门店名称
        /// </summary>
        public  string  StoreName{get;set;}

        /// <summary>
        /// 电话
        /// </summary>
        public  string  StoreTel{get;set;}

        /// <summary>
        /// 店长
        /// </summary>
        public  string  StoreManager{get;set;}

        /// <summary>
        /// 门店模板ID
        /// </summary>
        //public  int?  Fra_StoreTemplateID{get;set;}

        /// <summary>
        /// 门店调研表
        /// </summary>
        public  string  StoreContext{get;set;}

        public SoftProjectAreaEntity Fra_Store { get; set; }
        public List<SoftProjectAreaEntity> Fra_Stores { get; set; }
    }
}
