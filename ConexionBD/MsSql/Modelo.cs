namespace ConexionBD.MsSql
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
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Producto_Pedido> Producto_Pedido { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorias>()
                .HasMany(e => e.Cateoria_Producto)
                .WithOptional(e => e.Categorias)
                .HasForeignKey(e => e.IdCatego);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Telefono)
                .IsFixedLength();

            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Clientes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.Subtotal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.Iva)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Productos>()
                .Property(e => e.Precio)
                .HasPrecision(18, 0);
        }
    }
}
