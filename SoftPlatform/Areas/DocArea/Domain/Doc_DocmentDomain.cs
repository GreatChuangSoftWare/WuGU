
using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using SoftProject.CellModel;
using System.Web;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Doc_Docment(文档类别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 查询--首页
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Doc_Docment_Home()
        {
            var sbsql = new StringBuilder();
            sbsql.AppendLine(";WITH T0 AS");
            sbsql.AppendLine("(");
            sbsql.AppendLine("	SELECT A.Doc_CategoryID,Doc_DocmentID,[DocmentTitle]");
            sbsql.AppendLine("      ,[DocmentSort]");
            sbsql.AppendLine("      ,[Position]");
            sbsql.AppendLine("      ,[DocmentPersonID]");
            sbsql.AppendLine("      ,[DocmentPerson]");
            sbsql.AppendLine("      ,[DocmentDate]");
            sbsql.AppendLine("      ,[DocmentAuditingContext]");
            sbsql.AppendLine("      ,[DocmentAuditingPersonID]");
            sbsql.AppendLine("      ,[DocmentAuditingPerson]");
            sbsql.AppendLine("      ,[DocmentAuditingDate]");
            sbsql.AppendLine("      ,[DocmentStatuID]");
            sbsql.AppendLine("      ,[DocmentOutline]");
            sbsql.AppendLine("      ,[DocmentThumbnailPath]");
            sbsql.AppendLine("	  ,ROW_NUMBER() OVER(PARTITION BY A.Doc_CategoryID ORDER BY B.UpdateDate DESC) R");
            sbsql.AppendLine("	FROM Doc_Category A");
            sbsql.AppendLine("	LEFT JOIN Doc_Docment B ON A.Doc_CategoryID=B.Doc_CategoryID");
            sbsql.AppendLine(")");
            sbsql.AppendLine("SELECT *");
            sbsql.AppendLine("FROM T0");
            sbsql.AppendLine("WHERE T0.R<=10");
            var resp = Query16(sbsql.ToString());
            return resp;
        }

    }
}
