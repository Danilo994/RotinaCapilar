using APICuidadosCapilar.Interfaces;
using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;
using Models.CuidadosCapilar.ViewModel;

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

        [HttpGet]
        public async Task<ActionResult<List<CuidadoVM>>> GetProdutoByCuidado()
        {
            try
            {
                var cuidados = await _repositoryCuidado.GetCuidadosComProdutos();
                if (cuidados == null)
                    return NotFound("Nenhum cuidado encontrado");

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
                return Ok(cuidado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar cuidado: {ex.Message}");
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
                return Ok(cuidado);
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
