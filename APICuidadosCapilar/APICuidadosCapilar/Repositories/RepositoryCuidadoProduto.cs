using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryCuidadoProduto : RepositoryBase<CuidadoProduto>
    {
        public RepositoryCuidadoProduto(DBRotinaCapilarContext context) : base(context)
        {

        }
    }
}
