SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Compra																	 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Iyali Ake Mezeta   Creaci�n del script						     *
*  1.1      21/10/2019   Jose Gonzalez A.    Cambie los nombres de variables             *
******************************************************************************************/

/*Creaci�n de la llave For�nea cliente*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Compra')
BEGIN

ALTER TABLE tblCat_Compra 
ADD CONSTRAINT [FK_tblCat_Compra_iIdCliente_tblCat_Cliente_iIdCliente]
FOREIGN KEY (iIdCliente)
REFERENCES tblCat_Cliente (iIdCliente);
PRINT 'Tabla tblCat_Compra se ha alterado con exito, llave foranea a tblCliente agregada.'

END
ELSE 
PRINT 'Tabla tblCat_Compra no pudo ser alterada.'

/*Creaci�n de la llave For�nea Tarjeta*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Compra')
BEGIN

ALTER TABLE tblCat_Compra 
ADD CONSTRAINT [FK_tblCat_Compra_iIdTarjeta_tblCat_Tarjeta_iIdTarjeta]
FOREIGN KEY (iIdTarjeta)
REFERENCES tblCat_Tarjeta (iIdTarjeta);
PRINT 'Tabla tblCat_Compra se ha alterado con exito, llave foranea a tblCat_Tarjeta agregada.'

END
ELSE 
PRINT 'Tabla tblCat_Compra no pudo ser alterada.'

/*Creaci�n de la llave For�nea Direcci�n */
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Compra')
BEGIN

ALTER TABLE tblCat_Compra 
ADD CONSTRAINT [FK_tblCat_Compra_iIdDireccion_tblCat_Direccion_iIdDireccion]
FOREIGN KEY (iIdDireccion)
REFERENCES tblCat_Direccion (iIdDireccion);
PRINT 'Tabla tblCat_Compra se ha alterado con exito, llave foranea a tblCat_Direccion agregada.'

END
ELSE 
PRINT 'Tabla tblCat_Compra no pudo ser alterada.'

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Compra', @level2type=N'COLUMN',@level2name=N'iIdCompra' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n entre tarjeta y compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Compra', @level2type=N'COLUMN',@level2name=N'iIdTarjeta' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relaci�n entre cliente y compra ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Compra', @level2type=N'COLUMN',@level2name=N'iIdCliente' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n de la direccion ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Compra', @level2type=N'COLUMN',@level2name=N'iIdDireccion' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'estado de compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Compra', @level2type=N'COLUMN',@level2name=N'lEstatus' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'fecha de la compra' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Compra', @level2type=N'COLUMN',@level2name=N'dtFechaCompra' 
 