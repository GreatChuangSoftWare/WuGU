
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
    /// 表：P_Category(商品类别)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 商品类别ID
        /// </summary>
        public  int?  P_CategoryID{get;set;}

        /// <summary>
        /// 公司ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 父节点ID
        /// </summary>
        public  int?  ParentP_CategoryID{get;set;}

        /// <summary>
        /// 父节点名称
        /// </summary>
        public  string  ParentP_CategoryName{get;set;}

        /// <summary>
        /// 商品类别名称
        /// </summary>
        public  string  PCategoryName{get;set;}

        /// <summary>
        /// 商品类型序号
        /// </summary>
        public  int?  PCategorySort{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  PStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  PStatuName{get;set;}

        public SoftProjectAreaEntity P_Category { get; set; }
        public List<SoftProjectAreaEntity> P_Categorys { get; set; }
    }
}
