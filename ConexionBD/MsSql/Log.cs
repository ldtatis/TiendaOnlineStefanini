namespace ConexionBD.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLog { get; set; }

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }

        [Required]
        public string Message { get; set; }

        public string InnerException { get; set; }

        public string Source { get; set; }

        public string TargetSite { get; set; }

        public bool? success { get; set; }
    }
}
