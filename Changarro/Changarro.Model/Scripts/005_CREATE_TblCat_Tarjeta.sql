SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Tarjeta																     *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script					     *
*																						 *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Tarjeta') 
 BEGIN
	 CREATE TABLE [dbo].[tblCat_Tarjeta]
	 (	
		iIdTarjeta INT NOT NULL IDENTITY (1,1),  
		iIdCliente INT NOT NULL,
		cNombre VARCHAR(50) NOT NULL,
		cTitular VARCHAR(50) NOT NULL,
		cNumeroTarjeta VARCHAR(50) NOT NULL,
		lEstatus BIT DEFAULT 1 NOT NULL,
		dtMesExpiracion DATETIME NOT NULL,
		dtAnioExpiracion DATETIME NOT NULL
	 )  
  
 PRINT 'Tabla tblCat_Tarjeta creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Tarjeta ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tblCat_Tarjeta'
				)
BEGIN 
	ALTER TABLE [dbo].tblCat_Tarjeta
	ADD CONSTRAINT PK_tblCat_Tarjeta_iIdTarjeta
	PRIMARY KEY (iIdTarjeta)
	PRINT 'Se agreg� la llave primaria a tblCat_Tarjeta'
END


