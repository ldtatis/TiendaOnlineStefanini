namespace TiendaOnile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pedidos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedidos()
        {
            Producto_Pedido = new HashSet<Producto_Pedido>();
        }

        [Key]
        public int IdPedido { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Iva { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto_Pedido> Producto_Pedido { get; set; }
    }
}
