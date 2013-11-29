-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 29-11-2013 a las 03:03:29
-- Versión del servidor: 5.5.24-log
-- Versión de PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `restaurant`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `hola`(IN campo VARCHAR(15))
BEGIN
SELECT * FROM mesa WHERE estatus = campo;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alimento`
--

CREATE TABLE IF NOT EXISTS `alimento` (
  `idalimento` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) NOT NULL,
  `tipoalimento` varchar(45) DEFAULT NULL,
  `descripcion` varchar(100) DEFAULT NULL,
  `precio` decimal(4,2) DEFAULT NULL,
  `estatus` varchar(15) NOT NULL,
  PRIMARY KEY (`idalimento`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Volcado de datos para la tabla `alimento`
--

INSERT INTO `alimento` (`idalimento`, `nombre`, `tipoalimento`, `descripcion`, `precio`, `estatus`) VALUES
(1, 'Cereal', 'Entrada', 'tutache', '10.00', 'Disponible'),
(2, 'cuchara', 'Postre', 'jomi', '1.00', 'Disponible'),
(3, 'Galletas', 'Postre', 'asasa', '12.00', 'Disponible'),
(4, 'Gelatina', 'Postre', 'aasasa', '13.00', 'Disponible'),
(5, 'Carne', 'Plato fuerte', 'sasaasa', '30.00', 'Disponible'),
(6, 'Ensalada', 'Entrada', 'asassasas', '10.00', 'Disponible'),
(7, 'Camarones', 'Plato fuerte', 'bajo del mar', '10.00', 'Disponible'),
(8, 'Atun', 'Entrada', 'efefefefe', '15.00', 'Disponible'),
(9, 'sopanissin', 'Entrada', 'yumi', '5.00', 'Disponible');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente`
--

CREATE TABLE IF NOT EXISTS `cliente` (
  `idcliente` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idcliente`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mesa`
--

CREATE TABLE IF NOT EXISTS `mesa` (
  `idmesa` int(11) NOT NULL AUTO_INCREMENT,
  `numpersonas` int(11) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  `idcliente` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmesa`),
  KEY `idcliente` (`idcliente`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `mesa`
--

INSERT INTO `mesa` (`idmesa`, `numpersonas`, `estatus`, `idcliente`) VALUES
(1, 6, 'No disponible', NULL),
(2, 4, 'Reservada', NULL),
(3, 4, 'Disponible', NULL),
(4, 3, 'Disponible', NULL),
(5, 5, 'Disponible', NULL),
(6, 3, 'Disponible', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mesero`
--

CREATE TABLE IF NOT EXISTS `mesero` (
  `idmesero` int(11) NOT NULL DEFAULT '0',
  `nombre` varchar(30) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idmesero`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `orden`
--

CREATE TABLE IF NOT EXISTS `orden` (
  `idorden` int(11) NOT NULL AUTO_INCREMENT,
  `idmesa` int(11) DEFAULT NULL,
  `idmesero` int(11) DEFAULT NULL,
  `fecha` datetime DEFAULT NULL,
  `total` decimal(4,2) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`idorden`),
  KEY `idmesa` (`idmesa`),
  KEY `idmesero` (`idmesero`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pedido`
--

CREATE TABLE IF NOT EXISTS `pedido` (
  `idpedido` int(11) NOT NULL AUTO_INCREMENT,
  `idorden` int(11) DEFAULT NULL,
  `idalimento` int(11) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`idpedido`),
  KEY `idorden` (`idorden`),
  KEY `idalimento` (`idalimento`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE IF NOT EXISTS `usuario` (
  `idusuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) COLLATE utf8_unicode_ci NOT NULL,
  `apellidos` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `nickname` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `pass` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `area` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  `estatus` varchar(15) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`idusuario`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idusuario`, `nombre`, `apellidos`, `nickname`, `pass`, `area`, `estatus`) VALUES
(1, 'Zero', 'Vivanco', 'Zero', '1234', 'Administrador', ''),
(2, 'luis', 'perez bautista', 'peerez', '123', 'Recepcion', 'Disponible'),
(3, 'jesus', 'serna', 'jserna', 'jserna', 'Meseros', 'Disponible'),
(4, 'antonio', 'gorocica', 'gorocica', 'gorocica', 'Recepcion', 'No disponible'),
(5, 'matias', 'martinez', 'mati', 'mati', 'Recepcion', 'Disponible'),
(6, 'Leticia', 'Bautista', 'lety', 'lety', 'Recepcion', 'Disponible');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `mesa`
--
ALTER TABLE `mesa`
  ADD CONSTRAINT `mesa_ibfk_1` FOREIGN KEY (`idcliente`) REFERENCES `cliente` (`idcliente`);

--
-- Filtros para la tabla `orden`
--
ALTER TABLE `orden`
  ADD CONSTRAINT `orden_ibfk_1` FOREIGN KEY (`idmesa`) REFERENCES `mesa` (`idmesa`),
  ADD CONSTRAINT `orden_ibfk_2` FOREIGN KEY (`idmesero`) REFERENCES `mesero` (`idmesero`);

--
-- Filtros para la tabla `pedido`
--
ALTER TABLE `pedido`
  ADD CONSTRAINT `pedido_ibfk_1` FOREIGN KEY (`idorden`) REFERENCES `orden` (`idorden`),
  ADD CONSTRAINT `pedido_ibfk_2` FOREIGN KEY (`idalimento`) REFERENCES `alimento` (`idalimento`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
