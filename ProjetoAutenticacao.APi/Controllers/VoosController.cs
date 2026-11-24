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
    public class VoosController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public VoosController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpPost("Criar")]
        public async Task<IActionResult> CriarVoo(CriarVooDto criarvoo)
        {
            var VooNovo = new Voo
            {
                Codigo = criarvoo.Codigo,
                Destino = criarvoo.Destino,
                Origem = criarvoo.Origem
            };

            _Context.Voos.Add(VooNovo);
            await _Context.SaveChangesAsync();

            return Ok("Voo criado com sucesso");

        }

        [HttpGet]
        async Task<ActionResult<IEnumerable<ResumoVooDto>>> GetVoo()
        {
            var voos = await _Context.Voos.AsNoTracking().OrderBy(v => v.Codigo).Select(v => new ResumoVooDto
            {
                Id = v.Id,
                Codigo = v.Codigo,
                Trajeto = v.Origem + "->" + v.Destino
            }).ToListAsync();
            return Ok(voos);
        }

    }
}
