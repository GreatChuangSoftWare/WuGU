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
    //SoftProjectAreaEntityDomain
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Sys_OperLog_Domain()
        {
            PKField = "Sys_OperLogID";// new List<string> { "Sys_OperLogID" };
            //base.PKOperCode = "Sys_OperLog.ByID";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public bool Sys_OperLog_PKCheck()
        {
            if (Item.Sys_OperLogID == null)
            {
                resp.RespAttachInfo.ValidationErrors.Add(new ValidationInfo { ValidationItemType = 1, FieldName = "Sys_OperLogID", Message = "操作日志主键不能为空！" });
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查询：根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_OperLog_GetByID()
        {
            if (Sys_OperLog_PKCheck())
            {
                this.OperCode = "Sys_OperLog.ByID";
                resp = Execute();
            }
            return resp;
        }

        #endregion

        #region 功能

        /// <summary>
        /// 操作日志--查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_OperLog_Index()
        {
            PageQueryBase.IsPagination = 1;
            if (PageQueryBase.RankInfo == null || PageQueryBase.RankInfo.Length == 0)
            {
                PageQueryBase.RankInfo = "UpdateDate|0";
            }
            OperCode = "Sys_OperLog.Index";
            var resp = Execute();
            resp.Item = this.Item;

            return resp;
        }

        /// <summary>
        /// 日志添加
        /// </summary>
        /// <param name="LoginCategoryID">登录类型：1：公司，2：加盟商</param>
        /// <param name="LogCategoryID">日志类型：1：登录</param>
        /// <param name="LogCategoryName">日志类型名称：</param>
        /// <param name="OperLogIdent">操作标识</param>
        /// <param name="OperName">操作名称</param>
        /// <param name="DetailDescription">描述</param>
        /// <returns></returns>
        public static MyResponseBase Sys_OperLog_AddSave(int? LoginCategoryID,
            int? LogCategoryID, string LogCategoryName, int? OperLogIdent, string OperName,
            string DetailDescription = "")
        {
            MyResponseBase resp = new MyResponseBase();

            SoftProjectAreaEntityDomain operLogDetailDomain = new SoftProjectAreaEntityDomain
            {
                Item = new SoftProjectAreaEntity
                {
                    LoginCategoryID = LoginCategoryID,
                    LogCategoryID = LogCategoryID,
                    LogCategoryName = LogCategoryName,
                    OperLogIdent = OperLogIdent,
                    OperName = OperName,
                    OperDate = DateTime.Now,
                    DetailDescription = DetailDescription,
                }
            };

            using (var scope = new TransactionScope())
            {
                try
                {
                    operLogDetailDomain.OperCode = "Sys_OperLogDetail.AddSave";
                    resp = operLogDetailDomain.Execute();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    scope.Dispose();
                }
            }
            return resp;
        }

        ///// <summary>
        ///// 操作日志--添加
        ///// </summary>
        ///// <returns></returns>
        //public static MyResponseBase Sys_OperLog_AddSave(int LoginCategory,string OperLogCategoryIdentEn,string OperLogCategoryIdentCn,string OperLogIdent)
        //{
        //    SoftProjectAreaEntityDomain operLogDomain = new SoftProjectAreaEntityDomain
        //    {
        //        Item = new SoftProjectAreaEntity
        //        {
        //            LoginCategory = LoginCategory,
        //            OperLogCategoryIdentEn = OperLogCategoryIdentEn,
        //            OperLogCategoryIdentCn = OperLogCategoryIdentCn,
        //            OperLogIdent = OperLogIdent
        //        }
        //    };
        //    MyResponseBase resp = new MyResponseBase();
        //    using (var scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            operLogDomain.OperCode = "Sys_OperLog.Add";
        //            resp =operLogDomain.Execute();
        //            scope.Complete();
        //        }
        //        catch (Exception ex)
        //        {
        //            resp.RespAttachInfo.ValidationErrors.ErrorMessage = ex.Message;
        //        }
        //        finally
        //        {
        //            scope.Dispose();
        //        }
        //    }
        //    return resp;
        //}


        #endregion
    }
}
