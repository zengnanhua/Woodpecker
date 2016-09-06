﻿/**
 * jQuery LeaRunUI 4.1
 *上海力软信息技术有限公司 Copyright © Learun 2014
 */
/* 表单验证使用实例！  */

//格式化时间
function formatTime(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    var mm = date.getMinutes();
    var s = date.getSeconds();
    return y + "-" + (m < 10 ? ("0" + m) : m) + "-" + (d < 10 ? ("0" + d) : d) + " " + (h < 10 ? ("0" + h) : h) + ":" + (mm < 10 ? ("0" + mm) : mm) + ":" + (s < 10 ? ("0" + s) : s);
}


//获取Request notnull
function isRequestNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}
//验证不为空 notnull
function isNotNull(obj) {
    obj = $.trim(obj);
    if (obj.length == 0 || obj == null || obj == undefined) {
        return true;
    }
    else
        return false;
}

//验证数字 num
function isInteger(obj) {
    reg = /^[-+]?\d+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证数字 num  或者null,空
function isIntegerOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[-+]?\d+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//Email验证 email
function isEmail(obj) {
    reg = /^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//Email验证 email   或者null,空
function isEmailOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^\w{3,}@\w+(\.\w+)+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文和数字字符串 echar
function isECStr(obj) {
    reg = /^[a-z,A-Z,0-9]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文字符串 echar
function isEnglishStr(obj) {
    reg = /^[a-z,A-Z]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证只能输入英文字符串 echar 或者null,空
function isEnglishStrOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[a-z,A-Z]+$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否是n位数字字符串编号 nnum
function isLenNum(obj, n) {
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否是n位数字字符串编号 nnum或者null,空
function isLenNumOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^[0-9]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否小于等于n位数的字符串 nchar
function isLenStr(obj, n) {
    //reg = /^[A-Za-z0-9\u0391-\uFFE5]+$/;
    obj = $.trim(obj);
    if (obj.length == 0 || obj.length > n)
        return false;
    else
        return true;
    //    if (!reg.test(obj)) {
    //        return false;
    //    } else {
    //        return true;
    //    }
}

//验证是否小于等于n位数的字符串 nchar或者null,空
function isLenStrOrNull(obj, n) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    //reg = /^[A-Za-z0-9\u0391-\uFFE5]+$/;
    obj = $.trim(obj);
    if (obj.length > n)
        return false;
        //    if (!reg.test(obj)) {
        //        return false;
        //    } else {
        //        return true;
        //    }
    else
        return true;
}

//验证是否电话号码 phone
function isTelephone(obj) {
    reg = /^(0[0-9]{2,3}-)?([2-9][0-9]{6,7})$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否电话号码 phone或者null,空
function isTelephoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号 mobile
function isMobile(obj) {
    reg = /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号 mobile或者null,空
function isMobileOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号或电话号码 mobile phone 
function isMobileOrPhone(obj) {
    reg_mobile = /^(\+\d{2,3}\-)?\d{11}$/;
    reg_phone = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg_mobile.test(obj) && !reg_phone.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证是否手机号或电话号码 mobile phone或者null,空
function isMobileOrPhoneOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^(\+\d{2,3}\-)?\d{11}$/;
    reg2 = /^(\d{3,4}\-)?[1-9]\d{6,7}$/;
    if (!reg.test(obj) && !reg2.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证网址 uri
function isUri(obj) {
    reg = /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证网址 uri或者null,空
function isUriOrnull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    reg = /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/;
    if (!reg.test(obj)) {
        return false;
    } else {
        return true;
    }
}

//验证两个值是否相等 equals
function isEqual(obj1, controlObj) {
    if (obj1.length != 0 && controlObj.length != 0) {
        if (obj1 == controlObj)
            return true;
        else
            return false;
    }
    else
        return false;
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date
function isDate(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD格式的类型 date或者null,空
function isDateOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime
function isDateTime(obj) {
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime或者null,空
function isDateTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time
function isTime(obj) {
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断日期类型是否为hh:mm:ss格式的类型 time或者null,空
function isTimeOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为中文 cchar 
function isChinese(obj) {
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        if (!reg.test(str)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为中文 cchar或者null,空
function isChineseOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^[\u0391-\uFFE5]+$/;
        if (!reg.test(str)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的邮编(只能为六位)是否正确 zip
function isZip(obj) {
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        if (!reg.test(str)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的邮编(只能为六位)是否正确 zip或者null,空
function isZipOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^\d{6}$/;
        if (!reg.test(str)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为双精度 double
function isDouble(obj) {
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断输入的字符是否为双精度 double或者null,空
function isDoubleOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^[-\+]?\d+(\.\d+)?$/;
        if (!reg.test(obj)) {
            return false;
        }
        else {
            return true;
        }
    }
}

//判断是否为身份证 idcard
function isIDCard(obj) {
    if (obj.length != 0) {
        reg = /^\d{15}(\d{2}[A-Za-z0-9;])?$/;
        if (!reg.test(obj))
            return false;
        else
            return true;
    }
}

//判断是否为身份证 idcard或者null,空
function isIDCardOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    if (obj.length != 0) {
        reg = /^\d{15}(\d{2}[A-Za-z0-9;])?$/;
        if (!reg.test(obj))
            return false;
        else
            return true;
    }
}
//判断是否为IP地址格式
function isIP(obj) {
    var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
    if (re.test(obj)) {
        if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
    }
    return false;
}
//判断是否为IP地址格式 或者null,空
function isIPOrNull(obj) {
    var controlObj = $.trim(obj);
    if (controlObj.length == 0 || controlObj == null || controlObj == undefined) {
        return true;
    }
    var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
    if (re.test(obj)) {
        if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
    }
    return false;
}
/**
数据验证完整性
**/
function CheckDataValid(id) {
    if (!JudgeValidate(id)) {
        return false;
    } else {
        return true;
    }
}
//验证脚本
//obj为当前input所在的空间容器 (例如：Div,Panel)
//脚本中 checkvalue 验证函数  err 属性表示提示【中文名称】
function JudgeValidate(obj) {
    var Validatemsg = "";
    var Validateflag = true;
    $(obj).find("[datacol=yes]").each(function () {
        if ($(this).attr("checkexpession") != undefined) {
            var value = $(this).val();
            if (value == "==请选择==") {
                value = "";
            }
            switch ($(this).attr("checkexpession")) {
                case "default":
                    {
                        if (isNotNull(value)) {
                            Validatemsg = $(this).attr("err") + "\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NotNull":
                    {
                        if (isNotNull(value)) {
                            Validatemsg = $(this).attr("err") + "不能为空！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Num":
                    {
                        if (!isInteger(value)) {
                            Validatemsg = $(this).attr("err") + "必须为数字！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "NumOrNull":
                    {
                        if (!isIntegerOrNull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为数字！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Email":
                    {
                        if (!isEmail(value)) {
                            Validatemsg = $(this).attr("err") + "必须为E-mail格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EmailOrNull":
                    {
                        if (!isEmailOrNull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为E-mail格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EnglishStr":
                    {
                        if (!isEnglishStr(value)) {
                            Validatemsg = $(this).attr("err") + "必须为字符串！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "EnglishStrOrNull":
                    {
                        if (!isEnglishStrOrNull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为字符串！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenNum":
                    {
                        if (!isLenNum(value, $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenNumOrNull":
                    {
                        if (!isLenNumOrNull(value, $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须为" + $(this).attr("length") + "位数字！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenStr":
                    {
                        if (!isLenStr(value, $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须小于" + $(this).attr("length") + "位字符！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "LenStrOrNull":
                    {
                        if (!isLenStrOrNull(value, $(this).attr("length"))) {
                            Validatemsg = $(this).attr("err") + "必须小于" + $(this).attr("length") + "位字符！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Phone":
                    {
                        if (!isTelephone(value)) {
                            Validatemsg = $(this).attr("err") + "必须电话格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Fax":
                    {
                        if (!isTelephoneOrNull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为传真格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "PhoneOrNull":
                    {
                        if (!isTelephoneOrNull(value)) {
                            Validatemsg = $(this).attr("err") + "必须电话格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Mobile":
                    {
                        if (!isMobile(value)) {
                            Validatemsg = $(this).attr("err") + "必须为手机格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrNull":
                    {
                        if (!isMobileOrnull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为手机格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrPhone":
                    {
                        if (!isMobileOrPhone(value)) {
                            Validatemsg = $(this).attr("err") + "必须为电话格式或手机格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "MobileOrPhoneOrNull":
                    {
                        if (!isMobileOrPhoneOrNull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为电话格式或手机格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Uri":
                    {
                        if (!isUri(value)) {
                            Validatemsg = $(this).attr("err") + "必须为网址格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "UriOrNull":
                    {
                        if (!isUriOrnull(value)) {
                            Validatemsg = $(this).attr("err") + "必须为网址格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Equal":
                    {
                        if (!isEqual(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "不相等！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Date":
                    {
                        if (!isDate(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateOrNull":
                    {
                        if (!isDateOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateTime":
                    {
                        if (!isDateTime(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期时间格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DateTimeOrNull":
                    {
                        if (!isDateTimeOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为日期时间格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Time":
                    {
                        if (!isTime(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为时间格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "TimeOrNull":
                    {
                        if (!isTimeOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为时间格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseStr":
                    {
                        if (!isChinese(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为中文！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ChineseStrOrNull":
                    {
                        if (!isChineseOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为中文！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Zip":
                    {
                        if (!isZip(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为邮编格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "ZipOrNull":
                    {
                        if (!isZipOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为邮编格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "Double":
                    {
                        if (!isDouble(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为小数！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "DoubleOrNull":
                    {
                        if (!isDoubleOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为小数！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IDCard":
                    {
                        if (!isIDCard(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为身份证格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IDCardOrNull":
                    {
                        if (!isIDCardOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为身份证格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "RequestNotNull":
                    {
                        if (isNotNull(value)) {
                            Validatemsg = $(this).attr("err") + "！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IsExist":
                    {
                        Validatemsg = $(this).attr("err") + "！\n";
                        Validateflag = false;
                        tipCss($(this), Validatemsg); return false;
                        break;
                    }
                case "IsIP":
                    {
                        if (!isIP(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为IP格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                case "IPOrNull":
                    {
                        if (!isIPOrNullOrNull(value, $(this).attr("eqvalue"))) {
                            Validatemsg = $(this).attr("err") + "必须为IP格式！\n";
                            Validateflag = false;
                            tipCss($(this), Validatemsg); return false;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    });
    if (Validatemsg.length > 0) {
        return Validateflag;
    }
    return Validateflag;
}
//提示信息
function tipCss(obj, Validatemsg) {
    var Isrequired = false;
    if ($('#message').length > 0) {
        $('#message').html("");
        $("#message").html("<div class=\"note-error\"><div class=\"note-icon-error\"></div><div class=\"note-text\">" + Validatemsg + "</div></div>").slideDown('fast');
    } else {
        top.TipMsg(Validatemsg, 5000, 'error');
    }
    $(obj).focus();
    if ($(obj).attr('class') == 'txt' || $(obj).attr('class') == 'txt required' || $(obj).attr('class') == 'txt icontree') {
        if ($(obj).hasClass('required')) {
            Isrequired = true;
        }
        $(obj).addClass("warning");
        $(obj).parent().addClass('tdwarning');
        $(obj).removeClass("required");
    }
    $(obj).change(function () {
        if ($(obj).val() != "") {
            if ($(obj).attr('type') == 'text' || $(obj).attr('type') == 'password') {
                $(obj).removeClass("warning");
                $(obj).parent().removeClass('tdwarning');
                if (Isrequired) {
                    $(obj).addClass("required");
                }
            }
            $('#message').slideUp(300);
        }
    });
    window.setTimeout(docBubbleremove, 5000);
    return false;
}
function docBubbleremove() {
    $('#message').slideUp(300);
}