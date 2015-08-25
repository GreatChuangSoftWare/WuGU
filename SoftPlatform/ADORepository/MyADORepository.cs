
using Framework.Core;
using Framework.Web.Mvc.ADORepository;
using SoftProject.CellModel;
using SoftProject.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Framework.Web.Mvc
{
    public class MyADORepository
    {
        SoftProjectAreaEntityDomain domain;

        public MyADORepository(SoftProjectAreaEntityDomain domain)
        {
            this.domain = domain;
        }

        public MyADORepository()
        { }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModularOrFunCode { get; set; }

        #region IRepository<T> Members

        //#region 持久化

        public void Execute(MyResponseBase response)
        {
            SoftProjectAreaEntity moci = domain.Sys_HOperControl;
            Tuple<string, SqlParameter[]> vals = null;
            //MyResponseBase<CellT> response = new MyResponseBase<CellT>();
            try
            {
                switch (moci.DBOperType)
                {
                    case 1://Insert
                        #region 插入
                        vals = SqlTool.BulidInsertParas(domain);
                        var UID = SqlHelper.ExecuteScalar(ProviderHelper.ConnectionString, CommandType.Text, vals.Item1, vals.Item2);
                        if (UID != null)
                        {
                            var type = domain.Item.GetType();
                            string tabName = type.Name;

                            PropertyInfo propertyInfo = type.GetProperty(domain.PKField);//PKFields[0]);
                            //domain.Item = new CellT();
                            propertyInfo.SetValue(domain.Item, Convert.ToInt32(UID), null);
                            response.Item = domain.Item;
                        }
                        #endregion
                        break;
                    case 2://Update
                        #region 更新
                        vals = SqlTool.BulidUpdateParas(domain);
                        SqlHelper.ExecuteScalar(ProviderHelper.ConnectionString, CommandType.Text, vals.Item1, vals.Item2);
                        response.Item = domain.Item;
                        #endregion
                        break;
                    case 4://Delete
                        vals = SqlTool.BulidDeleteParas(domain);
                        response.Obj = SqlHelper.ExecuteNonQuery(ProviderHelper.ConnectionString, CommandType.Text, vals.Item1, vals.Item2);
                        break;
                    case 8://查询
                        #region 128
                        vals = SqlTool.BulidBaseSqlParas128(domain);
                        var SelectSubType = domain.SelectSubType;
                        if (SelectSubType == null)
                            SelectSubType = moci.SelectSubType;
                        var strCal = "";
                        if (domain.bCal==1)//计算
                        {
                            strCal = SqlTool.AppendTotal(vals.Item1,domain.ModularOrFunCode);
                        }
                        else if((SelectSubType & 4)==4)
                        {
                            strCal = SqlTool.AppendCount(vals.Item1);                            
                        }
                        if (strCal.Length>0)//如果分页，则计算记录总数,或者合计
                        {
                            //var sql = SqlTool.AppendCount(vals.Item1);// SqlTool.BulidSelectPageByTotalItemsParas<CellT>(domain);
                            //vals = Tuple.Create<string, SqlParameter[]>(sql, vals.Item2);
                            var xx = vals.Item2;
                            Select(strCal, xx, 1, 4, response);
                            #region 分页处理
                            ///////////////////////
                            #region 获取总数值
                            var type = domain.Item.GetType();
                            PropertyInfo propertyInfo = type.GetProperty("TotalItems");
                            //domain.Item = new CellT();
                            var TotalItems=propertyInfo.GetValue(response.Item);//.SetValue(domain.Item, Convert.ToInt32(UID), null);
                            ///////////////////////
                            response.PageQueryBase = domain.PageQueryBase;
                            response.PageQueryBase.TotalItems = (int)TotalItems;

                            if (response.PageQueryBase.TotalPages < response.PageQueryBase.PageIndex)
                                response.PageQueryBase.PageIndex = response.PageQueryBase.TotalPages;
                            #endregion
                            response.ItemTotal = response.Item;
                            #endregion
                        }

                        #region SelectType说明
                        //--如果为1：select * from t1000--不分页、不排序
                        //--    2:                       --不分页、排序
                        //--如果为4：select * from tp1000--分页--一定要排序
                        //--如果为8：select * from tcal1000--计算
                        //--如果为9：不分页+不排序、计算
                        //--		   select * from t1000
                        //--			select * from tcal1000
                        //--如果为10：不分页、排序、计算
                        //--如果为14：分页+计算
                        //--			select * from tp1000
                        //--			union all
                        //--			select * from tcal1000
                        #endregion

                        var strsql128 = vals.Item1;
                        #region 追加排序、分页语句
                        if ((SelectSubType & 2) == 2)//排序,TO1000
                        {
                            strsql128 = SqlTool.AppendOrder(strsql128, domain.PageQueryBase);
                        }

                        if ((SelectSubType & 4) == 4)//分页TP1000
                        {
                            strsql128 = SqlTool.AppendPage(strsql128, domain.PageQueryBase);
                        }
                        #endregion

                        #region 根据功能拼接 最后语句
                        //select * from T1000
                        var paramleng = vals.Item2.Count();
                        //SqlParameter[] paras =new SqlParameter[paramleng]{};// List<SqlParameter>();

                        var sqlparams = new List<SqlParameter>();
                        //sqlparams=vals.Item2.AsEnumerable().ToList();//.CopyTo(sqlparams, 0L);
                        //var paras = sqlparams.ToArray();
                        //vals.Item2 =new SqlParameter();

                        for (var i = 0; i < vals.Item2.Count(); i++)
                        { 
                            sqlparams.Add(new SqlParameter(vals.Item2[i].ParameterName,vals.Item2[i].Value));
                        }
                        var paras = sqlparams.ToArray();
                        if (SelectSubType == 1)//不排序+不分页
                        {
                            strsql128 += "\n SELECT * FROM T1000";
                            vals = Tuple.Create<string, SqlParameter[]>(strsql128, paras);
                        }
                        else if (SelectSubType == 2)//排序+不分页
                        {
                            strsql128 += "\n SELECT * FROM TO1000";
                            vals = Tuple.Create<string, SqlParameter[]>(strsql128, paras);
                        }
                        else if (SelectSubType == 6)//分页(包含排序)
                        {
                            strsql128 += "SELECT * FROM TP1000";

                            vals = Tuple.Create<string, SqlParameter[]>(strsql128, paras);
                        }
                        else
                        {
                            strsql128 += "SELECT * FROM T1000";
                            vals = Tuple.Create<string, SqlParameter[]>(strsql128, paras);
                        }
                        //else if (moci.SelectSubType == 8)//计算
                        //{
                        //    strsql128 += "\n SELECT * FROM TCal1000";
                        //    vals = Tuple.Create<string, SqlParameter[]>(strsql128, vals.Item2);
                        //}
                        //else if (moci.SelectSubType == 9)//不排序+不分页+计算(1+8)
                        //{
                        //    strsql128 += "\n SELECT * FROM TCal1000  \n UNION ALL \n SELECT * FROM T1000";
                        //    vals = Tuple.Create<string, SqlParameter[]>(strsql128, vals.Item2);
                        //}
                        //else if (moci.SelectSubType == 10)//排序+不分页计算(2+8)
                        //{
                        //    strsql128 += "\n SELECT * FROM TCal1000 \n UNION ALL \n SELECT * FROM TO1000  ";
                        //    vals = Tuple.Create<string, SqlParameter[]>(strsql128, vals.Item2);
                        //}
                        //else if (moci.SelectSubType == 14)//分页(包含排序)+计算(6+8)
                        //{
                        //    strsql128 += "\n SELECT * FROM TCal1000  \n UNION ALL \n SELECT * FROM TP1000";
                        //    vals = Tuple.Create<string, SqlParameter[]>(strsql128, vals.Item2);
                        //}
                        #endregion
                        Select(vals.Item1, vals.Item2, 1, (int)moci.DBSelectResultType, response);
                        #endregion
                        break;
                    case 16:
                        vals = SqlTool.BulidSqlItemsParas16(domain);
                        Select(vals.Item1, vals.Item2, 1, (int)moci.DBSelectResultType, response);
                        break;
                }
            }
            catch (Exception ex)
            {
                var validationInfo = new ValidationInfo(null)
                {
                    FieldName = "",
                    Title = domain.Sys_HOperControl.OperName,
                    Message = string.Format("{0}:{1}",
                        domain.Sys_HOperControl.OperName + "操作失败", ex.Message)
                };
                var TupleSql = vals;
                throw new Exception(string.Format("{0}:{1}", domain.Sys_HOperControl.OperName + "操作失败", ex.Message));
            }
        }

        public void Select(string TSql, SqlParameter[] paras, int commandType, int DBSelectResultType, MyResponseBase response)
        {
            if (DBSelectResultType == 1)//标量查询
            {
                response.Obj = SqlHelper.ExecuteScalar(ProviderHelper.ConnectionString, CommandType.Text, TSql, paras);
            }
            else if (DBSelectResultType == 2)//集合
            {
                List<SoftProjectAreaEntity> CellTs = SelectCells(CommandType.Text, TSql, paras);
                response.Items = CellTs;
            }
            else if (DBSelectResultType == 4)//单个对象
            {
                List<SoftProjectAreaEntity> CellTs = SelectCells(CommandType.Text, TSql, paras);
                if (CellTs.Count > 0)
                    response.Item = CellTs.FirstOrDefault();
                else
                    response.Item = new SoftProjectAreaEntity();
            }
            else
            {
                DataTable dt = SqlHelper.ExecuteDataTable(ProviderHelper.ConnectionString, CommandType.Text, TSql, paras);
                response.DataTable = dt;
            }
        }

        public List<SoftProjectAreaEntity> SelectCells(CommandType CommandType, string TSql, SqlParameter[] paras)
        {
            List<SoftProjectAreaEntity> CellTs = new List<SoftProjectAreaEntity>();

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(ProviderHelper.ConnectionString,
                    CommandType, TSql, paras))
            {
                while (sdr.Read())
                {
                    SoftProjectAreaEntity cellT = SqlTool.Read<SoftProjectAreaEntity>(new SafeDataReader(sdr));
                    CellTs.Add(cellT);
                }
            }
            return CellTs;
        }

        //public void IUHelper(string TSql, SqlParameter[] paras, int CommandType1, int DBSelectResultType, Sys_HOperControl moci, MyResponseBase<CellT> response)
        //{
        //    var UID = SqlHelper.ExecuteScalar(ProviderHelper.ConnectionString, CommandType.Text, TSql, paras);
        //    if (UID != null)
        //    {
        //        var type = domain.Item.GetType();
        //        string tabName = type.Name;

        //        PropertyInfo propertyInfo = type.GetProperty(domain.PKFields[0]);
        //        //domain.Item = new CellT();
        //        propertyInfo.SetValue(domain.Item, Convert.ToInt32(UID), null);
        //        response.Item = domain.Item;
        //    }
        //}

        //public void GetByID(Sys_HOperControl moci, MyResponseBase<CellT> response)
        //{
        //    var vals = SqlTool.BulidPKQueryParas<CellT>(new List<string>().ToArray(), domain.Item);
        //    Select(vals.Item1, vals.Item2, CommandType.Text, 3, response);
        //}

        //public bool IUD(DMT DMItem, ResponseBase<CellVT> response)
        //{
        //    try
        //    {
        //        Tuple<string, SqlParameter[]> vals=null;
        //        string DBOperType=DMItem.OperEnumInfo.DBOperType;
        //        if (DBOperType == "Insert")
        //        {
        //            vals = SqlTool.BulidAddParas<DMT, CellT, CellVT>(DMItem);
        //        }
        //        else if (DBOperType == "Update")
        //        {
        //            vals = SqlTool.BulidModiParas<DMT, CellT, CellVT>(DMItem);
        //        }
        //        else
        //            vals = SqlTool.BulidDeleteParas<DMT, CellT, CellVT>(DMItem);

        //        var ID = SqlHelper.ExecuteScalar(ProviderHelper.ConnectionString, CommandType.Text, vals.Item1, vals.Item2);
        //        if (ID != null)
        //        {
        //            ResponseBase<CellVT> responseDal = new ResponseBase<CellVT>();
        //            GetViewByID(DMItem, responseDal);

        //            response.Item = responseDal.Item;
        //            response.RespAttachInfo.Merg(responseDal.RespAttachInfo);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ValidationInfo validateInfo = new ValidationInfo(DMItem.DMMarkVals) { Title = string.Format("{0}{1}的\"{2}\"出错", DMItem.OperNameCn, DMItem.EntityNameCn, DMItem.EntityItemMarkMess), Message = ex.Message };
        //        response.RespAttachInfo.ValidationErrors.Add(validateInfo);
        //        return false;
        //    }
        //    return true;
        //}

        #endregion

        ///// <summary>
        ///// 根据主键获取对象
        ///// </summary>
        ///// <param name="DMItem"></param>
        ///// <param name="resp"></param>
        //public void GetByID(DMT DMItem, ResponseBase<CellT> resp)
        //{
        //    List<SqlParameter> sqlparams = new List<SqlParameter>();

        //    var vals = SqlTool.BulidPKQueryParas<CellT>(DMItem.EntityPKs.ToArray(), DMItem.Item);

        //    CellVT cellVT = new CellVT();
        //    CellT cellT = new CellT();
        //    try
        //    {
        //        using (SqlDataReader sdr = SqlHelper.ExecuteReader(ProviderHelper.ConnectionString,
        //            System.Data.CommandType.Text, vals.Item1, vals.Item2))
        //        {
        //            while (sdr.Read())
        //            {
        //                cellT = SqlTool.Read<CellT>(new SafeDataReader(sdr));
        //            }
        //        }
        //        resp.Item = cellT;
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.RespAttachInfo.ValidationErrors.Add(new ValidationInfo(null) { Title = DMItem.OperNameCn, Message = ex.Message });
        //    }
        //}

        ///// <summary>
        ///// 根据主键获取对象并将格式进行转换
        ///// </summary>
        ///// <param name="DMItem"></param>
        ///// <param name="resp"></param>
        //public void GetViewByID(DMT DMItem, ResponseBase<CellVT> resp)
        //{
        //    ResponseBase<CellT> resp1 = new ResponseBase<CellT>();
        //    GetByID(DMItem, resp1);
        //    resp.Item = new CellVT { CellTItem = resp1.Item };
        //    resp.RespAttachInfo.Merg(resp1.RespAttachInfo);
        //}
    }

    //public class MyADORepository
    //{
    //    public MyADORepository()
    //    { }

    //    public static void Select<CellT>(Tuple<string, SqlParameter[]> vals, int commandType, int DBSelectResultType, MyResponseBase response)
    //        where CellT : new()
    //    {
    //        string TSql = vals.Item1;
    //        SqlParameter[] paras = vals.Item2;

    //        if (DBSelectResultType == 1)//标量查询
    //        {
    //            response.Obj = SqlHelper.ExecuteScalar(ProviderHelper.ConnectionString, CommandType.Text, TSql, paras);
    //        }
    //        else if (DBSelectResultType == 2)//集合
    //        {
    //            List<CellT> CellTs = SelectCells<CellT>(CommandType.Text, TSql, paras);
    //            response.Items = CellTs;
    //        }
    //        else if (DBSelectResultType == 4)//单个对象
    //        {
    //            List<CellT> CellTs = SelectCells<CellT>(CommandType.Text, TSql, paras);
    //            if (CellTs.Count > 0)
    //                response.Item = CellTs.FirstOrDefault();
    //            else
    //                response.Item = default(CellT);
    //        }
    //        else
    //        {
    //            DataTable dt = SqlHelper.ExecuteDataTable(ProviderHelper.ConnectionString, CommandType.Text, TSql, paras);
    //            response.DataTable = dt;
    //        }
    //    }

    //    public static List<CellT> SelectCells<CellT>(CommandType CommandType, string TSql, SqlParameter[] paras)
    //        where CellT : new()
    //    {
    //        List<CellT> CellTs = new List<CellT>();

    //        using (SqlDataReader sdr = SqlHelper.ExecuteReader(ProviderHelper.ConnectionString,
    //                CommandType, TSql, paras))
    //        {
    //            while (sdr.Read())
    //            {
    //                CellT cellT = SqlTool.Read<CellT>(new SafeDataReader(sdr));
    //                CellTs.Add(cellT);
    //            }
    //        }
    //        return CellTs;
    //    }
    //}
}