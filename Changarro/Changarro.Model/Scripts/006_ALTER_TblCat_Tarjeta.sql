SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Tarjeta																     *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Jose Gonzalez Angulo   Creaci�n del script					     *
*																						 *
******************************************************************************************/

/*Creaci�n de la llave For�nea Cliente*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Tarjeta')
BEGIN

ALTER TABLE tblCat_Tarjeta
ADD CONSTRAINT [FK_tblCat_Tarjeta_iIdCliente_tblCat_Cliente_iIdCliente]
FOREIGN KEY (iIdCliente)
REFERENCES tblCat_Cliente (iIdCliente);
PRINT 'Tabla tblCat_Tarjeta alterada con exito.'

END
ELSE
PRINT 'Tabla tblCat_Tarjeta no pudo ser alterada.'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tarjeta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'iIdTarjeta' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n de la tarjeta y el cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'iIdCliente' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre de tarjeta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'cNombre' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'nombre del titular' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'cTitular' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N�mero de tarjeta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'cNumeroTarjeta' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estatus de tarjeta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'lEstatus' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'mes de expiraci�n de la tarjeta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'dtMesExpiracion' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A�o que expira la tarjeta' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Tarjeta', @level2type=N'COLUMN',@level2name=N'dtAnioExpiracion' 
 