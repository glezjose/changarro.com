//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Changarro.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCat_Compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCat_Compra()
        {
            this.tbl_DetalleCompra = new HashSet<tbl_DetalleCompra>();
        }
    
        public int iIdCompra { get; set; }
        public int iIdTarjeta { get; set; }
        public int iIdCliente { get; set; }
        public int iIdDireccion { get; set; }
        public bool lEstatus { get; set; }
        public Nullable<System.DateTime> dtFechaCompra { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DetalleCompra> tbl_DetalleCompra { get; set; }
        public virtual tblCat_Cliente tblCat_Cliente { get; set; }
        public virtual tblCat_Direccion tblCat_Direccion { get; set; }
        public virtual tblCat_Tarjeta tblCat_Tarjeta { get; set; }
    }
}
