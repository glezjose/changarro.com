SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Administrador														     *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								*		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      01/11/2019   Iyali Ake Mezeta   Creaci�n del script					        *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Administrador') 
 BEGIN
 CREATE TABLE [dbo].[tblCat_Administrador]
 (	
    iIdAdministrador INT NOT NULL IDENTITY (1,1),  
	cNombre VARCHAR(50) NOT NULL,
    cApellido VARCHAR(50) NOT NULL,
    cTelefono VARCHAR(50) NOT NULL,
    cCorreo VARCHAR(50) NOT NULL,
    cContrasenia VARCHAR(50) NOT NULL,
    cImagen VARCHAR(50) NULL
 )  
 

 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del administrador' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'iIdAdministrador' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre del administrador' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'cNombre' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'apellido del administrador' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'cApellido' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'telefono administrador' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'cTelefono' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'correo electrónico' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'cCorreo' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'contraseña' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'cContrasenia' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'foto de perfil' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Administrador', @level2type=N'COLUMN',@level2name=N'cImagen' 

PRINT 'Tabla tblCat_Administrador creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Administrador ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (SELECT 1 FROM sys.objects WHERE name = 'tblCat_Administrador')
BEGIN 
	ALTER TABLE [dbo].[tblCat_Administrador]
	ADD CONSTRAINT PK_tblCat_Administrador_iIdAdministrador 
	PRIMARY KEY (iIdAdministrador)
	PRINT 'Se agreg� la llave primaria a tblCat_Administrador'
END