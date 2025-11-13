using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryAvaliacao : RepositoryBase<Avaliacao>
    {
        private readonly IWebHostEnvironment _env;
        public RepositoryAvaliacao(DBRotinaCapilarContext context, IWebHostEnvironment env) : base(context)
        {
            _env = env;
        }

        public async Task<ActionResult<Avaliacao>> AvaliarCuidado(Avaliacao avaliacao)
        {
            var existe = await _context.Avaliacaos.FirstOrDefaultAsync(a => a.IdCuidado == avaliacao.IdCuidado);

            if (existe != null)
            {
                existe.Nota = avaliacao.Nota;
                existe.Observacao = avaliacao.Observacao;
                existe.DataAvaliacao = DateTime.Now;
                _context.Avaliacaos.Update(existe);
            }
            else
            {
                avaliacao.DataAvaliacao = DateTime.Now;
                await _context.Avaliacaos.AddAsync(avaliacao);
            }

            await _context.SaveChangesAsync();
            return avaliacao;
        }

        public async Task<bool> DeleteAvaliacao(int idCuidado)
        {
            var avaliacao = await _context.Avaliacaos.FirstOrDefaultAsync(a => a.IdCuidado==idCuidado);
            if(avaliacao == null)
            {
                return false;
            }

            var fotos = await _context.Fotos.Where(f => f.IdCuidado == idCuidado).ToListAsync();

            foreach (var foto in fotos)
            {
                var filePath = Path.Combine(_env.WebRootPath, foto.UrlImagem.TrimStart('/'));
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                _context.Fotos.Remove(foto);
            }

            _context.Avaliacaos.Remove(avaliacao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
