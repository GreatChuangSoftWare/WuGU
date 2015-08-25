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
    /// 表：Design_ModularField(功能模块字段)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 功能模块字段ID
        /// </summary>
        public int? Design_ModularFieldID { get; set; }

        /// <summary>
        /// 字段类型ID：字段类型：1：表字段   2：实体字段   3：表和实体字段
        /// </summary>
        public int? FieldTypeID { get; set; }

        /// <summary>
        /// 模块字段名s
        /// </summary>
        public string Design_ModularFieldIDs { get; set; }

        ///// <summary>
        ///// 模块功能ID：在Design_ModularOrFun表中
        ///// </summary>
        //public int? Design_ModularOrFunID { get; set; }

        /// <summary>
        /// 字段英文名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 字段中文名称
        /// </summary>
        public string NameCn { get; set; }

        /// <summary>
        /// 同样的数据集，有的表头字段不多，不需要换行时使用。
        /// 同样的数据集，有的表头字段很多，需要换行使用。
        /// </summary>
        public string NameCn2 { get; set; }

        /// <summary>
        /// 数据类型：56：整数   106：小数    167：字符串   61：日期
        /// </summary>
        public int? xtype { get; set; }

        /// <summary>
        /// 数据类型长度
        /// </summary>
        public int? length { get; set; }

        /// <summary>
        /// 查询字段名：针对有下拉列表框
        /// </summary>
        public string QueryEn { get; set; }

        /// <summary>
        /// 查询名称：针对日期类型
        /// </summary>
        public string QueryCn { get; set; }

        /// <summary>
        /// 字典：SexID$1|男,2|女
        /// </summary>
        public string Dicts { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string Calformula { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 字段配置是否显示
        /// </summary>
        public int? bFieldsConfigDisp { get; set; }

        /// <summary>
        /// Table：显示列宽
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public string Align { get; set; }

        /// <summary>
        /// 显示格式
        /// </summary>
        public string DisFormat { get; set; }

        /// <summary>
        /// 必填
        /// </summary>
        public int? Required { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否为主键字段
        /// </summary>
        public int? bPrimaryKeyOrFK { get; set; }

        /// <summary>
        /// 日志字段
        /// </summary>
        public int? bOperLog { get; set; }

        /// <summary>
        /// 是否可以重复
        /// </summary>
        public int? bRepeat { get; set; }

        /// <summary>
        /// 表字段：位置
        /// </summary>
        public int? colid { get; set; }

        public string ModularFieldRemark { get; set; }

        /// <summary>
        /// 功能模块字段列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularFields { get; set; }

    }
}
