-- MySQL dump 10.10
--
-- Host: localhost    Database: restaurant
-- ------------------------------------------------------
-- Server version	5.0.22-community-nt

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
CREATE TABLE `actualizacion` (
  `Nombre` varchar(6) default NULL,
  `Orden` tinyint(4) default NULL,
  `Pedido` tinyint(4) default NULL,
  `NumCamOrden` int(11) default NULL,
  `NumCamPedido` int(11) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `actualizacion`
--


/*!40000 ALTER TABLE `actualizacion` DISABLE KEYS */;
LOCK TABLES `actualizacion` WRITE;
INSERT INTO `actualizacion` VALUES ('Cambio',0,0,0,0);
UNLOCK TABLES;
/*!40000 ALTER TABLE `actualizacion` ENABLE KEYS */;

--
-- Table structure for table `alimento`
--

DROP TABLE IF EXISTS `alimento`;
CREATE TABLE `alimento` (
  `idalimento` int(11) NOT NULL auto_increment,
  `nombre` varchar(30) NOT NULL,
  `tipoalimento` varchar(45) default NULL,
  `descripcion` varchar(100) default NULL,
  `precio` decimal(6,2) default NULL,
  `estatus` varchar(15) NOT NULL,
  PRIMARY KEY  (`idalimento`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `alimento`
--


/*!40000 ALTER TABLE `alimento` DISABLE KEYS */;
LOCK TABLES `alimento` WRITE;
INSERT INTO `alimento` VALUES (1,'Ensalada de pepino','Entrada','Radajas de pepino marinadas en una mezcla de agua , vinagre, pimienta y sal','100.00','Disponible'),(2,'Ensalada de champiñones','Entrada','Champiñones con camarones y aderezo especial','120.00','Disponible'),(3,'Melones al moscato','Entrada','Melon rebanado por la mitad con jamon, melocotones ','90.00','Disponible'),(4,'Tarta de pollo','Entrada','Contiene pollo y acelga condimentada con sal, y pinienta','85.00','Disponible'),(5,'Pastel de calabacines','Entrada','Pastel rellenado con calabacines e ingredientes varios','95.00','Disponible'),(6,'Tortitas de lenteja','Plato fuerte','Un platillo con proteínas, ideal para cuando no quieras añadir carne a su dieta.','125.00','Disponible'),(7,'Arroz a lo pobre','Plato fuerte','Un arroz muy diferente por sus ingredientes y delicioso para acompañarlo con vino tinto Viña Real','100.00','Disponible'),(8,'Lomo de cerdo relleno de queso','Plato fuerte','Una excelente manera de preparar una cena de navidad sin utilizar horno','120.00','Disponible'),(9,'Nubes de frijoles','Plato fuerte','Un antojito muy sabroso y fácil de preparar para toda la familia','90.00','Disponible'),(10,'Chimicanga de pollo','Plato fuerte','Es uno de los ricos y fáciles platillos mexicanos','70.00','Disponible'),(11,'Arroz con leche','Postre','  ','50.00','Disponible'),(12,'Buñuelos','Postre','  ','55.00','Disponible'),(13,'Pay de limon','Postre','  ','55.00','Disponible'),(14,'Churros','Postre','  ','40.00','Disponible'),(15,'Galletas rellenas de chocolate','Postre','  ','40.00','Disponible'),(16,'Aguas frescas','Bebida','Jamaica - Horchata - Limon  1 Litro','35.00','Disponible'),(17,'Rafescos ','Bebida','Varios sabores','40.00','Disponible'),(18,'Tequila','Bebida','Reposado 50 años ','150.00','Disponible'),(19,'Tequila Blanco','Bebida','Blanco','120.00','Disponible'),(20,'Vino','Bebida','Tinto 150 años','180.00','Disponible');
UNLOCK TABLES;
/*!40000 ALTER TABLE `alimento` ENABLE KEYS */;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
CREATE TABLE `cliente` (
  `idcliente` int(11) NOT NULL auto_increment,
  `nombre` varchar(30) default NULL,
  `apellidos` varchar(50) default NULL,
  PRIMARY KEY  (`idcliente`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cliente`
--


/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
LOCK TABLES `cliente` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;

--
-- Table structure for table `mesa`
--

DROP TABLE IF EXISTS `mesa`;
CREATE TABLE `mesa` (
  `idmesa` int(11) NOT NULL auto_increment,
  `numpersonas` int(11) default NULL,
  `estatus` varchar(30) default NULL,
  `idcliente` int(11) default NULL,
  PRIMARY KEY  (`idmesa`),
  KEY `idcliente` (`idcliente`),
  CONSTRAINT `mesa_ibfk_1` FOREIGN KEY (`idcliente`) REFERENCES `cliente` (`idcliente`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mesa`
--


/*!40000 ALTER TABLE `mesa` DISABLE KEYS */;
LOCK TABLES `mesa` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `mesa` ENABLE KEYS */;

--
-- Table structure for table `mesero`
--

DROP TABLE IF EXISTS `mesero`;
CREATE TABLE `mesero` (
  `idmesero` int(11) NOT NULL auto_increment,
  `nombre` varchar(30) default NULL,
  `apellidos` varchar(50) default NULL,
  `estatus` varchar(15) NOT NULL,
  `idusuario` int(11) NOT NULL,
  PRIMARY KEY  (`idmesero`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mesero`
--


/*!40000 ALTER TABLE `mesero` DISABLE KEYS */;
LOCK TABLES `mesero` WRITE;
INSERT INTO `mesero` VALUES (1,'Luis','Perez','Disponible',2),(2,'Mario','Lopez','Disponible',4),(3,'Maria','Aguilar','Disponible',6);
UNLOCK TABLES;
/*!40000 ALTER TABLE `mesero` ENABLE KEYS */;

--
-- Table structure for table `orden`
--

DROP TABLE IF EXISTS `orden`;
CREATE TABLE `orden` (
  `idorden` int(11) NOT NULL auto_increment,
  `idmesa` int(11) default NULL,
  `idmesero` int(11) default NULL,
  `fecha` datetime default NULL,
  `total` decimal(4,2) default NULL,
  `estatus` varchar(30) default NULL,
  `actualizado` tinyint(4) NOT NULL,
  PRIMARY KEY  (`idorden`),
  KEY `idmesa` (`idmesa`),
  KEY `idmesero` (`idmesero`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `orden`
--


/*!40000 ALTER TABLE `orden` DISABLE KEYS */;
LOCK TABLES `orden` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `orden` ENABLE KEYS */;

--
-- Table structure for table `pedido`
--

DROP TABLE IF EXISTS `pedido`;
CREATE TABLE `pedido` (
  `idpedido` int(11) NOT NULL auto_increment,
  `idorden` int(11) default NULL,
  `idalimento` int(11) default NULL,
  `estatus` varchar(30) default NULL,
  `actualizado` tinyint(4) NOT NULL,
  PRIMARY KEY  (`idpedido`),
  KEY `idorden` (`idorden`),
  KEY `idalimento` (`idalimento`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pedido`
--


/*!40000 ALTER TABLE `pedido` DISABLE KEYS */;
LOCK TABLES `pedido` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `pedido` ENABLE KEYS */;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `idusuario` int(11) NOT NULL auto_increment,
  `nombre` varchar(30) collate utf8_unicode_ci NOT NULL,
  `apellidos` varchar(50) collate utf8_unicode_ci NOT NULL,
  `nickname` varchar(15) collate utf8_unicode_ci NOT NULL,
  `pass` varchar(15) collate utf8_unicode_ci NOT NULL,
  `area` varchar(15) collate utf8_unicode_ci NOT NULL,
  `estatus` varchar(15) collate utf8_unicode_ci NOT NULL,
  PRIMARY KEY  (`idusuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `usuario`
--


/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
LOCK TABLES `usuario` WRITE;
INSERT INTO `usuario` VALUES (1,'Jesus','Serna','Jesus','1234','Administrador','Disponible'),(2,'Luis','Perez','luis','1234','Meseros','Disponible'),(3,'Edgar','Vivanco','edgar','1234','Recepcion','Disponible'),(4,'Mario','Lopez','mario','1234','Meseros','Disponible'),(5,'Juan','Perez','home','1234','Recepcion','Disponible'),(6,'Maria','Aguilar','maria','1234','Meseros','Disponible');
UNLOCK TABLES;
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

