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
*  1.0      21/10/2019   Jose Gonzalez Angulo   Creaci�n del script					     *
*																			             *
******************************************************************************************/

 /*Creaci�n de la llave for�nea*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Producto')
BEGIN

ALTER TABLE tblCat_Producto 
ADD CONSTRAINT [FK_tblCat_Producto_iIdCategoria_tblCat_Categoria_iIdCategoria]
FOREIGN KEY (iIdCategoria)
REFERENCES tblCat_Categoria (iIdCategoria);
PRINT 'Tabla alterada con exito.'
END

ELSE
PRINT 'Tabla no pudo ser alterada' 

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'iIdProducto' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relaci�n con la categor�a' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'iIdCategoria' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad de producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'iCantidad' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Precio del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'dPrecio' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripci�n del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'cDescripcion' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'cNombre' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la imagen del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'cImagen' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estatus del producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'lEstatus' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de alta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'dtFechaAlta' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de baja' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'dfFechaBaja' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de modificaci�n' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Producto', @level2type=N'COLUMN',@level2name=N'dtFechaModificacion' 