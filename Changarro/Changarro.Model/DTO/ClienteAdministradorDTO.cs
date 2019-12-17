using System;

namespace Changarro.Model.DTO
{
    public class ClienteAdministradorDTO {

        /// <summary>
        /// Apellido del cliente.
        /// </summary>
        public string cApellido { get; set; }

        /// <summary>
        /// Correo electrónico del cliente.
        /// </summary>
        public string cCorreo { get; set; }

        /// <summary>
        /// Nombre del cliente.
        /// </summary>
        public string cNombre { get; set; }
        /// <summary>
        /// Número telefónico del cliente.
        /// </summary>
        public string cTelefono { get; set; }

        /// <summary>
        /// Fecha de alta del cliente.
        /// </summary>
        public DateTime? dtFechaAlta { get; set; }

        /// <summary>
        /// Fecha de Baja del cliente.
        /// </summary>
        public DateTime? dtFechaBaja { get; set; }

        /// <summary>
        /// Fecha de Modificación del cliente.
        /// </summary>
        public DateTime? dtFechaModificacion { get; set; }

        /// <summary>
        /// Identificador del Id.
        /// </summary>
        public int iIdCliente { get; set; }

        /// <summary>
        /// Estatus del Cliente.
        /// </summary>
        public bool lEstatus { get; set; }
    }

}