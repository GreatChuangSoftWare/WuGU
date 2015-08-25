using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    /// <summary>
    /// 排序
    /// </summary>
    public class RankInfo
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 升序或降序
        /// </summary>
        public bool Ascending { get; set; }
    }
}
