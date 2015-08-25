
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
    /// 表：P_Product(商品管理)
    /// </summary>
    public partial class SoftProjectAreaEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public  int?  P_ProductID{get;set;}

        /// <summary>
        /// 公司ID
        /// </summary>
        //public  int?  Pre_CompanyID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  int?  ProductStatuID{get;set;}

        /// <summary>
        /// 状态
        /// </summary>
        public  string  ProductStatuName{get;set;}

        /// <summary>
        /// 品牌
        /// </summary>
        public  string  BrandName{get;set;}

        /// <summary>
        /// 商品编号
        /// </summary>
        public  string  ProductNo{get;set;}

        /// <summary>
        /// 商品名称
        /// </summary>
        public  string  ProductName{get;set;}

        /// <summary>
        /// 商品类别
        /// </summary>
        //public  int?  P_CategoryID{get;set;}

        /// <summary>
        /// 商品型号
        /// </summary>
        public  string  ProductModel{get;set;}

        /// <summary>
        /// 规格
        /// </summary>
        public  string  Specifications{get;set;}

        /// <summary>
        /// 规格重量
        /// </summary>
        public  decimal?  SpecificationsWeight{get;set;}

        /// <summary>
        /// 单位
        /// </summary>
        public  int?  UnitID{get;set;}

        /// <summary>
        /// 单位
        /// </summary>
        public  string  UnitName{get;set;}

        /// <summary>
        /// 价格
        /// </summary>
        public  decimal?  ProductPrice{get;set;}

        /// <summary>
        /// 规格价格
        /// </summary>
        public  decimal?  SpecificationsPrice{get;set;}

        /// <summary>
        /// 图片
        /// </summary>
        public  string  ImageSubQuery{get;set;}

        /// <summary>
        /// 图片标识
        /// </summary>
        public  string  ImageFileNameGuid{get;set;}

        /// <summary>
        /// 主图片文件名
        /// </summary>
        public  string  ProductMainImageFileName{get;set;}

        /// <summary>
        /// 主图片文件路径
        /// </summary>
        public  string  ProductMainImageFilePath{get;set;}

        /// <summary>
        /// 主图片
        /// </summary>
        public  string  ProductMainImage{get;set;}

        /// <summary>
        /// 主图片
        /// </summary>
        public  string  ProductMainImageUpload{get;set;}

        /// <summary>
        /// 新品
        /// </summary>
        public  int?  BNewID{get;set;}

        /// <summary>
        /// 新品
        /// </summary>
        public  string  BNewName{get;set;}

        /// <summary>
        /// 精品
        /// </summary>
        public  int?  BBoutiqueID{get;set;}

        /// <summary>
        /// 精品
        /// </summary>
        public  string  BBoutiqueName{get;set;}

        /// <summary>
        /// 商品描述
        /// </summary>
        public  string  ProductContext{get;set;}

        /// <summary>
        /// 完善描述
        /// </summary>
        public  int?  BProductContext{get;set;}

        /// <summary>
        /// 完善描述
        /// </summary>
        public  string  BProductContextName{get;set;}

        /// <summary>
        /// 商品信息
        /// </summary>
        public  string  ProductInfo{get;set;}

        /// <summary>
        /// 完善信息
        /// </summary>
        public  int?  BProductInfo{get;set;}

        /// <summary>
        /// 完善信息
        /// </summary>
        public  string  BProductInfoName{get;set;}

        /// <summary>
        /// 商品编码、名称、规格
        /// </summary>
        public  string  ProductNo__ProductName__Specifications{get;set;}

        /// <summary>
        /// 商品IDs--用于弹窗选择商品
        /// </summary>
        public  string  P_ProductIDs{get;set;}

        /// <summary>
        /// 设计备注
        /// </summary>
        public  string  DesignPersonRemark{get;set;}

        /// <summary>
        /// 工艺备注
        /// </summary>
        public  string  TechnologyPersonRemark{get;set;}

        /// <summary>
        /// 品质备注
        /// </summary>
        public  string  QualityPersonRemark{get;set;}

        public SoftProjectAreaEntity P_Product { get; set; }
        public List<SoftProjectAreaEntity> P_Products { get; set; }
    }
}
