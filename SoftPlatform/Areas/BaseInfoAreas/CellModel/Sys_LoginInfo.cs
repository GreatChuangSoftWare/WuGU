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
        /// 登录人ID
        /// </summary>
        public int? Sys_LoginInfoID { get; set; }

        ///// <summary>
        ///// 手机号
        ///// </summary>
        //public string MobilePhone { get; set; }

        ///// <summary>
        ///// 密码
        ///// </summary>
        //public string PasswordDigest { get; set; }

        /// <summary>
        /// 重复密码
        /// </summary>
        public string RePasswordDigest { get; set; }

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
        /// 主页名称
        /// </summary>
        public string HomePageName{get;set;}

        /// <summary>
        /// 主页Url
        /// </summary>
        public string HomePageUrl { get; set; }

    }
}
