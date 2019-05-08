if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
    new WOW().init();
};
$(function () {
    os.banner();
    $(".banner_item,.x_about_us").on("mousemove", function (e) {
        var w = $(window).width();
        var h = $(window).height();
        var offsetX = 0.5 - e.pageX / w;
        var offsetY = 0.5 - e.pageY / h;

        $(".hover-image img,.x_about_img img,.hover-image .dwwave").each(function (i, el) {
            var offset = parseInt($(el).data("offset"));
            var translate = "translate3d(" + Math.round(offsetX * offset) + "px," + Math.round(offsetY * offset) + "px, 0px)";

            $(el).css({
                "-webkit-transform": translate,
                transform: translate,
                "moz-transform": translate
            });
        });
    });
    //提交需求
    $('.btn-save').click(function () {
        var t = $('#Title').val(), m = $('#Mobile').val(), s = $('#Summary').val();
        if (!t) {
            $('.mess-tip').html('请填写您的姓名！');
            $('#Title').focus();
            return;
        }
        if (!m) {
            $('.mess-tip').html('请填写您的联系电话！');
            $('#Mobile').focus();
            return;
        }
        if (!s) {
            $('.mess-tip').html('请简单描述下您的需求！');
            $('#Summary').focus();
            return;
        }
        $('.mess-tip').html('');
        $('.btn-save').attr('disabled', true);
        $.ajax('/index?handler=message', {
            data: { Title: t, Mobile: m, Summary: s },
            dataType: 'json',
            type: 'get',
            timeout: 10 * 1000,
            success: function (res) {
                $('.btn-save').attr('disabled', false);
                if (res.statusCode === 200) {
                    alert('您的信息已提交成功，我们会在一个工作日联系您~');
                } else {
                    alert(res.message);
                }
            },
            error: function (xhr, type, errorThrown) {
                $('.btn-save').attr('disabled', false);
                $('.mess-tip').html('服务器无响应，请稍后重试！');
            }
        });
    });
    $(window)
        .scroll(function () {
            if ($(window).scrollTop() >= 100) {
                $('.go-top').fadeIn(300);
            } else {
                $('.go-top').fadeOut(300);
            }
        });
    $('.go-top')
        .click(function () {
            $('html,body').animate({ scrollTop: '0px' }, 800);
        });
});

var os = {
    banner: function () {
        var winWid = $(window).width();
        var nub_ = $(".num1 ul");
        var oUl = $(".L-banner .banner_item_cont");
        var aLiUl = $(".banner_item_cont .banner_item");

        var iHeight = aLiUl.eq(0).width();
        var Length = $(".L-banner .banner_item").length;
        var oBox = $(".num1");
        var iNow = 0;
        var timer = null;
        for (var i = 0; i < Length; i++) {
            $('.num1').append('<span></span>')
        }

        var aLiOl = $(".num1 span");
        $(".num1 span").eq(0).addClass("active");
        $(".num1 span").on("mouseover", function () {
            clearInterval(timer);
            $(".num1 span").removeClass("active");
            $(this).addClass('active');
            iNow = $(this).index();
            aLiUl.eq(iNow).siblings().removeClass("animation-top").css('display', 'none');
            aLiUl.eq(iNow).show();
            aLiUl.eq(iNow).addClass("animation-top");
        })
        $(".num1 span").on("mouseout", function () {
            timer = setInterval(toRun, 6000);
        })

        timer = setInterval(toRun, 6000);

        function toRun() {
            if (iNow == aLiOl.length - 1) {
                iNow = 0;
            } else {
                iNow++;
            }
            for (var i = 0; i < aLiOl.length; i++) {
                aLiOl[i].className = '';
            }
            $(".num1 span").eq(iNow).addClass('active');
            aLiUl.eq(iNow).siblings().removeClass("animation-top").css('display', 'none');
            aLiUl.eq(iNow).show();
            aLiUl.eq(iNow).addClass("animation-top");

        }
    },
}