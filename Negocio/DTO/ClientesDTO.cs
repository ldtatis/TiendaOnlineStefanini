using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class ClientesDTO
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Nombre cliente")]
        public string NombreCliente { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public string Correo { get; set; }
    }
}
