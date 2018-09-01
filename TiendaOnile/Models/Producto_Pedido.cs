namespace TiendaOnile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Producto_Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRegistro { get; set; }

        public int? IdProducto { get; set; }

        public int? IdPedido { get; set; }

        public virtual Pedidos Pedidos { get; set; }

        public virtual Productos Productos { get; set; }
    }
}
