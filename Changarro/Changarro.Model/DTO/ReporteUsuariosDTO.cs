using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase es la responsable de contener las propiedades para crear la gráfica de los usuarios con más compras.
    /// </summary>
    public class ReporteUsuariosDTO
    {
        /// <summary>
        /// Identificador del cliente.
        /// </summary>
        public int iIdCliente { get; set; }

        /// <summary>
        /// Total de Compras realizadas.
        /// </summary>
        public int iTotalCompras { get; set; }

        /// <summary>
        /// Variable que regresa el valor.
        /// </summary>
        private string _cNombre;

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string cNombre
        {
            get { return _cNombre; }
            set
            {
                if (value.Contains(" "))
                {
                    char[] separador = { ' ' };
                    String[] primerNombre = value.Split(separador);
                    _cNombre = primerNombre[0];
                }
                else _cNombre = value;
            }
        }

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string cApellido { get; set; }

        /// <summary>
        /// Variable que regresa el valor.
        /// </summary>
        private string _cImagen;

        /// <summary>
        /// Ruta de la imagen del perfil del cliente.
        /// </summary>
        public string cImagen {
            get { return _cImagen; }
            set
            {
                char[] separador = { '_' };

                String[] _arrImagen = value.Split(separador);

                _cImagen = "https://res.cloudinary.com/blue-ocean-technologies/image/upload/v" + _arrImagen[0] + "/Changarro/Clientes/" + _arrImagen[1];
            }
        }

    }
}
