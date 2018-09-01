using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class PedidosXProductos
    {
        public PedidosDTO Pedido { get; set; }
        public List<ProductoCheckbox> Productos { get; set; }

        public PedidosXProductos()
        {
            Pedido = new PedidosDTO();
            Productos = new List<ProductoCheckbox>();
        }
    }
    public class ProductoCheckbox
    {
        public ProductosDTO Producto { get; set; }
        public bool Selected { get; set; }
    }
}
