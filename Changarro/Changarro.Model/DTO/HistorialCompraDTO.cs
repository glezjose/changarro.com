using System;

namespace Changarro.Model.DTO
{
    public class HistorialCompraDTO
    {
        /// <summary>
        /// ID del producto
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// Cantidad del producto
        /// </summary>
        public int iCantidad { get; set; }

        /// <summary>
        /// Precio del producto
        /// </summary>
        public decimal dPrecio { get; set; }

        /// <summary>
        ///Nombre del producto
        /// </summary>
        public string cNombre { get; set; }

        /// <summary>
        /// Imagen del producto
        /// </summary>
        public string cImagen { get; set; }

        /// <summary>
        /// Fecha de la compra del producto
        /// </summary>
        public DateTime? dtFechaCompra { get; set; }
    }
}
