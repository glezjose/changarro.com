using Changarro.Model;
using Changarro.Model.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Changarro.Business
{
    public class Categoria
    {
        private readonly CHANGARROEntities db;

        public Categoria()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        ~Categoria()
        {

        }

        public ListaCategoriaDTO AgregarCategoria()
        {

            return null;
        }

        /// <summary>
        /// Método para habilitar o deshabilitar una Categoría
        /// </summary>
        /// <param name="_objCategoria">objeto que contiene los datos de una categoría según su id</param>

        public void DesactivarCategoria(tblCat_Categoria _objCategoria)
        {

            tblCat_Categoria otblCategoria = db.tblCat_Categoria.FirstOrDefault(Cat => Cat.iIdCategoria == _objCategoria.iIdCategoria);
            if (otblCategoria.lEstatus == true)
            {
                otblCategoria.lEstatus = false;
            }
            else
            {
                otblCategoria.lEstatus = true;
            }

            db.Entry(otblCategoria).State = EntityState.Modified;

            db.SaveChanges();

        }

        /// <summary>
        /// Método para actualizar los datos de una categoría 
        /// </summary>
        /// <param name="_objCategoria">objeto que contiene los datos de una categoría según su id </param>

        public void EditarCategoria(tblCat_Categoria _objCategoria)
        {

            tblCat_Categoria otblCategoria = db.tblCat_Categoria.FirstOrDefault(Cat => Cat.iIdCategoria == _objCategoria.iIdCategoria);
            otblCategoria.cNombre = _objCategoria.cNombre;


            db.Entry(otblCategoria).State = EntityState.Modified;

            db.SaveChanges();

        }

        /// <summary>
        /// Obtiene una lista de las categorías de la tabla y lo manda al controlador
        /// </summary>
        /// <returns>Lista con los datos de las categorías</returns>
        public List<ListaCategoriaDTO> ObtenerListaCategoria()
        {
            List<ListaCategoriaDTO> lstCategoria = (from c in db.tblCat_Categoria
                                                    select new ListaCategoriaDTO
                                                    {
                                                        iIdCategoria = c.iIdCategoria,
                                                        cNombre = c.cNombre,
                                                        lEstatus = c.lEstatus
                                                    }).ToList();
            return lstCategoria;
        }
        /// <summary>
        /// Método para insertar una nueva categoría
        /// </summary>
        /// <param name="_objCategoria"> almacena los valores a insertar a la base de datos</param>
        public void AgregarCategoria(tblCat_Categoria _objCategoria)
        {
            tblCat_Categoria otblCategoria = new tblCat_Categoria
            {
                cNombre = _objCategoria.cNombre,
                lEstatus = true
            };

            db.tblCat_Categoria.Add(otblCategoria);

            db.SaveChanges();

        }

        /// <summary>
        /// Este método obtiene una lista de las categorías en la BD.
        /// </summary>
        /// <returns>Regresa tal lista.</returns>
        public List<tblCat_Categoria> ObtenerCategorias()
        {

            List<tblCat_Categoria> _lstCategorias = db.tblCat_Categoria.AsNoTracking().ToList();

            return _lstCategorias;
        }

        public List<ProductosDTO> ObtenerProductos()
        {

            return null;
        }

        /// <summary>
        /// Este método obtiene el nombre de la categoría por id.
        /// </summary>
        /// <param name="iIdCategoria">La id de la categoría.</param>
        /// <returns>Regresa la cadena del nombre.</returns>
        public string ObtenerNombreCategoria(int iIdCategoria)
        {
            string _cNombreCategoria = db.tblCat_Categoria.AsNoTracking().Where(c => c.iIdCategoria == iIdCategoria).FirstOrDefault().cNombre;

            return _cNombreCategoria;
        }

        /// <summary>
        /// Obtiene solo las categorías activas
        /// </summary>
        /// <returns>Lista de categorías activas</returns>
        public List<ListaCategoriaDTO> CategoriaActiva()
        {

            List<ListaCategoriaDTO> lstCategoria = (from c in db.tblCat_Categoria
                                                    where (c.lEstatus == true)
                                                    select new ListaCategoriaDTO
                                                    {
                                                        iIdCategoria = c.iIdCategoria,
                                                        cNombre = c.cNombre,
                                                        lEstatus = c.lEstatus
                                                    }).ToList();
            return lstCategoria;

        }

    }

}