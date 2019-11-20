SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Estado															         *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								*		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali Ake Mezeta   Creaci�n del script					        *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_Estado') 
 BEGIN
	 CREATE TABLE [dbo].[tbl_Estado]
		(	
		iIdEstado INT NOT NULL IDENTITY (1,1),  
		cNombre VARCHAR (20) NOT NULL
		)  

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Estado', @level2type=N'COLUMN',@level2name=N'iIdEstado' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre de estado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Estado', @level2type=N'COLUMN',@level2name=N'cNombre' 
  
 PRINT 'Tabla tbl_Estado creada' 
 END
 ELSE  
	PRINT 'La Tabla tbl_Estado ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tbl_Estado'
				)
BEGIN 
	ALTER TABLE [dbo].[tbl_Estado]
	ADD CONSTRAINT PK_tbl_Estado_iIdEstado
	PRIMARY KEY (iIdEstado)
	PRINT 'Se agreg� la llave primaria a tbl_Estado'
END