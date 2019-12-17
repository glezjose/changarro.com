﻿using System;

namespace Changarro.Model.DTO
{
    public class CatalogoProductosDTO
    {
        /// <summary>
        /// Propiedad que almacena el id del producto
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// Propiedad que almacena el id de la categoría del producto
        /// </summary>
        public int iIdCategoria { get; set; }

        /// <summary>
        /// Propiedad que almacena la cantidad del producto
        /// </summary>
        public int iCantidad { get; set; }

        /// <summary>
        /// Propiedad que almacena el precio del producto
        /// </summary>
        public decimal dPrecio { get; set; }

        /// <summary>
        /// Propiedad que almacena el nombre del producto
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
        /// Propiedad que almacena la fecha de alta del producto
        /// </summary>
        public DateTime dtFechaAlta { get; set; }
    }
}
