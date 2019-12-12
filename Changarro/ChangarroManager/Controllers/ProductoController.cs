using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace ChangarroManager.Controllers
{
    public class ProductoController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        CHANGARROEntities ctx = new CHANGARROEntities();
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VerProductos()
        {
            return View();
        }
        public ActionResult EditarProducto()
        {
            return View();
        }
        public ActionResult AddProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportarProducto()
        {
            return View();
        }
        public JsonResult AgregarProducto(tblCat_Producto _objProducto)
        {
            bool r = false;

            try
            {
                Productos ProductoBus = new Productos();

                tblCat_Producto obp = new tblCat_Producto
                {
                    cNombre = _objProducto.cNombre
                     
                };

                ProductoBus.AgregarProducto(obp);

                r = true;

            }
            catch (Exception)
            {
                r = false;
            }

            return Json(new { result = r }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VisualizarProducto(int id)
        {
            using (ctx)
            {
                Productos changarroBusiness = new Productos();
                tblCat_Producto oProducto = changarroBusiness.ObtenerProducto(id);
                return View(oProducto);
            }
        }

        public JsonResult Listar()
        {
            List<ListaProductosDTO> lstProducto = new List<ListaProductosDTO>();

            Productos ProductoSer = new Productos();
            lstProducto = ProductoSer.ObtenerListaProductos();

            return Json(new { data = lstProducto }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método que retorna el archivo generado para descargar la plantilla vacía
        /// </summary>
        /// <returns>Retorna el archivo a descargar</returns>
        [HttpGet]
        public ActionResult DescargarPlantilla()
        {
            Productos oLogicaProducto = new Productos();
            string cRutaPlantilla = oLogicaProducto.GenerarPlantillaVacia();
            byte[] datosBinariosPlantilla = System.IO.File.ReadAllBytes(cRutaPlantilla);
            return File(datosBinariosPlantilla, System.Net.Mime.MediaTypeNames.Application.Octet, "Plantilla.xlsx");
        }

        /// <summary>
        /// Método que Guarda en el servidor el archivo subido por el lado del cliente para registrar productos (Plantilla llena)
        /// </summary>
        /// <returns>Retorna un mensaje y el estatus booleano de la operación</returns>
        [HttpPost]
        public ActionResult SubirArchivo()
        {
            bool lArchivoSubido = false;

            string cMensaje = "pendiente";

            var _oArchivoSubido = Request.Files[0];
            
            if(_oArchivoSubido != null && _oArchivoSubido.ContentLength > 0)
            {
                try
                {
                    var cNombreDeArchivo = Path.GetFileName(_oArchivoSubido.FileName);
                    var cRuta = Path.Combine(Server.MapPath("~/Plantillas/PlantillaSubida/"), cNombreDeArchivo);
                    _oArchivoSubido.SaveAs(cRuta);
                    lArchivoSubido = true;
                    cMensaje = "exito";
                }catch(Exception e)
                {
                    lArchivoSubido = false;
                    cMensaje = e.Message;
                }
            }
            
            return Json(new { estatus = lArchivoSubido, mensaje = cMensaje});
        }

        /// <summary>
        /// Método que llama al método de lógica para guardar en la BDD los registros del archivo subido (Excel Plantilla llena)
        /// </summary>
        /// <param name="_cNombreArchivo">Nombre del archivo subido por el lado del cliente</param>
        /// <returns>retorna un json con un mensaje que de estatus de la operación</returns>
        [HttpPost]
        public ActionResult ImportarRegistros(string _cNombreArchivo)
        {
            Productos oProductosBusiness = new Productos();
            string Mensaje = oProductosBusiness.ImportarProductosEnPlantilla(_cNombreArchivo);
            return Json(new { message = Mensaje});
        }
        /// <summary>
        /// Método que retorna el archivo con los registros de la BDD (Trabajo en proceso)
        /// </summary>
        /// <returns>Retorna el Archivo a descargar</returns>
        [HttpGet]
        public ActionResult ExportarRegistros()
        {
            Productos oLogicaProducto = new Productos();
            string cRutaPlantilla = oLogicaProducto.ExportarRegistrosExcel();
            byte[] datosBinariosPlantilla = System.IO.File.ReadAllBytes(cRutaPlantilla);
            return File(datosBinariosPlantilla, System.Net.Mime.MediaTypeNames.Application.Octet, "DatosChangarro.xlsx");
        }
    }
}