using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Changarro.Model;
using Changarro.Model.DTO;

namespace Changarro.Business
{
    public class Domicilio
    {

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

        /// <summary>
        /// Método para agregar domicilios
        /// </summary>
        /// <param name="oDomicilio">Objeto con los datos del nuevo domicilio</param>
        public void AgregarDomicilio(DomicilioDTO oDomicilio)
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

            db.tblCat_Direccion.Add(_oDireccion);
            db.SaveChanges();
        }

        /// <summary>
        /// Método para desactivar domicilio
        /// </summary>
        /// <param name="iIdDireccion"></param>
        public void DesactivarDomicilio(int iIdDireccion)
        {

            tblCat_Direccion _oDireccion = db.tblCat_Direccion.FirstOrDefault(d => d.iIdDireccion == iIdDireccion);

            _oDireccion.lEstatus = false;

            db.Entry(_oDireccion).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Método para editar los domicilios del cliente
        /// </summary>
        /// <param name="oDomicilio">Objeto con los nuevos datos del domicilio</param>
        public void EditarDomicilio(DomicilioDTO oDomicilio)
        {

            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            tblCat_Direccion _oDireccion = db.tblCat_Direccion.FirstOrDefault(d => d.iIdDireccion == oDomicilio.iIdDireccion);

            _oDireccion.iIdEstado = oDomicilio.iIdEstado;
            _oDireccion.iCodigoPostal = oDomicilio.iCodigoPostal;
            _oDireccion.cNombre = oDomicilio.cNombre;
            _oDireccion.cCalle = oDomicilio.cCalle;
            _oDireccion.cColonia = oDomicilio.cColonia;
            _oDireccion.cDescripcion = oDomicilio.cDescripcion;
            _oDireccion.cMunicipio = oDomicilio.cMunicipio;
            _oDireccion.cNumeroExterior = oDomicilio.cNumeroExterior;
            _oDireccion.cNumeroInterior = oDomicilio.cNumeroInterior;

            db.Entry(_oDireccion).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Método para obtener la lista direcciones de envío del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Lista con los registros de los domicilios</returns>
        public List<DomicilioDTO> ObtenerDomicilios(int iIdCliente)
        {

            List<DomicilioDTO> _lstDomicilios = new List<DomicilioDTO>();

            _lstDomicilios = db.tblCat_Direccion.AsNoTracking()
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

            return _lstDomicilios;
        }

        /// <summary>
        /// Método para obtener domicilio
        /// </summary>
        /// <param name="iIdDireccion">ID del domicilio</param>
        /// <returns>Objeto con los datos del domicilio</returns>
        public DomicilioDTO ObtenerDomicilio(int iIdDireccion)
        {
            DomicilioDTO _oDomicilio = new DomicilioDTO();

            _oDomicilio = db.tblCat_Direccion.AsNoTracking()
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

            return _oDomicilio;
        }

        /// <summary>
        /// Método para obtener una la lista de estados registrados
        /// </summary>
        /// <returns>Lista con todos los estados</returns>
        public List<ListaEstadosDTO> ObtenerEstados()
        {
            List<ListaEstadosDTO> _lstEstados = new List<ListaEstadosDTO>();

            _lstEstados = db.tbl_Estado.AsNoTracking().Select(e => new ListaEstadosDTO { iIdEstado = e.iIdEstado, cNombre = e.cNombre }).ToList();

            return _lstEstados;
        }

        /// <summary>
        /// Obtiene los domicilios del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Lista con los domicilios obtenidos</returns>
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

        /// <summary>
        /// Obtiene el domicilio que se usara para la compra
        /// </summary>
        /// <param name="iIdDomicilio">ID del domicilio</param>
        /// <returns>Objeto con los datos del domicilio</returns>
        public DomicilioCompraDTO ObtenerDomicilioCompra(int iIdDomicilio)
        {
            DomicilioCompraDTO _oDomicilio = db.tblCat_Direccion.AsNoTracking().Where(d => d.iIdDireccion == iIdDomicilio).Select(d => new DomicilioCompraDTO
            {
                iIdDomicilio = d.iIdDireccion,
                cNombre = d.cNombre,
                cDomicilio = d.cCalle + " " + d.cNumeroExterior + " " + d.cColonia + ", " + d.cMunicipio + ", " + d.tbl_Estado.cNombre
            }).FirstOrDefault();

            return _oDomicilio;
        }

        /// <summary>
        /// Método para registrar un nuevo domicilio
        /// </summary>
        /// <param name="oDomicilio">Objeto con los datos del domicilio</param>
        /// <returns>Entero que contiene el ID del domicilio</returns>
        public int AgregarNuevoDomicilio(DomicilioDTO oDomicilio)
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

            db.tblCat_Direccion.Add(_oDireccion);
            db.SaveChanges();

            int iIdDomicilio = _oDireccion.iIdDireccion;

            return iIdDomicilio;
        }

    }

}