using Changarro.Model;
using Changarro.Model.DTO;
using ChangarroBusiness;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
        public JsonResult AgregarProducto(tblCat_Producto _objProducto)
        {
            bool r = false;

            try
            {
                Productos ProductoBus = new Productos(ctx);

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
                Productos changarroBusiness = new Productos(ctx);
                tblCat_Producto oProducto = changarroBusiness.ObtenerProducto(id);
                return View(oProducto);
            }
        }

        public JsonResult Listar()
        {
            List<ListaProductosDTO> lstProducto = new List<ListaProductosDTO>();

            Productos ProductoSer = new Productos(ctx);
            lstProducto = ProductoSer.ObtenerListaProductos();

            return Json(new { data = lstProducto }, JsonRequestBehavior.AllowGet);
        }
    }
}