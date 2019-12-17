namespace Changarro.Model.DTO
{
    public class DetallesProductoDTO {

        /// <summary>
        /// Propiedad que almacena el id del producto
        /// </summary>
        public int iIdProducto { get; set; }

        /// <summary>
        /// Propiedad que almacena el id de la categor�a del producto
        /// </summary>
        public int iIdCategoria { get; set; }

        /// <summary>
        /// Propiedad que almacena la cantidad del producto
        /// </summary>
        public int iCantidad { get; set; }

        /// <summary>
        /// Propiedad que almacena el precio del producto
        /// </summary>
        public decimal dPrecio { get; set; }

        /// <summary>
        /// Propiedad que almacena el nombre del producto
        /// </summary>
        public string cNombre { get; set; }
        /// <summary>
        /// Variable representado como almac�n de respaldo.
        /// </summary>
        private string _cImagen;

        /// <summary>
        /// Ruta de la imagen de perfil del cliente.
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

        /// <summary>
        /// Propiedad la descripci�n del producto
        /// </summary>
        public string cDescripcion { get; set; }


    }

}