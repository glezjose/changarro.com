SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Compra																	 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali Ake Mezeta   Creaci�n del script					     *
*  1.1      21/10/2019   Jose Gonzalez A.    Cambie los nombres de variables             *
******************************************************************************************/
 
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Compra') 
 BEGIN
	 CREATE TABLE [dbo].[tblCat_Compra]
	 (	
		iIdCompra INT NOT NULL IDENTITY (1,1),  
		iIdTarjeta INT NOT NULL,
		iIdCliente INT NOT NULL,
		iIdDireccion INT NOT NULL,
		lEstatus BIT DEFAULT 1 NOT NULL,
		dtFechaCompra DATETIME DEFAULT (GETDATE()) NULL
	 )
 
 PRINT 'Tabla tblCat_Compra creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Compra ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tblCat_Compra'
				)
BEGIN 
	ALTER TABLE [dbo].[tblCat_Compra]
	ADD CONSTRAINT PK_tblCat_Compra_iIdCompra
	PRIMARY KEY (iIdCompra)
	PRINT 'Se agreg� la llave primaria a tblCat_Compra'
END


