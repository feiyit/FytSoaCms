 **[Demo演示地址](http://fytsoacms.netcore.club/fytadmin/)**   

# 2018-12-22更新  
增加MemoryCache缓存和RedisCache缓存  
CoreSDK升级到2.2  
增加前台网站示例——演示地址：[点击查看](http://www.feiyit.net)  
  
  
# 2018-12-15更新  
媒体资源库增加本地管理/上传文件，目前支持本地+七牛云  
增加下载功能模块  
优化所有表单提交按钮状态，在提交之前禁用提交按钮，服务器响应后，提交按钮才可用  
升级ORM版本到v4.9+  
去掉数据库重复表  
解决加密类冲突  


# 2018-11-30更新  
修改登录密码使用RSA加密方式传输  
修改Mysql备份引用出现不兼容问题  
完善Jwt接口权限认证机制  
后台主页工作台布局调整，以及消息通知功能  
后台静态文件压缩bundleconfig.json  
完善内容管理功能  

#### 项目介绍  
FytSoaCms内容管理系统  
使用NetCore2.1开发，  Vs2017       数据Mysql  
NetCore  SDK版本2.1.5， Mysql 5.7.17  版本越高越OK  
系统管理  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;权限管理  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;站点管理  
内容管理  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;栏目管理  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;模板管理  
扩展管理   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;广告管理  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;留言管理    

权限管理目前还没有做到具体功能模块，只做到菜单模块  
后续不断完善权限管理  
  
其它功能模块也在后续不断增加，投票系统，下载系统，微信公众号模块等  
  
第一步：在DB文件夹找到.sql   在Mysql里面执行一下，数据库就建好了  
第二步：修改FytSoa.Web下面的AppSetting里面的链接字符串，并设置FytSoa.Web为启动项
第三步：访问后台地址  http://localhost:4909/fytadmin   
账号：admins   密码：123456  
  
系统集成Swagger  
接口访问地址：http://localhost:4909/swagger/index.html  
记住在下面要先获得Token 然后加入Authorize里面，并保存，就访问其它接口的，获得Token在  
最下面/api/Token/getAdmin获得

#### 软件架构
0. 前端框架使用Layui Vue
1. DB=数据库文件夹  mysql
2. FytSoa.Api=webApi  可在项目中配置使用权限，如后台管理，APP,微信等
3. FytSoa.Common=公共类，提供项目一些常用工具方法
4. FytSoa.Core=数据库实体对象，以及连接对象
5. FytSoa.Extensions=扩展方法
6. FytSoa.Service=业务类，接口和实现       提供代码生成器
7. FytSoa.Web=项目目录，Jwt认证  Swagger可视化接口服务

#### 安装教程

1. 开发工具   visual studio 2017  15.3+  
2. 数据库     Mysql 8.0.12    注意：Linux 默认Mysql是区分大小写的，要设置一下  
3. NetCore  SDK 2.1.5  
4. Orm使用的SqlSugar   网址：http://www.codeisbug.com  
5. 文件存储使用的七牛云，在FytSoa.Extensions  项目中，需要配置在七牛云申请的AK、SK   具体请看七牛云开发文档


#### 参与贡献

1. Fork 本项目
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request


#### 有问题联系我

 **QQ群： **   
858895405——当前Cms同性群友交流社区 :smile:  :smile:   
86594082——ASP.NET MVC技术交流  
726648662——SqlSugar Orm交流群  
 **群内提供，代码生成工具，欢迎大家踊跃加群  ** 


### 项目截图预览

![](http://img.feiyit.com/feiyit/website/else/login.png)
![](http://img.feiyit.com/feiyit/website/else/2.png)
![](http://img.feiyit.com/feiyit/website/else/3.png)
![](http://img.feiyit.com/feiyit/website/else/4.png)
![](http://img.feiyit.com/feiyit/website/else/5.png)
![](http://img.feiyit.com/feiyit/website/else/6.png)
![](http://img.feiyit.com/feiyit/website/else/7.png)
![](http://img.feiyit.com/feiyit/website/else/8.png)
>>>>>>> bca44c268cfa0eb19adf2549da83af1b2a00f6d2
