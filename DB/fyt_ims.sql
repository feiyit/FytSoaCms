/*
Navicat MySQL Data Transfer

Source Server         : fyt
Source Server Version : 50719
Source Host           : localhost:3306
Source Database       : fyt_ims

Target Server Type    : MYSQL
Target Server Version : 50719
File Encoding         : 65001

Date: 2018-07-09 23:56:59
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for erpappsetting
-- ----------------------------
DROP TABLE IF EXISTS `erpappsetting`;
CREATE TABLE `erpappsetting` (
  `Guid` varchar(50) NOT NULL,
  `AndroidVersion` decimal(2,1) NOT NULL DEFAULT '0.0' COMMENT '安卓版本号',
  `AndroidFile` varchar(255) DEFAULT NULL COMMENT '更新文件',
  `IosVersion` decimal(2,1) NOT NULL COMMENT 'Ios版本号',
  `IosFile` varchar(255) DEFAULT NULL COMMENT 'Ios更新文件地址',
  `IosAudit` tinyint(4) NOT NULL DEFAULT '0' COMMENT 'Ios审核开关  0=关/1=开',
  `UpdateDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpappsetting
-- ----------------------------

-- ----------------------------
-- Table structure for erpbackgoods
-- ----------------------------
DROP TABLE IF EXISTS `erpbackgoods`;
CREATE TABLE `erpbackgoods` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一编号',
  `Number` varchar(30) NOT NULL COMMENT '订单号',
  `ShopGuid` varchar(50) NOT NULL COMMENT '退货涉及的店铺',
  `OrderNumber` varchar(50) DEFAULT NULL COMMENT '退货涉及的订单号',
  `AdminGuid` varchar(50) NOT NULL COMMENT '谁提交的退货',
  `GoodsGuid` varchar(50) NOT NULL COMMENT '退货的商品',
  `BackCount` int(11) NOT NULL DEFAULT '1' COMMENT '退货数量',
  `BackMoney` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '退货的金额',
  `Status` tinyint(4) NOT NULL DEFAULT '1' COMMENT '退货的状态 1=提交退货 2=受理 3=完成 4=其他',
  `Summary` varchar(500) DEFAULT NULL COMMENT '退货原因',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '提交退货的时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpbackgoods
-- ----------------------------
INSERT INTO `erpbackgoods` VALUES ('1111', '111', '111', '111', '111', '111', '1', '0.00', '1', null, '2018-07-07 20:58:12');

-- ----------------------------
-- Table structure for erpgoods
-- ----------------------------
DROP TABLE IF EXISTS `erpgoods`;
CREATE TABLE `erpgoods` (
  `Guid` varchar(50) NOT NULL,
  `Title` varchar(50) DEFAULT NULL COMMENT '商品名称',
  `Brank` varchar(50) DEFAULT NULL COMMENT '品牌',
  `Style` varchar(50) DEFAULT NULL COMMENT '类别',
  `Cover` varchar(100) DEFAULT NULL COMMENT '商品封面图',
  `Status` tinyint(4) NOT NULL DEFAULT '1' COMMENT '状态 1=上架  2=下架  3=其他',
  `Attribute` varchar(2000) DEFAULT NULL COMMENT '规格属性',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除  0=否 1=是',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpgoods
-- ----------------------------
INSERT INTO `erpgoods` VALUES ('0a5f0b36-c457-48d1-8f68-eda48080e6d1', 'ADI阿迪春装连衣裙', 'ADI阿迪', '连衣裙', null, '1', null, '\0', '2018-06-24 23:16:40');
INSERT INTO `erpgoods` VALUES ('31f602d2-71a1-4d1a-8885-173f09b2a9b3', 'ADI阿迪春装上衣', 'ADI阿迪', '上衣', null, '1', null, '\0', '2018-06-27 21:33:16');
INSERT INTO `erpgoods` VALUES ('34057bbe-6165-4ded-9ac1-4b38a45fdc8d', 'ADI阿迪秋装T恤', 'ADI阿迪', 'T恤', null, '1', null, '\0', '2018-06-27 21:32:47');
INSERT INTO `erpgoods` VALUES ('585056c6-5c91-4b74-8c1b-b88a25300a49', 'NIK耐克夏装连衣裙', 'NIK耐克', '连衣裙', null, '1', null, '\0', '2018-06-27 21:31:14');
INSERT INTO `erpgoods` VALUES ('5e46347c-9643-4e76-a9b0-85e93bbf0396', 'LIN李宁夏装连衣裙', 'LIN李宁', '连衣裙', null, '1', null, '\0', '2018-06-27 21:31:30');
INSERT INTO `erpgoods` VALUES ('64fd9c02-279c-4e12-97ee-97b1c2a06783', 'LIN李宁春装T恤', 'LIN李宁', 'T恤', null, '1', null, '\0', '2018-06-27 21:33:04');
INSERT INTO `erpgoods` VALUES ('99205af7-ccaa-49a2-b934-4b064b0226dd', 'JOR乔丹春装T恤', 'JOR乔丹', 'T恤', null, '1', null, '\0', '2018-06-27 21:33:35');
INSERT INTO `erpgoods` VALUES ('a433162a-7af7-4834-826d-68c54174d9ff', 'NIK耐克夏装连衣裙', 'NIK耐克', '连衣裙', null, '1', null, '\0', '2018-06-24 22:51:08');
INSERT INTO `erpgoods` VALUES ('be85fcff-d419-428b-b12b-df173fc311bd', 'JOR乔丹夏装T恤', 'JOR乔丹', 'T恤', null, '1', null, '\0', '2018-06-27 21:31:02');
INSERT INTO `erpgoods` VALUES ('eabd1d03-cc80-43cb-bd45-834d1f741b5e', 'LIN李宁冬装连衣裙', 'LIN李宁', '连衣裙', null, '1', null, '\0', '2018-06-27 21:32:22');
INSERT INTO `erpgoods` VALUES ('f1a4cecc-829b-49a0-983a-aeddaff541b6', 'LIN李宁夏装连衣裙', 'LIN李宁', '连衣裙', null, '1', null, '\0', '2018-06-27 21:31:42');

-- ----------------------------
-- Table structure for erpgoodssku
-- ----------------------------
DROP TABLE IF EXISTS `erpgoodssku`;
CREATE TABLE `erpgoodssku` (
  `Guid` varchar(50) NOT NULL,
  `GoodsGuid` varchar(50) DEFAULT NULL COMMENT '商品编号',
  `Code` varchar(50) DEFAULT NULL COMMENT '条形码',
  `SalePrice` varchar(10) NOT NULL DEFAULT '0.00' COMMENT '销售价格',
  `DisPrice` varchar(10) NOT NULL DEFAULT '0.00' COMMENT '折扣价格',
  `StockSum` int(11) NOT NULL DEFAULT '0' COMMENT '库存',
  `SaleSum` int(11) NOT NULL DEFAULT '0' COMMENT '销售数量',
  `Status` tinyint(4) NOT NULL DEFAULT '1' COMMENT '状态 1=正常 2=异常',
  `BrankGuid` varchar(50) DEFAULT NULL COMMENT '品牌编号',
  `SeasonGuid` varchar(50) DEFAULT NULL COMMENT '季节编号',
  `StyleGuid` varchar(50) DEFAULT NULL COMMENT '款式编号',
  `BatchGuid` varchar(50) DEFAULT NULL COMMENT '批次编号',
  `SizeGuid` varchar(50) DEFAULT NULL COMMENT '尺码编号',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除  0=否 1=是',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpgoodssku
-- ----------------------------
INSERT INTO `erpgoodssku` VALUES ('1f1eaeac-388f-4fac-a81c-627f4c9f817f', '64fd9c02-279c-4e12-97ee-97b1c2a06783', 'LINAAAA01000090', '0100', '0090', '0', '0', '1', '9bc94f7c-5c4f-4b35-8516-fb6235e27348', 'a61ee6cc-beda-4060-8c37-4d3a774a4420', 'f6dcb7b8-711f-42d6-b5ec-d7d49e021618', '46f2ec29-8437-48eb-9daf-12ba6603dba5', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-27 21:32:50');
INSERT INTO `erpgoodssku` VALUES ('435389da-8fa5-495a-a1e3-a5ce1eaa6c23', '585056c6-5c91-4b74-8c1b-b88a25300a49', 'NIKBBBA13041200', '1304', '1200', '0', '0', '1', '0f12dca9-9c49-49a0-a82a-5e5147261e64', '10c2d55f-2115-4e6b-9104-bc8d7667ae5d', 'b91f790a-22fc-45a3-b14e-8643f0f1353f', '14a00ed2-e1c0-4869-8245-20b7e98add36', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-27 21:31:03');
INSERT INTO `erpgoodssku` VALUES ('5a1203e8-2976-404c-a71e-f1d3a6abbf38', '31f602d2-71a1-4d1a-8885-173f09b2a9b3', 'ADIACAC02300210', '0230', '0210', '0', '0', '1', '90c7c15d-9cc5-4612-a925-9834de0aeb2f', 'a61ee6cc-beda-4060-8c37-4d3a774a4420', '73ab9f29-193c-4cb7-a953-7b8fba964375', '46f2ec29-8437-48eb-9daf-12ba6603dba5', 'c2cb8776-7318-415c-8027-487a97ed4aaa', '\0', '2018-06-28 18:23:46');
INSERT INTO `erpgoodssku` VALUES ('6cd495ad-3465-45e3-90ea-65a321490a69', '0a5f0b36-c457-48d1-8f68-eda48080e6d1', 'ADIABBA02130180', '0213', '0180', '30', '0', '1', '90c7c15d-9cc5-4612-a925-9834de0aeb2f', 'a61ee6cc-beda-4060-8c37-4d3a774a4420', 'b91f790a-22fc-45a3-b14e-8643f0f1353f', '14a00ed2-e1c0-4869-8245-20b7e98add36', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-28 18:25:36');
INSERT INTO `erpgoodssku` VALUES ('a15d8107-679b-4e61-8eb0-aba2a4cf8023', '99205af7-ccaa-49a2-b934-4b064b0226dd', 'JORAABA06600550', '0660', '0550', '0', '0', '1', 'b9ece0a4-d3ce-4ebd-bf60-52895dfdc9db', 'a61ee6cc-beda-4060-8c37-4d3a774a4420', 'f6dcb7b8-711f-42d6-b5ec-d7d49e021618', '14a00ed2-e1c0-4869-8245-20b7e98add36', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-28 18:23:47');
INSERT INTO `erpgoodssku` VALUES ('c5fa40ec-d403-4fe5-b8ea-327c3fc3e25c', '5e46347c-9643-4e76-a9b0-85e93bbf0396', 'LINBBCB05600450', '0560', '0450', '0', '0', '1', '9bc94f7c-5c4f-4b35-8516-fb6235e27348', '10c2d55f-2115-4e6b-9104-bc8d7667ae5d', 'b91f790a-22fc-45a3-b14e-8643f0f1353f', 'a280b15b-ce10-4863-ba84-e61164761a58', '93e895c2-4b65-45cd-ab94-d8ccaa4ac1db', '\0', '2018-06-27 21:31:15');
INSERT INTO `erpgoodssku` VALUES ('c86ec042-8ed7-4a8a-85c1-1290c97b2ebe', 'f1a4cecc-829b-49a0-983a-aeddaff541b6', 'LINBBBA15601230', '1560', '1230', '0', '0', '1', '9bc94f7c-5c4f-4b35-8516-fb6235e27348', '10c2d55f-2115-4e6b-9104-bc8d7667ae5d', 'b91f790a-22fc-45a3-b14e-8643f0f1353f', '14a00ed2-e1c0-4869-8245-20b7e98add36', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-27 21:31:32');
INSERT INTO `erpgoodssku` VALUES ('d5c41749-c07d-4227-91b1-8f4e83bd7421', 'a433162a-7af7-4834-826d-68c54174d9ff', 'NIKBBBA15601300', '1560', '1300', '0', '0', '1', '0f12dca9-9c49-49a0-a82a-5e5147261e64', '10c2d55f-2115-4e6b-9104-bc8d7667ae5d', 'b91f790a-22fc-45a3-b14e-8643f0f1353f', '14a00ed2-e1c0-4869-8245-20b7e98add36', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-24 22:52:06');
INSERT INTO `erpgoodssku` VALUES ('e3e53984-aa87-4771-aff0-b1e2e1a244dd', '34057bbe-6165-4ded-9ac1-4b38a45fdc8d', 'ADICAAD06500550', '0650', '0550', '0', '0', '1', '90c7c15d-9cc5-4612-a925-9834de0aeb2f', '09fdf8b4-aef9-45c9-84d6-d2bfbf89fa5b', 'f6dcb7b8-711f-42d6-b5ec-d7d49e021618', '46f2ec29-8437-48eb-9daf-12ba6603dba5', '6c50d781-91ab-45f6-9aaa-dcd76c5a981e', '\0', '2018-06-27 21:32:33');
INSERT INTO `erpgoodssku` VALUES ('e8451a70-b7ff-4e3a-9d36-efdc878aeeef', 'be85fcff-d419-428b-b12b-df173fc311bd', 'JORBACA01500134', '0150', '0134', '0', '0', '1', 'b9ece0a4-d3ce-4ebd-bf60-52895dfdc9db', '10c2d55f-2115-4e6b-9104-bc8d7667ae5d', 'f6dcb7b8-711f-42d6-b5ec-d7d49e021618', 'a280b15b-ce10-4863-ba84-e61164761a58', '24ab542a-50fd-4060-bbb9-95a08425ddd5', '\0', '2018-06-27 21:30:50');
INSERT INTO `erpgoodssku` VALUES ('ede1f3d6-6de9-4a62-a207-d63e2e4f815f', 'eabd1d03-cc80-43cb-bd45-834d1f741b5e', 'LINDBCC05600450', '0560', '0450', '0', '0', '1', '9bc94f7c-5c4f-4b35-8516-fb6235e27348', '2b15a147-d1b0-4bc8-b937-21999d202d16', 'b91f790a-22fc-45a3-b14e-8643f0f1353f', 'a280b15b-ce10-4863-ba84-e61164761a58', 'c2cb8776-7318-415c-8027-487a97ed4aaa', '\0', '2018-06-27 21:32:07');

-- ----------------------------
-- Table structure for erpinoutlog
-- ----------------------------
DROP TABLE IF EXISTS `erpinoutlog`;
CREATE TABLE `erpinoutlog` (
  `Guid` varchar(50) NOT NULL,
  `Types` tinyint(4) NOT NULL DEFAULT '1' COMMENT '类型 1=入库 2=出库',
  `InTypes` tinyint(4) NOT NULL DEFAULT '1' COMMENT '入库类型  1=入库单方式  2=调拨方式',
  `PackGuid` varchar(50) NOT NULL COMMENT '出入库打包日志的编号',
  `ShopGuid` varchar(50) DEFAULT NULL COMMENT '出库商品到店铺的编号',
  `GoodsGuid` varchar(50) NOT NULL COMMENT '商品SKU的唯一编号',
  `GoodsSku` varchar(50) DEFAULT NULL,
  `GoodsSum` int(11) NOT NULL DEFAULT '0' COMMENT '出入库商品的数量',
  `AdminGuid` varchar(50) NOT NULL COMMENT '后台管理人员的编号',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '出入库的时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpinoutlog
-- ----------------------------
INSERT INTO `erpinoutlog` VALUES ('31f3094a-929c-43bc-8b4c-a6fc5c3a56f6', '2', '2', 'Transfer', '29e4fba5-47d4-49e1-b500-b494394628ee', '6cd495ad-3465-45e3-90ea-65a321490a69', 'ADIABBA02130180', '10', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', '2018-06-29 00:30:43');
INSERT INTO `erpinoutlog` VALUES ('6b741aa4-be5b-427c-bc6e-7dfdaf9507e0', '2', '1', 'ede09775-80f8-4ff6-8cef-d599c62c1b55', '5f997966-d21c-4048-9894-86a47d779c73', '6cd495ad-3465-45e3-90ea-65a321490a69', 'ADIABBA02130180', '10', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', '2018-06-29 00:30:43');
INSERT INTO `erpinoutlog` VALUES ('d2e36b8a-3f26-49d4-bb95-285ecead2813', '1', '1', '1e864555-182e-4f67-bf76-41a9008e0799', null, '6cd495ad-3465-45e3-90ea-65a321490a69', 'ADIABBA02130180', '50', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', '2018-06-29 00:30:45');

-- ----------------------------
-- Table structure for erppacklog
-- ----------------------------
DROP TABLE IF EXISTS `erppacklog`;
CREATE TABLE `erppacklog` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一编号',
  `Types` tinyint(4) NOT NULL DEFAULT '1' COMMENT '类型：2=出库打包日志  1=入库打包日志',
  `Number` varchar(30) NOT NULL COMMENT '打包订单号',
  `PackName` varchar(50) NOT NULL COMMENT '打包名称（可以是时间）',
  `GoodsSum` int(11) NOT NULL DEFAULT '0' COMMENT '当前打包商品总数量',
  `ShopGuid` varchar(50) DEFAULT NULL COMMENT '打包的商品归属商铺编号',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除 0=不未删除 1=已删除',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '打包时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erppacklog
-- ----------------------------
INSERT INTO `erppacklog` VALUES ('1e864555-182e-4f67-bf76-41a9008e0799', '1', '20180628011852185255', '测试入库', '50', null, '\0', '2018-06-28 01:18:52');
INSERT INTO `erppacklog` VALUES ('ede09775-80f8-4ff6-8cef-d599c62c1b55', '2', '20180628011925192538', '测试出库ABC', '20', '5f997966-d21c-4048-9894-86a47d779c73', '\0', '2018-06-28 01:19:25');

-- ----------------------------
-- Table structure for erppurchase
-- ----------------------------
DROP TABLE IF EXISTS `erppurchase`;
CREATE TABLE `erppurchase` (
  `Guid` varchar(50) NOT NULL COMMENT '采购单唯一编号',
  `Number` varchar(50) NOT NULL COMMENT '采购编号',
  `SupplierGuid` varchar(50) NOT NULL COMMENT '供应商',
  `Contacts` varchar(20) DEFAULT NULL COMMENT '联系人',
  `Mobile` varchar(20) DEFAULT NULL COMMENT '联系电话',
  `Money` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '采购金额',
  `DeliverCity` varchar(255) DEFAULT NULL COMMENT '交付区域',
  `DeliverDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '交付日期',
  `AdminGuid` varchar(50) NOT NULL COMMENT '操作人',
  `Attribute` text COMMENT '属性=自定义 Json对象',
  `Status` tinyint(4) NOT NULL DEFAULT '1' COMMENT '状态 1=未完成入库  2=未完成付款  3=未完成到票  4=完成',
  `IsDel` bit(1) NOT NULL COMMENT '是否删除 0=否  1=是',
  `Summary` varchar(2000) DEFAULT NULL COMMENT '备注',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '采购日期',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erppurchase
-- ----------------------------
INSERT INTO `erppurchase` VALUES ('cb108e2f-084f-4ba1-baab-b9c541489de3', 'CG-20180630-1000', 'd96e7ad5-960c-439c-a715-e088a63d43ea', '张三', '12355444444', '735', '北京/朝阳区/三环到四环之间', '2018-07-01 02:36:57', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', '[{\"Name\":\"A\",\"Value\":\"B\"},{\"Name\":\"C\",\"Value\":\"D\"}]', '4', '\0', null, '2018-07-01 02:36:57');

-- ----------------------------
-- Table structure for erppurchasegoods
-- ----------------------------
DROP TABLE IF EXISTS `erppurchasegoods`;
CREATE TABLE `erppurchasegoods` (
  `Guid` varchar(50) NOT NULL COMMENT '采购商品表唯一编号',
  `PurchaseGuid` varchar(50) DEFAULT NULL COMMENT '物品属于哪个采购单',
  `Number` varchar(50) NOT NULL COMMENT '物品编号',
  `Name` varchar(50) NOT NULL COMMENT '物品名称',
  `Specification` varchar(100) DEFAULT NULL COMMENT '规格型号',
  `Unit` varchar(20) NOT NULL COMMENT '单位',
  `Quantity` int(11) NOT NULL DEFAULT '0' COMMENT '采购数量',
  `Price` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '单价',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除 0=否 1=是',
  `Summary` varchar(500) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erppurchasegoods
-- ----------------------------
INSERT INTO `erppurchasegoods` VALUES ('821098a4-6850-496e-b7be-905a69cc265f', 'cb108e2f-084f-4ba1-baab-b9c541489de3', '1003', 'CCC', '', '箱', '1', '50.00', '\0', '');
INSERT INTO `erppurchasegoods` VALUES ('a8457a81-5ad3-4ed7-9112-48b33b298b01', 'cb108e2f-084f-4ba1-baab-b9c541489de3', '1002', 'BBBBBB', '橙色', '箱', '15', '30.00', '\0', '');
INSERT INTO `erppurchasegoods` VALUES ('f271b10e-be94-4009-b06f-4e92d3d78fc0', 'cb108e2f-084f-4ba1-baab-b9c541489de3', '1001', 'AAAAAA', '黄色', '条', '10', '23.50', '\0', '');

-- ----------------------------
-- Table structure for erppush
-- ----------------------------
DROP TABLE IF EXISTS `erppush`;
CREATE TABLE `erppush` (
  `Guid` varchar(50) NOT NULL,
  `Mode` tinyint(4) NOT NULL COMMENT '推送方式 1=全部  2=某个店铺  3=更新通知',
  `Types` tinyint(4) NOT NULL DEFAULT '1' COMMENT '1=普通消息  2=透传消息',
  `Title` varchar(50) DEFAULT NULL COMMENT '消息标题',
  `Summary` varchar(500) DEFAULT NULL COMMENT '消息内容',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erppush
-- ----------------------------
INSERT INTO `erppush` VALUES ('7ed391a6-399a-4ecb-9d16-4fe867e9e655', '1', '1', 'aaaaaaaaaa', 'abbbbbbbbbbbbbbbb', '2018-06-23 00:43:36');

-- ----------------------------
-- Table structure for erpreturngoods
-- ----------------------------
DROP TABLE IF EXISTS `erpreturngoods`;
CREATE TABLE `erpreturngoods` (
  `Guid` varchar(50) NOT NULL,
  `OrderGuid` varchar(50) DEFAULT NULL COMMENT '返货订单编号',
  `GoodsGuid` varchar(50) NOT NULL COMMENT '返货的是哪件衣服',
  `ReturnCount` int(11) NOT NULL DEFAULT '1' COMMENT '返货的数量',
  `Summary` varchar(500) DEFAULT NULL COMMENT '返货描述',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpreturngoods
-- ----------------------------

-- ----------------------------
-- Table structure for erpreturnorder
-- ----------------------------
DROP TABLE IF EXISTS `erpreturnorder`;
CREATE TABLE `erpreturnorder` (
  `Guid` varchar(50) NOT NULL,
  `Number` varchar(50) NOT NULL COMMENT '返货订单编号',
  `ShopGuid` varchar(50) DEFAULT NULL COMMENT '所属返货店铺',
  `GoodsSum` int(11) NOT NULL DEFAULT '0' COMMENT '返货数量',
  `Status` tinyint(4) NOT NULL DEFAULT '0' COMMENT '返货的状态 1=提交返货 2=受理 3=完成 4=其他',
  `StaffGuid` varchar(50) DEFAULT NULL COMMENT '操作人-员工编号',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除 0=否 1=是',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加返货时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpreturnorder
-- ----------------------------

-- ----------------------------
-- Table structure for erpsaleorder
-- ----------------------------
DROP TABLE IF EXISTS `erpsaleorder`;
CREATE TABLE `erpsaleorder` (
  `Guid` varchar(50) NOT NULL,
  `Number` varchar(50) NOT NULL COMMENT '销售订单编号',
  `ShopGuid` varchar(50) NOT NULL COMMENT '店铺编号',
  `AdminGuid` varchar(50) NOT NULL COMMENT '操作员编号',
  `UserGuid` varchar(50) DEFAULT NULL COMMENT '会员编号',
  `ActivityName` varchar(50) DEFAULT NULL COMMENT '活动名称',
  `Types` tinyint(4) NOT NULL DEFAULT '1' COMMENT '订单类型 1=普通销售/2=打折销售/3=满减销售',
  `Counts` int(11) NOT NULL DEFAULT '0' COMMENT '订单总件数',
  `Money` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT '订单金额',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '下单日期',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpsaleorder
-- ----------------------------

-- ----------------------------
-- Table structure for erpsaleordergoods
-- ----------------------------
DROP TABLE IF EXISTS `erpsaleordergoods`;
CREATE TABLE `erpsaleordergoods` (
  `Guid` varchar(50) NOT NULL,
  `OrderNumber` varchar(50) NOT NULL COMMENT '订单编号',
  `GoodsGuid` varchar(50) NOT NULL COMMENT '订单商品编号',
  `Counts` int(11) NOT NULL DEFAULT '1' COMMENT '购买数量',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpsaleordergoods
-- ----------------------------

-- ----------------------------
-- Table structure for erpshopactivity
-- ----------------------------
DROP TABLE IF EXISTS `erpshopactivity`;
CREATE TABLE `erpshopactivity` (
  `Guid` varchar(50) NOT NULL,
  `ShopGuid` varchar(50) NOT NULL COMMENT '店铺编号',
  `BrandGuid` varchar(50) DEFAULT NULL COMMENT '品牌ID',
  `Types` tinyint(4) NOT NULL DEFAULT '1' COMMENT '类型(0=全部店铺/1=按商铺/2=按品牌/3=按地区)',
  `Method` tinyint(4) NOT NULL DEFAULT '1' COMMENT '方式(1=打折/2=满减)',
  `CountNum` int(11) DEFAULT NULL COMMENT '折扣数',
  `FullBack` text COMMENT '满减Json',
  `BeginDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '开始时间',
  `EndDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '结束时间',
  `Enable` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否启用  1=启用  0=不启用',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除  0=不删除  1=删除',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '活动添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpshopactivity
-- ----------------------------
INSERT INTO `erpshopactivity` VALUES ('054acf04-433a-4c5f-8f95-7d580adfa597', 'all', '0f12dca9-9c49-49a0-a82a-5e5147261e64', '2', '1', '95', null, '2018-07-01 21:33:32', '2018-07-01 21:33:32', '', '\0', '2018-07-01 21:33:32');
INSERT INTO `erpshopactivity` VALUES ('613d2e06-b263-4ee4-b0ac-2ed4a102e834', 'all', '9bc94f7c-5c4f-4b35-8516-fb6235e27348', '0', '1', '50', null, '2018-07-01 21:34:00', '2018-07-01 21:34:00', '', '', '2018-07-01 21:34:00');
INSERT INTO `erpshopactivity` VALUES ('adb5e4ce-b3c8-43a5-bd8b-bc55df6f3be9', '29e4fba5-47d4-49e1-b500-b494394628ee', null, '1', '2', '0', '[{\"fullbegin\":100,\"fullend\":30},{\"fullbegin\":50,\"fullend\":10},{\"fullbegin\":30,\"fullend\":5}]', '2018-06-20 21:56:13', '2018-07-20 21:56:13', '', '\0', '2018-06-20 21:56:13');
INSERT INTO `erpshopactivity` VALUES ('d014260d-fe01-44dd-aa2d-62eeb3429f7a', '29e4fba5-47d4-49e1-b500-b494394628ee', null, '1', '1', '90', null, '2018-05-20 21:16:40', '2018-06-20 21:16:40', '', '\0', '2018-06-20 21:16:40');

-- ----------------------------
-- Table structure for erpshops
-- ----------------------------
DROP TABLE IF EXISTS `erpshops`;
CREATE TABLE `erpshops` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一编号',
  `LoginName` varchar(20) NOT NULL COMMENT '店铺登录账号',
  `LoginPwd` varchar(100) NOT NULL COMMENT '店铺登录密码',
  `AdminName` varchar(10) DEFAULT NULL COMMENT '负责人姓名',
  `Sex` varchar(10) DEFAULT '' COMMENT '性别',
  `ShopCover` varchar(100) DEFAULT NULL COMMENT '店铺封面图',
  `Status` tinyint(4) NOT NULL DEFAULT '1' COMMENT '审核状态 0=正常 1=账号冻结  2=停业',
  `ShopName` varchar(50) NOT NULL COMMENT '店铺名称',
  `Mobile` varchar(50) DEFAULT NULL COMMENT '负责人联系电话',
  `Tel` varchar(50) DEFAULT NULL COMMENT '座机号码',
  `ShopCity` varchar(50) DEFAULT NULL COMMENT '店铺所在区域',
  `ShopAddress` varchar(100) DEFAULT NULL COMMENT '店铺详细地址',
  `ShopLogo` varchar(255) DEFAULT NULL COMMENT '店铺Logo',
  `Summary` varchar(500) DEFAULT NULL COMMENT '备注',
  `RegDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '注册时间',
  `LoginCount` int(11) NOT NULL DEFAULT '0' COMMENT '登录次数',
  `LastLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '最后登录时间',
  `UpLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '上次登录时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpshops
-- ----------------------------
INSERT INTO `erpshops` VALUES ('29e4fba5-47d4-49e1-b500-b494394628ee', 'testzh', 'pPo9vFeTWOCF0oLKKdX9Jw==', '张三', '男', null, '1', '河北张家口店', '13888888888', '010-88888888', '河北/张家口市/阳原县/辛堡乡', '测试详细地址', null, null, '2018-06-20 15:00:41', '0', null, null);
INSERT INTO `erpshops` VALUES ('5f997966-d21c-4048-9894-86a47d779c73', '2233222', 'pPo9vFeTWOCF0oLKKdX9Jw==', '刘向荣', '男', null, '0', '刘向荣河西店', '13876545678', '010-88888888', '天津/河西区/全境', null, null, null, '2018-06-27 15:35:21', '0', null, null);

-- ----------------------------
-- Table structure for erpshopuser
-- ----------------------------
DROP TABLE IF EXISTS `erpshopuser`;
CREATE TABLE `erpshopuser` (
  `Guid` varchar(50) NOT NULL,
  `ShopGuid` varchar(50) NOT NULL COMMENT '归属于某个店铺',
  `UserName` varchar(20) NOT NULL COMMENT '会员姓名',
  `Mobile` varchar(20) NOT NULL COMMENT '手机号码',
  `LoginPwd` varchar(50) NOT NULL COMMENT '登录密码',
  `Points` int(11) NOT NULL DEFAULT '0' COMMENT '积分数',
  `Sex` varchar(5) DEFAULT NULL COMMENT '性别',
  `Birthday` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '生日',
  `Status` tinyint(4) NOT NULL DEFAULT '0' COMMENT '状态 0=正常 1=账号冻结',
  `RegDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '注册时间',
  `LoginCount` int(11) NOT NULL DEFAULT '0' COMMENT '登录次数',
  `LastLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '最后登录日期',
  `UpLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '上次登录日期',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpshopuser
-- ----------------------------
INSERT INTO `erpshopuser` VALUES ('dc4c6934-8606-4633-9350-aed41c7b8ef9', '29e4fba5-47d4-49e1-b500-b494394628ee', '张三', '13866666666', 'ee4td/28wS2cEL04Hez5oA==', '0', '男', null, '0', '2018-06-20 20:25:27', '0', null, null);

-- ----------------------------
-- Table structure for erpstaff
-- ----------------------------
DROP TABLE IF EXISTS `erpstaff`;
CREATE TABLE `erpstaff` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一编号',
  `ShopGuid` varchar(50) DEFAULT NULL COMMENT '所属店铺（可以是总店）',
  `LoginName` varchar(20) NOT NULL COMMENT '员工登录账号',
  `LoginPwd` varchar(50) NOT NULL COMMENT '登录密码',
  `TrueName` varchar(10) NOT NULL COMMENT '真实姓名',
  `Mobile` varchar(20) DEFAULT NULL COMMENT '手机号码',
  `Sex` varchar(5) DEFAULT NULL COMMENT '性别',
  `Status` tinyint(4) NOT NULL COMMENT '状态 0=正常 1=账号冻结 2=离职',
  `IsDevice` tinyint(4) NOT NULL DEFAULT '0' COMMENT ' 0=苹果 1=安卓',
  `DeviceName` varchar(50) DEFAULT NULL COMMENT '设备类型名称，是苹果还是安卓',
  `Token` varchar(500) DEFAULT NULL COMMENT '设备发送通知用的Token',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  `LoginCount` int(11) NOT NULL DEFAULT '0' COMMENT '登录次数',
  `LastLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '最后一次登录时间',
  `UpLoginDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '上次登录时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpstaff
-- ----------------------------
INSERT INTO `erpstaff` VALUES ('21576fef-1a5b-4af8-a394-ec5166b4a8e5', '29e4fba5-47d4-49e1-b500-b494394628ee', '13888888888', 'pPo9vFeTWOCF0oLKKdX9Jw==', '张三', '13888888888', '男', '0', '1', '小米', 'aaaaaaaaaaaaaaaaaaaa', '2018-07-06 11:23:35', '1', '2018-07-06 11:23:35', '2018-07-06 11:23:35');
INSERT INTO `erpstaff` VALUES ('21576fef-1a5b-4af8-a394-ec5166b4a8e8', '21576fef-1a5b-4af8-a394-ec5166b4a810', '1366666666', 'pPo9vFeTWOCF0oLKKdX9Jw==', '李四', '13666666666', '女', '0', '1', null, null, '2018-07-06 11:08:59', '0', '2018-07-06 11:08:59', '2018-07-06 11:08:59');

-- ----------------------------
-- Table structure for erpsupplier
-- ----------------------------
DROP TABLE IF EXISTS `erpsupplier`;
CREATE TABLE `erpsupplier` (
  `Guid` varchar(50) NOT NULL COMMENT '采购供应商唯一编号',
  `Name` varchar(50) NOT NULL COMMENT '供应商名称',
  `Contacts` varchar(50) NOT NULL COMMENT '联系人',
  `Mobile` varchar(15) DEFAULT NULL COMMENT '联系电话',
  `Email` varchar(50) DEFAULT NULL COMMENT '联系邮箱',
  `Attribute` text COMMENT '属性=自定义 Json对象',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除  0=否 1=是',
  `Summary` text COMMENT '备注',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erpsupplier
-- ----------------------------
INSERT INTO `erpsupplier` VALUES ('d96e7ad5-960c-439c-a715-e088a63d43ea', '测试供应商', '张三', '12355444444', null, '[{\"Name\":\"aaaa\",\"Value\":\"bbbb\"}]', '\0', null, '2018-07-01 02:46:56');

-- ----------------------------
-- Table structure for erptransfer
-- ----------------------------
DROP TABLE IF EXISTS `erptransfer`;
CREATE TABLE `erptransfer` (
  `Guid` varchar(50) NOT NULL,
  `Number` varchar(50) NOT NULL COMMENT '编号',
  `InShopGuid` varchar(50) NOT NULL COMMENT '入库加盟商',
  `OutShopGuid` varchar(50) NOT NULL COMMENT '出库加盟商',
  `GoodsSum` int(11) NOT NULL DEFAULT '0' COMMENT '商品数量',
  `AdminGuid` varchar(50) DEFAULT NULL COMMENT '操作人',
  `IsDel` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除',
  `AddDate` datetime NOT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT '添加时间',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erptransfer
-- ----------------------------
INSERT INTO `erptransfer` VALUES ('91e333f8-ef48-4f3f-af98-193e02e7f223', '20180627154029402975', '29e4fba5-47d4-49e1-b500-b494394628ee', '5f997966-d21c-4048-9894-86a47d779c73', '10', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', '\0', '2018-06-27 15:40:29');

-- ----------------------------
-- Table structure for erptransfergoods
-- ----------------------------
DROP TABLE IF EXISTS `erptransfergoods`;
CREATE TABLE `erptransfergoods` (
  `Guid` varchar(50) NOT NULL COMMENT '唯一编号',
  `TransferGuid` varchar(50) NOT NULL COMMENT '调拨单编号',
  `GoodsGuid` varchar(50) NOT NULL COMMENT '商品编号',
  `GoodsSum` int(11) NOT NULL DEFAULT '0' COMMENT '商品数量',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of erptransfergoods
-- ----------------------------
INSERT INTO `erptransfergoods` VALUES ('46dee8ab-a8df-4dcf-9660-cee5a7856494', '91e333f8-ef48-4f3f-af98-193e02e7f223', '6cd495ad-3465-45e3-90ea-65a321490a69', '10');

-- ----------------------------
-- Table structure for sysadmin
-- ----------------------------
DROP TABLE IF EXISTS `sysadmin`;
CREATE TABLE `sysadmin` (
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

-- ----------------------------
-- Records of sysadmin
-- ----------------------------
INSERT INTO `sysadmin` VALUES ('12cc96cf-7ccf-430b-a54a-e1c6f04690cb', null, '商务中心', '52523a76-52b3-4c25-a1bd-9123a011f2a8', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,52523a76-52b3-4c25-a1bd-9123a011f2a8,', 'admins', 'pPo9vFeTWOCF0oLKKdX9Jw==', '张三', '1101', '/themes/img/avatar.jpg', '男', '13888888888', '', null, null, '2018-06-13 21:43:43', '2018-07-07 20:36:21', '2018-07-07 20:36:21');
INSERT INTO `sysadmin` VALUES ('30d3da88-bb72-4ace-a303-b3aae0ecb732', null, '事业发展部', '4b6ab27f-c0fa-483d-9b5a-55891ee8d727', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,4b6ab27f-c0fa-483d-9b5a-55891ee8d727,', 'testadmin', 'pPo9vFeTWOCF0oLKKdX9Jw==', '李四', '1002', '/themes/img/avatar.jpg', '男', null, '\0', null, null, '2018-06-16 23:35:36', null, null);

-- ----------------------------
-- Table structure for sysbtnfun
-- ----------------------------
DROP TABLE IF EXISTS `sysbtnfun`;
CREATE TABLE `sysbtnfun` (
  `Guid` varchar(50) NOT NULL,
  `MenuGuid` varchar(50) NOT NULL,
  `Name` varchar(20) NOT NULL COMMENT '功能名称',
  `FunType` varchar(20) NOT NULL COMMENT '功能标识名称',
  `Summary` varchar(500) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sysbtnfun
-- ----------------------------
INSERT INTO `sysbtnfun` VALUES ('6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61', '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', '新增', 'Add', null);
INSERT INTO `sysbtnfun` VALUES ('931bd729-f160-4fe3-bb7c-7828a2da047a', '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', '修改', 'Edit', null);
INSERT INTO `sysbtnfun` VALUES ('c4261103-dfc7-46e5-ab20-4ca5fc7729f6', '5ce13ead-971b-4ed4-ad5f-acacccd82381', '删除', 'Delete', null);

-- ----------------------------
-- Table structure for syscode
-- ----------------------------
DROP TABLE IF EXISTS `syscode`;
CREATE TABLE `syscode` (
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

-- ----------------------------
-- Records of syscode
-- ----------------------------
INSERT INTO `syscode` VALUES ('00fca0bf-e26c-4a17-b0bb-29fd3c2ad701', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1011', '台盟盟员', null, '24', '', null, '2018-03-29 12:30:43', '2018-03-29 12:30:43');
INSERT INTO `syscode` VALUES ('042e6708-3303-4fa0-8cf1-2958d5144cd0', '1f1db281-ce59-42ca-9647-26f7fb882b2e', '1002', '经济与贸易', null, '46', '', null, '2018-03-30 14:24:04', '2018-03-30 14:24:04');
INSERT INTO `syscode` VALUES ('080e5a5c-3f46-49c6-a77c-e6be211cc7ce', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'G', '春秋装', null, '58', '', null, '2018-06-24 21:30:47', '2018-06-24 21:30:47');
INSERT INTO `syscode` VALUES ('0975c3e9-c1e5-4864-bc9c-80de58c37c9d', 'e9b04253-49a3-47bc-82e2-53d506fda641', '1003', '满族', null, '29', '', null, '2018-03-29 12:39:19', '2018-03-29 12:39:19');
INSERT INTO `syscode` VALUES ('09fdf8b4-aef9-45c9-84d6-d2bfbf89fa5b', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'C', '秋装', null, '54', '', null, '2018-06-24 21:30:11', '2018-06-24 21:30:11');
INSERT INTO `syscode` VALUES ('0d8bbb2b-dc2d-4744-ac95-6a45ca6a1963', 'e9b04253-49a3-47bc-82e2-53d506fda641', '1002', '蒙古族', null, '28', '', null, '2018-03-29 12:39:12', '2018-03-29 12:39:12');
INSERT INTO `syscode` VALUES ('0f12dca9-9c49-49a0-a82a-5e5147261e64', '7b664e3e-f58a-4e66-8c0f-be1458541d14', 'NIK', 'NIK耐克', null, '49', '', null, '2018-06-24 21:29:08', '2018-06-24 21:29:08');
INSERT INTO `syscode` VALUES ('10c2d55f-2115-4e6b-9104-bc8d7667ae5d', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'B', '夏装', null, '53', '', null, '2018-06-24 21:30:01', '2018-06-24 21:30:01');
INSERT INTO `syscode` VALUES ('14a00ed2-e1c0-4869-8245-20b7e98add36', '2e0393f9-e6d6-4c15-98cf-96072f0d3d70', 'B', 'B', null, '64', '', null, '2018-06-24 21:32:02', '2018-06-24 21:32:02');
INSERT INTO `syscode` VALUES ('1537252f-88cd-4189-a7e8-bd646c602f5c', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1008', '博士', null, '10', '', null, '2018-05-06 10:25:24', '2018-05-06 10:25:25');
INSERT INTO `syscode` VALUES ('18d55180-c690-4f82-bf81-0e6b5e61dada', '6ceddac9-c24a-4e36-bcc3-33d31a2d737e', '1001', '国立', null, '42', '', null, '2018-03-30 14:23:07', '2018-03-30 14:23:07');
INSERT INTO `syscode` VALUES ('1a0b73f2-3ed6-4818-9bc0-5f4960f58703', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1002', '初中', null, '4', '', null, '2018-03-29 12:26:54', '2018-03-29 12:26:54');
INSERT INTO `syscode` VALUES ('1f722c76-cc36-416e-855f-7a2c4ecac12f', 'd14fb7c9-e867-467e-94fa-9f1a94692b88', '1003', '日语', null, '13', '', null, '2018-03-29 13:51:50', '2018-03-29 13:51:51');
INSERT INTO `syscode` VALUES ('21ba8e52-3336-41d7-9567-dc376f7e44ec', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1001', '中共党员', null, '14', '', null, '2018-03-29 12:29:38', '2018-03-29 12:29:39');
INSERT INTO `syscode` VALUES ('24ab542a-50fd-4060-bbb9-95a08425ddd5', 'e86cf108-dc4d-4532-8cce-fdb041363902', 'A', 'XXL', null, '69', '', null, '2018-06-24 21:34:40', '2018-06-24 21:34:40');
INSERT INTO `syscode` VALUES ('27a399ca-9415-4a39-84c7-88cae944ca25', 'e9b04253-49a3-47bc-82e2-53d506fda641', '1001', '汉族', null, '27', '', null, '2018-03-29 12:38:36', '2018-03-29 12:38:36');
INSERT INTO `syscode` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', '2128dd06-6414-44e4-ae83-502f58d886d2', '1001', '已婚', null, '37', '', null, '2018-03-29 12:41:10', '2018-03-29 12:41:10');
INSERT INTO `syscode` VALUES ('2999af31-6d30-4c9e-a8e2-9331c537b361', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1004', '高中', null, '6', '', null, '2018-03-29 12:27:07', '2018-03-29 12:27:07');
INSERT INTO `syscode` VALUES ('29ee3b9c-b426-4e11-a3a9-f8ba2cfa1a19', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1003', '共青团员', null, '16', '', null, '2018-03-29 12:29:53', '2018-03-29 12:29:53');
INSERT INTO `syscode` VALUES ('2b15a147-d1b0-4bc8-b937-21999d202d16', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'D', '冬装', null, '55', '', null, '2018-06-24 21:30:21', '2018-06-24 21:30:21');
INSERT INTO `syscode` VALUES ('2d42a5cc-9911-4807-9b1e-b29595dacb9b', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1007', '硕士', null, '9', '', null, '2018-03-29 12:27:24', '2018-03-29 12:27:24');
INSERT INTO `syscode` VALUES ('2f3fcc5e-737b-4ae8-8e8d-4a9b0032453c', '1f1db281-ce59-42ca-9647-26f7fb882b2e', '1001', '计算机专业', null, '45', '', null, '2018-03-30 14:23:33', '2018-03-30 14:23:33');
INSERT INTO `syscode` VALUES ('331dcdc9-a57a-42a3-9f5e-49fb156fd030', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1012', '无党派人士', null, '25', '', null, '2018-03-29 12:30:58', '2018-03-29 12:30:58');
INSERT INTO `syscode` VALUES ('36821613-cdf3-423b-a430-2d882b8e2d44', '2e0393f9-e6d6-4c15-98cf-96072f0d3d70', '2', '2', null, '68', '', null, '2018-06-24 21:32:13', '2018-06-24 21:32:13');
INSERT INTO `syscode` VALUES ('3c9cae16-42dd-482b-804a-da75b3d7a160', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1006', '民建会员', null, '19', '', null, '2018-03-29 12:30:09', '2018-03-29 12:30:09');
INSERT INTO `syscode` VALUES ('439083c4-c62d-459f-9564-c9ae85fd5320', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1001', '小学', null, '3', '', null, '2018-05-06 00:52:41', '2018-05-06 00:52:42');
INSERT INTO `syscode` VALUES ('46f2ec29-8437-48eb-9daf-12ba6603dba5', '2e0393f9-e6d6-4c15-98cf-96072f0d3d70', 'A', 'A', null, '63', '', null, '2018-06-24 21:31:59', '2018-06-24 21:31:59');
INSERT INTO `syscode` VALUES ('484a55b2-410b-428a-bdaa-54f6f48d4219', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1009', '致公党党员', null, '22', '', null, '2018-03-29 12:30:27', '2018-03-29 12:30:27');
INSERT INTO `syscode` VALUES ('4c8cbfd1-5aad-4019-98cd-3c9dfd667257', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1008', '农工党党员', null, '21', '', null, '2018-03-29 12:30:21', '2018-03-29 12:30:21');
INSERT INTO `syscode` VALUES ('515ba6ca-4f37-48ad-b88c-0a444ab13d49', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1005', '价值观信仰', null, '34', '', '道德主义、律法主义、自由主义、功利主义、拜金主义', '2018-03-29 12:40:38', '2018-03-29 12:40:38');
INSERT INTO `syscode` VALUES ('6c50d781-91ab-45f6-9aaa-dcd76c5a981e', 'e86cf108-dc4d-4532-8cce-fdb041363902', 'D', 'M', null, '72', '', null, '2018-06-24 21:35:05', '2018-06-24 21:35:05');
INSERT INTO `syscode` VALUES ('72fd052f-15e7-499b-9bc1-7eee1b5d6f8c', '59dd330c-13a5-4ef8-9e86-918e95b5b1e2', '1001', '男', null, '1', '', null, '2018-07-01 21:20:13', '2018-07-01 21:20:14');
INSERT INTO `syscode` VALUES ('73ab9f29-193c-4cb7-a953-7b8fba964375', '48458681-48b0-490a-a840-0ffcbe49f5d4', 'C', '上衣', null, '62', '', null, '2018-06-24 21:31:46', '2018-06-24 21:31:46');
INSERT INTO `syscode` VALUES ('74a40e09-36bf-4311-808d-f8fb7142374a', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1002', '政治信仰', null, '31', '', '三民主义、共产主义、自由民主、天赋人权', '2018-03-29 12:40:16', '2018-03-29 12:40:17');
INSERT INTO `syscode` VALUES ('74d07de9-2ba5-4bf0-b9a4-5061321d6b48', 'd14fb7c9-e867-467e-94fa-9f1a94692b88', '1005', '葡萄言语', null, '41', '', null, '2018-05-06 22:07:49', '2018-05-06 22:07:50');
INSERT INTO `syscode` VALUES ('775b3811-a752-41a8-96e0-6369a9c7d281', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'F', '秋冬装', null, '57', '', null, '2018-06-24 21:30:37', '2018-06-24 21:30:37');
INSERT INTO `syscode` VALUES ('77ef89a8-649d-43cd-af36-d47a7336c8d1', '2e0393f9-e6d6-4c15-98cf-96072f0d3d70', '1', '1', null, '67', '', null, '2018-06-24 21:32:11', '2018-06-24 21:32:11');
INSERT INTO `syscode` VALUES ('7d209f2e-e7b8-4a91-819e-a955c593ec85', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1006', '包', null, '78', '', null, '2018-06-30 19:29:11', '2018-06-30 19:29:11');
INSERT INTO `syscode` VALUES ('800bd8d9-eccd-4e14-8642-e30046e835b2', '2128dd06-6414-44e4-ae83-502f58d886d2', '1003', '保密', null, '39', '', null, '2018-03-29 12:41:28', '2018-03-29 12:41:28');
INSERT INTO `syscode` VALUES ('8df42099-7001-41ee-9e28-736b1ab6d71f', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1003', '中专', null, '5', '', null, '2018-03-29 12:27:02', '2018-03-29 12:27:02');
INSERT INTO `syscode` VALUES ('8e37481c-db25-4b7a-929b-0f0a5bd05e5d', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1006', '本科', null, '8', '', null, '2018-03-29 12:27:19', '2018-03-29 12:27:19');
INSERT INTO `syscode` VALUES ('90c7c15d-9cc5-4612-a925-9834de0aeb2f', '7b664e3e-f58a-4e66-8c0f-be1458541d14', 'ADI', 'ADI阿迪', null, '48', '', 'ADI阿迪', '2018-06-24 21:28:44', '2018-06-24 21:28:44');
INSERT INTO `syscode` VALUES ('93e895c2-4b65-45cd-ab94-d8ccaa4ac1db', 'e86cf108-dc4d-4532-8cce-fdb041363902', 'B', 'XS', null, '70', '', null, '2018-06-24 21:34:49', '2018-06-24 21:34:49');
INSERT INTO `syscode` VALUES ('9593506f-193a-4503-8929-e88cb13be728', '6ceddac9-c24a-4e36-bcc3-33d31a2d737e', '1002', '私立', null, '43', '', null, '2018-03-30 14:23:16', '2018-03-30 14:23:16');
INSERT INTO `syscode` VALUES ('9b5b8286-3021-4809-addd-18a334076518', '59dd330c-13a5-4ef8-9e86-918e95b5b1e2', '1002', '女', null, '2', '', null, '2018-05-07 21:58:21', '2018-05-07 21:58:22');
INSERT INTO `syscode` VALUES ('9bc94f7c-5c4f-4b35-8516-fb6235e27348', '7b664e3e-f58a-4e66-8c0f-be1458541d14', 'LIN', 'LIN李宁', null, '50', '', null, '2018-06-24 21:29:22', '2018-06-24 21:29:22');
INSERT INTO `syscode` VALUES ('9bcb2638-6fa8-4dba-8240-4d70566f5ae9', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'H', '四季装', null, '59', '', null, '2018-06-24 21:30:56', '2018-06-24 21:30:56');
INSERT INTO `syscode` VALUES ('9d484458-5dad-4233-b18e-c5d37f185f13', 'd14fb7c9-e867-467e-94fa-9f1a94692b88', '1002', '英语', null, '12', '', null, '2018-03-29 13:51:45', '2018-03-29 13:51:46');
INSERT INTO `syscode` VALUES ('a280b15b-ce10-4863-ba84-e61164761a58', '2e0393f9-e6d6-4c15-98cf-96072f0d3d70', 'C', 'C', null, '65', '', null, '2018-06-24 21:32:05', '2018-06-24 21:32:05');
INSERT INTO `syscode` VALUES ('a61ee6cc-beda-4060-8c37-4d3a774a4420', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'A', '春装', null, '52', '', null, '2018-06-24 21:29:54', '2018-06-24 21:29:54');
INSERT INTO `syscode` VALUES ('a773fdd3-952c-4b15-bff5-19e92462863a', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1001', '神学信仰', null, '30', '', '一神信仰、多神信仰、泛神信仰、无神信仰', '2018-03-29 12:40:20', '2018-03-29 12:40:20');
INSERT INTO `syscode` VALUES ('ad01ceee-5394-444c-9a0a-00dfbb7c6d0a', 'b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '1005', '大专', null, '7', '', null, '2018-03-29 12:27:14', '2018-03-29 12:27:14');
INSERT INTO `syscode` VALUES ('b07e654b-92a4-4067-90e2-f2ce39ca4657', '2e0393f9-e6d6-4c15-98cf-96072f0d3d70', 'D', 'D', null, '66', '', null, '2018-06-24 21:32:08', '2018-06-24 21:32:08');
INSERT INTO `syscode` VALUES ('b614c223-d776-4045-a729-dc18294b190e', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1004', '公斤', null, '76', '', null, '2018-06-30 19:28:56', '2018-06-30 19:28:56');
INSERT INTO `syscode` VALUES ('b6ec418c-738e-4fbd-92a3-1805fc14bb72', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1004', '科学信仰', null, '33', '', '进化论、设计论、年前地球论、年老地球论、无限膨胀论', '2018-03-29 12:40:29', '2018-03-29 12:40:29');
INSERT INTO `syscode` VALUES ('b91f790a-22fc-45a3-b14e-8643f0f1353f', '48458681-48b0-490a-a840-0ffcbe49f5d4', 'B', '连衣裙', null, '61', '', null, '2018-06-24 21:31:39', '2018-06-24 21:31:39');
INSERT INTO `syscode` VALUES ('b9ece0a4-d3ce-4ebd-bf60-52895dfdc9db', '7b664e3e-f58a-4e66-8c0f-be1458541d14', 'JOR', 'JOR乔丹', null, '51', '', null, '2018-06-24 21:29:33', '2018-06-24 21:29:33');
INSERT INTO `syscode` VALUES ('ba317503-58fe-42fc-ba6e-1bda17cbb2d7', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1007', '民进会员', null, '20', '', null, '2018-03-29 12:30:16', '2018-03-29 12:30:16');
INSERT INTO `syscode` VALUES ('ba45cbe2-3b61-4e20-b344-cab885d87554', '8cb134d5-979b-40e2-b453-aeee265f4ab2', 'E', '春夏装', null, '56', '', null, '2018-06-24 21:30:29', '2018-06-24 21:30:29');
INSERT INTO `syscode` VALUES ('ba90d75a-5d8a-4dac-9458-f46d12511d57', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1003', '条', null, '75', '', null, '2018-06-30 19:28:50', '2018-06-30 19:28:50');
INSERT INTO `syscode` VALUES ('bdf398e0-cd54-49a4-8623-4262309fa7d6', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1006', '社会观信仰', null, '35', '', '自我主义、社群主义、爱国主义、民族主义、国际主义', '2018-03-29 12:40:49', '2018-03-29 12:40:49');
INSERT INTO `syscode` VALUES ('bf69cf81-d484-4fc8-bd2e-da88c737c799', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1013', '群众', null, '26', '', null, '2018-03-29 12:31:08', '2018-03-29 12:31:08');
INSERT INTO `syscode` VALUES ('c2cb8776-7318-415c-8027-487a97ed4aaa', 'e86cf108-dc4d-4532-8cce-fdb041363902', 'C', 'S', null, '71', '', null, '2018-06-24 21:34:57', '2018-06-24 21:34:57');
INSERT INTO `syscode` VALUES ('c485c665-8997-4105-9ecb-dc997bff9c73', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1002', '中共预备党员', null, '15', '', null, '2018-03-29 12:29:47', '2018-03-29 12:29:47');
INSERT INTO `syscode` VALUES ('c7f0ea3d-42c1-4e7f-8102-402ac55c0b01', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1007', '套', null, '79', '', null, '2018-06-30 19:29:26', '2018-06-30 19:29:26');
INSERT INTO `syscode` VALUES ('ca3a1e29-1864-4747-8a29-39bdedc466d3', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1004', '民革党员', null, '17', '', null, '2018-03-29 12:29:58', '2018-03-29 12:29:58');
INSERT INTO `syscode` VALUES ('cb4c0674-7646-4c4e-8404-35bdd50e2fb6', 'd14fb7c9-e867-467e-94fa-9f1a94692b88', '1001', '汉语', null, '11', '', null, '2018-03-29 13:51:40', '2018-03-29 13:51:41');
INSERT INTO `syscode` VALUES ('d79c9866-de4a-4a5c-a243-4ab79783a860', '2128dd06-6414-44e4-ae83-502f58d886d2', '1002', '未婚', null, '38', '', null, '2018-03-29 12:41:17', '2018-03-29 12:41:17');
INSERT INTO `syscode` VALUES ('d90d8b10-b4a4-459b-a976-1ae911996cd7', '6ceddac9-c24a-4e36-bcc3-33d31a2d737e', '1003', '职业培训', null, '44', '', null, '2018-03-30 14:23:24', '2018-03-30 14:23:24');
INSERT INTO `syscode` VALUES ('dab7d485-278d-460f-a722-ee6311d51d23', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1001', '件', null, '73', '', null, '2018-06-30 19:28:37', '2018-06-30 19:28:37');
INSERT INTO `syscode` VALUES ('dbbf8787-343c-4962-bdf2-e5e36944f51f', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1005', '箱', null, '77', '', null, '2018-06-30 19:29:06', '2018-06-30 19:29:06');
INSERT INTO `syscode` VALUES ('e02c378f-9ca4-409c-9d44-c340944560b6', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1007', '财富观信仰', null, '36', '', '按劳分配、按需分配、平均分配、计效分配', '2018-03-29 12:40:59', '2018-03-29 12:40:59');
INSERT INTO `syscode` VALUES ('e4d0d874-126c-4ac4-aa99-f5c7eebbd2b5', 'd14fb7c9-e867-467e-94fa-9f1a94692b88', '1004', '法语', null, '40', '', null, '2018-03-29 13:52:00', '2018-03-29 13:52:00');
INSERT INTO `syscode` VALUES ('ea8b94e5-4bf9-4e7d-b861-83e8385a53c7', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1005', '民盟盟员', null, '18', '', null, '2018-03-29 12:30:03', '2018-03-29 12:30:03');
INSERT INTO `syscode` VALUES ('ef24276c-173c-4912-8d85-62d89956b79d', '36eefb08-1a44-4989-b4dc-e7be65e32349', '1003', '哲学信仰', null, '32', '', '唯物主义、唯心主义、虚空主义', '2018-03-29 12:40:11', '2018-03-29 12:40:11');
INSERT INTO `syscode` VALUES ('ef5df705-7ce0-4d08-ade0-42d6c9af48a9', '7088d9b9-6692-4fc7-a83c-da580f1407c3', '1002', '个', null, '74', '', null, '2018-06-30 19:28:42', '2018-06-30 19:28:42');
INSERT INTO `syscode` VALUES ('f51ad49a-fcd3-48ee-b2b8-ac789b732dd4', '1f1db281-ce59-42ca-9647-26f7fb882b2e', '1003', '政治学', null, '47', '', null, '2018-03-30 14:24:17', '2018-03-30 14:24:17');
INSERT INTO `syscode` VALUES ('f57a726d-dd3e-4f65-af19-73ba9a71d483', '21576fef-1a5b-4af8-a394-ec5166b4a8e4', '1010', '九三学社社员', null, '23', '', null, '2018-03-29 12:30:33', '2018-03-29 12:30:33');
INSERT INTO `syscode` VALUES ('f6dcb7b8-711f-42d6-b5ec-d7d49e021618', '48458681-48b0-490a-a840-0ffcbe49f5d4', 'A', 'T恤', null, '60', '', null, '2018-06-24 21:31:30', '2018-06-24 21:31:30');

-- ----------------------------
-- Table structure for syscodetype
-- ----------------------------
DROP TABLE IF EXISTS `syscodetype`;
CREATE TABLE `syscodetype` (
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

-- ----------------------------
-- Records of syscodetype
-- ----------------------------
INSERT INTO `syscodetype` VALUES ('1f1db281-ce59-42ca-9647-26f7fb882b2e', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '专业', '11', '2018-03-30 14:22:39', '2018-03-30 14:22:39', null);
INSERT INTO `syscodetype` VALUES ('2128dd06-6414-44e4-ae83-502f58d886d2', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '婚姻', '10', '2018-03-29 12:28:33', '2018-03-29 12:28:33', null);
INSERT INTO `syscodetype` VALUES ('21576fef-1a5b-4af8-a394-ec5166b4a8e4', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '政治面貌', '7', '2018-03-29 12:28:14', '2018-03-29 12:28:14', null);
INSERT INTO `syscodetype` VALUES ('2bbdd992-862e-476c-b54f-00c9e50dab61', null, '0', '员工字典', '1', '2018-03-26 12:37:03', '2018-03-26 12:37:03', null);
INSERT INTO `syscodetype` VALUES ('2e0393f9-e6d6-4c15-98cf-96072f0d3d70', '8d3158d6-e179-4046-99e9-53eb8c04ddb1', '1', '批次', '15', '2018-06-18 06:38:03', '2018-06-18 06:38:03', null);
INSERT INTO `syscodetype` VALUES ('36eefb08-1a44-4989-b4dc-e7be65e32349', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '信仰', '9', '2018-03-29 12:28:27', '2018-03-29 12:28:27', null);
INSERT INTO `syscodetype` VALUES ('48458681-48b0-490a-a840-0ffcbe49f5d4', '8d3158d6-e179-4046-99e9-53eb8c04ddb1', '1', '款式', '14', '2018-06-18 06:37:55', '2018-06-18 06:37:55', null);
INSERT INTO `syscodetype` VALUES ('59dd330c-13a5-4ef8-9e86-918e95b5b1e2', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '性别', '2', '2018-03-26 12:37:05', '2018-03-26 12:37:05', null);
INSERT INTO `syscodetype` VALUES ('6ceddac9-c24a-4e36-bcc3-33d31a2d737e', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '院校类型', '12', '2018-03-30 14:22:54', '2018-03-30 14:22:54', null);
INSERT INTO `syscodetype` VALUES ('7088d9b9-6692-4fc7-a83c-da580f1407c3', '9d7643f0-d739-4342-b2da-45781b62ddd0', '1', '采购商品单位', '18', '2018-06-30 19:28:13', '2018-06-30 19:28:13', null);
INSERT INTO `syscodetype` VALUES ('7b664e3e-f58a-4e66-8c0f-be1458541d14', '8d3158d6-e179-4046-99e9-53eb8c04ddb1', '1', '品牌', '5', '2018-06-18 06:21:54', '2018-06-18 06:21:54', null);
INSERT INTO `syscodetype` VALUES ('8cb134d5-979b-40e2-b453-aeee265f4ab2', '8d3158d6-e179-4046-99e9-53eb8c04ddb1', '1', '季节', '13', '2018-06-18 06:35:32', '2018-06-18 06:35:32', null);
INSERT INTO `syscodetype` VALUES ('8d3158d6-e179-4046-99e9-53eb8c04ddb1', null, '0', '服装SKU', '4', '2018-06-18 06:21:45', '2018-06-18 06:21:46', null);
INSERT INTO `syscodetype` VALUES ('9d7643f0-d739-4342-b2da-45781b62ddd0', null, '0', '采购字典', '17', '2018-06-30 19:28:02', '2018-06-30 19:28:02', null);
INSERT INTO `syscodetype` VALUES ('b3767d9d-2ecc-48c5-b56d-d9af628c0c63', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '学历', '3', '2018-03-26 21:08:13', '2018-03-26 21:08:13', null);
INSERT INTO `syscodetype` VALUES ('d14fb7c9-e867-467e-94fa-9f1a94692b88', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '语言能力', '6', '2018-03-29 12:27:58', '2018-03-29 12:27:58', null);
INSERT INTO `syscodetype` VALUES ('e86cf108-dc4d-4532-8cce-fdb041363902', '8d3158d6-e179-4046-99e9-53eb8c04ddb1', '1', '尺码', '16', '2018-06-18 06:38:09', '2018-06-18 06:38:09', null);
INSERT INTO `syscodetype` VALUES ('e9b04253-49a3-47bc-82e2-53d506fda641', '2bbdd992-862e-476c-b54f-00c9e50dab61', '1', '民族', '8', '2018-03-29 12:28:20', '2018-03-29 12:28:20', null);

-- ----------------------------
-- Table structure for sysimage
-- ----------------------------
DROP TABLE IF EXISTS `sysimage`;
CREATE TABLE `sysimage` (
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

-- ----------------------------
-- Records of sysimage
-- ----------------------------

-- ----------------------------
-- Table structure for syslog
-- ----------------------------
DROP TABLE IF EXISTS `syslog`;
CREATE TABLE `syslog` (
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

-- ----------------------------
-- Records of syslog
-- ----------------------------
INSERT INTO `syslog` VALUES ('02dcf71f-dd51-41b3-b343-b78a6bd5ec05', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-05 17:44:19');
INSERT INTO `syslog` VALUES ('03a70eaa-a7f3-4837-9d10-b35e70b90567', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 14:35:56');
INSERT INTO `syslog` VALUES ('05237e11-988f-4fbc-b05a-689f48a245c5', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 18:32:49');
INSERT INTO `syslog` VALUES ('0551e4af-15c3-45a7-962d-3d50edab413a', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 16:15:12');
INSERT INTO `syslog` VALUES ('064bb09c-303a-41aa-bf9e-8bc7f190b81b', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-29 22:21:16');
INSERT INTO `syslog` VALUES ('109ab36a-e462-4b4e-a941-005f40683676', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 10:28:52');
INSERT INTO `syslog` VALUES ('123ace48-b144-4162-82ed-a1ff5b205cae', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-23 23:09:31');
INSERT INTO `syslog` VALUES ('18f92f1a-3400-47d2-9a79-ee87ed853d7c', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 12:58:14');
INSERT INTO `syslog` VALUES ('1f4ff433-bd1c-4afa-8d09-f94a286d3359', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-05 10:21:26');
INSERT INTO `syslog` VALUES ('2094e8bb-9bd4-40a6-aaa3-a972114e1528', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 15:20:03');
INSERT INTO `syslog` VALUES ('21daf62b-23ec-451c-9a8b-43f132f08940', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 13:54:34');
INSERT INTO `syslog` VALUES ('24ed042f-0924-40a9-918f-e5f3f6712fc7', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 23:26:02');
INSERT INTO `syslog` VALUES ('265ded84-11d8-40b3-ac6d-bbc084bbaeb3', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-28 00:29:11');
INSERT INTO `syslog` VALUES ('2e69a87a-6e35-4c2c-991c-026743721a42', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-06 11:02:57');
INSERT INTO `syslog` VALUES ('3491bb1b-5279-49a6-adec-f1f0dc4bb4bb', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-16 17:31:48');
INSERT INTO `syslog` VALUES ('35a33665-c341-4fff-8cec-8afa001a666d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 01:56:03');
INSERT INTO `syslog` VALUES ('36986ff6-96d7-4f5a-90d7-79e9017e3697', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 17:27:40');
INSERT INTO `syslog` VALUES ('379565ac-9e74-4bd7-842d-b1d85cead1b9', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-21 22:32:06');
INSERT INTO `syslog` VALUES ('38e89fc8-3936-4de7-8657-1c45f1e00533', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-22 22:58:01');
INSERT INTO `syslog` VALUES ('3ef0093d-fd80-4a3f-af3b-f53e179ec9d0', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 22:23:40');
INSERT INTO `syslog` VALUES ('3fb076fb-2a73-4a9e-b6e3-91624b9d1d28', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-23 11:04:25');
INSERT INTO `syslog` VALUES ('41cc908d-1db0-4638-a8ca-6da59ac88de8', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '', '2018-06-13 23:24:13');
INSERT INTO `syslog` VALUES ('4472790a-e267-468b-af5c-b9ce48695ddf', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-28 17:45:56');
INSERT INTO `syslog` VALUES ('457e858b-b7b3-498c-abf6-fd674b80798c', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 11:42:02');
INSERT INTO `syslog` VALUES ('4719b57d-7b74-45a4-85d7-a496df51949d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-28 23:43:57');
INSERT INTO `syslog` VALUES ('47bae6ad-5545-4e47-9772-2a5c442cbec7', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 11:56:58');
INSERT INTO `syslog` VALUES ('4a2ebd0b-b32b-40db-8174-764d54f28e85', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 21:49:36');
INSERT INTO `syslog` VALUES ('4caa3b25-152a-4d25-92d1-f60cdb3de1dc', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 21:07:05');
INSERT INTO `syslog` VALUES ('4f23b4b3-68dc-45e1-a6b9-d69f8438a184', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-24 21:11:06');
INSERT INTO `syslog` VALUES ('504cd3e2-0b6b-455d-9532-2f6f7b19e618', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 11:38:26');
INSERT INTO `syslog` VALUES ('54af6c0f-7766-42e6-a7b7-d2be2f182b53', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 11:05:38');
INSERT INTO `syslog` VALUES ('57171254-c3e9-4219-a77a-bd546c2f5355', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 19:27:06');
INSERT INTO `syslog` VALUES ('5ca915b8-5a74-46c5-8bb4-fa436d8b9cf6', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-15 22:40:17');
INSERT INTO `syslog` VALUES ('5e055f94-da30-45e1-992f-be931bb821d5', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-19 21:30:51');
INSERT INTO `syslog` VALUES ('60ae7a82-c81f-4d2a-8d06-3f802515366d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 18:25:15');
INSERT INTO `syslog` VALUES ('6120f664-c215-461b-a8e3-5749f72c4b46', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 23:26:36');
INSERT INTO `syslog` VALUES ('627648c4-612d-4db4-8f1d-165d779875bb', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 21:23:50');
INSERT INTO `syslog` VALUES ('6634e635-b8b9-4e5f-aea6-9ff1b9371993', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-07 20:36:21');
INSERT INTO `syslog` VALUES ('6ca95159-8f79-4bc0-92c3-7000052c771d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-24 21:18:59');
INSERT INTO `syslog` VALUES ('7157442c-45f8-49be-921f-ac1f16cf9c46', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 09:23:36');
INSERT INTO `syslog` VALUES ('71ef99c6-f4d6-4a63-a4ac-54e5beb3baa2', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 10:00:30');
INSERT INTO `syslog` VALUES ('735275c1-f72a-4242-8701-c4fe5c3f5cf6', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-16 15:46:00');
INSERT INTO `syslog` VALUES ('761487e4-5f18-498a-afc1-4d239462e1c7', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-16 09:52:36');
INSERT INTO `syslog` VALUES ('7624303f-ff23-4a86-98d8-caefca51d749', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 22:52:35');
INSERT INTO `syslog` VALUES ('7aee8b35-19bd-49e4-b3cb-d3dab8bd07ab', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 16:50:55');
INSERT INTO `syslog` VALUES ('7dae3a5d-3a44-4532-92da-a17aa5997a5b', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 12:48:13');
INSERT INTO `syslog` VALUES ('7ff5bdf7-073a-45cf-8be1-75389d7e6e85', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-21 18:21:34');
INSERT INTO `syslog` VALUES ('80b8baec-5aa3-4714-b55d-ce481c6e7283', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 19:42:31');
INSERT INTO `syslog` VALUES ('83026695-b85d-4472-9420-38148725dbf8', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-23 00:08:15');
INSERT INTO `syslog` VALUES ('8318386a-efde-4903-9313-692f172a2c5c', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 09:57:02');
INSERT INTO `syslog` VALUES ('8a40199b-b66f-4f12-8eda-6962334af5d0', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 19:33:18');
INSERT INTO `syslog` VALUES ('8b776e1d-8e04-41a5-a7a6-d2b4e147ab8c', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-16 11:18:04');
INSERT INTO `syslog` VALUES ('8bec1f9f-dbde-4fec-9c52-20225c0b0824', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-16 22:52:52');
INSERT INTO `syslog` VALUES ('8c58ac5a-7f11-40ba-aae3-f0bc130e33f8', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 22:34:22');
INSERT INTO `syslog` VALUES ('92f819b0-8e30-480a-8e79-8b90ed07ef65', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-23 01:46:30');
INSERT INTO `syslog` VALUES ('96ad7900-0c3a-429d-9427-f84d68be8b27', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-18 06:10:02');
INSERT INTO `syslog` VALUES ('9bf65643-86c7-49c7-99d8-9b7492dc499f', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-24 22:31:16');
INSERT INTO `syslog` VALUES ('9d81d47f-0812-418e-8702-22f70d67084a', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-05 14:53:50');
INSERT INTO `syslog` VALUES ('9ec2c692-7935-4591-bdc1-425155fe99a6', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 13:10:51');
INSERT INTO `syslog` VALUES ('9f5ee004-2a8d-40ee-bfc9-047dec592368', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 21:17:34');
INSERT INTO `syslog` VALUES ('a49e2a2c-5133-4bff-8b53-65ffd8d57717', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-23 15:38:23');
INSERT INTO `syslog` VALUES ('a5a558ad-2f39-475b-904d-73111665acad', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 10:10:14');
INSERT INTO `syslog` VALUES ('ab97515a-4d8c-4b3f-ae2a-47e1c44bf31c', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 09:36:05');
INSERT INTO `syslog` VALUES ('acbc1fe8-a016-4ab9-aee0-365d967fd4e6', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 12:07:10');
INSERT INTO `syslog` VALUES ('b4c1ea10-aa82-44ad-bcae-3fc6b6011356', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 21:05:56');
INSERT INTO `syslog` VALUES ('b551b0dc-dabd-434b-8ccd-4da9c3e5b986', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-26 23:41:07');
INSERT INTO `syslog` VALUES ('b667f987-5338-4396-8d72-fde229382b1d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-06 09:50:59');
INSERT INTO `syslog` VALUES ('b7cd2285-a05f-4168-ba2c-176dfaa68bbb', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-05 09:20:39');
INSERT INTO `syslog` VALUES ('ba4c5061-bc8e-4efa-93ee-82d935813bdd', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-16 18:33:02');
INSERT INTO `syslog` VALUES ('ba692f57-cbe8-4fe6-a97d-4b8ea5cd2293', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 20:35:39');
INSERT INTO `syslog` VALUES ('bbc5cc55-fd9c-4464-b277-8051dfbd852d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 00:38:51');
INSERT INTO `syslog` VALUES ('bc57c675-fc63-4fc2-a55d-db5d0749289d', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-21 16:16:31');
INSERT INTO `syslog` VALUES ('be4c7ba7-336f-48e3-a318-268d3eadec39', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-29 19:31:43');
INSERT INTO `syslog` VALUES ('bf95bbbc-72e5-4045-89b5-2d10caca1ee8', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 15:00:26');
INSERT INTO `syslog` VALUES ('c1ac92d1-4026-4e5e-9a1c-3c8a23529be5', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '', '2018-06-14 00:29:27');
INSERT INTO `syslog` VALUES ('c2ec0a71-9262-49c2-9bdd-04a00a346c04', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 18:00:58');
INSERT INTO `syslog` VALUES ('c634011b-cc8a-4f94-9955-21251349b65f', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 10:42:43');
INSERT INTO `syslog` VALUES ('c9d9ae43-a0f6-4d28-8fcc-0fd87b04896f', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-01 12:51:50');
INSERT INTO `syslog` VALUES ('ceb69199-c8d6-4c0a-8f2c-4964196d32f0', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-15 23:41:25');
INSERT INTO `syslog` VALUES ('ceec2e78-2d1e-45d5-89e3-c86a67027691', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-29 21:19:32');
INSERT INTO `syslog` VALUES ('d28db11a-24f0-4330-925f-85201f7b64d0', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-18 06:30:01');
INSERT INTO `syslog` VALUES ('d4e763ce-fd76-424b-8b8a-869232764947', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 16:09:10');
INSERT INTO `syslog` VALUES ('d6837afc-2b0b-4262-980c-6a7412a2e8f0', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-21 17:20:24');
INSERT INTO `syslog` VALUES ('da210a5c-a04a-4d98-ba3b-f963409fd879', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 16:29:42');
INSERT INTO `syslog` VALUES ('daaf69df-3079-4571-ac7e-046c0f20ee88', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 17:27:48');
INSERT INTO `syslog` VALUES ('dbb986e8-2c30-47c7-b537-97355956867a', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-22 21:01:44');
INSERT INTO `syslog` VALUES ('dc5fc779-52b6-4ecd-a8ad-d8311527e836', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 13:24:13');
INSERT INTO `syslog` VALUES ('dd3b9bad-46ec-4f9f-b032-be0f27518afa', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-26 21:05:13');
INSERT INTO `syslog` VALUES ('e1221cf9-38f5-4c32-b828-16a627fae434', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-28 16:42:51');
INSERT INTO `syslog` VALUES ('e4adfaa2-2b6a-4430-a300-bd7f3c06bed3', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-20 00:02:25');
INSERT INTO `syslog` VALUES ('e8e33493-8fe4-4b3e-a137-ce429e6c7e16', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-29 23:26:34');
INSERT INTO `syslog` VALUES ('e9df80a7-991a-418b-9240-a82b212f95e6', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-28 21:20:24');
INSERT INTO `syslog` VALUES ('ed3f596b-91d2-405d-a8e3-df4555156dfb', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-07-06 14:36:34');
INSERT INTO `syslog` VALUES ('ee46cf74-5288-476d-b920-7e23a3f4ed46', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 17:49:58');
INSERT INTO `syslog` VALUES ('ef0937e1-b60e-4373-b60f-e15da9adb5ea', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-30 22:14:45');
INSERT INTO `syslog` VALUES ('f3eae8f5-e1f6-49bc-b70c-03a07530ca05', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 09:43:29');
INSERT INTO `syslog` VALUES ('f626126c-f102-45ad-848c-43697ecab1de', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 12:19:37');
INSERT INTO `syslog` VALUES ('f7060ca4-e024-4024-8550-5d8ddef420db', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 16:46:20');
INSERT INTO `syslog` VALUES ('f71b1b4d-ca5c-419d-aeb1-d1fb7aa40da2', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-19 22:51:22');
INSERT INTO `syslog` VALUES ('f8fd36ea-8854-43b7-a048-8ba4a3683d06', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-24 23:37:48');
INSERT INTO `syslog` VALUES ('fb53d0ed-50f9-4b40-a844-4323264171ff', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-27 15:07:39');
INSERT INTO `syslog` VALUES ('fd1f1412-9fbc-4da0-a1de-f9b9ba5163b2', 'admins', '商务中心', 'SysAdmin', '登录操作', '127.0.0.1', null, '1', '/fytadmin/login', '2018-06-17 17:26:13');
INSERT INTO `syslog` VALUES ('ff033cff-94d3-424d-b97e-eeac74278481', 'admins', '商务中心', 'SysAdmin', '登录操作', '::1', null, '1', '/fytadmin/login', '2018-06-25 11:13:01');

-- ----------------------------
-- Table structure for sysmenu
-- ----------------------------
DROP TABLE IF EXISTS `sysmenu`;
CREATE TABLE `sysmenu` (
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

-- ----------------------------
-- Records of sysmenu
-- ----------------------------
INSERT INTO `sysmenu` VALUES ('1fc3d8e8-e3f2-49cf-a652-2341082643df', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '菜单管理', '1012', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,1fc3d8e8-e3f2-49cf-a652-2341082643df,', '2', '/fytadmin/sys/menu/', null, '6', '', '2018-05-17 22:08:48', '2018-05-17 22:08:48');
INSERT INTO `sysmenu` VALUES ('404d4b8b-8e3c-42ee-aee5-f29df31308fa', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '管理员管理', '1013', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,404d4b8b-8e3c-42ee-aee5-f29df31308fa,', '2', '/fytadmin/sys/admin/', null, '7', '', '2018-05-17 22:09:12', '2018-05-17 22:09:12');
INSERT INTO `sysmenu` VALUES ('40823e8a-bc10-4e38-a45f-a6dd7dd0ef78', null, null, '根目录', '库存管理', '1002', ',40823e8a-bc10-4e38-a45f-a6dd7dd0ef78,', '1', null, null, '3', '', '2018-05-17 21:45:45', '2018-05-17 21:45:45');
INSERT INTO `sysmenu` VALUES ('51216bb3-d0c7-474a-b565-71cf96db19f4', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '信息推送', '1016', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,51216bb3-d0c7-474a-b565-71cf96db19f4,', '2', '/fytadmin/sys/push/', null, '10', '', '2018-05-17 22:10:25', '0001-01-01 00:00:00');
INSERT INTO `sysmenu` VALUES ('5ce13ead-971b-4ed4-ad5f-acacccd82381', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '角色管理', '1011', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,5ce13ead-971b-4ed4-ad5f-acacccd82381,', '2', '/fytadmin/sys/role/', null, '5', '', '2018-05-17 22:08:23', '2018-05-17 22:08:23');
INSERT INTO `sysmenu` VALUES ('5ed17c74-0fff-4f88-8581-3b4f26d005a8', null, null, '跟目录', '系统管理', '1001', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,', '1', null, null, '2', '', '2018-05-17 21:39:41', '2018-05-17 21:39:41');
INSERT INTO `sysmenu` VALUES ('6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '部门管理', '1010', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,6d4cfcf7-ff1c-4537-aa3f-75cc9df27583,', '2', '/fytadmin/sys/organize/', null, '4', '', '2018-05-17 22:08:02', '0001-01-01 00:00:00');
INSERT INTO `sysmenu` VALUES ('a280f6e2-3100-445f-871d-77ea443356a9', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '字段管理', '1015', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,a280f6e2-3100-445f-871d-77ea443356a9,', '2', '/fytadmin/sys/codes/', null, '9', '', '2018-05-17 22:09:52', '2018-05-17 22:09:52');
INSERT INTO `sysmenu` VALUES ('b354ea64-88b6-4032-a26a-fee198e24d9d', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', '系统管理', '系统日志', '1014', ',5ed17c74-0fff-4f88-8581-3b4f26d005a8,b354ea64-88b6-4032-a26a-fee198e24d9d,', '2', '/fytadmin/sys/log/', null, '8', '', '2018-05-17 22:09:29', '2018-05-17 22:09:29');

-- ----------------------------
-- Table structure for sysorganize
-- ----------------------------
DROP TABLE IF EXISTS `sysorganize`;
CREATE TABLE `sysorganize` (
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

-- ----------------------------
-- Records of sysorganize
-- ----------------------------
INSERT INTO `sysorganize` VALUES ('24febdc4-655f-4492-ac8a-4adab18c22c8', null, '388b72d3-e10a-4183-8ef7-6be44eb99b1a', '融媒体中心', '包头分公司', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,24febdc4-655f-4492-ac8a-4adab18c22c8,', '2', '7', '', '2018-05-16 22:09:35');
INSERT INTO `sysorganize` VALUES ('388b72d3-e10a-4183-8ef7-6be44eb99b1a', null, '883deb1c-ddd7-484e-92c1-b3ad3b32e655', '包头分公司', '飞易腾集团', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,', '1', '3', '', '2018-05-16 22:06:20');
INSERT INTO `sysorganize` VALUES ('4b6ab27f-c0fa-483d-9b5a-55891ee8d727', null, '388b72d3-e10a-4183-8ef7-6be44eb99b1a', '事业发展部', '包头分公司', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,388b72d3-e10a-4183-8ef7-6be44eb99b1a,4b6ab27f-c0fa-483d-9b5a-55891ee8d727,', '2', '6', '', '2018-05-16 22:09:30');
INSERT INTO `sysorganize` VALUES ('52523a76-52b3-4c25-a1bd-9123a011f2a8', null, '5533b6c5-ba2e-4659-be29-c860bb41e04d', '商务中心', '北京总公司', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,52523a76-52b3-4c25-a1bd-9123a011f2a8,', '2', '4', '', '2018-05-16 22:09:27');
INSERT INTO `sysorganize` VALUES ('5533b6c5-ba2e-4659-be29-c860bb41e04d', null, '883deb1c-ddd7-484e-92c1-b3ad3b32e655', '北京总公司', '飞易腾集团', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,', '1', '2', '', '2018-05-16 22:06:02');
INSERT INTO `sysorganize` VALUES ('883deb1c-ddd7-484e-92c1-b3ad3b32e655', null, null, '飞易腾集团', '根目录', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,', '0', '1', '', '2018-05-15 00:11:55');
INSERT INTO `sysorganize` VALUES ('dcf99638-5db6-4dd7-a485-31df1160d45a', null, '5533b6c5-ba2e-4659-be29-c860bb41e04d', '互联网中心', '北京总公司', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,', '2', '5', '', '2018-05-16 22:09:36');

-- ----------------------------
-- Table structure for syspermissions
-- ----------------------------
DROP TABLE IF EXISTS `syspermissions`;
CREATE TABLE `syspermissions` (
  `RoleGuid` varchar(50) NOT NULL COMMENT '角色Guid',
  `AdminGuid` varchar(50) DEFAULT NULL,
  `MenuGuid` varchar(50) DEFAULT NULL COMMENT '菜单Guid',
  `BtnFunGuid` varchar(50) DEFAULT NULL,
  `Types` tinyint(1) NOT NULL DEFAULT '1' COMMENT '授权类型1=角色-菜单 2=用户-角色 3=角色-菜单-按钮功能'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of syspermissions
-- ----------------------------
INSERT INTO `syspermissions` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', null, null, '2');
INSERT INTO `syspermissions` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', null, '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', '6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61', '3');
INSERT INTO `syspermissions` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', null, '5ce13ead-971b-4ed4-ad5f-acacccd82381', 'c4261103-dfc7-46e5-ab20-4ca5fc7729f6', '3');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', '931bd729-f160-4fe3-bb7c-7828a2da047a', '3');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '5ce13ead-971b-4ed4-ad5f-acacccd82381', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '1fc3d8e8-e3f2-49cf-a652-2341082643df', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '404d4b8b-8e3c-42ee-aee5-f29df31308fa', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, 'b354ea64-88b6-4032-a26a-fee198e24d9d', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, 'a280f6e2-3100-445f-871d-77ea443356a9', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '51216bb3-d0c7-474a-b565-71cf96db19f4', null, '1');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', '6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61', '3');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', null, '5ce13ead-971b-4ed4-ad5f-acacccd82381', 'c4261103-dfc7-46e5-ab20-4ca5fc7729f6', '3');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', null, '5ed17c74-0fff-4f88-8581-3b4f26d005a8', null, '1');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', null, '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', null, '1');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', null, '5ce13ead-971b-4ed4-ad5f-acacccd82381', null, '1');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', null, '6d4cfcf7-ff1c-4537-aa3f-75cc9df27583', '6b8d6513-5da7-4f68-a2c4-f0c7cfaf7f61', '3');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', null, '5ce13ead-971b-4ed4-ad5f-acacccd82381', 'c4261103-dfc7-46e5-ab20-4ca5fc7729f6', '3');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', '30d3da88-bb72-4ace-a303-b3aae0ecb732', null, null, '2');
INSERT INTO `syspermissions` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', null, null, '2');
INSERT INTO `syspermissions` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', null, null, '2');
INSERT INTO `syspermissions` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', '12cc96cf-7ccf-430b-a54a-e1c6f04690cb', null, null, '2');
INSERT INTO `syspermissions` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', '30d3da88-bb72-4ace-a303-b3aae0ecb732', null, null, '2');

-- ----------------------------
-- Table structure for sysrole
-- ----------------------------
DROP TABLE IF EXISTS `sysrole`;
CREATE TABLE `sysrole` (
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

-- ----------------------------
-- Records of sysrole
-- ----------------------------
INSERT INTO `sysrole` VALUES ('2949c266-575a-4e5d-a663-e39d5f33df7e', 'dcf99638-5db6-4dd7-a485-31df1160d45a', '互联网中心', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,', '超级管理员', '1001', '', '超级管理员', '2018-03-29 00:00:00', '2018-05-17 23:36:28');
INSERT INTO `sysrole` VALUES ('9bf21fc0-6e39-4e16-a55f-6717977a697a', '52523a76-52b3-4c25-a1bd-9123a011f2a8', '商务中心', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,52523a76-52b3-4c25-a1bd-9123a011f2a8,', '客服管理员', '1002', '', '只能查看会员相关功能', '2018-05-17 23:37:56', '2018-05-17 23:37:56');
INSERT INTO `sysrole` VALUES ('d1bbd2f4-ea8f-4c53-9f67-3b6acd9c29fb', 'dcf99638-5db6-4dd7-a485-31df1160d45a', '互联网中心', ',883deb1c-ddd7-484e-92c1-b3ad3b32e655,5533b6c5-ba2e-4659-be29-c860bb41e04d,dcf99638-5db6-4dd7-a485-31df1160d45a,', '财务管理员', '1003', '', '只能查看财务相关功能', '2018-05-17 23:39:01', '2018-05-17 23:39:01');
