﻿namespace Sistema.Notas.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? ContrasenaHash { get; set; }
        public string? Rol { get; set; }  // "Administrador", "Profesor", "Estudiante"
    }

}
