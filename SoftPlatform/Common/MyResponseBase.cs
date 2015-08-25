using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class MyResponseBase
    {
        /// <summary>
        /// 单个对象
        /// </summary>
        public SoftProjectAreaEntity Item { get; set; }

        /// <summary>
        /// 汇总对象
        /// </summary>
        public SoftProjectAreaEntity ItemTotal { get; set; }

        /// <summary>
        /// 对象集合
        /// </summary>
        public List<SoftProjectAreaEntity> Items { get; set; }

        public SoftProjectAreaEntity BaseAction { get; set; }

        public SoftProjectAreaEntity EditAction { get; set; }

        public List<SoftProjectAreaEntity> EditActions { get; set; }

        public SoftProjectAreaEntity ChildAction { get; set; }

        public string TreeID { get; set; }

        public int TreeQueryType { get; set; }

        public string ViewContextName { get; set; }

        public string LayoutName { get; set; }

        ///// <summary>
        ///// 查询条件
        ///// </summary>
        //public List<Query> Querys { get; set; }

        /// <summary>
        /// 模块功能编码
        /// </summary>
        public string ModularOrFunCode { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int? TotalItems { get; set; }

        /// <summary>
        /// 主键名
        /// </summary>
        public string PrimaryKeyName { get; set; }

        public Querys Querys { get; set; }

        ///// <summary>
        ///// 条件对象集合
        ///// </summary>
        //public List<SoftProjectAreaEntity> ItemsW { get; set; }

        /// <summary>
        /// 单个值
        /// </summary>
        public Object Obj { get; set; }

        private string _Disabled = "";
        /// <summary>
        /// 是否可以编辑：Disabled
        /// </summary>
        public string Disabled
        {
            get
            {
                return _Disabled;
            }
            set
            {
                _Disabled = value;
            }
        }

        private string _FunNameEn = "";
        /// <summary>
        /// 功能英文名
        /// </summary>
        public string FunNameEn
        {
            get { return _FunNameEn; }
            set { _FunNameEn = value; }
        }

        private string _FunNameCn = "";
        
        /// <summary>
        /// 功能中文名
        /// </summary>
        public string FunNameCn
        {
            get { return _FunNameCn; }
            set { _FunNameCn = value; }
        }

        private string _FunBtnNameCn = "";

        /// <summary>
        /// 功能按钮：中文名
        /// </summary>
        public string FunBtnNameCn
        {
            get { return _FunBtnNameCn; }
            set { _FunBtnNameCn = value; }
        }

        public PageQueryBase PageQueryBase { get; set; }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        public DataTable DataTable { get; set; }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        public DataSet DataSet { get; set; }

        public string LogMessage { get; set; }

        /// <summary>
        /// 搜索类型：0：快速   1：高级
        /// </summary>
        public int? searchType { get; set; }

        //回复附加信息
        public RespAttachInfo RespAttachInfo { get; set; }

        public MyResponseBase()
        {
            searchType = 0;
            Item = new SoftProjectAreaEntity();
            Items = new List<SoftProjectAreaEntity>();
            RespAttachInfo = new RespAttachInfo();
            PageQueryBase = new PageQueryBase();
            Querys = new Querys();
        }

        public void Set(MyResponseBase Item)
        {
            this.Item = Item.Item;
            this.Items.AddRange(Item.Items);
            this.RespAttachInfo.Merg(Item.RespAttachInfo);
        }

        public MyResponseBase(MyResponseBase Item)
        {
            this.Obj = Item.Obj;
            this.DataSet = Item.DataSet;
            this.DataTable = Item.DataTable;
            this.LogMessage = Item.LogMessage;
            this.PageQueryBase = Item.PageQueryBase;
            this.RespAttachInfo = Item.RespAttachInfo;
        }
    }

}
