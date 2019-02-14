$(function () {
    //提交需求
    $('.submit').click(function () {
        var t = $('#Title').val(), m = $('#Mobile').val(), s = $('#Summary').val(), e = $('#Email').val();
        if (!t) {
            alert('请填写您的姓名！');
            $('#Title').focus();
            return;
        }
        if (!m) {
            alert('请填写您的联系电话！');
            $('#Mobile').focus();
            return;
        }
        if (!s) {
            alert.html('请简单描述下您的需求！');
            $('#Summary').focus();
            return;
        }
        $('.submit').attr('disabled', true);
        $.ajax('/index?handler=message', {
            data: { Title: t, Mobile: m, Summary: s,Email:e },
            dataType: 'json',
            type: 'get',
            timeout: 10 * 1000,
            success: function (res) {
                $('.submit').attr('disabled', false);
                if (res.statusCode === 200) {
                    alert('您的信息已提交成功，我们会在一个工作日联系您~');
                } else {
                    alert(res.message);
                }
            },
            error: function (xhr, type, errorThrown) {
                $('.submit').attr('disabled', false);
                alert.html('服务器无响应，请稍后重试！');
            }
        });
    });
});