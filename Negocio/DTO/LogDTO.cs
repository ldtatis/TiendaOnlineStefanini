using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTO
{
    public class LogDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
        public bool success { get; set; }
    }
}
