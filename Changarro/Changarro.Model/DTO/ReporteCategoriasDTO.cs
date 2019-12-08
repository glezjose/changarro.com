using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase contiene las propiedades necesarias para obtener los productos por categoría.
    /// </summary>
    public class ReporteCategoriasDTO
    {

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
