using System;

namespace Changarro.Model.DTO
{
	public class GestionProductoDTO {

		/// <summary>
		/// Nombre de la categor�a del producto
		/// </summary>
		public string cCategoria { get; set; }

		/// <summary>
		/// Nombre del producto
		/// </summary>
		public string cNombre { get; set; }

		/// <summary>
		/// Presio del producto
		/// </summary>
		public decimal dPrecio { get; set; }

		/// <summary>
		/// Fecha de alta del producto
		/// </summary>
		public DateTime dtFechaAlta { get; set; }

		/// <summary>
		/// Fecha de modificaci�n del producto
		/// </summary>
		public DateTime dtFechaModificacion { get; set; }

		/// <summary>
		/// ID del producto
		/// </summary>
		public int iIdProducto { get; set; }

	}

}