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
/////////////////////////////////////////////////////////
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

/////////////////////////////////////////////////////////////
////获取EditArea内的数据
Framework.Tool.GetEditAreaData = function (obj, parentobj) {
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
        if (item.hasClass("multiselect")) {
            if (val.length > 0) {
                var vals = "";
                for (var i = 0; i < val.length; i++) {
                    //var fieldname = key + "[" + i + "]";
                    //obj[fieldname] = val[i];
                    vals += val[i] + ",";
                }
                //var fieldname = key + "s";
                obj[key] = vals;
            }
        }
        else
            obj[key] = val;

        //multiselect
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

    //$(parentobj).find('input:checkbox:checked').each(function (i) {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    key = key.format(i);
    //    var val = item.val();
    //    obj[key] = val;

    //    //key = key.format(i, k2);
    //    //var val = item.val();
    //    //obj[key] = val;

    //    //var item = $(this);
    //    //var key = item.attr("name");
    //    //if (!Framework.Tool.isUndefined(key)) {
    //    //    var val = item.val();

    //    //    if (ht.contain(key)) {
    //    //        var num = ht.get(key) + 1;
    //    //        obj[key + "[" + num + "]"] = val;
    //    //        ht.remove(key);
    //    //        ht.add(key, num);
    //    //    }
    //    //    else {
    //    //        ht.add(key, 0);
    //    //        obj[key + "[0]"] = val;
    //    //    }
    //    //}
    //});

    var checkboxArr = new Array();
    var checkboxDoms = new Array();

    //$('input[name="fruit"]:checked')
    var checkboxs = $(parentobj).find('input:checkbox:checked');
    //var checkboxs = $(parentobj).find('input[type="checkbox"]:checked');
    checkboxs.each(function (i) {
        var name = $(this).attr("name")
        checkboxArr.push(name);
        checkboxDoms.push({ name: name, val: $(this).val() });
    });

    var tempp = $.ENumberable.From(checkboxs);
    var apart = $.ENumberable.From(checkboxArr).Distinct().ToArray();
    var checkboxDoms = $.ENumberable.From(checkboxDoms);
    for (var i = 0; i < apart.length; i++) {
        var itemtemp = apart[i];// apart.pop();
        var checkboxItesm = checkboxDoms.Where(function (x) {// checkboxs.Where(function (x) {
            return (x.name == itemtemp)
        });
        var j = 0;
        if (checkboxItesm.length > 0) {
            checkboxItesm.ToArray().forEach(function (item) {
                var key = item.name;
                if (!Framework.Tool.isUndefined(row)) {
                    key = key.format(j);
                    obj[key] = item.val;
                    j++;
                }
            });
        }
    }
    return obj;
}

///获取table中数据
//data:原数据对象
//tbselect:表格选择器，如果长度为2，则针对选中的
//row：当前行，可能前面已有数组
//bselect：是否只针对CheckBox选中的
Framework.Tool.GetTableColls = function (data, tbselect, row) {
    var tbselects = tbselect.split('|');
    var obj = $(tbselects[0] + ' tbody');
    //if (!Framework.Form.Validates(obj))
    //    return false;
    if (Framework.Tool.isUndefined(row))
        row = 0;

    var table = $(tbselects[0]);
    var tbodyDom = table.find("tbody");
    var collsname = tbodyDom.data("collsname");

    if (Framework.Tool.isUndefined(collsname))
        collsname = "Items";

    var trrows = $(tbselects[0] + ' tbody>tr');
    if (tbselects.length == 2)
        trrows = $(tbselects[0] + ' tbody .jq-checkall-item:checked').closest("tr");

    trrows.each(function (i) {
        var tr = $(this);
        var objs = $(this).data();
        for (att in objs) {
            var AttName = tbodyDom.data(att);
            if (!Framework.Tool.isUndefined(AttName)) {
                var Att = AttName;
                var preName = "Item." + collsname + '[' + row + ']' + "." + Att;
                data[preName] = objs[att];
            }
        }
        //调取行绑定
        Framework.Tool.GetTrFormDataNew(collsname, data, tr, row);
        row++;
    });
    return true;
};

//获取table中tr中的data-xxx数据，非集合数据
Framework.Tool.GetTrData = function (data, tr, tbodyDom) {
    var trdata = tr.data();
    for (att in trdata) {
        var AttName = tbodyDom.data(att);
        if (!Framework.Tool.isUndefined(AttName)) {
            var Att = AttName;
            var preName = "Item." + Att;
            data[preName] = trdata[att];
        }
    }
};

///获取table中tr中表单控件数据
Framework.Tool.GetTrFormData = function (obj, parentobj, i, kn) {
    //var obj = {};
    var k2 = 10000;
    if (!Framework.Tool.isUndefined(kn))
        k2 = kn;
    if (Framework.Tool.isUndefined(i))
        i = 0;

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

            //if (ht.contain(key)) {
            //    var num = ht.get(key) + 1;
            //    obj[key + "[" + num + "]"] = val;
            //    ht.remove(key);
            //    ht.add(key, num);
            //}
            //else {
            //    ht.add(key, 0);
            //    obj[key + "[0]"] = val;
            //}
        }
    });

    return obj;
}

///获取table中tr中表单控件数据
Framework.Tool.GetTrFormDataNew = function (collsname, obj, parentobj, i, kn) {
    //var obj = {};
    var k2 = 10000;
    if (!Framework.Tool.isUndefined(kn))
        k2 = kn;
    if (Framework.Tool.isUndefined(i))
        i = 0;

    $(parentobj).find('input:text').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = "Item." + collsname + "[{0}]." + key;
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('input:password').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = "Item." + collsname + "[{0}]." + key;
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });


    $(parentobj).find('textarea').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            //key = key.format(i, k2);
            key = "Item." + collsname + "[{0}]." + key;
            key = key.format(i, k2);
            var val = item.val();
            val = escape(val);
            obj[key] = val;
        }
    });

    $(parentobj).find('input:hidden').each(function () {
        var item = $(this);
        if (item[0].type == "hidden") {
            var key = item.attr("name");
            if (!Framework.Tool.isUndefined(key)) {
                key = "Item." + collsname + "[{0}]." + key;
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
            key = "Item." + collsname + "[{0}]." + key;
            key = key.format(i, k2);
            var val = item.val();
            obj[key] = val;
        }
    });

    $(parentobj).find('select').each(function () {
        var item = $(this);
        var key = item.attr("name");
        if (!Framework.Tool.isUndefined(key)) {
            key = "Item." + collsname + "[{0}]." + key;
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

            //if (ht.contain(key)) {
            //    var num = ht.get(key) + 1;
            //    obj[key + "[" + num + "]"] = val;
            //    ht.remove(key);
            //    ht.add(key, num);
            //}
            //else {
            //    ht.add(key, 0);
            //    obj[key + "[0]"] = val;
            //}
        }
    });

    return obj;
}

/////////////////////////////////////////////////////////////
Import("Framework.Form");
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

Framework.Form.Validates = function (parentobj) {
    var pass = true;
    $(parentobj).find('input:text').each(function () {
        var obj = $(this);
        var passtemp = Framework.Form.EleValidate($(this));
        if (obj.hasClass("validaterrorpro")) {
            pass = false;
        }
    });

    $(parentobj).find('textarea').each(function () {
        //$(this).css({ "border": "1px solid black" });
        //obj.addClass("validaterrorpro");
        $(this).removeClass("validaterrorpro");
        $(this).attr({ "title": "" });
        var passtemp = Framework.Form.EleValidate($(this));
        if (!passtemp)
            pass = false;
    });
    return pass;
}

