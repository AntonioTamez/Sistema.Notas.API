using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Notas.API.Shared.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [Range(0, 100, ErrorMessage = "The value must be between {0} y {1}")]
        [Precision(5, 2)]
        public decimal Value { get; set; }
    }
}
