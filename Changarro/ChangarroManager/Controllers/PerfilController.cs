using Changarro.Model.DTO;
using ChangarroBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{
    public class PerfilController : Controller
    {
        Administrador Datos = new Administrador();  // Instancia de la clase de negocios ClienteBusiness
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public JsonResult ObtenerDatosAdministrador()
        //{

        //}
    }
}