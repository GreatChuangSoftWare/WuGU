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
        public int? Design_ModularOrFunControllID { get; set; }

        public string Design_ModularOrFunControllIDs { get; set; }

        public string LableTitle { get; set; }

        public string ControllName { get; set; }
        public string ControllActionPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionMethod { get; set; }
        public int? Method { get; set; }
        public int?  ActionName { get; set; }
        
        public string ActionNameCN { get; set; }

        public string ParamName { get; set; }
        public int? ViewName { get; set; }

        public string ViewNameCN { get; set; }

        //public int? Design_ModularOrFunID{get;set;}
        //public int? Design_ModularOrFunID { get; set; }
        //public int? Design_ModularOrFunDomainID{get;set;}

        public string ControllProgramCode { get; set; }

        /// <summary>
        /// 是否权限验证：1：是
        /// </summary>
        public int? BPrem{get;set;}

        /// <summary>
        /// Action中文名称
        /// </summary>
        public string ActionMethodCn { get; set; }

        /// <summary>
        /// 数据权限字典名
        /// </summary>
        public string DataRightDict { get; set; }

        /// <summary>
        /// 页面Code，用于与页面关联
        /// </summary>
        public string ControllModularOrFunCode { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunControlls { get; set; }

    }
}
