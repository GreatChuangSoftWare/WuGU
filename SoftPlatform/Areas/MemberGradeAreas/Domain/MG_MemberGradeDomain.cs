
using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using SoftProject.CellModel;
using System.Web;

//namespace Framework.Web.Mvc
namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：MG_MemberGrade(会员级别管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 编辑保存,包含更新企业管理员信息
        /// </summary>
        /// <returns></returns>
        public MyResponseBase MG_MemberGrade_AddSave()
        {
            var resp = new MyResponseBase();
            var bAddProduct = true;//是否添加加盟商零售价格商品

            //添加加盟商零售价格表的商品并设置有效值
            //(1)查询Sql语句
            var sqlFra_ProductPriceAddProduct = ProjectCache.Sys_HOperControls.Where(p => p.OperCode == "MG_MemberGrade.Fra_ProductPriceAddProduct").FirstOrDefault().DBTSql;
            //(2)查找加盟商授权的商品类别
            var sql = string.Format("SELECT * FROM  Pre_Company  WHERE Pre_CompanyID={0}", Item.Pre_CompanyID);
            var OperatingItemIDs = Query16(sql, 4).Item.OperatingItemIDs;
            if(string.IsNullOrEmpty( OperatingItemIDs))
                bAddProduct=false;
            ExecuteDelegate(new Action<SoftProjectAreaEntityDomain>(p =>
            {
                resp = AddSaveNotTran();
                if(bAddProduct)
                {
                    sqlFra_ProductPriceAddProduct = string.Format(sqlFra_ProductPriceAddProduct, Item.Pre_CompanyID,resp.Item.MG_MemberGradeID, OperatingItemIDs);
                    Query16(sqlFra_ProductPriceAddProduct,1);
                }
            }));
            return resp;
        }


        #region 缓存、界面元素

        public void MG_MemberGrade_AddCache()
        {
            #region 更新：用户缓存

            ModularOrFunCode = "MemberGradeAreas.MG_MemberGrade.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            SoftProjectAreaEntityDomain.MG_MemberGrades.Add(resp.Item);

            #endregion
        }

        public void MG_MemberGrade_UpdateCache()
        {
            #region (3)根据ID查询，替换

            ModularOrFunCode = "MemberGradeAreas.MG_MemberGrade.Index";
            Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            resp = ByID();
            var MG_MemberGrade = SoftProjectAreaEntityDomain.MG_MemberGrades.Where(p => p.MG_MemberGradeID == Item.MG_MemberGradeID).FirstOrDefault();

            SoftProjectAreaEntityDomain.MG_MemberGrades.Remove(MG_MemberGrade);
            SoftProjectAreaEntityDomain.MG_MemberGrades.Add(resp.Item);
            #endregion
        }

        /// <summary>
        /// 生成树或下拉树，缓存
        /// </summary>
        /// <returns></returns>
        public MyResponseBase MG_MemberGrade_GetAll()
        {
            #region 树数据

            //SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
            //domain.Sys_HOperControl = new SoftProjectAreaEntity
            //{
            //    DBTSql = "SELECT * FROM MG_MemberGrade ",
            //    DBOperType = 16,
            //    DBSelectResultType = 2,
            //};

            //OperCode = "MG_MemberGrade.GetAll";
            //var resp = domain.Execute();
            //var resp = Execute();
            //resp.Item.MG_MemberGrades = resptrees.Items;

            string sql = "SELECT * FROM V_MG_MemberGrade ";
            var resp = Query16(sql, 2);
            #endregion
            return resp;
        }

        #region 会员级别

        static List<SoftProjectAreaEntity> _MG_MemberGrades = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> MG_MemberGrades
        {
            get
            {
                if (_MG_MemberGrades.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _MG_MemberGrades = domain.MG_MemberGrade_GetAll().Items;
                }
                return _MG_MemberGrades;
            }
        }

        public static void MG_MemberGrade_Clear()
        {
            _MG_MemberGrades = new List<SoftProjectAreaEntity>();
        }

        #endregion

        public static string QueryHtmlDropDownList_MG_MemberGradeID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //var Pre_Roles = ProjectCache.Caches["MG_MemberGradeID"];
            var MG_MemberGradesTemp = MG_MemberGrades.Where(p => p.Pre_CompanyID == SoftProjectAreaEntityDomain.LoginInfostatic.CompanyID);
            var str = HtmlHelpers.DropDownList(null, "MG_MemberGradeID___equal", MG_MemberGradesTemp, "MG_MemberGradeID", "MemberGradeName", val, "", "==" + NameCn + "==");
            var strDrop = str.ToString();
            return strDrop;
        }

        public static string HtmlDropDownLiss_MG_MemberGradeID(string val, string NameCn, SoftProjectAreaEntity item)
        {
            //必须知道是公司、企业、顾客功能，从而进行过滤
            var MG_MemberGradesTemp = MG_MemberGrades.Where(p => p.Pre_CompanyID == SoftProjectAreaEntityDomain.LoginInfostatic.CompanyID);
            var str = HtmlHelpers.DropDownList(null, "Item.MG_MemberGradeID", MG_MemberGradesTemp, "MG_MemberGradeID", "MemberGradeName", val, "");
            return str.ToString();
        }

        #endregion
    }
}
