using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuidadoController : ControllerBase
    {
        RepositoryCuidado _repositoryCuidado;
        private readonly DBRotinaCapilarContext _context;

        public CuidadoController(DBRotinaCapilarContext context)
        {
            _repositoryCuidado = new RepositoryCuidado(context);
            _context = context;
        }

        //lista Cuidados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuidado>>> ListaCuidados()
        {
            try
            {
                var cuidados = await _repositoryCuidado.SelecionarTodosAsync();
                return Ok(cuidados);
            }
            catch
            {
                return BadRequest("Erro ao retornar lista de cuidados");
            }
        }

        //escolhe Cuidado
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuidado>> GetCuidado(int id)
        {
            try
            {
                var cuidado = await _repositoryCuidado.SelecionarPkAsync(id);
                return Ok(cuidado);
            }
            catch
            {
                return BadRequest("Erro ao retornar cuidado");
            }
        }

        //adiciona Cuidado
        [HttpPost]
        public async Task<ActionResult<Cuidado>> AddCuidado(Cuidado cuidado)
        {
            try
            {
                cuidado.DataCriacao = DateTime.Now;
                cuidado.DataModificacao = DateTime.Now;
                await _repositoryCuidado.IncluirAsync(cuidado);
                return Ok("Cuidado adicionado");
            }
            catch
            {
                return BadRequest("Erro ao adicionar cuidado");
            }
        }

        //edita Cuidado
        [HttpPut]
        public async Task<ActionResult<Cuidado>> EditLavagem(Cuidado cuidado)
        {
            try
            {
                cuidado.DataModificacao = DateTime.Now;
                await _repositoryCuidado.AlterarAsync(cuidado);
                return Ok("Cuidado editado");
            }
            catch
            {
                return BadRequest("Erro ao editar cuidado");
            }
        }

        //exclui Cuidado
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cuidado>> DeleteCuidado(int id)
        {
            try
            {
                var cuidado = await _repositoryCuidado.SelecionarPkAsync(id);
                await _repositoryCuidado.ExcluirAsync(cuidado);
                return Ok("Cuidado excluida");
            }
            catch
            {
                return BadRequest("Erro ao excluir cuidado");
            }
        }
    }
}
