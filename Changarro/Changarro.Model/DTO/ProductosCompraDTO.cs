namespace Changarro.Model.DTO
{
    public class ProductosCompraDTO
    {
        /// <summary>
        /// La ID del producto.
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// La cantidad seleccionada a comprar.
        /// </summary>
        public int iCantidadSeleccion { get; set; }

        /// <summary>
        /// El precio total considerando la cantidad del producto.
        /// </summary>
        public decimal dPrecioTotal { get; set; }

        /// <summary>
        /// La cadena del nombre del producto.
        /// </summary>
        public string cNombre { get; set; }

        /// <summary>
        /// La cadena de la imagen del producto.
        /// </summary>
        public string cImagen { get; set; }
    }
}
