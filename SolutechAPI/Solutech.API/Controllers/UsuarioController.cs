using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Solutech.Data.Models;
using Solutech.Logic;
using Solutech.Model;

namespace Solutech.API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UsuarioLogic _logic;

        public UsuarioController(AppDbContext dbContext, UsuarioLogic logic)
        {
            _dbContext = dbContext;
            _logic = logic;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Usuario>>> Get()
        {
            return await _logic.GetListUsuarios();
        }


        [HttpGet("{id:int}")]
        public async Task<ApiResponse<Usuario>> Get([FromRoute] int id)
        {
            return await _logic.GetUsuario(id);
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<string>> Login([FromBody] Login login)
        {
            return await _logic.LoginUsuario(login);
        }

        [HttpPost]
        public async Task<ApiResponse<int>> Post([FromBody] Usuario usuario)
        {
            return await _logic.CreateUsuario(usuario);
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<Usuario>> Put([FromRoute]int id, [FromBody] Usuario usuario)
        {
            return await _logic.UpdateUsuario(id, usuario);
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<Usuario>> Delete([FromRoute] int id)
        {
            return await _logic.DeleteUsuario(id);
        }
    }
}
