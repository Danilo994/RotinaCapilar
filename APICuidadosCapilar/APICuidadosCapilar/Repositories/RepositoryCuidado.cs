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
                .Include(c => c.Avaliacaos)
                .ToListAsync();

            var resultado = cuidados.Select(c => new CuidadoVM
            {
                idCuidado = c.IdCuidado,
                DataCuidado = c.DataCuidado,
                Lavagem = c.IdLavagemNavigation.NomeLavagem,
                Produtos = c.CuidadoProdutos.Select(cp => cp.IdProdutoNavigation.NomeProduto).ToList(),
                idAvaliacao = c.Avaliacaos.FirstOrDefault()?.IdAvaliacao,
                Nota = c.Avaliacaos.FirstOrDefault()?.Nota,
                Observacao = c.Avaliacaos.FirstOrDefault()?.Observacao,
                DataAvaliacao = c.Avaliacaos.FirstOrDefault()?.DataAvaliacao
            }).ToList();

            return resultado;
        }

        public async Task ExcluirAsync(Cuidado cuidado)
        {
            var entity = await _context.Cuidados
                .Include(c => c.CuidadoProdutos)
                .Include(c => c.Fotos)
                .Include(c => c.Avaliacaos)
                .FirstOrDefaultAsync(c => c.IdCuidado == cuidado.IdCuidado);

            if (entity != null)
            {
                _context.CuidadoProdutos.RemoveRange(entity.CuidadoProdutos);
                _context.Fotos.RemoveRange(entity.Fotos);
                _context.Avaliacaos.RemoveRange(entity.Avaliacaos);
                _context.Cuidados.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
