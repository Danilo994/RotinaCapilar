using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Repositories
{
    public class RepositoryFoto : RepositoryBase<Foto>
    {
        private readonly IWebHostEnvironment _env;
        public RepositoryFoto(DBRotinaCapilarContext context, IWebHostEnvironment env) : base(context)
        {
            _env = env;
        }

        public async Task<Foto> UploadFoto(int idCuidado, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");

            // Caminho físico da pasta uploads
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Gera nome único pro arquivo
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Salva o arquivo fisicamente
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Cria registro no banco
            var foto = new Foto
            {
                IdCuidado = idCuidado,
                UrlImagem = $"/uploads/{fileName}",
                DataUpload = DateTime.Now
            };

            await _context.Fotos.AddAsync(foto);
            await _context.SaveChangesAsync();

            return foto;
        }
    }
}
