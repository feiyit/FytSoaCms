layui.config({
    base: '/themes/js/modules/'
});
layui.use(['element', 'jquery', 'form', 'common'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery,
        os = layui.common;
    $(".layui-btn-danger").click(function () {
        document.getElementById("forms").reset();
    });
    form.on('submit(loginsub)', function (data) {
        var crypt = new JSEncrypt();
        crypt.setPrivateKey(data.field.privateKey);
        var enc = crypt.encrypt(data.field.password);
        $("#password").val(enc);
        data.field.password = enc;
        var btns = $(".layui-btn-normal");
        btns.html('<i class="layui-icon layui-anim layui-anim-rotate layui-anim-loop"></i>');
        btns.attr('disabled', 'disabled');     
        $.ajax('/fytadmin/login?handler=login', {
            data: data.field,
            dataType: 'json', //服务器返回json格式数据
            type: 'post', //HTTP请求类型
            timeout: 10 * 1000, //超时时间设置为50秒；
            success: function (res) {
                if (res.statusCode === 200) {
                    os.SetSession('FYTADMIN_ACCESS_TOKEN', res.data);
                    setTimeout(function () {
                        var rurl = os.getUrlParam('ReturnUrl');
                        if (!rurl) {
                            window.location.href = '/fytadmin/index';
                        }
                        else {
                            window.location.href = rurl;
                        }
                    }, 1000);
                } else {
                    $(".login-tip span").html(res.message);
                    $("#password").val('');
                    $(".login-tip").animate({ 'height': '30px' });
                    setTimeout(function () {
                        $(".login-tip").animate({ 'height': 0 });
                    }, 2500);
                }
                btns.attr('disabled', false);
                setTimeout(function () {
                    btns.html('登录');
                }, 1000);
            },
            error: function (xhr, type, errorThrown) {
                tool.error('连接异常，请稍后重试！');
            }
        });
        return false;
    });
    $(window).resize(
		bodysize
    );
    bodysize();
    function bodysize()
    {
        $("body").height($(window).height());
    }
});