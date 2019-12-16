///////////////////////////////////////////////////////////
//  DetallesProductoDTO.cs
//  Implementation of the Class DetallesProductoDTO
//  Generated by Enterprise Architect
//  Created on:      20-nov.-2019 04:24:48 p. m.
//  Original author: Mike
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Changarro.Model.DTO {
    public class DetallesProductoDTO {

        public int iIdProducto { get; set; }
        public int iIdCategoria { get; set; }
        public int iCantidad { get; set; }
        public decimal dPrecio { get; set; }
        public string cNombre { get; set; }
        /// <summary>
        /// Variable representado como almac�n de respaldo.
        /// </summary>
        private string _cImagen;

        /// <summary>
        /// Ruta de la imagen de perfil del cliente.
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
        public string cDescripcion { get; set; }

    }

}