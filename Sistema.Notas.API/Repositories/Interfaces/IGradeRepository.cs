using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Task<ActionResponse<Grade>> AddAsync(Grade entity);
        Task<ActionResponse<Grade>> DeleteAsync(int id);
        Task<ActionResponse<Grade>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Grade>>> GetAsync();
        Task<ActionResponse<Grade>> UpdateAsync(Grade entity);
    }
}
