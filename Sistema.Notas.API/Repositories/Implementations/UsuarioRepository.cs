using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Repositories.Interfaces;
using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<ActionResponse<Usuario>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResponse<IEnumerable<Usuario>>> GetAsync()
        {
            var usuarios = await _context.Usuarios
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Usuario>>
            {
                WasSuccess = true,
                Result = usuarios
            };
        }

        public async Task<ActionResponse<Usuario>> AddAsync(Usuario entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Usuario>
                {
                    WasSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException!.Message.Contains("duplicate"))
                    {
                        return DbUpdateExceptionActionResponse();
                    }
                }

                return new ActionResponse<Usuario>
                {
                    WasSuccess = false,
                    Message = ex.Message
                };
            }
            catch (Exception exception)
            {
                return ExceptionActionResponse(exception);
            }
        }

        private ActionResponse<Usuario> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<Usuario>
            {
                WasSuccess = false,
                Message = "Ya existe el registro que estas intentando crear."
            };
        }

        private ActionResponse<Usuario> ExceptionActionResponse(Exception exception)
        {
            return new ActionResponse<Usuario>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }



    }
}
