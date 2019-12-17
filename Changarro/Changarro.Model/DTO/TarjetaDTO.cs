namespace Changarro.Model.DTO
{
	public class TarjetaDTO {

		/// <summary>
		/// ID de la terjeta
		/// </summary>
		public int iIdTarjeta { get; set; }

		/// <summary>
		/// ID del cliente
		/// </summary>
		public int iIdCliente { get; set; }

		/// <summary>
		/// Nombre descriptivo de la tarjeta
		/// </summary>
		public string cNombre { get; set; }

		/// <summary>
		/// Numero de la tarjeta
		/// </summary>
		public string cNumeroTarjeta { get; set; }

		/// <summary>
		/// Nombre del titular de la tarjeta
		/// </summary>
		public string cTitular { get; set; }

		/// <summary>
		/// Año de expiración de la tarjeta
		/// </summary>
		public byte iAnioExpiracion { get; set; }

		/// <summary>
		/// Mes de expiración de la tarjeta
		/// </summary>
		public byte iMesExpiracion { get; set; }

	}

}