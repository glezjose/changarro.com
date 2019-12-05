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
        private readonly Carrito carrito = new Carrito();
        private readonly Productos producto = new Productos();

        public ActionResult Inicio()
        {
            int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));


            return View();
        }

        [HttpPost]
        public JsonResult AgregarProductoCarrito(int iIdProducto)
        {
            string cMensaje;
            string cIcono;
            try
            {
                if (producto.ChecarExistencia(iIdProducto))
                {
                int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

                carrito.AgregarAcarrito(iIdProducto, iIdCarrito);

                cMensaje = "Se ha agregado un producto al carrito.";
                cIcono = "success";
                }
                else
                {
                    cMensaje = "No hay existencia disponible para este producto.";
                    cIcono = "error";
                }
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
            
            int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

            ViewBag.iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

            return PartialView();
        }
    }
}