namespace Changarro.Model.DTO
{
    public class DomicilioDTO {

        /// <summary>
        /// ID del domicilio
        /// </summary>
        public int iIdDireccion { get; set; }

        /// <summary>
        /// ID del cliente
        /// </summary>
        public int iIdCliente { get; set; }

        /// <summary>
        /// ID del estado al que pertenece el domicilio
        /// </summary>
        public int iIdEstado { get; set; }

        /// <summary>
        /// Código postal del domicilio
        /// </summary>
        public int iCodigoPostal { get; set; }

        /// <summary>
        /// Calle del domicilio
        /// </summary>
        public string cCalle { get; set; }        

        /// <summary>
        /// Colonia del domicilio
        /// </summary>
        public string cColonia { get; set; }

        /// <summary>
        /// Descripción del domicilio
        /// </summary>
        public string cDescripcion { get; set; }

        /// <summary>
        /// Municipio al que pertenece el domicilio
        /// </summary>
        public string cMunicipio { get; set; }

        /// <summary>
        /// Nombre descriptivo del domicilio
        /// </summary>
        public string cNombre { get; set; }

        /// <summary>
        /// Número exterior del domicilio
        /// </summary>
        public string cNumeroExterior { get; set; }

        /// <summary>
        /// Numero interior del domicilio
        /// </summary>
        public string cNumeroInterior { get; set; }

        /// <summary>
        /// Nombre del estado al que pertenece el domicilio
        /// </summary>
        public string cEstado { get; set; }        

    }

}