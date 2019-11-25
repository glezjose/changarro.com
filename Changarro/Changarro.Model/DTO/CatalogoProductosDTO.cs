using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    public class CatalogoProductosDTO
    {
        public int iIdProducto { get; set; }
        public int iIdCategoria { get; set; }
        public decimal dPrecio { get; set; }
        public string cNombre { get; set; }
        public string cImagen { get; set; }
        public DateTime dtFechaAlta { get; set; }
    }
}
