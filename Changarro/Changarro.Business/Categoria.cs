///////////////////////////////////////////////////////////
//  Categoria.cs
//  Implementation of the Class Categoria
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:22:49 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using Changarro.Model;
using Changarro.Model.DTO;

namespace ChangarroBusiness
{
    public class Categoria
    {
        CHANGARROEntities db = new CHANGARROEntities();
        public Categoria(CHANGARROEntities db)
        {
            this.db = db;
            this.db.Configuration.LazyLoadingEnabled = false;
            this.db.Configuration.ProxyCreationEnabled = false;
        }

        ~Categoria()
        {

        }

        public ListaCategoriaDTO AgregarCategoria()
        {

            return null;
        }

        /// 
        /// <param name="iIdCategoria"></param>
        public void DesactivarCategoria(int iIdCategoria)
        {

        }

        /// 
        /// <param name="iIdCategoria"></param>
        public ListaCategoriaDTO EditarCategoria(int iIdCategoria)
        {

            return null;
        }

        public List<tblCat_Categoria> ObtenerCategorias()
        {

            return null;
        }
        /// <summary>
        /// Obtiene una lista de las categor�as de la tabla y lo manda al controlador
        /// </summary>
        /// <returns></returns>
        public List<ListaCategoriaDTO> ObtenerListaCategoria()
        {
            List<ListaCategoriaDTO> lstCategoria = (from c in db.tblCat_Categoria
                                                   select new ListaCategoriaDTO
                                                   {
                                                       iIdCategoria = c.iIdCategoria,
                                                       cNombre = c.cNombre,
                                                   }).ToList();
            return lstCategoria;
        }
        /// <summary>
        /// M�todo para insertar una nueva categor�a
        /// </summary>
        /// <param name="_objCategoria"> almacena los valores a insertar a la base de datos</param>
        public void AgregarCategoria(tblCat_Categoria _objCategoria)
        {
            DbContextTransaction dbTran = db.Database.BeginTransaction();//investigar 

            try
            {
                db.tblCat_Categoria.Add(_objCategoria);

                db.SaveChanges();
                dbTran.Commit();
            }
            catch (Exception)
            {
                dbTran.Rollback();
                throw;
            }
        }

    }//end Categoria

}//end namespace ChangarroBusiness