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
        CHANGARROEntities db = new CHANGARROEntities();
        // GET: Categoria
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

            Categoria categoriaSer = new Categoria();
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
            bool r = false;

            try
            {
                Categoria CategoriaService = new Categoria();

                tblCat_Categoria obp = new tblCat_Categoria
                {
                    cNombre = _objCategoria.cNombre
                };

                CategoriaService.AgregarCategoria(obp);

                r = true;

            }
            catch (Exception)
            {
                r = false;
            }

            return Json(new { result = r }, JsonRequestBehavior.AllowGet);
        }
    }
}