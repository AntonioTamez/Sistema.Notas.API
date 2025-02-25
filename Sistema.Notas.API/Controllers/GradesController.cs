using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Repositories.Interfaces;
using Sistema.Notas.API.Shared.Entities;

namespace Sistema.Notas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;

        public GradesController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        [Authorize(Policy = "onlyStudent")]
        [HttpGet("GetGrades")]
        public async Task<IActionResult> GetGrades()
        {
            var response = await _gradeRepository.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGrade(Grade grade)
        {
            var response = await _gradeRepository.AddAsync(grade);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("UpdateGrade")]
        public async Task<IActionResult> UpdateGrade(Grade grade)
        {
            var response = await _gradeRepository.UpdateAsync(grade);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("DeleteGrade")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var response = await _gradeRepository.DeleteAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }
    }
}
