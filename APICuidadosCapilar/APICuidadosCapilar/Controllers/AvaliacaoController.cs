using APICuidadosCapilar.Interfaces;
using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        RepositoryAvaliacao _repositoryAvaliacao;
        public readonly DBRotinaCapilarContext _context;

        public AvaliacaoController(DBRotinaCapilarContext context)
        {
            _repositoryAvaliacao = new RepositoryAvaliacao(context);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> ListaAvaliacoes()
        {
            try
            {
                var avaliacoes = await _repositoryAvaliacao.SelecionarTodosAsync();
                return Ok(avaliacoes);
            }
            catch
            {
                return BadRequest("Erro ao retornar lista de avaliações");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(int id)
        {
            try
            {
                var avaliacao = await _repositoryAvaliacao.SelecionarPkAsync(id);
                return Ok(avaliacao);
            }
            catch
            {
                return BadRequest("Erro ao retornar avaliação");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Avaliacao>> AddAvaliacao(Avaliacao avaliacao)
        {
            try
            {
                avaliacao.DataAvaliacao = DateTime.Now;
                await _repositoryAvaliacao.IncluirAsync(avaliacao);
                return Ok("Avaliação adicionado");
            }
            catch
            {
                return BadRequest("Erro ao adicionar avaliação");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Avaliacao>> DeleteAvaliacao(int id)
        {
            try
            {
                var avaliacao = await _repositoryAvaliacao.SelecionarPkAsync(id);
                await _repositoryAvaliacao.ExcluirAsync(avaliacao);
                return Ok("Avaliacao excluida");
            }
            catch
            {
                return BadRequest("Erro ao excluir avaliação");
            }
        }
    }
}
