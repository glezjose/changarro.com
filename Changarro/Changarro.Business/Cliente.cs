///////////////////////////////////////////////////////////
//  Cliente.cs
//  Implementation of the Class Cliente
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:14:48 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;
using System;

namespace Changarro.Business
{
    public class Cliente
    {
        CHANGARROEntities db;

        public Cliente()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// M�todo para obtener cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Objeto con datos del cliente</returns>
        public ClienteDTO ObtenerCliente(int iIdCliente)
        {
            ClienteDTO _oCliente = new ClienteDTO();
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _oCliente = ctx.tblCat_Cliente.AsNoTracking()
                    .Where(c => c.iIdCliente == iIdCliente)
                    .Select(o => new ClienteDTO
                    {
                        cNombre = o.cNombre,
                        cCorreo = o.cCorreo,
                        cImagen = o.cImagen

                    }).FirstOrDefault();
            }

            return _oCliente;
        }

        /// <summary>
        /// M�todo para obtener los datos del cliente
        /// </summary>
        /// <param name="iIdCliente">Id del cliente</param>
        /// <returns>Objeto con los datos del cliente</returns>
        public DatosClienteDTO ObtenerDatosCliente(int iIdCliente)
        {
            DatosClienteDTO _oCliente = new DatosClienteDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _oCliente = ctx.tblCat_Cliente.AsNoTracking()
                    .Where(c => c.iIdCliente == iIdCliente)
                    .Select(o => new DatosClienteDTO
                    {
                        cNombre = o.cNombre,
                        cApellido = o.cApellido,
                        cTelefono = o.cTelefono,
                        cCorreo = o.cCorreo

                    }).FirstOrDefault();
            }
            return _oCliente;
        }

        /// <summary>
        /// M�todo para habilitar o deshabilitar a un Cliente y asignar una fecha.
        /// </summary>
        /// <param name="iIdCliente"> ID del Cliente seleccionado</param>
        /// <param name="lEstatus"> Estatus del Cliente seleccionado </param>
        /// <returns>Objeto con los valores</returns>
        public ClienteAdministradorDTO CambiarEstatusCliente(int iIdCliente, bool lEstatus)
        {
            DateTime? _dtFechaBaja;
            if (lEstatus == true)
            {
                _dtFechaBaja = null;
            }
            else
            {
                _dtFechaBaja = DateTime.Now;
            }

            tblCat_Cliente _oCliente = db.tblCat_Cliente.Where(c => c.iIdCliente == iIdCliente).FirstOrDefault();

            _oCliente.lEstatus = lEstatus;
            _oCliente.dtFechaBaja = _dtFechaBaja;

            db.SaveChanges();

            ClienteAdministradorDTO _oClienteActualizado = new ClienteAdministradorDTO()
            {
                iIdCliente = _oCliente.iIdCliente,
                cNombre = _oCliente.cNombre,
                cApellido = _oCliente.cApellido,
                cTelefono = _oCliente.cTelefono,
                cCorreo = _oCliente.cCorreo,
                lEstatus = _oCliente.lEstatus,
                dtFechaAlta = _oCliente.dtFechaAlta,
                dtFechaBaja = _oCliente.dtFechaBaja,
                dtFechaModificacion = _oCliente.dtFechaModificacion,

            };

            return _oClienteActualizado;
        }

        /// <summary>
        /// M�todo para crear una lista con los datos de los clientes
        /// </summary>
        /// <returns>Lista con los datos de los clientes</returns>
        public List<ClienteAdministradorDTO> ObtenerClientes()
        {
            List<ClienteAdministradorDTO> listClientes = db.tblCat_Cliente.AsNoTracking().Select(c => new ClienteAdministradorDTO()
            {
                iIdCliente = c.iIdCliente,
                cNombre = c.cNombre,
                cApellido = c.cApellido,
                cTelefono = c.cTelefono,
                cCorreo = c.cCorreo,
                lEstatus = c.lEstatus,
                dtFechaAlta = c.dtFechaAlta,
                dtFechaBaja = c.dtFechaBaja,
                dtFechaModificacion = c.dtFechaModificacion,

            }).ToList();

            return listClientes;
        }

        /// <summary>
        /// M�todo para obtener el ID de un cliente.
        /// </summary>
        /// <param name="iIdCliente"> ID del Cliente seleccionado </param>
        /// <returns>Objeto oCliente que contiene valores del cliente seleccionado por si ID</returns>
        public tblCat_Cliente ObtenerDatos(int iIdCliente) 
        {
            tblCat_Cliente oCliente = db.tblCat_Cliente.AsNoTracking().FirstOrDefault(c => c.iIdCliente == iIdCliente);
            return oCliente;
        }

    }//end Cliente

}//end namespace ChangarroBusiness