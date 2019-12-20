using Changarro.Model;
using Changarro.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Changarro.Business
{
    public class Compra
    {

        private readonly CHANGARROEntities db;

        public Compra()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        ~Compra()
        {

        }

        /// <summary>
        /// Este método agrega una compra a la BD.
        /// </summary>
        /// <param name="iIdCliente">La ID del cliente comprador.</param>
        /// <param name="iIdTarjeta">La ID de la tarjeta con la que se realizará la compra.</param>
        /// <param name="iIdDireccion">La ID de la dirección donde se enviará la compra.</param>
        /// <returns>Regresa la ID de la compra registrada.</returns>
        public int AgregarCompra(int iIdCliente, tblCat_Compra oCompra)
        {
            oCompra.iIdCliente = iIdCliente;
            oCompra.dtFechaCompra = DateTime.Now;
            oCompra.lEstatus = true;

            db.tblCat_Compra.Add(oCompra);
            db.SaveChanges();

            return oCompra.iIdCompra;
        }

        /// <summary>
        /// Método para obtener los datos de la compra
        /// </summary>
        /// <param name="iIdCompra">ID de la compra</param>
        /// <returns>Lista con los detalles de la compra</returns>
        public List<CatalogoProductosDTO> ObtenerCompra(int iIdCompra)
        {
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = true;
                ctx.Configuration.ProxyCreationEnabled = true;

                List<CatalogoProductosDTO> _lstCompras = ctx.tbl_DetalleCompra.Where(c => c.iIdCompra == iIdCompra).Select(p => new CatalogoProductosDTO
                {
                    iCantidad = p.iCantidad,
                    dPrecio = p.tblCat_Producto.dPrecio * p.iCantidad,
                    cNombre = p.tblCat_Producto.cNombre
                }).ToList();

                return _lstCompras;
            }

        }

        /// <summary>
        /// Método para realizar la compra de un producto
        /// </summary>
        /// <param name="oProducto">Objeto con los datos del producto</param>
        /// <param name="iIdCompra">ID de la compra</param>
        public void RealizarCompraDirecta(ProductosCompraDTO oProducto, int iIdCompra)
        {
            tbl_DetalleCompra _oDetalleCompra = new tbl_DetalleCompra()
            {
                iIdCompra = iIdCompra,
                iIdProducto = oProducto.iIdProducto,
                iCantidad = oProducto.iCantidadSeleccion
            };

            db.Entry(_oDetalleCompra).State = EntityState.Added;

            tblCat_Producto _oProducto = db.tblCat_Producto.AsNoTracking().FirstOrDefault(p => p.iIdProducto == _oDetalleCompra.iIdProducto);
            _oProducto.iCantidad -= _oDetalleCompra.iCantidad;

            db.Entry(_oProducto).State = EntityState.Modified;

            db.SaveChanges();
        }

        /// <summary>
        /// Método para realizar la compra desde el carrito
        /// </summary>
        /// <param name="iIdCarrito">ID del carrito</param>
        /// <param name="iIdCompra">ID de la compra</param>
        public void RealizarCompraCarrito(int iIdCarrito, int iIdCompra)
        {
            List<ProductosCompraDTO> _lstProductos = ObtenerProductosCompra(iIdCarrito);

            foreach (var _producto in _lstProductos)
            {
                tbl_DetalleCompra _oDetalleCompra = new tbl_DetalleCompra();

                _oDetalleCompra.iIdCompra = iIdCompra;
                _oDetalleCompra.iIdProducto = _producto.iIdProducto;
                _oDetalleCompra.iCantidad = _producto.iCantidadSeleccion;

                db.Entry(_oDetalleCompra).State = EntityState.Added;

                tblCat_Producto _oProducto = db.tblCat_Producto.AsNoTracking().FirstOrDefault(p => p.iIdProducto == _producto.iIdProducto);
                _oProducto.iCantidad -= _producto.iCantidadSeleccion;

                db.Entry(_oProducto).State = EntityState.Modified;
            }
            db.SaveChanges();

        }

        /// <summary>
        /// Método para obtener los datos del producto al comprarlo directamente para el carrito
        /// </summary>
        /// <param name="iIdProducto">ID del producto</param>
        /// <returns>Objeto con los datos del producto</returns>
        public CarritoDTO ObtenerProductoCompraDirecta(int iIdProducto)
        {
            CarritoDTO _oProducto = db.tblCat_Producto.AsNoTracking().Where(p => p.iIdProducto == iIdProducto).Select(p => new CarritoDTO
            {
                iIdProducto = p.iIdProducto,
                iIdCategoria = p.iIdCategoria,
                iCantidad = 1,
                iCantidadExistente = p.iCantidad,
                dPrecio = p.dPrecio,
                cNombre = p.cNombre,
                cImagen = p.cImagen,
                cDescripcion = p.cDescripcion
            }).FirstOrDefault();

            return _oProducto;

        }

        /// <summary>
        /// Método para obtener el producto desde la compra directa
        /// </summary>
        /// <param name="iIdProducto"></param>
        /// <param name="iCantidad"></param>
        /// <returns></returns>
        public ProductosCompraDTO ObtenerProductoCompraDirecta(int iIdProducto, int iCantidad)
        {
            ProductosCompraDTO _oProducto = db.tblCat_Producto.AsNoTracking().Where(p => p.iIdProducto == iIdProducto).Select(p => new ProductosCompraDTO
            {
                iIdProducto = p.iIdProducto,
                iCantidadSeleccion = iCantidad,
                dPrecioTotal = (iCantidad * p.dPrecio),
                cNombre = p.cNombre,
                cImagen = p.cImagen
            }).FirstOrDefault();

            return _oProducto;
        }

        /// <summary>
        /// Método para obtener los productos de una compra
        /// </summary>
        /// <param name="iIdCarrito">ID del carrito</param>
        /// <returns>Lista con los datos de los productos</returns>
        public List<ProductosCompraDTO> ObtenerProductosCompra(int iIdCarrito)
        {
            List<ProductosCompraDTO> _lstProductos = db.tbl_DetalleCarrito.AsNoTracking().Where(p => p.iIdCarrito == iIdCarrito).Select(p => new ProductosCompraDTO
            {
                iIdProducto = p.iIdProducto,
                iCantidadSeleccion = p.iCantidad,
                dPrecioTotal = (p.iCantidad * p.tblCat_Producto.dPrecio),
                cNombre = p.tblCat_Producto.cNombre,
                cImagen = p.tblCat_Producto.cImagen
            }).ToList();

            return _lstProductos;
        }

        /// <summary>
        /// Método para obtener los datos de la compra
        /// </summary>
        /// <param name="iIdCompra">ID de la compra</param>
        /// <returns>Objeto con los datos de la compra</returns>
        public DatosClienteCompraDTO ObtenerInfoCompra(int iIdCompra)
        {
            DatosClienteCompraDTO _oDatos = db.tblCat_Compra.AsNoTracking().Where(c => c.iIdCompra == iIdCompra).Select(c => new DatosClienteCompraDTO
            {
                cNombre = c.tblCat_Cliente.cNombre,
                cDomicilio = c.tblCat_Direccion.cCalle + " " + c.tblCat_Direccion.cNumeroExterior + " " + c.tblCat_Direccion.cColonia + ", " + c.tblCat_Direccion.cMunicipio + ", " + c.tblCat_Direccion.tbl_Estado.cNombre,
                cCorreo = c.tblCat_Cliente.cCorreo,
                dtFecha = (DateTime)c.dtFechaCompra
            }).FirstOrDefault();

            return _oDatos;
        }
    }

}