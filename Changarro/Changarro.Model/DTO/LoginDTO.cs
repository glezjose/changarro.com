﻿///////////////////////////////////////////////////////////
//  LoginDTO.cs
//  Implementation of the Class LoginDTO
//  Generated by Enterprise Architect
//  Created on:      20-nov.-2019 04:24:50 p. m.
//  Original author: Mike
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Changarro.Model.DTO {
    public class LoginDTO {

        public LoginDTO(){

        }

        ~LoginDTO(){

        }

        /// <summary>
        /// Propiedad que almacena el ID del usuario;
        /// </summary>
        public int iIdUsuario { get; set; }

        /// <summary>
        /// Propiedad que almacena la contraseña del usuario.
        /// </summary>
        public string cContrasenia { get; set; }

        /// <summary>
        /// Propiedad que almacena el correo del usuario.
        /// </summary>
        public string cCorreo { get; set; }

    }
}