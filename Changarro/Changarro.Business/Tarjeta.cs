using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Changarro.Model;
using Changarro.Model.DTO;

namespace Changarro.Business
{
    public class Tarjeta
    {
        CHANGARROEntities db;

        public Tarjeta()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        ~Tarjeta()
        {

        }

        /// <summary>
        /// Método para insertar tarjetas a la base de datos
        /// </summary>
        /// <param name="oTarjeta">Objeto con los datos de la tarjeta</param>
        public string AgregarTarjeta(TarjetaDTO oTarjeta)
        {
            string _cNumeroTarjeta;

            tblCat_Tarjeta _oTarjeta = new tblCat_Tarjeta
            {
                iIdCliente = oTarjeta.iIdCliente,
                itMesExpiracion = oTarjeta.iMesExpiracion,
                iAnioExpiracion = oTarjeta.iAnioExpiracion,
                lEstatus = true,
                cNombre = oTarjeta.cNombre,
                cTitular = oTarjeta.cTitular,
                cNumeroTarjeta = oTarjeta.cNumeroTarjeta,
            };

            if (ValidarTarjeta(oTarjeta.cNumeroTarjeta))
            {
                db.tblCat_Tarjeta.Add(_oTarjeta);

                db.SaveChanges();

                _cNumeroTarjeta = null;
            }
            else
            {
                _cNumeroTarjeta = _oTarjeta.cNumeroTarjeta;
            }


            return _cNumeroTarjeta;
        }

        /// <summary>
        /// Método para desactivar tarjetas
        /// </summary>
        /// <param name="iIdTarjeta">ID de la tarjeta</param>
        public void DesactivarTarjeta(int iIdTarjeta)
        {

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                tblCat_Tarjeta _oTarjeta = ctx.tblCat_Tarjeta.FirstOrDefault(t => t.iIdTarjeta == iIdTarjeta);

                _oTarjeta.lEstatus = false;

                ctx.Entry(_oTarjeta).State = EntityState.Modified;

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Método para obtener la lista de las tarjetas del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Lista con los datos de las tarjetas</returns>
        public List<TarjetaDTO> ObtenerTarjetas(int iIdCliente)
        {

            string _cDigitosTarjeta = "************";

            List<TarjetaDTO> _lstTarjeta = new List<TarjetaDTO>();

            _lstTarjeta = db.tblCat_Tarjeta.AsNoTracking()
                .Where(t => t.iIdCliente == iIdCliente && t.lEstatus == true)
                .Select(t => new TarjetaDTO
                {
                    iIdTarjeta = t.iIdTarjeta,
                    cNombre = t.cNombre,
                    cTitular = t.cTitular,
                    cNumeroTarjeta = _cDigitosTarjeta + t.cNumeroTarjeta.Substring(12),
                    iAnioExpiracion = t.iAnioExpiracion,
                    iMesExpiracion = t.itMesExpiracion

                }).ToList();
            return _lstTarjeta;
        }

        /// <summary>
        /// Método para obtener la tarjeta del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Objeto con los datos de la tarjeta</returns>
        public TarjetaDTO ObtenerTarjeta(int iIdTarjeta)
        {

            TarjetaDTO _oTarjeta = new TarjetaDTO();

            _oTarjeta = db.tblCat_Tarjeta.AsNoTracking()
                .Select(t => new TarjetaDTO
                {
                    iIdTarjeta = t.iIdTarjeta,
                    cNombre = t.cNombre,
                    cTitular = t.cTitular,
                    cNumeroTarjeta = t.cNumeroTarjeta,
                    iAnioExpiracion = t.iAnioExpiracion,
                    iMesExpiracion = t.itMesExpiracion

                }).FirstOrDefault(p => p.iIdTarjeta == iIdTarjeta);
            return _oTarjeta;
        }

        /// <summary>
        /// Método para comprobar si existe un numero de tarjeta ya registrado
        /// </summary>
        /// <param name="cNumeroTarjeta">Cadena que contiene el numero de tarjeta a verificar</param>
        /// <returns>Bandera de tipo bool</returns>
        public bool ValidarTarjeta(string cNumeroTarjeta)
        {

            bool lStatus;

            lStatus = db.tblCat_Tarjeta.AsNoTracking().Any(t => t.cNumeroTarjeta == cNumeroTarjeta) ? false : true;

            return lStatus;
        }

        /// <summary>
        /// Método para obtener las tarjetas del cliente 
        /// </summary>
        /// <param name="iIdCliente">Entero con el ID del cliente</param>
        /// <returns>Lista con los datos de la tarjeta</returns>
        public List<TarjetaCompraDTO> ObtenerTarjetasCompra(int iIdCliente)
        {
            List<TarjetaCompraDTO> _lstTarjetas = db.tblCat_Tarjeta.AsNoTracking().Where(t => t.iIdCliente == iIdCliente && t.lEstatus == true).Select(t => new TarjetaCompraDTO
            {
                iIdTarjeta = t.iIdTarjeta,
                cNombre = t.cNombre,
                cTarjeta = t.cNumeroTarjeta.Substring(t.cNumeroTarjeta.Length - 4)
            }).ToList();

            return _lstTarjetas;
        }

        /// <summary>
        /// Método para obtener tarjeta para la compra
        /// </summary>
        /// <param name="iIdTarjeta">Entero con el ID de la tarjeta</param>
        /// <returns>Objeto con los datos de la tarjeta</returns>
        public TarjetaCompraDTO ObtenerTarjetaCompra(int iIdTarjeta)
        {
            TarjetaCompraDTO _oTarjeta = db.tblCat_Tarjeta.AsNoTracking().Where(t => t.iIdTarjeta == iIdTarjeta).Select(t => new TarjetaCompraDTO
            {
                iIdTarjeta = t.iIdTarjeta,
                cNombre = t.cNombre,
                cTarjeta = t.cNumeroTarjeta.Substring(t.cNumeroTarjeta.Length - 4)
            }).FirstOrDefault();

            return _oTarjeta;
        }

        /// <summary>
        /// Método para registrar nueva tarjeta 
        /// </summary>
        /// <param name="oTarjeta">Objeto con los datos de la tarjeta</param>
        /// <returns>Entero con el ID de la tarjeta</returns>
        public int AgregarNuevaTarjeta(TarjetaDTO oTarjeta)
        {
            tblCat_Tarjeta _oTarjeta = new tblCat_Tarjeta
            {
                iIdCliente = oTarjeta.iIdCliente,
                itMesExpiracion = oTarjeta.iMesExpiracion,
                iAnioExpiracion = oTarjeta.iAnioExpiracion,
                lEstatus = true,
                cNombre = oTarjeta.cNombre,
                cTitular = oTarjeta.cTitular,
                cNumeroTarjeta = oTarjeta.cNumeroTarjeta,
            };
            db.tblCat_Tarjeta.Add(_oTarjeta);

            db.SaveChanges();

            int iIdTarjeta = _oTarjeta.iIdTarjeta;

            return iIdTarjeta;
        }
    }

}