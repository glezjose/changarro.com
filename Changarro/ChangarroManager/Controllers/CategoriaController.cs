using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChangarroManager.Controllers
{
    public class CategoriaController : Controller
    {
        Categoria categoriaSer = new Categoria();

        private string cMensaje = string.Empty;
        private string cEstatus = string.Empty;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Categoria()
        {
            return View();
        }
        /// <summary>
        /// Método que devuelve una lista de las categorías de la tabla de categorías
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        public JsonResult AgregarCategoria(tblCat_Categoria _objCategoria)
        {
            try
            {
                categoriaSer.AgregarCategoria(_objCategoria);

                cMensaje = "Se agregó un calzado correctamente.";
                cEstatus = "¡Guardado!";

            }
            catch (Exception e)
            {
                cMensaje = e.Message;
                cEstatus = "Error";
            }

            return Json(new { Mensaje = cMensaje, Estatus = cEstatus }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditarCategoria(tblCat_Categoria _objCategoria)
        {
            return null;
        }
    }
}