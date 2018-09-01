namespace ConexionBD.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cateoria_Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRegistro { get; set; }

        public int? IdCatego { get; set; }

        public int? IdProducto { get; set; }

        public virtual Categorias Categorias { get; set; }

        public virtual Productos Productos { get; set; }
    }
}
