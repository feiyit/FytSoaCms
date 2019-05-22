tinymce.PluginManager.add("filemanager", function (editor, url) {
    editor.addButton("filemanager", {
        title: "图片管理",
        tooltip: '上传图片',
        icon: 'image',
        onclick: function () {
            layer.open({
                type: 2,
                title: '媒体资源库',
                shadeClose: true,
                shade: 0.2,
                maxmin: false, //开启最大化最小化按钮
                area: ['950px', '600px'],
                content: '/fytadmin/file/cloud/?type=edit',
                zIndex: "10000"
            });
        }
    });
    editor.addMenuItem('filemanager', {
        icon: 'image',
        text: '上传图片',
        context: 'tools',
        onclick: function () {
            layer.open({
                type: 2,
                title: '媒体资源库',
                shadeClose: true,
                shade: 0.2,
                maxmin: false, //开启最大化最小化按钮
                area: ['950px', '600px'],
                content: '/fytadmin/file/cloud/?type=edit',
                zIndex: "10000"
            });
        }
    });
});