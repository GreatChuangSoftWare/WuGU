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
    /// 表：Design_ModularOrFunSql(Sql语句配置)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// Sql语句配置ID
        /// </summary>
        public int? Design_ModularOrFunSqlID{ get; set; }

        /// <summary>
        /// Sql语句名称
        /// </summary>
        public string OperName { get; set; }

        /// <summary>
        /// Sql语句编码
        /// </summary>
        public string OperCode { get; set; }

        /// <summary>
        /// Sql语句类型
        /// </summary>
        public int? DBOperType { get; set; }

        /// <summary>
        /// Sql语句
        /// </summary>
        public string DBTSql { get; set; }

        /// <summary>
        /// 子查询类型
        /// </summary>
        public int? SelectSubType { get; set; }

        /// <summary>
        /// Sql语句返回类型
        /// </summary>
        public int? DBSelectResultType { get; set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string DBSqlParam { get; set; }

        /// <summary>
        /// 添加或修改字段
        /// </summary>
        public string DBFieldVals { get; set; }

        /// <summary>
        /// 默认排序
        /// </summary>
        public string DefaultSort { get; set; }

        /// <summary>
        /// 默认排序方向
        /// </summary>
        public int? DefaultSortDirection { get; set; }

        /// <summary>
        /// 暂时用于重复性说明
        /// </summary>
        public string FieldDesc { get; set; }

        ///// <summary>
        ///// 排序
        ///// </summary>
        //public int? Sort { get; set; }

        ///// <summary>
        ///// 功能模块ID
        ///// </summary>
        //public int? Design_ModularOrFunID { get; set; }

        public string EqualQueryParam { get; set; }

        /// <summary>
        /// Sql语句配置
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularOrFunSqls { get; set; }

    }
}
