using Microsoft.EntityFrameworkCore;
using Sistema.Notas.API.Data;
using Sistema.Notas.API.Repositories.Interfaces;
using Sistema.Notas.API.Shared.Entities;
using Sistema.Notas.API.Shared.Responses;

namespace Sistema.Notas.API.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        private readonly DbSet<Course> _entity;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<Course>();
        }

        public Task<ActionResponse<Course>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResponse<IEnumerable<Course>>> GetAsync()
        {
            var courses = await _context.Courses
                .OrderBy(x => x.Name)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Course>>
            {
                WasSuccess = true,
                Result = courses
            };
        }

        public async Task<ActionResponse<Course>> AddAsync(Course entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Course>
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

                return new ActionResponse<Course>
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

        public async Task<ActionResponse<Course>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return new ActionResponse<Course>
                {
                    WasSuccess = false,
                    Message = "Data not found"
                };
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<Course>
                {
                    WasSuccess = true
                };
            }
            catch
            {
                return new ActionResponse<Course>
                {
                    WasSuccess = false,
                    Message = "No se pude borrar, porque tiene registros relacionados."
                };
            }
        }

        public async Task<ActionResponse<Course>> UpdateAsync(Course entity)
        {
            _context.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Course>
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

                return new ActionResponse<Course>
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

        private ActionResponse<Course> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<Course>
            {
                WasSuccess = false,
                Message = "Ya existe el registro que estas intentando crear."
            };
        }

        private ActionResponse<Course> ExceptionActionResponse(Exception exception)
        {
            return new ActionResponse<Course>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }


    }
}
