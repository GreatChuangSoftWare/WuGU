/// <reference path="jquery-1.9.1.intellisense.js" />
/// <reference path="jquery.validate-vsdoc.js" />

var Framework = Framework || {},
isFormModal = false;

Import = function () {
    var a = arguments, o, i = 0, j, d, arg;
    for (; i < a.length; i++) {
        o = window;
        arg = a[i];
        if (arg.indexOf(".") > -1) {
            d = arg.split(".");
            for (j = 0; j < d.length; j++) {
                o[d[j]] = o[d[j]] || {};
                o = o[d[j]];
            }
        } else {
            o[arg] = o[arg] || {};
            o = o[arg];
        }
    }
    return o;
};

Import("Framework.UI");

String.prototype.padLeft = function (width, padChar) {
    if (!padChar) padChar = ' ';
    var str = new String(this);
    while (str.length < width) {
        str = padChar + str;
    }
    return str;
};

Number.prototype.padLeft = function (width, padChar) {
    return this.toString().padLeft(width, padChar);
};

String.prototype.toMvcDate = function () {
    return (this !== "") ? new Date(parseInt(this.replace(/\/Date\(([0-9-]+)\)\//, "$1"))) : null;
};


String.prototype.isNullOrEmpty = function () {
    return this == null || this.length <= 0;
};
String.prototype.format = function () {
    var formattedString = this.toString();
    for (var i = 0; i < arguments.length; i++) {
        formattedString = formattedString.split('{' + i + '}').join(arguments[i]);
    }
    return formattedString;
};
String.prototype.trimBegin = function () {
    var str = this;
    var i;
    for (i = 0; i < str.length; i++) {
        if (str.charAt(i) != " ") break;
    }
    return str.substring(i, str.length);
};
String.prototype.trimEnd = function () {
    var str = this;
    var i;
    for (i = str.length - 1; i >= 0; i--) {
        if (str.charAt(i) != " ") break;
    }
    return str.substring(0, i + 1);
};
String.prototype.trim = function () {
    return this.trimBegin().trimEnd();
};
String.prototype.startWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;

    return this.substr(0, str.length) == str;
};
String.prototype.endWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;

    return this.substring(this.length - str.length) == str;
};
String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
};
String.prototype.elipsizeText = function (width) {
    if (this == null || this == "" || this.length == 0)
        return "";
    if (this.length <= width) {
        return this;
    }
    else {
        return this.substr(0, width) + '...';
    }
};
String.prototype.maskString = function (len) {
    if (this == null || this == "" || this.length == 0)
        return "";
    var maskedStr = "";
    for (var i = 0; i < len; i++) {
        maskedStr += "●";
    }
    if (this.length > 4) {
        maskedStr += this.substr(this.length - 4, 4);
    }
    else {
        maskedStr += this;
    }
    return maskedStr.substr(maskedStr.length - len, len);
};


String.prototype.fmMoney = function (n) {
    var s = this;
    n = n > 0 && n <= 20 ? n : 2;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
    var l = s.split(".")[0].split("").reverse(), r = s.split(".")[1];
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("") + "." + r;
}

Array.prototype.contains = function (element) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == element) {
            return true;
        }
    }
    return false;
};
Array.prototype.distinct = function () {
    return _.uniq(this);
};
//// note:this method not compare time part
Date.prototype.compareToWithOutTime = function (date) {
    var year = this.getFullYear() - date.getFullYear();
    var month = this.getMonth() - date.getMonth();
    var day = this.getDate() - date.getDate();

    if (year == 0 && month == 0 && day == 0)
        return 0;

    if (year > 0 || (year == 0 && month > 0) ||
         (year == 0 && month == 0 && day > 0)) {
        return 1;
    }

    return -1;
};
Date.getUTCTimeStamp = function (date) {
    var yy = date.getYear();
    var MM = date.getMonth();
    var dd = date.getDay();
    var hh = date.getHours();
    var mm = date.getMinutes();
    var ss = date.getSeconds();
    var sss = date.getMilliseconds();
    return Date.UTC(yy, MM, dd, hh, mm, ss, sss);
};
Date.getUTCDate = function (date) {
    var yy = date.getUTCFullYear();
    var MM = date.getUTCMonth();
    var dd = date.getUTCDate();
    var hh = date.getUTCHours();
    var mm = date.getUTCMinutes();
    var ss = date.getUTCSeconds();
    var sss = date.getUTCMilliseconds();
    return new Date(yy, MM, dd, hh, mm, ss, sss);
};
Date.prototype.addDay = function (n) {
    var millis = this.getTime();
    return new Date(millis + (n * 24 * 60 * 60 * 1000));
};

Date.prototype.addMonths = function (n) {
    var date = this;
    date.setMonth(date.getMonth() + n);
    return date;
};

//function AddMonths(date, value) {
//    date.setMonth(date.getMonth() + value);
//    return date;
//}
////增加天 
//function AddDays(date, value) {
//    date.setDate(date.getDate() + value);
//    return date;
//}
////增加时
//function AddHours(date, value) {
//    date.setHours(date.getHours() + value);
//    return date;
//}
/*-------------------------- hash table begin-------------------------*/

function Hashtable() {
    this._hashValue = new Object();
    this._iCount = 0;
}

Hashtable.prototype.add = function (strKey, value) {
    if (typeof (strKey) == "string") {
        this._hashValue[strKey] = typeof (value) != "undefined" ? value : null;
        this._iCount++;
        return true;
    }
    else
        throw "hash key not allow null!";
}
Hashtable.prototype.get = function (key) {
    if (typeof (key) == "string" && this._hashValue[key] != typeof ('undefined')) {
        return this._hashValue[key];
    }
    if (typeof (key) == "number")
        return this._getCellByIndex(key);
    else
        throw "hash value not allow null!";

    return null;
}
Hashtable.prototype.contain = function (key) {
    return this.get(key) != null;
}
Hashtable.prototype.findKey = function (iIndex) {
    if (typeof (iIndex) == "number")
        return this._getCellByIndex(iIndex, false);
    else
        throw "find key parameter must be a number!";
}
Hashtable.prototype.count = function () {
    return this._iCount;
}
Hashtable.prototype._getCellByIndex = function (iIndex, bIsGetValue) {
    var i = 0;
    if (bIsGetValue == null) bIsGetValue = true;
    for (var key in this._hashValue) {
        if (i == iIndex) {
            return bIsGetValue ? this._hashValue[key] : key;
        }
        i++;
    }
    return null;
}
Hashtable.prototype.remove = function (key) {
    for (var strKey in this._hashValue) {
        if (key == strKey) {
            delete this._hashValue[key];
            this._iCount--;
        }
    }
}
Hashtable.prototype.clear = function () {
    for (var key in this._hashValue) {
        delete this._hashValue[key];
    }
    this._iCount = 0;
}
/*-------------------------- hash table end-------------------------*/

Framework.DomHelper = {
    findSibling: function (/*jquery elem*/startingElem, /*string*/selector) {
        var sibling;
        startingElem = $(startingElem);
        if (startingElem.length <= 0) {
            return startingElem;
        }
        if (startingElem.parent().find(selector).length > 0) {
            sibling = startingElem.parent().find(selector);
        } else {
            if (!startingElem.is('body')) {
                sibling = Framework.DomHelper.findSibling(startingElem.parent(), selector);
            } else {
                return $('body ' + selector);
            }
        }
        return sibling;
    },
    findSiblingParent: function (/*jquery elem*/startingElem, /*string*/selector) {
        var parent;
        startingElem = $(startingElem);
        if (startingElem.parent().find(selector).length > 0) {
            parent = startingElem.parent();
        } else {
            parent = Framework.DomHelper.findSiblingParent(startingElem.parent(), selector);
        }
        return parent;
    },
    findParent: function (/*jquery elem*/startingElem, /*string*/selector) {
        var parent;
        startingElem = $(startingElem);
        if (startingElem.parent().is(selector)) {
            parent = startingElem.parent();
        } else {
            parent = Framework.DomHelper.findParent(startingElem.parent(), selector);
        }
        return parent;
    }
};

Import("Framework.UI.Behavior");
//左侧菜单展开与关闭
Framework.UI.Behavior.MenuSet = function () {
    $("#isclose").click(function () {
        $(".tree").hide();
        //$(".body_left").removeClass("span2");
        $(".open_menu").show();
        //$(".body_right").removeClass("span10").addClass("span11");
        $(".body_left").css("width", 30);
    })
    $(".open_menu").click(function () {
        $(this).hide();
        $(".tree").show();
        if ($(window).width() > 1024) {
            $(".body_left").css("width", 215);
        }
        //$(".body_left").addClass("span2");
    })
}
Framework.UI.Behavior.Collapse = function () {
    $(".show_root").removeClass("change");
    $(".show_root").click(function () {
        if ($(this).removeClass("change").hasClass("change_root")) {
            $(this).removeClass("change_root");
        } else {
            $(this).removeClass("change").addClass("change_root");
        }
    })

    $(document).delegate(".jq-collapse-title", 'click.collapse', function () {
        var parent = $(this).parent();
        var content = parent.children(".jq-collapse-content");
        if (!parent.hasClass("empty")) {
            $(this).find(".show_icon").toggleClass("change");
            if ($(this).parent().hasClass("first")) {
                $(this).toggleClass("active");
            }
            if (content.length <= 0) {
                content = parent.parent().find(".jq-collapse-content");
            }
            content.toggle();
        }
        return false;
    });
    $(".jq-collapse-title a").click(function (event) {
        event.stopPropagation();
    })
};

Framework.UI.Behavior.Pagination = function () {
    $(document).delegate(".pagination-goto-button", 'click.pagination', function () {
        var value = Framework.DomHelper.findSibling($(this), ".pagination-goto-url").val();
        var page = Framework.DomHelper.findParent($(this), ".pagination-goto").data("url");
        var max = Framework.DomHelper.findSibling($(this), ".pagination-goto-url").data("max");

        //page = "abc?def=10&PageQueryBase.PageIndex=4&kkk=55";

        if (isNaN(parseInt(value, 10))) {
            value = 1;
        }

        if (value > parseInt(max)) {
            value = max;
        }

        if (value < 1) {
            value = 1;
        }
        if (page.indexOf("?") == -1) {
            page = page + "?PageQueryBase.PageIndex=" + value;
        }
        else {
            var pos = page.indexOf("PageQueryBase.PageIndex=");
            if (pos >= 0) {
                var temppage = page.substr(0, pos);//取出前部分
                var temppageafter = page.substr(pos + "PageQueryBase.PageIndex=".length);
                var pos1 = temppageafter.indexOf("&");
                if (pos1 > 0)
                    temppageafter = temppageafter.substr(pos1);
                else
                    temppageafter = "";
                page = temppage + "PageQueryBase.PageIndex=" + value + temppageafter;
            }
            else {
                page = page + "&PageQueryBase.PageIndex=" + value;
            }

        }

        document.location.href = page;
        return false;
    });

    $(document).delegate('.pagination-goto-url', 'keypress.pagination.enter', function (event) {
        if (event.keyCode == "13") {
            Framework.DomHelper.findSibling($(this), ".pagination-goto-button").click();
            event.preventDefault();
            event.stopPropagation();
            return false;
        }
    });

    $(document).delegate(".pagination-pagesize-select", 'change.pagination', function () {

        var value = $(this).val();
        var page = Framework.DomHelper.findParent($(this), ".pagination-pagesize").data("url");

        if (isNaN(parseInt(value, 10))) {
            value = 1;
        }

        if (page.indexOf("?") == -1) {
            page = page + "?PageQueryBase.PageSize=" + value;
        }
        else {
            //page = page + "&PageQueryBase.PageSize=" + value;
            var pos = page.indexOf("PageQueryBase.PageSize=");
            if (pos >= 0) {
                var temppage = page.substr(0, pos);//取出前部分
                var temppageafter = page.substr(pos + "PageQueryBase.PageSize=".length);
                var pos1 = temppageafter.indexOf("&");
                if (pos1 > 0)
                    temppageafter = temppageafter.substr(pos1);
                else
                    temppageafter = "";
                page = temppage + "PageQueryBase.PageSize=" + value + temppageafter;
            }
            else {
                page = page + "&PageQueryBase.PageSize=" + value;
            }
        }

        document.location.href = page;
        return false;
    });
};

//全选与反选
Framework.UI.Behavior.CheckAll = function () {
    $(document).delegate(".jq-checkall-switch", 'click.checkall', function (e) {

        var items = $(this).closest('table').find(".jq-checkall-item");
        if ($(this).prop("checked") == true) {
            items.each(function () {
                if ($(this).prop("checked") == false) {
                    $(this).click();

                }
            })
        } else {
            items.each(function () {
                if ($(this).prop("checked") == true) {
                    $(this).click();
                }
            })
        }
        //var siblings = Framework.DomHelper.findSibling($(this), ".jq-checkall-item").prop('checked', $(this).is(":checked"));
    });

};

//树及内容区域高度控制
Framework.UI.Behavior.Doc = function () {
    if ($(document.body).width() > 720) {
        if ($(document.body).find(".tree").length > 0) {

            if ($(window).height() < $(document.body).height()) {
                $(".tree").css("height", $(document.body).height() - 60);
            } else {
                $(".tree").css("height", $(window).height() - 128);
            }

        } else {
            // $(".container-fluid").css("height", $(document.body).height() - 70);
        }
    }

}


//给input和select添加span11样式
Framework.UI.Behavior.addClassSpan = function () {
    //$(".wrap-span10 .single-line").each(function () {
    //    $(this).addClass("span11");
    //})

    //$(".wrap-span10 select").each(function () {
    //    $(this).addClass("span11");
    //})

    //$(".wrap-span10 input[type='text']").each(function () {
    //    $(this).addClass("span11");
    //})
}


//窗口缩小到720像素时左侧菜单收拢 .tree的高度自由伸缩
//window.onresize = function () {

//    if ($(document.body).width() < 720) {

//        $(".text-sub").css({ "display": 'none' });
//        $(".tree").css({ "height": 'auto' });
//    } else {
//        Framework.UI.Behavior.Doc();
//    }
//}


//必填项加“*”
Framework.UI.Behavior.JQVerify = function (popup) {
    var dom = document;
    if (!!popup) {
        dom = popup;
    }
    $(function () {
        $(dom).find("input[type=text]").each(function () {
            if (!!($(this).attr('data-val-required'))) {
                $(this).after("<strong style='color:red'>*</strong>");

            }
        })
        $(dom).find("input[type = password]").each(function () {
            if (!!($(this).attr('data-val-required'))) {
                $(this).after("<strong style='color:red'>*</strong>");

            }
        })
        $(dom).find("input[type = number]").each(function () {
            if (!!($(this).attr('data-val-required'))) {
                $(this).after("<strong style='color:red'>*</strong>");

            }
        })
        $(dom).find("select").each(function () {
            if (!!($(this).attr('data-val-required'))) {
                $(this).after("<strong style='color:red'>*</strong>");

            }
        })
    })
}

Framework.Map = function () {
    /** 存放键的数组(遍历用到) */
    this.keys = new Array();
    /** 存放数据 */
    this.data = new Object();

    /**   
            * 放入一个键值对   
            * @param {String} key   
        * @param {Object} value   
        */
    this.put = function (key, value) {
        if (this.data[key] == null) {
            this.keys.push(key);
        }
        this.data[key] = value;
    };

    /**   
      * 获取某键对应的值   
      * @param {String} key   
  * @return {Object} value   
  */
    this.get = function (key) {
        return this.data[key];
    };

    /**   
     * 删除一个键值对   
     * @param {String} key   
 */
    this.remove = function (key) {
        this.keys.remove(key);
        this.data[key] = null;
    };

    /**   
      * 遍历Map,执行处理函数   
      *    
      * @param {Function} 回调函数 function(key,value,index){..}   
     */
    this.each = function (fn) {
        if (typeof fn != 'function') {
            return;
        }
        var len = this.keys.length;
        for (var i = 0; i < len; i++) {
            var k = this.keys[i];
            fn(k, this.data[k], i);
        }
    };

    /**   
     * 获取键值数组(类似Java的entrySet())   
     * @return 键值对象{key,value}的数组   
 */
    this.entrys = function () {
        var len = this.keys.length;
        var entrys = new Array(len);
        for (var i = 0; i < len; i++) {
            entrys[i] = {
                key: this.keys[i],
                value: this.data[i]
            };
        }
        return entrys;
    };

    /**   
     * 判断Map是否为空   
     */
    this.isEmpty = function () {
        return this.keys.length == 0;
    };

    /**   
     * 获取键值对数量   
     */
    this.size = function () {
        return this.keys.length;
    };

    /**   
     * 重写toString    
     */
    this.toString = function () {
        var s = "{";
        for (var i = 0; i < this.keys.length; i++, s += ',') {
            var k = this.keys[i];
            s += k + "=" + this.data[k];
        }
        s += "}";
        return s;
    };
}

Framework.UI.Modal = function (params) {
    var title = "信息提示";
    //    var message = new Framework.Map();
    var message = "";
    var confirmText = "确 认";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    var width = "600";

    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }

    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<form>';
    str += '<div class="modal-dialog" style="width: ' + width + 'px;">';

    //str += '<div class="modal-dialog">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    //str += '<div class="modal-body">';
    str += '<div class="modal-body clearfix">';
    str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';

    //str += '<p>' + message + '</p>';

    str += '<div class="form-body form-horizontal">';
    str += message;
    //message.each(function (key, value, index) {
    //    str += '<div class="form-group">';
    //    str += '<label class="col-md-3 show-title"><label>' + key + '</label></label>';
    //    str += '<div class="col-md-8">';
    //    str += value;
    //    str += '</div>';
    //    str += '</div>';
    //    //if (value != "")
    //    //    url += key + "=" + value + "&";
    //});

    //str += '<div class="form-group">';
    //str += '<label class="col-md-3 show-title"><label for="RoleDescription">机构名称</label></label>';
    //str += '<div class="col-md-8">';
    //str += ' 111';
    //str += '</div>';
    //str += '</div>';

    str += '</div>';




    str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-info">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';


    var popup = $(str);

    popup.success = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-danger");
        popup.find('.messprompt').addClass("alert-success");
        popup.find('.messprompt span:first').removeClass("glyphicon-ban-circle").addClass("glyphicon-ok-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.fail = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-success");
        popup.find('.messprompt').addClass("alert-danger");
        popup.find('.messprompt span:first').removeClass("glyphicon-ok-circle").addClass("glyphicon-ban-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.modal({
        keyboard: true
    });

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
        if (onCancel) {
            onCancel(popup, e);
        }
    })

    popup.on('shown.bs.modal', function (e) {
        if (onShown) {
            alert("onShown");
            onShown(popup, e);
        }
    })

    popup.find(".btn-info").click(function (e) {
        if (onConfirm) {
            onConfirm(popup, e);
        }
        else {
            popup.modal('hide');
            if (onCancel) {
                onCancel(popup, e);
            }
        }
    });
    popup.find(".close,.btn-cancel").click(function (e) {
        if (onCancel) {
            onCancel(popup, e);
        }
    });
};

Framework.UI.ModalBatch = function (params) {
    var title = "信息提示";
    //var hearder = new Array();
    //var message = new Array();;
    var confirmText = "确 认";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    if (params) {
        title = params.title || title;
        //hearder = params.hearder || hearder;
        message = params.message || "";
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }


    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<form>';
    str += '<div class="modal-dialog">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body">';
    //str += '符合条件的记录：'
    str += message;
    //str += '不符合条件的记录：'
    //str += errmessage;

    //str += '<p>' + message + '</p>';

    //str += '<table class="table table-condensed table-bordered" >';
    //str += '<thead>';
    //str += '<tr>';
    //for (var i = 0; i < hearder.length; i++) {
    //    str += '<th>' + hearder[i] + '</th>';
    //}
    ////str += '<th width="15%">机构编码</th>';
    ////str += '<th>机构名称</th>';
    ////str += '<th>机构简称</th>';
    //str += '</tr>';
    //str += '</thead>';
    //str += '<tbody>';
    //for (var i = 0; i < message.length; i++) {
    //    var row = message[i];
    //    str += "<tr>";
    //    for (var j = 0; j < row.length; j++)
    //        str += '<td>' + row[j] + '</td>';
    //    str += "</tr>";
    //}
    ////str += '<tr>';
    ////str += '<td>0111</td>';
    ////str += '<td>1111</td>';
    ////str += '<td>111</td>';
    //str += '</tr>';
    //str += '<tr>';


    //str += '</tbody>';
    //str += '</table>';

    str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-info">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';


    var popup = $(str);

    popup.modal({
        keyboard: true
    });

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })

    popup.on('shown.bs.modal', function (e) {
        if (onShown) {
            alert("onShown");
            onShown(popup, e);
        }
    })

    popup.find(".btn-info").click(function (e) {
        if (onConfirm) {
            onConfirm(popup, e);
        }
        else {
            popup.modal('hide');
        }
    });
    popup.find(".close,.btn-cancel").click(function (e) {
        if (onCancel) {
            onCancel(popup, e);
        }
    });
};

Framework.UI.ModalTablePrompt = function (params) {
    var title = "信息提示";
    //var hearder = new Array();
    //var message = new Array();;
    var confirmText = "关闭";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    if (params) {
        title = params.title || title;
        //hearder = params.hearder || hearder;
        message = params.message || "";
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }


    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<form>';
    str += '<div class="modal-dialog">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body">';
    //str += '<p>' + message + '</p>';
    str += message;
    //str += '<table class="table table-condensed table-bordered" >';
    //str += '<thead>';
    //str += '<tr>';
    //for (var i = 0; i < hearder.length; i++) {
    //    str += '<th>' + hearder[i] + '</th>';
    //}
    ////str += '<th width="15%">机构编码</th>';
    ////str += '<th>机构名称</th>';
    ////str += '<th>机构简称</th>';
    //str += '</tr>';
    //str += '</thead>';
    //str += '<tbody>';
    //for (var i = 0; i < message.length; i++) {
    //    var row = message[i];
    //    str += "<tr>";
    //    for (var j = 0; j < row.length; j++)
    //        str += '<td>' + row[j] + '</td>';
    //    str += "</tr>";
    //}
    ////str += '<tr>';
    ////str += '<td>0111</td>';
    ////str += '<td>1111</td>';
    ////str += '<td>111</td>';
    //str += '</tr>';
    //str += '<tr>';

    //str += '</tbody>';
    //str += '</table>';

    str += '</div>';
    str += '<div class="modal-footer">';
    //str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-default">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';


    var popup = $(str);

    popup.modal({
        keyboard: true
    });

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })

    popup.on('shown.bs.modal', function (e) {
        //if (onShown) {
        //    alert("onShown");
        //    onShown(popup, e);
        //}
    })

    popup.find(".btn-default").click(function (e) {
        if (onConfirm) {
            onConfirm(popup, e);
        }
        else {
            popup.modal('hide');
        }
    });
    popup.find(".close,.btn-cancel").click(function (e) {
        if (onCancel) {
            onCancel(popup, e);
        }
    });
};

Framework.UI.FormModal = function (params) {
    if (isFormModal) {
        return false;
    }
    isFormModal = true;
    var title = "操作提示";
    var message = "";
    var confirmText = "保 存";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    if (params) {
        title = params.title || title;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }

    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<form>';
    str += '<div class="modal-dialog" style="width:800px;">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body">';
    str += '<p id="modalFormxx">' + message + '</p>';
    str += '</div>';
    str += '<div id="messageArea"></div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-info">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';

    var popup = $(str);
    popup.on('shown.bs.modal', function (e) {
        Framework.UI.Behavior.JQVerify(popup);//弹出层加“*”
        if (onShown) {
            onShown(popup, e);
        }
        $.validator.unobtrusive.parse(popup.find("form"));

        if ($.fn.datepicker) {
            popup.find('form .datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
        if ($.fn.select2) {
            popup.find("form .select2me").css("width", "95%").select2({
                placeholder: "请选择"//,
                //allowClear: true
            });
        }
        popup.find('input').on("keypress", function (event) {
            if (event.keyCode == "13") {
                popup.find(".btn-info").click();
                return false;
            }
        });
    })

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })


    popup.on('hide.bs.modal', function (e) {
        isFormModal = false;
    })

    popup.find(".btn-info").click(function (event) {
        if (onConfirm) {
            onConfirm(popup, event);
        }

        event.preventDefault();
        event.stopPropagation();
    });
    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });
    popup.modal({
        keyboard: true
    });
};

Framework.UI.FormModalMy = function (params) {
    if (isFormModal) {
        return false;
    }
    isFormModal = true;
    var title = "操作提示";
    var width = "600";
    var message = "";
    var confirmText = "保 存";
    var cancelText = "关 闭";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    var onBack;
    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
        onBack = params.onBack;
    }
    //    str+='<div class="modal fade">';
    //    str += '    <div class="modal-dialog" style="width: ' + width + 'px;">';
    //    str+='        <div class="modal-content">';
    //    str+='            <div class="modal-header">';
    //    str+='                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>';
    //    str+='                <h4 class="modal-title">'+title+'</h4>';
    //    str+='            </div>';
    //    str+='            <div class="modal-body clearfix">';
    //    //str+='                <div class="padding20">';

    //str += '<div class="modal fade">';
    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    //str += '<form>';
    str += '<div class="modal-dialog" style="width: ' + width + 'px;">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body clearfix">';
    str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';
    // <div class="alert  alert-danger" role="alert">                           <span class="glyphicon glyphicon-ban-circle"></span> 错误提示</div>

    //str += '  <div class="padding20">';
    //str += '<p id="modalFormxx">' + message + '</p>';
    str += "<div id='popupContent'>";
    str += message;
    str += "</div>";
    //str += '</div>';
    str += '</div>';
    //    str += '<div id="messageArea"></div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-info">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    //str += '</form>';
    str += '</div>';

    var popup = $(str);
    popup.success = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-danger");
        popup.find('.messprompt').addClass("alert-success");
        popup.find('.messprompt span:first').removeClass("glyphicon-ban-circle").addClass("glyphicon-ok-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.fail = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-success");
        popup.find('.messprompt').addClass("alert-danger");
        popup.find('.messprompt span:first').removeClass("glyphicon-ok-circle").addClass("glyphicon-ban-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.on('shown.bs.modal', function (e) {
        Framework.UI.Behavior.JQVerify(popup);//弹出层加“*”
        if (onShown) {
            onShown(popup, e);
        }
        $.validator.unobtrusive.parse(popup.find("form"));

        if ($.fn.datepicker) {
            popup.find('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
        if ($.fn.select2) {
            popup.find("form .select2me").css("width", "95%").select2({
                placeholder: "请选择"//,
                //allowClear: true
            });
        }
        popup.find('input').on("keypress", function (event) {
            if (event.keyCode == "13") {
                popup.find(".btn-info").click();
                return false;
            }
        });
    })
    //回调
    if (onBack)
        onBack(popup);

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })


    popup.on('hide.bs.modal', function (e) {
        isFormModal = false;
    })

    popup.find(".btn-info").click(function (event) {
        if (onConfirm) {
            onConfirm(popup, event);
        }

        event.preventDefault();
        event.stopPropagation();
    });
    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });
    popup.modal({
        keyboard: true
    });
};

