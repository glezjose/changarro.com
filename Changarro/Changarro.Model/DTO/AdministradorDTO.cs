using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    public class AdministradorDTO
    {
        /// <summary>
        /// Apellido del administrador.
        /// </summary>
        public string cApellido { get; set; }
        /// <summary>
        /// Correo electrónico del administrador.
        /// </summary>
        public string cCorreo { get; set; }
        /// <summary>
        /// Imagen de perfil del administrador.
        /// </summary>
        public string cImagen { get; set; }
        /// <summary>
        /// Nombre del administrador.
        /// </summary>
        public string cNombre { get; set; }
        /// <summary>
        /// Número telefónico del administrador.
        /// </summary>
        public string cTelefono { get; set; }
        /// <summary>
        /// Identificador del administrador.
        /// </summary>
        public int iIdAdministrador { get; set; }
    }
}
