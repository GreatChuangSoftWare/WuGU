/// <reference path='jquery-1.9.1.intellisense.js' />

Import('Pre_Organizations');

(function (Pre_Organizations) {

    Pre_Organizations.Init = function () {

        var initData = {
            "areasRoute": "AuthorizationAreas",
            "modularName": "部门",
            "primaryKey": "Pre_OrganizationID",
            "popupwidth": "500",
            "indexUrl": "Pre_Organization/Index",
            "addUrl": "Pre_Organization/Add",
            "editUrl": "Pre_Organization/Edit",
            "deleteUrl": "Pre_Organization/Delete",
            "detailUrl": "Pre_Organization/Detail",
            "tbrowUrl": "Pre_Organization/TableRow",
            "tbhead": $("#IndexPPre_Organization #tbhead").data(),
            "tbbody": $("#IndexPPre_Organization #tbbody")
        };

        var trPre_OrganizationsDataFieldMap = new Hashtable();
        trPre_OrganizationsDataFieldMap.add('pre_organizationid', 'Pre_OrganizationID');
        trPre_OrganizationsDataFieldMap.add('organizationcode', 'OrganizationCode');
        trPre_OrganizationsDataFieldMap.add('organizationname', 'OrganizationName');
        trPre_OrganizationsDataFieldMap.add('shortorganizationname', 'ShortOrganizationName');
        trPre_OrganizationsDataFieldMap.add('parentorganizationcode', 'ParentOrganizationCode');
        trPre_OrganizationsDataFieldMap.add('descriptions', 'Descriptions');
        trPre_OrganizationsDataFieldMap.add('isfinallevel', 'IsFinalLevel');
        trPre_OrganizationsDataFieldMap.add('isentity', 'IsEntity');
        trPre_OrganizationsDataFieldMap.add('pinyinmnemoniccode', 'PinyinMnemonicCode');
        trPre_OrganizationsDataFieldMap.add('wubimnemoniccode', 'WubiMnemonicCode');
        trPre_OrganizationsDataFieldMap.add('sortid', 'SortID');
        trPre_OrganizationsDataFieldMap.add('state', 'State');
        trPre_OrganizationsDataFieldMap.add('statecn', 'StateCn');

        trPre_OrganizationsDataFieldMap.add('createddate', 'CreatedDate');
        trPre_OrganizationsDataFieldMap.add('createduserid', 'CreatedUserID');
        trPre_OrganizationsDataFieldMap.add('createdusername', 'CreatedUserName');
        trPre_OrganizationsDataFieldMap.add('modifieddate', 'ModifiedDate');
        trPre_OrganizationsDataFieldMap.add('modifieduserid', 'ModifiedUserID');
        trPre_OrganizationsDataFieldMap.add('modifiedusername', 'ModifiedUserName');

        initData["rowFieldMap"] = trPre_OrganizationsDataFieldMap;

        var tmp = "" +
       "<tr   data-pre_organizationid='{Pre_OrganizationID}'  data-organizationname='{OrganizationName}'  data-sortid='{SortID}'   data-statecn='{StateCn}'  >" +
       "    <td>" +
        "    </td>" +
        "    <td>" +
        "            <button class='btn btn-xs btn-Edit btn-info'><i class='fa fa-edit fa-1'></i>编辑</button>" +
        "" +
        "    </td>" +
        "    <td class='align-left'>{ParentOrganizationName}</td>" +
        "    <td class='align-left'>{OrganizationName}</td>" +
        "    <td>{SortID}</td>" +
        "    <td>{StateCn}</td>" +
        "</tr>";

        var tmpMap = Framework.Tool.TmpKeys(tmp);
        initData["RowTemp"]=tmp;
        initData["RowTempMap"] = tmpMap;

        /////////////JS代码//////////////////////

        $('#IndexPPre_Organization #toolbarbtn .btn-Add').click(function (e) {
            //alert();
            //获取组织机构父节点
            //data, title, width, url, tmp, tmpMap, tbbody;
            //var tbbody = $("#IndexPPre_Organization #tbbody");
            //Framework.JsFunHandle.Add({}, "添加部门", "500", "AuthorizationAreas/Pre_Organization/Add", tmp, tmpMap, tbbody);
            ///////////////////////////////////////////////////////////
            var data = {};
            Framework.JsFunHandle.Add(data, 2, initData, function (popup) {
                //alert("添加--操作完成");
            });

        });


        $(document).delegate('#IndexPPre_Organization #tbbody  .btn-Edit', 'click', function (e) {
            //var obj = $(this);
            //Framework.JsFunHandle.Edit(obj, "修改部门", "500", "Pre_Organization/Edit", tmp, tmpMap, trPre_OrganizationsDataFieldMap);
            var obj = $(this);
            Framework.JsFunHandle.Edit(obj, {}, 2, initData);

        });

        //////////////////快速：查询事件/////////////////////
        $('#IndexPPre_Organization .btn-Search').click(function () {
            $('#searchType').val('0');
            Pre_Organizations.Search({});
        });

        ////////////////高级查询/////////////////////////////
        //$('#mainPage .btn-Search').click(function () {
        //    $('#searchType').val('1');
        //    Pre_Organizations.Search({});
        //});

        ////////////////查询：pageIndex：当前页//////////////
        Pre_Organizations.Search = function (params) {
            //alert("sdd");
            var data = {};
            //(1)找到查询条件：快速查询、高级查询
            var searchType = $('#searchType').val() || 0;
            if (searchType == 0)
                data = Framework.Form.GetFormItemByObj($('#fastSearchArea'));
            else
                data = Framework.Form.GetFormItemByObj($('.SearchArea'));

            data['searchType'] = searchType;
            //data['']
            var url = Framework.Page.BulidUrl($('#pageArea'), data, params, 'AuthorizationAreas/Pre_Organization/Index');
            //alert(url);
            document.location.href = url;
        };

        ///////////////////单行启用////////////////////////
        //$(document).delegate('#IndexPPre_Organization #tbbody .btn-Start', 'click', function (e) {
        //    var currobj = $(this);
        //    var tbheadsobjs = $("#IndexPPre_Organization #tbhead").data();
        //    var url = "Pre_Organization/Start";

        //    Framework.JsFunHandle.Delete(currobj, tbheadsobjs, trPre_OrganizationsDataFieldMap, url, true, "启用");
        //});

        //////////////////单行停用//////////////////////////
        //$(document).delegate('#IndexPPre_Organization #tbbody .btn-Stop', 'click', function (e) {
        //    var currobj = $(this);
        //    var tbheadsobjs = $("#IndexPPre_Organization #tbhead").data();
        //    var url = "Pre_Organization/Stop";

        //    Framework.JsFunHandle.Delete(currobj, tbheadsobjs, trPre_OrganizationsDataFieldMap, url, true,"停用");
        //});

        ////////////////////批量启用/////////////////////////
        //$(document).delegate('#IndexPPre_Organization #toolbarbtn .btn-Start', 'click', function () {
        //    var currobj = $(this);
        //    var tbheadsobjs = $("#IndexPPre_Organization #tbhead").data();
        //    var checkeds = $("#IndexPPre_Organization #tbbody .jq-checkall-item:checked");
        //    Framework.JsFunHandle.BatchDelete(tbheadsobjs, checkeds, trPre_OrganizationsDataFieldMap, 'Pre_Organization/Start', true,"启用");
        //});

        ////////////////////批量停用/////////////////////////
        //$(document).delegate('#IndexPPre_Organization #toolbarbtn .btn-Stop', 'click', function () {
        //    var currobj = $(this);
        //    var tbheadsobjs = $("#IndexPPre_Organization #tbhead").data();
        //    var checkeds = $("#IndexPPre_Organization #tbbody .jq-checkall-item:checked");
        //    Framework.JsFunHandle.BatchDelete(tbheadsobjs, checkeds, trPre_OrganizationsDataFieldMap, 'Pre_Organization/Stop', true,"停用");
        //});

        if ($('#OrganizationsTree').length > 0) {
            //alert();
            $('#OrganizationsTree').myTree({
                /*子节点样式*/
                subClass: 'sub'
            })

            //$("#OrganizationsTree").treeview({
            //    collapsed: true,
            //    animated: "fast",
            //    control: "#sidetreecontrol",
            //    prerendered: true,
            //    persist: "location"
            //});
        }
    };



    return Pre_Organizations;
}(Pre_Organizations));


$(document).ready(
	function () {
	    Pre_Organizations.Init();
	}
);

