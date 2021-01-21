var imgtotal = 0, page = 0, $, $$, oc, os, vm = new Vue({
    el: '#app',
    data: {
        typeOpen: 'layui-hide',
        typeModel: {
            Guid: '',
            ParentGuid: '',
            Name: '',
            Level: 0,
            Type: 0,
            EnName: ''
        },
        parm: {
            prefix: 'website',
            marker: ''
        },
        localParm: {
            page: 1,
            limit: 40,
            total: 0,
            where: '/'
        },
        list: [],
        noShow: 'layui-hide',
        more: 'layui-hide',
        active: [],
        menuActive: '',
        firstMenu: '',
        tagName: '全部分类',
        typeMenu: [],
        upStatus: 'layui-hide',
        tabActive: 0
    },
    created: function () {

    },
    updated: function () {
        //layui.form.render('select');
    },
    methods: {
        //切换类型
        savetype: function (t) {
            this.active = [];
            this.tabActive = t;
            this.typeModel.Type = t;
            this.list = [];
            if (t === 1) {
                this.parm.prefix = '';
                $('#cloudup').removeClass('layui-hide');
                $('#localup').addClass('layui-hide');
                vm.parm.marker = '';
                oc.init();
            } else {
                $('#cloudup').addClass('layui-hide');
                $('#localup').removeClass('layui-hide');
                this.localParm.page = 1;
                this.localParm.where = '/';
                $('#localpath').val("/");
                oc.initLocal();
            }
            oc.initType();
        },
        //获得文件路径，后面的文件名称
        imgLastUrl: function (s) {
            var index = s.lastIndexOf("\/");
            return s.substring(index + 1, s.length);
        },
        //获得文件扩展名，不包含.
        getFileExt: function (m) {
            var str = '';
            var fext = m.substr(m.lastIndexOf(".")).toLowerCase().replace('.', '');
            return '<i class="file-type-' + fext + ' file-preview"></i>';
        },
        //判断是否是图片
        isImage: function (m) {
            m = m.toLowerCase();
            if (!m.extMatch(imgExt)) {
                return false;
            }
            return true;
        },
        //点击确定，返回给父窗体的值
        saveSelect: function () {
            if (this.active.length==0) {
                os.error('请选择图片~'); return;
            }
            var imgControl = os.getUrlParam('img');
            var index = parent.layer.getFrameIndex(window.name);
            var control = os.getUrlParam('control');
            var type = os.getUrlParam('type');
            var frameid = os.getUrlParam('frameid');
            //非iframe 文本框选择文件
            if (type === 'sign') {
                $$('#' + control).val(this.active[0].name);
            }
            //弹出表单选择文件
            if (type === 'form') {
                var formframes = $$("#" + frameid)[0].contentWindow;
                formframes.oc.fileSave(this.active[0].name);
            }
            //弹出表单-产品-多选
            if (type === 'multiple') {
                var formframes = $$("#" + frameid)[0].contentWindow;
                formframes.oc.multipleSave(this.active[0].name);
            }
            //编辑器选择文件
            if (type === 'edit') {
                //window.parent.oc.setContent(this.active);
                var formframes = $$("#" + frameid)[0].contentWindow;
                formframes.oc.setContent(this.active);
            }
            //多选文件
            if (type === 'many') {
                var formframes = $$("#" + frameid)[0].contentWindow;
                formframes.oc.setMany(this.active);
            }
            //iframe  选择图片
            if (type === 'iframe') {
                var frames = $$("#" + frameid)[0].contentWindow;
                frames.document.getElementById(control).value = this.active[0].name;
            }
            parent.layer.close(index);
        },
        //选择图片操作
        selectImg: function (m) {
            var that = this;
            //最多只能选择6张图片，并判断是否存在，如果存在，则删除
            var iscz = false;
            for (var i = 0; i < that.active.length; i++) {
                if (that.active[i] == m) {
                    iscz = true;
                    var sy = that.active.indexOf(m);
                    that.active.splice(sy, 1);
                }
            }
            if (!iscz) {
                if (that.active.length === 6) {
                    os.error('最多只能选择6张图片~');
                    return;
                }
                this.active.push(m);
            }
        },
        //判断图片是否在选择的里面
        isSelect: function (m) {
            var that = this;
            var cz = false;
            for (var i = 0; i < that.active.length; i++) {
                if (that.active[i].name == m) {
                    cz = true;
                }
            }
            return cz;
        },
        //点击左侧分类，显示图片列表
        goTypeList: function (parentM, thisM) {
            this.active = [];
            this.tagName = parentM.name + ' / ' + thisM.name;
            this.menuActive = thisM.enName;
            this.firstMenu = parentM.enName;
            this.parm.marker = '';
            this.parm.prefix = parentM.enName + '/' + thisM.enName;
            this.list = [];
            if (this.tabActive === 1) {
                oc.init();
            }
            else {
                this.localParm.page = 1;
                this.localParm.where = parentM.enName + '/' + thisM.enName;
                $('#localpath').val(parentM.enName + '/' + thisM.enName);
                oc.initLocal();
            }
        },
        //点击图片列表，加载更多
        getMore: function () {
            if (this.tabActive == 1) {
                //云
                oc.init();
            }
            else {
                //本地
                this.localParm.page = this.localParm.page + 1;
                oc.initLocal();
            }
        },
        //关闭当前云媒体窗体
        closeDig: function () {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        },
        //关闭弹出添加分类的模态框
        closeType: function () {
            this.clearForm();
            this.typeOpen = 'layui-hide';
        },
        //弹出添加分类的模态框
        addType: function () {
            this.typeOpen = 'layui-show';
        },
        //编辑图片分类
        editType: function (m) {
            this.typeOpen = 'layui-show';
            this.typeModel.Guid = m.guid;
            this.typeModel.ParentGuid = m.parentGuid;
            this.typeModel.Name = m.name;
            this.typeModel.Level = m.level;
            this.typeModel.EnName = m.enName;
            this.typeModel.Type = this.tabActive;
            oc.editTypeSlect(m.parentGuid);
        },
        //删除图片分类
        delType: function (m) {
            var delIndex = layer.confirm('确定要删除分类吗？删除后可能找不到文件了', {
                btn: ['确定', '取消']
            }, function () {
                oc.delType(m);
                layer.close(delIndex);
            });
        },
        //清空添加分类模态框表单内容
        clearForm: function () {
            this.typeModel.Guid = '';
            this.typeModel.ParentGuid = '';
            this.typeModel.Name = '';
            this.typeModel.EnName = '';
            this.typeModel.Level = 0;
        },
        //云删除图片
        delImg: function (m) {
            var that = this;
            var delIndex = layer.confirm('确定要删除吗？删除后可能找不到文件了', {
                btn: ['确定', '取消']
            }, function () {
                if (that.tabActive == 1) {
                    oc.delImage(m);
                }
                else {
                    oc.delLocalImage(m);
                }
                layer.close(delIndex);
            });
        },
        //云上传图片
        upload: function () {
            var that = this;
            if (this.menuActive === '') {
                os.error('请选择图片分类~');
                return;
            }
            $("#cloudupfile").click();
            $("#cloudupfile").off("change");
            $("#cloudupfile").change(function () {
                vm.upStatus = 'layui-show';
                var file = this.files[0];
                oc.upload(that.firstMenu + "/" + that.menuActive + "/", file);
            });
        }
    }
});
layui.config({
    base: '/themes/js/modules/'
}).use(['element', 'layer', 'jquery', 'common', 'form', 'upload'], function () {
    var form = layui.form, element = layui.element, upload = layui.upload;
    os = layui.common;
    $ = layui.jquery;
    $$ = parent.layui.jquery;
    //本地上传

    
    upload.render({
        elem: '#localup' //绑定元素
        , multiple: true
        , headers: os.getToken()
        , size: 0
        , number: 5
        , progress: function (value) {//上传进度回调 value进度值
            element.progress('uppro', value+ '%');
            if (value === 100) {
                vm.upStatus = 'layui-hide';
            }
        }
        , accept:'file'
        //, exts: 'jpg|png|gif|bmp|jpeg|zip|rar|7z|doc|docx|ppt|pptx|xls|xlsx|txt|mp3|mp4|flv|pdf'
        , url: '/api/localfiles/upload' //上传接口
        , data: {
            path: function () { return $('#localpath').val() }
        }
        , before: function () {
            vm.upStatus = 'layui-show';
        }
        , done: function (res) {
            if (res.statusCode === 200) {
                os.success('上传成功~');
                vm.list = [];
                oc.initLocal();
            } else {
                vm.upStatus = 'layui-hide';
                os.error(res.message);
            }
            
        }
        , error: function () {
            vm.upStatus = 'layui-hide';
            os.error('上传失败');
        }
    });
    oc = {
        //初始化，加载云资源列表
        init: function () {
            os.tableLoading();
            os.ajax('api/cloudfiles/list', vm.parm, function (res) {
                if (res.code === 200) {
                    $.each(res.list, function (i, item) {
                        vm.list.push(item);
                    });
                    if (vm.list === null || vm.list.length === 0) {
                        vm.noShow = 'layui-show';
                        vm.more = 'layui-hide';
                    } else {
                        vm.noShow = 'layui-hide';
                        if (res.page !== null && res.page !== '') {
                            vm.parm.marker = res.page;
                            vm.more = 'layui-show';
                        } else {
                            vm.more = 'layui-hide';
                        }
                    }
                    vm.$nextTick(function () {
                        os.tableLoadingClose();
                    });
                } else {
                    os.error(res.message);
                }
            });
        },
        //加载本地图片
        initLocal: function () {
            os.tableLoading(); os.log(vm.localParm);
            os.ajax('api/localfiles/list', vm.localParm, function (res) {
                
                if (res.code === 200) {
                    //总页数
                    vm.localParm.total = parseInt(res.page);
                    $.each(res.list, function (i, item) {
                        vm.list.push(item);
                    });
                    if (vm.list === null || vm.list.length === 0) {
                        vm.noShow = 'layui-show';
                        vm.more = 'layui-hide';
                    } else {
                        vm.noShow = 'layui-hide';
                        if (vm.localParm.page < vm.localParm.total) {
                            vm.more = 'layui-show';
                        } else {
                            vm.more = 'layui-hide';
                        }
                    }
                    vm.$nextTick(function () {
                        os.tableLoadingClose();
                    });
                } else {
                    os.error(res.message);
                }
            });
        },
        //删除本地图片
        delLocalImage: function (m) {
            os.ajax('api/localfiles/delete', { filename: m.name }, function (res) {
                if (res.statusCode === 200) {
                    vm.list = [];
                    oc.initLocal();
                }
                else {
                    os.error(res.message);
                }
            });
        },
        //云删除图片
        delImage: function (m) {
            os.ajax('api/cloudfiles/delete', { filename: m.name }, function (res) {
                if (res.code === 200) {
                    vm.list = [];
                    oc.init();
                }
                else {
                    os.error(res.message);
                }
            });
        },
        //初始化左侧图片分类
        initType: function () {
            os.ajax('api/cloudfiles/type/list', { types: vm.tabActive }, function (res) {
                if (res.statusCode === 200) {
                    vm.typeMenu = res.data;
                    vm.$nextTick(function () {
                        form.render('select');
                        element.render();

                    });
                }
                else {
                    os.error(res.message);
                }
            });
        },
        //编辑图片分类
        editTypeSlect: function (v) {
            $("#ParentGuid").val(v);
            form.render('select');
        },
        //删除图片分类
        delType: function (m) {
            os.ajax('api/cloudfiles/type/del', { parm: m.guid }, function (res) {
                if (res.statusCode === 200) {
                    os.success('删除成功~');
                    oc.initType();
                }
                else {
                    os.error(res.message);
                }
            });
        },
        //云上传
        upload: function (prefix, file) {
            var config = {
                useCdnDomain: true,
                region: qiniu.region.z1
            };
            var putExtra = {
                fname: "",
                params: {},
                mimeType: null
            };
            var error = function (err) {
                os.error(err.message);
            };
            var complete = function (res) {
                //os.log(res);
                os.success('上传成功~');
                vm.list = [];
                oc.init();
                setTimeout(function () {
                    layer.close(uploading);
                }, 500);
            };
            var observer = {
                next(res) {
                    element.progress('uppro', parseInt(res.total.percent) + '%');
                    if (res.total.percent === 100) {
                        vm.upStatus = 'layui-hide';
                        os.success('上传成功~');
                        vm.list = [];
                        oc.init();
                    }
                },
                error(err) {
                    os.error(err.message);
                },
                complete(res) {
                }
            }
            var key = file.name;
            os.ajax('api/cloudfiles/token', null, function (res) {
                if (res.code === 200) {
                    key = res.page + prefix + key; 
                    var observable = qiniu.upload(file, key, res.token, putExtra, config);
                    var subscription = observable.subscribe(observer) // 上传开始
                }
                else {
                    os.error(res.message);
                }
            });
        }
    };
    //初始化本地源列表
    oc.initLocal();
    //初始化左侧图片分类
    oc.initType();
    //添加/编辑图片分类，用这个方法做了为空验证
    form.on('submit(submit)', function (data) {
        var urls = 'api/cloudfiles/type/add';
        if (vm.typeModel.Guid !== '') {
            urls = 'api/cloudfiles/type/modify';
        }
        //os.log(vm.typeModel);
        os.ajax(urls, vm.typeModel, function (res) {
            if (res.statusCode === 200) {
                vm.closeType();
                oc.initType();
                os.success('分类保存成功~');
            }
            else {
                os.error(res.message);
            }
        });
        return false;
    });
    //解决vue  layui 下拉选择不赋值问题
    form.on('select(ParentGuid)', function (data) {
        vm.typeModel.ParentGuid = data.value;
    });
});
var imgExt = new Array(".png", ".jpg", ".jpeg", ".bmp", ".gif");//图片文件的后缀名
var docExt = new Array(".doc", ".docx");//word文件的后缀名
var xlsExt = new Array(".xls", ".xlsx");//excel文件的后缀名
var cssExt = new Array(".css");//css文件的后缀名
var jsExt = new Array(".js");//js文件的后缀名
//获取文件名后缀名
String.prototype.extension = function () {
    var ext = null;
    var name = this.toLowerCase();
    var i = name.lastIndexOf(".");
    if (i > -1) {
        var ext = name.substring(i);
    }
    return ext;
};
//判断Array中是否包含某个值
Array.prototype.contain = function (obj) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] === obj)
            return true;
    }
    return false;
};
String.prototype.extMatch = function (extType) {
    if (extType.contain(this.extension()))
        return true;
    else
        return false;
};