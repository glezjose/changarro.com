using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changarro.Model.DTO
{
    public class CatalogoProductosDTO
    {
        public int iIdProducto { get; set; }
        public int iIdCategoria { get; set; }
        public int iCantidad { get; set; }
        public decimal dPrecio { get; set; }
        public string cNombre { get; set; }
        /// <summary>
        /// Variable representado como almacén de respaldo.
        /// </summary>
        private string _cImagen;

        /// <summary>
        /// Ruta de la imagen del producto.
        /// </summary>
        public string cImagen
        {
            get { return _cImagen; }
            set
            {
                char[] separador = { '_' };

                String[] _arrImagen = value.Split(separador);

                _cImagen = "https://res.cloudinary.com/blue-ocean-technologies/image/upload/v" + _arrImagen[0] + "/Changarro/Productos/" + _arrImagen[1];
            }
        }
        public DateTime dtFechaAlta { get; set; }
    }
}
