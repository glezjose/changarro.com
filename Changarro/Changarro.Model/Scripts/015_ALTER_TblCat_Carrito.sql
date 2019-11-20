SET NOCOUNT ON 
/***************************************************************************************** 
* BLUE OCEAN TECHNOLOGIES S.A. de C.V.													 *
* Sistema: Changarro																	 *
* Tabla: tblCat_Carrito																	 *
*																					  	 *
* -------------------------------------------------------------------------------------- * 
*																						 * 
* Versi�n   Fecha        Usuario             Descripci�n								 *		
* -------   ----------   ------------------ -------------------------------------------- * 
*  1.0      21/10/2019   Jose Gonzalez Angulo   Creaci�n del script					     *
******************************************************************************************/

/*Creaci�n de la llave For�nea Cliente*/
IF EXISTS(SELECT 1 FROM sys.objects WHERE name = 'tblCat_Carrito')
BEGIN

ALTER TABLE tblCat_Carrito 
ADD CONSTRAINT [FK_tblCat_Carrito_iIdCliente_tblCat_Cliente_iIdCliente]
FOREIGN KEY (iIdCliente)
REFERENCES tblCat_Cliente (iIdCliente);
PRINT 'Tabla tblCat_Carrito alterada con exito, llave foranea a tblCat_Cliente agregada.'

END
PRINT 'Tabla tblCat_Carrito no se pudo alterar.'

 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de carrito' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Carrito', @level2type=N'COLUMN',@level2name=N'iIdCarrito' 
 EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'relaci�n cliente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblCat_Carrito', @level2type=N'COLUMN',@level2name=N'iIdCliente' 
   