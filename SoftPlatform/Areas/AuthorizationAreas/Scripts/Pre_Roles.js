/// <reference path='jquery-1.9.1.intellisense.js' />

Import('Pre_Roless');

(function (Pre_Roless) {

    Pre_Roless.Init = function () {
        var items = $("#PermAssignTable input:checkbox");
        var hsPermAssigns = new Hashtable();
        var pPermAssignsArr = new Array();
        items.each(function () {
            var val = $(this).val();
            var parentid = $(this).data("parentid");
            hsPermAssigns.add(val + "", { "val": val + "", "parentid": parentid + "", "ckbox": $(this) });
            //查找节点：知道某节点，获取所有父节点，
            //查找父节点，主键：主键
            pPermAssignsArr.push({ "val": val + "", "parentid": parentid + "", "ckbox": $(this) });
        });

        //$('#IndexPPre_Roles #toolbarbtn .btn-Add').click(function (e) {
        //    Framework.Ajax.GetView({}, 'Pre_Roles/Edit', function (result) {
        //        var params = {
        //            title: '添加角色信息',
        //            width: 600,
        //            message: result,
        //            onConfirm: function (popup, event) {
        //                //popup.success("添加成功");
        //                //alert();
        //                //popup.fail("添加失败");
        //                //return;
        //                if (Framework.Form.Validates(popup)) {
        //                    var data = Framework.Form.GetFormItemByObj(popup);
        //                    Framework.Ajax.PostJson(data, 'Pre_Roles/Edit', function (results) {
        //                        //错误处理
        //                        if (results.Data.RespAttachInfo.bError) {
        //                            //显示错误
        //                            var strerrors = Framework.UI.Behavior.ErrorHandling(results);
        //                            if (strerrors.length > 0) {
        //                                popup.fail(strerrors);
        //                            }
        //                            return;
        //                        }
        //                        Framework.Form.ClearForm(popup);
        //                        var data = results.Data;
        //                        //在列表中添加行
        //                        var temp = Framework.Tool.TmpBind(tmp, tmpMap, results.Data.Item);
        //                        temp = $("<table>" + temp + "</table>");
        //                        var tbbody = $("#IndexPPre_Organization  #tbbody");
        //                        tbbody.append(temp.find("tr"));

        //                        popup.success("添加成功");
        //                    });
        //                }
        //            },
        //            onCancel: function (popup) {
        //            }
        //        };
        //        Framework.UI.FormModalMy(params);
        //    }
        //    );
        //});

        ///////////////////权限：树--Pre_HModularTree/////////////////////////////////////
        //$('#Pre_HModularTree input:checkbox').click(function () {
        //    treeSelect($(this));
        //});

        //var treeSelect = function (obj) {
        //    $("#Pre_HModularTree input:checkbox").unbind("click");

        //    if (obj.prop("checked") == true) {
        //        obj.closest('li').find("input:checkbox").each(function () {
        //            if ($(this).prop("checked") == false) {
        //                $(this).click();
        //            }
        //        });
        //        obj.parents("li").find(">div input:checkbox").each(function () {
        //            if ($(this).prop("checked") == false) {
        //                $(this).click();
        //            }
        //        });
        //    } else {
        //        obj.closest('li').find("input:checkbox").each(function () {
        //            if ($(this).prop("checked") == true) {
        //                $(this).click();
        //            }
        //        })
        //    }
        //    $('#Pre_HModularTree input:checkbox').bind("click", function () { treeSelect($(this)); });
        //};
        ///////////////////////////角色编辑--保存/////////////////////////
        //$(document).delegate('#editPPre_RolesHModular .btn-Save', 'click', function (e) {
        //    var btn = $(this);

        //    var obj = $("#editPPre_Roles");
        //    var data = {};
        //    if (Framework.Form.Validates(obj)) {
        //        var data = Framework.Form.GetFormItemByObj(obj);
        //        //权限数据
        //        $("#PermAssignTable input:checkbox:checked").each(function (i) {
        //            var tr = $(this).closest('tr');
        //            var fieldname = "Item.Sys_PremSets[" + i + "].Sys_PermSetID";
        //            data[fieldname] = $(this).val();
        //        });
                 
        //        //权限数据
        //        var k = 0;
        //        $("#PermAssignTable input:checkbox:checked").each(function (i) {
        //            var tr = $(this).closest('tr');
        //            //Sys_PremCodeID
        //            var DataRight = tr.find("#DataRight");
        //            if (DataRight.length > 0) {
        //                var DataRightVal = DataRight.val();
        //                var Sys_PremCodeID = DataRight.data("premcodeid");
        //                //alert(DataRightVal);
        //                //alert(Sys_PremCodeID);
        //                var fieldname1 = "Item.Pre_RolesPremCodes[" + k + "].DataRight";
        //                data[fieldname1] = DataRightVal;
        //                var fieldnamePremCodeID = "Item.Pre_RolesPremCodes[" + k + "].Sys_PremCodeID";
        //                data[fieldnamePremCodeID] = Sys_PremCodeID;
        //                k++;
        //            }
        //        });
        //        //进行保存
        //        var url = "Pre_Roles/Edit";
        //        if (data["Item.Pre_RolesID"] == "")
        //        {
        //            url = "Pre_Roles/Add";
        //        }

        //        btn.addClass("disabled");
        //        Framework.Ajax.PostJson(data, url, function (results) {
        //            btn.removeClass("disabled");

        //            if (results.Data.RespAttachInfo.bError) {
        //                //显示错误
        //                var strerrors = Framework.UI.Behavior.ErrorHandling(results);
        //                if (strerrors.length > 0)
        //                    alert(strerrors);
        //                return;
        //            }
        //            var data = results.Data;
        //            alert("保存成功");
        //            window.location.href = Framework.Page.BaseURL + "AuthorizationAreas/Pre_Roles/Index";
        //        })
        //    }
        //});

        /////////////////////////权限表格///////////////////////////////
        $("#PermAssignTable input:checkbox").click(function () {
            permAssignSelect($(this));
        });

        var permAssignSelect = function (obj) {
            $("#PermAssignTable input:checkbox").unbind("click");

            if (obj.prop("checked") == true) {
                //选中所有父节点
                permAssignSeledParent(obj.val() + "");

                //选择所有子节点
                permAssignSeledChilds(obj.val() + "");
            } else {
                permAssignChildChilds(obj.val() + "");
            }
            $('#PermAssignTable input:checkbox').bind("click", function () { permAssignSelect($(this)); });
        };

        //选中父节点
        var permAssignSeledParent = function (key) {
            if (hsPermAssigns.contain(key)) {
                var obj = hsPermAssigns.get(key);

                if (obj["ckbox"].prop("checked") == false) {
                    obj["ckbox"].click();
                }
                permAssignSeledParent(obj["parentid"] + "");
            }
        };

        //选中所有子节点
        var permAssignSeledChilds = function (key) {
            for (var i = 0; i < pPermAssignsArr.length; i++) {
                var obj = pPermAssignsArr[i];
                if (obj["parentid"] + "" == key) {
                    if (obj["ckbox"].prop("checked") == false) {
                        obj["ckbox"].click();
                    }
                    permAssignSeledChilds(obj["val"] + "");
                }
            }
        };

        //选中所有子节点
        var permAssignChildChilds = function (key) {
            for (var i = 0; i < pPermAssignsArr.length; i++) {
                var obj = pPermAssignsArr[i];
                if (obj["parentid"] + "" == key) {
                    if (obj["ckbox"].prop("checked") == true) {
                        obj["ckbox"].click();
                    }
                    permAssignChildChilds(obj["val"] + "");
                }
            }
        };

        /////////////////////////////////////////////////////////////
        $("#PermAssignTable .btn-Expcoll").click(function () {
            //alert("dd");
            var obj = $(this);
            var val = obj.closest('tr').find("#Sys_PermSetID").val()+"";
        
            if (obj.hasClass("tabletreeminus")) {//折叠
                obj.removeClass("tabletreeminus");
                $(obj).addClass("tabletreeplus");
                permAssignHideChilds(val + "");
            }
            else {//展开
                obj.removeClass("tabletreeplus");
                $(obj).addClass("tabletreeminus");
                //查找本行上的checkbox,获取data-id，再找子无素进行隐藏
                for (var i = 0; i < pPermAssignsArr.length; i++) {
                    var objchild = pPermAssignsArr[i];
                    if (objchild["parentid"] + "" == val) {
                        objchild["ckbox"].closest('tr').removeClass("hide");
                        //objchild["ckbox"].closest('tr').addClass("show");
                    }
                }
            }
        });

        var permAssignHideChilds = function (key) {
            for (var i = 0; i < pPermAssignsArr.length; i++) {
                var obj = pPermAssignsArr[i];
                if (obj["parentid"] + "" == key) {
                    var tr = obj["ckbox"].closest('tr');
                    if (!tr.hasClass("hide")) {
                        tr.addClass("hide");
                        var temp=tr.find(".tabletreeminus");
                        temp.removeClass("tabletreeminus");
                        temp.addClass("tabletreeplus");
                    }
                    permAssignHideChilds(obj["val"] + "");
                }
            }
        };
    };

    return Pre_Roless;
}(Pre_Roless));


$(document).ready(
	function () {
	    Pre_Roless.Init();
	}
);

