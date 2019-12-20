using System;

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
    }
}
