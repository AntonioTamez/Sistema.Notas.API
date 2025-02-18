using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<ActionResponse<Course>> AddAsync(Course entity);
        Task<ActionResponse<Course>> DeleteAsync(int id);
        Task<ActionResponse<Course>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Course>>> GetAsync();
        Task<ActionResponse<Course>> UpdateAsync(Course entity);
    }
}
