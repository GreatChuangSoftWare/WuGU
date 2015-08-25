
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Fra_Guidance(指导内容)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 最后1次指导
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Fra_Guidance_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM Fra_Guidance  Where  Pre_CompanyID={0} ORDER BY GuidanceDate DESC", Item.Pre_CompanyID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
