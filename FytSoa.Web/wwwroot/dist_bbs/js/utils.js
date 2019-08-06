(function($) {
    if (typeof $.validator !== 'undefined') {
        $.extend($.validator.messages, {
            required: "This field is required.",
            email: "Please enter a valid email address.",
            url: "Please enter a valid URL.",
            number: "Please enter a valid number.",
            digits: "Please enter only digits.",
            equalTo: "Please enter the same value again.",
            date: "Please enter a valid date.",
            creditcard: "Please enter a valid credit card number.",
            accept: "Please enter a value with a valid extension.",
            integer: "You must enter an integer value.",
            maxlength: $.validator.format("Please enter no more than {0} characters."),
            minlength: $.validator.format("Please enter at least {0} characters."),
            rangelength: $.validator.format("Please enter a value between {0} and {1} characters long."),
            range: jQuery.validator.format("Please enter a value between {0} and {1}."),
            min: $.validator.format("Please enter a value greater than or equal to {0}."),
            max: $.validator.format("Please enter a value less than or equal to {0}.")
        });
    }


})(jQuery);

var fromMsg = {
    Login_UserName: '请输入手机号码/账号/邮箱（Please fill out all fields required.）',
    Login_PassWord: '请输入密码（Please fill out all fields required.）',
    Forgot_Pass: '请输入邮箱（Please fill out all fields required.）',
    Re_Password: '请再次输入密码（Please fill out all fields required.）',
    EqualTo_Pass: '两次密码输入的不一致（Please fill out all fields required.）',
    Reg_UserName: '请输入用户名（Please fill out all fields required.）',
    Reg_Phone: '请输入手机号码（Please fill out all fields required.）',
};


