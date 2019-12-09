using Changarro.Model;
using Changarro.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Changarro.Business
{
    public class Administrador
    {

        CHANGARROEntities db;

        public Administrador()
        {
            db = new CHANGARROEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        public AdministradorDTO ObtenerAdministrador(int iIdAdministrador)
        {
            AdministradorDTO _oAdministrador = new AdministradorDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _oAdministrador = ctx.tblCat_Administrador.AsNoTracking()
                    .Where(c => c.iIdAdministrador == iIdAdministrador)
                    .Select(o => new AdministradorDTO
                    {
                        cNombre = o.cNombre,
                        cApellido = o.cApellido,
                        cTelefono = o.cTelefono,
                        cCorreo = o.cCorreo

                    }).FirstOrDefault();
            }
            return _oAdministrador;
        }

        public AdministradorDTO ObtenerDatosAdministrador(int iIdAdministrador)
        {
            AdministradorDTO _oAdministrador = new AdministradorDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _oAdministrador = ctx.tblCat_Administrador.AsNoTracking()
                    .Where(c => c.iIdAdministrador == iIdAdministrador)
                    .Select(o => new AdministradorDTO
                    {
                        cNombre = o.cNombre,
                        cApellido = o.cApellido,
                        cTelefono = o.cTelefono,
                        cCorreo = o.cCorreo

                    }).FirstOrDefault();
            }
            return _oAdministrador;
        }

        public AdministradorDTO EditarDatos(AdministradorDTO oAdministrador)
        {
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                tblCat_Administrador _oAdministrador = ctx.tblCat_Administrador.FirstOrDefault(c => c.iIdAdministrador == oAdministrador.iIdAdministrador); //consulta para obtener al cliente

                _oAdministrador.cNombre = oAdministrador.cNombre;
                _oAdministrador.cApellido = oAdministrador.cApellido;
                _oAdministrador.cTelefono = oAdministrador.cTelefono;
                _oAdministrador.cCorreo = oAdministrador.cCorreo;

                ctx.Entry(oAdministrador).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            return oAdministrador;
        }


        /// <summary>
        /// Este método obtiene el nombre del administrador.
        /// </summary>
        /// <param name="iIdAdministrador">Identificador del administrador.</param>
        /// <returns>Regresa el nombre obtenido.</returns>
        public string ObtenerNombreAdministrador(int iIdAdministrador)
        {
            string _cNombre = db.tblCat_Administrador.AsNoTracking().Where(p => p.iIdAdministrador == iIdAdministrador).Select(p => p.cNombre).FirstOrDefault();

            return _cNombre;
        }

    }
}