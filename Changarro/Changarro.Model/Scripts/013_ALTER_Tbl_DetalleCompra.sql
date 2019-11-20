SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tbl_DetallesCompra																 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Jose Gonzalez Angulo   Creaci�n del script						 *
******************************************************************************************/

/*Creaci�n de la llave For�nea compra*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_DetalleCompra')
BEGIN

ALTER TABLE tbl_DetalleCompra 
ADD CONSTRAINT [FK_tbl_DetalleCompra_iIdCompra_tblCat_Compra_iIdCompra]
FOREIGN KEY (iIdCompra)
REFERENCES tblCat_Compra (iIdCompra);
PRINT 'Tabla tbl_DetalleCompra alterada con exito, llave foranea a tblCat_Compra agregada.'

END
ELSE
PRINT 'Tabla tbl_DetalleCompra no pudo ser alterada.'

/*Creaci�n de la llave For�nea producto*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_DetalleCompra')
BEGIN

ALTER TABLE tbl_DetalleCompra 
ADD CONSTRAINT [FK_tblCat_DetalleCompra_iIdProducto_tblCat_Producto_iIdProducto]
FOREIGN KEY (iIdProducto)
REFERENCES tblCat_Producto (iIdProducto);
PRINT 'Tabla tbl_DetalleCompra alterada con exito, llave foranea tblCat_Producto agregada.'

END
ELSE
PRINT 'Tabla tbl_DetalleCompra no pudo ser alterada.'

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de Detalle de compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCompra', @level2type=N'COLUMN',@level2name=N'iIdDetalleCompra' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCompra', @level2type=N'COLUMN',@level2name=N'iIdCompra' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCompra', @level2type=N'COLUMN',@level2name=N'iIdProducto' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad de productos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCompra', @level2type=N'COLUMN',@level2name=N'iCantidad' 
  