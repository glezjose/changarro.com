using System;

namespace Changarro.Model.DTO
{
    public class ListaProductosDTO
    {
        /// <summary>
        /// ID del producto
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// Nombre de la categoría del producto
        /// </summary>
        public string NombreCate { get; set; }

        /// <summary>
        /// Cantidad del producto
        /// </summary>
        public int iCantidad { get; set; }

        /// <summary>
        /// Precio del producto
        /// </summary>
        public decimal dPrecio { get; set; }

        /// <summary>
        /// Descripción del producto
        /// </summary>
        public string cDescripcion { get; set; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string cNombre { get; set; }

        /// <summary>
        /// Imagen del producto
        /// </summary>
        public string cImagen { get; set; }

        /// <summary>
        /// Estatus del producto
        /// </summary>
        public bool lEstatus { get; set; }

        /// <summary>
        /// Fecha de alta del producto
        /// </summary>
        public Nullable<System.DateTime> dtFechaAlta { get; set; }

        /// <summary>
        /// Fecha de baja del producto
        /// </summary>
        public Nullable<System.DateTime> dfFechaBaja { get; set; }

        /// <summary>
        /// Fecha de modificación del producto
        /// </summary>
        public Nullable<System.DateTime> dtFechaModificacion { get; set; }
    }
}
