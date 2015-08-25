using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{

    public class ValidationErrors : List<ValidationInfo>
    {
        public void Add(int ValidationItemType, string Title, string FieldName, string Message, Dictionary<string, string> PKeyVal)
        {
            base.Add(new ValidationInfo(PKeyVal) { ValidationItemType = ValidationItemType, Title = Title, FieldName = FieldName, Message = Message });
        }

        /// <summary>
        /// 简单错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

}
