using System;
using System.Collections.Generic;

namespace splash_alert.Models
{
    public partial class Causa
    {
        public int IdCausa { get; set; }
        public string? Causa1 { get; set; }
        public DateTime? FechaCausa { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarios { get; set; }
    }
}
