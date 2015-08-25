
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
    /// 表：BC_PartnerProductPrice(合作商商品价格编辑)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 合作商商品价格ID
        /// </summary>
        public  int?  BC_PartnerProductPriceID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 合作商ID
        /// </summary>
        //public  int?  Pre_UserID{get;set;}

        /// <summary>
        /// 商品类别ID
        /// </summary>
        //public  int?  P_CategoryID{get;set;}

        /// <summary>
        /// 商品ID
        /// </summary>
        //public  int?  P_ProductID{get;set;}

        /// <summary>
        /// 商品价格
        /// </summary>
        public  decimal?  BCProductPrice{get;set;}

        /// <summary>
        /// 规格价格
        /// </summary>
        public  decimal?  BCSpecificationsPrice{get;set;}

        /// <summary>
        /// 是否有效
        /// </summary>
        //public  int?  bValidate{get;set;}

        public SoftProjectAreaEntity BC_PartnerProductPrice { get; set; }
        public List<SoftProjectAreaEntity> BC_PartnerProductPrices { get; set; }
    }
}
