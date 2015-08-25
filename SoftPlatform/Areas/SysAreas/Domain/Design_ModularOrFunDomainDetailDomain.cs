
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using SoftProject.CellModel;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Design_ModularOrFunDomainDetailDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFunDomainDetail_Domain()
        {
            PKField = "Design_ModularOrFunDomainDetailID";
            //PKFields = new List<string> { "Design_ModularOrFunDomainDetailID" };
            TableName = "Design_ModularOrFunDomainDetail";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFunDomainDetail_PKCheck()
        {
            if (Item.Design_ModularOrFunDomainDetailID == null)
            {
                throw new Exception("功能模块页面字段主键不能为空！");
            }
        }

        ///// <summary>
        ///// 查询：根据ID查询
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase<Design_ModularOrFunDomainDetailPremCodeAreaEntity> Design_ModularOrFunDomainDetail_GetByID()
        //{
        //    Sys_PremCode_PKCheck();
        //    SelectType = 1;
        //    DBSelectResultType = 4;
        //    OperCode = "Design_ModularOrFunDomainDetail.Index";
        //    Querys.Add(new Query { QuryType = 1, FieldName = "Design_ModularOrFunDomainDetailID", AndOr = " And ", Oper = "=", Value = Item.Design_ModularOrFunDomainDetailID.ToString() });
        //    resp = Execute();
        //    return resp;
        //}

        #endregion

        /// <summary>
        /// 根据页面ID查询页面字段
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunDomainDetail_GetByModularOrFunDomainID()
        {
            string sql = string.Format("SELECT * FROM Design_ModularOrFunDomainDetail WHERE  Design_ModularOrFunDomainID={0} ", Item.Design_ModularOrFunDomainID);
            var resp = Query16(sql, 2);
            return resp.Items;
        }

        /// <summary>
        /// 根据功能模块ID查询业务层
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunDomainDetail_GetByModularOrFunID()//Design_ModularOrFunID
        {
            //string sql = string.Format("SELECT * FROM Design_ModularOrFunDomainDetail WHERE  Design_ModularOrFunDomainID={0} ", Item.Design_ModularOrFunDomainID);
            StringBuilder sbsql = new StringBuilder();
            sbsql.AppendLine("SELECT B.* ");
            sbsql.AppendLine("FROM Design_ModularOrFunDomain A ");
            sbsql.AppendLine("JOIN Design_ModularOrFunDomainDetail B ON A.Design_ModularOrFunDomainID=B.Design_ModularOrFunDomainID");
            var resp = Query16(sbsql.ToString(), 2);
            return resp.Items;
        }

        ///// <summary>
        ///// 主键不为空检查
        ///// </summary>
        ///// <returns></returns>
        //public void Design_ModularOrFunDomainDetail_PKCheck()
        //{
        //    if (Item.Design_ModularOrFunDomainDetailID == null)
        //    {
        //        throw new Exception("案例管理主键不能为空！");
        //    }
        //}

        //public MyResponseBase Design_ModularOrFunDomainDetail_ByID()
        //{
        //    Design_ModularOrFunDomainDetail_PKCheck();
        //    var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFunDomainDetail] A WHERE Design_ModularOrFunDomainDetailID={0} ", Item.Design_ModularOrFunDomainDetailID);
        //    var resp = Query16(sql, 4);
        //    return resp;
        //}

        /// <summary>
        /// 根据页面ID查询页面字段
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFunDomainDetail_EditList()
        {
            MyResponseBase resp = new MyResponseBase();
            //(1)根据页面ID查询页面字段
            if (FunNameEn == "Add")
            { }
            else if (FunNameEn == "Edit")
            {
                string sql = string.Format("SELECT * FROM Design_ModularOrFunDomainDetail WHERE  Design_ModularOrFunDomainID={0} Order  By Serial", Item.Design_ModularOrFunDomainID);
                resp = Query16(sql, 2);
                ////(2)查询模块编码字段
                //var ModularFields = Design_ModularField_GetModularPageOrQueryField(Item.Design_ModularOrFunID);
                //resp.Item.Design_ModularFields = ModularFields;
            }
            return resp;
        }

        public MyResponseBase Design_ModularOrFunDomainDetail_EditSave()
        {
            MyResponseBase resp = new MyResponseBase();

            Design_ModularOrFunDomainDetail_Domain();

            #region (2)修改顾客
            using (var scope = new TransactionScope())
            {
                try
                {
                    #region (3)根据功能模块ID查询所有字段
                    var Design_ModularOrFunDomainDetails = Design_ModularOrFunDomainDetail_GetByModularOrFunDomainID();
                    #endregion

                    #region (2)模块字段--数据整理
                    //Item.Design_Design_ModularOrFunDomainDetails.ForEach(p =>
                    //{ p.Design_ModularPageID = Item.Design_ModularPageID; });

                    Item.Items.ForEach(p => p.Design_ModularOrFunDomainID = Item.Design_ModularOrFunDomainID);

                    var deleteIDsEnum = (from p in Design_ModularOrFunDomainDetails select p.Design_ModularOrFunDomainDetailID).Except(from o in Item.Items select o.Design_ModularOrFunDomainDetailID);
                    var updateItems = Item.Items.Where(p => p.Design_ModularOrFunDomainDetailID != null);
                    var addItems = Item.Items.Where(p => p.Design_ModularOrFunDomainDetailID == null);
                    #endregion

                    MyResponseBase resptemp = new MyResponseBase();
                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFunDomainDetail] WHERE  Design_ModularOrFunDomainDetailID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    var DBFieldVals = "";
                    #region (5)更新模块字段
                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFunDomainDetail_Domain();
                        DBFieldVals = "ModularOrFunDomainDetailName,Design_ModularOrFunDomainID,DomainType,DBOperCode,Serial,ParamName";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFunDomainDetail_Domain();
                        DBFieldVals = "ModularOrFunDomainDetailName,Design_ModularOrFunDomainID,DomainType,DBOperCode,Serial,ParamName";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

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
            #endregion

            return resp;
        }


    }
}
