using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryCuidado : RepositoryBase<Cuidado>
    {
        public RepositoryCuidado(DBRotinaCapilarContext context) : base(context) 
        { 
            
        }
    }
}
