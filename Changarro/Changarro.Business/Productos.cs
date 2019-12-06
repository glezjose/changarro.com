///////////////////////////////////////////////////////////
//  Productos.CHANGARROEntitiescs
//  Implementation of the Class Productos
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:22:17 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;
using System.Data.Entity;
using System;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace Changarro.Business
{

    public class Productos
    {
        private readonly CHANGARROEntities db;

        public Productos()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }


        ~Productos()
        {

        }
        public ProductosDTO AgregarProducto(tblCat_Producto _objProducto)
        {
            DbContextTransaction dbTran = db.Database.BeginTransaction();//investigar 
            try
            {
                db.tblCat_Producto.Add(_objProducto);

                db.SaveChanges();
                dbTran.Commit();
            }
            catch (Exception)
            {
                dbTran.Rollback();
                throw;
            }


            return null;
        }

        /// 
        /// <param name="iIdProducto"></param>
        public void DesactivarProducto(int iIdProducto)
        {

        }

        /// 
        /// <param name="iIdProducto"></param>
        public ProductosDTO EditarProducto(int iIdProducto)
        {

            return null;
        }

        public List<ProductosDTO> ObtenerProductos()
        {

            return null;
        }

        public List<ProductosDTO> ObtenerTopProductos()
        {

            return null;
        }
        /// <summary>
        /// M�todo que trae los datos de la tabla de productos de la base de datos
        /// </summary>
        /// <returns>regresa una lista con todos los registros de la tabla</returns>
        public List<ListaProductosDTO> ObtenerListaProductos()
        {
            List<ListaProductosDTO> lstProducto = (from producto in db.tblCat_Producto
                                                   join cat in db.tblCat_Categoria
                                                   on producto.iIdCategoria equals cat.iIdCategoria
                                                   select new ListaProductosDTO
                                                   {
                                                       iIdProducto = producto.iIdProducto,
                                                       cNombre = producto.cNombre,
                                                       NombreCate = cat.cNombre,
                                                       iCantidad = producto.iCantidad,
                                                       dtFechaAlta = producto.dtFechaAlta,
                                                       dtFechaModificacion = producto.dtFechaModificacion

                                                   }).ToList();

            return lstProducto;
        }
        /// <summary>
        /// M�todo que Obtiene los datos de un producto de acuerdo a su identificador para mostrar los detalles
        /// </summary>
        /// <param name="_iIdProducto">la id del producto a localizar </param>
        /// <returns>regresa un objeto con los datos del producto</returns>
        public tblCat_Producto ObtenerProducto(int _iIdProducto)
        {
            tblCat_Producto oProducto = db.tblCat_Producto.Where(p => p.iIdProducto == _iIdProducto).FirstOrDefault();
            return oProducto;
        }
        /// <summary>
        /// Obtiene una lista de los 6 productos mas recientes que han sido agregados a la BD.
        /// </summary>
        /// <returns>Regresa la lista de los 6 productos</returns>
        public List<CatalogoProductosDTO> ObtenerProductosRecientes()
        {
            List<CatalogoProductosDTO> _lstProductos = db.tblCat_Producto.AsNoTracking().Select(p => new CatalogoProductosDTO()
            {
                iIdProducto = p.iIdProducto,
                iIdCategoria = p.iIdCategoria,
                dPrecio = p.dPrecio,
                cNombre = p.cNombre,
                cImagen = p.cImagen,
                dtFechaAlta = (DateTime)p.dtFechaAlta
            }).OrderByDescending(p => p.dtFechaAlta).Take(6).ToList();
            return _lstProductos;

        }

        /// <summary>
        /// Este m�todo obtiene los productos relacionados a la categor�a seleccionada.
        /// </summary>
        /// <param name="iIdCategoria">La id de la categor�a seleccionada.</param>
        /// <returns>Regresa un listado de los productos relacionados.</returns>
        public List<CatalogoProductosDTO> ObtenerProductosPorCategoria(int iIdCategoria)
        {
            List<CatalogoProductosDTO> _lstProductos = db.tblCat_Producto.AsNoTracking().Select(p => new CatalogoProductosDTO()
            {
                iIdProducto = p.iIdProducto,
                iIdCategoria = p.iIdCategoria,
                dPrecio = p.dPrecio,
                cNombre = p.cNombre,
                cImagen = p.cImagen,
                dtFechaAlta = (DateTime)p.dtFechaAlta
            }).Where(p => p.iIdCategoria == iIdCategoria).ToList();

            return _lstProductos;
        }

        public List<CatalogoProductosDTO> ObtenerProductosPorBusqueda(string cBusqueda)
        {
            List<CatalogoProductosDTO> _lstProductos = db.tblCat_Producto.AsNoTracking().Select(p => new CatalogoProductosDTO()
            {
                iIdProducto = p.iIdProducto,
                iIdCategoria = p.iIdCategoria,
                dPrecio = p.dPrecio,
                cNombre = p.cNombre,
                cImagen = p.cImagen,
                dtFechaAlta = (DateTime)p.dtFechaAlta
            }).Where(p => p.cNombre.Contains(cBusqueda)).ToList();

            return _lstProductos;
        }

        /// <summary>
        /// Obtiene un objeto del producto seleccionado para ver detalles.
        /// </summary>
        /// <param name="iIdProducto">La id del producto seleccionado.</param>
        /// <returns>Regresa el objeto encontrado con la id.</returns>
        public DetallesProductoDTO ObtenerDetallesProducto(int iIdProducto)
        {
            DetallesProductoDTO _oProducto = db.tblCat_Producto.AsNoTracking().Select(p => new DetallesProductoDTO()
            {
                iIdProducto = p.iIdProducto,
                iIdCategoria = p.iIdCategoria,
                iCantidad = p.iCantidad,
                dPrecio = p.dPrecio,
                cNombre = p.cNombre,
                cImagen = p.cImagen,
                cDescripcion = p.cDescripcion
            }).FirstOrDefault(p => p.iIdProducto == iIdProducto);

            return _oProducto;
        }

        /// <summary>
        /// M�todo que crea la plantilla vac�a que el administrador podr� descargar para llenar y registrar varios productos
        /// </summary>
        public string GenerarPlantillaVacia()
        {
            string cHome = AppDomain.CurrentDomain.BaseDirectory;
            string cRutaPlantilla = cHome + "Plantillas\\PlantillaVacia\\Plantilla.xlsx";
            cRutaPlantilla = cRutaPlantilla.Normalize();
            List<string> lstEncabezados = new List<string>{
                "Nombre",
                "Descripci�n",
                "Precio",
                "Categor�a",
                "Estatus",
                "Existencia"
            };

            using (FileStream _oFileStream = new FileStream(cRutaPlantilla, FileMode.Create, FileAccess.Write))
            {
                IWorkbook oLibro = new XSSFWorkbook();
                ISheet oHoja = oLibro.CreateSheet("Plantilla");
                ICreationHelper oAyudanteCreacion = oLibro.GetCreationHelper();
                IRow oFilaEncabezados = oHoja.CreateRow(0);
                for (int i = 0; i < lstEncabezados.Count; i++)
                {
                    ICell oCelda = oFilaEncabezados.CreateCell(i);
                    oCelda.SetCellValue(lstEncabezados[i]);
                }
                oLibro.Write(_oFileStream);
            }
            return cRutaPlantilla;
        }//end Generar Plantilla Vacia

        public bool ChecarExistencia(int iIdProducto)
        {

            int iCantidad = db.tblCat_Producto.AsNoTracking().FirstOrDefault(p => p.iIdProducto == iIdProducto).iCantidad;

            return (iCantidad == 0) ? false : true;
        }
    }//end Productos

}//end namespace ChangarroBusiness