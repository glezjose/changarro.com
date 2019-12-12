using System.Web.Mvc;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using Newtonsoft.Json;

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
                TempData["lConexion"] = true;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImagenPerfil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CancelarSuscripcion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MisDirecciones()
        {
            int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            DatosClienteDTO _oCliente = oCliente.ObtenerDatosCliente(iIdCliente);

            return PartialView(_oCliente);
        }
        #endregion

        #region [Métodos]

        /// <summary>
        /// Método para actualizar los datos del usuario
        /// </summary>
        /// <returns>Objeto json con los datos de actualizados del usuario</returns>
        [HttpPost]
        public JsonResult ActualizarDatosCliente()
        {
            bool lStatus;

            DatosClienteDTO _oCliente = JsonConvert.DeserializeObject<DatosClienteDTO>(Request["oCliente"]); //datos del cliente

            _oCliente.iIdCliente = Convert.ToInt32(Session["iIdCliente"]); //Al objeto _oCliente se le agrega el id del cliente que se encuentra en la sesión

            DatosClienteDTO _oDatos = new DatosClienteDTO();

            try
            {
                _oDatos = oCliente.ValidarCliente(_oCliente);

                if (_oDatos == null)
                {
                    _oDatos = oCliente.EditarDatos(_oCliente);                    
                }

                lStatus = true;
            }
            catch (Exception)
            {
                lStatus = false;                
            }

            return Json( new { lStatus, _oDatos }); 
        }

        [HttpPost]
        public JsonResult DesactivarCuenta()
        {
            bool _lStatus = true;

            string _cMensaje = null;

            int _iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            string _cContrasenia = Request["_cContrasenia"];
            
            try
            {
                _cMensaje = oCliente.CancelarSuscripcion(_iIdCliente, _cContrasenia);
            }
            catch (Exception)
            {
                _cMensaje = "Ha ocurrido un error al desactivar la cuenta, intente más tarde";
                _lStatus = false;
            }

            return Json(new { _lStatus, _cMensaje });
        }
        #endregion
    }
}