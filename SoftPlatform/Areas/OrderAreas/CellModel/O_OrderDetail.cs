
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
    /// 表：O_OrderDetail(订单明细-编辑列表)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public  int?  O_OrderDetailID{get;set;}

        /// <summary>
        /// 订单ID
        /// </summary>
        //public  int?  O_OrderID{get;set;}

        /// <summary>
        /// 商品ID
        /// </summary>
        //public  int?  P_ProductID{get;set;}

        /// <summary>
        /// 数量
        /// </summary>
        public  int?  Number{get;set;}

        /// <summary>
        /// 单价
        /// </summary>
        //public  decimal?  ProductPrice{get;set;}

        /// <summary>
        /// 规格价格
        /// </summary>
        //public  decimal?  SpecificationsPrice{get;set;}

        /// <summary>
        /// 总价
        /// </summary>
        public  decimal?  PriceTotal{get;set;}

        /// <summary>
        /// 销售类别
        /// </summary>
        public  int?  SalesCategoryID{get;set;}

        /// <summary>
        /// 销售类别
        /// </summary>
        public  string  SalesCategoryName{get;set;}

        public SoftProjectAreaEntity O_OrderDetail { get; set; }
        public List<SoftProjectAreaEntity> O_OrderDetails { get; set; }
    }
}
