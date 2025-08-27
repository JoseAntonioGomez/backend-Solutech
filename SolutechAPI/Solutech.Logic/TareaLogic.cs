using Microsoft.EntityFrameworkCore;
using Solutech.Data.Models;
using Solutech.Model;

namespace Solutech.Logic
{
    public class TareaLogic
    {
        private readonly AppDbContext _dbContext;

        public TareaLogic(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<Tarea>>> GetListTareas(int idUsuario)
        {
            var result = await _dbContext.Tareas.Where(x => x.UsuarioId == idUsuario).ToListAsync();

            return new ApiResponse<List<Tarea>>
            {
                Status = 200,
                Success = true,
                Data = result,
                Message = "Exitoso",
            };
        }

        public async Task<ApiResponse<Tarea>> GetTarea(int id)
        {
            var result = await _dbContext.Tareas.FirstOrDefaultAsync(x=>x.TareaId == id && x.Estado);

            if (result is null)
            {
                return new ApiResponse<Tarea>
                {
                    Status = 404,
                    Success = false,
                    Data = null,
                    Message = "Error",
                };
            }
            return new ApiResponse<Tarea> 
            {
                Status = 200,
                Success = true,
                Data = result,
                Message = "Exitoso",
            };
        }

        public async Task<ApiResponse<int>> CreateTarea(Tarea Tarea)
        {
            try
            {
                _dbContext.Add(Tarea);
                await _dbContext.SaveChangesAsync();
                int id = Tarea.TareaId;
                return new ApiResponse<int>
                {
                    Status = 200,
                    Success = true,
                    Data = id,
                    Message = "Tarea registrado",
                };
            }
            catch (Exception)
            {
                return new ApiResponse<int>
                {
                    Status = 500,
                    Success = false,
                    Data = 0,
                    Message = "Error de registro Tarea",
                };
            }
            
        }

        public async Task<ApiResponse<Tarea>> UpdateTarea(int id, Tarea Tarea)
        {
            if (!await _dbContext.Tareas.AnyAsync(x=>x.TareaId == id && x.Estado))
            {
                return new ApiResponse<Tarea>
                {
                    Status = 404,
                    Success = false,
                    Data = null,
                    Message = "Tarea no encontrado",
                };
            }

            _dbContext.Update(Tarea);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Tarea>
            {
                Status = 200,
                Success = true,
                Data = Tarea,
                Message = "Tarea actualizado",
            };
        }

        public async Task<ApiResponse<Tarea>> DeleteTarea(int id)
        {         
            var Tarea = await _dbContext.Tareas.FirstOrDefaultAsync(x => x.TareaId == id && x.Estado);
            if (Tarea is null)
            {
                return new ApiResponse<Tarea>
                {
                    Status = 404,
                    Success = false,
                    Data = null,
                    Message = "Tarea no encontrado",
                };
            }
            Tarea.Estado = false;
            Tarea.FechaModificacion = DateTime.Now;

            _dbContext.Update(Tarea);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Tarea>
            {
                Status = 200,
                Success = true,
                Data = Tarea,
                Message = "Tarea eliminado",
            };
        }
    }
}
