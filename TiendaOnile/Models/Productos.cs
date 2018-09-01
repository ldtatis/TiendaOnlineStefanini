namespace TiendaOnile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Productos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Productos()
        {
            Cateoria_Producto = new HashSet<Cateoria_Producto>();
            Producto_Pedido = new HashSet<Producto_Pedido>();
        }

        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        public int NumeroProducto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Precio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cateoria_Producto> Cateoria_Producto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto_Pedido> Producto_Pedido { get; set; }
    }
}
