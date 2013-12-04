



#El que usamos en el form
SELECT pedido.idorden,tablota.nombre,tablota.precio,tablota.idalimento  FROM pedido  LEFT JOIN(
	SELECT *FROM alimento AS alimentodia WHERE idalimento IN(
		SELECT idalimento FROM pedido AS pedidos WHERE idorden IN(
			SELECT idorden FROM orden AS ordenes WHERE fecha LIKE '2013-12-03%'))) AS tablota
	USING(idalimento);
	


#Seleccionando la tablota
SELECT *FROM((
		SELECT pedido.idorden,tablota.nombre,tablota.precio,tablota.idalimento  FROM pedido  LEFT JOIN(
			SELECT *FROM alimento AS alimentodia WHERE idalimento IN(
				SELECT idalimento FROM pedido AS pedidos WHERE idorden IN(
					SELECT idorden FROM orden AS ordenes WHERE fecha LIKE '2013-12-02%'))) AS tablota
			USING(idalimento)
		)AS supertabla
	)
;


#Seleccionando la tablota
SELECT  *FROM pedido LEFT JOIN((
		SELECT pedido.idorden,tablota.nombre,tablota.precio,tablota.idalimento  FROM pedido  LEFT JOIN(
			SELECT *FROM alimento AS alimentodia WHERE idalimento IN(
				SELECT idalimento FROM pedido AS pedidos WHERE idorden IN(
					SELECT idorden FROM orden AS ordenes WHERE fecha LIKE '2013-12-02%'))) AS tablota
			USING(idalimento)
		)AS supertabla
	)USING(idalimento)
;


SELECT COUNT(*) FROM pedido WHERE idalimento =
				(SELECT idalimento FROM pedido AS pedidos WHERE idorden IN(
					SELECT idorden FROM orden AS ordenes WHERE fecha LIKE '2013-12-02%') )
;




#El definitivo
 SELECT tablota.nombre,tablota.precio, count(*)  FROM pedido  LEFT JOIN 
(SELECT *FROM alimento AS alimentodia WHERE idalimento IN 
(SELECT idalimento FROM pedido AS pedidos WHERE idorden IN 
(SELECT idorden FROM orden AS ordenes WHERE fecha LIKE '2013-12-02%')))  
AS tablota USING(idalimento) GROUP BY nombre;


#El de cobro
SELECT pedido.idorden,tablota.nombre,tablota.precio,tablota.idalimento  FROM pedido  LEFT JOIN(
	SELECT *FROM alimento AS alimentodia WHERE idalimento IN(
		SELECT idalimento FROM pedido AS pedidos WHERE idorden IN(
			SELECT idorden FROM orden AS ordenes WHERE idorden=1))) AS tablota
	USING(idalimento);
	
	
#Para resetear mesas
SELECT *FROM mesa WHERE idmesa IN(
	SELECT idmesa FROM orden WHERE idorden=1
);

UPDATE mesa SET estatus='Disponible',idcliente=null  WHERE idmesa IN(
	SELECT idmesa FROM orden WHERE idorden=1
);

UPDATE orden SET estatus='PAGADA' WHERE idorden = 1;