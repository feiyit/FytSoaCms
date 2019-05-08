var httpUrl = 'http://www.feiyit.net/', //http://192.168.1.3:4909/
    os = {
        ajax: function(url, options, callFun) {
            $.ajax(httpUrl + url, {
                data: options,
                async: true,
                dataType: 'json', //服务器返回json格式数据
                type: 'post', //HTTP请求类型
                timeout: 10 * 1000, //超时时间设置为50秒；
                success: function(data) {
                    callFun(data);
                },
                error: function(xhr, type, errorThrown) {
                    alert('错误');
                }
            });
        },
        getUrlParam: function(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]);
            return null;
        },
        SetSession: function(key, options) {
            localStorage.setItem(key, JSON.stringify(options));
        },
        GetSession: function(key) {
            var obj = localStorage.getItem(key);
            if (obj != null) {
                return JSON.parse(obj);
            }
            return null;
        },
        SessionRemove: function(key) {
            localStorage.removeItem(key);
        },
        loading: function() {
            $('body').append('<div class="mask"><div class="loading"><span></span><p>正在加载...</p></div></div>');;
        },
        loadclose: function() {
            $('.mask,.loading').remove();
        }
    }