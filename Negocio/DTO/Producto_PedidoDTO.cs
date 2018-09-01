using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class Producto_PedidoDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRegistro { get; set; }

        public int? IdProducto { get; set; }

        public int? IdPedido { get; set; }
        
    }
}
