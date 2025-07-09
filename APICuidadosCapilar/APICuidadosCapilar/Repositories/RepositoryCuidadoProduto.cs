using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryCuidadoProduto : RepositoryBase<CuidadoProduto>
    {
        public RepositoryCuidadoProduto(bool saveChanges = true) : base(saveChanges)
        {

        }
    }
}
