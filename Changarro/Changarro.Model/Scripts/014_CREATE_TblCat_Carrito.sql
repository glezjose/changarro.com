SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Carrito															 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script					     *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Carrito') 
 BEGIN
	 CREATE TABLE [dbo].[tblCat_Carrito]
		(	
		iIdCarrito INT NOT NULL IDENTITY (1,1),  
		iIdCliente INT NOT NULL,
		)  

 PRINT 'Tabla tblCat_Carrito creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Carrito ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tblCat_Carrito'
				)
BEGIN 
	ALTER TABLE [dbo].[tblCat_Carrito]
	ADD CONSTRAINT PK_tblCat_Carrito_iIdCarrito
	PRIMARY KEY (iIdCarrito)
	PRINT 'Se agreg� la llave primaria a tblCat_Carrito'
END