//验证方法
Framework.Form.EleValidate = function (obj) {
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

Import("Framework.Validate");

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

$(document).delegate(":input:text,input:password", 'blur', function () {
    Framework.Form.EleValidate($(this));
});

$(document).delegate(":input:text,input:password", 'focus', function () {
    $(this).removeClass("validaterrorpro");
    $(this).attr({ "title": "" });
});

///////////////////////////////////////////////////////
Import("Framework.Ajax");
//异步-视图
Framework.Ajax.GetView = function (data, controlAndaction, successCallBack, errorCallBack) {
    //alert(controlAndaction);

    var url = controlAndaction;
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
        url: controlAndaction,
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
    //alert(controlAndaction);

    var url = controlAndaction;
    var request = $.ajax({
        url: url,
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
    //alert(controlAndaction);
    var request = $.ajax({
        url: controlAndaction,
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
        url: controlAndaction,
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
    //alert(controlAndaction);
    var request = $.ajax({
        url: controlAndaction,
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
        url: controlAndaction,
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
Import("Framework.UI");

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

Framework.UI.FormModalUI = function (params) {
    var message = "你确定要执行此操作吗？";
    message = params.message || message;
    var str = '<div class="modal-body clearfix">';
    str += '<form>';
    str += '' + message;
    str += '</form>';
    str += '</div>';
    //str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>';
    if (!Framework.Tool.isUndefined(params.confirmText)) {
        if (params.confirmText != "")
            str += '<button type="button" class="btn btn-info btn-primary" data-fun=' + params.fun + '>' + params.confirmText + '</button>';
    }
    //if (!Framework.Tool.isUndefined(params.confirmCloseText)) {
    //    str += '<button type="button" class="btn btn-Close">' + params.confirmCloseText + '</button>';
    //}
    str += '</div>';
    params.message = str;
    Framework.UI.FormModal(params);
};

Framework.UI.FormModalSelectPopup = function (params) {
    var message = "";
    message = params.message || message;
    //var str = '<div class="modal-body clearfix">';
    //str += '<form>';
    var str = '' + message;
    //str += '</form>';
    //str += '</div>';
    //str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default btn-close" data-dismiss="modal">关闭</button>';
    str += '<button type="button" class="btn btn-info btn-primary" data-fun="">选择</button>';

    str += '</div>';
    params.message = str;
    Framework.UI.FormModal(params);
};

Framework.UI.FormModal = function (params) {
    if (isFormModal) {
        return false;
    }
    isFormModal = true;
    var title = "操作提示";
    var width = "600";
    var message = "";
    //var confirmText = "保 存";
    //confirmText confirmCloseText
    var cancelText = "关闭";
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
        //confirmText = params.confirmText || confirmText;
        cancelText = params.cancelText || cancelText;
        onShown = params.onShown;
        onHidden = params.onHidden;
        onConfirm = params.onConfirm;
        onSaveClose = params.onSaveClose;
        onCancel = params.onCancel;
        onBack = params.onBack;
    }

    var str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    str += '<div class="modal-dialog" style="width: ' + width + 'px;">';
    str += '<div class="modal-content">';
    str += '<div class="modal-header">';
    str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    str += '<h4 class="modal-title">' + title + '</h4>';
    str += '</div>';
    str += '<div class="alert  alert-success messprompt hide" role="alert"><span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';
    //str += '<div id="popupContent">';
    //str += '<div class="modal-body clearfix">';
    //str += '<form>';
    str += '' + message;
    //str += '</form>';
    //str += '</div>';
    //str += '</div>';
    //str += '<div class="modal-footer">';
    //str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    //str += '        <button type="button" class="btn btn-info btn-primary">' + params.confirmText + '</button>';
    ////str += '        <button type="button" class="btn btn-Close">' + params.confirmCloseText + '</button>';

    //if (!Framework.Tool.isUndefined(params.confirmText)) {
    //    if (params.confirmText != "")
    //        str += '<button type="button" class="btn btn-info btn-primary">' + params.confirmText + '</button>';
    //}
    //if (!Framework.Tool.isUndefined(params.confirmCloseText)) {
    //    str += '<button type="button" class="btn btn-Close">' + params.confirmCloseText + '</button>';
    //}

    //str += '</div>';



    //str += '</div>';
    str += '</div>';
    str += '</div>';
    str += '</div>';


    //var str = '<div class="modal fade in" aria-hidden="true" data-backdrop="static">';
    //str += '<div class="modal-dialog" style="width: ' + width + 'px;">';
    //str += '<div class="modal-content">';
    //str += '<div class="modal-header">';
    //str += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
    //str += '<h4 class="modal-title">' + title + '</h4>';
    //str += '</div>';
    //str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';
    //str += '<div class="modal-body clearfix">';
    ////str += '<div class="alert  alert-success messprompt hide" role="alert"> <span class="glyphicon glyphicon-ok-circle"><span>提示：</span><span class="messpromptmess">如果要隐藏请在父元素增加class=“hide”</span></div>';
    //str += "<div id='popupContent'>";
    //str += "<form>";
    //str += message;
    //str += "</form>";
    //str += '</div>';
    //str += '</div>';
    //str += '<div class="modal-footer">';
    //str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    //if (!Framework.Tool.isUndefined(params.confirmText)) {
    //    if (params.confirmText != "")
    //        str += '<button type="button" class="btn btn-info btn-primary">' + params.confirmText + '</button>';
    //}
    //if (!Framework.Tool.isUndefined(params.confirmCloseText)) {
    //    str += '<button type="button" class="btn btn-Close">' + params.confirmCloseText + '</button>';
    //}
    //str += '</div>';
    //str += '</div>';
    //str += '</div>';
    //str += '</div>';

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
                placeholder: "请选择1"//,
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

    Framework.Tool.PopupEvenBind = function () {
        //alert(popup.find(".modal-footer button").length+"sss");
        //alert("sdsd---");
        //popup.find(".modal-footer button").unbind();
        //popup.find(".modal-footer button").click(function (event) {
        //    //alert("mnb");
        //    if (onConfirm) {
        //        onConfirm(popup, $(this));
        //    }
        //    event.preventDefault();
        //    event.stopPropagation();
        //});
        popup.find(".modal-footer .btn").unbind();
        popup.find(".modal-footer .btn").click(function (event) {
            //alert("mnb");
            if (onConfirm) {
                onConfirm(popup, $(this));
            }
            event.preventDefault();
            event.stopPropagation();
        });
        popup.find(".datetimepicker").datetimepicker({
            lang: 'ch'          //语言
            , timepicker: false  //是否显示时间下拉框 false 不显示，true 显示
            , format: 'Y-m-d  '   //日期时间格式化  带时间  format：'Y-m-d H:i:s'
            , onChangeDateTime: function () {    //日期改变事件
                // console.log("日期改变事件");
            }
        });
        //event.preventDefault();
        //event.stopPropagation();
    };
    //alert("Popup");
    Framework.Tool.PopupEvenBind();
    //popup.find(".modal-footer button").click(function (event) {
    //    if (onConfirm) {
    //        onConfirm(popup, $(this));
    //    }
    //    event.preventDefault();
    //    event.stopPropagation();
    //});

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
        //confirmText = params.confirmText || confirmText;
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
    str += '</div>';
    str += '</div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    if (!Framework.Tool.isUndefined(params.confirmText)) {
        if (params.confirmText != "")
            str += '<button type="button" class="btn btn-info btn-primary">' + params.confirmText + '</button>';
    }
    //str += '<button type="button" class="btn btn-info">' + confirmText + '</button>';
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

/////////////////////////////////////////////////////////////
$(document).delegate('.btn-FwSearch', 'click', function (e) {
    var currobj = $(this);
    Framework.Tool.PageHelper(currobj, {});
})

Framework.FwSearch = function (currobj, params) {
    //alert("window调用");
    var operArea = currobj.closest('.operArea');
    var targetdom = operArea.data("targetdom");
    var searchArea = operArea.find(".search-bar").first();
    var targetdom = currobj.closest(targetdom);// operArea.data("targetdom");
    //var targetdom = operArea.find(">" + targetdom);// currobj.closest(targetdom);// operArea.data("targetdom");

    var data = {};
    var row = 0;
    if (searchArea.find(".generalWhere").length > 0)//通用查询条件，一般针对必填
    {
        var parentDomTemp = searchArea.find(".generalWhere");
        data = Framework.Tool.GetEditAreaData(data, parentDomTemp);
    }
    //(1)获取快速找到查询条件：快速查询、高级查询
    if (searchArea.find(".fastWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, searchArea.find(".fastWhere"), row);
    //(2)获取更多查询条件
    if (searchArea.find(".moreWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, searchArea.find(".moreWhere"), row);
    //(3)获取自定义查询条件
    //row++;
    //if (operArea.find(".custWhere").length > 0)
    //    Framework.Tool.GetTableColls(data, operArea.find(".custWhere"), row);

    var url = operArea.data("url");
    var bulidurl = Framework.BulidSearchDataOrUrl(operArea, data, params, url, 1);

    if (Framework.Tool.isUndefined(targetdom) || targetdom.length == 0) {
        window.location.href = bulidurl;
    }
    else {
        Framework.Ajax.GetView(data, url, function (result) {//PostView
            //$(targetdom).html(result);
            $(targetdom).replaceWith(result);
            if ($('.multiselect').length > 0) {
                //alert();
                $('.multiselect').multiselect({
                    nonSelectedText: '请选择',
                    buttonWidth: '180px',
                    onChange: function (element, checked) {
                        //element.text()
                    }
                });;
            }
            $(window).resize();
        });
    }
};

/////////////////////////////////////////////////////////////
$(document).delegate('.btn-Export', 'click', function (e) {
    var currobj = $(this);
    //Framework.Tool.PageHelper(currobj, {});
    Framework.Export(currobj);
})

Framework.Export = function (currobj) {
    //alert("window调用");
    var operArea = currobj.closest('.operArea');
    var targetdom = operArea.data("targetdom");
    var searchArea = operArea.find(".search-bar").first();
    var targetdom = currobj.closest(targetdom);// operArea.data("targetdom");
    //var targetdom = operArea.find(">" + targetdom);// currobj.closest(targetdom);// operArea.data("targetdom");

    var data = {};
    var row = 0;
    if (searchArea.find(".generalWhere").length > 0)//通用查询条件，一般针对必填
    {
        var parentDomTemp = searchArea.find(".generalWhere");
        data = Framework.Tool.GetEditAreaData(data, parentDomTemp);
    }
    //(1)获取快速找到查询条件：快速查询、高级查询
    if (searchArea.find(".fastWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, searchArea.find(".fastWhere"), row);
    //(2)获取更多查询条件
    if (searchArea.find(".moreWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, searchArea.find(".moreWhere"), row);
    //(3)获取自定义查询条件
    //row++;
    //if (operArea.find(".custWhere").length > 0)
    //    Framework.Tool.GetTableColls(data, operArea.find(".custWhere"), row);

    var url = currobj.data("url");
    //var bulidurl = Framework.BulidSearchDataOrUrl(operArea, data, params, url, 1);

    url = url + "";
    for (itemx in data) {
        if (!!data[itemx] && data[itemx].toString().length > 0)
            url += itemx + "=" + data[itemx] + "&";
    }
    //windows.open(bulidurl);
    window.open(url);

    //if (Framework.Tool.isUndefined(targetdom) || targetdom.length == 0) {
    //    window.location.href = bulidurl;
    //}
    //else {
    //    Framework.Ajax.GetView(data, url, function (result) {//PostView
    //        //$(targetdom).html(result);
    //        $(targetdom).replaceWith(result);
    //        if ($('.multiselect').length > 0) {
    //            //alert();
    //            $('.multiselect').multiselect({
    //                nonSelectedText: '请选择',
    //                buttonWidth: '180px',
    //                onChange: function (element, checked) {
    //                    //element.text()
    //                }
    //            });;
    //        }
    //        $(window).resize();
    //    });
    //}
};

//获取快速、更多查询条件
Framework.Tool.GetQuerys = function (obj, parentobj, row) {
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

    //$(parentobj).find('select').each(function () {
    //    var item = $(this);
    //    var key = item.attr("name");
    //    var val = item.val();
    //    if (item.hasClass("multiselect")) {
    //        if (val.length > 0) {
    //            for (var i = 0; i < val.length; i++) {
    //                var fieldname = key + "[" + i + "]";
    //                obj[fieldname] = val[i];
    //            }
    //        }
    //    }
    //    else
    //        obj[key] = val;

    //    //multiselect
    //});


    $(parentobj).find('select').each(function () {
        var item = $(this);
        var key = item.attr("name");
        var val = item.val();
        if (item.hasClass("multiselect")) {
            if (val == null) {
                val = "";
            }
            else if (val.length > 0) {
                var value = 0;
                for (var i = 0; i < val.length; i++) {
                    //var fieldname = key + "[" + i + "]";
                    var v = new Number(val[i]);
                    value += v;
                }
                val = value;
            }
        }
        obj[key] = val;
        obj["Querys[" + row + "].FieldName"] = key;
        obj["Querys[" + row + "].Value"] = val;
        row++;
    });
    return row;
}

//生成查询Data、Url
Framework.BulidSearchDataOrUrl = function (operArea, data, params, url, bformsubmit) {
    data["PageQueryBase.PageIndex"] = params.pageIndex || 1;
    var RankInfo = params.RankInfo || "";
    var bformsubmit = bformsubmit || 1;
    //(2)获取排序字段
    if (RankInfo.length == 0) {
        //var sortArea = operArea.find(".sortArea").first();
        operArea.find(".fieldorderby").each(function () {
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
    var pageArea = operArea.find(">.page-bar");
    data["PageQueryBase.PageSize"] = pageArea.find("#PageSize").val() || 10;

    //(5)生成url
    if (bformsubmit == 2)
        url = url + "?"
    else
        url = url + "?";
    for (itemx in data) {
        if (!!data[itemx] && data[itemx].toString().length > 0)
            url += itemx + "=" + data[itemx] + "&";
    }
    return url;
}

//////////////////Goto：跳转页/////////////////////////////////////////
$(document).delegate('.pageindexgoto', 'click', function (e) {
    var obj = $(this);
    var pageArea = $(obj).closest(".page-bar");
    Framework.Tool.PageHelper(obj, { pageIndex: pageArea.find(".gotoNumber").val() });
});

/////////////////每页大小改变/////////////////////////////////////////
$(document).delegate('#PageSize', 'change.pagination', function (e) {
    Framework.Tool.PageHelper($(this), {});
});

/////////////////排序/////////////////////////////////////////////////
$(document).delegate('.fieldorderby', 'click', function (e) {
    //内部可以查找到
    var obj = $(this);
    var Property = $(obj).data("field");
    var RankInfo = Property + "|0";

    var objordercss = "";
    if ($(obj).hasClass("ascorange")) {
        objordercss = "ascorange";
        RankInfo = Property + "|1";
    }
    Framework.Tool.PageHelper(obj, { RankInfo: RankInfo });
});

//////////////////切换页码--事件////////////////////////////////////////
$(document).delegate('.pageIndex', 'click', function (e) {
    Framework.Tool.PageHelper($(this), { pageIndex: $(this).data("val") })
});

Framework.Tool.PageHelper = function (obj, val) {
    var currobj = obj;
    var operArea = obj.closest('.operArea');
    var searchmethod = operArea.data("searchmethod");
    var searchmethods = searchmethod.split(".");
    if (searchmethods.length == 2)
        window[searchmethods[0]][searchmethods[1]](currobj, val)
}

////////////////////////////////////////////////////////////////////
//回车：进入下一个表单控件
Framework.Tool.EnterToTab = function (e, obj) {
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

//回车：进入下一个表单控件
$(document).delegate('.editArea :text,select', 'keydown', function (e) {
    //alert("sdsdwe");
    Framework.Tool.EnterToTab(e, $(this));
});

//回车：进入下一个表单控件
Framework.Tool.TableToTab = function (e, obj) {//, selectobj) {//, scrollobj) {
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

//回车：进入下一个表单控件
$(document).delegate('.FwtabletoTab input', 'keydown', function (e) {
    Framework.Tool.TableToTab(e, $(this));
});

////////////////////////////////////////////////////////////////
//////提交
//Framework.Tool.FwBtnSubmit = function (btn) {
//    var OperParamDom = btn.closest(".OperArea").find(".OperParam");
//    var configinfo = OperParamDom.data("configinfo");//提交确认提示

//    var posturl = OperParamDom.data("posturl");
//    var targeturl = OperParamDom.data("targeturl");//提交后目标url
//    var targeturlparamname = OperParamDom.data("targeturlparamname");//提交后目标url的参数名

//    //var indexurlpkb = OperParamDom.data("indexurlpkb");//是否要更新主键
//    //var indexurlpk = OperParamDom.data("indexurlpk");
//    var pk = OperParamDom.data("pkname");
//    var pkval = OperParamDom.data("pkval");
//    var masteditarea = OperParamDom.data("masteditarea");
//    var childeditarea = OperParamDom.data("childtableselect");
//    var data = {};

//    //////////////////获取主键////////////////////////
//    if (!Framework.Tool.isUndefined(pkval)) {
//        data["Item." + pk] = pkval;
//    }
//    //alert("sd1");

//    //////////////////区域数据获取/////////////////////////////////
//    if (!Framework.Tool.isUndefined(masteditarea)) {
//        var obj = $(masteditarea);
//        if (!Framework.Form.Validates(obj))
//            return;
//        //data = Framework.Form.GetFormItemByObjNew(data, $(masteditarea));
//        data = Framework.Tool.GetEditAreaData(data, $(masteditarea));
//    }
//    //var areasname = initData.areasRoute;
//    //////////////////子表区域数据获取////////////////////////////
//    if (!Framework.Tool.isUndefined(childeditarea)) {
//        var results = Framework.Tool.GetTableColls(data, childeditarea)
//        if (!results)
//            return;
//    }

//    btn.addClass("disabled");
//    //alert("sd2");
//    //return;
//    if (Framework.Tool.isUndefined(configinfo)) {//不进行操作提示
//        //alert("不提示");
//        Framework.Ajax.PostJson(data, posturl, function (results) {
//            btn.removeClass("disabled");
//            if (results.Data.RespAttachInfo.bError) {
//                //显示错误
//                var strerrors = Framework.UI.Behavior.ErrorHandling(results);
//                if (strerrors.length > 0)
//                    alert(strerrors);
//                return;
//            }
//            var data = results.Data;
//            alert('操作成功');
//            //targeturl  
//            if (Framework.Tool.isUndefined(targeturl))//无indexurl重新加载页面
//                window.location.reload();
//            else {//根据请求刷新页面
//                var targeturlparam = "";
//                if (Framework.Tool.isUndefined(targeturlparamname))
//                    targeturlparam = "";
//                else {
//                    targeturlparam = "?Item." + targeturlparamname + "=" + data.Item[targeturlparamname];
//                }
//                window.location.href = "/" + targeturl + targeturlparam;
//            }
//        })
//    }
//    else {
//        var params = {
//            title: '【操作确认】',
//            width: 400,
//            message: configinfo + "，你确定要执行操作吗？",
//            onConfirm: function (popup) {
//                //alert("提示");
//                //return;
//                Framework.Ajax.PostJson(data, posturl, function (results) {
//                    btn.removeClass("disabled");
//                    if (results.Data.RespAttachInfo.bError) {
//                        //显示错误
//                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
//                        if (strerrors.length > 0)
//                            alert(strerrors);
//                        return;
//                    }
//                    var data = results.Data;
//                    alert('保存成功');

//                    if (Framework.Tool.isUndefined(targeturl))//无indexurl重新加载页面
//                        window.location.reload();
//                    else {//根据请求刷新页面
//                        var targeturlparam = "";
//                        if (Framework.Tool.isUndefined(targeturlparamname))
//                            targeturlparam = "";
//                        else {
//                            targeturlparam = "?Item." + targeturlparamname + "=" + data.Item[targeturlparamname];
//                        }
//                        window.location.href = Framework.Page.BaseURL + targeturl + targeturlparam;
//                    }
//                })
//            },
//            onCancel: function (popup) {
//                //alert("sdsd取消");
//                btn.removeClass("disabled");
//            }
//        };
//        Framework.UI.Modal(params);
//    }
//}

//清空Jobj中表单控件值
Framework.Tool.reSet = function (Jobj) {
    var form = Jobj.find("form");
    if (form.length > 0)
        form[0].reset();
}

////弹窗Get
//$(document).delegate('.btn-FwRowPopup', 'click', function (e) {
//    var currobj = $(this);
//    //alert("dsd111");
//    Framework.Tool.FwRowPopup(currobj);
//});

//Framework.Tool.FwRowPopup = function (currobj) {
//    //alert();
//    //data-btnbehavior='{0}' data-fun='{1}' data-pkname='{2}'  data-pkval='{3}' data-modularname='{4}' data-popupwidth='{5}'
//    //var btnbehavior = currobj.data("btnbehavior");
//    //var fun = currobj.data("fun");
//    //var pkname = currobj.data("pkname");
//    //var pkval = currobj.data("pkval");
//    var modularname = currobj.data("modularname");
//    //var btnnamecn = currobj.data("btnnamecn");
//    var tableselect = currobj.data("tableselect");
//    if (Framework.Tool.isUndefined(modularname))
//        modularname = "";

//    //var targeturlparamname = currobj.data("targeturlparamname");

//    var popupwidth = currobj.data("popupwidth");
//    if (Framework.Tool.isUndefined(popupwidth))
//        popupwidth = 600;
//    //var tr = currobj.closest('tr');
//    var table = currobj.closest('table');
//    var calcol = table.data("calcol");

//    var posturl = currobj.data("posturl");
//    var posturltemp = posturl;
//    var title = "";
//    if (fun == 0)//查看，不显示确定按钮
//    {
//        title = "【" + "" + btnnamecn + modularname + "】";
//        btnnamecn = "";
//    }
//    else {
//        title: '【' + btnnamecn + modularname + '确定】';
//    }
//    var geturl = posturl;

//    Framework.Ajax.GetView(data, geturl, function (result) {
//        ////错误判断
//        var params = {
//            title: title,
//            width: popupwidth,
//            message: result,
//            //confirmText: btnnamecn,
//            onConfirm: function (popup, currobj) {
//                //var btn = $(this);
//                //alert("FwBtnSubmit");
//                //判断是否为关闭
//                if (currobj.hasClass("btn-close")) {
//                    popup.find(".close").trigger("click");
//                }
//                var popuptemp = popup;
//                Framework.Tool.FwBtnSubmit(currobj, 2, popuptemp, tr, function (trnew) {
//                    tr = trnew;
//                });
//            }
//        };
//        Framework.UI.FormModal(params);
//    })
//};

//弹窗Get：参数保存在url中
$(document).delegate('.btn-FwRowUIPopup', 'click', function (e) {
    var currobj = $(this);
    //alert("dsd111");
    Framework.Tool.FwRowUIPopup(currobj);
});

Framework.Tool.FwRowUIPopup = function (currobj) {
    //alert();
    //data-btnbehavior='{0}' data-fun='{1}' data-pkname='{2}'  data-pkval='{3}' data-modularname='{4}' data-popupwidth='{5}'
    //var btnbehavior = currobj.data("btnbehavior");
    var fun = currobj.data("fun");
    //var pkname = currobj.data("pkname");
    //var pkval = currobj.data("pkval");
    var modularname = currobj.data("modularname");
    var btnnamecn = currobj.data("btnnamecn");
    var tableselect = currobj.data("tableselect");
    if (Framework.Tool.isUndefined(modularname))
        modularname = "";

    var targeturlparamname = currobj.data("targeturlparamname");

    var popupwidth = currobj.data("popupwidth");
    if (Framework.Tool.isUndefined(popupwidth))
        popupwidth = 600;
    var tr = currobj.closest('tr');
    var table = currobj.closest('table');
    var calcols = currobj.data("calcols");

    var posturl = currobj.data("posturl");
    var posturltemp = posturl;
    var title = "";
    if (fun == 0)//查看，不显示确定按钮
    {
        title = "【" + "" + btnnamecn + modularname + "】";
        btnnamecn = "";
    }
    else {
        title: '【' + btnnamecn + modularname + '确定】';
    }
    var tr = currobj.closest('tr');
    var table = currobj.closest('table');
    var calcol = table.data("calcol");
    var tbheadsobjs = table.find("thead").data();
    var rowItem = tr.data();
    var strMess = '<ul class="form-horizontal merge">';
    //alert(tbheadsobjs.length);
    var length = 0;
    for (att in tbheadsobjs) {
        length++;
    }
    var colcopies = 12;
    if (length > 4) {
        colcopies = 6;
    }
    var strMessTemp = "";
    for (att in tbheadsobjs) {
        //<ul class='form-horizontal merge'>
        strMessTemp += "<li class='col-lg-" + colcopies + "'>";
        strMessTemp += "    <div class='form-group'>";
        strMessTemp += "        <label class='col-lg-4 control-label'>" + tbheadsobjs[att] + "</label>";
        strMessTemp += "        <div class='col-lg-8'>";
        strMessTemp += "        <label class='col-lg-4 control-label'>" + rowItem[att] + "</label>";
        //strMess += rowItem[att];
        strMessTemp += "        </div>";
        strMessTemp += "    </div>";
        strMess += "</li>";
    }
    if (strMessTemp.length == 0)
        strMessTemp = "你确定要执行此操作吗？";
    strMess += strMessTemp + '</ul>'
    var params = {
        title: title,
        width: popupwidth,
        message: strMess,
        confirmText: btnnamecn, //删除、提交、审核
        fun: fun,
        onConfirm: function (popup, popupbtn) {
            fun = popupbtn.data("fun");
            ////弹窗，对表格的操作：1:首行插入行，2:替换行，3:删除行，4：重载页面 5:界面删除
            ////查看：0
            ////界面删除：5
            ////删除不刷新页面：3或4：posturl
            ////审核：2: posturl......rowurl
            //在前台删除行
            if (fun == 0)//没有此按钮
            {
            }
            else if (fun == 21)//提交替换：2：posturl      rowurl 建议不使用
            {
                var posturl = obj.data("posturl");
                var prepname = "Item." + pkname;
                var data = {};
                data[prepname] = pkval;
                Framework.Ajax.PostJson(data, editurl, function (results) {
                    //错误处理
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            popup.fail(strerrors);
                        return;
                    }
                    var rowurl = obj.data("rowurl");
                    Framework.Ajax.GetView(data, rowurl, function (results) {
                        tr.replaceWith(results);
                        alert("操作成功");
                        //popup.find(".btn-default").trigger("click");
                        popup.find(".close").trigger("click");
                    });
                });
            }
            else if (fun == 41) {//前台删除
                tr.remove();
                popup.find(".close").trigger("click");
                //计算
                if (!Framework.Tool.isUndefined(calcols))
                    Framework.Tool.CalculationColNew(calcols, table);
            }
            else if (fun == 42)//后台删除，删除table中的行，不刷新
            {
                ////删除不刷新页面：3或4：posturl
                var posturl = currobj.data("posturl");
                //var prepname = "Item." + pkname;
                var data = {};
                //data[prepname] = pkval;
                Framework.Ajax.PostJson(data, posturl, function (results) {
                    //错误处理
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            popup.fail(strerrors);
                        return;
                    }
                    tr.remove();
                    popup.find(".close").trigger("click");
                    //popup.find(".btn-default").trigger("click");
                });
            }
            else if (fun == 70)//后台操作，不刷新表格行
            {
                ////不刷新页面：3或4：posturl
                var posturl = currobj.data("posturl");
                //var prepname = "Item." + pkname;
                var data = {};
                //data[prepname] = pkval;
                Framework.Ajax.PostJson(data, posturl, function (results) {
                    //错误处理
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            popup.fail(strerrors);
                        return;
                    }
                    alert("操作成功");
                    //tr.remove();
                    popup.find(".close").trigger("click");
                    //popup.find(".btn-default").trigger("click");
                });
            }
            else if (fun == 43)//后台删除,重载页面
            {
                var posturl = currobj.data("posturl");
                var prepname = "Item." + pkname;
                var data = {};
                data[prepname] = pkval;
                Framework.Ajax.PostJson(data, posturl, function (results) {
                    //错误处理
                    if (results.Data.RespAttachInfo.bError) {
                        //显示错误
                        var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                        if (strerrors.length > 0)
                            popup.fail(strerrors);
                        return;
                    }
                    alert("操作成功");
                    window.location.reload(true);//重载页面
                });
            }
            else
                popup.find(".close").trigger("click");
            //计算
            if (!Framework.Tool.isUndefined(calcol))
                Framework.Tool.CalculationColNew(calcol, table);
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.FormModalUI(params);
    //Framework.UI.Modal(params);
};

//批量删除UI,带计算
Framework.Tool.BatchUI = function (currobj) {//tbheadsobjs, checkeds, map, url, bRefresh, Title, fun) {
    //var table = $(currobj.data("tableid"));
    //var calcols = table.data("calcol");
    var fun = currobj.data("fun");
    var tableselect = currobj.data("tableselect");
    if (Framework.Tool.isUndefined(tableselect) || tableselect.length == 0)
        tableselect = "#tbList";
    var table = $(tableselect);
    var calcols = currobj.data("calcols");

    var modularname = currobj.data("modularname");
    if (Framework.Tool.isUndefined(modularname))
        modularname = "";

    var popupwidth = currobj.data("popupwidth");
    if (Framework.Tool.isUndefined(popupwidth))
        popupwidth = 600;
    var btnnamecn = currobj.data("btnnamecn");
    var title = "";
    if (fun == 0)//查看，不显示确定按钮
    {
        title = "【" + "" + btnnamecn + modularname + "】";
        btnnamecn = "";
    }
    else {
        title: '【' + btnnamecn + modularname + '确定】';
    }


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
        //title: '【批量删除' + Title + '确认】',
        //message: strMess,
        title: title,
        width: popupwidth,
        message: strMess,
        confirmText: btnnamecn, //删除、提交、审核
        fun: fun,
        onConfirm: function (popup, popupbtn) {
            var fun = popupbtn.data("fun");
            var opterSussecRows = [];
            var opterErrorRows = [];
            //////////执行操作/////////////////
            //$('#mainPage #tbbody  .jq-checkall-item:checked').each(function () {
            //currobj.closest(".IndexCM_LoanID").find("#tbbody .jq-checkall-item:checked").each(function () {
            if (fun == 51) {//前台删除
                checkeds.each(function () {
                    var tr = $(this).closest('tr');
                    var rowItem = tr.data();
                    tr.remove();
                });
                popup.find(".close").trigger("click");
                //计算
                if (!Framework.Tool.isUndefined(calcols))
                    Framework.Tool.CalculationColNew(calcols, table);
            }
            else
                popup.find(".close").trigger("click");
        },
        onCancel: function (popup) {
        }
    };
    Framework.UI.FormModalUI(paramsn);
    //Framework.UI.ModalBatch(paramsn);
};

//界面批量删除
$(document).delegate(".btn-BatchUI", "click", function () {
    var currobj = $(this);
    Framework.Tool.BatchUI(currobj);
});


//弹窗Get：参数保存在url中
$(document).delegate('.btn-FwPopupGet', 'click', function (e) {
    var currobj = $(this);
    //alert("dsd111");
    Framework.Tool.FwPopupGet(currobj);
});

Framework.Tool.FwPopupGet = function (currobj) {
    //alert();
    //data-btnbehavior='{0}' data-fun='{1}' data-pkname='{2}'  data-pkval='{3}' data-modularname='{4}' data-popupwidth='{5}'
    //var btnbehavior = currobj.data("btnbehavior");
    //var fun = currobj.data("fun");
    //var pkname = currobj.data("pkname");
    //var pkval = currobj.data("pkval");
    var modularname = currobj.data("modularname");
    var btnnamecn = currobj.data("btnnamecn");
    var tableselect = currobj.data("tableselect");
    if (Framework.Tool.isUndefined(modularname))
        modularname = "";

    //var targeturlparamname = currobj.data("targeturlparamname");

    var popupwidth = currobj.data("popupwidth");
    if (Framework.Tool.isUndefined(popupwidth))
        popupwidth = 600;
    var tr = currobj.closest('tr');
    //var table = currobj.closest('table');
    //var calcol = table.data("calcol");

    var posturl = currobj.data("posturl");
    //var posturltemp = posturl;
    var title = "";
    title = "【" + "" + btnnamecn + modularname + "】";

    //if (fun == 0)//查看，不显示确定按钮
    //{
    //    title = "【" + "" + btnnamecn + modularname + "】";
    //    btnnamecn = "";
    //}
    //else {
    //    title: '【' + btnnamecn + modularname + '确定】';
    //}
    var geturl = posturl;
    //参数在url中，此处是弹窗添加功能
    Framework.Ajax.GetView({}, posturl, function (result) {
        ////错误判断
        var params = {
            title: title,
            width: popupwidth,
            message: result,
            //confirmText: btnnamecn,
            onConfirm: function (popup, currobj) {
                //var btn = $(this);
                //alert("FwBtnSubmit");
                //判断是否为关闭
                if (currobj.hasClass("btn-close")) {
                    popup.find(".close").trigger("click");
                }
                //new TextValue{Text="保存并添加",Value="13"},
                //new TextValue{Text="保存继续编辑",Value="26"},
                //new TextValue{Text="保存关闭-插入行",Value="11"},
                //new TextValue{Text="保存关闭-替换行",Value="27"},
                //new TextValue{Text="保存跳转",Value="28"},

                var popuptemp = popup;
                Framework.Tool.FwBtnSubmit(currobj, 2, popuptemp, tr, function (trnew) {
                    tr = trnew;
                });
            }
        };
        Framework.UI.FormModal(params);
    })
};

//提交处理             btn-FwBtnSubmit
$(document).delegate('.btn-FwBtnSubmit', 'click', function (e) {
    var btn = $(this);
    //alert("FwBtnSubmit");
    Framework.Tool.FwBtnSubmit(btn);
});

////提交
Framework.Tool.FwBtnSubmit = function (btn, souretype, popup, tr, funback) {
    //alert("sds");
    var configinfo = btn.data("configinfo");//提交确认提示
    var posturl = btn.data("posturl");
    var fun = btn.data("fun");
    var targeturl = btn.data("targeturl");//提交后目标url

    var tableselect = btn.data("tableselect");//表格选择器：添加时使用
    var pkname = btn.data("pkname");//主键名：添加时使用
    if (Framework.Tool.isUndefined(tableselect) || tableselect == "") {
        tableselect = "#tbList";
    }

    var targeturlparamname = btn.data("targeturlparamname");//提交后目标url的参数名
    if (Framework.Tool.isUndefined(targeturlparamname)) {
        targeturlparamname = "";
    }

    //targeturlparamname
    var targetdom = btn.data("targetdom");
    if (Framework.Tool.isUndefined(targetdom) || targetdom == "") {
        targetdom = ".targetdom";
    }
    //$(".masteditarea")
    //var indexurlpkb = OperParamDom.data("indexurlpkb");//是否要更新主键
    //var indexurlpk = OperParamDom.data("indexurlpk");
    //var pk = btn.data("pkname");
    //var pkval = btn.data("pkval");
    var masteditarea = btn.data("masteditarea");
    if (Framework.Tool.isUndefined(masteditarea) || masteditarea == "") {
        masteditarea = ".mastEditArea";
    }

    var childtableselect = btn.data("childtableselect");
    if (Framework.Tool.isUndefined(childtableselect) || childtableselect == "") {
        childtableselect = ".childTableSelect #tbList";
    }

    var data = {};

    //////////////////获取主键////////////////////////
    //if (!Framework.Tool.isUndefined(pkval)) {
    //    data["Item." + pk] = pkval;
    //}

    //////////////////区域数据获取/////////////////////////////////
    if (masteditarea.length > 0) {
        var obj = $(masteditarea);
        if (obj.length > 0) {
            if (!Framework.Form.Validates(obj))
                return;
            //data = Framework.Form.GetFormItemByObjNew(data, $(masteditarea));
            data = Framework.Tool.GetEditAreaData(data, $(masteditarea));
            //保存编辑区域
            //if (!Framework.Tool.isUndefined(Framework.Tool.UeEditItem)) {
            //    var context = Framework.Tool.UeEditItem.ueEditEle.getContent();

            //    var name = "Item." + Framework.Tool.UeEditItem.name;
            //    data[name] = escape(context);
            //}

            if (!Framework.Tool.isUndefined(Framework.Tool.UeEditItems)) {
                //for (var i = 0; i < Framework.Tool.UeEditItems.length; i++) {
                //    var context = Framework.Tool.UeEditItems[i].ueEditEle.getContent();

                //    var name = "Item." + Framework.Tool.UeEditItems[i].name;
                //    data[name] = escape(context);
                //}

                //checkboxItesm.ToArray().forEach(function (item) {
                //    var key = item.name;
                //    if (!Framework.Tool.isUndefined(row)) {
                //        key = key.format(j);
                //        obj[key] = item.val;
                //        j++;
                //    }
                //});

                //Framework.Tool.UeEditItems.forEach(function (item) {
                //        var context = item.ueEditEle.getContent();

                //        var name = "Item." + item.name;
                //        data[name] = escape(context);

                //});
                for (var name in Framework.Tool.UeEditItems) {
                    //names += name + ": " + obj[name] + ", ";
                    var item = Framework.Tool.UeEditItems[name];
                    var context = item.ueEditEle.getContent();

                    var name = "Item." + item.name;
                    data[name] = escape(context);

                }
            }

            //保存html
            //name = "Item.StoreContext"
            var frmHtmls = $(masteditarea).find(".frmHtml");
            if (frmHtmls.length > 0) {
                frmHtmls.each(function () {
                    var htmldom = $(this);
                    htmldom.find("input,textarea,button").each(function () {
                        this.setAttribute('value', this.value);
                    });
                    htmldom.find(":radio,:checkbox").each(function () {
                        if (this.checked)
                            this.setAttribute('checked', 'checked');
                        else
                            this.removeAttribute('checked');
                    });
                    htmldom.find("option").each(function () {
                        if (this.selected)
                            this.setAttribute('selected', 'selected');
                        else
                            this.removeAttribute('selected');
                    });
                });

                frmHtmls.each(function () {
                    var item = $(this);
                    var key = "Item." + item.data("name");
                    var val = item.html();
                    //alert(val);
                    data[key] = escape(val);
                })
            }

            ////下拉复选框           
            //var checkboxdrops = $(masteditarea).find(".checkboxdrop");
            //if (checkboxdrops.length > 0)
            //{
            //    checkboxdrops.each(function () {
            //        var item = $(this);
            //        var key = item.attr("name");
            //        var valarr = item.val();

            //        //var val = new Number(0);
            //        //if (valarr.length > 0)
            //        //{
            //        //    //alert(val.length);
            //        //    for (var i = 0; i < valarr.length;i++) {
            //        //        val +=new Number( valarr[i]);
            //        //    }
            //        //}

            //        data[key] = valarr;// val;
            //        alert(key);
            //        alert(valarr);
            //    });
            //}
        }
    }
    //var areasname = initData.areasRoute;
    //////////////////子表区域数据获取////////////////////////////

    var tbselects = childtableselect.split('|');
    if ($(tbselects[0]).length > 0) {
        var results = Framework.Tool.GetTableColls(data, childtableselect)
        //childtableselect
        //data=Framework.Tool.GetEditAreaData(data, $(childtableselect))
        //if (!results)
        //    return;
    }

    //btn.addClass("disabled");

    Framework.Tool.FwBtnSubmitGetView = function (url) {
        if (Framework.Tool.isUndefined(url)) {
            url = posturl;
        }
        var data = {};//targeturlparamname
        var targeturlparamnameArr = targeturlparamname.split(",");
        for (var m = 0; m < targeturlparamnameArr.length; m++) {
            var getprename = "#Item_" + targeturlparamnameArr[m];
            var setprename = "Item." + targeturlparamnameArr[m];
            var val = $(masteditarea).find(getprename).val();
            data[setprename] = val;
        }
        Framework.Ajax.GetView(data, url, function (results) {
            btn.removeClass("disabled");
            btn.closest('.operArea').find(targetdom).html(results);
            //$(targetdom).html(results);
            //Framework.Tool.PopupEvenBind();
        });
    };
    //alert("dddddddddddd");
    Framework.Tool.FwBtnSubmitBulid = function () {
        //11:"保存并关闭窗口(添加行)posturl、targerurl、参数、mastarea、childarea"
        //12:保存并添加--添加到列表(弹窗)

        //13:保存并添加--新页
        //21:区域数据UI：确认并关闭=>替换行，确认并关闭=>替换行，建议不使用
        //22:确认并关闭=>替换行，确认并关闭=>替换行，主键
        //23:确认并下一条，不同功能有不同下一条
        //26:确定(编辑)提交=>保持在本页
        //27:"保存并返回(添加和所有编辑，拼接url)posturl、mastarea、childarea、targerurl、参数"
        //24:上一条，不同功能有不同下一条
        //25:下一条?不同功能有不同下一条
        if (Framework.Tool.isUndefined(fun)) {
            btn.removeClass("disabled");
            popup.find(".close").trigger("click");
            return;
        }
        //if (fun == 9) {//ajax的url访问
        //    var geturl = posturl;
        //    //var data = {};
        //    Framework.Ajax.GetView({}, geturl, function (results) {
        //        btn.removeClass("disabled");
        //        $(targetdom).replaceWith(results);
        //    });
        //}
        //else if (fun == 10) {
        //    var geturl = posturl;
        //    //var data = {};
        //    Framework.Ajax.GetView({}, geturl, function (result) {
        //        btn.removeClass("disabled");
        //        $(tableselect).find("tbody").prepend(result);
        //    });
        //}
        //else if (fun == 24 || fun == 25) {
        //    //alert();
        //    Framework.Tool.FwBtnSubmitGetView();
        //}
        if (fun == 1) {
            window.location.href = posturl;
        }
        else if (fun == 2)//查询url
        {
            var geturl = posturl;
            //var data = {};
            Framework.Ajax.GetView({}, geturl, function (results) {
                btn.removeClass("disabled");
                $(targetdom).replaceWith(results);
            });
        }
        else if (fun == 200) {
            var geturl = posturl;
            //var data = {};
            Framework.Ajax.GetView({}, geturl, function (result) {
                //btn.removeClass("disabled");
                $(tableselect).find("tbody").prepend(result);
            });
        }
        else {
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
                alert('操作成功');
                if (fun == 101 || fun == 102 || fun == 105 || fun == 111) {//保存并关闭--插入行(弹窗Get)
                    //var tbbody = $(tableselect).find("tbody");
                    var data = results.Data;
                    var targeturltemp = "?Item." + targeturlparamname + "=" + data.Item[targeturlparamname];
                    targeturl += targeturltemp;
                    //alert("111_");
                }
                else if (fun == 310 || fun == 311) {
                    var data = results.Data;
                    if ($("#Item_" + pkname).length > 0) {
                        if ($("#Item_" + pkname).val().length == 0)
                            $("#Item_" + pkname).val(data.Item[pkname]);
                    }

                    var targeturlparam = "?";
                    if (targeturlparamname.length > 0) {
                        var btnparamnameArr = targeturlparamname.split(",");
                        for (var m = 0; m < btnparamnameArr.length; m++) {
                            var getprename = "#Item_" + btnparamnameArr[m];
                            var setprename = "Item." + btnparamnameArr[m];
                            var val = $(masteditarea).find(getprename).val();
                            targeturlparam += setprename + "=" + val + "&";
                        }
                    }
                    targeturl = targeturl + targeturlparam;
                }
                if (fun == 300 || fun == 310) {
                    //300:新页:提交+Url查询(跳转)
                    window.location.href = targeturl;
                }
                else if (fun == 200) {
                    $(tableselect).find("tbody").prepend(result);
                }
                else {
                    Framework.Ajax.GetView({}, targeturl, function (results) {
                        if (fun == 101 || fun == 102 || fun == 105 || fun == 111 || fun == 112) {
                            if (fun == 111 || fun == 112)//弹窗:编辑-保存并关闭
                            {
                                //alert("111");
                                btn.removeClass("disabled");
                                //行替换
                                //tr.replaceWith(results);
                                //popup.find(".close").trigger("click");
                                ////////////////////////////////////////

                                var tablenew = $("<table>" + results + "</table>");
                                var trnew = tablenew.find("tr");
                                //tr.replaceWith(results);
                                tr.replaceWith(trnew);
                                //tr = trnew;
                                //alert(results);
                                //tr.replaceWith(trnew);
                                if (fun == 111) {
                                    popup.find(".close").trigger("click");
                                }
                                else {
                                    funback(trnew);
                                }
                                //window.location.reload();
                            }
                            else {
                                var tbbody = $(tableselect).find("tbody");
                                tbbody.prepend(results);
                                if (fun == 101)
                                    popup.find(".close").trigger("click");
                                else if (fun == 102) {//保存并添加
                                    var masteditareaDom = popup.find(masteditarea);
                                    Framework.Tool.reSet(masteditareaDom);
                                }
                                else if (fun == 105) {//保存并添加-插入行(新页)
                                    var masteditareaDom = $(masteditarea);
                                    Framework.Tool.reSet(masteditareaDom);
                                }
                            }
                        }
                        else if (fun == 301 || fun == 311) {
                            Framework.Ajax.GetView({}, targeturl, function (results) {
                                $(targetdom).replaceWith(results);
                            });
                        }
                    });
                }
                return;
                if (fun == 112)//保存并编辑-替换行(弹窗Get)
                {
                    var data = {};//targeturlparamname
                    var targeturlparamnameArr = targeturlparamname.split(",");
                    for (var m = 0; m < targeturlparamnameArr.length; m++) {
                        var getprename = "#Item_" + targeturlparamnameArr[m];
                        var setprename = "Item." + targeturlparamnameArr[m];
                        var val = $(masteditarea).find(getprename).val();
                        data[setprename] = val;
                    }
                    Framework.Ajax.GetView(data, targeturl, function (results) {
                        btn.removeClass("disabled");
                        //行替换
                        var tablenew = $("<table>" + results + "</table>");
                        var trnew = tablenew.find("tr");
                        //tr.replaceWith(results);
                        //tr.replaceWith(trnew);
                        //tr = trnew;
                        tr.replaceWith(trnew);
                        //tr = trnew;
                        funback(trnew);
                    });
                }
                    //new TextValue{Text="保存并返回-(新页)",Value="121"},
                    //new TextValue{Text="保存并编辑-(新页)",Value="122"},
                else if (fun == 121)//保存并查询-ALink-(新页)
                {
                    var data = results.Data;
                    if ($("#Item_" + pkname).length > 0) {
                        if ($("#Item_" + pkname).val().length == 0)
                            $("#Item_" + pkname).val(data.Item[pkname]);
                    }

                    var targeturlparam = "?";
                    if (targeturlparamname.length > 0) {
                        var btnparamnameArr = targeturlparamname.split(",");
                        for (var m = 0; m < btnparamnameArr.length; m++) {
                            var getprename = "#Item_" + btnparamnameArr[m];
                            var setprename = "Item." + btnparamnameArr[m];
                            var val = $(masteditarea).find(getprename).val();
                            targeturlparam += setprename + "=" + val + "&";
                        }
                    }
                    var urltemp = targeturl + targeturlparam;
                    window.location.href = urltemp;
                }
                else if (fun == 123)//保存并查询-Ajax
                {
                    var data = results.Data;
                    if ($("#Item_" + pkname).length > 0) {
                        if ($("#Item_" + pkname).val().length == 0)
                            $("#Item_" + pkname).val(data.Item[pkname]);
                    }

                    var targeturlparam = "?";
                    if (targeturlparamname.length > 0) {
                        var btnparamnameArr = targeturlparamname.split(",");
                        for (var m = 0; m < btnparamnameArr.length; m++) {
                            var getprename = "#Item_" + btnparamnameArr[m];
                            var setprename = "Item." + btnparamnameArr[m];
                            var val = $(masteditarea).find(getprename).val();
                            targeturlparam += setprename + "=" + val + "&";
                        }
                    }
                    var urltemp = targeturl + targeturlparam;
                    //window.location.href = urltemp;
                    Framework.Ajax.GetView({}, urltemp, function (results) {
                        btn.removeClass("disabled");
                        btn.closest('.operArea').find(targetdom).html(results);
                        //$(targetdom).html(results);
                        //Framework.Tool.PopupEvenBind();
                    });
                }
                else if (fun == 122) {//保存并编辑-(新页)
                    //保持不变，也可以使用121完成
                }
            })
            return false;
        }
    };

    if (Framework.Tool.isUndefined(configinfo)) {//不进行操作提示
        Framework.Tool.FwBtnSubmitBulid();
    }
    else {
        var params = {
            title: '【操作确认】',
            width: 400,
            message: configinfo + "，你确定要执行操作吗？",
            onConfirm: function (popup) {
                Framework.Tool.FwBtnSubmitBulid();
            },
            onCancel: function (popup) {
                //alert("sdsd取消");
                btn.removeClass("disabled");
            }
        };
        Framework.UI.Modal(params);
    }
    //alert("eeeeeeeee");
    return false;
}

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

//////////////////Start：自定义查询条件///////////////////////////////////////////////
//自定义：查询条件
$(document).delegate('.queryfieldname', 'change', function (e) {
    //alert("sdsdwe");
    //查找行
    var obj = $(this);
    var tr = obj.closest('tr');
    var targetdom = tr.find(".targetdom");//目标Dom

    var fieldname = obj.val();
    //判断是否需要下拉列表框
    var strresults = "<input type='text' class='form-control' name='Querys[{0}].Value' value='' />";

    if (!Framework.Tool.QueryFieldNameConst.contain(fieldname)) {
        targetdom.html(strresults);
        return;
    }
    var fieldnameItem = Framework.Tool.QueryFieldNameConst.get(fieldname);
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

//下拉列表框改变事件
$(document).delegate('select', 'change', function (e) {
    var obj = $(this);
    Framework.Tool.selectchange(obj);
});

Framework.Tool.selectchange = function (obj) {
    var pkname = obj.data("pkname");
    if (Framework.Tool.isUndefined(pkname)) {
        pkname = obj.attr("name");
    }
    var url = obj.data("changeurl");
    if (Framework.Tool.isUndefined(url))
        return;
    var optionlabel = obj.data("optionlabel");
    //alert();
    var pageArea = obj.closest(".operArea");
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
                var subchangeurl = targetdom.data("changeurl");

                if (!Framework.Tool.isUndefined(subchangeurl)) {
                    Framework.Tool.selectchange(targetdom);
                }
            });
        }
        else {
            targetdom.html(strOptions);
        }
    }
};

$(document).delegate('.btn-FwPopup', 'click', function (e) {
    Framework.Tool.Popup($(this));
});
//data-tableselect
Framework.Tool.Popup = function (obj) {
    //alert("sds");
    //var popupArea = obj.closest('.popupArea');
    //var tableid = obj.data("tableid");
    var tableid = obj.data("tableselect");
    if (Framework.Tool.isUndefined(tableid) || tableid.length == 0)
        tableid = "#tbList";

    var tabledom = $(tableid);
    var popupurl = obj.data("popupurl");
    //var popupurlInit = popupurl + "Init";
    popupurlInit = popupurl;

    //pkname|Fra_ProductPriceID,popupaddrepeat|1,paramname|Pre_UserID
    var paramname = obj.data("paramname");//参数
    var paramnameArrs = "";
    if (Framework.Tool.isUndefined(popuppk))
    {
        paramname = "";
    }
    else
        paramnameArrs = paramname.split(",");

    var popupaddrepeat = obj.data("popupaddrepeat");
    var rowsurl = obj.data("targeturl");

    var refdataarea = obj.data("refdataarea");
    //var targetdom = data("targetdom");
    //rows的主键
    var pk = obj.data("pkname");
    var pklower = pk.toLowerCase();

    //popup过滤的主键
    var popuppk = obj.data("popuppkname");
    if (Framework.Tool.isUndefined(popuppk) || popuppk.length == 0)
        popuppk = pk;
    var popuppklower = popuppk.toLowerCase();

    var Title = obj.data("popuptitle");
    if (Framework.Tool.isUndefined(Title))
        Title = "选择";
    var width = obj.data("selectpopupwidth");
    if (Framework.Tool.isUndefined(width))
        width = 1000;
    //FunNameEn
    //var orgTbBodyID = tabledom.data("orgtbbodyid");
    /////////////////////////获取界面中：订单明细列表的产品列表信息////////////////
    var GetListEles = function (data) {
        if (!Framework.Tool.isUndefined(refdataarea)) {
            Framework.Tool.GetEditAreaData(data, $(refdataarea));
        }

        //var sss = paramnameArrs;
        if (paramname.length > 0)
        {
            for (var i = 0; i < paramnameArrs.length; i++)
            {
                //Item_Pre_UserID
                var idtemp = "#Item_" + paramnameArrs[i];
                var idtemp1="Item."+paramnameArrs[i];
                data[idtemp1] = $(idtemp).val();
            }
        }

        if (popupaddrepeat == 0) {
            var IDs = "";
            //popuppk  popuppklower
            tabledom.find("tbody>tr").each(function (i) {
                debugger;
                IDs += $(this).data(popuppklower) + ',';
            });
            if (IDs.length > 0) {
                IDs = IDs.substr(0, IDs.length - 1);
                var pks = "Item." + popuppk + "s";
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
                popup.find(".btn-info").removeClass("disabled");
            });
        });
        //alert();
        $(window).resize();
    };

    Framework.SearchPopup = function (currobj, params) {
        //alert("Framework.SearchPopup ");
        var operArea = currobj.closest('.operArea');
        var targetdom = operArea.data("targetdom");
        var targetdom = currobj.closest(targetdom);

        var data = {};
        var row = 0;
        //(1)获取快速找到查询条件：快速查询、高级查询
        GetListEles(data);

        if (operArea.find(".generalWhere").length > 0)//通用查询条件，一般针对必填
        {
            var parentDomTemp = operArea.find(".generalWhere");
            data = Framework.Tool.GetEditAreaData(data, parentDomTemp);
        }

        if (operArea.find(".fastWhere").length > 0)
            row = Framework.Tool.GetQuerys(data, operArea.find(".fastWhere"), row);
        //(2)获取更多查询条件
        //row++;
        if (operArea.find(".moreWhere").length > 0)
            row = Framework.Tool.GetQuerys(data, operArea.find(".moreWhere"), row);
        //(3)获取自定义查询条件
        //row++;
        if (operArea.find(".custWhere").length > 0)
            Framework.Tool.GetCustWhere(data, ".custWhere", row)
        var url = popupurl;

        //var url = operArea.data("url");
        var bulidurl = Framework.BulidSearchDataOrUrl(operArea, data, params, url, 1);

        //var bulidurl = Framework.Page.BulidSearchWhereData(pageArea, data, params, url, 1);
        Framework.Ajax.GetView(data, url, function (result) {//PostView
            targetdom.replaceWith(result);
            //$(targetdom).replaceWith(result);
            //$(targetdom).html(result);
            //popup.find(".btn-info").removeClass("disabled");
            $(window).resize();
        });
    };

    /////////////////////////选中商品后的保存方法/////////////////////////////
    var SelectProduct = function (popup) {
        //在popop中查找选中的数据
        var checkeds = popup.find('.jq-checkall-item:checked');
        if (checkeds.length == 0) {
            alert("请选择x");
            return;
        }
        //popup.find(".btn-info").addClass("disabled");

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
            Framework.Tool.GetEditAreaData(data, $(refdataarea));
            //data = Framework.Form.GetFormItemByObj($(refdataarea));
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
            popup.find(".btn-info").removeClass("disabled");

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
    //Framework.Ajax.GetView(data, url, function (results) {
    //    btn.removeClass("disabled");
    //    btn.closest('.operArea').find(targetdom).html(results);
    //    //$(targetdom).html(results);
    //    Framework.Tool.PopupEvenBind();
    //});
    Framework.Ajax.GetView(data, popupurlInit, function (result) {
        //alert(result);
        var params = {
            title: Title,
            width: width,
            message: result,
            onConfirm: function (popup, currobj) {
                //var btn = $(this);
                //alert("FwBtnSubmit");
                //判断是否为关闭
                if (currobj.hasClass("btn-close")) {
                    popup.find(".close").trigger("click");
                    return;
                }
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
        Framework.UI.FormModalSelectPopup(params);
        //Framework.UI.FormModalMySearchPopup(params);
    }
);
};

//全选与反选
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

//文本框失去焦点，计算列
//table列计算
$(document).delegate('.calele', 'blur', function (e) {
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
            //objdom.is();
            if (objdom.is('input')) {
                objdom.val(calval);
                objdom.removeClass("validaterrorpro");
                objdom.attr({ "title": "" });
                var passtemp = Framework.UI.Behavior.FormEleValidate(objdom);
            }
            else //if (domxx.is('span'))
            {
                objdom.html(calval);
            }
            //if (objgs[1] == "3") {
            //    if (objgs[2] == "val")
            //        objdom.val(calval);
            //}
            //else if (objgs[1] == "5") {
            //    var valt = calval.toString().fmMoney(2);
            //    if (objgs[2] == "val")
            //        objdom.val(valt);
            //    else if (objgs[2] == "html")
            //        objdom.html(valt);
            //}
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

Framework.Tool.CalculationColNew = function (calcol, tableDom) {
    //alert();
    //var calcol = obj.data("calcol");
    var table = tableDom;// obj.closest('table');
    var arrcols = calcol.split(",");//计算列个数数组
    for (var i = 0; i < arrcols.length; i++) {
        var arr = arrcols[i].split("|");
        var trs = table.find(arr[0]);
        var sum = 0;
        var errorMessage = "";
        //计算列
        trs.each(function (i) {
            var val = "";
            var domyy = $(this);
            if (domyy.is('input')) {
                val = $(this).val();
            }
            else
                val = $(this).html();
            //var val = $(this).val();//.val();
            //判断是否为数值
            val = Number(val);
            //price = Number(price);
            if (!Framework.Tool.isNumber(val)) {
                errorMessage += Framework.Const.ValidationMessageFormatConst.NumberType();
                alert(errorMessage);
            }
            if (errorMessage.length == 0) {
                sum += val;
                //sumPrice += val * price;
            }
        });
        if (arr[2] == "5") {
            sum = sum.toString().fmMoney(2);
            //table.find("." + arr[0] + 'Total').html(valt);
        }
        var domxx = table.find(arr[0] + 'Total');
        if (domxx.length > 0) {

            //domxx.is('select')
            if (domxx.is('input')) {
                domxx.val(sum);
                domxx.removeClass("validaterrorpro");
                domxx.attr({ "title": "" });
                var passtemp = Framework.UI.Behavior.FormEleValidate(domxx);
            }
            else //if (domxx.is('span'))
            {
                domxx.html(sum);
            }
        }
    }
};

$(document).delegate(".js_close", "click", function () {

    var panel = $(this).parents(".kPanel_wrap");
    var btn = $(this);

    if (panel.attr("running")) {
        return false;
    }
    panel.attr("running", true);

    if (panel.hasClass("closePanel")) {
        panel.find(".kPanel_content_wrap").stop().slideDown("200", function () {
            panel.removeAttr("running");
            btn.addClass("dropup");
        });
        panel.removeClass("closePanel");
    } else {
        panel.find(".kPanel_content_wrap").stop().slideUp("200", function () {
            panel.removeAttr("running");
            btn.removeClass("dropup");
            panel.addClass("closePanel");
        });
    }
});

/**
* 显示数字动态标题
*/
var showTitle = function () {
    var oldText = $(".kPanel_wrap:first").find(".kPanel_title_text").text();
    $(".kPanel_wrap").each(function (i) {
        var title = $(this).find(".kPanel_title_text");
        var titleText = oldText.replace("{i}", i + 1);
        title.text(titleText);
    });
}

Framework.Tool.FileSizeDisp = function (filesize) {
    var HandoutFileSize = filesize;
    var HandoutFileSizeDisp = "";
    if (HandoutFileSize > 1024 * 1024 * 1024) {
        HandoutFileSizeDisp = (HandoutFileSize / (1024 * 1024 * 1024)) + "G";
    }
    if (HandoutFileSize > 1024 * 1024) {
        HandoutFileSizeDisp = (HandoutFileSize / (1024 * 1024)) + "M";
    }
    else if (HandoutFileSize > 1024) {
        HandoutFileSizeDisp = (HandoutFileSize / (1024)) + "K";
    }
    return HandoutFileSizeDisp;
}

Framework.Tool.uploadifyRow = function (dom) {
    //var dom = $("#uploadify");
    var IdentValue = "";

    var operArea = dom.closest(".operArea");
    var tbList = operArea.find("#tbList");
    //IdentName:1|ProductNo
    var IdentName = dom.data("identname");
    if (IdentName != "") {
        var IdentNames = IdentName.split("|");
        if (IdentNames[0] == 1)
            IdentValue = $("#" + IdentNames[1]).val();
        else
            IdentValue = $("." + IdentNames[1]).html();
    }

    //IdentValue = "aaaaaaaaa";

    //$("#" + IdentName)
    dom.uploadify({
        'buttonImage': '/Content/images/btn.png',
        //'buttonImg': '/Content/images/btn.png', //浏览按钮的图片的路径。
        'buttonText': '浏览文件',
        //'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
        'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
        'wmode': 'transparent', //设置该项为transparent 可以使浏览按钮的flash背景文件透明，并且flash文件会被置为页面的最高层。默认值：opaque 。

        //'auto': true, //设置为true当选择文件后就直接上传了，为false需要点击上传按钮才上传
        'auto': true,
        'successTimeout': 99999,
        //'uploader': '/Scripts/uploadify.swf', //上传控件的主体文件，flash控件
        'swf': '/Scripts/uploadify.swf',//上传控件的主体文件，flash控件
        'queueID': 'uploadfileQueue',
        //'script': '/Ashx/UploadHander.ashx', //相对路径的后端脚本，它将处理您上传的文件。
        'uploader': '/Ashx/UploadHander.ashx',//相对路径的后端脚本，它将处理您上传的文件。
        //'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024 * 1024, //控制上传文件的大小，单位byte
        'fileSizeLimit': '0',//控制上传文件的大小，单位byte
        //'fileDesc': '只允许上传doc,txt,docx,pdf,ppt,xls,psd,cdr,swf,zip,rar,wmv,avi,rm,rmvb,ram,mpg,mpeg,3gp,mov,mp4,mkv,flv,vob文件', //出现在上传对话框中的文件类型描述。与fileExt需同时使用
        //'fileExt': '*.doc;*.txt;*.docx;*.pdf;*.ppt;*.xls;*.psd;*.cdr;*.swf;*.zip;*.rar;*.wmv;*.avi;*.rm;*.rmvb;*.ram;*.mpg;*.mpeg;*.3gp;*.mov;*.mp4;*.mkv;*.flv;*.vob', //设置可以选择的文件的类型
        //'fileExt': dom.data("browerext"),// '*.psd;*.jpeg;*.jpg', //设置可以选择的文件的类型
        'fileTypeDesc': dom.data("browerext"),
        'fileTypeExts': dom.data("browerext"),// '*.gif; *.jpeg; *.jpg; *.png',

        'method': 'get', //提交方式Post 
        'removeCompleted': true, //队列上传完后不消失
        //'height': 50,
        //'width': 180,

        //'multi': false, //设置为true时可以上传多个文件。
        'multi': false,
        //'queueID': 'uploadifydiv', //文件队列的ID，该ID与存放文件队列的div的ID一致。
        'queueSizeLimit': 1,//限制在一次队列中的次数（可选定几个文件）。
        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'folder': dom.data("folder"),
        'formData': { 'IdentValue': IdentValue, 'folder': dom.data("folder") },//这里只能传静态参数  
        //'fileTypeExts': '*.rar;*.zip;*.7z;*.jpg;*.jpge;*.gif;*.png',  
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onClearQueue': function (queueItemCount) {
            alert("取消上传");
            return;
        },
        //'onQueueComplete': function (queueData) {
        //    //alert("文件上传成功！");
        //    return;
        //},
        'onUploadStart': function (file) {
            //alert("上传");
            //$("#uploadify").uploadify("settings", "formData", { 'abcd': "asdds" });
            //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
        },

        'onUploadSuccess': function (fileObj, resp, response) {//': function (queueData) {//(event, queueId, fileObj, response, data) {
            //alert("---------");
            var geturl = dom.data("url");// "/P_ProductAreas/P_Attachment/Row";
            var pkname = dom.data("pkname");
            var data = {};
            var pknameValue = $("#Item_" + pkname).val();
            if (pknameValue.length == 0) {
                alert("附件Guid不能为空");
                return;
            }
            var pknameItemName = "Item.RefPKTableGuid";
            data[pknameItemName] = pknameValue;// $("#Item_Fra_StoreID").val();

            var filenameItemName = "Item." + dom.data("filename");
            data[filenameItemName] = fileObj.name;

            var fileguidItemName = "Item." + dom.data("fileguid");
            data[fileguidItemName] = resp;

            var fileextItemName = "Item." + dom.data("fileext");
            data[fileextItemName] = fileObj.type;

            var filesizeItemName = "Item." + dom.data("filesize");
            data[filesizeItemName] = fileObj.size;
            //data["Item.PAttachmentFileSizeDisp"] = response;

            Framework.Ajax.GetView(data, geturl, function (result) {
                //alert(result);
                tbList.find("tbody").prepend(result);;
                //$("#tbList").find("tbody").prepend(result);
            });
        }
    });
};

//Framework.Tool.uploadifyRow = function () {
//    var dom = $("#uploadify");
//    var IdentValue = "";
//    //IdentName:1|ProductNo
//    var IdentName = dom.data("identname");
//    if (IdentName != "") {
//        var IdentNames = IdentName.split("|");
//        if (IdentNames[0] == 1)
//            IdentValue = $("#" + IdentNames[1]).val();
//        else
//            IdentValue = $("." + IdentNames[1]).html();
//    }

//    //IdentValue = "aaaaaaaaa";

//    //$("#" + IdentName)
//    jQuery("#uploadify").uploadify({
//        'buttonImage': '/Content/images/btn.png',
//        //'buttonImg': '/Content/images/btn.png', //浏览按钮的图片的路径。
//        'buttonText': '浏览文件',
//        //'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
//        'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
//        'wmode': 'transparent', //设置该项为transparent 可以使浏览按钮的flash背景文件透明，并且flash文件会被置为页面的最高层。默认值：opaque 。

//        //'auto': true, //设置为true当选择文件后就直接上传了，为false需要点击上传按钮才上传
//        'auto': true,
//        'successTimeout': 99999,
//        //'uploader': '/Scripts/uploadify.swf', //上传控件的主体文件，flash控件
//        'swf': '/Scripts/uploadify.swf',//上传控件的主体文件，flash控件
//        'queueID': 'uploadfileQueue',
//        //'script': '/Ashx/UploadHander.ashx', //相对路径的后端脚本，它将处理您上传的文件。
//        'uploader': '/Ashx/UploadHander.ashx',//相对路径的后端脚本，它将处理您上传的文件。
//        //'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024 * 1024, //控制上传文件的大小，单位byte
//        'fileSizeLimit': '0',//控制上传文件的大小，单位byte
//        //'fileDesc': '只允许上传doc,txt,docx,pdf,ppt,xls,psd,cdr,swf,zip,rar,wmv,avi,rm,rmvb,ram,mpg,mpeg,3gp,mov,mp4,mkv,flv,vob文件', //出现在上传对话框中的文件类型描述。与fileExt需同时使用
//        //'fileExt': '*.doc;*.txt;*.docx;*.pdf;*.ppt;*.xls;*.psd;*.cdr;*.swf;*.zip;*.rar;*.wmv;*.avi;*.rm;*.rmvb;*.ram;*.mpg;*.mpeg;*.3gp;*.mov;*.mp4;*.mkv;*.flv;*.vob', //设置可以选择的文件的类型
//        //'fileExt': dom.data("browerext"),// '*.psd;*.jpeg;*.jpg', //设置可以选择的文件的类型
//        'fileTypeDesc': dom.data("browerext"),
//        'fileTypeExts': dom.data("browerext"),// '*.gif; *.jpeg; *.jpg; *.png',

//        'method': 'get', //提交方式Post 
//        'removeCompleted': true, //队列上传完后不消失
//        //'height': 50,
//        //'width': 180,

//        //'multi': false, //设置为true时可以上传多个文件。
//        'multi': false,
//        //'queueID': 'uploadifydiv', //文件队列的ID，该ID与存放文件队列的div的ID一致。
//        'queueSizeLimit': 1,//限制在一次队列中的次数（可选定几个文件）。
//        'progressData': 'speed',
//        'overrideEvents': ['onDialogClose'],
//        'folder': dom.data("folder"),
//        'formData': { 'IdentValue': IdentValue, 'folder': dom.data("folder") },//这里只能传静态参数  
//        //'fileTypeExts': '*.rar;*.zip;*.7z;*.jpg;*.jpge;*.gif;*.png',  
//        'onSelectError': function (file, errorCode, errorMsg) {
//            switch (errorCode) {
//                case -100:
//                    alert("上传的文件数量已经超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
//                    break;
//                case -110:
//                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
//                    break;
//                case -120:
//                    alert("文件 [" + file.name + "] 大小异常！");
//                    break;
//                case -130:
//                    alert("文件 [" + file.name + "] 类型不正确！");
//                    break;
//            }
//        },
//        'onClearQueue': function (queueItemCount) {
//            alert("取消上传");
//            return;
//        },
//        //'onQueueComplete': function (queueData) {
//        //    //alert("文件上传成功！");
//        //    return;
//        //},
//        'onUploadStart': function (file) {
//            //alert("上传");
//            //$("#uploadify").uploadify("settings", "formData", { 'abcd': "asdds" });
//            //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
//        },

//        'onUploadSuccess': function (fileObj, resp, response) {//': function (queueData) {//(event, queueId, fileObj, response, data) {
//            //alert("---------");
//            var geturl = dom.data("url");// "/P_ProductAreas/P_Attachment/Row";
//            var pkname = dom.data("pkname");
//            var data = {};
//            var pknameValue = $("#Item_" + pkname).val();
//            if (pknameValue.length == 0) {
//                alert("主键表不能为空");
//                return;
//            }
//            var pknameItemName = "Item." + pkname;
//            data[pknameItemName] = pknameValue;// $("#Item_Fra_StoreID").val();

//            var filenameItemName = "Item." + dom.data("filename");
//            data[filenameItemName] = fileObj.name;

//            var fileguidItemName = "Item." + dom.data("fileguid");
//            data[fileguidItemName] = resp;

//            var fileextItemName = "Item." + dom.data("fileext");
//            data[fileextItemName] = fileObj.type;

//            var filesizeItemName = "Item." + dom.data("filesize");
//            data[filesizeItemName] = fileObj.size;
//            //data["Item.PAttachmentFileSizeDisp"] = response;

//            Framework.Ajax.GetView(data, geturl, function (result) {
//                //alert(result);
//                $("#tbList").find("tbody").prepend(result);
//            });
//        }
//    });
//};


//Framework.Tool.uploadifyRow = function () {
//    var dom = $("#uploadify");
//    //$("#file_upload").uploadify("settings", "formData", { 'ctrlid': ctrlid })

//    $("#uploadify").uploadify({
//        'uploader': '/Scripts/uploadify.swf', //上传控件的主体文件，flash控件
//        'script': '/Ashx/UploadHander.ashx', //相对路径的后端脚本，它将处理您上传的文件。
//        'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
//        'folder': dom.data("folder"),//'@Url.Content("/Files/ProductFiles")', //您想将文件保存到的路径。
//        'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024 * 1024, //控制上传文件的大小，单位byte
//        'queueSizeLimit': 1, //限制在一次队列中的次数（可选定几个文件）。
//        'fileDesc': '只允许上传doc,txt,docx,pdf,ppt,xls,psd,cdr,swf,zip,rar,wmv,avi,rm,rmvb,ram,mpg,mpeg,3gp,mov,mp4,mkv,flv,vob文件', //出现在上传对话框中的文件类型描述。与fileExt需同时使用
//        //'fileExt': '*.doc;*.txt;*.docx;*.pdf;*.ppt;*.xls;*.psd;*.cdr;*.swf;*.zip;*.rar;*.wmv;*.avi;*.rm;*.rmvb;*.ram;*.mpg;*.mpeg;*.3gp;*.mov;*.mp4;*.mkv;*.flv;*.vob', //设置可以选择的文件的类型
//        'fileExt': dom.data("browerext"),// '*.psd;*.jpeg;*.jpg', //设置可以选择的文件的类型
//        'method': 'get', //提交方式Post 
//        'queueID': 'uploadifydiv', //文件队列的ID，该ID与存放文件队列的div的ID一致。
//        'buttonImg': '/Content/images/btn.png', //浏览按钮的图片的路径。
//        'buttonText':'浏览文件',
//        'wmode': 'transparent', //设置该项为transparent 可以使浏览按钮的flash背景文件透明，并且flash文件会被置为页面的最高层。默认值：opaque 。
//        'auto': true, //设置为true当选择文件后就直接上传了，为false需要点击上传按钮才上传
//        'multi': false, //设置为true时可以上传多个文件。
//        'removeCompleted': true, //队列上传完后不消失
//        //'formData': {'searid':'E001_00001'},//JSON格式上传每个文件的同时提交到服务器的额外数据，可在’onUploadStart’事件中使用’settings’方法动态设置。
//        'height': 50,
//        'width': 180,
//        'onUploadStart': function (file) {
//            alert();
//            $("#uploadify").uploadify("settings", "formData", { 'ctrlid': 'E001_00001' });
//            //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
//        },
//        'onSelect': function (e, queueId, fileObj) {
//        },
//        'onComplete': function (event, queueId, fileObj, response, data) {
//            var geturl = dom.data("url");// "/P_ProductAreas/P_Attachment/Row";
//            var pkname = dom.data("pkname");
//            var data = {};
//            var pknameValue = $("#Item_" + pkname).val();
//            if (pknameValue.length == 0) {
//                alert("主键表不能为空");
//                return;
//            }
//            var pknameItemName = "Item." + pkname;
//            data[pknameItemName] = pknameValue;// $("#Item_Fra_StoreID").val();

//            var filenameItemName = "Item." + dom.data("filename");
//            data[filenameItemName] = fileObj.name;

//            var fileguidItemName = "Item." + dom.data("fileguid");
//            data[fileguidItemName] = response;

//            var fileextItemName = "Item." + dom.data("fileext");
//            data[fileextItemName] = fileObj.type;

//            var filesizeItemName = "Item." + dom.data("filesize");
//            data[filesizeItemName] = fileObj.size;
//            //data["Item.PAttachmentFileSizeDisp"] = response;

//            Framework.Ajax.GetView(data, geturl, function (result) {
//                //alert(result);
//                $("#tbList").find("tbody").prepend(result);
//            });
//        }
//    });
//    //alert();
//    //dom.attr("width", 180);
//    //dom.attr("heigth", 40);
//    //$("#uploadify").uploadify("settings", "formData", { 'ctrlid': "E001_00001" });

//}


Framework.Tool.uploadifyEdit = function (dom) {
    //var dom = $("#uploadify");
    var IdentValue = "";

    //var operArea = dom.closest(".operArea");
    //var tbList = operArea.find("#tbList");
    ////IdentName:1|ProductNo
    var IdentName = dom.data("identname");
    if (!Framework.Tool.isUndefined(IdentName)) {
        if (IdentName != "") {
            var IdentNames = IdentName.split("|");
            if (IdentNames[0] == 1)
                IdentValue = $("#" + IdentNames[1]).val();
            else
                IdentValue = $("." + IdentNames[1]).html();
        }
    }

    //data-folder="/Files/ProductFiles" 
    //data-browerext="*.psd;*.jpeg;*.jpg"  

    //data-img="ProductMainImage"  
    //data-filename="ProductMainImageFileName"  
    //data-filepath="ProductMainImageFilePath"

    //$("#" + IdentName)
    dom.uploadify({
        'buttonImage': '/Content/images/btn.png',
        //'buttonImg': '/Content/images/btn.png', //浏览按钮的图片的路径。
        'buttonText': '浏览文件',
        //'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
        'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
        'wmode': 'transparent', //设置该项为transparent 可以使浏览按钮的flash背景文件透明，并且flash文件会被置为页面的最高层。默认值：opaque 。

        //'auto': true, //设置为true当选择文件后就直接上传了，为false需要点击上传按钮才上传
        'auto': true,
        'successTimeout': 99999,
        //'uploader': '/Scripts/uploadify.swf', //上传控件的主体文件，flash控件
        'swf': '/Scripts/uploadify.swf',//上传控件的主体文件，flash控件
        'queueID': 'uploadfileQueue',
        //'script': '/Ashx/UploadHander.ashx', //相对路径的后端脚本，它将处理您上传的文件。
        'uploader': '/Ashx/UploadHander.ashx',//相对路径的后端脚本，它将处理您上传的文件。
        //'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024 * 1024, //控制上传文件的大小，单位byte
        'fileSizeLimit': '0',//控制上传文件的大小，单位byte
        //'fileDesc': '只允许上传doc,txt,docx,pdf,ppt,xls,psd,cdr,swf,zip,rar,wmv,avi,rm,rmvb,ram,mpg,mpeg,3gp,mov,mp4,mkv,flv,vob文件', //出现在上传对话框中的文件类型描述。与fileExt需同时使用
        //'fileExt': '*.doc;*.txt;*.docx;*.pdf;*.ppt;*.xls;*.psd;*.cdr;*.swf;*.zip;*.rar;*.wmv;*.avi;*.rm;*.rmvb;*.ram;*.mpg;*.mpeg;*.3gp;*.mov;*.mp4;*.mkv;*.flv;*.vob', //设置可以选择的文件的类型
        //'fileExt': dom.data("browerext"),// '*.psd;*.jpeg;*.jpg', //设置可以选择的文件的类型
        'fileTypeDesc': dom.data("browerext"),
        'fileTypeExts': dom.data("browerext"),// '*.gif; *.jpeg; *.jpg; *.png',

        'method': 'get', //提交方式Post 
        'removeCompleted': true, //队列上传完后不消失
        //'height': 50,
        //'width': 180,

        //'multi': false, //设置为true时可以上传多个文件。
        'multi': false,
        //'queueID': 'uploadifydiv', //文件队列的ID，该ID与存放文件队列的div的ID一致。
        'queueSizeLimit': 1,//限制在一次队列中的次数（可选定几个文件）。
        'progressData': 'speed',
        'overrideEvents': ['onDialogClose'],
        'folder': dom.data("folder"),
        'formData': { 'IdentValue': IdentValue, 'folder': dom.data("folder") },//这里只能传静态参数  
        //'fileTypeExts': '*.rar;*.zip;*.7z;*.jpg;*.jpge;*.gif;*.png',  
        'onSelectError': function (file, errorCode, errorMsg) {
            switch (errorCode) {
                case -100:
                    alert("上传的文件数量已经超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                    break;
                case -110:
                    alert("文件 [" + file.name + "] 大小超出系统限制的" + jQuery('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                    break;
                case -120:
                    alert("文件 [" + file.name + "] 大小异常！");
                    break;
                case -130:
                    alert("文件 [" + file.name + "] 类型不正确！");
                    break;
            }
        },
        'onClearQueue': function (queueItemCount) {
            alert("取消上传");
            return;
        },
        //'onQueueComplete': function (queueData) {
        //    //alert("文件上传成功！");
        //    return;
        //},
        'onUploadStart': function (file) {
            //alert("上传");
            //$("#uploadify").uploadify("settings", "formData", { 'abcd': "asdds" });
            //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
        },

        'onUploadSuccess': function (fileObj, resp, response) {//': function (queueData) {//(event, queueId, fileObj, response, data) {
            //data-img="ProductMainImage"  
            //data-filename="ProductMainImageFileName"  
            //data-filepath="ProductMainImageFilePath"

            var filename = "#Item_" + dom.data("filename");
            $(filename).val(fileObj.name);

            var filepath = "#Item_" + dom.data("filepath");
            $(filepath).val(resp);
            var img = filepath + "Img";
            if ($(img).length > 0)
                $(img).attr("src", resp);

            //            var img = "#" + dom.data("img");
            //            var fileguid = "#" + dom.data("fileguid");
            //            $(fileguid).val(response);
            //            $(img).attr("src", response);
        }
    });
};

//Framework.Tool.uploadifyEdit = function () {
//    var dom = $("#uploadify");
//    $("#uploadify").uploadify({
//        'uploader': '/Scripts/uploadify.swf', //上传控件的主体文件，flash控件
//        'script': '/Ashx/UploadHander.ashx', //相对路径的后端脚本，它将处理您上传的文件。
//        'cancelImg': '/Content/images/cancel.png', //取消按钮。设定图片路径。默认cancel.png
//        'folder': dom.data("folder"), //您想将文件保存到的路径。
//        'sizeLimit': 1024 * 1024 * 1024 * 1024 * 1024 * 1024, //控制上传文件的大小，单位byte
//        'queueSizeLimit': 1, //限制在一次队列中的次数（可选定几个文件）。
//        'fileDesc': '只允许上传' + dom.data("browerext") + '文件', //出现在上传对话框中的文件类型描述。与fileExt需同时使用
//        //'fileExt': '*.doc;*.txt;*.docx;*.pdf;*.ppt;*.xls;*.psd;*.cdr;*.swf;*.zip;*.rar;*.wmv;*.avi;*.rm;*.rmvb;*.ram;*.mpg;*.mpeg;*.3gp;*.mov;*.mp4;*.mkv;*.flv;*.vob', //设置可以选择的文件的类型
//        'fileExt': dom.data("browerext"), //设置可以选择的文件的类型
//        'method': 'get', //提交方式Post 
//        'queueID': 'uploadifydiv', //文件队列的ID，该ID与存放文件队列的div的ID一致。
//        'buttonImg': '/Content/images/btn.png', //浏览按钮的图片的路径。
//        'wmode': 'transparent', //设置该项为transparent 可以使浏览按钮的flash背景文件透明，并且flash文件会被置为页面的最高层。默认值：opaque 。
//        'auto': true, //设置为true当选择文件后就直接上传了，为false需要点击上传按钮才上传
//        'multi': false, //设置为true时可以上传多个文件。
//        'removeCompleted': true, //队列上传完后不消失
//        'onSelect': function (e, queueId, fileObj) {
//        },
//        'onComplete': function (event, queueId, fileObj, response, data) {
//            var img = "#" + dom.data("img");
//            var fileguid = "#" + dom.data("fileguid");
//            $(fileguid).val(response);
//            $(img).attr("src", response);
//        }
//    });
//}

$(document).ready(
    function () {
        if ($('.multiselect').length > 0) {
            //alert();
            $('.multiselect').multiselect({
                nonSelectedText: '请选择',
                buttonWidth: '180',
                onChange: function (element, checked) {
                    //element.text()
                }
            });
        }

        //if (!Framework.Tool.isUndefined(Framework.Tool.UeEditItems)) {
        //    var context = Framework.Tool.UeEditItem.ueEditEle.getContent();

        //    var name = "Item." + Framework.Tool.UeEditItem.name;
        //    data[name] = escape(context);
        //}


        //if ($("#ueEdit").length > 0) {
        //    //alert("ueEdit");
        //    var ue = UE.getEditor('ueEdit');

        //    var name = $("#ueEdit").data("name");
        //    //alert(name);
        //    Framework.Tool.UeEditItem = { name: name, ueEditEle: ue };
        //}

        if ($(".ueEdit").length > 0) {
            Framework.Tool.UeEditItems = {};
            $(".ueEdit").each(function (i) {
                var item = $(this);
                var itemID = item.attr("id");

                var ue = UE.getEditor(itemID);
                var name = item.data("name");
                //alert(name);
                Framework.Tool.UeEditItems[i] = { name: name, ueEditEle: ue };
            });
        }

        if ($(".uploadifyrow").length > 0) {
            //alert();
            $(".uploadifyrow").each(function (i) {
                var dom = $(this);
                Framework.Tool.uploadifyRow(dom);
            });


        }

        if ($(".uploadifyedit").length > 0) {
            $(".uploadifyedit").each(function (i) {
                var dom = $(this);
                Framework.Tool.uploadifyEdit(dom);
            });
        }
        //$('.datepicker').datepicker({ format: "yyyy-MM-dd" }).on('changeDate', function (ev) {
        //    $(this).datepicker('hide');
        //    $(this).removeClass("validaterrorpro");
        //    $(this).attr({ "title": "" });
        //    var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
        //});

        //日期插件调用
        if ($('.datetimepicker1').length > 0) {
            $('.datetimepicker1').datetimepicker({
                lang: 'ch',
                format: 'Y-m-d'  //时间格式化,默认方式
                , timepicker: false	  //是否显示时间选择面板
                , onSelectDate: function (ct) {
                    //console.log(ct);
                    //alert();
                    //选择日期回调函数
                    $(this).datepicker('hide');
                    $(this).removeClass("validaterrorpro");
                    $(this).attr({ "title": "" });
                    var passtemp = Framework.UI.Behavior.FormEleValidate($(this));
                }
            });
        }

        $(".scrollBar").scroll(function () {

            var temp = $(this).scrollTop();
            $(this).find(".lockhead").css("top", temp - 1);
            $(this).find(".lockhead2").css("top", temp - 2);
        });

        $(window).resize(function () {
            //$(window).resize();

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
                height += 30;
                thisDom.css("height", height);

            });
        });
    }
);
