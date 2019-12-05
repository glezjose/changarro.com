using Changarro.Model.DTO;
using Changarro.Model;
using Changarro.Business;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{
    public class ClienteController : Controller
    {

        Cliente lista = new Cliente();  // Instancia de la clase de negocios ClienteBusiness
        Cliente cliente = new Cliente();  // Instancia de la clase de negocios ClienteBusiness

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método que carga los datos de todos los Clientes
        /// </summary>
        /// <return>Objeto JSON con todos los Clientes y su información</return>
        [HttpPost]
        public JsonResult ObtenerClientes()
        {
            List<ClienteAdministradorDTO> ListaClientes = lista.ObtenerClientes();

            return Json(ListaClientes);
        }

       /// <summary>
       /// Método que carga los detalles del cliente seleccionado
       /// </summary>
       /// <param name="iIdCliente"> ID del cliente seleccionado</param>
       /// <returns>Vista con todos los datos de un cliente</returns>
       [HttpPost]
       public ActionResult VerDetallesCliente (int iIdCliente)
        {
            tblCat_Cliente oCliente = cliente.ObtenerDatos(iIdCliente);

            return View(oCliente);
        }

        /// <summary>
        /// Método para cambiar el estatus del cliente seleccionado
        /// </summary>
        /// <param name="iIdCliente"> ID del cliente seleccionado</param>
        /// <param name="lEstatus"> Estatus del cliente seleccionado</param>
        /// <returns>Json con el cambio del Estatus actualizado</returns>
        [HttpPost]
        public JsonResult CambiarEstatusCliente(int iIdCliente, bool lEstatus)
        {
            ClienteAdministradorDTO _oCliente = cliente.CambiarEstatusCliente(iIdCliente, lEstatus);
            return Json(_oCliente);
        }

    }
}