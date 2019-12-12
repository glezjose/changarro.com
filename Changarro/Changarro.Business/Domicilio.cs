///////////////////////////////////////////////////////////
//  Domicilio.cs
//  Implementation of the Class Domicilio
//  Generated by Enterprise Architect
//  Created on:      14-nov.-2019 05:15:16 p. m.
//  Original author: jose.gonzalez
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Changarro.Model;
using Changarro.Model.DTO;

namespace Changarro.Business
{
    public class Domicilio {

        CHANGARROEntities db;
        public Domicilio()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        ~Domicilio()
        {

        }

        public void AgregarDomicilio(DomicilioDTO oDomicilio)
        {
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                tblCat_Direccion _oDireccion = new tblCat_Direccion()
                {
                    lEstatus = true,
                    iIdCliente = oDomicilio.iIdCliente,
                    iIdEstado = oDomicilio.iIdEstado,
                    iCodigoPostal = oDomicilio.iCodigoPostal,
                    cNombre = oDomicilio.cNombre,
                    cCalle = oDomicilio.cCalle,
                    cColonia = oDomicilio.cColonia,
                    cDescripcion = oDomicilio.cDescripcion,
                    cMunicipio = oDomicilio.cMunicipio,
                    cNumeroExterior = oDomicilio.cNumeroExterior,
                    cNumeroInterior = oDomicilio.cNumeroInterior,
                };

                ctx.tblCat_Direccion.Add(_oDireccion);
                ctx.SaveChanges();
            }
        }


        public void DesactivarDomicilio(int iIdDireccion)
        {
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                tblCat_Direccion _oDireccion = ctx.tblCat_Direccion.FirstOrDefault(d => d.iIdDireccion == iIdDireccion);

                _oDireccion.lEstatus = false;

                ctx.Entry(_oDireccion).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// M�todo para editar los domicilios del cliente
        /// </summary>
        /// <param name="oDomicilio">Objeto con los nuevos datos del domicilio</param>
        public void EditarDomicilio(DomicilioDTO oDomicilio)
        {
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                tblCat_Direccion _oDireccion = ctx.tblCat_Direccion.FirstOrDefault(d => d.iIdDireccion == oDomicilio.iIdDireccion);

                _oDireccion.iIdEstado = oDomicilio.iIdEstado;
                _oDireccion.iCodigoPostal = oDomicilio.iCodigoPostal;
                _oDireccion.cNombre = oDomicilio.cNombre;
                _oDireccion.cCalle = oDomicilio.cCalle;
                _oDireccion.cColonia = oDomicilio.cColonia;
                _oDireccion.cDescripcion = oDomicilio.cDescripcion;
                _oDireccion.cMunicipio = oDomicilio.cMunicipio;
                _oDireccion.cNumeroExterior = oDomicilio.cNumeroExterior;
                _oDireccion.cNumeroInterior = oDomicilio.cNumeroInterior;

                ctx.Entry(_oDireccion).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// M�todo para obtener la lista direcciones de env�o del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Lista con los registros de los domicilios</returns>
        public List<DomicilioDTO> ObtenerDomicilios(int iIdCliente){

            List<DomicilioDTO> _lstDomicilios = new List<DomicilioDTO>();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _lstDomicilios = ctx.tblCat_Direccion.AsNoTracking()
                    .Where(d => d.iIdCliente == iIdCliente && d.lEstatus == true)
                    .Select(d => new DomicilioDTO 
                        { 
                            iIdDireccion = d.iIdDireccion,
                            cNombre = d.cNombre,
                            cNumeroExterior = d.cNumeroExterior,
                            cNumeroInterior = d.cNumeroInterior,
                            cCalle = d.cCalle,
                            cColonia = d.cColonia,
                            iCodigoPostal = d.iCodigoPostal,
                            cMunicipio = d.cMunicipio,
                            cEstado = d.tbl_Estado.cNombre,
                            cDescripcion = d.cDescripcion
                    
                
                        }).ToList();
            }

            return _lstDomicilios;
        }

        /// <summary>
        /// M�todo para obtener domicilio
        /// </summary>
        /// <param name="iIdDireccion">ID del domicilio</param>
        /// <returns>Objeto con los datos del domicilio</returns>
        public DomicilioDTO ObtenerDomicilio(int iIdDireccion)
        {
            DomicilioDTO _oDomicilio = new DomicilioDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _oDomicilio = ctx.tblCat_Direccion.AsNoTracking()
                    .Select(d => new DomicilioDTO
                        {
                            iIdDireccion = d.iIdDireccion,
                            iIdEstado = d.iIdEstado,
                            iCodigoPostal = d.iCodigoPostal,
                            cNombre = d.cNombre,
                            cNumeroExterior = d.cNumeroExterior,
                            cNumeroInterior = d.cNumeroInterior,
                            cCalle = d.cCalle,
                            cColonia = d.cColonia,                            
                            cMunicipio = d.cMunicipio,
                            cEstado = d.tbl_Estado.cNombre,
                            cDescripcion = d.cDescripcion


                        }).FirstOrDefault(d => d.iIdDireccion == iIdDireccion);
            }

            return _oDomicilio;
        }

        /// <summary>
        /// M�todo para obtener una la lista de estados registrados
        /// </summary>
        /// <returns>Lista con todos los estados</returns>
        public List<ListaEstadosDTO> ObtenerEstados()
        {
            List<ListaEstadosDTO> _lstEstados = new List<ListaEstadosDTO>();
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _lstEstados = ctx.tbl_Estado.AsNoTracking().Select(e => new ListaEstadosDTO { iIdEstado = e.iIdEstado, cNombre = e.cNombre }).ToList();
            }

            return _lstEstados;
        }

        public List<DomicilioCompraDTO> ObtenerDomiciliosCompra(int iIdCliente)
        {
            List<DomicilioCompraDTO> _lstDomicilios = db.tblCat_Direccion.AsNoTracking().Where(d => d.iIdCliente == iIdCliente && d.lEstatus == true).Select(d => new DomicilioCompraDTO
            {
                iIdDomicilio = d.iIdDireccion,
                cNombre = d.cNombre,
                cDomicilio = d.cCalle + " " + d.cNumeroExterior + " " + d.cColonia + ", " + d.cMunicipio + ", " + d.tbl_Estado.cNombre
            }).ToList();

            return _lstDomicilios;
        }

		public bool ValidarDomicilio(){

            return false;
        }

    }//end Domicilio

}//end namespace ChangarroBusiness