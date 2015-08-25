
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
    /// 业务层：Fra_Activity(活动管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 添加保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Fra_Activity_Add()
        {
            var resp = new MyResponseBase();
            var sbsql = new StringBuilder();
            sbsql.AppendLine("SELECT TOP 1 *");
            sbsql.AppendLine("FROM [dbo].[Act_ActivityFlow]");
            sbsql.AppendLine("WHERE ActivityFlowStatuID=1");
            sbsql.AppendLine("ORDER BY ActivityFlowSort DESC");
            resp = Query16(sbsql.ToString(), 4);
            resp.Item.ActivityFlowContext = resp.Item.ActivityFlowContext;

            sbsql = new StringBuilder();
            sbsql.AppendLine("SELECT TOP 1 *");
            sbsql.AppendLine("FROM Act_SurveyTemplate");
            sbsql.AppendLine("WHERE SurveyTemplateStatuID=1");
            sbsql.AppendLine("ORDER BY SurveyTemplateSort DESC");
            var resp1 = Query16(sbsql.ToString(), 4);
            resp.Item.Act_SurveyTemplateID = resp1.Item.Act_SurveyTemplateID;
            resp.Item.ActivitySelfAtudyTable = resp1.Item.SurveyTemplateTitleContext;

            resp.Item.ApplyDate = DateTime.Now;
            resp.Item.ApplyPersonID = LoginInfo.Sys_LoginInfoID;
            resp.Item.ApplyPerson = LoginInfo.UserName;
            resp.Item.Pre_CompanyID = LoginInfo.CompanyID;

            return resp;
        }

        /// <summary>
        /// 最后1个活动
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Fra_Activity_Last()
        {
            var sql = string.Format("SELECT TOP 1  * FROM  V_Fra_Activity  Where  Pre_CompanyID={0} ORDER BY ActivityStartDate DESC", Item.Pre_CompanyID);
            var resp = Query16(sql, 4);
            return resp;
        }

    }
}
