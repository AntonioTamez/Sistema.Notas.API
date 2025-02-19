using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Repositories.Interfaces;
using Sistema.Notas.API.Shared.Entities;

namespace Sistema.Notas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usuarioRepository;

        public UsersController(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await _usuarioRepository.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUsuarios(User usuario)
        {
            var response = await _usuarioRepository.AddAsync(usuario);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User usuario)
        {
            var response = await _usuarioRepository.UpdateAsync(usuario);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _usuarioRepository.DeleteAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

    }
}
