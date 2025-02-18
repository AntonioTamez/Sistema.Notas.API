using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Notas.API.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? passwordHash { get; set; }
        public string? Role { get; set; }  // "Administrador", "Profesor", "Estudiante"
    }
}
