### 特别提示 :heart:   
  
项目依赖  
```
本项目运行NetCore SDK版本为2.2+     数据库：Mysql 5.7+    使用SqlSugar  ORM
```
  
## 项目源代码地址
  **github地址**：[https://github.com/feiyit/FytSoaCms](https://github.com/feiyit/FytSoaCms)  
**码云地址**：[https://gitee.com/feiyit/FytSoaCms](https://gitee.com/feiyit/FytSoaCms)  

  

#### **简介:**  
 > - 模块化：全新的架构和模块化的开发机制，便于灵活扩展和二次开发。
 > - 模型/栏目/分类信息体系：通过栏目和模型绑定，以及不同的模型类型，不同栏目可以实现差异化的功能，轻松实现诸如资讯、下载、讨论和图片等功能。通过分类信息和栏目绑定，可以自动建立索引表，轻松实现复杂的信息检索。
 > - FytSoaCms是一套基于NetCore+SqlSugar+Layui开发出来的框架，源代码完全开源，并支持前后端分离。
 > - 支持SQLServer、MySQL、Oracle、PostgreSQL、SQLite等多数据库类型。模块化设计，层次结构清晰。  
 > - Jwt权限认证，操作权限控制精密细致，对所有管理链接都进行权限验证，可控制到导航菜单、功能按钮。提高开发效率及质量。
 > - 常用类封装，日志、缓存、验证、字典、文件（本地、七牛云）。等等，目前兼容浏览器（Chrome、Firefox、360浏览器等）
 > - 适用范围：可以开发OA、ERP、BPM、CRM、WMS、TMS、MIS、BI、电商平台后台、物流管理系统、快递管理系统、教务管理系统等各类管理软件。  
  

 **Demo演示地址[ http://fytsoacms.netcore.club/fytadmin/](http://fytsoacms.netcore.club/fytadmin/)**  
  账号：demo888    密码：demo  
  
 
  
#### **开发者信息:**   


1. 系统名称：FytSoaCms  
1. 作者：飞易腾科技有限公司CTO  平头哥  
1. QQ群：858895405  
1. 官网网址：[http://www.feiyit.com/](http://www.feiyit.com/)  
1. 开源协议：MIT License  
  

#### **系统技术点:**   

1. 核心框架：NetCore Razor Pages  
1. ORM：SqlSugar [http://www.codeisbug.com/](http://www.codeisbug.com/)  
1. 缓存依赖：CSRedis    MemoryCache  
1. 日志管理：Nlog   登陆日志、操作日志、异常日志
1. 工具类：Aes加密、Md5加密、RSA加密、Des加密  
1. 静态分布式：七牛云存储
1. 其他模块：微信多账号管理、自定义菜单、素材管理、消息管理 
 
  
===================================================================================================  
  
  
  
#### **下一步工作计划**   

- 投票模块完成  
- 增加商城模块-分销  
- 前端小程序-app使用uni-app  
- 后台前端框架迁移到vue+element  
 
  
  
  
#### **最近由于个人和工作原因导致更新缓慢，最近稳定了，我会不定时更新，会根据群友和朋友们反馈信息继续完善**   
# 2019-07-03更新 



1、【新增】社区网站模块  
2、【新增】首页，默认不是网站了  
3、【新增】示例网站图片增加说明   
4、【新增】后台用户管理、用户组管理   
5、【新增】底层仓储新增返回Count和是否存在方法  
6、【修复】站点管理，新增bug和图片上传影响其他问题  
7、【修复】广告管理，时间控件一闪不出现的问题  
8、【修复】栏目管理，增加栏目时没有根据站点区分  
9、【升级】Layui升级到5.X  
10、【升级】ORM升级到5.X  


   
  
  
  
  
# 2019-05-04更新  
1、【新增】增加日志到数据库模块（登录日志、操作日志、异常日志）  
2、【完成】微信自定义菜单-可选择素材模块  
3、【完成】微信素材管理，添加、同步到微信平台  
4、【新增】CURD（增删改查）权限验证模块  
5、【新增】Redis缓存单例实现  
6、【优化】登录保存方式，可在appsettings.json配置  可配置Cookie和Session方式  
微信模块如下：  
![输入图片说明](https://gitee.com/uploads/images/2019/0504/222321_c22ca5dc_645017.png "在这里输入图片标题")  
![输入图片说明](https://gitee.com/uploads/images/2019/0504/222447_81666fcd_645017.png "在这里输入图片标题")  
![输入图片说明](https://gitee.com/uploads/images/2019/0504/222509_9e52adcf_645017.png "在这里输入图片标题")  
![输入图片说明](https://gitee.com/uploads/images/2019/0504/222556_0f5da63c_645017.png "在这里输入图片标题")  
![输入图片说明](https://gitee.com/uploads/images/2019/0504/222616_64e64810_645017.png "在这里输入图片标题")  
![输入图片说明](https://gitee.com/uploads/images/2019/0504/222742_1743bf00_645017.png "在这里输入图片标题")
   
   
 **

### 下一步计划，要增加功能模块，投票，然后在做一个社区，欢迎大家一起参与，QQ群：858895405  
** 
  
 
  



# 2019-03-17更新  
增加微信相关模块  
1、【新增】微信多账号设置  
2、【新增】自定义菜单管理-同步  
3、【新增】微信素材读取，未完成——保存到本地和展示  
4、【新增】手机端网站[点击查看](http://h5.feiyit.com)  使用了shtml 所以在core里面不支持，需要单独提出一个站点，如果不提出，把shtml改为html，并且把  <!-- #include file="/template/header.html" -->去掉  
5、【新增】自动注册
6、升级到最新的ORM  

增加前台网站示例——演示地址：[点击查看](http://www.feiyit.com)  


# 2018-12-22更新  
增加MemoryCache缓存和RedisCache缓存  
CoreSDK升级到2.2  
增加前台网站示例——演示地址：[点击查看](http://www.feiyit.com)  
  
  
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

![](https://gitee.com/uploads/images/2019/0504/220641_e086f581_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220645_63a5e40b_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220641_4289c361_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220641_86b5af0c_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220646_2fed6681_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220646_3425c5e4_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220646_aa82777f_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220641_6d865c6b_645017.png)
 