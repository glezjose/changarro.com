using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{

    public class InicioController : Controller
    {
        readonly ReporteGraficas oReportes = new ReporteGraficas();
        string _cMensaje = null;

        public ActionResult Inicio()
        {
            return View();
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
            List<ReporteUsuariosDTO> _lstClientes = new List<ReporteUsuariosDTO>();
            try
            {
                _lstClientes = oReportes.ObtenerUsuariosConMasCompras();
            }
            catch (Exception e)
            {

                _cMensaje = e.Message;
            }
           
            return Json(new { _lstClientes,_cMensaje});
        }
    }
}