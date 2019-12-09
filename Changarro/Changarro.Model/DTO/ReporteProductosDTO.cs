namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase contiene las propiedades necesarias para obtener los productos más vendidos.
    /// </summary>
    public class ReporteProductosDTO
    {
        /// <summary>
        /// Identificador del producto.
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// Cantidad de compra.
        /// </summary>
        public int iCantidad{ get; set; }
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string cNombre{ get; set; }

        
    }
}