Framework.UI.FormModalMySearchPopup = function (params) {
    if (isFormModal) {
        return false;
    }
    isFormModal = true;
    var title = "操作提示";
    var width = "600";
    var message = "";
    var confirmText = "保 存";
    var cancelText = "关 闭";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    var onBack;
    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
        onBack = params.onBack;
    }
    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    //str += '<form>';
    str += '<div class="modal-dialog" style="width: ' + width + 'px;">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body clearfix">';
    str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';
    // <div class="alert  alert-danger" role="alert"><span class="glyphicon glyphicon-ban-circle"></span> 错误提示</div>

    //str += '  <div class="padding20">';
    //str += '<p id="modalFormxx">' + message + '</p>';
    str += "<div id='popupContent'>";
    str += message;
    str += "</div>";
    //str += '</div>';
    str += '</div>';
    //    str += '<div id="messageArea"></div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-info">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    //str += '</form>';
    str += '</div>';

    var popup = $(str);
    popup.success = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-danger");
        popup.find('.messprompt').addClass("alert-success");
        popup.find('.messprompt span:first').removeClass("glyphicon-ban-circle").addClass("glyphicon-ok-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.fail = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-success");
        popup.find('.messprompt').addClass("alert-danger");
        popup.find('.messprompt span:first').removeClass("glyphicon-ok-circle").addClass("glyphicon-ban-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.on('shown.bs.modal', function (e) {
        Framework.UI.Behavior.JQVerify(popup);//弹出层加“*”
        if (onShown) {
            onShown(popup, e);
        }
        $.validator.unobtrusive.parse(popup.find("form"));

        if ($.fn.datepicker) {
            popup.find('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
        if ($.fn.select2) {
            popup.find("form .select2me").css("width", "95%").select2({
                placeholder: "请选择"//,
                //allowClear: true
            });
        }
        popup.find('input').on("keypress", function (event) {
            if (event.keyCode == "13") {
                popup.find(".btn-info").click();
                return false;
            }
        });
    })
    //回调
    if (onBack)
        onBack(popup);

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })


    popup.on('hide.bs.modal', function (e) {
        isFormModal = false;
    })

    popup.find(".btn-info").click(function (event) {
        var currobj = $(this);
        if (onConfirm) {
            onConfirm(popup, event, currobj);
        }

        event.preventDefault();
        event.stopPropagation();
    });
    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });
    popup.modal({
        keyboard: true
    });
};

Framework.UI.FormModalMy1 = function (params) {

    var title = "操作提示";
    var width = "600";
    var message = "";
    var confirmText = "保 存";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }

    var str = "";
    str += '<div class="modal fade">';
    str += '    <div class="modal-dialog" style="width: ' + width + 'px;">';
    str += '        <div class="modal-content">';
    str += '            <div class="modal-header">';
    str += '                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>';
    str += '                <h4 class="modal-title">' + title + '</h4>';
    str += '            </div>';
    str += '            <div class="modal-body clearfix">';
    //str+='                <div class="padding20">';
    str += message;
    //str+='                </div>';
    str += '            </div>';
    str += '            <div class="modal-footer">';
    str += '                <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>';
    str += '                <button type="button" class="btn btn-info">保存</button>';
    //str+='                <button type="button" class="btn btn-primary">保存并新增</button>';
    str += '            </div>';
    str += '        </div>';
    str += '    </div>';
    str += '</div>';

    var popup = $(str);
    popup.on('shown.bs.modal', function (e) {
        Framework.UI.Behavior.JQVerify(popup);//弹出层加“*”
        if (onShown) {
            onShown(popup, e);
        }
        $.validator.unobtrusive.parse(popup.find("form"));

        if ($.fn.datepicker) {
            popup.find('form .datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
        if ($.fn.select2) {
            popup.find("form .select2me").css("width", "95%").select2({
                placeholder: "请选择"//,
                //allowClear: true
            });
        }
        popup.find('input').on("keypress", function (event) {
            if (event.keyCode == "13") {
                popup.find(".btn-info").click();
                return false;
            }
        });
    })

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })


    popup.on('hide.bs.modal', function (e) {
        isFormModal = false;
    })

    popup.find(".btn-info").click(function (event) {
        if (onConfirm) {
            onConfirm(popup, event);
        }

        event.preventDefault();
        event.stopPropagation();
    });
    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });
    popup.modal({
        keyboard: true
    });
};

Framework.UI.FormModalAdd = function (params) {
    if (isFormModal) {
        return false;
    }
    isFormModal = true;
    var title = "操作提示";
    var width = "600";
    var message = "";
    var confirmText = "保 存";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onSaveClose;
    var onCancel;
    var onBack;

    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onSaveClose = params.onSaveClose;
        onCancel = params.onCancel;
        onBack = params.onBack;
    }

    //str += '<div class="modal fade">';
    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    //str += '<form>';
    str += '<div class="modal-dialog" style="width: ' + width + 'px;">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body clearfix">';
    str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';
    // <div class="alert  alert-danger" role="alert">                           <span class="glyphicon glyphicon-ban-circle"></span> 错误提示</div>

    //str += '  <div class="padding20">';
    //str += '<p id="modalFormxx">' + message + '</p>';
    str += "<form>";
    str += message;
    //str += '</div>';
    str += "</form>";
    str += '</div>';
    //    str += '<div id="messageArea"></div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-info btn-primary">保存并添加</button>';
    str += '<button type="button" class="btn btn-Close">保存并关闭</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    //str += '</form>';
    str += '</div>';

    var popup = $(str);
    popup.success = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-danger");
        popup.find('.messprompt').addClass("alert-success");
        popup.find('.messprompt span:first').removeClass("glyphicon-ban-circle").addClass("glyphicon-ok-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.fail = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-success");
        popup.find('.messprompt').addClass("alert-danger");
        popup.find('.messprompt span:first').removeClass("glyphicon-ok-circle").addClass("glyphicon-ban-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.on('shown.bs.modal', function (e) {
        Framework.UI.Behavior.JQVerify(popup);//弹出层加“*”
        if (onShown) {
            onShown(popup, e);
        }
        $.validator.unobtrusive.parse(popup.find("form"));

        if ($.fn.datepicker) {
            popup.find('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
        if ($.fn.select2) {
            popup.find("form .select2me").css("width", "95%").select2({
                placeholder: "请选择"//,
                //allowClear: true
            });
        }
        popup.find('input').on("keypress", function (event) {
            if (event.keyCode == "13") {
                popup.find(".btn-info").click();
                return false;
            }
        });
    })

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })

    popup.on('hide.bs.modal', function (e) {
        isFormModal = false;
    })

    popup.find(".btn-info").click(function (event) {
        if (onConfirm) {
            onConfirm(popup, event);
        }
        event.preventDefault();
        event.stopPropagation();
    });

    popup.find(".btn-Close").click(function (event) {
        if (onSaveClose) {
            onSaveClose(popup, event);
        }

        event.preventDefault();
        event.stopPropagation();
    });

    if (onBack)
        onBack(popup);

    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });
    popup.modal({
        keyboard: true
    });
};

Framework.UI.FormModalSubmit = function (params) {
    debugger;
    if (isFormModal) {
        return false;
    }
    isFormModal = true;
    var title = "操作提示";
    var width = "600";
    var message = "";
    var confirmText = "保 存";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onSaveClose;
    var onCancel;
    var onBack;

    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onSaveClose = params.onSaveClose;
        onCancel = params.onCancel;
        onBack = params.onBack;
    }

    //str += '<div class="modal fade">';
    var str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    //str += '<form>';
    str += '<div class="modal-dialog" style="width: ' + width + 'px;">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body clearfix">';
    str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';

    str += "<form>";
    str += message;
    str += "</form>";
    str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '<button type="button" class="btn btn-primary">提交</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</div>';

    var popup = $(str);
    popup.success = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-danger");
        popup.find('.messprompt').addClass("alert-success");
        popup.find('.messprompt span:first').removeClass("glyphicon-ban-circle").addClass("glyphicon-ok-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.fail = function (mess) {
        popup.find('.messprompt').removeClass("hide").removeClass("alert-success");
        popup.find('.messprompt').addClass("alert-danger");
        popup.find('.messprompt span:first').removeClass("glyphicon-ok-circle").addClass("glyphicon-ban-circle");
        popup.find('.messpromptmess').html(mess);
    };

    popup.on('shown.bs.modal', function (e) {
        Framework.UI.Behavior.JQVerify(popup); //弹出层加“*”
        if (onShown) {
            onShown(popup, e);
        }
        $.validator.unobtrusive.parse(popup.find("form"));

        if ($.fn.datepicker) {
            popup.find('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
        if ($.fn.select2) {
            popup.find("form .select2me").css("width", "95%").select2({
                placeholder: "请选择" //,
                //allowClear: true
            });
        }
        popup.find('input').on("keypress", function (event) {
            if (event.keyCode == "13") {
                popup.find(".btn-info").click();
                return false;
            }
        });
    });

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    });

    popup.on('hide.bs.modal', function (e) {
        isFormModal = false;
    });

    popup.find(".btn-primary").click(function (event) {
        debugger;
        if (onConfirm) {
            onConfirm(popup, event);
        }
        debugger;
        if (onSaveClose) {
            onSaveClose(popup, event);
        }

        event.preventDefault();
        event.stopPropagation();
    });

    if (onBack)
        onBack(popup);

    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });
    popup.modal({
        keyboard: true
    });
};


Framework.UI.ModalInfo = function (params) {
    var title = "信息提示";
    var message = "";
    var confirmText = "关 闭";
    var cancelText = "取 消";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    var width = "600";

    if (params) {
        title = params.title || title;
        width = params.width || width;
        message = params.message || message;
        confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }



    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<form>';
    str += '<div class="modal-dialog">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body">';
    str += '<p>' + message + '</p>';
    str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + confirmText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';

    var popup = $(str);

    popup.modal({
        keyboard: true
    });

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })

    popup.on('shown.bs.modal', function (e) {
        if (onShown) {
            alert("onShown");
            onShown(popup, e);
        }
    })

    popup.find(".btn-info").click(function (e) {
        if (onConfirm) {
            onConfirm(popup, e);
        }
        else {
            popup.modal('hide');
        }
    });


    popup.find(".btn-default").click(function (e) {
        onConfirm(popup, e);
    });
    popup.find(".close").click(function (e) {
        onConfirm(popup, e);
    });
    //popup.find(".close,.btn-cancel").click(function (e) {
    //    if (onCancel) {
    //        onCancel(popup, e);
    //    }
    //});
};

//详情弹出层
Framework.UI.FormDetailModal = function (params) {
    var title = "操作提示";
    var message = "";
    var cancelText = "关 闭";
    var onShown;
    var onHidden;
    var onConfirm;
    var onCancel;
    if (params) {
        title = params.title || title;
        message = params.message || message;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onCancel = params.onCancel;
    }


    str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<form>';
    str += '<div class="modal-dialog">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body">';
    str += message;
    str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';


    var popup = $(str);



    popup.on('shown.bs.modal', function (e) {
        if (onShown) {
            onShown(popup, e);
        }


        $.validator.unobtrusive.parse(popup.find("form"));
        //if ($.fn.bootstrapSwitch) {
        //    popup.find("form .switch")['bootstrapSwitch']();
        //}
        if ($.fn.datepicker) {
            popup.find('form .datepicker').datepicker().on('changeDate', function (ev) {
                $(this).datepicker('hide');
            });
        }
        if ($.fn.uniform) {

            var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
            if (test.size() > 0) {
                test.each(function () {
                    if ($(this).parents(".checker").size() == 0) {
                        $(this).show();
                        $(this).uniform();
                    }
                });
            }
        }
    })

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })

    popup.find(".btn-info").click(function (event) {
        if (onConfirm) {
            onConfirm(popup, event);
        }

        event.preventDefault();
        event.stopPropagation();
    });
    popup.find(".close,.btn-cancel").click(function (event) {
        if (onCancel) {
            onCancel(popup, event);
        }
    });

    popup.modal({
        keyboard: true
    });

};



Framework.UI.ProgressModal = function (params) {
    var title = "操作提示";
    var message = "";
    var from = 0;
    var to = 0;
    var onShown;
    var onHidden;
    var onProgress;
    if (params) {
        title = params.title || title;
        message = params.message || message;
        from = params.from || from;
        to = params.total || to;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onProgress = params.onProgress;
    }
    str = '<div class="modal hide fade" aria-hidden="true">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal"" aria-hidden="true">&times;</button>';
    str += '<h4>' + title + '</h4>';
    str += '</div>';

    str += '<div class="modal-body"><p>';
    str += '<div class="progress progress-success">';
    str += '<div class="bar" style="width: 0%">' + from + '/' + to + '</div>';
    str += '</div>';
    str += '</p></div>';

    str += '</div>';

    str = '<div class="modal fade in">';
    str += '<div class="modal-dialog">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4>' + title + '</h4>';
    str += '</div>';
    str += '<div class="modal-body">';
    str += '<div class="progress">';
    str += '<div class="progress-bar" role="progressbar"  aria-valuemin="0" aria-valuemax="100" style="width:0%;">';
    str += '<span class="sr-only">' + from + '/' + to + '</span>';
    str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '<div class="modal-footer">';

    str += '</div>';
    str += '</div><!-- /.modal-content -->';
    str += '</div><!-- /.modal-dialog -->';
    str += '</div>';


    var popup = $(str);


    popup.on('shown.bs.modal', function (e) {
        if (onShown) {
            onShown(popup, e);
        }
        if (onProgress) {
            onProgress(popup);
            popup.find('.sr-only').html(from + '/' + to)
        }
    })

    popup.on('hidden.bs.modal', function (e) {
        if (onHidden) {
            onHidden(popup, e);
        }
        popup.remove();
    })

    popup.modal({
        keyboard: true
    });

    return {
        Increase: function () {
            from = from + 1;
            $('.progress-bar').attr('style', 'width:' + ((from / to) * 100).toFixed(0) + '%');
            if (from == to) {
                $('.progress-bar').attr('style', 'width:100%');
            }
            popup.find('.sr-only').html(from + '/' + to);
        }
    }
};


Framework.UI.HandleError = function (jqXHR, customMessage, onErrorCallback) {
    var showErrorInfo = function (msg) {
        Framework.UI.ModalInfo({ title: "出错提示", message: customMessage + "</br>" + msg });
    }
    var userJustLeaveThePage = function (xhr) {
        return !xhr.getAllResponseHeaders();
    }
    // 0 - Chrom: 无法连接服务器
    // 12007 - IE: 服务器找不到
    // 12009 - IE: 服务器连接出错
    if (jqXHR.status == 0 || jqXHR.status == 12007 || jqXHR.status == 12029) {
        showErrorInfo("网络连接错误！");
        return;
    }

    if (userJustLeaveThePage(jqXHR)) {//ajax request was aborted - no error here.
        return;
    }
    // TODO: 利用特殊的错误状态提示错误信息
    var textStatus = arguments.callee.caller.arguments[1] || '';

    if (jqXHR.status == 499) {
        showErrorInfo("Session过期或不合法!");
        return;
    }

    var errorMessage = "未知错误！";
    var detailMessage = "未知错误！";

    try {
        var result = $.parseJSON(jqXHR.responseText);
        errorMessage = result.DisplayMessage;
        detailMessage = result.DetailMessage;
    } catch (err) {
        detailMessage = err.name + " - " + err.message;
    }

    if (jqXHR.status == 404) {
        showErrorInfo("请求的URL地址不存在！");

    } else if (jqXHR.status == 401) {
        showErrorInfo("用户认证信息不合法！");

    } else if (jqXHR.status == 500) {
        showErrorInfo("服务器错误！</br>" + detailMessage);

    } else if (textStatus == 'parsererror') {
        showErrorInfo("程序解析出错！");

    } else if (textStatus == 'timeout') {
        showErrorInfo("网络超时！");

    } else if (jqXHR.status != 498) {
        showErrorInfo("未知错误！<br/>" + detailMessage);

    } else if (typeof (onErrorCallback) === "undefined") {
        showErrorInfo(errorMessage);

    } else {
        onErrorCallback(errorMessage, detailMessage);
    }
}

//页面基本UI效果：显示/隐藏，Tooggle等 @Date:2013-03-13,@author:link
Framework.UI.SetShow = function () {
    /*左侧nav块状导航*/
    $(".nav-left").click(function () {
        var isHide = $(".con-display").css("display");
        //alert(isHide);
        var bgColor = $(".nav-bgcolor").css("background-color");
        //alert(bgColor);
        if (isHide == "none") {
            $(".nav-left").css("background", bgColor);
            $(".display-level").addClass("display-vertical").removeClass("display-level");
            $(".con-display").fadeIn(1000);
        } else {
            //alert(isHide);
            $(".nav-left").css("background", "");
            $(".display-vertical").addClass("display-level").removeClass("display-vertical");
            $(".con-display").fadeOut(0);
        }
        //$(".con-display").toggle(500);
    });

    // 高级搜索切换
    $(".research").click(function () {
        if ($(this).text() == "高级查询") {
            $(".research-show").fadeIn(1000);
            $(this).text("普通查询");
        } else {
            $(".research-show").fadeOut(500);
            $(this).text("高级查询");
        }
    });

}


Framework.UI.Validation = function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || /^\d{4}[\/\-]\d{1,2}[\/\-]\d{1,2}$/.test(value);
    };

    $.validator.addMethod("childrenof", function (value, element, params) {
        var parent = params.parent;
        var length = parseInt(params.length);

        var v2 = value.trim();
        var v1;

        if ($(parent).length == 0) {
            return true;
        }
        else {
            v1 = $(parent).val().trim()
        }
        return v2.length == (v1.length + length) && v2.indexOf(v1) > -1
    });


    $.validator.unobtrusive.adapters.add("childrenof", ["parent", "length"], function (options) {
        options.rules["childrenof"] = {
            parent: "#" + options.params.parent,
            length: options.params.length
        };
        options.messages["childrenof"] = options.message;
    });
}
Framework.UI.Validation();

Framework.UI.Placeholder = function () {

    function isPlaceholer() {
        var input = document.createElement("input");
        return "placeholder" in input;
    }

    if (!isPlaceholer()) {

        $('input[type="text"]').each(function () {
            if ($(this).val() === "") {
                // $(this).val($(this).css("color",'#a3a3a3').attr("placeholder"));
            }
        })

        $(document).delegate('input[type="text"]', 'focusout.input', function (event) {
            var v1 = $(this).val();
            var v2 = $(this).attr("placeholder");

            if (v2 !== "" && v1 === "") {
                $(this).css("color", '#a3a3a3').val(v2);
            }
        });

        $(document).delegate('input[type="text"]', 'focusin.input', function (event) {
            var v1 = $(this).val();
            var v2 = $(this).attr("placeholder");

            if (v2 !== "" && v1 === v2) {
                $(this).val("");
            }
        });
    }
};


function toTxt(str) {
    str = str + "";
    var RexStr = /\<|\>/g;
    str = str.replace(RexStr,
        function (MatchStr) {
            switch (MatchStr) {
                case "<":
                    return "&lt;";
                    break;
                case ">":
                    return "&gt;";
                    break;
            }
        }
    )
    return str;
};

Framework.UI.DisableModalLoopForIE8 = function () {
    $.fn.modal.Constructor.prototype.enforceFocus = function () {
        var that = this
        $(document).off('focusin.modal').on('focusin.modal', function (e) {
            if (that.$element[0] !== e.target && !that.$element.has(e.target).length) {
                that.$element.focus()
            }
        })
    };
};

Import("Framework.JsFunHandle");

