﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    public class HistorialCompraDTO
    {
        public int iIdProducto { get; set; }
        public int iCantidad { get; set; }
        public decimal dPrecio { get; set; }
        public string cNombre { get; set; }
        public string cImagen { get; set; }
        public DateTime? dtFechaCompra { get; set; }
    }
}