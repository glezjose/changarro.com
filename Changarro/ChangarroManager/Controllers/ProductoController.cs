using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace ChangarroManager.Controllers
{
    public class ProductoController : Controller
    {
        CHANGARROEntities ctx = new CHANGARROEntities();
        Productos ProductoBus = new Productos();

        private string cMensaje = string.Empty;
        private string cEstatus = string.Empty;

        /// <summary>
        /// Vista en la que se carga en una tabla que contiene los registros de productos.
        /// </summary>
        /// <returns>Es una vista con los datos de productos</returns>
        [HttpGet]
        public ActionResult VerProductos()
        {
            return View();
        }

        /// <summary>
        /// Método que carga la vista modal para actualizar un registro
        /// </summary>
        /// <param name="iIdProducto">contiene el Identificador del registro</param>
        /// <returns>vista modal html con formulario para actualizar un registro</returns>
        [HttpPost]
        public ActionResult EditarProducto(int iIdProducto)
        {
            Categoria categoria = new Categoria();

            tblCat_Producto oProducto = ProductoBus.ObtenerProducto(iIdProducto);

            List<ListaCategoriaDTO> _lstCategoria = categoria.CategoriaActiva();

            ViewBag.ListaCategoria = new SelectList(_lstCategoria, nameof(ListaCategoriaDTO.iIdCategoria), nameof(ListaCategoriaDTO.cNombre));

            return View(oProducto);
        }


       
        /// <summary>
        /// Vista que carga un modal para agregar un nuevo producto
        /// </summary>
        /// <returns>regresa una vista parcial con un formulario</returns>
        
        public ActionResult AddProducto()
        {
            Categoria categoria = new Categoria();

            List<ListaCategoriaDTO> _lstCategoria = categoria.CategoriaActiva();

            ViewBag.ListaCategoria = new SelectList(_lstCategoria, nameof(ListaCategoriaDTO.iIdCategoria), nameof(ListaCategoriaDTO.cNombre));

            return View();
        }

        [HttpPost]
        public ActionResult ImportarProducto()
        {
            return View();
        }


        /// <summary>
        /// Método que guarda un nuevo producto
        /// </summary>
        /// <returns>Devuelve un mensaje de acuerdo al estatus de la acción</returns>
        [HttpPost]
        public JsonResult AgregarProducto()
        {
            tblCat_Producto _objProducto;
            try
            {
                _objProducto = JsonConvert.DeserializeObject<tblCat_Producto>(Request["Producto"]);
                ProductoBus.AgregaProducto(_objProducto);

                cMensaje = "Se agregó un producto correctamente.";
                cEstatus = "¡Guardado!";

            }
            catch (Exception e)
            {
                cMensaje = e.Message;
                cEstatus = "Error";
            }

            return Json(new { Mensaje = cMensaje, Estatus = cEstatus }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// vista que toma el id de un registro y muestra los datos en un formulario
        /// </summary>
        /// <param name="iIdProducto">id del producto seleccionado</param>
        /// <returns>devuelve los datos del registro del objeto en una vista modal </returns>
        public ActionResult VisualizarProducto(int id)
        {
            Categoria CategoriaBus = new Categoria();

            tblCat_Producto oProducto = ProductoBus.ObtenerProducto(id);

            ViewBag.cNombreCat = CategoriaBus.ObtenerNombreCategoria(oProducto.iIdCategoria);


            return View(oProducto);

        }


        /// <summary>
        /// Método para obtener la lista de todos los registro para mostrarlo en tabla
        /// </summary>
        /// <returns>devuelve una lista con todos los registros de la tabla </returns>
        [HttpPost]
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

            if (_oArchivoSubido != null && _oArchivoSubido.ContentLength > 0)
            {
                try
                {
                    var cNombreDeArchivo = Path.GetFileName(_oArchivoSubido.FileName);
                    var cRuta = Path.Combine(Server.MapPath("~/Plantillas/PlantillaSubida/"), cNombreDeArchivo);
                    _oArchivoSubido.SaveAs(cRuta);
                    lArchivoSubido = true;
                    cMensaje = "exito";
                }
                catch (Exception e)
                {
                    lArchivoSubido = false;
                    cMensaje = e.Message;
                }
            }

            return Json(new { estatus = lArchivoSubido, mensaje = cMensaje });
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
            return Json(new { message = Mensaje });
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

        /// <summary>
        /// Método para actualizar los datos del producto
        /// </summary>
        /// <returns>Devuelve un mensaje de acuerdo al estatus de la acción</returns>
        [HttpPost]
        public JsonResult ActualizaProducto()
        {
            tblCat_Producto _objProducto;
            try
            {
                _objProducto = JsonConvert.DeserializeObject<tblCat_Producto>(Request["Producto"]);
                ProductoBus.EditarProducto(_objProducto);

                cMensaje = "Se modificó un producto correctamente.";
                cEstatus = "¡Actualizado!";

            }
            catch (Exception e)
            {
                cMensaje = e.Message;
                cEstatus = "Error";
            }

            return Json(new { Mensaje = cMensaje, Estatus = cEstatus }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Método para cambiar el estatus del producto
        /// </summary>
        /// <returns>Devuelve un mensaje de acuerdo al estatus de la acción</returns>
        [HttpPost]
        public JsonResult CambiarEstatusProducto()
        {
            tblCat_Producto _objProducto;
            try
            {
                Productos ProductoBus = new Productos();

                _objProducto = JsonConvert.DeserializeObject<tblCat_Producto>(Request["EstatusProducto"]);

                ProductoBus.DesactivarProducto(_objProducto);

                cMensaje = "Se cambió el estatus correctamente.";
                cEstatus = "¡Actualizado!";

            }
            catch (Exception e)
            {
                cMensaje = e.Message;
                cEstatus = "Error";
            }

            return Json(new { Mensaje = cMensaje, Estatus = cEstatus }, JsonRequestBehavior.AllowGet);
        }

    }
}