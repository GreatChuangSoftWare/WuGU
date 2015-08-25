
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
    /// 表：BC_Order(合作商订单)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 合作商订单ID
        /// </summary>
        public  int?  BC_OrderID{get;set;}

        /// <summary>
        /// 合作商ID
        /// </summary>
        //public  int?  Pre_UserID{get;set;}

        /// <summary>
        /// 订单编号
        /// </summary>
        //public  string  OrderNo{get;set;}

        /// <summary>
        /// 订单日期
        /// </summary>
        //public  DateTime?  OrderDate{get;set;}

        /// <summary>
        /// 订单金额
        /// </summary>
        //public  decimal?  OrderAmount{get;set;}

        /// <summary>
        /// 录入人
        /// </summary>
        //public  string  OrderEntityPerson{get;set;}

        /// <summary>
        /// 订单状态
        /// </summary>
        //public  int?  OrderStatuID{get;set;}

        /// <summary>
        /// 订单状态
        /// </summary>
        //public  string  OrderStatuName{get;set;}

        public SoftProjectAreaEntity BC_Order { get; set; }
        public List<SoftProjectAreaEntity> BC_Orders { get; set; }
    }
}
