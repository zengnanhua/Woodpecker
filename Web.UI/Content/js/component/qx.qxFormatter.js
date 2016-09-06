(function ($, undefined) {

    $.qxFormatter = function () { };

    $.qxFormatter.prototype = {

        imageFormatter: function (cellvalue, options, rowObject) {
            var htmlTxt = "";

            htmlTxt = "<img src='" + cellvalue + "' height=25/>";
            return htmlTxt;
        },

        msgTypeFormatter: function (cellvalue, options, rowObject) {
            if (cellvalue == 1) {
                return "普通消息";
            }
        },

        boolTypeFormatter: function (cellvalue, options, rowObject) {
            if (cellvalue == 1) {
                return "是";
            }
            else {
                return "否";
            }
        },

        passTypeFormatter: function (cellvalue, options, rowObject) {
            if (cellvalue == 0) {
                return "通过";
            } else {
                return "未通过";
            }
        }
    }

})(jQuery);