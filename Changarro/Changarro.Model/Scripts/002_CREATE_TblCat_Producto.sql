SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Producto																 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script					     *
*  1.1      21/10/2019   Jose Gonzalez A.    Cambie los nombres de variables             *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Producto') 
 BEGIN
 CREATE TABLE [dbo].[tblCat_Producto]
 (	
	iIdProducto INT NOT NULL IDENTITY (1,1),  
	iIdCategoria INT NOT NULL,
	iCantidad INT NOT NULL,
	dPrecio DECIMAL (7,2) NOT NULL,
	cDescripcion VARCHAR(MAX) NOT NULL,
	cNombre VARCHAR(50) NOT NULL,
	cImagen VARCHAR (20) NOT NULL,
	lEstatus BIT DEFAULT 1 NOT NULL,
	dtFechaAlta DATETIME DEFAULT (GETDATE()) NULL,
	dfFechaBaja DATETIME NULL,
	dtFechaModificacion DATETIME NULL
 )  
 
 PRINT 'Tabla tblCat_Producto creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Producto ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (SELECT 1 FROM sys.objects WHERE name = 'tblCat_Producto')
BEGIN 
	ALTER TABLE [dbo].[tblCat_Producto]
	ADD CONSTRAINT PK_tblCat_Producto_iIdProducto 
	PRIMARY KEY (iIdProducto)
	PRINT 'Se agreg� la llave primaria a tblCat_Producto'
END
