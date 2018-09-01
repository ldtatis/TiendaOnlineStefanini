using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class ProductosDTO
    {
        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "título")]
        public string Titulo { get; set; }
        [Display(Name = "Número de producto")]
        public int NumeroProducto { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Precio { get; set; }
    }
}
