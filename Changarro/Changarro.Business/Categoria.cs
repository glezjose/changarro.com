///////////////////////////////////////////////////////////
//  Categoria.cs
//  Implementation of the Class Categor�a
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:22:49 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Linq;
using Changarro.Model;
using Changarro.Model.DTO;

namespace Changarro.Business
{
    public class Categoria {

        private readonly CHANGARROEntities db;

		public Categoria(){
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
		}

		~Categoria(){

		}

		public CategoriasDTO AgregarCategoria(){

			return null;
		}

		/// 
		/// <param name="iIdCategoria"></param>
		public void DesactivarCategoria(int iIdCategoria){

		}

		/// 
		/// <param name="iIdCategoria"></param>
		public CategoriasDTO EditarCategoria(int iIdCategoria){

			return null;
		}

        /// <summary>
        /// Este m�todo obtiene una lista de las categor�as en la BD.
        /// </summary>
        /// <returns>Regresa tal lista.</returns>
		public List<tblCat_Categoria> ObtenerCategorias(){

            List<tblCat_Categoria> _lstCategorias = db.tblCat_Categoria.AsNoTracking().ToList();

            return _lstCategorias;
		}

		public List<ProductosDTO> ObtenerProductos(){

			return null;
		}

        /// <summary>
        /// Este m�todo obtiene el nombre de la categor�a por id.
        /// </summary>
        /// <param name="iIdCategoria">La id de la categor�a.</param>
        /// <returns>Regresa la cadena del nombre.</returns>
        public string ObtenerNombreCategoria(int iIdCategoria)
        {
            string _cNombreCategoria = db.tblCat_Categoria.AsNoTracking().Where(c => c.iIdCategoria == iIdCategoria).FirstOrDefault().cNombre;

            return _cNombreCategoria;
        }


	}//end Categor�a

}//end namespace ChangarroBusiness