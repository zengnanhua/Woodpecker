

var Utilities = {

}

Utilities.UrlHelp = {
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
}
Utilities.CheckHelp = {
    CheckMail: function (mail) {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(mail)) return true;
        else {
            return false;
        }
    },
    CheckTwoPwd: function (pwd1, pwd2) {
        return pwd1 == pwd2;
    }
}

var Controls = {

}


Controls.DataGrid = {
    //前端假分页
    loader: function (param, success, error, data) {
        var json = new Array();
        var rows = data.rows;
        var start = (param.page - 1) * parseInt(param.rows);         //获取分页起始数据
        var end = start + parseInt(param.rows);                      //获取分页结束数据
        for (var i = start; i < rows.length && i < end; i++) {
            json.push(rows[i]);
        }
        var newData = { "total": data.total, "rows": json };                               //构建新的数据
        success(newData);
    },
    //单元格编辑事件
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;

                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    },
    //添加行
    appendRow: function (dataGridId, dataGridrowData) {
        $('#' + dataGridId).datagrid("appendRow", dataGridrowData);
    },
    //删除行
    deleteRow: function (dataGridId) {
        var arr = $('#' + dataGridId).datagrid("getSelections");
        if (arr.length > 0) {
            $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
                if (r) {

                    for (var i = 0; i < arr.length; i++) {
                        var index = $('#' + dataGridId).datagrid("getRowIndex", arr[i]);
                        $('#' + dataGridId).datagrid("deleteRow", index);
                    }
                }
            });
        }
        else {
            $.messager.alert('警告', '请选择至少一行');
        }
    },
    //进行排序
    sorter: function (a, b) {
        if (!isNaN(a) && !isNaN(b)) {
            var number1 = parseFloat(a);
            var number2 = parseFloat(b);
            return (number1 > number2 ? 1 : -1);
        }
        a = a.split('/');
        b = b.split('/');
        if (a[2] == b[2]) {
            if (a[0] == b[0]) {
                return (a[1] > b[1] ? 1 : -1);
            } else {
                return (a[0] > b[0] ? 1 : -1);
            }
        } else {
            return (a[2] > b[2] ? 1 : -1);
        }
    }

}
Controls.Window = {
    //添加新增界面
    OpenInsertWindow: function (functionFrom_id, fatherId) {
        $.post("/home/FunctionFormTableEdit", { "JIAHUA_Handle": "add", "functionFrom_id": functionFrom_id }, function (data) {
            $(fatherId).html(data);
            $.parser.parse(fatherId);
        });
    },
    //添加编辑页面
    OpenEditWindow: function (functionFrom_id, fatherId) {
        $.post("/home/FunctionFormTableEdit", { "JIAHUA_Handle": "edit", "functionFrom_id": functionFrom_id }, function (data) {
            $(fatherId).html(data);
            $.parser.parse(fatherId);
        });
    },
    SaveWindow: function (formId, formType, functionFrom_id, windowId, editGridId, isBody) {       // formId 是表单id,formType保存类型,functionFrom_id 是功能表单id ,fatherId父id,editGridId 编辑grid
        $(formId).form('submit', {
            url: "/home/FunctionFormTableEdit",
            onSubmit: function (param) {
                alert(isBody);
                if (isBody) {
                    var arr = $(editGridId).datagrid('getRows');                           //获取表体数据
                    param.bodyData = JSON.stringify(arr);                                  //把表体转换成字符串
                }
                param.JIAHUA_Handle = formType;
                param.functionFrom_id = functionFrom_id;
            },
            success: function (data) {
                var newdata = $.parseJSON(data);
                if (newdata.codeE == "success") {
                    $(windowId).window('destroy');
                    $.messager.alert('提示', newdata.msg);
                } else {
                    $.messager.alert('提示', newdata.msg);
                }
            }
        });
    }
}

Controls.Dialog = {
    Html_ShowModalDialog: function (url, width, height) {
        window.open(url + "&tempRomdon=" + Math.random(), window, "dialogwidth:" + width + "px;dialogheight:" + height + "px;center:yes;Help:No;Resizable:No;Status:Yes;Scroll:Yes;");
    }

}

//easyui 的扩展
$.extend(
    $.fn.datagrid.methods, {
        editCell: Controls.DataGrid.editCell
    });
//对checkbox进行扩展
$.extend($.fn.datagrid.defaults.editors, {
    checkbox: {
        init: function (container, options) {
            var input = $('<input type="checkbox" checked="checked" />').appendTo(container);
            return input;
        },
        getValue: function (target) {
            if ($(target).get(0).checked) {
                return "是";
            } else {
                return "否";
            }

        },
        setValue: function (target, value) {
            if (value == "是") {
                $(target).get(0).checked = true;
            } else {
                $(target).get(0).checked = false;
            }
        },
        resize: function (target, width) {

        }
    }


});