$(".chosen-select").chosen({
    disable_search_threshold: 10,
    no_results_text: "Oops, nothing found!",
    width: "95%"
});
//登录弹出
$('.login-url').click(function() {
    $('#login_register').modal('toggle');
});
let fv = 500;
//忘记密码
$('.link_forgot_pass').click(function(event) {
    event.preventDefault();
    $('#signin_form').fadeOut("slow", function() {
        $(this).css({
            'z-index': 1
        });
        $('#myModalLabel').html('忘记密码（Retrieve the password）');
        $('#forgotpass_form').fadeIn(500).css({
            'z-index': 2
        });
    });
});
//登录
$('.return_link_sign_in').click(function(event) {
    event.preventDefault();
    $('#forgotpass_form').fadeOut("slow", function() {
        $(this).css({
            'z-index': 1
        });
        $('#myModalLabel').html('登录（Sign In）');
        $('#signin_form').fadeIn(500).css({
            'z-index': 2
        });
    });
});
//注册
$('.link_sign_up').click(function(event) {
    event.preventDefault();
    $('#signin_form').fadeOut("slow", function() {
        $(this).css({
            'z-index': 1
        });
        $('#signup_form').fadeIn(fv).css({
            'z-index': 2
        });
        $('#myModalLabel').html('注册（Register）');
    });
});
//登录
$('.link_sign_in').click(function(event) {
    event.preventDefault();
    $('#signup_form').fadeOut("slow", function() {
        $(this).css({
            'z-index': 1
        });
        $('#myModalLabel').html('登录（Sign In）');
        $('#signin_form').fadeIn(500).css({
            'z-index': 2
        });
    });
});
toastr.options = {
    "positionClass": "toast-top-center",
    "timeOut": "1500"
};
var os = {
    ajax: function (url, options, callFun, method = "post") {
        const httpUrl = "/";
        options = method === "get" ? options : JSON.stringify(options);
        $.ajax(httpUrl + url, {
            data: options,
            contentType: "application/json",
            dataType: "json",
            type: method, 
            timeout: 30 * 1000,
            success: function (data) {
                callFun(data);
            },
            error: function (xhr, type, errorThrown) {
                if (type === "timeout") {
                    alert("连接超时，请稍后重试！");
                } else if (type === "error") {
                    alert("连接异常，请稍后重试！");
                }
            }
        });
    },
    SetSession: function (key, options) {
        localStorage.setItem(key, JSON.stringify(options));
    },
    GetSession: function (key) {
        const obj = localStorage.getItem(key);
        if (obj !== null) {
            return JSON.parse(obj);
        }
        return null;
    },
    SessionRemove: function (key) {
        localStorage.removeItem(key);
    },
    log: function (data) {
        console.log(JSON.stringify(data));
    }
};
var user = {
    login: function (d) {
        os.ajax("api/bbs/user/login",
            d,
            function (res) {
                if (res.statusCode === 200) {
                    window.location.reload();
                    //os.SetSession("BBSLOGINSUCCESS","1");
                } else {
                    toastr.error(res.message);
                }
            });
    },
    reg: function (d) {
        os.ajax("api/bbs/user/reg",
            d,
            function (res) {
                if (res.statusCode === 200) {
                    toastr.success('注册成功~');
                    $('#signup_form').fadeOut("slow", function () {
                        $(this).css({
                            'z-index': 1
                        });
                        $('#myModalLabel').html('登录（Sign In）');
                        $('#signin_form').fadeIn(500).css({
                            'z-index': 2
                        });
                    });
                } else {
                    toastr.error(res.message);
                }
            });
    },
    forgotpass: function (d) {
        os.ajax("api/bbs/user/forgotpass",
            d,
            function (res) {
                if (res.statusCode === 200) {
                    toastr.success('邮件发送成功~');
                } else {
                    toastr.error(res.message);
                }
            });
    },
    //save question
    saveQuestion: function(d) {
        os.ajax("api/bbs/user/save/question",
            d,
            function (res) {
                if (res.statusCode === 200) {
                    toastr.success('问题发表成功，请等待审核~');
                    setTimeout(() => {
                        window.location = "/bbs";
                    }, 1000);
                } else {
                    toastr.error(res.message);
                }
            });
    },
    //is login
    init: function() {
        //const logins = os.GetSession("BBSLOGINSUCCESS");
        //if (logins!==null) {
            
        //}
        os.ajax("api/bbs/user/getuser",
            {},
            function (res) {
                if (res.statusCode === 200) {
                    $("#rlogin").hide();
                    $(".login-success").show();
                    $("#login_name").html(res.data.loginName);
                    $("#uheadpic").attr('src', res.data.headPic);
                    $('.base-href').attr('href','/bbs/user/'+res.data.guid);
                }
            });
        $("#logout").click(function () {
            os.ajax("api/bbs/user/logout",
                {},
                function (res) {
                    if (res.statusCode === 200) {
                        os.SessionRemove("BBSLOGINSUCCESS");
                        $("#rlogin").show();
                        $(".login-success").hide();
                    } else {
                        toastr.error(res.message);
                    }
                });
        });
        if ($("#question_title").length > 0) {
            $("#question_title").keyup(function () {
                var el = $(this);
                if (el.val().length >= 150) {
                    el.val(el.val().substr(0, 150));
                } else {
                    $("#charNumPoll").text(150 - el.val().length);
                }
            });
            //tags
            $("#question_tags").keyup(function (event) {
                const el = $(this);
                const evt = window.event || e;
                if (el.val()) {
                    if (asktags && asktags.length > 0) {
                        var s = "";
                        $.each(asktags,
                            function(i, item) {
                                if (el.val().toLowerCase() === item.FirstLetter.toLowerCase()) {
                                    s += '<li>' + item.Name + '</li>';
                                }
                            });
                        $(".typeahead").html('');
                        if (s !== "") {
                            $(".tip-add-tag").removeClass("active");
                            $(".typeahead").append(s).show();
                            $(".typeahead li").click(function() {
                                $(".typeahead").hide();
                                const liv = $(this).html();
                                $("#poll_tag_list")
                                    .append('<li class="tag-item"><input type="hidden" name="stag" value="' +
                                        liv +
                                        '">' +
                                        liv +
                                        ' <a href="javascript:void(0)" class="delete"><i class="fa fa-times"></i></a></li>');
                                $("#question_tags").val('');
                                $("#poll_tag_list li a").click(function() {
                                    $(this).parent().remove();
                                });
                            });

                        } else {
                            $(".typeahead").append('<p>没有相关标签</p>').show();
                            $(".tip-add-tag").addClass("active");
                        }
                    }
                } else {
                    $(".tip-add-tag").removeClass("active");
                    $(".typeahead").hide();
                }
            });
            $(".tip-add-tag").click(function () {
                if (!$("#question_tags").val()) {
                    toastr.error('请输入标签关键字'); return;
                }
                $(".typeahead").hide();
                $("#poll_tag_list").append('<li class="tag-item"><input type="hidden" name="stag" value="' + $("#question_tags").val() + '">' + $("#question_tags").val() + ' <a href="javascript:void(0)" class="delete"><i class="fa fa-times"></i></a></li>');
                $("#question_tags").val('');
                $("#poll_tag_list li a").click(function () {
                    $(this).parent().remove();
                });
            });
        }
        
        $(".ask-question").click(function () {
            os.ajax("api/bbs/user/getuser",
                {},
                function (res) {
                    if (res.statusCode === 200) {
                        window.location.href = '/bbs/askquestion';
                    } else {
                        $('#login_register').modal('toggle');
                    }
                });
        });
    }
};
user.init();
var bbs_menu = {
    bg: function() {
        if ($('.modal-backdrop').length === 0) {
            $('body').append('<div class="modal-backdrop fade in"></div>');
            // setTimeout(() => {
            //     $('.modal-backdrop').addClass('in');
            // }, 300);
            $('.modal-backdrop').click(function() {
                $('#cbp-spmenu-s1,#cbp-spmenu-s2,#cbp-spmenu-s3').removeClass('cbp-spmenu-open');
                let that = this;
                $('.modal-backdrop').removeClass('in');
                $('body').removeClass('cbp-spmenu-push-toright cbp-spmenu-push-toleft');
                setTimeout(() => {
                    that.remove();
                }, 300);
            });
        }
    },
    tableMenuDropdown: function() {
        if ($(window).width() <= 997) {
            $(".menu-item-has-children i").click(function() {
                var parent = $(this).parent('li');
                var subMenu = parent.find(".sub-menu");

                if (subMenu.is(":hidden")) {
                    $(this).addClass("rotate");
                    subMenu.slideDown();
                } else {
                    $(this).removeClass("rotate");
                    subMenu.slideUp();
                }
            });
        }
    },
    rightLoginText: function() {
        if ($(window).width() <= 997) {
            $('.login-url').html('登录');
        }
    },
    searchWidth: function () {
        $("#header_search input").focus(function (event) {
            $(this).css('width', '400px');
        });

        $("#header_search input").blur(function (event) {
            $(this).css('width', '350px');
        });
    }
};
bbs_menu.tableMenuDropdown();
bbs_menu.rightLoginText();
bbs_menu.searchWidth();
//移动菜单
if ($('#showTop').length > 0) {
    //移动端菜单
    $('#showTop').click(function() {
        bbs_menu.bg();
        $(this).addClass('active');
        $('#cbp-spmenu-s3').toggleClass('cbp-spmenu-open');
        $('#cbp-spmenu-s1,#cbp-spmenu-s2').removeClass('cbp-spmenu-open');
    });
}
//移动菜单
if ($('#showRightPush').length > 0) {
    //移动端菜单
    $('#showRightPush').click(function() {
        bbs_menu.bg();
        $(this).addClass('active');
        $('#cbp-spmenu-s2').toggleClass('cbp-spmenu-open');
        $('#cbp-spmenu-s1,#cbp-spmenu-s3').removeClass('cbp-spmenu-open');
        $('body').removeClass('cbp-spmenu-push-toright');
        $('body').addClass('cbp-spmenu-push-toleft');
    });
}
//移动菜单
if ($('#showLeftPush').length > 0) {
    //移动端菜单
    $('#showLeftPush').click(function() {
        bbs_menu.bg();
        $(this).addClass('active');
        $('#cbp-spmenu-s1').toggleClass('cbp-spmenu-open');
        $('#cbp-spmenu-s2,#cbp-spmenu-s3').removeClass('cbp-spmenu-open');
        $('body').removeClass('cbp-spmenu-push-toleft');
        $('body').addClass('cbp-spmenu-push-toright');
    });
}
$("#signup_form").validate({
    rules: {
        username: "required",
        password: "required",
        phone: "required",
        email: {
            required: true,
            email: true
        },
        re_password: {
            required: true,
            equalTo: "#password1"
        }
    },
    messages: {
        username: {
            required: fromMsg.Reg_UserName,
            username: fromMsg.Reg_UserName,
        },
        password: fromMsg.Login_PassWord,
        phone: fromMsg.Reg_Phone,
        email: {
            required: fromMsg.Forgot_Pass,
            email: fromMsg.Forgot_Pass,
        },
        re_password: {
            required: fromMsg.Re_Password,
            equalTo: fromMsg.EqualTo_Pass,
        }
    },
    submitHandler: function(form) {
        console.log(JSON.stringify($(form).serializeArray()));
        user.reg($(form).serializeArray());
        return false;
    }
});
$("#forgotpass_form").validate({
    rules: {
        email: {
            required: true,
            email: true
        },
    },
    messages: {
        email: {
            required: fromMsg.Forgot_Pass,
            email: fromMsg.Forgot_Pass,
        },
    },
    submitHandler: function(form) {
        user.forgotpass($(form).serializeArray());
        return false;
    }
});
$("#signin_form").validate({
    rules: {
        username: "required",
        password: "required",
    },
    messages: {
        username: fromMsg.Login_UserName,
        password: fromMsg.Login_PassWord,
    },
    submitHandler: function (form) {
        user.login($(form).serializeArray());
        return false;
    }
});
//ask question
$("#submit_poll").validate({
    rules: {
        title: "required",
        category: "required",
        contents: "required",
    },
    messages: {
        title: "请输入问题标题，最多150个字符",
        category: "请选择分类",
        contents:"请输入问题内容"
    },
    ignore: ":hidden:not(select)",
    submitHandler: function (form) {
        const d = $(form).serializeArray();
        if (!d[2].value) {
            toastr.error("请输入问题内容~");
            return false;
        }
        if (d.length === 3) {
            toastr.error("请选择标签~");
            return false;
        }
        user.saveQuestion(d);
        return false;
    }
});