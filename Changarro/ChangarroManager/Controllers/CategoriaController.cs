using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ChangarroManager.Controllers
{
    public class CategoriaController : Controller
    {
        Categoria categoriaSer = new Categoria();

        private string cMensaje = string.Empty;
        private string cEstatus = string.Empty;

        /// <summary>
        /// Esta acción es una vista parcial que contiene una tabla con todos los registros de las categorías.
        /// </summary>
        /// <returns>Regresa la vista parcial con una tabla de las categorías</returns>
        [HttpPost]
        public ActionResult Categoria()
        {
            return View();
        }


        /// <summary>
        /// Método que devuelve una lista de las categorías de la tabla de categorías
        /// </summary>
        /// <returns>devuelve una lista con los datos de la tabla</returns>
        [HttpGet]
        public JsonResult ListarCat()
        {
            List<ListaCategoriaDTO> lstCategoria = new List<ListaCategoriaDTO>();

            lstCategoria = categoriaSer.ObtenerListaCategoria();

            return Json(new { data = lstCategoria }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Método para agregar una categoría
        /// </summary>
        /// <param name="_objCategoria">almacena los valores una categoría</param>
        /// <returns>Retorna el msj de estado segun la acción</returns>
        public JsonResult AgregarCategoria()
        {
            tblCat_Categoria _objCategoria;
            try
            {
                _objCategoria = JsonConvert.DeserializeObject<tblCat_Categoria>(Request["Categoria"]);
                categoriaSer.AgregarCategoria(_objCategoria);

                cMensaje = "Se agregó una categoría correctamente.";
                cEstatus = "¡Guardado!";

            }
            catch (Exception e)
            {
                cMensaje = e.Message;
                cEstatus = "Error";
            }

            return Json(new { Mensaje = cMensaje, Estatus = cEstatus }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditarCategoria()
        {
            tblCat_Categoria _objCategoria;
            try
            {
                _objCategoria = JsonConvert.DeserializeObject<tblCat_Categoria>(Request["Categoria"]);
                categoriaSer.EditarCategoria(_objCategoria);

                cMensaje = "Se modificó un categoría correctamente.";
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
        /// Método que permite cambiar el estatus de una categoría seleccionada
        /// </summary>
        /// <returns>Devuelve un mensaje de acuerdo al estatus de la acción</returns>
        [HttpPost]
        public JsonResult CambiarEstatusCategoria()
        {
            tblCat_Categoria _objCategoria;
            try
            {
                _objCategoria = JsonConvert.DeserializeObject<tblCat_Categoria>(Request["EstatusCat"]);
                categoriaSer.DesactivarCategoria(_objCategoria);

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