SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tbl_DetallesCompra																 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script						 *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_DetalleCompra') 
 BEGIN
	 CREATE TABLE [dbo].[tbl_DetalleCompra]
		(	
		iIdDetalleCompra INT NOT NULL IDENTITY (1,1),  
		iIdCompra INT NOT NULL,
		iIdProducto INT NOT NULL,
		iCantidad INT NOT NULL
		)  

 PRINT 'Tabla tbl_DetalleCompra creada' 
 END
 ELSE  
	PRINT 'La Tabla tbl_DetalleCompra ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tbl_DetalleCompra'
				)
BEGIN 
	ALTER TABLE [dbo].[tbl_DetalleCompra]
	ADD CONSTRAINT PK_tbl_DetalleCompra_iIdDetalleCompra
	PRIMARY KEY (iIdDetalleCompra)

	PRINT 'Se agreg� la llave primaria a tbl_DetalleCompra'
END
