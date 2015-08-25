
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
    /// 表：Fra_ProductPrice(商品价格编辑)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 加盟商价格ID
        /// </summary>
        public  int?  Fra_ProductPriceID{get;set;}

        /// <summary>
        /// 加盟商价格IDs
        /// </summary>
        public  string  Fra_ProductPriceIDs{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 商品类别
        /// </summary>
        //public  int?  P_CategoryID{get;set;}

        /// <summary>
        /// 商品ID
        /// </summary>
        //public  int?  P_ProductID{get;set;}

        /// <summary>
        /// 会员等级
        /// </summary>
        //public  int?  MG_MemberGradeID{get;set;}

        /// <summary>
        /// 顾客斤价格
        /// </summary>
        public  decimal?  CustomerProductPrice{get;set;}

        /// <summary>
        /// 顾客规格价格
        /// </summary>
        public  decimal?  CustomerSpecificationsPrice{get;set;}

        /// <summary>
        /// 有效否
        /// </summary>
        public  int?  bValidate{get;set;}

        public SoftProjectAreaEntity Fra_ProductPrice { get; set; }
        public List<SoftProjectAreaEntity> Fra_ProductPrices { get; set; }
    }
}
