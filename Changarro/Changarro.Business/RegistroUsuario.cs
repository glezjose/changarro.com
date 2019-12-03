using Changarro.Model;
using Changarro.Model.DTO;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ChangarroBusiness
{
    public class RegistroUsuario
    {
        private readonly CHANGARROEntities ctx;

        public RegistroUsuario()
        {
            ctx = new CHANGARROEntities();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Método para registrar usuarios
        /// </summary>
        /// <param name="oCliente">Objeto con los datos del Cliente</param>
        public void RegistrarCliente(RegistroDTO oCliente)
        {
            tblCat_Cliente _oCliente = new tblCat_Cliente()
            {
                cNombre = oCliente.cNombre,
                cApellido = oCliente.cApellido,
                cTelefono = "N/A",
                cCorreo = oCliente.cCorreo,
                cContrasenia = GenerarHash(oCliente.cContrasenia),
                cImagen = "1574316321_ImagenClienteDef",
                dtFechaAlta = DateTime.Today,
                lEstatus = true
            };

            ctx.tblCat_Cliente.Add(_oCliente);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Método para validar que no existan usuarios con el mismo nombre
        /// </summary>
        /// <param name="oUsuario"></param>
        /// <returns>Objeto con mensajes de campos inválidos</returns>
        public RegistroDTO ValidarDatos(RegistroDTO oUsuario)
        {
            RegistroDTO _oUsuario = new RegistroDTO();            

            if (ctx.tblCat_Cliente.Any(c => c.cCorreo == oUsuario.cCorreo))
            {
                _oUsuario.cCorreo = oUsuario.cCorreo;
            }
            if (ctx.tblCat_Cliente.Any(c => (c.cNombre + c.cApellido).Trim().ToLower() == (oUsuario.cNombre + c.cApellido).Trim().ToLower()))
            {
                _oUsuario.cNombre = oUsuario.cNombre.ToLower();
                _oUsuario.cApellido = oUsuario.cApellido.ToLower();
            }
            else
            {
                _oUsuario = null;
            }

            return _oUsuario;
        }

        /// <summary>
        /// Método para cifrar la contraseña del usuario
        /// </summary>
        /// <param name="cContraseña">Contraseña del usuario</param>
        /// <returns>Regresa la contraseña cifrada del usuario</returns>
        public string GenerarHash(string cContraseña)
        {
            // Crea un hash tipo SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - regresa un byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cContraseña));

                // Convierte el byte array en string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
