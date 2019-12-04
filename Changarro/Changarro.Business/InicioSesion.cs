using Changarro.Model;
using Changarro.Model.DTO;
using ChangarroBusiness;
using System.Data.Entity;
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
        public LoginDTO ValidarLogin(LoginDTO oLogin)
        {
            RegistroUsuario Registro = new RegistroUsuario();

            string _cHashContrasenia = Registro.GenerarHash(oLogin.cContrasenia);

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                LoginDTO _oUsuario = (from c in ctx.tblCat_Cliente.AsNoTracking()
                                     where c.cCorreo == oLogin.cCorreo
                                     select (new LoginDTO
                                     {
                                         iIdUsuario = c.iIdCliente,
                                         cCorreo = c.cCorreo,
                                         cContrasenia = c.cContrasenia

                                     })).FirstOrDefault();

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
            }

            return oLogin;
        }
    }
}
