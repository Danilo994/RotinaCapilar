using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryLavagem : RepositoryBase<Lavagem>
    {
        public RepositoryLavagem(bool saveChanges = true) : base(saveChanges) 
        {

        }
    }
}
