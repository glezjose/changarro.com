using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;

namespace ChangarroManager.Controllers
{

    public class InicioController : Controller
    {
        readonly ReporteGraficas oReportes = new ReporteGraficas(); //Instancia del business

        string _cMensaje = null;

        #region [Vistas]
        /// <summary>
        /// Método que carga la vista principal al condicionar la sesión del administrador.
        /// </summary>
        /// <returns>Vista principal.</returns>
        public ActionResult Inicio()
        {
            if (Session["iIdAdministrador"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Inicio");
            }
        }

        // <summary>
        /// Método que carga la vista de inicio de sesión.
        /// </summary>
        /// <returns>Vista inicio de sesión.</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        #endregion

        #region Sesión
        /// <summary>
        /// Método para el inicio de sesión del administrador.
        /// </summary>
        /// <returns>Objeto Json con el mensaje de error.</returns>
        [HttpPost]
        public JsonResult IniciarSesion()
        {
            LoginDTO _oAdministrador = JsonConvert.DeserializeObject<LoginDTO>(Request["oAdmin"]);

            InicioSesion _Login = new InicioSesion();
            try
            {
                LoginDTO _oSesion = _Login.ValidarLogin(_oAdministrador, false);

                if (_oSesion.iIdUsuario > 0)
                {
                    Session["iIdAdministrador"] = _oSesion.iIdUsuario.ToString();
                    _oAdministrador = null;

                    RedirectToAction("Inicio");
                }
                else
                {
                    _oAdministrador = _oSesion;
                }
            }
            catch (Exception)
            {
                _cMensaje = "Ha ocurrido un error al iniciar sesión por favor intente más tarde";
            }
            return Json(new { _cMensaje, _oAdministrador });
        }

        /// <summary>
        /// Método para cerrar sesión del administrador.
        /// </summary>
        /// <returns>Acción que redirige al administrador a la página de inicio de sesión.</returns>
        [HttpPost]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut(); //Elimina información de autenticación.
            Session.Abandon(); // Limpiará la sesión al final de la petición.
            return RedirectToAction("Login", "Inicio");
        }
        #endregion

        #region Gráficas
        /// <summary>
        /// Método que obtiene los productos más vendidos.
        /// </summary>
        /// <returns>Devuelve la lista en objeto Json y el mensaje de excepción.</returns>
        [HttpPost]
        public JsonResult ListaProductosMasVendidos()
        {
            List<ReporteProductosDTO> _lstLista = new List<ReporteProductosDTO>();
            try
            {
                _lstLista = oReportes.ObtenerProductosMasVendidos();
            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }

            return Json(new { _lstLista, _cMensaje });
        }

        /// <summary>
        /// Método que obtiene los 10 clientes con más compras.
        /// </summary>
        /// <returns>Devuelve la lista en objeto Json y el mensaje de excepción.</returns>
        [HttpPost]
        public JsonResult ListaClientesConMasCompras()
        {
            List<ReporteUsuariosDTO> _lstLista = new List<ReporteUsuariosDTO>();
            try
            {
                _lstLista = oReportes.ObtenerUsuariosConMasCompras();
            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }

            return Json(new { _lstLista, _cMensaje });
        }


        /// <summary>
        /// Método que obtiene productos por cada categoría.
        /// </summary>
        /// <returns>Devuelve la lista en objeto Json y el mensaje de excepción.</returns>
        [HttpPost]
        public JsonResult ListaProductosPorCategoria()
        {
            List<ReporteCategoriasDTO> _lstLista = new List<ReporteCategoriasDTO>();
            try
            {
                _lstLista = oReportes.ObtenerProductosporCategoria();

            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }

            return Json(new { _lstLista, _cMensaje });
        }

        #endregion

    }
}