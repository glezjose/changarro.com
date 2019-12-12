using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    public class DomicilioCompraDTO
    {
        /// <summary>
        /// La ID del domicilio.
        /// </summary>
        public int iIdDomicilio { get; set; }

        /// <summary>
        /// El nombre del domicilio.
        /// </summary>
        public string cNombre { get; set; }  

        /// <summary>
        /// El domicilio completo.
        /// </summary>
        public string cDomicilio { get; set; }
    }
}
