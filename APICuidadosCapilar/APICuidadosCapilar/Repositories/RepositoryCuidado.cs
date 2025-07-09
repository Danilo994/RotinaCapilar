using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryCuidado : RepositoryBase<Cuidado>
    {
        public RepositoryCuidado(bool saveChanges = true) : base(saveChanges) 
        { 
            
        }
    }
}
