using System.Collections.Generic;
using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;
using System.Data.Entity;
using System;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

        /// <summary>
        /// M�todo para registrar productos
        /// </summary>
        /// <param name="_objProducto">Objeto con los datos del producto</param>
        public int AgregaProducto(tblCat_Producto _objProducto)
        {
            tblCat_Producto otblProducto = new tblCat_Producto
            {
                iIdCategoria = _objProducto.iIdCategoria,
                cNombre = _objProducto.cNombre,
                iCantidad = _objProducto.iCantidad,
                dPrecio = _objProducto.dPrecio,
                cDescripcion = _objProducto.cDescripcion,
                cImagen = "1576172857_ImagenProductoDef",
                lEstatus = true,
                dtFechaAlta = DateTime.Now,
                dfFechaBaja = null,
                dtFechaModificacion = null

            };
            db.tblCat_Producto.Add(otblProducto);

            db.SaveChanges();

            return otblProducto.iIdProducto;
        }

        /// <summary>
        /// M�todo para desactivar productos
        /// </summary>
        /// <param name="_objPrododucto">Objeto con los datos del producto</param>
        public void DesactivarProducto(tblCat_Producto _objPrododucto)
        {
            tblCat_Producto otblProducto = db.tblCat_Producto.FirstOrDefault(Cat => Cat.iIdProducto == _objPrododucto.iIdProducto);

            if (otblProducto.lEstatus == true)
            {
                otblProducto.lEstatus = false;
                otblProducto.dfFechaBaja = DateTime.Now;
            }
            else
            {
                otblProducto.lEstatus = true;
            }

            db.Entry(otblProducto).State = EntityState.Modified;

            db.SaveChanges();
        }

        /// <summary>
        /// M�todo para editar productos
        /// </summary>
        /// <param name="_objProducto">Objeto con los datos del producto</param>
        public void EditarProducto(tblCat_Producto _objProducto)
        {
            tblCat_Producto otblProducto = db.tblCat_Producto.FirstOrDefault(Cat => Cat.iIdProducto == _objProducto.iIdProducto);

            otblProducto.cNombre = _objProducto.cNombre;
            otblProducto.iIdCategoria = _objProducto.iIdCategoria;
            otblProducto.cNombre = _objProducto.cNombre;
            otblProducto.iCantidad = _objProducto.iCantidad;
            otblProducto.dPrecio = _objProducto.dPrecio;
            otblProducto.cDescripcion = _objProducto.cDescripcion;
            otblProducto.cImagen = "Foto";
            otblProducto.lEstatus = true;
            otblProducto.dtFechaModificacion = DateTime.Now;

            db.Entry(otblProducto).State = EntityState.Modified;

            db.SaveChanges();
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
                                                       dPrecio=producto.dPrecio,
                                                       iCantidad = producto.iCantidad,
                                                       lEstatus= producto.lEstatus,
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
                iCantidad = p.iCantidad,
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
                iCantidad = p.iCantidad,
                dPrecio = p.dPrecio,
                cNombre = p.cNombre,
                cImagen = p.cImagen,
                dtFechaAlta = (DateTime)p.dtFechaAlta
            }).Where(p => p.iIdCategoria == iIdCategoria).ToList();

            return _lstProductos;
        }

        /// <summary>
        /// M�todo para obtener productos por medio de b�squeda 
        /// </summary>
        /// <param name="cBusqueda">Cadena con el texto a buscar</param>
        /// <returns>Lista con los productos obtenidos de la b�squeda</returns>
        public List<CatalogoProductosDTO> ObtenerProductosPorBusqueda(string cBusqueda)
        {
            List<CatalogoProductosDTO> _lstProductos = db.tblCat_Producto.AsNoTracking().Select(p => new CatalogoProductosDTO()
            {
                iIdProducto = p.iIdProducto,
                iIdCategoria = p.iIdCategoria,
                iCantidad = p.iCantidad,
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
        public string GenerarPlantillaVacia(bool _lParaDescargar = true, string _cRutaAlternativa = "")
        {
            string cHome = AppDomain.CurrentDomain.BaseDirectory;
            string cRutaPlantilla = "";
            if (_lParaDescargar)
            {
                cRutaPlantilla = cHome + "Plantillas\\PlantillaVacia\\Plantilla.xlsx";
            }
            else
            {
                cRutaPlantilla = cHome + _cRutaAlternativa;
            }

            cRutaPlantilla = cRutaPlantilla.Normalize();
            List<string> lstEncabezados = new List<string>{
                "Nombre",
                "Descripci�n",
                "Precio",
                "Categor�a",
                "Estatus",
                "Existencia"
            };

            List<string> lstCategorias = (from cat in db.tblCat_Categoria
                                          select cat.cNombre).ToList();

            using (FileStream _oFileStream = new FileStream(cRutaPlantilla, FileMode.Create, FileAccess.Write))
            {
                IWorkbook oLibro = new XSSFWorkbook();
                ISheet oHoja = oLibro.CreateSheet("Plantilla");
                ISheet oHojaCategorias = oLibro.CreateSheet("Categorias");
                ICreationHelper oAyudanteCreacion = oLibro.GetCreationHelper();
                IRow oFilaEncabezados = oHoja.CreateRow(0);
                ICellStyle oEstiloDeCelda = oLibro.CreateCellStyle();
                oEstiloDeCelda.Alignment = HorizontalAlignment.Center;
                XSSFFont fuenteDeCelda = new XSSFFont();
                fuenteDeCelda.Boldweight = 700;
                oEstiloDeCelda.SetFont(fuenteDeCelda);
                for (int i = 0; i < lstEncabezados.Count; i++)
                {
                    ICell oCelda = oFilaEncabezados.CreateCell(i);
                    oCelda.SetCellValue(lstEncabezados[i]);
                    oCelda.CellStyle = oEstiloDeCelda;
                    oHoja.AutoSizeColumn(i);
                    GC.Collect();
                }
                
                IDataValidationHelper oAyudanteDeValidacionExcel = new XSSFDataValidationHelper((XSSFSheet)oHoja); 
                CellRangeAddressList oRangoDeCeldasCategoria = new CellRangeAddressList(1, 300, 3, 3);
                CellRangeAddressList oRangoDeCeldasEstatus = new CellRangeAddressList(1, 300, 4, 4);
                XSSFDataValidationConstraint oReglaValidacionListaCategorias = (XSSFDataValidationConstraint)oAyudanteDeValidacionExcel.CreateExplicitListConstraint(lstCategorias.ToArray());
                XSSFDataValidationConstraint oReglaDeValidacionEstatus = (XSSFDataValidationConstraint)oAyudanteDeValidacionExcel.CreateExplicitListConstraint(new string[] { "Activo", "Inactivo" });
                IDataValidation oValidadorCategorias = oAyudanteDeValidacionExcel.CreateValidation(oReglaValidacionListaCategorias, oRangoDeCeldasCategoria);
                IDataValidation oValidadorEstatus = oAyudanteDeValidacionExcel.CreateValidation(oReglaDeValidacionEstatus, oRangoDeCeldasEstatus);
                oHoja.AddValidationData(oValidadorCategorias);
                oHoja.AddValidationData(oValidadorEstatus);
                oLibro.Write(_oFileStream);

                oLibro.Close();
                _oFileStream.Close();
            }

            return cRutaPlantilla;
        }

        /// <summary>
        /// M�todo para verificar la existencia de productos
        /// </summary>
        /// <param name="iIdProducto">Entero con el ID del producto</param>
        /// <returns>Booleano que indica la existencia del producto</returns>
        public bool ChecarExistencia(int iIdProducto)
        {

            int iCantidad = db.tblCat_Producto.AsNoTracking().FirstOrDefault(p => p.iIdProducto == iIdProducto).iCantidad;

            return (iCantidad == 0) ? false : true;
        }

        /// <summary>
        /// M�todo que lee los datos registrados en una plantilla de Excel y los registra en la BDD
        /// </summary>
        /// <param name="_cNombreArchivo">Nombre del archivo subido</param>
        /// <returns>una cadena de texto con un mensaje con el resultado de la operaci�n</returns>
        public string ImportarProductosEnPlantilla(string _cNombreArchivo)
        {
            string cEstatus = "pendiente";
            string cHome = AppDomain.CurrentDomain.BaseDirectory;
            string cRutaPlantilla = cHome + "Plantillas\\PlantillaSubida\\" + _cNombreArchivo;
            using (FileStream _oFileStream = new FileStream(cRutaPlantilla, FileMode.Open, FileAccess.Read))
            {
                IWorkbook oLibro = new XSSFWorkbook(_oFileStream);
                ISheet oHoja = oLibro.GetSheetAt(0);

                for (int i = 1; i <= oHoja.LastRowNum; i++)
                {
                    tblCat_Producto oProducto = new tblCat_Producto();
                    IRow oFilaDatos = oHoja.GetRow(i);
                    for (int j = 0; j < oFilaDatos.LastCellNum; j++)
                    {
                        ICell oCeldaDatos = oFilaDatos.GetCell(j);
                        switch (j)
                        {
                            case 0:
                                oProducto.cNombre = oCeldaDatos.StringCellValue.Trim();
                                break;
                            case 1:
                                oProducto.cDescripcion = oCeldaDatos.StringCellValue.Trim();
                                break;
                            case 2:
                                oProducto.dPrecio = (decimal)oCeldaDatos.NumericCellValue;
                                break;
                            case 3:
                                oProducto.iIdCategoria = ObtenerIdCategoria(oCeldaDatos.StringCellValue);
                                break;
                            case 4:
                                
                                bool lEstatusActivoProducto = true;
                                string cDatoEstatusEnCelda = oCeldaDatos.StringCellValue;

                                if (cDatoEstatusEnCelda.Equals("Activo"))
                                {
                                    lEstatusActivoProducto = true;
                                }
                                else
                                {
                                    lEstatusActivoProducto = false;
                                }
                                oProducto.lEstatus = lEstatusActivoProducto;
                                
                                break;
                            case 5:
                                oProducto.iCantidad = Convert.ToInt32(oCeldaDatos.NumericCellValue);
                                break;
                            default:
                                break;
                        }

                    }
                    oProducto.cImagen = "Foto";
                    oProducto.dtFechaAlta = DateTime.Now;
                    oProducto.dtFechaModificacion = DateTime.Now;
                    try
                    {
                        db.tblCat_Producto.Add(oProducto);
                        db.SaveChanges();
                        cEstatus = "Registro exitoso";
                    }
                    catch (Exception e)
                    {
                        cEstatus = "Error: " + e.Message;
                    }


                }

            }

            return cEstatus;

        }

        /// <summary>
        /// M�todo que Retorna el ID de una categor�a registrada en base al nombre de esta
        /// </summary>
        /// <param name="cNombreCategoria">Nombre de la categor�a</param>
        /// <returns>ID de la categor�a</returns>
        private int ObtenerIdCategoria(string cNombreCategoria)
        {
            int iIdCategoria = 0;
            tblCat_Categoria oTblCatCategoria = db.tblCat_Categoria.FirstOrDefault(cat => cat.cNombre == cNombreCategoria);
            if (oTblCatCategoria != null)
            {
                iIdCategoria = oTblCatCategoria.iIdCategoria;
            }
            return iIdCategoria;
        }

        /// <summary>
        /// M�todo que genera un archivo de Excel que contiene todos los registros de la BDD
        /// </summary>
        /// <returns>Retorna la ruta absoluta del archivo en el servidor</returns>
        public string ExportarRegistrosExcel()
        {
            List<tblCat_Producto> lstProductos = db.tblCat_Producto.ToList();
            string cHome = AppDomain.CurrentDomain.BaseDirectory;
            string cRutaLocalPlantilla = "Plantillas\\PlantillaLlena\\DatosChangarro.xlsx";
            string cRutaAbsolutaPlantilla = cHome + cRutaLocalPlantilla;
            cRutaAbsolutaPlantilla = cRutaAbsolutaPlantilla.Normalize();
            List<string> lstEncabezados = new List<string>{
                "Nombre",
                "Descripci�n",
                "Precio",
                "Categor�a",
                "Estatus",
                "Existencia"
            };

            using (FileStream _oFileStream = new FileStream(cRutaAbsolutaPlantilla, FileMode.Create, FileAccess.Write))
            {
                IWorkbook oLibro = new XSSFWorkbook();
                ISheet oHoja = oLibro.CreateSheet("Registros");
                ICreationHelper oAyudanteCreacion = oLibro.GetCreationHelper();
                IRow oFilaEncabezados = oHoja.CreateRow(0);
                for (int i = 0; i < lstEncabezados.Count; i++)
                {
                    ICell oCelda = oFilaEncabezados.CreateCell(i);
                    oCelda.SetCellValue(lstEncabezados[i]);
                    oHoja.AutoSizeColumn(i);
                    GC.Collect();
                }

                for (int i = 0; i < lstProductos.Count; i++)
                {
                    
                    
                    IRow oFilaProducto = oHoja.CreateRow(i+1);
                    
                    ICell oCeldaNombre = oFilaProducto.CreateCell(0); oCeldaNombre.SetCellValue(lstProductos[i].cNombre);
                    ICell oCeldaDescripcion = oFilaProducto.CreateCell(1); oCeldaDescripcion.SetCellValue(lstProductos[i].cDescripcion);
                    ICell oCeldaPrecio = oFilaProducto.CreateCell(2); oCeldaPrecio.SetCellValue(double.Parse(lstProductos[i].dPrecio.ToString()));
                    string _cCategoriaAux = db.tblCat_Categoria.Find(lstProductos[i].iIdCategoria).cNombre;
                    ICell oCeldaCategoria = oFilaProducto.CreateCell(3); oCeldaCategoria.SetCellValue(_cCategoriaAux);
                    string cEstatus = "NA";
                    if (lstProductos[i].lEstatus)
                    {
                        cEstatus = "Activo";
                    }
                    else
                    {
                        cEstatus = "Inactivo";
                    }
                    ICell oCeldaEstatus = oFilaProducto.CreateCell(4); oCeldaEstatus.SetCellValue(cEstatus);
                    ICell oCeldaExistencia = oFilaProducto.CreateCell(5); oCeldaExistencia.SetCellValue(lstProductos[i].iCantidad);
                    oHoja.AutoSizeColumn(i);
                    GC.Collect();
                }


                oLibro.Write(_oFileStream);

                oLibro.Close();
                _oFileStream.Close();
            }

            return cRutaAbsolutaPlantilla;
        }

        /// <summary>
        /// M�todo que Genera un archivo PDF con los datos de los productos registrados en la BDD
        /// No recibe ning�n par�metro
        /// </summary>
        /// <returns>Retorna la ruta del archivo generado (string)</returns>
        public string ExportarRegistrosPDF()
        {
            string cRutaAbsoluta = "";
            string cNombreArchivo = "DatosChangarro.pdf";
            string cHome = AppDomain.CurrentDomain.BaseDirectory;
            cRutaAbsoluta = cHome + "Plantillas\\PlantillaLlena\\" + cNombreArchivo;
            using (FileStream _oFileStream = new FileStream(cRutaAbsoluta, FileMode.Create, FileAccess.Write)){
                using (Document _oArchivoPdf = new Document(PageSize.LETTER, 10f, 10f, 50f, 10f))
                {
                    try
                    {
                        PdfWriter oEscritorPdf = PdfWriter.GetInstance(_oArchivoPdf, _oFileStream);
                        _oArchivoPdf.Open();
                        
                        Paragraph oParrafoTitulo = new Paragraph(@"Productos Changarro�", new Font(Font.FontFamily.TIMES_ROMAN, 24));
                        oParrafoTitulo.Alignment = Element.ALIGN_CENTER;
                        _oArchivoPdf.Add(oParrafoTitulo);
                        _oArchivoPdf.Add(Chunk.NEWLINE);
                        PdfPTable oTablaProductos = new PdfPTable(6);
                        List<tblCat_Producto> lstProductos = db.tblCat_Producto.ToList();
                        Font oFuenteNegritas = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
                        List<Phrase> lstEncabezados = new List<Phrase>() {
                            new Phrase("Nombre", oFuenteNegritas),
                            new Phrase("Descripcion", oFuenteNegritas),
                            new Phrase("Precio", oFuenteNegritas),
                            new Phrase("Categoria", oFuenteNegritas),
                            new Phrase("Estatus", oFuenteNegritas),
                            new Phrase("Existencia", oFuenteNegritas)
                        };
                        foreach(Phrase contenido in lstEncabezados)
                        {
                            oTablaProductos.AddCell(contenido);
                        }
                        foreach (var producto in lstProductos)
                        {
                            oTablaProductos.AddCell(producto.cNombre);
                            oTablaProductos.AddCell(producto.cDescripcion);
                            oTablaProductos.AddCell(producto.dPrecio.ToString());
                            oTablaProductos.AddCell(db.tblCat_Categoria.Find(producto.iIdCategoria).cNombre);
                            oTablaProductos.AddCell(producto.lEstatus ? "Activo" : "Inactivo");
                            oTablaProductos.AddCell(producto.iCantidad.ToString());
                        }
                        _oArchivoPdf.Add(oTablaProductos);

                    }
                    catch (Exception e)
                    {
                        string mensaje = e.Message;
                        cRutaAbsoluta = "NA";
                    }
                }
            }
                return cRutaAbsoluta;
        }

    }

}