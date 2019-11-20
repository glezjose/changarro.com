SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_CarritoProducto															 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali Ake Mezeta   Creaci�n del script					     *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_DetalleCarrito') 
 BEGIN
	 CREATE TABLE [dbo].[tbl_DetalleCarrito]
		(	
		iIdDetalleCarrito INT NOT NULL IDENTITY (1,1),  
		iIdCarrito INT NOT NULL,
		iIdProducto INT NOT NULL,
		iCantidad DECIMAL (7) NOT NULL
		)  
 
 PRINT 'Tabla tbl_DetalleCarrito creada' 
 END
 ELSE  
	PRINT 'La Tabla tbl_DetalleCarrito ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tbl_DetalleCarrito'
				)
BEGIN 
	ALTER TABLE [dbo].[tbl_DetalleCarrito]
	ADD CONSTRAINT PK_tbl_DetalleCarrito_iIdDetalleCarrito
	PRIMARY KEY (iIdDetalleCarrito)
	PRINT 'Se agreg� la llave primaria a tblCat_DetalleCarrito '
END



