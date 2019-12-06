using System.Web.Mvc;
using Changarro.Model.DTO;
using Changarro.Business;
using System;

namespace ChangarroUser.Controllers
{
    public class PerfilController : Controller
    {
        Cliente oCliente = new Cliente();

        #region [Vistas]

        /// <summary>
        /// Método que carga la pagina de perfil del cliente
        /// </summary>
        /// <returns>Vista HTML del perfil del cliente</returns>
        [HttpGet]
        public ActionResult MiPerfil()
        {
            if (Session["iIdCliente"] != null)
            {
                int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

                ClienteDTO _oCliente = oCliente.ObtenerCliente(iIdCliente);

                return View(_oCliente);
            }
            else
            {
                return RedirectToAction("Inicio","Producto");
            }           
        }

        /// <summary>
        /// Método que devuelve una vista parcial con los datos del cliente
        /// </summary>
        /// <returns>Vista HTML parcial con los datos del cliente</returns>
        [HttpPost]
        public ActionResult MisDatos()
        {
            int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            DatosClienteDTO _oCliente = oCliente.ObtenerDatosCliente(iIdCliente);

            return PartialView(_oCliente);
        }
        #endregion
    }
}