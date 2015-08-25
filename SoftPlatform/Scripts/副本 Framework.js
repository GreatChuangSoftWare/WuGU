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

///获取table中数据
//data:原数据对象
//tbselect:表格选择器
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

    var trrows = $(tbselects[0] + ' tbody>tr');
    if (tbselects.length = 2)
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
        Framework.Tool.GetTrFormData(data, tr, row);
        row++;
    });
    return true;
};

//获取table中tr中的data-xxx数据，非集合数据
Framework.Tool.GetTrData = function (data,tr, tbodyDom) {
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
    if (!Framework.Tool.isUndefined(i))
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
    var url = "/" + controlAndaction;
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
        url: "/" + controlAndaction,
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
    var url = "/" + controlAndaction;
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
    var request = $.ajax({
        url: "/" + controlAndaction,
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
        url: "/" + controlAndaction,
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
        url: "/" + controlAndaction,
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
        url: "/" + controlAndaction,
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

/////////////////////////////////////////////////////////////
$(document).delegate('.btn-FwSearch', 'click', function (e) {
    var currobj = $(this);
    Framework.Tool.PageHelper(currobj, {});
})

Framework.FwSearch = function (currobj, params) {
    //alert("window调用");
    var operArea = currobj.closest('.operArea');
    var targetdom = operArea.data("targetdom");
    var data = {};
    var row = 0;
    //(1)获取快速找到查询条件：快速查询、高级查询
    if (operArea.find(".fastWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, operArea.find(".fastWhere"), row);
    //(2)获取更多查询条件
    if (operArea.find(".moreWhere").length > 0)
        row = Framework.Tool.GetQuerys(data, operArea.find(".moreWhere"), row);
    //(3)获取自定义查询条件
    //row++;
    //if (operArea.find(".custWhere").length > 0)
    //    Framework.Tool.GetTableColls(data, operArea.find(".custWhere"), row);

    var url = operArea.data("url");
    var bulidurl = Framework.BulidSearchDataOrUrl(operArea, data, params, url, 1);

    if (Framework.Tool.isUndefined(targetdom)) {
        window.location.href = bulidurl;
    }
    else {
        Framework.Ajax.GetView(data, url, function (result) {//PostView
            $(targetdom).html(result);
            $(window).resize();
        });
    }
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
}

//生成查询Data、Url
Framework.BulidSearchDataOrUrl = function (operArea, data, params, url, bformsubmit) {
    data["PageQueryBase.PageIndex"] = params.pageIndex || 1;
    var RankInfo = params.RankInfo || "";
    var bformsubmit = bformsubmit || 1;
    //(2)获取排序字段
    if (RankInfo.length == 0) {
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
    data["PageQueryBase.PageSize"] = operArea.find("#PageSize").val() || 10;

    //(5)生成url
    if (bformsubmit == 2)
        url = url + "?"
    else
        url = "/" + url + "?";
    for (itemx in data) {
        if (!!data[itemx] && data[itemx].toString().length > 0)
            url += itemx + "=" + data[itemx] + "&";
    }
    return url;
}

//////////////////Goto：跳转页/////////////////////////////////////////
$(document).delegate('.pageindexgoto', 'click', function (e) {
    var obj = $(this);
    var pageArea = $(obj).closest("#pageArea");
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
////提交
Framework.Tool.FwBtnSubmit = function (btn) {
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
        //data = Framework.Form.GetFormItemByObjNew(data, $(masteditarea));
        data = Framework.Tool.GetEditAreaData(data, $(masteditarea));
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
                window.location.href = "/"+ targeturl + targeturlparam;
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
$(document).delegate('.btn-FwBtnSubmit', 'click', function (e) {
    var btn = $(this);
    //alert("FwBtnSubmit");
    Framework.Tool.FwBtnSubmit(btn);
});

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
    str += "<form>";
    str += message;
    //str += '</div>';
    str += "</form>";
    str += '</div>';
    str += '</div>';
    //    str += '<div id="messageArea"></div>';
    str += '<div class="modal-footer">';
    str += '<button type="button" class="btn btn-default" data-dismiss="modal">' + cancelText + '</button>';
    if (!Framework.Tool.isUndefined(params.confirmText))
    {
        str += '<button type="button" class="btn btn-info btn-primary">' + params.confirmText + '</button>';
    }

    if (!Framework.Tool.isUndefined(params.confirmCloseText))
    {//保存并关闭
        str += '<button type="button" class="btn btn-Close">' + params.confirmCloseText + '</button>';
    }
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

//清空Jobj中表单控件值
Framework.Tool.reSet = function (Jobj) {
    var form = Jobj.find("form");
    if (form.length > 0)
        form[0].reset();
}


Framework.Tool.AddPopup = function (currObj) {
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
    var keyName = tableDom.data("pkname");
    //var keyName = params.primaryKey || "";
    var tbbody = tableDom.find("tbody");
    //var width = params.popupwidth || 600;

    var data = {};
    if (!Framework.Tool.isUndefined(refdataarea)) {
        Framework.Tool.GetEditAreaData(data, $(refdataarea));
    }

    Framework.Ajax.GetView(data, addurl, function (result) {
        debugger;
        var params = {
            title: "添加" + modularName,
            width: width,
            message: result,
            confirmText: "保存并添加",
            confirmCloseText: "保存并关闭",
            //保存并添加params.confirmText  params.confirmCloseText 保存并关闭
            onConfirm: function (popup, event) {
                Add(popup, tbbody, false);
            },
            onSaveClose: function (popup, event) {
                Add(popup, tbbody, true);
            }
        };
        Framework.UI.FormModal(params);

        var Add = function Add(popup, tbbody, blcose) {
            if (!Framework.Form.Validates(popup))
                return;

            var data = {};
            Framework.Tool.GetEditAreaData(data, popup);
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
                    //var ss = results;
                    tbbody.prepend(results);
                    if (blcose) {
                        //关闭窗体
                        alert("添加成功");
                        popup.find(".btn-default").trigger("click");
                    }
                    else {
                        popup.success("添加成功");
                    }
                    //清空提示文字
                    //Framework.Form.reSet(popup);
                    $(window).resize();
                });
                //if (fun)
                //    fun(popup);
                //清空提示文字
                Framework.Tool.reSet(popup);
            });
        }
    });
};

//添加Popup
$(document).delegate('.btn-FwAddPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.Tool.AddPopup(currobj);
});

//编辑窗体
Framework.Tool.EditPopup = function (obj) {
    //var tableselect = currObj.data("tableselect");//Table选择器
    var tableDom = obj.closest('table');//Table的Dom对象
    var tr = obj.closest('tr');

    var modularName = tableDom.data("modularname");
    var editurl = tableDom.data("editurl");
    var rowurl = tableDom.data("rowurl");

    var width = tableDom.data("popupwidth");//.popupwidth || 600;
    if (Framework.Tool.isUndefined(width))
        width = 600;
    var keyName = tableDom.data("pkname");
    //var keyName = params.primaryKey || "";
    var tbbody = tableDom.find("tbody");

    var data = {};
    Framework.Tool.GetTrData(data, tr, tbbody);
    //var data = Framework.Tool.GetTbRowDataNew(obj);

    Framework.Ajax.GetView(data, editurl, function (result) {
        ////错误判断
        var params = {
            title: "编辑" + modularName,
            width: width,
            message: result,
            confirmText: "保存",
            onConfirm: function (popup, event) {
                Edit(popup, false);
            }
        };
        Framework.UI.FormModal(params);

        ////////////////////////////////////////////////////
        var Edit = function (popup) {
            if (!Framework.Form.Validates(popup))
                return;
            var data = {};
            Framework.Tool.GetEditAreaData(data, popup);
            //var data = Framework.Form.GetFormItemByObj(popup);
            Framework.Ajax.PostJson(data, editurl, function (results) {
                //错误处理
                if (results.Data.RespAttachInfo.bError) {
                    //显示错误
                    var strerrors = Framework.UI.Behavior.ErrorHandling(results);
                    if (strerrors.length > 0)
                        popup.fail(strerrors);
                    return;
                }
                Framework.Ajax.GetView(data, rowurl, function (results) {
                    //tbbody.prepend($(results));
                    tr.replaceWith(results);
                    alert("操作成功");
                    popup.find(".btn-default").trigger("click");
                });
            });
        };
    }
);
};
alert("dsd");
//编辑Popup
$(document).delegate('.btn-FwEditPopup', 'click', function (e) {
    var currobj = $(this);
    Framework.Tool.EditPopup(currobj);
});


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

//////////////////Start：自定义查询条件///////////////////////////////////////////////
Framework.Tool.QueryFieldNameConst = (function () {
    var m = new Hashtable();
    //m.add("P_BrandID", { bDict: "0", pkname: "P_BrandID", url: "ProductAreas/P_Brand/GetAll", textfield: "BrandName", valuefield: "P_BrandID" });
    //m.add("P_GoodCategoryID", { bDict: "0", pkname: "P_GoodCategoryID", url: "ProductAreas/P_GoodCategory/GetAll", textfield: "GoodCategoryName", valuefield: "P_GoodCategoryID" });
    //m.add("P_ProductCategoryID", { bDict: "0", pkname: "P_ProductCategoryID", url: "ProductAreas/P_ProductCategory/GetAll", textfield: "ProductCategoryName", valuefield: "P_ProductCategoryID" });

    //m.add("Ba_WarehouseID", { bDict: "0", pkname: "Ba_WarehouseID", url: "BasicInfoAreas/Ba_Warehouse/GetAll", textfield: "ReservoirAreaName", valuefield: "Ba_WarehouseID" });
    //m.add("C_CustomerID", { bDict: "0", pkname: "C_CustomerID", url: "CustomerAreas/C_Customer/GetAll", textfield: "CompanyName", valuefield: "C_CustomerID" });

    //m.add("StorageCategoryID", { bDict: "1", pkname: "Category", pkvalue: "StorageType", url: "Home/GetByCategory", textfield: "DText", valuefield: "DValue" });

    return m;
})();

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

