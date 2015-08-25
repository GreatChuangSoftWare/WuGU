using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 表:Sys_Dict(字典)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 字典明细ID
        /// </summary>
        public int? Sys_DictDetailID{get;set;}

        ///// <summary>
        ///// 字典ID
        ///// </summary>
        //public int? Sys_DictID{get;set;}

        /// <summary>
        /// 字典文本
        /// </summary>
        public string DText { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string DValue { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int? DictSortID { get; set; }

        public List<SoftProjectAreaEntity> Sys_DictDetails { get; set; }
    }
}

