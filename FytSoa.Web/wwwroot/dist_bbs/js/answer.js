tinymce.init({
    selector: '#insert_poll',
    height: 200,
    readonly: os.GetSession("BBSLOGINSUCCESS")!==null?false:true,
    plugins: 'link lists image code table colorpicker textcolor wordcount contextmenu codesample ',
    toolbar:
        'bold italic underline codesample  | fontsizeselect | forecolor backcolor | alignleft aligncenter alignright alignjustify | bullist numlist | outdent indent blockquote | undo redo | link unlink image code | removeformat',
    branding: false,
    menubar: false
});
$(function () {
    if (os.GetSession("BBSLOGINSUCCESS") !== null) {
        $(".alert-warning").remove();
    }
    $("#submit_reply").click(function() {
        var content = tinyMCE.editors[0].getContent();
        if (!content) {
            toastr.error("请输入回答的内容");
            return;
        }
        os.ajax("api/bbs/user/save/answer", { QuestionGuid: $("#Ask").val(), Content: content }, function(res) {
            if (res.statusCode === 200) {
                toastr.success('回答成功~');
                setTimeout(() => {
                    window.location.reload();
                }, 1000);
            } else {
                toastr.error(res.message);
            }
        });
    });
});