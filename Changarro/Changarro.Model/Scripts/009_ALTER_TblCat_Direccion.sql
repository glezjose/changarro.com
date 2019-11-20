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
*  1.0      21/10/2019   Jose Gonzalez Angulo   Creaci�n del script					     *
*																						 *
******************************************************************************************/

/*Creaci�n de la llave For�nea Cliente*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Direccion')
BEGIN


ALTER TABLE tblCat_Direccion
ADD CONSTRAINT [FK_tblCat_Direccion_iIdCliente_tblCat_Cliente_iIdCliente]
FOREIGN KEY (iIdCliente)
REFERENCES tblCat_Cliente (iIdCliente);
PRINT 'Tabla tblCat_Direccion alterada con exito, llave foranea a tblCliente agregada.'

END
ELSE
PRINT 'Tabla tblCat_Direccion no pudo ser alterada.'

/*Creaci�n de la llave For�nea Estado*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Direccion')
BEGIN

ALTER TABLE tblCat_Direccion
ADD CONSTRAINT [FK_tblCat_Direccion_iIdEstado_tblCat_Estado_iIdEstado]
FOREIGN KEY (iIdEstado)
REFERENCES tbl_Estado (iIdEstado);
PRINT 'Tabla tblCat_Direccion alterada con exito, llave foranea a tblEstado agregada.'

END
ELSE
PRINT 'Tabla tblCat_Direccion no pudo ser alterada.'

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la direcci�n' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'iIdDireccion' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n de la direcci�n y el cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'iIdCliente' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n de el estado y la direcci�n' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'iIdEstado' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'c�digo postal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'iCodigoPostal'
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre de la direcci�n' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cNombre' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'calle' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cCalle' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'n�mero exterior del inmueble' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cNumeroExterior' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'n�mero interior del inmueble' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cNumeroInterior' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre de la colonia', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cColonia' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre del municipio', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cMunicipio' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripci�n', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'cDescripcion' 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estatus del inmueble', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Direccion', @level2type=N'COLUMN',@level2name=N'lEstatus' 
  