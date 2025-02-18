using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ActionResponse<User>> AddAsync(User entity);
        Task<ActionResponse<User>> DeleteAsync(int id);
        Task<ActionResponse<User>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<User>>> GetAsync();
        Task<ActionResponse<User>> UpdateAsync(User entity);
    }
}
