using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryFoto : RepositoryBase<Foto>
    {
        public RepositoryFoto(DBRotinaCapilarContext context) : base(context)
        {

        }
    }
}
