using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class Cateoria_ProductoDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRegistro { get; set; }

        public int? IdCatego { get; set; }

        public int? IdProducto { get; set; }

        public virtual CategoriasDTO Categorias { get; set; }

        public virtual ProductosDTO Productos { get; set; }
    }
}
