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
        public int? Sys_DictID { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否可以修改
        /// </summary>
        public int? IsModi { get; set; }
        
        public List<SoftProjectAreaEntity> Sys_Dicts { get; set; }
    }
}

