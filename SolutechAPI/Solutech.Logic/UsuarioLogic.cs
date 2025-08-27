using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Solutech.Data.Models;
using Solutech.Model;

namespace Solutech.Logic
{
    public class UsuarioLogic
    {
        private readonly AppDbContext _dbContext;

        public UsuarioLogic(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<Usuario>>> GetListUsuarios()
        {
            var result = await _dbContext.Usuarios.ToListAsync();

            return new ApiResponse<List<Usuario>>
            {
                Status = 200,
                Success = true,
                Data = result,
                Message = "Exitoso",
            };
        }

        public async Task<ApiResponse<Usuario>> GetUsuario(int id)
        {
            var result = await _dbContext.Usuarios.FirstOrDefaultAsync(x=>x.UsuarioId == id && x.Estado);

            if (result is null)
            {
                return new ApiResponse<Usuario>
                {
                    Status = 404,
                    Success = false,
                    Data = null,
                    Message = "Error",
                };
            }
            return new ApiResponse<Usuario> 
            {
                Status = 200,
                Success = true,
                Data = result,
                Message = "Exitoso",
            };
        }

        public async Task<ApiResponse<string>> LoginUsuario(Login login)
        {
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x=>x.NombreUsuario == login.Usuario && x.Estado);
      
            if (usuario is null)
            {
                return new ApiResponse<string>
                {
                    Status = 404,
                    Success = false,
                    Data = "Token",
                    Message = "Login Erroneo",
                };
            }
            
            var passOk = Utils.VerifyPass(login.Password, usuario.PasswordUsuario!);
            if (!passOk)
            {
                return new ApiResponse<string>
                {
                    Status = 404,
                    Success = false,
                    Data = "Token",
                    Message = "Usuario o password incorrectos",
                };
            }
            return new ApiResponse<string> 
            {
                Status = 200,
                Success = true,
                Data = "Token",
                Message = "Login Exitoso",
            };
        }

        public async Task<ApiResponse<int>> CreateUsuario(Usuario usuario)
        {
            try
            {
                var existeUsuario = await _dbContext.Usuarios.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario && x.Estado);
                if (existeUsuario)
                {
                    return new ApiResponse<int>
                    {
                        Status = 404,
                        Success = false,
                        Data = 0,
                        Message = "Usuario no encontrado",
                    };
                }
                var password = Utils.HashPass(usuario.PasswordUsuario!);
                usuario.PasswordUsuario = password;
                _dbContext.Add(usuario);
                await _dbContext.SaveChangesAsync();
                int id = usuario.UsuarioId;
                return new ApiResponse<int>
                {
                    Status = 200,
                    Success = true,
                    Data = id,
                    Message = "Usuario registrado",
                };
            }
            catch (Exception)
            {
                return new ApiResponse<int>
                {
                    Status = 500,
                    Success = false,
                    Data = 0,
                    Message = "Error de registro",
                };
            }          
        }

        public async Task<ApiResponse<Usuario>> UpdateUsuario(int id, Usuario usuario)
        {
            if (!await _dbContext.Usuarios.AnyAsync(x=>x.UsuarioId == id && x.Estado))
            {
                return new ApiResponse<Usuario>
                {
                    Status = 404,
                    Success = false,
                    Data = null,
                    Message = "Usuario no encontrado",
                };
            }

            _dbContext.Update(usuario);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Usuario>
            {
                Status = 200,
                Success = true,
                Data = usuario,
                Message = "Usuario actualizado",
            };
        }

        public async Task<ApiResponse<Usuario>> DeleteUsuario(int id)
        {         
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.UsuarioId == id && x.Estado);
            if (usuario is null)
            {
                return new ApiResponse<Usuario>
                {
                    Status = 404,
                    Success = false,
                    Data = null,
                    Message = "Usuario no encontrado",
                };
            }
            usuario.Estado = false;
            usuario.FechaModificacion = DateTime.Now;

            _dbContext.Update(usuario);
            await _dbContext.SaveChangesAsync();
            return new ApiResponse<Usuario>
            {
                Status = 200,
                Success = true,
                Data = usuario,
                Message = "Usuario eliminado",
            };
        }
    }
}
