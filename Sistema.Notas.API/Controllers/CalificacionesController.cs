using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Models;

namespace Sistema.Notas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalificacionesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacion>>> GetUsuarios()
        {
            return await _context.Calificaciones.ToListAsync();
        }
    }
}
