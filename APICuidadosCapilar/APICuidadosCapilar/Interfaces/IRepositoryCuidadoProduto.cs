using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Interfaces
{
    public interface IRepositoryCuidadoProduto : IRepositoryBase<CuidadoProduto>
    {
        Task<CuidadoProduto> GetProdutosByCuidado(int idCuidado);
        Task<CuidadoProduto> DeleteProdutosByCuidado(int idCuidado);
    }
}
