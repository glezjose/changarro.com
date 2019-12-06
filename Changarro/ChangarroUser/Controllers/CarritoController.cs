using Changarro.Business;
using Changarro.Model.DTO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChangarroUser.Controllers
{
    public class CarritoController : Controller
    {
        private readonly Carrito carrito = new Carrito();
        private readonly Productos producto = new Productos();

        public ActionResult Inicio()
        {
            if (Session["iIdCliente"] != null)
            {
                int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

                ViewBag.iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

                ViewBag.iSubTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito);

                ViewBag.iTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito) + 50;

                List<CarritoDTO> _lstProductos = carrito.ObtenerProductosCarrito(iIdCarrito);
                return View(_lstProductos);
            }
            else
            {
                return RedirectToAction("Inicio", "Producto");
            }
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
                    if (carrito.AgregarAcarrito(iIdProducto, iIdCarrito))
                    {
                        cMensaje = "Se ha agregado un producto al carrito.";
                        cIcono = "success";
                    }
                    else
                    {
                        cMensaje = "La existencia disponible se ha agregado al carrito.";
                        cIcono = "error";
                    }
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