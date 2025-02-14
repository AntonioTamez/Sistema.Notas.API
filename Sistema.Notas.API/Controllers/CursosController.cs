using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Shared.Entities;

namespace Sistema.Notas.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Cursos.ToListAsync();
        }
    }
}
