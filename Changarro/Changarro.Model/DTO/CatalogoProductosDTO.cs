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
        /// Propiedad que almacena la imagen del producto
        /// </summary>
        public string cImagen { get; set; }

        /// <summary>
        /// Propiedad que almacena la fecha de alta del producto
        /// </summary>
        public DateTime dtFechaAlta { get; set; }
    }
}
