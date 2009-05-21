-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.1.34-community


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema kona_dbo
--

CREATE DATABASE IF NOT EXISTS kona;
USE kona;

--
-- Definition of table `addresses`
--

DROP TABLE IF EXISTS `addresses`;
CREATE TABLE `addresses` (
  `AddressID` int(10) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Email` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Street1` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Street2` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `City` varchar(150) CHARACTER SET utf8 NOT NULL,
  `StateOrProvince` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Zip` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Country` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Latitude` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Longitude` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `IsDefault` tinyint(4) NOT NULL,
  PRIMARY KEY (`AddressID`),
  KEY `FK_Addresses_Customers` (`UserName`),
  CONSTRAINT `FK_Addresses_Customers` FOREIGN KEY (`UserName`) REFERENCES `customers` (`UserName`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `addresses`
--

/*!40000 ALTER TABLE `addresses` DISABLE KEYS */;
INSERT INTO `addresses` (`AddressID`,`UserName`,`FirstName`,`LastName`,`Email`,`Street1`,`Street2`,`City`,`StateOrProvince`,`Zip`,`Country`,`Latitude`,`Longitude`,`IsDefault`) VALUES 
 (69,'c4a4376c-a1e4-47b1-afc4-77217a82c263','Rob','Conery','robcon@microsoft.com','PO Box 808','','Hanalei','HI','96714','US',NULL,NULL,0),
 (70,'robconery','Rob','Conery','robcon@microsoft.com','PO Box 803','','Hanalei','HI','96714','US',NULL,NULL,0);
/*!40000 ALTER TABLE `addresses` ENABLE KEYS */;


--
-- Definition of table `categories`
--

DROP TABLE IF EXISTS `categories`;
CREATE TABLE `categories` (
  `CategoryID` int(10) NOT NULL AUTO_INCREMENT,
  `ParentID` int(10) DEFAULT NULL,
  `IsDefault` tinyint(4) NOT NULL,
  `DefaultImageFile` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categories`
--

/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` (`CategoryID`,`ParentID`,`IsDefault`,`DefaultImageFile`) VALUES 
 (9,NULL,0,'Hat2_1_Thumbnail.jpg'),
 (10,9,0,'Boots4_1_Thumbnail.jpg'),
 (11,9,0,'Hat3_1_Thumbnail.jpg'),
 (12,9,0,'Sunglasses2_1_Thumbnail.jpg'),
 (13,NULL,0,'Rope3_2_Thumbnail.jpg'),
 (14,13,0,'Compass3_1_Thumbnail.jpg'),
 (15,13,0,'Backpack1_1_Thumbnail.jpg'),
 (16,13,0,'Binoculars2_1_Thumbnail.jpg'),
 (17,13,0,'GPS1_1_Thumbnail.jpg'),
 (18,13,0,'Boots2_1_Thumbnail.jpg'),
 (19,NULL,0,'Tent2_1_Thumbnail.jpg'),
 (20,19,0,'Flashlight2_3_Thumbnail.jpg'),
 (21,19,0,'Lantern1_1_Thumbnail.jpg'),
 (22,19,0,'Tent2_1_Thumbnail.jpg'),
 (23,19,0,'Knife2_1_Thumbnail.jpg'),
 (24,19,0,'Sleepingbag1_2_Thumbnail.jpg'),
 (25,NULL,0,'Caribiner3_3_Thumbnail.jpg'),
 (26,25,0,'Caribiner3_3_Thumbnail.jpg'),
 (27,25,0,'Rope3_2_Thumbnail.jpg'),
 (28,25,0,'Boots2_2_Thumbnail.jpg'),
 (29,NULL,0,'Tirerepair1_1_Thumbnail.jpg'),
 (30,29,0,'Bike1_1_Thumbnail.jpg'),
 (31,29,0,'Helmet2_1_Thumbnail.jpg'),
 (32,29,0,'Boots5_1_Thumbnail.jpg'),
 (33,NULL,1,'Boots5_1_Thumbnail.jpg'),
 (34,33,0,'Featured_Thumbnail.jpg');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;


--
-- Definition of table `categories_products`
--

DROP TABLE IF EXISTS `categories_products`;
CREATE TABLE `categories_products` (
  `CategoryID` int(10) NOT NULL,
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`CategoryID`,`SKU`),
  KEY `FK_Categories_Products_Products` (`SKU`),
  CONSTRAINT `FK_Categories_Products_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Categories_Products_Categories` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categories_products`
--

/*!40000 ALTER TABLE `categories_products` DISABLE KEYS */;
INSERT INTO `categories_products` (`CategoryID`,`SKU`) VALUES 
 (33,'Backpack1'),
 (33,'Backpack2'),
 (33,'Backpack3'),
 (33,'Backpack4'),
 (30,'Bike1'),
 (33,'Bike1'),
 (30,'Bike2'),
 (33,'Bike2'),
 (30,'Bike3'),
 (33,'Bike3'),
 (16,'Binoculars1'),
 (19,'Binoculars1'),
 (16,'Binoculars2'),
 (19,'Binoculars2'),
 (10,'Boots1'),
 (18,'Boots1'),
 (19,'Boots1'),
 (28,'Boots1'),
 (33,'Boots1'),
 (10,'Boots2'),
 (18,'Boots2'),
 (19,'Boots2'),
 (28,'Boots2'),
 (33,'Boots2'),
 (10,'Boots3'),
 (18,'Boots3'),
 (19,'Boots3'),
 (28,'Boots3'),
 (33,'Boots3'),
 (10,'Boots4'),
 (10,'Boots5'),
 (26,'Caribiner1'),
 (33,'Caribiner1'),
 (26,'Caribiner2'),
 (33,'Caribiner2'),
 (26,'Caribiner3'),
 (33,'Caribiner3'),
 (14,'Compass1'),
 (14,'Compass2'),
 (14,'Compass3'),
 (33,'Flashlight1'),
 (33,'Flashlight2'),
 (33,'Flashlight3'),
 (17,'GPS1'),
 (33,'GPS1'),
 (17,'GPS2'),
 (33,'GPS2'),
 (11,'Hat1'),
 (33,'Hat1'),
 (11,'Hat2'),
 (33,'Hat2'),
 (11,'Hat3'),
 (33,'Hat3'),
 (31,'Helmet1'),
 (33,'Helmet1'),
 (31,'Helmet2'),
 (33,'Helmet2'),
 (31,'Helmet3'),
 (33,'Helmet3'),
 (33,'Lantern1'),
 (33,'Lantern2'),
 (32,'Pump1'),
 (33,'Pump1'),
 (32,'Pump2'),
 (33,'Pump2'),
 (27,'Rope1'),
 (33,'Rope1'),
 (26,'Rope2'),
 (33,'Rope2'),
 (26,'Rope3'),
 (33,'Rope3'),
 (24,'SleepingBag1'),
 (12,'Sunglasses1'),
 (29,'Sunglasses1'),
 (33,'Sunglasses1'),
 (12,'SunGlasses2'),
 (29,'SunGlasses2'),
 (33,'SunGlasses2'),
 (22,'Tent1'),
 (33,'Tent1'),
 (22,'Tent2'),
 (33,'Tent2'),
 (22,'Tent3'),
 (33,'Tent3');
/*!40000 ALTER TABLE `categories_products` ENABLE KEYS */;


--
-- Definition of table `categories_widgets`
--

DROP TABLE IF EXISTS `categories_widgets`;
CREATE TABLE `categories_widgets` (
  `CategoryID` int(10) NOT NULL,
  `WidgetID` varchar(64) NOT NULL,
  PRIMARY KEY (`CategoryID`,`WidgetID`),
  KEY `FK_Categories_Widgets_Widgets` (`WidgetID`),
  CONSTRAINT `FK_Categories_Widgets_Categories` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Categories_Widgets_Widgets` FOREIGN KEY (`WidgetID`) REFERENCES `widgets` (`WidgetID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categories_widgets`
--

/*!40000 ALTER TABLE `categories_widgets` DISABLE KEYS */;
INSERT INTO `categories_widgets` (`CategoryID`,`WidgetID`) VALUES 
 (33,'400840E1-90FC-4DFB-9937-F39C55E13178'),
 (33,'463E497D-1C80-4D87-A7D4-E887E444203C'),
 (33,'AB5F9903-5D8F-40E8-A586-BACB9D43AA8B'),
 (33,'AD6F60ED-8826-4E0E-8D06-2DE60DE5F9FA');
/*!40000 ALTER TABLE `categories_widgets` ENABLE KEYS */;


--
-- Definition of table `categorylocalized`
--

DROP TABLE IF EXISTS `categorylocalized`;
CREATE TABLE `categorylocalized` (
  `CategoryNameID` int(10) NOT NULL AUTO_INCREMENT,
  `CategoryID` int(10) NOT NULL,
  `LanguageCode` char(2) NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Slug` varchar(50) CHARACTER SET utf8 NOT NULL,
  `DefaultImageFile` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(1500) CHARACTER SET utf8 DEFAULT NULL,
  `IsHome` tinyint(4) NOT NULL,
  PRIMARY KEY (`CategoryNameID`),
  KEY `FK_CategoryNames_Categories` (`CategoryID`),
  CONSTRAINT `FK_CategoryNames_Categories` FOREIGN KEY (`CategoryID`) REFERENCES `categories` (`CategoryID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categorylocalized`
--

/*!40000 ALTER TABLE `categorylocalized` DISABLE KEYS */;
INSERT INTO `categorylocalized` (`CategoryNameID`,`CategoryID`,`LanguageCode`,`Name`,`Slug`,`DefaultImageFile`,`Description`,`IsHome`) VALUES 
 (1,9,'en','Apparel','Apparel','Hat2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (2,10,'en','Boots','Boots','Boots4_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (3,11,'en','Hats','Hats','Hat3_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (4,12,'en','Sunglasses','Sunglasses','Sunglasses2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (5,13,'en','Hiking Equipment','Hiking-Equipment','Rope3_2_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (6,14,'en','Compasses','Compasses','Compass3_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (7,15,'en','Backpacks','Backpacks','Backpack1_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (8,16,'en','Binoculars','Binoculars','Binoculars2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (9,17,'en','GPS Devices','GPS-Devices','GPS1_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (10,18,'en','Boots','Boots','Boots2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (11,19,'en','Camping Gear','Camping-Gear','Tent2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (12,20,'en','Flashlights','Flashlights','Flashlight2_3_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (13,21,'en','Lanterns','Lanterns','Lantern1_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (14,22,'en','Tents','Tents','Tent2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (15,23,'en','Knives','Knives','Knife2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (16,24,'en','Sleeping Bags','Sleeping-Bags','Sleepingbag1_2_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (17,25,'en','Climbing Equipment','Climbing-Equipment','Caribiner3_3_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (18,26,'en','Carabiners','Carabiners','Caribiner3_3_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (19,27,'en','Climbing Rope','Climbing-Rope','Rope3_2_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (20,28,'en','Boots','Boots','Boots2_2_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (21,29,'en','Biking Gear','Biking-Gear','Tirerepair1_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (22,30,'en','Bikes','Bikes','Bike1_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (23,31,'en','Helmets','Helmets','Helmet2_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (24,32,'en','Gear','Gear','Boots5_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0),
 (25,33,'en','Welcome to Adventure Works!!','','Boots5_1_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',1),
 (26,34,'en','All Items','All-Items','Featured_Thumbnail.jpg','Lorem ipsum dolor sit amet, consectetur adipiscing elit. In venenatis. Donec venenatis risus eget arcu. Nulla blandit. Sed bibendum, pede egestas posuere sagittis, mauris tortor vulputate erat, porttitor facilisis urna erat a erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque scelerisque. Sed tempor, ante adipiscing congue scelerisque, nibh arcu hendrerit ante, nec congue tellus eros dictum odio. Mauris in lorem ac mi pulvinar vehicula. Nam nec diam. Vivamus ac erat sit amet nunc facilisis lobortis.',0);
/*!40000 ALTER TABLE `categorylocalized` ENABLE KEYS */;


--
-- Definition of table `customerbehaviors`
--

DROP TABLE IF EXISTS `customerbehaviors`;
CREATE TABLE `customerbehaviors` (
  `UserBehaviorID` int(10) NOT NULL,
  `Description` varchar(50) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`UserBehaviorID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customerbehaviors`
--

/*!40000 ALTER TABLE `customerbehaviors` DISABLE KEYS */;
INSERT INTO `customerbehaviors` (`UserBehaviorID`,`Description`) VALUES 
 (1,'Logging In'),
 (2,'Logging Out'),
 (3,'Add Item To Basket'),
 (4,'Remove Item From Basket'),
 (5,'Checkout: Billing'),
 (6,'Checkout: Shipping'),
 (7,'Checkout: Finalize'),
 (8,'View Order'),
 (9,'View Basket'),
 (10,'View Category'),
 (11,'View Product');
/*!40000 ALTER TABLE `customerbehaviors` ENABLE KEYS */;


--
-- Definition of table `customerevents`
--

DROP TABLE IF EXISTS `customerevents`;
CREATE TABLE `customerevents` (
  `EventID` int(10) NOT NULL AUTO_INCREMENT,
  `UserBehaviorID` int(10) NOT NULL,
  `UserName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `EventDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IP` varchar(50) CHARACTER SET utf8 NOT NULL,
  `SKU` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `CategoryID` int(10) DEFAULT NULL,
  `OrderID` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`EventID`),
  KEY `FK_CustomerEvents_Customers` (`UserName`),
  KEY `FK_UserEvents_UseBehaviors` (`UserBehaviorID`),
  CONSTRAINT `FK_CustomerEvents_Customers` FOREIGN KEY (`UserName`) REFERENCES `customers` (`UserName`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserEvents_UseBehaviors` FOREIGN KEY (`UserBehaviorID`) REFERENCES `customerbehaviors` (`UserBehaviorID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customerevents`
--

/*!40000 ALTER TABLE `customerevents` DISABLE KEYS */;
/*!40000 ALTER TABLE `customerevents` ENABLE KEYS */;


--
-- Definition of table `customers`
--

DROP TABLE IF EXISTS `customers`;
CREATE TABLE `customers` (
  `UserName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Email` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `First` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Last` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `LanguageCode` char(2) DEFAULT NULL,
  PRIMARY KEY (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customers`
--

/*!40000 ALTER TABLE `customers` DISABLE KEYS */;
INSERT INTO `customers` (`UserName`,`Email`,`First`,`Last`,`LanguageCode`) VALUES 
 ('00186f3c-bbb5-4ca5-8fd1-4a4d22b6f4e2',NULL,NULL,'en','en'),
 ('015b35d2-6b99-4d53-adb7-2eb8e9d7b416',NULL,NULL,'en','en'),
 ('018fdba5-d4b3-4d5a-a7d0-9c40e6e7c456',NULL,'Guest',NULL,NULL),
 ('01ed6529-f22e-4096-82ea-8fa3eeda6ce6','','Guest','','en'),
 ('02840385-0545-418c-b269-c707f20c0961',NULL,NULL,'en','en'),
 ('0323aa2c-0ec8-4f9c-b908-7c4ee875bbd8',NULL,NULL,'en','en'),
 ('06361b66-651e-4ebd-8159-1c39702dbbfb',NULL,NULL,'en','en'),
 ('0670afc5-cf20-49c6-94cb-09c5effe3f7b',NULL,NULL,'en','en'),
 ('0a361f68-a91c-4c80-943a-e0108d11c249',NULL,NULL,'en','en'),
 ('0a7df585-d6d3-4287-a18b-406268f15f48',NULL,NULL,'en','en'),
 ('0c2398af-d000-4531-bf93-26cb0dd5003f',NULL,NULL,'en','en'),
 ('0f7a09e6-c24b-4b68-94b2-515bd3eb48b1',NULL,NULL,'en','en'),
 ('10eee5bc-2919-45d1-9b97-d6e2a4bfa759',NULL,NULL,'en','en'),
 ('123ba83d-548c-4949-b4ef-74bcc8e41440',NULL,NULL,'en','en'),
 ('12ca52c7-f9ab-413f-988a-29562f040695',NULL,NULL,'en','en'),
 ('13a4de5c-5edf-4524-9d2a-4fb564d30b01',NULL,NULL,'en','en'),
 ('1590232e-1143-4881-9d34-7c8b934211fe',NULL,NULL,'en','en'),
 ('165a82cd-29b7-46ad-a183-8e6e48e2d020',NULL,NULL,'en','en'),
 ('16d07a91-3f61-45d5-842d-2d8f33e19ddd',NULL,NULL,'en','en'),
 ('19d3d6c7-325d-4140-952f-4525ce9db286',NULL,NULL,'en','en'),
 ('19d3e0bc-e71b-4e95-8fc6-a9e3290d0852',NULL,NULL,'en','en'),
 ('19f5e060-bade-4356-a838-1e9b5760fc5f',NULL,NULL,'en','en'),
 ('1b24755b-5056-4c1e-bfd7-29b2083f127d',NULL,'Guest',NULL,NULL),
 ('1b48bfac-4353-4f8f-a6a0-45c431273a8a',NULL,NULL,'en','en'),
 ('1c27e5bf-226e-497a-96aa-44cb54fd7a7a',NULL,NULL,'en','en'),
 ('1cb6a575-9c66-4c45-a17b-602584df8343',NULL,NULL,'en','en'),
 ('1cea43f0-761a-4d50-bb4c-7f6eedcc3dde',NULL,NULL,'en','en'),
 ('1d7b3544-e0d7-4b98-b824-968dbbbc0141',NULL,NULL,'en','en'),
 ('22471768-a516-46cc-8ae1-b789f962d9ed',NULL,NULL,'en','en'),
 ('2392ef94-ff6c-460f-9b6d-3546d1139c2e',NULL,NULL,'en','en'),
 ('25e5173f-39f3-4e5d-9434-32091c405b32',NULL,NULL,'en','en'),
 ('266c80b1-3be8-4467-957a-d22e867b6af5',NULL,NULL,'en','en'),
 ('295d4c33-3b22-428a-a6ae-390cbbd54436',NULL,NULL,'en','en'),
 ('2bbdd60a-5ad5-430a-a3a1-396c18f3c721',NULL,NULL,'en','en'),
 ('2bdd81e0-6fbc-459a-bfb7-079401b303ae',NULL,NULL,'en','en'),
 ('2cb4c900-7795-4653-80d5-f338ad8149d8',NULL,NULL,'en','en'),
 ('2d0273a7-7a56-41dd-a1f4-32d3b28d1de9',NULL,NULL,'en','en'),
 ('2da1505a-94cb-41a0-b45f-0fe1212027d2',NULL,NULL,'en','en'),
 ('2dfc58ab-10d5-42c9-b1bf-6584df701b66',NULL,NULL,'en','en'),
 ('2faf34da-9f27-40f3-bf02-4949d8cf8573','','Guest','','en'),
 ('30d23e24-d695-4d5c-9465-03db7406775e',NULL,NULL,'en','en'),
 ('32a9440c-124f-472c-a866-525ccbd98a30',NULL,NULL,'en','en'),
 ('33108ffc-f710-4fa4-8a92-d4a91d0d96fc',NULL,NULL,'en','en'),
 ('35e2abfb-b1ab-4ebf-9e9f-f22ed7cd020d',NULL,NULL,'en','en'),
 ('35ea0d7c-15fc-4660-b39a-f34f7e1f1b1d',NULL,NULL,'en','en'),
 ('38b99dcf-ba29-4658-83bd-3d67b21947d3',NULL,NULL,'en','en'),
 ('38c5fe06-e03e-4d12-b31c-39a52dd62c53',NULL,NULL,'en','en'),
 ('3a232a81-4748-4a49-a303-e5fe51344614',NULL,NULL,'en','en'),
 ('3bc15475-3980-439c-a566-d53c3ef03d53',NULL,NULL,'en','en'),
 ('3c371b3c-0f49-4ed2-aef3-0fb2f560bcd1',NULL,'Guest',NULL,NULL),
 ('3d1c098e-9118-4c3e-8afe-a9ed34da77ab',NULL,NULL,'en','en'),
 ('3db05d74-37de-4147-a9b2-36650b562aa3',NULL,NULL,'en','en'),
 ('3ea0707a-74f8-4367-bab0-3e7d5d20eac8','','Guest','','en'),
 ('4753426a-2f71-4fcd-b312-666f43291d43',NULL,NULL,'en','en'),
 ('4ac5b884-eec2-4980-9bd5-558269644520',NULL,NULL,'en','en'),
 ('4b051b4f-80d5-496d-b9bc-61dfe8c5917b',NULL,NULL,'en','en'),
 ('4b490c44-9c9b-487d-bcfd-219631fb1637',NULL,NULL,'en','en'),
 ('4bc1d26d-b9e3-48a7-bf44-04e11370b2e4',NULL,NULL,'en','en'),
 ('4c995e06-cbea-4f45-8907-24dab7f85f21',NULL,NULL,'en','en'),
 ('551ce105-4e56-47cb-8f64-eb526f2b0ade',NULL,'Guest',NULL,NULL),
 ('57967c4f-88d7-4922-a5a2-b386dde9c119',NULL,NULL,'en','en'),
 ('59694307-b01a-4125-a23d-defff89fd65e',NULL,NULL,'en','en'),
 ('59f1dce6-1f64-4e97-b85a-41475ef07c4c',NULL,NULL,'en','en'),
 ('5a15dae4-81e0-44b8-9a2b-fb48808b01e2',NULL,NULL,'en','en'),
 ('5a51dbb6-b869-43b4-80cd-9e9bc431a9aa',NULL,NULL,'en','en'),
 ('5b8d315e-5292-4c8c-b846-aa4b391c4ab9',NULL,NULL,'en','en'),
 ('5c2db939-1870-454b-a204-75aa339e9825',NULL,NULL,'en','en'),
 ('5e63c9af-bb15-4fc9-a7c6-85092652f39a',NULL,NULL,'en','en'),
 ('5f8de891-f8ad-4f7b-a191-c6106b17891c',NULL,NULL,'en','en'),
 ('60b1c5dd-ee4e-4ee6-9efd-a8cca5eb5202',NULL,NULL,'en','en'),
 ('60e16d5f-4e3f-49f7-a001-f91380b3d5d7',NULL,NULL,'en','en'),
 ('61e004aa-8395-41f5-a49d-e457703dd3a0',NULL,NULL,'en','en'),
 ('63f3aa2b-5598-46b9-a96b-111bb676af93',NULL,NULL,'en','en'),
 ('67767e13-b4da-44cc-9da7-827324eb7555',NULL,NULL,'en','en'),
 ('67fdfdf7-4fd9-46f1-b095-709849121777',NULL,'Guest',NULL,NULL),
 ('69dddf2a-eaf4-4bce-b3bc-e5971ebf5679',NULL,NULL,'en','en'),
 ('6a595832-88a0-4191-a0c5-350e6f24ca5d',NULL,NULL,'en','en'),
 ('6aa9a4c6-f360-4bfb-a344-c5f52597c406',NULL,NULL,'en','en'),
 ('6cf2aa42-69f8-4903-a8c5-ef5e5b7f68a7',NULL,NULL,'en','en'),
 ('6d96f46e-da64-4202-b07f-4b3a93aaaba4',NULL,NULL,'en','en'),
 ('6fa94cd5-d352-4a53-a802-bc9299a1844a',NULL,NULL,'en','en'),
 ('71b49c54-f914-473c-9c73-2d76e28961e3',NULL,NULL,'en','en'),
 ('73e32a62-af80-41c0-8b32-07523441593a',NULL,NULL,'en','en'),
 ('76496c1a-45d9-419a-b64c-035db88dc2a2',NULL,NULL,'en','en'),
 ('76b9ae0a-1825-4e53-8896-62951146dace',NULL,NULL,'en','en'),
 ('770e5d6a-e989-44b2-a411-206ffa26a7b7',NULL,NULL,'en','en'),
 ('781489d0-4ee1-43f3-8319-936e914000c3',NULL,'Guest',NULL,NULL),
 ('7a48b64a-1eed-4964-9665-ca5ecce43d13',NULL,NULL,'en','en'),
 ('7af75019-3e92-4500-b7db-3d33698967be',NULL,'Guest',NULL,NULL),
 ('7ca422b1-1da0-4380-a433-1334ecb332b1',NULL,NULL,'en','en'),
 ('7e5ed5e4-35ff-4345-9374-cef8a3fc1bfa',NULL,NULL,'en','en'),
 ('7f87e3ee-6884-4120-a827-7290bb89727d',NULL,NULL,'en','en'),
 ('810fba33-7ccd-4e26-98d3-6a8d519b2b5d',NULL,NULL,'en','en'),
 ('84d85d13-0fc0-49a9-ae92-fdcf2e79a36e','','Guest','','en'),
 ('864dddab-74e8-4b3a-804e-b64c6cc0488c',NULL,NULL,'en','en'),
 ('86bdaae5-4320-4858-acb3-0ab72958e8ba',NULL,NULL,'en','en'),
 ('86da2f2c-1a7b-496c-8551-b05838462ef0',NULL,NULL,'en','en'),
 ('86e0037d-fdc9-430b-89a4-cd5c1df22aa0',NULL,NULL,'en','en'),
 ('8af19fc4-b2f8-48a5-a8f1-e9012261e2fb',NULL,NULL,'en','en'),
 ('8edb26a2-8c0e-4774-8bca-66ea41e24a03',NULL,'Guest',NULL,NULL),
 ('8f2bc915-f135-4d5c-a0e2-e9665a304b6c',NULL,NULL,'en','en'),
 ('8fb302f2-1798-4c9e-8446-091250966370',NULL,NULL,'en','en'),
 ('906c2a76-d907-4746-ba24-24d5bca2f56a',NULL,NULL,'en','en'),
 ('91509727-20c3-4d59-8a2b-17cc6ad0225f',NULL,NULL,'en','en'),
 ('922ea773-9234-4b0a-af08-1d06f847a6cc',NULL,'Guest',NULL,NULL),
 ('927f252c-b683-4e8a-85eb-74c8a5f1542d',NULL,'Guest',NULL,NULL),
 ('98be41aa-8d04-415c-980e-8b677cadbff3',NULL,NULL,'en','en'),
 ('98c2664c-7521-473e-b81b-5542dc4d8ffb',NULL,NULL,'en','en'),
 ('9b0f358c-9a60-4c23-ac70-184686c8a599',NULL,NULL,'en','en'),
 ('9b3c42bd-4457-4ed2-943f-8ea420cfd403',NULL,NULL,'en','en'),
 ('9b52b0fe-3e20-4dc5-ad28-5591caf19d37',NULL,NULL,'en','en'),
 ('9b89c527-28eb-42f9-82ee-09eaf919a419',NULL,NULL,'en','en'),
 ('9bf8cbc0-4b00-4978-8c0e-86d4e6c6e506',NULL,'Guest',NULL,NULL),
 ('9c762a3f-a983-48f5-b883-8fd379dd50de',NULL,NULL,'en','en'),
 ('9d12d9b5-cdc3-4a2f-86b0-c25d0d067521',NULL,NULL,'en','en'),
 ('9e4c41e4-778b-4ae2-b159-1e3576f8fd30',NULL,NULL,'en','en'),
 ('9ea63cba-5f5e-46ec-8e64-02b67fd2231e',NULL,NULL,'en','en'),
 ('9eaa0915-da96-4114-8251-4f08e37199ee',NULL,NULL,'en','en'),
 ('a18ea379-3e1b-493b-afc4-dea661318633',NULL,NULL,'en','en'),
 ('a199e0b8-cdbc-457a-8cf8-16759f10e044',NULL,NULL,'en','en'),
 ('a20cc85c-dcc5-423d-94c6-6bc85a27ce3b',NULL,NULL,'en','en'),
 ('a3115ce2-c65f-4972-a8f7-c738c0b0cff1',NULL,NULL,'en','en'),
 ('a315d787-1c1f-4faf-8748-e2981f5164e3',NULL,NULL,'en','en'),
 ('a864ec0c-9761-4c42-9e0e-0ec6af54c667',NULL,'Guest',NULL,NULL),
 ('aae908f8-7963-4564-9494-96afaef7535e',NULL,NULL,'en','en'),
 ('abaf1354-cc42-44bd-b585-ad98b44380cd',NULL,'Guest',NULL,NULL),
 ('admin',NULL,NULL,'en','en'),
 ('aedfe464-1ff6-464a-99c1-138ddda867d0',NULL,NULL,'en','en'),
 ('aff6d6dc-9daf-4d0e-8e6a-6a16ddc68716',NULL,NULL,'en','en'),
 ('b181b42f-64db-4149-aa97-081222e015da',NULL,'Guest',NULL,NULL),
 ('b4da27d1-8a3c-48fe-9b08-91019f57767a',NULL,NULL,'en','en'),
 ('b56b93aa-dd20-4ece-8863-c2ac63ab218a',NULL,NULL,'en','en'),
 ('b70cd06b-6ef8-4335-ab80-852a8abcfad9',NULL,NULL,'en','en'),
 ('bada7918-8c87-471a-bc2a-761255c6fcd1',NULL,NULL,'en','en'),
 ('bbd47022-db7e-40ec-9602-826799b5de5c','','Guest','','en'),
 ('bc93c4f4-8610-46df-b8fb-ae4747b05a38',NULL,NULL,'en','en'),
 ('bd8a4258-2d4e-4b50-b0b3-34294ba4e0fc',NULL,NULL,'en','en'),
 ('c145b5c8-04ae-4eef-9856-3aaa2f138a2a',NULL,NULL,'en','en'),
 ('c34b309e-53d2-4f40-8e7b-8107d1cadf4e',NULL,NULL,'en','en'),
 ('c42effeb-706d-4987-98f6-bfa77902e6c6',NULL,'Guest',NULL,NULL),
 ('c4a4376c-a1e4-47b1-afc4-77217a82c263','','','',''),
 ('c4f392ab-2261-4aae-bcb3-89fcc59b7a46',NULL,NULL,'en','en'),
 ('c904cd98-e8d9-40f0-a86e-0a4a7b86b6ae',NULL,NULL,'en','en'),
 ('ca77ca2f-172b-446b-8597-8afb7f89e898',NULL,NULL,'en','en'),
 ('ce2b59c5-e196-4046-96e8-28588dacf6fe',NULL,NULL,'en','en'),
 ('cfbc83de-a6f3-44c8-bc9e-f95798a79ad8',NULL,NULL,'en','en'),
 ('d0032bfc-4aaf-4b31-9ee6-ecebed57613c',NULL,NULL,'en','en'),
 ('d0ab1a1e-e3a0-4ef0-9e11-467bf4348bc9',NULL,NULL,'en','en'),
 ('d2151301-a4c6-4f32-ab8e-e0f0f3dabe2d',NULL,NULL,'en','en'),
 ('d854a87e-c887-403f-8256-a35cc69fbda6',NULL,NULL,'en','en'),
 ('db152fb2-a6fe-4db6-8f99-4739a05c68c6',NULL,NULL,'en','en'),
 ('dc28a2a1-ef00-4043-b3ba-0ae62462f97f',NULL,NULL,'en','en'),
 ('dec4f037-09c2-4198-a062-211d08df686f',NULL,NULL,'en','en'),
 ('e2b47dea-469f-466f-8375-f729aa190b02',NULL,NULL,'en','en'),
 ('e40ea3e1-f94e-437f-b556-d06f04b036bc',NULL,NULL,'en','en'),
 ('e5d1529f-1bf1-4118-a6c7-56503133e80f',NULL,NULL,'en','en'),
 ('e5de4ed1-80a2-4d76-b3b7-87e359de59a6',NULL,NULL,'en','en'),
 ('e8f624c7-7010-4291-b3d9-4fa793a6863e',NULL,NULL,'en','en'),
 ('e98b0ec7-b4d8-402d-b117-5d4d5e84b7bd',NULL,NULL,'en','en'),
 ('ed49e3ed-984a-4c60-9ba3-3c48f174ad9d',NULL,NULL,'en','en'),
 ('efec6a0f-3384-42ac-95fa-35bd9573574e',NULL,NULL,'en','en'),
 ('f1a3ff65-43d7-415f-92a7-9b637889acb0',NULL,NULL,'en','en'),
 ('f369abc0-9d08-4155-9c2b-c9761589865a',NULL,NULL,'en','en'),
 ('f3d3ac1d-0988-4bc7-be56-2f71b7511709',NULL,NULL,'en','en'),
 ('f5ca3fba-733d-4994-bd9b-5beee654608a',NULL,NULL,'en','en'),
 ('f5efc6cf-dfa0-43fb-955c-c2d44ce5fce1',NULL,NULL,'en','en'),
 ('f87df655-2bb6-4aff-a1b1-92f9c7828c2e',NULL,NULL,'en','en'),
 ('f95b69b2-06ea-4fdb-8a2c-fd600499ba57',NULL,NULL,'en','en'),
 ('f96a4041-dafe-4c57-8e68-382bd0e4f877','','Guest','','en'),
 ('fdc8c61e-8233-4a85-bdc4-d3eb962dafde',NULL,'Guest',NULL,NULL),
 ('ff2abd1c-705f-425f-ad24-6435fe99a790',NULL,NULL,'en','en'),
 ('http://spookytooth.myopenid.com/','robcon@microsoft.com','Rob','Conery','en'),
 ('robconery','rob@wekeroad.com','Rob','Conery','en');
/*!40000 ALTER TABLE `customers` ENABLE KEYS */;


--
-- Definition of table `inventoryrecords`
--

DROP TABLE IF EXISTS `inventoryrecords`;
CREATE TABLE `inventoryrecords` (
  `InventoryRecordID` int(10) NOT NULL AUTO_INCREMENT,
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Increment` int(10) NOT NULL DEFAULT '0',
  `DateEntered` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Notes` varchar(500) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`InventoryRecordID`),
  KEY `FK_InventoryRecords_Products` (`SKU`),
  CONSTRAINT `FK_InventoryRecords_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inventoryrecords`
--

/*!40000 ALTER TABLE `inventoryrecords` DISABLE KEYS */;
INSERT INTO `inventoryrecords` (`InventoryRecordID`,`SKU`,`Increment`,`DateEntered`,`Notes`) VALUES 
 (29,'Hat1',-1,'2008-09-30 10:51:56','Debiting inventory for order f68be88c-2469-4642-8620-1dd692d11f6c'),
 (30,'Binoculars1',-1,'2008-09-30 10:59:50','Adjustment for order 19ef4fc0-d1dc-4363-8e17-bcb9fc4593a7'),
 (31,'GPS1',-1,'2008-09-30 11:06:00','Debiting inventory for order 0d405783-c278-48f0-9b6b-c725a5ee303b'),
 (32,'Backpack1',-1,'2008-10-08 10:40:59','Debiting inventory for order fcf233fb-d622-4b47-b2da-0e59c4d39896'),
 (33,'Boots2',-1,'2008-10-08 10:40:59','Debiting inventory for order fcf233fb-d622-4b47-b2da-0e59c4d39896'),
 (34,'Compass1',-1,'2008-10-08 10:40:59','Debiting inventory for order fcf233fb-d622-4b47-b2da-0e59c4d39896'),
 (35,'Boots1',-1,'2008-10-08 10:53:32','Debiting inventory for order d53b63fd-c7b1-4b36-a882-d5dd0c5320d0'),
 (36,'Compass1',-1,'2008-10-08 10:53:32','Debiting inventory for order d53b63fd-c7b1-4b36-a882-d5dd0c5320d0'),
 (37,'Rope1',-1,'2008-10-08 10:58:41','Debiting inventory for order 8f3bb88b-bb0a-41b0-afcd-fb806c2b88ed'),
 (38,'Tent1',-1,'2008-10-08 10:58:41','Debiting inventory for order 8f3bb88b-bb0a-41b0-afcd-fb806c2b88ed'),
 (39,'Hat1',-1,'2008-10-08 11:06:48','Debiting inventory for order 836df1e6-d3b0-4161-992c-b5691481b367'),
 (40,'SleepingBag1',-1,'2008-10-08 11:06:48','Debiting inventory for order 836df1e6-d3b0-4161-992c-b5691481b367'),
 (41,'Boots2',-2,'2008-10-08 11:14:56','Debiting inventory for order cf1b554d-0c2b-4050-9dd7-aee7d897af48'),
 (42,'GPS1',-1,'2008-10-08 11:14:56','Debiting inventory for order cf1b554d-0c2b-4050-9dd7-aee7d897af48'),
 (43,'Binoculars1',-1,'2008-10-08 11:18:12','Debiting inventory for order a89f6272-c71e-4344-acef-dee020bcfa22'),
 (44,'Hat2',-1,'2008-10-08 11:18:12','Debiting inventory for order a89f6272-c71e-4344-acef-dee020bcfa22'),
 (45,'Caribiner1',-1,'2008-10-08 11:20:07','Debiting inventory for order 49c67035-2af0-40b0-b743-9a02c796eb52'),
 (46,'GPS1',-1,'2008-10-08 11:20:07','Debiting inventory for order 49c67035-2af0-40b0-b743-9a02c796eb52'),
 (47,'Compass2',-1,'2008-10-08 11:28:41','Debiting inventory for order cfe1e457-f37e-4896-b543-5d7131ad2167'),
 (48,'Tent1',-1,'2008-10-08 11:28:41','Debiting inventory for order cfe1e457-f37e-4896-b543-5d7131ad2167'),
 (49,'GPS1',-1,'2008-10-08 11:41:34','Debiting inventory for order 01836fb3-4a6b-41c7-a3fa-dec7d15f3d27'),
 (50,'Sunglasses1',-2,'2008-10-08 11:41:34','Debiting inventory for order 01836fb3-4a6b-41c7-a3fa-dec7d15f3d27'),
 (51,'Hat2',-1,'2008-10-08 11:44:27','Debiting inventory for order 45e320b1-fb4e-48ef-85f0-ed9f05145952'),
 (52,'Rope1',-1,'2008-10-08 11:44:27','Debiting inventory for order 45e320b1-fb4e-48ef-85f0-ed9f05145952'),
 (53,'Binoculars1',-1,'2008-10-08 11:53:51','Debiting inventory for order 98072e92-1237-43c5-bbdb-3fb2adafb4cb'),
 (54,'Boots1',-2,'2008-10-08 11:53:51','Debiting inventory for order 98072e92-1237-43c5-bbdb-3fb2adafb4cb'),
 (55,'Hat1',-1,'2008-10-20 10:45:17','Debiting inventory for order 677495e6-b05d-4a5d-be14-68dc8c7bea7a'),
 (56,'Hat1',-1,'2008-10-20 11:15:47','Debiting inventory for order b5b8e9a2-29b6-4b62-8d1d-f0df67356acb'),
 (57,'Sunglasses1',-1,'2008-10-20 11:17:48','Debiting inventory for order c82d17d8-4bad-4b7e-8287-2b942a209eb6'),
 (58,'Binoculars2',-1,'2008-10-20 11:21:10','Debiting inventory for order 6c80c418-3441-4ebf-abbf-4ecec6a73fd4'),
 (59,'Binoculars1',-1,'2008-10-20 11:23:03','Debiting inventory for order df0b5ee6-f320-4600-b01a-77f87abdb94b'),
 (60,'Binoculars2',-1,'2008-10-21 14:15:00','Debiting inventory for order 74be119f-f90f-443d-b795-d9e27de6e23b');
/*!40000 ALTER TABLE `inventoryrecords` ENABLE KEYS */;


--
-- Definition of table `inventorystatus`
--

DROP TABLE IF EXISTS `inventorystatus`;
CREATE TABLE `inventorystatus` (
  `InventoryStatusID` int(10) NOT NULL AUTO_INCREMENT,
  `Description` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`InventoryStatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `inventorystatus`
--

/*!40000 ALTER TABLE `inventorystatus` DISABLE KEYS */;
INSERT INTO `inventorystatus` (`InventoryStatusID`,`Description`) VALUES 
 (1,'In Stock'),
 (2,'Back Order'),
 (3,'Pre-order'),
 (4,'Special Order'),
 (5,'Discontinued'),
 (6,'CurrentlyUnavailable');
/*!40000 ALTER TABLE `inventorystatus` ENABLE KEYS */;


--
-- Definition of table `objectstore`
--

DROP TABLE IF EXISTS `objectstore`;
CREATE TABLE `objectstore` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `ObjectKey` varchar(50) CHARACTER SET utf8 NOT NULL,
  `SearchString` varchar(50) CHARACTER SET utf8 NOT NULL,
  `SystemType` varchar(150) CHARACTER SET utf8 NOT NULL,
  `Data` longblob NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ModifiedOn` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7763 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `objectstore`
--

/*!40000 ALTER TABLE `objectstore` DISABLE KEYS */;
INSERT INTO `objectstore` (`ID`,`ObjectKey`,`SearchString`,`SystemType`,`Data`,`CreatedOn`,`ModifiedOn`) VALUES 
 (662,'PluginSetting','FakePaymentGateway','PluginSetting',0x3C506C7567696E53657474696E6720786D6C6E733D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E6672617374727563747572652220786D6C6E733A693D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D612D696E7374616E6365223E3C4973456E61626C65643E66616C73653C2F4973456E61626C65643E3C506C7567696E4E616D653E46616B655061796D656E74476174657761793C2F506C7567696E4E616D653E3C53657474696E677320786D6C6E733A613D22687474703A2F2F736368656D61732E6D6963726F736F66742E636F6D2F323030332F31302F53657269616C697A6174696F6E2F417272617973223E3C613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B65793E4973456E61626C65643C2F613A4B65793E3C613A56616C756520693A747970653D22623A737472696E672220786D6C6E733A623D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61223E747275653C2F613A56616C75653E3C2F613A4B657956616C75654F66737472696E67616E79547970653E3C2F53657474696E67733E3C2F506C7567696E53657474696E673E,'2009-02-12 15:30:14','2009-02-12 16:21:06'),
 (696,'PluginSetting','AkismetSpamBlocker','PluginSetting',0x3C506C7567696E53657474696E6720786D6C6E733D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E6672617374727563747572652220786D6C6E733A693D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D612D696E7374616E6365223E3C4973456E61626C65643E66616C73653C2F4973456E61626C65643E3C506C7567696E4E616D653E416B69736D65745370616D426C6F636B65723C2F506C7567696E4E616D653E3C53657474696E677320786D6C6E733A613D22687474703A2F2F736368656D61732E6D6963726F736F66742E636F6D2F323030332F31302F53657269616C697A6174696F6E2F417272617973223E3C613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B65793E416B69736D65744B65793C2F613A4B65793E3C613A56616C756520693A747970653D22623A737472696E672220786D6C6E733A623D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61223E414B49534D4554204B4559213C2F613A56616C75653E3C2F613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B65793E4D79446174654669656C643C2F613A4B65793E3C613A56616C756520693A747970653D22623A737472696E672220786D6C6E733A623D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61223E30322F30332F323030393C2F613A56616C75653E3C2F613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B65793E4D6178696D756D3C2F613A4B65793E3C613A56616C756520693A747970653D22623A737472696E672220786D6C6E733A623D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61223E313C2F613A56616C75653E3C2F613A4B657956616C75654F66737472696E67616E79547970653E3C2F53657474696E67733E3C2F506C7567696E53657474696E673E,'2009-02-12 16:21:21','2009-03-13 18:35:25'),
 (2140,'PluginSetting','LinkToLoremIpsum','PluginSetting',0x3C506C7567696E53657474696E6720786D6C6E733D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E6672617374727563747572652220786D6C6E733A693D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D612D696E7374616E6365223E3C4973456E61626C65643E66616C73653C2F4973456E61626C65643E3C506C7567696E4E616D653E4C696E6B546F4C6F72656D497073756D3C2F506C7567696E4E616D653E3C53657474696E677320786D6C6E733A613D22687474703A2F2F736368656D61732E6D6963726F736F66742E636F6D2F323030332F31302F53657269616C697A6174696F6E2F417272617973222F3E3C2F506C7567696E53657474696E673E,'2009-03-03 09:41:49','2009-03-03 09:41:49'),
 (4540,'Cart','f96a4041-dafe-4c57-8e68-382bd0e4f877','ShoppingCart',0x3C53686F7070696E674361727420786D6C6E733D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E436F6D6D657263652E4D6F64656C2220786D6C6E733A693D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D612D696E7374616E6365223E3C42696C6C696E674164647265737320693A6E696C3D22747275652220786D6C6E733A613D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E667261737472756374757265222F3E3C4372656469744361726420693A6E696C3D2274727565222F3E3C446973636F756E74416D6F756E743E303C2F446973636F756E74416D6F756E743E3C4974656D733E3C53686F7070696E67436172744974656D3E3C4461746541646465643E323030392D30332D31335432313A30333A32352E3139362D31303A30303C2F4461746541646465643E3C446973636F756E743E303C2F446973636F756E743E3C50726F647563743E3C6B657920693A747970653D22613A737472696E672220786D6C6E733D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E6672617374727563747572652220786D6C6E733A613D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61223E42696E6F63756C617273313C2F6B65793E3C2F50726F647563743E3C5175616E746974793E313C2F5175616E746974793E3C2F53686F7070696E67436172744974656D3E3C2F4974656D733E3C53656C65637465645368697070696E6720693A6E696C3D2274727565222F3E3C5368697070696E674164647265737320693A6E696C3D22747275652220786D6C6E733A613D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E667261737472756374757265222F3E3C546178416D6F756E743E303C2F546178416D6F756E743E3C557365724E616D653E66393661343034312D646166652D346335372D386536382D3338326264306534663837373C2F557365724E616D653E3C2F53686F7070696E67436172743E,'2009-03-13 21:03:25','2009-03-13 21:03:25'),
 (7762,'PluginSetting','MVCOrderNumber','PluginSetting',0x3C506C7567696E53657474696E6720786D6C6E733D22687474703A2F2F736368656D61732E64617461636F6E74726163742E6F72672F323030342F30372F4D6963726F736F66742E5765622E496E6672617374727563747572652220786D6C6E733A693D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D612D696E7374616E6365223E3C4973456E61626C65643E66616C73653C2F4973456E61626C65643E3C506C7567696E4E616D653E4D56434F726465724E756D6265723C2F506C7567696E4E616D653E3C53657474696E677320786D6C6E733A613D22687474703A2F2F736368656D61732E6D6963726F736F66742E636F6D2F323030332F31302F53657269616C697A6174696F6E2F417272617973223E3C613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B65793E566965774E616D653C2F613A4B65793E3C613A56616C756520693A747970653D22623A737472696E672220786D6C6E733A623D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61222F3E3C2F613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B657956616C75654F66737472696E67616E79547970653E3C613A4B65793E446973706C61795A6F6E653C2F613A4B65793E3C613A56616C756520693A747970653D22623A737472696E672220786D6C6E733A623D22687474703A2F2F7777772E77332E6F72672F323030312F584D4C536368656D61222F3E3C2F613A4B657956616C75654F66737472696E67616E79547970653E3C2F53657474696E67733E3C2F506C7567696E53657474696E673E,'2009-03-16 15:48:18','2009-03-16 19:56:53');
/*!40000 ALTER TABLE `objectstore` ENABLE KEYS */;


--
-- Definition of table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
CREATE TABLE `orderitems` (
  `OrderID` varchar(64) NOT NULL,
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `Quantity` int(10) NOT NULL,
  `DateAdded` datetime NOT NULL,
  `LineItemPrice` decimal(18,0) NOT NULL,
  `Discount` decimal(18,0) NOT NULL,
  `DiscountReason` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `LineItemWeightInPounds` decimal(18,0) NOT NULL,
  PRIMARY KEY (`OrderID`,`SKU`),
  KEY `FK_OrderItems_Products` (`SKU`),
  CONSTRAINT `FK_OrderItems_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_OrderItems_Orders` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `orderitems`
--

/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
INSERT INTO `orderitems` (`OrderID`,`SKU`,`Quantity`,`DateAdded`,`LineItemPrice`,`Discount`,`DiscountReason`,`LineItemWeightInPounds`) VALUES 
 ('3729A09D-DD53-4F57-9041-3C1403121246','Flashlight1',1,'2009-04-30 16:12:47','0','0','','0'),
 ('6EBD1ACE-3890-4C41-B3E1-8233FA8B65A4','Flashlight1',100,'2009-04-27 10:15:15','0','0','','0'),
 ('BD94893B-3A9C-4FBD-89E3-831154F900B0','Flashlight1',1,'2009-05-15 14:05:17','0','0','','0');
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;


--
-- Definition of table `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders` (
  `OrderID` varchar(64) NOT NULL,
  `OrderNumber` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `UserName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `UserLanguageCode` char(2) NOT NULL,
  `TaxAmount` decimal(19,4) NOT NULL,
  `ShippingService` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ShippingAmount` decimal(19,4) NOT NULL,
  `DiscountAmount` decimal(19,4) NOT NULL,
  `DiscountReason` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ShippingAddressID` int(10) DEFAULT NULL,
  `BillingAddressID` int(10) DEFAULT NULL,
  `DateShipped` datetime DEFAULT NULL,
  `TrackingNumber` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `EstimatedDelivery` datetime DEFAULT NULL,
  `SubTotal` decimal(19,4) NOT NULL,
  `OrderStatusID` int(10) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ExecutedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime NOT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `FK_Orders_Customers` (`UserName`),
  KEY `FK_Orders_OrderStatus` (`OrderStatusID`),
  KEY `FK_Orders_Addresses` (`ShippingAddressID`),
  KEY `FK_Orders_Addresses1` (`BillingAddressID`),
  CONSTRAINT `FK_Orders_Customers` FOREIGN KEY (`UserName`) REFERENCES `customers` (`UserName`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Orders_OrderStatus` FOREIGN KEY (`OrderStatusID`) REFERENCES `orderstatus` (`OrderStatusID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Orders_Addresses` FOREIGN KEY (`ShippingAddressID`) REFERENCES `addresses` (`AddressID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Orders_Addresses1` FOREIGN KEY (`BillingAddressID`) REFERENCES `addresses` (`AddressID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `orders`
--

/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` (`OrderID`,`OrderNumber`,`UserName`,`UserLanguageCode`,`TaxAmount`,`ShippingService`,`ShippingAmount`,`DiscountAmount`,`DiscountReason`,`ShippingAddressID`,`BillingAddressID`,`DateShipped`,`TrackingNumber`,`EstimatedDelivery`,`SubTotal`,`OrderStatusID`,`CreatedOn`,`ExecutedOn`,`ModifiedOn`) VALUES 
 ('3729A09D-DD53-4F57-9041-3C1403121246',NULL,'robconery','en','0.0000','Overnight','21.0000','0.0000',NULL,70,70,NULL,NULL,NULL,'0.0000',99,'2009-04-30 16:06:12',NULL,'2009-05-15 14:21:04'),
 ('6EBD1ACE-3890-4C41-B3E1-8233FA8B65A4',NULL,'c4a4376c-a1e4-47b1-afc4-77217a82c263','en','0.0000',NULL,'0.0000','0.0000',NULL,NULL,NULL,NULL,NULL,NULL,'0.0000',99,'2009-04-27 10:15:35',NULL,'2009-04-27 10:15:35'),
 ('BD94893B-3A9C-4FBD-89E3-831154F900B0',NULL,'c42effeb-706d-4987-98f6-bfa77902e6c6','en','0.0000',NULL,'0.0000','0.0000',NULL,NULL,NULL,NULL,NULL,NULL,'0.0000',99,'2009-05-15 14:01:26',NULL,'2009-05-15 14:01:26');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;


--
-- Definition of table `orderstatus`
--

DROP TABLE IF EXISTS `orderstatus`;
CREATE TABLE `orderstatus` (
  `OrderStatusID` int(10) NOT NULL,
  `Description` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`OrderStatusID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `orderstatus`
--

/*!40000 ALTER TABLE `orderstatus` DISABLE KEYS */;
INSERT INTO `orderstatus` (`OrderStatusID`,`Description`) VALUES 
 (1,'New'),
 (2,'Submitted'),
 (3,'Verified'),
 (4,'Charged'),
 (5,'Packaging'),
 (6,'Shipped'),
 (7,'Returned'),
 (8,'Cancelled'),
 (9,'Refunded'),
 (10,'Closed'),
 (99,'Not Checked Out');
/*!40000 ALTER TABLE `orderstatus` ENABLE KEYS */;


--
-- Definition of table `productdescriptors`
--

DROP TABLE IF EXISTS `productdescriptors`;
CREATE TABLE `productdescriptors` (
  `DescriptorID` int(10) NOT NULL AUTO_INCREMENT,
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `LanguageCode` char(2) NOT NULL,
  `Title` varchar(50) CHARACTER SET utf8 NOT NULL,
  `IsDefault` tinyint(4) NOT NULL,
  `Body` varchar(2500) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`DescriptorID`),
  KEY `FK_ProductDescriptors_Products` (`SKU`),
  CONSTRAINT `FK_ProductDescriptors_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `productdescriptors`
--

/*!40000 ALTER TABLE `productdescriptors` DISABLE KEYS */;
INSERT INTO `productdescriptors` (`DescriptorID`,`SKU`,`LanguageCode`,`Title`,`IsDefault`,`Body`) VALUES 
 (1,'Backpack1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (2,'Backpack1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (3,'Backpack2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (4,'Backpack2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (5,'Backpack3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (6,'Backpack3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (7,'Backpack4','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (8,'Backpack4','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (9,'Bike1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (10,'Bike1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (11,'Bike2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (12,'Bike2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (13,'Bike3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (14,'Bike3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (19,'Boots1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (20,'Boots1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (21,'Boots2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (22,'Boots2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (23,'Boots3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (24,'Boots3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (25,'Boots4','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (26,'Boots4','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (27,'Boots5','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (28,'Boots5','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (29,'Caribiner1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (30,'Caribiner1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (31,'Caribiner2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (32,'Caribiner2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (33,'Caribiner3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (34,'Caribiner3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (35,'Compass1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (36,'Compass1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (37,'Compass2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (38,'Compass2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (39,'Compass3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (40,'Compass3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (41,'Compass4','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (42,'Compass4','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (43,'Compass5','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (44,'Compass5','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (45,'Compass6','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (46,'Compass6','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (47,'Flashlight1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (48,'Flashlight1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (49,'Flashlight2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (50,'Flashlight2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (51,'Flashlight3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (52,'Flashlight3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (53,'GPS1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (54,'GPS1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (55,'GPS2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (56,'GPS2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (57,'Hat1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (58,'Hat1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (59,'Hat2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (60,'Hat2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (61,'Hat3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (62,'Hat3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (63,'Helmet1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (64,'Helmet1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>');
INSERT INTO `productdescriptors` (`DescriptorID`,`SKU`,`LanguageCode`,`Title`,`IsDefault`,`Body`) VALUES 
 (65,'Helmet2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (66,'Helmet2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (67,'Helmet3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (68,'Helmet3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (69,'Knife1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (70,'Knife1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (71,'Knife2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (72,'Knife2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (73,'Knife3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (74,'Knife3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (75,'Lantern1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (76,'Lantern1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (77,'Lantern2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (78,'Lantern2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (79,'Pump1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (80,'Pump1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (81,'Pump2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (82,'Pump2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (83,'Rope1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (84,'Rope1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (85,'Rope2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (86,'Rope2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (87,'Rope3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (88,'Rope3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (89,'SleepingBag1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (90,'SleepingBag1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (91,'Sunglasses1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (92,'Sunglasses1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (93,'SunGlasses2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (94,'SunGlasses2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (95,'Tent1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (96,'Tent1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (97,'Tent2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (98,'Tent2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (99,'Tent3','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (100,'Tent3','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (101,'Tirerepair1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (102,'Tirerepair1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (103,'Binoculars1','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (104,'Binoculars1','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>'),
 (105,'Binoculars2','en','From The Manufacturer',0,'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget, ultrices aliquet, purus. Maecenas erat. Morbi commodo. Quisque nec purus. Duis dui.</p>\r\n<p>Nunc non ante. Nullam ac diam. Nullam vel dolor eu ante pellentesque vulputate. Aliquam erat volutpat. Suspendisse a erat. In velit. Duis tellus lorem, porta eget, tempus a, volutpat vitae, nibh. Maecenas arcu dolor, adipiscing vel, tincidunt sit amet, mattis quis, metus. Vestibulum non justo. Ut ac nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque dolor. Duis nec metus ut justo malesuada pellentesque. Etiam pretium leo ac leo. Curabitur nunc. Aliquam facilisis porta est. Cras hendrerit venenatis nisl. Sed placerat, urna sed condimentum mollis, ante elit sollicitudin est, at tempus odio dolor et quam.</p>'),
 (106,'Binoculars2','en','Technical Details',0,'<ul>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Material</b>: XX</li>\r\n	<li><b>Height</b>: XX</li>\r\n	<li><b>Width</b>: XX</li>\r\n	<li><b>Weight</b>: XX</li>\r\n	<li><b>Special Feature</b>: Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam nec nisi vel eros ornare auctor. Aenean vitae nulla. Sed in velit sit amet metus sollicitudin porttitor. Fusce non tortor. Nunc ornare imperdiet mauris. Nulla facilisi. In hac habitasse platea dictumst. Nullam ut odio eu nunc scelerisque tempor. Nulla facilisi. Quisque sem. Etiam risus dui, fringilla eget, imperdiet ac, vehicula in, nunc. Praesent pellentesque iaculis orci. Ut imperdiet dolor non turpis. In hac habitasse platea dictumst. Morbi neque sem, consequat sed, dignissim eget</li>\r\n</ul>');
/*!40000 ALTER TABLE `productdescriptors` ENABLE KEYS */;


--
-- Definition of table `productimages`
--

DROP TABLE IF EXISTS `productimages`;
CREATE TABLE `productimages` (
  `ProductImageID` int(10) NOT NULL AUTO_INCREMENT,
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `ThumbUrl` varchar(150) CHARACTER SET utf8 NOT NULL,
  `FullImageUrl` varchar(150) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`ProductImageID`),
  KEY `FK_ProductImages_Products` (`SKU`),
  CONSTRAINT `FK_ProductImages_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `productimages`
--

/*!40000 ALTER TABLE `productimages` DISABLE KEYS */;
INSERT INTO `productimages` (`ProductImageID`,`SKU`,`ThumbUrl`,`FullImageUrl`) VALUES 
 (1,'Backpack1','Backpack1_1_Thumbnail.jpg','Backpack1_1_Full.jpg'),
 (2,'Backpack1','Backpack1_2_Thumbnail.jpg','Backpack1_2_Full.jpg'),
 (3,'Backpack1','Backpack1_3_Thumbnail.jpg','Backpack1_3_Full.jpg'),
 (4,'Backpack2','Backpack2_1_Thumbnail.jpg','Backpack2_1_Full.jpg'),
 (5,'Backpack2','Backpack2_2_Thumbnail.jpg','Backpack2_2_Full.jpg'),
 (6,'Backpack2','Backpack2_3_Thumbnail.jpg','Backpack2_3_Full.jpg'),
 (7,'Backpack3','Backpack3_1_Thumbnail.jpg','Backpack3_1_Full.jpg'),
 (8,'Backpack4','Backpack4_1_Thumbnail.jpg','Backpack4_1_Full.jpg'),
 (9,'Bike1','Bike1_1_Thumbnail.jpg','Bike1_1_Full.jpg'),
 (10,'Bike2','Bike2_1_Thumbnail.jpg','Bike2_1_Full.jpg'),
 (11,'Bike3','Bike3_1_Thumbnail.jpg','Bike3_1_Full.jpg'),
 (12,'Binoculars1','Binoculars1_1_Thumbnail.jpg','Binoculars1_1_Full.jpg'),
 (13,'Binoculars2','Binoculars2_1_Thumbnail.jpg','Binoculars2_1_Full.jpg'),
 (14,'Boots1','Boots1_1_Thumbnail.jpg','Boots1_1_Full.jpg'),
 (15,'Boots2','Boots2_1_Thumbnail.jpg','Boots2_1_Full.jpg'),
 (16,'Boots2','Boots2_2_Thumbnail.jpg','Boots2_2_Full.jpg'),
 (17,'Boots3','Boots3_1_Thumbnail.jpg','Boots3_1_Full.jpg'),
 (18,'Boots4','Boots4_1_Thumbnail.jpg','Boots4_1_Full.jpg'),
 (19,'Boots5','Boots5_1_Thumbnail.jpg','Boots5_1_Full.jpg'),
 (20,'Caribiner1','Caribiner1_1_Thumbnail.jpg','Caribiner1_1_Full.jpg'),
 (21,'Caribiner1','Caribiner1_2_Thumbnail.jpg','Caribiner1_2_Full.jpg'),
 (22,'Caribiner2','Caribiner2_1_Thumbnail.jpg','Caribiner2_1_Full.jpg'),
 (23,'Caribiner3','Caribiner3_1_Thumbnail.jpg','Caribiner3_1_Full.jpg'),
 (24,'Caribiner3','Caribiner3_2_Thumbnail.jpg','Caribiner3_2_Full.jpg'),
 (25,'Caribiner3','Caribiner3_3_Thumbnail.jpg','Caribiner3_3_Full.jpg'),
 (26,'Compass1','Compass1_1_Thumbnail.jpg','Compass1_1_Full.jpg'),
 (27,'Compass2','Compass2_1_Thumbnail.jpg','Compass2_1_Full.jpg'),
 (28,'Compass3','Compass3_1_Thumbnail.jpg','Compass3_1_Full.jpg'),
 (29,'Compass4','Compass4_1_Thumbnail.jpg','Compass4_1_Full.jpg'),
 (30,'Compass5','Compass5_1_Thumbnail.jpg','Compass5_1_Full.jpg'),
 (31,'Compass6','Compass6_1_Thumbnail.jpg','Compass6_1_Full.jpg'),
 (32,'Flashlight1','Flashlight1_1_Thumbnail.jpg','Flashlight1_1_Full.jpg'),
 (33,'Flashlight1','Flashlight1_2_Thumbnail.jpg','Flashlight1_2_Full.jpg'),
 (34,'Flashlight1','Flashlight1_3_Thumbnail.jpg','Flashlight1_3_Full.jpg'),
 (35,'Flashlight2','Flashlight2_1_Thumbnail.jpg','Flashlight2_1_Full.jpg'),
 (36,'Flashlight2','Flashlight2_2_Thumbnail.jpg','Flashlight2_2_Full.jpg'),
 (37,'Flashlight2','Flashlight2_3_Thumbnail.jpg','Flashlight2_3_Full.jpg'),
 (38,'Flashlight3','Flashlight3_1_Thumbnail.jpg','Flashlight3_1_Full.jpg'),
 (39,'Flashlight3','Flashlight3_2_Thumbnail.jpg','Flashlight3_2_Full.jpg'),
 (40,'GPS1','GPS1_1_Thumbnail.jpg','GPS1_1_Full.jpg'),
 (41,'GPS2','GPS2_1_Thumbnail.jpg','GPS2_1_Full.jpg'),
 (42,'Hat1','Hat1_1_Thumbnail.jpg','Hat1_1_Full.jpg'),
 (43,'Hat2','Hat2_1_Thumbnail.jpg','Hat2_1_Full.jpg'),
 (44,'Hat3','Hat3_1_Thumbnail.jpg','Hat3_1_Full.jpg'),
 (45,'Helmet1','Helmet1_1_Thumbnail.jpg','Helmet1_1_Full.jpg'),
 (46,'Helmet1','Helmet1_2_Thumbnail.jpg','Helmet1_2_Full.jpg'),
 (47,'Helmet2','Helmet2_1_Thumbnail.jpg','Helmet2_1_Full.jpg'),
 (48,'Helmet2','Helmet2_2_Thumbnail.jpg','Helmet2_2_Full.jpg'),
 (49,'Helmet2','Helmet2_3_Thumbnail.jpg','Helmet2_3_Full.jpg'),
 (50,'Helmet3','Helmet3_1_Thumbnail.jpg','Helmet3_1_Full.jpg'),
 (51,'Knife1','Knife1_1_Thumbnail.jpg','Knife1_1_Full.jpg'),
 (52,'Knife2','Knife2_1_Thumbnail.jpg','Knife2_1_Full.jpg'),
 (53,'Knife3','Knife3_1_Thumbnail.jpg','Knife3_1_Full.jpg'),
 (54,'Knife3','Knife3_2_Thumbnail.jpg','Knife3_2_Full.jpg'),
 (55,'Lantern1','Lantern1_1_Thumbnail.jpg','Lantern1_1_Full.jpg'),
 (56,'Lantern2','Lantern2_1_Thumbnail.jpg','Lantern2_1_Full.jpg'),
 (57,'Lantern2','Lantern2_2_Thumbnail.jpg','Lantern2_2_Full.jpg'),
 (58,'Lantern2','Lantern2_3_Thumbnail.jpg','Lantern2_3_Full.jpg'),
 (59,'Pump1','Pump1_1_Thumbnail.jpg','Pump1_1_Full.jpg'),
 (60,'Pump2','Pump2_1_Thumbnail.jpg','Pump2_1_Full.jpg'),
 (61,'Rope1','Rope1_1_Thumbnail.jpg','Rope1_1_Full.jpg'),
 (62,'Rope2','Rope2_1_Thumbnail.jpg','Rope2_1_Full.jpg'),
 (63,'Rope3','Rope3_1_Thumbnail.jpg','Rope3_1_Full.jpg'),
 (64,'Rope3','Rope3_2_Thumbnail.jpg','Rope3_2_Full.jpg'),
 (65,'Rope3','Rope3_3_Thumbnail.jpg','Rope3_3_Full.jpg'),
 (66,'SleepingBag1','SleepingBag1_1_Thumbnail.jpg','SleepingBag1_1_Full.jpg'),
 (67,'SleepingBag1','SleepingBag1_2_Thumbnail.jpg','SleepingBag1_2_Full.jpg'),
 (68,'SleepingBag1','SleepingBag1_3_Thumbnail.jpg','SleepingBag1_3_Full.jpg'),
 (69,'Sunglasses1','Sunglasses1_1_Thumbnail.jpg','Sunglasses1_1_Full.jpg'),
 (70,'Sunglasses1','Sunglasses1_2_Thumbnail.jpg','Sunglasses1_2_Full.jpg'),
 (71,'Sunglasses1','Sunglasses1_3_Thumbnail.jpg','Sunglasses1_3_Full.jpg'),
 (72,'SunGlasses2','SunGlasses2_1_Thumbnail.jpg','SunGlasses2_1_Full.jpg'),
 (73,'SunGlasses2','SunGlasses2_2_Thumbnail.jpg','SunGlasses2_2_Full.jpg'),
 (74,'SunGlasses2','SunGlasses2_3_Thumbnail.jpg','SunGlasses2_3_Full.jpg'),
 (75,'Tent1','Tent1_1_Thumbnail.jpg','Tent1_1_Full.jpg'),
 (76,'Tent1','Tent1_2_Thumbnail.jpg','Tent1_2_Full.jpg'),
 (77,'Tent2','Tent2_1_Thumbnail.jpg','Tent2_1_Full.jpg'),
 (78,'Tent2','Tent2_2_Thumbnail.jpg','Tent2_2_Full.jpg'),
 (79,'Tent2','Tent2_3_Thumbnail.jpg','Tent2_3_Full.jpg'),
 (80,'Tent3','Tent3_1_Thumbnail.jpg','Tent3_1_Full.jpg'),
 (81,'Tent3','Tent3_2_Thumbnail.jpg','Tent3_2_Full.jpg'),
 (82,'Tent3','Tent3_3_Thumbnail.jpg','Tent3_3_Full.jpg'),
 (83,'Tirerepair1','Tirerepair1_1_Thumbnail.jpg','Tirerepair1_1_Full.jpg');
/*!40000 ALTER TABLE `productimages` ENABLE KEYS */;


--
-- Definition of table `products`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE `products` (
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `SiteID` varchar(64) NOT NULL,
  `DeliveryMethodID` int(10) NOT NULL,
  `ProductName` varchar(50) CHARACTER SET utf8 NOT NULL,
  `BasePrice` decimal(18,0) NOT NULL,
  `WeightInPounds` decimal(19,4) NOT NULL,
  `DateAvailable` datetime NOT NULL,
  `InventoryStatusID` int(10) NOT NULL,
  `EstimatedDelivery` varchar(50) CHARACTER SET utf8 NOT NULL,
  `AllowBackOrder` tinyint(4) NOT NULL,
  `IsTaxable` tinyint(4) NOT NULL,
  `DefaultImageFile` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `AmountOnHand` int(10) NOT NULL,
  `AllowPreOrder` tinyint(4) NOT NULL,
  PRIMARY KEY (`SKU`),
  KEY `FK_Products_InventoryStatus` (`InventoryStatusID`),
  CONSTRAINT `FK_Products_InventoryStatus` FOREIGN KEY (`InventoryStatusID`) REFERENCES `inventorystatus` (`InventoryStatusID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `products`
--

/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` (`SKU`,`SiteID`,`DeliveryMethodID`,`ProductName`,`BasePrice`,`WeightInPounds`,`DateAvailable`,`InventoryStatusID`,`EstimatedDelivery`,`AllowBackOrder`,`IsTaxable`,`DefaultImageFile`,`AmountOnHand`,`AllowPreOrder`) VALUES 
 ('Backpack1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Hiking Backpack','50','1.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Backpack1_1_Thumbnail.jpg',10,1),
 ('Backpack2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Wide-base Backpack','50','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Backpack2_1_Thumbnail.jpg',10,1),
 ('Backpack3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Short Backpack','40','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Backpack3_1_Thumbnail.jpg',10,1),
 ('Backpack4','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Mountaineering Backpack','130','1.0000','2003-01-01 00:00:00',1,'2-3 Days',1,1,'Backpack4_1_Thumbnail.jpg',10,1),
 ('Bike1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Sprint 500 Bike','460','10.2000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Bike1_1_Thumbnail.jpg',10,1),
 ('Bike2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Escape 3.0 Bike','680','10.2500','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Bike2_1_Thumbnail.jpg',10,1),
 ('Bike3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Scoop Cruiser','380','10.3500','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Bike3_1_Thumbnail.jpg',10,1),
 ('Binoculars1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Adventure Works 20x50 Binoculars','170','1.5000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Binoculars1_1_Thumbnail.jpg',10,1),
 ('Binoculars2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Adventure Works 20x30 Binoculars','170','1.5000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Binoculars2_1_Thumbnail.jpg',10,1),
 ('Boots1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Sierra Leather Hiking Boots','90','2.2500','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Boots1_1_Thumbnail.jpg',10,1),
 ('Boots2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Rainier Leather Hiking Boots','110','2.3500','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Boots2_1_Thumbnail.jpg',10,1),
 ('Boots3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Cascade Fur-lined Hiking Boots','130','2.4660','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Boots3_1_Thumbnail.jpg',10,1),
 ('Boots4','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Adirondak Fur-lined Hiking Boots','60','0.0000','2003-01-01 00:00:00',1,'2-3 Days',1,1,'Boots4_1_Thumbnail.jpg',10,1),
 ('Boots5','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Olympic Hiking Boots','90','2.1000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Boots5_1_Thumbnail.jpg',10,1),
 ('Caribiner1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Sentinel Locking Carabiner','16','0.1000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Caribiner1_1_Thumbnail.jpg',10,1),
 ('Caribiner2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Guardian Locking Carabiner','6','0.1000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Caribiner2_1_Thumbnail.jpg',10,1),
 ('Caribiner3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Trailhead Locking Carabiner','8','0.1000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Caribiner3_1_Thumbnail.jpg',10,1),
 ('Compass1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'TrailGuide Compass','30','1.2000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Compass1_1_Thumbnail.jpg',10,1),
 ('Compass2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'North Star Compass','18','1.2000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Compass2_1_Thumbnail.jpg',10,1),
 ('Compass3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'SunDial Compass','12','1.2000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Compass3_1_Thumbnail.jpg',10,1),
 ('Compass4','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'The Navigator Compass','15','1.6000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Compass4_1_Thumbnail.jpg',10,1),
 ('Compass5','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Polar Star Compass','12','0.0000','2003-01-01 00:00:00',1,'2-3 Days',1,1,'Compass5_1_Thumbnail.jpg',10,1),
 ('Compass6','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Compass Necklace','16','1.7000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Compass6_1_Thumbnail.jpg',10,1),
 ('Flashlight1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Battery Operated Flashlight','8','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Flashlight1_1_Thumbnail.jpg',10,1),
 ('Flashlight2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Heavy-Duty Flashlight','24','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Flashlight2_1_Thumbnail.jpg',10,1),
 ('Flashlight3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Retro Flashlight','19','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Flashlight3_1_Thumbnail.jpg',10,1),
 ('GPS1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Hand-Held Global Positioning System','140','1.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'GPS1_1_Thumbnail.jpg',10,1),
 ('GPS2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Dashboard Global Positioning System','150','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'GPS2_1_Thumbnail.jpg',10,1),
 ('Hat1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Weathered Leather Baseball Cap','13','0.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Hat1_1_Thumbnail.jpg',10,1),
 ('Hat2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Colorful Straw Hat','10','1.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Hat2_1_Thumbnail.jpg',10,1),
 ('Hat3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Summertime Straw Hat','23','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Hat3_1_Thumbnail.jpg',10,1),
 ('Helmet1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Bicycle Safety Helmet','80','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Helmet1_1_Thumbnail.jpg',10,1),
 ('Helmet2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Fusion Helmet','150','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Helmet2_1_Thumbnail.jpg',10,1),
 ('Helmet3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Fire Helmet','125','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Helmet3_1_Thumbnail.jpg',10,1),
 ('Knife1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Multipurpose Utility Knife','16','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Knife1_1_Thumbnail.jpg',10,1),
 ('Knife2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Multipurpose Utility Tool','18','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Knife2_1_Thumbnail.jpg',10,1),
 ('Knife3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Adventure Works Classic Knife','19','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Knife3_1_Thumbnail.jpg',10,1),
 ('Lantern1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Celestial Lantern','46','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Lantern1_1_Thumbnail.jpg',10,1),
 ('Lantern2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Moon Lantern','50','3.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Lantern2_1_Thumbnail.jpg',10,1),
 ('Pump1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Bike Pump','20','5.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Pump1_1_Thumbnail.jpg',10,1),
 ('Pump2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Air Pump','25','5.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Pump2_1_Thumbnail.jpg',10,1),
 ('Rope1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'AdventureWorks Climbing Rope','25','4.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Rope1_1_Thumbnail.jpg',10,1),
 ('Rope2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Climbing Rope with Chrome Caribiners','25','4.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Rope2_1_Thumbnail.jpg',10,1),
 ('Rope3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Climbing Rope with Single Caribiner','25','4.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Rope3_1_Thumbnail.jpg',10,1),
 ('SleepingBag1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Dream 0 Degree Sleeping Bag','55','8.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'SleepingBag1_1_Thumbnail.jpg',10,1),
 ('Sunglasses1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Northwind Traders Arizona Sunglasses','35','1.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Sunglasses1_1_Thumbnail.jpg',10,1),
 ('SunGlasses2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Northwind Traders Eclipse Sunglasses','55','1.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'SunGlasses2_1_Thumbnail.jpg',10,1),
 ('Tent1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Adventure Works Basic 4 Tent','130','20.0000','2003-01-01 00:00:00',2,'2-3 Weeks',1,1,'Tent1_1_Thumbnail.jpg',10,1),
 ('Tent2','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Adventure Works Marcus 2 Tent','180','20.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Tent2_1_Thumbnail.jpg',10,1),
 ('Tent3','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Northwind Traders Arrow 2 Tent','80','20.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Tent3_1_Thumbnail.jpg',10,1),
 ('Tirerepair1','1C3F09E7-8EE8-46C5-B9B1-1C1FC1144B5A',1,'Tire Repair Kit','10','0.0000','2003-01-01 00:00:00',1,'3-5 Days',1,1,'Tirerepair1_1_Thumbnail.jpg',10,1);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;


--
-- Definition of table `products_crosssell`
--

DROP TABLE IF EXISTS `products_crosssell`;
CREATE TABLE `products_crosssell` (
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `CrossSKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`SKU`,`CrossSKU`),
  KEY `FK_Products_CrossSell_Products1` (`CrossSKU`),
  CONSTRAINT `FK_Products_CrossSell_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Products_CrossSell_Products1` FOREIGN KEY (`CrossSKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `products_crosssell`
--

/*!40000 ALTER TABLE `products_crosssell` DISABLE KEYS */;
/*!40000 ALTER TABLE `products_crosssell` ENABLE KEYS */;


--
-- Definition of table `products_related`
--

DROP TABLE IF EXISTS `products_related`;
CREATE TABLE `products_related` (
  `SKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  `RelatedSKU` varchar(50) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`SKU`,`RelatedSKU`),
  KEY `FK_Products_Related_Products1` (`RelatedSKU`),
  CONSTRAINT `FK_Products_Related_Products` FOREIGN KEY (`SKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Products_Related_Products1` FOREIGN KEY (`RelatedSKU`) REFERENCES `products` (`SKU`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `products_related`
--

/*!40000 ALTER TABLE `products_related` DISABLE KEYS */;
INSERT INTO `products_related` (`SKU`,`RelatedSKU`) VALUES 
 ('Backpack2','Backpack1'),
 ('Backpack3','Backpack1'),
 ('Backpack4','Backpack1'),
 ('Backpack1','Backpack2'),
 ('Backpack3','Backpack2'),
 ('Backpack4','Backpack2'),
 ('Backpack1','Backpack3'),
 ('Backpack2','Backpack3'),
 ('Backpack4','Backpack3'),
 ('Backpack1','Backpack4'),
 ('Backpack2','Backpack4'),
 ('Backpack3','Backpack4'),
 ('Bike2','Bike1'),
 ('Bike3','Bike1'),
 ('Pump1','Bike1'),
 ('Pump2','Bike1'),
 ('Bike1','Bike2'),
 ('Bike3','Bike2'),
 ('Pump1','Bike2'),
 ('Pump2','Bike2'),
 ('Bike1','Bike3'),
 ('Bike2','Bike3'),
 ('Pump1','Bike3'),
 ('Pump2','Bike3'),
 ('Compass1','Binoculars1'),
 ('Compass2','Binoculars1'),
 ('Compass3','Binoculars1'),
 ('Compass4','Binoculars1'),
 ('Compass5','Binoculars1'),
 ('Compass6','Binoculars1'),
 ('Compass1','Binoculars2'),
 ('Compass2','Binoculars2'),
 ('Compass3','Binoculars2'),
 ('Compass4','Binoculars2'),
 ('Compass5','Binoculars2'),
 ('Compass6','Binoculars2'),
 ('Boots2','Boots1'),
 ('Boots3','Boots1'),
 ('Boots4','Boots1'),
 ('Boots5','Boots1'),
 ('Boots1','Boots2'),
 ('Boots3','Boots2'),
 ('Boots4','Boots2'),
 ('Boots5','Boots2'),
 ('Boots1','Boots3'),
 ('Boots2','Boots3'),
 ('Boots4','Boots3'),
 ('Boots5','Boots3'),
 ('Boots1','Boots4'),
 ('Boots2','Boots4'),
 ('Boots3','Boots4'),
 ('Boots5','Boots4'),
 ('Boots1','Boots5'),
 ('Boots2','Boots5'),
 ('Boots3','Boots5'),
 ('Boots4','Boots5'),
 ('Caribiner2','Caribiner1'),
 ('Caribiner3','Caribiner1'),
 ('Rope1','Caribiner1'),
 ('Rope2','Caribiner1'),
 ('Rope3','Caribiner1'),
 ('Caribiner1','Caribiner2'),
 ('Caribiner3','Caribiner2'),
 ('Rope1','Caribiner2'),
 ('Rope2','Caribiner2'),
 ('Rope3','Caribiner2'),
 ('Caribiner1','Caribiner3'),
 ('Caribiner2','Caribiner3'),
 ('Rope1','Caribiner3'),
 ('Rope2','Caribiner3'),
 ('Rope3','Caribiner3'),
 ('Compass2','Compass1'),
 ('Compass3','Compass1'),
 ('Compass4','Compass1'),
 ('Compass5','Compass1'),
 ('Compass6','Compass1'),
 ('GPS1','Compass1'),
 ('GPS2','Compass1'),
 ('Compass1','Compass2'),
 ('Compass3','Compass2'),
 ('Compass4','Compass2'),
 ('Compass5','Compass2'),
 ('Compass6','Compass2'),
 ('GPS1','Compass2'),
 ('GPS2','Compass2'),
 ('Compass1','Compass3'),
 ('Compass2','Compass3'),
 ('Compass4','Compass3'),
 ('Compass5','Compass3'),
 ('Compass6','Compass3'),
 ('GPS1','Compass3'),
 ('GPS2','Compass3'),
 ('Compass1','Compass4'),
 ('Compass2','Compass4'),
 ('Compass3','Compass4'),
 ('Compass5','Compass4'),
 ('Compass6','Compass4'),
 ('GPS1','Compass4'),
 ('GPS2','Compass4'),
 ('Compass1','Compass5'),
 ('Compass2','Compass5'),
 ('Compass3','Compass5'),
 ('Compass4','Compass5'),
 ('Compass6','Compass5'),
 ('GPS1','Compass5'),
 ('GPS2','Compass5'),
 ('Compass2','Compass6'),
 ('Compass5','Compass6'),
 ('GPS1','Compass6'),
 ('GPS2','Compass6'),
 ('Flashlight2','Flashlight1'),
 ('Flashlight3','Flashlight1'),
 ('Lantern1','Flashlight1'),
 ('Lantern2','Flashlight1'),
 ('Flashlight1','Flashlight2'),
 ('Flashlight3','Flashlight2'),
 ('Lantern1','Flashlight2'),
 ('Lantern2','Flashlight2'),
 ('Flashlight1','Flashlight3'),
 ('Flashlight2','Flashlight3'),
 ('Lantern1','Flashlight3'),
 ('Lantern2','Flashlight3'),
 ('Compass1','GPS1'),
 ('Compass2','GPS1'),
 ('Compass3','GPS1'),
 ('Compass4','GPS1'),
 ('Compass5','GPS1'),
 ('Compass6','GPS1'),
 ('GPS2','GPS1'),
 ('Compass1','GPS2'),
 ('Compass2','GPS2'),
 ('Compass3','GPS2'),
 ('Compass4','GPS2'),
 ('Compass5','GPS2'),
 ('Compass6','GPS2'),
 ('GPS1','GPS2'),
 ('Hat2','Hat1'),
 ('Hat3','Hat1'),
 ('Hat1','Hat2'),
 ('Hat1','Hat3'),
 ('Hat2','Hat3'),
 ('Bike2','Helmet1'),
 ('Bike3','Helmet1'),
 ('Binoculars1','Helmet1'),
 ('Binoculars2','Helmet1'),
 ('Helmet2','Helmet1'),
 ('Helmet3','Helmet1'),
 ('Bike2','Helmet2'),
 ('Bike3','Helmet2'),
 ('Helmet1','Helmet2'),
 ('Helmet3','Helmet2'),
 ('Bike2','Helmet3'),
 ('Bike3','Helmet3'),
 ('Helmet1','Helmet3'),
 ('Helmet2','Helmet3'),
 ('Knife2','Knife1'),
 ('Knife3','Knife1'),
 ('Knife1','Knife2'),
 ('Knife3','Knife2'),
 ('Knife1','Knife3'),
 ('Knife2','Knife3'),
 ('Flashlight1','Lantern1'),
 ('Flashlight2','Lantern1'),
 ('Flashlight3','Lantern1'),
 ('Knife1','Lantern1'),
 ('Lantern2','Lantern1'),
 ('Flashlight1','Lantern2'),
 ('Flashlight2','Lantern2'),
 ('Flashlight3','Lantern2'),
 ('Lantern1','Lantern2'),
 ('Pump2','Pump1'),
 ('Tirerepair1','Pump1'),
 ('Pump1','Pump2'),
 ('Tirerepair1','Pump2'),
 ('Caribiner1','Rope1'),
 ('Caribiner2','Rope1'),
 ('Caribiner3','Rope1'),
 ('Rope2','Rope1'),
 ('Caribiner1','Rope2'),
 ('Caribiner2','Rope2'),
 ('Caribiner3','Rope2'),
 ('Rope1','Rope2'),
 ('Rope3','Rope2'),
 ('Caribiner1','Rope3'),
 ('Caribiner2','Rope3'),
 ('Caribiner3','Rope3'),
 ('Rope1','Rope3'),
 ('Hat1','Sunglasses1'),
 ('Hat2','Sunglasses1'),
 ('Hat3','Sunglasses1'),
 ('SunGlasses2','Sunglasses1'),
 ('Hat1','SunGlasses2'),
 ('Hat2','SunGlasses2'),
 ('Hat3','SunGlasses2'),
 ('Sunglasses1','SunGlasses2'),
 ('SleepingBag1','Tent1'),
 ('SleepingBag1','Tent2'),
 ('Tent1','Tent2'),
 ('Tent3','Tent2'),
 ('SleepingBag1','Tent3'),
 ('Tent2','Tent3'),
 ('Pump1','Tirerepair1');
/*!40000 ALTER TABLE `products_related` ENABLE KEYS */;


--
-- Definition of table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
CREATE TABLE `transactions` (
  `TransactionID` varchar(64) NOT NULL,
  `OrderID` varchar(64) NOT NULL,
  `TransactionDate` datetime NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `AuthorizationCode` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Notes` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Processor` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`TransactionID`),
  KEY `FK_Transactions_Orders` (`OrderID`),
  CONSTRAINT `FK_Transactions_Orders` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `transactions`
--

/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;


--
-- Definition of table `widgets`
--

DROP TABLE IF EXISTS `widgets`;
CREATE TABLE `widgets` (
  `WidgetID` varchar(64) NOT NULL,
  `ListOrder` int(10) DEFAULT NULL,
  `Zone` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `Title` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `Body` varchar(2500) CHARACTER SET utf8 DEFAULT NULL,
  `SKUList` varchar(1500) CHARACTER SET utf8 DEFAULT NULL,
  `ViewName` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `LanguageCode` char(2) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ModifiedOn` datetime NOT NULL,
  `CreatedBy` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `ModifiedBy` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`WidgetID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `widgets`
--

/*!40000 ALTER TABLE `widgets` DISABLE KEYS */;
INSERT INTO `widgets` (`WidgetID`,`ListOrder`,`Zone`,`Title`,`Body`,`SKUList`,`ViewName`,`LanguageCode`,`CreatedOn`,`ModifiedOn`,`CreatedBy`,`ModifiedBy`) VALUES 
 ('400840E1-90FC-4DFB-9937-F39C55E13178',0,'sidebar1','','','','Favorites','en','2009-05-15 17:14:57','2009-05-15 17:14:57','',''),
 ('463E497D-1C80-4D87-A7D4-E887E444203C',0,'sidebar1','','','','RecentlyViewed','en','2009-05-18 10:53:12','2009-05-18 10:53:12','',''),
 ('864D4A03-E4BD-4406-B342-9AC88BAD9560',0,'sidebar1',NULL,NULL,NULL,'Favorites','en','2009-05-15 17:06:13','2009-05-15 17:06:13','',''),
 ('AB5F9903-5D8F-40E8-A586-BACB9D43AA8B',0,'center','Spring Sale',NULL,'Backpack1;Backpack2','ProductList','en','2009-05-15 15:22:43','2009-05-15 15:22:43',NULL,NULL),
 ('AD6F60ED-8826-4E0E-8D06-2DE60DE5F9FA',0,'sidebar2','','','','RecentlyViewed','en','2009-05-18 10:56:14','2009-05-18 10:56:14','','');
/*!40000 ALTER TABLE `widgets` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
