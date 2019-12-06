﻿using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
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

        [HttpGet]
        public ActionResult DescargarPlantilla()
        {
            Productos oLogicaProducto = new Productos();
            string cRutaPlantilla = oLogicaProducto.GenerarPlantillaVacia();
            byte[] datosBinariosPlantilla = System.IO.File.ReadAllBytes(cRutaPlantilla);
            return File(datosBinariosPlantilla, System.Net.Mime.MediaTypeNames.Application.Octet, "Plantilla.xlsx");
        }
    }
}