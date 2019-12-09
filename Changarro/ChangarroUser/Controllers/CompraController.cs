using Changarro.Business;
using Changarro.Model.DTO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChangarroUser.Controllers
{
    public class CompraController : Controller
    {
        private readonly Carrito carrito = new Carrito();
        private readonly Compra compra = new Compra();
        // GET: Compra
        public ActionResult Inicio()
        {
            if(Session["iIdCliente"] != null)
            {
                int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

                ViewBag.iTotalProductos = carrito.ObtenerTotalProductos(iIdCarrito);

                ViewBag.dSubTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito);

                ViewBag.dTotalPrecio = carrito.ObtenerTotalPrecio(iIdCarrito) + 50;

                List<ProductosCompraDTO> _lstProductos = compra.ObtenerProductosCompra(iIdCarrito);

                return View(_lstProductos);
            }
            else
            {
                TempData["lConexion"] = true;

                return RedirectToAction("Inicio", "Producto");
            }
        }

        [ChildActionOnly]
        public ActionResult SeleccionDomicilio()
        {

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SeleccionTarjeta()
        {
            return PartialView();
        }
    }
}