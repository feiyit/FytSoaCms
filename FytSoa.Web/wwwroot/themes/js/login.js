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
        var btns = $(".layui-btn-normal");
        btns.html('<i class="layui-icon layui-anim layui-anim-rotate layui-anim-loop"></i>');
        btns.attr('disabled', 'disabled');        
        os.ajax('fytadmin/login?handler=login', data.field, function (res) {                    
            if (res.statusCode === 200) {
                setTimeout(function () {
                    window.location.href = '/fytadmin/index';
                },1000)                
            } else {
                $(".login-tip span").html(res.message);
                $(".login-tip").animate({ 'height': '30px' });
                setTimeout(function () {
                    $(".login-tip").animate({ 'height': 0 });
                }, 2500);
            }
            btns.attr('disabled', false);    
            setTimeout(function () {
                btns.html('登录');
            },1000);
        })       
        return false;
    });
    $(window).resize(
		bodysize
    );
    bodysize();
    function bodysize()
    {
        $("body").height($(window).height())
    }
});