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
    /// 表：Design_ModularOrFun(模块功能)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public int? Design_ModularOrFunID { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int? Design_ModularOrFunID1 { get; set; }

        public string DataRightDropDown { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int? SubDesign_ModularOrFunID { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string AreasCode { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllCode { get; set; }

        ///// <summary>
        ///// Action路径
        ///// </summary>
        //public string ActionPath { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModularOrFunCode { get; set; }

        /// <summary>
        /// 控制器方法ID
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 主视图名
        /// </summary>
        public string MainView { get; set; }

        /// <summary>
        /// 子视图名
        /// </summary>
        public string PartialView { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModularName { get; set; }

        /// <summary>
        /// 模块父ID
        /// </summary>
        public int? Design_ModularOrFunParentID { get; set; }

        /// <summary>
        /// 模块父节点名
        /// </summary>
        public string ParentModularName { get; set; }

        /// <summary>
        /// 分组模块或功能：1：分组  2：模块  3：功能
        /// </summary>
        public int? GroupModularOrFun { get; set; }

        ///// <summary>
        ///// 功能页面ID：在Design_ModularPage表中
        ///// </summary>
        //public int? Design_ModularPageID { get; set; }

        /// <summary>
        /// 主键字段名称
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 查询方法
        /// </summary>
        public string SearchMethod { get; set; }

        /// <summary>
        /// 是否有计算列
        /// </summary>
        public int? BCalCol { get; set; }

        /// <summary>
        /// 页面表单元素配置名称
        /// </summary>
        public string PageFormEleTypeName { get; set; }

        /// <summary>
        /// 页面查询条件表单元素配置名称
        /// </summary>
        public string QueryFormEleTypeName { get; set; }

        /// <summary>
        /// 菜单位置
        /// </summary>
        public int? MenuPostion { get; set; }

        /// <summary>
        /// 是否菜单面板
        /// </summary>
        public int? BMenuPanel { get; set; }

        /// <summary>
        /// 是否Url导航
        /// </summary>
        public int? BUrlNva { get; set; }

        public string SortCol { get; set; }

        /// <summary>
        /// 菜单显示
        /// </summary>
        public int? BMenu { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单父节点
        /// </summary>
        public int? MenuParentID { get; set; }

        /// <summary>
        /// 工具条：按钮区域显示宽度
        /// </summary>
        public int? ToolbarButtonAreaWidth { get; set; }

        public int? ToolbarSearchAreaWidth { get; set; }

        /// <summary>
        /// 表格宽度
        /// </summary>
        public string TableWidth { get; set; }

        /// <summary>
        /// 权限序号
        /// </summary>
        public int? PremSort { get; set; }

        public string PremName { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public int? GroupID { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int? ModularID { get; set; }

        ///// <summary>
        ///// 模块名称
        ///// </summary>
        //public string ModularName { get; set; }

        /// <summary>
        /// 页面ID
        /// </summary>
        public int PageID { get; set; }

        public int? bPage { get; set; }

        //public int? BPrem { get; set; }

        public int? ParentPremID { get; set; }

        /////// <summary>
        /////// 页面名称
        /////// </summary>
        //public string PageName { get; set; }

        ///// <summary>
        ///// 区域编码
        ///// </summary>
        //public string AreasCode { get; set; }

        ///// <summary>
        ///// 控制器编码
        ///// </summary>
        //public string ControllCode { get; set; }

        ///// <summary>
        ///// 页面编码
        ///// </summary>
        //public string ModularOrFunCode { get; set; }

        ///// <summary>
        ///// 菜单名称
        ///// </summary>
        //public string MenuName { get; set; }

        //public int? BMenu { get; set; }

        ///// <summary>
        ///// 页面字段列
        ///// </summary>
        //public string PageFormEleTypeName { get; set; }

        ///// <summary>
        ///// 查询字段列
        ///// </summary>
        //public string QueryFormEleTypeName { get; set; }
        //public int? PageType { get; set; }
        //public int? TableWidth { get; set; }
        ///// <summary>
        ///// 工具条的按钮区域宽度
        ///// </summary>
        //public int ToolbarButtonAreaWidth { get; set; }
        //public int MenuParentID { get; set; }
        //public string SearchMethod { get; set; }
        //public string ActionMethod { get; set; }
        //public string ActionMethodCn { get; set; }
        //public string ActionPath { get; set; }

        public int? PageType { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public int? bValidModularOrFun { get; set; }

        /// <summary>
        /// 是否显示导航栏
        /// </summary>
        public int? bNavModularOrFun { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ModularOrFunRemarks { get; set; }

        public int? DBOperTypeFun{get;set;}

        /// <summary>
        /// 视图名称
        /// </summary>
        public string TabViewName { get; set; }

        public string TSql { get; set; }

        /// <summary>
        /// 默认排序字段
        /// </summary>
        public string TSqlDefaultSort { get; set; }

        /// <summary>
        /// 默认排序方向
        /// </summary>
        public int? TSqlDefaultSortDirection { get; set; }

        /// <summary>
        /// 页面标题
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 菜单标识
        /// </summary>
        public string MenuIdent { get; set; }

        /// <summary>
        /// 主表主键
        /// </summary>
        public string MainPrimaryKey { get; set; }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFuns { get; set; }

        public List<SoftProjectAreaEntity> SubDesign_ModularOrFuns { get; set; }
    }
}
