using Framework.Core;
using Framework.Web.Mvc;
using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace SoftProject.Domain
{
    /// <summary>
    /// SoftProjectAreaEntityDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        MyResponseBase resp = new MyResponseBase();

        public void Sys_OperLogDetail_Domain()
        {
            PKField = "Sys_OperLogDetailID";
            //PKFields = new List<string> { "Sys_OperLogDetailID" };
            TableName = "Sys_OperLogDetail";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public bool Sys_OperLogDetail_PKCheck()
        {
            if (Item.Sys_OperLogDetailID == null)
            {
                resp.RespAttachInfo.ValidationErrors.Add(new ValidationInfo { ValidationItemType = 1, FieldName = "Sys_OperLogDetailID", Message = "操作日志明细主键不能为空！" });
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查询：根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_OperLogDetail_GetByID()
        {
            if (Sys_OperLogDetail_PKCheck())
            {
                this.OperCode = "Sys_OperLogDetail.ByID";
                resp = Execute();
            }
            return resp;
        }

        #endregion

        #region 功能

        /// <summary>
        /// 操作日志--根据类别主键查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_OperLogDetail_ByOperLogIdent()
        {
            OperCode = "Sys_OperLogDetail.ByOperLogIdent";
            resp = Execute();
            //resp.Item = new Sys_OperLogView(this.Item);

            return resp;
        }

        /// <summary>
        /// 操作日志--根据类别主键查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_OperLogDetail_Index()
        {
            OperCode = "Sys_OperLogDetail.Index";
            resp = Execute();
            //resp.Item = new Sys_OperLogView(this.Item);

            return resp;
        }

        /// <summary>
        /// 日志添加
        /// </summary>
        /// <param name="LoginCategoryID">登录类型：1：公司，2：加盟商</param>
        /// <param name="LogCategoryID">日志类型：1：登录</param>
        /// <param name="LogCategoryName">日志类型名称：</param>
        /// <param name="Comp_CompanyID">公司ID：</param>
        /// <param name="LogPersonID">日志人员ID</param>
        /// <param name="LogPerson">日志人员</param>
        /// <param name="OperLogIdent">操作标识</param>
        /// <param name="OperName">操作名称</param>
        /// <param name="DetailDescription">描述</param>
        /// <returns></returns>
        public static MyResponseBase Sys_OperLogDetail_AddSave(int? LoginCategoryID,
            int?LogCategoryID,string LogCategoryName,
            int? Pre_CompanyID, int? LogPersonID, string LogPerson,
            int? OperLogIdent, string OperName, 
            string DetailDescription="")
        {
            MyResponseBase resp = new MyResponseBase();

            SoftProjectAreaEntityDomain operLogDetailDomain = new SoftProjectAreaEntityDomain
            {
                Item = new SoftProjectAreaEntity
                {
                    LoginCategoryID=LoginCategoryID,
                    LogCategoryID=LogCategoryID,
                    LogCategoryName=LogCategoryName,
                    Pre_CompanyID = Pre_CompanyID,
                    LogPersonID=LogPersonID,
                    LogPerson=LogPerson,

                    OperLogIdent = OperLogIdent,
                    OperName = OperName,
                    OperDate=DateTime.Now,
                    DetailDescription = DetailDescription,
                }
            };

            using (var scope = new TransactionScope())
            {
                try
                {
                    operLogDetailDomain.Sys_OperLogDetail_Domain();
                    operLogDetailDomain.OperCode = "Sys_OperLogDetail.AddSave";
                    resp = operLogDetailDomain.Execute();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception( ex.Message);
                }
                finally
                {
                    scope.Dispose();
                }
            }
            return resp;
        }

        #endregion
    }
}
