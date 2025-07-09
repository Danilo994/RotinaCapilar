using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryFoto : RepositoryBase<Foto>
    {
        public RepositoryFoto(bool saveChanges = true) : base(saveChanges) 
        {

        }
    }
}
