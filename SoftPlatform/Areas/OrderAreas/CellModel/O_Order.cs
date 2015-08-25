
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
    /// 表：O_Order(订单管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
         ///<summary>
         ///订单ID
         ///</summary>
        public  int?  O_OrderID{get;set;}

        /// <summary>
        /// 加盟商ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 订单编号
        /// </summary>
        public  string  OrderNo{get;set;}

        /// <summary>
        /// 订单日期
        /// </summary>
        public  DateTime?  OrderDate{get;set;}

        /// <summary>
        /// 订单金额
        /// </summary>
        public  decimal?  OrderAmount{get;set;}

        /// <summary>
        /// 订单状态
        /// </summary>
        public  int?  OrderStatuID{get;set;}

        /// <summary>
        /// 订单状态
        /// </summary>
        public  string  OrderStatuName{get;set;}

        /// <summary>
        /// 处理日期
        /// </summary>
        public  DateTime?  OrderHandleDate{get;set;}

        /// <summary>
        /// 处理人ID
        /// </summary>
        public  int?  OrderHandlePersonID{get;set;}

        /// <summary>
        /// 处理人
        /// </summary>
        public  string  OrderHandlePerson{get;set;}

        public SoftProjectAreaEntity O_Order { get; set; }
        public List<SoftProjectAreaEntity> O_Orders { get; set; }
    }
}
