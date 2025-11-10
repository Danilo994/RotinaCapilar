using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Interfaces
{
    public interface IRepositoryFoto : IRepositoryBase<Foto>
    {
        Task<Foto> UploadFoto(int idCuidado, IFormFile file);
    }
}
