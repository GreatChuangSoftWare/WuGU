
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Sys_PremSetPremCodeDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Sys_PremSetPremCode_Domain()
        {
            PKField= "Sys_PremSetPremCodeID";
            //PKFields = new List<string> { "Sys_PremSetPremCodeID" };
            TableName = "Sys_PremSetPremCode";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Sys_PremSetPremCode_PKCheck()
        {
            if (Item.Sys_PremSetPremCodeID == null)
            {
                throw new Exception("权限集-权限码关系的主键不能为空！");
            }
        }

        #endregion
    }
}
