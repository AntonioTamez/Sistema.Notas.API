using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Repositories.Interfaces;
using Sistema.Notas.API.Shared.Entities;

namespace Sistema.Notas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _usuarioRepository.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [HttpPost("AddUsuarios")]
        public async Task<IActionResult> AddUsuarios(Usuario usuario)
        {
            var response = await _usuarioRepository.AddAsync(usuario);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }


    }
}
