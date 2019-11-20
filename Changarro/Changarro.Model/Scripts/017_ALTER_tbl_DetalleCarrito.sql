SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_CarritoProducto															 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Jose Gonzalez Angulo   Creaci�n del script					     *
******************************************************************************************/

/*Creaci�n de la llave For�nea carrito*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_DetalleCarrito')
BEGIN

ALTER TABLE tbl_DetalleCarrito 
ADD CONSTRAINT [FK_tbl_DetalleCarrito_iIdCarrito_tblCat_Carrito_iIdCarrito]
FOREIGN KEY (iIdCarrito)
REFERENCES tblCat_Carrito (iIdCarrito) ON UPDATE CASCADE ON DELETE CASCADE;

END

/*Creaci�n de la llave For�nea Producto*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tbl_DetalleCarrito')
BEGIN

ALTER TABLE tbl_DetalleCarrito 
ADD CONSTRAINT [FK_tbl_DetalleCarrito_iIdProducto_tblCat_Producto_iIdProducto]
FOREIGN KEY (iIdProducto)
REFERENCES tblCat_Producto (iIdProducto) ON UPDATE CASCADE ON DELETE CASCADE;

END

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de CarritoProducto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCarrito', @level2type=N'COLUMN',@level2name=N'iIdDetalleCarrito' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n  respecto a carrito' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCarrito', @level2type=N'COLUMN',@level2name=N'iIdCarrito' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n respecto a producto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCarrito', @level2type=N'COLUMN',@level2name=N'iIdProducto' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'cantidad de productos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DetalleCarrito', @level2type=N'COLUMN',@level2name=N'iCantidad' 
 