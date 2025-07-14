using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Controllers
{
    public class LavagemController : ControllerBase
    {
        RepositoryLavagem _repositoryLavagem;
        private readonly DBRotinaCapilarContext _context;

        public LavagemController(DBRotinaCapilarContext context)
        {
            _repositoryLavagem = new RepositoryLavagem();
            _context = context;
        }

        //lista lavagens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lavagem>>> ListaLavagens()
        {
            try
            {
                var lavagens = await _repositoryLavagem.SelecionarTodosAsync();
                return Ok(lavagens);
            }
            catch
            {
                return BadRequest("Erro ao retornar lista de lavagens");
            }
        }

        //escolhe lavagem
        [HttpGet("{id}")]
        public async Task<ActionResult<Lavagem>> GetLavagem(int id)
        {
            try
            {
                var lavagem = await _repositoryLavagem.SelecionarPkAsync(id);
                return Ok(lavagem);
            }
            catch
            {
                return BadRequest("Erro ao retornar lavagem");
            }
        }

        //adiciona lavagem
        [HttpPost]
        public async Task<ActionResult<Lavagem>> AddLavagem(Lavagem lavagem)
        {
            try
            {
                await _repositoryLavagem.IncluirAsync(lavagem);
                return Ok("Lavagem adicionado");
            }
            catch
            {
                return BadRequest("Erro ao adicionar lavagem");
            }
        }

        //edita lavagem
        [HttpPut]
        public async Task<ActionResult<Lavagem>> EditLavagem(Lavagem lavagem)
        {
            try
            {
                await _repositoryLavagem.AlterarAsync(lavagem);
                return Ok("Lavagem editado");
            }
            catch
            {
                return BadRequest("Erro ao editar lavagem");
            }
        }

        //exclui lavagem
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lavagem>> DeleteLavagem(int id)
        {
            try
            {
                var lavagem = await _repositoryLavagem.SelecionarPkAsync(id);
                await _repositoryLavagem.ExcluirAsync(lavagem);
                return Ok("Lavagem excluida");
            }
            catch
            {
                return BadRequest("Erro ao excluir lavagem");
            }
        }
    }
}
