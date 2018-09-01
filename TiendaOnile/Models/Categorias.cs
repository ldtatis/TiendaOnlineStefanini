namespace TiendaOnile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categorias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categorias()
        {
            Cateoria_Producto = new HashSet<Cateoria_Producto>();
        }

        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreCategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cateoria_Producto> Cateoria_Producto { get; set; }
    }
}
