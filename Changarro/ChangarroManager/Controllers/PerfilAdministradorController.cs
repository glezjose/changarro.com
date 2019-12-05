using Changarro.Model.DTO;
using Changarro.Model;
using ChangarroBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{
    public class PerfilAdministradorController : Controller
    {
        Administrador d = new Administrador(); 

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CargarDatos()
        {
            List<AdministradorDTO> ListaDatos = d.CargarDatos();

            return Json(ListaDatos);
        }
    }
}