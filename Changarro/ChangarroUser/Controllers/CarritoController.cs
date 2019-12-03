using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangarroUser.Controllers
{
    public class CarritoController : Controller
    {
        readonly Carrito carrito = new Carrito();

        [HttpPost]
        public JsonResult AgregarProductoCarrito(int iIdProducto)
        {
            string cMensaje;
            string cIcono;
            try
            {
                int iIdCarrito = 1; //[Session"carrito"]

                carrito.AgregarAcarrito(iIdProducto, iIdCarrito);

                cMensaje = "Se ha agregado un producto al carrito.";
                cIcono = "success";
            }
            catch (Exception)
            {
                cMensaje = "Ha ocurrido un error";
                cIcono = "error";
            }
                return Json(new { cMensaje, cIcono });
        }

        [ChildActionOnly]
        public ActionResult ProductosCarrito()
        {
            int iIdCarrito = 1;//[Session"carrito"]

            ViewBag.iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

            return PartialView();
        }
    }
}