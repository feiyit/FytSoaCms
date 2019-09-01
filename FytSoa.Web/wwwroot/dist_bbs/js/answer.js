
$(function () {
    os.ajax("api/bbs/user/getuser",
        {},
        function (res) {
            var isedit = false;
            if (res.statusCode !== 200) {
                isedit = true;
            } else {
                $(".alert-warning").remove();
            }
            tinymce.init({
                selector: '#insert_poll',
                height: 200,
                readonly: isedit,
                plugins: 'link lists image code table colorpicker textcolor wordcount contextmenu codesample ',
                toolbar:
                    'bold italic underline codesample  | fontsizeselect | forecolor backcolor | alignleft aligncenter alignright alignjustify | bullist numlist | outdent indent blockquote | undo redo | link unlink image code | removeformat',
                branding: false,
                menubar: false
            });
        });
    
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