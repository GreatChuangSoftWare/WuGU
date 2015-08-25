
using Framework.Core;
using Framework.Web.Mvc;
using Framework.Web.Mvc.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：Pre_User(用户管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? Pre_UserID { get; set; }

        public int? UserCategoryID { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 角色
        /// </summary>
        //public  int?  Pre_RoleID{get;set;}

        /// <summary>
        /// 部门
        /// </summary>
        //public  int?  Pre_OrganizationID{get;set;}

        /// <summary>
        /// 机构名称
        /// </summary>
        public int? UserOrganizationName { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public int? UserJob { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 扩展动态字段：会员姓名
        /// </summary>
        public string UserName2 { get; set; }

        /// <summary>
        /// 扩展动态字段：顾客姓名
        /// </summary>
        public string UserName3 { get; set; }

        /// <summary>
        /// 扩展动态字段：合作商
        /// </summary>
        public string UserName4 { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordDigest { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birth { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZIPCode { get; set; }

        /// <summary>
        /// 手机2
        /// </summary>
        public string MobilePhone2 { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 公司邮件
        /// </summary>
        public string CompanyEmail { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? SexID { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string EmergencyPerson { get; set; }

        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        public string EmergencyTele { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// MSN
        /// </summary>
        public string MSN { get; set; }

        /// <summary>
        /// 教育程序
        /// </summary>
        public int? EducationID { get; set; }

        /// <summary>
        /// 教育程序
        /// </summary>
        public string EducationName { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        public string SpouseName { get; set; }

        /// <summary>
        /// 配偶电话
        /// </summary>
        public string SpouseTele { get; set; }

        /// <summary>
        /// 婚姻情况
        /// </summary>
        public int? MaritalID { get; set; }

        /// <summary>
        /// 婚姻情况
        /// </summary>
        public string MaritalName { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? EntryTime { get; set; }

        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime? PositiveTime { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? QuitDate { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        public string QuitReason { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? UserStatuID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string UserStatuName { get; set; }

        /// <summary>
        /// 下次回访日期
        /// </summary>
        public DateTime? NextVisitDate { get; set; }

        public int? Age { get; set; }

        /// <summary>
        /// 资金余额
        /// </summary>
        public decimal? FundBalance { get; set; }

        public SoftProjectAreaEntity Pre_User { get; set; }
        public List<SoftProjectAreaEntity> Pre_Users { get; set; }
    }
}
