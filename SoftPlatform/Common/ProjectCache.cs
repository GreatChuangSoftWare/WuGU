using SoftProject.CellModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace SoftProject.Domain
{
    public partial class ProjectCache
    {
        //public static string ModularOrFunCode { get; set; }
        //public static SoftProjectAreaEntity Design_ModularOrFun
        //{
        //    get
        //    {
        //        //var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode && p.ActionMethod == ActionName).FirstOrDefault();
        //        var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
        //        return Design_ModularOrFun;
        //    }
        //}

        public static SoftProjectAreaEntity GetDesign_ModularOrFun(string ModularOrFunCode)
        {
            var Design_ModularOrFun = ProjectCache.Design_ModularOrFuns.Where(p => p.ModularOrFunCode == ModularOrFunCode).FirstOrDefault();
            return Design_ModularOrFun;
        }


        #region 获取所有字典数据：(1)字典添加修改时清空(2)在Home控制器中设置1个Acion完成

        static List<SoftProjectAreaEntity> _Sys_Dicts = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Sys_Dicts
        {
            get
            {
                if (_Sys_Dicts.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Sys_Dicts = domain.Sys_Dict_AreaGetAll();
                }
                return _Sys_Dicts;
            }
            set
            {
                _Sys_Dicts = value;
            }
        }

        public static void ClearSys_Dicts()
        {
            _Sys_Dicts = new List<SoftProjectAreaEntity>();
        }

        //public static Dictionary<string, SoftProjectAreaEntity> _DropDicts = new Dictionary<string, SoftProjectAreaEntity> { 
        //    {"StorageCategoryID",new SoftProjectAreaEntity{Category="StorageType",DText="==入库类型=="}},
        //    {"SupplyUnitID",new SoftProjectAreaEntity{Category="SupplyUnit",DText="==供应商=="}}
        //};

        /// <summary>
        /// 获取某种类型的集合
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public static List<SoftProjectAreaEntity> GetByCategory(string Category)
        {
            var sys_Dicts = Sys_Dicts.Where(p => p.Category == Category);
            if (sys_Dicts.Count() > 0)
                sys_Dicts = sys_Dicts.OrderBy(p => p.DictSortID).ToList();
            return sys_Dicts.ToList();
        }

        //<summary>
        //获取某种类型的集合
        //</summary>
        //<param name="Category"></param>
        //<returns></returns>
        public static bool IsExistyCategory(string Category)
        {
            //List<Sys_Dict> Sys_Dicts = new List<Sys_Dict>();
            var sys_Dicts = Sys_Dicts.Where(p => p.Category == Category);
            if (sys_Dicts.Count() > 0)
                return true;
            return false;
        }

        ////<summary>
        ////获取某种类型的集合
        ////</summary>
        ////<param name="Category"></param>
        ////<returns></returns>
        //public static List<SoftProjectAreaEntity> GetByCategory(string Category)
        //{
        //    //List<Sys_Dict> Sys_Dicts = new List<Sys_Dict>();
        //    var sys_Dicts = Sys_Dicts.Where(p => p.Category == Category);
        //    if (sys_Dicts.Count() > 0)
        //        sys_Dicts = sys_Dicts.OrderBy(p => p.DValue).ToList();
        //    return sys_Dicts.ToList();
        //}

        //<summary>
        //获取某种类型的DValue值对应的DText值
        //</summary>
        //<param name="Category"></param>
        //<returns></returns>
        public static string GetDTextByCategoryDValue(string Category, string DValue)
        {
            string dText = "";
            var _dTexts = Sys_Dicts.Where(p => p.Category == Category && p.DValue == DValue);
            if (_dTexts.Count() > 0)
                dText = _dTexts.First().DText;
            return dText;
        }

        //<summary>
        //获取某种类型的DValue值对应的DText值
        //</summary>
        //<param name="Category"></param>
        //<returns></returns>
        public static string GetDValueByCategoryDText(string Category, string DText)
        {
            string DValue = "";
            var _dTexts = Sys_Dicts.Where(p => p.Category == Category && p.DText == DText);
            if (_dTexts.Count() > 0)
                DValue = _dTexts.First().DValue;
            return DValue;
        }

        #endregion

        #region 一、数据库操作：完成Sql语句保存时，清空

        static List<SoftProjectAreaEntity> _Sys_HOperControls = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Sys_HOperControls
        {
            get
            {
                if (_Sys_HOperControls.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Sys_HOperControls = domain.Design_ModularOrFunSql_GetAll().Items;
                }
                return _Sys_HOperControls;
            }
        }

        public static void ClearHOperControls()
        {
            _Sys_HOperControls = new List<SoftProjectAreaEntity>();
        }

        public static string QuerySql = ";WITH T1000 AS (SELECT * 	FROM {0} A 	WHERE 1=1  	sqlplaceholder )";

        public static string GetQuerySql(string ViewName)
        {
            var sql = string.Format(QuerySql, ViewName);
            return sql;
        }

        public static string QuerySqlByID = "SELECT * FROM {0} A WHERE {1}={2} ";

        public static string GetQuerySqlByID(string ViewName, string PkName, int? PkValue)
        {
            var sql = string.Format(QuerySqlByID, ViewName, PkName, PkValue);
            return sql;
        }


        #endregion

        #region 所有功能模块权限：用于查找子菜单:在修改模块功能、页面信息时清空

        static List<SoftProjectAreaEntity> _Design_ModularOrFuns = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Design_ModularOrFuns
        {
            get
            {
                if (_Design_ModularOrFuns.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Design_ModularOrFuns = domain.Design_ModularOrFun_GetAll();
                    _Design_ModularOrFuns.ForEach(p =>
                    {
                        if (string.IsNullOrEmpty(p.SearchMethod))
                            p.SearchMethod = "Framework.FwSearch";
                        p.ToolbarSearchAreaWidth = 12 - p.ToolbarButtonAreaWidth;
                        if (p.TableWidth == null)
                            p.TableWidth = "100%";
                    });
                }
                return _Design_ModularOrFuns;
            }
        }

        public static void Design_ModularOrFuns_Clear()
        {
            _Design_ModularOrFuns = new List<SoftProjectAreaEntity>();
        }
        #endregion

        #region 功能页面字段：在保存页面字段时清空

        static List<SoftProjectAreaEntity> _Design_ModularPageFields = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Design_ModularPageFields
        {
            get
            {
                if (_Design_ModularPageFields.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Design_ModularPageFields = domain.Design_ModularPageField_GetAll();
                }
                return _Design_ModularPageFields;
            }
        }

        public static void Design_ModularPageFields_Clear()
        {
            _Design_ModularPageFields = new List<SoftProjectAreaEntity>();
        }

        #endregion

        #region 权限按钮：在保存权限对应的页面按钮时清空


        static List<SoftProjectAreaEntity> _Design_ModularOrFunRefBtns = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Design_ModularOrFunRefBtns
        {
            get
            {
                if (_Design_ModularOrFunRefBtns.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Design_ModularOrFunRefBtns = domain.Design_ModularOrFunRefBtn_GetAll();
                }
                return _Design_ModularOrFunRefBtns;
            }
        }

        public static void ClearDesign_ModularOrFunRefBtns()
        {
            _Design_ModularOrFunRefBtns = new List<SoftProjectAreaEntity>();
        }

        #endregion

        #region 按钮控制器：用于生成页面按钮的url地址：在保存按钮=>控制器时清空

        static List<SoftProjectAreaEntity> _Design_ModularOrFunBtnControlls = new List<SoftProjectAreaEntity>();

        public static List<SoftProjectAreaEntity> Design_ModularOrFunBtnControlls
        {
            get
            {
                if (_Design_ModularOrFunBtnControlls.Count == 0)
                {
                    SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain();
                    _Design_ModularOrFunBtnControlls = domain.Design_ModularOrFunBtnControll_GetAll();
                }
                return _Design_ModularOrFunBtnControlls;
            }
        }

        public static void Design_ModularOrFunBtnControlls_Clear()
        {
            _Design_ModularOrFunBtnControlls = new List<SoftProjectAreaEntity>();
        }

        #endregion
        
        #region 服务器权限验证

        #region 所有权限：用于服务器权限拦截：在修改权限按钮、按钮控制器时清空缓存

        //static List<SoftProjectAreaEntity> _Sys_PremSetsAll = new List<SoftProjectAreaEntity>();
        //public static List<SoftProjectAreaEntity> Sys_PremSetsAll
        //{
        //    get
        //    {
        //        if (_Sys_PremSetsAll.Count == 0)
        //        {
        //            var domain = new SoftProjectAreaEntityDomain();
        //            _Sys_PremSetsAll = domain.Pre_RolePremSet_PremSetsAll().Items;

        //            //var domain = new SoftProjectAreaEntityDomain();
        //            //_Sys_PremSetsAll = domain.Design_ModularOrFun_AllPrems().Items;
        //        }
        //        return _Sys_PremSetsAll;
        //    }
        //}

        //public static void Sys_PremSetsAll_Clear()
        //{
        //    _Sys_PremSetsAll = new List<SoftProjectAreaEntity>();
        //}

        #endregion

        #endregion

    }

}