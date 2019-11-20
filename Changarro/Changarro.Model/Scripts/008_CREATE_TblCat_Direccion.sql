SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Direccion																 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script					     *
*																						 *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Direccion') 
 BEGIN
	 CREATE TABLE [dbo].tblCat_Direccion
	 (	
		iIdDireccion INT NOT NULL IDENTITY (1,1),  
		iIdCliente INT NOT NULL,
		iIdEstado INT NOT NULL,
		iCodigoPostal INT NOT NULL,
		cNombre VARCHAR (50) NOT NULL,
		cCalle VARCHAR(50) NOT NULL,
		cNumeroExterior VARCHAR (10) NULL,
		cNumeroInterior VARCHAR (10) NULL,
		cColonia VARCHAR (50),
		cMunicipio VARCHAR (30),
		cDescripcion VARCHAR (MAX),
		lEstatus BIT DEFAULT 1 NOT NULL
	 )  
  
 
 PRINT 'Tabla tblCat_Direccion creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Direccion ya existe' 
 GO 

 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tblCat_Direccion'
				)
BEGIN 
	ALTER TABLE [dbo].tblCat_Direccion
	ADD CONSTRAINT PK_tblCat_Direccion_iIdDireccion
	PRIMARY KEY (iIdDireccion)
	PRINT 'Se agreg� la llave primaria a tblCat_Direccion'
END

