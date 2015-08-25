using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 登录类型
        /// </summary>
        public int? LoginCategory { get; set; }

        ///// <summary>
        ///// 登录名
        ///// </summary>
        //public string LoginName { get; set; }

        ///// <summary>
        ///// 登录密码
        ///// </summary>
        //public string PasswordDigest { get; set; }

        /// <summary>
        /// 重复密码
        /// </summary>
        public string RePasswordDigest { get; set; }

        /// <summary>
        /// 登录人ID
        /// </summary>
        public int? LoginID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string LoginNameCn { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int? CompanyID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 登录人的首页控制器
        /// </summary>
        public string HomeConstrollName{get;set;}

        /// <summary>
        /// 登录人的首页Action
        /// </summary>
        public string HomeActionName { get; set; }

        public int? CurrMenuModularOrFunID { get; set; }

        /// <summary>
        /// 当前位置：方便显示菜单
        /// </summary>
        public string CurrNav { get; set; }

        /// <summary>
        /// 当前位置类型ID
        /// </summary>
        public int? CurrNavCategoryID { get; set; }

        /// <summary>
        /// 当前位置：标识
        /// </summary>
        public int? CurrNavIdent { get; set; }
        
        /// <summary>
        /// 主页名称
        /// </summary>
        public string HomePageName{get;set;}

        /// <summary>
        /// 主页Url
        /// </summary>
        public string HomePageUrl { get; set; }

    }
}
