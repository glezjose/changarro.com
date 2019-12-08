using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;

namespace ChangarroManager.Controllers
{

    public class InicioController : Controller
    {
        readonly ReporteGraficas oReportes = new ReporteGraficas();
        string _cMensaje = null;

        #region [Vistas]
        /// <summary>
        /// Método que carga la vista principal.
        /// </summary>
        /// <returns>Vista HTML principal</returns>
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
        /// <returns>Vista HTML de inicio de sesión.</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// Método para el inicio de sesión del administrador
        /// </summary>
        /// <returns>Mensajes de error y validaciones</returns>
        [HttpPost]
        public JsonResult IniciarSesion()
        {
            string _cMensajeError = null;

            LoginDTO _oUsuario = JsonConvert.DeserializeObject<LoginDTO>(Request["oAdmin"]);

            InicioSesion Login = new InicioSesion();
            try
            {
                LoginDTO _oLogin = Login.ValidarLogin(_oUsuario, false);

                if (_oLogin.iIdUsuario > 0)
                {
                    Session["iIdAdministrador"] = _oLogin.iIdUsuario.ToString();
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
        /// Método que obtiene productos por cada categoría.
        /// </summary>
        /// <returns>Devuelve la lista y el mensaje de excepción.</returns>
        [HttpPost]
        public JsonResult ListaProductosPorCategoria ()
        {
            List<ReporteCategoriasDTO> lstLista = new List<ReporteCategoriasDTO>();
            try
            {
                lstLista = oReportes.ObtenerProductosporCategoria();

            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }
            
            return Json(new {lstLista, _cMensaje});
        }

        /// <summary>
        /// Método que obtiene los 10 clientes con más compras.
        /// </summary>
        /// <returns>Devuelve la lista y el mensaje de excepción</returns>
        [HttpPost]
        public JsonResult ListaClientesConMasCompras()
        {
            List<ReporteUsuariosDTO> lstLista = new List<ReporteUsuariosDTO>();
            try
            {
                lstLista = oReportes.ObtenerUsuariosConMasCompras();
            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }
           
            return Json(new { lstLista,_cMensaje});
        }

        /// <summary>
        /// Método que obtiene los 10 clientes con más compras.
        /// </summary>
        /// <returns>Devuelve la lista y el mensaje de excepción</returns>
        [HttpPost]
        public JsonResult ListaPorductosMasVendidos()
        {
            List<ReporteProductosDTO> lstLista = new List<ReporteProductosDTO>();
            try
            {
                lstLista = oReportes.ObtenerProductosMasVendidos();
            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }

            return Json(new { lstLista, _cMensaje });
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
            return RedirectToAction("Login", "Inicio");
        }
    }
}