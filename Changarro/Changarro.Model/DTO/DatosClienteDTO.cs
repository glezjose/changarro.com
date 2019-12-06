///////////////////////////////////////////////////////////
//  DatosClienteDTO.cs
//  Implementation of the Class DatosClienteDTO
//  Generated by Enterprise Architect
//  Created on:      20-nov.-2019 04:24:47 p. m.
//  Original author: Mike
///////////////////////////////////////////////////////////

namespace Changarro.Model.DTO
{
    public class DatosClienteDTO : ClienteDTO {

        public DatosClienteDTO(){

        }

        ~DatosClienteDTO(){

        }

        /// <summary>
        /// Propiedad que almacena el Apellido del Cliente
        /// </summary>
        public string cApellido { get; set; }

        /// <summary>
        /// Propiedad que almacena el tel�fono del cliente
        /// </summary>
        public string cTelefono { get; set; }

    }

}