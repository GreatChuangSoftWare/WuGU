/// <reference path='jquery-1.9.1.intellisense.js' />

Import('Pre_RoleModulars');

(function (Pre_RoleModulars) {

    Pre_RoleModulars.Init = function () {

        var trPre_RoleModularsDataFieldMap = new Hashtable();
        trPre_RoleModularsDataFieldMap.add('pre_rolemodularid', 'Pre_RoleModularID');
        trPre_RoleModularsDataFieldMap.add('pre_rolesid', 'Pre_RolesID');
        trPre_RoleModularsDataFieldMap.add('modularid', 'ModularID');


        //////////////////快速：查询事件/////////////////////
        $('#mainPage #btn-Search').click(function () {
            $('#searchType').val('0');
            Pre_RoleModulars.Search({});
        });

        ////////////////高级查询/////////////////////////////
        $('#mainPage #btn-advSearch').click(function () {
            $('#searchType').val('1');
            Pre_RoleModulars.Search({});
        });

        ////////////////查询：pageIndex：当前页//////////////
        Pre_RoleModulars.Search = function (params) {
            var data = {};
            //(1)找到查询条件：快速查询、高级查询
            var searchType = $('#searchType').val() || 0;
            if (searchType == 0)
                data = Framework.Form.GetFormItemByObj($('#fastSearchArea'));
            else
                data = Framework.Form.GetFormItemByObj($('#advSearchArea'));

            data['searchType'] = searchType;
            var url = Framework.Page.BulidUrl($('#pageArea'), data, params, 'Notice/Index');
            document.location.href = url;
        };
        /////////////JS代码//////////////////////
        $(document).delegate('#mainPage #tbbody .btn-Detail', 'click', function (e) {
            var tr = $(this).closest('tr');
            var data = Framework.Tool.GetTbRowData($(this), trPre_RoleModularDataFieldMap);
            Framework.Ajax.GetView(data, 'Pre_RoleModular/Detail', function (result) {
                var params = {
                    title: 'XXX详情',
                    message: result,
                    onConfirm: function (popup, event) {
                    },
                    onCancel: function (popup) {
                    }
                };

                Framework.UI.FormDetailModal(params);
            }
            );
        });

        $('#mainPage #toolbarbtn .btn-Add').click(function (e) {
            Framework.Ajax.GetView({}, 'Pre_RoleModular/Edit', function (result) {
                var params = {
                    title: '添加XX信息',
                    message: result,
                    onConfirm: function (popup, event) {
                        if (Framework.Form.Validates(popup)) {
                            var data = Framework.Form.GetFormItemByObj(popup);
                            Framework.Ajax.PostJson(data, 'Pre_RoleModular/Edit', function (results) {
                                //错误处理
                                if (results.Data.RespAttachInfo.bError) {
                                    //显示错误
                                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                                    if (strerrors.length > 0)
                                        popup.find('#messageArea').html(strerrors);
                                    return;
                                }
                                Framework.Form.ClearForm(popup);
                                var data = results.Data;
                                //在列表中添加行
                                Framework.Ajax.GetView({ 'Item.Pre_RoleModularID': data.Item.NoticeID }, 'Pre_RoleModular/TableRow', function (results) {
                                    //错误处理
                                    if (results.Data.RespAttachInfo.bError) {
                                        //显示错误
                                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                                        if (strerrors.length > 0)
                                            popup.find('#messageArea').html(strerrors);
                                        return;
                                    }
                                    $('#tbbody').prepend($(results));
                                });
                                popup.find('#messageArea').html('添加成功');
                            });
                        }
                    },
                    onCancel: function (popup) {
                    }
                };
                Framework.UI.FormModal(params);
            }
            );
        });

        $(document).delegate('#mainPage #tbbody  .btn-Edit', 'click', function (e) {
            var tr = $(this).closest('tr');
            var data = Framework.Tool.GetTbRowData($(this), trPre_RoleModularDataFieldMap);
            Framework.Ajax.GetView(data, 'Pre_RoleModular/Edit', function (result) {
                var params = {
                    title: '编辑XX信息',
                    message: result,
                    onConfirm: function (popup, event) {
                        if (Framework.Form.Validates(popup)) {
                            var data = Framework.Form.GetFormItemByObj(popup);
                            //进行保存
                            Framework.Ajax.PostJson(data, 'Pre_RoleModular/Edit', function (results) {
                                if (results.Data.RespAttachInfo.bError) {
                                    //显示错误
                                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                                    if (strerrors.length > 0)
                                        popup.find("#messageArea").html(strerrors);
                                    return;
                                }
                                var data = results.Data;
                                //更新列表行
                                Framework.Ajax.GetView({ 'Item.Pre_RoleModularID': data.Item.Pre_RoleModularID }, 'Pre_RoleModular/TableRow', function (results) {
                                    if (results.Data.RespAttachInfo.bError) {
                                        //显示错误
                                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                                        if (strerrors.length > 0)
                                            popup.find('#messageArea').html(strerrors);
                                        return;
                                    }
                                    var trtemp = $(results);
                                    tr.replaceWith(trtemp);
                                    tr = trtemp;
                                    popup.hide();
                                    //弹窗提示成功，关闭窗口
                                });
                            })
                        }
                    },
                    onCancel: function (popup) {
                    }
                };
                Framework.UI.FormModal(params);
            }
            );
        });

        $('#mainPage #toolbarbtn .btn-Delete').click(function () {
            var tbheadsobjs = $('#mainPage #toolbarbtn #tbhead').data();

            /////////////////////////////////////////////////////////////
            var strMess = '<table class="table table-condensed table-bordered" >';
            strMess += '<thead>';
            strMess += '<tr>';
            for (att in tbheadsobjs) {
                strMess += '<td>' + tbheadsobjs[att] + '</td>'
            }
            strMess += '</tr>';
            strMess += '</thead>';
            strMess += '<tbody>';
            ////////////////////////////////////////////////////////////////
            $('#mainPage #tbbody .jq-checkall-item:checked').each(function () {
                strMess += '<tr>';
                var tr = $(this).closest('tr');
                var rowItem = tr.data();
                for (att in tbheadsobjs) {
                    strMess += '<td>' + rowItem[att] + '</td>'
                }
                strMess += '<tr>';
            });
            strMess += '</tbody>';
            strMess += '</table>';

            var params = {
                title: '【XX确认】',
                message: strMess,
                onConfirm: function (popup) {
                    var opterSussecRows = [];
                    var opterErrorRows = [];
                    //////////执行操作/////////////////
                    $('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
                        var tr = $(this).closest('tr');
                        var rowItem = tr.data();
                        var obj = Framework.Tool.GetTbRowData($(this), trPre_RoleModularsDataFieldMap);

                        Framework.Ajax.PostJsonAsync(obj, 'Pre_RoleModular/Delete',
                            function (result) {
                                var Data = result.Data;
                                if (!Data.RespAttachInfo.bError) {
                                    opterErrorRows.push({ trobj: tr, errorMessag: '' });
                                }
                                else
                                    opterSussecRows.push(tr);
                            },
                            function (jqXHR) {
                                Framework.UI.HandleError(jqXHR, '');
                            }
                        );
                    });
                    popup.modal('hide');
                    //进行提示
                    //显示消息
                    var strMess = '以下记录操作成功：';
                    strMess += '<table class="table table-condensed table-bordered" >';
                    strMess += '<thead>';
                    strMess += '<tr>';
                    for (att in tbheadsobjs) {
                        strMess += '<td>' + tbheadsobjs[att] + '</td>'
                    }
                    strMess += '</tr>';
                    strMess += '</thead>';
                    for (var i = 0; i < opterSussecRows.length; i++) {
                        strMess += '<tr>';
                        var rowItem = opterSussecRows[i].data();
                        for (att in tbheadsobjs) {
                            strMess += '<td>' + rowItem[att] + '</td>'
                        }
                        strMess += '<tr>';
                    }
                    strMess += '</table>'
                    strMess += '以下记录操作失败：'
                    //strMess += '<table><tr>';
                    strMess += '<table class="table table-condensed table-bordered" >';
                    strMess += '<thead>';
                    strMess += '<tr>';
                    for (att in tbheadsobjs) {
                        strMess += '<td>' + tbheadsobjs[att] + '</td>'
                    }
                    strMess += '</tr>';
                    strMess += '</thead>';
                    for (var i = 0; i < opterErrorRows.length; i++) {
                        strMess += '<tr>';
                        var rowItem = opterErrorRows[i].data();
                        for (att in tbheadsobjs) {
                            strMess += '<td>' + rowItem[att] + '</td>'
                        }
                        strMess += '<td>' + obj.errorMessag + '</td>';
                        strMess += '<tr>';
                    }
                    strMess += '</table>';

                    var paramserror = {
                        title: '操作提示',
                        message: strMess,//BulkDisabledErrorArray,
                        onConfirm: function () {
                            window.location.reload(true);
                        },
                        onCancel: function (popup) {
                        }
                    };

                    Framework.UI.ModalTablePrompt(paramserror);
                },
                onCancel: function (popup) {
                }
            };
            Framework.UI.ModalBatch(params);
        });
        $(document).delegate('#mainPage #tbbody .btn-Delete', 'click', function (e) {
            var entityobj = Framework.Tool.GetTbRowData($(this), trPre_RoleModularsDataFieldMap);
            var tr = $(this).closest('tr');
            var rowItem = tr.data();
            var objs = $('#tbhead').data();

            var strMess = '';
            for (att in objs) {

                strMess += '<div class="form-group">';
                strMess += '<label class="col-md-3 show-title"><label>' + objs[att] + '</label></label>';
                strMess += '<div class="col-md-8">';
                strMess += rowItem[att];
                strMess += '</div>';
                strMess += '</div>';
            }
            strMess += ''

            var params = {
                title: '【XX确认】',
                message: strMess,
                onConfirm: function (popup) {
                    Framework.Ajax.PostJson(entityobj, 'Pre_RoleModular/Delete', function () {
                        if (results.Data.RespAttachInfo.bError) {
                            //显示错误
                            var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                            if (strerrors.length > 0)
                                popup.find('#messageArea').html(strerrors);
                            return;
                        }
                        window.location.reload(true);
                    })
                },
                onCancel: function (popup) {
                }
            };
            Framework.UI.Modal(params);
        });


    };

    return Pre_RoleModulars;
}(Pre_RoleModulars));


$(document).ready(
	function () {
	    Pre_RoleModulars.Init();
	}
);

