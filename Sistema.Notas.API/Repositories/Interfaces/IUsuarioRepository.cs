using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ActionResponse<Usuario>> AddAsync(Usuario entity);
        Task<ActionResponse<Usuario>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Usuario>>> GetAsync();

    }
}
