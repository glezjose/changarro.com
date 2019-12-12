using Changarro.Model;
using Changarro.Model.DTO;
using Changarro.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ChangarroUser.Controllers
{
    public class ProductoController : Controller
    {
        /// <summary>
        /// Esta vista es la vista principal de la pagina y contiene un apartado de los 6 productos recientes.
        /// </summary>
        /// <returns>Regresa la vista con los seis productos recientes.</returns>
        [HttpGet]
        public ActionResult Inicio()
        {
            Productos productos = new Productos();

            List<CatalogoProductosDTO> _lstProductos = productos.ObtenerProductosRecientes();

            if (TempData["lConexion"] != null)
            {
                ViewBag.lConexion = TempData["lConexion"];
            }

            return View(_lstProductos);
        }

        #region Pagina de productos

        /// <summary>
        /// Esta vista se carga en un modal con los detalles del producto seleccionado.
        /// </summary>
        /// <param name="iIdProducto">La id del producto que se desea ver</param>
        /// <returns>Regresa un objeto del producto como modelo para la vista.</returns>
        [HttpPost]
        public ActionResult VerDetalles(int iIdProducto)
        {
            Productos productos = new Productos();
            Categoria categoria = new Categoria();

            DetallesProductoDTO _oProducto = productos.ObtenerDetallesProducto(iIdProducto);

            ViewBag.NombreCategoria = categoria.ObtenerNombreCategoria(_oProducto.iIdCategoria);

            return View(_oProducto);
        }

        /// <summary>
        /// En esta acción se crea una vista parcial de las categorías en la BD que se renderiza en el sistema.
        /// </summary>
        /// <returns>Regresa la vista parcial con el modelo que es un listado de las categorías.</returns>
        [ChildActionOnly]
        public ActionResult MenuCategorias()
        {
            Categoria categoria = new Categoria();

            List<tblCat_Categoria> _lstCategoria = categoria.ObtenerCategorias();

            return PartialView(_lstCategoria);
        }

        /// <summary>
        /// Esta acción es para visualizar los productos de la categoría seleccionada.
        /// </summary>
        /// <param name="iIdCategoria"></param>
        /// <returns>Regresa la vista con el modelo que es un listado de los productos relacionados a la categoría.</returns>
        [HttpGet]
        public ActionResult VerProductosPorCategoria(int iIdCategoria, int? iPagina)
        {
            ViewBag.iIdCategoria = iIdCategoria;

            int _iIndicePagina = iPagina.HasValue ? Convert.ToInt32(iPagina) : 1;

            Productos productos = new Productos();
            Categoria categoria = new Categoria();

            ViewBag.cNombreCategoria = categoria.ObtenerNombreCategoria(iIdCategoria);

            List<CatalogoProductosDTO> _lstProductos = productos.ObtenerProductosPorCategoria(iIdCategoria);

            IPagedList<CatalogoProductosDTO> _pagedlstProductos = _lstProductos.ToPagedList(_iIndicePagina, 1);

            return View(_pagedlstProductos);
        }

        /// <summary>
        /// Esta acción se crea una vista parcial que renderiza las categorías en una columna de la vista de productos
        /// </summary>
        /// <returns>Regresa la vista parcial con el modelo que es un listado de las categorías</returns>
        [ChildActionOnly]
        public ActionResult ColumnaCategorias()
        {
            Categoria categoria = new Categoria();

            List<tblCat_Categoria> _lstCategoria = categoria.ObtenerCategorias();

            return PartialView(_lstCategoria);
        }

        /// <summary>
        /// Esta acción es una vista que visualiza los productos por la búsqueda ingresada.
        /// </summary>
        /// <param name="cBusqueda">Es la cadena de la búsqueda ingresada.</param>
        /// <returns>Regresa tal vista con el modelo que es un listado de productos relacionados a la cadena de búsqueda.</returns>
        [HttpGet]
        public ActionResult VerProductosPorBusqueda(string cBusqueda, int? iPagina)
        {
            ViewBag.cBusqueda = cBusqueda;

            int _iIndicePagina = iPagina.HasValue ? Convert.ToInt32(iPagina) : 1;

            Productos _productos = new Productos();

            List<CatalogoProductosDTO> _lstProductos = _productos.ObtenerProductosPorBusqueda(cBusqueda);

            if (_lstProductos.Count() == 0)
            {
                ViewBag.cMensaje = "No existe productos relacionados con la búsqueda " + '"' + cBusqueda + '"';
            }
            else
            {
                ViewBag.cMensaje = "";
            }

            IPagedList<CatalogoProductosDTO> _pagedlstProductos = _lstProductos.ToPagedList(_iIndicePagina, 1);

            return View(_pagedlstProductos);
        }

        [HttpGet]
        public ActionResult DirigirAcompraAhora(int iIdProducto)
        {
            TempData["lValidarCompra"] = true;

            TempData["iIdProductoCompra"] = iIdProducto;

            return RedirectToAction("SeleccionarCantidad", "Compra");
        }

        #endregion
    }
}