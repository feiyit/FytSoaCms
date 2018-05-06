layui.define(['layer','toastr','ztree'], function (exports) {
    "use strict";

    var $ = layui.jquery,
        layer = layui.layer,
        ztree = layui.ztree,
        toastr=layui.toastr;
    var tool = {
        error: function (msg) {
            toastr.error(msg);
        },
        warning: function (msg) {
            toastr.warning(msg);
        },
        success: function (msg) {
            toastr.success(msg);
        },
        ajax: function (url, options, callFun) {
            var httpUrl = "/";
            $.ajax(httpUrl + url, {
                data: options,
                async: false,
                dataType: 'json', //服务器返回json格式数据
                type: 'post', //HTTP请求类型
                timeout: 50 * 1000, //超时时间设置为50秒；
                success: function (data) {
                    callFun(data);
                },
                error: function (xhr, type, errorThrown) {
                    if (type === 'timeout') {
                        tool.error('连接超时，请稍后重试！');
                    } else if (type === 'error') {
                        tool.error('连接异常，请稍后重试！');
                    }
                }
            });
        },
        Open: function (title, url, width, height, fun) {
            layer.open({
                type: 2,
                title: title,
                shadeClose: true,
                shade: 0.3,
                maxmin: false, //开启最大化最小化按钮
                area: [width, height],
                content: url,
                zIndex: "10",
                end: function () {
                    if (fun) fun();
                }
            });
        },
        closeOpen: function () {
            layer.closeAll();      
        }, 
    };
    exports('common', tool);
});

