using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Changarro.Model;
using Changarro.Model.DTO;


namespace Changarro.Business
{
    public class ReporteGraficas
    {
        private readonly CHANGARROEntities ctx;
        /// <summary>
        /// Constructor para inicializar el contexto y las configuraciones.
        /// </summary>
        public ReporteGraficas()
        {
            ctx = new CHANGARROEntities(); 
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
        }
        /// <summary>
        /// Método que obtiene los productos más vendidos.
        /// </summary>
        /// <returns>Devuelve la lista de la cantidad de productos.</returns>
        public List<ReporteProductosDTO> ObtenerProductosMasVendidos()
        {
            List<ReporteProductosDTO> _lstTopProductos = ctx.tbl_DetalleCompra.AsNoTracking()
                .GroupBy(p => new { p.iIdProducto, p.tblCat_Producto.cNombre })
                .Select(p => new ReporteProductosDTO
                {
                    iIdProducto = p.Key.iIdProducto,
                    cNombre = p.Key.cNombre,
                    iCantidad = p.Sum(c => c.iCantidad)
                }).OrderByDescending(p => p.iCantidad).Take(10).ToList();

            return _lstTopProductos;

        }

        /// <summary>
        /// Método que obtiene los clientes con más compras.
        /// </summary>
        /// <returns>Devuelve la lista de los clientes.</returns>
        public List<ReporteUsuariosDTO> ObtenerUsuariosConMasCompras()
        {

            List<ReporteUsuariosDTO> _lstTopClientes = ctx.tblCat_Cliente.AsNoTracking()
                .Select(c => new ReporteUsuariosDTO()
                {
                    iIdCliente = c.iIdCliente,
                    cNombre = c.cNombre,
                    cApellido = c.cApellido,
                    cImagen = c.cImagen,
                    iTotalCompras = c.tblCat_Compra.Where(t => t.lEstatus == true).Count()
                }).OrderByDescending(c => c.iTotalCompras).Take(10).ToList();

            return _lstTopClientes;
        }


        /// <summary>
        /// Método que obtiene la cantidad de productos que hay en cada categoría.
        /// </summary>
        /// <returns>Devuelve la lista de los productos por cada categoría.</returns>
        public List<ReporteCategoriasDTO> ObtenerProductosporCategoria()
        {


            List<ReporteCategoriasDTO> _lstProductosPorCategoria = ctx.tblCat_Producto.AsNoTracking()
                .Where(e => e.tblCat_Categoria.lEstatus == true)
                .GroupBy(p => new { p.iIdCategoria, p.tblCat_Categoria.cNombre })
                .Select(p => new ReporteCategoriasDTO
                {
                    cNombre = p.Key.cNombre,
                    iCantidad = p.Count()
                }).ToList();


            return _lstProductosPorCategoria;
        }
    }
}
