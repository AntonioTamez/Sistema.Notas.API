using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Notas.API.Shared.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int ProfesorId { get; set; }
    }
}
