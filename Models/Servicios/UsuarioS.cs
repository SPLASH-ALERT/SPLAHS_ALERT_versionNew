namespace splash_alert.Models.Servicios
{
    public class UsuarioS
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Estado { get; set; }
        public int? IdRol { get; set; }
        public string? Tokken { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
