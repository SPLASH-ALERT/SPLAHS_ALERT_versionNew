using System;
using System.Collections.Generic;

namespace splash_alert.Models
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdrRoles { get; set; }
        public string? NombreRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
