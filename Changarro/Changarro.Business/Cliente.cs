using System.Collections.Generic;
using Changarro.Model;
using Changarro.Model.DTO;
using System.Linq;
using System;
using System.Data.Entity;

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
        /// Método para obtener cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Objeto con datos del cliente</returns>
        public ClienteDTO ObtenerCliente(int iIdCliente)
        {

            ClienteDTO _oCliente = new ClienteDTO();

            ReporteUsuariosDTO _oUsuarios = new ReporteUsuariosDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                _oCliente = ctx.tblCat_Cliente.AsNoTracking()
                    .Where(c => c.iIdCliente == iIdCliente)
                    .Select(o => new ClienteDTO
                    {
                        cNombre = o.cNombre,
                        cCorreo = o.cCorreo,
                        cImagen = o.cImagen

                    }).FirstOrDefault();
            }

            _oUsuarios.cImagen = _oCliente.cImagen;
            _oUsuarios.cNombre = _oCliente.cNombre;

            _oCliente.cImagen = _oUsuarios.cImagen;
            _oCliente.cNombre = _oUsuarios.cNombre;

            return _oCliente;
        }

        /// <summary>
        /// Método para obtener los datos del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Objeto con los datos del cliente</returns>
        public DatosClienteDTO ObtenerDatosCliente(int iIdCliente)
        {
            DatosClienteDTO _oCliente = new DatosClienteDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

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
        /// Método para habilitar o deshabilitar a un Cliente y asignar una fecha.
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
        /// Método para editar los datos del cliente
        /// </summary>
        /// <param name="oCliente">Objeto con los datos del cliente</param>
        /// <returns>Objeto con los nuevos datos del cliente</returns>
        public DatosClienteDTO EditarDatos(DatosClienteDTO oCliente)
        {
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                tblCat_Cliente _oCliente = ctx.tblCat_Cliente.FirstOrDefault(c => c.iIdCliente == oCliente.iIdCliente); 

                _oCliente.cNombre = oCliente.cNombre;
                _oCliente.cApellido = oCliente.cApellido;
                _oCliente.cTelefono = oCliente.cTelefono;
                _oCliente.cCorreo = oCliente.cCorreo;
                _oCliente.dtFechaModificacion = DateTime.Today;

                ctx.Entry(_oCliente).State = EntityState.Modified;

                ctx.SaveChanges();
            }
            
            return oCliente;
        }

        /// <summary>
        /// Método para cambiar la imagen de perfil
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <param name="cImagen">Cadena con el nombre de la nueva imagen</param>
        public string CambiarImagen(int iIdCliente, string cImagen)
        {
            ReporteUsuariosDTO _oUsuarios = new ReporteUsuariosDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                tblCat_Cliente _oCliente = ctx.tblCat_Cliente.FirstOrDefault(c => c.iIdCliente == iIdCliente);

                _oCliente.cImagen = cImagen;

                _oCliente.dtFechaModificacion = DateTime.Today;

                ctx.Entry(_oCliente).State = EntityState.Modified;

                ctx.SaveChanges();

                _oUsuarios.cImagen = _oCliente.cImagen;

                return _oUsuarios.cImagen;
            }
        }

        /// <summary>
        /// Método para crear una lista con los datos de los clientes
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
        /// Método para obtener el ID de un cliente.
        /// </summary>
        /// <param name="iIdCliente"> ID del Cliente seleccionado </param>
        /// <returns>Objeto oCliente que contiene valores del cliente seleccionado por si ID</returns>
        public tblCat_Cliente ObtenerDatos(int iIdCliente)
        {
            tblCat_Cliente oCliente = db.tblCat_Cliente.AsNoTracking().FirstOrDefault(c => c.iIdCliente == iIdCliente);
            return oCliente;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ClienteAdministradorDTO> ObtenerTopClientes()
        {

            return null;
        }

        /// <summary>
        /// Método para validar que no se repitan los datos del cliente con otro usuario
        /// </summary>
        /// <param name="oCliente"></param>
        /// <returns></returns>
        public DatosClienteDTO ValidarCliente(DatosClienteDTO oCliente)
        {
            bool _lStatus = false;

            DatosClienteDTO _oUsuario = new DatosClienteDTO();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
               
                if (ctx.tblCat_Cliente.Any(c => c.cCorreo == oCliente.cCorreo && c.iIdCliente != oCliente.iIdCliente))
                {
                    _lStatus = true;
                    _oUsuario.cCorreo = oCliente.cCorreo;
                }
                if (ctx.tblCat_Cliente.Any(c => c.cTelefono == oCliente.cTelefono && c.iIdCliente != oCliente.iIdCliente))
                {
                    _lStatus = true;
                    _oUsuario.cTelefono = oCliente.cTelefono;
                }
                if (ctx.tblCat_Cliente.Any(c => (c.cNombre + c.cApellido).Trim().ToLower() == (oCliente.cNombre + c.cApellido).Trim().ToLower() && c.iIdCliente != oCliente.iIdCliente))
                {
                    _lStatus = true;
                    _oUsuario.cNombre = oCliente.cNombre.ToLower();
                    _oUsuario.cApellido = oCliente.cApellido.ToLower();
                }
                if (_lStatus != true)
                {
                    _oUsuario = null;
                }
            }

            return _oUsuario;
        }

        /// <summary>
        /// Método para desactivar la cuenta del cliente
        /// </summary>
        /// <param name="iIdCliente">ID del  cliente</param>
        /// <param name="cContrasenia">Contraseña del cliente</param>
        /// <returns>Mensaje de error por contraseña incorrecta</returns>
        public string CancelarSuscripcion(int iIdCliente, string cContrasenia)
        {
            RegistroUsuario Registro = new RegistroUsuario();
            string _cMensaje = null;

            string _cContrasenia = Registro.GenerarHash(cContrasenia);          

            
            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                tblCat_Cliente _oCliente = ctx.tblCat_Cliente.FirstOrDefault(c => c.iIdCliente == iIdCliente && c.cContrasenia == _cContrasenia);

                if (_oCliente != null)
                {
                    _oCliente.lEstatus = false;
                    _oCliente.dtFechaBaja = DateTime.Today;
                    ctx.Entry(_oCliente).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                else
                {
                    _cMensaje = "Contraseña Incorrecta";
                }
            }

            return _cMensaje;
        }

        /// <summary>
        /// Método para obtener las compras del usuario
        /// </summary>
        /// <param name="iIdCliente">ID del cliente</param>
        /// <returns>Lista con todas las compras del cliente</returns>
        public List<HistorialCompraDTO> HistorialCompras(int iIdCliente)
        {
            List<HistorialCompraDTO> _lstHistorialCompras = new List<HistorialCompraDTO>();

            using (CHANGARROEntities ctx = new CHANGARROEntities())
            {
                _lstHistorialCompras = ctx.tbl_DetalleCompra.AsNoTracking()
                    .Where(c => c.tblCat_Compra.iIdCliente == iIdCliente)
                    .Select(c => new HistorialCompraDTO
                    {
                        cNombre = c.tblCat_Producto.cNombre,
                        cImagen = c.tblCat_Producto.cImagen,
                        iCantidad = c.iCantidad,
                        dPrecio = c.tblCat_Producto.dPrecio * c.iCantidad,
                        dtFechaCompra = c.tblCat_Compra.dtFechaCompra,

                    }).ToList();
            }

            return _lstHistorialCompras;
        }

    }
}