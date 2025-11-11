using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICuidadosCapilar.DTOs
{
    public class UploadFotoDTO
    {
        [FromForm(Name = "idCuidado")]
        public int idCuidado { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; } = null!;
    }
}