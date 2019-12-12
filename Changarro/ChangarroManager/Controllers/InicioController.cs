using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using System.Linq;

namespace ChangarroManager.Controllers
{

    public class InicioController : Controller
    {
        readonly ReporteGraficas oReportes = new ReporteGraficas(); //Instancia del business gráficas.
        readonly Administrador oAdministrador = new Administrador(); // Instancia del business administrador.

        string _cMensaje = null; // Variable que almacena un mensaje de error y validación.

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
        /// <returns>Objeto Json con el mensaje de error y validación.</returns>
        [HttpPost]
        public JsonResult IniciarSesion()
        {
            LoginDTO _oAdministrador = JsonConvert.DeserializeObject<LoginDTO>(Request["oAdmin"]);

            InicioSesion _oLogin = new InicioSesion(); //Instancia de la clase business.

            try
            {
                LoginDTO _oSesion = _oLogin.ValidarLogin(_oAdministrador, false);

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
                _cMensaje = "Ha ocurrido un error poder establecer una conexión para iniciar tu sesión, por favor intente más tarde.";
            }
            return Json(new { _oAdministrador, _cMensaje });
        }

        /// <summary>
        /// Método para obtener nombre del administrador.
        /// </summary>
        /// <returns>Devuelve una vista parcial.</returns>
        [ChildActionOnly]
        public ActionResult ObtenerNombreAdministrador()
        {
            ViewBag.cNombre = oAdministrador.ObtenerNombreAdministrador(Convert.ToInt32(Session["iIdAdministrador"]));

            return PartialView();
        }

        /// <summary>
        /// Método para cerrar sesión del administrador.
        /// </summary>
        /// <returns>Acción que redirige al administrador a la página de inicio de sesión.</returns>
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut(); //Elimina información de autenticación.
            Session.RemoveAll(); // Elimina los elementos agregados en el contenido del objeto session.
            return RedirectToAction("Login");
        }
        #endregion

        #region Gráficas
        /// <summary>
        /// Método que obtiene los productos más vendidos.
        /// </summary>
        /// <returns>Devuelve la lista en objeto Json con el mensaje de excepción y validación.</returns>
        [HttpPost]
        public JsonResult ListaProductosMasVendidos()
        {
            List<ReporteProductosDTO> _lstLista = new List<ReporteProductosDTO>();
            try
            {
                _lstLista = oReportes.ObtenerProductosMasVendidos();
                if (!_lstLista.Any())
                {
                    _cMensaje = "No hay productos vendidos.";
                }
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
        /// <returns>Devuelve la lista en objeto Json con el mensaje de excepción y validación.</returns>
        [HttpPost]
        public JsonResult ListaClientesConMasCompras()
        {
            List<ReporteUsuariosDTO> _lstLista = new List<ReporteUsuariosDTO>();
            try
            {
                _lstLista = oReportes.ObtenerUsuariosConMasCompras();
                if (!_lstLista.Any())
                {
                    _cMensaje = "Ningún cliente ha realizado compras.";
                }
            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }

            return Json(new { _lstLista, _cMensaje });
        }


        /// <summary>
        /// Método que obtiene los productos por cada categoría.
        /// </summary>
        /// <returns>Devuelve la lista en objeto Json con el mensaje de excepción y validación.</returns>
        [HttpPost]
        public JsonResult ListaProductosPorCategoria()
        {
            List<ReporteCategoriasDTO> _lstLista = new List<ReporteCategoriasDTO>();
            try
            {
                _lstLista = oReportes.ObtenerProductosporCategoria();
                if (!_lstLista.Any())
                {
                    _cMensaje = "No hay ningún producto registrado en cada categoría.";
                }
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