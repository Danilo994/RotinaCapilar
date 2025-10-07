using Microsoft.EntityFrameworkCore;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryCuidadoProduto : RepositoryBase<CuidadoProduto>
    {
        public RepositoryCuidadoProduto(DBRotinaCapilarContext context) : base(context)
        {

        }

        public async Task<List<CuidadoProduto>> GetProdutosByCuidado(int idCuidado)
        {
            return await _context.CuidadoProdutos
                .Include(c => c.IdProdutoNavigation)
                .Where(c => c.IdCuidado == idCuidado)
                .ToListAsync();
        }

        public async Task<List<CuidadoProduto>> DeleteProdutosByCuidado(int idCuidado)
        {
            var lista = _context.CuidadoProdutos.Where(c => c.IdCuidado==idCuidado).ToList();

            _context.CuidadoProdutos.RemoveRange(lista);
            await _context.SaveChangesAsync();

            return lista;
        }
    }
}
