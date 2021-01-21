###<center> <font size=18 color=#526069>**FytSoa**</font> :heart:  </center>
 ## <center>项目依赖 </center>
<center>
  <img src=https://img.shields.io/badge/NetCoreSDK-2.2-red>
  <img src=https://img.shields.io/badge/ORM-SqlSugar-yellow>
  <img src=https://img.shields.io/badge/缓存服务-Redis-success>
  </center>


## <center>项目源代码 </center>
  <center><a href=https://gitee.com/feiyit/FytSoaCms><img src=https://img.shields.io/badge/Gitee-491-red></a>  
<a href=https://github.com/feiyit/FytSoaCms><img src=https://img.shields.io/badge/GitHub-410-green></a>
<img src=https://img.shields.io/github/license/feiyit/FytSoaCms>
</center>

  
#### **演示地址:**  [http://fytsoa.feiyit.com/](http://fytsoa.feiyit.com/)  
  
```
账号：demo888    密码：demo888
```
  
  
  

#### **简介:**  
 > - 模块化：全新的架构和模块化的开发机制，便于灵活扩展和二次开发。
 > - 模型/栏目/分类信息体系：通过栏目和模型绑定，以及不同的模型类型，不同栏目可以实现差异化的功能，轻松实现诸如资讯、下载、讨论和图片等功能。通过分类信息和栏目绑定，可以自动建立索引表，轻松实现复杂的信息检索。
 > - FytSoa是一套基于NetCore+SqlSugar+Layui开发出来的框架，源代码完全开源。
 > - 支持SQLServer、MySQL、Oracle、PostgreSQL、SQLite等多数据库类型。模块化设计，层次结构清晰。  
 > - Jwt权限认证，操作权限控制精密细致，对所有管理链接都进行权限验证，可控制到导航菜单、功能按钮。提高开发效率及质量。
 > - 常用类封装，日志、缓存、验证、字典、文件（本地、七牛云）。等等，目前兼容浏览器（Chrome、Firefox、360浏览器极速模式等）
 > - 适用范围：可以开发OA、ERP、BPM、CRM、WMS、TMS、MIS、BI、电商平台后台、物流管理系统、快递管理系统、教务管理系统等各类管理软件。  
  

 
  
#### **开发者信息:**   


1. 系统名称：FytSoa  
1. 作者：飞易腾科技有限公司   CTO  平头哥  
1. QQ群：1060012125 
1. 官网网址：[http://www.feiyit.com/](http://www.feiyit.com/)  
1. 开源协议：MIT License  
  

#### **系统技术点:**   

1. 核心框架：NetCore Razor Pages  
1. ORM：SqlSugar [http://www.codeisbug.com/](http://www.codeisbug.com/)  
1. 缓存依赖：CSRedis    MemoryCache  
1. 日志管理：Nlog   登陆日志、操作日志、异常日志
1. 工具类：Aes加密、Md5加密、RSA加密、Des加密  
1. 静态分布式：七牛云存储
1. 基于Redis持久化任务调度系统
1. Jwt多角色接口安全机制
1. 过滤器按钮权限控制、日志收集
1. 其他模块：微信多账号管理、自定义菜单、素材管理、消息管理 
 
  


#### 软件架构
- 前端框架使用Layui Vue  
- DB=数据库文件夹  默认是：mysql  
- FytSoa.Api=webApi  可在项目中配置使用权限，如后台管理，APP,微信等  
- FytSoa.Common=公共类，提供项目一些常用工具方法  
- FytSoa.Core=数据库实体对象，以及连接对象  
- FytSoa.Extensions=扩展方法  
- FytSoa.Service=业务类，接口和实现       提供代码生成器  
- FytSoa.Web=项目目录，Jwt认证  Swagger可视化接口服务

### 环境部署
#### 准备工作
```
- 开发工具   visual studio 2017/2019
- 数据库     Mysql 5.7+    注意：Linux 默认Mysql是区分大小写的，要设置一下  
- NetCore   SDK 2.2+
- ORM       SqlSugar   网址：http://www.codeisbug.com  
- 文件存储   七牛云，在FytSoa.Extensions  项目中，需要配置在七牛云申请的AK、SK   具体请看七牛云开发文档
```
#### 运行系统
```bash
# 安装redis 服务
git https://github.com/redis/redis/releases/tag

# 导入数据库
进入文件目录DB文件夹中，创建数据库，执行数据库脚本

# 修改FytSoa.Web下面的appsettings.json文件中的数据库连接字符串
"MySqlConnectionString": "server=localhost;database=fyt_cms;uid=root;pwd=123456;charset='utf8';SslMode=None"

# 运行FytSoa.Web项目（注：无需单独运行FytSoa.Api项目）
dotnet run urls=http://*:4012
```

#### 参与贡献

1. Fork 本项目
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request



### 项目截图

![](https://gitee.com/uploads/images/2019/0504/220641_e086f581_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220645_63a5e40b_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220641_4289c361_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220641_86b5af0c_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220646_2fed6681_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220646_3425c5e4_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220646_aa82777f_645017.png)
![](https://gitee.com/uploads/images/2019/0504/220641_6d865c6b_645017.png)
 