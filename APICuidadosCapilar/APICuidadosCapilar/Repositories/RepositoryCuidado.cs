using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.CuidadosCapilar.Model;
using Models.CuidadosCapilar.ViewModel;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryCuidado : RepositoryBase<Cuidado>
    {
        public RepositoryCuidado(DBRotinaCapilarContext context) : base(context) 
        { 
            
        }
        public async Task<ActionResult<List<CuidadoVM>>> GetCuidadosComProdutos()
        {
            var cuidados = await _context.Cuidados
                .Include(c => c.CuidadoProdutos)
                    .ThenInclude(cp => cp.IdProdutoNavigation)
                .Include(c => c.IdLavagemNavigation)
                .ToListAsync();

            var resultado = cuidados.Select(c => new CuidadoVM
            {
                idCuidado = c.IdCuidado,
                DataCuidado = c.DataCuidado,
                Lavagem = c.IdLavagemNavigation.NomeLavagem,
                Produtos = c.CuidadoProdutos.Select(cp => cp.IdProdutoNavigation.NomeProduto).ToList()
            }).ToList();

            return resultado;
        }
    }
}
