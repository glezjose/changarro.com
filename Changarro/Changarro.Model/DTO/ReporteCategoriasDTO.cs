using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase es la responsable de obtener nombre de la categoría y la cantidad de productos.
    /// </summary>
    public class ReporteCategoriasDTO
    {

        public int iIdCategoria { get; set; }

        public int iIdProducto { get; set; }

        /// <summary>
        /// Cantidad de productos.
        /// </summary>
        public int iCantidad { get; set; }

        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        public string cNombre { get; set; }


    }
}
