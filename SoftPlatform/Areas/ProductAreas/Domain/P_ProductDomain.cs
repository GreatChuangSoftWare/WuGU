
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

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：P_Product(商品管理)
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        public void P_Product_Domain()
        {
            PKField = "P_ProductID";
            TableName = "P_Product";
            TabViewName = "V_P_Product";
        }

        /// <summary>
        /// 主键不为空检查
        /// </summary>
        /// <returns></returns>
        public void P_Product_PKCheck()
        {
            //主键不为空检查
            if (Item.P_ProductID == null)
            {
                throw new Exception("商品管理主键不能为空！");
            }
        }

        /// <summary>
        /// 根据主键查询--显示
        /// </summary>
        /// <returns></returns>
        public MyResponseBase P_Product_ByID()
        {
            //根据主键查询--显示
            P_Product_PKCheck();
            Sys_HOperControl = null;
            string sql =string.Format("SELECT * FROM P_Product WHERE P_ProductID={0}",Item.P_ProductID);
            resp =Query16(sql,4);
            return resp;
        }

        /// <summary>
        /// 根据主键查询--显示
        /// </summary>
        /// <returns></returns>
        public static List<SoftProjectAreaEntity> P_Product_ByNewHome()
        {
            var domain = new SoftProjectAreaEntityDomain();
            domain.Sys_HOperControl = null;
            string sql = string.Format("SELECT TOP 10 * FROM V_P_Product WHERE BNewID=1 ORDER BY CREATEDATE DESC");
            var resp = domain.Query16(sql);
            return resp.Items;
        }

        
    }
}
