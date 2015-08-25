using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    //回复附加信息
    public class RespAttachInfo
    {
        //错误信息
        //public List<InfoBase> ErrorInfos { get; set; }
        /// <summary>
        /// 整体错误类型，1：一般错误，2：数据不存在
        /// </summary>
        public int ErrorType { get; set; }


        public Tuple<string, SqlParameter[]> TupleSql { get; set; }

        public ValidationErrors ValidationErrors { get; set; }

        //消息信息
        public List<ValidationInfo> MessageInfos { get; set; }

        /// <summary>
        /// 是否有错误，false：无错误，true：有错误
        /// </summary>
        public bool bError
        {
            get
            {
                if (ValidationErrors.Count == 0 && MessageInfos.Count == 0 && ValidationErrors.ErrorMessage == null)
                    return false;
                return true;
            }
        }
        public RespAttachInfo()
        {
            ValidationErrors = new ValidationErrors();
            MessageInfos = new List<ValidationInfo>();
            ErrorType = 1;
        }

        public void Merg(RespAttachInfo RespAttachInfo)
        {
            this.ValidationErrors.AddRange(RespAttachInfo.ValidationErrors);
            this.MessageInfos.AddRange(RespAttachInfo.MessageInfos);
        }
    }
}
