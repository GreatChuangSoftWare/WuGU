
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
    /// 表：C_OrderDetail(顾客订单明细)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 消费明细ID
        /// </summary>
        public  int?  C_OrderDetailID{get;set;}

        /// <summary>
        /// 消费单ID
        /// </summary>
        //public  int?  C_OrderID{get;set;}

        /// <summary>
        /// 商品价格ID
        /// </summary>
        //public  int?  Fra_ProductPriceID{get;set;}

        /// <summary>
        /// 商品ID
        /// </summary>
        //public  int?  P_ProductID{get;set;}

        /// <summary>
        /// 数量
        /// </summary>
        //public  int?  Number{get;set;}

        /// <summary>
        /// 价格
        /// </summary>
        //public  decimal?  CustomerProductPrice{get;set;}

        /// <summary>
        /// 规格价格
        /// </summary>
        //public  decimal?  CustomerSpecificationsPrice{get;set;}

        /// <summary>
        /// 合计
        /// </summary>
        //public  decimal?  PriceTotal{get;set;}

        public SoftProjectAreaEntity C_OrderDetail { get; set; }
        public List<SoftProjectAreaEntity> C_OrderDetails { get; set; }
    }
}
