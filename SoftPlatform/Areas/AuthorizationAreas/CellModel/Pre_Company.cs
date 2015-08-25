
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
    /// 表：Pre_Company(加盟商管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 公司企业ID
        /// </summary>
        public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 公司名称
        /// </summary>
        public  string  PreCompanyName{get;set;}

        /// <summary>
        /// 企业名称
        /// </summary>
        public  string  EnterpriseName{get;set;}

        /// <summary>
        /// 加盟商
        /// </summary>
        public  string  PreCompanyName2{get;set;}

        /// <summary>
        /// 合作商
        /// </summary>
        public  string  PreCompanyName3{get;set;}

        /// <summary>
        /// 省(市)
        /// </summary>
        public  int?  Ba_AreaID1{get;set;}

        /// <summary>
        /// 省(市)
        /// </summary>
        public  string  AreaName1{get;set;}

        /// <summary>
        /// 市(区、县)
        /// </summary>
        public  int?  Ba_AreaID2{get;set;}

        /// <summary>
        /// 市(区、县)
        /// </summary>
        public  string  AreaName2{get;set;}

        /// <summary>
        /// 市(区、县)
        /// </summary>
        public  int?  Ba_AreaID3{get;set;}

        /// <summary>
        /// 市(区、县)
        /// </summary>
        public  string  AreaName3{get;set;}

        /// <summary>
        /// 行政区域
        /// </summary>
        public  string  AreaName{get;set;}

        /// <summary>
        /// 经营类别
        /// </summary>
        public  int?  BusinessCategoryID{get;set;}

        /// <summary>
        /// 经营类别
        /// </summary>
        public  string  BusinessCategoryName{get;set;}

        /// <summary>
        /// 经营项目
        /// </summary>
        public  int?  OperatingItemID{get;set;}

        /// <summary>
        /// 项目集合
        /// </summary>
        public  string  OperatingItemIDs{get;set;}

        /// <summary>
        /// 经营项目
        /// </summary>
        public  string  OperatingItemName{get;set;}

        /// <summary>
        /// 姓名
        /// </summary>
        //public  string  UserName{get;set;}

        /// <summary>
        /// 手机
        /// </summary>
        //public  string  MobilePhone{get;set;}

        /// <summary>
        /// 负责人
        /// </summary>
        public  string  PreHead{get;set;}

        /// <summary>
        /// 联系人
        /// </summary>
        public  string  PreContactPerson{get;set;}

        /// <summary>
        /// 手机
        /// </summary>
        public  string  PreMobilePhone{get;set;}

        /// <summary>
        /// 电话
        /// </summary>
        public  string  PreTel{get;set;}

        /// <summary>
        /// 传真
        /// </summary>
        public  string  PreFax{get;set;}

        /// <summary>
        /// 邮编
        /// </summary>
        public  string  PreZipcode{get;set;}

        /// <summary>
        /// 邮件
        /// </summary>
        public  string  PreEmail{get;set;}

        /// <summary>
        /// 主页
        /// </summary>
        public  string  PreHomePage{get;set;}

        /// <summary>
        /// 地址
        /// </summary>
        public  string  PreAddress{get;set;}

        /// <summary>
        /// 办公地址
        /// </summary>
        public  string  PreOfficeAddress{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  PreStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  PreStatuName{get;set;}

        /// <summary>
        /// 开票类型
        /// </summary>
        public  int?  PreInvoiceCategoryID{get;set;}

        /// <summary>
        /// 帐户名称
        /// </summary>
        public  string  PreInvoiceAccountName{get;set;}

        /// <summary>
        /// 开户银行
        /// </summary>
        public  string  PreInvoiceBank{get;set;}

        /// <summary>
        /// 税号
        /// </summary>
        public  string  PreInvoiceTariff{get;set;}

        /// <summary>
        /// 银行帐户
        /// </summary>
        public  string  PreInvoiceBankAccount{get;set;}

        /// <summary>
        /// 注册地址
        /// </summary>
        public  string  PreInvoiceRegAddress{get;set;}

        /// <summary>
        /// 开票电话
        /// </summary>
        public  string  PreInvoiceTele{get;set;}

        /// <summary>
        /// 收票地址
        /// </summary>
        public  string  PreTicketCollectorAddr{get;set;}

        /// <summary>
        /// 收票邮编
        /// </summary>
        public  string  PreTicketCollectorPost{get;set;}

        /// <summary>
        /// 收票人
        /// </summary>
        public  string  PreTicketCollectorPerson{get;set;}

        /// <summary>
        /// 收票手机
        /// </summary>
        public  string  PreTicketCollectorMobilePhone{get;set;}

        /// <summary>
        /// 收票电话
        /// </summary>
        public  string  PreTicketCollectorTele{get;set;}

        /// <summary>
        /// 公司类别
        /// </summary>
        public  int?  CompanyCategoryID{get;set;}

        public SoftProjectAreaEntity Pre_Company { get; set; }
        public List<SoftProjectAreaEntity> Pre_Companys { get; set; }
    }
}
