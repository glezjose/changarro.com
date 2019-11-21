///////////////////////////////////////////////////////////
//  TarjetaDTO.cs
//  Implementation of the Class TarjetaDTO
//  Generated by Enterprise Architect
//  Created on:      20-nov.-2019 04:24:53 p. m.
//  Original author: Mike
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Changarro.Model.DTO {
	public class TarjetaDTO {

		public TarjetaDTO(){

		}

		~TarjetaDTO(){

		}

		public string cNombre{
			get{
				return cNombre;
			}
			set{
				cNombre = value;
			}
		}

		public string cNumeroTarjeta{
			get{
				return cNumeroTarjeta;
			}
			set{
				cNumeroTarjeta = value;
			}
		}

		public string cTitular{
			get{
				return cTitular;
			}
			set{
				cTitular = value;
			}
		}

		public DateTime dtAnioExpiracion{
			get{
				return dtAnioExpiracion;
			}
			set{
				dtAnioExpiracion = value;
			}
		}

		public DateTime dtMesExpiracion{
			get{
				return dtMesExpiracion;
			}
			set{
				dtMesExpiracion = value;
			}
		}

		public int iIdTarjeta{
			get{
				return iIdTarjeta;
			}
			set{
				iIdTarjeta = value;
			}
		}

	}//end TarjetaDTO

}//end namespace ChangarroDTO