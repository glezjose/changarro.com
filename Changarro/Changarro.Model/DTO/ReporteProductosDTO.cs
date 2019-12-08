using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase contiene las propiedades necesarias para obtener los productos más vendidos.
    /// </summary>
    public class ReporteProductosDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int iIdProducto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int iIdDetalleCompra { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int iCantidad{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cNombre{ get; set; }

        
    }
}