//批量删除
//tbheadsobjs:批量删除表头提示
//checkeds：选中的复选框
//map：tr行data-xxx与字段映射
//url：地址
//bRefresh：是否刷新地址栏，true：刷新地址栏，false：不刷新，未定义则刷新
Framework.JsFunHandle.BatchDelete = function (params) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {
    var bRefresh = params.bRefresh;
    if (Framework.Tool.isUndefined(bRefresh))
        bRefresh = true;

    //tbheadsobjs, checkeds, map, url, bRefresh, Title, fun
    var checkeds = params.checkeds;

    var initData = params["initData"];
    var areasRoute = initData.areasRoute;
    var map = initData.rowFieldMap;
    var tbheadsobjs = initData.tbhead;

    var url = areasRoute + "/" + initData.deleteUrl;
    var Title = initData.modularName;
    var recCheckCallback = params["recCheckCallback"];
    var fun = initData.callbackfun;

    var strMess = '<table class="table table-condensed table-bordered" >';
    strMess += '<thead>';
    strMess += '<tr>';
    //不符合条件
    var strerrmessage = '以下为不符合条件的记录：<table class="table table-condensed table-bordered" ><thead><tr>';

    for (att in tbheadsobjs) {
        strMess += '<td>' + tbheadsobjs[att] + '</td>';
        strerrmessage += '<td>' + tbheadsobjs[att] + '</td>';
    }
    strMess += '</tr>';
    strMess += '</thead>';
    strMess += '<tbody>';
    strerrmessage += "</tr></thead><tbody>";

    ////////////////////////////////////////////////////////////////
    //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
    var strErrMess = "";
    checkeds.each(function () {
        //执行回调：是否满足条件的
        var tr = $(this).closest('tr');
        if (recCheckCallback) {
            if (recCheckCallback(tr)) {
                strErrMess += '<tr>';
                var rowItem = tr.data();
                for (att in tbheadsobjs) {
                    strMess += '<td>' + rowItem[att] + '</td>'
                }
                strMess += '<tr>';
            }
            else {
                strErrMess += '<tr>';
                var rowItem = tr.data();
                for (att in tbheadsobjs) {
                    strErrMess += '<td>' + rowItem[att] + '</td>'
                }
                strErrMess += '<tr>';
            }
        }
        else {
            strMess += '<tr>';
            var rowItem = tr.data();
            for (att in tbheadsobjs) {
                strMess += '<td>' + rowItem[att] + '</td>'
            }
            strMess += '<tr>';
        }
    });
    strMess += '</tbody>';
    strMess += '</table>';

    if (strErrMess.length > 0) {
        strerrmessage += strErrMess;
        strerrmessage += '</tbody>';
        strerrmessage += '</table>';

        strMess += strerrmessage;
    }
    var paramsn = {
        title: '【批量删除' + Title + '确认】',
        message: strMess,
        onConfirm: function (popup) {
            var opterSussecRows = [];
            var opterErrorRows = [];
            //////////执行操作/////////////////
            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
            checkeds.each(function () {
                var tr = $(this).closest('tr');
                if (recCheckCallback) {
                    if (recCheckCallback(tr)) {
                        var rowItem = tr.data();
                        var obj = Framework.Tool.GetTbRowData($(this), map);

                        Framework.Ajax.PostJsonAsync(obj, url,
                            function (result) {
                                var Data = result.Data;
                                if (Data.RespAttachInfo.bError) {
                                    opterErrorRows.push({ trobj: tr, errorMessag: '' });
                                }
                                else
                                    opterSussecRows.push(tr);
                            });
                    }
                }
                else {
                    var rowItem = tr.data();
                    var obj = Framework.Tool.GetTbRowData($(this), map);

                    Framework.Ajax.PostJsonAsync(obj, url,
                        function (result) {
                            var Data = result.Data;
                            if (Data.RespAttachInfo.bError) {
                                var strerrors = Framework.UI.Behavior.ErrorHandling(result);
                                opterErrorRows.push({ trobj: tr, errorMessag: strerrors });
                            }
                            else
                                opterSussecRows.push(tr);
                        });
                }
            });
            popup.modal('hide');
            //popup.remove();//('hide');
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
            strMess += '<table class="table table-condensed table-bordered" >';
            strMess += '<thead>';
            strMess += '<tr>';
            for (att in tbheadsobjs) {
                strMess += '<td>' + tbheadsobjs[att] + '</td>'
            }
            strMess += '<td>出错原因</td>'
            strMess += '</tr>';
            strMess += '</thead>';
            for (var i = 0; i < opterErrorRows.length; i++) {
                strMess += '<tr>';
                var rowItem = null;

                //if (opterErrorRows[i].data() == undefined) {
                //    alert("请先删除与之相关联的信息");
                //    return;
                //}
                //else {
                //    rowItem = opterErrorRows[i].data();
                //}
                rowItem = opterErrorRows[i].trobj.data();
                for (att in tbheadsobjs) {
                    strMess += '<td>' + rowItem[att] + '</td>'
                }
                strMess += '<td>' + opterErrorRows[i].errorMessag + '</td>';
                strMess += '<tr>';
            }
            strMess += '</table>';

            var paramserror = {
                title: '操作提示',
                message: strMess,
                onConfirm: function (popup) {
                    if (bRefresh)
                        window.location.reload(true);
                    for (var i = 0; i < opterSussecRows.length; i++) {
                        opterSussecRows[i].remove();
                    }
                    popup.find(".close,.btn-cancel").trigger("click");
                },
                onCancel: function (popup) {
                }
            };

            Framework.UI.ModalTablePrompt(paramserror);
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.ModalBatch(paramsn);
};

//后台批量删除,带计算
Framework.JsFunHandle.BatchDeleteNew = function (currobj) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {

    var table = $(currobj.data("tableid"));
    var calcol = table.data("calcol");
    var checkeds = table.find(".jq-checkall-item:checked");
    var tbheadsobjs = table.find("thead").data();
    var url = table.data("deleteurl");
    var Title = table.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "";
    var strMess = '<table class="table table-condensed table-bordered" >';
    strMess += '<thead>';
    strMess += '<tr>';
    //不符合条件
    var strerrmessage = '以下为不符合条件的记录：';
    strerrmessage += '<table class="table table-condensed table-bordered" ><thead><tr>';

    for (att in tbheadsobjs) {
        strMess += '<td>' + tbheadsobjs[att] + '</td>';
        strerrmessage += '<td>' + tbheadsobjs[att] + '</td>';
    }
    strMess += '</tr>';
    strMess += '</thead>';
    strMess += '<tbody>';
    strerrmessage += "</tr></thead><tbody>";

    ////////////////////////////////////////////////////////////////
    //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
    var strErrMess = "";
    checkeds.each(function () {
        //执行回调：是否满足条件的
        var tr = $(this).closest('tr');
        strMess += '<tr>';
        var rowItem = tr.data();
        for (att in tbheadsobjs) {
            strMess += '<td>' + rowItem[att] + '</td>'
        }
        strMess += '<tr>';
    });
    strMess += '</tbody>';
    strMess += '</table>';
    //strMess += "</div>";
    if (strErrMess.length > 0) {
        strerrmessage += strErrMess;
        strerrmessage += '</tbody>';
        strerrmessage += '</table>';
        //strerrmessage += "</div>";
        strMess += strerrmessage;
    }
    var paramsn = {
        title: '【批量删除' + Title + '确认】',
        message: strMess,
        onConfirm: function (popup) {
            var opterSussecRows = [];
            var opterErrorRows = [];
            //////////执行操作/////////////////
            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
            var tbodyDom = table.find("tbody");
            checkeds.each(function () {

                var tr = $(this).closest('tr');
                var rowItem = tr.data();
                var entityobj = {};
                for (att in rowItem) {
                    var AttName = tbodyDom.data(att);
                    if (!Framework.Tool.isUndefined(AttName)) {
                        var Att = AttName;
                        var preName = "Item." + Att;
                        entityobj[preName] = rowItem[att];
                    }
                }

                Framework.Ajax.PostJson(entityobj, url, function (results) {
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            popup.fail(strerrors);
                        return;
                    }
                });
                //var rowItem = tr.data();
                //tr.remove();
            });
            alert("删除成功");
            //if (bRefresh)
            window.location.reload(true);
            //tr.remove();
            popup.find(".btn-default").trigger("click");
            ////countFn();
            //// connt.html(countFn());
            ////if (fun)
            ////    fun();

            ////计算
            //if (!Framework.Tool.isUndefined(calcol))
            //    Framework.Tool.CalculationColNew(calcol, table);
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.ModalBatch(paramsn);
};

////异步批量删除
//Framework.JsFunHandle.BatchDeleteAsyn = function (params) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {
//    var bRefresh = params.bRefresh;
//    if (Framework.Tool.isUndefined(bRefresh))
//        bRefresh = true;

//    //tbheadsobjs, checkeds, map, url, bRefresh, Title, fun
//    var checkeds = params.checkeds;

//    var initData = params["initData"];
//    var areasRoute = initData.areasRoute;
//    var map = initData.rowFieldMap;
//    var tbheadsobjs = initData.tbhead;

//    var url = areasRoute + "/" + initData.deleteUrl;
//    var Title = initData.modularName;
//    var recCheckCallback = params["recCheckCallback"];
//    var fun = initData.callbackfun;

//    var strMess = '<table class="table table-condensed table-bordered" >';
//    strMess += '<thead>';
//    strMess += '<tr>';
//    //不符合条件
//    var strerrmessage = '以下为不符合条件的记录：<table class="table table-condensed table-bordered" ><thead><tr>';

//    for (att in tbheadsobjs) {
//        strMess += '<td>' + tbheadsobjs[att] + '</td>';
//        strerrmessage += '<td>' + tbheadsobjs[att] + '</td>';
//    }
//    strMess += '</tr>';
//    strMess += '</thead>';
//    strMess += '<tbody>';
//    strerrmessage += "</tr></thead><tbody>";

//    ////////////////////////////////////////////////////////////////
//    //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
//    var strErrMess = "";
//    checkeds.each(function () {
//        //执行回调：是否满足条件的
//        var tr = $(this).closest('tr');
//        if (recCheckCallback) {
//            if (recCheckCallback(tr)) {
//                strErrMess += '<tr>';
//                var rowItem = tr.data();
//                for (att in tbheadsobjs) {
//                    strMess += '<td>' + rowItem[att] + '</td>'
//                }
//                strMess += '<tr>';
//            }
//            else {
//                strErrMess += '<tr>';
//                var rowItem = tr.data();
//                for (att in tbheadsobjs) {
//                    strErrMess += '<td>' + rowItem[att] + '</td>'
//                }
//                strErrMess += '<tr>';
//            }
//        }
//        else {
//            strMess += '<tr>';
//            var rowItem = tr.data();
//            for (att in tbheadsobjs) {
//                strMess += '<td>' + rowItem[att] + '</td>'
//            }
//            strMess += '<tr>';
//        }
//    });
//    strMess += '</tbody>';
//    strMess += '</table>';

//    if (strErrMess.length > 0) {
//        strerrmessage += strErrMess;
//        strerrmessage += '</tbody>';
//        strerrmessage += '</table>';

//        strMess += strerrmessage;
//    }
//    var paramsn = {
//        title: '【批量删除' + Title + '确认】',
//        message: strMess,
//        onConfirm: function (popup) {
//            var opterSussecRows = [];
//            var opterErrorRows = [];
//            //////////执行操作/////////////////
//            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
//            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
//            checkeds.each(function () {
//                var tr = $(this).closest('tr');
//                if (recCheckCallback) {
//                    if (recCheckCallback(tr)) {
//                        var rowItem = tr.data();
//                        var obj = Framework.Tool.GetTbRowData($(this), map);

//                        Framework.Ajax.PostJsonAsync(obj, url,
//                            function (result) {
//                                var Data = result.Data;
//                                if (Data.RespAttachInfo.bError) {
//                                    opterErrorRows.push({ trobj: tr, errorMessag: '' });
//                                }
//                                else
//                                    opterSussecRows.push(tr);
//                            });
//                    }
//                }
//                else {
//                    var rowItem = tr.data();
//                    var obj = Framework.Tool.GetTbRowData($(this), map);

//                    Framework.Ajax.PostJsonAsync(obj, url,
//                        function (result) {
//                            var Data = result.Data;
//                            if (Data.RespAttachInfo.bError) {
//                                var strerrors = Framework.UI.Behavior.ErrorHandling(result);
//                                opterErrorRows.push({ trobj: tr, errorMessag: strerrors });
//                            }
//                            else
//                                opterSussecRows.push(tr);
//                        });
//                }
//            });
//            popup.modal('hide');
//            //popup.remove();//('hide');
//            var strMess = '以下记录操作成功：';
//            strMess += '<table class="table table-condensed table-bordered" >';
//            strMess += '<thead>';
//            strMess += '<tr>';
//            for (att in tbheadsobjs) {
//                strMess += '<td>' + tbheadsobjs[att] + '</td>'
//            }
//            strMess += '</tr>';
//            strMess += '</thead>';
//            for (var i = 0; i < opterSussecRows.length; i++) {
//                strMess += '<tr>';
//                var rowItem = opterSussecRows[i].data();
//                for (att in tbheadsobjs) {
//                    strMess += '<td>' + rowItem[att] + '</td>'
//                }
//                strMess += '<tr>';
//            }
//            strMess += '</table>'
//            strMess += '以下记录操作失败：'
//            strMess += '<table class="table table-condensed table-bordered" >';
//            strMess += '<thead>';
//            strMess += '<tr>';
//            for (att in tbheadsobjs) {
//                strMess += '<td>' + tbheadsobjs[att] + '</td>'
//            }
//            strMess += '<td>出错原因</td>'
//            strMess += '</tr>';
//            strMess += '</thead>';
//            for (var i = 0; i < opterErrorRows.length; i++) {
//                strMess += '<tr>';
//                var rowItem = null;

//                //if (opterErrorRows[i].data() == undefined) {
//                //    alert("请先删除与之相关联的信息");
//                //    return;
//                //}
//                //else {
//                //    rowItem = opterErrorRows[i].data();
//                //}
//                rowItem = opterErrorRows[i].trobj.data();
//                for (att in tbheadsobjs) {
//                    strMess += '<td>' + rowItem[att] + '</td>'
//                }
//                strMess += '<td>' + opterErrorRows[i].errorMessag + '</td>';
//                strMess += '<tr>';
//            }
//            strMess += '</table>';

//            var paramserror = {
//                title: '操作提示',
//                message: strMess,
//                onConfirm: function (popup) {
//                    if (bRefresh)
//                        window.location.reload(true);
//                    for (var i = 0; i < opterSussecRows.length; i++) {
//                        opterSussecRows[i].remove();
//                    }
//                    popup.find(".close,.btn-cancel").trigger("click");
//                },
//                onCancel: function (popup) {
//                }
//            };
//            Framework.UI.ModalTablePrompt(paramserror);
//        },
//        onCancel: function (popup) {
//        }
//    };
//    Framework.UI.ModalBatch(paramsn);
//};



//批量删除UI
Framework.JsFunHandle.BatchDeleteUI = function (params) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {
    var bRefresh = params.bRefresh;
    if (Framework.Tool.isUndefined(bRefresh))
        bRefresh = true;

    //tbheadsobjs, checkeds, map, url, bRefresh, Title, fun
    var checkeds = params.checkeds;

    var initData = params["initData"];
    var areasRoute = initData.areasRoute;
    var map = initData.rowFieldMap;
    var tbheadsobjs = initData.tbhead;

    var url = areasRoute + "/" + initData.deleteUrl;
    var Title = initData.modularName;
    var recCheckCallback = params["recCheckCallback"];
    var fun = params.callbackfun;
    //var strMess = "<div style='width: 100%; overflow: auto; height: 380px;'>";
    //var strMess = "<div class='mylist container-fluid' style='height: 200px;'>";

    var strMess = '<table class="table table-condensed table-bordered" >';
    strMess += '<thead>';
    strMess += '<tr>';
    //不符合条件
    var strerrmessage = '以下为不符合条件的记录：';
    //strerrmessage += '<div class="mylist container-fluid" style="height: 200px;">';
    //strerrmessage += '<div class="mylist container-fluid" style="height: 300px;">';
    strerrmessage += '<table class="table table-condensed table-bordered" ><thead><tr>';

    for (att in tbheadsobjs) {
        strMess += '<td>' + tbheadsobjs[att] + '</td>';
        strerrmessage += '<td>' + tbheadsobjs[att] + '</td>';
    }
    strMess += '</tr>';
    strMess += '</thead>';
    strMess += '<tbody>';
    strerrmessage += "</tr></thead><tbody>";

    ////////////////////////////////////////////////////////////////
    //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
    var strErrMess = "";
    checkeds.each(function () {
        //执行回调：是否满足条件的
        var tr = $(this).closest('tr');
        if (recCheckCallback) {
            if (recCheckCallback(tr)) {
                strErrMess += '<tr>';
                var rowItem = tr.data();
                for (att in tbheadsobjs) {
                    strMess += '<td>' + rowItem[att] + '</td>'
                }
                strMess += '<tr>';
            }
            else {
                strErrMess += '<tr>';
                var rowItem = tr.data();
                for (att in tbheadsobjs) {
                    strErrMess += '<td>' + rowItem[att] + '</td>'
                }
                strErrMess += '<tr>';
            }
        }
        else {
            strMess += '<tr>';
            var rowItem = tr.data();
            for (att in tbheadsobjs) {
                strMess += '<td>' + rowItem[att] + '</td>'
            }
            strMess += '<tr>';
        }
    });
    strMess += '</tbody>';
    strMess += '</table>';
    //strMess += "</div>";
    if (strErrMess.length > 0) {
        strerrmessage += strErrMess;
        strerrmessage += '</tbody>';
        strerrmessage += '</table>';
        //strerrmessage += "</div>";
        strMess += strerrmessage;
    }
    var paramsn = {
        title: '【批量删除' + Title + '确认】',
        message: strMess,
        onConfirm: function (popup) {
            var opterSussecRows = [];
            var opterErrorRows = [];
            //////////执行操作/////////////////
            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
            checkeds.each(function () {
                var tr = $(this).closest('tr');
                if (recCheckCallback) {
                    if (recCheckCallback(tr)) {
                        tr.remove();
                    }
                }
                else {
                    var rowItem = tr.data();
                    tr.remove();
                }
            });
            if (fun)
                fun();
            popup.find(".btn-default").trigger("click");
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.ModalBatch(paramsn);
};

//批量删除UI,带计算
Framework.JsFunHandle.BatchDeleteUINew = function (currobj) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {

    var table = $(currobj.data("tableid"));
    var calcol = table.data("calcol");
    var checkeds = table.find(".jq-checkall-item:checked");
    var tbheadsobjs = table.find("thead").data();
    var Title = table.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "";
    var strMess = '<table class="table table-condensed table-bordered" >';
    strMess += '<thead>';
    strMess += '<tr>';
    //不符合条件
    var strerrmessage = '以下为不符合条件的记录：';
    strerrmessage += '<table class="table table-condensed table-bordered" ><thead><tr>';

    for (att in tbheadsobjs) {
        strMess += '<td>' + tbheadsobjs[att] + '</td>';
        strerrmessage += '<td>' + tbheadsobjs[att] + '</td>';
    }
    strMess += '</tr>';
    strMess += '</thead>';
    strMess += '<tbody>';
    strerrmessage += "</tr></thead><tbody>";

    ////////////////////////////////////////////////////////////////
    //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
    var strErrMess = "";
    checkeds.each(function () {
        //执行回调：是否满足条件的
        var tr = $(this).closest('tr');
        strMess += '<tr>';
        var rowItem = tr.data();
        for (att in tbheadsobjs) {
            strMess += '<td>' + rowItem[att] + '</td>'
        }
        strMess += '<tr>';
    });
    strMess += '</tbody>';
    strMess += '</table>';
    //strMess += "</div>";
    if (strErrMess.length > 0) {
        strerrmessage += strErrMess;
        strerrmessage += '</tbody>';
        strerrmessage += '</table>';
        //strerrmessage += "</div>";
        strMess += strerrmessage;
    }
    var paramsn = {
        title: '【批量删除' + Title + '确认】',
        message: strMess,
        onConfirm: function (popup) {
            var opterSussecRows = [];
            var opterErrorRows = [];
            //////////执行操作/////////////////
            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
            checkeds.each(function () {
                var tr = $(this).closest('tr');
                var rowItem = tr.data();
                tr.remove();
            });
            //if (fun)
            //    fun();

            popup.find(".btn-default").trigger("click");

            //计算
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, table);

            //if (calcol.length > 0) {//需要列计算
            //    var arr1 = calcol.split(",");
            //    if (arr1.length) {
            //        for (var i = 0; arr1.length; i++) {
            //            var arr = arr1[i].split("|");
            //            //var table = obj.closest('table');
            //            var trs = table.find('.' + arr[0]);
            //            var sum = 0;
            //            var errorMessage = "";
            //            trs.each(function (i) {
            //                var val = $(this).val();//.val();
            //                //判断是否为数值
            //                val = Number(val);
            //                //price = Number(price);
            //                if (!Framework.Tool.isNumber(val)) {
            //                    errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
            //                    alert(errorMessage);
            //                }
            //                //if (!(val % 1 === 0)) {
            //                //    errorMessage += Framework.Const.ValidationMessageFormatConst.IntType();
            //                //    alert(errorMessage);
            //                //}
            //                if (errorMessage.length == 0) {
            //                    sum += val;
            //                    //sumPrice += val * price;
            //                }
            //            });
            //            if (arr.length < 3 || arr[2] == "3" || arr[2] == "1")//1：整数 3：小数 5：货币
            //                table.find("." + arr[0] + 'Total').html(sum);
            //            if (arr[2] == "5") {
            //                var valt = sum.toString().fmMoney(2);
            //                table.find("." + arr[0] + 'Total').html(valt);
            //            }
            //            //table.find("." + arr[0] + 'Total').html(sum);
            //        }
            //    }
            //}






        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.ModalBatch(paramsn);
};

//界面批量删除
$(document).delegate(".btn-BatchDeleteUI", "click", function () {
    var currobj = $(this);
    Framework.JsFunHandle.BatchDeleteUINew(currobj);
});

//单条记录删除
Framework.JsFunHandle.Delete = function (currobj, bRefresh, params, fun) {// tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    //Framework.JsFunHandle.Delete = function (currobj, tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    //alert("sdsd");
    if (Framework.Tool.isUndefined(bRefresh))
        bRefresh = true;

    var areasRoute = params.areasRoute;
    var tbheadsobjs = params.tbhead;
    var map = params.rowFieldMap;
    var url = areasRoute + "/" + params.deleteUrl;
    var Title = params.modularName;
    var Width = params.popupwidth || 600;

    var tr = currobj.closest('tr');
    var entityobj = Framework.Tool.GetTbRowData(currobj, map);
    var rowItem = tr.data();

    var strMess = '';
    for (att in tbheadsobjs) {

        strMess += '<div class="form-group">';
        strMess += '<label class="col-md-2"></label>';
        strMess += '<label class="col-md-4 show-title"><label>' + tbheadsobjs[att] + '：</label></label>';
        strMess += '<div class="col-md-6">';
        strMess += rowItem[att];
        strMess += '</div>';
        strMess += '</div>';
    }
    strMess += ''

    var params = {
        title: '【删除' + Title + '确认】',
        width: Width,
        message: strMess,
        onConfirm: function (popup) {
            Framework.Ajax.PostJson(entityobj, url, function (results) {
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }
                alert("删除成功");
                if (bRefresh)
                    window.location.reload(true);
                tr.remove();
                popup.find(".btn-default").trigger("click");
                //countFn();
                // connt.html(countFn());
                if (fun)
                    fun();
            })
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.Modal(params);
}

//后台单条记录删除，带计算
Framework.JsFunHandle.DeleteNew = function (currobj) {//, bRefresh, params) {// tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    var tr = currobj.closest('tr');
    var table = currobj.closest('table');
    var calcol = table.data("calcol");
    var tbheadsobjs = table.find("thead").data();
    var Title = table.data("popuptitle");
    var url = table.data("deleteurl");// areasRoute + "/" + params.deleteUrl;
    if (Framework.Tool.isUndefined(Title))
        Title = "";
    var Width = table.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(Width))
        Width = 600;
    //var entityobj = Framework.Tool.GetTbRowData(currobj, map);

    var rowItem = tr.data();

    var strMess = '';
    for (att in tbheadsobjs) {
        strMess += '<div class="form-group">';
        strMess += '<label class="col-md-2"></label>';
        strMess += '<label class="col-md-4 show-title"><label>' + tbheadsobjs[att] + '：</label></label>';
        strMess += '<div class="col-md-6">';
        strMess += rowItem[att];
        strMess += '</div>';
        strMess += '</div>';
    }
    strMess += ''

    ////////////////////14号：新国展/////////////////////////////////////////
    var entityobj = {};
    var tbodyDom = table.find("tbody");
    for (att in rowItem) {
        var AttName = tbodyDom.data(att);
        if (!Framework.Tool.isUndefined(AttName)) {
            var Att = AttName;
            var preName = "Item." + Att;
            entityobj[preName] = rowItem[att];
        }
    }

    var params = {
        title: '【操作' + Title + '确认】',
        width: Width,
        message: strMess,
        onConfirm: function (popup) {
            //在后台删除行
            Framework.Ajax.PostJson(entityobj, url, function (results) {
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }
                alert("操作成功");
                //if (bRefresh)
                window.location.reload(true);
                //tr.remove();
                ////popup.find(".btn-default").trigger("click");
                ////countFn();
                //// connt.html(countFn());
                //if (fun)
                //    fun();
            });
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.Modal(params);
}


//单条记录删除UI
Framework.JsFunHandle.DeleteUI = function (currobj, bRefresh, params, fun) {// tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    //Framework.JsFunHandle.Delete = function (currobj, tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    //alert("sdsd");
    debugger;
    if (Framework.Tool.isUndefined(bRefresh))
        bRefresh = true;

    var areasRoute = params.areasRoute;
    var tbheadsobjs = params.tbhead;
    var map = params.rowFieldMap;
    var url = areasRoute + "/" + params.deleteUrl;
    var Title = params.modularName;
    var Width = params.popupwidth || 600;

    var tr = currobj.closest('tr');
    var entityobj = Framework.Tool.GetTbRowData(currobj, map);
    var rowItem = tr.data();

    var strMess = '';
    for (att in tbheadsobjs) {

        strMess += '<div class="form-group">';
        strMess += '<label class="col-md-2"></label>';
        strMess += '<label class="col-md-4 show-title"><label>' + tbheadsobjs[att] + '：</label></label>';
        strMess += '<div class="col-md-6">';
        strMess += rowItem[att];
        strMess += '</div>';
        strMess += '</div>';
    }
    strMess += ''

    var params = {
        title: '【删除' + Title + '确认】',
        width: Width,
        message: strMess,
        onConfirm: function (popup) {
            //在前台删除行
            tr.remove();
            popup.find(".btn-default").trigger("click");
            if (fun)
                fun();
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.Modal(params);
}

//带计算
Framework.JsFunHandle.DeleteUINew = function (currobj) {//, bRefresh, params) {// tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    var tr = currobj.closest('tr');
    var table = currobj.closest('table');
    var calcol = table.data("calcol");
    var tbheadsobjs = table.find("thead").data();
    var Title = table.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "";
    var Width = table.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(Width))
        Width = 600;
    var rowItem = tr.data();

    var strMess = '';
    for (att in tbheadsobjs) {
        strMess += '<div class="form-group">';
        strMess += '<label class="col-md-2"></label>';
        strMess += '<label class="col-md-4 show-title"><label>' + tbheadsobjs[att] + '：</label></label>';
        strMess += '<div class="col-md-6">';
        strMess += rowItem[att];
        strMess += '</div>';
        strMess += '</div>';
    }
    strMess += ''

    var params = {
        title: '【删除' + Title + '确认】',
        width: Width,
        message: strMess,
        onConfirm: function (popup) {
            //在前台删除行
            tr.remove();
            popup.find(".btn-default").trigger("click");

            //计算
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, table);

            //if (calcol.length > 0) {
            //    var arr1 = calcol.split(",");
            //    if (arr1.length) {
            //        for (var i = 0; arr1.length; i++) {
            //            var arr = arr1[i].split("|");
            //            //var table = obj.closest('table');
            //            var trs = table.find('.' + arr[0]);
            //            var sum = 0;
            //            var errorMessage = "";
            //            trs.each(function (i) {
            //                var val = $(this).val();//.val();
            //                //判断是否为数值
            //                val = Number(val);
            //                //price = Number(price);
            //                if (!Framework.Tool.isNumber(val)) {
            //                    errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
            //                    alert(errorMessage);
            //                }
            //                //if (!(val % 1 === 0)) {
            //                //    errorMessage += Framework.Const.ValidationMessageFormatConst.IntType();
            //                //    alert(errorMessage);
            //                //}
            //                if (errorMessage.length == 0) {
            //                    sum += val;
            //                    //sumPrice += val * price;
            //                }
            //            });
            //            if (arr.length < 3 || arr[2] == "3" || arr[2] == "1")//1：整数 3：小数 5：货币
            //                table.find("." + arr[0] + 'Total').html(sum);
            //            if (arr[2] == "5") {
            //                var valt = sum.toString().fmMoney(2);
            //                table.find("." + arr[0] + 'Total').html(valt);
            //            }
            //            //table.find("." + arr[0] + 'Total').html(sum);
            //        }
            //    }
            //}



        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.Modal(params);
}

//界面删除
$(document).delegate('.btn-DeleteUI', 'click', function (e) {
    var currobj = $(this);
    Framework.JsFunHandle.DeleteUINew(currobj);
});

//添加功能
//Framework.JsFunHandle.Add = function (data, modularName, width, url, RowTemp, RowTempMap, tbbody, urlTabRow, keyName, rowUpdateType) {
Framework.JsFunHandle.Add = function (data, rowUpdateType, params, fun) {// modularName, width, url, RowTemp, RowTempMap, tbbody, urlTabRow, keyName, rowUpdateType) {
    //必填：rowUpdateType,url,modularName,areasName,keyName,,tbbody
    var areasRoute = params.areasRoute || "";
    var modularName = params.modularName || "";
    var keyName = params.primaryKey || "";
    var url = areasRoute + "/" + (params.addUrl || "");
    var tbbody = params.tbbody || {};

    var width = params.popupwidth || 600;
    var urlTabRow = areasRoute + "/" + (params.tbrowUrl || "");
    var RowTemp = params.RowTemp || "";
    var RowTempMap = params.RowTempMap || new Hashtable();

    Framework.Ajax.GetView(data, url, function (result) {
        debugger;
        var params = {
            title: "添加" + modularName,
            width: width,
            message: result,
            onConfirm: function (popup, event) {
                Add(popup, tbbody, false);
            },
            onSaveClose: function (popup, event) {
                Add(popup, tbbody, true);
            }
        };
        Framework.UI.FormModalAdd(params);

        var Add = function Add(popup, tbbody, blcose) {
            if (!Framework.Form.Validates(popup))
                return;

            var data = Framework.Form.GetFormItemByObj(popup);
            Framework.Ajax.PostJson(data, url, function (results) {
                //错误处理
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }

                //行模板绑定
                if (rowUpdateType == 1) {
                    var temp = Framework.Tool.TmpBind(RowTemp, RowTempMap, results.Data.Item);
                    temp = $("<table>" + temp + "</table>");
                    tbbody.prepend(temp.find("tr"));
                    if (blcose) {
                        alert("添加成功");
                        popup.find(".btn-default").trigger("click");
                    }
                    else {
                        popup.success("添加成功");
                    }
                }
                else {
                    var data = results.Data;
                    var tabrowname = "Item." + keyName;
                    var datat = {};
                    datat[tabrowname] = data.Item[keyName];
                    //在列表中添加行
                    Framework.Ajax.GetView(datat, urlTabRow, function (results) {
                        tbbody.prepend($(results));
                        if (blcose) {
                            //关闭窗体
                            alert("添加成功");
                            popup.find(".btn-default").trigger("click");
                        }
                        else {
                            popup.success("添加成功");
                        }
                    });
                }
                if (fun)
                    fun(popup);
                //清空提示文字
                Framework.Form.reSet(popup);
            });
        }
    });
};

//编辑功能
Framework.JsFunHandle.Edit = function (obj, data, rowUpdateType, params, fun) {//obj, modularName, width, url,tmp,tmpMap,rowFieldMap,urlTabRow, keyName, rowUpdateType) {

    var areasRoute = params.areasRoute || "";
    var modularName = params.modularName || "";
    var keyName = params.primaryKey || "";

    var url = areasRoute + "/" + (params.editUrl || "");
    //var tbbody = params.tbbody || {};
    var rowFieldMap = params.rowFieldMap || new Hashtable();

    var width = params.popupwidth || 600;
    var urlTabRow = areasRoute + "/" + (params.tbrowUrl || "");
    var RowTemp = params.RowTemp || "";
    var RowTempMap = params.RowTempMap || new Hashtable();

    var tr = obj.closest('tr');

    var data = Framework.Tool.GetTbRowData(obj, rowFieldMap);

    Framework.Ajax.GetView(data, url, function (result) {
        ////错误判断
        //if (results.Data.RespAttachInfo.bError) {
        //    //显示错误
        //    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
        //    if (strerrors.length > 0)
        //        alert(strerrors);
        //    return;
        //}
        var params = {
            title: "编辑" + modularName,
            width: width,
            message: result,
            onConfirm: function (popup, event) {
                Edit(popup, false);
            }
        };
        Framework.UI.FormModalMy(params);

        ////////////////////////////////////////////////////
        var Edit = function (popup) {
            if (!Framework.Form.Validates(popup))
                return;
            var data = Framework.Form.GetFormItemByObj(popup);
            Framework.Ajax.PostJson(data, url, function (results) {
                //错误处理
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }
                //行模板绑定
                if (rowUpdateType == 1) {
                    //var temp = Framework.Tool.TmpBind(RowTemp, RowTempMap, results.Data.Item);
                    //temp = $("<table>" + temp + "</table>");
                    //tbbody.prepend(temp.find("tr"));
                    //if (blcose) {
                    //    alert("添加成功");
                    //    popup.find(".btn-default").trigger("click");
                    //}
                    //else {
                    //    popup.success("添加成功");
                    //}
                    /////////////////////更新到列表//////////////////////////////
                    var temp = Framework.Tool.TmpBind(RowTemp, RowTempMap, results.Data.Item);
                    temp = $("<table>" + temp + "</table>");
                    tr.replaceWith(temp.find("tr"));
                    alert("修改成功");
                    popup.find(".btn-default").trigger("click");
                }
                else {
                    var data = results.Data;
                    var tabrowname = "Item." + keyName;
                    var datat = {};
                    datat[tabrowname] = data.Item[keyName];
                    //alert(keyName);

                    //alert(data.Item[keyName]);

                    //alert(datat[tabrowname]);
                    //在列表中添加行
                    Framework.Ajax.GetView(datat, urlTabRow, function (results) {
                        //tbbody.prepend($(results));
                        tr.replaceWith(results);
                        alert("操作成功");
                        popup.find(".btn-default").trigger("click");
                    });
                }
                if (fun)
                    fun(popup);
            });
        };
    }
);
};


Framework.JsFunHandle.DetailPopup = function (obj, params) {
    var areasRoute = params.areasRoute || "";
    var modularName = params.modularName || "";

    var url = areasRoute + "/" + (params.detailUrl || "");
    //var tbbody = params.tbbody || {};
    var rowFieldMap = params.rowFieldMap || new Hashtable();

    var width = params.popupwidth || 600;
    //var urlTabRow = areasRoute + "/" + (params.tbrowUrl || "");
    //var RowTemp = params.RowTemp || "";
    //var RowTempMap = params.RowTempMap || new Hashtable();

    var tr = obj.closest('tr');

    var data = Framework.Tool.GetTbRowData(obj, rowFieldMap);

    Framework.Ajax.GetView(data, url, function (result) {
        //错误判断
        if (results.Data.RespAttachInfo.bError) {
            //显示错误
            var strerrors = Framework.UI.Behavior.ErrorHandling(results);
            if (strerrors.length > 0)
                alert(strerrors);
            return;
        }
        var paramsn = {
            title: "详情" + modularName,
            width: width,
            message: result,
            onConfirm: function (popup, event) {
                Edit(popup, false);
            }
        };
        Framework.UI.FormModalMy(paramsn);
    }
);
};

Framework.JsFunHandle.Detail = function (obj, params, fun) {
    var areasRoute = params.areasRoute || "";
    var modularName = params.modularName || "";

    var url = areasRoute + "/" + (params.detailUrl || "");
    //var tbbody = params.tbbody || {};
    var rowFieldMap = params.rowFieldMap || new Hashtable();

    var width = params.popupwidth || 600;
    var tr = obj.closest('tr');
    var data = Framework.Tool.GetTbRowData(obj, rowFieldMap);

    Framework.Ajax.GetView(data, url, function (result) {
        //错误判断
        if (results.Data.RespAttachInfo.bError) {
            //显示错误
            var strerrors = Framework.UI.Behavior.ErrorHandling(results);
            if (strerrors.length > 0)
                alert(strerrors);
            return;
        }
        if (fun)
            fun(result);
    }
);
};


/////////////////////////////////////////////////////
Import("Framework.Form");
//清空表单元素
Framework.Form.ClearForm = function (Jobj) {
    Jobj.find('input:text').each(function () {
        $(this).val("");
    });

    Jobj.find('textarea').each(function () {
        $(this).val();
    });

    Jobj.find('input:hidden').each(function () {
        $(this).val("");
    });

    Jobj.find('select').each(function () {
        //var item = $(this).val("");
    });

    Jobj.find('input:radio').each(function () {
        $(this).attr("checked", false);
    });

    Jobj.find('input:checkbox').each(function () {
        $(this).attr("checked", false);
    });
}

Framework.Form.reSet = function (Jobj) {
    var form = Jobj.find("form");
    if (form.length > 0)
        form[0].reset();
}

//从容器对象中获取值
Framework.Form.GetFormItemByObj = function (parentobj) {
    var obj = {};
    $(parentobj).find('input:text').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('input:password').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });


    $(parentobj).find('textarea').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('input:hidden').each(function () {
        var item = $(this);
        if (item[0].type == "hidden") {
            var key = item.attr("name");
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('input:radio:checked').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('select').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    //    var keycheckboxs = [];
    var ht = new Hashtable();
    //    ht.add(key, value);

    $(parentobj).find('input:checkbox:checked').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            var val = item.val();

            if (ht.contain(key)) {
                var num = ht.get(key) + 1;
                obj[key + "[" + num + "]"] = val;
                ht.remove(key);
                ht.add(key, num);
            }
            else {
                ht.add(key, 0);
                obj[key + "[0]"] = val;
            }
        }
        //if (keycheckboxs.contains(key)) {
        //    obj["Item." + key].push(val);
        //}
        //else {
        //    keycheckboxs.push(key);
        //    obj["Item." + key] = [];
        //    obj["Item." + key].push(val);
        //}
    });

    return obj;
}

Framework.Form.GetFormItemByObjNew = function (obj, parentobj) {
    //var obj = {};
    $(parentobj).find('input:text').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('input:password').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('textarea').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('input:hidden').each(function () {
        var item = $(this);
        if (item[0].type == "hidden") {
            var key = item.attr("name");
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('input:radio:checked').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    $(parentobj).find('select').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
    });

    ////    var keycheckboxs = [];
    //var ht = new Hashtable();
    ////    ht.add(key, value);
    //$(parentobj).find('input:checkbox:checked').each(function () {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    if (!Framework.Tool.isUndefined(key)) {
    //        var val = item.val();

    //        if (ht.contain(key)) {
    //            var num = ht.get(key) + 1;
    //            obj[key + "[" + num + "]"] = val;
    //            ht.remove(key);
    //            ht.add(key, num);
    //        }
    //        else {
    //            ht.add(key, 0);
    //            obj[key + "[0]"] = val;
    //        }
    //    }
    //});
    return obj;
}

Framework.Form.GetFormItemByArray = function (obj, parentobj, i, kn) {
    //var obj = {};
    var k2 = 10000;
    if (!Framework.Tool.isUndefined(kn))
        k2 = kn;
    $(parentobj).find('input:text').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('input:password').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });


    $(parentobj).find('textarea').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('input:hidden').each(function () {
        var item = $(this);
        if (item[0].type == "hidden") {
            var key = item.attr("name");
            if (!Framework.Tool.isUndefined(key)) {
                key = key.format(i, k2);
                var val = item.val();
                obj[key] = val;
            }
        }
    });

    $(parentobj).find('input:radio:checked').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('select').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });

    //    var keycheckboxs = [];
    var ht = new Hashtable();
    //    ht.add(key, value);

    $(parentobj).find('input:checkbox:checked').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = key.format(i, k2);
            var val = item.val();

            if (ht.contain(key)) {
                var num = ht.get(key) + 1;
                obj[key + "[" + num + "]"] = val;
                ht.remove(key);
                ht.add(key, num);
            }
            else {
                ht.add(key, 0);
                obj[key + "[0]"] = val;
            }
        }
        //if (keycheckboxs.contains(key)) {
        //    obj["Item." + key].push(val);
        //}
        //else {
        //    keycheckboxs.push(key);
        //    obj["Item." + key] = [];
        //    obj["Item." + key].push(val);
        //}
    });

    return obj;
}


