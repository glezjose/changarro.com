///////////////////////////////////////////////////////////
//  Administrador.cs
//  Implementation of the Class Administrador
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:21:53 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using Changarro.Model;
using Changarro.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangarroBusiness
{
    public class Administrador {

        CHANGARROEntities db = new CHANGARROEntities();

        public Administrador(){

        }

        public List<AdministradorDTO> CargarDatosPerfil()
        {
            throw new NotImplementedException();
        }

        ~Administrador(){

        }

        public AdministradorDTO EditarDatos(){

            return null;
        }

        /// 
        /// <param name="iIdAdministrador"></param>
        public List<AdministradorDTO> ObtenerDatos()
        {
            List<AdministradorDTO> listdatos = db.tblCat_Administrador.AsNoTracking().Select(p => new AdministradorDTO()
            {
                cApellido = p.cApellido,
                cCorreo = p.cCorreo,
                cImagen = p.cImagen,
                cNombre = p.cNombre,
                cTelefono = p.cTelefono,
                iIdAdministrador = p.iIdAdministrador,


            }).ToList();

            return listdatos;
        }

        public AdministradorDTO CrearObjetoEmpleado(tblCat_Administrador oDatos)
        {
            AdministradorDTO oAdministradorDTO = new AdministradorDTO()
            {
                iIdAdministrador = oDatos.iIdAdministrador,
                cApellido = oDatos.cApellido,
                cCorreo = oDatos.cCorreo,
                cImagen = oDatos.cImagen,
                cNombre = oDatos.cNombre,
                cTelefono = oDatos.cTelefono,             
            };
            return oAdministradorDTO;
        }

    }//end Administrador

}//end namespace ChangarroBusiness