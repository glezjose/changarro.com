///////////////////////////////////////////////////////////
//  GestionProductoDTO.cs
//  Implementation of the Class GestionProductoDTO
//  Generated by Enterprise Architect
//  Created on:      20-nov.-2019 04:24:49 p. m.
//  Original author: Mike
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Changarro.Model.DTO {
	public class GestionProductoDTO {

		public GestionProductoDTO(){

		}

		~GestionProductoDTO(){

		}

		public string cCategoria{
			get{
				return cCategoria;
			}
			set{
				cCategoria = value;
			}
		}

		public string cNombre{
			get{
				return cNombre;
			}
			set{
				cNombre = value;
			}
		}

		public decimal dPrecio{
			get{
				return dPrecio;
			}
			set{
				dPrecio = value;
			}
		}

		public DateTime dtFechaAlta{
			get{
				return dtFechaAlta;
			}
			set{
				dtFechaAlta = value;
			}
		}

		public DateTime dtFechaModificacion{
			get{
				return dtFechaModificacion;
			}
			set{
				dtFechaModificacion = value;
			}
		}

		public int iIdProducto{
			get{
				return iIdProducto;
			}
			set{
				iIdProducto = value;
			}
		}

	}//end GestionProductoDTO

}//end namespace ChangarroDTO