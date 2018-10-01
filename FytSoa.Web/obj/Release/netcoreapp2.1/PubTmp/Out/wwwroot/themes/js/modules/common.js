layui.define(['layer', 'toastr', 'ztree', 'ztreecheck', 'pjax'], function (exports) {
    "use strict";

    var $ = layui.jquery,
        layer = layui.layer,
        ztree = layui.ztree,
        zcheck = layui.ztreecheck,
        toastr = layui.toastr,
        pjax = layui.pjax;
    toastr.options = {
        "positionClass": "toast-top-center"
    };
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
        get: function (url, options, callFun) {
            var httpUrl = "/";
            $.ajax(httpUrl + url, {
                data: options,
                async: false,
                dataType: 'json', //服务器返回json格式数据
                type: 'get', //HTTP请求类型
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
                shade: 0.2,
                maxmin: false, //开启最大化最小化按钮
                area: [width, height],
                content: url,
                zIndex: "10000",
                end: function () {
                    if (fun) fun();
                }
            });
        },
        closeOpen: function () {
            layer.closeAll();      
        }, 
        getUrlParam: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); 
            var r = window.location.search.substr(1).match(reg); 
            if (r != null) return unescape(r[2]); return null;
        },
        formatdate: function (str) {
            if (str) {
                var d = eval('new ' + str.substr(1, str.length - 2));
                var ar_date = [
                    d.getFullYear(), d.getMonth() + 1, d.getDate()
                ];
                for (var i = 0; i < ar_date.length; i++) ar_date[i] = dFormat(ar_date[i]);
                return ar_date.slice(0, 3).join('-') + ' ' + ar_date.slice(3).join(':');

                function dFormat(i) { return i < 10 ? "0" + i.toString() : i; }
            } else {
                return "无信息";
            }
        }
    };
    exports('common', tool);
});

