using Changarro.Business;
using Changarro.Model.DTO;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace ChangarroUser.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método para registrar clientes
        /// </summary>
        /// <returns>Mensajes de error y validaciones</returns>
        [HttpPost]
        public JsonResult RegistrarCliente()
        {
            string _cMensajeError = string.Empty;
            string _clienteJSON = Request["oCliente"];
            RegistroDTO _oUsuario = JsonConvert.DeserializeObject<RegistroDTO>(_clienteJSON);
            RegistroUsuario Registro = new RegistroUsuario();
            Carrito carrito = new Carrito();

            try
            {
                RegistroDTO _oMensajesError = Registro.ValidarDatos(_oUsuario);

                if (_oMensajesError == null)
                {
                   int iIdCliente = Registro.RegistrarCliente(_oUsuario);

                    carrito.RegistrarCarrito(iIdCliente);
                }
                else
                {
                    _oUsuario = _oMensajesError;
                }
            }
            catch (System.Exception)
            {
                _cMensajeError = "Ha ocurrido un error al registrarse por favor intente mas tarde";
            }
            return Json(new { _cMensajeError, _oUsuario });
        }

        [HttpPost]
        public JsonResult IniciarSesion()
        {
            string _cMensajeError = string.Empty;
            string _loginJSON = Request["oLogin"];
            LoginDTO _oUsuario = JsonConvert.DeserializeObject<LoginDTO>(_loginJSON);

            //InicioSesion Iniciar = new InicioSesion();
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }
            return Json(null);


        }
    }
}
