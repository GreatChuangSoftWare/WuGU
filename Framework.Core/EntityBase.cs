using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.Core
{
    public class EntityBase
    {
        public int? CreateUserID { get; set; }

        public string CreateUserName { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateUserID { get; set; }

        public string UpdateUserName { get; set; }

        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 费用状态:0：未引用  1：已引用
        /// </summary>
        public int? RefState { get; set; }

        /// <summary>
        /// 判断是查看，还是编辑,值为：Disabled
        /// </summary>
        public string Disabled { get; set; }

        public int? TotalItems { get; set; }
    }
}