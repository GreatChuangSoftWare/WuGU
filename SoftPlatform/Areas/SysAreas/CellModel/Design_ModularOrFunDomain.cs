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
    /// 表：Design_ModularOrFunControll(功能模块Controll)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        public int? Design_ModularOrFunDomainID { get; set; }
        
        public string ModularOrFunDomainName { get; set; }

        public string MethodName { get; set; }

        public int? MethodReturnTypeID { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunDomains { get; set; }

    }
}
