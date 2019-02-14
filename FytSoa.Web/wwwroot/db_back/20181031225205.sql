-- MySqlBackup.NET 2.0.9.2
-- Dump Time: 2018-10-31 22:52:05
-- --------------------------------------
-- Server version 5.7.19 MySQL Community Server (GPL)


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of cms_advclass
-- 

DROP TABLE IF EXISTS `cms_advclass`;
CREATE TABLE IF NOT EXISTS `cms_advclass` (
  `Guid` varchar(50) NOT NULL,
  `ParentGuid` varchar(50) NOT NULL DEFAULT '0' COMMENT '父ID',
  `Title` varchar(50) NOT NULL COMMENT '栏位名称',
  `Flag` varchar(50) NOT NULL COMMENT '栏位类型',
  `Width` varchar(20) DEFAULT NULL COMMENT '宽度',
  `Height` varchar(20) DEFAULT NULL COMMENT '高度',
  `Summary` varchar(500) DEFAULT NULL COMMENT '说明',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `SiteID` int(11) NOT NULL DEFAULT '0' COMMENT '站点ID',
  `UpdateDate` datetime NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`Guid`)
) ENGINE=MyISAM AUTO_INCREMENT=20 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_advclass
-- 

/*!40000 ALTER TABLE `cms_advclass` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_advclass` ENABLE KEYS */;

-- 
-- Definition of cms_advlist
-- 

DROP TABLE IF EXISTS `cms_advlist`;
CREATE TABLE IF NOT EXISTS `cms_advlist` (
  `Guid` varchar(50) NOT NULL,
  `ClassGuid` varchar(50) NOT NULL DEFAULT '0' COMMENT '栏目ID',
  `Title` varchar(200) NOT NULL COMMENT '广告位名称',
  `Types` int(11) NOT NULL DEFAULT '0' COMMENT '广告位类型',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否启用',
  `ImgUrl` varchar(255) DEFAULT NULL COMMENT '图片地址',
  `LinkUrl` varchar(500) DEFAULT NULL COMMENT '链接地址',
  `LinkSummary` varchar(200) DEFAULT NULL COMMENT '链接描述',
  `Target` varchar(50) NOT NULL COMMENT '打开窗口类型',
  `AdvCode` varchar(2000) DEFAULT NULL COMMENT '广告位说明',
  `IsTimeLimit` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否启用时间显示',
  `BeginTime` datetime DEFAULT NULL COMMENT '开始时间',
  `EndTime` datetime DEFAULT NULL COMMENT '结束时间',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `Hits` int(11) NOT NULL DEFAULT '0' COMMENT '点击量',
  `UpdateDate` datetime NOT NULL COMMENT '更新时间',
  `Summary` varchar(2000) DEFAULT NULL COMMENT '广告位说明',
  PRIMARY KEY (`Guid`)
) ENGINE=MyISAM AUTO_INCREMENT=44 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_advlist
-- 

/*!40000 ALTER TABLE `cms_advlist` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_advlist` ENABLE KEYS */;

-- 
-- Definition of cms_article
-- 

DROP TABLE IF EXISTS `cms_article`;
CREATE TABLE IF NOT EXISTS `cms_article` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '自动增长',
  `ColumnId` int(11) NOT NULL DEFAULT '0' COMMENT '栏目ID',
  `Types` int(11) NOT NULL DEFAULT '0' COMMENT '0=新闻1=多图片',
  `Title` varchar(200) NOT NULL COMMENT '文章标题',
  `TitleColor` varchar(20) DEFAULT NULL COMMENT '文章标题颜色',
  `SubTitle` varchar(200) DEFAULT NULL COMMENT '文章副标题',
  `Author` varchar(20) DEFAULT NULL COMMENT '作者',
  `Source` varchar(20) DEFAULT NULL COMMENT '来源',
  `IsLink` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否外链',
  `LinkUrl` varchar(255) DEFAULT NULL COMMENT '外部链接地址',
  `Tag` varchar(200) DEFAULT NULL COMMENT '文章标签',
  `ImgUrl` varchar(255) DEFAULT NULL COMMENT '文章宣传图',
  `ThumImg` varchar(255) DEFAULT NULL COMMENT '文章缩略图',
  `VideoUrl` varchar(255) DEFAULT NULL COMMENT '视频链接地址',
  `IsTop` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否置顶',
  `IsHot` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否热点',
  `IsScroll` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否滚动',
  `IsSlide` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否幻灯',
  `IsComment` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否允许评论',
  `IsWap` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否手机站显示',
  `IsRecyc` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否在回收站',
  `Audit` bit(1) NOT NULL DEFAULT b'1' COMMENT '审核状态',
  `Summary` varchar(2000) DEFAULT NULL COMMENT '文章摘要',
  `Content` text COMMENT '文章内容',
  `Hits` int(11) NOT NULL DEFAULT '0' COMMENT '点击量',
  `DayHits` int(11) NOT NULL DEFAULT '0' COMMENT '当日点击量',
  `WeedHits` int(11) NOT NULL DEFAULT '0' COMMENT '星期点击量',
  `MonthHits` int(11) NOT NULL DEFAULT '0' COMMENT '月点击量',
  `LastHitDate` datetime DEFAULT NULL COMMENT '最后点击时间',
  `EditDate` datetime DEFAULT NULL COMMENT '编辑时间',
  `AddDate` datetime DEFAULT NULL COMMENT '添加时间',
  `DelDate` datetime DEFAULT NULL COMMENT '删除到回收站时间',
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=1000 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_article
-- 

/*!40000 ALTER TABLE `cms_article` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_article` ENABLE KEYS */;

-- 
-- Definition of cms_column
-- 

DROP TABLE IF EXISTS `cms_column`;
CREATE TABLE IF NOT EXISTS `cms_column` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '自动递增',
  `Number` varchar(50) NOT NULL COMMENT '栏目编号',
  `Title` varchar(50) NOT NULL COMMENT '栏目标题',
  `EnTitle` varchar(50) DEFAULT NULL COMMENT '英文栏位名称',
  `SubTitle` varchar(50) DEFAULT NULL COMMENT '栏位副标题',
  `ParentId` int(11) NOT NULL DEFAULT '0' COMMENT '父栏目',
  `ClassList` varchar(50) DEFAULT NULL COMMENT '栏位集合',
  `ClassLayer` int(11) NOT NULL DEFAULT '0' COMMENT '栏位等级',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `TypeID` int(11) NOT NULL DEFAULT '0' COMMENT '栏目类型',
  `GoodsTypeID` int(11) NOT NULL COMMENT '商品类别ID',
  `Attr` varchar(20) DEFAULT NULL COMMENT '栏位属性',
  `TempId` int(11) NOT NULL DEFAULT '0' COMMENT '模板ID',
  `TempName` varchar(50) NOT NULL COMMENT '模板名称',
  `TempUrl` varchar(255) NOT NULL COMMENT '模板地址',
  `ImgUrl` varchar(255) DEFAULT NULL COMMENT '栏位图片',
  `LinkUrl` varchar(255) DEFAULT NULL COMMENT '外部链接地址',
  `WapLinkUrl` varchar(255) DEFAULT NULL COMMENT '移动端链接地址',
  `IsTopShow` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否顶部显示',
  `IsWapShow` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否手机端展示',
  `KeyWord` varchar(100) DEFAULT NULL COMMENT '关键词',
  `Summary` varchar(500) DEFAULT NULL COMMENT '描述',
  `Content` text COMMENT '内容',
  `SiteID` int(11) NOT NULL DEFAULT '0' COMMENT '站点ID',
  `UpdateDate` datetime DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=1000 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_column
-- 

/*!40000 ALTER TABLE `cms_column` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_column` ENABLE KEYS */;

-- 
-- Definition of cms_comment
-- 

DROP TABLE IF EXISTS `cms_comment`;
CREATE TABLE IF NOT EXISTS `cms_comment` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一ID',
  `ColumnId` varchar(50) NOT NULL COMMENT '归属栏目ID',
  `UserId` varchar(255) DEFAULT '0' COMMENT '评论人ID',
  `UserName` varchar(255) DEFAULT NULL COMMENT '评论人昵称',
  `Summary` text COMMENT '评论内容',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '评论时间',
  `Option` int(11) NOT NULL DEFAULT '0' COMMENT '评论类型，如=1文章   2=下载  3=商品',
  `Star` int(255) NOT NULL DEFAULT '0' COMMENT '如果评论有星，显示星数',
  `RepUserId` varchar(255) DEFAULT NULL COMMENT '回复人ID',
  `RepUserName` varchar(255) DEFAULT NULL COMMENT '回复人昵称',
  `RepDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '回复时间',
  `Audit` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否审核通过',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_comment
-- 

/*!40000 ALTER TABLE `cms_comment` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_comment` ENABLE KEYS */;

-- 
-- Definition of cms_download
-- 

DROP TABLE IF EXISTS `cms_download`;
CREATE TABLE IF NOT EXISTS `cms_download` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ColumnId` int(11) NOT NULL COMMENT '下载所属类型',
  `Title` varchar(255) NOT NULL COMMENT '下载标题',
  `SubTitle` varchar(255) DEFAULT NULL COMMENT '下载副标题',
  `FileUrl` varchar(500) DEFAULT NULL COMMENT '文件地址',
  `FileType` varchar(255) DEFAULT NULL COMMENT '文件类型',
  `FileSize` varchar(255) DEFAULT NULL COMMENT '文件大小',
  `DownSum` int(11) NOT NULL DEFAULT '0' COMMENT '下载数量',
  `Hits` int(11) NOT NULL DEFAULT '0' COMMENT '访问数',
  `IsSystem` varchar(255) DEFAULT NULL COMMENT '适用系统',
  `IsCharge` varchar(255) DEFAULT NULL COMMENT '软件类型',
  `ImgUrl` varchar(255) DEFAULT NULL COMMENT '图片地址',
  `ThumImg` varchar(255) DEFAULT NULL COMMENT '缩略图地址',
  `IsTop` bit(1) NOT NULL COMMENT '是否置顶',
  `IsComment` bit(1) NOT NULL COMMENT '是否评论',
  `Audit` bit(1) NOT NULL COMMENT '状态',
  `IsLink` bit(1) NOT NULL COMMENT '是否有外链',
  `LinkUrl` varchar(255) DEFAULT NULL COMMENT '外链地址',
  `Tag` varchar(255) DEFAULT NULL COMMENT '标签',
  `Summary` varchar(1000) DEFAULT NULL COMMENT '简介',
  `Content` text COMMENT '详细描述',
  `EditDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_download
-- 

/*!40000 ALTER TABLE `cms_download` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_download` ENABLE KEYS */;

-- 
-- Definition of cms_image
-- 

