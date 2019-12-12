namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase contiene los datos necesarios a utilizar.
    /// </summary>
    public class AdministradorDTO
    {
        public string cApellido { get; set; }
        /// <summary>
        /// Correo electrónico del administrador.
        /// </summary>
        public string cCorreo { get; set; }
        /// <summary>
        /// Teléfono del administrador.
        /// </summary>
        public string cTelefono { get; set; }
        /// <summary>
        /// Foto de perfil del administrador.
        /// </summary>
        public string cImagen { get; set; }
        /// <summary>
        /// Nombre del administrador.
        /// </summary>
        public string cNombre { get; set; }

        /// <summary>
        /// Identificador del administrador.
        /// </summary>
        public int iIdAdministrador { get; set; }
    }
}
