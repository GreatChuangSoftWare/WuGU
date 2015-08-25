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
        public int? Design_ModularOrFunDomainDetailID { get; set; }
        
        public string ModularOrFunDomainDetailName { get; set; }
        
        //public int? Design_ModularOrFunDomainID { get; set; }
        
        public int? DomainType { get; set; }

        public int? DomainTypeTemp { get; set; }

        //public int? Design_ModularOrFunSqlID { get; set; }
        
        public int? Serial { get; set; }

        //public string ParamName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DBOperCode { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunDomainDetails { get; set; }

    }
}
