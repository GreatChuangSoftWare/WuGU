/// <reference path='jquery-1.9.1.intellisense.js' />

Import('MyJss');

(function (MyJss) {

    MyJss.Init = function () {

        //下拉列表框改变事件
        $(document).delegate('#Item_ComplaintCategoryID', 'change', function (e) {
            //alert();
            var obj = $(this);
            var targDom = $("#Item_FraDispComplaintPerson");
            var soureDom = $("#Item_ComplaintPerson");
            if (obj.val() == "1") {
                targDom.val(soureDom.val());
            }
            else {
                targDom.val("匿名");
            }
        });

        //采购订单，单价改变时，计算总价
        $(document).delegate("#Item_PurchaseNumber", 'blur', function () {
            var val = $(this).val();
            val = Number(val);
            //price = Number(price);
            if (!Framework.Tool.isNumber(val)) {
                alert("请输入数字");
                return;
            }
            var ProductPrice = $(".ProductPrice").text();
            ProductPrice = Number(ProductPrice);
            if (!Framework.Tool.isNumber(ProductPrice)) {
                alert("单价值无效");
                return;
            }
            var TotalPrice = val * ProductPrice;
            $("#Item_PurchaseTotalPrice").val(TotalPrice);
        });

        if ($(".autocompleteaddrow").length > 0) {
            $(".autocompleteaddrow").each(function (i) {
                var dom = $(this);
                var autourl = dom.data("autourl");
                var rowurl = dom.data("rowurl");
                var pkname ="Item."+ dom.data("pkname");

                var autocompleteFields = dom.data("autocompletefields");
                var autocompleteFieldsArrs = autocompleteFields.split(',');
                var data = {};
                for (var i = 0; i < autocompleteFieldsArrs.length; i++)
                {
                    var FieldName = "Item." + autocompleteFieldsArrs[i];
                    var FieldNameDomID = "#Item_" + autocompleteFieldsArrs[i];
                    data[FieldName] = $(FieldNameDomID).val();
                }
                dom.autocomplete({
                    source:
                        function (request, response) {
                            Framework.Ajax.PostJson(data, autourl,
                                    function (items) {
                                        if (items) {
                                            if (items.length < 1) {
                                                response(["不存在匹配数据，请重新输入"]);
                                                return;
                                            }
                                            response(items);
                                        } else {
                                            response([]);
                                        }
                                    });
                        },
                    minLength: 2,
                    select: function (event, ui) {
                        if (ui.item.label != "不存在匹配数据，请重新输入") {
                            $(this).val(ui.item.label);
                            var data = {};
                            data[pkname] = ui.item.value;
                            Framework.Ajax.GetView(data,rowurl,
                                    function (results) {
                                        $("#tbList tbody").append(results);
                                        dom.val("");
                                    });
                        } else {
                            $(this).val("");
                        }
                        return false;
                    },
                });
            })
        }

        //加盟商订单
        if ($("#xxxFra_ProductNo__ProductName__Specifications").length > 0) {
            $("#Fra_ProductNo__ProductName__Specifications")
                .autocomplete({
                    source:
                        function (request, response) {
                            Framework.Ajax.PostJson(
                                {
                                    "Item.ProductNo__ProductName__Specifications": $("#Fra_ProductNo__ProductName__Specifications").val(),
                                },
                                "/ProductAreas/P_Product/AutoCompleteProduct",
                                    function (items) {
                                        if (items) {
                                            if (items.length < 1) {
                                                response(["不存在匹配数据，请重新输入"]);
                                                return;
                                            }
                                            response(items);
                                        } else {
                                            response([]);
                                        }
                                    });
                        },
                    minLength: 2,
                    select: function (event, ui) {
                        if (ui.item.label != "不存在匹配数据，请重新输入") {
                            $(this).val(ui.item.label);
                            Framework.Ajax.GetView(
                                {
                                    "Item.P_ProductID": ui.item.value
                                },
                                "/ProductAreas/O_OrderDetail/Rows",
                                    function (results) {
                                        $("#tbList tbody").append(results);
                                    });
                        } else {
                            $(this).val("");
                        }
                        return false;
                    },
                });
        }




        //(1)合作商ID
        //(2)条件：商品编号、商品名称、型号查询
        if ($("#BC_ProductNo__ProductName__Specifications").length > 0) {
            //alert("ProductNo__ProductName__Specifications");
            $("#BC_ProductNo__ProductName__Specifications")//.bind("keydown", keyDown)
                .autocomplete({
                    source:
                        function (request, response) {
                            Framework.Ajax.PostJson(
                                {
                                    "Item.ProductNo__ProductName__Specifications": $("#BC_ProductNo__ProductName__Specifications").val(),
                                    "Item.Pre_UserID": $("#Item_Pre_UserID").val()
                                },
                                "/PartnerAreas/BC_PartnerProductPrice/AutoCompletePartnerProduct",
                                    function (items) {
                                        if (items) {
                                            if (items.length < 1) {
                                                response(["不存在匹配数据，请重新输入"]);
                                                return;
                                            }
                                            response(items);
                                        } else {
                                            response([]);
                                        }
                                    });
                        },
                    minLength: 2,
                    select: function (event, ui) {
                        if (ui.item.label != "不存在匹配数据，请重新输入") {
                            //alert(ui.item.label);
                            //alert(ui.item.value);

                            $(this).val(ui.item.label);
                            //获取行
                            //$("#editPC_Visit #Item_NegotiatorsID").val(ui.item.value);
                            Framework.Ajax.GetView(
                                {
                                    "Item.BC_PartnerProductPriceID": ui.item.value
                                },
                                //"/CustomerAreas/C_OrderDetail/RowsCompCP",
                                "/PartnerAreas/BC_OrderDetail/Rows",
                                    function (results) {
                                        $("#tbList tbody").append(results);
                                    });
                        } else {
                            $(this).val("");
                        }
                        return false;
                    },
                });
        }
    };

    return MyJss;
}(MyJss));


$(document).ready(
	function () {
	    MyJss.Init();
	}
);

