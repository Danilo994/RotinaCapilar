using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryProduto : RepositoryBase<Produto>
    {
        public RepositoryProduto(DBRotinaCapilarContext context) : base(context)
        {

        }
    }
}
