layui.use(['element','jquery'], function(){
    var $=layui.jquery;
    $(window).resize(
		bodysize
    );
    bodysize();
    function bodysize()
    {
        $("body").height($(window).height())
    }
});