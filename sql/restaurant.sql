-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 01-12-2013 a las 16:53:32
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

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `actualizacion`
--

CREATE TABLE IF NOT EXISTS `actualizacion` (
  `Nombre` varchar(6) DEFAULT NULL,
  `Orden` tinyint(4) DEFAULT NULL,
  `Pedido` tinyint(4) DEFAULT NULL,
  `NumCamOrden` int(11) DEFAULT NULL,
  `NumCamPedido` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `actualizacion`
--

INSERT INTO `actualizacion` (`Nombre`, `Orden`, `Pedido`, `NumCamOrden`, `NumCamPedido`) VALUES
('Cambio', 0, 0, 0, 0);

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Volcado de datos para la tabla `mesa`
--

INSERT INTO `mesa` (`idmesa`, `numpersonas`, `estatus`, `idcliente`) VALUES
(1, 6, 'Disponible', NULL),
(2, 4, 'Reservada', NULL),
(3, 4, 'Disponible', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mesero`
--

CREATE TABLE IF NOT EXISTS `mesero` (
  `idmesero` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) DEFAULT NULL,
  `apellidos` varchar(50) DEFAULT NULL,
  `estatus` varchar(15) NOT NULL,
  `idusuario` int(11) NOT NULL,
  PRIMARY KEY (`idmesero`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Volcado de datos para la tabla `mesero`
--

INSERT INTO `mesero` (`idmesero`, `nombre`, `apellidos`, `estatus`, `idusuario`) VALUES
(1, 'Jesus', 'Serna', '', 1),
(2, 'naruto', 'uzumaki', 'Disponible', 0),
(3, 'sasuke', 'uchiha', 'Disponible', 0),
(4, 'sakura', 'chan', 'Disponible', 0),
(5, 'kakashi', 'kun', 'Disponible', 24);

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
  `actualizado` tinyint(4) NOT NULL,
  PRIMARY KEY (`idorden`),
  KEY `idmesa` (`idmesa`),
  KEY `idmesero` (`idmesero`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=30 ;

--
-- Volcado de datos para la tabla `orden`
--

INSERT INTO `orden` (`idorden`, `idmesa`, `idmesero`, `fecha`, `total`, `estatus`, `actualizado`) VALUES
(23, 1, 0, '2013-11-28 17:04:37', '0.00', 'PAGADA', 0),
(24, 1, 0, '2013-11-28 17:33:03', '0.00', 'PAGADA', 0),
(25, 1, 0, '2013-11-29 02:47:53', '0.00', 'PAGADA', 0),
(26, 1, 0, '2013-11-29 10:30:48', '0.00', 'PAGADA', 0),
(27, 1, 0, '2013-11-30 17:45:45', '0.00', 'ABIERTA', 1),
(28, 3, 0, '2013-11-30 17:50:14', '0.00', 'ABIERTA', 1),
(29, 1, 0, '2013-11-30 19:00:05', '0.00', 'ABIERTA', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pedido`
--

CREATE TABLE IF NOT EXISTS `pedido` (
  `idpedido` int(11) NOT NULL AUTO_INCREMENT,
  `idorden` int(11) DEFAULT NULL,
  `idalimento` int(11) DEFAULT NULL,
  `estatus` varchar(30) DEFAULT NULL,
  `actualizado` tinyint(4) NOT NULL,
  PRIMARY KEY (`idpedido`),
  KEY `idorden` (`idorden`),
  KEY `idalimento` (`idalimento`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=52 ;

--
-- Volcado de datos para la tabla `pedido`
--

INSERT INTO `pedido` (`idpedido`, `idorden`, `idalimento`, `estatus`, `actualizado`) VALUES
(1, 24, 1, 'Listo', 0),
(2, 24, 6, 'Entregado', 0),
(3, 24, 4, 'Listo', 0),
(4, 24, 3, 'Cancelado', 0),
(5, 26, 5, 'Cancelado', 0),
(6, 25, 7, 'Listo', 0),
(7, 26, 4, 'Listo', 0),
(8, 26, 5, 'Listo', 0),
(9, 26, 2, 'Listo', 0),
(10, 26, 8, 'Listo', 0),
(11, 26, 9, 'Listo', 0),
(12, 28, 7, 'Listo', 0),
(13, 29, 6, 'Listo', 0),
(14, 28, 7, 'Listo', 0),
(15, 28, 5, 'Listo', 0),
(16, 28, 6, 'Listo', 0),
(17, 28, 7, 'Listo', 0),
(18, 28, 9, 'Listo', 0),
(19, 29, 7, 'Entregado', 0),
(20, 27, 7, 'Entregado', 0),
(21, 27, 7, 'Entregado', 0),
(22, 27, 7, 'Entregado', 0),
(23, 29, 1, 'Listo', 0),
(24, 29, 6, 'Listo', 0),
(25, 29, 8, 'Listo', 0),
(26, 29, 9, 'Listo', 0),
(27, 29, 1, 'Listo', 0),
(28, 29, 1, 'Listo', 0),
(29, 29, 6, 'Listo', 0),
(30, 29, 8, 'Listo', 0),
(31, 29, 9, 'Listo', 0),
(32, 28, 5, 'Listo', 0),
(33, 28, 7, 'Listo', 0),
(34, 28, 1, 'Listo', 0),
(35, 28, 6, 'Listo', 0),
(36, 28, 8, 'Listo', 0),
(37, 29, 1, 'Listo', 0),
(38, 29, 6, 'Listo', 0),
(39, 29, 8, 'Listo', 0),
(40, 29, 9, 'Listo', 0),
(41, 28, 1, 'Listo', 0),
(42, 28, 6, 'Listo', 0),
(43, 28, 8, 'Listo', 0),
(44, 28, 9, 'Listo', 0),
(45, 29, 5, 'Listo', 0),
(46, 29, 7, 'Listo', 0),
(47, 29, 8, 'Listo', 0),
(48, 28, 1, 'Listo', 0),
(49, 28, 6, 'Listo', 0),
(50, 28, 8, 'Pendiente', 0),
(51, 28, 9, 'Pendiente', 0);

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=25 ;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idusuario`, `nombre`, `apellidos`, `nickname`, `pass`, `area`, `estatus`) VALUES
(1, 'Zero', 'Vivanco', 'Zero', '1234', 'Administrador', ''),
(2, 'luis', 'perez bautista', 'peerez', '123', 'Recepcion', 'Disponible'),
(3, 'Jesus', 'Serna', 'Jesus', '12345', 'Mesero', 'Disponible'),
(4, 'juan', 'gorocica', 'goro', '1234', 'Meseros', 'Disponible'),
(5, 'lolo', 'perez', 'lolo', '1234', 'Meseros', 'Disponible'),
(6, 'matias', 'martinez', 'mati', '1234', 'Meseros', 'Disponible'),
(7, 'pepito', 'perez', 'pepito', '1234', 'Meseros', 'Disponible'),
(8, 'perengano', 'perez', 'pengano', '1234', 'Meseros', 'Disponible'),
(9, 'generico', 'perez', 'generico', '1234', 'Meseros', 'Disponible'),
(10, 'lala', 'perez', 'lala', '1234', 'Meseros', 'Disponible'),
(11, 'pepe', 'perez', 'pepe', '1234', 'Meseros', 'Disponible'),
(12, 'maria', 'sanchez', 'maria', '1234', 'Meseros', 'Disponible'),
(13, 'ultimo', 'intento', 'ultimo', '1234', 'Meseros', 'Disponible'),
(14, 'goku', 'son', 'goku', '1234', 'Meseros', 'Disponible'),
(15, 'gohan', 'son', 'gohan', '1234', 'Meseros', 'Disponible'),
(16, 'goten', 'son', 'goten', '1234', 'Meseros', 'Disponible'),
(17, 'trunks', 'vegita', 'trunks', '1234', 'Meseros', 'Disponible'),
(18, 'vegeta', 'vegita', 'vegeta', '1234', 'Meseros', 'Disponible'),
(19, 'jose', 'jose', 'jose', '1234', 'Meseros', 'Disponible'),
(20, 'bulma', 'vegita', 'bulma', '1234', 'Meseros', 'Disponible'),
(21, 'naruto', 'uzumaki', 'naruto', '1234', 'Meseros', 'Disponible'),
(22, 'sasuke', 'uchiha', 'sasuke', '1234', 'Meseros', 'Disponible'),
(23, 'sakura', 'chan', 'sakura', '1234', 'Meseros', 'Disponible'),
(24, 'kakashi', 'kun', 'kakashi', '1234', 'Meseros', 'Disponible');

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