Framework.Form.Validates = function (parentobj) {
    var pass = true;
    $(parentobj).find('input:text').each(function () {
        //$(this).css({ "border": "1px solid black" });
        //$(this).removeClass("validaterrorpro");
        //$(this).attr({ "title": "" });
        var obj = $(this);
        var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
        if (obj.hasClass("validaterrorpro")) {
            pass = false;
        }

        //if (!passtemp)
        //    pass = false;
    });

    $(parentobj).find('textarea').each(function () {
        //$(this).css({ "border": "1px solid black" });
        //obj.addClass("validaterrorpro");
        $(this).removeClass("validaterrorpro");
        $(this).attr({ "title": "" });
        var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
        if (!passtemp)
            pass = false;
    });

    //$(parentobj).find('input:hidden').each(function () {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    var val = item.val();
    //    obj[key] = val;
    //});

    //$(parentobj).find('input:radio:checked').each(function () {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    var val = item.val();
    //    obj[key] = val;
    //});

    //$(parentobj).find('select').each(function () {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    var val = item.val();
    //    obj[key] = val;
    //});

    ////    var keycheckboxs = [];
    //var ht = new Hashtable();
    ////    ht.add(key, value);

    //$(parentobj).find('input:checkbox:checked').each(function () {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    var val = item.val();

    //    if (ht.contain(key)) {
    //        var num = ht.get(key) + 1;
    //        obj[key + "[" + num + "]"] = val;
    //        ht.remove(key);
    //        ht.add(key, num);
    //    }
    //    else {
    //        ht.add(key, 0);
    //        obj[key + "[0]"] = val;
    //    }
    //    //if (keycheckboxs.contains(key)) {
    //    //    obj["Item." + key].push(val);
    //    //}
    //    //else {
    //    //    keycheckboxs.push(key);
    //    //    obj["Item." + key] = [];
    //    //    obj["Item." + key].push(val);
    //    //}
    //});

    return pass;
}

/////////////////////////////////////////////////////
Import("Framework.Tool");
//绑定数据行：获取Table的Tr元素上的数据
Framework.Tool.GetTbRowData = function (ele, ht) {
    debugger;
    var tr = ele.closest("tr");
    var obj = {};
    var objs = tr.data();
    for (att in objs) {
        if (ht.contain(att)) {
            var Att = ht.get(att);//获取字段名
            obj["Item." + Att] = objs[att];
        }
    }
    return obj;
}

//data-xx属性对应后台实体字段在table的tbody中
Framework.Tool.GetTbRowDataNew = function (ele) {
    debugger;
    var tr = ele.closest("tr");
    var tbody = ele.closest("tbody");
    var obj = {};
    var objs = tr.data();
    for (att in objs) {
        var AttName = tbody.data(att);
        if (!Framework.Tool.isUndefined(AttName)) {
            var Att = AttName;//获取字段名
            obj["Item." + Att] = objs[att];
        }
        //if (ht.contain(att)) {
        //    var Att = ht.get(att);//获取字段名
        //    obj["Item." + Att] = objs[att];
        //}
    }
    return obj;
}


//根据html模板，获取键名列表
Framework.Tool.TmpKeys = function (tmp) {
    var re = /{\w+}/gi;
    //模块的键值对
    var m = new Framework.Map();
    var mas = tmp.match(re);
    if (mas != null) {
        for (var k = 0; k < mas.length; k++) {
            var filed1 = mas[k];
            var filed2 = filed1.replace("{", "").replace("}", "").trim();
            m.put(filed2, filed1);
        }
    }
    return m;
}

//模板：绑定数据
Framework.Tool.TmpBind = function (tmp, tmpMap, Item) {
    var temp = tmp;
    tmpMap.each(function (key, value, index) {
        if (value != "") {
            var v = Item[key];
            temp = temp.replaceAll(value, v);
        }
    });
    return temp;
};


Framework.Map = function () {
    /** 存放键的数组(遍历用到) */
    this.keys = new Array();
    /** 存放数据 */
    this.data = new Object();

    /**   
            * 放入一个键值对   
            * @param {String} key   
        * @param {Object} value   
        */
    this.put = function (key, value) {
        if (this.data[key] == null) {
            this.keys.push(key);
        }
        this.data[key] = value;
    };

    /**   
      * 获取某键对应的值   
      * @param {String} key   
  * @return {Object} value   
  */
    this.get = function (key) {
        return this.data[key];
    };

    /**   
     * 删除一个键值对   
     * @param {String} key   
 */
    this.remove = function (key) {
        this.keys.remove(key);
        this.data[key] = null;
    };

    /**   
      * 遍历Map,执行处理函数   
      *    
      * @param {Function} 回调函数 function(key,value,index){..}   
     */
    this.each = function (fn) {
        if (typeof fn != 'function') {
            return;
        }
        var len = this.keys.length;
        for (var i = 0; i < len; i++) {
            var k = this.keys[i];
            fn(k, this.data[k], i);
        }
    };

    /**   
     * 获取键值数组(类似Java的entrySet())   
     * @return 键值对象{key,value}的数组   
 */
    this.entrys = function () {
        var len = this.keys.length;
        var entrys = new Array(len);
        for (var i = 0; i < len; i++) {
            entrys[i] = {
                key: this.keys[i],
                value: this.data[i]
            };
        }
        return entrys;
    };

    /**   
     * 判断Map是否为空   
     */
    this.isEmpty = function () {
        return this.keys.length == 0;
    };

    /**   
     * 获取键值对数量   
     */
    this.size = function () {
        return this.keys.length;
    };

    /**   
     * 重写toString    
     */
    this.toString = function () {
        var s = "{";
        for (var i = 0; i < this.keys.length; i++, s += ',') {
            var k = this.keys[i];
            s += k + "=" + this.data[k];
        }
        s += "}";
        return s;
    };
}

