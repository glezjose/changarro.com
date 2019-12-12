using Changarro.Model.DTO;
using Changarro.Business;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{
    public class PerfilDatosAdministradorController : Controller
    {
        Administrador oAdministrador = new Administrador();

        /// <summary>
        /// Método que carga la página de perfil del Administrador.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PerfilAdministrador()
        {
            if (Session["iIdAdministrador"] != null)
            {
                int iIdAdministrador = Convert.ToInt32(Session["iIdAdministrador"]);

                AdministradorDTO _oAdministrador = oAdministrador.ObtenerAdministrador(iIdAdministrador);

                return View(_oAdministrador);
            }
            return null;
        }

        /// <summary>
        /// Método que devuelve una vista parcial con los datos del Administrador.
        /// </summary>
        /// <returns>Vista html con los datos del administrador.</returns>
        [HttpPost]
        public ActionResult DatosPerfilAdministrador()
        {
            int iIdAdmnistrador = Convert.ToInt32(Session["iIdAdministrador"]);

            AdministradorDTO _oAdministrador = oAdministrador.ObtenerDatosAdministrador(iIdAdmnistrador);

            return PartialView(_oAdministrador);

        }

        /// <summary>
        /// Método para actualizar los datos del Administrador.
        /// </summary>
        /// <returns>Json con los datos del administrador actualizados.</returns>
        [HttpPost]
        public JsonResult ActualizarDatosAdministrador()
        {
            bool lStatus;
            AdministradorDTO _oAdministrador = JsonConvert.DeserializeObject<AdministradorDTO>(Request["oAdministrador"]); //Datos del administrador.
            _oAdministrador.iIdAdministrador = Convert.ToInt32(Session["iIdAdministrador"]); //Al objeto _oAdministrador se le agrega el id del administrador.

            Administrador oAdministrador = new Administrador();
            try
            {
                _oAdministrador = oAdministrador.EditarDatos(_oAdministrador); //Editar datos.
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