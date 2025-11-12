using APICuidadosCapilar.Interfaces;
using APICuidadosCapilar.Repositories;
using APICuidadosCapilar.DTOs;
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

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFoto([FromForm] UploadFotoDTO dto)
        {
            try
            {
                var foto = await _repositoryFoto.UploadFoto(dto.idCuidado, dto.File);
                return Ok(foto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao enviar a foto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoto(int id)
        {
            try
            {
                var sucesso = await _repositoryFoto.DeletarFoto(id);
                if (!sucesso)
                {
                    return NotFound("Foto não encontrada");
                }
                return Ok("Foto deletada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar foto: {ex.Message}");
            }
        }
    }
}
