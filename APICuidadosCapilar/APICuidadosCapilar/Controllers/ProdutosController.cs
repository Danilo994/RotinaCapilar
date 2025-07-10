using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Controllers
{
    public class ProdutosController : ControllerBase
    {
        RepositoryProduto _repositoryProduto;
        private readonly DBRotinaCapilarContext _context;

        public ProdutosController(DBRotinaCapilarContext context)
        {
            _repositoryProduto = new RepositoryProduto();
            _context = context;
        }

        //lista produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ListaProdutos()
        {
            try
            {
                var produtos = await _repositoryProduto.SelecionarTodosAsync();
                return Ok(produtos);
            }
            catch
            {
                return BadRequest("Erro ao retornar lista de produtos");
            }
        }

        //escolhe produto
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            try
            {
                var produto = await _repositoryProduto.SelecionarPkAsync(id);
                return Ok(produto);
            }
            catch
            {
                return BadRequest("Erro ao retornar produto");
            }
        }

        //adiciona produto
        [HttpPost]
        public async Task<ActionResult<Produto>> AddProduto(Produto produto)
        {
            try
            {
                await _repositoryProduto.IncluirAsync(produto);
                return Ok("Produto adicionado");
            }
            catch
            {
                return BadRequest("Erro ao adicionar produto");
            }
        }

        //edita produto
        [HttpPut]
        public async Task<ActionResult<Produto>> EditProduto(Produto produto)
        {
            try
            {
                await _repositoryProduto.AlterarAsync(produto);
                return Ok("Produto editado");
            }
            catch
            {
                return BadRequest("Erro ao editar produto");
            }
        }

        //exclui produto
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            try
            {
                var produto = await _repositoryProduto.SelecionarPkAsync(id);
                await _repositoryProduto.ExcluirAsync(produto);
                return Ok("Produto excluido");
            }
            catch
            {
                return BadRequest("Erro ao excluir produto");
            }
        }
    }
}
