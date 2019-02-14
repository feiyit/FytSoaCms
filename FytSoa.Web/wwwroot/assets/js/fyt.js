if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
    new WOW().init();
};
$(function () {
    $(window)
        .scroll(function () {
            if ($(window).scrollTop() >= 100) {
                $('#gotop').fadeIn(300);
                $(".fyt-kf").addClass('cur');
            } else {
                $('#gotop').fadeOut(300);
                $(".fyt-kf").removeClass('cur');
            }
        });
    $('#gotop')
        .click(function () {
            $('html,body').animate({ scrollTop: '0px' }, 800);
        });
    $(".drop-project .drop-left li").hover(function () {
        $(this).addClass("active").siblings().removeClass('active');
        var i = $(this).index();
        $(".drop-project .drop-right").addClass('hidden').eq(i).removeClass('hidden');
    });
    $(".drop-solution .drop-left li").hover(function () {
        $(this).addClass("active").siblings().removeClass('active');
        var i = $(this).index();
        $(".drop-solution .drop-right").addClass('hidden').eq(i).removeClass('hidden');
    });
    $('.xcy-fyt a').hover(bnshow, bnhide);
    $(".nav-wall>li").each(function (i) {
        $(this).mouseover(function () {
            var i = $(this).index();
            if (i != 1) {
                $(".nav-drop").hide();
                $(".drop-project").addClass('hidden');
                $(".drop-solution").addClass('hidden');
            } else {
                $(".nav-drop").show();
            }
            var lw = 0;
            for (var j = 0; j < i; j++) {
                lw += $(".nav-wall li").eq(j).width();
            };
            var nw = $(this).width();
            $(".nav-ul>ul>.line").css({
                'left': lw
            }).show();
            $(".nav-ul>ul>.line").animate({
                'width': nw
            }, 100);
        });
    });

    $(".nav-wall>li").eq(1).mouseover(function (e) {
        $(".nav-drop,.drop-project").show();
        $(".drop-project").removeClass('hidden');
        $(".drop-solution").addClass('hidden');
        //e.preventDefault();
    });
    
    $(".nav-drop").mouseover(function (e) {
        $(".nav-drop").show();
        //e.preventDefault();
    });
    $(".nav-drop").mouseout(function (e) {
        $(".nav-drop").hide();
        //$(".nav-ul>ul>.line").css({'width':0});
        e.preventDefault();
    });
    $("#mpbtn").click(function() {
        $("#fixed_mp").addClass("show");
    });
    $("#fixed_mp").click(function() {
        $(this).removeClass("show");
    });
	
	 //点击立即咨询
    $(".service-im").click(function () {
        if ($('#nb_invite_ok').length > 0) {
            $('#nb_invite_ok').click();
        }
    });

    setTimeout(function() {
        $('body').append(kfh);
        $(".fyt-ation").fadeIn(300);
        //点击立即咨询
        $(".action-ok").click(function() {
            if ($('#nb_invite_ok').length > 0) {
                $('#nb_invite_ok').click();
            }  
        });
        //稍后再说，以及关闭
        $(".action-wait,.fyt-action-close").click(function () {
            $(".fyt-ation").fadeOut(300);
            kfclear = setInterval(reset_kf, 1000);
        });
    }, 3000);
    
});

function bnshow() {
    $('.xcy-fyt a span').css('left', '170px');
    $('.xcy-fyt a i').css('left', '75px');
}
function bnhide() {
    $('.xcy-fyt a span').css('left', '0px');
    $('.xcy-fyt a i').css('left', '-113px');
}
function searchToggle(obj, evt) {
    var container = $(obj).closest('.search-wrapper');

    if (!container.hasClass('active')) {
        container.addClass('active');
        evt.preventDefault();
    } else if (container.hasClass('active') && $(obj).closest('.input-holder').length == 0) {
        container.removeClass('active');
        container.find('.search-input').val('');
    }
}

function submitFn(obj, evt) {
    value = $(obj).find('.search-input').val().trim();
    if (!value.length) {
        _html = "Yup yup! Add some text friend :D";
    } else {
        _html += "<b>" + value + "</b>";
    }
    evt.preventDefault();
}

var fyt = {
    Success: function (res) {
        if (res.Status === "y") {
            layer.alert("提交成功，我们会尽快与您取得联系~", { icon: 6, title: "提示" }, function () {
                window.location.href = '/cases/';
            });
        } else {
            //提示信息
            layer.alert(res.Msg, { icon: 2, title: "错误提示" });
        }
        $(".btn-save").attr("disabled", false).html("确定提交");
    },
    Begin: function () {
        $(".btn-save").attr("disabled", "disabled").html("正在提交...");
    },
    Complete: function () {
        $(".btn-save").attr("disabled", false).html("确定提交");
    },
    Failure: function () {
        layer.alert("失败了，请稍后重试", { icon: 2, title: "错误提示" });
    }
};

//开始倒计时20秒后，重新打开客服邀请框
function reset_kf() {
    ketime--;
    if (ketime == 0) {
        ketime = 20;
        clearInterval(kfclear);
        $(".fyt-ation").fadeIn(300);
    }
}

var kfclear=null, ketime=20, kfh = '<div class="fyt-ation">';
kfh += '<div class="fyt-action-wrap">';
kfh += '<div class="fyt-action-close">x</div>';
kfh += '<div class="fyt-action-text">';
kfh += '<h3>多一份参考，总有益处</h3>';
kfh += ' <p>专注企业个性化网站定制，APP、小程序、微信公众号开发</p>';
kfh += '<div class="tel"><a href="tel:010-57178368">010-57178368</a><a href="tel:13511084034">13511084034</a></div>';
kfh += '</div><div class="fyt-action-btn">';
kfh += '<a href="javascript:void(0)" class="action-ok">在线咨询</a><a href="javascript:void(0)" class="action-wait">稍后再说</a>';
kfh += '</div></div>';
kfh += '</div>';