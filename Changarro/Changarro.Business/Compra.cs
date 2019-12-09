///////////////////////////////////////////////////////////
//  Compra.cs
//  Implementation of the Class Compra
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:15:32 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;

namespace Changarro.Business {
	public class Compra {

        CHANGARROEntities db;
		public Compra(){
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
		}

		~Compra(){

		}

		/// 
		/// <param name="iIdCompra"></param>
		public void CancelarCompra(int iIdCompra){

		}

		/// 
		/// <param name="iIdCliente"></param>
		public List<tblCat_Compra> ObtenerCompras(int iIdCliente){

			return null;
		}

		/// 
		/// <param name="iIdProducto"></param>
		/// <param name="iCantidad"></param>
		public void RealizarCompra(int iIdProducto, int iCantidad){

		}

		/// 
		/// <param name="iIdCarrito"></param>
		public void RealizarCompraCarrito(int iIdCarrito){

		}

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

	}//end Compra

}//end namespace ChangarroBusiness