/////////////////////////////////////////////////////
Import("Framework.Ajax");
//异步-视图
Framework.Ajax.GetView = function (data, controlAndaction, successCallBack, errorCallBack) {
    var url = Framework.Page.BaseURL + controlAndaction;
    var request = $.ajax({
        url: url,//Framework.Page.BaseURL + controlAndaction,
        type: "GET",
        cache: false,
        data: data,
        async: true,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

//同步执行
Framework.Ajax.GetViewAsync = function (data, controlAndaction, successCallBack, errorCallBack) {
    var request = $.ajax({
        url: Framework.Page.BaseURL + controlAndaction,
        type: "GET",
        cache: false,
        data: data,
        async: false,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

Framework.Ajax.PostView = function (data, controlAndaction, successCallBack, errorCallBack) {
    var url = Framework.Page.BaseURL + controlAndaction;
    var request = $.ajax({
        url: url,//Framework.Page.BaseURL + controlAndaction,
        type: "POST",
        cache: false,
        data: data,
        async: true,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

//异步：Json数据
Framework.Ajax.GetJson = function (data, controlAndaction, successCallBack, errorCallBack) {
    var request = $.ajax({
        url: Framework.Page.BaseURL + controlAndaction,
        type: "GET",
        data: data,
        dataType: "json",
        async: true,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

Framework.Ajax.GetJsonAsync = function (data, controlAndaction, successCallBack, errorCallBack) {
    var request = $.ajax({
        url: Framework.Page.BaseURL + controlAndaction,
        type: "Get",
        data: data,
        dataType: "json",
        async: false,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

//异步：Json数据
Framework.Ajax.PostJson = function (data, controlAndaction, successCallBack, errorCallBack) {
    var request = $.ajax({
        url: Framework.Page.BaseURL + controlAndaction,
        type: "POST",
        data: data,
        dataType: "json",
        async: true,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

Framework.Ajax.PostJsonAsync = function (data, controlAndaction, successCallBack, errorCallBack) {
    var request = $.ajax({
        url: Framework.Page.BaseURL + controlAndaction,
        type: "POST",
        data: data,
        dataType: "json",
        async: false,
    }).done(function (result) {
        if (successCallBack) {
            successCallBack(result);
        }
    }).fail(function (jqXHR, textStatus) {
        if (errorCallBack) {
            errorCallBack(jqXHR);
        }
        else
            Framework.UI.HandleError(jqXHR, '');
    });
};

//////////////////////////////////////////////////////////////////

Import("Framework.Const.ValidationMessageFormatConst");
Import("Framework.Validate");

Framework.Validate.NullOrEmptyValidate = function (rightItem, obj, area) {
    var pass = true;
    for (var i = 0; i < rightItem.RuleRequireds.length; i++) {
        var field = rightItem.RuleRequireds[i];
        var fieldValidateRuleItem = Framework.Const.FieldValidateRule[field];
        if (obj[fieldValidateRuleItem.ValFieldName].trim().length == 0) {
            //var mess = Framework.Const.ValidationMessageFormatConst.NullOrEmpty(fieldValidateRuleItem.Title);
            var mess = "{0}：不能为空！".format(fieldValidateRuleItem.Title);
            $("#" + area + " #" + fieldValidateRuleItem.Field).css({ "border": "1px solid red" });
            $("#" + area + " #" + fieldValidateRuleItem.Field).attr({ "title": mess });
            pass = false;
            //editArea
        }
    }
    return pass;
    //    return "{0}：不能为空！".format(PropertyNameCn);
};

Framework.Validate.RuleDataTypeValidate = function (rightItem, obj, area) {

    for (var i = 0; i < rightItem.RuleDataTypes.length; i++) {
        var field = rightItem.RuleDataTypes[i];
        //从集合中查询此值
        var fieldValidateRuleItem = Framework.Const.FieldValidateRule[field];
        var Title = fieldValidateRuleItem.Title;
        var val = obj[fieldValidateRuleItem.ValFieldName];

        var Mess = "";
        var Min = null, Max = null;
        //获取数据类型，
        //如果数据为：整数、大整数、小数、日期，判断获取范围
        if (fieldValidateRuleItem.DataType == 5)//字符串
        {
            Min = fieldValidateRuleItem.MinLen, Max = fieldValidateRuleItem.MaxLen;
            val = String(val);
            //长度判断
            if (Min != null && Max != null) {
                if (val.length < Min && val.length >= Max) {
                    mess = Framework.Const.ValidationMessageFormatConst.RangLenMinMax(Title, Min, Max);
                }
            }
            else if (Min != null) {
                if (val.length < fieldValidateRuleItem.MinLen)
                    mess = Framework.Const.ValidationMessageFormatConst.RangLenMin(Title, Min);
            }
            else {
                if (val.length >= Max)
                    mess = Framework.Const.ValidationMessageFormatConst.RangLenMax(Title, Max);
            }

            //正则表达式
            var RegExpree = "";//正则表达式
            if (fieldValidateRuleItem.RegExpreeType != null) {
                if (fieldValidateRuleItem.RegExpreeType == 1) {
                    //RegExpree
                }
                else {

                }
                //正则表达式判断
            }
        }
        else {
            if (fieldValidateRuleItem.DataType == 4)//日期类型
            {
                //var reg = /^(d+)-(d{1,2})-(d{1,2}) (d{1,2}):(d{1,2}):(d{1,2})$/;
                //var r = str.match(reg);
                //if (r == null) return false;
                //r[2] = r[2] - 1;
                //var d = new Date(r[1], r[2], r[3], r[4], r[5], r[6]);
                //if (d.getFullYear() != r[1]) return false;
                //if (d.getMonth() != r[2]) return false;
                //if (d.getDate() != r[3]) return false;
                //if (d.getHours() != r[4]) return false;
                //if (d.getMinutes() != r[5]) return false;
                //if (d.getSeconds() != r[6]) return false;

                //var val = "2014-3-1 10:11:12";
                var r = /^\d{4}-\d{1,2}-\d{1,2}$/;
                if (r.test(val)) {
                    val = new Date(val.replace(/-/g, "/"));
                }
                else
                    mess = Title + "：不是有效的日期类型。";

                //var Min = "2014-1-1", Max = "2014-5-1";
                Min = fieldValidateRuleItem.MinLen, Max = fieldValidateRuleItem.MaxLen;
                if (r.test(Min) && r.test(Max)) {
                    //alert("不是日期类型");
                    Min = new Date(Min.replace(/-/g, "/"));
                    Max = new Date(Max.replace(/-/g, "/"));
                    if (val < min && val > max)
                        mess = Framework.Const.ValidationMessageFormatConst.RangMinMax(Title, Min, Max);
                }
                else if (r.test(Min)) {
                    Min = new Date(Min.replace(/-/g, "/"));
                    if (val < min)
                        mess = Framework.Const.ValidationMessageFormatConst.RangMin(Title, Min);
                }
                else {
                    Max = new Date(Min.replace(/-/g, "/"));
                    if (val > max)
                        mess = Framework.Const.ValidationMessageFormatConst.RangMax(Title, Max);
                }
            }
            else {
                var val = Number(val);
                if (!Framework.Tool.isNumber(val)) {
                    mess = $("#" + area + " #" + fieldValidateRuleItem.Field).css({ "border": "1px solid red" });
                    //continue;
                }
                else {
                    if (fieldValidateRuleItem.DataType == 1) {
                        Min = fieldValidateRuleItem.IntMin;
                        Max = fieldValidateRuleItem.IntMax;
                    }
                    else if (fieldValidateRuleItem.DataType == 2) {
                        Min = fieldValidateRuleItem.IntMin;
                        Max = fieldValidateRuleItem.IntMax;
                    } if (fieldValidateRuleItem.DataType == 3) {
                        Min = fieldValidateRuleItem.DecMin;
                        Max = fieldValidateRuleItem.DecMax;
                    }
                    if (Min != null)
                        Min = Number(Min);
                    if (Max != null)
                        Max = Number(Max);
                    if (Framework.Tool.isNumber(Min) && Framework.Tool.isNumber(Max) && val < Min && val > Max)
                        mess = Framework.Const.ValidationMessageFormatConst.RangMinMax(Title, Min, Max);
                    else if (Framework.Tool.isNumber(Min) && val < Min)
                        mess = Framework.Const.ValidationMessageFormatConst.RangMin(Title, Min);
                    else if (val > Max)
                        mess = Framework.Const.ValidationMessageFormatConst.RangMax(Title, Max);
                }
            }
        }
        if (Mess.length > 0) {
            $("#" + area + " #" + fieldValidateRuleItem.Field).css({ "border": "1px solid red" });
            $("#" + area + " #" + fieldValidateRuleItem.Field).attr({ "title": mess });
        }
    }
}

Framework.Validate.CompareValidates = function () {
    for (var i = 0; i < rightItem.CompareValidates.length; i++) {
        //判断比较类型类型
        var CompareValidateItem = rightItem.CompareValidates[i];
        if (CompareValidateItem.CurrField.trim().length == 0)//比较下一个验证项
            continue;

        //判断数据类型
        var CurrVal;
        if (CompareValidateItem.DataType == 1 || CompareValidateItem.DataType == 2 || CompareValidateItem.DataType == 3)//整数类型
        {
            CurrVal = Number(obj[CompareValidateItem.CurrField]);
            if (!Framework.Tool.isNumber(CurrVal)) {
                alert("xx数据类型不对");
                continue;
            }
        }
        if (CompareValidateItem.DataType == 4)//日期类型
        {
            var r = /^\d{4}-\d{1,2}-\d{1,2}$/;
            if (r.test(x.value)) {
                alert("不是日期类型");
                continue;
            }
            CurrVal = new Date(obj[CompareValidateItem.CurrField].replace(/-/g, "/"));
            if (Framework.Tool.isDate(CurrVal)) {
                alert("不是日期类型");
                continue;
            }
        }
        if (CompareValidateItem.DataType == 5)//字符串类型
        {
            CurrVal = String(OrgVal);
            if (!Framework.Tool.isStringType(CurrVal)) {
                alert("xx数据类型不对");
                continue;
            }
        }

        if (CompareValidateItem.ComparesType == 2 && CompareValidateItem.OrgField.trim().length == 0)//比较目标为空
            continue;
        //转换目标值
        var OrgVal = CompareValidateItem.Val;
        if (CompareValidateItem.ComparesType == 2)
            OrgVal = obj[CompareValidateItem.OrgField];

        //判断数据类型
        if (CompareValidateItem.DataType == 1 || CompareValidateItem.DataType == 2 || CompareValidateItem.DataType == 3)//整数类型
        {
            OrgVal = Number(OrgVal);
            if (!Framework.Tool.isNumber(OrgVal)) {
                alert("xx数据类型不对");
                continue;
            }
        }
        if (CompareValidateItem.DataType == 4)//日期类型
        {
            var r = /^\d{4}-\d{1,2}-\d{1,2}$/;
            if (r.test(x.value)) {
                alert("不是日期类型");
                continue;
            }
            OrgVal = new Date(OrgVal.replace(/-/g, "/"));
            if (Framework.Tool.isDate(OrgVal)) {
                alert("不是日期类型");
                continue;
            }
        }
        if (CompareValidateItem.DataType == 5)//字符串类型
        {
            OrgVal = String(OrgVal);
            if (!Framework.Tool.isStringType(OrgVal)) {
                alert("xx数据类型不对");
                continue;
            }
        }

        //进行比较
        if (CompareValidateItem.CompOperEn == "Equal") {
            if (CurrVal != OrgVal) {
                alert("xx应该等于yyy");
            }
        }
        else if (CompareValidateItem.CompOperEn == "NoEqual") {
            if (CurrVal == OrgVal) {
                alert("xx应该不等于yyy");
            }
        }
        else if (CompareValidateItem.CompOperEn == "Ge") {
            if (CurrVal > OrgVal) {
                alert("xx应该大于yyy");
            }
        }
    }

}

//Framework.Validate.RegexConst = (function () {
//    var m = new Hashtable();
//    m.add("email", /w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*/);
//    m.add("url", /[a-zA-z]+:\\[^s]*/);
//    m.add("post", /[1-9]d{5}(?!d)$/);
//    m.add("date", /^\d{4}-\d{1,2}-\d{1,2}$/);
//    m.add("telephone", /^13\d{9}$/);
//    m.add("mobilephone", /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/);
//    m.add("cardNo", /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/);
//    return m;
//})();

Framework.Validate.RegexConst = (function () {
    var m = new Hashtable();
    //    m.add("email",{regex: /w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*/,error:"邮箱：格式不正确。"});
    m.add("email", { regex: /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/, error: "邮箱的格式不正确。" });
    m.add("url", { regex: /[a-zA-z]+:\\[^s]*/, error: "url：格式不正确。" });
    m.add("post", { regex: /[1-9]d{5}(?!d)$/, error: "邮件：格式不正确。" });
    m.add("date", { regex: /^\d{4}-\d{1,2}-\d{1,2}$/, error: "日期：格式不正确。" });
    m.add("telephone", { regex: /^13\d{9}$/, error: "电话：格式不正确。" });
    m.add("AmountMoney", { regex: /^([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(.[0-9]{1,2})?$/, error: "金额：格式不正确。" });

    m.add("mobilephone", { regex: /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/, error: "手机：格式不正确。" });
    m.add("cardNo", { regex: /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/, error: "格式不正确。" });
    return m;
})();


Framework.Validate.RegexConst.ValidationMessages = (function () {
    var m = new Hashtable();
    //m.add("email", /w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*/);
    //m.add("url", /[a-zA-z]+:\\[^s]*/);
    //m.add("post", /[1-9]d{5}(?!d)$/);
    //m.add("date", /^\d{4}-\d{1,2}-\d{1,2}$/);
    m.add("telephone", "请输入正确的手机号码");
    m.add("mobilephone", "请输入正确的电话号码");
    m.add("cardNo", "请输入正确的身份证号码");
    return m;
})();


////////////////////为空错误提示/////////////////////////
Framework.Const.ValidationMessageFormatConst.NullOrEmpty = function () {
    return "不能为空！".format();
};

////////////////////数据类型错误提示///////////////////////
Framework.Const.ValidationMessageFormatConst.IntType = function () {
    return "请输入整数型数据！".format();
};

Framework.Const.ValidationMessageFormatConst.NumberType = function () {
    return "请输入数值型数据！".format();
};

Framework.Const.ValidationMessageFormatConst.DateType = function () {
    return "请输入日期型数据(格式：yyyy-MM-dd)".format();
};

Framework.Const.ValidationMessageFormatConst.TelephoneType = function () {
    return "请输入正确的手机号码".format();
};

Framework.Const.ValidationMessageFormatConst.MobilephoneType = function () {
    return "请输入正确的电话号码".format();
}
Framework.Const.ValidationMessageFormatConst.CardNoType = function () {
    return "请输入正确的身份证号码".format();
};

//比较时，被比较数据类型错误
Framework.Const.ValidationMessageFormatConst.NumberTypeComp = function (PropertyNameCn) {
    return "【{0}】的值应该为数值型数据！".format(PropertyNameCn);
};

Framework.Const.ValidationMessageFormatConst.DateTypeComp = function (PropertyNameCn) {
    return "【{0}】的值应该为日期型数据(格式：yyyy-MM-dd)".format(PropertyNameCn);
};

//////////////////长度判断错误提示/////////////////////////
Framework.Const.ValidationMessageFormatConst.RangLenMinMax = function (Min, Max) {
    return "长度必须大于【{0}】并且小于【{1}】".format(Min, Max);
};

Framework.Const.ValidationMessageFormatConst.RangLenMin = function (Min) {
    return "长度必须大于【{0}】".format(Min);
};

Framework.Const.ValidationMessageFormatConst.RangLenMax = function (Max) {
    return "取值范围必须小于【{0}】".format(Max);
};

//////////////大小比较判断错误提示//////////////////////////////
Framework.Const.ValidationMessageFormatConst.RangMinMax = function (Min, Max) {
    return "取值范围必须大于【{0}】并且小于【{1}】".format(Min, Max);
};

Framework.Const.ValidationMessageFormatConst.RangMin = function (Min) {
    return "取值范围必须大于【{0}】".format(Min);
};

Framework.Const.ValidationMessageFormatConst.RangMax = function (Max) {
    return "取值范围必须小于【{0}】".format(Max);
};

Framework.Const.ValidationMessageFormatConst.Equal = function (CompNameCn) {
    return "应该等于\"【{0}】\"".format(CompNameCn);
};

Framework.Const.ValidationMessageFormatConst.NotEqual = function (CompNameCn) {
    return "应该不等于\"【{0}】\"".format(CompNameCn);
};

Framework.Const.ValidationMessageFormatConst.Greater = function (CompNameCn) {
    return "应该大于\"【{0}】\"".format(CompNameCn);
};

Framework.Const.ValidationMessageFormatConst.GreaterEqual = function (CompNameCn) {
    return "应该大于等于\"【{0}】\"".format(CompNameCn);
};

Framework.Const.ValidationMessageFormatConst.Less = function (CompNameCn) {
    return "应该小于\"【{0}】\"".format(CompNameCn);
};

Framework.Const.ValidationMessageFormatConst.Less = function (CompNameCn) {
    return "应该小于\"【{0}】\"".format(CompNameCn);
};

Framework.Const.ValidationMessageFormatConst.LessEqual = function (CompNameCn) {
    return "应该小于等于\"【{0}】\"".format(CompNameCn);
};

////////////////////////////////////////////////////////////////////////////////////////////

Framework.Validate.StringValidate = function (obj) {
    var errorMessage = "";
    var datatype = $(obj).data("datatype");
    var rules = $(obj).data();
    var val = $(obj).val();

    if (datatype == "string")//字符串
    {
        val = String(val);
        for (rule in rules) {
            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
                var vals = rules[rule].toString().split("|");
                //var maxlenarr = ruleval.split('|');
                switch (rule) {
                    case "maxlen":
                        if (val.length > vals[0])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangLenMax(vals[0]);
                        break;
                    case "minlen":
                        if (val.length < vals[0])
                            //alert(vals[1]);
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangLenMin(vals[0]);
                        break;
                    case "len":
                        if (val.length < vals[0] || val.length > vals[1])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangLenMinMax(vals[0], vals[1]); //alert(vals[2]);
                        break;
                    case "regex":
                        //根据常量进行提示
                        //alert("正则表达式");
                        //1：正则表达式
                        //2：常量
                        if (Framework.Validate.RegexConst.contain(vals[0])) {
                            var reg = Framework.Validate.RegexConst.get(vals[0]);
                            if (!reg.regex.test(val.toString())) {
                                errorMessage += reg.error;// Framework.Validate.RegexConst.ValidationMessages.get(vals[0]);
                                return errorMessage;
                            }
                        }
                        break;
                    case "compare":
                        var OrgVal = vals[2];
                        var OrgValCn = "";
                        if (vals[0] == "2") {
                            OrgVal = $("#" + vals[2]).val();
                            OrgValCn = $("#" + vals[2]).data("fieldnamecn");
                        }
                        if (OrgVal == "")
                            return;
                        //获取中文提示
                        //var OrgValCn = $("#" + vals[2]).data("fieldnamecn");

                        switch (vals[1]) {
                            case "Equal":
                                if (val != OrgVal) {
                                    //alert("：应该等于" + vals[2]);
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Equal(OrgValCn);
                                }
                                break;
                            case "Greater":
                                if (val < OrgVal) {
                                    //alert("：应该大于" + vals[2]);
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Greater(OrgValCn);
                                }
                                break;
                            case "Less":
                                if (val > OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Less(OrgValCn);
                                }
                                break;

                        }
                        break;
                }
            }
        }
    }
    return errorMessage;
};

Framework.Validate.IntValidate = function (obj) {
    var errorMessage = "";
    var datatype = $(obj).data("datatype");
    var rules = $(obj).data();
    var val = $(obj).val();
    if (datatype == "int")//字符串
    {
        val = Number(val);
        if (!Framework.Tool.isNumber(val)) {
            errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
            return errorMessage;
        }
        if (!(val % 1 === 0)) {
            errorMessage += Framework.Const.ValidationMessageFormatConst.IntType();
            return errorMessage;
        }
        for (rule in rules) {
            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
                var vals = rules[rule].toString().split("|");
                //var maxlenarr = ruleval.split('|');
                switch (rule) {
                    case "maxval":
                        if (val > vals[0])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMax(vals[0]);
                        break;
                    case "minval":
                        if (val < vals[0])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMin(vals[0]);
                        break;
                    case "rangeval":
                        //var vals = rules[item].split("|")
                        if (val < vals[0] || val > vals[1])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMinMax(vals[0], vals[1]);
                        break;

                    case "compare":
                        var OrgVal = vals[2];
                        if (vals[0] == "2")//值比较
                            OrgVal = $("#" + vals[2]).val();
                        if (OrgVal == "")
                            return "";
                        //获取中文提示
                        var OrgValCn = $("#" + vals[2]).data("fieldnamecn");
                        OrgVal = Number(OrgVal);
                        if (!Framework.Tool.isNumber(OrgVal)) {
                            errorMessage += Framework.Const.ValidationMessageFormatConst.NumberTypeComp(OrgValCn);
                            //return;
                        }
                        switch (vals[1]) {
                            case "Equal":
                                if (val != OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Equal(OrgValCn);
                                    //alert("：应该等于" + vals[2]);
                                }
                                break;
                            case "Greater":
                                if (val < OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Greater(OrgValCn);
                                }
                                break;
                            case "Less":
                                if (val > OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Less(OrgValCn);
                                }
                                break;

                        }
                        break;
                }
            }
        }
    }
    return errorMessage;
};

Framework.Validate.DecValidate = function (obj) {
    var errorMessage = "";
    var datatype = $(obj).data("datatype");
    var rules = $(obj).data();
    var val = $(obj).val();
    if (datatype == "dec")//字符串
    {
        val = Number(val);
        if (!Framework.Tool.isNumber(val)) {
            errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
            return errorMessage;
        }
        for (rule in rules) {
            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
                var vals = rules[rule].toString().split("|");
                //var maxlenarr = ruleval.split('|');
                switch (rule) {
                    case "maxval":
                        if (val > vals[0])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMax(vals[0]);
                        break;
                    case "minval":
                        if (val < vals[0])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMin(vals[0]);
                        break;
                    case "rangeval":
                        //var vals = rules[item].split("|")
                        if (val < vals[0] || val > vals[1])
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMinMax(vals[0], vals[1]);
                        break;

                    case "compare":
                        var OrgVal = vals[2];
                        if (vals[0] == "2")//值比较
                            OrgVal = $("#" + vals[2]).val();
                        if (OrgVal == "")
                            return "";
                        //获取中文提示
                        var OrgValCn = $("#" + vals[2]).data("fieldnamecn");
                        OrgVal = Number(OrgVal);
                        if (!Framework.Tool.isNumber(OrgVal)) {
                            errorMessage += Framework.Const.ValidationMessageFormatConst.NumberTypeComp(OrgValCn);
                            //return;
                        }
                        switch (vals[1]) {
                            case "Equal":
                                if (val != OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Equal(OrgValCn);
                                    //alert("：应该等于" + vals[2]);
                                }
                                break;
                            case "Greater":
                                if (val < OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Greater(OrgValCn);
                                }
                                break;
                            case "Less":
                                if (val > OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Less(OrgValCn);
                                }
                                break;

                        }
                        break;
                }
            }
        }
    }
    return errorMessage;
};

Framework.Validate.DateValidate = function (obj) {
    var errorMessage = "";
    var datatype = $(obj).data("datatype");
    var rules = $(obj).data();
    var val = $(obj).val();
    var regexval = val;
    if (datatype == "date") {
        var r = /^\d{4}-\d{1,2}-\d{1,2}$/;
        if (r.test(val)) {
            val = new Date(val.replace(/-/g, "/"));
        }
        else {
            //mess = "不是有效的日期类型。";
            //alert("不是有效的日期类型！");
            errorMessage += Framework.Const.ValidationMessageFormatConst.DateType();
            return errorMessage;
        }

        for (rule in rules) {
            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
                var vals = rules[rule].toString().split("|");
                //var maxlenarr = ruleval.split('|');
                switch (rule) {
                    case "maxval":
                        var Max = new Date(vals[0].replace(/-/g, "/"));
                        if (val > Max)
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMax(vals[0]);
                        break;
                    case "minval":
                        var Min = new Date(vals[0].replace(/-/g, "/"));
                        if (val < Min)
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMin(vals[0]);
                        break;
                    case "rangeval":
                        var Min = new Date(vals[0].replace(/-/g, "/"));
                        var Max = new Date(vals[1].replace(/-/g, "/"));
                        if (val < Min || val > Max)
                            errorMessage += Framework.Const.ValidationMessageFormatConst.RangMinMax(vals[0], vals[1]);
                        break;
                    case "regex":
                        //根据常量进行提示
                        //alert("正则表达式");
                        if (Framework.Validate.RegexConst.contain(vals[0])) {
                            //var r = Framework.Validate.RegexConst.get(vals[0]);
                            //var r = /^\d{4}-\d{1,2}-\d{1,2}$/;
                            //if (!r.test(regexval.toString())) {
                            //    errorMessage += Framework.Const.ValidationMessageFormatConst.DateType();
                            //    return errorMessage;
                            //}
                            var reg = Framework.Validate.RegexConst.get(vals[0]);
                            if (!reg.regex.test(val.toString())) {
                                errorMessage += reg.error;// Framework.Validate.RegexConst.ValidationMessages.get(vals[0]);
                                return errorMessage;
                            }
                        }
                        break;
                    case "compare":
                        var OrgVal = vals[2];
                        if (vals[0] == "2")//值比较:compare=1|Greater|aa
                            OrgVal = $("#" + vals[2]).val();
                        if (OrgVal == "")
                            return "";
                        //OrgVal = OrgVal.replace(/-/g, "/");
                        var fieldnamecn = $("#" + vals[2]).data("fieldnamecn");
                        //alert(OrgVal);
                        if (r.test(OrgVal)) {
                            OrgVal = new Date(OrgVal.replace(/-/g, "/"));
                        }
                        else {
                            errorMessage += Framework.Const.ValidationMessageFormatConst.DateTypeComp(fieldnamecn);
                            return errorMessage;
                        }
                        switch (vals[1]) {
                            case "Equal":
                                if (val != OrgVal) {
                                    //alert("：应该等于" + vals[2]);
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Equal(fieldnamecn);
                                }
                                break;
                            case "Greater":
                                if (val < OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Greater(fieldnamecn);
                                    //alert("：应该大于" + vals[2]);

                                }
                                break;
                            case "Less":
                                if (val > OrgVal) {
                                    errorMessage += Framework.Const.ValidationMessageFormatConst.Less(fieldnamecn);
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
    return errorMessage;
};

//Framework.Validate.TelephoneType = function (obj) {
//    var errorMessage = "";
//    var datatype = $(obj).data("datatype");
//    var rules = $(obj).data();
//    var val = $(obj).val();
//    var regexval = val;
//    if (datatype == "telephone") {
//        var r = /^13\d{9}$/;
//        if (!r.test(val)) {
//            errorMessage += Framework.Const.ValidationMessageFormatConst.TelephoneType();
//            return errorMessage;
//        }
//        for (rule in rules) {
//            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
//                var vals = rules[rule].toString().split("|");
//                switch (rule) {
//                    case "regex":
//                        if (Framework.Validate.RegexConst.contain(vals[0])) {
//                            var r = /^13\d{9}$/;
//                            if (!r.test(regexval.toString())) {
//                                errorMessage += Framework.Const.ValidationMessageFormatConst.TelephoneType();
//                                return false;
//                            }
//                        }
//                        break;

//                }
//            }
//        }
//    }
//    return errorMessage;
//};

//Framework.Validate.MobilephoneType = function (obj) {
//    var errorMessage = "";
//    var datatype = $(obj).data("datatype");
//    var rules = $(obj).data();
//    var val = $(obj).val();
//    var regexval = val;
//    if (datatype == "mobilephone") {
//        var r = /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/;
//        if (!r.test(val)) {
//            errorMessage += Framework.Const.ValidationMessageFormatConst.MobilephoneType();
//            return errorMessage;
//        }
//        for (rule in rules) {
//            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
//                var vals = rules[rule].toString().split("|");
//                switch (rule) {
//                    case "regex":
//                        if (Framework.Validate.RegexConst.contain(vals[0])) {
//                            var r = /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/;
//                            if (!r.test(regexval.toString())) {
//                                errorMessage += Framework.Const.ValidationMessageFormatConst.MobilephoneType();
//                                return false;
//                            }
//                        }
//                        break;

//                }
//            }
//        }
//    }
//    return errorMessage;
//};

//Framework.Validate.CardNoType = function (obj) {
//    var errorMessage = "";
//    var datatype = $(obj).data("datatype");
//    var rules = $(obj).data();
//    var val = $(obj).val();
//    var regexval = val;
//    if (datatype == "cardNo") {
//        var r = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
//        if (!r.test(val)) {
//            errorMessage += Framework.Const.ValidationMessageFormatConst.CardNoType();
//            return errorMessage;
//        }
//        for (rule in rules) {
//            if (rule != "datatype" && rule != "required" && rule != "fieldnamecn") {
//                var vals = rules[rule].toString().split("|");
//                switch (rule) {
//                    case "regex":
//                        if (Framework.Validate.RegexConst.contain(vals[0])) {
//                            var r = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
//                            if (!r.test(regexval.toString())) {
//                                errorMessage += Framework.Const.ValidationMessageFormatConst.CardNoType();
//                                return false;
//                            }
//                        }
//                        break;

//                }
//            }
//        }
//    }
//    return errorMessage;
//};


//Framework.UI.Behavior
Framework.UI.Behavior.FormValidate = function () {
    $(document).delegate(":input:text", 'blur', function () {
        Framework.UI.Behavior.FormEleValidate($(this));
    });

    $(document).delegate(":input:text", 'focus', function () {
        //$(this).css({ "border": "1px solid black" });
        $(this).removeClass("validaterrorpro");
        //alert("ss");
        $(this).attr({ "title": "" });
        //alert("获取焦点");
    });

    $(document).delegate(":input:password", 'blur', function () {
        Framework.UI.Behavior.FormEleValidate($(this));
    });

    $(document).delegate(":input:password", 'focus', function () {
        //$(this).css({ "border": "1px solid black" });
        $(this).removeClass("validaterrorpro");
        $(this).attr({ "title": "" });
        //Framework.UI.Behavior.FormEleValidate($(this));
    });

};

Framework.UI.Behavior.ErrorHandling = function (results) {
    var strerrors = "";
    var Errors = results.Data.RespAttachInfo.ValidationErrors;
    if (!Framework.Tool.isUndefined(results.Data.RespAttachInfo.ValidationErrors.ErrorMessage)) {
        //alert(results.Data.RespAttachInfo.ValidationErrors.ErrorMessage);
        strerrors += results.Data.RespAttachInfo.ValidationErrors.ErrorMessage;
    }
    if (Errors.length > 0) {
        for (var i = 0; i < Errors.length; i++) {
            if (Errors[i].ValidationItemType == 0) {
                var obj = $("#Item_" + Errors[i].FieldName);
                obj.css({ "border": "1px solid red" });
                obj.attr({ "title": Errors[i].Message });
            }
            else
                strerrors += Errors[i].Message;
        }
    }
    return strerrors;
};

Framework.UI.Behavior.ErrorHandlingN = function (areaobj, results) {
    var strerrors = "";
    var Errors = results.Data.RespAttachInfo.ValidationErrors;
    if (!Framework.Tool.isUndefined(results.Data.RespAttachInfo.ValidationErrors.ErrorMessage))
        strerrors += results.Data.RespAttachInfo.ValidationErrors.ErrorMessage;
    if (Errors.length > 0) {
        for (var i = 0; i < Errors.length; i++) {
            if (Errors[i].ValidationItemType == 0) {
                var obj = areaobj.find("#Item_" + Errors[i].FieldName);
                obj.css({ "border": "1px solid red" });
                obj.attr({ "title": Errors[i].Message });
            }
            else
                strerrors += Errors[i].Message;
        }
    }
    return strerrors;
};

//验证方法
Framework.UI.Behavior.FormEleValidate = function (obj) {
    var errorMessage = "";
    var fieldnamecn = obj.data("fieldnamecn");
    var val = obj.val();
    if (Framework.Tool.isUndefined(obj.data("required")))//未定义
    {
        if (val.trim().length == 0)//未定义，长度为0
            return true;
    }
    else {
        if (val.trim().length == 0) {//不能为空验证
            errorMessage = Framework.Const.ValidationMessageFormatConst.NullOrEmpty(fieldnamecn);
            errorMessage = ("{0}：" + errorMessage).format(fieldnamecn);
            //obj.css({ "border": "1px solid red" });
            obj.addClass("validaterrorpro");
            obj.attr({ "title": errorMessage });
            //alert(errorMessage);
            obj.attr({ "alt": errorMessage });
            return false;
        }
    }

    errorMessage += Framework.Validate.IntValidate(obj);
    errorMessage += Framework.Validate.StringValidate(obj);
    errorMessage += Framework.Validate.DecValidate(obj);
    errorMessage += Framework.Validate.DateValidate(obj);

    //errorMessage += Framework.Validate.TelephoneType(obj);
    //errorMessage += Framework.Validate.CardNoType(obj);
    //errorMessage += Framework.Validate.MobilephoneType(obj);
    if (errorMessage.length > 0) {
        errorMessage = ("{0}：" + errorMessage).format(fieldnamecn);
        //obj.css({ "border": "1px solid red" });
        obj.addClass("validaterrorpro");
        obj.attr({ "title": errorMessage });
        obj.attr({ "alt": errorMessage });
        obj.attr({ "data-original-title": errorMessage });
    }
    return errorMessage.length == 0;
};

///////////////////////////////////////////////////////////////////////////////////////////
Import("Framework.Tool");

Framework.Tool.isNumber = function (val) {
    return typeof val === 'number' && isFinite(val);
    //return (typeof val == 'number') && val.constructor == Number;
};

Framework.Tool.isBooleanType = function (val) {
    return typeof val === "boolean";
}

Framework.Tool.isStringType = function (val) {
    return typeof val === "string";
}

Framework.Tool.isUndefined = function (val) {
    return typeof val === "undefined";
}

Framework.Tool.isObj = function (str) {
    if (str === null || typeof str === 'undefined') {
        return false;
    }

    //return typeof str === 'object';
    return (typeof str == 'object') && str.constructor == Object;
}

Framework.Tool.isNull = function (val) {
    return val === null;
}

Framework.Tool.isArray = function (arr) {
    return Object.prototype.toString.apply(arr) === '[object Array]';
}

Framework.Tool.isDate = function (obj) {
    return (typeof obj == 'object') && obj.constructor == Date;
}

Framework.Tool.isFunction = function (obj) {
    return (typeof obj == 'function') && obj.constructor == Function;
}

///////////////////分页////////////////////////////////////////////////////////////////////////

Import("Framework.Page");
//////////////////Goto：跳转页/////////////////////////////////////////
$(document).delegate('.pageindexgoto', 'click', function (e) {
    //内部可以查询到
    //alert();
    var obj = $(this);
    var pageArea = $(obj).closest("#pageArea");

    //Framework.Page.Helper(obj, '{ pageIndex: ' + pageArea.find(".gotoNumber").val() + ' }');
    Framework.Page.Helper(obj, { pageIndex: pageArea.find(".gotoNumber").val() });
});
/////////////////每页大小改变/////////////////////////////////////////
$(document).delegate('#PageSize', 'change.pagination', function (e) {
    //查找第一个分页查询区域
    //内部可以查找到
    //Framework.Page.Search({ PageSize: $(this).val() });
    //alert("sds");
    //alert("change.pagination");
    Framework.Page.Helper($(this), {});
});

/////////////////排序/////////////////////////////////////////////////
$(document).delegate('.fieldorderby', 'click', function (e) {
    //内部可以查找到
    var obj = $(this);
    var Property = $(obj).data("field");
    var RankInfo = Property + "|0";

    var objordercss = "";
    //if ($(obj).hasClass("ascwhite")) {
    //    objordercss = "ascwhite";
    //    RankInfo = Property + "|0";
    //}
    //else if ($(obj).hasClass("ascorange")) {
    //    objordercss = "ascorange";
    //    RankInfo = Property + "|0";
    //}
    if ($(obj).hasClass("ascorange")) {
        objordercss = "ascorange";
        RankInfo = Property + "|1";
    }
    //alert(RankInfo);
    //Framework.Page.Helper(obj, { pageIndex: pageArea.find(".gotoNumber").val() });

    //Framework.Page.Helper(obj, '{ RankInfo: "' + RankInfo + '" }');
    Framework.Page.Helper(obj, { RankInfo: RankInfo });
});

//////////////////切换页码--事件////////////////////////////////////////
$(document).delegate('.pageIndex', 'click', function (e) {
    //内部不能查询
    //Framework.Page.Helper($(this), '{ pageIndex: ' + $(this).data("val") + ' }')
    Framework.Page.Helper($(this), { pageIndex: $(this).data("val") })
});

Framework.Page.BulidUrl = function (areaobj, data, params, url, bformsubmit) {
    data["PageQueryBase.PageIndex"] = params.pageIndex || 1;
    var RankInfo = params.RankInfo || "";
    var bformsubmit = bformsubmit || 1;
    //(2)获取排序字段
    if (RankInfo.length == 0) {
        areaobj.find(".fieldorderby").each(function () {
            if (!$(this).hasClass("disabled")) {
                if ($(this).hasClass("ascorange")) {
                    Property = $(this).data("field");
                    data["PageQueryBase.RankInfo"] = Property + "|1";
                    return false;
                }
                else if ($(this).hasClass("descorange")) {
                    Property = $(this).data("field");
                    data["PageQueryBase.RankInfo"] = Property + "|0";
                    return false;
                }
            }
        });
    }
    else
        data["PageQueryBase.RankInfo"] = RankInfo;
    //(3)每页大小
    //获取分页
    data["PageQueryBase.PageSize"] = areaobj.find("#PageSize").val() || 10;

    //(5)生成url
    if (bformsubmit == 2)
        url = url + "?"
    else
        url = Framework.Page.BaseURL + url + "?";
    for (itemx in data) {
        if (!!data[itemx] && data[itemx].toString().length > 0)
            url += itemx + "=" + data[itemx] + "&";
    }
    return url;
}

Framework.Page.BulidSearchWhereData = function (areaobj, data, params, url, bformsubmit) {
    data["PageQueryBase.PageIndex"] = params.pageIndex || 1;
    var RankInfo = params.RankInfo || "";
    var bformsubmit = bformsubmit || 1;
    //(2)获取排序字段
    if (RankInfo.length == 0) {
        areaobj.find(".fieldorderby").each(function () {
            if (!$(this).hasClass("disabled")) {
                if ($(this).hasClass("ascorange")) {
                    Property = $(this).data("field");
                    data["PageQueryBase.RankInfo"] = Property + "|1";
                    return false;
                }
                else if ($(this).hasClass("descorange")) {
                    Property = $(this).data("field");
                    data["PageQueryBase.RankInfo"] = Property + "|0";
                    return false;
                }
            }
        });
    }
    else
        data["PageQueryBase.RankInfo"] = RankInfo;
    //(3)每页大小
    //获取分页
    data["PageQueryBase.PageSize"] = areaobj.find("#PageSize").val() || 10;

    //(5)生成url
    if (bformsubmit == 2)
        url = url + "?"
    else
        url = Framework.Page.BaseURL + url + "?";
    for (itemx in data) {
        if (!!data[itemx] && data[itemx].toString().length > 0)
            url += itemx + "=" + data[itemx] + "&";
    }
    return url;
}

//Framework.Page.BulidSearchWhereData = function (areaobj, data, params, url, bformsubmit) {
//    data["PageQueryBase.PageIndex"] = params.pageIndex || 1;
//    var RankInfo = params.RankInfo || "";
//    var bformsubmit = bformsubmit || 1;
//    //(2)获取排序字段
//    if (RankInfo.length == 0) {
//        areaobj.find(".fieldorderby").each(function () {
//            if (!$(this).hasClass("disabled")) {
//                if ($(this).hasClass("ascorange")) {
//                    Property = $(this).data("field");
//                    data["PageQueryBase.RankInfo"] = Property + "|1";
//                    return false;
//                }
//                else if ($(this).hasClass("descorange")) {
//                    Property = $(this).data("field");
//                    data["PageQueryBase.RankInfo"] = Property + "|0";
//                    return false;
//                }
//            }
//        });
//    }
//    else
//        data["PageQueryBase.RankInfo"] = RankInfo;
//    //(3)每页大小
//    //获取分页
//    data["PageQueryBase.PageSize"] = areaobj.find("#PageSize").val() || 10;
//}


Framework.Page.Helper = function (obj, val) {
    var pagedata = {};

    var pageArea = $(obj).closest("#pageArea");

    var pageAreamName = pageArea.data("pagearea");
    //var val=params.toString();
    //var funName = pageAreamName + '(' + val + ')';
    //eval(funName);
    //window['Framework']['SearchNew'](obj, 'aaaaaaaaaaaaaaaaaaaaa');
    var arrmethods = pageAreamName.split(".");
    if (arrmethods.length == 2)
        window[arrmethods[0]][arrmethods[1]](obj, val)
}

//Import("Framework.Search");
$(document).delegate('.Framework .btn-Search', 'click', function (e) {
    $("#searchType").val("0");
    Framework.Search({});
})

$(document).delegate('.Framework .btn-advSearch', 'click', function (e) {
    $("#searchType").val("1");
    Framework.Search({});
})

$(document).delegate('.btn-FwSearch', 'click', function (e) {
    var currobj = $(this);
    var pageArea = $(this).closest('#pageArea');
    pageArea.find("#searchType").val("0");
    //alert("区域查询");
    //Framework.Search(currobj, {});
    var pageAreamName = pageArea.data("pagearea");
    var arrmethods = pageAreamName.split(".");
    if (arrmethods.length == 2)
        window[arrmethods[0]][arrmethods[1]](currobj, {})
})

$(document).delegate('.btn-FwAdvSearch', 'click', function (e) {
    var currobj = $(this);
    var pageArea = $(this).closest('#pageArea');
    pageArea.find("#searchType").val(1);

    var pageAreamName = pageArea.data("pagearea");

    var arrmethods = pageAreamName.split(".");
    if (arrmethods.length == 2)
        window[arrmethods[0]][arrmethods[1]](currobj, {})

    //Framework.Search(currobj, {});
})

Framework.Search = function (currobj, params) {
    //alert("window调用");
    var pageArea = currobj.closest('#pageArea');
    var targetdom = pageArea.data("targetdom");
    var data = {};
    //(1)找到查询条件：快速查询、高级查询
    var searchType = pageArea.find("#searchType").val();
    var searchType = searchType || 0;

    if (searchType == 0)
        data = Framework.Form.GetFormItemByObj(pageArea.find("#fastSearchArea"));
    else
        data = Framework.Form.GetFormItemByObj(pageArea.find("#advSearchArea"));

    data["searchType"] = searchType;
    var url = pageArea.data("url");// initData.areasRoute + "/" + initData.indexUrl;
    var url = Framework.Page.BulidUrl(pageArea, data, params, url);
    if (Framework.Tool.isUndefined(targetdom)) {
        document.location.href = url;
    }
    else {
        url = url.substr(1, url.length);
        //Framework.Ajax.GetView({}, url, function (result) {
        //    $(targetdom).replaceWith(result);
        //    $(window).resize();
        //});
        Framework.Ajax.PostView({}, url, function (result) {
            $(targetdom).replaceWith(result);
            $(window).resize();
        });
    }
};

Framework.SearchStepless = function (currobj, params) {
    //alert("window调用");
    var pageArea = currobj.closest('#pageArea');
    var targetdom = pageArea.data("targetdom");
    var data = {};
    //(1)找到查询条件：快速查询、高级查询
    var results = Framework.Tool.GetSearchWhereTableColls(data, "#tabsearch")
    if (!results)
        return;

    var url = pageArea.data("url");// initData.areasRoute + "/" + initData.indexUrl;
    Framework.Page.BulidSearchWhereData(pageArea, data, params, url);
    //url = url.substr(1, url.length);
    //Framework.Ajax.GetView({}, url, function (result) {
    //    $(targetdom).replaceWith(result);
    //    $(window).resize();
    //});
    Framework.Ajax.PostView(data, url, function (result) {
        //$(targetdom).replaceWith(result);
        $(targetdom).html(result);
        $(window).resize();
    });
};

Framework.SearchSteplessN = function (currobj, params) {
    //alert("window调用");
    var pageArea = currobj.closest('#pageArea');
    var targetdom = pageArea.data("targetdom");
    var data = {};
    var row = 0;
    //(1)获取快速找到查询条件：快速查询、高级查询
    if (pageArea.find(".fastWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, pageArea.find(".fastWhere"), row);
    //(2)获取更多查询条件
    //row++;
    if (pageArea.find(".moreWhere"))
        row = Framework.Tool.GetQuerys(data, pageArea.find(".moreWhere"), row);
    //(3)获取自定义查询条件
    //row++;
    if (pageArea.find(".custWhere"))
        Framework.Tool.GetCustWhere(data, ".custWhere", row)

    var url = pageArea.data("url");// initData.areasRoute + "/" + initData.indexUrl;
    var bulidurl = Framework.Page.BulidSearchWhereData(pageArea, data, params, url, 1);

    if (Framework.Tool.isUndefined(targetdom)) {
        window.location.href = bulidurl;// Framework.Page.BaseURL + indexurl;// Framework.Page.BaseURL + areasname + '/C_OutboundOrder/Index';
    }
    else {
        Framework.Ajax.GetView(data, url, function (result) {//PostView
            //$(targetdom).replaceWith(result);
            $(targetdom).html(result);
            $(window).resize();
        });
    }
};

Framework.Tool.GetQuerys = function (obj, parentobj, row) {
    //var obj = {};
    //Querys[{0}].FieldName
    //var row = 0;
    $(parentobj).find('input:text').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
        obj["Querys[" + row + "].FieldName"] = key;
        obj["Querys[" + row + "].Value"] = val;
        row++;
    });

    $(parentobj).find('input:password').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
        obj["Querys[" + row + "].FieldName"] = key;
        obj["Querys[" + row + "].Value"] = val;
        row++;
    });

    $(parentobj).find('textarea').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
        obj["Querys[" + row + "].FieldName"] = key;
        obj["Querys[" + row + "].Value"] = val;
        row++;
    });

    $(parentobj).find('input:hidden').each(function () {
        var item = $(this);
        if (item[0].type == "hidden") {
            var key = item.attr("name");
            var val = item.val();
            obj[key] = val;
            obj["Querys[" + row + "].FieldName"] = key;
            obj["Querys[" + row + "].Value"] = val;
            row++;
        }
    });

    $(parentobj).find('input:radio:checked').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
        obj["Querys[" + row + "].FieldName"] = key;
        obj["Querys[" + row + "].Value"] = val;
        row++;
    });

    $(parentobj).find('select').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        obj[key] = val;
        obj["Querys[" + row + "].FieldName"] = key;
        obj["Querys[" + row + "].Value"] = val;
        row++;
    });
    return row;
    //return obj;
}

//获取表格中的数据
Framework.Tool.GetCustWhere = function (data, tbselect, row) {
    var obj = $(tbselect + ' tbody');// '#IndexPC_OutboundOrderDetail #tbbody';
    if (!Framework.Form.Validates(obj))
        return false;
    var table = $(tbselect);//"#tabsearch");
    var tbodyDom = table.find("tbody");
    var collsname = "Querys";// tbodyDom.data("collsname");

    $(tbselect + ' tbody>tr').each(function (i) {
        data[collsname + '[' + row + ']' + ".QuryType"] = 1;
        var tr = $(this);
        //var objs = $(this).data();
        //for (att in objs) {
        //    var AttName = tbodyDom.data(att);
        //    if (!Framework.Tool.isUndefined(AttName)) {
        //        var Att = AttName;
        //        var preName = collsname + '[' + row + ']' + "." + Att;
        //        data[preName] = objs[att];
        //    }
        //}
        //调取行绑定
        Framework.Form.GetFormItemByArray(data, tr, row);
        row++;
    });

    return true;
};

$(document).delegate('.custWhere .btn-FwCopy', 'click', function (e) {
    var currobj = $(this);
    //克隆tr
    var tr = $(this).closest('tr').clone();

    //Framework.Form.ClearForm(tr);
    //日期绑定
    tr.find('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
        $(this).datepicker('hide');
        //alert("日期改变事件");
        $(this).removeClass("validaterrorpro");
        $(this).attr({ "title": "" });
        //alert($(this).val());
        var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
    });

    tr.find(".btn-FwDeleteNotHint").show();
    //追加到后面
    currobj.closest('tr').after(tr);

    //$(window).resize();
});

$(document).delegate('.custWhere .btn-FwDeleteNotHint', 'click', function (e) {
    var currobj = $(this);
    ////////////////////////////////////////////////////////////////
    //alert("Copy");
    var currobj = $(this);
    var tr = currobj.closest('tr');
    tr.remove();
});

//Framework.Search = function (params) {
//    var data = {};
//    //(1)找到查询条件：快速查询、高级查询
//    var searchType = $("#searchType").val() || 0;

//    if (searchType == 0)
//        data = Framework.Form.GetFormItemByObj($("#fastSearchArea"));
//    else
//        data = Framework.Form.GetFormItemByObj($("#advSearchArea"));

//    data["searchType"] = searchType;
//    var url = $("#pageArea").data("url");// initData.areasRoute + "/" + initData.indexUrl;
//    var url = Framework.Page.BulidUrl($("#pageArea"), data, params, url);
//    //alert(url);
//    document.location.href = url;
//};

$(document).delegate('.btn-FwExcel', 'click', function (e) {
    var data = {};
    //(1)找到查询条件：快速查询、高级查询
    var searchType = $("#searchType").val() || 0;

    if (searchType == 0)
        data = Framework.Form.GetFormItemByObj($("#fastSearchArea"));
    else
        data = Framework.Form.GetFormItemByObj($("#advSearchArea"));

    data["searchType"] = searchType;
    var url = $("#pageArea").data("excelurl");// initData.areasRoute + "/" + initData.indexUrl;
    //var url = Framework.Page.BulidUrl($("#pageArea"), data, params, url);
    var url = Framework.Page.BulidUrl($("#pageArea"), data, {}, url);
    //alert(url);
    //document.location.href = url;
    window.open(url);
})

//货物类别改变
$(document).delegate('.Framework #Item_P_GoodCategoryID', 'change', function (e) {
    var pageArea = $(this).closest('#pageArea');
    var data = { "GoodCategoryID": $(this).val() }
    var url = "ProductAreas/P_ProductCategory/ByGoodCategoryID";
    Framework.Ajax.GetJson(data, url, function (results) {
        var data = results.Data;
        var strProductCategoryOption = "<option value=''>==请选择==</option>";
        for (var i = 0; i < data.length; i++) {
            strProductCategoryOption += "<option value=" + data[i].P_ProductCategoryID + " >" + data[i].ProductCategoryName + "</option>";
        }
        pageArea.find("#Item_P_ProductCategoryID").html(strProductCategoryOption);
    });
})




Framework.Tool.EnterToTab = function (e, obj, selectobj) {
    if (e.which == 13) {
        //var inputs = $("#IndexPPu_PurchaseOrderDetail #tbbody .OrderNum"); // 获取表单中的所有输入框  
        var idx = selectobj.index(obj); // 获取当前焦点输入框所处的位置  
        if (idx == selectobj.length - 1) // 判断是否是最后一个输入框  
        {
            selectobj[0].focus();
        } else {
            //idx++;
            var tempobj = {};
            while (true) {
                idx++;
                tempobj = selectobj[idx];
                if ($(tempobj).prop("disabled")) {
                    if (idx == selectobj.length - 1) {
                        idx = -1;
                    }
                    continue;
                }
                else {
                    tempobj.focus();
                    break;
                }
            }
            //$("#Item_ContractCode").focus()
            //$(selectobj[idx + 1]).focus().select()
            //selectobj[idx + 1].focus(); // 设置焦点  
            //selectobj[idx + 1].select(); // 选中文字  
        }
        return false;// alert();
    }
};

Framework.Tool.EnterToTabNew = function (e, obj) {
    if (e.which == 13) {
        //alert("EnterToTabNew");
        //var inputs = $("#IndexPPu_PurchaseOrderDetail #tbbody .OrderNum"); // 获取表单中的所有输入框  
        var selectobj = obj.closest('.editArea').find(":text,select");
        var idx = selectobj.index(obj); // 获取当前焦点输入框所处的位置  
        if (idx == selectobj.length - 1) // 判断是否是最后一个输入框  
        {
            selectobj[0].focus();
        } else {
            //idx++;
            var tempobj = {};
            while (true) {
                idx++;
                tempobj = selectobj[idx];
                if ($(tempobj).prop("disabled")) {
                    if (idx == selectobj.length - 1) {
                        idx = -1;
                    }
                    continue;
                }
                else {
                    tempobj.focus();
                    break;
                }
            }
            //$("#Item_ContractCode").focus()
            //$(selectobj[idx + 1]).focus().select()
            //selectobj[idx + 1].focus(); // 设置焦点  
            //selectobj[idx + 1].select(); // 选中文字  
        }
        return false;// alert();
    }
};

$(document).delegate('.editArea :text,select', 'keydown', function (e) {
    //alert("sdsdwe");
    Framework.Tool.EnterToTabNew(e, $(this));
});

Framework.Tool.TableToTab = function (e, obj, selectobj) {//, scrollobj) {
    if (e.which == 13) {
        //var inputs = $("#IndexPPu_PurchaseOrderDetail #tbbody .OrderNum"); // 获取表单中的所有输入框  
        var scrollobj = obj.closest('.scrollBar');
        //var selectobj = obj.closest('table');
        var idx = selectobj.index(obj); // 获取当前焦点输入框所处的位置  
        if (idx == selectobj.length - 1) // 判断是否是最后一个输入框  
        {
            selectobj[0].focus();
            if (scrollobj)
                scrollobj.scrollTop(0);
        } else {
            //idx++;
            var tempobj = {};
            while (true) {
                idx++;
                tempobj = selectobj[idx];
                if ($(tempobj).prop("disabled")) {
                    if (idx == selectobj.length - 1) {
                        idx = -1;
                    }
                    continue;
                }
                else {
                    tempobj.focus();
                    break;
                }
            }
            //$("#Item_ContractCode").focus()
            //$(selectobj[idx + 1]).focus().select()
            //selectobj[idx + 1].focus(); // 设置焦点  
            //selectobj[idx + 1].select(); // 选中文字  
        }
        return false;// alert();
    }
};

Framework.Tool.TableToTabNew = function (e, obj) {//, selectobj) {//, scrollobj) {
    if (e.which == 13) {
        //var inputs = $("#IndexPPu_PurchaseOrderDetail #tbbody .OrderNum"); // 获取表单中的所有输入框  

        var scrollobj = obj.closest('.scrollBarAuto');
        var tableDom = obj.closest('table');
        //var tabdirection = tableDom.data("tabdirection");

        var selectobj = {};
        var tabnextcol = obj.data("tabnextcol");
        if (Framework.Tool.isUndefined(tabnextcol)) {
            selectobj = tableDom.find(":text,select");
        }
        else {
            selectobj = tableDom.find("." + tabnextcol);
        }
        var idx = selectobj.index(obj); // 获取当前焦点输入框所处的位置  
        if (idx == selectobj.length - 1) // 判断是否是最后一个输入框  
        {
            selectobj[0].focus();
            if (scrollobj)
                scrollobj.scrollTop(0);
        } else {
            //idx++;
            var tempobj = {};
            while (true) {
                idx++;
                tempobj = selectobj[idx];
                if ($(tempobj).prop("disabled")) {
                    if (idx == selectobj.length - 1) {
                        idx = -1;
                    }
                    continue;
                }
                else {
                    tempobj.focus();
                    break;
                }
            }
            //$("#Item_ContractCode").focus()
            //$(selectobj[idx + 1]).focus().select()
            //selectobj[idx + 1].focus(); // 设置焦点  
            //selectobj[idx + 1].select(); // 选中文字  
        }
        return false;// alert();
    }
};

$(document).delegate('.FwtabletoTab input', 'keydown', function (e) {
    //var calcol = $(this).data("calcol").split("|")[0];
    Framework.Tool.TableToTabNew(e, $(this));//, $("#editListPC_OutboundOrderDetail #tbbody ." + calcol), $("#editListPC_OutboundOrderDetail  .mylist"));
});

$(document).delegate('.btn-FwPopup', 'click', function (e) {
    Framework.Tool.Popup($(this));
});

Framework.Tool.Popup = function (obj) {
    //var popupArea = obj.closest('.popupArea');
    var tableid = obj.data("tableid");
    var tabledom = $(tableid);
    var popupurl = tabledom.data("popupurl");
    var popupaddrepeat = tabledom.data("popupaddrepeat");
    var rowsurl = tabledom.data("rowsurl");
    var refdataarea = tabledom.data("refdataarea");
    //var targetdom = data("targetdom");
    var pk = tabledom.data("pk");
    var pklower = pk.toLowerCase();

    var Title = tabledom.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "选择商品";
    var width = tabledom.data("selectpopupwidth");
    if (Framework.Tool.isUndefined(width))
        width = 1000;
    //FunNameEn
    //var orgTbBodyID = tabledom.data("orgtbbodyid");
    /////////////////////////获取界面中：订单明细列表的产品列表信息////////////////
    var GetListEles = function (data) {
        if (!Framework.Tool.isUndefined(refdataarea)) {
            Framework.Form.GetFormItemByObjNew(data, $(refdataarea))
        }
        if (popupaddrepeat == 0) {
            var IDs = "";
            tabledom.find("tbody>tr").each(function (i) {
                debugger;
                IDs += $(this).data(pklower) + ',';
            });
            if (IDs.length > 0) {
                IDs = IDs.substr(0, IDs.length - 1);
                var pks = "Item." + pk + "s";
                data[pks] = IDs;
            }
        }
    };
    //////////////////////////弹窗:事件绑定/////////////////////////////////
    var EventBind = function (popup) {
        popup.find(".scrollBar").scroll(function () {
            var temp = $(this).scrollTop();
            $(this).find(".lockhead").css("top", temp - 1);
            $(this).find(".lockhead2").css("top", temp - 2);

        });

        ////物品类别改变后获取商品类别
        //popup.find("#Item_P_GoodCategoryID").change(function () {
        //    alert("#Item_P_GoodCategoryID");
        //    var data = { "GoodCategoryID": $(this).val() }
        //    var url = "ProductAreas/P_ProductCategory/ByGoodCategoryID";
        //    Framework.Ajax.GetJson(data, url, function (results) {
        //        var data = results.Data;
        //        var strProductCategoryOption = "<option value=''>==请选择==</option>";
        //        for (var i = 0; i < data.length; i++) {
        //            strProductCategoryOption += "<option value=" + data[i].P_ProductCategoryID + " >" + data[i].ProductCategoryName + "</option>";
        //        }
        //        popup.find("#Item_P_ProductCategoryID").html(strProductCategoryOption);
        //    });
        //});

        popup.find(".btn-Search").click(function () {
            //查询条件获取
            var data = Framework.Form.GetFormItemByObj(popup.find(".SearchAreaDetail"));
            GetListEles(data);
            //请求后台
            Framework.Ajax.GetView(data, popupurl, function (result) {
                popup.find("#popupContent").html(result);
                EventBind(popup);
            });
        });
        //alert();
        $(window).resize();
    };

    Framework.SearchPopup = function (currobj, params) {
        alert("Framework.SearchPopup ");
        var pageArea = currobj.closest('#pageArea');
        var targetdom = pageArea.data("targetdom");
        var data = {};
        var row = 0;
        //(1)获取快速找到查询条件：快速查询、高级查询
        if (pageArea.find(".fastWhere").length > 0)
            row = Framework.Tool.GetQuerys(data, pageArea.find(".fastWhere"), row);
        //(2)获取更多查询条件
        //row++;
        if (pageArea.find(".moreWhere"))
            row = Framework.Tool.GetQuerys(data, pageArea.find(".moreWhere"), row);
        //(3)获取自定义查询条件
        //row++;
        if (pageArea.find(".custWhere"))
            Framework.Tool.GetCustWhere(data, ".custWhere", row)
        GetListEles(data);
        var url = popupurl;
        var bulidurl = Framework.Page.BulidSearchWhereData(pageArea, data, params, url, 1);
        Framework.Ajax.GetView(data, url, function (result) {//PostView
            //$(targetdom).replaceWith(result);
            $(targetdom).html(result);
            $(window).resize();
        });
    };

    /////////////////////////选中商品后的保存方法/////////////////////////////
    var SelectProduct = function (popup) {
        //在popop中查找选中的数据
        var P_ProductIDs = "";
        var checkeds = popup.find('.jq-checkall-item:checked');
        if (checkeds.length == 0) {
            alert("请选择");
            return;
        }
        var IDs = "";
        checkeds.each(function (i) {
            var tr = $(this).closest('tr');
            IDs += tr.data(pklower) + ',';
        });
        IDs = IDs.substr(0, IDs.length - 1);
        //后台请求
        var idsName = "Item." + pk + 's';
        var data = {};
        //获取初始化数据
        if (!Framework.Tool.isUndefined(refdataarea)) {
            data = Framework.Form.GetFormItemByObj($(refdataarea));
        }

        data[idsName] = IDs;
        //alert(idsName+'  '+IDs);
        data["FunNameEn"] = $("#FunNameEn").val();

        Framework.Ajax.GetView(data, rowsurl, function (result) {
            tabledom.find("tbody").append(result);
            //$(orgTbBodyID).append(result);
            //search(popup);
            //删除界面元素
            if (popupaddrepeat == 0) {
                checkeds.each(function (i) {
                    var tr = $(this).closest('tr');
                    tr.remove();
                });
            }
            else {
                checkeds.each(function (i) {
                    if ($(this).prop("checked") == true) {
                        $(this).click();
                    }
                });
            }
            ///////////进行计算/////////////////////////////////////
            var calcol = tabledom.data("calcol");
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, tabledom);
            //日期绑定
            $('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
                //alert("日期改变事件");
                $(this).removeClass("validaterrorpro");
                $(this).attr({ "title": "" });
                //alert($(this).val());
                var objd = $(this);
                var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
                objd.blur();
                //alert();
            });
            $(window).resize();
        });
    };

    /////////////////////正常流程/////////////////////////////////////////
    debugger;
    var data = {};
    GetListEles(data);
    Framework.Ajax.GetView(data, popupurl, function (result) {
        //alert(result);
        var params = {
            title: Title,
            width: width,
            message: result,
            onConfirm: function (popup, event) {
                SelectProduct(popup);
            },
            onBack: function (popup) {
                EventBind(popup);
                //$(window).resize();
            }
        };
        //Framework.UI.FormModalMy(params);
        //$(window).resize();
        //alert("FormModalMySearchPopup");
        Framework.UI.FormModalMySearchPopup(params);
    }
);
};

$(document).delegate('.btn-FwPopupNew', 'click', function (e) {
    Framework.Tool.Popup($(this));
});

Framework.Tool.PopupNew = function (obj) {
    //var popupArea = obj.closest('.popupArea');
    var tableid = obj.data("tableid");
    var tabledom = $(tableid);
    var popupurl = tabledom.data("popupurl");
    var popupaddrepeat = tabledom.data("popupaddrepeat");
    var rowsurl = tabledom.data("rowsurl");
    var refdataarea = tabledom.data("refdataarea");
    //var targetdom = data("targetdom");
    var pk = tabledom.data("pk");
    var pklower = pk.toLowerCase();

    var Title = tabledom.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "选择商品";
    var width = tabledom.data("selectpopupwidth");
    if (Framework.Tool.isUndefined(width))
        width = 1000;
    //FunNameEn
    //var orgTbBodyID = tabledom.data("orgtbbodyid");
    /////////////////////////获取界面中：订单明细列表的产品列表信息////////////////
    var GetListEles = function (data) {
        if (!Framework.Tool.isUndefined(refdataarea)) {
            Framework.Form.GetFormItemByObjNew(data, $(refdataarea))
        }
        if (popupaddrepeat == 0) {
            //从表格数据
            var IDs = "";
            tabledom.find("tbody>tr").each(function (i) {
                debugger;
                IDs += $(this).data(pklower) + ',';
            });
            if (IDs.length > 0) {
                IDs = IDs.substr(0, IDs.length - 1);
                var pks = "Item." + pk + "s";
                data[pks] = IDs;
            }
        }
    };
    //////////////////////////弹窗:事件绑定/////////////////////////////////
    var EventBind = function (popup) {
        popup.find(".scrollBar").scroll(function () {
            var temp = $(this).scrollTop();
            $(this).find(".lockhead").css("top", temp - 1);
            $(this).find(".lockhead2").css("top", temp - 2);

        });

        ////物品类别改变后获取商品类别
        //popup.find("#Item_P_GoodCategoryID").change(function () {
        //    alert("#Item_P_GoodCategoryID");
        //    var data = { "GoodCategoryID": $(this).val() }
        //    var url = "ProductAreas/P_ProductCategory/ByGoodCategoryID";
        //    Framework.Ajax.GetJson(data, url, function (results) {
        //        var data = results.Data;
        //        var strProductCategoryOption = "<option value=''>==请选择==</option>";
        //        for (var i = 0; i < data.length; i++) {
        //            strProductCategoryOption += "<option value=" + data[i].P_ProductCategoryID + " >" + data[i].ProductCategoryName + "</option>";
        //        }
        //        popup.find("#Item_P_ProductCategoryID").html(strProductCategoryOption);
        //    });
        //});

        popup.find(".btn-Search").click(function () {
            //查询条件获取
            var data = Framework.Form.GetFormItemByObj(popup.find(".SearchAreaDetail"));
            GetListEles(data);
            //请求后台
            Framework.Ajax.GetView(data, popupurl, function (result) {
                popup.find("#popupContent").html(result);
                EventBind(popup);
            });
        });
        //alert();
        $(window).resize();
    };

    /////////////////////////选中商品后的保存方法/////////////////////////////
    var SelectProduct = function (popup) {
        //在popop中查找选中的数据
        var P_ProductIDs = "";
        var checkeds = popup.find('.jq-checkall-item:checked');
        if (checkeds.length == 0) {
            alert("请选择");
            return;
        }
        //var IDs = "";
        //checkeds.each(function (i) {
        //    //var tr = $(this).closest('tr');
        //    //IDs += tr.data(pklower) + ',';
        //});
        //IDs = IDs.substr(0, IDs.length - 1);
        //后台请求
        //var idsName = "Item." + pk + 's';
        var data = {};
        if (!Framework.Tool.isUndefined(refdataarea)) {
            var obj = $(refdataarea);
            if (!Framework.Form.Validates(obj))
                return;
            data = Framework.Form.GetFormItemByObjNew(data, $(refdataarea));
        }
        //////////////////子表区域数据获取////////////////////////////
        if (!Framework.Tool.isUndefined(childeditarea)) {
            var results = Framework.Tool.GetTableColls(data, childeditarea)
            if (!results)
                return;
        }
        Framework.Ajax.GetView(data, rowsurl, function (result) {
            tabledom.find("tbody").append(result);
            //删除界面元素
            if (popupaddrepeat == 0) {
                checkeds.each(function (i) {
                    var tr = $(this).closest('tr');
                    tr.remove();
                });
            }
            else {
                checkeds.each(function (i) {
                    if ($(this).prop("checked") == true) {
                        $(this).click();
                    }
                });
            }
            ///////////进行计算/////////////////////////////////////
            var calcol = tabledom.data("calcol");
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, tabledom);
            //日期绑定
            $('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
                $(this).datepicker('hide');
                //alert("日期改变事件");
                $(this).removeClass("validaterrorpro");
                $(this).attr({ "title": "" });
                //alert($(this).val());
                var objd = $(this);
                var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
                objd.blur();
            });
            $(window).resize();
        });
    };
    /////////////////////正常流程/////////////////////////////////////////
    debugger;
    var data = {};
    GetListEles(data);
    Framework.Ajax.GetView(data, popupurl, function (result) {
        //alert(result);
        var params = {
            title: Title,
            width: width,
            message: result,
            onConfirm: function (popup, event, currobj) {
                SelectProduct(popup, currobj);
            },
            onBack: function (popup) {
                EventBind(popup);
                //$(window).resize();
            }
        };
        Framework.UI.FormModalMySearchPopup(params);
    }
);
};

//下拉列表框改变事件
$(document).delegate('select', 'change', function (e) {
    var obj = $(this);
    var pkname = obj.data("pkname");
    if (Framework.Tool.isUndefined(pkname)) {
        pkname = obj.attr("name");
    }
    var url = obj.data("changeurl");
    if (Framework.Tool.isUndefined(url))
        return;
    var optionlabel = obj.data("optionlabel");
    //alert();
    var pageArea = obj.closest("#pageArea");
    if (pageArea.length == 0) {
        pageArea = obj.closest(".editArea");
    }
    var targetdomselects = obj.data("targetdom").split(",");

    var targetdomselect = targetdomselects[0];
    var targetdomselectTemp = targetdomselect.split("|");
    var targetdom = pageArea.find(targetdomselectTemp[0]);
    if (targetdom.is('input')) {
        //var key = obj.attr("name");
        var val = obj.val();
        var data = {}
        data[pkname] = val;
        Framework.Ajax.PostJson(data, url, function (results) {
            //var fiedname = targetdomselectTemp[1];
            var Item = results.Data.Item;
            //targetdom.val(Item[fiedname]);

            //.editPSa_SalesOrder #Item_IgneeName|DeliveryPerson,.editPSa_SalesOrder #Item_IgneeTele|Tele,.editPSa_SalesOrder #Item_ReceiptAddr|DeliveryAddress

            //$(".editPSa_SalesOrder #Item_IgneeName").val(Item.DeliveryPerson);
            //$(".editPSa_SalesOrder #Item_IgneeTele").val(Item.Tele);
            //$(".editPSa_SalesOrder #Item_ReceiptAddr").val(Item.DeliveryAddress);
            for (var i = 0; i < targetdomselects.length; i++) {
                targetdomselectTemp = targetdomselects[i].split("|");
                targetdom = pageArea.find(targetdomselectTemp[0]);
                targetdom.val(Item[targetdomselectTemp[1]]);
            }
        });
    }
    else if (targetdom.is('select')) {
        //alert("NNNNNN");
        var textfield = obj.data("textfield");
        var valuefield = obj.data("valuefield");

        var val = obj.val();
        var data = {}
        var key = obj.attr("name");
        data[key] = val;
        var keyarr = key.split("___")[0];
        if (keyarr.length > 1) {
            key = "Item." + key.split("___")[0];
            data[key] = val;
        }
        var strOptions = "";
        if (!Framework.Tool.isUndefined(optionlabel) && optionlabel != "") {
            strOptions = "<option value=''>==" + optionlabel + "==</option>";
        }
        if (val.length > 0) {
            //var url = "ProductAreas/P_ProductCategory/ByGoodCategoryID";
            Framework.Ajax.GetJson(data, url, function (results) {
                var data = results.Data;
                for (var i = 0; i < data.length; i++) {
                    strOptions += "<option value=" + data[i][valuefield] + " >" + data[i][textfield] + "</option>";
                }
                targetdom.html(strOptions);
                //popup.find("#Item_P_ProductCategoryID").html(strOptions);
            });
        }
        else {
            targetdom.html(strOptions);
        }
    }
});

//////////////////Start：自定义查询条件///////////////////////////////////////////////
Framework.Validate.QueryFieldNameConst = (function () {
    var m = new Hashtable();
    m.add("P_BrandID", { bDict: "0", pkname: "P_BrandID", url: "ProductAreas/P_Brand/GetAll", textfield: "BrandName", valuefield: "P_BrandID" });
    m.add("P_GoodCategoryID", { bDict: "0", pkname: "P_GoodCategoryID", url: "ProductAreas/P_GoodCategory/GetAll", textfield: "GoodCategoryName", valuefield: "P_GoodCategoryID" });
    m.add("P_ProductCategoryID", { bDict: "0", pkname: "P_ProductCategoryID", url: "ProductAreas/P_ProductCategory/GetAll", textfield: "ProductCategoryName", valuefield: "P_ProductCategoryID" });

    m.add("Ba_WarehouseID", { bDict: "0", pkname: "Ba_WarehouseID", url: "BasicInfoAreas/Ba_Warehouse/GetAll", textfield: "ReservoirAreaName", valuefield: "Ba_WarehouseID" });
    m.add("C_CustomerID", { bDict: "0", pkname: "C_CustomerID", url: "CustomerAreas/C_Customer/GetAll", textfield: "CompanyName", valuefield: "C_CustomerID" });

    m.add("StorageCategoryID", { bDict: "1", pkname: "Category", pkvalue: "StorageType", url: "Home/GetByCategory", textfield: "DText", valuefield: "DValue" });

    return m;
})();

//查询条件
$(document).delegate('.queryfieldname', 'change', function (e) {
    //alert("sdsdwe");
    //查找行
    var obj = $(this);
    var tr = obj.closest('tr');
    var targetdom = tr.find(".targetdom");//目标Dom

    var fieldname = obj.val();
    //判断是否需要下拉列表框
    var strresults = "<input type='text' class='form-control' name='Querys[{0}].Value' value='' />";

    if (!Framework.Validate.QueryFieldNameConst.contain(fieldname)) {
        targetdom.html(strresults);
        return;
    }
    var fieldnameItem = Framework.Validate.QueryFieldNameConst.get(fieldname);
    var bDict = fieldnameItem["bDict"];
    var pkname = fieldnameItem["pkname"];

    var url = fieldnameItem["url"];
    var textfield = fieldnameItem["textfield"];
    var valuefield = fieldnameItem["valuefield"];

    var data = {}
    if (bDict == "1") {
        var pkvalue = fieldnameItem["pkvalue"];
        data["Item.Category"] = pkvalue;
    }
    //var url = "ProductAreas/P_ProductCategory/ByGoodCategoryID";
    Framework.Ajax.GetJson(data, url, function (results) {
        var data = results.Data;
        var strOptions = "<select id='Querys_Value' name='Querys[{0}].Value' class='form-control'>";
        for (var i = 0; i < data.length; i++) {
            strOptions += "<option value=" + data[i][valuefield] + " >" + data[i][textfield] + "</option>";
        }
        strOptions += "</select>";
        targetdom.html(strOptions);
        //popup.find("#Item_P_ProductCategoryID").html(strOptions);
    });
});

///////////////////End：自定义查询条件//////////////////////////////////////////////

////计算列
//Framework.Tool.CalculationCol = function (obj) {
//    //alert();
//    var calcol = obj.data("calcol");
//    var table = obj.closest('table');
//    var arrcols = calcol.split(",");//计算列个数数组
//    for (var i = 0; i < arrcols.length; i++) {
//        var arr = arrcols[i].split("|");
//        var trs = table.find('.' + arr[0]);
//        var sum = 0;
//        var errorMessage = "";
//        //计算列
//        trs.each(function (i) {
//            var val = $(this).val();//.val();
//            //判断是否为数值
//            val = Number(val);
//            //price = Number(price);
//            if (!Framework.Tool.isNumber(val)) {
//                errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
//                alert(errorMessage);
//            }
//            //if (!(val % 1 === 0)) {
//            //    errorMessage += Framework.Const.ValidationMessageFormatConst.IntType();
//            //    alert(errorMessage);
//            //}
//            if (errorMessage.length == 0) {
//                sum += val;
//                //sumPrice += val * price;
//            }
//        });
//        if (arr.length < 3 || arr[2] == "3" || arr[2] == "1")//1：整数 3：小数 5：货币
//            table.find("." + arr[0] + 'Total').html(sum);
//        if (arr[2] == "5") {
//            var valt = sum.toString().fmMoney(2);
//            table.find("." + arr[0] + 'Total').html(valt);
//        }
//    }
//};

//计算列
Framework.Tool.CalculationColNew = function (calcol, tableDom) {
    //alert();
    //var calcol = obj.data("calcol");
    var table = tableDom;// obj.closest('table');
    var arrcols = calcol.split(",");//计算列个数数组
    for (var i = 0; i < arrcols.length; i++) {
        var arr = arrcols[i].split("|");
        var trs = table.find('.' + arr[0]);
        var sum = 0;
        var errorMessage = "";
        //计算列
        trs.each(function (i) {
            var val = $(this).val();//.val();
            //判断是否为数值
            val = Number(val);
            //price = Number(price);
            if (!Framework.Tool.isNumber(val)) {
                errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
                alert(errorMessage);
            }
            //if (!(val % 1 === 0)) {
            //    errorMessage += Framework.Const.ValidationMessageFormatConst.IntType();
            //    alert(errorMessage);
            //}
            if (errorMessage.length == 0) {
                sum += val;
                //sumPrice += val * price;
            }
        });
        //if (arr.length < 3 || arr[2] == "3" || arr[2] == "1")//1：整数 3：小数 5：货币
        //    table.find("." + arr[0] + 'Total').html(sum);
        if (arr[2] == "5") {
            sum = sum.toString().fmMoney(2);
            //table.find("." + arr[0] + 'Total').html(valt);
        }
        var domxx = table.find("." + arr[0] + 'Total');

        if (arr.length < 4 || arr[3] == "html")
            domxx.html(sum);
        else {
            domxx.val(sum);
            domxx.removeClass("validaterrorpro");
            domxx.attr({ "title": "" });
            var passtemp = Framework.UI.Behavior.FormEleValidate(domxx);
        }
    }
};


Framework.Tool.CalculationTableRow = function (currobj) {
    ////////////////////行计算:data-calrow="#Item_OrderNum|mul|#Item_Price|div|mmm $ #Item_PriceTotal|3|val&#PriceTotalFm|5|html"////////////////////////////////////
    var tr = currobj.closest('tr');
    var calrow = currobj.data("calrow");
    var arrcals = calrow.split(",");//计算表达式公式个数，可能一行有多个值需要计算
    for (var i = 0; i < arrcals.length; i++) {
        var arrcal = arrcals[0].split("$");//拆分出计算公式、目标值部分
        ////////////计算值//////////////////////////////////////////
        var calgs = arrcal[0].split("|");
        var calval = tr.find(calgs[0]).val();//获取第1个值
        for (var j = 2; j < calgs.length; j += 2) {
            var valn = tr.find(calgs[j]).val();
            if (calgs[j - 1] == "mul")//运算符
            {
                calval = calval * valn;
            }
        }
        ///////////设置或显示目标值:#Item_PriceTotal|3|val&#PriceTotalFm|5|html/////////////////////////////////////////
        var objgss = arrcal[1].split("&");
        for (var j = 0; j < objgss.length; j++)//设置或显示目标个数
        {
            var objgs = objgss[j].split("|");
            var objdom = tr.find(objgs[0]);
            if (objgs[1] == "3") {
                if (objgs[2] == "val")
                    objdom.val(calval);
            }
            else if (objgs[1] == "5") {
                var valt = calval.toString().fmMoney(2);
                if (objgs[2] == "val")
                    objdom.val(valt);
                else if (objgs[2] == "html")
                    objdom.html(valt);
            }
        }
    }
}

//基本数据处理
Framework.Tool.BlurHandleTableInput = function (currobj) {
    var tr = currobj.closest('tr');
    //(1)验证格式是否正确
    var val = currobj.val();
    //将值转换为小数值
    //val = val.replaceAll(",", "");
    ////////////////基本数据处理///////////////////////////////////
    var blurhandle = currobj.data("blurhandle");
    var arr1 = blurhandle.split(",");
    for (var i = 0; i < arr1.length; i++) {
        var arr2 = arr1[i].split("|");
        var tgdom = tr.find(arr2[0]);
        if (arr2[1] == 3) {
            if (arr2[2] == "val")
                tgdom.val(val);
        }
        else if (arr2[1] == 5) {
            //格式转换
            var valt = val.toString().fmMoney(2);
            if (arr2[2] == "val")
                tgdom.val(valt);
        }
    }
};

//文本框失去焦点，计算列
//table列计算
$(document).delegate('.Fwtablecalcol input', 'blur', function (e) {
    var currobj = $(this);
    //基本数据处理
    var blurhandle = currobj.data("blurhandle");
    if (!Framework.Tool.isUndefined(blurhandle))
        Framework.Tool.BlurHandleTableInput(currobj);

    //计算行
    var calrow = currobj.data("calrow");
    if (!Framework.Tool.isUndefined(calrow))
        Framework.Tool.CalculationTableRow(currobj);

    //计算列
    var calcol = currobj.data("calcol");
    if (!Framework.Tool.isUndefined(calcol)) {
        var tableDom = currobj.closest('table');
        Framework.Tool.CalculationColNew(calcol, tableDom);
        //Framework.Tool.CalculationCol(currobj);
    }
});

//获取表格中的数据
Framework.Tool.GetTableColls = function (data, tbselect) {
    var obj = $(tbselect + ' tbody');// '#IndexPC_OutboundOrderDetail #tbbody';
    if (!Framework.Form.Validates(obj))
        return false;
    var table = $(tbselect);
    var tbodyDom = table.find("tbody");
    var collsname = tbodyDom.data("collsname");

    $(tbselect + ' tbody>tr').each(function (i) {
        var tr = $(this);
        var objs = $(this).data();
        for (att in objs) {
            var AttName = tbodyDom.data(att);
            if (!Framework.Tool.isUndefined(AttName)) {
                var Att = AttName;
                var preName = "Item." + collsname + '[' + i + ']' + "." + Att;
                data[preName] = objs[att];
            }
        }
        //调取行绑定
        Framework.Form.GetFormItemByArray(data, tr, i);
    });

    return true;
};

//主从表保存
Framework.Tool.MasterChildSave = function (btn) {
    var posturl = btn.data("posturl");
    var indexurl = btn.data("indexurl");
    var masteditarea = btn.data("masteditarea");
    var childeditarea = btn.data("childeditarea");

    var obj = $(masteditarea);
    if (!Framework.Form.Validates(obj))
        return;

    var data = Framework.Form.GetFormItemByObj($(masteditarea));
    //var areasname = initData.areasRoute;
    var results = Framework.Tool.GetTableColls(data, childeditarea)
    if (!results)
        return;
    btn.addClass("disabled");

    Framework.Ajax.PostJson(data, posturl, function (results) {
        btn.removeClass("disabled");

        if (results.Data.RespAttachInfo.bError) {
            //显示错误
            var strerrors = Framework.UI.Behavior.ErrorHandling(results);
            if (strerrors.length > 0)
                alert(strerrors);
            return;
        }
        var data = results.Data;
        alert('保存成功');
        window.location.href = Framework.Page.BaseURL + indexurl;// Framework.Page.BaseURL + areasname + '/C_OutboundOrder/Index';
    })
}

//使用Framework的主从表保存
$(document).delegate('.btn-FwSave', 'click', function (e) {
    var btn = $(this);
    Framework.Tool.MasterChildSave(btn);
});

//主表--行单击事件后的方法
Framework.Tool.MasterTabTrClick = function (trobj) {
    var tableDom = trobj.closest("table");
    var pk = tableDom.data("pk");
    var funnameen = tableDom.data("funnameen");
    var pklower = pk.toLowerCase();
    var childtaburl = tableDom.data("childtaburl");
    var childtabselect = tableDom.data("childtabselect");

    var data = {};
    var ID = trobj.data(pklower);
    data["Item." + pk] = ID;
    data["FunNameEn"] = funnameen;//针对主表有多个功能
    Framework.Ajax.GetView(data, childtaburl, function (results) {
        $(childtabselect).html(results);
        tableDom.find("tr").removeClass("tbrow-ighlight");
        trobj.addClass("tbrow-ighlight");

        $(".scrollBar").scroll(function () {
            var temp = $(this).scrollTop();
            $(this).find(".lockhead").css({ top: temp - 1 });
            $(this).find(".lockhead2").css("top", temp - 2);
        });
    });
};

Framework.Tool.FwBtnSubmit = function (btn) {
    var posturl = btn.data("posturl");
    var indexurl = btn.data("indexurl");
    var indexurlpkb = btn.data("indexurlpkb");//是否要更新主键
    var indexurlpk = btn.data("indexurlpk");
    var configinfo = btn.data("configinfo");//确认提示
    var pk = btn.data("pk");
    var pkval = btn.data("pkval");
    var masteditarea = btn.data("masteditarea");
    var childeditarea = btn.data("childeditarea");
    var data = {};

    //////////////////获取主键////////////////////////
    if (!Framework.Tool.isUndefined(pkval)) {
        data["Item." + pk] = pkval;
    }
    //alert("sd1");

    //////////////////区域数据获取/////////////////////////////////
    if (!Framework.Tool.isUndefined(masteditarea)) {
        var obj = $(masteditarea);
        if (!Framework.Form.Validates(obj))
            return;
        data = Framework.Form.GetFormItemByObjNew(data, $(masteditarea));
    }
    //var areasname = initData.areasRoute;
    //////////////////子表区域数据获取////////////////////////////
    if (!Framework.Tool.isUndefined(childeditarea)) {
        var results = Framework.Tool.GetTableColls(data, childeditarea)
        if (!results)
            return;
    }

    btn.addClass("disabled");
    //alert("sd2");
    //return;
    if (Framework.Tool.isUndefined(configinfo)) {//不进行操作提示
        //alert("不提示");
        Framework.Ajax.PostJson(data, posturl, function (results) {
            btn.removeClass("disabled");
            if (results.Data.RespAttachInfo.bError) {
                //显示错误
                var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                if (strerrors.length > 0)
                    alert(strerrors);
                return;
            }
            var data = results.Data;
            alert('保存成功');

            if (Framework.Tool.isUndefined(indexurl))
                window.location.reload();
            else {
                if (Framework.Tool.isUndefined(indexurlpkb) || indexurlpkb == "0")
                    indexurlpk = "";
                else {
                    if (Framework.Tool.isUndefined(indexurlpk)) {
                        indexurlpk = "?Item." + pk + "=" + data.Item[pk];
                    }
                    else
                        indexurlpk = "?Item." + indexurlpk + "=" + data.Item[indexurlpk];
                }
                window.location.href = Framework.Page.BaseURL + indexurl + indexurlpk;
            }
        })
    }
    else {
        var params = {
            title: '【操作确认】',
            width: 400,
            message: configinfo + "，你确定要执行操作吗？",
            onConfirm: function (popup) {
                //alert("提示");
                //return;
                Framework.Ajax.PostJson(data, posturl, function (results) {
                    btn.removeClass("disabled");
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            alert(strerrors);
                        return;
                    }
                    var data = results.Data;
                    alert('保存成功');

                    if (Framework.Tool.isUndefined(indexurl))
                        window.location.reload();
                    else {
                        if (Framework.Tool.isUndefined(indexurlpkb) || indexurlpkb == "0")
                            indexurlpk = "";
                        else {
                            if (Framework.Tool.isUndefined(indexurlpk)) {
                                indexurlpk = "?Item." + pk + "=" + data.Item[pk];
                            }
                            else
                                indexurlpk = "?Item." + indexurlpk + "=" + data.Item[indexurlpk];
                        }
                        window.location.href = Framework.Page.BaseURL + indexurl + indexurlpk;
                    }
                })
            },
            onCancel: function (popup) {
                //alert("sdsd取消");
                btn.removeClass("disabled");
            }
        };
        Framework.UI.Modal(params);
    }
}

//提交处理
$(document).delegate('.btn-FwBtnSubmit', 'click', function (e) {
    var btn = $(this);
    //alert("FwBtnSubmit");
    Framework.Tool.FwBtnSubmit(btn);
});

Framework.Tool.FwBtnSubmitNew = function (btn) {
    var OperParamDom = btn.closest(".OperArea").find(".OperParam");
    var configinfo = OperParamDom.data("configinfo");//提交确认提示

    var posturl = OperParamDom.data("posturl");
    var targeturl = OperParamDom.data("targeturl");//提交后目标url
    var targeturlparamname = OperParamDom.data("targeturlparamname");//提交后目标url的参数名

    //var indexurlpkb = OperParamDom.data("indexurlpkb");//是否要更新主键
    //var indexurlpk = OperParamDom.data("indexurlpk");
    var pk = OperParamDom.data("pkname");
    var pkval = OperParamDom.data("pkval");
    var masteditarea = OperParamDom.data("masteditarea");
    var childeditarea = OperParamDom.data("childtableselect");
    var data = {};

    //////////////////获取主键////////////////////////
    if (!Framework.Tool.isUndefined(pkval)) {
        data["Item." + pk] = pkval;
    }
    //alert("sd1");

    //////////////////区域数据获取/////////////////////////////////
    if (!Framework.Tool.isUndefined(masteditarea)) {
        var obj = $(masteditarea);
        if (!Framework.Form.Validates(obj))
            return;
        data = Framework.Form.GetFormItemByObjNew(data, $(masteditarea));
    }
    //var areasname = initData.areasRoute;
    //////////////////子表区域数据获取////////////////////////////
    if (!Framework.Tool.isUndefined(childeditarea)) {
        var results = Framework.Tool.GetTableColls(data, childeditarea)
        if (!results)
            return;
    }

    btn.addClass("disabled");
    //alert("sd2");
    //return;
    if (Framework.Tool.isUndefined(configinfo)) {//不进行操作提示
        //alert("不提示");
        Framework.Ajax.PostJson(data, posturl, function (results) {
            btn.removeClass("disabled");
            if (results.Data.RespAttachInfo.bError) {
                //显示错误
                var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                if (strerrors.length > 0)
                    alert(strerrors);
                return;
            }
            var data = results.Data;
            alert('操作成功');
            //targeturl  
            if (Framework.Tool.isUndefined(targeturl))//无indexurl重新加载页面
                window.location.reload();
            else {//根据请求刷新页面
                var targeturlparam = "";
                if (Framework.Tool.isUndefined(targeturlparamname))
                    targeturlparam = "";
                else {
                    targeturlparam = "?Item." + targeturlparamname + "=" + data.Item[targeturlparamname];
                }
                window.location.href = Framework.Page.BaseURL + targeturl + targeturlparam;
            }
        })
    }
    else {
        var params = {
            title: '【操作确认】',
            width: 400,
            message: configinfo + "，你确定要执行操作吗？",
            onConfirm: function (popup) {
                //alert("提示");
                //return;
                Framework.Ajax.PostJson(data, posturl, function (results) {
                    btn.removeClass("disabled");
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            alert(strerrors);
                        return;
                    }
                    var data = results.Data;
                    alert('保存成功');

                    if (Framework.Tool.isUndefined(targeturl))//无indexurl重新加载页面
                        window.location.reload();
                    else {//根据请求刷新页面
                        var targeturlparam = "";
                        if (Framework.Tool.isUndefined(targeturlparamname))
                            targeturlparam = "";
                        else {
                            targeturlparam = "?Item." + targeturlparamname + "=" + data.Item[targeturlparamname];
                        }
                        window.location.href = Framework.Page.BaseURL + targeturl + targeturlparam;
                    }
                })
            },
            onCancel: function (popup) {
                //alert("sdsd取消");
                btn.removeClass("disabled");
            }
        };
        Framework.UI.Modal(params);
    }
}

//提交处理
$(document).delegate('.btn-FwBtnSubmitNew', 'click', function (e) {
    var btn = $(this);
    //alert("FwBtnSubmit");
    Framework.Tool.FwBtnSubmitNew(btn);
});

//主表--行单击
$(document).delegate('.MasterTab  tbody  > tr', 'click', function (e) {
    Framework.Tool.MasterTabTrClick($(this));
});


//主表--行单击，阻止事件冒泡
$(document).delegate('.btn-xs', 'click', function (e) {
    e.stopPropagation();
})

//处理货币时的抽象
$(document).delegate("table input", "focus", function (e) {
    //alert("focus");
    var currobj = $(this);
    var tr = $(this).closest('tr');
    var focushandle = currobj.data("focushandle");
    if (!Framework.Tool.isUndefined(focushandle)) {
        var arr = focushandle.split("|");
        var sourceDom = tr.find(arr[0]);
        var val = sourceDom.val();
        if (arr[2] == "3") {
            currobj.val(val);
        }
    }
});

$(document).delegate(".btn-FwImport", "click", function (e) {
    //alert("focus");
    var currobj = $(this);
    var from = $(this).closest('form');
    from.submit();
});

////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
//添加窗体
Framework.JsFunHandle.AddPopup = function (currObj) {// modularName, width, url, RowTemp, RowTempMap, tbbody, urlTabRow, keyName, rowUpdateType) {
    //必填：rowUpdateType,url,modularName,areasName,keyName,,tbbody
    var tableselect = currObj.data("tableselect");//Table选择器
    var tableDom = $(tableselect);//Table的Dom对象

    var modularName = tableDom.data("modularname");
    var addurl = tableDom.data("addurl");
    var rowurl = tableDom.data("rowurl");
    var refdataarea = tableDom.data("refdataarea");
    var width = tableDom.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(width))
        width = 600;
    var keyName = tableDom.data("pk");
    //var keyName = params.primaryKey || "";
    var tbbody = tableDom.find("tbody");
    //var width = params.popupwidth || 600;

    var data = {};
    if (!Framework.Tool.isUndefined(refdataarea)) {
        Framework.Form.GetFormItemByObjNew(data, $(refdataarea))
    }

    Framework.Ajax.GetView(data, addurl, function (result) {
        debugger;
        var params = {
            title: "添加" + modularName,
            width: width,
            message: result,
            onConfirm: function (popup, event) {
                Add(popup, tbbody, false);
            },
            onSaveClose: function (popup, event) {
                Add(popup, tbbody, true);
            }
        };
        Framework.UI.FormModalAdd(params);

        var Add = function Add(popup, tbbody, blcose) {
            if (!Framework.Form.Validates(popup))
                return;

            var data = Framework.Form.GetFormItemByObj(popup);
            Framework.Ajax.PostJson(data, addurl, function (results) {
                //错误处理
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }

                //行模板绑定
                var data = results.Data;
                var tabrowname = "Item." + keyName;
                var datat = {};
                datat[tabrowname] = data.Item[keyName];
                //在列表中添加行
                Framework.Ajax.GetView(datat, rowurl, function (results) {
                    tbbody.prepend($(results));
                    if (blcose) {
                        //关闭窗体
                        alert("添加成功");
                        popup.find(".btn-default").trigger("click");
                    }
                    else {
                        popup.success("添加成功");
                    }
                    $(window).resize();
                });
                //if (fun)
                //    fun(popup);
                //清空提示文字
                Framework.Form.reSet(popup);
            });
        }
    });
};

$(document).delegate('.btn-FwAddPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.JsFunHandle.AddPopup(currobj);
});

//编辑窗体
Framework.JsFunHandle.EditPopupNew = function (obj) {//obj, modularName, width, url,tmp,tmpMap,rowFieldMap,urlTabRow, keyName, rowUpdateType) {

    //var tableselect = currObj.data("tableselect");//Table选择器
    var tableDom = obj.closest('table');//Table的Dom对象
    var tr = obj.closest('tr');

    var modularName = tableDom.data("modularname");
    var editurl = tableDom.data("editurl");
    var rowurl = tableDom.data("rowurl");

    var width = tableDom.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(width))
        width = 600;
    var keyName = tableDom.data("pk");
    //var keyName = params.primaryKey || "";
    var tbbody = tableDom.find("tbody");

    ////////////////////
    //var areasRoute = params.areasRoute || "";
    //var modularName = params.modularName || "";
    //var keyName = params.primaryKey || "";

    //var url = areasRoute + "/" + (params.editUrl || "");
    //var tbbody = params.tbbody || {};
    //var rowFieldMap = params.rowFieldMap || new Hashtable();

    //var width = params.popupwidth || 600;
    //var urlTabRow = areasRoute + "/" + (params.tbrowUrl || "");
    //var RowTemp = params.RowTemp || "";
    //var RowTempMap = params.RowTempMap || new Hashtable();

    var data = Framework.Tool.GetTbRowDataNew(obj);

    Framework.Ajax.GetView(data, editurl, function (result) {
        ////错误判断
        var params = {
            title: "编辑" + modularName,
            width: width,
            message: result,
            onConfirm: function (popup, event) {
                Edit(popup, false);
            }
        };
        Framework.UI.FormModalMy(params);

        ////////////////////////////////////////////////////
        var Edit = function (popup) {
            if (!Framework.Form.Validates(popup))
                return;
            var data = Framework.Form.GetFormItemByObj(popup);
            Framework.Ajax.PostJson(data, editurl, function (results) {
                //错误处理
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }
                //行模板绑定
                //var data = results.Data;
                //var tabrowname = "Item." + keyName;
                //var datat = {};
                //datat[tabrowname] = data.Item[keyName];
                ////在列表中添加行
                Framework.Ajax.GetView(data, rowurl, function (results) {
                    //tbbody.prepend($(results));
                    tr.replaceWith(results);
                    alert("操作成功");
                    popup.find(".btn-default").trigger("click");
                });
                //if (fun)
                //    fun(popup);
            });
        };
    }
);
};

$(document).delegate('.btn-FwEditPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.JsFunHandle.EditPopupNew(currobj);
});

//详情
Framework.JsFunHandle.DetailPopup = function (obj, params) {
    var tableDom = obj.closest('table');//Table的Dom对象
    var tr = obj.closest('tr');

    var modularName = tableDom.data("modularname");
    var detailurl = tableDom.data("detailurl");
    //var rowurl = tableDom.data("rowurl");

    var width = tableDom.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(width))
        width = 600;
    var keyName = tableDom.data("pk");
    //var keyName = params.primaryKey || "";
    var tbodyDom = tableDom.find("tbody");
    ///////////////////////////////////////////////////////////////
    //var areasRoute = params.areasRoute || "";
    //var modularName = params.modularName || "";

    //var url = areasRoute + "/" + (params.detailUrl || "");
    ////var tbbody = params.tbbody || {};
    //var rowFieldMap = params.rowFieldMap || new Hashtable();

    //var width = params.popupwidth || 600;
    //var urlTabRow = areasRoute + "/" + (params.tbrowUrl || "");
    //var RowTemp = params.RowTemp || "";
    //var RowTempMap = params.RowTempMap || new Hashtable();

    var tr = obj.closest('tr');

    var data = {};// Framework.Tool.GetTbRowData(obj, rowFieldMap);
    //var tbodyDom = tableDom.find("tbody");
    var rowItem = tr.data();
    for (att in rowItem) {
        var AttName = tbodyDom.data(att);
        if (!Framework.Tool.isUndefined(AttName)) {
            var Att = AttName;
            var preName = "Item." + Att;
            data[preName] = rowItem[att];
        }
    }


    Framework.Ajax.GetView(data, detailurl, function (results) {
        //错误判断
        //if (results.Data.RespAttachInfo.bError) {
        //    //显示错误
        //    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
        //    if (strerrors.length > 0)
        //        alert(strerrors);
        //    return;
        //}
        var paramsn = {
            title: "详情" + modularName,
            width: width,
            message: results,
            onConfirm: function (popup, event) {
                //Edit(popup, false);
                //alert("关闭");
                //popup.find(".btn-default").trigger("click");
            }
        };
        Framework.UI.FormModalMy(paramsn);
    }
);
};

