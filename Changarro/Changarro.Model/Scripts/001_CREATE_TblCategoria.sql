SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Categoria															 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali  Ake Mezeta   Creaci�n del script					     *
******************************************************************************************/
 IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Categoria') 
 BEGIN
	 CREATE TABLE [dbo].[tblCat_Categoria]
		(	
		iIdCategoria INT NOT NULL IDENTITY (1,1),  
		cNombre VARCHAR (20) NOT NULL
		)  

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de categor�a' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Categoria', @level2type=N'COLUMN',@level2name=N'iIdCategoria' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre de categor�a' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Categoria', @level2type=N'COLUMN',@level2name=N'cNombre' 
  
 PRINT 'Tabla tblCat_Categoria creada' 
 END
 ELSE  
	PRINT 'La Tabla tblCat_Categoria ya existe' 
 GO 


 /*Creaci�n de la llave primaria*/
 IF EXISTS (
				SELECT 1
				FROM sys.objects
				WHERE name = 'tblCat_Categoria'
				)
BEGIN 
	ALTER TABLE [dbo].[tblCat_Categoria]
	ADD CONSTRAINT PK_tblCat_Categoria_iIdCategoria
	PRIMARY KEY (iIdCategoria)
	PRINT 'Se agreg� la llave primaria a tblCat_Categoria'
END