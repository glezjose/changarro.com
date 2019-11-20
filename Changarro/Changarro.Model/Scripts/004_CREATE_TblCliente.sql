SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Cliente																	 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script					     *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Cliente') 
 BEGIN
 CREATE TABLE [dbo].[tblCat_Cliente]
 (	
	iIdCliente INT NOT NULL IDENTITY (1,1),
	cNombre VARCHAR (50) NOT NULL,
	cApellido VARCHAR (50) NOT NULL,
	cTelefono VARCHAR (10) NOT NULL,
	cCorreo VARCHAR (50) NOT NULL,
	cContrasenia VARCHAR (50) NOT NULL,
	cImagen VARCHAR (20) NOT NULL,
	lEstatus BIT NOT NULL,
	dtFechaAlta DATETIME DEFAULT (GETDATE()) NULL,
	dtFechaBaja DATETIME NULL,
	dtFechaModificacion DATETIME NULL
 )   
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'iIdCliente' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'cNombre' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Apellido del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'cApellido' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'n�mero de tel�fono' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'cTelefono' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'correo electr�nico' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'cCorreo' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'contrase�a' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'cContrasenia' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la imagen del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'cImagen' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'estado del cliente activo o de baja' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'lEstatus' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de alta del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'dtFechaAlta' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de baja del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'dtFechaBaja' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de modificaci�n del cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Cliente', @level2type=N'COLUMN',@level2name=N'dtFechaModificacion' 

 
 PRINT 'Tabla tblCat_Cliente creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Cliente ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tblCat_Cliente'
				)
BEGIN 
	ALTER TABLE [dbo].[tblCat_Cliente]
	ADD CONSTRAINT PK_tblCat_Cliente_iIdCliente
	PRIMARY KEY (iIdCliente)
	PRINT 'Se agreg� la llave primaria a tblCat_Cliente'
END