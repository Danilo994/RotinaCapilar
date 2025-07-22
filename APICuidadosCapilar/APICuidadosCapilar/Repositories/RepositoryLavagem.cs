using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryLavagem : RepositoryBase<Lavagem>
    {
        public RepositoryLavagem(DBRotinaCapilarContext context) : base(context)
        {

        }
    }
}
