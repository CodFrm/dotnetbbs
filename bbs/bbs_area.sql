/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : bbs

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2018-06-22 11:30:18
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for bbs_area
-- ----------------------------
DROP TABLE IF EXISTS `bbs_area`;
CREATE TABLE `bbs_area` (
  `aid` int(11) NOT NULL AUTO_INCREMENT,
  `area_name` varchar(64) NOT NULL,
  `area_explain` text NOT NULL,
  `area_logo` varchar(128) NOT NULL,
  `area_father` int(11) NOT NULL,
  `area_priority` tinyint(4) NOT NULL,
  PRIMARY KEY (`aid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bbs_area
-- ----------------------------
INSERT INTO `bbs_area` VALUES ('1', '谈天说地', '吹水一波', '', '4', '0');
INSERT INTO `bbs_area` VALUES ('2', '技术交流', 'dalao汇聚地', '', '5', '0');
INSERT INTO `bbs_area` VALUES ('3', '失物招领', '失物招领', '', '6', '0');
INSERT INTO `bbs_area` VALUES ('4', '一区', '', '', '0', '10');
INSERT INTO `bbs_area` VALUES ('5', '二区', '', '', '0', '9');
INSERT INTO `bbs_area` VALUES ('6', '三区', '', '', '0', '8');
INSERT INTO `bbs_area` VALUES ('7', '快乐吹水', '?????就是这样', '', '4', '0');

-- ----------------------------
-- Table structure for bbs_post
-- ----------------------------
DROP TABLE IF EXISTS `bbs_post`;
CREATE TABLE `bbs_post` (
  `pid` bigint(20) NOT NULL AUTO_INCREMENT,
  `post_uid` int(11) NOT NULL,
  `post_aid` int(11) NOT NULL,
  `post_title` varchar(64) NOT NULL,
  `post_content` text NOT NULL,
  `post_time` bigint(20) NOT NULL,
  `post_end_reply_time` bigint(20) NOT NULL,
  `post_reply_number` int(11) NOT NULL,
  `post_viewer_number` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`pid`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bbs_post
-- ----------------------------
INSERT INTO `bbs_post` VALUES ('2', '1', '2', '超星慕课-发送分包的方式挂机', '# test\r\n\r\nwd wqdwefErgergre\r\nfgbr rtrth然后不方便\r\nvdfgv潍坊吧的', '1527555370', '1527555370', '0', null);
INSERT INTO `bbs_post` VALUES ('3', '1', '1', '重构框架-一', '# test\r\n\r\n暗示法算法看难道是发表时间分vd赛诺菲VMvdvxbx,b,vds,', '1528163360', '1528163360', '0', null);
INSERT INTO `bbs_post` VALUES ('4', '1', '1', '在差三分实得分实得分实得分顾问费我', '啊实打实大阿斯顿暗示法\r\n\r\n# test\r\n\r\n速度覆是为非我哥哥额\r\n\r\n## 233', '1528163902', '1528163902', '0', null);
INSERT INTO `bbs_post` VALUES ('5', '2', '2', '测试123534564', '# test\r\n请问请问请问全文\r\n\r\n气温气温二等分官方\r\n\r\n如果人人头软件\r\n\r\n还头一回替换他', '1528252611', '1528252611', '0', null);
INSERT INTO `bbs_post` VALUES ('6', '1', '2', '测试中心擦是', '# test\r\n\r\n撒大声地请问请问请问撒的\r\n\r\n请问却无法让区分\r\n覆盖\r\n实得分实得分是\r\n\r\n实得分实得分苏倩薇', '1528698296', '1528698296', '0', null);

-- ----------------------------
-- Table structure for bbs_reply
-- ----------------------------
DROP TABLE IF EXISTS `bbs_reply`;
CREATE TABLE `bbs_reply` (
  `rid` bigint(20) NOT NULL AUTO_INCREMENT,
  `reply_uid` int(11) NOT NULL,
  `reply_pid` bigint(20) NOT NULL,
  `reply_content` text NOT NULL,
  `reply_time` bigint(20) NOT NULL,
  PRIMARY KEY (`rid`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bbs_reply
-- ----------------------------
INSERT INTO `bbs_reply` VALUES ('1', '1', '6', 'adsasd qw dsf sdf sd\n\n> qweqEqweqrwerew\n\n# werwe人文氛围', '1528700237');
INSERT INTO `bbs_reply` VALUES ('2', '1', '2', '66666666666666666666666666666666\n\n# 啊沙发斯蒂芬实得分速度发送到', '1528774570');
INSERT INTO `bbs_reply` VALUES ('3', '1', '2', '都了安达市啊', '1528786043');
INSERT INTO `bbs_reply` VALUES ('4', '1', '3', '厉害啊,卧槽ヾ(｡｀Д´｡)', '1528875496');
INSERT INTO `bbs_reply` VALUES ('5', '1', '4', '6翻了,好不好', '1528875515');
INSERT INTO `bbs_reply` VALUES ('6', '1', '4', '哈哈哈哈哈哈', '1528875519');
INSERT INTO `bbs_reply` VALUES ('7', '2', '6', '厉害啊,楼主666', '1529026075');
INSERT INTO `bbs_reply` VALUES ('8', '1', '6', '66666666666', '1529637959');
INSERT INTO `bbs_reply` VALUES ('9', '1', '6', '# markdown\n\n哈哈哈  [按时打算发司法所](http://www.baidu.com)', '1529637978');

-- ----------------------------
-- Table structure for bbs_user
-- ----------------------------
DROP TABLE IF EXISTS `bbs_user`;
CREATE TABLE `bbs_user` (
  `uid` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(32) NOT NULL,
  `password` varchar(64) NOT NULL,
  `reg_time` bigint(20) NOT NULL,
  PRIMARY KEY (`uid`),
  KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bbs_user
-- ----------------------------
INSERT INTO `bbs_user` VALUES ('1', 'farmer', 'qwe123', '12123');
INSERT INTO `bbs_user` VALUES ('2', 'qwe123', 'qwe123', '1526351970');
INSERT INTO `bbs_user` VALUES ('3', 'æµ‹è¯•123', 'qwe123', '1526957105');
INSERT INTO `bbs_user` VALUES ('4', 'afdasfasf', 'qwe123', '1527651855');

-- ----------------------------
-- Table structure for bbs_user_token
-- ----------------------------
DROP TABLE IF EXISTS `bbs_user_token`;
CREATE TABLE `bbs_user_token` (
  `token` varchar(128) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `uid` int(10) NOT NULL,
  `time` bigint(20) unsigned NOT NULL,
  `type` int(4) NOT NULL DEFAULT '0' COMMENT '0 user login 1 email 2 验证码',
  PRIMARY KEY (`token`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of bbs_user_token
-- ----------------------------
INSERT INTO `bbs_user_token` VALUES ('06Fyl36X09q0yfGo', '1', '1529370257', '0');
INSERT INTO `bbs_user_token` VALUES ('59vmjSLL5kWtmYTV1', '3', '1526957120', '0');
INSERT INTO `bbs_user_token` VALUES ('69JjzW1JeYXaCgdw', '1', '1528786025', '0');
INSERT INTO `bbs_user_token` VALUES ('8GzrN34c8AWYYnu3', '2', '1528252577', '0');
INSERT INTO `bbs_user_token` VALUES ('C9S37m45945DNAp0', '1', '1529370256', '0');
INSERT INTO `bbs_user_token` VALUES ('Fx8jhW1vaeksX3IQ', '1', '1528698244', '0');
INSERT INTO `bbs_user_token` VALUES ('GMeabHEl3LfWS6As', '1', '1528162106', '0');
INSERT INTO `bbs_user_token` VALUES ('Iz6x6px75hOByWxF', '1', '1528875476', '0');
INSERT INTO `bbs_user_token` VALUES ('JHB7emBKcb65FSHH', '2', '1526958130', '0');
INSERT INTO `bbs_user_token` VALUES ('OeQJsaIFJS2woLpp', '2', '1529026049', '0');
INSERT INTO `bbs_user_token` VALUES ('Xe1ViJJSFbT40oon', '2', '1527651872', '0');
INSERT INTO `bbs_user_token` VALUES ('ZUzordoPr7kBmMiW', '1', '1528875475', '0');
INSERT INTO `bbs_user_token` VALUES ('bzQEFRm38SoveAsQ', '2', '1528252578', '0');
INSERT INTO `bbs_user_token` VALUES ('cECN7l3jdYnaKoIG', '1', '1527042976', '0');
INSERT INTO `bbs_user_token` VALUES ('dHHmGtn2jnD3m21M', '2', '1528162233', '0');
INSERT INTO `bbs_user_token` VALUES ('gC0v9HhQQur74swj', '2', '1526958207', '0');
INSERT INTO `bbs_user_token` VALUES ('hyKh6NBIcX4BL9Zz', '1', '1529637946', '0');
INSERT INTO `bbs_user_token` VALUES ('mXmEySmmecZ4v6oF', '1', '1527652111', '0');
INSERT INTO `bbs_user_token` VALUES ('n7xk4ciff2ECJlBD', '1', '1529024547', '0');
INSERT INTO `bbs_user_token` VALUES ('nA2IDW4tj7p7f6vY', '1', '1527555198', '0');
INSERT INTO `bbs_user_token` VALUES ('om99SQCgJJdCuW3N', '1', '1527649689', '0');
INSERT INTO `bbs_user_token` VALUES ('ouiXL7qv9oxIdCps', '1', '1529637667', '0');
INSERT INTO `bbs_user_token` VALUES ('ubGx0gFxP3jKTsgA', '2', '1526958150', '0');
INSERT INTO `bbs_user_token` VALUES ('w7HMtQbm6Kpg7wYI', '1', '1527650093', '0');
INSERT INTO `bbs_user_token` VALUES ('zQP1epryMzWW0lXG', '1', '1527564092', '0');
