
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
    /// 业务层：Pre_Company(公司管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 添加保存，包含添加企业管理员信息
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_Company_AddSave()
        {
            var resp = new MyResponseBase();
            if (Item.Ba_AreaID1 != null)
            {
                Item.AreaName1 = SoftProjectAreaEntityDomain.Ba_Area_GetByAreaID(Item.Ba_AreaID1).AreaName;
                Item.AreaName = Item.AreaName1;
            }
            if (Item.Ba_AreaID2 != null)
            {
                Item.AreaName2 = SoftProjectAreaEntityDomain.Ba_Area_GetByAreaID(Item.Ba_AreaID2).AreaName;
                Item.AreaName += Item.AreaName2;
            }
            if (Item.Ba_AreaID3 != null)
            {
                Item.AreaName3 = SoftProjectAreaEntityDomain.Ba_Area_GetByAreaID(Item.Ba_AreaID3).AreaName;
                Item.AreaName += Item.AreaName3;
            }
            //经营项目：字符串
            if (!string.IsNullOrEmpty(Item.OperatingItemIDs))// != null && Item.OperatingItemIDs.Count > 0)
            {
                Item.OperatingItemIDs = Item.OperatingItemIDs.Substring(0, Item.OperatingItemIDs.Length - 1);
                var arrs=Item.OperatingItemIDs.Split(',');
                var OperatingItemNames="";
                foreach (var s in arrs)
                { 
                    OperatingItemNames += SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.P_CategoryID.ToString() == s).FirstOrDefault().PCategoryName+",";
                }
                Item.OperatingItemName = OperatingItemNames.Substring(0,OperatingItemNames.Length-1);
            }

            resp = ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                Item.Pre_CompanyID = resp.Item.Pre_CompanyID;
                Pre_User_AddAdminSave();
            }));
            return resp;
        }

        /// <summary>
        /// 编辑保存,包含更新企业管理员信息
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Pre_Company_EditSave()
        {
            var resp = new MyResponseBase();
            #region 数据处理

            if (Item.Ba_AreaID1 != null)
            {
                Item.AreaName1 = SoftProjectAreaEntityDomain.Ba_Area_GetByAreaID(Item.Ba_AreaID1).AreaName;
                Item.AreaName = Item.AreaName1;
            }
            if (Item.Ba_AreaID2 != null)
            {
                Item.AreaName2 = SoftProjectAreaEntityDomain.Ba_Area_GetByAreaID(Item.Ba_AreaID2).AreaName;
                Item.AreaName += Item.AreaName2;
            }
            if (Item.Ba_AreaID3 != null)
            {
                Item.AreaName3 = SoftProjectAreaEntityDomain.Ba_Area_GetByAreaID(Item.Ba_AreaID3).AreaName;
                Item.AreaName += Item.AreaName3;
            }
            //经营项目：字符串
            if (!string.IsNullOrEmpty(Item.OperatingItemIDs))// != null && Item.OperatingItemIDs.Count > 0)
            {
                Item.OperatingItemIDs = Item.OperatingItemIDs.Substring(0, Item.OperatingItemIDs.Length - 1);
                var arrs = Item.OperatingItemIDs.Split(',');
                var OperatingItemNames = "";
                foreach (var s in arrs)
                {
                    OperatingItemNames += SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.P_CategoryID.ToString() == s).FirstOrDefault().PCategoryName + ",";
                }
                Item.OperatingItemName = OperatingItemNames.Substring(0, OperatingItemNames.Length - 1);
            }
            #endregion

            //更新加盟商零售价格表的商品为有效值
            var sqlProductPrice = ProjectCache.Sys_HOperControls.Where(p => p.OperCode == "Pre_Company.Fra_ProductPriceByUpdateBValidate").FirstOrDefault().DBTSql;
            sqlProductPrice = string.Format(sqlProductPrice, Item.Pre_CompanyID, Item.OperatingItemIDs);
            //更新加盟商的合作商价格表的商品为有效值
            var sqlPartnerProductPrice = ProjectCache.Sys_HOperControls.Where(p => p.OperCode == "Pre_Company.BC_PartnerProductPriceByUpdateBValidate").FirstOrDefault().DBTSql;
            sqlPartnerProductPrice = string.Format(sqlPartnerProductPrice, Item.Pre_CompanyID, Item.OperatingItemIDs);

            resp = ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = EditSaveNotTran();
                Pre_User_EditAdminSave();
                Query16(sqlProductPrice, 1);//更新加盟商零售价格表中，商品有效性。
                Query16(sqlPartnerProductPrice, 1);//更新加盟商的合作商价格表中，商品有效性。
            }));
            return resp;
        }

        /// <summary>
        /// 多选下拉列表框
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NameCn"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static string DropDownListMultiSelect_OperatingItemIDs(string val, string NameCn, SoftProjectAreaEntity Item)
        {
            var OperatingItemIDArrs = new List<string>().ToArray();
            if (!string.IsNullOrEmpty(Item.OperatingItemIDs))
            {
                OperatingItemIDArrs = Item.OperatingItemIDs.Split(',');
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<select id='Item_OperatingItemIDs' name='Item.OperatingItemIDs' multiple='multiple' class='multiselect ' >"));

            var items = SoftProjectAreaEntityDomain.P_Categorys.Where(p => p.ParentP_CategoryID == 0);
            foreach (var item in items)
            {
                if (OperatingItemIDArrs.Contains(item.P_CategoryID.ToString()))
                    sb.AppendLine(string.Format("<option value='{0}' selected>{1}</option>", item.P_CategoryID, item.PCategoryName));//<input class='ck-align' " + disabled + " name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' checked='checked' />" + text.ToString() + "</label>&nbsp;&nbsp;");
                else
                    sb.AppendLine(string.Format("<option value='{0}'>{1}</option>", item.P_CategoryID, item.PCategoryName)); ;// sb.AppendLine("<label class='checkbox-inline'><input class='ck-align' " + disabled + "name='Item." + name + "' type='checkbox' value='" + value.ToString() + "' />" + text.ToString() + "</label>&nbsp;&nbsp;");
            }
            sb.AppendLine("</select>");
            return sb.ToString();
        }

    }
}
