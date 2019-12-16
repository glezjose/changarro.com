using System.Web.Mvc;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Linq;

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

                return RedirectToAction("Inicio", "Producto");
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
        /// Método que devuelve una vista parcial para cambiar la imagen del cliente
        /// </summary>
        /// <returns>Vista HTML parcial con formulario para subir imagen</returns>
        [HttpPost]
        public ActionResult ImagenPerfil()
        {
            return View();
        }

        /// <summary>
        /// Método que devuelve una vista parcial con un formulario para cancelar suscripción
        /// </summary>
        /// <returns>Vista HTML parcial con un formulario para cancelar suscripción</returns>
        [HttpPost]
        public ActionResult CancelarSuscripcion()
        {
            return View();
        }

        /// <summary>
        /// Método que devuelve una vista parcial con los datos de direcciones del cliente
        /// </summary>
        /// <returns>Vista HTML parcial con los datos de las direcciones</returns>
        [HttpPost]
        public ActionResult MisDirecciones()
        {
            Domicilio oDomicilio = new Domicilio();

            int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            List<DomicilioDTO> _lstDomicilios = oDomicilio.ObtenerDomicilios(iIdCliente);

            return PartialView(_lstDomicilios);
        }

        /// <summary>
        /// Método que devuelve una vista parcial con el formulario de edición de domicilios
        /// </summary>
        /// <returns>Vista HTML parcial con el formulario de registro de domicilios</returns>
        [HttpPost]
        public ActionResult FormularioDireccion()
        {
            int.TryParse(Request["iIdDireccion"], out int iIdDireccion);

            Domicilio oDomicilio = new Domicilio();

            DomicilioDTO _oDomicilio = oDomicilio.ObtenerDomicilio(iIdDireccion);

            return View(_oDomicilio);
        }

        /// <summary>
        /// Método que devuelve una vista parcial con el formulario de registro de domicilios
        /// </summary>
        /// <returns>Vista HTML parcial con el formulario de registro de domicilios</returns>
        [HttpPost]
        public ActionResult RegistroDireccion()
        {
            return View();
        }

        /// <summary>
        /// Método que devuelve una vista parcial para desactivar el domicilio
        /// </summary>
        /// <returns>Vista HTML parcial para confirmar la desactivación</returns>
        [HttpPost]
        public ActionResult DesactivarDomicilio()
        {
            int.TryParse(Request["iIdDireccion"], out int iIdDireccion);

            Domicilio oDomicilio = new Domicilio();

            DomicilioDTO _oDomicilio = oDomicilio.ObtenerDomicilio(iIdDireccion);

            return View(_oDomicilio);
        }

        /// <summary>
        /// Método que carga una vista parcial con la lista de todos los estados
        /// </summary>
        /// <returns>Vista HTML parcial con todos los estados registrados puestos en un dropdown</returns>
        [ChildActionOnly]
        public ActionResult MenuEstados()
        {
            Domicilio oDomicilio = new Domicilio();

            List<ListaEstadosDTO> _lstEstados = oDomicilio.ObtenerEstados();

            return PartialView(_lstEstados);
        }

        /// <summary>
        /// Método que devuelve una vista parcial con las tarjetas del usuario
        /// </summary>
        /// <returns>Vista HTML parcial con todas las tarjetas del usuario</returns>
        [HttpPost]
        public ActionResult MisTarjetas()
        {
            Tarjeta oTarjeta = new Tarjeta();

            int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            List<TarjetaDTO> _lstTarjetas = oTarjeta.ObtenerTarjetas(iIdCliente);

            return PartialView(_lstTarjetas);
        }

        /// <summary>
        /// Método que devuelve una vista parcial con el formulario de registro de tarjetas
        /// </summary>
        /// <returns>Vista HTML parcial con el formulario de registro de tarjetas</returns>
        [HttpPost]
        public ActionResult RegistroTarjeta()
        {
            return View();
        }

        /// <summary>
        /// Método que devuelve una vista parcial para desactivar la tarjeta
        /// </summary>
        /// <returns>Vista HTML parcial para confirmar la desactivación</returns>
        [HttpPost]
        public ActionResult DesactivacionTarjeta()
        {
            int.TryParse(Request["iIdTarjeta"], out int iIdTarjeta);

            Tarjeta oTarjeta = new Tarjeta();

            TarjetaDTO _oTarjeta = oTarjeta.ObtenerTarjeta(iIdTarjeta);

            return View(_oTarjeta);
        }

        /// <summary>
        /// Método que devuelve una vista parcial para ver el historial de compras
        /// </summary>
        /// <returns>Vista HTML parcial para con el historial de compras</returns>
        [HttpPost]
        public ActionResult HistorialCompras()
        {
            Cliente oCliente = new Cliente();

            int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            List<HistorialCompraDTO> _lstCompras = oCliente.HistorialCompras(iIdCliente);

            return PartialView(_lstCompras);
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

            DatosClienteDTO _oDatos = new DatosClienteDTO();

            try
            {
                DatosClienteDTO _oCliente = JsonConvert.DeserializeObject<DatosClienteDTO>(Request["oCliente"]);

                _oCliente.iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

                

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

        /// <summary>
        /// Método para desactivar la cuenta del usuario
        /// </summary>
        /// <returns>Objeto json con un valor tipo bool que indica el resultado de la operación 
        /// y una cadena con mensaje de error</returns>
        [HttpPost]
        public JsonResult DesactivarCuenta()
        {
            bool _lStatus = true;

            string _cMensaje = null;
            
            try
            {
                int _iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

                string _cContrasenia = Request["_cContrasenia"];

                _cMensaje = oCliente.CancelarSuscripcion(_iIdCliente, _cContrasenia);
            }
            catch (Exception)
            {
                _cMensaje = "Ha ocurrido un error al desactivar la cuenta, intente más tarde";
                _lStatus = false;
            }

            return Json(new { _lStatus, _cMensaje });
        }

        /// <summary>
        /// Método para actualizar el domicilio
        /// </summary>
        /// <returns>Objeto json con bandera tipo bool</returns>
        [HttpPost]
        public JsonResult ActualizarDomicilio()
        {
            bool lStatus;

            Domicilio oDomicilio = new Domicilio(); 

            try
            {
                DomicilioDTO _oDomicilio = JsonConvert.DeserializeObject<DomicilioDTO>(Request["oDomicilio"]);

                if (_oDomicilio.iIdDireccion > 0)
                {
                    oDomicilio.EditarDomicilio(_oDomicilio);
                }
                else
                {
                    _oDomicilio.iIdCliente = Convert.ToInt32(Session["iIdCliente"]);
                    oDomicilio.AgregarDomicilio(_oDomicilio);
                }                

                lStatus = true;
            }
            catch (Exception)
            {
                lStatus = false;
            }

            return Json(new { lStatus });
        }

        /// <summary>
        /// Método para desactivar direcciones
        /// </summary>
        /// <returns>Objeto json con bandera tipo bool</returns>
        [HttpPost]
        public JsonResult DesactivarDireccion()
        {
            bool lStatus;

            try
            {
                int.TryParse(Request["iIdDireccion"], out int iIdDireccion);

                Domicilio oDomicilio = new Domicilio();

                oDomicilio.DesactivarDomicilio(iIdDireccion);

                lStatus = true;
            }
            catch (Exception)
            {

                lStatus = false;
            }

            return Json(new { lStatus });
        }

        /// <summary>
        /// Método para agregar tarjetas
        /// </summary>
        /// <returns>Objeto json con bandera tipo bool</returns>
        [HttpPost]
        public JsonResult AgregarTarjeta()
        {
            bool lStatus;

            string _oDatos = null;

            Tarjeta oTarjeta = new Tarjeta();
            try
            {
                TarjetaDTO _oTarjeta = JsonConvert.DeserializeObject<TarjetaDTO>(Request["oTarjeta"]);

                _oTarjeta.iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

                _oDatos = oTarjeta.AgregarTarjeta(_oTarjeta);

                lStatus = true;
            }
            catch (Exception)
            {
                lStatus = false;
            }

            return Json(new { lStatus, _oDatos });
        }

        /// <summary>
        /// Método para desactivar tarjetas
        /// </summary>
        /// <returns>Objeto json con bandera tipo bool</returns>
        [HttpPost]
        public JsonResult DesactivarTarjeta()
        {
            bool lStatus;

            Tarjeta oTarjeta = new Tarjeta();

            try
            {
                int.TryParse(Request["iIdTarjeta"], out int iIdTarjeta);

                oTarjeta.DesactivarTarjeta(iIdTarjeta);

                lStatus = true;
            }
            catch (Exception)
            {
                lStatus = false;
            }
            return Json(new { lStatus });
        }

        /// <summary>
        /// Método para subir imágenes
        /// </summary>
        /// <returns>Objeto json con bandera tipo bool y cadena con mensaje de error</returns>
        [HttpPost]
        public JsonResult SubirImagen(HttpPostedFileBase file)
        {
            bool _lStatus;

            int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

            string _cNuevaImagen = null;

            string _cNombreImagen;

            Cliente _oCliente = new Cliente();

            try
            {
                DatosClienteDTO _oDatosCliente = _oCliente.ObtenerDatosCliente(iIdCliente);
                _oDatosCliente.cTelefono = _oDatosCliente.cTelefono != "N/A" ? _oDatosCliente.cTelefono : "imgDefCliente";
                _cNombreImagen = _oDatosCliente.cNombre.First() + new string(_oDatosCliente.cTelefono.Take(9).ToArray());

                if (file != null || file.ContentLength != 0)
                {
                    Account account = new Account(

                      "blue-ocean-technologies",
                      "488187921138398",
                      "zqhipLe2tEf3tIr5FI_JAIQaU-I");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.InputStream),
                        PublicId = "Changarro/Clientes/" + _cNombreImagen,
                        Overwrite = true,
                    };       

                    string _cImagen = String.Format(cloudinary.Upload(uploadParams).Version + "_" + _cNombreImagen);                    

                    _cNuevaImagen = _oCliente.CambiarImagen(iIdCliente, _cImagen);
                }

                _lStatus = true;
            }
            catch (Exception)
            {
                _lStatus = false;

                _cNuevaImagen = "Tu imagen no pudo ser actualizada, por favor intente más tarde";
            }

            return Json(new {_lStatus, _cNuevaImagen });
        }
        #endregion
    }
}