using Microsoft.AspNetCore.Mvc;
using Solutech.Data.Models;
using Solutech.Logic;
using Solutech.Model;

namespace Solutech.API.Controllers
{
    [Route("api/[controller]")]
    public class TareasController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly TareaLogic _logic;

        public TareasController(AppDbContext dbContext, TareaLogic logic)
        {
            _dbContext = dbContext;
            _logic = logic;
        }

        [HttpGet("usuario/{id:int}")]
        public async Task<ApiResponse<List<Tarea>>> GetList([FromRoute]int id)
        {
            return await _logic.GetListTareas(id);
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<Tarea>> Get([FromRoute] int id)
        {
            return await _logic.GetTarea(id);
        }

        [HttpPost]
        public async Task<ApiResponse<int>> Post([FromBody] Tarea tarea)
        {
            return await _logic.CreateTarea(tarea);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<Tarea>> Put([FromRoute]int id, [FromBody]Tarea tarea)
        {
            return await _logic.UpdateTarea(id, tarea);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<Tarea>> Delete([FromRoute] int id)
        {
            return await _logic.DeleteTarea(id);
        }
    }
}
