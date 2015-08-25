
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Sys_PremCodeDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Sys_PremCode_Domain()
        {
            PKField = "Sys_PremCodeID";
            //PKFields = new List<string> { "Sys_PremCodeID" };
            TableName = "Sys_PremCode";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Sys_PremCode_PKCheck()
        {
            if (Item.Sys_PremCodeID == null)
            {
                throw new Exception("权限码主键不能为空！");
            }
        }

        /// <summary>
        /// 查询：根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_PremCode_GetByID()
        {
            Sys_PremCode_PKCheck();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
            {
                DBTSql = ";WITH T1000 AS (SELECT * FROM [dbo].[Sys_PremCode] A )",
                DBOperType = 8,
                SelectSubType = 1,
                DBSelectResultType = 4,
                EqualQueryParam = "Sys_PremCodeID"
            };
            resp = Execute();
            return resp;
        }

        #endregion

        /// <summary>
        /// 获取所有权限码：将进行缓存：用于判断是否需要进行权限验证
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Sys_PremCode_GetAll()
        {
            PageQueryBase.RankInfo = "[PremCode]|1";

            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
            {
                DBTSql = "SELECT * FROM [dbo].[Sys_PremCode] A Order  By [PremCode]",
                DBOperType = 16,
                DBSelectResultType = 2,
                EqualQueryParam=""
            };
            Sys_HOperControl = hOperControl;

            var resp = Execute();
            resp = Execute();
            return resp.Items;
        }
    }
}
