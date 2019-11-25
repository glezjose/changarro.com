using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;

namespace Changarro.Business
{
    public class InicioSesion
    {
        CHANGARROEntities ctx = new CHANGARROEntities();
        public string ValidarLogin(LoginDTO oLogin)
        {
            string _cMensaje;
            try
            {
                if (ctx.tblCat_Cliente.Any(c => c.cCorreo == oLogin.cCorreo))
                {
                    if (ctx.tblCat_Cliente.Any(c => c.cContrasenia == oLogin.cContrasenia))
                    {
                        _cMensaje = string.Empty;
                    }
                    else
                    {
                        _cMensaje = "Contraseña incorrecta";
                    }
                }
                else
                {
                    _cMensaje = "Esta dirección de correo no se encuentra registrada";
                }
            }
            catch (System.Exception)
            {

                _cMensaje = "Error de conexión por favor intente mas tarde";
            }
            
            return _cMensaje;
        }
    }
}
