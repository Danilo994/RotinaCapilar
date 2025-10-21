using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;
using Models.CuidadosCapilar.ViewModel;

namespace APICuidadosCapilar.Interfaces
{
    public interface IRepositoryAvaliacao : IRepositoryBase<Avaliacao>
    {
        Task<ActionResult<List<Avaliacao>>> AvaliarCuidado(Avaliacao avaliacao);
        Task<ActionResult> DeleteAvaliacao(int idCuidado);
    }
}
