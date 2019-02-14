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
    var tmls,tool = {
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
            var httpUrl = "/", token = tool.GetSession('FYTADMIN_ACCESS_TOKEN');
            $.ajax(httpUrl + url, {
                data: options,
                async: true,
                dataType: 'json', //服务器返回json格式数据
                type: 'post', //HTTP请求类型
                timeout: 10 * 1000, //超时时间设置为50秒；
                headers: {
                    'Authorization': 'Bearer ' + token
                },
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
            var httpUrl = "/", token = tool.GetSession('FYTADMIN_ACCESS_TOKEN');
            $.ajax(httpUrl + url, {
                data: options,
                async: true,
                dataType: 'json', //服务器返回json格式数据
                type: 'get', //HTTP请求类型
                timeout: 10 * 1000, //超时时间设置为50秒；
                headers: {
                    'Authorization': 'Bearer ' + token
                },
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
            top.layer.open({
                type: 2,
                title: title,
                shadeClose: false,
                shade: 0.2,
                move:false,
                maxmin: false, //开启最大化最小化按钮
                area: [width, height],
                content: url,
                zIndex: "10000",
                end: function () {
                    if (fun) fun();
                }
            });
        },
        OpenRight: function (title, url, width, height, fun,cancelFun) {
            var index=layer.open({
                title: title
                , type: 2
                , area: [width, height]
                , shade: [0.1, '#333']
                , resize: false
                , move: false
                , anim: -1
                , offset: 'rb'
                , zIndex: "1000"
                , shadeClose: false
                , skin: 'layer-anim-07'
                , content: url
                , end: function () {
                    if (fun) fun();
                }
                , cancel: function (index) {
                    if (cancelFun) cancelFun(index);
                }
            });
            return index;
        },
        getToken: function () {
            var token = tool.GetSession('FYTADMIN_ACCESS_TOKEN');
            return { 'Authorization': 'Bearer ' + token};
        },
        closeOpen: function () {
            layer.closeAll();
        },
        tableLoading: function () {
            tmls = layer.msg('<i class="layui-icon layui-icon-loading layui-icon layui-anim layui-anim-rotate layui-anim-loop"></i> 正在加载数据哦', { time: 20000});
        },
        tableLoadingClose: function () {
            setTimeout(function () {
                layer.close(tmls);
            }, 500);
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
        },
        SetSession: function (key, options) {
            localStorage.setItem(key, JSON.stringify(options));
        },
        GetSession: function (key) {
            var obj = localStorage.getItem(key);
            if (obj != null) {
                return JSON.parse(obj);
            }
            return null;
        },
        /**
         * 删除键值对json
         * @param {key} key : 键
         */
        SessionRemove: function (key) {
            localStorage.removeItem(key);
        },
        /**
         * 打印日志到控制台
         * @param {data} data : Json
         */
        log: function (data) {
            console.log(JSON.stringify(data));
        },
        cloudFile: function () {
            $(".fyt-cloud").click(function () {
                var input_text = $(this).data("text");
                var showImg = $(this).data('img');
                var type = $(this).data('type'); //edit=编辑器  sign=默认表单  iframe=弹出层  form=带图片显示
                var frameId = window.frameElement && window.frameElement.id || '',frameUrl='';
                if (frameId) {
                    frameUrl = '&frameid=' + frameId;
                }
                tool.Open('媒体资源库', '/fytadmin/file/cloud/?type=' + type + '&img=' + showImg + '&control=' + input_text + frameUrl, '950px', '600px');
            });
        },
        isExtImage: function(name){
            var imgExt = new Array(".png", ".jpg", ".jpeg", ".bmp", ".gif");
            name = name.toLowerCase();
            var i = name.lastIndexOf(".");
            var ext;
            if (i > -1) {
                ext = name.substring(i);
            }
            for (var i = 0; i < imgExt.length; i++) {
                if (imgExt[i] === ext)
                    return true;
            }
            return false;
        }
    };
    exports('common', tool);
});

