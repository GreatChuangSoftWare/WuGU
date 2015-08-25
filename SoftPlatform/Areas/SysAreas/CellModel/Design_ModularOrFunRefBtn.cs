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
    /// 表：Design_PremSetControll(功能模块Controll)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 权限按钮ID
        /// </summary>
        public int? Design_ModularOrFunRefBtnID { get; set; }

        //public int? Design_PremSetID { get; set; }
        //public int? Design_ModularOrFunBtnID { get; set; }
        public int? PremSetBtnSort { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int? Design_PremSetID { get; set; }

        ///// <summary>
        ///// 控制器ID
        ///// </summary>
        //public int? Design_ModularOrFunControllID { get; set; }

        /// <summary>
        /// 权限控制器排序
        /// </summary>
        //public int? PremSetControllSort { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunRefBtns { get; set; }

    }
}
