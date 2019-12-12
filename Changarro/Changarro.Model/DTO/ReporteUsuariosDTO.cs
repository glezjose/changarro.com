using System;

namespace Changarro.Model.DTO
{
    /// <summary>
    /// Esta clase contiene las propiedades para obtener a los usuarios con más compras.
    /// </summary>
    public class ReporteUsuariosDTO
    {
        /// <summary>
        /// Identificador del cliente.
        /// </summary>
        public int iIdCliente { get; set; }

        /// <summary>
        /// Total de compras realizadas.
        /// </summary>
        public int iTotalCompras { get; set; }

        /// <summary>
        /// Variable representado como almacén de respaldo.
        /// </summary>
        private string _cNombre;

        /// <summary>
        /// Nombre del cliente.
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
        /// Apellido del cliente.
        /// </summary>
        public string cApellido { get; set; }

        /// <summary>
        /// Variable representado como almacén de respaldo.
        /// </summary>
        private string _cImagen;

        /// <summary>
        /// Ruta de la imagen de perfil del cliente.
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
