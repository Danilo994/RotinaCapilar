using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Controllers
{
    public class CuidadoProdutoController : ControllerBase
    {
        RepositoryCuidadoProduto _repositoryCuidadoProduto;
        public readonly DBRotinaCapilarContext _context;

        public CuidadoProdutoController(DBRotinaCapilarContext context)
        {
            _repositoryCuidadoProduto = new RepositoryCuidadoProduto();
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuidadoProduto>>> ListaCuidadoProduto()
        {
            try
            {
                var lista = await _repositoryCuidadoProduto.SelecionarTodosAsync();
                return Ok(lista);
            }
            catch
            {
                return BadRequest("Erro ao retornar lista de CuidadoProduto");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuidadoProduto>> GetCuidadoProduto(int id)
        {
            try
            {
                var cuidadoProduto = await _repositoryCuidadoProduto.SelecionarPkAsync(id);
                return Ok(cuidadoProduto);
            }
            catch
            {
                return BadRequest("Erro ao retornar CuidadoProduto");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CuidadoProduto>> AddCuidadoProduto(CuidadoProduto cuidadoProduto)
        {
            try
            {
                await _repositoryCuidadoProduto.IncluirAsync(cuidadoProduto);
                return Ok("CuidadoProduto adicionado");
            }
            catch
            {
                return BadRequest("Erro ao adicionar CuidadoProduto");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CuidadoProduto>> DeleteCuidadoProduto(int id)
        {
            try
            {
                var item = await _repositoryCuidadoProduto.SelecionarPkAsync(id);
                await _repositoryCuidadoProduto.ExcluirAsync(item);
                return Ok("CuidadoProduto excluida");
            }
            catch
            {
                return BadRequest("Erro ao excluir CuidadoProduto");
            }
        }
    }
}
