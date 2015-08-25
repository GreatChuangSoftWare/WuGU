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
    /// 表：Design_ModularPageField(功能模块页面字段)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 功能模块页面字段ID
        /// </summary>
        public int? Design_ModularPageFieldID { get; set; }

        /// <summary>
        /// 字段功能类型：
        /// 字段类型：1：td  2:thead-data  4: tbody-data  6：thead|tbody-data
        /// </summary>
        public int? FieldFunTypeID { get; set; }

        ///// <summary>
        ///// 功能模块页面ID
        ///// </summary>
        //public int? Design_ModularPageID { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public int? HeadOrDataType { get; set; }

        ///// <summary>
        ///// 功能模块字段ID
        ///// </summary>
        //public int? Design_ModularFieldID { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int? TableInfoSort { get; set; }

        /// <summary>
        /// 名称类型
        /// </summary>
        public int? NameCnType { get; set; }

        /// <summary>
        /// 表头宽度
        /// </summary>
        public int? HeadWidth { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public int? bDisabled { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public int? bHidden { get; set; }

        public int? PageFormEleSort { get; set; }

        public int? PageFormElePos { get; set; }

        public int? FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page01FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page01FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page01FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page02FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page02FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page02FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page03FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page03FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page03FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page04FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page04FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page04FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page05FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page05FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page05FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page06FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page06FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page06FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page07FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page07FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page07FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page08FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page08FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page08FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page09FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page09FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page09FormEleType { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page10FormEleSort { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page10FormElePos { get; set; }

        /// <summary>
        /// 表单元素类型
        /// </summary>
        public int? Page10FormEleType { get; set; }


        public int? Page11FormEleSort { get; set; }
        public int? Page11FormElePos { get; set; }
        public int? Page11FormEleType { get; set; }
        public int? Sort11 { get; set; }
        public int? Query11Pos { get; set; }
        public int? Query11FormEleType { get; set; }
        public int? Query11QueryType { get; set; }

        public int? Page12FormEleSort { get; set; }
        public int? Page12FormElePos { get; set; }
        public int? Page12FormEleType { get; set; }
        public int? Sort12 { get; set; }
        public int? Query12Pos { get; set; }
        public int? Query12FormEleType { get; set; }
        public int? Query12QueryType { get; set; }

        public int? Page13FormEleSort { get; set; }
        public int? Page13FormElePos { get; set; }
        public int? Page13FormEleType { get; set; }
        public int? Sort13 { get; set; }
        public int? Query13Pos { get; set; }
        public int? Query13FormEleType { get; set; }
        public int? Query13QueryType { get; set; }

        public int? Page14FormEleSort { get; set; }
        public int? Page14FormElePos { get; set; }
        public int? Page14FormEleType { get; set; }
        public int? Sort14 { get; set; }
        public int? Query14Pos { get; set; }
        public int? Query14FormEleType { get; set; }
        public int? Query14QueryType { get; set; }

        public int? Page15FormEleSort { get; set; }
        public int? Page15FormElePos { get; set; }
        public int? Page15FormEleType { get; set; }
        public int? Sort15 { get; set; }
        public int? Query15Pos { get; set; }
        public int? Query15FormEleType { get; set; }
        public int? Query15QueryType { get; set; }

        public int? Page16FormEleSort { get; set; }
        public int? Page16FormElePos { get; set; }
        public int? Page16FormEleType { get; set; }
        public int? Sort16 { get; set; }
        public int? Query16Pos { get; set; }
        public int? Query16FormEleType { get; set; }
        public int? Query16QueryType { get; set; }

        public int? Page17FormEleSort { get; set; }
        public int? Page17FormElePos { get; set; }
        public int? Page17FormEleType { get; set; }
        public int? Sort17 { get; set; }
        public int? Query17Pos { get; set; }
        public int? Query17FormEleType { get; set; }
        public int? Query17QueryType { get; set; }

        public int? Page18FormEleSort { get; set; }
        public int? Page18FormElePos { get; set; }
        public int? Page18FormEleType { get; set; }
        public int? Sort18 { get; set; }
        public int? Query18Pos { get; set; }
        public int? Query18FormEleType { get; set; }
        public int? Query18QueryType { get; set; }

        public int? Page19FormEleSort { get; set; }
        public int? Page19FormElePos { get; set; }
        public int? Page19FormEleType { get; set; }
        public int? Sort19 { get; set; }
        public int? Query19Pos { get; set; }
        public int? Query19FormEleType { get; set; }
        public int? Query19QueryType { get; set; }

        public int? Page20FormEleSort { get; set; }
        public int? Page20FormElePos { get; set; }
        public int? Page20FormEleType { get; set; }
        public int? Sort20 { get; set; }
        public int? Query20Pos { get; set; }
        public int? Query20FormEleType { get; set; }
        public int? Query20QueryType { get; set; }

         public int? Page21FormEleSort { get; set; }
         public int? Page21FormElePos { get; set; }
         public int? Page21FormEleType { get; set; }
         public int? Sort21 { get; set; }
        public int? Query21Pos { get; set; }
        public int? Query21FormEleType { get; set; }
        public int? Query21QueryType { get; set; }

        public int? Page22FormEleSort { get; set; }
        public int? Page22FormElePos { get; set; }
        public int? Page22FormEleType { get; set; }
        public int? Sort22 { get; set; }
        public int? Query22Pos { get; set; }
        public int? Query22FormEleType { get; set; }
        public int? Query22QueryType { get; set; }

        public int? Page23FormEleSort { get; set; }
        public int? Page23FormElePos { get; set; }
        public int? Page23FormEleType { get; set; }
        public int? Sort23 { get; set; }
        public int? Query23Pos { get; set; }
        public int? Query23FormEleType { get; set; }
        public int? Query23QueryType { get; set; }

        public int? Page24FormEleSort { get; set; }
        public int? Page24FormElePos { get; set; }
        public int? Page24FormEleType { get; set; }
        public int? Sort24 { get; set; }
        public int? Query24Pos { get; set; }
        public int? Query24FormEleType { get; set; }
        public int? Query24QueryType { get; set; }

        public int? Page25FormEleSort { get; set; }
        public int? Page25FormElePos { get; set; }
        public int? Page25FormEleType { get; set; }
        public int? Sort25 { get; set; }
        public int? Query25Pos { get; set; }
        public int? Query25FormEleType { get; set; }
        public int? Query25QueryType { get; set; }

        public int? Page26FormEleSort { get; set; }
        public int? Page26FormElePos { get; set; }
        public int? Page26FormEleType { get; set; }
        public int? Sort26 { get; set; }
        public int? Query26Pos { get; set; }
        public int? Query26FormEleType { get; set; }
        public int? Query26QueryType { get; set; }

        public int? Page27FormEleSort { get; set; }
        public int? Page27FormElePos { get; set; }
        public int? Page27FormEleType { get; set; }
        public int? Sort27 { get; set; }
        public int? Query27Pos { get; set; }
        public int? Query27FormEleType { get; set; }
        public int? Query27QueryType { get; set; }

        public int? Page28FormEleSort { get; set; }
        public int? Page28FormElePos { get; set; }
        public int? Page28FormEleType { get; set; }
        public int? Sort28 { get; set; }
        public int? Query28Pos { get; set; }
        public int? Query28FormEleType { get; set; }
        public int? Query28QueryType { get; set; }

        public int? Page29FormEleSort { get; set; }
        public int? Page29FormElePos { get; set; }
        public int? Page29FormEleType { get; set; }
        public int? Sort29 { get; set; }
        public int? Query29Pos { get; set; }
        public int? Query29FormEleType { get; set; }
        public int? Query29QueryType { get; set; }

        public int? Page30FormEleSort { get; set; }
        public int? Page30FormElePos { get; set; }
        public int? Page30FormEleType { get; set; }
        public int? Sort30 { get; set; }
        public int? Query30Pos { get; set; }
        public int? Query30FormEleType { get; set; }
        public int? Query30QueryType { get; set; }

        public int? Page31FormEleSort { get; set; }
        public int? Page31FormElePos { get; set; }
        public int? Page31FormEleType { get; set; }
        public int? Sort31 { get; set; }
        public int? Query31Pos { get; set; }
        public int? Query31FormEleType { get; set; }
        public int? Query31QueryType { get; set; }

        public int? Page32FormEleSort { get; set; }
        public int? Page32FormElePos { get; set; }
        public int? Page32FormEleType { get; set; }
        public int? Sort32 { get; set; }
        public int? Query32Pos { get; set; }
        public int? Query32FormEleType { get; set; }
        public int? Query32QueryType { get; set; }

        public int? Page33FormEleSort { get; set; }
        public int? Page33FormElePos { get; set; }
        public int? Page33FormEleType { get; set; }
        public int? Sort33 { get; set; }
        public int? Query33Pos { get; set; }
        public int? Query33FormEleType { get; set; }
        public int? Query33QueryType { get; set; }

        public int? Page34FormEleSort { get; set; }
        public int? Page34FormElePos { get; set; }
        public int? Page34FormEleType { get; set; }
        public int? Sort34 { get; set; }
        public int? Query34Pos { get; set; }
        public int? Query34FormEleType { get; set; }
        public int? Query34QueryType { get; set; }

        public int? Page35FormEleSort { get; set; }
        public int? Page35FormElePos { get; set; }
        public int? Page35FormEleType { get; set; }
        public int? Sort35 { get; set; }
        public int? Query35Pos { get; set; }
        public int? Query35FormEleType { get; set; }
        public int? Query35QueryType { get; set; }

        public int? Page36FormEleSort { get; set; }
        public int? Page36FormElePos { get; set; }
        public int? Page36FormEleType { get; set; }
        public int? Sort36 { get; set; }
        public int? Query36Pos { get; set; }
        public int? Query36FormEleType { get; set; }
        public int? Query36QueryType { get; set; }

        public int? Page37FormEleSort { get; set; }
        public int? Page37FormElePos { get; set; }
        public int? Page37FormEleType { get; set; }
        public int? Sort37 { get; set; }
        public int? Query37Pos { get; set; }
        public int? Query37FormEleType { get; set; }
        public int? Query37QueryType { get; set; }

        public int? Page38FormEleSort { get; set; }
        public int? Page38FormElePos { get; set; }
        public int? Page38FormEleType { get; set; }
        public int? Sort38 { get; set; }
        public int? Query38Pos { get; set; }
        public int? Query38FormEleType { get; set; }
        public int? Query38QueryType { get; set; }

        public int? Page39FormEleSort { get; set; }
        public int? Page39FormElePos { get; set; }
        public int? Page39FormEleType { get; set; }
        public int? Sort39 { get; set; }
        public int? Query39Pos { get; set; }
        public int? Query39FormEleType { get; set; }
        public int? Query39QueryType { get; set; }

        public int? Page40FormEleSort { get; set; }
        public int? Page40FormElePos { get; set; }
        public int? Page40FormEleType { get; set; }
        public int? Sort40 { get; set; }
        public int? Query40Pos { get; set; }
        public int? Query40FormEleType { get; set; }
        public int? Query40QueryType { get; set; }

        /// <summary>
        /// css类
        /// </summary>
        public string cssclass { get; set; }

        /// <summary>
        /// 列计算表达式
        /// </summary>
        public string calcol { get; set; }

        /// <summary>
        /// 行计算表达式
        /// </summary>
        public string calrow { get; set; }

        /// <summary>
        /// 回车焦点垂直方向
        /// </summary>
        public int? bTabVer { get; set; }

        /// <summary>
        /// 元素绑定的字段值：暂用于功能复选框，用于判断其状态：如果为1可以提交，如果为2可以审核
        /// </summary>
        public string EleDatas { get; set; }

        /// <summary>
        /// 编辑区域名
        /// </summary>
        public string EditAreaName { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// 查询条件位置：根据位置设置，快速查询只有3个
        /// </summary>
        public int? QueryPos { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query01Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query01FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query01QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query02Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query02FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query02QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query03Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query03FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query03QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query04Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query04FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query04QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query05Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query05FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query05QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query06Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query06FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query06QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query07Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query07FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query07QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query08Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query08FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query08QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query09Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query09FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query09QueryType { get; set; }

        /// <summary>
        /// 查询条件位置
        /// </summary>
        public int? Query10Pos { get; set; }

        /// <summary>
        /// 表单元素
        /// </summary>
        public int? Query10FormEleType { get; set; }

        /// <summary>
        /// 查询类型：1:模糊   2:等值   3:范围
        /// </summary>
        public int? Query10QueryType { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort01 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort02 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort03 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort04 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort05 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort06 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort07 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort08 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort09 { get; set; }

        /// <summary>
        /// 排序列序号
        /// </summary>
        public int? Sort10 { get; set; }

        /// <summary>
        /// 功能模块字段列表
        /// </summary>
        public List<SoftProjectAreaEntity> Design_ModularPageFields { get; set; }

    }
}