$(document).delegate('.btn-FwDetailPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.JsFunHandle.DetailPopup(currobj);
});

//确认窗体
Framework.JsFunHandle.FwConfirmPopup = function (currobj) {//, bRefresh, params) {// tbheadsobjs, map, url, bRefresh, Title, Width, fun) {
    var funmark = currobj.data("funmark");
    var tr = currobj.closest('tr');
    var tableDom = currobj.closest('table');

    //////////////////////////////////////////////////
    //data-deletefun="2" data-deleteurl="sss/aaa/www"
    var fun = tableDom.data(funmark + "fun");
    var funurl = tableDom.data(funmark + "url");
    var modularName = tableDom.data("modularname");
    //var fun = tableDom.data("fun");
    var posturl = tableDom.data(fun);////////////////???????
    var rowurl = tableDom.data("rowurl");
    /////////////////////////////////////////////////

    var calcol = tableDom.data("calcol");
    var Title = tableDom.data("popuptitle");
    //var url = table.data("deleteurl");
    var keyName = tableDom.data("pk");
    if (Framework.Tool.isUndefined(Title))
        Title = "";
    var Width = tableDom.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(Width))
        Width = 600;
    //var entityobj = Framework.Tool.GetTbRowData(currobj, map);

    var rowItem = tr.data();
    var tbheadsobjs = tableDom.find("thead").data();

    var strMess = '';
    for (att in tbheadsobjs) {
        strMess += '<div class="form-group">';
        strMess += '<label class="col-md-2"></label>';
        strMess += '<label class="col-md-4 show-title"><label>' + tbheadsobjs[att] + '：</label></label>';
        strMess += '<div class="col-md-6">';
        strMess += rowItem[att];
        strMess += '</div>';
        strMess += '</div>';
    }
    strMess += ''

    ////////////////////14号：新国展/////////////////////////////////////////
    var entityobj = {};
    var tbodyDom = tableDom.find("tbody");
    for (att in rowItem) {
        var AttName = tbodyDom.data(att);
        if (!Framework.Tool.isUndefined(AttName)) {
            var Att = AttName;
            var preName = "Item." + Att;
            entityobj[preName] = rowItem[att];
        }
    }

    var params = {
        title: '【操作' + Title + '确认】',
        width: Width,
        message: strMess,
        onConfirm: function (popup) {
            // fun  funurl
            var funarr = fun.toString().split("|");// fun.split("|");
            if (funarr[0] == 2 || funarr.length == 1) {
                tr.remove();
                popup.find(".btn-default").trigger("click");
            }
            else {
                //在后台删除行
                Framework.Ajax.PostJson(entityobj, posturl, function (results) {
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            popup.fail(strerrors);
                        return;
                    }
                    if (funarr.length == 2) {//替换当前行
                        Framework.Ajax.GetView(entityobj, rowurl, function (results) {
                            //tbbody.prepend($(results));
                            tr.replaceWith(results);
                            popup.find(".btn-default").trigger("click");
                        });
                    }
                    else if (funarr[2] == "0") {//删除当前行不刷新
                        tr.remove();
                        popup.find(".btn-default").trigger("click");
                        //计算
                        if (!Framework.Tool.isUndefined(calcol))
                            Framework.Tool.CalculationColNew(calcol, tableDom);
                    }
                    else {//删除当前行刷新
                        window.location.reload(true);
                    }
                    alert("操作成功");
                });
            }
            //计算
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, tableDom);
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.Modal(params);
}

