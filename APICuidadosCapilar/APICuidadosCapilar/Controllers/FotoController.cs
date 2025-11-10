using APICuidadosCapilar.Interfaces;
using APICuidadosCapilar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models.CuidadosCapilar.Model;

namespace APICuidadosCapilar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotoController : ControllerBase
    {
        RepositoryFoto _repositoryFoto;
        public readonly DBRotinaCapilarContext _context;

        public FotoController(DBRotinaCapilarContext context, IWebHostEnvironment env)
        {
            _repositoryFoto = new RepositoryFoto(context, env);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Foto>>> ListaFotos()
        {
            try
            {
                var fotos = await _repositoryFoto.SelecionarTodosAsync();
                return Ok(fotos);
            }
            catch
            {
                return BadRequest("Erro ao retornar lista de fotos");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Foto>> GetFoto(int id)
        {
            try
            {
                var foto = await _repositoryFoto.SelecionarPkAsync(id);
                return Ok(foto);
            }
            catch
            {
                return BadRequest("Erro ao retornar foto");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Foto>> AddFoto(Foto foto)
        {
            try
            {
                foto.DataUpload = DateTime.Now;
                await _repositoryFoto.IncluirAsync(foto);
                return Ok("Foto adicionado");
            }
            catch
            {
                return BadRequest("Erro ao adicionar foto");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Foto>> DeleteFoto(int id)
        {
            try
            {
                var foto = await _repositoryFoto.SelecionarPkAsync(id);
                await _repositoryFoto.ExcluirAsync(foto);
                return Ok("Foto excluida");
            }
            catch
            {
                return BadRequest("Erro ao excluir foto");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFoto([FromForm] int idCuidado, [FromForm] IFormFile file)
        {
            try
            {
                var foto = await _repositoryFoto.UploadFoto(idCuidado, file);
                return Ok(foto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao enviar a foto: {ex.Message}");
            }
        }
    }
}
