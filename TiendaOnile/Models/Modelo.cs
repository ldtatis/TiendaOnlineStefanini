namespace TiendaOnile
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Modelo : DbContext
    {
        public Modelo()
            : base("name=Modelo")
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Cateoria_Producto> Cateoria_Producto { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Producto_Pedido> Producto_Pedido { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }

    }
}
