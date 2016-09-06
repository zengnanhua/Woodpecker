var JsHelp = {

    WriteLog: function (msg) {
        console.info(msg);
    },
    //显示弹出选择框
    ShowOk: function (msg) {
        if (layer == null)
        {
            WriteLog("layer 没有引用"); return;
        }
        layer.msg(msg, { icon: 1 });
    },
    //显示失败选择框
    ShowNo: function (msg) {
        if (layer == null) {
            WriteLog("layer 没有引用"); return;
        }
        layer.msg(msg, { icon: 2 });
    },
    //处理返回值
    HandleReturn: function (data, func) {
        function DataHandle(data1)
        {
            if (data1.BackUrl != null && data1.BackUrl != "")
            {
                if (data1.Status == 200) {
                    layer.msg(data1.Msg, {
                        icon: 1,
                        time: 1000 //2秒关闭（如果不配置，默认是3秒）
                    }, function () {
                        window.location.href = data1.BackUrl;
                    });
                } else {
                    layer.msg(data1.Msg, {
                        icon: 2,
                        time: 1000 //2秒关闭（如果不配置，默认是3秒）
                    }, function () {
                        window.location.href = data1.BackUrl;
                    });
                }
              
                return;
            }
            if (data1.Status == 200) {
                JsHelp.ShowOk(data1.Msg);
            } else {
                JsHelp.ShowNo(data1.Msg);
            }
            
        }
        switch (data.Status)
        {
            //成功
            case 200:
                var returnvalue= func(data);
                if (returnvalue == null || returnvalue)
                {
                    DataHandle(data)
                }
                break;
           //尚未登陆
            case 202:
                DataHandle(data)
                break;
            //没有访问权限
            case 203:
                DataHandle(data)
                break;
            //操作失败
            case 204:
                DataHandle(data)
                break;
            //异常
            case 500:
                DataHandle(data)
                break;
            default:
                break;
        }
    },

    Ajax: function (url, postData, func) {
        if ($ == null)
        {
            WriteLog("jquery 没有引用"); return;
        }
        var index= layer.load(2);
        $.ajax({
            type: "POST",
            url: url,
            data: postData,
            success: function (result) {
           
                JsHelp.HandleReturn(result, func);
                layer.close(index);
            },
            error: function () {
                layer.close(index);
            }
        });
    },
    
    datagridPara:{
        paras: {
            onLoadSuccess: function (jsonData) {
                JsHelp.HandleReturn(jsonData, function () { return false; })
            },
            rownumbers: true,
            fitColumns: true,
            pagination: true,//启用分页
            pageList: [10, 14, 20, 30],//页容量组
            pageSize: 14,//初始页容量，注意 值必须和 pageList里的某个值一致
            singleSelect: true,
            url: "",
            toolbar: null,
            columns: null
        },
        init: function (url, toolbar, columns) {
            this.paras.url = url;
            this.paras.toolbar = toolbar;
            this.paras.columns = columns;
        }
    },
    changeDateFormat: function (cellval) {
        var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    },

    //用layer打开指定窗口,url 路径，titleName 标题名,func 回调
    OpenWindow: function (url, titleName, width, height, func) {

        var flag = true;
        if (height > $(window).height())
        {
            flag = false;
            height = $(window).height();
        }
        if (width > $(document.body).width())
        {
            flag = false;
            width = $(window).width();
        }
        var content = null;
        if (flag) {
            content = [url, "no"];
        }
        else {
            content = [url, "yes"];
        }

        layer.open({
            title: [titleName, 'background-color:#222D32;color:#FFF'],
            scrollbar: true,
            type: 2,
            closeBtn: 2,
            area: [width + 'px', height + 'px'],
            skin: 'layui-layer-rim', //加上边框
            content: content,
            success: function (layero, index) {
                if (func != null)
                {
                    func(layero,index)
                }
            }
        });
    },
    ValidateFrom:{
        Validate: function (obj) {
            var flag = true;
            $(obj).find("[validate]").each(function () {
                if (!flag)
                {
                    return;
                }
                var method = $(this).attr("validate");
                var err = $(this).attr("err");
                var bool= eval("JsHelp.ValidateFrom."+method+"($(this).val())");
                if (bool) {
                    flag = false;
                    layer.tips(err, $(this), { tips: 3, time: 1500 });
                }
            });
          
            return flag;
        },

        isNotNull:function(obj){
            obj = $.trim(obj);
            if (obj.length == 0 || obj == null || obj == undefined) {
                return true;
            }
            else
                return false;
        }
    },
    GetUrlParam: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式的对象
        var r = window.location.search.substr(1).match(reg); //匹配目标参数
        if (r != null) {
            return unescape(r[2]); //返回参数值
        } else {
            return null;
        }
    }
   
};

