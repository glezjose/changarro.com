using Changarro.Business;
using Changarro.Model;
using Changarro.Model.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;

namespace ChangarroUser.Controllers
{
    public class CompraController : Controller
    {
        private readonly Carrito carrito = new Carrito();
        private readonly Compra compra = new Compra();
        private readonly Domicilio domicilio = new Domicilio();
        private readonly Tarjeta tarjeta = new Tarjeta();
        private readonly CorreoPDF generar = new CorreoPDF();

        #region Vistas

        /// <summary>
        /// Es el método inicial de compra, donde se valida si es una compra directa o una compra de carrito.
        /// </summary>
        /// <returns>Regresa la vista con el modelo de la compra.</returns>
        [HttpGet]
        public ActionResult Inicio()
        {
            if (Session["iIdCliente"] != null)
            {
                if (TempData["lValidarCompra"] != null)
                {
                    if (TempData["iIdProductoCompra"] == null)
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

                        int iCantidad = Convert.ToInt32(Request["iCantidad"]);

                        ProductosCompraDTO _oProducto = compra.ObtenerProductoCompraDirecta(Convert.ToInt32(TempData["iIdProductoCompra"]), iCantidad);

                        TempData["oProductoUnico"] = _oProducto;

                        ViewBag.iTotalProductos = iCantidad;

                        ViewBag.dSubTotalPrecio = _oProducto.dPrecioTotal;

                        ViewBag.dTotalPrecio = _oProducto.dPrecioTotal + 50;

                        ViewBag.lValidarCompraDirecta = true;

                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Inicio", "Producto");
                }
            }
            else
            {
                TempData["lConexion"] = true;

                return RedirectToAction("Inicio", "Producto");
            }
        }

        /// <summary>
        /// Es la vista de seleccionar la cantidad del producto en compra directa.
        /// </summary>
        /// <returns>Regresa la vista con el modelo siendo el producto que se comprara.</returns>
        [HttpGet]
        public ActionResult SeleccionarCantidad()
        {
            if (Session["iIdCliente"] != null)
            {
                if (TempData["lValidarCompra"] != null)
                {
                    CarritoDTO _oProducto = compra.ObtenerProductoCompraDirecta(Convert.ToInt32(TempData["iIdProductoCompra"]));

                    ViewBag.dTotalPrecio = _oProducto.dPrecio + 50;

                    TempData.Keep("lValidarCompra");
                    TempData.Keep("iIdProductoCompra");

                    return View(_oProducto);
                }
                else
                {
                    return RedirectToAction("Inicio", "Producto");
                }
            }
            else
            {
                TempData["lConexion"] = true;

                return RedirectToAction("Inicio", "Producto");
            }
        }

        /// <summary>
        /// Es una vista parcial que carga el producto único a comprar en compra directa.
        /// </summary>
        /// <returns>Regresa la vista con el modelo siendo el producto.</returns>
        [ChildActionOnly]
        public ActionResult CargarProductoUnico()
        {
            ProductosCompraDTO _oProducto = TempData["oProductoUnico"] as ProductosCompraDTO;

            TempData.Keep("oProductoUnico");

            TempData["lValidarCompraUnica"] = true;

            return PartialView(_oProducto);
        }

        /// <summary>
        /// Es la vista parcial donde se selecciona el domicilio.
        /// </summary>
        /// <returns>Regresa la vista parcial con el modelo siendo la lista de domicilios.</returns>
        [ChildActionOnly]
        public ActionResult SeleccionDomicilio()
        {
            List<DomicilioCompraDTO> _lstDomicilios = domicilio.ObtenerDomiciliosCompra(Convert.ToInt32(Session["iIdCliente"]));

            return PartialView(_lstDomicilios);
        }

        /// <summary>
        /// Es la vista parcial donde se selecciona la tarjeta.
        /// </summary>
        /// <returns>Regresa la vista parcial con el modelo siendo la lista de tarjetas.</returns>
        [ChildActionOnly]
        public ActionResult SeleccionTarjeta()
        {
            List<TarjetaCompraDTO> _lstTarjetas = tarjeta.ObtenerTarjetasCompra(Convert.ToInt32(Session["iIdCliente"]));

            return PartialView(_lstTarjetas);
        }

        /// <summary>
        /// Es la vista de términos y condiciones.
        /// </summary>
        /// <returns>Regresa tal vista.</returns>
        [HttpPost]
        public ActionResult TerminosYcondiciones()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilaDomicilio(int iIdDomicilio)
        {
            DomicilioCompraDTO _oDomicilio = domicilio.ObtenerDomicilioCompra(iIdDomicilio);

            return PartialView(_oDomicilio);
        }

        public JsonResult AgregarDomicilio()
        {

            Domicilio oDomicilio = new Domicilio();

            DomicilioDTO _oDomicilio = JsonConvert.DeserializeObject<DomicilioDTO>(Request["oDomicilio"]);

            _oDomicilio.iIdCliente = Convert.ToInt32(Session["iIdCliente"]);
            int iIdDomicilio =  oDomicilio.AgregarNuevoDomicilio(_oDomicilio);


            return Json(new { iIdDomicilio });
        }
        #endregion

        #region Hacer compra

        /// <summary>
        /// Es el método que ejecuta la compra sea única o por carrito.
        /// </summary>
        /// <returns>Regresa un json con mensajes de validación.</returns>
        [HttpPost]
        public JsonResult RealizarCompra()
        {
            string cMensaje;
            string cIcono;

            try
            {
                Nullable<bool> lValidarCompraUnica = TempData["lValidarCompraUnica"] as Nullable<bool>;
                if (lValidarCompraUnica != true)
                {
                    string _cCompra = Request["oCompra"];
                    tblCat_Compra _oCompra = JsonConvert.DeserializeObject<tblCat_Compra>(_cCompra);

                    int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

                    int iIdCompra = compra.AgregarCompra(iIdCliente, _oCompra);

                    int iIdCarrito = carrito.ObtenerCarrito(Convert.ToInt32(Session["iIdCliente"]));

                    compra.RealizarCompraCarrito(iIdCarrito, iIdCompra);

                    carrito.VaciarCarrito(iIdCarrito);

                    MailMessage _mmMensaje = generar.GenerarPDF(iIdCliente, iIdCompra);

                    generar.EnviarCorreo(_mmMensaje);

                    cMensaje = "Se ha realizado la compra!";
                    cIcono = "success";
                }
                else if (lValidarCompraUnica == true)
                {
                    string _cCompra = Request["oCompra"];
                    tblCat_Compra _oCompra = JsonConvert.DeserializeObject<tblCat_Compra>(_cCompra);

                    int iIdCliente = Convert.ToInt32(Session["iIdCliente"]);

                    int iIdCompra = compra.AgregarCompra(iIdCliente, _oCompra);

                    ProductosCompraDTO _oProducto = TempData["oProductoUnico"] as ProductosCompraDTO;

                    compra.RealizarCompraDirecta(_oProducto, iIdCompra);

                    MailMessage _mmMensaje = generar.GenerarPDF(iIdCliente, iIdCompra);

                    generar.EnviarCorreo(_mmMensaje);

                    cMensaje = "Se ha realizado la compra!";
                    cIcono = "success";
                }
                else
                {
                    cMensaje = "Ha ocurrido un error";
                    cIcono = "error";
                }
            }
            catch (Exception e)
            {
                cMensaje = e.Message;
                cIcono = "error";

            }

            return Json(new { cMensaje, cIcono });
        }

        #endregion
    }
}