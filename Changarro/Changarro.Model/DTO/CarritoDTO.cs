using System;

namespace Changarro.Model.DTO
{
    public class CarritoDTO {

        /// <summary>
        /// ID del producto
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// ID de la categoría
        /// </summary>
        public int iIdCategoria { get; set; }

        /// <summary>
        /// cantidad del producto
        /// </summary>
        public int iCantidad { get; set; }

        /// <summary>
        /// Cantidad actual existente del producto
        /// </summary>
        public int iCantidadExistente { get; set; }

        /// <summary>
        /// Precio del producto
        /// </summary>
        public decimal dPrecio { get; set; }

        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string cNombre { get; set; }

        /// <summary>
        /// Variable representado como almacén de respaldo.
        /// </summary>
        private string _cImagen;

        /// <summary>
        /// Ruta de la imagen del producto.
        /// </summary>
        public string cImagen
        {
            get { return _cImagen; }
            set
            {
                char[] separador = { '_' };

                String[] _arrImagen = value.Split(separador);

                _cImagen = "https://res.cloudinary.com/blue-ocean-technologies/image/upload/v" + _arrImagen[0] + "/Changarro/Productos/" + _arrImagen[1];
            }
        }

        /// <summary>
        /// Descripción del producto
        /// </summary>
        public string cDescripcion { get; set; }

    }

}