//弹窗确认--单行，带一些信息特征数据，从tr中获取
$(document).delegate('.btn-FwConfirmPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.JsFunHandle.FwConfirmPopup(currobj);
});

//弹窗确认--批量，批量删除、批量审核、批量启动、批量停止
Framework.JsFunHandle.FwBatchConfirmPopup = function (currobj) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {
    var funmark = currobj.data("funmark");

    var table = $(currobj.data("tableid"));
    var calcol = table.data("calcol");
    var checkeds = table.find(".jq-checkall-item:checked");
    var tbheadsobjs = table.find("thead").data();
    var url = table.data("deleteurl");
    var Title = table.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "";


    var fun = table.data(funmark + "fun");
    var funurl = table.data(funmark + "url");
    var modularName = table.data("modularname");
    //var fun = tableDom.data("fun");
    var rowurl = table.data("rowurl");


    var strMess = '<table class="table table-condensed table-bordered" >';
    strMess += '<thead>';
    strMess += '<tr>';
    //不符合条件
    var strerrmessage = '以下为不符合条件的记录：';
    strerrmessage += '<table class="table table-condensed table-bordered" ><thead><tr>';

    for (att in tbheadsobjs) {
        strMess += '<td>' + tbheadsobjs[att] + '</td>';
        strerrmessage += '<td>' + tbheadsobjs[att] + '</td>';
    }
    strMess += '</tr>';
    strMess += '</thead>';
    strMess += '<tbody>';
    strerrmessage += "</tr></thead><tbody>";

    ////////////////////////////////////////////////////////////////
    //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
    var strErrMess = "";
    checkeds.each(function () {
        //执行回调：是否满足条件的
        var tr = $(this).closest('tr');
        strMess += '<tr>';
        var rowItem = tr.data();
        for (att in tbheadsobjs) {
            strMess += '<td>' + rowItem[att] + '</td>'
        }
        strMess += '<tr>';
    });
    strMess += '</tbody>';
    strMess += '</table>';
    //strMess += "</div>";
    if (strErrMess.length > 0) {
        strerrmessage += strErrMess;
        strerrmessage += '</tbody>';
        strerrmessage += '</table>';
        //strerrmessage += "</div>";
        strMess += strerrmessage;
    }
    var funarr = fun.toString().split("|");

    var paramsn = {
        title: '【批量删除' + Title + '确认】',
        message: strMess,
        onConfirm: function (popup) {
            var opterSussecRows = [];
            var opterErrorRows = [];
            //////////执行操作/////////////////
            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
            var tbodyDom = table.find("tbody");
            checkeds.each(function () {
                var tr = $(this).closest('tr');
                if (funarr[0] == 2 || funarr.length == 1) {
                    //alert("sddcxxc")
                    tr.remove();
                }
                else {
                    var rowItem = tr.data();
                    var entityobj = {};
                    for (att in rowItem) {
                        var AttName = tbodyDom.data(att);
                        if (!Framework.Tool.isUndefined(AttName)) {
                            var Att = AttName;
                            var preName = "Item." + Att;
                            entityobj[preName] = rowItem[att];
                        }
                    }
                    Framework.Ajax.PostJson(entityobj, url, function (results) {
                        if (results.Data.RespAttachInfo.bError) {
                            //显示错误
                            var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                            if (strerrors.length > 0)
                                popup.fail(strerrors);
                            return;
                        }
                        if (funarr.length == 2) {//替换当前行
                            Framework.Ajax.GetView(entityobj, rowurl, function (results) {
                                //tbbody.prepend($(results));
                                tr.replaceWith(results);
                                //popup.find(".btn-default").trigger("click");
                            });
                        }
                        else if (funarr[2] == "0") {//删除当前行不刷新
                            tr.remove();
                        }
                        else {//删除当前行刷新
                            //window.location.reload(true);
                        }
                        //alert("操作成功");
                    });
                }
            });
            popup.find(".btn-default").trigger("click");
            //计算
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, table);

            if (funarr.length == 3 && funarr[2] == 1) {
                window.location.reload(true);
            }
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.ModalBatch(paramsn);
};

$(document).delegate('.btn-FwBatchConfirmPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.JsFunHandle.FwBatchConfirmPopup(currobj);
});

