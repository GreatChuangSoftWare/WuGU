
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
        public void Sys_Dict_Domain()
        {
            PKField = "Sys_DictID";
            //PKFields = new List<string> { "Sys_DictID" };
            TableName = "Sys_Dict";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Sys_Dict_PKCheck()
        {
            if (Item.Sys_DictID == null)
            {
                throw new Exception("字典主键不能为空！");
            }
        }

        /// <summary>
        /// 查询：根据ID查询
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_Dict_GetByID()
        {
            //(1)检查ID是否为空
            Sys_Dict_PKCheck();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity
            {
                DBTSql = string.Format("SELECT * FROM  Sys_Dict  WHERE  Sys_DictID={0}", Item.Sys_DictID),
                DBOperType = 16,
                DBSelectResultType = 4,
            };
            Sys_HOperControl = hOperControl;
            var resp = Execute();

            return resp;
        }

        /// <summary>
        /// 获取所有数据字典
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Sys_Dict_AreaGetAll()
        {
            string sql = "SELECT * FROM  V_Sys_DictArea  Order  By  Category,DictSortID";
            var resp =Query16(sql);
            return resp.Items;
        }

        ///// <summary>
        ///// 获取所有能编辑的类型
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase IndexByModi()
        //{
        //    this.OperCode = "Sys_Dict.GetAllByIsModi";
        //    resp = Execute();// ReturnView();

        //    if (Item.Category == null)
        //        Item.Category = resp.Items.First().Category;

        //    resp.Item = Item;

        //    Sys_DictDomain domain = new Sys_DictDomain { Item = new Sys_Dict { Category = Item.Category } };
        //    domain.OperCode = "Sys_Dict.GetByCategory";
        //    var resptemp = domain.Execute();// ReturnView();
        //    resp.Item.Sys_Dicts = resptemp.Items;

        //    return resp;
        //}

        /// <summary>
        /// 获取所有能编辑的类型
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_Dict_ByCategory()
        {
            this.OperCode = "Sys_Dict.GetByCategory";
            resp = Execute();// ReturnView();
            return resp;
        }

        /// <summary>
        /// 添加--初始化
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Sys_Dict_Add()
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
        public MyResponseBase Sys_Dict_AddSave()
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
                    Item.DValue =( Convert.ToInt32(resp.Obj) + 2).ToString();

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
        public MyResponseBase Sys_Dict_Edit()
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
        public MyResponseBase Sys_Dict_EditSave()
        {
            //(1)ID检查
            Sys_Dict_PKCheck();

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
