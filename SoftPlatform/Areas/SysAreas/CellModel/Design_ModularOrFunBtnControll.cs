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
    /// 表：Design_ModularOrFunBtnControll(功能模块Controll)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        //public int? Design_ModularOrFunBtnID { get; set; }
        //public int? Design_ModularOrFunControllID { get; set; }
        //public int? Sort { get; set; }
        public int? Design_ModularOrFunBtnControllID { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunBtnControlls { get; set; }

    }
}
