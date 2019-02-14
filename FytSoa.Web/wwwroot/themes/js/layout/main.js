var main_vm = new Vue({
    el: '#mainapp',
    data: {
        menulist: [],
        messList: [],
        messCount: 0,
        isDown: false
    },
    created: function () {
        var that = this;
        document.querySelector('body').addEventListener('click', function (e) {
            if (e.target.id === 'notificdown' || e.target.id === 'notificicon') {
                that.isDown = true;
            } else {
                that.isDown = false;
            }
        });
    },
    methods: {
        clearCache: function () {
            os.success('这里是测试，可以ajax到后台清除网站缓存');
        },
        godown: function () {
            this.isDown = this.isDown ? false : true;
        }
    }
});

var os, rm_vm = new Vue({
    el: '#rmapp',
    data: {
        tmlist: [],
        list: []
    }
});
layui.config({
    base: '/themes/js/modules/'
}).use(['element', 'layer', 'jquery', 'common', 'pjax'], function () {
    var element = layui.element, $ = layui.jquery;
    os = layui.common;
    os.ajax('api/message/page', { limit: 5 }, function (res) {
        if (res.statusCode === 200) {
            main_vm.messCount = res.data.totalItems;
            main_vm.messList = res.data.items;
        } else {
            os.error(res.message);
        }
    });
    os.get('api/menu/authmenu', { parm: $("#UserGuid").val() }, function (res) {
        //console.log(res);
        if (res.statusCode === 200) {
            $.each(res.data, function (index, item) {
                if (item.layer === 1) {
                    main_vm.menulist.push(item);
                }
            });
            rm_vm.tmlist = main_vm.menulist;
            rm_vm.list = res.data;
            main_vm.$nextTick(function () {
                element.render();
                //定位到菜单
                $(".layui-bg-black .fyt-nav-tree li a").each(function () {
                    if (window.location.pathname === $(this).attr('href')) {
                        $(".layui-bg-black .fyt-nav-tree li").removeClass('layui-nav-itemed');
                        $(this).parents('.layui-nav-item').addClass('layui-nav-itemed');
                        $(this).parent().addClass('layui-this');
                    }
                });
                $(".load8").fadeOut(200);
                element.on('nav(topmenu)', function (elem) {
                    var that = $(elem);
                    var topIndex = that.data('index');
                    $("#rmapp .layui-side-scroll ul").addClass('layui-hide');
                    $("#rmapp .layui-side-scroll ul").eq(topIndex).removeClass('layui-hide');
                });
                $("#rmapp .layui-side-scroll ul").eq(0).removeClass('layui-hide');
            });
        } else {
            os.error(res.message);
        }
    });
    $('.layui-layout-admin').pjax('a[data-pjax]', '#main-container',
        {
            fragment: "#container",
            cache: false,
            show: 'fade'
        }
    );
    $(document).on('pjax:start', function () { NProgress.start(); $(".load8").show(); });
    $(document).on('pjax:end', function () { NProgress.done(); $(".load8").fadeOut(200); });
});