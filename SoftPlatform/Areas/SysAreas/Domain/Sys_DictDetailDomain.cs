
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Framework.Web.Mvc.Sys;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Sys_DictDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        public void Sys_DictDetail_Domain()
        {
            PKField = "Sys_DictID";
            //PKFields = new List<string> { "Sys_DictDetailID" };
            TableName = "Sys_DictDetail";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Sys_DictDetail_PKCheck()
        {
            if (Item.Sys_DictID == null)
            {
                throw new Exception("字典明细主键不能为空！");
            }
        }

        /// <summary>
        /// 查询：根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_DictDetail_GetByID()
        {
            //(1)检查ID是否为空
            Sys_DictDetail_PKCheck();

            string sql=string.Format("SELECT * FROM  Sys_DictDetail  WHERE  Sys_DictDetailID={0}", Item.Sys_DictDetailID);
            var resp =Query16(sql,4);
            return resp;
        }

        /// <summary>
        /// 获取所有能编辑的类型
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_DictDetail_ByCategory()
        {
            this.OperCode = "Sys_Dict.GetByCategory";
            resp = Execute();// ReturnView();
            return resp;
        }

        /// <summary>
        /// 添加--初始化
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_DictDetail_Add()
        {
            resp.Item = new SoftProjectAreaEntity { Category = Item.Category };
            resp.FunNameEn = "Add";
            resp.FunNameCn = "添加";
            return resp;
        }

        /// <summary>
        /// 添加--保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_DictDetail_AddSave()
        {
            //return null;
            using (var scope = new TransactionScope())
            {
                try
                {
                    #region 重复验证
                    OperCode = "Sys_Dict.AddRep";
                    var rescheck = Execute();
                    if (resp.RespAttachInfo.bError)
                        return resp;
                    if (rescheck.Items.Count() != 0)
                    {
                        resp.RespAttachInfo.ValidationErrors.Add(new ValidationInfo { FieldName = "DText", Message = "该字典已存在" });
                        return resp;
                    }
                    #endregion

                    #region 最大的DValue

                    OperCode = "Sys_Dict.MaxDValueByCategory";
                    resp = Execute();
                    if (resp.RespAttachInfo.bError)
                        return resp;

                    #endregion
                    Item.DValue =(Convert.ToInt32( resp.Obj.ToString()) + 2).ToString();

                    OperCode = "Sys_Dict.Add";
                    resp = Execute();

                    #region (3)事务提交
                    if (!resp.RespAttachInfo.bError)//判断是否存在错误
                    {
                        scope.Complete();//提交事务
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    resp.RespAttachInfo.ValidationErrors.ErrorMessage = ex.Message;
                }
                finally
                {
                    scope.Dispose();//结束事务
                }
            }
            return resp;
        }

        /// <summary>
        /// 编辑--查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_DictDetail_Edit()
        {
            Sys_Dict_GetByID();//获取采购订单、采购订单明细信息
            resp.FunNameCn = "编辑";//设置功能标记，以便在Edit.cshtml中显示相应导航
            resp.FunNameEn = "Edit";//设置功能标记，以便在Edit.cshtml中显示相应按钮
            return resp;
        }

        /// <summary>
        /// 编辑--保存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_DictDetail_EditSave()
        {
            //(1)ID检查
            Sys_DictDetail_PKCheck();

            using (var scope = new TransactionScope())
            {
                try
                {
                    #region 重复验证
                    OperCode = "Sys_Dict.EditRep";
                    var rescheck = Execute();
                    if (resp.RespAttachInfo.bError)
                        return resp;
                    if (rescheck.Items.Count() != 0)
                    {
                        resp.RespAttachInfo.ValidationErrors.Add(new ValidationInfo { FieldName = "DText", Message = "该字典已存在" });
                        return resp;
                    }
                    #endregion

                    OperCode = "Sys_Dict.Edit";
                    resp = Execute();// ReturnView();

                    #region (3)事务提交
                    if (!resp.RespAttachInfo.bError)//判断是否存在错误
                    {
                        scope.Complete();//事务提交
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    resp.RespAttachInfo.ValidationErrors.ErrorMessage = ex.Message;
                }
                finally
                {
                    scope.Dispose();//结束事务
                }
            }
            return resp;
        }
    }
}
