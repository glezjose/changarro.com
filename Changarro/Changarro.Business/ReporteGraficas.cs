using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Changarro.Model;
using Changarro.Model.DTO;


namespace Changarro.Business
{
    public class ReporteGraficas
    {
        private readonly CHANGARROEntities ctx;

        public ReporteGraficas()
        {
            ctx = new CHANGARROEntities();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
        }
        //public List<ReporteProductosDTO> ObtenerProductosMasVendidos()
        //{
         
            
        //}

        /// <summary>
        /// Obtiene los clientes con más compras realizadas.
        /// </summary>
        /// <returns>Devuelve la lista de los clientes.</returns>
        public List<ReporteUsuariosDTO> ObtenerUsuariosConMasCompras()
        {

            List<ReporteUsuariosDTO> _lstTopClientes = ctx.tblCat_Cliente.AsNoTracking().Select(c => new ReporteUsuariosDTO()
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
        /// Obtiene la cantidad de productos que hay en cada categoría.
        /// </summary>
        /// <returns>Devuelve la lista de los productos por cada categoría</returns>
        public List<ReporteCategoriasDTO> ObtenerProductosporCategoria()
        {


            List<ReporteCategoriasDTO> _lstProductosPorCategoria = (from p in ctx.tblCat_Producto
                                         join c in ctx.tblCat_Categoria on p.iIdCategoria equals c.iIdCategoria
                                         group new { c.cNombre, p.iIdProducto }
                                         by c.cNombre into informacion
                                         select new ReporteCategoriasDTO
                                         {
                                             cNombre = informacion.Key,
                                             iCantidad = informacion.Count()

                                         }).AsNoTracking().ToList();

            return _lstProductosPorCategoria;
        }
    }
}
