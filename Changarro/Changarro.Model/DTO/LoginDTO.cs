namespace Changarro.Model.DTO
{
    public class LoginDTO {

        /// <summary>
        /// Propiedad que almacena el ID del usuario;
        /// </summary>
        public int iIdUsuario { get; set; }

        /// <summary>
        /// Propiedad que almacena la contraseña del usuario.
        /// </summary>
        public string cContrasenia { get; set; }

        /// <summary>
        /// Propiedad que almacena el correo del usuario.
        /// </summary>
        public string cCorreo { get; set; }

    }
}