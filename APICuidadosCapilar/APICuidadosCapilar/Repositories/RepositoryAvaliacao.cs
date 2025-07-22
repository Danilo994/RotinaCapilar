using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryAvaliacao : RepositoryBase<Avaliacao>
    {
        public RepositoryAvaliacao(DBRotinaCapilarContext context) : base(context)
        {

        }
    }
}
