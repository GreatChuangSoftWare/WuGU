using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftProject.CellModel
{
    /// <summary>
    /// 软件项目实体：SoftProjectAreaEntity
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        public SoftProjectAreaEntity()
        {
            Formats = new SoftProjectAreaEntityFormat(this);
            //Item = new SoftProjectAreaEntity();
            Items = new List<SoftProjectAreaEntity>();

            Design_ModularOrFuns = new List<SoftProjectAreaEntity>();
            Design_ModularFields = new List<SoftProjectAreaEntity>();
            Design_ModularOrFunBtns = new List<SoftProjectAreaEntity>();

            Design_ModularPageFields = new List<SoftProjectAreaEntity>();
        }

        public SoftProjectAreaEntityFormat Formats
        {
            get;
            set;
        }

        ///// <summary>
        ///// 状态ID
        ///// </summary>
        //public int? StatuID { get; set; }

        ///// <summary>
        ///// 状态名称
        ///// </summary>
        //public string StatuName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModularOrFunName { get; set; }

        #region 导航

        /// <summary>
        /// 最小ID
        /// </summary>
        public int? MinID{ get; set; }

        /// <summary>
        /// 最大ID
        /// </summary>
        public int? MaxID { get; set; }

        /// <summary>
        /// 当前ID
        /// </summary>
        public int? CurrID { get; set; }

        #endregion

        public string ActionFieldNames { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int? Count { get; set; }

        public int? CreateUserID { get; set; }

        public string CreateUserName { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateUserID { get; set; }

        public string UpdateUserName { get; set; }

        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 判断是查看，还是编辑,值为：Disabled
        /// </summary>
        public string Disabled { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public int? BEffective { get; set; }

        public int? TotalItems { get; set; }

        public string ErrorMessage { get; set; }

        public List<SoftProjectAreaEntity> Items { get; set; }

        public int? RoleID { get; set; }

        /// <summary>
        /// 附件表关联主表的Guid
        /// </summary>
        public string RefPKTableGuid { get; set; }

        //public SoftProjectAreaEntity Item { get; set; }
    }

    public partial class SoftProjectAreaEntityFormat
    {
        private SoftProjectAreaEntity _SoftProjectAreaEntity;

        public SoftProjectAreaEntityFormat()
        {
        }

        public SoftProjectAreaEntityFormat(SoftProjectAreaEntity SoftProjectAreaEntity)
        {
            this._SoftProjectAreaEntity = SoftProjectAreaEntity;
        }
    }

}
