using Changarro.Business;
using Changarro.Model.DTO;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace ChangarroUser.Controllers
{
    public class InicioController : Controller
    {
        #region [Vistas]

        /// <summary>
        /// Método que carga la vista de la página principal
        /// </summary>
        /// <returns>Vista HTML de la página principal</returns>
        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la página de registro
        /// </summary>
        /// <returns>Vista HTML de la página de registro</returns>
        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la vista de la página de inicio de sesión
        /// </summary>
        /// <returns>Vista HTML de la página de inicio de sesión</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la vista parcial de la página de cierre de sesión
        /// </summary>
        /// <returns>Vista HTML parcial de cierre de sesión</returns>
        [HttpPost]
        public ActionResult LogOut()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la vista parcial de la página de inicio de sesión
        /// </summary>
        /// <returns>Vista HTML parcial inicio de sesión</returns>
        [HttpPost]
        public ActionResult LoginParcial()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la vista parcial de la página de registro
        /// </summary>
        /// <returns>Vista HTML parcial del registro</returns>
        [HttpPost]
        public ActionResult RegistroParcial()
        {
            return View();
        }
        #endregion

        #region [Métodos]

        /// <summary>
        /// Método para registrar clientes
        /// </summary>
        /// <returns>Mensajes de error y validaciones</returns>
        [HttpPost]
        public JsonResult RegistrarCliente()
        {
            string _cMensajeError = null;
            RegistroDTO _oUsuario = JsonConvert.DeserializeObject<RegistroDTO>(Request["oCliente"]);

            RegistroUsuario Registro = new RegistroUsuario();
            Carrito carrito = new Carrito();
            InicioSesion Login = new InicioSesion();

            try
            {
                RegistroDTO _oMensajesError = Registro.ValidarDatos(_oUsuario);

                if (_oMensajesError == null)
                {
                    int iIdCliente = Registro.RegistrarCliente(_oUsuario);
                    carrito.RegistrarCarrito(iIdCliente);

                    Session["iIdCliente"] = iIdCliente.ToString();

                    _oUsuario = null;
                }
                else
                {
                    _oUsuario = _oMensajesError;
                }
            }
            catch (Exception)
            {
                _cMensajeError = "Ha ocurrido un error al registrarse por favor intente mas tarde";
            }
            return Json(new { _cMensajeError, _oUsuario });
        }

        /// <summary>
        /// Método para el inicio de sesión de los clientes
        /// </summary>
        /// <returns>Mensajes de error y validaciones</returns>
        [HttpPost]
        public JsonResult IniciarSesion()
        {
            string _cMensajeError = null;

            LoginDTO _oUsuario = JsonConvert.DeserializeObject<LoginDTO>(Request["oCliente"]);

            InicioSesion Login = new InicioSesion();
            try
            {
                LoginDTO _oLogin = Login.ValidarLogin(_oUsuario, true);

                if (_oLogin.iIdUsuario > 0)
                {
                    Session["iIdCliente"] = _oLogin.iIdUsuario.ToString();
                    _oUsuario = null;

                    RedirectToAction("Inicio");
                }
                else
                {
                    _oUsuario = _oLogin;
                }
            }
            catch (System.Exception)
            {
                _cMensajeError = "Ha ocurrido un error al iniciar sesión por favor intente mas tarde";
            }
            return Json(new { _cMensajeError, _oUsuario });
        }

        /// <summary>
        /// Método para cerrar sesión
        /// </summary>
        /// <returns>Vista de pagina de inicio</returns>
        [HttpPost]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // Limpiara la sesión al final de la petición
            return RedirectToAction("inicio", "Producto");
        }
        #endregion
    }
}
