using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class PedidosDTO
    {

        [Key]
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Iva { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }
        public ClientesDTO ClientesAsoc { get; set; }
        public List<ClientesDTO> ClientesDto { get; set; }

    }
}
