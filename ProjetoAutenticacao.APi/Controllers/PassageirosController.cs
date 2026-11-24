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
    public class PassageirosController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public PassageirosController(AppDbContext Context)
        {
            _Context = Context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPassageiro(CriarPassageiroDto dto)
        {
            var novoPassageiro = new Passageiro
            {
                Cpf = dto.Cpf,
                Nome = dto.Nome
            };
            _Context.Passageiros.Add(novoPassageiro);
            await _Context.SaveChangesAsync();
            return Ok("Passageiro Registradi");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassageiroInfoDto>>> GetPassageiro([FromQuery] string? Nome)
        {
            var passageiros = await _Context.Passageiros.AsNoTracking().Where(p => string.IsNullOrEmpty(Nome) || p.Nome.Contains(Nome)).OrderBy(p => p.Nome).Select(p => new PassageiroInfoDto { Id = p.Id, Nome = p.Nome }).ToListAsync();
            return Ok(passageiros);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<PassageiroInfoDto>> GetPassageiroCpf(string cpf)
        {
            var passageiro = await _Context.Passageiros.AsNoTracking().Where(p => p.Cpf == cpf).Select(p => new PassageiroInfoDto { Id = p.Id, Nome = p.Nome }).FirstOrDefaultAsync();

            if (passageiro is null)
            {
                return NotFound(new { mensagem = "passageiro não encontrado" });

            }

            return Ok(passageiro);

        }
        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<VooManifestoDto>> GetVooCodigo(int codigo)
        {
            var manifesto = await _Context.Voos
                .AsNoTracking()
                .Where(v => v.Codigo == codigo)
                .Select(v => new VooManifestoDto
                {
                    id = v.Id,
                    Codigo = v.Codigo,
                    Trajeto = v.Origem + " -> " + v.Destino,
                    Passageiros = v.Passagens.Select(p => new PassageirosVooDto
                    {
                        Nome = p.Passageiro.Nome,
                        Assento = p.Assento
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (manifesto == null)
                return NotFound();

            return Ok(manifesto);
        }
    }
}