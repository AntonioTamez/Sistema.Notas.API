using System.ComponentModel.DataAnnotations;

namespace Sistema.Notas.API.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }

        [Range(0,100 , ErrorMessage ="El valor debe ser entre {0} y {1}")]
        public decimal Nota { get; set; }
    }
}
