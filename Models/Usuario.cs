using System;
using System.Collections.Generic;

namespace splash_alert.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Causas = new HashSet<Causa>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Estado { get; set; }
        public int? IdRol { get; set; }
        public string? Tokken{ get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Role? IdRoles { get; set; }
        public virtual ICollection<Causa> Causas { get; set; }
    }
}
