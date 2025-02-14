using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Notas.API.Shared.Entities
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
