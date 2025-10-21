using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;
using Models.CuidadosCapilar.ViewModel;

namespace APICuidadosCapilar.Interfaces
{
    public interface IRepositoryCuidado : IRepositoryBase<Cuidado>
    {
        Task<ActionResult<List<CuidadoVM>>> GetCuidadosComProdutos();
    }
}
