namespace TiendaOnile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
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
    }
}
