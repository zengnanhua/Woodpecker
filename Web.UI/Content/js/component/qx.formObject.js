(function ($, undefined) {

    /**
     * 查询表单的前缀 query_
     * 新增表单的前缀 add_
     * 编辑表单的前缀 edit_
     */
    
    $.qxFormObject = function () { };

    $.qxFormObject.prototype = {

        /**
        * 获取表单数据
        */
        getFormObject: function (formId) {
            var $this = this;
            var formElem = ["input", "select", "textarea"];
            var postObj = new Object();

            for (var i = 0; i < formElem.length; i++) {
                $("#" + formId + " " + formElem[i]).each(function (index, elem) {
                    var aName = $this._getNoPrefixName(elem);

                    postObj[aName] = $(elem).val();
                });
            }

            return postObj;
        },

        /**
        * 设置表单数据
        */
        setFormObject: function (formId, dataObj) {
            var $this = this;
            var formElem = ["input", "select", "textarea"];

            for (var i = 0; i < formElem.length; i++) {
                $("#" + formId + " " + formElem[i]).each(function (index, elem) {
                    var aName = $this._getNoPrefixName(elem);

                    $(elem).val(dataObj[aName]);
                });
            }
        },

        /**
        * 清空表单数据
        */
        clearFormObject: function (formId) {
            var formElem = ["input", "select", "textarea"];

            for (var i = 0; i < formElem.length; i++) {
                $("#" + formId + " " + formElem[i]).each(function (index, elem) {
                    $(elem).val("");
                });
            }
        },

        /**
        * 获取无前缀的属性名
        */
        _getNoPrefixName: function (elem) {
            var index = $(elem).attr("id").indexOf("_");
            var noPrefix = $(elem).attr("id").substring(index + 1);

            return noPrefix;
        }
    }

})(jQuery);