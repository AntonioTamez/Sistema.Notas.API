﻿using Microsoft.AspNetCore.Mvc;
using Sistema.Notas.API.Services;
using Sistema.Notas.API.Shared.DTOs;

namespace Sistema.Notas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Usuario y contraseña requeridos.");

            if (_authService.ValidateUser(userDto.Username, userDto.Password, out string role, out string name))
            {
                var token = _authService.GenerateJwtToken(userDto.Username, role, name);
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciales incorrectas.");
        }
    }
}
