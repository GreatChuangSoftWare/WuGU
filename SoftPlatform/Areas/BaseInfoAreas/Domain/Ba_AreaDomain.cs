
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
    /// 业务层：Ba_Area
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {

        /// <summary>
        /// 为缓存
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Ba_Area_GetAll()
        {
            var resp = new MyResponseBase();

            StringBuilder sbSql = new StringBuilder();
            string sql = "SELECT * FROM Ba_Area Order By AreaCode";
            resp = Query16(sql, 2);
            return resp.Items;
        }

        #region #区域

        static List<SoftProjectAreaEntity> _Ba_Areas = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Ba_Areas
        {
            get
            {
                if (_Ba_Areas.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Ba_Areas = domain.Ba_Area_GetAll();
                }
                return _Ba_Areas;
            }
        }

        /// <summary>
        /// 获取所有1级区域
        /// </summary>
        public static List<SoftProjectAreaEntity> Ba_Area_AreaID1s
        {
            get
            {
                var lists = Ba_Areas.Where(p => p.AreaParentCode == "0");
                return lists.ToList();
            }
        }

        /// <summary>
        /// 根据ID，获取对应ID父区域下的子区域
        /// </summary>
        /// <param name="Ba_AreaID"></param>
        /// <returns></returns>
        public static List<SoftProjectAreaEntity> Ba_Area_GetBrotherBa_AreaIDss(int? Ba_AreaID)
        {
            var item = Ba_Areas.Where(p => p.Ba_AreaID == Ba_AreaID).First();

            var lists = Ba_Areas.Where(p => p.AreaParentCode == item.AreaParentCode);
            return lists.ToList();
        }

        /// <summary>
        /// 根据ID，查询所有子区域
        /// </summary>
        /// <param name="Ba_AreaID"></param>
        /// <returns></returns>
        public static List<SoftProjectAreaEntity> Ba_Area_GetSubBa_AreaIDss(int? Ba_AreaID)
        {
            var item = Ba_Areas.Where(p => p.Ba_AreaID == Ba_AreaID).First();

            var lists = _Ba_Areas.Where(p => p.AreaParentCode == item.AreaCode);
            return lists.ToList();
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="Ba_AreaID"></param>
        /// <returns></returns>
        public static SoftProjectAreaEntity Ba_Area_GetByAreaID(int? Ba_AreaID)
        {
            var item = Ba_Areas.Where(p => p.Ba_AreaID == Ba_AreaID).First();

            return item;
        }


        #endregion

        #region 1级

        public static string QueryHtmlDropDownList_Ba_AreaID1(string val, string NameCn, SoftProjectAreaEntity item)
        {
            var str = HtmlHelpers.DropDownList(null, "Ba_AreaID1___equal", SoftProjectAreaEntityDomain.Ba_Area_AreaID1s, "Ba_AreaID", "AreaName",
                val, "", "==省(市)==", "form-control",
                "  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs1s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Ba_AreaID2___equal' data-optionlabel='市(区、县)' ");
            return str.ToString();
        }

        public static string HtmlDropDownLiss_Ba_AreaID1(string val, string NameCn, SoftProjectAreaEntity item)
        {
            var str = HtmlHelpers.DropDownList(null, "Item.Ba_AreaID1", SoftProjectAreaEntityDomain.Ba_Area_AreaID1s, "Ba_AreaID", "AreaName",
                        val, "", "", "form-control",
                        "  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs1s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Item_Ba_AreaID2'  ");
            return str.ToString();
        }

        #endregion

        #region 2级

        public static string QueryHtmlDropDownList_Ba_AreaID2(string val, string NameCn, SoftProjectAreaEntity item)
        {
            if (!string.IsNullOrEmpty(val))
            {//data-optionlabel='市(区、县)'
                var Ba_AreaID = Convert.ToInt32(val);
                var str = HtmlHelpers.DropDownList(null, "Ba_AreaID2___equal", SoftProjectAreaEntityDomain.Ba_Area_GetBrotherBa_AreaIDss(Ba_AreaID), "Ba_AreaID", "AreaName",
                    val, "", "==市(区、县)==", "form-control",
                    "  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Ba_AreaID3___equal' data-optionlabel='市(区、县)' ");
                return str.ToString();
            }
            else
            {
                //var str = string.Format("<select id='Item_{0}' name='Item.{0}' class='form-control'  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Item_Ba_AreaID3' ><option value='' selected=''>==市(区、县)==</option></select>", field.QueryEn);
                var str = string.Format("<select id='Ba_AreaID2___equal' name='Ba_AreaID2___equal' class='form-control'  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Ba_AreaID3___equal' data-optionlabel='市(区、县)'><option value=''>==市(区、县)==</option></select>");
                return str.ToString();
            }
        }

        public static string HtmlDropDownLiss_Ba_AreaID2(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region 2级区域
            if (!string.IsNullOrEmpty(val))
            {//data-optionlabel='市(区、县)'
                var Ba_AreaID = Convert.ToInt32(val);
                var str = HtmlHelpers.DropDownList(null, "Item.Ba_AreaID2", SoftProjectAreaEntityDomain.Ba_Area_GetBrotherBa_AreaIDss(Ba_AreaID), "Ba_AreaID", "AreaName",
                    val, "", "", "form-control",
                    "  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Item_Ba_AreaID3' ");
                return str.ToString();
            }
            else
            {
                //var str = string.Format("<select id='Item_{0}' name='Item.{0}' class='form-control'  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Item_Ba_AreaID3' ><option value='' selected=''>==市(区、县)==</option></select>", field.QueryEn);
                var str = string.Format("<select id='Item_Ba_AreaID2' name='Item.Ba_AreaID2' class='form-control'  data-changeurl='/BaseInfoAreas/Ba_Area/GetSubBa_AreaIDs2s' data-textfield='AreaName' data-valuefield='Ba_AreaID' data-targetdom='#Item_Ba_AreaID3' ></select>");
                return str.ToString();
            }
            #endregion
        }

        #endregion

        #region 3级

        public static string QueryHtmlDropDownList_Ba_AreaID3(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region Ba_AreaID3
            if (!string.IsNullOrEmpty(val))
            {//==市(区、县)==
                var Ba_AreaID = Convert.ToInt32(val);
                var str = HtmlHelpers.DropDownList(null, "Ba_AreaID3___equal", SoftProjectAreaEntityDomain.Ba_Area_GetBrotherBa_AreaIDss(Ba_AreaID), "Ba_AreaID", "AreaName",
                    val, "", "==市(区、县)==", "form-control");
                return str.ToString();
            }
            else
            {//<option value='' selected=''>==市(区、县)==</option>
                var str = string.Format("<select id='Ba_AreaID3___equal' name='Ba_AreaID3___equal' class='form-control' ><option value=''>==市(区、县)==</option></select>");
                return str.ToString();
            }
            #endregion
        }

        public static string HtmlDropDownLiss_Ba_AreaID3(string val, string NameCn, SoftProjectAreaEntity item)
        {
            #region Ba_AreaID3
            if (!string.IsNullOrEmpty(val))
            {//==市(区、县)==
                var Ba_AreaID = Convert.ToInt32(val);
                var str = HtmlHelpers.DropDownList(null, "Item.Ba_AreaID3", SoftProjectAreaEntityDomain.Ba_Area_GetBrotherBa_AreaIDss(Ba_AreaID), "Ba_AreaID", "AreaName",
                    val, "", "", "form-control");
                return str.ToString();
            }
            else
            {//<option value='' selected=''>==市(区、县)==</option>
                var str = string.Format("<select id='Item_Ba_AreaID3' name='Item.Ba_AreaID3' class='form-control' ></select>");
                return str.ToString();
            }
            #endregion

            //必须知道是公司、企业、顾客功能，从而进行过滤
            //var Pre_RolesTemp = Pre_Roles.Where(p => p.LoginCategoryID == item.LoginCategoryID);
            //var str = HtmlHelpers.DropDownList(null, "Item.Pre_RoleID", Pre_RolesTemp, "Pre_RoleID", "RoleName", val, "");
            //return str.ToString();
            //return "";
        }

        #endregion
    }
}
