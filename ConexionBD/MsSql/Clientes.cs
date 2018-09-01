namespace ConexionBD.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clientes()
        {
            Pedidos = new HashSet<Pedidos>();
        }

        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(200)]
        public string NombreCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(10)]
        public string Telefono { get; set; }

        [StringLength(200)]
        public string Correo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}
