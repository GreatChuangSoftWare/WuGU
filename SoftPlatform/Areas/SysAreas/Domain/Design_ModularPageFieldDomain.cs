
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
    /// 业务层：Design_ModularPageFieldDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularPageField_Domain()
        {
            PKField = "Design_ModularPageFieldID";
            //PKFields = new List<string> { "Design_ModularPageFieldID" };
            TableName = "Design_ModularPageField";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularPageField_PKCheck()
        {
            if (Item.Design_ModularPageFieldID == null)
            {
                throw new Exception("功能模块页面字段主键不能为空！");
            }
        }

        ///// <summary>
        ///// 查询：根据ID查询
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase<Design_ModularPageFieldPremCodeAreaEntity> Design_ModularPageField_GetByID()
        //{
        //    Sys_PremCode_PKCheck();
        //    SelectType = 1;
        //    DBSelectResultType = 4;
        //    OperCode = "Design_ModularPageField.Index";
        //    Querys.Add(new Query { QuryType = 1, FieldName = "Design_ModularPageFieldID", AndOr = " And ", Oper = "=", Value = Item.Design_ModularPageFieldID.ToString() });
        //    resp = Execute();
        //    return resp;
        //}

        #endregion

        /// <summary>
        /// 配置系统--缓存
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularPageField_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            string sql = "SELECT * FROM V_Design_ModularPageField Order By TableInfoSort";
            var resp = Query16(sql, 2);
            return resp.Items;
        }

        /// <summary>
        /// 配置系统--缓存
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFunBtn_ModularPageField_GetAll()
        {
            StringBuilder sbSql = new StringBuilder();
            string sql = "SELECT * FROM V_Design_ModularOrFunBtn_ModularPageField ";
            var resp = Query16(sql, 2);
            return resp.Items;
        }

        /// <summary>
        /// 根据页面ID查询页面字段
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularPageField_Rows()
        {
            MyResponseBase resp = new MyResponseBase();
            //string sql = "SELECT * FROM V_Design_ModularPageField Order By TableInfoSort";
            string sql = string.Format("SELECT * FROM Design_ModularField WHERE  Design_ModularFieldID IN({0}) ", Item.Design_ModularFieldIDs);
            resp = Query16(sql, 2);

            ////(2)查询模块编码字段
            //var ModularFields = Design_ModularField_GetModularPageOrQueryField(Item.Design_ModularOrFunID);
            //resp.Item.Design_ModularFields = ModularFields;
            return resp;
        }

        /// <summary>
        /// 根据功能模块ID：查询页面字段
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularPageField_GetByModularOrFunID()
        {
            string sql = string.Format("SELECT * FROM  V_Design_ModularPageField WHERE  Design_ModularOrFunID={0} ORDER BY TableInfoSort ", Item.Design_ModularOrFunID);
            var resp = Query16(sql, 2);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS ");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunID={0}");
            sb.AppendLine("	UNION ALL");
            sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunParentID={0} AND GroupModularOrFun={1}");
            sb.AppendLine(")");
            sb.AppendLine("SELECT * FROM T0 ORDER BY Sort");
            sql = sb.ToString();
            sql = string.Format(sql, Item.Design_ModularOrFunID, "3");

            //sql = string.Format("SELECT * FROM Design_ModularOrFun WHERE  Design_ModularOrFunParentID={0} ORDER BY Sort ", Item.Design_ModularOrFunID);
            var resp1 = Query16(sql, 2);
            resp.Item.Design_ModularOrFuns = resp1.Items;
            return resp;
        }

        public MyResponseBase Design_ModularPageField_EditListSave()
        {
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };
                    var DBFieldVals = "";

                    #region (3)根据功能模块ID查询所有字段
                    var ModularPageFields = Design_ModularPageField_GetByModularOrFunID().Items;
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Design_ModularPageFields.ForEach(p =>
                    { p.Design_ModularOrFunID = Item.Design_ModularOrFunID; });

                    var deleteIDsEnum = (from p in ModularPageFields select p.Design_ModularPageFieldID).Except(from o in Item.Design_ModularPageFields select o.Design_ModularPageFieldID);
                    var updateItems = Item.Design_ModularPageFields.Where(p => p.Design_ModularPageFieldID != null && !deleteIDsEnum.Contains(p.Design_ModularPageFieldID));
                    var addItems = Item.Design_ModularPageFields.Where(p => p.Design_ModularPageFieldID == null);
                    #endregion
                    MyResponseBase resptemp = new MyResponseBase();
                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularPageField] WHERE  Design_ModularPageFieldID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(";WITH T0 AS ");
                    sb.AppendLine("(");
                    sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunID={0}");
                    sb.AppendLine("	UNION ALL");
                    sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunParentID={0} AND GroupModularOrFun={1}");
                    sb.AppendLine(")");
                    sb.AppendLine("SELECT * FROM T0 ORDER BY Sort");

                    var sql1 = sb.ToString();
                    sql1 = string.Format(sql1, Item.Design_ModularOrFunID, "3");

                    //var sql1 = sb.ToString();
                    //sql1 = string.Format("SELECT * FROM Design_ModularOrFun WHERE  Design_ModularOrFunParentID={0} ORDER BY Sort ", Item.Design_ModularOrFunID);
                    var resp1 = Query16(sql1, 2);
                    //resp.Item.Design_ModularOrFuns = resp1.Items;
                    resp1.Items = resp1.Items.Where(p => p.bFieldsConfigDisp == 1).ToList();
                    var sqlfields = "";
                    for (int m = 0; m < resp1.Items.Count; m++)
                    {
                        #region PageFormEleTypeName
                        if (resp1.Items[m].PageFormEleTypeName == "Page01FormEleType")
                        {
                            sqlfields += "Page01FormEleSort,Page01FormElePos,Page01FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page02FormEleType")
                        {
                            sqlfields += "Page02FormEleSort,Page02FormElePos,Page02FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page03FormEleType")
                        {
                            sqlfields += "Page03FormEleSort,Page03FormElePos,Page03FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page04FormEleType")
                        {
                            sqlfields += "Page04FormEleSort,Page04FormElePos,Page04FormEleType,";

                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page05FormEleType")
                        {
                            sqlfields += "Page05FormEleSort,Page05FormElePos,Page05FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page06FormEleType")
                        {
                            sqlfields += "Page06FormEleSort,Page06FormElePos,Page06FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page07FormEleType")
                        {
                            sqlfields += "Page07FormEleSort,Page07FormElePos,Page07FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page08FormEleType")
                        {
                            sqlfields += "Page08FormEleSort,Page08FormElePos,Page08FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page09FormEleType")
                        {
                            sqlfields += "Page09FormEleSort,Page09FormElePos,Page09FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page10FormEleType")
                        {
                            sqlfields += "Page10FormEleSort,Page10FormElePos,Page10FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page11FormEleType")
                        {
                            sqlfields += "Page11FormEleSort,Page11FormElePos,Page11FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page12FormEleType")
                        {
                            sqlfields += "Page12FormEleSort,Page12FormElePos,Page12FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page13FormEleType")
                        {
                            sqlfields += "Page13FormEleSort,Page13FormElePos,Page13FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page14FormEleType")
                        {
                            sqlfields += "Page14FormEleSort,Page14FormElePos,Page14FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page15FormEleType")
                        {
                            sqlfields += "Page15FormEleSort,Page15FormElePos,Page15FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page16FormEleType")
                        {
                            sqlfields += "Page16FormEleSort,Page16FormElePos,Page16FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page17FormEleType")
                        {
                            sqlfields += "Page17FormEleSort,Page17FormElePos,Page17FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page18FormEleType")
                        {
                            sqlfields += "Page18FormEleSort,Page18FormElePos,Page18FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page19FormEleType")
                        {
                            sqlfields += "Page19FormEleSort,Page19FormElePos,Page19FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page20FormEleType")
                        {
                            sqlfields += "Page20FormEleSort,Page20FormElePos,Page20FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page20FormEleType")
                        {
                            sqlfields += "Page20FormEleSort,Page20FormElePos,Page20FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page21FormEleType")
                        {
                            sqlfields += "Page21FormEleSort,Page21FormElePos,Page21FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page22FormEleType")
                        {
                            sqlfields += "Page22FormEleSort,Page22FormElePos,Page22FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page23FormEleType")
                        {
                            sqlfields += "Page23FormEleSort,Page23FormElePos,Page23FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page24FormEleType")
                        {
                            sqlfields += "Page24FormEleSort,Page24FormElePos,Page24FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page25FormEleType")
                        {
                            sqlfields += "Page25FormEleSort,Page25FormElePos,Page25FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page26FormEleType")
                        {
                            sqlfields += "Page26FormEleSort,Page26FormElePos,Page26FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page27FormEleType")
                        {
                            sqlfields += "Page27FormEleSort,Page27FormElePos,Page27FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page28FormEleType")
                        {
                            sqlfields += "Page28FormEleSort,Page28FormElePos,Page28FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page29FormEleType")
                        {
                            sqlfields += "Page29FormEleSort,Page29FormElePos,Page29FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page30FormEleType")
                        {
                            sqlfields += "Page30FormEleSort,Page30FormElePos,Page30FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page31FormEleType")
                        {
                            sqlfields += "Page31FormEleSort,Page31FormElePos,Page31FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page32FormEleType")
                        {
                            sqlfields += "Page32FormEleSort,Page32FormElePos,Page32FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page33FormEleType")
                        {
                            sqlfields += "Page33FormEleSort,Page33FormElePos,Page33FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page34FormEleType")
                        {
                            sqlfields += "Page34FormEleSort,Page34FormElePos,Page34FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page35FormEleType")
                        {
                            sqlfields += "Page35FormEleSort,Page35FormElePos,Page35FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page36FormEleType")
                        {
                            sqlfields += "Page36FormEleSort,Page36FormElePos,Page36FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page37FormEleType")
                        {
                            sqlfields += "Page37FormEleSort,Page37FormElePos,Page37FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page38FormEleType")
                        {
                            sqlfields += "Page38FormEleSort,Page38FormElePos,Page38FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page39FormEleType")
                        {
                            sqlfields += "Page39FormEleSort,Page39FormElePos,Page39FormEleType,";
                        }
                        else if (resp1.Items[m].PageFormEleTypeName == "Page40FormEleType")
                        {
                            sqlfields += "Page40FormEleSort,Page40FormElePos,Page40FormEleType,";
                        }


                        #endregion

                        #region SortCol
                        if (resp1.Items[m].SortCol == "SortCol01")
                        {
                            sqlfields += "Sort01,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol02")
                        {
                            sqlfields += "Sort02,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol03")
                        {
                            sqlfields += "Sort03,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol04")
                        {
                            sqlfields += "Sort04,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol05")
                        {
                            sqlfields += "Sort05,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol06")
                        {
                            sqlfields += "Sort06,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol07")
                        {
                            sqlfields += "Sort07,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol08")
                        {
                            sqlfields += "Sort08,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol09")
                        {
                            sqlfields += "Sort09,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol10")
                        {
                            sqlfields += "Sort10,";
                        }

                        else if (resp1.Items[m].SortCol == "SortCol11")
                        {
                            sqlfields += "Sort11,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol12")
                        {
                            sqlfields += "Sort12,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol13")
                        {
                            sqlfields += "Sort13,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol14")
                        {
                            sqlfields += "Sort14,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol15")
                        {
                            sqlfields += "Sort15,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol16")
                        {
                            sqlfields += "Sort16,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol17")
                        {
                            sqlfields += "Sort17,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol18")
                        {
                            sqlfields += "Sort18,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol19")
                        {
                            sqlfields += "Sort19,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol20")
                        {
                            sqlfields += "Sort20,";
                        }
                        if (resp1.Items[m].SortCol == "SortCol21")
                        {
                            sqlfields += "Sort21,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol22")
                        {
                            sqlfields += "Sort22,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol23")
                        {
                            sqlfields += "Sort23,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol24")
                        {
                            sqlfields += "Sort24,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol25")
                        {
                            sqlfields += "Sort25,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol26")
                        {
                            sqlfields += "Sort26,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol27")
                        {
                            sqlfields += "Sort27,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol28")
                        {
                            sqlfields += "Sort28,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol29")
                        {
                            sqlfields += "Sort29,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol30")
                        {
                            sqlfields += "Sort30,";
                        }

                        else if (resp1.Items[m].SortCol == "SortCol31")
                        {
                            sqlfields += "Sort31,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol32")
                        {
                            sqlfields += "Sort32,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol33")
                        {
                            sqlfields += "Sort33,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol34")
                        {
                            sqlfields += "Sort34,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol35")
                        {
                            sqlfields += "Sort35,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol36")
                        {
                            sqlfields += "Sort36,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol37")
                        {
                            sqlfields += "Sort37,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol38")
                        {
                            sqlfields += "Sort38,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol39")
                        {
                            sqlfields += "Sort39,";
                        }
                        else if (resp1.Items[m].SortCol == "SortCol40")
                        {
                            sqlfields += "Sort40,";
                        }

                        #endregion

                        #region 查询
                        if (resp1.Items[m].QueryFormEleTypeName == "Query01")
                        {
                            sqlfields += "Query01Pos,Query01FormEleType,Query01QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query02")
                        {
                            sqlfields += "Query02Pos,Query02FormEleType,Query02QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query03")
                        {
                            sqlfields += "Query03Pos,Query03FormEleType,Query03QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query04")
                        {
                            sqlfields += "Query04Pos,Query04FormEleType,Query04QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query05")
                        {
                            sqlfields += "Query05Pos,Query05FormEleType,Query05QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query06")
                        {
                            sqlfields += "Query06Pos,Query06FormEleType,Query06QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query07")
                        {
                            sqlfields += "Query07Pos,Query07FormEleType,Query07QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query08")
                        {
                            sqlfields += "Query08Pos,Query08FormEleType,Query08QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query09")
                        {
                            sqlfields += "Query09Pos,Query09FormEleType,Query09QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query10")
                        {
                            sqlfields += "Query10Pos,Query10FormEleType,Query10QueryType,";
                        }

                        else if (resp1.Items[m].QueryFormEleTypeName == "Query11")
                        {
                            sqlfields += "Query11Pos,Query11FormEleType,Query11QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query12")
                        {
                            sqlfields += "Query12Pos,Query12FormEleType,Query12QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query13")
                        {
                            sqlfields += "Query13Pos,Query13FormEleType,Query13QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query14")
                        {
                            sqlfields += "Query14Pos,Query14FormEleType,Query14QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query15")
                        {
                            sqlfields += "Query15Pos,Query15FormEleType,Query15QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query16")
                        {
                            sqlfields += "Query16Pos,Query16FormEleType,Query16QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query17")
                        {
                            sqlfields += "Query17Pos,Query17FormEleType,Query17QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query18")
                        {
                            sqlfields += "Query18Pos,Query18FormEleType,Query18QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query19")
                        {
                            sqlfields += "Query19Pos,Query19FormEleType,Query19QueryType,";
                        }
                        else if (resp1.Items[m].QueryFormEleTypeName == "Query20")
                        {
                            sqlfields += "Query20Pos,Query20FormEleType,Query20QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query21")
                        {
                            sqlfields += "Query21Pos,Query21FormEleType,Query21QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query22")
                        {
                            sqlfields += "Query22Pos,Query22FormEleType,Query22QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query23")
                        {
                            sqlfields += "Query23Pos,Query23FormEleType,Query23QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query24")
                        {
                            sqlfields += "Query24Pos,Query24FormEleType,Query24QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query25")
                        {
                            sqlfields += "Query25Pos,Query25FormEleType,Query25QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query26")
                        {
                            sqlfields += "Query26Pos,Query26FormEleType,Query26QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query27")
                        {
                            sqlfields += "Query27Pos,Query27FormEleType,Query27QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query28")
                        {
                            sqlfields += "Query28Pos,Query28FormEleType,Query28QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query29")
                        {
                            sqlfields += "Query29Pos,Query29FormEleType,Query29QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query30")
                        {
                            sqlfields += "Query30Pos,Query30FormEleType,Query30QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query31")
                        {
                            sqlfields += "Query31Pos,Query31FormEleType,Query31QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query32")
                        {
                            sqlfields += "Query32Pos,Query32FormEleType,Query32QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query33")
                        {
                            sqlfields += "Query33Pos,Query33FormEleType,Query33QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query34")
                        {
                            sqlfields += "Query34Pos,Query34FormEleType,Query34QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query35")
                        {
                            sqlfields += "Query35Pos,Query35FormEleType,Query35QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query36")
                        {
                            sqlfields += "Query36Pos,Query36FormEleType,Query36QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query37")
                        {
                            sqlfields += "Query37Pos,Query37FormEleType,Query37QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query38")
                        {
                            sqlfields += "Query38Pos,Query38FormEleType,Query38QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query39")
                        {
                            sqlfields += "Query39Pos,Query39FormEleType,Query39QueryType,";
                        }
                        if (resp1.Items[m].QueryFormEleTypeName == "Query40")
                        {
                            sqlfields += "Query40Pos,Query40FormEleType,Query40QueryType,";
                        }


                        #endregion
                    }

                    if (sqlfields.Length > 0)
                    {
                        sqlfields = sqlfields.Trim();
                        sqlfields = sqlfields.Substring(0, sqlfields.Length - 1);
                    }
                    DBFieldVals = "Design_ModularOrFunID,TableInfoSort,FieldFunTypeID,cssclass,calcol,calrow,bTabVer,Design_ModularFieldID,FormEleType,EditAreaName,Design_ModularPageID,AdditionalInfo,";
                    DBFieldVals += sqlfields;
                    #region (5)更新模块字段
                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularPageField_Domain();
                        //DBFieldVals = "Design_ModularOrFunID,TableInfoSort,FieldFunTypeID,cssclass,calcol,calrow,bTabVer,Design_ModularFieldID,FormEleType,EditAreaName,Design_ModularPageID,";
                        //Page01FormEleSort,Page01FormElePos,Page01FormEleType,Page02FormEleSort,Page02FormElePos,Page02FormEleType,Page03FormEleSort,Page03FormElePos,Page03FormEleType,";
                        //DBFieldVals += "Page04FormEleSort,Page04FormElePos, Page04FormEleType,Page05FormEleSort,Page05FormElePos,Page05FormEleType,Page06FormEleSort,Page06FormElePos,Page06FormEleType,";
                        //DBFieldVals += "Page07FormEleSort,Page07FormElePos, Page07FormEleType,Page08FormEleSort,Page08FormElePos,Page08FormEleType,Page09FormEleSort,Page09FormElePos,Page09FormEleType,";
                        //DBFieldVals += "Page10FormEleSort,Page10FormElePos, Page10FormEleType,";
                        //DBFieldVals += "Query01Pos,Query01FormEleType,Query01QueryType,Query02Pos,Query02FormEleType,Query02QueryType,Query03Pos,Query03FormEleType,Query03QueryType,Query04Pos,Query04FormEleType,Query04QueryType,";
                        //DBFieldVals += "Query05Pos, Query05FormEleType,Query05QueryType,Query06Pos,Query06FormEleType,Query06QueryType,Query07Pos,Query07FormEleType,Query07QueryType,Query08Pos,Query08FormEleType,Query08QueryType,Query09Pos,Query09FormEleType,Query09QueryType,Query10Pos,Query10FormEleType,Query10QueryType,";
                        //DBFieldVals += "Sort01,Sort02,Sort03,Sort04,Sort05,Sort06,Sort07,Sort08,Sort09,Sort10";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularPageField_Domain();
                        //DBFieldVals = "Design_ModularOrFunID,TableInfoSort,FieldFunTypeID,cssclass,calcol,calrow,bTabVer,Design_ModularFieldID,FormEleType,Page01FormEleSort,Page01FormElePos,Page01FormEleType,Page02FormEleSort,Page02FormElePos,Page02FormEleType,Page03FormEleSort,Page03FormElePos,Page03FormEleType,";
                        //DBFieldVals += "Page04FormEleSort,Page04FormElePos, Page04FormEleType,Page05FormEleSort,Page05FormElePos,Page05FormEleType,Page06FormEleSort,Page06FormElePos,Page06FormEleType,";
                        //DBFieldVals += "Page07FormEleSort,Page07FormElePos, Page07FormEleType,Page08FormEleSort,Page08FormElePos,Page08FormEleType,Page09FormEleSort,Page09FormElePos,Page09FormEleType,";
                        //DBFieldVals += "Page10FormEleSort,Page10FormElePos, Page10FormEleType,EditAreaName,Design_ModularPageID,";
                        //DBFieldVals += "Query01Pos,Query01FormEleType,Query01QueryType,Query02Pos,Query02FormEleType,Query02QueryType,Query03Pos,Query03FormEleType,Query03QueryType,Query04Pos,Query04FormEleType,Query04QueryType,";
                        //DBFieldVals += "Query05Pos, Query05FormEleType,Query05QueryType,Query06Pos,Query06FormEleType,Query06QueryType,Query07Pos,Query07FormEleType,Query07QueryType,Query08Pos,Query08FormEleType,Query08QueryType,Query09Pos,Query09FormEleType,Query09QueryType,Query10Pos,Query10FormEleType,Query10QueryType,";
                        //DBFieldVals += "Sort01,Sort02,Sort03,Sort04,Sort05,Sort06,Sort07,Sort08,Sort09,Sort10";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion

                    ProjectCache.Design_ModularPageFields_Clear();
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

        /// <summary>
        /// 生成页面记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularPageField_BulidRecord()
        {
            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            var sbsql = new StringBuilder();
            string sql = "";
            //sbsql.AppendLine("SELECT 1 Design_ModularFieldID,1 TableInfoSort");
            //sbsql.AppendLine("UNION ALL");
            //sbsql.AppendLine("SELECT 3,100");
            //sbsql.AppendLine("UNION ALL");
            //sbsql.AppendLine("SELECT Design_ModularFieldID,Sort+2 TableInfoSort");
            //sbsql.AppendLine("FROM Design_ModularField");
            //sbsql.AppendLine("WHERE Design_ModularOrFunID={0}");
            //sbsql.AppendLine("ORDER BY TableInfoSort");

            sbsql.AppendLine(";WITH T0 AS");
            sbsql.AppendLine("(");
            sbsql.AppendLine("	SELECT 1 Design_ModularFieldID,1 TableInfoSort");
            sbsql.AppendLine("	UNION ALL");
            sbsql.AppendLine("	SELECT 3,100");
            sbsql.AppendLine("	UNION ALL");
            sbsql.AppendLine("	SELECT Design_ModularFieldID,Sort+2 TableInfoSort");
            sbsql.AppendLine("	FROM Design_ModularField");
            sbsql.AppendLine("	WHERE Design_ModularOrFunID={0}");
            //sbsql.AppendLine("	ORDER BY TableInfoSort");
            sbsql.AppendLine(")");
            sbsql.AppendLine("INSERT INTO Design_ModularPageField(Design_ModularOrFunID,Design_ModularFieldID,TableInfoSort)");
            sbsql.AppendLine("SELECT {0},*");
            sbsql.AppendLine("FROM T0  ");
            sbsql.AppendLine("ORDER BY TableInfoSort");
            sql = sbsql.ToString();
            sql = string.Format(sql, Item.Design_ModularOrFunID);
            Query16(sql, 1);
            return resp;
        }

    }
}
