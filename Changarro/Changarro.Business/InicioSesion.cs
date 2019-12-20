using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;

namespace Changarro.Business
{
    public class InicioSesion
    {
        /// <summary>
        /// Método para validar el inicio de sesión del usuario
        /// </summary>
        /// <param name="oLogin">Objeto con las credenciales de inicio de sesión del usuario</param>
        /// <returns>Objeto con las credenciales inválidas de inicio de sesión del usuario</returns>
        public LoginDTO ValidarLogin(LoginDTO oLogin, bool lOpcionMetodo)
        {

            RegistroUsuario Registro = new RegistroUsuario();

            string _cHashContrasenia = Registro.GenerarHash(oLogin.cContrasenia);

            LoginDTO _oUsuario = new LoginDTO();

            if (lOpcionMetodo == true)
            {
                _oUsuario = ObtenerCliente(oLogin.cCorreo);
            }
            else
            {
                _oUsuario = ObtenerAdministrador(oLogin.cCorreo);
            }

            if (_oUsuario != null)
            {
                oLogin.cCorreo = null;

                if (_oUsuario.cContrasenia == _cHashContrasenia)
                {
                    oLogin.cContrasenia = null;
                    oLogin.iIdUsuario = _oUsuario.iIdUsuario;
                }
                else
                {
                    oLogin.cCorreo = null;
                }
            }
            else
            {
                oLogin.cContrasenia = null;
            }

            return oLogin;
        }

        /// <summary>
        /// Método para obtener datos de inicio de sesión del administrador.
        /// </summary>
        /// <param name="cCorreo">Correo del administrador.</param>
        /// <returns>Devuelve datos completos del administrador.</returns>
        public LoginDTO ObtenerAdministrador(string cCorreo)
        {
            LoginDTO _oAdministrador = new LoginDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                ctx.Configuration.AutoDetectChangesEnabled = false;

                _oAdministrador = ctx.tblCat_Administrador.AsNoTracking()
                                    .Where(c => c.cCorreo == cCorreo)
                                    .Select(l => new LoginDTO
                                    {
                                        iIdUsuario = l.iIdAdministrador,
                                        cCorreo = l.cCorreo,
                                        cContrasenia = l.cContrasenia

                                    }).FirstOrDefault();
            }

            return _oAdministrador;
        }

        /// <summary>
        /// Método para obtener datos de inicio de sesión del cliente
        /// </summary>
        /// <param name="cCorreo">Cadena con el correo del cliente</param>
        /// <returns>Objeto con datos del cliente</returns>
        public LoginDTO ObtenerCliente(string cCorreo)
        {
            LoginDTO _oUsuario = new LoginDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                ctx.Configuration.AutoDetectChangesEnabled = false;

                _oUsuario = ctx.tblCat_Cliente.AsNoTracking()
                                    .Where(c => c.cCorreo == cCorreo && c.lEstatus == true)
                                    .Select(l => new LoginDTO
                                    {
                                        iIdUsuario = l.iIdCliente,
                                        cCorreo = l.cCorreo,
                                        cContrasenia = l.cContrasenia

                                    }).FirstOrDefault();
            }

            return _oUsuario;
        }
    }
}
