-- MySQL dump 10.13  Distrib 5.6.13, for Win64 (x86_64)
--
-- Host: localhost    Database: restaurant
-- ------------------------------------------------------
-- Server version	5.6.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `actualizacion`
--

DROP TABLE IF EXISTS `actualizacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `actualizacion` (
  `Nombre` varchar(6) DEFAULT NULL,
  `Orden` tinyint(4) DEFAULT NULL,
  `Pedido` tinyint(4) DEFAULT NULL,
  `NumCamOrden` int(11) DEFAULT NULL,
  `NumCamPedido` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `actualizacion`
--

LOCK TABLES `actualizacion` WRITE;
/*!40000 ALTER TABLE `actualizacion` DISABLE KEYS */;
INSERT INTO `actualizacion` VALUES ('Cambio',0,0,0,0);
/*!40000 ALTER TABLE `actualizacion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `alimento`
--

DROP TABLE IF EXISTS `alimento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `alimento` (
  `idalimento` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) NOT NULL,
  `tipoalimento` varchar(45) DEFAULT NULL,
  `descripcion` varchar(100) DEFAULT NULL,
  `precio` decimal(4,2) DEFAULT NULL,
  `estatus` varchar(15) NOT NULL,
  PRIMARY KEY (`idalimento`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `alimento`
--

LOCK TABLES `alimento` WRITE;
/*!40000 ALTER TABLE `alimento` DISABLE KEYS */;
INSERT INTO `alimento` VALUES (1,'Cereal','Entrada','tutache',10.00,'Disponible'),(2,'cuchara','Postre','jomi',1.00,'Disponible'),(3,'Galletas','Postre','asasa',12.00,'Disponible'),(4,'Gelatina','Postre','aasasa',13.00,'Disponible'),(5,'Carne','Plato fuerte','sasaasa',30.00,'Disponible'),(6,'Ensalada','Entrada','asassasas',10.00,'Disponible'),(7,'Camarones','Plato fuerte','bajo del mar',10.00,'Disponible'),(8,'Atun','Entrada','efefefefe',15.00,'Disponible'),(9,'sopanissin','Entrada','yumi',5.00,'Disponible');
/*!40000 ALTER TABLE `alimento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cliente` (
  `idcliente` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idcliente`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mesa`
--

DROP TABLE IF EXISTS `mesa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mesa` (
  `idmesa` int(11) NOT NULL AUTO_INCREMENT,
  `numpersonas` int(11) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  `idcliente` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmesa`),
  KEY `idcliente` (`idcliente`),
  CONSTRAINT `mesa_ibfk_1` FOREIGN KEY (`idcliente`) REFERENCES `cliente` (`idcliente`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mesa`
--

LOCK TABLES `mesa` WRITE;
/*!40000 ALTER TABLE `mesa` DISABLE KEYS */;
INSERT INTO `mesa` VALUES (1,6,'Disponible',NULL),(2,4,'Reservada',NULL),(3,4,'Disponible',NULL);
/*!40000 ALTER TABLE `mesa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mesero`
--

DROP TABLE IF EXISTS `mesero`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `mesero` (
  `idmesero` int(11) NOT NULL DEFAULT '0',
  `nombre` varchar(30) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idmesero`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mesero`
--

LOCK TABLES `mesero` WRITE;
/*!40000 ALTER TABLE `mesero` DISABLE KEYS */;
INSERT INTO `mesero` VALUES (0,'Jesus','Serna');
/*!40000 ALTER TABLE `mesero` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orden`
--

DROP TABLE IF EXISTS `orden`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orden` (
  `idorden` int(11) NOT NULL AUTO_INCREMENT,
  `idmesa` int(11) DEFAULT NULL,
  `idmesero` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `total` decimal(4,2) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  `actualizado` tinyint(4) NOT NULL,
  PRIMARY KEY (`idorden`),
  KEY `idmesa` (`idmesa`),
  KEY `idmesero` (`idmesero`),
  CONSTRAINT `orden_ibfk_1` FOREIGN KEY (`idmesa`) REFERENCES `mesa` (`idmesa`),
  CONSTRAINT `orden_ibfk_2` FOREIGN KEY (`idmesero`) REFERENCES `mesero` (`idmesero`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orden`
--

LOCK TABLES `orden` WRITE;
/*!40000 ALTER TABLE `orden` DISABLE KEYS */;
INSERT INTO `orden` VALUES (23,1,0,'2013-11-28 17:04:37',0.00,'PAGADA',0),(24,1,0,'2013-11-28 17:33:03',0.00,'PAGADA',0),(25,1,0,'2013-11-29 02:47:53',0.00,'PAGADA',0),(26,1,0,'2013-11-29 10:30:48',0.00,'PAGADA',0),(27,1,0,'2013-11-30 17:45:45',0.00,'ABIERTA',1),(28,3,0,'2013-11-30 17:50:14',0.00,'ABIERTA',1),(29,1,0,'2013-11-30 19:00:05',0.00,'ABIERTA',1);
/*!40000 ALTER TABLE `orden` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pedido`
--

DROP TABLE IF EXISTS `pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pedido` (
  `idpedido` int(11) NOT NULL AUTO_INCREMENT,
  `idorden` int(11) DEFAULT NULL,
  `idalimento` int(11) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  `actualizado` tinyint(4) NOT NULL,
  PRIMARY KEY (`idpedido`),
  KEY `idorden` (`idorden`),
  KEY `idalimento` (`idalimento`),
  CONSTRAINT `pedido_ibfk_1` FOREIGN KEY (`idorden`) REFERENCES `orden` (`idorden`),
  CONSTRAINT `pedido_ibfk_2` FOREIGN KEY (`idalimento`) REFERENCES `alimento` (`idalimento`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedido`
--

LOCK TABLES `pedido` WRITE;
/*!40000 ALTER TABLE `pedido` DISABLE KEYS */;
INSERT INTO `pedido` VALUES (1,24,1,'Listo',0),(2,24,6,'Entregado',0),(3,24,4,'Listo',0),(4,24,3,'Cancelado',0),(5,26,5,'Cancelado',0),(6,25,7,'Listo',0),(7,26,4,'Listo',0),(8,26,5,'Listo',0),(9,26,2,'Listo',0),(10,26,8,'Listo',0),(11,26,9,'Listo',0),(12,28,7,'Listo',0),(13,29,6,'Listo',0),(14,28,7,'Listo',0),(15,28,5,'Listo',0),(16,28,6,'Listo',0),(17,28,7,'Listo',0),(18,28,9,'Listo',0),(19,29,7,'Entregado',0),(20,27,7,'Entregado',0),(21,27,7,'Entregado',0),(22,27,7,'Entregado',0),(23,29,1,'Listo',0),(24,29,6,'Listo',0),(25,29,8,'Listo',0),(26,29,9,'Listo',0),(27,29,1,'Listo',0),(28,29,1,'Listo',0),(29,29,6,'Listo',0),(30,29,8,'Listo',0),(31,29,9,'Listo',0),(32,28,5,'Listo',0),(33,28,7,'Listo',0),(34,28,1,'Listo',0),(35,28,6,'Listo',0),(36,28,8,'Listo',0),(37,29,1,'Listo',0),(38,29,6,'Listo',0),(39,29,8,'Listo',0),(40,29,9,'Listo',0),(41,28,1,'Listo',0),(42,28,6,'Listo',0),(43,28,8,'Listo',0),(44,28,9,'Listo',0),(45,29,5,'Listo',0),(46,29,7,'Listo',0),(47,29,8,'Listo',0),(48,28,1,'Listo',0),(49,28,6,'Listo',0),(50,28,8,'Pendiente',0),(51,28,9,'Pendiente',0);
/*!40000 ALTER TABLE `pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `idusuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `apellidos` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `nickname` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `pass` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `area` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `estatus` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`idusuario`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'Zero','Vivanco','Zero','1234','Administrador',''),(2,'luis','perez bautista','peerez','123','Recepcion','Disponible'),(3,'Jesus','Serna','Jesus','12345','Mesero','Disponible');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-11-30 22:54:09
