///////////////////////////////////////////////////////////
//  DetallesProductoDTO.cs
//  Implementation of the Class DetallesProductoDTO
//  Generated by Enterprise Architect
//  Created on:      20-nov.-2019 04:24:48 p. m.
//  Original author: Mike
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Changarro.Model.DTO {
	public class DetallesProductoDTO {

		public DetallesProductoDTO(){

		}

		~DetallesProductoDTO(){

		}

		public string cCategoria{
			get{
				return cCategoria;
			}
			set{
				cCategoria = value;
			}
		}

		public string cDescripcion{
			get{
				return cDescripcion;
			}
			set{
				cDescripcion = value;
			}
		}

		public int iCantidad{
			get{
				return iCantidad;
			}
			set{
				iCantidad = value;
			}
		}

	}//end DetallesProductoDTO

}//end namespace ChangarroDTO