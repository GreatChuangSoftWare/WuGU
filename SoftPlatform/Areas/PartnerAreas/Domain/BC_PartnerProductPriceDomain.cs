
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：BC_PartnerProductPrice(合作商商品价格)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 合作商商品价格
        /// </summary>
        /// <returns></returns>
        public MyResponseBase BC_PartnerProductPrice_AutoCompletePartnerProduct()
        {
            Sys_HOperControl = null;
            SelectSubType = 6;
            OperCode = "PartnerAreas.BC_PartnerProductPrice.AutoCompletePartnerProduct";
            var resp = Execute();

            resp.Querys = Querys;
            resp.Item = Item;
            return resp;
        }

    }
}
