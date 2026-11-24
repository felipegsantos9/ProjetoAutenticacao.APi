using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacao.Api;
using ProjetoAutenticacao.APi.Models;
using ProjetoAutenticacao.APi.Models.DTOs;

namespace ProjetoAutenticacao.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public AuthController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar (RegistroDto registroDto)
        {
            var novoUsuario = new Usuario
            {
                Nome = registroDto.Nome,
                Login = registroDto.Login,
                Senha = registroDto.Senha,
                CreatedAt = DateTime.Now
            };

            _Context.Usuarios.Add(novoUsuario);
            await _Context.SaveChangesAsync();

            return Ok("Usuario cadastrado com sucesso");

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var usuario = await _Context.Usuarios.FirstOrDefaultAsync(u => u.Login == loginDto.Login && u.Senha == loginDto.Senha);

            if(usuario == null)
            {
                return NotFound("Usuario ou Senha invalido");
            }

            return Ok(new { Nome = usuario.Nome });
        }

    }
}
