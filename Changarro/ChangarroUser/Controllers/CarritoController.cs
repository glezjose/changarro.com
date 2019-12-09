using Changarro.Business;
using Changarro.Model.DTO;
using ChangarroUser.Filters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChangarroUser.Controllers
{
    public class CarritoController : Controller
    {
        private readonly Carrito carrito = new Carrito();
        private readonly Productos producto = new Productos();

        /// <summary>
        /// Vista general del carrito del cliente con sesión iniciada.
        /// </summary>
        /// <returns>Regresa la vista con el modelo que son los productos que el cliente ha agregado al carrito.</returns>
        public ActionResult Inicio()
        {
            if (Session["iIdCliente"] != null)
            {
                int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

                ViewBag.iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

                ViewBag.dSubTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito);

                ViewBag.dTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito) + 50;

                List<CarritoDTO> _lstProductos = carrito.ObtenerProductosCarrito(iIdCarrito);
                return View(_lstProductos);
            }
            else
            {
                TempData["lConexion"] = true;

                return RedirectToAction("Inicio", "Producto");
            }
        }

        /// <summary>
        /// Agrega un producto al carrito o checa si ya existe y simplemente agrega una cantidad de mas.
        /// </summary>
        /// <param name="iIdProducto">Es la id del producto que se desea agregar al carrito.</param>
        /// <returns>Regresa los mensajes que se usan para alertar el suceso.</returns>
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

        [HttpPost]
        public JsonResult AumentarCantidad(int iIdProducto)
        {
            int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

            carrito.AumentarCantidad(iIdProducto, iIdCarrito);

            decimal dSubTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito);

            decimal dTotalPrecio = dSubTotalPrecio + 50;

            return Json(new { dSubTotalPrecio, dTotalPrecio});
        }

        [HttpPost]
        public JsonResult DisminuirCantidad(int iIdProducto)
        {
            int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

            carrito.DisminuirCantidad(iIdProducto, iIdCarrito);

            decimal dSubTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito);

            decimal dTotalPrecio = dSubTotalPrecio + 50;

            return Json(new { dSubTotalPrecio, dTotalPrecio });
        }

        [HttpPost]
        public JsonResult EliminarProducto(int iIdProducto)
        {
            int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

            carrito.EliminarProducto(iIdProducto, iIdCarrito);

            decimal dSubTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito);

            decimal dTotalPrecio = dSubTotalPrecio + 50;

            int iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

            return Json(new { dSubTotalPrecio, dTotalPrecio, iTotalProductos });
        }

        [ChildActionOnly]
        public ActionResult ProductosCarrito()
        {

            int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

            ViewBag.iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

            return PartialView();
        }

        [AjaxChildActionOnly]
        public ActionResult CarritoVacio()
        {
            return PartialView();
        }
    }
}