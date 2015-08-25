using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class ValidationInfo
    {
        /// <summary>
        /// 验证类型：
        /// ValidationItemType.Entirety：实体
        /// </summary>
        public int ValidationItemType { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 行记录标记信息
        /// </summary>
        public Dictionary<string, string> PKeyVals { get; set; }

        /// <summary>
        /// 错误或提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 为1表示为，数据被修改错误
        /// </summary>
        public int ErrorType { get; set; }

        public ValidationInfo(Dictionary<string, string> PKeyVals)
        {
            this.PKeyVals = PKeyVals == null ? null : PKeyVals;
            ValidationItemType = 1;// "1";// ValidationItemType.Entirety;
            ErrorType = 0;
        }

        public ValidationInfo()
        {
            this.PKeyVals = null;
            ValidationItemType = 1;// ValidationItemType.Entirety;
            ErrorType = 0;
            //var tt = { ValidationItemType:"",Title:"",FieldName:"",Message:"" };
        }
    }
}