DROP TABLE IF EXISTS `cms_image`;
CREATE TABLE IF NOT EXISTS `cms_image` (
  `Guid` varchar(50) NOT NULL,
  `TheGuid` varchar(50) DEFAULT NULL COMMENT '所属栏目Guid',
  `Types` int(11) NOT NULL DEFAULT '0' COMMENT '图片类型，一个栏目可有多个图片类型',
  `Title` varchar(50) DEFAULT NULL COMMENT '图片名称',
  `ImgBig` varchar(255) NOT NULL COMMENT '图片原图',
  `ImgSmall` varchar(255) DEFAULT NULL COMMENT '图片缩略图',
  `IsCover` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否为封面',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序字段',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_image
-- 

/*!40000 ALTER TABLE `cms_image` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_image` ENABLE KEYS */;

-- 
-- Definition of cms_site
-- 

DROP TABLE IF EXISTS `cms_site`;
CREATE TABLE IF NOT EXISTS `cms_site` (
  `Guid` varchar(50) NOT NULL,
  `SysId` int(11) NOT NULL DEFAULT '0' COMMENT '系统ID',
  `SiteName` varchar(50) NOT NULL COMMENT '网站名称',
  `SiteUrl` varchar(100) DEFAULT NULL COMMENT '网站域名',
  `SiteLogo` varchar(255) DEFAULT NULL COMMENT '网站Logo',
  `Summary` varchar(500) DEFAULT NULL COMMENT '网站描述',
  `SiteTel` varchar(50) DEFAULT NULL COMMENT '公司电话',
  `SiteFax` varchar(50) DEFAULT NULL COMMENT '公司传真',
  `SiteEmail` varchar(100) DEFAULT NULL COMMENT '公司人事邮箱',
  `QQ` varchar(500) DEFAULT NULL COMMENT '公司客服QQ',
  `WeiXin` varchar(255) DEFAULT NULL COMMENT '微信公众号图片',
  `WeiBo` varchar(255) DEFAULT NULL COMMENT '微博链接地址或者二维码',
  `SiteAddress` varchar(200) DEFAULT NULL COMMENT '公司地址',
  `SiteCode` varchar(2000) DEFAULT NULL COMMENT '网站备案号其它等信息',
  `SeoTitle` varchar(255) DEFAULT NULL COMMENT '网站SEO标题',
  `SeoKey` varchar(500) DEFAULT NULL COMMENT '网站SEO关键字',
  `SeoDescribe` varchar(2000) DEFAULT NULL COMMENT '网站SEO描述',
  `SiteCopyright` varchar(2000) DEFAULT NULL COMMENT '网站版权等信息',
  `Status` bit(1) DEFAULT b'1' COMMENT '网站开启关闭状态',
  `CloseInfo` varchar(2000) DEFAULT NULL COMMENT '如果状态关闭，请输入关闭网站原因',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  PRIMARY KEY (`Guid`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_site
-- 

/*!40000 ALTER TABLE `cms_site` DISABLE KEYS */;
INSERT INTO `cms_site`(`Guid`,`SysId`,`SiteName`,`SiteUrl`,`SiteLogo`,`Summary`,`SiteTel`,`SiteFax`,`SiteEmail`,`QQ`,`WeiXin`,`WeiBo`,`SiteAddress`,`SiteCode`,`SeoTitle`,`SeoKey`,`SeoDescribe`,`SiteCopyright`,`Status`,`CloseInfo`,`IsDel`) VALUES
('78756a6c-50c8-47a5-b898-5d6d24a20327',0,'飞易腾官网','http://www.feiyit.com','ss',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'飞易腾-高端网站定制商城开发APP','APP开发,APP设计,网站制作,网页设计,网站建设,网站开发,微网站,手机网站,html5,响应式网站','飞易腾是北京地区专业做网站的公司，拥有多年实战经验，为企业提供网站策划、“APP开发”、“网站制作”、“网站开发”、网站营销、网站托管等“网站建设”一条龙服务；精通企业网站、行业门户网站、APP移动网站、电子商务网站的设计开发。我们提供的不只是技术，更是品质与服务；欢迎咨询洽谈010-57178368','版权所有 北京飞易腾科技有限公司 © Copyright 2011 - 2016 京ICP备13006710号-1',0,NULL,0);
/*!40000 ALTER TABLE `cms_site` ENABLE KEYS */;

-- 
-- Definition of cms_tags
-- 

DROP TABLE IF EXISTS `cms_tags`;
CREATE TABLE IF NOT EXISTS `cms_tags` (
  `Guid` varchar(50) NOT NULL,
  `FirstLetter` varchar(5) DEFAULT NULL COMMENT '首字母',
  `Name` varchar(20) DEFAULT NULL COMMENT '标签名称',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否启用',
  `Links` varchar(255) DEFAULT NULL COMMENT '标签链接地址',
  `TagsHits` int(11) NOT NULL DEFAULT '0' COMMENT '标签点击量',
  PRIMARY KEY (`Guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_tags
-- 

/*!40000 ALTER TABLE `cms_tags` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_tags` ENABLE KEYS */;

-- 
-- Definition of cms_template
-- 

DROP TABLE IF EXISTS `cms_template`;
CREATE TABLE IF NOT EXISTS `cms_template` (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '自动增长',
  `Title` varchar(50) NOT NULL COMMENT '模板名称',
  `Url` varchar(255) NOT NULL COMMENT '模板地址',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否启用',
  `Sort` int(11) DEFAULT '0' COMMENT '排序',
  `AddDate` datetime DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_template
-- 

/*!40000 ALTER TABLE `cms_template` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_template` ENABLE KEYS */;

-- 
-- Definition of cms_vote
-- 

DROP TABLE IF EXISTS `cms_vote`;
CREATE TABLE IF NOT EXISTS `cms_vote` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键自增',
  `ColumnId` int(11) NOT NULL COMMENT '所属栏目ID',
  `Title` varchar(200) DEFAULT NULL COMMENT '投票标题',
  `SubTitle` varchar(200) DEFAULT NULL COMMENT '投票副标题',
  `ItemSum` int(11) NOT NULL COMMENT '选项总数',
  `VoteSum` int(11) NOT NULL COMMENT '投票总数',
  `Options` bit(1) NOT NULL,
  `VoteType` int(11) NOT NULL COMMENT '投票类型',
  `ImgUrl` varchar(200) DEFAULT NULL COMMENT '宣传图',
  `IsTime` bit(1) NOT NULL COMMENT '时间限制',
  `BeginDate` datetime NOT NULL COMMENT '开始时间',
  `EndDate` datetime NOT NULL,
  `Summary` varchar(2000) DEFAULT NULL COMMENT '投票简介',
  `AddDate` datetime NOT NULL COMMENT '发布时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_vote
-- 

/*!40000 ALTER TABLE `cms_vote` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_vote` ENABLE KEYS */;

-- 
-- Definition of cms_voteitem
-- 

DROP TABLE IF EXISTS `cms_voteitem`;
CREATE TABLE IF NOT EXISTS `cms_voteitem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键自增',
  `VoteId` int(11) NOT NULL COMMENT '投票项ID',
  `Title` varchar(200) DEFAULT NULL COMMENT '投票项',
  `VoteSum` int(11) NOT NULL COMMENT '投票数',
  `Scale` varchar(200) DEFAULT NULL,
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序字段',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_voteitem
-- 

/*!40000 ALTER TABLE `cms_voteitem` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_voteitem` ENABLE KEYS */;

-- 
-- Definition of cms_votelog
-- 

DROP TABLE IF EXISTS `cms_votelog`;
CREATE TABLE IF NOT EXISTS `cms_votelog` (
  `Guid` varchar(255) NOT NULL COMMENT '唯一ID',
  `VoteId` int(11) DEFAULT NULL COMMENT '投票ID',
  `ItemId` varchar(255) DEFAULT NULL COMMENT '投票项ID',
  `UserId` varchar(255) NOT NULL COMMENT '投票人ID',
  `UserName` varchar(255) DEFAULT NULL COMMENT '投票人昵称',
  `Ip` varchar(255) DEFAULT NULL COMMENT 'IP地址',
  `AddDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '投票时间',
  `Summary` varchar(500) DEFAULT NULL COMMENT '投票详情',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table cms_votelog
-- 

/*!40000 ALTER TABLE `cms_votelog` DISABLE KEYS */;

/*!40000 ALTER TABLE `cms_votelog` ENABLE KEYS */;

-- 
-- Definition of sys_admin
-- 

DROP TABLE IF EXISTS `sys_admin`;
CREATE TABLE IF NOT EXISTS `sys_admin` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标识',
  `RoleGuid` varchar(50) DEFAULT NULL COMMENT '角色标识',
  `DepartmentName` varchar(50) NOT NULL COMMENT '部门名称',
  `DepartmentGuid` varchar(50) NOT NULL COMMENT '部门标识',
  `DepartmentGuidList` varchar(500) DEFAULT NULL,
  `LoginName` varchar(30) NOT NULL COMMENT '登录账号',
  `LoginPwd` varchar(50) NOT NULL COMMENT '登录密码',
  `TrueName` varchar(20) DEFAULT NULL COMMENT '真是姓名',
  `Number` varchar(10) NOT NULL COMMENT '编号',
  `HeadPic` varchar(100) NOT NULL COMMENT '头像',
  `Sex` varchar(10) NOT NULL DEFAULT '' COMMENT '性别',
  `Mobile` varchar(15) DEFAULT NULL COMMENT '手机号码',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `Email` varchar(50) DEFAULT NULL COMMENT '邮箱',
  `Summary` varchar(500) DEFAULT NULL COMMENT '描述',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `LoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '登录时间',
  `UpLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '上次登录时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_admin
-- 

/*!40000 ALTER TABLE `sys_admin` DISABLE KEYS */;
INSERT INTO `sys_admin`(`Guid`,`RoleGuid`,`DepartmentName`,`DepartmentGuid`,`DepartmentGuidList`,`LoginName`,`LoginPwd`,`TrueName`,`Number`,`HeadPic`,`Sex`,`Mobile`,`Status`,`Email`,`Summary`,`AddDate`,`LoginDate`,`UpLoginDate`) VALUES
('12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,'商务中心','52523a76-52b3-4c25-a1bd-9123a011f2a8',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,52523a76-52b3-4c25-a1bd-9123a011f2a8,','admins','pPo9vFeTWOCF0oLKKdX9Jw==','子恒国际','1101','/themes/img/avatar.jpg','男','13888888888',1,NULL,NULL,'2018-10-09 22:54:47','2018-10-31 22:34:58','2018-10-31 22:34:58'),
('30d3da88-bb72-4ace-a303-b3aae0ecb732',NULL,'事业发展部','4b6ab27f-c0fa-483d-9b5a-55891ee8d727',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,4b6ab27f-c0fa-483d-9b5a-55891ee8d727,','testadmin','Ycdvj7dGDz45F6Qlw7OMQ904o/xRuq0k','李四','1002','/themes/img/avatar.jpg','男',NULL,0,NULL,NULL,'2018-07-22 00:42:14','2018-07-22 00:42:14','2018-07-22 00:42:14');
/*!40000 ALTER TABLE `sys_admin` ENABLE KEYS */;

-- 
-- Definition of sys_appsetting
-- 

DROP TABLE IF EXISTS `sys_appsetting`;
CREATE TABLE IF NOT EXISTS `sys_appsetting` (
  `Guid` varchar(50) NOT NULL,
  `AndroidVersion` varchar(50) NOT NULL DEFAULT '0.0' COMMENT '安卓版本号',
  `AndroidFile` varchar(255) DEFAULT NULL COMMENT '更新文件',
  `IosVersion` varchar(50) NOT NULL COMMENT 'Ios版本号',
  `IosFile` varchar(255) DEFAULT NULL COMMENT 'Ios更新文件地址',
  `IosAudit` tinyint(4) NOT NULL DEFAULT '0' COMMENT 'Ios审核开关  0=关/1=开',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除 0=不删除/1=删除',
  `UpdateDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_appsetting
-- 

/*!40000 ALTER TABLE `sys_appsetting` DISABLE KEYS */;

/*!40000 ALTER TABLE `sys_appsetting` ENABLE KEYS */;

-- 
-- Definition of sys_btnfun
-- 

DROP TABLE IF EXISTS `sys_btnfun`;
CREATE TABLE IF NOT EXISTS `sys_btnfun` (
  `Guid` varchar(50) NOT NULL,
  `MenuGuid` varchar(50) NOT NULL,
  `Name` varchar(20) NOT NULL COMMENT '功能名称',
  `FunType` varchar(20) NOT NULL COMMENT '功能标识名称',
  `Summary` varchar(500) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_btnfun
-- 

/*!40000 ALTER TABLE `sys_btnfun` DISABLE KEYS */;
INSERT INTO `sys_btnfun`(`Guid`,`MenuGuid`,`Name`,`FunType`,`Summary`) VALUES
('6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','新增','Add',NULL),
('6d2c2da5-8bb8-4905-aaa9-cd297a46e4ed','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','导出','Export',NULL),
('8112ffb0-a84e-496c-93d7-95c02678754a','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','审核','Audit',NULL),
('931bd729-f160-4fe3-bb7c-7828a2da047a','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','修改','Edit',NULL),
('b1ab3437-6481-4a4e-b536-d7870a822de4','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','导入','Import',NULL),
('b943200f-7c99-44b5-93d9-e4ea2505928a','5ce13ead-971b-4ed4-ad5f-acacccd82381','新增','Add',NULL);
/*!40000 ALTER TABLE `sys_btnfun` ENABLE KEYS */;

-- 
-- Definition of sys_code
-- 

DROP TABLE IF EXISTS `sys_code`;
CREATE TABLE IF NOT EXISTS `sys_code` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标号Guid',
  `ParentGuid` varchar(50) NOT NULL COMMENT '字典类型标识',
  `CodeType` varchar(50) DEFAULT NULL COMMENT '字典值——类型',
  `Name` varchar(255) NOT NULL COMMENT '字典值——名称',
  `EnName` varchar(255) DEFAULT NULL COMMENT '字典值——英文名称',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '字典值——排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '字典值——状态',
  `Summary` varchar(1000) DEFAULT NULL COMMENT '字典值——描述',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '字典值——添加时间',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '字典值——修改时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_code
-- 

/*!40000 ALTER TABLE `sys_code` DISABLE KEYS */;
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('0042c8e2-60dc-44b9-a637-d98d6e4c6d1a','7b664e3e-f58a-4e66-8c0f-be1458541d14','BLZ','BLZ百禄姿',NULL,317,1,'BLZ百禄姿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0086fdc0-1718-4dae-96de-4eb56f94be83','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZB','YZB伊姿百瑞',NULL,261,1,'YZB伊姿百瑞','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('00bc1e1a-1ed3-4b89-9b20-707715652148','48458681-48b0-490a-a840-0ffcbe49f5d4','P','皮草',NULL,155,1,'皮草','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('00e579a7-7766-401e-a9a3-24636e8e5895','7b664e3e-f58a-4e66-8c0f-be1458541d14','SDP','SDP皮衣三',NULL,169,1,'皮衣三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0409f43c-f999-4ef2-a5be-9405ba5ba7e9','7b664e3e-f58a-4e66-8c0f-be1458541d14','M46','M46曼紫46羽绒服三',NULL,182,1,'曼紫46羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('06256d8f-5206-4282-b206-b82d4ace5565','7b664e3e-f58a-4e66-8c0f-be1458541d14','HNS','HNS海宁双面尼',NULL,288,1,'HNS海宁双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('064aac2c-1bee-4cad-9f86-1486159d20be','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDZ','吊带杂DDZ',NULL,225,1,'吊带杂DDZ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('089b20ed-c712-4b10-839c-06b574c7f35d','7b664e3e-f58a-4e66-8c0f-be1458541d14','XHX','XHX雪鸿羽绒服',NULL,149,1,'雪鸿羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0915806a-5315-4bcb-bcda-402b272d9f27','7b664e3e-f58a-4e66-8c0f-be1458541d14','JJX','JJX晶晶薇琪',NULL,198,1,'JJX晶晶薇琪','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('09c453df-df66-4b8d-9324-d74db78a385b','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFL','YFL音非',NULL,278,1,'YFL音非','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0b9abd29-a571-40eb-95ba-afb591f3412c','7b664e3e-f58a-4e66-8c0f-be1458541d14','JLY','佳澜依尔JLY',NULL,210,1,'佳澜依尔JLY','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('0ccd9c0e-4df4-401e-b40a-531ca53ae849','7b664e3e-f58a-4e66-8c0f-be1458541d14','HSX','HSX花色',NULL,308,1,'HSX花色','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0ceee612-c739-49ff-a635-b4f67f3e1ffd','7b664e3e-f58a-4e66-8c0f-be1458541d14','CSL','CSL尘色',NULL,44,1,'尘色','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0e425b2d-9549-45d5-abbf-f95cbd52cb72','48458681-48b0-490a-a840-0ffcbe49f5d4','G','半裙',NULL,143,1,'半裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0e56f826-a388-4472-beb6-f8539b5e2883','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZGP','ZGP专供皮衣',NULL,189,1,'ZGP专供皮衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0e6a54df-6fb9-4e5a-b0a9-61de5eab1f7a','7b664e3e-f58a-4e66-8c0f-be1458541d14','JWN','JWN杰文妮',NULL,123,1,'杰文妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0eb83125-cfb4-4062-9e23-1b1a1a90aee7','7b664e3e-f58a-4e66-8c0f-be1458541d14','SKL','SKL萨酷睿L',NULL,117,1,'SKL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1050bee3-8a02-441b-977a-537b4e060e6a','7b664e3e-f58a-4e66-8c0f-be1458541d14','DMX','麦之林DMX',NULL,239,1,'麦之林DMX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1083421d-2cc4-47c7-aad3-534e877a71cb','7b664e3e-f58a-4e66-8c0f-be1458541d14','DRD','DRD貂绒打底毛衫',NULL,258,1,'DRD貂绒打底毛衫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('10bdbd39-c147-4131-9903-b540b6e96121','7b664e3e-f58a-4e66-8c0f-be1458541d14','XMX','XMX绚萌',NULL,124,1,'绚萌','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('11c2d713-43b6-45b1-8338-50342ac23b62','7b664e3e-f58a-4e66-8c0f-be1458541d14','YNX','依诺YNX',NULL,224,1,'依诺YNX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('12738a20-2ae0-4a30-bee5-9922efaf964a','7b664e3e-f58a-4e66-8c0f-be1458541d14','SNY','SNY圣娜依儿',NULL,197,1,'SNY圣娜依儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('12f73730-5e4d-4ae8-bb10-e62f08749dd7','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZYX','ZYX甄妍',NULL,302,1,'ZYX甄妍','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('14dc428c-6d46-4c3d-ac59-b16c68b8e358','7b664e3e-f58a-4e66-8c0f-be1458541d14','BFN','BFN贝芙妮',NULL,75,1,'贝芙妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('15b25377-72ad-4087-83ad-ca28a8cedfdf','7b664e3e-f58a-4e66-8c0f-be1458541d14','HEL','HEL禾尔美',NULL,69,1,'禾尔美','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('15bc0cdb-a28b-4784-b7f7-8dbd76d6416a','7b664e3e-f58a-4e66-8c0f-be1458541d14','DSY','DSY迪丝雅',NULL,265,1,'DSY迪丝雅','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1913afc4-0c1e-4e0e-9f4f-19788e8aac35','7b664e3e-f58a-4e66-8c0f-be1458541d14','MEN','MEN沐恩真丝',NULL,217,1,'MEN沐恩真丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1970b666-7b15-481d-8702-1368e2380c3f','8cb134d5-979b-40e2-b453-aeee265f4ab2','H','四季装',NULL,12,1,'四季装-H','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1b87910f-d007-4d33-afbe-98ffda089589','7b664e3e-f58a-4e66-8c0f-be1458541d14','AYX','AYX艾燕春装',NULL,259,1,'AYX艾燕春装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1cffd0c2-f35c-42d8-bd77-265f5e282fe8','7b664e3e-f58a-4e66-8c0f-be1458541d14','PYX','PYX皮衣',NULL,236,1,'PYX皮衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1d164b70-f91b-41bb-b348-62cfd6ae75fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','HXX','HXX鸿秀双面妮',NULL,147,1,'鸿秀双面妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1da78dc4-908e-4cd8-aa0c-0cd5172365fc','7b664e3e-f58a-4e66-8c0f-be1458541d14','NKS','NKS男款双面尼',NULL,267,1,'NKS男款双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1dd9460a-ca52-4016-80f9-c56c0434654f','7b664e3e-f58a-4e66-8c0f-be1458541d14','IKL','IK',NULL,67,1,'IK','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1efed59b-232f-4c80-867a-8ddc925848e3','7b664e3e-f58a-4e66-8c0f-be1458541d14','AWL','AWL艾薇儿awl',NULL,72,1,'艾薇儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2011a2a0-e5aa-4dd0-b0e7-8fb1a429cdcd','7b664e3e-f58a-4e66-8c0f-be1458541d14','HYY','HYY韩以羽绒服',NULL,315,1,'HYY韩以羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('2169b09a-8b58-4376-b1c4-3f56839d2304','7b664e3e-f58a-4e66-8c0f-be1458541d14','MSL','MSL曼丝L秀登',NULL,119,1,'MSL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('22c6f678-cd30-4248-92a2-e9baa6b6f117','48458681-48b0-490a-a840-0ffcbe49f5d4','O','羊绒大衣',NULL,141,1,'羊绒大衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('22f7bd06-33f6-45d6-8c21-6290e098ceb2','7b664e3e-f58a-4e66-8c0f-be1458541d14','XFZ','西服杂XFZ',NULL,228,1,'西服杂XFZ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2418bfca-9640-4382-a665-f6fd5ffcc017','7b664e3e-f58a-4e66-8c0f-be1458541d14','YBL','YBL伊百丽',NULL,142,1,'伊百丽','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2474293b-a3b0-41a3-baa4-1b2a75257b97','7b664e3e-f58a-4e66-8c0f-be1458541d14','137','137高圆圆同款三',NULL,190,1,'137高圆圆同款三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('24f9efe2-b80e-4ab1-833c-f02a61948b95','7b664e3e-f58a-4e66-8c0f-be1458541d14','YMR','YMR依目了然',NULL,132,1,'依目了然','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('25b47163-315e-4c63-abed-8412f537a516','7b664e3e-f58a-4e66-8c0f-be1458541d14','JXL','JXL杰西伍',NULL,70,1,'杰西伍','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2628b747-9986-4a5c-9a30-abf89b6a8742','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZNW','ZNW珍妮文',NULL,216,1,'ZNW珍妮文','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('273309c3-34d0-4cf4-8abe-7b83ddd22fe6','7b664e3e-f58a-4e66-8c0f-be1458541d14','FZL','FZL梵姿',NULL,55,1,'梵姿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('27fe6c17-2e1d-4f3e-8416-074e9033a53d','7b664e3e-f58a-4e66-8c0f-be1458541d14','XDZ','XDZ雪丹枝',NULL,275,1,'XDZ雪丹枝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('28245b5c-9205-4084-99aa-c34a5e695c9c','8cb134d5-979b-40e2-b453-aeee265f4ab2','A','春装',NULL,5,1,'春装-A','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('28d7c022-5749-40e2-8b3d-fed90cf2e02e','7b664e3e-f58a-4e66-8c0f-be1458541d14','M53','M53曼紫53 三',NULL,203,1,'M53曼紫53 三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('28ffb566-5247-41cf-bcd3-725d305b19be','7b664e3e-f58a-4e66-8c0f-be1458541d14','OSL','OSL欧时力',NULL,73,1,'欧时力','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2963ff16-26fe-4754-98f6-332d33b91dc5','7b664e3e-f58a-4e66-8c0f-be1458541d14','XYY','XYY夏映颗粒绒',NULL,316,1,'XYY夏映颗粒绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('29c41b94-4b92-4676-a1d2-2cf89abb8c90','48458681-48b0-490a-a840-0ffcbe49f5d4','T','夹克',NULL,298,1,'机车夹克','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2a3aa0f8-c5b3-4570-a8a2-e04e0f05d6ef','7b664e3e-f58a-4e66-8c0f-be1458541d14','MQX','MQX玛琪雅朵',NULL,87,1,'玛琪雅朵','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2a64211c-e0c0-40a5-a291-7e44dec9f681','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZM','YZM羽绒服三',NULL,178,1,'YZM羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2afd1417-9b6f-4842-afca-ae72805609a1','7b664e3e-f58a-4e66-8c0f-be1458541d14','BJS','BJS北京双面尼',NULL,243,1,'BJS北京双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2b2cf889-6245-4a21-9454-35c9e882ace5','7b664e3e-f58a-4e66-8c0f-be1458541d14','WYL','WYL唯依',NULL,93,1,'唯依','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2bdac142-528e-433f-9a7e-3aa02ccea7fe','7b664e3e-f58a-4e66-8c0f-be1458541d14','RFY','RFY人佛缘',NULL,164,1,'人佛缘','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2e6da0d5-3f35-4093-a442-d9d1b50c3a07','7b664e3e-f58a-4e66-8c0f-be1458541d14','AWR','AWR艾薇儿awr',NULL,125,1,'艾薇儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2ea653ab-1a18-4c84-a872-cf873ab11f5d','7b664e3e-f58a-4e66-8c0f-be1458541d14','BSK','BSK百思寇',NULL,251,1,'BSK百思寇','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2eaf5d0f-3010-46ef-96c3-0d79ea17f9b2','7b664e3e-f58a-4e66-8c0f-be1458541d14','MMW','MMW棉麻围巾',NULL,146,1,'棉麻围巾','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2fa78c1b-40d0-4eb7-a1a8-b44de4e64589','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZK','YZK依庄可人',NULL,246,1,'YZK依庄可人','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('3035f671-abca-437b-96ff-990225cb6f28','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','2','2',NULL,135,1,'2','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('32ceaa41-dea6-4cff-bb95-11f2789870db','7b664e3e-f58a-4e66-8c0f-be1458541d14','HNX','HNX亨奴',NULL,226,1,'HNX亨奴','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('333ba8b5-d2ba-4901-97fd-6edd6e761736','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','0','0',NULL,172,1,'0','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('33ec687b-e773-4a8c-a644-43821e63e7ff','7b664e3e-f58a-4e66-8c0f-be1458541d14','YYY','YYY依艺缘',NULL,276,1,'YYY依艺缘','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3681512b-3fc7-458e-a9db-b15990b922fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','YSD','YSD依莎蒂妮',NULL,230,1,'YSD依莎蒂妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('38e51871-1eac-4201-89d4-06533623fb2e','48458681-48b0-490a-a840-0ffcbe49f5d4','H','50以上休闲',NULL,151,1,'50以上休闲','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('39d525ac-0df2-4c36-919c-301d2af15127','7b664e3e-f58a-4e66-8c0f-be1458541d14','HBN','HBN韩版连衣裙',NULL,289,1,'HBN韩版连衣裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3a0c2c57-5b29-42ea-900e-ba751772128e','7b664e3e-f58a-4e66-8c0f-be1458541d14','NYN','NYN诺喑呢',NULL,188,1,'NYN诺喑呢','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3c92c5b5-a74c-4483-b053-b7061d2ed8a5','7b664e3e-f58a-4e66-8c0f-be1458541d14','YDA','YDA雅蒂安娜',NULL,173,1,'YDA雅蒂安娜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3c95046e-b1b3-4a39-8006-9987056e84be','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFX','YFX佑芙妮',NULL,231,1,'YFX佑芙妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3d6bb971-13e5-4c50-a0b2-4d7f1c5643b4','7b664e3e-f58a-4e66-8c0f-be1458541d14','PXL','飘宣PXL',NULL,159,1,'飘宣PXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3ddeae38-b336-4450-b5d9-5e9e90115e98','7b664e3e-f58a-4e66-8c0f-be1458541d14','QTM','QTM晴天明月',NULL,43,1,'晴天明月','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('400920ee-9e41-4a9a-8561-9abeff8f26a5','7b664e3e-f58a-4e66-8c0f-be1458541d14','JXW','JXW 杰西伍',NULL,200,1,'JXW 杰西伍','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('40f67656-6b9c-49ab-b8c3-377ebcde16c7','e86cf108-dc4d-4532-8cce-fdb041363902','I','XXXXL',NULL,40,1,'XXXXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('41bc4e6d-25a8-462a-b8b4-b6079ae2a57d','7b664e3e-f58a-4e66-8c0f-be1458541d14','NRS','NRS女人时报',NULL,85,1,'女人时报','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('429f8ceb-69a4-42ef-a3e4-c907389e97d3','7b664e3e-f58a-4e66-8c0f-be1458541d14','YRT','YRT羽绒服三',NULL,171,1,'YRT羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('433885fa-b07d-40f1-b175-f3475ab744fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','DZK','DZK 2017定制款',NULL,235,1,'DZK 2017定制款','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('43854363-22b0-4176-a0bc-66dd612f660d','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSN','SSN似水年华',NULL,53,1,'似水年华','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('443df199-e5f2-4509-894f-cf1ec1d57a9d','7b664e3e-f58a-4e66-8c0f-be1458541d14','LSQ','LSQ陆氏青云',NULL,56,1,'陆氏青云','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('450562e4-3293-4d71-8f82-ecb5889a74a4','1942d4fd-3203-42b1-a955-4a84a532b2a2','19','2019',NULL,319,1,'2019','2018-10-19 13:07:22','2018-10-19 13:07:22'),
('45744b0c-a792-4ea8-8490-1cf5ae7556fa','7b664e3e-f58a-4e66-8c0f-be1458541d14','FMY','FMY风媚衣坊 晨贝 靡曼',NULL,290,1,'FMY风媚衣坊 晨贝 靡曼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('45f44976-7856-4e34-b12c-8e7d568b1aaa','7b664e3e-f58a-4e66-8c0f-be1458541d14','WHZ','武汉杂2017WHZ',NULL,238,1,'武汉杂2017WHZ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('463ad549-3d7b-4a87-8b13-5878f84eb035','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZY','MZY曼紫羽绒服三',NULL,166,1,'曼紫羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('46ac0b25-660b-48cc-a3ec-ff8dd47864cd','7b664e3e-f58a-4e66-8c0f-be1458541d14','WBB','WBB 武汉彬彬 ',NULL,205,1,'WBB 武汉彬彬 ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('46c3f4a1-aea3-4df6-a8c4-1e65ae626335','e86cf108-dc4d-4532-8cce-fdb041363902','G','XXL',NULL,38,1,'XXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('477b792f-fee7-46af-9547-55f7684ce5cd','7b664e3e-f58a-4e66-8c0f-be1458541d14','SMR','双面尼三楼SMR',NULL,174,1,'双面尼三楼SMR','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('489ed30a-12c7-4c71-8871-1031681f9a5c','7b664e3e-f58a-4e66-8c0f-be1458541d14','M52','M52曼紫52羽绒服三',NULL,184,1,'曼紫52羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('495ccc11-f0a3-452c-b401-ecc91794350d','7b664e3e-f58a-4e66-8c0f-be1458541d14','SYS','SYS似水映',NULL,214,1,'SYS似水映','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4987917b-062e-4e21-8f4d-36df5cf7c76e','48458681-48b0-490a-a840-0ffcbe49f5d4','C','上衣',NULL,15,1,'上衣-C','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4a32b9c5-fae7-462c-aec4-93d5fdf6812d','7b664e3e-f58a-4e66-8c0f-be1458541d14','MQY','MQY玛琪雅朵',NULL,129,1,'玛琪雅朵','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4b48af91-e964-4d82-b18b-50589db0dbda','7b664e3e-f58a-4e66-8c0f-be1458541d14','YDM','YDM雅蒂安娜毛呢',NULL,209,1,'YDM雅蒂安娜毛呢','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4b9f096f-c053-41fb-9477-ce52375b7ef5','7b664e3e-f58a-4e66-8c0f-be1458541d14','MLM','MLM梅丽摩尔',NULL,291,1,'MLM梅丽摩尔','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4be990a5-d6a3-4e75-a43b-65731c44e3bf','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','1','1',NULL,134,1,'1','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4c249ebc-20ef-40ee-b00c-9c63aec3f74c','e86cf108-dc4d-4532-8cce-fdb041363902','F','XL',NULL,37,1,'XL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4c882546-9e1f-425c-a807-78e15ce4e526','48458681-48b0-490a-a840-0ffcbe49f5d4','S','围巾',NULL,145,1,'围巾','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('4dbb2a64-36a4-4f43-89f9-ed7da48aa193','7b664e3e-f58a-4e66-8c0f-be1458541d14','HJY','HJY惠景媛',NULL,204,1,'HJY惠景媛','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4e20068b-63be-4d2c-9e38-6cdbe02391b2','e86cf108-dc4d-4532-8cce-fdb041363902','B','XS',NULL,33,1,'XS','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4e6f026b-3eb0-47ad-9184-854c5199e5d8','48458681-48b0-490a-a840-0ffcbe49f5d4','U','马甲',NULL,299,1,'马甲','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4f075839-9804-4998-8659-a919391e561f','7b664e3e-f58a-4e66-8c0f-be1458541d14','YSY','YSY羽绒服三',NULL,170,1,'YSY羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4f4d3428-fd3c-48c2-85fa-57db17a7fe3f','8cb134d5-979b-40e2-b453-aeee265f4ab2','D','冬装',NULL,8,1,'冬装-D','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('500fb1ae-1861-4a5a-9f77-ee6b69bd5375','48458681-48b0-490a-a840-0ffcbe49f5d4','V','内衣',NULL,312,1,'内衣-V','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('50523d77-235a-4c30-b033-be84259b2ff2','7b664e3e-f58a-4e66-8c0f-be1458541d14','YML','YML依美瑞',NULL,63,1,'YML依美瑞','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5057e97d-cdc2-413b-8e50-a3fddfb5b822','7b664e3e-f58a-4e66-8c0f-be1458541d14','BFL','BFL贝芙妮',NULL,212,1,'BFL贝芙妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('51957ec1-e8b2-4f9e-8f27-39a4cf261266','7b664e3e-f58a-4e66-8c0f-be1458541d14','HTF','HTF红头发风衣',NULL,222,1,'HTF红头发风衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('55ba863c-664c-40df-8199-7d06992274ac','7b664e3e-f58a-4e66-8c0f-be1458541d14','BZM','BZM宝姿毛衣',NULL,140,1,'宝姿毛衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('570b6e38-0333-4bd5-b857-89f6ba8da34a','7b664e3e-f58a-4e66-8c0f-be1458541d14','MNL','MNL沐恩冬装',NULL,122,1,'MNL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5aee6bfc-dae4-4616-b129-cb63528570d2','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','A','A',NULL,28,1,'A','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('5b39701e-285c-4bd9-b080-3eeaf70190d5','7b664e3e-f58a-4e66-8c0f-be1458541d14','SKR','SKR萨酷睿',NULL,57,1,'萨酷睿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5b3ea153-d6e6-42bd-a82e-8497af8f7f0e','48458681-48b0-490a-a840-0ffcbe49f5d4','K','年轻处理',NULL,154,1,'年轻处理','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5b406c33-4552-4d03-809f-20a147523893','8cb134d5-979b-40e2-b453-aeee265f4ab2','F','秋冬装',NULL,10,1,'秋冬装-F','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5d24cddd-de94-4602-921e-c8e4b14e8172','7b664e3e-f58a-4e66-8c0f-be1458541d14','LZL','LZL朗姿丽',NULL,286,1,'LZL朗姿丽','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5e5b962f-362f-4a94-9846-81e6db713be7','7b664e3e-f58a-4e66-8c0f-be1458541d14','TZD','童装冬棉服TZD',NULL,207,1,'童装冬棉服TZD','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('61fa64da-07f7-4067-9ded-cd8d106e7ba6','7b664e3e-f58a-4e66-8c0f-be1458541d14','SRN','SRN赛睿娜',NULL,59,1,'赛睿娜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('62413bf5-d888-4594-8c50-225d3554f085','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZD','YZD艺之蝶',NULL,54,1,'艺之蝶','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6300ace3-6443-4d78-bbc3-cdea2c3f57a8','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZTG','ZTG紫藤谷',NULL,273,1,'ZTG紫藤谷','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('65590d1d-1c7c-44ae-a328-481a6b5dc2cc','1942d4fd-3203-42b1-a955-4a84a532b2a2','20','2020',NULL,320,1,'2020','2018-10-19 13:07:31','2018-10-19 13:07:31'),
('65d6afad-2c99-4bf7-a4f8-64824562ba97','7b664e3e-f58a-4e66-8c0f-be1458541d14','SYX','SYX水映',NULL,181,1,'水映','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('69471576-93bb-473d-a301-204193219f52','7b664e3e-f58a-4e66-8c0f-be1458541d14','WYX','WYX唯依',NULL,202,1,'WYX唯依','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6948a911-3034-45ca-8a17-7797e1a63641','7b664e3e-f58a-4e66-8c0f-be1458541d14','SMM','SMM双面呢三',NULL,168,1,'双面呢三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('6befc43f-e491-4a84-bc31-756c008707f3','e86cf108-dc4d-4532-8cce-fdb041363902','H','XXXL',NULL,39,1,'XXXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6c28ecc7-f193-498c-b314-0315b321de95','7b664e3e-f58a-4e66-8c0f-be1458541d14','FRL','FRL妃萱',NULL,64,1,'妃萱','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6c67c5d6-fc73-4dec-8754-b5cf242de955','48458681-48b0-490a-a840-0ffcbe49f5d4','A','T恤',NULL,13,1,'T恤-A','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6c9da850-84fb-4f3a-a0a4-a0822b7cef7b','7b664e3e-f58a-4e66-8c0f-be1458541d14','OBY','OBY欧版羽绒服',NULL,133,1,'欧版羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6cf31787-0a53-419d-be61-b32847b6df79','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZGY','ZGY专供羽绒服',NULL,187,1,'ZGY专供羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6d4c33ba-05d9-4238-a9b8-c82fc290d393','7b664e3e-f58a-4e66-8c0f-be1458541d14','AML','AML艾米拉羽绒服三',NULL,167,1,'艾米拉羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6e838a10-94c3-4089-8766-3b4bfe85c24d','7b664e3e-f58a-4e66-8c0f-be1458541d14','BYM','BYM倍艺蒙',NULL,272,1,'BYM倍艺蒙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6ed694c7-6efa-47ff-8425-de995b1953fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','TPN','TPN太平鸟',NULL,271,1,'TPN太平鸟','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('709c8230-b39f-442f-872f-1f38bae53e4e','7b664e3e-f58a-4e66-8c0f-be1458541d14','DRX','DRX貂绒',NULL,128,1,'貂绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('711c4b57-8336-4f31-b421-80c83e996dbc','e86cf108-dc4d-4532-8cce-fdb041363902','D','M',NULL,35,1,'M','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('717c1829-0ec6-46b2-bfa9-40e0aef20871','7b664e3e-f58a-4e66-8c0f-be1458541d14','M61','M61曼紫M61羽绒服三',NULL,185,1,'曼紫M61羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('71bee291-1391-4cb3-b533-f8f1e78e485e','7b664e3e-f58a-4e66-8c0f-be1458541d14','SJL','世纪蓝天SJL',NULL,211,1,'世纪蓝天SJL','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('71f3252a-638d-43a7-ad79-f9eee672f019','e86cf108-dc4d-4532-8cce-fdb041363902','J','均码',NULL,41,1,'均码','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('75c3b9b1-beb5-42e1-aa10-26a1962a76fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','AXX','AXX暗香',NULL,282,1,'AXX暗香','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('76b32e1f-ddd7-43b0-a589-0298bfcfcbe5','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZL','MZL曼紫mzl',NULL,71,1,'曼紫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('775401f9-f43f-4bdf-a253-bb2717ddf4b2','7b664e3e-f58a-4e66-8c0f-be1458541d14','FXX','FXX妃萱',NULL,46,1,'妃萱','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7794ac40-1385-43d2-be28-8db183207a67','8cb134d5-979b-40e2-b453-aeee265f4ab2','G','春秋装',NULL,11,1,'春秋装-G','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('77b25de4-731c-4f16-a5f0-a463f1cc2ad7','7b664e3e-f58a-4e66-8c0f-be1458541d14','LZW','LZW靓姿屋',NULL,232,1,'LZW靓姿屋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7835dbc9-9a6f-4ea4-8326-1889779cf28e','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDL','DDL迪迪欧',NULL,68,1,'迪迪欧','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('784dc39b-90bd-431c-895f-e9de04564181','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','4','4',NULL,137,1,'4','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('79f45c74-7492-4816-869c-66b59bb5f0ba','7b664e3e-f58a-4e66-8c0f-be1458541d14','YMM','YMM怡梦情缘棉服',NULL,257,1,'YMM怡梦情缘棉服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7b03a3f5-cd03-400d-876d-70b367f6a24f','7b664e3e-f58a-4e66-8c0f-be1458541d14','YKD','YKD羽绒服三',NULL,186,1,'YKD羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7b79b747-56b1-4d81-842d-99715e94b12a','48458681-48b0-490a-a840-0ffcbe49f5d4','E','风衣',NULL,17,1,'风衣-E','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7bcf0dbe-6f54-4231-967e-1a0513f85f23','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDK','DDK打底裤 ',NULL,191,1,'DDK打底裤 ','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('7d209f2e-e7b8-4a91-819e-a955c593ec85','7088d9b9-6692-4fc7-a83c-da580f1407c3','1006','包',NULL,78,1,NULL,'2018-06-30 19:29:11','2018-06-30 19:29:11'),
('7e3f1f8a-7b58-4de2-9326-ddd08f2196f1','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFQ','依芳秋水双面尼YFQ',NULL,206,1,'依芳秋水双面尼YFQ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7f2375e8-c387-4c8d-9934-b6833edaf4a0','7b664e3e-f58a-4e66-8c0f-be1458541d14','SJS','SJS世纪蓝天双面尼',NULL,245,1,'SJS世纪蓝天双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('804ee952-3f85-4e4a-aa8e-1ba37b5ac486','7b664e3e-f58a-4e66-8c0f-be1458541d14','TKY','TKY挑款羽绒服',NULL,255,1,'TKY挑款羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('80994916-bf65-49ff-8529-9e8796bd46dd','e86cf108-dc4d-4532-8cce-fdb041363902','A','XXL',NULL,32,1,'XXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('80ce475a-781e-4e58-a506-a77dc9648fca','7b664e3e-f58a-4e66-8c0f-be1458541d14','YHM','YHM羽绒服三',NULL,179,1,'YHM羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('81d70b21-ee20-48eb-b254-6b456517833b','7b664e3e-f58a-4e66-8c0f-be1458541d14','NRL','NRL女人屋',NULL,103,1,'女人屋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8383ab1c-18ee-4039-8e2c-33ce68ee1615','7b664e3e-f58a-4e66-8c0f-be1458541d14','PXM','PXM飘轩',NULL,74,1,'飘轩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('867a05c8-ebf8-460a-9de9-9d2e1e4aae73','48458681-48b0-490a-a840-0ffcbe49f5d4','I','50以下休闲',NULL,152,1,'50以下休闲','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('86a900ad-98c8-40d6-8a00-c7fd2cf2f568','7b664e3e-f58a-4e66-8c0f-be1458541d14','DRN','DRN貂绒打底',NULL,144,1,'貂绒打底','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('88b76b27-e764-4421-965e-822b8640e155','7b664e3e-f58a-4e66-8c0f-be1458541d14','TRY','条绒羽绒服',NULL,307,1,'条绒羽绒服TRY','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8913777b-bfc5-41db-8c38-91c8b4956fe5','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','B','B',NULL,29,1,'B','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('8994730c-0ae1-45f6-a1a8-38b609a46252','7b664e3e-f58a-4e66-8c0f-be1458541d14','BJY','BJY北京羽绒服',NULL,256,1,'BJY北京羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8bb35887-5ff6-4b85-b700-ff49e830e3d1','7b664e3e-f58a-4e66-8c0f-be1458541d14','SJZ','SJZ世纪蓝天真丝',NULL,215,1,'SJZ世纪蓝天真丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8dda6501-f1a4-4ede-99df-98731ac37648','7b664e3e-f58a-4e66-8c0f-be1458541d14','WMZ','WMZ外贸真丝',NULL,285,1,'WMZ外贸真丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8f67ee08-799f-4369-ad56-ee25066da0cd','7b664e3e-f58a-4e66-8c0f-be1458541d14','XCX','XCX炫彩',NULL,283,1,'XCX炫彩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8fccca92-1f94-4bc3-b74a-40ef7f8f109c','7b664e3e-f58a-4e66-8c0f-be1458541d14','MXX','MXX茉希',NULL,277,1,'MXX茉希','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8ff8c4e9-ad53-438d-84fb-11d0ed3207fd','7b664e3e-f58a-4e66-8c0f-be1458541d14','YDW','依丁物语YDW',NULL,218,1,'依丁物语YDW','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('93321313-2711-40e6-b7c2-d469bb54eec6','7b664e3e-f58a-4e66-8c0f-be1458541d14','KDM','KDM凯蒂梅露羊剪绒',NULL,294,1,'KDM凯蒂梅露羊剪绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('93a26353-fd83-4c17-832f-7724a35a5490','7b664e3e-f58a-4e66-8c0f-be1458541d14','STS','STS尚缇诗',NULL,244,1,'STS尚缇诗','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('93e7d972-b6c8-403e-86ad-2fec6ae63de4','7b664e3e-f58a-4e66-8c0f-be1458541d14','TKX','TKX唐卡',NULL,281,1,'TKX唐卡','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('94a7cbb7-b07e-4a77-b5c8-0674993548a3','7b664e3e-f58a-4e66-8c0f-be1458541d14','PKF','PKF派克服',NULL,240,1,'PKF派克服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('95a7e559-2714-4b06-b776-18dd3860841e','7b664e3e-f58a-4e66-8c0f-be1458541d14','5ZY','5ZY5字羽绒服三',NULL,192,1,'5ZY5字羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('966afc63-956e-4900-86a0-61c811ec1e30','7b664e3e-f58a-4e66-8c0f-be1458541d14','YBX','YBX约布',NULL,295,1,'YBX约布','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('96e356fa-5b68-49d0-9fcd-aba2f0222303','7b664e3e-f58a-4e66-8c0f-be1458541d14','PJN','PJN帕佳妮',NULL,304,1,'PJN帕佳妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('96e43f17-ef09-4e55-a61e-34292b2753f3','7b664e3e-f58a-4e66-8c0f-be1458541d14','MXM','MXM莫西莫',NULL,66,1,'莫西莫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('98d45a4c-3035-4583-8040-5c1821c7ca97','7b664e3e-f58a-4e66-8c0f-be1458541d14','BHX','BHX鋇禾春装',NULL,266,1,'BHX鋇禾春装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('98dda8cf-0193-4431-9a8e-dea7524dfca4','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZSL','ZSL真丝连衣裙',NULL,227,1,'ZSL真丝连衣裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('99bb643f-99d1-4d58-9b0e-887a774877b8','7b664e3e-f58a-4e66-8c0f-be1458541d14','XRX','XRX熙然',NULL,241,1,'XRX熙然','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9a838347-2e8d-4acb-ac6d-2615ce15fc0c','1942d4fd-3203-42b1-a955-4a84a532b2a2','18','2018',NULL,318,1,'2018','2018-10-19 13:07:14','2018-10-19 13:07:14'),
('9b8d73b7-e51c-469c-a035-b9c634f44c24','7b664e3e-f58a-4e66-8c0f-be1458541d14','NSQ','NSQ诺诗琪',NULL,220,1,'NSQ诺诗琪','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9b91b963-d46a-463b-814b-c47805146050','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','D',' D',NULL,31,1,'D','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9ba942ed-37ac-457e-9cad-5ae962762242','7b664e3e-f58a-4e66-8c0f-be1458541d14','MXY','MXY魔犀羽绒服',NULL,165,1,'魔犀羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9cdfcb80-3647-4b59-b4ec-9e88bc1dbf36','7b664e3e-f58a-4e66-8c0f-be1458541d14','SDX','SDX天格双面呢',NULL,120,1,'SDX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9d414f7d-1213-4409-af54-1efe4a14a87a','48458681-48b0-490a-a840-0ffcbe49f5d4','L','棉服',NULL,150,1,'棉服','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('9dd88a48-9522-46f6-b676-af1ff37afa36','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','6','6',NULL,24,1,'6','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9e283ca6-df02-485a-840f-4b37918ebaba','7b664e3e-f58a-4e66-8c0f-be1458541d14','NIK','NIK耐克',NULL,2,1,'耐克-NIK','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9e420c91-320e-44bc-ab99-c9cc95b8c69f','7b664e3e-f58a-4e66-8c0f-be1458541d14','TRG','TRG唐人阁',NULL,76,1,'唐人阁','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9e4d15d3-c0d2-456d-8da3-a873d89181d0','7b664e3e-f58a-4e66-8c0f-be1458541d14','WYS','WYS连衣裙',NULL,314,1,'WYS连衣裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9f2edffa-aea8-4014-bd9e-546a3094c54b','7b664e3e-f58a-4e66-8c0f-be1458541d14','FMX','FMX纷漫',NULL,42,1,'纷漫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9f784c92-22e1-434e-8792-43d1829d9009','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','5','5',NULL,23,1,'5','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a0664601-d919-4dec-a1ea-28e90edcd0c2','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFN','YFN妍妃霓',NULL,126,1,'妍妃霓','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a092d9a4-da9f-44e5-8850-6b3b9bdd5a7d','7b664e3e-f58a-4e66-8c0f-be1458541d14','YGX','YGX雅阁',NULL,121,1,'YGX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a1034ce4-d115-4496-b161-64eb5c69d45e','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','9','9',NULL,27,1,'9','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a1332c02-4c8f-4bbe-88de-52e5f9a09166','7b664e3e-f58a-4e66-8c0f-be1458541d14','YBB','YBB一布百布',NULL,250,1,'YBB一布百布','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a143fe9d-480c-4486-b20d-a572ba40e510','7b664e3e-f58a-4e66-8c0f-be1458541d14','ADI','ADI阿迪',NULL,1,1,'阿迪-ADI','2018-10-19 13:06:29','2018-10-19 13:06:29'),
('a15d5b75-7f13-4f23-b2a5-20cfe7941f44','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZSW','ZSW真丝围巾',NULL,305,1,'ZSW真丝围巾','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('a1aa0583-bae7-4e85-b71f-6b3f871fa118','8cb134d5-979b-40e2-b453-aeee265f4ab2','B','夏装',NULL,6,1,'夏装-B','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a29395f1-cbb6-4d3f-bd86-b83c4db15c69','7b664e3e-f58a-4e66-8c0f-be1458541d14','LGS','LGS爱女孩',NULL,116,1,'爱女孩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a30fb363-6976-4232-b9c6-b154170e3dc0','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZGM','ZGM专供棉服',NULL,201,1,'ZGM专供棉服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a36dfd28-f130-4d84-83a7-e9d15d5682a3','7b664e3e-f58a-4e66-8c0f-be1458541d14','YRW','YRW羊绒围巾',NULL,138,1,'羊绒围巾','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a50b6cb4-560f-41af-906a-ef5f43b19186','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSL','SSL桑索',NULL,51,1,'桑索','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a5d04a95-7fd7-4e6a-8962-4f8889c3d431','7b664e3e-f58a-4e66-8c0f-be1458541d14','YXX','YXX羽希',NULL,58,1,'羽希','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a8d9bd2e-8a0f-4235-8d93-4b5909c945b4','7b664e3e-f58a-4e66-8c0f-be1458541d14','MDX','MDX漫多',NULL,223,1,'MDX漫多','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a97b8fa7-c260-4df6-a1bb-d5314d398baa','7b664e3e-f58a-4e66-8c0f-be1458541d14','JNX','JNX邂逅江南',NULL,310,1,'JNX邂逅江南','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ab170ed2-56c5-467e-8630-02db6910ab44','7b664e3e-f58a-4e66-8c0f-be1458541d14','JJW','JJW晶晶旗威',NULL,161,1,'晶晶旗威','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ad5cff15-ab87-4ad6-9ea1-89503d2f1832','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','8','8',NULL,26,1,'8','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ad80fbf8-7db6-40e7-8c88-32a1abb1eee4','7b664e3e-f58a-4e66-8c0f-be1458541d14','FZX','梵姿FZX',NULL,79,1,'梵姿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ade7d3e9-0026-43e7-83fe-96c1be30122c','7b664e3e-f58a-4e66-8c0f-be1458541d14','CRZ','CR''Z',NULL,52,1,'CR''Z','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('af8c9140-12ec-44e2-9691-c08adbb4b8de','48458681-48b0-490a-a840-0ffcbe49f5d4','W','卫衣',NULL,297,1,'卫衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b1b5f177-0fff-4574-b90f-2ae0fdf3d8bd','7b664e3e-f58a-4e66-8c0f-be1458541d14','YJR','YJR羊剪绒',NULL,237,1,'YJR羊剪绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b2229e38-a32d-4373-831d-0c3c438365ec','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZX','MZX曼紫秋mzx',NULL,130,1,'曼紫秋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b289b54a-9362-4db5-941f-886b9519e5ed','48458681-48b0-490a-a840-0ffcbe49f5d4','F','裤子',NULL,18,1,'裤子-F','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b2f81d20-4274-41ee-bbee-0adf5994b401','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDM','打底毛衫DDM',NULL,162,1,'打底毛衫DDM','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b34e3d29-f2e2-4fe4-81cd-a5b4347d44d8','8cb134d5-979b-40e2-b453-aeee265f4ab2','C','秋装',NULL,7,1,'秋装-C','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b444ae4e-244d-401a-b3d3-37f1c71c4ee6','7b664e3e-f58a-4e66-8c0f-be1458541d14','YMX','YMX翼美',NULL,97,1,'翼美','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b614c223-d776-4045-a729-dc18294b190e','7088d9b9-6692-4fc7-a83c-da580f1407c3','1004','公斤',NULL,76,1,NULL,'2018-06-30 19:28:56','2018-06-30 19:28:56'),
('b61e977f-6162-4443-97c3-0d0473aa281b','7b664e3e-f58a-4e66-8c0f-be1458541d14','JZK','JZK羽绒服三',NULL,180,1,'JZK羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b87554b5-541d-48ec-9978-49f422fd7ac0','7b664e3e-f58a-4e66-8c0f-be1458541d14','LYR','LYR蓝雅绒',NULL,160,1,'蓝雅绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ba90d75a-5d8a-4dac-9458-f46d12511d57','7088d9b9-6692-4fc7-a83c-da580f1407c3','1003','条',NULL,75,1,NULL,'2018-06-30 19:28:50','2018-06-30 19:28:50'),
('baab9800-b7db-4ea3-8b89-1f4ae38ea822','7b664e3e-f58a-4e66-8c0f-be1458541d14','GFN','GFN哥芙妮羽绒服',NULL,177,1,'哥芙妮羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('bc2d7429-eb0b-4f24-aa70-f40d7589aa81','e86cf108-dc4d-4532-8cce-fdb041363902','C','S',NULL,34,1,'S','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('bc6ba969-a659-4e1c-8dee-d29608324b33','7b664e3e-f58a-4e66-8c0f-be1458541d14','WHY','WHY王后羽绒服',NULL,176,1,'王后羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('be8c4b17-e786-4441-ab9e-7f1dbeb4a776','7b664e3e-f58a-4e66-8c0f-be1458541d14','HHS','HHS黄鹤双面尼',NULL,248,1,'HHS黄鹤双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c0d871ba-0670-466c-8586-e02ce2c65c02','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDO','DDO迪迪欧秋',NULL,92,1,'迪迪欧秋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c0e51b55-671b-480a-b864-69c23a81fc0b','7b664e3e-f58a-4e66-8c0f-be1458541d14','DBQ','DBQ迪碧茜毛衣',NULL,303,1,'DBQ迪碧茜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c1d2b3f8-032a-4410-bf05-249053a825e3','7b664e3e-f58a-4e66-8c0f-be1458541d14','XKL','XKL茜可可',NULL,50,1,'茜可可','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c203be2e-2873-4d0a-9faf-68e894e1e2f3','7b664e3e-f58a-4e66-8c0f-be1458541d14','HZZ','HZZ杭州杂',NULL,247,1,'HZZ杭州杂','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c2abdd8e-2eae-47eb-a694-4714da6137fe','7b664e3e-f58a-4e66-8c0f-be1458541d14','BBL','BBL播',NULL,65,1,'播','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c2e89f09-04f6-4645-95fc-c480bd551714','7b664e3e-f58a-4e66-8c0f-be1458541d14','HHJ','HHJ红火家人',NULL,234,1,'HHJ红火家人','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c2ff2b85-e617-4e71-95c0-3b9638b606d6','7b664e3e-f58a-4e66-8c0f-be1458541d14','JOR','JOR乔丹',NULL,4,1,'乔丹-JOR','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c36abb33-00ac-42c6-8cdc-45fca59bb9f1','7b664e3e-f58a-4e66-8c0f-be1458541d14','QCX','千禅QCX',NULL,208,1,'千禅女装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c7f0ea3d-42c1-4e7f-8102-402ac55c0b01','7088d9b9-6692-4fc7-a83c-da580f1407c3','1007','套',NULL,79,1,NULL,'2018-06-30 19:29:26','2018-06-30 19:29:26');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('c8ee7d8b-1f25-423d-b9ec-d557805d6b67','48458681-48b0-490a-a840-0ffcbe49f5d4','R','衬衣',NULL,157,1,'衬衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ca73d117-461c-4a3f-aacb-36a8570abd48','7b664e3e-f58a-4e66-8c0f-be1458541d14','SRL','SRL赛睿娜L',NULL,118,1,'SRL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('caf2b551-3599-4b86-be78-f540d3ef5dcc','7b664e3e-f58a-4e66-8c0f-be1458541d14','CZM','CZM粗针毛衫',NULL,263,1,'CZM粗针毛衫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cb500968-e8ab-4494-bc64-76b338fff965','7b664e3e-f58a-4e66-8c0f-be1458541d14','QLS','QLS琦丽莎',NULL,213,1,'QLS琦丽莎','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cc708b6c-a96d-4c5d-9241-0bbb6af11f26','7b664e3e-f58a-4e66-8c0f-be1458541d14','PSB','PSB帕斯宝',NULL,279,1,'PSB帕斯宝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ccf9a0cc-aba8-4e05-81ce-9585686b6643','7b664e3e-f58a-4e66-8c0f-be1458541d14','FRX','FRX芙瑞宣',NULL,62,1,'芙瑞宣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ce01577b-f8f7-4619-994b-968235c42ad2','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZRS','ZRS绽然双面尼',NULL,287,1,'ZRS绽然','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cecc6dc2-76ed-4737-99b9-870c42ca03e6','7b664e3e-f58a-4e66-8c0f-be1458541d14','PYY','PYY球球款三',NULL,194,1,'PYY球球款三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cf054a61-9bdc-4508-9567-585060f067e5','7b664e3e-f58a-4e66-8c0f-be1458541d14','IKX','IKX',NULL,158,1,'IKX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cf992e05-e8c5-451a-b95b-8b4735903082','7b664e3e-f58a-4e66-8c0f-be1458541d14','MQL','MQL玛琪雅朵',NULL,60,1,'玛琪雅朵','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d16848b9-60d2-48db-a746-79c082aa6578','7b664e3e-f58a-4e66-8c0f-be1458541d14','M03','M03曼紫03羽绒服三',NULL,182,1,'曼紫03羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d16d0a0d-c884-412c-b32d-c1912b0c7f41','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZR','MZR曼紫三',NULL,195,1,'MZR曼紫三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('d2ef5490-f39b-4652-b9f2-a4ba87369750','7b664e3e-f58a-4e66-8c0f-be1458541d14','MEX','MEX沐恩',NULL,48,1,'沐恩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d34c7cb9-68d6-4875-bc0b-75a2b93000de','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','3','3',NULL,136,1,'3','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d36d28d3-2054-44cb-9817-a1a43ac4d5c6','7b664e3e-f58a-4e66-8c0f-be1458541d14','HEM','HEM和尔美',NULL,196,1,'HEM和尔美','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d4c6125c-558d-406e-a4dc-3cf8d72f92b8','7b664e3e-f58a-4e66-8c0f-be1458541d14','MEL','MEL沐恩L',NULL,163,1,'沐恩L','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d56ddc33-ebb7-44ad-a7a2-cc0efe4589ff','7b664e3e-f58a-4e66-8c0f-be1458541d14','SNL','SNL圣娜依儿',NULL,100,1,'圣娜依儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d6c64f0a-dc6d-4cab-b51f-b7db35a3eb02','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZLX','ZLX庄丽欣',NULL,45,1,'庄丽欣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d7acb442-d4dd-4745-af1e-6fd6ec69fae6','7b664e3e-f58a-4e66-8c0f-be1458541d14','SWX','述忘SWX',NULL,219,1,'述忘SWX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d7c945e2-3917-44db-999c-e7a51b9ddd99','7b664e3e-f58a-4e66-8c0f-be1458541d14','QSX','QSX强缩绒',NULL,309,1,'QSX强缩绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d7e140c1-0a74-4829-8e97-c0a378172f2c','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZHG','ZHG子恒国际双面尼',NULL,293,1,'ZHG子恒国际双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d844e30b-5eb6-4800-a449-061dd4c56af5','7b664e3e-f58a-4e66-8c0f-be1458541d14','MSX','MSX曼丝秀登',NULL,90,1,'曼丝秀登','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dab7d485-278d-460f-a722-ee6311d51d23','7088d9b9-6692-4fc7-a83c-da580f1407c3','1001','件',NULL,73,1,NULL,'2018-06-30 19:28:37','2018-06-30 19:28:37'),
('daea1534-8f45-4dd3-8344-42c7cd41a636','7b664e3e-f58a-4e66-8c0f-be1458541d14','BBX','BBX播',NULL,199,1,'BBX播','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('db09e1fc-40b6-4e06-b754-5f217b6b5262','7b664e3e-f58a-4e66-8c0f-be1458541d14','ARB','ARB阿尔巴卡双面尼',NULL,264,1,'ARB阿尔巴卡双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dbbf8787-343c-4962-bdf2-e5e36944f51f','7088d9b9-6692-4fc7-a83c-da580f1407c3','1005','箱',NULL,77,1,NULL,'2018-06-30 19:29:06','2018-06-30 19:29:06'),
('dbda1090-2471-47d8-a32d-765f9eb08910','7b664e3e-f58a-4e66-8c0f-be1458541d14','KWN','KWN柯文娜',NULL,242,1,'KWN柯文娜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dcb6bd5a-4057-444b-8edd-e2355aa36954','48458681-48b0-490a-a840-0ffcbe49f5d4','N','毛衫',NULL,139,1,'','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dd0abc85-f78f-421c-843b-a6959a08c105','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','C','C',NULL,30,1,'C','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dd272d8b-9a09-4672-a4dd-5f740b17a363','7b664e3e-f58a-4e66-8c0f-be1458541d14','APM','APM艾普玛',NULL,296,1,'APM艾普玛','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('deb3a772-884d-4fd9-a555-1b8b7b05d124','7b664e3e-f58a-4e66-8c0f-be1458541d14','XKM','XKM2017新款毛衣',NULL,252,1,'XKM2017新款毛衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('df908b0c-c2c4-41ed-bdc5-2fe97889b344','7b664e3e-f58a-4e66-8c0f-be1458541d14','FAR','FAR F.艾人',NULL,270,1,' F.艾人','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e00ae7bb-c23d-4326-b050-4986e5138c5a','7b664e3e-f58a-4e66-8c0f-be1458541d14','NRW','NRW女人屋',NULL,61,1,'女人屋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e064b9a9-924d-488b-8e5d-00b751d2821b','7b664e3e-f58a-4e66-8c0f-be1458541d14','XPP','XPP溆牌',NULL,269,1,'溆牌','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e1ee070b-1932-43e3-a7c4-20f05d672ba1','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','7','7',NULL,25,1,'7','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e2d9b374-1c31-4260-bafa-717c4bda88cb','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZTL','ZTL紫藤罗',NULL,49,1,'紫藤罗','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('e48f49c9-626c-4bcb-be1f-70a9e17765a1','7b664e3e-f58a-4e66-8c0f-be1458541d14','HZS','HZS杭州双面尼',NULL,254,1,'HZS杭州双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e5d71e81-9e0c-4d58-93ab-cdad615f5edf','7b664e3e-f58a-4e66-8c0f-be1458541d14','AGW','AGW昂购物',NULL,311,1,'AGW昂购物','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e6b2a41e-37d1-4104-a79f-ef3927bf73bd','48458681-48b0-490a-a840-0ffcbe49f5d4','M','羽绒服',NULL,148,1,'羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e6f0fce3-37da-4731-a08d-c0e07e086938','8cb134d5-979b-40e2-b453-aeee265f4ab2','E','春夏装',NULL,9,1,'春夏装-E','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e7c6b1ea-645f-406a-a8ac-9706768472cc','7b664e3e-f58a-4e66-8c0f-be1458541d14','YXL','YXL依香丽影',NULL,47,1,'依香丽影','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e98ed7b9-c959-4326-a1a7-0fe7ad6179c0','48458681-48b0-490a-a840-0ffcbe49f5d4','J','老年处理',NULL,153,1,'老年处理','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e9d62a8b-09bd-435a-b3e2-a9822a2bede0','7b664e3e-f58a-4e66-8c0f-be1458541d14','JJL','JJL晶晶旗威',NULL,99,1,'晶晶旗威','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ea7d64e7-a57a-4df9-924d-0a2d73ff9b08','7b664e3e-f58a-4e66-8c0f-be1458541d14','BRM','BRM芭而慕',NULL,292,1,'BRM芭而慕','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('eb23a73e-cc4d-4caf-83cd-ca74bb226b85','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSX','SSX述色',NULL,306,1,'SSX述色','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ed24f032-166f-4ffb-88d5-a23edcbb23f5','7b664e3e-f58a-4e66-8c0f-be1458541d14','AAA','AAA处理品',NULL,233,1,'AAA处理品','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ee673e40-ec67-4653-92fc-3fd33c220514','7b664e3e-f58a-4e66-8c0f-be1458541d14','LIN','LIN李宁',NULL,3,1,'李宁-LIN','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('eeec08d1-c9a5-491c-ba1b-01335c7e3b95','48458681-48b0-490a-a840-0ffcbe49f5d4','B','连衣裙',NULL,14,1,'连衣裙-B','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('ef5df705-7ce0-4d08-ade0-42d6c9af48a9','7088d9b9-6692-4fc7-a83c-da580f1407c3','1002','个',NULL,74,1,NULL,'2018-06-30 19:28:42','2018-06-30 19:28:42'),
('f0a9b98f-534f-4edb-9f88-23c60a823406','7b664e3e-f58a-4e66-8c0f-be1458541d14','BFX','BFX柏芙澜',NULL,262,1,'BFX柏芙澜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f0b27702-4212-430c-ba28-5adda333a86a','7b664e3e-f58a-4e66-8c0f-be1458541d14','MNX','MNX玛尼毛衫',NULL,221,1,'MNX玛尼毛衫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f1e5ca9a-2970-4849-bbd3-efb766e5a848','7b664e3e-f58a-4e66-8c0f-be1458541d14','GLS','格鲁丝',NULL,301,1,'GLS格鲁丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f266600a-7af3-4238-aed1-ccbffd887db3','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZS','MZS曼紫双面呢',NULL,77,1,'曼紫双面呢','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f3251ee9-8c9d-4547-ae7c-a7bdc7ca95dd','7b664e3e-f58a-4e66-8c0f-be1458541d14','TAS','TAS铜氨丝',NULL,284,1,'TAS铜氨丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f38bbf12-e3c1-497c-95d9-1c68b3ae3e9c','48458681-48b0-490a-a840-0ffcbe49f5d4','Q','套装',NULL,156,1,'套装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f3a5b120-3ff6-4679-a159-a2af679a6e24','7b664e3e-f58a-4e66-8c0f-be1458541d14','BJN','BJN贝婕妮春装',NULL,260,1,'BJN贝婕妮春装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f44155fb-734c-4e6b-bbe2-fa0a3c3e6e94','7b664e3e-f58a-4e66-8c0f-be1458541d14','CCC','CCC折扣品',NULL,268,1,'折扣品','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f4c60280-5885-48b5-9093-623170a8a2a1','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSS','SSS莎莎双面尼',NULL,249,1,'SSS莎莎双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f4d4f3ba-326f-473b-ac5e-c3fad3ac56f4','7b664e3e-f58a-4e66-8c0f-be1458541d14','MGG','MGG木果果木',NULL,274,1,'MGG木果果木','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f65fd2dd-63c5-4d00-bba7-a1593a4ca40b','7b664e3e-f58a-4e66-8c0f-be1458541d14','STX','STX诗婷',NULL,313,1,'STX诗婷','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `sys_code`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('f6d55b1a-10c4-4ee9-a160-ed982411e663','7b664e3e-f58a-4e66-8c0f-be1458541d14','CSX','尘色CSX',NULL,127,1,' 尘色X','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f7b26c3e-4128-40ab-b080-144cfd28b6fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','XKB','XKB西可可',NULL,80,1,'西可可','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f8ce041e-8da3-4cba-9ca3-762922062a63','48458681-48b0-490a-a840-0ffcbe49f5d4','D','外套',NULL,16,1,'外套-D','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fbf9703e-23d9-4b3d-b8d1-b747c1d44a89','7b664e3e-f58a-4e66-8c0f-be1458541d14','RST','RST双面尼三',NULL,175,1,'RST双面尼三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fdd9a2c3-5256-4eb7-abc6-d11789517550','7b664e3e-f58a-4e66-8c0f-be1458541d14','RLX','RLX芮丽',NULL,280,1,'RLX芮丽','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fe355bd2-9ac5-4127-8b75-7830d30353af','7b664e3e-f58a-4e66-8c0f-be1458541d14','YXS','YXS伊袖',NULL,300,1,'YXS伊袖','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fedc9430-77ea-4055-b923-ce90964f09c9','e86cf108-dc4d-4532-8cce-fdb041363902','E','L',NULL,36,1,'L','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ff1f8041-dfb8-43af-8540-76e1de25af53','7b664e3e-f58a-4e66-8c0f-be1458541d14','XBX','XBX小背心',NULL,229,1,'XBX小背心','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ff899c09-9242-4d0a-aad6-64594079227a','7b664e3e-f58a-4e66-8c0f-be1458541d14','LCX','LCX莱茨大衣',NULL,253,1,'LCX莱茨大衣','2018-10-19 13:06:36','2018-10-19 13:06:36');
/*!40000 ALTER TABLE `sys_code` ENABLE KEYS */;

-- 
-- Definition of sys_codetype
-- 

DROP TABLE IF EXISTS `sys_codetype`;
CREATE TABLE IF NOT EXISTS `sys_codetype` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标号Guid',
  `ParentGuid` varchar(50) DEFAULT NULL COMMENT '字典类型父级',
  `Layer` int(11) NOT NULL DEFAULT '0' COMMENT '深度',
  `Name` varchar(50) NOT NULL COMMENT '字典类型名称',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '字典类型排序',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `SiteGuid` varchar(50) DEFAULT NULL COMMENT '归属公司或站点',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_codetype
-- 

/*!40000 ALTER TABLE `sys_codetype` DISABLE KEYS */;
INSERT INTO `sys_codetype`(`Guid`,`ParentGuid`,`Layer`,`Name`,`Sort`,`AddTime`,`EditTime`,`SiteGuid`) VALUES
('1942d4fd-3203-42b1-a955-4a84a532b2a2','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'年份',19,'2018-07-24 18:54:48','2018-07-24 18:54:48',NULL),
('2e0393f9-e6d6-4c15-98cf-96072f0d3d70','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'批次',15,'2018-06-18 06:38:03','2018-06-18 06:38:03',NULL),
('48458681-48b0-490a-a840-0ffcbe49f5d4','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'款式',14,'2018-06-18 06:37:55','2018-06-18 06:37:55',NULL),
('7088d9b9-6692-4fc7-a83c-da580f1407c3','9d7643f0-d739-4342-b2da-45781b62ddd0',1,'采购商品单位',18,'2018-06-30 19:28:13','2018-06-30 19:28:13',NULL),
('7b664e3e-f58a-4e66-8c0f-be1458541d14','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'品牌',5,'2018-06-18 06:21:54','2018-06-18 06:21:54',NULL),
('8cb134d5-979b-40e2-b453-aeee265f4ab2','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'季节',13,'2018-06-18 06:35:32','2018-06-18 06:35:32',NULL),
('8d3158d6-e179-4046-99e9-53eb8c04ddb1',NULL,0,'服装SKU',4,'2018-06-18 06:21:45','2018-06-18 06:21:46',NULL),
('9d7643f0-d739-4342-b2da-45781b62ddd0',NULL,0,'采购字典',17,'2018-06-30 19:28:02','2018-06-30 19:28:02',NULL),
('e86cf108-dc4d-4532-8cce-fdb041363902','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'尺码',16,'2018-06-18 06:38:09','2018-06-18 06:38:09',NULL);
/*!40000 ALTER TABLE `sys_codetype` ENABLE KEYS */;

-- 
-- Definition of sys_log
-- 

DROP TABLE IF EXISTS `sys_log`;
CREATE TABLE IF NOT EXISTS `sys_log` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标识——Guid',
  `LoginName` varchar(50) NOT NULL COMMENT '日志操作ID',
  `DepartName` varchar(50) DEFAULT NULL COMMENT '日志操作人所属部门Guid',
  `OptionTable` varchar(50) DEFAULT NULL COMMENT '操作表名',
  `Summary` varchar(255) NOT NULL COMMENT '日志操作内容',
  `IP` varchar(20) NOT NULL COMMENT '日志操作IP地址',
  `MacUrl` varchar(50) DEFAULT NULL COMMENT '日志操作Mac地址',
  `LogType` int(11) NOT NULL DEFAULT '0' COMMENT '日志操作类型',
  `Urls` varchar(255) NOT NULL COMMENT '日志操作Url',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '日志添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_log
-- 

/*!40000 ALTER TABLE `sys_log` DISABLE KEYS */;
INSERT INTO `sys_log`(`Guid`,`LoginName`,`DepartName`,`OptionTable`,`Summary`,`IP`,`MacUrl`,`LogType`,`Urls`,`AddTime`) VALUES
('1cc1dd0c-6d10-4655-a1ed-f846ebcf76bc','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-25 14:31:59'),
('1f411ac3-cd27-4ee7-9fb3-ecb53a06258a','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-25 10:30:56'),
('3b988aa9-93b8-409a-bb5e-8f3b2e1e5319','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-23 19:02:36'),
('5e1aa40a-aab5-4e70-b050-a6d9e1300db8','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-29 09:40:58'),
('69772693-198d-4e58-94a8-ac599a15022b','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-30 21:52:32'),
('72064576-6926-4b4c-8dcb-b37cb663c917','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-30 22:53:14'),
('74e45eba-f1dd-4358-b955-1fe2b42743ff','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-31 21:24:43'),
('7b9029e2-6276-4f3e-99c6-350c4cff88ec','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-25 09:29:58'),
('f2c549f3-cc47-4ee0-9cf0-5ffd40cba44c','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-31 22:34:58');
/*!40000 ALTER TABLE `sys_log` ENABLE KEYS */;

-- 
-- Definition of sys_menu
-- 

DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE IF NOT EXISTS `sys_menu` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标识Guid',
  `SiteGuid` varchar(50) DEFAULT NULL COMMENT '所属站点或公司菜单',
  `ParentGuid` varchar(50) DEFAULT NULL COMMENT '菜单父级Guid',
  `ParentName` varchar(50) NOT NULL COMMENT '父级菜单名称',
  `Name` varchar(50) NOT NULL COMMENT '菜单名称',
  `NameCode` varchar(50) NOT NULL COMMENT '菜单名称标识',
  `ParentGuidList` varchar(500) DEFAULT NULL COMMENT '所属父级的集合',
  `Layer` int(10) NOT NULL COMMENT '菜单深度',
  `Urls` varchar(255) DEFAULT NULL COMMENT '菜单Url',
  `Icon` varchar(50) DEFAULT NULL COMMENT '菜单图标Class',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '菜单排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '菜单状态 true=正常 false=不显示',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  `AddTIme` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_menu
-- 

/*!40000 ALTER TABLE `sys_menu` DISABLE KEYS */;
INSERT INTO `sys_menu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('00aaf062-ee50-4844-9b51-80743a65cd4d',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','功能组件','2040',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,',2,NULL,'layui-icon-component',62,1,'2018-10-22 23:02:09','2018-10-22 23:02:09'),
('0a61ddff-ead5-49c0-8bed-2189872b8646',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','栏目管理','2020',',a4b3b26f-076a-4267-b03d-613081b13a12,0a61ddff-ead5-49c0-8bed-2189872b8646,',2,NULL,'layui-icon-template',56,1,'2018-09-29 22:03:38','0001-01-01 00:00:00'),
('0d65e849-f903-4cf3-b413-9e4e7bbda82e',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','内容管理','2030',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,',2,NULL,'layui-icon-survey',58,1,'2018-09-29 22:03:56','0001-01-01 00:00:00'),
('1d0bb157-868e-41e6-b048-f2c139111ab3',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','文本回复','3023',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,1d0bb157-868e-41e6-b048-f2c139111ab3,',3,NULL,NULL,74,1,'2018-09-29 21:58:54','2018-09-29 21:58:54'),
('1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633','我的工作台','数据库文件','2013',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1,',3,'/fytadmin/cms/datafile/',NULL,55,1,'2018-10-30 21:53:35','0001-01-01 00:00:00'),
('1fc3d8e8-e3f2-49cf-a652-2341082643df',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','用户管理','1012',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,1fc3d8e8-e3f2-49cf-a652-2341082643df,',3,'/fytadmin/sys/admin/',NULL,6,1,'2018-09-28 23:26:43','2018-09-28 23:26:43');
INSERT INTO `sys_menu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('2a3a4afe-2a51-4858-9c85-df26bb7a7611',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809','微信公众号','基本设置','3010',',f752cbdb-48b9-4958-bd05-0b8c3602e809,2a3a4afe-2a51-4858-9c85-df26bb7a7611,',2,NULL,'layui-icon-set',68,1,'2018-09-29 22:05:43','0001-01-01 00:00:00'),
('35834721-2287-416d-aed2-d0a43277e70e',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','留言管理','2043',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,35834721-2287-416d-aed2-d0a43277e70e,',3,NULL,NULL,65,1,'2018-09-29 21:52:21','2018-09-29 21:52:21'),
('3d0acfb2-fa3c-4fe6-bd5c-e587c4523978',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809','微信公众号','消息管理','3020',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,',2,NULL,'layui-icon-speaker',71,1,'2018-09-29 22:05:53','0001-01-01 00:00:00'),
('3d4f34cb-b69f-47e1-8abb-2f2b7ae20520',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','关注回复','3021',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,3d4f34cb-b69f-47e1-8abb-2f2b7ae20520,',3,NULL,NULL,72,1,'2018-09-29 21:58:22','2018-09-29 21:58:22'),
('3f8327fd-b8be-44d9-801c-39520e72da87',NULL,'0a61ddff-ead5-49c0-8bed-2189872b8646','栏目管理','栏目列表','2021',',a4b3b26f-076a-4267-b03d-613081b13a12,0a61ddff-ead5-49c0-8bed-2189872b8646,3f8327fd-b8be-44d9-801c-39520e72da87,',3,NULL,NULL,57,1,'2018-09-29 21:50:00','2018-09-29 21:50:00'),
('404d4b8b-8e3c-42ee-aee5-f29df31308fa',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','菜单管理','1013',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,404d4b8b-8e3c-42ee-aee5-f29df31308fa,',3,'/fytadmin/sys/menu/',NULL,7,1,'2018-09-28 23:26:50','2018-09-28 23:26:50');
INSERT INTO `sys_menu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('4e104381-22f5-4a91-a784-00a7276afa61',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','文件管理','2045',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,4e104381-22f5-4a91-a784-00a7276afa61,',3,NULL,NULL,67,1,'2018-09-29 21:52:58','2018-09-29 21:52:58'),
('51216bb3-d0c7-474a-b565-71cf96db19f4',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','版本更新','1016',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,51216bb3-d0c7-474a-b565-71cf96db19f4,',3,'/fytadmin/app/setting/',NULL,10,1,'2018-09-28 23:27:39','2018-09-28 23:27:39'),
('51c9c0aa-de65-47d0-87bc-cef0624cb8f9',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633','我的工作台','数据库备份','2012',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,51c9c0aa-de65-47d0-87bc-cef0624cb8f9,',3,'/fytadmin/cms/database/',NULL,54,1,'2018-10-30 21:53:18','0001-01-01 00:00:00'),
('5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','角色管理','1011',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,5ce13ead-971b-4ed4-ad5f-acacccd82381,',3,'/fytadmin/sys/role/',NULL,5,1,'2018-09-28 23:26:07','2018-09-28 23:26:07'),
('5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','系统管理','1001',',a4b3b26f-076a-4267-b03d-613081b13a12,5ed17c74-0fff-4f88-8581-3b4f26d005a8,',2,NULL,'layui-icon-set',2,1,'2018-10-22 23:06:12','0001-01-01 00:00:00'),
('6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','部门管理','1010',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,6d4cfcf7-ff1c-4537-aa3f-75cc9df27583,',3,'/fytadmin/sys/organize/',NULL,4,1,'2018-09-28 23:22:49','2018-09-28 23:22:49');
INSERT INTO `sys_menu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('787ae7bf-fb35-4ed4-9c6a-15aba81609c3',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','语音回复','3025',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,787ae7bf-fb35-4ed4-9c6a-15aba81609c3,',3,NULL,NULL,76,1,'2018-09-29 21:59:25','2018-09-29 21:59:25'),
('7e2356b0-f77f-41fe-b27b-15665b0ccba0',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','下载管理','2044',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,7e2356b0-f77f-41fe-b27b-15665b0ccba0,',3,NULL,NULL,66,1,'2018-09-29 21:52:37','2018-09-29 21:52:37'),
('8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','默认回复','3022',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f,',3,NULL,NULL,73,1,'2018-09-29 21:58:38','2018-09-29 21:58:38'),
('945de8ba-a13d-4ffc-aa62-c072ea2a3b05',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e','内容管理','文章管理','2031',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,945de8ba-a13d-4ffc-aa62-c072ea2a3b05,',3,NULL,NULL,59,1,'2018-09-29 21:50:43','2018-09-29 21:50:43'),
('98285095-b35d-458d-9908-355d2e4fddbd',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','广告管理','2041',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,98285095-b35d-458d-9908-355d2e4fddbd,',3,NULL,NULL,63,1,'2018-09-29 21:51:54','2018-09-29 21:51:54'),
('a05afbda-1234-4ca0-a160-6dd11ea3bf7d',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','消息记录','3026',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,a05afbda-1234-4ca0-a160-6dd11ea3bf7d,',3,NULL,NULL,77,1,'2018-09-29 21:59:42','2018-09-29 21:59:42');
INSERT INTO `sys_menu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('a171bbb0-c65c-4e09-82f5-9ed51169b24d',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611','基本设置','公众平台管理','3011',',f752cbdb-48b9-4958-bd05-0b8c3602e809,2a3a4afe-2a51-4858-9c85-df26bb7a7611,a171bbb0-c65c-4e09-82f5-9ed51169b24d,',3,NULL,NULL,69,1,'2018-09-29 21:56:59','2018-09-29 21:56:59'),
('a280f6e2-3100-445f-871d-77ea443356a9',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','字段管理','1015',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,a280f6e2-3100-445f-871d-77ea443356a9,',3,'/fytadmin/sys/codes/',NULL,9,1,'2018-09-28 23:27:32','2018-09-28 23:27:32'),
('a4b3b26f-076a-4267-b03d-613081b13a12',NULL,NULL,'根目录','CMS内容管理','0002',',a4b3b26f-076a-4267-b03d-613081b13a12,',1,NULL,'layui-icon-website',50,1,'2018-09-29 16:18:57','2018-09-29 16:18:57'),
('a82ecfbf-b428-4022-b9a3-81ad277fc05c',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','投票管理','2042',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,a82ecfbf-b428-4022-b9a3-81ad277fc05c,',3,NULL,NULL,64,1,'2018-09-29 21:52:07','2018-09-29 21:52:07'),
('a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e','内容管理','点击排行','2033',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a,',3,NULL,NULL,61,1,'2018-09-29 21:51:13','2018-09-29 21:51:13'),
('b354ea64-88b6-4032-a26a-fee198e24d9d',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','系统日志','1014',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,b354ea64-88b6-4032-a26a-fee198e24d9d,',3,'/fytadmin/sys/log/',NULL,8,1,'2018-09-28 23:27:06','2018-09-28 23:27:06'),
('b8ede145-b5c2-4339-a3cc-f9808aa0c776',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','图文回复','3024',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,b8ede145-b5c2-4339-a3cc-f9808aa0c776,',3,NULL,NULL,75,1,'2018-09-29 21:59:10','2018-09-29 21:59:10');
INSERT INTO `sys_menu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('be526a42-9a48-4221-bc9b-3e1d5ddddf2f',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633','我的工作台','站点管理','2011',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,be526a42-9a48-4221-bc9b-3e1d5ddddf2f,',3,'/fytadmin/cms/site/',NULL,53,1,'2018-10-30 21:53:27','0001-01-01 00:00:00'),
('c1f840e9-e822-4d0f-aca2-28365c52a7c4',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e','内容管理','回收站管理','2032',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,c1f840e9-e822-4d0f-aca2-28365c52a7c4,',3,NULL,NULL,60,1,'2018-09-29 21:50:58','2018-09-29 21:50:58'),
('dad12bae-d3f3-4c0e-a728-2e6af5f40e66',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611','基本设置','自定义菜单','3012',',f752cbdb-48b9-4958-bd05-0b8c3602e809,2a3a4afe-2a51-4858-9c85-df26bb7a7611,dad12bae-d3f3-4c0e-a728-2e6af5f40e66,',3,NULL,NULL,70,1,'2018-09-29 21:57:15','2018-09-29 21:57:15'),
('f752cbdb-48b9-4958-bd05-0b8c3602e809',NULL,NULL,'根目录','微信公众号','0003',',f752cbdb-48b9-4958-bd05-0b8c3602e809,',1,NULL,'layui-icon-login-wechat',51,1,'2018-09-29 16:19:10','2018-09-29 16:19:10'),
('fe96606e-2b92-4587-8196-e5b4e85ed633',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','我的工作台','2010',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,',2,NULL,'layui-icon-website',52,1,'2018-09-29 21:43:43','2018-09-29 21:43:43');
/*!40000 ALTER TABLE `sys_menu` ENABLE KEYS */;

-- 
-- Definition of sys_message
-- 

DROP TABLE IF EXISTS `sys_message`;
CREATE TABLE IF NOT EXISTS `sys_message` (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '自动标识',
  `ClassID` int(11) NOT NULL DEFAULT '0' COMMENT '栏目ID',
  `Types` int(11) NOT NULL DEFAULT '0' COMMENT '类型',
  `Title` varchar(200) DEFAULT NULL COMMENT '标题',
  `Mobile` varchar(50) DEFAULT NULL COMMENT '电话号码',
  `Email` varchar(255) DEFAULT NULL COMMENT '联系邮箱',
  `QQ` varchar(50) DEFAULT NULL COMMENT 'QQ',
  `Status` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否查看',
  `Summary` varchar(2000) DEFAULT NULL COMMENT '描述',
  `Content` text COMMENT '内容',
  `UserId` int(11) NOT NULL DEFAULT '0' COMMENT '用户ID',
  `UserName` varchar(50) DEFAULT NULL COMMENT '用户名称',
  `ParentId` int(11) NOT NULL DEFAULT '0' COMMENT '父ID',
  `RepId` int(11) NOT NULL DEFAULT '0' COMMENT '回复ID',
  `AddDate` datetime DEFAULT NULL COMMENT '添加时间',
  `RepDate` datetime DEFAULT NULL COMMENT '回复时间',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_message
-- 

/*!40000 ALTER TABLE `sys_message` DISABLE KEYS */;

/*!40000 ALTER TABLE `sys_message` ENABLE KEYS */;

-- 
-- Definition of sys_organize
-- 

DROP TABLE IF EXISTS `sys_organize`;
CREATE TABLE IF NOT EXISTS `sys_organize` (
  `Guid` varchar(50) NOT NULL,
  `SiteGuid` varchar(50) DEFAULT NULL COMMENT '归属站点',
  `ParentGuid` varchar(50) DEFAULT NULL COMMENT '父节点',
  `Name` varchar(20) NOT NULL COMMENT '组织名称',
  `ParentName` varchar(20) DEFAULT NULL,
  `ParentGuidList` varchar(500) DEFAULT NULL COMMENT '父节点集合',
  `Layer` int(11) NOT NULL DEFAULT '1' COMMENT '层级',
  `Sort` int(11) NOT NULL DEFAULT '1' COMMENT '排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_organize
-- 

/*!40000 ALTER TABLE `sys_organize` DISABLE KEYS */;
INSERT INTO `sys_organize`(`Guid`,`SiteGuid`,`ParentGuid`,`Name`,`ParentName`,`ParentGuidList`,`Layer`,`Sort`,`Status`,`EditTime`) VALUES
('24febdc4-655f-4492-ac8a-4adab18c22c8',NULL,'388b72d3-e10a-4183-8ef7-6be44eb99b1a','融媒体中心','包头分公司',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,',2,7,1,'2018-05-16 22:09:35'),
('388b72d3-e10a-4183-8ef7-6be44eb99b1a',NULL,'883deb1c-ddd7-484e-92c1-b3ad3b32e655','包头分公司','飞易腾集团',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,',1,3,1,'2018-05-16 22:06:20'),
('4b6ab27f-c0fa-483d-9b5a-55891ee8d727',NULL,'388b72d3-e10a-4183-8ef7-6be44eb99b1a','事业发展部','包头分公司',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,4b6ab27f-c0fa-483d-9b5a-55891ee8d727,',2,6,1,'2018-05-16 22:09:30'),
('52523a76-52b3-4c25-a1bd-9123a011f2a8',NULL,'24febdc4-655f-4492-ac8a-4adab18c22c8','商务中心','融媒体中心',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,52523a76-52b3-4c25-a1bd-9123a011f2a8,',3,4,1,'2018-07-20 23:03:59'),
('5533b6c5-ba2e-4659-be29-c860bb41e04d',NULL,'883deb1c-ddd7-484e-92c1-b3ad3b32e655','北京总公司','飞易腾集团',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,',1,2,1,'2018-05-16 22:06:02'),
('883deb1c-ddd7-484e-92c1-b3ad3b32e655',NULL,NULL,'飞易腾集团','根目录',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,',0,1,1,'2018-05-15 00:11:55'),
('dcf99638-5db6-4dd7-a485-31df1160d45a',NULL,'5533b6c5-ba2e-4659-be29-c860bb41e04d','互联网中心','北京总公司',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,',2,5,1,'2018-05-16 22:09:36');
/*!40000 ALTER TABLE `sys_organize` ENABLE KEYS */;

-- 
-- Definition of sys_permissions
-- 

DROP TABLE IF EXISTS `sys_permissions`;
CREATE TABLE IF NOT EXISTS `sys_permissions` (
  `RoleGuid` varchar(50) NOT NULL COMMENT '角色Guid',
  `AdminGuid` varchar(50) DEFAULT NULL COMMENT '管理员ID',
  `MenuGuid` varchar(50) DEFAULT NULL COMMENT '菜单Guid',
  `BtnFunGuid` varchar(50) DEFAULT NULL,
  `Types` tinyint(1) NOT NULL DEFAULT '1' COMMENT '授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_permissions
-- 

/*!40000 ALTER TABLE `sys_permissions` DISABLE KEYS */;
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61',1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','c4261103-dfc7-46e5-ab20-4ca5fc7729f6',1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','931bd729-f160-4fe3-bb7c-7828a2da047a',1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61',1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','c4261103-dfc7-46e5-ab20-4ca5fc7729f6',1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61',1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','c4261103-dfc7-46e5-ab20-4ca5fc7729f6',1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb','30d3da88-bb72-4ace-a303-b3aae0ecb732',NULL,NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a','12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e','30d3da88-bb72-4ace-a303-b3aae0ecb732',NULL,NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb','12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','b943200f-7c99-44b5-93d9-e4ea2505928a',1),
('2949c266-575a-4e5d-a663-e39d5f33df7e','12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'de263753-0706-4985-bf96-317059e483ff',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'1fc3d8e8-e3f2-49cf-a652-2341082643df',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'404d4b8b-8e3c-42ee-aee5-f29df31308fa',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'b354ea64-88b6-4032-a26a-fee198e24d9d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a280f6e2-3100-445f-871d-77ea443356a9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'51216bb3-d0c7-474a-b565-71cf96db19f4',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'40823e8a-bc10-4e38-a45f-a6dd7dd0ef78',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'f9129ddd-3d96-4980-ac48-f6aa9a8b6ba9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d46fb5d3-27fc-411f-8bc8-df175cc248e4',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'cc59a616-2ca6-4ca8-9907-80ace8d38b47',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'8f6d2ac6-0c9b-4c9c-a1cd-80ca6365781e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d1f782d2-55c3-4ca6-8002-894a1da52515',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'eafbc38f-fccd-4a5a-9df2-44ff41fde6d9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5be5bed2-8b11-470f-a233-69a208737f47',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'88989fbb-57e0-4813-a125-f54ca941299c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'01d476db-17c1-42b9-9725-c995a942006f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'18e89f9b-dc89-4289-a8fd-ee1330e43f79',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d9a75927-0700-42b9-9ded-55774ee5c20b',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'9d1dcb18-6db8-4d79-bc7e-f2c830f4262d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'7bfb29b1-b0af-440f-afe9-82883e2e114e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'b217116f-27c3-4d1e-9eda-538ca34bee45',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'401273ed-1639-4646-8b2d-8171beba1c60',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'79315453-3610-435e-80ed-abd7d8c4f770',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'86fc504c-3ce8-40ec-804a-c0c8fb6b520f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'7f0f61dd-ff53-460e-878c-bb3af87740eb',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'db4e4295-fce4-42a6-96f2-387bddcc5449',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'de39aa52-87a6-4539-884d-1ae3a9d6f99c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5cbca08b-77a6-4294-aa15-4d82d0baa5f8',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'8360ffe4-b8f4-4697-9930-9fbc058d7f92',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6dc1436e-6ed9-43be-96c5-2e165d43459b',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'cb7e34dc-54cd-41a1-bbd2-666ab2bdf742',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'155b3ab6-1043-4a78-9b59-bb3d1433a17a',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'cb2e4ab6-48de-4a1b-80e9-f3f77eaf1a6f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'663d5881-70ec-49a5-ae94-34f53d23608e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d651f1e3-653c-4033-9c80-7de6dc9be422',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6cc8a71a-ef58-48cc-aafb-aafa5a311d7e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'078cff27-e491-48a0-8f64-8abc06e20bd3',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3476c160-da68-4bed-9e0b-e38c7af7d099',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3543facc-f81a-4e66-9f62-1a7ffa7bd8e0',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ef5e2eb-5902-446b-82e4-11a6d36140fe',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'e63fd97a-2a8c-40d4-a1e1-6f574c85864c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'c8728c2f-0637-4b8c-808f-289e31aaf495',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'c26b94c0-cae6-4332-8814-f5c8fbfaa58c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'f879f695-5d6c-4b62-9bfe-79ba8714079f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'be526a42-9a48-4221-bc9b-3e1d5ddddf2f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'51c9c0aa-de65-47d0-87bc-cef0624cb8f9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'0a61ddff-ead5-49c0-8bed-2189872b8646',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3f8327fd-b8be-44d9-801c-39520e72da87',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'945de8ba-a13d-4ffc-aa62-c072ea2a3b05',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'c1f840e9-e822-4d0f-aca2-28365c52a7c4',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'98285095-b35d-458d-9908-355d2e4fddbd',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a82ecfbf-b428-4022-b9a3-81ad277fc05c',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'35834721-2287-416d-aed2-d0a43277e70e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'7e2356b0-f77f-41fe-b27b-15665b0ccba0',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'4e104381-22f5-4a91-a784-00a7276afa61',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a171bbb0-c65c-4e09-82f5-9ed51169b24d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'dad12bae-d3f3-4c0e-a728-2e6af5f40e66',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3d4f34cb-b69f-47e1-8abb-2f2b7ae20520',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'1d0bb157-868e-41e6-b048-f2c139111ab3',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'b8ede145-b5c2-4339-a3cc-f9808aa0c776',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'787ae7bf-fb35-4ed4-9c6a-15aba81609c3',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a05afbda-1234-4ca0-a160-6dd11ea3bf7d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'de263753-0706-4985-bf96-317059e483ff',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'1fc3d8e8-e3f2-49cf-a652-2341082643df',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'404d4b8b-8e3c-42ee-aee5-f29df31308fa',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'b354ea64-88b6-4032-a26a-fee198e24d9d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a280f6e2-3100-445f-871d-77ea443356a9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'51216bb3-d0c7-474a-b565-71cf96db19f4',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'40823e8a-bc10-4e38-a45f-a6dd7dd0ef78',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'f9129ddd-3d96-4980-ac48-f6aa9a8b6ba9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d46fb5d3-27fc-411f-8bc8-df175cc248e4',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'cc59a616-2ca6-4ca8-9907-80ace8d38b47',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'8f6d2ac6-0c9b-4c9c-a1cd-80ca6365781e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d1f782d2-55c3-4ca6-8002-894a1da52515',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'eafbc38f-fccd-4a5a-9df2-44ff41fde6d9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5be5bed2-8b11-470f-a233-69a208737f47',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'88989fbb-57e0-4813-a125-f54ca941299c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'01d476db-17c1-42b9-9725-c995a942006f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'18e89f9b-dc89-4289-a8fd-ee1330e43f79',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d9a75927-0700-42b9-9ded-55774ee5c20b',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'9d1dcb18-6db8-4d79-bc7e-f2c830f4262d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'7bfb29b1-b0af-440f-afe9-82883e2e114e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'b217116f-27c3-4d1e-9eda-538ca34bee45',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'401273ed-1639-4646-8b2d-8171beba1c60',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'79315453-3610-435e-80ed-abd7d8c4f770',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'86fc504c-3ce8-40ec-804a-c0c8fb6b520f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'7f0f61dd-ff53-460e-878c-bb3af87740eb',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'db4e4295-fce4-42a6-96f2-387bddcc5449',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'de39aa52-87a6-4539-884d-1ae3a9d6f99c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5cbca08b-77a6-4294-aa15-4d82d0baa5f8',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'8360ffe4-b8f4-4697-9930-9fbc058d7f92',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6dc1436e-6ed9-43be-96c5-2e165d43459b',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'cb7e34dc-54cd-41a1-bbd2-666ab2bdf742',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'155b3ab6-1043-4a78-9b59-bb3d1433a17a',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'cb2e4ab6-48de-4a1b-80e9-f3f77eaf1a6f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'663d5881-70ec-49a5-ae94-34f53d23608e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d651f1e3-653c-4033-9c80-7de6dc9be422',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6cc8a71a-ef58-48cc-aafb-aafa5a311d7e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'078cff27-e491-48a0-8f64-8abc06e20bd3',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3476c160-da68-4bed-9e0b-e38c7af7d099',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'da7019c8-5df1-46c5-b9ce-3d2d83a73cd8',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3543facc-f81a-4e66-9f62-1a7ffa7bd8e0',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ef5e2eb-5902-446b-82e4-11a6d36140fe',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'e63fd97a-2a8c-40d4-a1e1-6f574c85864c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'c8728c2f-0637-4b8c-808f-289e31aaf495',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'c26b94c0-cae6-4332-8814-f5c8fbfaa58c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'f879f695-5d6c-4b62-9bfe-79ba8714079f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'be526a42-9a48-4221-bc9b-3e1d5ddddf2f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'51c9c0aa-de65-47d0-87bc-cef0624cb8f9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'0a61ddff-ead5-49c0-8bed-2189872b8646',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3f8327fd-b8be-44d9-801c-39520e72da87',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'945de8ba-a13d-4ffc-aa62-c072ea2a3b05',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'c1f840e9-e822-4d0f-aca2-28365c52a7c4',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'98285095-b35d-458d-9908-355d2e4fddbd',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a82ecfbf-b428-4022-b9a3-81ad277fc05c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'35834721-2287-416d-aed2-d0a43277e70e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'7e2356b0-f77f-41fe-b27b-15665b0ccba0',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'4e104381-22f5-4a91-a784-00a7276afa61',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809',NULL,1);
INSERT INTO `sys_permissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a171bbb0-c65c-4e09-82f5-9ed51169b24d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'dad12bae-d3f3-4c0e-a728-2e6af5f40e66',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3d4f34cb-b69f-47e1-8abb-2f2b7ae20520',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'1d0bb157-868e-41e6-b048-f2c139111ab3',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'b8ede145-b5c2-4339-a3cc-f9808aa0c776',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'787ae7bf-fb35-4ed4-9c6a-15aba81609c3',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a05afbda-1234-4ca0-a160-6dd11ea3bf7d',NULL,1);
/*!40000 ALTER TABLE `sys_permissions` ENABLE KEYS */;

-- 
-- Definition of sys_role
-- 

DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE IF NOT EXISTS `sys_role` (
  `Guid` varchar(50) NOT NULL,
  `DepartmentGuid` varchar(50) NOT NULL COMMENT '部门Guid',
  `DepartmentName` varchar(50) NOT NULL COMMENT '部门名称',
  `DepartmentGroup` varchar(500) NOT NULL COMMENT '归属于角色组',
  `Name` varchar(50) NOT NULL COMMENT '部门名称',
  `Codes` varchar(50) NOT NULL COMMENT '部门编号',
  `IsSystem` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否为超级管理员',
  `Summary` varchar(500) DEFAULT NULL COMMENT '部门描述',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sys_role
-- 

/*!40000 ALTER TABLE `sys_role` DISABLE KEYS */;
INSERT INTO `sys_role`(`Guid`,`DepartmentGuid`,`DepartmentName`,`DepartmentGroup`,`Name`,`Codes`,`IsSystem`,`Summary`,`AddTime`,`EditTime`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a','52523a76-52b3-4c25-a1bd-9123a011f2a8','商务中心',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,52523a76-52b3-4c25-a1bd-9123a011f2a8,','客服管理员','1002',1,'只能查看会员相关功能','2018-05-17 23:37:56','2018-07-16 11:24:03'),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb','dcf99638-5db6-4dd7-a485-31df1160d45a','互联网中心',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,','财务管理员','1003',1,'只能查看财务相关功能','2018-05-17 23:39:01','2018-05-17 23:39:01');
/*!40000 ALTER TABLE `sys_role` ENABLE KEYS */;

-- 
-- Definition of sysadmin
-- 

DROP TABLE IF EXISTS `sysadmin`;
CREATE TABLE IF NOT EXISTS `sysadmin` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标识',
  `RoleGuid` varchar(50) DEFAULT NULL COMMENT '角色标识',
  `DepartmentName` varchar(50) NOT NULL COMMENT '部门名称',
  `DepartmentGuid` varchar(50) NOT NULL COMMENT '部门标识',
  `DepartmentGuidList` varchar(500) DEFAULT NULL,
  `LoginName` varchar(30) NOT NULL COMMENT '登录账号',
  `LoginPwd` varchar(50) NOT NULL COMMENT '登录密码',
  `TrueName` varchar(20) DEFAULT NULL COMMENT '真是姓名',
  `Number` varchar(10) NOT NULL COMMENT '编号',
  `HeadPic` varchar(100) NOT NULL COMMENT '头像',
  `Sex` varchar(10) NOT NULL DEFAULT '' COMMENT '性别',
  `Mobile` varchar(15) DEFAULT NULL COMMENT '手机号码',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `Email` varchar(50) DEFAULT NULL COMMENT '邮箱',
  `Summary` varchar(500) DEFAULT NULL COMMENT '描述',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `LoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '登录时间',
  `UpLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '上次登录时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysadmin
-- 

/*!40000 ALTER TABLE `sysadmin` DISABLE KEYS */;
INSERT INTO `sysadmin`(`Guid`,`RoleGuid`,`DepartmentName`,`DepartmentGuid`,`DepartmentGuidList`,`LoginName`,`LoginPwd`,`TrueName`,`Number`,`HeadPic`,`Sex`,`Mobile`,`Status`,`Email`,`Summary`,`AddDate`,`LoginDate`,`UpLoginDate`) VALUES
('12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,'商务中心','52523a76-52b3-4c25-a1bd-9123a011f2a8',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,52523a76-52b3-4c25-a1bd-9123a011f2a8,','admins','pPo9vFeTWOCF0oLKKdX9Jw==','子恒国际','1101','/themes/img/avatar.jpg','男','13888888888',1,NULL,NULL,'2018-10-09 22:54:47','2018-10-23 19:02:36','2018-10-23 19:02:36'),
('30d3da88-bb72-4ace-a303-b3aae0ecb732',NULL,'事业发展部','4b6ab27f-c0fa-483d-9b5a-55891ee8d727',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,4b6ab27f-c0fa-483d-9b5a-55891ee8d727,','testadmin','Ycdvj7dGDz45F6Qlw7OMQ904o/xRuq0k','李四','1002','/themes/img/avatar.jpg','男',NULL,0,NULL,NULL,'2018-07-22 00:42:14','2018-07-22 00:42:14','2018-07-22 00:42:14');
/*!40000 ALTER TABLE `sysadmin` ENABLE KEYS */;

-- 
-- Definition of sysappsetting
-- 

DROP TABLE IF EXISTS `sysappsetting`;
CREATE TABLE IF NOT EXISTS `sysappsetting` (
  `Guid` varchar(50) NOT NULL,
  `AndroidVersion` varchar(50) NOT NULL DEFAULT '0.0' COMMENT '安卓版本号',
  `AndroidFile` varchar(255) DEFAULT NULL COMMENT '更新文件',
  `IosVersion` varchar(50) NOT NULL COMMENT 'Ios版本号',
  `IosFile` varchar(255) DEFAULT NULL COMMENT 'Ios更新文件地址',
  `IosAudit` tinyint(4) NOT NULL DEFAULT '0' COMMENT 'Ios审核开关  0=关/1=开',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '删除 0=不删除/1=删除',
  `UpdateDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysappsetting
-- 

/*!40000 ALTER TABLE `sysappsetting` DISABLE KEYS */;

/*!40000 ALTER TABLE `sysappsetting` ENABLE KEYS */;

-- 
-- Definition of sysbtnfun
-- 

DROP TABLE IF EXISTS `sysbtnfun`;
CREATE TABLE IF NOT EXISTS `sysbtnfun` (
  `Guid` varchar(50) NOT NULL,
  `MenuGuid` varchar(50) NOT NULL,
  `Name` varchar(20) NOT NULL COMMENT '功能名称',
  `FunType` varchar(20) NOT NULL COMMENT '功能标识名称',
  `Summary` varchar(500) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysbtnfun
-- 

/*!40000 ALTER TABLE `sysbtnfun` DISABLE KEYS */;
INSERT INTO `sysbtnfun`(`Guid`,`MenuGuid`,`Name`,`FunType`,`Summary`) VALUES
('6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','新增','Add',NULL),
('6d2c2da5-8bb8-4905-aaa9-cd297a46e4ed','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','导出','Export',NULL),
('8112ffb0-a84e-496c-93d7-95c02678754a','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','审核','Audit',NULL),
('931bd729-f160-4fe3-bb7c-7828a2da047a','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','修改','Edit',NULL),
('b1ab3437-6481-4a4e-b536-d7870a822de4','6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','导入','Import',NULL),
('b943200f-7c99-44b5-93d9-e4ea2505928a','5ce13ead-971b-4ed4-ad5f-acacccd82381','新增','Add',NULL);
/*!40000 ALTER TABLE `sysbtnfun` ENABLE KEYS */;

-- 
-- Definition of syscode
-- 

DROP TABLE IF EXISTS `syscode`;
CREATE TABLE IF NOT EXISTS `syscode` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标号Guid',
  `ParentGuid` varchar(50) NOT NULL COMMENT '字典类型标识',
  `CodeType` varchar(50) DEFAULT NULL COMMENT '字典值——类型',
  `Name` varchar(255) NOT NULL COMMENT '字典值——名称',
  `EnName` varchar(255) DEFAULT NULL COMMENT '字典值——英文名称',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '字典值——排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '字典值——状态',
  `Summary` varchar(1000) DEFAULT NULL COMMENT '字典值——描述',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '字典值——添加时间',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '字典值——修改时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table syscode
-- 

/*!40000 ALTER TABLE `syscode` DISABLE KEYS */;
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('0042c8e2-60dc-44b9-a637-d98d6e4c6d1a','7b664e3e-f58a-4e66-8c0f-be1458541d14','BLZ','BLZ百禄姿',NULL,317,1,'BLZ百禄姿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0086fdc0-1718-4dae-96de-4eb56f94be83','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZB','YZB伊姿百瑞',NULL,261,1,'YZB伊姿百瑞','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('00bc1e1a-1ed3-4b89-9b20-707715652148','48458681-48b0-490a-a840-0ffcbe49f5d4','P','皮草',NULL,155,1,'皮草','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('00e579a7-7766-401e-a9a3-24636e8e5895','7b664e3e-f58a-4e66-8c0f-be1458541d14','SDP','SDP皮衣三',NULL,169,1,'皮衣三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0409f43c-f999-4ef2-a5be-9405ba5ba7e9','7b664e3e-f58a-4e66-8c0f-be1458541d14','M46','M46曼紫46羽绒服三',NULL,182,1,'曼紫46羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('06256d8f-5206-4282-b206-b82d4ace5565','7b664e3e-f58a-4e66-8c0f-be1458541d14','HNS','HNS海宁双面尼',NULL,288,1,'HNS海宁双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('064aac2c-1bee-4cad-9f86-1486159d20be','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDZ','吊带杂DDZ',NULL,225,1,'吊带杂DDZ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('089b20ed-c712-4b10-839c-06b574c7f35d','7b664e3e-f58a-4e66-8c0f-be1458541d14','XHX','XHX雪鸿羽绒服',NULL,149,1,'雪鸿羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0915806a-5315-4bcb-bcda-402b272d9f27','7b664e3e-f58a-4e66-8c0f-be1458541d14','JJX','JJX晶晶薇琪',NULL,198,1,'JJX晶晶薇琪','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('09c453df-df66-4b8d-9324-d74db78a385b','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFL','YFL音非',NULL,278,1,'YFL音非','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0b9abd29-a571-40eb-95ba-afb591f3412c','7b664e3e-f58a-4e66-8c0f-be1458541d14','JLY','佳澜依尔JLY',NULL,210,1,'佳澜依尔JLY','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('0ccd9c0e-4df4-401e-b40a-531ca53ae849','7b664e3e-f58a-4e66-8c0f-be1458541d14','HSX','HSX花色',NULL,308,1,'HSX花色','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0ceee612-c739-49ff-a635-b4f67f3e1ffd','7b664e3e-f58a-4e66-8c0f-be1458541d14','CSL','CSL尘色',NULL,44,1,'尘色','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0e425b2d-9549-45d5-abbf-f95cbd52cb72','48458681-48b0-490a-a840-0ffcbe49f5d4','G','半裙',NULL,143,1,'半裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0e56f826-a388-4472-beb6-f8539b5e2883','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZGP','ZGP专供皮衣',NULL,189,1,'ZGP专供皮衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0e6a54df-6fb9-4e5a-b0a9-61de5eab1f7a','7b664e3e-f58a-4e66-8c0f-be1458541d14','JWN','JWN杰文妮',NULL,123,1,'杰文妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('0eb83125-cfb4-4062-9e23-1b1a1a90aee7','7b664e3e-f58a-4e66-8c0f-be1458541d14','SKL','SKL萨酷睿L',NULL,117,1,'SKL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1050bee3-8a02-441b-977a-537b4e060e6a','7b664e3e-f58a-4e66-8c0f-be1458541d14','DMX','麦之林DMX',NULL,239,1,'麦之林DMX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1083421d-2cc4-47c7-aad3-534e877a71cb','7b664e3e-f58a-4e66-8c0f-be1458541d14','DRD','DRD貂绒打底毛衫',NULL,258,1,'DRD貂绒打底毛衫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('10bdbd39-c147-4131-9903-b540b6e96121','7b664e3e-f58a-4e66-8c0f-be1458541d14','XMX','XMX绚萌',NULL,124,1,'绚萌','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('11c2d713-43b6-45b1-8338-50342ac23b62','7b664e3e-f58a-4e66-8c0f-be1458541d14','YNX','依诺YNX',NULL,224,1,'依诺YNX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('12738a20-2ae0-4a30-bee5-9922efaf964a','7b664e3e-f58a-4e66-8c0f-be1458541d14','SNY','SNY圣娜依儿',NULL,197,1,'SNY圣娜依儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('12f73730-5e4d-4ae8-bb10-e62f08749dd7','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZYX','ZYX甄妍',NULL,302,1,'ZYX甄妍','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('14dc428c-6d46-4c3d-ac59-b16c68b8e358','7b664e3e-f58a-4e66-8c0f-be1458541d14','BFN','BFN贝芙妮',NULL,75,1,'贝芙妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('15b25377-72ad-4087-83ad-ca28a8cedfdf','7b664e3e-f58a-4e66-8c0f-be1458541d14','HEL','HEL禾尔美',NULL,69,1,'禾尔美','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('15bc0cdb-a28b-4784-b7f7-8dbd76d6416a','7b664e3e-f58a-4e66-8c0f-be1458541d14','DSY','DSY迪丝雅',NULL,265,1,'DSY迪丝雅','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1913afc4-0c1e-4e0e-9f4f-19788e8aac35','7b664e3e-f58a-4e66-8c0f-be1458541d14','MEN','MEN沐恩真丝',NULL,217,1,'MEN沐恩真丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1970b666-7b15-481d-8702-1368e2380c3f','8cb134d5-979b-40e2-b453-aeee265f4ab2','H','四季装',NULL,12,1,'四季装-H','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1b87910f-d007-4d33-afbe-98ffda089589','7b664e3e-f58a-4e66-8c0f-be1458541d14','AYX','AYX艾燕春装',NULL,259,1,'AYX艾燕春装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1cffd0c2-f35c-42d8-bd77-265f5e282fe8','7b664e3e-f58a-4e66-8c0f-be1458541d14','PYX','PYX皮衣',NULL,236,1,'PYX皮衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1d164b70-f91b-41bb-b348-62cfd6ae75fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','HXX','HXX鸿秀双面妮',NULL,147,1,'鸿秀双面妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1da78dc4-908e-4cd8-aa0c-0cd5172365fc','7b664e3e-f58a-4e66-8c0f-be1458541d14','NKS','NKS男款双面尼',NULL,267,1,'NKS男款双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1dd9460a-ca52-4016-80f9-c56c0434654f','7b664e3e-f58a-4e66-8c0f-be1458541d14','IKL','IK',NULL,67,1,'IK','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('1efed59b-232f-4c80-867a-8ddc925848e3','7b664e3e-f58a-4e66-8c0f-be1458541d14','AWL','AWL艾薇儿awl',NULL,72,1,'艾薇儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2011a2a0-e5aa-4dd0-b0e7-8fb1a429cdcd','7b664e3e-f58a-4e66-8c0f-be1458541d14','HYY','HYY韩以羽绒服',NULL,315,1,'HYY韩以羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('2169b09a-8b58-4376-b1c4-3f56839d2304','7b664e3e-f58a-4e66-8c0f-be1458541d14','MSL','MSL曼丝L秀登',NULL,119,1,'MSL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('22c6f678-cd30-4248-92a2-e9baa6b6f117','48458681-48b0-490a-a840-0ffcbe49f5d4','O','羊绒大衣',NULL,141,1,'羊绒大衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('22f7bd06-33f6-45d6-8c21-6290e098ceb2','7b664e3e-f58a-4e66-8c0f-be1458541d14','XFZ','西服杂XFZ',NULL,228,1,'西服杂XFZ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2418bfca-9640-4382-a665-f6fd5ffcc017','7b664e3e-f58a-4e66-8c0f-be1458541d14','YBL','YBL伊百丽',NULL,142,1,'伊百丽','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2474293b-a3b0-41a3-baa4-1b2a75257b97','7b664e3e-f58a-4e66-8c0f-be1458541d14','137','137高圆圆同款三',NULL,190,1,'137高圆圆同款三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('24f9efe2-b80e-4ab1-833c-f02a61948b95','7b664e3e-f58a-4e66-8c0f-be1458541d14','YMR','YMR依目了然',NULL,132,1,'依目了然','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('25b47163-315e-4c63-abed-8412f537a516','7b664e3e-f58a-4e66-8c0f-be1458541d14','JXL','JXL杰西伍',NULL,70,1,'杰西伍','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2628b747-9986-4a5c-9a30-abf89b6a8742','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZNW','ZNW珍妮文',NULL,216,1,'ZNW珍妮文','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('273309c3-34d0-4cf4-8abe-7b83ddd22fe6','7b664e3e-f58a-4e66-8c0f-be1458541d14','FZL','FZL梵姿',NULL,55,1,'梵姿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('27fe6c17-2e1d-4f3e-8416-074e9033a53d','7b664e3e-f58a-4e66-8c0f-be1458541d14','XDZ','XDZ雪丹枝',NULL,275,1,'XDZ雪丹枝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('28245b5c-9205-4084-99aa-c34a5e695c9c','8cb134d5-979b-40e2-b453-aeee265f4ab2','A','春装',NULL,5,1,'春装-A','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('28d7c022-5749-40e2-8b3d-fed90cf2e02e','7b664e3e-f58a-4e66-8c0f-be1458541d14','M53','M53曼紫53 三',NULL,203,1,'M53曼紫53 三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('28ffb566-5247-41cf-bcd3-725d305b19be','7b664e3e-f58a-4e66-8c0f-be1458541d14','OSL','OSL欧时力',NULL,73,1,'欧时力','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2963ff16-26fe-4754-98f6-332d33b91dc5','7b664e3e-f58a-4e66-8c0f-be1458541d14','XYY','XYY夏映颗粒绒',NULL,316,1,'XYY夏映颗粒绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('29c41b94-4b92-4676-a1d2-2cf89abb8c90','48458681-48b0-490a-a840-0ffcbe49f5d4','T','夹克',NULL,298,1,'机车夹克','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2a3aa0f8-c5b3-4570-a8a2-e04e0f05d6ef','7b664e3e-f58a-4e66-8c0f-be1458541d14','MQX','MQX玛琪雅朵',NULL,87,1,'玛琪雅朵','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2a64211c-e0c0-40a5-a291-7e44dec9f681','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZM','YZM羽绒服三',NULL,178,1,'YZM羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2afd1417-9b6f-4842-afca-ae72805609a1','7b664e3e-f58a-4e66-8c0f-be1458541d14','BJS','BJS北京双面尼',NULL,243,1,'BJS北京双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2b2cf889-6245-4a21-9454-35c9e882ace5','7b664e3e-f58a-4e66-8c0f-be1458541d14','WYL','WYL唯依',NULL,93,1,'唯依','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2bdac142-528e-433f-9a7e-3aa02ccea7fe','7b664e3e-f58a-4e66-8c0f-be1458541d14','RFY','RFY人佛缘',NULL,164,1,'人佛缘','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2e6da0d5-3f35-4093-a442-d9d1b50c3a07','7b664e3e-f58a-4e66-8c0f-be1458541d14','AWR','AWR艾薇儿awr',NULL,125,1,'艾薇儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2ea653ab-1a18-4c84-a872-cf873ab11f5d','7b664e3e-f58a-4e66-8c0f-be1458541d14','BSK','BSK百思寇',NULL,251,1,'BSK百思寇','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2eaf5d0f-3010-46ef-96c3-0d79ea17f9b2','7b664e3e-f58a-4e66-8c0f-be1458541d14','MMW','MMW棉麻围巾',NULL,146,1,'棉麻围巾','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('2fa78c1b-40d0-4eb7-a1a8-b44de4e64589','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZK','YZK依庄可人',NULL,246,1,'YZK依庄可人','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('3035f671-abca-437b-96ff-990225cb6f28','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','2','2',NULL,135,1,'2','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('32ceaa41-dea6-4cff-bb95-11f2789870db','7b664e3e-f58a-4e66-8c0f-be1458541d14','HNX','HNX亨奴',NULL,226,1,'HNX亨奴','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('333ba8b5-d2ba-4901-97fd-6edd6e761736','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','0','0',NULL,172,1,'0','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('33ec687b-e773-4a8c-a644-43821e63e7ff','7b664e3e-f58a-4e66-8c0f-be1458541d14','YYY','YYY依艺缘',NULL,276,1,'YYY依艺缘','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3681512b-3fc7-458e-a9db-b15990b922fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','YSD','YSD依莎蒂妮',NULL,230,1,'YSD依莎蒂妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('38e51871-1eac-4201-89d4-06533623fb2e','48458681-48b0-490a-a840-0ffcbe49f5d4','H','50以上休闲',NULL,151,1,'50以上休闲','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('39d525ac-0df2-4c36-919c-301d2af15127','7b664e3e-f58a-4e66-8c0f-be1458541d14','HBN','HBN韩版连衣裙',NULL,289,1,'HBN韩版连衣裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3a0c2c57-5b29-42ea-900e-ba751772128e','7b664e3e-f58a-4e66-8c0f-be1458541d14','NYN','NYN诺喑呢',NULL,188,1,'NYN诺喑呢','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3c92c5b5-a74c-4483-b053-b7061d2ed8a5','7b664e3e-f58a-4e66-8c0f-be1458541d14','YDA','YDA雅蒂安娜',NULL,173,1,'YDA雅蒂安娜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3c95046e-b1b3-4a39-8006-9987056e84be','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFX','YFX佑芙妮',NULL,231,1,'YFX佑芙妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3d6bb971-13e5-4c50-a0b2-4d7f1c5643b4','7b664e3e-f58a-4e66-8c0f-be1458541d14','PXL','飘宣PXL',NULL,159,1,'飘宣PXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('3ddeae38-b336-4450-b5d9-5e9e90115e98','7b664e3e-f58a-4e66-8c0f-be1458541d14','QTM','QTM晴天明月',NULL,43,1,'晴天明月','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('400920ee-9e41-4a9a-8561-9abeff8f26a5','7b664e3e-f58a-4e66-8c0f-be1458541d14','JXW','JXW 杰西伍',NULL,200,1,'JXW 杰西伍','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('40f67656-6b9c-49ab-b8c3-377ebcde16c7','e86cf108-dc4d-4532-8cce-fdb041363902','I','XXXXL',NULL,40,1,'XXXXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('41bc4e6d-25a8-462a-b8b4-b6079ae2a57d','7b664e3e-f58a-4e66-8c0f-be1458541d14','NRS','NRS女人时报',NULL,85,1,'女人时报','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('429f8ceb-69a4-42ef-a3e4-c907389e97d3','7b664e3e-f58a-4e66-8c0f-be1458541d14','YRT','YRT羽绒服三',NULL,171,1,'YRT羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('433885fa-b07d-40f1-b175-f3475ab744fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','DZK','DZK 2017定制款',NULL,235,1,'DZK 2017定制款','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('43854363-22b0-4176-a0bc-66dd612f660d','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSN','SSN似水年华',NULL,53,1,'似水年华','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('443df199-e5f2-4509-894f-cf1ec1d57a9d','7b664e3e-f58a-4e66-8c0f-be1458541d14','LSQ','LSQ陆氏青云',NULL,56,1,'陆氏青云','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('450562e4-3293-4d71-8f82-ecb5889a74a4','1942d4fd-3203-42b1-a955-4a84a532b2a2','19','2019',NULL,319,1,'2019','2018-10-19 13:07:22','2018-10-19 13:07:22'),
('45744b0c-a792-4ea8-8490-1cf5ae7556fa','7b664e3e-f58a-4e66-8c0f-be1458541d14','FMY','FMY风媚衣坊 晨贝 靡曼',NULL,290,1,'FMY风媚衣坊 晨贝 靡曼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('45f44976-7856-4e34-b12c-8e7d568b1aaa','7b664e3e-f58a-4e66-8c0f-be1458541d14','WHZ','武汉杂2017WHZ',NULL,238,1,'武汉杂2017WHZ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('463ad549-3d7b-4a87-8b13-5878f84eb035','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZY','MZY曼紫羽绒服三',NULL,166,1,'曼紫羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('46ac0b25-660b-48cc-a3ec-ff8dd47864cd','7b664e3e-f58a-4e66-8c0f-be1458541d14','WBB','WBB 武汉彬彬 ',NULL,205,1,'WBB 武汉彬彬 ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('46c3f4a1-aea3-4df6-a8c4-1e65ae626335','e86cf108-dc4d-4532-8cce-fdb041363902','G','XXL',NULL,38,1,'XXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('477b792f-fee7-46af-9547-55f7684ce5cd','7b664e3e-f58a-4e66-8c0f-be1458541d14','SMR','双面尼三楼SMR',NULL,174,1,'双面尼三楼SMR','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('489ed30a-12c7-4c71-8871-1031681f9a5c','7b664e3e-f58a-4e66-8c0f-be1458541d14','M52','M52曼紫52羽绒服三',NULL,184,1,'曼紫52羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('495ccc11-f0a3-452c-b401-ecc91794350d','7b664e3e-f58a-4e66-8c0f-be1458541d14','SYS','SYS似水映',NULL,214,1,'SYS似水映','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4987917b-062e-4e21-8f4d-36df5cf7c76e','48458681-48b0-490a-a840-0ffcbe49f5d4','C','上衣',NULL,15,1,'上衣-C','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4a32b9c5-fae7-462c-aec4-93d5fdf6812d','7b664e3e-f58a-4e66-8c0f-be1458541d14','MQY','MQY玛琪雅朵',NULL,129,1,'玛琪雅朵','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4b48af91-e964-4d82-b18b-50589db0dbda','7b664e3e-f58a-4e66-8c0f-be1458541d14','YDM','YDM雅蒂安娜毛呢',NULL,209,1,'YDM雅蒂安娜毛呢','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4b9f096f-c053-41fb-9477-ce52375b7ef5','7b664e3e-f58a-4e66-8c0f-be1458541d14','MLM','MLM梅丽摩尔',NULL,291,1,'MLM梅丽摩尔','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4be990a5-d6a3-4e75-a43b-65731c44e3bf','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','1','1',NULL,134,1,'1','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4c249ebc-20ef-40ee-b00c-9c63aec3f74c','e86cf108-dc4d-4532-8cce-fdb041363902','F','XL',NULL,37,1,'XL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4c882546-9e1f-425c-a807-78e15ce4e526','48458681-48b0-490a-a840-0ffcbe49f5d4','S','围巾',NULL,145,1,'围巾','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('4dbb2a64-36a4-4f43-89f9-ed7da48aa193','7b664e3e-f58a-4e66-8c0f-be1458541d14','HJY','HJY惠景媛',NULL,204,1,'HJY惠景媛','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4e20068b-63be-4d2c-9e38-6cdbe02391b2','e86cf108-dc4d-4532-8cce-fdb041363902','B','XS',NULL,33,1,'XS','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4e6f026b-3eb0-47ad-9184-854c5199e5d8','48458681-48b0-490a-a840-0ffcbe49f5d4','U','马甲',NULL,299,1,'马甲','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4f075839-9804-4998-8659-a919391e561f','7b664e3e-f58a-4e66-8c0f-be1458541d14','YSY','YSY羽绒服三',NULL,170,1,'YSY羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('4f4d3428-fd3c-48c2-85fa-57db17a7fe3f','8cb134d5-979b-40e2-b453-aeee265f4ab2','D','冬装',NULL,8,1,'冬装-D','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('500fb1ae-1861-4a5a-9f77-ee6b69bd5375','48458681-48b0-490a-a840-0ffcbe49f5d4','V','内衣',NULL,312,1,'内衣-V','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('50523d77-235a-4c30-b033-be84259b2ff2','7b664e3e-f58a-4e66-8c0f-be1458541d14','YML','YML依美瑞',NULL,63,1,'YML依美瑞','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5057e97d-cdc2-413b-8e50-a3fddfb5b822','7b664e3e-f58a-4e66-8c0f-be1458541d14','BFL','BFL贝芙妮',NULL,212,1,'BFL贝芙妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('51957ec1-e8b2-4f9e-8f27-39a4cf261266','7b664e3e-f58a-4e66-8c0f-be1458541d14','HTF','HTF红头发风衣',NULL,222,1,'HTF红头发风衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('55ba863c-664c-40df-8199-7d06992274ac','7b664e3e-f58a-4e66-8c0f-be1458541d14','BZM','BZM宝姿毛衣',NULL,140,1,'宝姿毛衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('570b6e38-0333-4bd5-b857-89f6ba8da34a','7b664e3e-f58a-4e66-8c0f-be1458541d14','MNL','MNL沐恩冬装',NULL,122,1,'MNL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5aee6bfc-dae4-4616-b129-cb63528570d2','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','A','A',NULL,28,1,'A','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('5b39701e-285c-4bd9-b080-3eeaf70190d5','7b664e3e-f58a-4e66-8c0f-be1458541d14','SKR','SKR萨酷睿',NULL,57,1,'萨酷睿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5b3ea153-d6e6-42bd-a82e-8497af8f7f0e','48458681-48b0-490a-a840-0ffcbe49f5d4','K','年轻处理',NULL,154,1,'年轻处理','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5b406c33-4552-4d03-809f-20a147523893','8cb134d5-979b-40e2-b453-aeee265f4ab2','F','秋冬装',NULL,10,1,'秋冬装-F','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5d24cddd-de94-4602-921e-c8e4b14e8172','7b664e3e-f58a-4e66-8c0f-be1458541d14','LZL','LZL朗姿丽',NULL,286,1,'LZL朗姿丽','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('5e5b962f-362f-4a94-9846-81e6db713be7','7b664e3e-f58a-4e66-8c0f-be1458541d14','TZD','童装冬棉服TZD',NULL,207,1,'童装冬棉服TZD','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('61fa64da-07f7-4067-9ded-cd8d106e7ba6','7b664e3e-f58a-4e66-8c0f-be1458541d14','SRN','SRN赛睿娜',NULL,59,1,'赛睿娜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('62413bf5-d888-4594-8c50-225d3554f085','7b664e3e-f58a-4e66-8c0f-be1458541d14','YZD','YZD艺之蝶',NULL,54,1,'艺之蝶','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6300ace3-6443-4d78-bbc3-cdea2c3f57a8','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZTG','ZTG紫藤谷',NULL,273,1,'ZTG紫藤谷','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('65590d1d-1c7c-44ae-a328-481a6b5dc2cc','1942d4fd-3203-42b1-a955-4a84a532b2a2','20','2020',NULL,320,1,'2020','2018-10-19 13:07:31','2018-10-19 13:07:31'),
('65d6afad-2c99-4bf7-a4f8-64824562ba97','7b664e3e-f58a-4e66-8c0f-be1458541d14','SYX','SYX水映',NULL,181,1,'水映','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('69471576-93bb-473d-a301-204193219f52','7b664e3e-f58a-4e66-8c0f-be1458541d14','WYX','WYX唯依',NULL,202,1,'WYX唯依','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6948a911-3034-45ca-8a17-7797e1a63641','7b664e3e-f58a-4e66-8c0f-be1458541d14','SMM','SMM双面呢三',NULL,168,1,'双面呢三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('6befc43f-e491-4a84-bc31-756c008707f3','e86cf108-dc4d-4532-8cce-fdb041363902','H','XXXL',NULL,39,1,'XXXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6c28ecc7-f193-498c-b314-0315b321de95','7b664e3e-f58a-4e66-8c0f-be1458541d14','FRL','FRL妃萱',NULL,64,1,'妃萱','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6c67c5d6-fc73-4dec-8754-b5cf242de955','48458681-48b0-490a-a840-0ffcbe49f5d4','A','T恤',NULL,13,1,'T恤-A','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6c9da850-84fb-4f3a-a0a4-a0822b7cef7b','7b664e3e-f58a-4e66-8c0f-be1458541d14','OBY','OBY欧版羽绒服',NULL,133,1,'欧版羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6cf31787-0a53-419d-be61-b32847b6df79','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZGY','ZGY专供羽绒服',NULL,187,1,'ZGY专供羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6d4c33ba-05d9-4238-a9b8-c82fc290d393','7b664e3e-f58a-4e66-8c0f-be1458541d14','AML','AML艾米拉羽绒服三',NULL,167,1,'艾米拉羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6e838a10-94c3-4089-8766-3b4bfe85c24d','7b664e3e-f58a-4e66-8c0f-be1458541d14','BYM','BYM倍艺蒙',NULL,272,1,'BYM倍艺蒙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('6ed694c7-6efa-47ff-8425-de995b1953fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','TPN','TPN太平鸟',NULL,271,1,'TPN太平鸟','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('709c8230-b39f-442f-872f-1f38bae53e4e','7b664e3e-f58a-4e66-8c0f-be1458541d14','DRX','DRX貂绒',NULL,128,1,'貂绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('711c4b57-8336-4f31-b421-80c83e996dbc','e86cf108-dc4d-4532-8cce-fdb041363902','D','M',NULL,35,1,'M','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('717c1829-0ec6-46b2-bfa9-40e0aef20871','7b664e3e-f58a-4e66-8c0f-be1458541d14','M61','M61曼紫M61羽绒服三',NULL,185,1,'曼紫M61羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('71bee291-1391-4cb3-b533-f8f1e78e485e','7b664e3e-f58a-4e66-8c0f-be1458541d14','SJL','世纪蓝天SJL',NULL,211,1,'世纪蓝天SJL','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('71f3252a-638d-43a7-ad79-f9eee672f019','e86cf108-dc4d-4532-8cce-fdb041363902','J','均码',NULL,41,1,'均码','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('75c3b9b1-beb5-42e1-aa10-26a1962a76fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','AXX','AXX暗香',NULL,282,1,'AXX暗香','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('76b32e1f-ddd7-43b0-a589-0298bfcfcbe5','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZL','MZL曼紫mzl',NULL,71,1,'曼紫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('775401f9-f43f-4bdf-a253-bb2717ddf4b2','7b664e3e-f58a-4e66-8c0f-be1458541d14','FXX','FXX妃萱',NULL,46,1,'妃萱','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7794ac40-1385-43d2-be28-8db183207a67','8cb134d5-979b-40e2-b453-aeee265f4ab2','G','春秋装',NULL,11,1,'春秋装-G','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('77b25de4-731c-4f16-a5f0-a463f1cc2ad7','7b664e3e-f58a-4e66-8c0f-be1458541d14','LZW','LZW靓姿屋',NULL,232,1,'LZW靓姿屋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7835dbc9-9a6f-4ea4-8326-1889779cf28e','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDL','DDL迪迪欧',NULL,68,1,'迪迪欧','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('784dc39b-90bd-431c-895f-e9de04564181','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','4','4',NULL,137,1,'4','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('79f45c74-7492-4816-869c-66b59bb5f0ba','7b664e3e-f58a-4e66-8c0f-be1458541d14','YMM','YMM怡梦情缘棉服',NULL,257,1,'YMM怡梦情缘棉服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7b03a3f5-cd03-400d-876d-70b367f6a24f','7b664e3e-f58a-4e66-8c0f-be1458541d14','YKD','YKD羽绒服三',NULL,186,1,'YKD羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7b79b747-56b1-4d81-842d-99715e94b12a','48458681-48b0-490a-a840-0ffcbe49f5d4','E','风衣',NULL,17,1,'风衣-E','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7bcf0dbe-6f54-4231-967e-1a0513f85f23','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDK','DDK打底裤 ',NULL,191,1,'DDK打底裤 ','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('7d209f2e-e7b8-4a91-819e-a955c593ec85','7088d9b9-6692-4fc7-a83c-da580f1407c3','1006','包',NULL,78,1,NULL,'2018-06-30 19:29:11','2018-06-30 19:29:11'),
('7e3f1f8a-7b58-4de2-9326-ddd08f2196f1','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFQ','依芳秋水双面尼YFQ',NULL,206,1,'依芳秋水双面尼YFQ','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('7f2375e8-c387-4c8d-9934-b6833edaf4a0','7b664e3e-f58a-4e66-8c0f-be1458541d14','SJS','SJS世纪蓝天双面尼',NULL,245,1,'SJS世纪蓝天双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('804ee952-3f85-4e4a-aa8e-1ba37b5ac486','7b664e3e-f58a-4e66-8c0f-be1458541d14','TKY','TKY挑款羽绒服',NULL,255,1,'TKY挑款羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('80994916-bf65-49ff-8529-9e8796bd46dd','e86cf108-dc4d-4532-8cce-fdb041363902','A','XXL',NULL,32,1,'XXL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('80ce475a-781e-4e58-a506-a77dc9648fca','7b664e3e-f58a-4e66-8c0f-be1458541d14','YHM','YHM羽绒服三',NULL,179,1,'YHM羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('81d70b21-ee20-48eb-b254-6b456517833b','7b664e3e-f58a-4e66-8c0f-be1458541d14','NRL','NRL女人屋',NULL,103,1,'女人屋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8383ab1c-18ee-4039-8e2c-33ce68ee1615','7b664e3e-f58a-4e66-8c0f-be1458541d14','PXM','PXM飘轩',NULL,74,1,'飘轩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('867a05c8-ebf8-460a-9de9-9d2e1e4aae73','48458681-48b0-490a-a840-0ffcbe49f5d4','I','50以下休闲',NULL,152,1,'50以下休闲','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('86a900ad-98c8-40d6-8a00-c7fd2cf2f568','7b664e3e-f58a-4e66-8c0f-be1458541d14','DRN','DRN貂绒打底',NULL,144,1,'貂绒打底','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('88b76b27-e764-4421-965e-822b8640e155','7b664e3e-f58a-4e66-8c0f-be1458541d14','TRY','条绒羽绒服',NULL,307,1,'条绒羽绒服TRY','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8913777b-bfc5-41db-8c38-91c8b4956fe5','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','B','B',NULL,29,1,'B','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('8994730c-0ae1-45f6-a1a8-38b609a46252','7b664e3e-f58a-4e66-8c0f-be1458541d14','BJY','BJY北京羽绒服',NULL,256,1,'BJY北京羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8bb35887-5ff6-4b85-b700-ff49e830e3d1','7b664e3e-f58a-4e66-8c0f-be1458541d14','SJZ','SJZ世纪蓝天真丝',NULL,215,1,'SJZ世纪蓝天真丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8dda6501-f1a4-4ede-99df-98731ac37648','7b664e3e-f58a-4e66-8c0f-be1458541d14','WMZ','WMZ外贸真丝',NULL,285,1,'WMZ外贸真丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8f67ee08-799f-4369-ad56-ee25066da0cd','7b664e3e-f58a-4e66-8c0f-be1458541d14','XCX','XCX炫彩',NULL,283,1,'XCX炫彩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8fccca92-1f94-4bc3-b74a-40ef7f8f109c','7b664e3e-f58a-4e66-8c0f-be1458541d14','MXX','MXX茉希',NULL,277,1,'MXX茉希','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('8ff8c4e9-ad53-438d-84fb-11d0ed3207fd','7b664e3e-f58a-4e66-8c0f-be1458541d14','YDW','依丁物语YDW',NULL,218,1,'依丁物语YDW','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('93321313-2711-40e6-b7c2-d469bb54eec6','7b664e3e-f58a-4e66-8c0f-be1458541d14','KDM','KDM凯蒂梅露羊剪绒',NULL,294,1,'KDM凯蒂梅露羊剪绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('93a26353-fd83-4c17-832f-7724a35a5490','7b664e3e-f58a-4e66-8c0f-be1458541d14','STS','STS尚缇诗',NULL,244,1,'STS尚缇诗','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('93e7d972-b6c8-403e-86ad-2fec6ae63de4','7b664e3e-f58a-4e66-8c0f-be1458541d14','TKX','TKX唐卡',NULL,281,1,'TKX唐卡','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('94a7cbb7-b07e-4a77-b5c8-0674993548a3','7b664e3e-f58a-4e66-8c0f-be1458541d14','PKF','PKF派克服',NULL,240,1,'PKF派克服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('95a7e559-2714-4b06-b776-18dd3860841e','7b664e3e-f58a-4e66-8c0f-be1458541d14','5ZY','5ZY5字羽绒服三',NULL,192,1,'5ZY5字羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('966afc63-956e-4900-86a0-61c811ec1e30','7b664e3e-f58a-4e66-8c0f-be1458541d14','YBX','YBX约布',NULL,295,1,'YBX约布','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('96e356fa-5b68-49d0-9fcd-aba2f0222303','7b664e3e-f58a-4e66-8c0f-be1458541d14','PJN','PJN帕佳妮',NULL,304,1,'PJN帕佳妮','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('96e43f17-ef09-4e55-a61e-34292b2753f3','7b664e3e-f58a-4e66-8c0f-be1458541d14','MXM','MXM莫西莫',NULL,66,1,'莫西莫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('98d45a4c-3035-4583-8040-5c1821c7ca97','7b664e3e-f58a-4e66-8c0f-be1458541d14','BHX','BHX鋇禾春装',NULL,266,1,'BHX鋇禾春装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('98dda8cf-0193-4431-9a8e-dea7524dfca4','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZSL','ZSL真丝连衣裙',NULL,227,1,'ZSL真丝连衣裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('99bb643f-99d1-4d58-9b0e-887a774877b8','7b664e3e-f58a-4e66-8c0f-be1458541d14','XRX','XRX熙然',NULL,241,1,'XRX熙然','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9a838347-2e8d-4acb-ac6d-2615ce15fc0c','1942d4fd-3203-42b1-a955-4a84a532b2a2','18','2018',NULL,318,1,'2018','2018-10-19 13:07:14','2018-10-19 13:07:14'),
('9b8d73b7-e51c-469c-a035-b9c634f44c24','7b664e3e-f58a-4e66-8c0f-be1458541d14','NSQ','NSQ诺诗琪',NULL,220,1,'NSQ诺诗琪','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9b91b963-d46a-463b-814b-c47805146050','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','D',' D',NULL,31,1,'D','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9ba942ed-37ac-457e-9cad-5ae962762242','7b664e3e-f58a-4e66-8c0f-be1458541d14','MXY','MXY魔犀羽绒服',NULL,165,1,'魔犀羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9cdfcb80-3647-4b59-b4ec-9e88bc1dbf36','7b664e3e-f58a-4e66-8c0f-be1458541d14','SDX','SDX天格双面呢',NULL,120,1,'SDX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9d414f7d-1213-4409-af54-1efe4a14a87a','48458681-48b0-490a-a840-0ffcbe49f5d4','L','棉服',NULL,150,1,'棉服','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('9dd88a48-9522-46f6-b676-af1ff37afa36','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','6','6',NULL,24,1,'6','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9e283ca6-df02-485a-840f-4b37918ebaba','7b664e3e-f58a-4e66-8c0f-be1458541d14','NIK','NIK耐克',NULL,2,1,'耐克-NIK','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9e420c91-320e-44bc-ab99-c9cc95b8c69f','7b664e3e-f58a-4e66-8c0f-be1458541d14','TRG','TRG唐人阁',NULL,76,1,'唐人阁','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9e4d15d3-c0d2-456d-8da3-a873d89181d0','7b664e3e-f58a-4e66-8c0f-be1458541d14','WYS','WYS连衣裙',NULL,314,1,'WYS连衣裙','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9f2edffa-aea8-4014-bd9e-546a3094c54b','7b664e3e-f58a-4e66-8c0f-be1458541d14','FMX','FMX纷漫',NULL,42,1,'纷漫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('9f784c92-22e1-434e-8792-43d1829d9009','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','5','5',NULL,23,1,'5','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a0664601-d919-4dec-a1ea-28e90edcd0c2','7b664e3e-f58a-4e66-8c0f-be1458541d14','YFN','YFN妍妃霓',NULL,126,1,'妍妃霓','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a092d9a4-da9f-44e5-8850-6b3b9bdd5a7d','7b664e3e-f58a-4e66-8c0f-be1458541d14','YGX','YGX雅阁',NULL,121,1,'YGX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a1034ce4-d115-4496-b161-64eb5c69d45e','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','9','9',NULL,27,1,'9','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a1332c02-4c8f-4bbe-88de-52e5f9a09166','7b664e3e-f58a-4e66-8c0f-be1458541d14','YBB','YBB一布百布',NULL,250,1,'YBB一布百布','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a143fe9d-480c-4486-b20d-a572ba40e510','7b664e3e-f58a-4e66-8c0f-be1458541d14','ADI','ADI阿迪',NULL,1,1,'阿迪-ADI','2018-10-19 13:06:29','2018-10-19 13:06:29'),
('a15d5b75-7f13-4f23-b2a5-20cfe7941f44','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZSW','ZSW真丝围巾',NULL,305,1,'ZSW真丝围巾','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('a1aa0583-bae7-4e85-b71f-6b3f871fa118','8cb134d5-979b-40e2-b453-aeee265f4ab2','B','夏装',NULL,6,1,'夏装-B','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a29395f1-cbb6-4d3f-bd86-b83c4db15c69','7b664e3e-f58a-4e66-8c0f-be1458541d14','LGS','LGS爱女孩',NULL,116,1,'爱女孩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a30fb363-6976-4232-b9c6-b154170e3dc0','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZGM','ZGM专供棉服',NULL,201,1,'ZGM专供棉服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a36dfd28-f130-4d84-83a7-e9d15d5682a3','7b664e3e-f58a-4e66-8c0f-be1458541d14','YRW','YRW羊绒围巾',NULL,138,1,'羊绒围巾','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a50b6cb4-560f-41af-906a-ef5f43b19186','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSL','SSL桑索',NULL,51,1,'桑索','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a5d04a95-7fd7-4e6a-8962-4f8889c3d431','7b664e3e-f58a-4e66-8c0f-be1458541d14','YXX','YXX羽希',NULL,58,1,'羽希','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a8d9bd2e-8a0f-4235-8d93-4b5909c945b4','7b664e3e-f58a-4e66-8c0f-be1458541d14','MDX','MDX漫多',NULL,223,1,'MDX漫多','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('a97b8fa7-c260-4df6-a1bb-d5314d398baa','7b664e3e-f58a-4e66-8c0f-be1458541d14','JNX','JNX邂逅江南',NULL,310,1,'JNX邂逅江南','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ab170ed2-56c5-467e-8630-02db6910ab44','7b664e3e-f58a-4e66-8c0f-be1458541d14','JJW','JJW晶晶旗威',NULL,161,1,'晶晶旗威','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ad5cff15-ab87-4ad6-9ea1-89503d2f1832','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','8','8',NULL,26,1,'8','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ad80fbf8-7db6-40e7-8c88-32a1abb1eee4','7b664e3e-f58a-4e66-8c0f-be1458541d14','FZX','梵姿FZX',NULL,79,1,'梵姿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ade7d3e9-0026-43e7-83fe-96c1be30122c','7b664e3e-f58a-4e66-8c0f-be1458541d14','CRZ','CR''Z',NULL,52,1,'CR''Z','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('af8c9140-12ec-44e2-9691-c08adbb4b8de','48458681-48b0-490a-a840-0ffcbe49f5d4','W','卫衣',NULL,297,1,'卫衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b1b5f177-0fff-4574-b90f-2ae0fdf3d8bd','7b664e3e-f58a-4e66-8c0f-be1458541d14','YJR','YJR羊剪绒',NULL,237,1,'YJR羊剪绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b2229e38-a32d-4373-831d-0c3c438365ec','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZX','MZX曼紫秋mzx',NULL,130,1,'曼紫秋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b289b54a-9362-4db5-941f-886b9519e5ed','48458681-48b0-490a-a840-0ffcbe49f5d4','F','裤子',NULL,18,1,'裤子-F','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b2f81d20-4274-41ee-bbee-0adf5994b401','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDM','打底毛衫DDM',NULL,162,1,'打底毛衫DDM','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b34e3d29-f2e2-4fe4-81cd-a5b4347d44d8','8cb134d5-979b-40e2-b453-aeee265f4ab2','C','秋装',NULL,7,1,'秋装-C','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b444ae4e-244d-401a-b3d3-37f1c71c4ee6','7b664e3e-f58a-4e66-8c0f-be1458541d14','YMX','YMX翼美',NULL,97,1,'翼美','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b614c223-d776-4045-a729-dc18294b190e','7088d9b9-6692-4fc7-a83c-da580f1407c3','1004','公斤',NULL,76,1,NULL,'2018-06-30 19:28:56','2018-06-30 19:28:56'),
('b61e977f-6162-4443-97c3-0d0473aa281b','7b664e3e-f58a-4e66-8c0f-be1458541d14','JZK','JZK羽绒服三',NULL,180,1,'JZK羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('b87554b5-541d-48ec-9978-49f422fd7ac0','7b664e3e-f58a-4e66-8c0f-be1458541d14','LYR','LYR蓝雅绒',NULL,160,1,'蓝雅绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ba90d75a-5d8a-4dac-9458-f46d12511d57','7088d9b9-6692-4fc7-a83c-da580f1407c3','1003','条',NULL,75,1,NULL,'2018-06-30 19:28:50','2018-06-30 19:28:50'),
('baab9800-b7db-4ea3-8b89-1f4ae38ea822','7b664e3e-f58a-4e66-8c0f-be1458541d14','GFN','GFN哥芙妮羽绒服',NULL,177,1,'哥芙妮羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('bc2d7429-eb0b-4f24-aa70-f40d7589aa81','e86cf108-dc4d-4532-8cce-fdb041363902','C','S',NULL,34,1,'S','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('bc6ba969-a659-4e1c-8dee-d29608324b33','7b664e3e-f58a-4e66-8c0f-be1458541d14','WHY','WHY王后羽绒服',NULL,176,1,'王后羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('be8c4b17-e786-4441-ab9e-7f1dbeb4a776','7b664e3e-f58a-4e66-8c0f-be1458541d14','HHS','HHS黄鹤双面尼',NULL,248,1,'HHS黄鹤双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c0d871ba-0670-466c-8586-e02ce2c65c02','7b664e3e-f58a-4e66-8c0f-be1458541d14','DDO','DDO迪迪欧秋',NULL,92,1,'迪迪欧秋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c0e51b55-671b-480a-b864-69c23a81fc0b','7b664e3e-f58a-4e66-8c0f-be1458541d14','DBQ','DBQ迪碧茜毛衣',NULL,303,1,'DBQ迪碧茜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c1d2b3f8-032a-4410-bf05-249053a825e3','7b664e3e-f58a-4e66-8c0f-be1458541d14','XKL','XKL茜可可',NULL,50,1,'茜可可','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c203be2e-2873-4d0a-9faf-68e894e1e2f3','7b664e3e-f58a-4e66-8c0f-be1458541d14','HZZ','HZZ杭州杂',NULL,247,1,'HZZ杭州杂','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c2abdd8e-2eae-47eb-a694-4714da6137fe','7b664e3e-f58a-4e66-8c0f-be1458541d14','BBL','BBL播',NULL,65,1,'播','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c2e89f09-04f6-4645-95fc-c480bd551714','7b664e3e-f58a-4e66-8c0f-be1458541d14','HHJ','HHJ红火家人',NULL,234,1,'HHJ红火家人','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c2ff2b85-e617-4e71-95c0-3b9638b606d6','7b664e3e-f58a-4e66-8c0f-be1458541d14','JOR','JOR乔丹',NULL,4,1,'乔丹-JOR','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c36abb33-00ac-42c6-8cdc-45fca59bb9f1','7b664e3e-f58a-4e66-8c0f-be1458541d14','QCX','千禅QCX',NULL,208,1,'千禅女装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('c7f0ea3d-42c1-4e7f-8102-402ac55c0b01','7088d9b9-6692-4fc7-a83c-da580f1407c3','1007','套',NULL,79,1,NULL,'2018-06-30 19:29:26','2018-06-30 19:29:26');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('c8ee7d8b-1f25-423d-b9ec-d557805d6b67','48458681-48b0-490a-a840-0ffcbe49f5d4','R','衬衣',NULL,157,1,'衬衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ca73d117-461c-4a3f-aacb-36a8570abd48','7b664e3e-f58a-4e66-8c0f-be1458541d14','SRL','SRL赛睿娜L',NULL,118,1,'SRL','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('caf2b551-3599-4b86-be78-f540d3ef5dcc','7b664e3e-f58a-4e66-8c0f-be1458541d14','CZM','CZM粗针毛衫',NULL,263,1,'CZM粗针毛衫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cb500968-e8ab-4494-bc64-76b338fff965','7b664e3e-f58a-4e66-8c0f-be1458541d14','QLS','QLS琦丽莎',NULL,213,1,'QLS琦丽莎','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cc708b6c-a96d-4c5d-9241-0bbb6af11f26','7b664e3e-f58a-4e66-8c0f-be1458541d14','PSB','PSB帕斯宝',NULL,279,1,'PSB帕斯宝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ccf9a0cc-aba8-4e05-81ce-9585686b6643','7b664e3e-f58a-4e66-8c0f-be1458541d14','FRX','FRX芙瑞宣',NULL,62,1,'芙瑞宣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ce01577b-f8f7-4619-994b-968235c42ad2','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZRS','ZRS绽然双面尼',NULL,287,1,'ZRS绽然','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cecc6dc2-76ed-4737-99b9-870c42ca03e6','7b664e3e-f58a-4e66-8c0f-be1458541d14','PYY','PYY球球款三',NULL,194,1,'PYY球球款三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cf054a61-9bdc-4508-9567-585060f067e5','7b664e3e-f58a-4e66-8c0f-be1458541d14','IKX','IKX',NULL,158,1,'IKX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('cf992e05-e8c5-451a-b95b-8b4735903082','7b664e3e-f58a-4e66-8c0f-be1458541d14','MQL','MQL玛琪雅朵',NULL,60,1,'玛琪雅朵','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d16848b9-60d2-48db-a746-79c082aa6578','7b664e3e-f58a-4e66-8c0f-be1458541d14','M03','M03曼紫03羽绒服三',NULL,182,1,'曼紫03羽绒服三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d16d0a0d-c884-412c-b32d-c1912b0c7f41','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZR','MZR曼紫三',NULL,195,1,'MZR曼紫三','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('d2ef5490-f39b-4652-b9f2-a4ba87369750','7b664e3e-f58a-4e66-8c0f-be1458541d14','MEX','MEX沐恩',NULL,48,1,'沐恩','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d34c7cb9-68d6-4875-bc0b-75a2b93000de','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','3','3',NULL,136,1,'3','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d36d28d3-2054-44cb-9817-a1a43ac4d5c6','7b664e3e-f58a-4e66-8c0f-be1458541d14','HEM','HEM和尔美',NULL,196,1,'HEM和尔美','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d4c6125c-558d-406e-a4dc-3cf8d72f92b8','7b664e3e-f58a-4e66-8c0f-be1458541d14','MEL','MEL沐恩L',NULL,163,1,'沐恩L','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d56ddc33-ebb7-44ad-a7a2-cc0efe4589ff','7b664e3e-f58a-4e66-8c0f-be1458541d14','SNL','SNL圣娜依儿',NULL,100,1,'圣娜依儿','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d6c64f0a-dc6d-4cab-b51f-b7db35a3eb02','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZLX','ZLX庄丽欣',NULL,45,1,'庄丽欣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d7acb442-d4dd-4745-af1e-6fd6ec69fae6','7b664e3e-f58a-4e66-8c0f-be1458541d14','SWX','述忘SWX',NULL,219,1,'述忘SWX','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d7c945e2-3917-44db-999c-e7a51b9ddd99','7b664e3e-f58a-4e66-8c0f-be1458541d14','QSX','QSX强缩绒',NULL,309,1,'QSX强缩绒','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d7e140c1-0a74-4829-8e97-c0a378172f2c','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZHG','ZHG子恒国际双面尼',NULL,293,1,'ZHG子恒国际双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('d844e30b-5eb6-4800-a449-061dd4c56af5','7b664e3e-f58a-4e66-8c0f-be1458541d14','MSX','MSX曼丝秀登',NULL,90,1,'曼丝秀登','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dab7d485-278d-460f-a722-ee6311d51d23','7088d9b9-6692-4fc7-a83c-da580f1407c3','1001','件',NULL,73,1,NULL,'2018-06-30 19:28:37','2018-06-30 19:28:37'),
('daea1534-8f45-4dd3-8344-42c7cd41a636','7b664e3e-f58a-4e66-8c0f-be1458541d14','BBX','BBX播',NULL,199,1,'BBX播','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('db09e1fc-40b6-4e06-b754-5f217b6b5262','7b664e3e-f58a-4e66-8c0f-be1458541d14','ARB','ARB阿尔巴卡双面尼',NULL,264,1,'ARB阿尔巴卡双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dbbf8787-343c-4962-bdf2-e5e36944f51f','7088d9b9-6692-4fc7-a83c-da580f1407c3','1005','箱',NULL,77,1,NULL,'2018-06-30 19:29:06','2018-06-30 19:29:06'),
('dbda1090-2471-47d8-a32d-765f9eb08910','7b664e3e-f58a-4e66-8c0f-be1458541d14','KWN','KWN柯文娜',NULL,242,1,'KWN柯文娜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dcb6bd5a-4057-444b-8edd-e2355aa36954','48458681-48b0-490a-a840-0ffcbe49f5d4','N','毛衫',NULL,139,1,'','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dd0abc85-f78f-421c-843b-a6959a08c105','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','C','C',NULL,30,1,'C','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('dd272d8b-9a09-4672-a4dd-5f740b17a363','7b664e3e-f58a-4e66-8c0f-be1458541d14','APM','APM艾普玛',NULL,296,1,'APM艾普玛','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('deb3a772-884d-4fd9-a555-1b8b7b05d124','7b664e3e-f58a-4e66-8c0f-be1458541d14','XKM','XKM2017新款毛衣',NULL,252,1,'XKM2017新款毛衣','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('df908b0c-c2c4-41ed-bdc5-2fe97889b344','7b664e3e-f58a-4e66-8c0f-be1458541d14','FAR','FAR F.艾人',NULL,270,1,' F.艾人','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e00ae7bb-c23d-4326-b050-4986e5138c5a','7b664e3e-f58a-4e66-8c0f-be1458541d14','NRW','NRW女人屋',NULL,61,1,'女人屋','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e064b9a9-924d-488b-8e5d-00b751d2821b','7b664e3e-f58a-4e66-8c0f-be1458541d14','XPP','XPP溆牌',NULL,269,1,'溆牌','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e1ee070b-1932-43e3-a7c4-20f05d672ba1','2e0393f9-e6d6-4c15-98cf-96072f0d3d70','7','7',NULL,25,1,'7','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e2d9b374-1c31-4260-bafa-717c4bda88cb','7b664e3e-f58a-4e66-8c0f-be1458541d14','ZTL','ZTL紫藤罗',NULL,49,1,'紫藤罗','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('e48f49c9-626c-4bcb-be1f-70a9e17765a1','7b664e3e-f58a-4e66-8c0f-be1458541d14','HZS','HZS杭州双面尼',NULL,254,1,'HZS杭州双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e5d71e81-9e0c-4d58-93ab-cdad615f5edf','7b664e3e-f58a-4e66-8c0f-be1458541d14','AGW','AGW昂购物',NULL,311,1,'AGW昂购物','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e6b2a41e-37d1-4104-a79f-ef3927bf73bd','48458681-48b0-490a-a840-0ffcbe49f5d4','M','羽绒服',NULL,148,1,'羽绒服','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e6f0fce3-37da-4731-a08d-c0e07e086938','8cb134d5-979b-40e2-b453-aeee265f4ab2','E','春夏装',NULL,9,1,'春夏装-E','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e7c6b1ea-645f-406a-a8ac-9706768472cc','7b664e3e-f58a-4e66-8c0f-be1458541d14','YXL','YXL依香丽影',NULL,47,1,'依香丽影','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e98ed7b9-c959-4326-a1a7-0fe7ad6179c0','48458681-48b0-490a-a840-0ffcbe49f5d4','J','老年处理',NULL,153,1,'老年处理','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('e9d62a8b-09bd-435a-b3e2-a9822a2bede0','7b664e3e-f58a-4e66-8c0f-be1458541d14','JJL','JJL晶晶旗威',NULL,99,1,'晶晶旗威','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ea7d64e7-a57a-4df9-924d-0a2d73ff9b08','7b664e3e-f58a-4e66-8c0f-be1458541d14','BRM','BRM芭而慕',NULL,292,1,'BRM芭而慕','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('eb23a73e-cc4d-4caf-83cd-ca74bb226b85','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSX','SSX述色',NULL,306,1,'SSX述色','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ed24f032-166f-4ffb-88d5-a23edcbb23f5','7b664e3e-f58a-4e66-8c0f-be1458541d14','AAA','AAA处理品',NULL,233,1,'AAA处理品','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ee673e40-ec67-4653-92fc-3fd33c220514','7b664e3e-f58a-4e66-8c0f-be1458541d14','LIN','LIN李宁',NULL,3,1,'李宁-LIN','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('eeec08d1-c9a5-491c-ba1b-01335c7e3b95','48458681-48b0-490a-a840-0ffcbe49f5d4','B','连衣裙',NULL,14,1,'连衣裙-B','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('ef5df705-7ce0-4d08-ade0-42d6c9af48a9','7088d9b9-6692-4fc7-a83c-da580f1407c3','1002','个',NULL,74,1,NULL,'2018-06-30 19:28:42','2018-06-30 19:28:42'),
('f0a9b98f-534f-4edb-9f88-23c60a823406','7b664e3e-f58a-4e66-8c0f-be1458541d14','BFX','BFX柏芙澜',NULL,262,1,'BFX柏芙澜','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f0b27702-4212-430c-ba28-5adda333a86a','7b664e3e-f58a-4e66-8c0f-be1458541d14','MNX','MNX玛尼毛衫',NULL,221,1,'MNX玛尼毛衫','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f1e5ca9a-2970-4849-bbd3-efb766e5a848','7b664e3e-f58a-4e66-8c0f-be1458541d14','GLS','格鲁丝',NULL,301,1,'GLS格鲁丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f266600a-7af3-4238-aed1-ccbffd887db3','7b664e3e-f58a-4e66-8c0f-be1458541d14','MZS','MZS曼紫双面呢',NULL,77,1,'曼紫双面呢','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f3251ee9-8c9d-4547-ae7c-a7bdc7ca95dd','7b664e3e-f58a-4e66-8c0f-be1458541d14','TAS','TAS铜氨丝',NULL,284,1,'TAS铜氨丝','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f38bbf12-e3c1-497c-95d9-1c68b3ae3e9c','48458681-48b0-490a-a840-0ffcbe49f5d4','Q','套装',NULL,156,1,'套装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f3a5b120-3ff6-4679-a159-a2af679a6e24','7b664e3e-f58a-4e66-8c0f-be1458541d14','BJN','BJN贝婕妮春装',NULL,260,1,'BJN贝婕妮春装','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f44155fb-734c-4e6b-bbe2-fa0a3c3e6e94','7b664e3e-f58a-4e66-8c0f-be1458541d14','CCC','CCC折扣品',NULL,268,1,'折扣品','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f4c60280-5885-48b5-9093-623170a8a2a1','7b664e3e-f58a-4e66-8c0f-be1458541d14','SSS','SSS莎莎双面尼',NULL,249,1,'SSS莎莎双面尼','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f4d4f3ba-326f-473b-ac5e-c3fad3ac56f4','7b664e3e-f58a-4e66-8c0f-be1458541d14','MGG','MGG木果果木',NULL,274,1,'MGG木果果木','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f65fd2dd-63c5-4d00-bba7-a1593a4ca40b','7b664e3e-f58a-4e66-8c0f-be1458541d14','STX','STX诗婷',NULL,313,1,'STX诗婷','2018-10-19 13:06:36','2018-10-19 13:06:36');
INSERT INTO `syscode`(`Guid`,`ParentGuid`,`CodeType`,`Name`,`EnName`,`Sort`,`Status`,`Summary`,`AddTime`,`EditTime`) VALUES
('f6d55b1a-10c4-4ee9-a160-ed982411e663','7b664e3e-f58a-4e66-8c0f-be1458541d14','CSX','尘色CSX',NULL,127,1,' 尘色X','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f7b26c3e-4128-40ab-b080-144cfd28b6fb','7b664e3e-f58a-4e66-8c0f-be1458541d14','XKB','XKB西可可',NULL,80,1,'西可可','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('f8ce041e-8da3-4cba-9ca3-762922062a63','48458681-48b0-490a-a840-0ffcbe49f5d4','D','外套',NULL,16,1,'外套-D','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fbf9703e-23d9-4b3d-b8d1-b747c1d44a89','7b664e3e-f58a-4e66-8c0f-be1458541d14','RST','RST双面尼三',NULL,175,1,'RST双面尼三','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fdd9a2c3-5256-4eb7-abc6-d11789517550','7b664e3e-f58a-4e66-8c0f-be1458541d14','RLX','RLX芮丽',NULL,280,1,'RLX芮丽','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fe355bd2-9ac5-4127-8b75-7830d30353af','7b664e3e-f58a-4e66-8c0f-be1458541d14','YXS','YXS伊袖',NULL,300,1,'YXS伊袖','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('fedc9430-77ea-4055-b923-ce90964f09c9','e86cf108-dc4d-4532-8cce-fdb041363902','E','L',NULL,36,1,'L','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ff1f8041-dfb8-43af-8540-76e1de25af53','7b664e3e-f58a-4e66-8c0f-be1458541d14','XBX','XBX小背心',NULL,229,1,'XBX小背心','2018-10-19 13:06:36','2018-10-19 13:06:36'),
('ff899c09-9242-4d0a-aad6-64594079227a','7b664e3e-f58a-4e66-8c0f-be1458541d14','LCX','LCX莱茨大衣',NULL,253,1,'LCX莱茨大衣','2018-10-19 13:06:36','2018-10-19 13:06:36');
/*!40000 ALTER TABLE `syscode` ENABLE KEYS */;

-- 
-- Definition of syscodetype
-- 

DROP TABLE IF EXISTS `syscodetype`;
CREATE TABLE IF NOT EXISTS `syscodetype` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标号Guid',
  `ParentGuid` varchar(50) DEFAULT NULL COMMENT '字典类型父级',
  `Layer` int(11) NOT NULL DEFAULT '0' COMMENT '深度',
  `Name` varchar(50) NOT NULL COMMENT '字典类型名称',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '字典类型排序',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `SiteGuid` varchar(50) DEFAULT NULL COMMENT '归属公司或站点',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table syscodetype
-- 

/*!40000 ALTER TABLE `syscodetype` DISABLE KEYS */;
INSERT INTO `syscodetype`(`Guid`,`ParentGuid`,`Layer`,`Name`,`Sort`,`AddTime`,`EditTime`,`SiteGuid`) VALUES
('1942d4fd-3203-42b1-a955-4a84a532b2a2','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'年份',19,'2018-07-24 18:54:48','2018-07-24 18:54:48',NULL),
('2e0393f9-e6d6-4c15-98cf-96072f0d3d70','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'批次',15,'2018-06-18 06:38:03','2018-06-18 06:38:03',NULL),
('48458681-48b0-490a-a840-0ffcbe49f5d4','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'款式',14,'2018-06-18 06:37:55','2018-06-18 06:37:55',NULL),
('7088d9b9-6692-4fc7-a83c-da580f1407c3','9d7643f0-d739-4342-b2da-45781b62ddd0',1,'采购商品单位',18,'2018-06-30 19:28:13','2018-06-30 19:28:13',NULL),
('7b664e3e-f58a-4e66-8c0f-be1458541d14','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'品牌',5,'2018-06-18 06:21:54','2018-06-18 06:21:54',NULL),
('8cb134d5-979b-40e2-b453-aeee265f4ab2','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'季节',13,'2018-06-18 06:35:32','2018-06-18 06:35:32',NULL),
('8d3158d6-e179-4046-99e9-53eb8c04ddb1',NULL,0,'服装SKU',4,'2018-06-18 06:21:45','2018-06-18 06:21:46',NULL),
('9d7643f0-d739-4342-b2da-45781b62ddd0',NULL,0,'采购字典',17,'2018-06-30 19:28:02','2018-06-30 19:28:02',NULL),
('e86cf108-dc4d-4532-8cce-fdb041363902','8d3158d6-e179-4046-99e9-53eb8c04ddb1',1,'尺码',16,'2018-06-18 06:38:09','2018-06-18 06:38:09',NULL);
/*!40000 ALTER TABLE `syscodetype` ENABLE KEYS */;

-- 
-- Definition of sysimage
-- 

DROP TABLE IF EXISTS `sysimage`;
CREATE TABLE IF NOT EXISTS `sysimage` (
  `Guid` varchar(50) NOT NULL,
  `TheGuid` varchar(50) DEFAULT NULL COMMENT '所属栏目Guid',
  `Types` int(11) NOT NULL DEFAULT '0' COMMENT '图片类型，一个栏目可有多个图片类型',
  `Title` varchar(50) DEFAULT NULL COMMENT '图片名称',
  `ImgBig` varchar(255) NOT NULL COMMENT '图片原图',
  `ImgSmall` varchar(255) DEFAULT NULL COMMENT '图片缩略图',
  `IsCover` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否为封面',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序字段',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysimage
-- 

/*!40000 ALTER TABLE `sysimage` DISABLE KEYS */;

/*!40000 ALTER TABLE `sysimage` ENABLE KEYS */;

-- 
-- Definition of syslog
-- 

DROP TABLE IF EXISTS `syslog`;
CREATE TABLE IF NOT EXISTS `syslog` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标识——Guid',
  `LoginName` varchar(50) NOT NULL COMMENT '日志操作ID',
  `DepartName` varchar(50) DEFAULT NULL COMMENT '日志操作人所属部门Guid',
  `OptionTable` varchar(50) DEFAULT NULL COMMENT '操作表名',
  `Summary` varchar(255) NOT NULL COMMENT '日志操作内容',
  `IP` varchar(20) NOT NULL COMMENT '日志操作IP地址',
  `MacUrl` varchar(50) DEFAULT NULL COMMENT '日志操作Mac地址',
  `LogType` int(11) NOT NULL DEFAULT '0' COMMENT '日志操作类型',
  `Urls` varchar(255) NOT NULL COMMENT '日志操作Url',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '日志添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table syslog
-- 

/*!40000 ALTER TABLE `syslog` DISABLE KEYS */;
INSERT INTO `syslog`(`Guid`,`LoginName`,`DepartName`,`OptionTable`,`Summary`,`IP`,`MacUrl`,`LogType`,`Urls`,`AddTime`) VALUES
('3b988aa9-93b8-409a-bb5e-8f3b2e1e5319','admins','商务中心','SysAdmin','登录操作','::1',NULL,1,'/fytadmin/login','2018-10-23 19:02:36');
/*!40000 ALTER TABLE `syslog` ENABLE KEYS */;

-- 
-- Definition of sysmenu
-- 

DROP TABLE IF EXISTS `sysmenu`;
CREATE TABLE IF NOT EXISTS `sysmenu` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一标识Guid',
  `SiteGuid` varchar(50) DEFAULT NULL COMMENT '所属站点或公司菜单',
  `ParentGuid` varchar(50) DEFAULT NULL COMMENT '菜单父级Guid',
  `ParentName` varchar(50) NOT NULL COMMENT '父级菜单名称',
  `Name` varchar(50) NOT NULL COMMENT '菜单名称',
  `NameCode` varchar(50) NOT NULL COMMENT '菜单名称标识',
  `ParentGuidList` varchar(500) DEFAULT NULL COMMENT '所属父级的集合',
  `Layer` int(10) NOT NULL COMMENT '菜单深度',
  `Urls` varchar(255) DEFAULT NULL COMMENT '菜单Url',
  `Icon` varchar(50) DEFAULT NULL COMMENT '菜单图标Class',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '菜单排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '菜单状态 true=正常 false=不显示',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  `AddTIme` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysmenu
-- 

/*!40000 ALTER TABLE `sysmenu` DISABLE KEYS */;
INSERT INTO `sysmenu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('00aaf062-ee50-4844-9b51-80743a65cd4d',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','功能组件','2040',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,',2,NULL,'layui-icon-component',62,1,'2018-10-22 23:02:09','2018-10-22 23:02:09'),
('0a61ddff-ead5-49c0-8bed-2189872b8646',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','栏目管理','2020',',a4b3b26f-076a-4267-b03d-613081b13a12,0a61ddff-ead5-49c0-8bed-2189872b8646,',2,NULL,'layui-icon-template',56,1,'2018-09-29 22:03:38','0001-01-01 00:00:00'),
('0d65e849-f903-4cf3-b413-9e4e7bbda82e',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','内容管理','2030',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,',2,NULL,'layui-icon-survey',58,1,'2018-09-29 22:03:56','0001-01-01 00:00:00'),
('1d0bb157-868e-41e6-b048-f2c139111ab3',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','文本回复','3023',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,1d0bb157-868e-41e6-b048-f2c139111ab3,',3,NULL,NULL,74,1,'2018-09-29 21:58:54','2018-09-29 21:58:54'),
('1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633','我的工作台','数据库文件','2013',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1,',3,NULL,NULL,55,1,'2018-09-29 21:49:18','2018-09-29 21:49:18'),
('1fc3d8e8-e3f2-49cf-a652-2341082643df',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','用户管理','1012',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,1fc3d8e8-e3f2-49cf-a652-2341082643df,',3,'/fytadmin/sys/admin/',NULL,6,1,'2018-09-28 23:26:43','2018-09-28 23:26:43');
INSERT INTO `sysmenu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('2a3a4afe-2a51-4858-9c85-df26bb7a7611',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809','微信公众号','基本设置','3010',',f752cbdb-48b9-4958-bd05-0b8c3602e809,2a3a4afe-2a51-4858-9c85-df26bb7a7611,',2,NULL,'layui-icon-set',68,1,'2018-09-29 22:05:43','0001-01-01 00:00:00'),
('35834721-2287-416d-aed2-d0a43277e70e',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','留言管理','2043',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,35834721-2287-416d-aed2-d0a43277e70e,',3,NULL,NULL,65,1,'2018-09-29 21:52:21','2018-09-29 21:52:21'),
('3d0acfb2-fa3c-4fe6-bd5c-e587c4523978',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809','微信公众号','消息管理','3020',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,',2,NULL,'layui-icon-speaker',71,1,'2018-09-29 22:05:53','0001-01-01 00:00:00'),
('3d4f34cb-b69f-47e1-8abb-2f2b7ae20520',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','关注回复','3021',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,3d4f34cb-b69f-47e1-8abb-2f2b7ae20520,',3,NULL,NULL,72,1,'2018-09-29 21:58:22','2018-09-29 21:58:22'),
('3f8327fd-b8be-44d9-801c-39520e72da87',NULL,'0a61ddff-ead5-49c0-8bed-2189872b8646','栏目管理','栏目列表','2021',',a4b3b26f-076a-4267-b03d-613081b13a12,0a61ddff-ead5-49c0-8bed-2189872b8646,3f8327fd-b8be-44d9-801c-39520e72da87,',3,NULL,NULL,57,1,'2018-09-29 21:50:00','2018-09-29 21:50:00'),
('404d4b8b-8e3c-42ee-aee5-f29df31308fa',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','菜单管理','1013',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,404d4b8b-8e3c-42ee-aee5-f29df31308fa,',3,'/fytadmin/sys/menu/',NULL,7,1,'2018-09-28 23:26:50','2018-09-28 23:26:50');
INSERT INTO `sysmenu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('4e104381-22f5-4a91-a784-00a7276afa61',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','文件管理','2045',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,4e104381-22f5-4a91-a784-00a7276afa61,',3,NULL,NULL,67,1,'2018-09-29 21:52:58','2018-09-29 21:52:58'),
('51216bb3-d0c7-474a-b565-71cf96db19f4',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','版本更新','1016',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,51216bb3-d0c7-474a-b565-71cf96db19f4,',3,'/fytadmin/app/setting/',NULL,10,1,'2018-09-28 23:27:39','2018-09-28 23:27:39'),
('51c9c0aa-de65-47d0-87bc-cef0624cb8f9',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633','我的工作台','数据库备份','2012',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,51c9c0aa-de65-47d0-87bc-cef0624cb8f9,',3,'/fytadmin/sys/database/',NULL,54,1,'2018-10-23 19:02:28','0001-01-01 00:00:00'),
('5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','角色管理','1011',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,5ce13ead-971b-4ed4-ad5f-acacccd82381,',3,'/fytadmin/sys/role/',NULL,5,1,'2018-09-28 23:26:07','2018-09-28 23:26:07'),
('5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','系统管理','1001',',a4b3b26f-076a-4267-b03d-613081b13a12,5ed17c74-0fff-4f88-8581-3b4f26d005a8,',2,NULL,'layui-icon-set',2,1,'2018-10-22 23:06:12','0001-01-01 00:00:00'),
('6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','部门管理','1010',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,6d4cfcf7-ff1c-4537-aa3f-75cc9df27583,',3,'/fytadmin/sys/organize/',NULL,4,1,'2018-09-28 23:22:49','2018-09-28 23:22:49');
INSERT INTO `sysmenu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('787ae7bf-fb35-4ed4-9c6a-15aba81609c3',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','语音回复','3025',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,787ae7bf-fb35-4ed4-9c6a-15aba81609c3,',3,NULL,NULL,76,1,'2018-09-29 21:59:25','2018-09-29 21:59:25'),
('7e2356b0-f77f-41fe-b27b-15665b0ccba0',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','下载管理','2044',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,7e2356b0-f77f-41fe-b27b-15665b0ccba0,',3,NULL,NULL,66,1,'2018-09-29 21:52:37','2018-09-29 21:52:37'),
('8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','默认回复','3022',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f,',3,NULL,NULL,73,1,'2018-09-29 21:58:38','2018-09-29 21:58:38'),
('945de8ba-a13d-4ffc-aa62-c072ea2a3b05',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e','内容管理','文章管理','2031',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,945de8ba-a13d-4ffc-aa62-c072ea2a3b05,',3,NULL,NULL,59,1,'2018-09-29 21:50:43','2018-09-29 21:50:43'),
('98285095-b35d-458d-9908-355d2e4fddbd',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','广告管理','2041',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,98285095-b35d-458d-9908-355d2e4fddbd,',3,NULL,NULL,63,1,'2018-09-29 21:51:54','2018-09-29 21:51:54'),
('a05afbda-1234-4ca0-a160-6dd11ea3bf7d',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','消息记录','3026',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,a05afbda-1234-4ca0-a160-6dd11ea3bf7d,',3,NULL,NULL,77,1,'2018-09-29 21:59:42','2018-09-29 21:59:42');
INSERT INTO `sysmenu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('a171bbb0-c65c-4e09-82f5-9ed51169b24d',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611','基本设置','公众平台管理','3011',',f752cbdb-48b9-4958-bd05-0b8c3602e809,2a3a4afe-2a51-4858-9c85-df26bb7a7611,a171bbb0-c65c-4e09-82f5-9ed51169b24d,',3,NULL,NULL,69,1,'2018-09-29 21:56:59','2018-09-29 21:56:59'),
('a280f6e2-3100-445f-871d-77ea443356a9',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','字段管理','1015',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,a280f6e2-3100-445f-871d-77ea443356a9,',3,'/fytadmin/sys/codes/',NULL,9,1,'2018-09-28 23:27:32','2018-09-28 23:27:32'),
('a4b3b26f-076a-4267-b03d-613081b13a12',NULL,NULL,'根目录','CMS内容管理','0002',',a4b3b26f-076a-4267-b03d-613081b13a12,',1,NULL,'layui-icon-website',50,1,'2018-09-29 16:18:57','2018-09-29 16:18:57'),
('a82ecfbf-b428-4022-b9a3-81ad277fc05c',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d','功能组件','投票管理','2042',',a4b3b26f-076a-4267-b03d-613081b13a12,00aaf062-ee50-4844-9b51-80743a65cd4d,a82ecfbf-b428-4022-b9a3-81ad277fc05c,',3,NULL,NULL,64,1,'2018-09-29 21:52:07','2018-09-29 21:52:07'),
('a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e','内容管理','点击排行','2033',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a,',3,NULL,NULL,61,1,'2018-09-29 21:51:13','2018-09-29 21:51:13'),
('b354ea64-88b6-4032-a26a-fee198e24d9d',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8','系统管理','系统日志','1014',',5ed17c74-0fff-4f88-8581-3b4f26d005a8,b354ea64-88b6-4032-a26a-fee198e24d9d,',3,'/fytadmin/sys/log/',NULL,8,1,'2018-09-28 23:27:06','2018-09-28 23:27:06'),
('b8ede145-b5c2-4339-a3cc-f9808aa0c776',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978','消息管理','图文回复','3024',',f752cbdb-48b9-4958-bd05-0b8c3602e809,3d0acfb2-fa3c-4fe6-bd5c-e587c4523978,b8ede145-b5c2-4339-a3cc-f9808aa0c776,',3,NULL,NULL,75,1,'2018-09-29 21:59:10','2018-09-29 21:59:10');
INSERT INTO `sysmenu`(`Guid`,`SiteGuid`,`ParentGuid`,`ParentName`,`Name`,`NameCode`,`ParentGuidList`,`Layer`,`Urls`,`Icon`,`Sort`,`Status`,`EditTime`,`AddTIme`) VALUES
('be526a42-9a48-4221-bc9b-3e1d5ddddf2f',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633','我的工作台','站点管理','2011',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,be526a42-9a48-4221-bc9b-3e1d5ddddf2f,',3,NULL,NULL,53,1,'2018-09-29 21:48:41','2018-09-29 21:48:41'),
('c1f840e9-e822-4d0f-aca2-28365c52a7c4',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e','内容管理','回收站管理','2032',',a4b3b26f-076a-4267-b03d-613081b13a12,0d65e849-f903-4cf3-b413-9e4e7bbda82e,c1f840e9-e822-4d0f-aca2-28365c52a7c4,',3,NULL,NULL,60,1,'2018-09-29 21:50:58','2018-09-29 21:50:58'),
('dad12bae-d3f3-4c0e-a728-2e6af5f40e66',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611','基本设置','自定义菜单','3012',',f752cbdb-48b9-4958-bd05-0b8c3602e809,2a3a4afe-2a51-4858-9c85-df26bb7a7611,dad12bae-d3f3-4c0e-a728-2e6af5f40e66,',3,NULL,NULL,70,1,'2018-09-29 21:57:15','2018-09-29 21:57:15'),
('f752cbdb-48b9-4958-bd05-0b8c3602e809',NULL,NULL,'根目录','微信公众号','0003',',f752cbdb-48b9-4958-bd05-0b8c3602e809,',1,NULL,'layui-icon-login-wechat',51,1,'2018-09-29 16:19:10','2018-09-29 16:19:10'),
('fe96606e-2b92-4587-8196-e5b4e85ed633',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12','CMS内容管理','我的工作台','2010',',a4b3b26f-076a-4267-b03d-613081b13a12,fe96606e-2b92-4587-8196-e5b4e85ed633,',2,NULL,'layui-icon-website',52,1,'2018-09-29 21:43:43','2018-09-29 21:43:43');
/*!40000 ALTER TABLE `sysmenu` ENABLE KEYS */;

-- 
-- Definition of sysorganize
-- 

DROP TABLE IF EXISTS `sysorganize`;
CREATE TABLE IF NOT EXISTS `sysorganize` (
  `Guid` varchar(50) NOT NULL,
  `SiteGuid` varchar(50) DEFAULT NULL COMMENT '归属站点',
  `ParentGuid` varchar(50) DEFAULT NULL COMMENT '父节点',
  `Name` varchar(20) NOT NULL COMMENT '组织名称',
  `ParentName` varchar(20) DEFAULT NULL,
  `ParentGuidList` varchar(500) DEFAULT NULL COMMENT '父节点集合',
  `Layer` int(11) NOT NULL DEFAULT '1' COMMENT '层级',
  `Sort` int(11) NOT NULL DEFAULT '1' COMMENT '排序',
  `Status` bit(1) NOT NULL DEFAULT b'1' COMMENT '状态',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysorganize
-- 

/*!40000 ALTER TABLE `sysorganize` DISABLE KEYS */;
INSERT INTO `sysorganize`(`Guid`,`SiteGuid`,`ParentGuid`,`Name`,`ParentName`,`ParentGuidList`,`Layer`,`Sort`,`Status`,`EditTime`) VALUES
('24febdc4-655f-4492-ac8a-4adab18c22c8',NULL,'388b72d3-e10a-4183-8ef7-6be44eb99b1a','融媒体中心','包头分公司',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,',2,7,1,'2018-05-16 22:09:35'),
('388b72d3-e10a-4183-8ef7-6be44eb99b1a',NULL,'883deb1c-ddd7-484e-92c1-b3ad3b32e655','包头分公司','飞易腾集团',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,',1,3,1,'2018-05-16 22:06:20'),
('4b6ab27f-c0fa-483d-9b5a-55891ee8d727',NULL,'388b72d3-e10a-4183-8ef7-6be44eb99b1a','事业发展部','包头分公司',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,4b6ab27f-c0fa-483d-9b5a-55891ee8d727,',2,6,1,'2018-05-16 22:09:30'),
('52523a76-52b3-4c25-a1bd-9123a011f2a8',NULL,'24febdc4-655f-4492-ac8a-4adab18c22c8','商务中心','融媒体中心',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,52523a76-52b3-4c25-a1bd-9123a011f2a8,',3,4,1,'2018-07-20 23:03:59'),
('5533b6c5-ba2e-4659-be29-c860bb41e04d',NULL,'883deb1c-ddd7-484e-92c1-b3ad3b32e655','北京总公司','飞易腾集团',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,',1,2,1,'2018-05-16 22:06:02'),
('883deb1c-ddd7-484e-92c1-b3ad3b32e655',NULL,NULL,'飞易腾集团','根目录',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,',0,1,1,'2018-05-15 00:11:55'),
('dcf99638-5db6-4dd7-a485-31df1160d45a',NULL,'5533b6c5-ba2e-4659-be29-c860bb41e04d','互联网中心','北京总公司',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,',2,5,1,'2018-05-16 22:09:36');
/*!40000 ALTER TABLE `sysorganize` ENABLE KEYS */;

-- 
-- Definition of syspermissions
-- 

DROP TABLE IF EXISTS `syspermissions`;
CREATE TABLE IF NOT EXISTS `syspermissions` (
  `RoleGuid` varchar(50) NOT NULL COMMENT '角色Guid',
  `AdminGuid` varchar(50) DEFAULT NULL COMMENT '管理员ID',
  `MenuGuid` varchar(50) DEFAULT NULL COMMENT '菜单Guid',
  `BtnFunGuid` varchar(50) DEFAULT NULL,
  `Types` tinyint(1) NOT NULL DEFAULT '1' COMMENT '授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table syspermissions
-- 

/*!40000 ALTER TABLE `syspermissions` DISABLE KEYS */;
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61',1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','c4261103-dfc7-46e5-ab20-4ca5fc7729f6',1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','931bd729-f160-4fe3-bb7c-7828a2da047a',1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61',1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','c4261103-dfc7-46e5-ab20-4ca5fc7729f6',1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583','6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61',1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','c4261103-dfc7-46e5-ab20-4ca5fc7729f6',1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb','30d3da88-bb72-4ace-a303-b3aae0ecb732',NULL,NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a','12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e','30d3da88-bb72-4ace-a303-b3aae0ecb732',NULL,NULL,1),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb','12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381','b943200f-7c99-44b5-93d9-e4ea2505928a',1),
('2949c266-575a-4e5d-a663-e39d5f33df7e','12cc96cf-7ccf-430b-a54a-e1c6f04690cb',NULL,NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'de263753-0706-4985-bf96-317059e483ff',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'1fc3d8e8-e3f2-49cf-a652-2341082643df',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'404d4b8b-8e3c-42ee-aee5-f29df31308fa',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'b354ea64-88b6-4032-a26a-fee198e24d9d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a280f6e2-3100-445f-871d-77ea443356a9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'51216bb3-d0c7-474a-b565-71cf96db19f4',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'40823e8a-bc10-4e38-a45f-a6dd7dd0ef78',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'f9129ddd-3d96-4980-ac48-f6aa9a8b6ba9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d46fb5d3-27fc-411f-8bc8-df175cc248e4',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'cc59a616-2ca6-4ca8-9907-80ace8d38b47',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'8f6d2ac6-0c9b-4c9c-a1cd-80ca6365781e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d1f782d2-55c3-4ca6-8002-894a1da52515',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'eafbc38f-fccd-4a5a-9df2-44ff41fde6d9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5be5bed2-8b11-470f-a233-69a208737f47',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'88989fbb-57e0-4813-a125-f54ca941299c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'01d476db-17c1-42b9-9725-c995a942006f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'18e89f9b-dc89-4289-a8fd-ee1330e43f79',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d9a75927-0700-42b9-9ded-55774ee5c20b',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'9d1dcb18-6db8-4d79-bc7e-f2c830f4262d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'7bfb29b1-b0af-440f-afe9-82883e2e114e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'b217116f-27c3-4d1e-9eda-538ca34bee45',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'401273ed-1639-4646-8b2d-8171beba1c60',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'79315453-3610-435e-80ed-abd7d8c4f770',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'86fc504c-3ce8-40ec-804a-c0c8fb6b520f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'7f0f61dd-ff53-460e-878c-bb3af87740eb',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'db4e4295-fce4-42a6-96f2-387bddcc5449',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'de39aa52-87a6-4539-884d-1ae3a9d6f99c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5cbca08b-77a6-4294-aa15-4d82d0baa5f8',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'8360ffe4-b8f4-4697-9930-9fbc058d7f92',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6dc1436e-6ed9-43be-96c5-2e165d43459b',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'cb7e34dc-54cd-41a1-bbd2-666ab2bdf742',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'155b3ab6-1043-4a78-9b59-bb3d1433a17a',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'cb2e4ab6-48de-4a1b-80e9-f3f77eaf1a6f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'663d5881-70ec-49a5-ae94-34f53d23608e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'d651f1e3-653c-4033-9c80-7de6dc9be422',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'6cc8a71a-ef58-48cc-aafb-aafa5a311d7e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'078cff27-e491-48a0-8f64-8abc06e20bd3',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3476c160-da68-4bed-9e0b-e38c7af7d099',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3543facc-f81a-4e66-9f62-1a7ffa7bd8e0',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'5ef5e2eb-5902-446b-82e4-11a6d36140fe',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'e63fd97a-2a8c-40d4-a1e1-6f574c85864c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'c8728c2f-0637-4b8c-808f-289e31aaf495',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'c26b94c0-cae6-4332-8814-f5c8fbfaa58c',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'f879f695-5d6c-4b62-9bfe-79ba8714079f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'be526a42-9a48-4221-bc9b-3e1d5ddddf2f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'51c9c0aa-de65-47d0-87bc-cef0624cb8f9',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'0a61ddff-ead5-49c0-8bed-2189872b8646',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3f8327fd-b8be-44d9-801c-39520e72da87',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'945de8ba-a13d-4ffc-aa62-c072ea2a3b05',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'c1f840e9-e822-4d0f-aca2-28365c52a7c4',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'98285095-b35d-458d-9908-355d2e4fddbd',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a82ecfbf-b428-4022-b9a3-81ad277fc05c',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'35834721-2287-416d-aed2-d0a43277e70e',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'7e2356b0-f77f-41fe-b27b-15665b0ccba0',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'4e104381-22f5-4a91-a784-00a7276afa61',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a171bbb0-c65c-4e09-82f5-9ed51169b24d',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'dad12bae-d3f3-4c0e-a728-2e6af5f40e66',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'3d4f34cb-b69f-47e1-8abb-2f2b7ae20520',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'1d0bb157-868e-41e6-b048-f2c139111ab3',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'b8ede145-b5c2-4339-a3cc-f9808aa0c776',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'787ae7bf-fb35-4ed4-9c6a-15aba81609c3',NULL,1),
('2949c266-575a-4e5d-a663-e39d5f33df7e',NULL,'a05afbda-1234-4ca0-a160-6dd11ea3bf7d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'de263753-0706-4985-bf96-317059e483ff',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ed17c74-0fff-4f88-8581-3b4f26d005a8',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6d4cfcf7-ff1c-4537-aa3f-75cc9df27583',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ce13ead-971b-4ed4-ad5f-acacccd82381',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'1fc3d8e8-e3f2-49cf-a652-2341082643df',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'404d4b8b-8e3c-42ee-aee5-f29df31308fa',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'b354ea64-88b6-4032-a26a-fee198e24d9d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a280f6e2-3100-445f-871d-77ea443356a9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'51216bb3-d0c7-474a-b565-71cf96db19f4',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'40823e8a-bc10-4e38-a45f-a6dd7dd0ef78',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'f9129ddd-3d96-4980-ac48-f6aa9a8b6ba9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d46fb5d3-27fc-411f-8bc8-df175cc248e4',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'cc59a616-2ca6-4ca8-9907-80ace8d38b47',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'8f6d2ac6-0c9b-4c9c-a1cd-80ca6365781e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d1f782d2-55c3-4ca6-8002-894a1da52515',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'eafbc38f-fccd-4a5a-9df2-44ff41fde6d9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5be5bed2-8b11-470f-a233-69a208737f47',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'88989fbb-57e0-4813-a125-f54ca941299c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'01d476db-17c1-42b9-9725-c995a942006f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'18e89f9b-dc89-4289-a8fd-ee1330e43f79',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d9a75927-0700-42b9-9ded-55774ee5c20b',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'9d1dcb18-6db8-4d79-bc7e-f2c830f4262d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'7bfb29b1-b0af-440f-afe9-82883e2e114e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'b217116f-27c3-4d1e-9eda-538ca34bee45',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'401273ed-1639-4646-8b2d-8171beba1c60',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'79315453-3610-435e-80ed-abd7d8c4f770',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'86fc504c-3ce8-40ec-804a-c0c8fb6b520f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'7f0f61dd-ff53-460e-878c-bb3af87740eb',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'db4e4295-fce4-42a6-96f2-387bddcc5449',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'de39aa52-87a6-4539-884d-1ae3a9d6f99c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5cbca08b-77a6-4294-aa15-4d82d0baa5f8',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'8360ffe4-b8f4-4697-9930-9fbc058d7f92',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6dc1436e-6ed9-43be-96c5-2e165d43459b',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'cb7e34dc-54cd-41a1-bbd2-666ab2bdf742',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'155b3ab6-1043-4a78-9b59-bb3d1433a17a',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'cb2e4ab6-48de-4a1b-80e9-f3f77eaf1a6f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'663d5881-70ec-49a5-ae94-34f53d23608e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'d651f1e3-653c-4033-9c80-7de6dc9be422',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'6cc8a71a-ef58-48cc-aafb-aafa5a311d7e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'078cff27-e491-48a0-8f64-8abc06e20bd3',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3476c160-da68-4bed-9e0b-e38c7af7d099',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'da7019c8-5df1-46c5-b9ce-3d2d83a73cd8',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3543facc-f81a-4e66-9f62-1a7ffa7bd8e0',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'5ef5e2eb-5902-446b-82e4-11a6d36140fe',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'e63fd97a-2a8c-40d4-a1e1-6f574c85864c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'c8728c2f-0637-4b8c-808f-289e31aaf495',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'c26b94c0-cae6-4332-8814-f5c8fbfaa58c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'f879f695-5d6c-4b62-9bfe-79ba8714079f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a4b3b26f-076a-4267-b03d-613081b13a12',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'fe96606e-2b92-4587-8196-e5b4e85ed633',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'be526a42-9a48-4221-bc9b-3e1d5ddddf2f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'51c9c0aa-de65-47d0-87bc-cef0624cb8f9',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'1d58c9b0-22fa-4fe4-aba4-06a4ecd5b0a1',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'0a61ddff-ead5-49c0-8bed-2189872b8646',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3f8327fd-b8be-44d9-801c-39520e72da87',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'0d65e849-f903-4cf3-b413-9e4e7bbda82e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'945de8ba-a13d-4ffc-aa62-c072ea2a3b05',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'c1f840e9-e822-4d0f-aca2-28365c52a7c4',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a90d2eaf-918f-4c43-8ce2-3f4f9b05c74a',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'00aaf062-ee50-4844-9b51-80743a65cd4d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'98285095-b35d-458d-9908-355d2e4fddbd',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a82ecfbf-b428-4022-b9a3-81ad277fc05c',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'35834721-2287-416d-aed2-d0a43277e70e',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'7e2356b0-f77f-41fe-b27b-15665b0ccba0',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'4e104381-22f5-4a91-a784-00a7276afa61',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'f752cbdb-48b9-4958-bd05-0b8c3602e809',NULL,1);
INSERT INTO `syspermissions`(`RoleGuid`,`AdminGuid`,`MenuGuid`,`BtnFunGuid`,`Types`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'2a3a4afe-2a51-4858-9c85-df26bb7a7611',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a171bbb0-c65c-4e09-82f5-9ed51169b24d',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'dad12bae-d3f3-4c0e-a728-2e6af5f40e66',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3d0acfb2-fa3c-4fe6-bd5c-e587c4523978',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'3d4f34cb-b69f-47e1-8abb-2f2b7ae20520',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'8cdf2bc4-b5f8-4d6a-9282-5e5f6042d69f',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'1d0bb157-868e-41e6-b048-f2c139111ab3',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'b8ede145-b5c2-4339-a3cc-f9808aa0c776',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'787ae7bf-fb35-4ed4-9c6a-15aba81609c3',NULL,1),
('9bf21fc0-6e39-4e16-a55f-6717977a697a',NULL,'a05afbda-1234-4ca0-a160-6dd11ea3bf7d',NULL,1);
/*!40000 ALTER TABLE `syspermissions` ENABLE KEYS */;

-- 
-- Definition of sysrole
-- 

DROP TABLE IF EXISTS `sysrole`;
CREATE TABLE IF NOT EXISTS `sysrole` (
  `Guid` varchar(50) NOT NULL,
  `DepartmentGuid` varchar(50) NOT NULL COMMENT '部门Guid',
  `DepartmentName` varchar(50) NOT NULL COMMENT '部门名称',
  `DepartmentGroup` varchar(500) NOT NULL COMMENT '归属于角色组',
  `Name` varchar(50) NOT NULL COMMENT '部门名称',
  `Codes` varchar(50) NOT NULL COMMENT '部门编号',
  `IsSystem` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否为超级管理员',
  `Summary` varchar(500) DEFAULT NULL COMMENT '部门描述',
  `AddTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `EditTime` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '修改时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sysrole
-- 

/*!40000 ALTER TABLE `sysrole` DISABLE KEYS */;
INSERT INTO `sysrole`(`Guid`,`DepartmentGuid`,`DepartmentName`,`DepartmentGroup`,`Name`,`Codes`,`IsSystem`,`Summary`,`AddTime`,`EditTime`) VALUES
('9bf21fc0-6e39-4e16-a55f-6717977a697a','52523a76-52b3-4c25-a1bd-9123a011f2a8','商务中心',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,52523a76-52b3-4c25-a1bd-9123a011f2a8,','客服管理员','1002',1,'只能查看会员相关功能','2018-05-17 23:37:56','2018-07-16 11:24:03'),
('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb','dcf99638-5db6-4dd7-a485-31df1160d45a','互联网中心',',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,','财务管理员','1003',1,'只能查看财务相关功能','2018-05-17 23:39:01','2018-05-17 23:39:01');
/*!40000 ALTER TABLE `sysrole` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2018-10-31 22:52:05
-- Total time: 0:0:0:0:66 (d:h:m:s:ms)
