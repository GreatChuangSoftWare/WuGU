
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
    /// 业务层：Fra_Store(门店管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 添加保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Fra_Store_Add()
        {
            var resp = new MyResponseBase();
            var sbsql = new StringBuilder();
            sbsql.AppendLine("SELECT TOP 1 * ");
            sbsql.AppendLine("FROM Fra_StoreTemplate");
            sbsql.AppendLine("WHERE StoreTemplateStatuID=1");
            sbsql.AppendLine("ORDER BY StoreTemplateSort DESC");
            resp = Query16(sbsql.ToString(), 4);

            resp.Item.StoreContext = resp.Item.StoreTemplateContext;
            return resp;
        }

    }
}
