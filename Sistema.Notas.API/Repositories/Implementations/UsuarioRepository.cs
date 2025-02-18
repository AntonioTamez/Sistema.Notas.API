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

        private readonly DbSet<User> _entity;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<User>();
        }

        public Task<ActionResponse<User>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResponse<IEnumerable<User>>> GetAsync()
        {
            var usuarios = await _context.Users
                .OrderBy(x => x.Name)
                .ToListAsync();

            return new ActionResponse<IEnumerable<User>>
            {
                WasSuccess = true,
                Result = usuarios
            };
        }

        public async Task<ActionResponse<User>> AddAsync(User entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<User>
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

                return new ActionResponse<User>
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

        public async Task<ActionResponse<User>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return new ActionResponse<User>
                {
                    WasSuccess = false,
                    Message = "Data not found"
                };
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<User>
                {
                    WasSuccess = true
                };
            }
            catch
            {
                return new ActionResponse<User>
                {
                    WasSuccess = false,
                    Message = "No se pude borrar, porque tiene registros relacionados."
                };
            }
        }

        public async Task<ActionResponse<User>> UpdateAsync(User entity)
        {
            _context.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<User>
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

                return new ActionResponse<User>
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

        private ActionResponse<User> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<User>
            {
                WasSuccess = false,
                Message = "Ya existe el registro que estas intentando crear."
            };
        }

        private ActionResponse<User> ExceptionActionResponse(Exception exception)
        {
            return new ActionResponse<User>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }



    }
}
