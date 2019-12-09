using Changarro.Model.DTO;
using ChangarroBusiness;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{
    public class PerfilDatosAdministradorController : Controller
    {
        Administrador oAdministrador = new Administrador();

        [HttpGet]

        public ActionResult PerfilAdministrador()
        {
            if (Session["iIdAdministrador"] != null)
            {
                int iIdAdministrador = Convert.ToInt32(Session["iIdAdministrador"]);

                AdministradorDTO _oAdministrador = oAdministrador.ObtenerAdministrador(iIdAdministrador);

                return View(_oAdministrador);
            }
        }

        [HttpPost]
        public ActionResult DatosPerfilAdministrador()
        {
            int iIdAdmnistrador = Convert.ToInt32(Session["iIdAdministrador"]);

            AdministradorDTO _oAdministrador = oAdministrador.ObtenerDatosAdministrador(iIdAdmnistrador);

            return PartialView(_oAdministrador);

        }

        public JsonResult ActualizarDatosCliente()
        {
            bool lStatus;
            AdministradorDTO _oAdministrador = JsonConvert.DeserializeObject<AdministradorDTO>(Request["oAdministrador"]); //datos del cliente
            _oAdministrador.iIdAdministrador = Convert.ToInt32(Session["iIdAdministrador"]); //Al objeto _oCliente se le agrega el id del cliente que se encuentra en la sesión

            Administrador oAdministrador = new Administrador();
            try
            {
                _oAdministrador = oAdministrador.EditarDatos(_oAdministrador); //metodo para editar datos
                lStatus = true;
            }
            catch (Exception)
            {
                lStatus = false;
            }

            return Json(new { lStatus, _oAdministrador });
        }
    }
}