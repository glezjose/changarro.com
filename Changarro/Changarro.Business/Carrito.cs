using Changarro.Model;
using Changarro.Model.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Changarro.Business
{
    public class Carrito
    {
        /// <summary>
        /// El contexto de la BD.
        /// </summary>
        CHANGARROEntities db;

        /// <summary>
        /// Constructor donde se instancia el contexto del modelo.
        /// </summary>
        public Carrito()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        ~Carrito()
        {

        }

        /// <summary>
        /// Este m�todo es para registrar un carrito nuevo al registrar un cliente nuevo.
        /// </summary>
        /// <param name="iIdCliente">La id del cliente que se registro.</param>
        public void RegistrarCarrito(int iIdCliente)
        {
            tblCat_Carrito _oCarrito = new tblCat_Carrito()
            {
                iIdCliente = iIdCliente
            };

            db.tblCat_Carrito.Add(_oCarrito);
            db.SaveChanges();

        }

        /// <summary>
        /// En este m�todo se agrega un producto al carrito, validando si ya existe el producto en el carrito.
        /// </summary>
        /// <param name="iIdProducto">Es la ID del producto que se desea agregar.</param>
        /// <param name="iIdCarrito">Es la ID del carrito donde se desea agregar el producto.</param>
        public bool AgregarAcarrito(int iIdProducto, int iIdCarrito)
        {
            bool lAgregar;
            if (db.tbl_DetalleCarrito.AsNoTracking().Any(c => c.iIdCarrito == iIdCarrito && c.iIdProducto == iIdProducto))
            {
                tbl_DetalleCarrito _oDetalleCarrito = db.tbl_DetalleCarrito.AsNoTracking().FirstOrDefault(c => c.iIdCarrito == iIdCarrito && c.iIdProducto == iIdProducto);

                if (_oDetalleCarrito.iCantidad < db.tblCat_Producto.AsNoTracking().FirstOrDefault(p => p.iIdProducto == iIdProducto).iCantidad)
                {
                    _oDetalleCarrito.iCantidad++;

                    db.Entry(_oDetalleCarrito).State = EntityState.Modified;
                    db.SaveChanges();

                    lAgregar = true;
                }
                else lAgregar = false;
            }
            else
            {
                tbl_DetalleCarrito oDetalleCarrito = new tbl_DetalleCarrito()
                {
                    iIdCarrito = iIdCarrito,
                    iIdProducto = iIdProducto,
                    iCantidad = 1
                };

                db.tbl_DetalleCarrito.Add(oDetalleCarrito);
                db.SaveChanges();

                lAgregar = true;
            }

            return lAgregar;
        }

        /// <summary>
        /// Aumenta la cantidad del producto seleccionado.
        /// </summary>
        /// <param name="iIdProducto">Es la ID del producto que se agrega cantidad.</param>
        /// <param name="iIdCarrito">Es la ID del carrito de sesi�n.</param>
        public void AumentarCantidad(int iIdProducto, int iIdCarrito)
        {
            tbl_DetalleCarrito _oDetalleCarrito = db.tbl_DetalleCarrito.AsNoTracking().FirstOrDefault(c => c.iIdCarrito == iIdCarrito && c.iIdProducto == iIdProducto);

            _oDetalleCarrito.iCantidad++;

            db.Entry(_oDetalleCarrito).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Disminuye la cantidad del producto seleccionado.
        /// </summary>
        /// <param name="iIdProducto"></param>
        /// <param name="iIdCarrito"></param>
        public void DisminuirCantidad(int iIdProducto, int iIdCarrito)
        {
            tbl_DetalleCarrito _oDetalleCarrito = db.tbl_DetalleCarrito.AsNoTracking().FirstOrDefault(c => c.iIdCarrito == iIdCarrito && c.iIdProducto == iIdProducto);

            _oDetalleCarrito.iCantidad--;

            db.Entry(_oDetalleCarrito).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Este m�todo es para obtener el total de productos que existen el el carrito especificado.
        /// </summary>
        /// <param name="iIdCarrito">Este ID es del carrito que se desea obtener el total de productos.</param>
        /// <returns>Regresa el total de productos del carrito.</returns>
        public int ObtenerTotalProductos(int iIdCarrito)
        {
            int iTotalProductos = db.tbl_DetalleCarrito.AsNoTracking().Any(p => p.iIdCarrito == iIdCarrito) ? db.tbl_DetalleCarrito.AsNoTracking().Where(p => p.iIdCarrito == iIdCarrito).Sum(p => p.iCantidad) : 0;

            return iTotalProductos;
        }

        /// <summary>
        /// En este m�todo se obtiene el total
        /// </summary>
        /// <param name="iIdCarrito"></param>
        /// <returns></returns>
        public decimal ObtenerTotalPrecio(int iIdCarrito)
        {
            decimal dTotalPrecio = db.tbl_DetalleCarrito.AsNoTracking().Any(p => p.iIdCarrito == iIdCarrito) ? db.tbl_DetalleCarrito.AsNoTracking().Where(p => p.iIdCarrito == iIdCarrito).Sum(p => p.tblCat_Producto.dPrecio * p.iCantidad) : 0;

            return dTotalPrecio;
        }
      
        /// <summary>
        /// M�todo que elimina el producto seleccionado en el carrito.
        /// </summary>
        /// <param name="iIdProducto">La ID el producto que se eliminara del carrito.</param>
        /// <param name="iIdCarrito">La ID del carrito de la sesi�n.</param>
        public void EliminarProducto(int iIdProducto, int iIdCarrito)
        {
            tbl_DetalleCarrito _oDetalleCarrito = db.tbl_DetalleCarrito.AsNoTracking().FirstOrDefault(c => c.iIdCarrito == iIdCarrito && c.iIdProducto == iIdProducto);

            db.Entry(_oDetalleCarrito).State = EntityState.Deleted;
            db.SaveChanges();
        }

        /// <summary>
        /// M�todo que sirve para obtener la iIdCarrito del cliente con la sesi�n iniciada.
        /// </summary>
        /// <param name="iIdCliente">La ID del cliente con la sesi�n iniciada.</param>
        /// <returns>Regresa la ID del cliente.</returns>
        public int ObtenerCarrito(int iIdCliente)
        {

            int iIdCarrito = db.tblCat_Carrito.AsNoTracking().FirstOrDefault(c => c.iIdCliente == iIdCliente).iIdCarrito;

            return iIdCarrito;
        }

        /// <summary>
        /// En este m�todo se obtiene los productos que existen en el carrito.
        /// </summary>
        /// <param name="iIdCarrito">Es la ID del carrito de la sesi�n.</param>
        /// <returns>Regresa la lista de productos encontrados en el carrito.</returns>
        public List<CarritoDTO> ObtenerProductosCarrito(int iIdCarrito)
        {
            List<CarritoDTO> _lstProductos = db.tbl_DetalleCarrito.AsNoTracking().Where(c => c.iIdCarrito == iIdCarrito).Select(c => new CarritoDTO()
            {
                iIdProducto = c.iIdProducto,
                iIdCategoria = c.tblCat_Producto.iIdCategoria,
                iCantidad = c.iCantidad,
                iCantidadExistente = c.tblCat_Producto.iCantidad,
                dPrecio = c.tblCat_Producto.dPrecio,
                cNombre = c.tblCat_Producto.cNombre,
                cImagen = c.tblCat_Producto.cImagen,
                cDescripcion = c.tblCat_Producto.cDescripcion
            }).ToList();

            return _lstProductos;
        }

        /// <summary>
        /// Esta funci�n elimina los elementos del carrito
        /// </summary>
        /// <param name="iIdCarrito"> ID del carrito</param>
        public void VaciarCarrito(int iIdCarrito)
        {
            List<tbl_DetalleCarrito> _lstProductos = db.tbl_DetalleCarrito.AsNoTracking().Where(c => c.iIdCarrito == iIdCarrito).ToList();

            foreach (var _producto in _lstProductos)
            {
                db.Entry(_producto).State = EntityState.Deleted;
            }
            db.SaveChanges();
        }

    }

}