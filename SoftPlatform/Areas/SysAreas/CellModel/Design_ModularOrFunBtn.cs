using Framework.Core;
using Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 表：Design_ModularOrFunBtn(功能模块操作)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 功能模块操作ID
        /// </summary>
        public int? Design_ModularOrFunBtnID { get; set; }

        public string Design_ModularOrFunBtnIDs { get; set; }

      //public string Design_ModularOrFunID{ get; set; }
      //public string Sort{get;set;}
      public string BtnNameEn{get;set;}
      //public string BtnNameCn{get;set;}
      //public string OperPos{get;set;}
      //public string BtnBehavior{get;set;}
      //public string PopupAfterTableFun{get;set;}
      //public string MastEditArea{get;set;}
      //public string ChildtableSelect{get;set;}
      //public string ParamName{get;set;}
      //public string DispConditionsExpression{get;set;}
      //public string OperConditionsExpression{get;set;}
      //public string TargetDom{get;set;}
      //public string TableSelect{get;set;}
      //public string PopupWidth{get;set;}




        ///// <summary>
        ///// 模块功能ID:Design_ModularOrFun表中
        ///// </summary>
        //public int? Design_ModularOrFunID{get;set;}

        /// <summary>
        /// 按钮类型,1：一般按钮   2：上传按钮
        /// </summary>
        public int? BtnType { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionNameEn { get; set; }

        /// <summary>
        /// 按钮位置
        /// </summary>
        public int? OperPos { get; set; }

        /// <summary>
        /// 编辑页面按钮
        /// </summary>
        public string BtnNameCn { get; set; }

        /// <summary>
        /// 显示条件表达式
        /// </summary>
        public string DispConditionsExpression { get; set; }

        /// <summary>
        /// 操作条件表达式：暂时针对批量提交、审核
        /// </summary>
        public string OperConditionsExpression { get; set; }

        /// <summary>
        /// 显示按钮表达式字段名
        /// </summary>
        public string DispConditionsField { get; set; }

        /// <summary>
        /// 显示按钮表达式的运算符
        /// </summary>
        public string DispConditionsOperator { get; set; }

        /// <summary>
        /// 显示按钮表达式的值
        /// </summary>
        public string DispConditionsValue { get; set; }

        /// <summary>
        /// Add的Popup的Table选择器
        /// </summary>
        public string TableSelect { get; set; }

        /// <summary>
        /// 对表的操作
        /// </summary>
        public int? PopupAfterTableFun { get; set; }

        /// <summary>
        /// 主编辑区域
        /// </summary>
        public string MastEditArea { get; set; }

        /// <summary>
        /// 子编辑区域
        /// </summary>
        public string ChildtableSelect { get; set; }

        /// <summary>
        /// 引用数据区域，针对弹窗要选择主表的数据
        /// </summary>
        public string RefDataArea { get; set; }

        /// <summary>
        /// 目标url
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 目标url参数
        /// </summary>
        public string TargetUrlParamName { get; set; }

        /// <summary>
        /// 目标Dom：AjaxGet请求页面
        /// </summary>
        public string TargetDom { get; set; }

        /// <summary>
        /// 按钮查询方法
        /// </summary>
        public string BtnSearchMethod { get; set; }

        public string AfterBehavior { get; set; }
        public string AfterBehaviorUrl { get; set; }
        public string AfterBehaviorUrlParamName { get; set; }

        /// <summary>
        /// 窗口大小
        /// </summary>
        public int? PopupWidth { get; set; }

        ///// <summary>
        ///// 序号
        ///// </summary>
        //public int? Sort { get; set; }

        /// <summary>
        /// 条件字段
        /// </summary>
        public string ConditionsField { get; set; }

        /// <summary>
        /// 条件运算符
        /// </summary>
        public string ConditionsOperator { get; set; }

        /// <summary>
        /// 条件值
        /// </summary>
        public string ConditionsValue { get; set; }

        /// <summary>
        /// Action路径
        /// </summary>
        public string ActionPath { get; set; }

        /// <summary>
        /// 单击按钮后对应显示页面
        /// </summary>
        public int? BtnToDesign_ModularOrFunID { get; set; }

        /// <summary>
        /// 按钮行为11(UI)、12(Get)、21(提交)、31(删除界面元素)、8查询
        /// </summary>
        public int? BtnBehavior { get; set; }

        /// <summary>
        /// 行为Url
        /// </summary>
        public string BehaviorUrl { get; set; }

        /// <summary>
        /// 参数：例如Edit
        /// </summary>
        public string BtnParamName { get; set; }

        /// <summary>
        /// popup弹窗选择是否可以重复，0:不可重复
        /// </summary>
        public int? popupaddrepeat { get; set; }

        public int? bValid { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string ModularOrFunBtnRemark { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunBtns { get; set; }

    }
}
