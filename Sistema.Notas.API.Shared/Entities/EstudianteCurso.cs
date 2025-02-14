using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Notas.API.Shared.Entities
{
    public class EstudianteCurso
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
    }
}
