using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Repositories.Interfaces;
using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Implementations
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        private readonly DbSet<Grade> _entity;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<Grade>();
        }

        public Task<ActionResponse<Grade>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResponse<IEnumerable<Grade>>> GetAsync()
        {
            var courses = await _context.Grades
                .OrderBy(x => x.CourseId)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Grade>>
            {
                WasSuccess = true,
                Result = courses
            };
        }

        public async Task<ActionResponse<Grade>> AddAsync(Grade entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Grade>
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

                return new ActionResponse<Grade>
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

        public async Task<ActionResponse<Grade>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return new ActionResponse<Grade>
                {
                    WasSuccess = false,
                    Message = "Data not found"
                };
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<Grade>
                {
                    WasSuccess = true
                };
            }
            catch
            {
                return new ActionResponse<Grade>
                {
                    WasSuccess = false,
                    Message = "No se pude borrar, porque tiene registros relacionados."
                };
            }
        }

        public async Task<ActionResponse<Grade>> UpdateAsync(Grade entity)
        {
            _context.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Grade>
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

                return new ActionResponse<Grade>
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

        private ActionResponse<Grade> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<Grade>
            {
                WasSuccess = false,
                Message = "Ya existe el registro que estas intentando crear."
            };
        }

        private ActionResponse<Grade> ExceptionActionResponse(Exception exception)
        {
            return new ActionResponse<Grade>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }
}
