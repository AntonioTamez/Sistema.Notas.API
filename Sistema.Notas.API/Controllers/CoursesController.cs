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

    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            var response = await _courseRepository.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize]
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(Course course)
        {
            var response = await _courseRepository.AddAsync(course);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize]
        [HttpPost("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse(Course course)
        {
            var response = await _courseRepository.UpdateAsync(course);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }

        [Authorize]
        [HttpDelete("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var response = await _courseRepository.DeleteAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();

        }
    }
}
