layui.use(['element', 'jquery', 'form'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    form.on('submit(loginsub)', function (data) {
        console.log(data.field);
        $.ajax({
            type: "post",
            url: "/fytadmin/login?handler=login",
            data: data.field,
            success: function (res) {
                if (res.statusCode == 200) {
                    window.location.href = '/fytadmin/index';
                } else {
                    alert(res.message);
                }
            }
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