
using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Mvc.Sys;
using System.Transactions;
using SoftProject.CellModel;
using Framework.Web.Mvc;
using System.IO;
using System.Web;

namespace SoftProject.Domain
{
    /// <summary>
    /// 业务层：Design_ModularOrFunDomain
    /// </summary>
    public partial class SoftProjectAreaEntityDomain
    {
        #region 公共部分

        public void Design_ModularOrFun_Domain()
        {
            PKField = "Design_ModularOrFunID";
            //PKFields = new List<string> { "Design_ModularOrFunID" };
            TableName = "Design_ModularOrFun";
        }

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <returns></returns>
        public void Design_ModularOrFun_PKCheck()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块主键不能为空！");
            }
        }

        public MyResponseBase Design_ModularOrFun_GetModular(int? GroupModularOrFun)
        {
            var sql = "SELECT * FROM Design_ModularOrFun WHERE GroupModularOrFun<=2  Order BY  Sort;";
            var resp = Query16(sql, 2);
            return resp;
        }

        public MyResponseBase Design_ModularOrFun_GetByID()
        {
            Design_ModularOrFun_PKCheck();
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE Design_ModularOrFunID={0} ", Item.Design_ModularOrFunID);
            var resp = Query16(sql, 4);
            return resp;
        }

        #endregion

        /// <summary>
        /// 权限管理系统--缓存：获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SoftProjectAreaEntity> Design_ModularOrFun_GetAll()
        {
            //string sql = "SELECT * FROM [V_Design_ModularOrFunControllNew] ";
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine(";WITH T0 AS ");
            //sql.AppendLine("(");
            //sql.AppendLine("	SELECT AreasCode+'.'+A.ControllCode+'.'+B.ActionMethod ControllModularOrFunCode,'/'+AreasCode+'/'+A.ControllCode+'/'+B.ActionMethod ActionPath,B.ParamName");
            //sql.AppendLine("	FROM [dbo].[Design_ModularOrFun] A");
            //sql.AppendLine("	JOIN [dbo].[Design_ModularOrFunControll] B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID");
            //sql.AppendLine(")");
            //sql.AppendLine(",T1 AS");
            //sql.AppendLine("(");
            //sql.AppendLine("	SELECT A.*,T0.ActionPath,T0.ParamName");
            //sql.AppendLine("	FROM Design_ModularOrFun A");
            //sql.AppendLine("	LEFT JOIN T0 ON A.ModularOrFunCode=T0.ControllModularOrFunCode");
            //sql.AppendLine(")");
            //sql.AppendLine("SELECT * ");
            //sql.AppendLine("FROM T1");
            //////////////////////////////////////////////////
            //sql.AppendLine(";WITH T0 AS ");
            //sql.AppendLine("(");
            //sql.AppendLine("	SELECT AreasCode,ControllCode,AreasCode+'.'+A.ControllCode+'.'+B.ActionMethod ControllModularOrFunCode,'/'+AreasCode+'/'+A.ControllCode+'/'+B.ActionMethod ActionPath,B.ParamName");
            //sql.AppendLine("	FROM [dbo].[Design_ModularOrFun] A");
            //sql.AppendLine("	JOIN [dbo].[Design_ModularOrFunControll] B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID");
            //sql.AppendLine(")");
            //sql.AppendLine(",T1 AS");
            //sql.AppendLine("(");
            //sql.AppendLine("	SELECT T0.AreasCode,T0.ControllCode,");
            //sql.AppendLine("	[ModularOrFunCode],[ModularName],[Design_ModularOrFunParentID],[GroupModularOrFun],[Design_ModularPageID]");
            //sql.AppendLine(",[Sort],[ActionPath_Del],[PrimaryKey],[SearchMethod],[CreateUserID],[CreateDate],[CreateUserName],[UpdateUserID],[UpdateDate]");
            //sql.AppendLine(",[UpdateUserName],[BCalCol],[ParamName_del],[BMenu],[PageFormEleTypeName],[QueryFormEleTypeName],[PageType],[TableWidth]");
            //sql.AppendLine(",[ToolbarButtonAreaWidth],[MenuName],[MenuParentID],[LoginCategoryID],[DataRightDropDown],[bPage],[BPrem],[ParentPremID]");
            //sql.AppendLine(",[SortCol],[bFieldsConfigDisp],[MenuPostion],[MainView],[PartialView],[BMenuPanel],[BUrlNva],[bValidModularOrFun],");
            //sql.AppendLine("[bNavModularOrFun],[ModularOrFunRemarks],[PremSort],[PremName],[Design_ModularOrFunControllID],[DBOperTypeFun],[TSql],[ActionCode],[PageTitle]");
            //sql.AppendLine("	ControllModularOrFunCode,T0.ActionPath,T0.ParamName");
            //sql.AppendLine("	FROM Design_ModularOrFun A");
            //sql.AppendLine("	LEFT JOIN T0 ON A.ModularOrFunCode=T0.ControllModularOrFunCode");
            //sql.AppendLine(")");
            //sql.AppendLine("SELECT *");
            //sql.AppendLine("FROM T1");


            sql.AppendLine(";WITH T0 AS ");
            sql.AppendLine("(");
            sql.AppendLine("	SELECT AreasCode,ControllCode,A.PrimaryKey,A.MainPrimaryKey,AreasCode+'.'+A.ControllCode+'.'+B.ActionMethod ControllModularOrFunCode,'/'+AreasCode+'/'+A.ControllCode+'/'+B.ActionMethod ActionPath,B.ParamName");
            sql.AppendLine("	FROM [dbo].[Design_ModularOrFun] A");
            sql.AppendLine("	JOIN [dbo].[Design_ModularOrFunControll] B ON A.Design_ModularOrFunID=B.Design_ModularOrFunID");
            sql.AppendLine(")");
            sql.AppendLine(",T1 AS");
            sql.AppendLine("(");
            sql.AppendLine("	SELECT Design_ModularOrFunID,T0.AreasCode,T0.ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,Sort,");
            sql.AppendLine("ActionPath_Del,SearchMethod,CreateUserID,CreateDate,CreateUserName,UpdateUserID,UpdateDate,UpdateUserName,BCalCol,");
            sql.AppendLine("ParamName_del,BMenu,PageFormEleTypeName,QueryFormEleTypeName,PageType,TableWidth,ToolbarButtonAreaWidth,MenuName,MenuParentID,");
            sql.AppendLine("LoginCategoryID,DataRightDropDown,bPage,BPrem,ParentPremID,SortCol,bFieldsConfigDisp,MenuPostion,MainView,PartialView,");
            sql.AppendLine("BMenuPanel,BUrlNva,bValidModularOrFun,bNavModularOrFun,ModularOrFunRemarks,PremSort,PremName,Design_ModularOrFunControllID,");
            sql.AppendLine("DBOperTypeFun,TSql,ActionCode,A.MenuIdent,PageTitle,ActionPath,ParamName,A.TSqlDefaultSort,A.TabViewName,T0.PrimaryKey,T0.MainPrimaryKey");
            sql.AppendLine("	FROM Design_ModularOrFun A");
            sql.AppendLine("	LEFT JOIN T0 ON A.ModularOrFunCode=T0.ControllModularOrFunCode");
            sql.AppendLine(")");
            sql.AppendLine("SELECT *");
            sql.AppendLine("FROM T1");

            //string sql = "SELECT * FROM [Design_ModularOrFun] ";

            var resp = Query16(sql.ToString(), 2);
            return resp.Items;
        }

        public MyResponseBase Design_ModularOrFun_AddSave()
        {
            MyResponseBase resp = new MyResponseBase();
            Design_ModularOrFun_Domain();
            Item.Design_ModularOrFunParentID = 0;
            //Item.GroupModularOrFun = 2;
            Item.ParentPremID = 0;
            Item.PremSort = 0;

            Item.bValidModularOrFun = 1;
            Item.BMenu = 1;
            Item.BPrem = 1;
            Item.GroupModularOrFun = 2;
            Item.MenuPostion = 2;
            Item.LoginCategoryID = 1;
            Item.PremName = Item.ModularName;
            Item.bFieldsConfigDisp = 1;
            Item.PageFormEleTypeName = "Page01FormEleType";
            Item.PageTitle = Item.ModularName;
            Item.PageType = 1;

            Item.QueryFormEleTypeName = "Query01";
            Item.bPage = 1;
            Item.BCalCol = 0;
            Item.ModularOrFunCode = Item.AreasCode + "." + Item.ControllCode + ".Index";
            //Item.ModularName = ""; 
            Item.MenuName = "列表";
            Item.BUrlNva = 1;
            Item.MainView = "NavPFrame";
            Item.PartialView = "TableContext";
            Item.ToolbarButtonAreaWidth = 3;
            Item.TableWidth = "100%";
            Item.TabViewName = "V_" + Item.ControllCode;
            Item.DBOperTypeFun = 8;
            Item.PrimaryKey = Item.ControllCode + "ID";

            var DBFieldVals = "AreasCode,ControllCode,PrimaryKey,Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //string DBFieldVals = "AreasCode,ControllCode,ModularName,PrimaryKey,Sort,Design_ModularOrFunParentID,bValidModularOrFun,BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            ProjectCache.Design_ModularOrFuns_Clear();
            return resp;
        }


        public MyResponseBase Design_ModularOrFun_EditSave()
        {
            MyResponseBase resp = new MyResponseBase();
            //(1)检查主键ID
            Design_ModularOrFun_PKCheck();

            Design_ModularOrFun_Domain();
            Item.PrimaryKey = Item.ControllCode + "ID";
            string DBFieldVals = "AreasCode,ControllCode,ModularName,PrimaryKey,Sort";//,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,ActionPath,PrimaryKey,SearchMethod,BMenu,MenuName,MenuParentID";
            resp = EditSave(DBFieldVals);
            ProjectCache.Design_ModularOrFuns_Clear();
            return resp;
        }

        public MyResponseBase Design_ModularOrFun_GetByModularOrFunParentID(int? GroupModularOrFun)
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块父节点主键不能为空！");
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS ");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunID={0}");
            sb.AppendLine("	UNION ALL");
            sb.AppendLine("	SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE   Design_ModularOrFunParentID={0} AND GroupModularOrFun={1}");
            sb.AppendLine(")");
            sb.AppendLine("SELECT * FROM T0 ORDER BY Sort");
            var sql = sb.ToString();
            sql = string.Format(sql, Item.Design_ModularOrFunID, GroupModularOrFun);
            var resp = Query16(sql, 2);
            return resp;
        }

        public MyResponseBase Design_ModularOrFun_GetByModularOrFunParentID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块父节点主键不能为空！");
            }
            //Item.Design_ModularOrFunParentID = Item.Design_ModularOrFunID;
            var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE  BMenu=1 AND Design_ModularOrFunParentID={0} AND GroupModularOrFun={1}  ORDER BY Sort ", Item.Design_ModularOrFunParentID, Item.GroupModularOrFun);
            var resp = Query16(sql, 2);
            return resp;
        }

        //public MyResponseBase Design_ModularOrFun_EditListSave()
        //{
        //    MyResponseBase resp = new MyResponseBase();
        //    //(1)检查主键ID
        //    Design_ModularOrFun_PKCheck();

        //    Design_ModularOrFun_Domain();
        //    string DBFieldVals = "AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,ActionPath,PrimaryKey,SearchMethod";
        //    resp = EditSave(DBFieldVals);

        //    return resp;
        //}

        public MyResponseBase Design_ModularOrFun_EditListSave()
        {
            Design_ModularOrFun_Domain();
            #region (2)修改功能模块字段
            using (var scope = new TransactionScope())
            {
                try
                {
                    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

                    #region (1)修改功能模块(无)

                    #endregion

                    #region (3)根据功能模块ID查询所有字段
                    var resptemp = Design_ModularOrFun_GetByModularOrFunParentID(3);
                    #endregion

                    #region (2)模块字段--数据整理
                    Item.Design_ModularOrFuns.ForEach(p =>
                    {
                        //p.Design_ModularOrFunParentID = Item.Design_ModularOrFunID;
                        //p.GroupModularOrFun = 3;
                        //if ( p.ParentPremID!=null)
                        //{
                        //    p.BPrem = 1;
                        //    //p.ParentPremID = Item.Design_ModularOrFunID;
                        //}
                        if (p.Design_ModularOrFunParentID == null)
                            p.Design_ModularOrFunParentID = Item.Design_ModularOrFunID;
                        if (p.GroupModularOrFun == null)
                            p.GroupModularOrFun = 3;
                    });

                    var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunID).Except(from o in Item.Design_ModularOrFuns select o.Design_ModularOrFunID);
                    var updateItems = Item.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID != null);
                    var addItems = Item.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == null);
                    #endregion

                    #region (4)删除元素:执行删除，通过In进行删除
                    //需要写专门语句？delete xxx where ID IN(XXX)
                    if (deleteIDsEnum.Count() > 0)
                    {
                        var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
                        var sql = string.Format("DELETE [dbo].[Design_ModularOrFun] WHERE  Design_ModularOrFunID IN({0})", deleteIDs);
                        resptemp = Query16(sql, 1);
                    }
                    #endregion

                    #region (5)更新模块字段

                    if (updateItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
                        domain.Design_ModularOrFun_Domain();
                        //string DBFieldVals = "Sort,AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,ActionPath,PrimaryKey,SearchMethod,";
                        //DBFieldVals += "PageFormEleTypeName,QueryFormEleTypeName,BMenu,MenuName,PageType,ToolbarButtonAreaWidth,TableWidth,MenuParentID,SortCol,bFieldsConfigDisp,bPage,MenuPostion,MainView,PartialView,BPrem,ParentPremID,LoginCategoryID";
                        //BMenu,MenuName,PageType,TableWidth,ToolbarButtonAreaWidth
                        string DBFieldVals = "Sort,GroupModularOrFun,Design_ModularOrFunParentID,BMenu,MenuPostion,BMenuPanel,LoginCategoryID,ParentPremID,DataRightDropDown,bFieldsConfigDisp,PageFormEleTypeName,QueryFormEleTypeName,SortCol,bPage,PageType,ModularName,MenuName,BUrlNva,SearchMethod,ModularOrFunCode,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,BPrem,bValidModularOrFun,bNavModularOrFun,ModularOrFunRemarks,PremSort,PremName,TSql,DBOperTypeFun,ActionCode,PageTitle,TabViewName,TSqlDefaultSort,BCalCol,MenuIdent";
                        domain.EditSaves(DBFieldVals);
                    }

                    #endregion

                    #region (6)添加

                    if (addItems.Count() > 0)
                    {
                        SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
                        domain.Design_ModularOrFun_Domain();
                        //string DBFieldVals = "Sort,AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,ActionPath,PrimaryKey,SearchMethod,PageFormEleTypeName,QueryFormEleTypeName,BMenu";
                        //string DBFieldVals = "Sort,AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,ActionPath,PrimaryKey,SearchMethod,";
                        //DBFieldVals += "PageFormEleTypeName,QueryFormEleTypeName,BMenu,MenuName,PageType,ToolbarButtonAreaWidth,TableWidth,MenuParentID,SortCol,bFieldsConfigDisp,bPage,MenuPostion,MainView,PartialView,BPrem,ParentPremID,LoginCategoryID";
                        string DBFieldVals = "Sort,GroupModularOrFun,Design_ModularOrFunParentID,BMenu,MenuPostion,BMenuPanel,LoginCategoryID,ParentPremID,DataRightDropDown,bFieldsConfigDisp,PageFormEleTypeName,QueryFormEleTypeName,SortCol,bPage,PageType,ModularName,MenuName,BUrlNva,SearchMethod,ModularOrFunCode,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,BPrem,bValidModularOrFun,bNavModularOrFun,ModularOrFunRemarks,PremSort,PremName,TSql,DBOperTypeFun,ActionCode,PageTitle,TabViewName,TSqlDefaultSort,BCalCol,MenuIdent";
                        domain.AddSaves(DBFieldVals);
                    }

                    #endregion
                    ProjectCache.Design_ModularOrFuns_Clear();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    scope.Dispose();
                }
            }
            #endregion

            return resp;
        }

        ///// <summary>
        ///// 生成页面记录
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFun_BulidRecord()
        //{
        //    var Design_ModularOrFunID = Item.Design_ModularOrFunID;
        //    Design_ModularOrFun_Domain();
        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //    #region 功能模块对象
        //    var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
        //    #endregion

        //    #region 列表页面

        //    #region 列表页面
        //    Item = new SoftProjectAreaEntity
        //    {
        //        PageType=1,
        //        Design_ModularOrFunParentID = Design_ModularOrFunID,
        //        ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Index",
        //        ModularName = Design_ModularOrFun.ModularName + "--列表",
        //        PageFormEleTypeName = "Page01FormEleType",
        //        BMenu=1,
        //        MenuName = Design_ModularOrFun.ModularName,
        //        ToolbarButtonAreaWidth=3,
        //        TableWidth="100%;",
        //        QueryFormEleTypeName = "Query01",
        //        GroupModularOrFun = 3,
        //        Sort = 1,
        //        //ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Add",
        //        SearchMethod = "",
        //        BCalCol = 0,
        //        ParamName = ""
        //    };
        //    //Design_ModularOrFun_PKCheck();

        //    Design_ModularOrFun_Domain();
        //    hOperControl = null;
        //    string DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
        //    DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol";

        //    var resp = AddSave(DBFieldVals);
        //    #endregion

        //    #region 列表页面--按钮
        //    Item.Design_ModularOrFunID = resp.Item.Design_ModularOrFunID;
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 1,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
        //        BtnNameCn = "添加",
        //        OperPos = 1,//工具条
        //        BtnBehavior = 13,
        //    });

        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 3,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
        //        BtnNameCn = "编辑",
        //        OperPos = 2,//工具条
        //        BtnBehavior = 13,
        //    });

        //    Design_ModularOrFunBtn_Domain();
        //    hOperControl = null;
        //    Design_ModularOrFunBtn_EditListSave();
        //    //return null;
        //    #endregion

        //    #region 列表页面--按钮控制器

        //    #region 添加按钮控制器

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add" + "'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Add'");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 编辑按钮--控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Edit'");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);


        //    #endregion

        //    #endregion

        //    #endregion

        //    #region 添加页面
        //    #region 添加页面
        //    Item = new SoftProjectAreaEntity
        //    {
        //        PageType=2,
        //        ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
        //        ModularName = Design_ModularOrFun.ModularName + "--添加",
        //        Design_ModularOrFunParentID = Design_ModularOrFunID,
        //        GroupModularOrFun = 3,
        //        Sort = 3,
        //        //ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Add",
        //        SearchMethod = "",
        //        BCalCol = 0,
        //        ParamName = ""
        //    };
        //    //Design_ModularOrFun_PKCheck();

        //    Design_ModularOrFun_Domain();
        //    hOperControl = null;
        //    DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
        //    DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol";
        //    resp = AddSave(DBFieldVals);
        //    #endregion

        //    #region 添加页面--按钮

        //    //BtnNameCn	    OperPos	BtnBehavior	PopupAfterTableFun
        //    //保存并返回	3	        21	        121
        //    //保存并添加	3	        21	        121
        //    //            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 1,

        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
        //        BtnNameCn = "保存并返回",
        //        OperPos = 3,//脚
        //        BtnBehavior = 21,
        //        PopupAfterTableFun = 121
        //    });

        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 3,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
        //        BtnNameCn = "保存并添加",
        //        OperPos = 3,//脚
        //        BtnBehavior = 21,
        //        PopupAfterTableFun = 121
        //    });
        //    //BtnNameCn	OperPos	BtnBehavior
        //    //返回	        3	    13
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 5,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back",
        //        BtnNameCn = "返回",
        //        OperPos = 3,//脚
        //        BtnBehavior = 13,
        //    });

        //    Design_ModularOrFunBtn_Domain();
        //    hOperControl = null;
        //    Design_ModularOrFunBtn_EditListSave();

        //    #endregion

        //    #region 添加页面--按钮控制器

        //    #region 保存并返回--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index" + "'");
        //    //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Index'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Index') ");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T2 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
        //    sb.AppendLine("	FROM T0,T1 ");
        //    //sb.AppendLine("	ORDER BY ActionMethod");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T2 ");

        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 保存并添加--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add" + "'");
        //    //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Add'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Add') ");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T2 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
        //    sb.AppendLine("	FROM T0,T1 ");
        //    //sb.AppendLine("	ORDER BY ActionMethod");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T2 ");

        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 返回--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back" + "'");

        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Back'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #endregion

        //    #endregion

        //    #region 编辑页面

        //    #region 编辑页面
        //    Item = new SoftProjectAreaEntity
        //    {
        //        PageType=2,
        //        ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
        //        ModularName = Design_ModularOrFun.ModularName + "--编辑",
        //        Design_ModularOrFunParentID = Design_ModularOrFunID,
        //        GroupModularOrFun = 3,
        //        Sort = 5,
        //        //ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Add",
        //        SearchMethod = "",
        //        BCalCol = 0,
        //        ParamName = ""
        //    };
        //    //Design_ModularOrFun_PKCheck();

        //    Design_ModularOrFun_Domain();
        //    hOperControl = null;
        //    DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
        //    DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol";
        //    resp = AddSave(DBFieldVals);

        //    #endregion

        //    #region 编辑页面--按钮

        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {

        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 1,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index",
        //        BtnNameCn = "保存并返回",
        //        OperPos = 3,//脚
        //        BtnBehavior = 21,
        //        PopupAfterTableFun = 121
        //    });

        //    //BtnNameCn	OperPos	BtnBehavior
        //    //返回	        3	    13
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 5,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back",
        //        BtnNameCn = "返回",
        //        OperPos = 3,//脚
        //        BtnBehavior = 13,
        //    });

        //    Design_ModularOrFunBtn_Domain();
        //    hOperControl = null;
        //    Design_ModularOrFunBtn_EditListSave();

        //    #endregion

        //    #region 编辑页面--按钮控制器

        //    #region 保存并返回--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index" + "'");

        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('EditSave','Index') ");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T2 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
        //    sb.AppendLine("	FROM T0,T1 ");
        //    //sb.AppendLine("	ORDER BY ActionMethod");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T2 ");

        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 返回--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back" + "'");

        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Back'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #endregion

        //    #endregion

        //    return resp;
        //}

        public void Design_ModularOrFun_CheckBulid()
        {
            Design_ModularOrFunControll_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            var sqltemp = "SELECT * FROM Design_ModularOrFunControll A WHERE Design_ModularOrFunParentID=" + Item.Design_ModularOrFunID;
            var controllTemps = Query16(sqltemp);
            if (controllTemps.Items.Count > 0)
                throw new Exception("已生成过页面，只能生成1次");
        }


        /// <summary>
        /// 生成页面记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_BulidRecord()
        {
            Design_ModularOrFun_CheckBulid();

            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            #endregion

            #region 最大序号

            var sbsqlMaxSort = new StringBuilder();// "SELECT * FROM "
            sbsqlMaxSort.AppendLine(";WITH T0 AS");
            sbsqlMaxSort.AppendLine("(");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunID={0}");
            sbsqlMaxSort.AppendLine("	UNION ALL");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunParentID={0}");
            sbsqlMaxSort.AppendLine(")");
            sbsqlMaxSort.AppendLine("SELECT MAX(Sort) Sort");
            sbsqlMaxSort.AppendLine("FROM T0");

            var strSqlMaxSort = sbsqlMaxSort.ToString();
            strSqlMaxSort = string.Format(strSqlMaxSort, Design_ModularOrFunID);
            var MaxSortItem = Query16(strSqlMaxSort, 1).Obj;
            var MaxSort = 0;
            if (MaxSortItem == null || MaxSortItem == DBNull.Value)
                MaxSort = 0;
            else
                MaxSort = Convert.ToInt32(MaxSortItem);

            #endregion

            string DBFieldVals = "";
            var fields = "";
            var sbfields = new StringBuilder();
            var strfields = "";

            #region 列表页面

            #region 列表页面
            //Item = new SoftProjectAreaEntity
            //{
            //    Sort = -100,
            //    Design_ModularOrFunParentID = 0,
            //    ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
            //    PremSort = 1,

            //    bValidModularOrFun = 1,
            //    BMenu = 1,
            //    BPrem = 1,
            //    PremName = "列表",

            //    GroupModularOrFun = 2,
            //    MenuPostion = 2,
            //    LoginCategoryID = 1,

            //    bFieldsConfigDisp = 1,
            //    PageFormEleTypeName = "Page01FormEleType",
            //    SortCol = "",
            //    PageTitle = Design_ModularOrFun.ModularName,

            //    PageType = 1,
            //    QueryFormEleTypeName = "Query01",
            //    bPage = 1,
            //    BCalCol = 0,

            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Index",
            //    ModularName = Design_ModularOrFun.ModularName,
            //    MenuName = "列表",
            //    BUrlNva = 1,
            //    MainView = "NavPFrame",
            //    PartialView = "EditPContext2",
            //    ToolbarButtonAreaWidth = 3,
            //    TableWidth = "100%",
            //    TabViewName = "V_" + Design_ModularOrFun.ControllCode,
            //    DBOperTypeFun = 8,
            //};

            ////DBFieldVals = "AreasCode,ControllCode,ModularName,PrimaryKey,Sort,Design_ModularOrFunParentID,bValidModularOrFun,
            ////BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            ////DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            //Design_ModularOrFun_Domain();

            //hOperControl = null;
            //string DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            ////var DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            ////DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,TableWidth,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            //var resp = AddSave(DBFieldVals);
            #endregion

            #region 列表页面--按钮
            Item.Design_ModularOrFunID = Design_ModularOrFun.Design_ModularOrFunID;
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                BtnNameCn = "添加",
                BtnType = 1,
                OperPos = 1,//工具条
                BtnBehavior = 1,//弹窗Get
                bValid=1
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                BtnNameCn = "编辑",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 1,//跳转Get
                bValid = 1
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 5,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail",
                BtnNameCn = "查看",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 1,//跳转Get
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();
            //return null;
            #endregion

            StringBuilder sb = new StringBuilder();

            #region 列表页面--按钮控制器####

            #region 添加按钮控制器

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add" + "'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Add'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 编辑按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Edit'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 3,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #region 查看按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Detail'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 5,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #endregion

            #region 查看权限：列表页面的Detail
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",

            fields = string.Format("'{0}.{1}.Detail'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            //resp.Item.Design_ModularOrFunID
            strfields = string.Format(strfields, Design_ModularOrFun.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 添加页面

            #region 添加页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort+2,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "添加",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                ModularName = Design_ModularOrFun.ModularName + "-添加",
                MenuName = "添加",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 1,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 添加页面--按钮

            //BtnNameCn	    OperPos	BtnBehavior	PopupAfterTableFun
            //保存并返回	3	        21	        121
            //保存并添加	3	        21	        121
            //            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,

                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
                BtnNameCn = "保存并返回",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 101,
                bValid=1
            });
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
                BtnNameCn = "保存并添加",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 102,
                bValid=1
            });
            //BtnNameCn	OperPos	BtnBehavior
            //返回	        3	    13
            //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            //{
            //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
            //    Sort = 5,
            //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back",
            //    BtnNameCn = "返回",
            //    OperPos = 3,//脚
            //    BtnBehavior = 1,
            //});

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 添加页面--按钮控制器 #########

            #region 保存并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Index'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Index') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            Query16(sb.ToString(), 1);

            #endregion

            #region 保存并添加--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Add'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Add') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            //#region 关闭--按钮控制器(无)

            ////sb = new StringBuilder();
            ////sb.AppendLine(";WITH T0 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            ////sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back" + "'");

            //////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Back'");
            ////sb.AppendLine(")");
            ////sb.AppendLine(",T1 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
            ////sb.AppendLine(")");
            ////sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            ////sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            ////sb.AppendLine("FROM T0,T1");

            ////Query16(sb.ToString(), 1);

            ///////////////////////
            ////sb = new StringBuilder();
            ////sb.AppendLine(";WITH T0 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            ////sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back" + "'");

            //////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Back'");
            ////sb.AppendLine(")");
            ////sb.AppendLine(",T1 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
            ////sb.AppendLine(")");
            ////sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            ////sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            ////sb.AppendLine("FROM T0,T1");

            ////Query16(sb.ToString(), 1);

            //#endregion

            #endregion

            #region 添加权限：列表页面的Add、添加页面的保存并返回、保存并添加
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
            fields = string.Format("'{0}.{1}.Add','{0}.{1}.AddSave_Index','{0}.{1}.AddSave_Add'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 编辑页面

            #region 编辑页面--废弃
            //Item = new SoftProjectAreaEntity
            //{
            //    PageType = 2,
            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
            //    ModularName = Design_ModularOrFun.ModularName + "--编辑",
            //    Design_ModularOrFunParentID = Design_ModularOrFunID,
            //    PageFormEleTypeName = "Page03FormEleType",
            //    GroupModularOrFun = 3,
            //    Sort = 5,
            //    //ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Edit",
            //    SearchMethod = "",
            //    //MenuParentID = Design_ModularOrFun.Design_ModularOrFunParentID,
            //    BCalCol = 0,
            //    ParamName = ""
            //};

            ////Design_ModularOrFun_PKCheck();

            //Design_ModularOrFun_Domain();
            //hOperControl = null;
            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            //resp = AddSave(DBFieldVals);

            #endregion

            #region 编辑页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 4,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 3,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "编辑",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                ModularName = Design_ModularOrFun.ModularName + "-编辑",
                MenuName = "编辑",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 2,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 编辑页面--按钮

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {

                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index",
                BtnNameCn = "保存并返回",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 121,
                bValid = 1
            });

            ////BtnNameCn	OperPos	BtnBehavior
            ////返回	        3	    13
            //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            //{
            //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
            //    Sort = 5,
            //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back",
            //    BtnNameCn = "返回",
            //    OperPos = 3,//脚
            //    BtnBehavior = 1,
            //});

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 编辑页面--按钮控制器

            #region 保存并关闭--按钮控制器

            //sb = new StringBuilder();
            //sb.AppendLine(";WITH T0 AS");
            //sb.AppendLine("(");
            //sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
            //sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Row" + "'");

            //sb.AppendLine(")");
            //sb.AppendLine(",T1 AS");
            //sb.AppendLine("(");
            //sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
            //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('EditSave','Row') ");
            //sb.AppendLine(")");
            //sb.AppendLine(",T2 AS");
            //sb.AppendLine("(");
            //sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
            //sb.AppendLine("	FROM T0,T1 ");
            ////sb.AppendLine("	ORDER BY ActionMethod");
            //sb.AppendLine(")");
            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T2 ");

            ////sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            ////sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            ////sb.AppendLine("FROM T0,T1");

            //Query16(sb.ToString(), 1);

            #endregion

            #region 保存并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index" + "'");

            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('EditSave','Index') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 关闭--按钮控制器(无)

            //sb = new StringBuilder();
            //sb.AppendLine(";WITH T0 AS");
            //sb.AppendLine("(");
            //sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            //sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back" + "'");

            ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Back'");
            //sb.AppendLine(")");
            //sb.AppendLine(",T1 AS");
            //sb.AppendLine("(");
            //sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
            //sb.AppendLine(")");
            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            //Query16(sb.ToString(), 1);

            #endregion

            #endregion

            #region 编辑权限：列表页面的Edit、编辑页面的保存并返回
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",

            fields = string.Format("'{0}.{1}.Edit','{0}.{1}.EditSave_Index'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 查看页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 6,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 5,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "查看",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page03FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail",
                ModularName = Design_ModularOrFun.ModularName + "-查看",
                MenuName = "查看",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            return resp;
        }

        /// <summary>
        /// 生成页面记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_BulidRecord010416()
        {
            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            #endregion

            #region 最大序号

            var sbsqlMaxSort = new StringBuilder();// "SELECT * FROM "
            sbsqlMaxSort.AppendLine(";WITH T0 AS");
            sbsqlMaxSort.AppendLine("(");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunID={0}");
            sbsqlMaxSort.AppendLine("	UNION ALL");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunParentID={0}");
            sbsqlMaxSort.AppendLine(")");
            sbsqlMaxSort.AppendLine("SELECT MAX(Sort) Sort");
            sbsqlMaxSort.AppendLine("FROM T0");

            var strSqlMaxSort = sbsqlMaxSort.ToString();
            strSqlMaxSort = string.Format(strSqlMaxSort, Design_ModularOrFunID);
            var MaxSortItem = Query16(strSqlMaxSort, 1).Obj;
            var MaxSort = 0;
            if (MaxSortItem == null || MaxSortItem == DBNull.Value)
                MaxSort = 0;
            else
                MaxSort = Convert.ToInt32(MaxSortItem);

            #endregion

            string DBFieldVals = "";
            var fields = "";
            var sbfields = new StringBuilder();
            var strfields = "";

            #region 列表页面

            #region 列表页面
            //Item = new SoftProjectAreaEntity
            //{
            //    Sort = -100,
            //    Design_ModularOrFunParentID = 0,
            //    ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
            //    PremSort = 1,

            //    bValidModularOrFun = 1,
            //    BMenu = 1,
            //    BPrem = 1,
            //    PremName = "列表",

            //    GroupModularOrFun = 2,
            //    MenuPostion = 2,
            //    LoginCategoryID = 1,

            //    bFieldsConfigDisp = 1,
            //    PageFormEleTypeName = "Page01FormEleType",
            //    SortCol = "",
            //    PageTitle = Design_ModularOrFun.ModularName,

            //    PageType = 1,
            //    QueryFormEleTypeName = "Query01",
            //    bPage = 1,
            //    BCalCol = 0,

            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Index",
            //    ModularName = Design_ModularOrFun.ModularName,
            //    MenuName = "列表",
            //    BUrlNva = 1,
            //    MainView = "NavPFrame",
            //    PartialView = "EditPContext2",
            //    ToolbarButtonAreaWidth = 3,
            //    TableWidth = "100%",
            //    TabViewName = "V_" + Design_ModularOrFun.ControllCode,
            //    DBOperTypeFun = 8,
            //};

            ////DBFieldVals = "AreasCode,ControllCode,ModularName,PrimaryKey,Sort,Design_ModularOrFunParentID,bValidModularOrFun,
            ////BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            ////DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            //Design_ModularOrFun_Domain();

            //hOperControl = null;
            //string DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            ////var DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            ////DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,TableWidth,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            //var resp = AddSave(DBFieldVals);
            #endregion

            #region 列表页面--按钮
            Item.Design_ModularOrFunID = Design_ModularOrFun.Design_ModularOrFunID;
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                BtnNameCn = "添加",
                BtnType = 1,
                OperPos = 1,//工具条
                BtnBehavior = 1,//弹窗Get
                bValid = 1
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                BtnNameCn = "编辑",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 1,//跳转Get
                bValid = 1
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 5,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail",
                BtnNameCn = "查看",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 1,//跳转Get
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();
            //return null;
            #endregion

            StringBuilder sb = new StringBuilder();

            #region 列表页面--按钮控制器####

            #region 添加按钮控制器

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add" + "'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Add'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 编辑按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Edit'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 3,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #region 查看按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Detail'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 5,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #endregion

            #region 查看权限：列表页面的Detail
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",

            fields = string.Format("'{0}.{1}.Detail'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            //resp.Item.Design_ModularOrFunID
            strfields = string.Format(strfields, Design_ModularOrFun.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 添加页面

            #region 添加页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 2,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "添加",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 2,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                ModularName ="我的"+ Design_ModularOrFun.ModularName + "-添加",
                MenuName = "添加",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 1,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 添加页面--按钮

            //BtnNameCn	    OperPos	BtnBehavior	PopupAfterTableFun
            //保存并返回	3	        21	        121
            //保存并添加	3	        21	        121
            //            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,

                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
                BtnNameCn = "保存并返回",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 101,
                bValid = 1
            });
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
                BtnNameCn = "保存并添加",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 102,
                bValid = 1
            });
            //BtnNameCn	OperPos	BtnBehavior
            //返回	        3	    13
            //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            //{
            //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
            //    Sort = 5,
            //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back",
            //    BtnNameCn = "返回",
            //    OperPos = 3,//脚
            //    BtnBehavior = 1,
            //});

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 添加页面--按钮控制器 #########

            #region 保存并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Index'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Index') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            Query16(sb.ToString(), 1);

            #endregion

            #region 保存并添加--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Add'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Add') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            //#region 关闭--按钮控制器(无)

            ////sb = new StringBuilder();
            ////sb.AppendLine(";WITH T0 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            ////sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back" + "'");

            //////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Back'");
            ////sb.AppendLine(")");
            ////sb.AppendLine(",T1 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
            ////sb.AppendLine(")");
            ////sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            ////sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            ////sb.AppendLine("FROM T0,T1");

            ////Query16(sb.ToString(), 1);

            ///////////////////////
            ////sb = new StringBuilder();
            ////sb.AppendLine(";WITH T0 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            ////sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back" + "'");

            //////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Back'");
            ////sb.AppendLine(")");
            ////sb.AppendLine(",T1 AS");
            ////sb.AppendLine("(");
            ////sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            ////sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
            ////sb.AppendLine(")");
            ////sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            ////sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            ////sb.AppendLine("FROM T0,T1");

            ////Query16(sb.ToString(), 1);

            //#endregion

            #endregion

            #region 添加权限：列表页面的Add、添加页面的保存并返回、保存并添加
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
            fields = string.Format("'{0}.{1}.Add','{0}.{1}.AddSave_Index','{0}.{1}.AddSave_Add'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 编辑页面

            #region 编辑页面--废弃
            //Item = new SoftProjectAreaEntity
            //{
            //    PageType = 2,
            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
            //    ModularName = Design_ModularOrFun.ModularName + "--编辑",
            //    Design_ModularOrFunParentID = Design_ModularOrFunID,
            //    PageFormEleTypeName = "Page03FormEleType",
            //    GroupModularOrFun = 3,
            //    Sort = 5,
            //    //ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Edit",
            //    SearchMethod = "",
            //    //MenuParentID = Design_ModularOrFun.Design_ModularOrFunParentID,
            //    BCalCol = 0,
            //    ParamName = ""
            //};

            ////Design_ModularOrFun_PKCheck();

            //Design_ModularOrFun_Domain();
            //hOperControl = null;
            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            //resp = AddSave(DBFieldVals);

            #endregion

            #region 编辑页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 4,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 3,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "编辑",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 2,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                ModularName = "我的" + Design_ModularOrFun.ModularName + "-编辑",
                MenuName = "编辑",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 2,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 编辑页面--按钮

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {

                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index",
                BtnNameCn = "保存并返回",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 121,
                bValid = 1
            });
            
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {

                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Submit_Index",
                BtnNameCn = "提交并返回",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 121,
                bValid = 1
            });

            ////BtnNameCn	OperPos	BtnBehavior
            ////返回	        3	    13
            //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            //{
            //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
            //    Sort = 5,
            //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back",
            //    BtnNameCn = "返回",
            //    OperPos = 3,//脚
            //    BtnBehavior = 1,
            //});

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 编辑页面--按钮控制器

            #region 保存并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index" + "'");

            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('EditSave','Index') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 提交并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Submit_Index" + "'");

            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('Submit','Index') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #endregion

            #region 编辑权限：列表页面的Edit、编辑页面的保存并返回
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
            //EditSave_Index Submit_Index
            fields = string.Format("'{0}.{1}.Edit','{0}.{1}.EditSave_Index','{0}.{1}.Submit_Index'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 查看页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 6,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 5,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "查看",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page03FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail",
                ModularName = "我的" + Design_ModularOrFun.ModularName + "-查看",
                MenuName = "查看",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion



            #region 待审核页面

            #region 待审核页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 9,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = 0,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 1,
                BPrem = 1,
                PremName = "待审核" + Design_ModularOrFun.ModularName,

                GroupModularOrFun = 3,
                MenuPostion = 2,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page06FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 1,
                QueryFormEleTypeName = "Query06",
                bPage = 1,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".IndexWaitAudit",
                ModularName = "待审核" + Design_ModularOrFun.ModularName,
                MenuName = "列表",
                BUrlNva = 1,
                MainView = "NavPFrame",
                PartialView = "TableContext",
                ToolbarButtonAreaWidth = 3,
                TableWidth = "100%",
                TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            resp = AddSave(DBFieldVals);

            var IndexWaitAuditID = resp.Item.Design_ModularOrFunID;
            #endregion

            #region 待审核列表--按钮

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Audit",
                BtnNameCn = "审核",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 1,//跳转Get
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 待审核列表按钮--按钮控制器

            #region 添加按钮控制器

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Audit" + "'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Audit'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #endregion

            #endregion

            #region 审核页面

            #region 审核页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 10,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID =IndexWaitAuditID,// Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 3,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "审核",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page07FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Audit",
                ModularName ="待审核"+ Design_ModularOrFun.ModularName + "-审核",
                MenuName = "审核",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 2,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);

            #endregion

            #region 审核页面--按钮

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {

                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AuditSave_Index",
                BtnNameCn = "审核并返回",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 300,
                PopupAfterTableFun = 121,
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 审核页面--按钮控制器

            #region 审核并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AuditSave_Index" + "'");

            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AuditSave','IndexWaitAudit') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort DESC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            Query16(sb.ToString(), 1);

            #endregion

            #endregion

            #region 编辑权限：列表页面的Edit、编辑页面的保存并返回
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",

            fields = string.Format("'{0}.{1}.Audit','{0}.{1}.AuditSave_Index'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 查询页面

            #region 查询页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 12,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = 0,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 1,
                BPrem = 1,
                PremName =  Design_ModularOrFun.ModularName+"查询",

                GroupModularOrFun = 3,
                MenuPostion = 2,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page11FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 1,
                QueryFormEleTypeName = "Query11",
                bPage = 1,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".IndexSearch",
                ModularName = Design_ModularOrFun.ModularName+"查询",
                MenuName ="列表",
                BUrlNva = 1,
                MainView = "NavPFrame",
                PartialView = "TableContext",
                ToolbarButtonAreaWidth = 3,
                TableWidth = "100%",
                TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            resp = AddSave(DBFieldVals);

            var IndexSearchID = resp.Item.Design_ModularOrFunID;
            #endregion

            #region 查看按钮

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 5,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail2",
                BtnNameCn = "查看",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 1,//跳转Get
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 查看按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail2" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Detail2'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 5,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #region 查看权限：列表页面的Detail
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",

//BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail2",

            fields = string.Format("'{0}.{1}.Detail2'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            //resp.Item.Design_ModularOrFunID
            strfields = string.Format(strfields, Design_ModularOrFun.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 查询页面-查看页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 16,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = IndexSearchID,
                PremSort = 2,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "查看",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page12FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail2",
                ModularName = Design_ModularOrFun.ModularName + "查询-查看",
                MenuName = "查看",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "EditPContext2",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            return resp;
        }

        /// <summary>
        /// 生成页面记录，订单明细模板
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_BulidRecordByOrderDetailTemplete()
        {
            Design_ModularOrFun_CheckBulid();

            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            #endregion

            #region 最大序号

            var sbsqlMaxSort = new StringBuilder();// "SELECT * FROM "
            sbsqlMaxSort.AppendLine(";WITH T0 AS");
            sbsqlMaxSort.AppendLine("(");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunID={0}");
            sbsqlMaxSort.AppendLine("	UNION ALL");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunParentID={0}");
            sbsqlMaxSort.AppendLine(")");
            sbsqlMaxSort.AppendLine("SELECT MAX(Sort) Sort");
            sbsqlMaxSort.AppendLine("FROM T0");

            var strSqlMaxSort = sbsqlMaxSort.ToString();
            strSqlMaxSort = string.Format(strSqlMaxSort, Design_ModularOrFunID);
            var MaxSortItem = Query16(strSqlMaxSort, 1).Obj;
            var MaxSort = 0;
            if (MaxSortItem == null || MaxSortItem == DBNull.Value)
                MaxSort = 0;
            else
                MaxSort = Convert.ToInt32(MaxSortItem);

            #endregion

            string DBFieldVals = "";
            var fields = "";
            var sbfields = new StringBuilder();
            var strfields = "";

            #region 列表页面

            #region 列表页面
            #endregion

            #region 列表页面--按钮
            Item.Design_ModularOrFunID = Design_ModularOrFun.Design_ModularOrFunID;
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Popup",
                BtnNameCn = "添加商品",
                BtnType = 1,
                OperPos = 1,//工具条
                BtnBehavior = 15,//弹窗选择
                bValid = 1
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Delete",
                BtnNameCn = "删除",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 41,//前台删除
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();
            //return null;
            #endregion

            StringBuilder sb = new StringBuilder();

            #region 列表页面--按钮控制器####

            #region 添加按钮控制器

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Popup" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Index'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('Popup','Rows') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort ASC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            Query16(sb.ToString(), 1);

            #endregion

            #region 删除按钮--控制器：由于是前台删除，所以无控制器

            #endregion

            #endregion

            #region 权限：由主表控制：无
            #endregion

            #endregion

            #region 添加Sql配置

            #region 添加页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 2,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "添加",

                GroupModularOrFun = 3,//Sql语句
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 0,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                ModularName = Design_ModularOrFun.ModularName + "-添加",
                MenuName = "添加",
                BUrlNva = 0,
                MainView = "",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 1,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            resp = AddSave(DBFieldVals);

            #endregion

            #endregion

            #region 编辑Sql语句

            #region 编辑页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 4,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 3,

                bValidModularOrFun = 0,
                BMenu = 0,
                BPrem = 0,
                PremName = "编辑",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 0,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                ModularName = Design_ModularOrFun.ModularName + "-编辑",
                MenuName = "编辑",
                BUrlNva = 0,
                MainView = "",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 2,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #endregion

            #region 查看列表
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 6,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 5,

                bValidModularOrFun = 0,
                BMenu = 0,
                BPrem = 0,
                PremName = "查看列表",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page05FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".IndexDetail",
                ModularName = Design_ModularOrFun.ModularName + "-查看列表",
                MenuName = "查看列表",
                BUrlNva = 0,
                MainView = "TableContext",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region Popup
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 8,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 9,

                bValidModularOrFun = 0,
                BMenu = 0,
                BPrem = 0,
                PremName = "Popup",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page07FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 1,
                QueryFormEleTypeName = "Query07",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Popup",
                ModularName = Design_ModularOrFun.ModularName + "-Popup",
                MenuName = "Popup",
                BUrlNva = 0,
                MainView = "NavPFrame",
                PartialView = "TableContext",
                ToolbarButtonAreaWidth = 1,
                TableWidth = "100%",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                TSqlDefaultSort="ProductNo",
                DBOperTypeFun = 8,
                ModularOrFunRemarks="操作按钮(选择)由界面控制"
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            return resp;
        }

        /// <summary>
        /// 生成页面记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_BulidRecordByAtt()
        {
            Design_ModularOrFun_CheckBulid();

            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            #endregion

            #region 最大序号

            var sbsqlMaxSort = new StringBuilder();// "SELECT * FROM "
            sbsqlMaxSort.AppendLine(";WITH T0 AS");
            sbsqlMaxSort.AppendLine("(");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunID={0}");
            sbsqlMaxSort.AppendLine("	UNION ALL");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunParentID={0}");
            sbsqlMaxSort.AppendLine(")");
            sbsqlMaxSort.AppendLine("SELECT MAX(Sort) Sort");
            sbsqlMaxSort.AppendLine("FROM T0");

            var strSqlMaxSort = sbsqlMaxSort.ToString();
            strSqlMaxSort = string.Format(strSqlMaxSort, Design_ModularOrFunID);
            var MaxSortItem = Query16(strSqlMaxSort, 1).Obj;
            var MaxSort = 0;
            if (MaxSortItem == null || MaxSortItem == DBNull.Value)
                MaxSort = 0;
            else
                MaxSort = Convert.ToInt32(MaxSortItem);

            #endregion

            string DBFieldVals = "";
            var fields = "";
            var sbfields = new StringBuilder();
            var strfields = "";
            var FKField=Design_ModularOrFun.ControllCode.Substring(0,Design_ModularOrFun.ControllCode.Length-("Attachment".Length))+"ID";
            #region 编辑列表页面



            #region 列表页面--按钮
            Item.Design_ModularOrFunID = Design_ModularOrFun.Design_ModularOrFunID;

            var  ModularOrFunBtnRemark=string.Format("data-folder=\"/Files/{0}Files\" data-browerext=\"*.*\" data-url=\"/{1}\" data-pkname=\"{2}\"  data-filename=\"AttachmentFileName\" data-fileguid=\"AttachmentFileNameGuid\" data-fileext=\"AttachmentFileType\" data-filesize=\"AttachmentFileSize\" data-identname=\"\""
                , Design_ModularOrFun.ControllCode, Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Upload", FKField);
            //上传按钮
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Upload",
                BtnNameCn = "上传",
                BtnType = 2,
                OperPos = 1,//工具条
                BtnBehavior = 200,//新页：Url查询(Ajax)-插入表格Row
                bValid = 1,
                ModularOrFunBtnRemark = ModularOrFunBtnRemark
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Delete",
                BtnNameCn = "删除",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 42,//前后台删除(弹窗UI)
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();
            //return null;
            #endregion

            StringBuilder sb = new StringBuilder();

            #region 列表页面--按钮控制器####

            #region 上传按钮控制器

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Upload" + "'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Upload'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 删除按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Delete" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Delete'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 3,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #endregion

            #endregion

            #region 查看列表页面
            //Item = new SoftProjectAreaEntity
            //{
            //    Sort = 2,
            //    Design_ModularOrFunParentID = 0,
            //    ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
            //    PremSort = 1,

            //    bValidModularOrFun = 1,
            //    BMenu = 1,
            //    BPrem = 1,
            //    PremName = "查看列表",

            //    GroupModularOrFun = 2,
            //    MenuPostion = 2,
            //    LoginCategoryID = 1,

            //    bFieldsConfigDisp = 1,
            //    PageFormEleTypeName = "Page03FormEleType",
            //    SortCol = "",
            //    PageTitle = Design_ModularOrFun.ModularName,

            //    PageType = 1,
            //    QueryFormEleTypeName = "",
            //    bPage = 1,
            //    BCalCol = 0,

            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Index",
            //    ModularName = Design_ModularOrFun.ModularName,
            //    MenuName = "查看列表",
            //    BUrlNva = 1,
            //    MainView = "TableContext",
            //    PartialView = "",
            //    ToolbarButtonAreaWidth = 3,
            //    TableWidth = "100%",
            //    TabViewName = "V_" + Design_ModularOrFun.ControllCode,
            //    DBOperTypeFun = 8,
            //};

            ////DBFieldVals = "AreasCode,ControllCode,ModularName,PrimaryKey,Sort,Design_ModularOrFunParentID,bValidModularOrFun,
            ////BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            ////DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            //Design_ModularOrFun_Domain();

            //hOperControl = null;
            //DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            ////var DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            ////DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,TableWidth,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            //var respz = AddSave(DBFieldVals);
            #endregion

            #region 上传页面

            #region 上传页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 2,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "上传",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Upload",
                ModularName = Design_ModularOrFun.ModularName + "-上传",
                MenuName = "上传",
                BUrlNva = 0,
                MainView = "",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 1,
                ModularOrFunRemarks = "配置Sql语句"
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            resp = AddSave(DBFieldVals);
            #endregion

            #endregion

            #region 查看页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 6,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 5,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "查看列表",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "Page01FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 1,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".IndexDetail",
                ModularName = Design_ModularOrFun.ModularName + "-查看列表",
                MenuName = "查看列表",
                BUrlNva = 0,
                MainView = "TableContext",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 删除功能
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 8,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 7,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "查看",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 0,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Delete",
                ModularName = Design_ModularOrFun.ModularName + "-删除",
                MenuName = "删除",
                BUrlNva = 0,
                MainView = "",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
                ModularOrFunRemarks = "配置Sql语句"
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 下载列表
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 10,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 9,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "下载列表",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 0,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".IndexDown",
                ModularName = Design_ModularOrFun.ModularName + "-下载列表",
                MenuName = "下载列表",
                BUrlNva = 0,
                MainView = "IndexDown",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
                ModularOrFunRemarks = "专用视图"
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 下载列表
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 12,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 13,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 0,
                PremName = "图片列表",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 0,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 0,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".IndexImage",
                ModularName = Design_ModularOrFun.ModularName + "-图片列表",
                MenuName = "图片列表",
                BUrlNva = 0,
                MainView = "IndexImage",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "V_" + Design_ModularOrFun.ControllCode,
                //TabViewName = "",
                DBOperTypeFun = 8,
                ModularOrFunRemarks="专用视图"
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            return resp;
        }

        /// <summary>
        /// 生成页面记录--弹窗
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_BulidRecordPopup()
        {
            Design_ModularOrFun_CheckBulid();

            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;

            #endregion

            #region 最大序号

            var sbsqlMaxSort = new StringBuilder();// "SELECT * FROM "
            sbsqlMaxSort.AppendLine(";WITH T0 AS");
            sbsqlMaxSort.AppendLine("(");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunID={0}");
            sbsqlMaxSort.AppendLine("	UNION ALL");
            sbsqlMaxSort.AppendLine("	SELECT *");
            sbsqlMaxSort.AppendLine("	FROM [dbo].[Design_ModularOrFun]");
            sbsqlMaxSort.AppendLine("	WHERE Design_ModularOrFunParentID={0}");
            sbsqlMaxSort.AppendLine(")");
            sbsqlMaxSort.AppendLine("SELECT MAX(Sort) Sort");
            sbsqlMaxSort.AppendLine("FROM T0");

            var strSqlMaxSort = sbsqlMaxSort.ToString();
            strSqlMaxSort = string.Format(strSqlMaxSort, Design_ModularOrFunID);
            var MaxSortItem = Query16(strSqlMaxSort, 1).Obj;
            var MaxSort = 0;
            if (MaxSortItem == null || MaxSortItem == DBNull.Value)
                MaxSort = 0;
            else
                MaxSort = Convert.ToInt32(MaxSortItem);

            #endregion

            string DBFieldVals = "";
            var fields = "";
            var sbfields = new StringBuilder();
            var strfields = "";

            #region 列表页面

            #region 列表页面
            //Item = new SoftProjectAreaEntity
            //{
            //    Sort = -100,
            //    Design_ModularOrFunParentID = 0,
            //    ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
            //    PremSort = 1,

            //    bValidModularOrFun = 1,
            //    BMenu = 1,
            //    BPrem = 1,
            //    PremName = "列表",

            //    GroupModularOrFun = 2,
            //    MenuPostion = 2,
            //    LoginCategoryID = 1,

            //    bFieldsConfigDisp = 1,
            //    PageFormEleTypeName = "Page01FormEleType",
            //    SortCol = "",
            //    PageTitle = Design_ModularOrFun.ModularName,

            //    PageType = 1,
            //    QueryFormEleTypeName = "Query01",
            //    bPage = 1,
            //    BCalCol = 0,

            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Index",
            //    ModularName = Design_ModularOrFun.ModularName,
            //    MenuName = "列表",
            //    BUrlNva = 1,
            //    MainView = "NavPFrame",
            //    PartialView = "EditPContext2",
            //    ToolbarButtonAreaWidth = 3,
            //    TableWidth = "100%",
            //    TabViewName = "V_" + Design_ModularOrFun.ControllCode,
            //    DBOperTypeFun = 8,
            //};

            ////DBFieldVals = "AreasCode,ControllCode,ModularName,PrimaryKey,Sort,Design_ModularOrFunParentID,bValidModularOrFun,
            ////BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            ////DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            //Design_ModularOrFun_Domain();

            //hOperControl = null;
            //string DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            ////var DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            ////DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,TableWidth,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            //var resp = AddSave(DBFieldVals);
            #endregion

            #region 列表页面--按钮
            Item.Design_ModularOrFunID = Design_ModularOrFun.Design_ModularOrFunID;
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                BtnNameCn = "添加",
                BtnType = 1,
                OperPos = 1,//工具条
                BtnBehavior = 10,//弹窗Get
                bValid = 1
            });

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                BtnNameCn = "编辑",
                BtnType = 1,
                OperPos = 2,//表格行
                BtnBehavior = 10,//弹窗Get
                bValid = 1
            });

            //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            //{
            //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
            //    Sort = 5,
            //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail",
            //    BtnNameCn = "查看",
            //    BtnType = 1,
            //    OperPos = 2,//表格行
            //    BtnBehavior = 1,//跳转Get
            //    bValid = 1
            //});

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();
            //return null;
            #endregion

            StringBuilder sb = new StringBuilder();

            #region 列表页面--按钮控制器####

            #region 添加按钮控制器

            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add" + "'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Add'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 编辑按钮--控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Edit'");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT 3,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);


            #endregion

            #endregion

            #endregion

            #region 添加页面

            #region 添加页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 2,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 1,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "添加",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 1,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
                ModularName = Design_ModularOrFun.ModularName + "-添加",
                MenuName = "添加",
                BUrlNva = 0,
                MainView = "PopupEdit",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 1,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "ModularName,Sort,Design_ModularOrFunParentID,bValidModularOrFun,";
            //DBFieldVals += "BMenu,BPrem,GroupModularOrFun,MenuPostion,LoginCategoryID,PremName,bFieldsConfigDisp,PageFormEleTypeName,PageTitle,PageType,";
            //DBFieldVals += "QueryFormEleTypeName,bPage,BCalCol,ModularOrFunCode,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";

            //DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
            //DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 添加页面--按钮

            //BtnNameCn	    OperPos	BtnBehavior	PopupAfterTableFun
            //保存并返回	3	        21	        121
            //保存并添加	3	        21	        121
            //            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,

                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
                BtnNameCn = "保存并关闭",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 101,
                PopupAfterTableFun = 101,
                bValid = 1
            });
            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {
                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 3,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
                BtnNameCn = "保存并添加",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 102,
                PopupAfterTableFun = 102,
                bValid = 1
            });
            //BtnNameCn	OperPos	BtnBehavior
            //返回	        3	    13
            //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            //{
            //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
            //    Sort = 5,
            //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back",
            //    BtnNameCn = "返回",
            //    OperPos = 3,//脚
            //    BtnBehavior = 1,
            //});

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 添加页面--按钮控制器 #########

            #region 保存并返回--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Index'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Row') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort ASC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            Query16(sb.ToString(), 1);

            #endregion

            #region 保存并添加--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add" + "'");
            //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Add'");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Row') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort ASC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #endregion

            #region 添加权限：列表页面的Add、添加页面的保存并返回、保存并添加
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",
            fields = string.Format("'{0}.{1}.Add','{0}.{1}.AddSave_Index','{0}.{1}.AddSave_Add'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 编辑页面

            #region 编辑页面--废弃

            #endregion

            #region 编辑页面
            Item = new SoftProjectAreaEntity
            {
                Sort = MaxSort + 4,
                Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
                ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
                PremSort = 3,

                bValidModularOrFun = 1,
                BMenu = 0,
                BPrem = 1,
                PremName = "编辑",

                GroupModularOrFun = 3,
                MenuPostion = 0,
                LoginCategoryID = 1,

                bFieldsConfigDisp = 0,
                PageFormEleTypeName = "Page02FormEleType",
                SortCol = "",
                PageTitle = Design_ModularOrFun.ModularName,

                PageType = 2,
                QueryFormEleTypeName = "",
                bPage = 0,
                BCalCol = 0,

                ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
                ModularName = Design_ModularOrFun.ModularName + "-编辑",
                MenuName = "编辑",
                BUrlNva = 0,
                MainView = "PopupEdit",
                PartialView = "",
                ToolbarButtonAreaWidth = null,
                TableWidth = "",
                TabViewName = "",
                DBOperTypeFun = 2,
            };

            Design_ModularOrFun_Domain();
            hOperControl = null;
            DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            resp = AddSave(DBFieldVals);
            #endregion

            #region 编辑页面--按钮

            Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
            {

                Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
                Sort = 1,
                BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index",
                BtnNameCn = "保存并关闭",
                BtnType = 1,
                OperPos = 3,//脚
                BtnBehavior = 111,//弹窗：保存并关闭
                PopupAfterTableFun = 121,
                bValid = 1
            });

            Design_ModularOrFunBtn_Domain();
            hOperControl = null;
            Design_ModularOrFunBtn_EditListSave();

            #endregion

            #region 编辑页面--按钮控制器

            #region 保存并关闭--按钮控制器

            sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Index" + "'");

            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName,Sort");
            sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('EditSave','Row') ");
            sb.AppendLine(")");
            sb.AppendLine(",T2 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY Sort ASC ) R");
            sb.AppendLine("	FROM T0,T1 ");
            //sb.AppendLine("	ORDER BY ActionMethod");
            sb.AppendLine(")");
            sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            sb.AppendLine("FROM T2 ");

            //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
            //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
            //sb.AppendLine("FROM T0,T1");

            Query16(sb.ToString(), 1);

            #endregion

            #region 关闭--按钮控制器(无)

            #endregion

            #endregion

            #region 编辑权限：列表页面的Edit、编辑页面的保存并返回
            //列表页面的添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
            //添加页面的保存并返回：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Index",
            //添加页面的保存并添加：BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Add",

            fields = string.Format("'{0}.{1}.Edit','{0}.{1}.EditSave_Index'", Design_ModularOrFun.AreasCode, Design_ModularOrFun.ControllCode);
            //var sqlAddPrems = "";
            sbfields = new StringBuilder();
            sbfields.AppendLine(";WITH T0 AS");
            sbfields.AppendLine("(");
            sbfields.AppendLine("	SELECT {0} Design_PremSetID,Design_ModularOrFunBtnID,Design_ModularOrFunBtnID PremSetBtnSort");
            sbfields.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
            sbfields.AppendLine("	WHERE BtnNameEn IN({1})");
            sbfields.AppendLine(")");
            sbfields.AppendLine("INSERT INTO [dbo].[Design_ModularOrFunRefBtn](Design_PremSetID,Design_ModularOrFunBtnID,PremSetBtnSort)");
            sbfields.AppendLine("SELECT * FROM T0");

            strfields = sbfields.ToString();
            strfields = string.Format(strfields, resp.Item.Design_ModularOrFunID, fields);
            Query16(strfields.ToString(), 1);

            #endregion

            #endregion

            #region 查看页面--弹窗无查看页面
            //Item = new SoftProjectAreaEntity
            //{
            //    Sort = MaxSort + 6,
            //    Design_ModularOrFunParentID = Design_ModularOrFun.Design_ModularOrFunID,
            //    ParentPremID = Design_ModularOrFun.Design_ModularOrFunID,
            //    PremSort = 5,

            //    bValidModularOrFun = 1,
            //    BMenu = 0,
            //    BPrem = 0,
            //    PremName = "查看",

            //    GroupModularOrFun = 3,
            //    MenuPostion = 0,
            //    LoginCategoryID = 0,

            //    bFieldsConfigDisp = 1,
            //    PageFormEleTypeName = "Page03FormEleType",
            //    SortCol = "",
            //    PageTitle = Design_ModularOrFun.ModularName,

            //    PageType = 2,
            //    QueryFormEleTypeName = "",
            //    bPage = 0,
            //    BCalCol = 0,

            //    ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Detail",
            //    ModularName = Design_ModularOrFun.ModularName + "-查看",
            //    MenuName = "查看",
            //    BUrlNva = 0,
            //    MainView = "NavPFrame",
            //    PartialView = "EditPContext2",
            //    ToolbarButtonAreaWidth = null,
            //    TableWidth = "",
            //    TabViewName = "V_" + Design_ModularOrFun.ControllCode,
            //    //TabViewName = "",
            //    DBOperTypeFun = 8,
            //};

            //Design_ModularOrFun_Domain();
            //hOperControl = null;
            //DBFieldVals = "Sort,Design_ModularOrFunParentID,ParentPremID,PremSort,bValidModularOrFun,BMenu,BPrem,PremName,GroupModularOrFun,MenuPostion,LoginCategoryID,";
            //DBFieldVals += "bFieldsConfigDisp,PageFormEleTypeName,SortCol,PageTitle,PageType,QueryFormEleTypeName,bPage,BCalCol,";
            //DBFieldVals += "ModularOrFunCode,ModularName,MenuName,BUrlNva,MainView,PartialView,ToolbarButtonAreaWidth,TableWidth,TabViewName,DBOperTypeFun";
            //resp = AddSave(DBFieldVals);
            #endregion

            return resp;
        }

        ///// <summary>
        ///// 生成页面记录:原代码：2015-0807
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFun_BulidRecordPopup()
        //{
        //    var Design_ModularOrFunID = Item.Design_ModularOrFunID;
        //    Design_ModularOrFun_Domain();
        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //    #region 功能模块对象
        //    var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
        //    #endregion

        //    #region 列表页面

        //    #region 列表页面
        //    Item = new SoftProjectAreaEntity
        //    {
        //        PageType = 1,
        //        Design_ModularOrFunParentID = Design_ModularOrFunID,
        //        ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Index",
        //        ModularName = Design_ModularOrFun.ModularName + "--列表",
        //        PageFormEleTypeName = "Page01FormEleType",
        //        //BMenu = 1,
        //        //MenuName = Design_ModularOrFun.ModularName,
        //        ToolbarButtonAreaWidth = 3,
        //        QueryFormEleTypeName = "Query01",
        //        TableWidth = "100%",
        //        ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Index",
        //        GroupModularOrFun = 3,
        //        Sort = 1,
        //        //MenuParentID=Design_ModularOrFun.Design_ModularOrFunParentID,
        //        //ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Add",
        //        SearchMethod = "",
        //        BCalCol = 0,
        //        ParamName = ""
        //    };

        //    Design_ModularOrFun_Domain();
        //    hOperControl = null;
        //    var DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
        //    DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,TableWidth,GroupModularOrFun,Sort,BCalCol,MenuParentID";
        //    var resp = AddSave(DBFieldVals);
        //    #endregion

        //    #region 列表页面--按钮
        //    Item.Design_ModularOrFunID = resp.Item.Design_ModularOrFunID;
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 1,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
        //        BtnNameCn = "添加",
        //        OperPos = 1,//工具条
        //        BtnBehavior = 10,//弹窗Get
        //    });

        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 3,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
        //        BtnNameCn = "编辑",
        //        OperPos = 2,//表格行
        //        BtnBehavior = 10,//弹窗Get
        //    });

        //    Design_ModularOrFunBtn_Domain();
        //    hOperControl = null;
        //    Design_ModularOrFunBtn_EditListSave();
        //    //return null;
        //    #endregion

        //    #region 列表页面--按钮控制器

        //    #region 添加按钮控制器

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add" + "'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Add'");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 编辑按钮--控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameCn='编辑'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Edit'");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);


        //    #endregion

        //    #endregion

        //    #endregion

        //    #region 添加页面
        //    #region 添加页面
        //    Item = new SoftProjectAreaEntity
        //    {
        //        PageType = 2,
        //        ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Add",
        //        ModularName = Design_ModularOrFun.ModularName + "--添加",
        //        Design_ModularOrFunParentID = Design_ModularOrFunID,
        //        GroupModularOrFun = 3,
        //        PageFormEleTypeName = "Page02FormEleType",
        //        Sort = 3,
        //        ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Add",
        //        SearchMethod = "",
        //        BCalCol = 0,
        //        //MenuParentID = Design_ModularOrFun.Design_ModularOrFunParentID,
        //        ParamName = ""
        //    };
        //    //Design_ModularOrFun_PKCheck();

        //    Design_ModularOrFun_Domain();
        //    hOperControl = null;
        //    DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
        //    DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
        //    resp = AddSave(DBFieldVals);
        //    #endregion

        //    #region 添加页面--按钮

        //    //BtnNameCn	    OperPos	BtnBehavior	PopupAfterTableFun
        //    //保存并返回	3	        21	        121
        //    //保存并添加	3	        21	        121
        //    //            sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit" + "'");
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 1,

        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Row_Close",
        //        BtnNameCn = "保存并关闭",
        //        OperPos = 3,//脚
        //        BtnBehavior = 101,
        //        PopupAfterTableFun = 101
        //    });
        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 3,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Row_Add",
        //        BtnNameCn = "保存并添加",
        //        OperPos = 3,//脚
        //        BtnBehavior = 102,
        //        PopupAfterTableFun = 102
        //    });
        //    //BtnNameCn	OperPos	BtnBehavior
        //    //返回	        3	    13
        //    //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    //{
        //    //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //    //    Sort = 5,
        //    //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back",
        //    //    BtnNameCn = "返回",
        //    //    OperPos = 3,//脚
        //    //    BtnBehavior = 13,
        //    //});

        //    Design_ModularOrFunBtn_Domain();
        //    hOperControl = null;
        //    Design_ModularOrFunBtn_EditListSave();

        //    #endregion

        //    #region 添加页面--按钮控制器

        //    #region 保存并关闭--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Row_Close" + "'");
        //    //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Index'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Row') ");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T2 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
        //    sb.AppendLine("	FROM T0,T1 ");
        //    //sb.AppendLine("	ORDER BY ActionMethod");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T2 ");

        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 保存并添加--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Row_Add" + "'");
        //    //            sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Add'");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('AddSave','Row') ");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T2 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
        //    sb.AppendLine("	FROM T0,T1 ");
        //    //sb.AppendLine("	ORDER BY ActionMethod");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T2 ");

        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 关闭--按钮控制器(无)

        //    //sb = new StringBuilder();
        //    //sb.AppendLine(";WITH T0 AS");
        //    //sb.AppendLine("(");
        //    //sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    //sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".AddSave_Back" + "'");

        //    ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='AddSave_Back'");
        //    //sb.AppendLine(")");
        //    //sb.AppendLine(",T1 AS");
        //    //sb.AppendLine("(");
        //    //sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
        //    //sb.AppendLine(")");
        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    //Query16(sb.ToString(), 1);

        //    #endregion

        //    #endregion

        //    #endregion

        //    #region 编辑页面

        //    #region 编辑页面
        //    Item = new SoftProjectAreaEntity
        //    {
        //        PageType = 2,
        //        ModularOrFunCode = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".Edit",
        //        ModularName = Design_ModularOrFun.ModularName + "--编辑",
        //        Design_ModularOrFunParentID = Design_ModularOrFunID,
        //        PageFormEleTypeName = "Page03FormEleType",
        //        GroupModularOrFun = 3,
        //        Sort = 5,
        //        ActionPath = "/" + Design_ModularOrFun.AreasCode + "/" + Design_ModularOrFun.ControllCode + "/Edit",
        //        SearchMethod = "",
        //        //MenuParentID = Design_ModularOrFun.Design_ModularOrFunParentID,
        //        BCalCol = 0,
        //        ParamName = ""
        //    };

        //    //Design_ModularOrFun_PKCheck();

        //    Design_ModularOrFun_Domain();
        //    hOperControl = null;
        //    DBFieldVals = "PageType,Design_ModularOrFunParentID,ModularOrFunCode,ModularName,PageFormEleTypeName,BMenu,";
        //    DBFieldVals += "MenuName,ToolbarButtonAreaWidth,QueryFormEleTypeName,GroupModularOrFun,Sort,BCalCol,MenuParentID";
        //    resp = AddSave(DBFieldVals);

        //    #endregion

        //    #region 编辑页面--按钮

        //    Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    {
        //        Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //        Sort = 1,
        //        BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Row",
        //        BtnNameCn = "保存并关闭",
        //        OperPos = 3,//脚
        //        BtnBehavior = 111,
        //        PopupAfterTableFun = 111
        //    });

        //    //BtnNameCn	OperPos	BtnBehavior
        //    //返回	        3	    13
        //    //Item.Design_ModularOrFunBtns.Add(new SoftProjectAreaEntity
        //    //{
        //    //    Design_ModularOrFunID = resp.Item.Design_ModularOrFunID,
        //    //    Sort = 5,
        //    //    BtnNameEn = Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back",
        //    //    BtnNameCn = "返回",
        //    //    OperPos = 3,//脚
        //    //    BtnBehavior = 13,
        //    //});

        //    Design_ModularOrFunBtn_Domain();
        //    hOperControl = null;
        //    Design_ModularOrFunBtn_EditListSave();

        //    #endregion

        //    #region 编辑页面--按钮控制器

        //    #region 保存并关闭--按钮控制器

        //    sb = new StringBuilder();
        //    sb.AppendLine(";WITH T0 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Index'");
        //    sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Row" + "'");

        //    sb.AppendLine(")");
        //    sb.AppendLine(",T1 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT Design_ModularOrFunControllID,ActionName");
        //    sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod IN('EditSave','Row') ");
        //    sb.AppendLine(")");
        //    sb.AppendLine(",T2 AS");
        //    sb.AppendLine("(");
        //    sb.AppendLine("	SELECT TOP 1000 Design_ModularOrFunBtnID,Design_ModularOrFunControllID,ROW_NUMBER() OVER(ORDER BY ActionName DESC ) R");
        //    sb.AppendLine("	FROM T0,T1 ");
        //    //sb.AppendLine("	ORDER BY ActionMethod");
        //    sb.AppendLine(")");
        //    sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    sb.AppendLine("SELECT R,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    sb.AppendLine("FROM T2 ");

        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    Query16(sb.ToString(), 1);

        //    #endregion

        //    #region 关闭--按钮控制器(无)

        //    //sb = new StringBuilder();
        //    //sb.AppendLine(";WITH T0 AS");
        //    //sb.AppendLine("(");
        //    //sb.AppendLine("	SELECT Design_ModularOrFunBtnID");
        //    //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunBtn] A");
        //    //sb.AppendLine("	WHERE BtnNameEn='" + Design_ModularOrFun.AreasCode + "." + Design_ModularOrFun.ControllCode + ".EditSave_Back" + "'");

        //    ////sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND BtnNameEn='EditSave_Back'");
        //    //sb.AppendLine(")");
        //    //sb.AppendLine(",T1 AS");
        //    //sb.AppendLine("(");
        //    //sb.AppendLine("	SELECT Design_ModularOrFunControllID");
        //    //sb.AppendLine("	FROM [dbo].[Design_ModularOrFunControll] A");
        //    //sb.AppendLine("	WHERE Design_ModularOrFunID=" + Design_ModularOrFunID + " AND ActionMethod='Index' ");
        //    //sb.AppendLine(")");
        //    //sb.AppendLine("INSERT INTO Design_ModularOrFunBtnControll(Sort,Design_ModularOrFunBtnID,Design_ModularOrFunControllID)");
        //    //sb.AppendLine("SELECT 1,Design_ModularOrFunBtnID,Design_ModularOrFunControllID");
        //    //sb.AppendLine("FROM T0,T1");

        //    //Query16(sb.ToString(), 1);

        //    #endregion

        //    #endregion

        //    #endregion

        //    return resp;
        //}

        /// <summary>
        /// 所有权限码
        /// </summary>
        /// <returns></returns>
        public MyResponseBase GetAllPrem()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(";WITH T0 AS ");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT top 1000");
            sb.AppendLine("	Design_ModularOrFunID,ModularName,Design_ModularOrFunParentID,Sort");
            sb.AppendLine("	FROM Design_ModularOrFun A");
            sb.AppendLine("	WHERE  BMenu=1");
            sb.AppendLine(")");
            sb.AppendLine(",T1 AS");
            sb.AppendLine("(");
            sb.AppendLine("	SELECT B.Design_ModularOrFunControllID+10000 Design_ModularOrFunID,B.ActionMethodCN,");
            sb.AppendLine("	T0.Design_ModularOrFunID Design_ModularOrFunParentID,B.SORT");
            sb.AppendLine("	FROM T0 ");
            sb.AppendLine("	JOIN Design_ModularOrFunControll B  ON T0.Design_ModularOrFunID=B.Design_ModularOrFunID");
            sb.AppendLine("	WHERE BPrem=1");
            sb.AppendLine(")");
            sb.AppendLine("SELECT * FROM T0");
            sb.AppendLine("UNION ALL");
            sb.AppendLine("SELECT * FROM T1");

            Sys_HOperControl = null;
            var resp = Query16(sb.ToString());
            return resp;
        }

        /// <summary>
        /// 获取功能模块的权限
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_GetPremByModularOrFunParentID()
        {
            if (Item.Design_ModularOrFunID == null)
            {
                throw new Exception("功能模块父节点主键不能为空！");
            }
            //Item.Design_ModularOrFunParentID = Item.Design_ModularOrFunID;
            //var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE Design_ModularOrFunParentID={0} AND GroupModularOrFun=3 ", Item.Design_ModularOrFunID);
            //sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE Design_ModularOrFunParentID={0} AND GroupModularOrFun=3 ", Item.Design_ModularOrFunID);

            //var sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE Design_ModularOrFunParentID={0} AND GroupModularOrFun=4 ", Item.Design_ModularOrFunID);
            //sql = string.Format(";SELECT * FROM [dbo].[Design_ModularOrFun] A WHERE Design_ModularOrFunParentID={0} AND GroupModularOrFun=4 ", Item.Design_ModularOrFunID);
            var sbsql = new StringBuilder();

            sbsql.AppendLine(";WITH T0 AS");
            sbsql.AppendLine("(");
            sbsql.AppendLine("	SELECT * ");
            sbsql.AppendLine("	FROM [dbo].[Design_ModularOrFun] A ");
            sbsql.AppendLine("	WHERE Design_ModularOrFunParentID=" + Item.Design_ModularOrFunID + " AND GroupModularOrFun=4 ");
            sbsql.AppendLine(")");
            sbsql.AppendLine(",T1 AS");
            sbsql.AppendLine("(");
            sbsql.AppendLine("	SELECT T0.*,A.ModularName ParentModularName");
            sbsql.AppendLine("	FROM Design_ModularOrFun A");
            sbsql.AppendLine("	JOIN T0 ON A.Design_ModularOrFunID=T0.Design_ModularOrFunParentID ");
            sbsql.AppendLine(")");
            sbsql.AppendLine(",T2 AS");
            sbsql.AppendLine("(");
            sbsql.AppendLine("	SELECT * ");
            sbsql.AppendLine("	FROM [dbo].[Design_ModularOrFun] A ");
            sbsql.AppendLine("	WHERE Design_ModularOrFunParentID=" + Item.Design_ModularOrFunID + " AND GroupModularOrFun=3	");
            sbsql.AppendLine(")");
            sbsql.AppendLine(",T3 AS");
            sbsql.AppendLine("(");
            sbsql.AppendLine("	SELECT A.*,T2.ModularName ParentModularName");
            sbsql.AppendLine("	FROM Design_ModularOrFun A");
            sbsql.AppendLine("	JOIN T2 ON A.Design_ModularOrFunParentID=T2.Design_ModularOrFunID AND A.GroupModularOrFun=4 ");
            sbsql.AppendLine(")");
            sbsql.AppendLine("SELECT * FROM T1");
            sbsql.AppendLine("UNION ALL");
            sbsql.AppendLine("SELECT * FROM T3");
            sbsql.AppendLine("order by sort");


            var resp = Query16(sbsql.ToString(), 2);
            return resp;


        }

        //public MyResponseBase Design_ModularOrFun_EditListPremSave()
        //{
        //    Design_ModularOrFun_Domain();

        //    #region (2)修改功能模块字段
        //    using (var scope = new TransactionScope())
        //    {
        //        try
        //        {
        //            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //            #region (1)修改功能模块(无)

        //            #endregion

        //            #region (3)根据功能模块ID查询所有字段
        //            var resptemp = Design_ModularOrFun_GetByModularOrFunParentID(4);
        //            #endregion

        //            #region (2)模块字段--数据整理
        //            Item.Design_ModularOrFuns.ForEach(p =>
        //            {
        //                if (p.ParentPremID == null)
        //                {
        //                    p.ParentPremID = Item.Design_ModularOrFunID;
        //                }
        //                p.Design_ModularOrFunParentID = Item.Design_ModularOrFunID;
        //                p.BPrem = 1;
        //                p.GroupModularOrFun = 4;
        //            });

        //            var deleteIDsEnum = (from p in resptemp.Items select p.Design_ModularOrFunID).Except(from o in Item.Design_ModularOrFuns select o.Design_ModularOrFunID);
        //            var updateItems = Item.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID != null && !deleteIDsEnum.Contains(p.Design_ModularOrFunID));
        //            var addItems = Item.Design_ModularOrFuns.Where(p => p.Design_ModularOrFunID == null);
        //            #endregion

        //            #region (4)删除元素:执行删除，通过In进行删除
        //            //需要写专门语句？delete xxx where ID IN(XXX)
        //            if (deleteIDsEnum.Count() > 0)
        //            {
        //                var deleteIDs = string.Join(",", deleteIDsEnum);//deleteForecastIDsEnum.ToArray()
        //                var sql = string.Format("DELETE [dbo].[Design_ModularOrFun] WHERE  Design_ModularOrFunID IN({0})", deleteIDs);
        //                resptemp = Query16(sql, 1);
        //            }
        //            #endregion

        //            #region (5)更新模块字段

        //            if (updateItems.Count() > 0)
        //            {
        //                SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = updateItems.ToList() };
        //                domain.Design_ModularOrFun_Domain();
        //                string DBFieldVals = "Sort,AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,PrimaryKey,SearchMethod,";
        //                DBFieldVals += "PageFormEleTypeName,QueryFormEleTypeName,BMenu,MenuName,PageType,ToolbarButtonAreaWidth,TableWidth,MenuParentID,BPrem,ParentPremID,LoginCategoryID,DataRightDropDown";
        //                //BMenu,MenuName,PageType,TableWidth,ToolbarButtonAreaWidth
        //                domain.EditSaves(DBFieldVals);
        //            }

        //            #endregion

        //            #region (6)添加

        //            if (addItems.Count() > 0)
        //            {
        //                SoftProjectAreaEntityDomain domain = new SoftProjectAreaEntityDomain { Items = addItems.ToList() };
        //                domain.Design_ModularOrFun_Domain();
        //                //string DBFieldVals = "Sort,AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,ActionPath,PrimaryKey,SearchMethod,PageFormEleTypeName,QueryFormEleTypeName,BMenu";
        //                string DBFieldVals = "Sort,AreasCode,ControllCode,ModularOrFunCode,ModularName,Design_ModularOrFunParentID,GroupModularOrFun,Design_ModularPageID,PrimaryKey,SearchMethod,";
        //                DBFieldVals += "PageFormEleTypeName,QueryFormEleTypeName,BMenu,MenuName,PageType,ToolbarButtonAreaWidth,TableWidth,MenuParentID,BPrem,ParentPremID,LoginCategoryID,DataRightDropDown";
        //                domain.AddSaves(DBFieldVals);
        //            }

        //            #endregion
        //            scope.Complete();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //        finally
        //        {
        //            scope.Dispose();
        //        }
        //    }
        //    #endregion

        //    return resp;
        //}

        ///// <summary>
        ///// 所有权限码
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFun_AllPrems()
        //{
        //    var sql = string.Format("SELECT *  FROM [dbo].[V_AllPrem]");
        //    var resp = Query16(sql, 2);
        //    return resp;
        //}

        /// <summary>
        /// 生成页面记录
        /// </summary>
        /// <returns></returns>
        public MyResponseBase Design_ModularOrFun_BulidSql()
        {
            if (Item.Design_ModularOrFunID == null)
                throw new Exception("主键不能为空");
            var Design_ModularOrFunID = Item.Design_ModularOrFunID;
            Design_ModularOrFun_Domain();
            SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

            #region 功能模块对象
            var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
            #endregion

            if (Item.DBOperTypeFun == 1 || Item.DBOperTypeFun == 2)
            {
                var DBFieldVals = HtmlHelpersProject.GetPageSaveEleTypes(Design_ModularOrFun);
                Item.TSql = DBFieldVals;
                //var sql =string.Format("Update Design_ModularOrFun  SET DBOperTypeFun={0},TSql={1} WHERE  Design_ModularOrFunID={2}",Item.DBOperTypeFun,DBFieldVals,Item.Design_ModularOrFunID);
                DBFieldVals = "DBOperTypeFun,TSql";
                resp = EditSave(DBFieldVals);
                ProjectCache.Design_ModularOrFuns_Clear();
            }

            return resp;
        }

        ///// <summary>
        ///// 生成页面记录
        ///// </summary>
        ///// <returns></returns>
        //public MyResponseBase Design_ModularOrFun_BulidPage()
        //{
        //    if (Item.Design_ModularOrFunID == null)
        //        throw new Exception("主键不能为空");
        //    var Design_ModularOrFunID = Item.Design_ModularOrFunID;
        //    Design_ModularOrFun_Domain();
        //    SoftProjectAreaEntity hOperControl = new SoftProjectAreaEntity { };

        //    #region 功能模块对象
        //    var Design_ModularOrFun = Design_ModularOrFun_GetByID().Item;
        //    #endregion

        //    if (Design_ModularOrFun.PageType == 1)//Table页面
        //    {
        //        BulidTable(Design_ModularOrFun);
        //    }

        //    return resp;
        //}


    }
}