$(document).ready(
    function () {
        Framework.UI.DisableModalLoopForIE8();

        Framework.UI.Behavior.MenuSet();
        Framework.UI.Behavior.Collapse();
        Framework.UI.Behavior.Pagination();
        Framework.UI.SetShow();
        Framework.UI.Behavior.CheckAll();
        Framework.UI.Behavior.Doc();
        Framework.UI.Behavior.addClassSpan();
        Framework.UI.Behavior.JQVerify();
        Framework.UI.Validation();
        Framework.UI.Placeholder();

        Framework.UI.Behavior.FormValidate();
        ////format: 'MM/dd/yyyy hh:mm'  yyyy-MM-dd
        $('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
            $(this).datepicker('hide');
            //alert("日期改变事件");
            $(this).removeClass("validaterrorpro");
            $(this).attr({ "title": "" });
            //alert($(this).val());
            var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
        });

        //$('.datepicker').datepicker({ format: "yyyy-MM-dd" });

        $(".scrollBar").scroll(function () {
            var temp = $(this).scrollTop();
            $(this).find(".lockhead").css("top", temp - 1);
            $(this).find(".lockhead2").css("top", temp - 2);
        });

        $(window).resize(function () {
            //var _addHeight = $(window).height() - $("body").outerHeight(true);
            //var EleouteHeight = $(".scrollBar").outerHeight(true);
            //var _height = $(".scrollBar").height();// _jahDivs.height();
            //$(".scrollBar").height(_height + _addHeight - (EleouteHeight - _height) / 2);

            //高度自适应
            //$(".scrollBarAuto").each(function () {
            //    var thisDom = $(this);
            //    var pageDom = $(".page-bar");
            //    var pagetop = 0;
            //    var pageheight = 0
            //    //if (pageDom.length > 0) {
            //    //    pagetop = $(".page-bar").offset().top;
            //    //    pageheight = $(".page-bar").outerHeight(true);
            //    //}
            //    //var scrollBartop = $(".scrollBar").offset().top;
            //    //var scrollBarheight = $("#tbList").outerHeight(true);
            //    //var winheight = $(window).height();
            //    //var docheight = $(document).height();
            //    //var height = winheight - scrollBartop - pageheight - 40;
            //    //if (scrollBarheight < height)
            //    //    height = scrollBarheight;
            //    //$(".scrollBar").css("height", height)
            //    /////////////////////////
            //    var scrollBartop = thisDom.offset().top;
            //    var scrollBarheight = thisDom.find("table").outerHeight(true);
            //    var winheight = $(window).height();
            //    //var docheight = $(document).height();
            //    var height = winheight - scrollBartop - 40;//pageheight;
            //    if (scrollBarheight < height * 0.8) {
            //        //alert("ssc");
            //        height = scrollBarheight;
            //    }
            //    else
            //        height = height * 0.9;
            //    thisDom.css("height", height)
            //});

            $(".scrollBarAuto").each(function () {
                var thisDom = $(this);
                var pageDom = $(".page-bar");
                var pagetop = 0;
                var pageheight = 0;
                var width = thisDom.data("width");
                /////////////////////////
                if (!Framework.Tool.isUndefined(width)) {
                    var scrollBarleft = thisDom.offset().left;
                    var winwidth = $(window).width();
                    var container = thisDom.find(".container");
                    if (winwidth - scrollBarleft > width) {
                        container.css("width", "100%");
                    }
                    else {
                        container.css("width", width);
                    }
                }

                var scrollBartop = thisDom.offset().top;

                var scrollBarheight = thisDom.find("table").outerHeight(true);
                //alert(thisDom.find("table").width());
                var winheight = $(window).height();
                //var docheight = $(document).height();
                var height = winheight - scrollBartop - 40;//pageheight;
                if (scrollBarheight < height * 0.8) {
                    //alert("ssc");
                    height = scrollBarheight;
                }
                else {
                    //alert(winheight + "  " + scrollBartop + "  " + height +"  "+ scrollBarheight);
                    height = height * 0.9;
                }
                if (height < 200)
                    height = 200;
                thisDom.css("height", height);

            });

            //主表自适应：有个参数不同
            $(".scrollBarAutoMaster").each(function () {
                var thisDom = $(this);
                var pageDom = $(".page-bar");
                var pagetop = 0;
                var pageheight = 0
                //if (pageDom.length > 0) {
                //    pagetop = $(".page-bar").offset().top;
                //    pageheight = $(".page-bar").outerHeight(true);
                //}
                //var scrollBartop = $(".scrollBar").offset().top;
                //var scrollBarheight = $("#tbList").outerHeight(true);
                //var winheight = $(window).height();
                //var docheight = $(document).height();
                //var height = winheight - scrollBartop - pageheight - 40;
                //if (scrollBarheight < height)
                //    height = scrollBarheight;
                //$(".scrollBar").css("height", height)
                /////////////////////////
                var scrollBartop = thisDom.offset().top;
                var scrollBarheight = thisDom.find("table").outerHeight(true);
                var winheight = $(window).height();
                //var docheight = $(document).height();
                var height = winheight - scrollBartop - 40;//pageheight;
                if (scrollBarheight < height * 0.85) {
                    //alert("ssc");
                    height = scrollBarheight;
                }
                else
                    height = height * 0.7;
                if (height < 200)
                    height = 200;
                thisDom.css("height", height)
            });
        }).resize();
        //$(window).resize();
        //主表--首行高亮显示
        if ($(".MasterTab tbody > tr:first").length > 0) {
            Framework.Tool.MasterTabTrClick($(".MasterTab tbody > tr:first"));
        }
    }
);
