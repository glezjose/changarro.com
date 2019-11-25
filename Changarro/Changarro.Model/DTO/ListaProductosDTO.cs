using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    public class ListaProductosDTO
    {
        public int iIdProducto { get; set; }
        public string NombreCate { get; set; }
        public int iCantidad { get; set; }
        public decimal dPrecio { get; set; }
        public string cDescripcion { get; set; }
        public string cNombre { get; set; }
        public string cImagen { get; set; }
        public bool lEstatus { get; set; }
        public Nullable<System.DateTime> dtFechaAlta { get; set; }
        public Nullable<System.DateTime> dfFechaBaja { get; set; }
        public Nullable<System.DateTime> dtFechaModificacion { get; set; }
    }
}
