using ApiConciertosMySql.Models;
using ApiConciertosMySql.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiConciertosMySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConciertosController : ControllerBase
    {
        private RepositoryConciertos repo;

        public ConciertosController(RepositoryConciertos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Evento>>> GetEventoList()
        {
            return await this.repo.GetEventosAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Categoria>>> GetCategoriaList()
        {
            return await this.repo.GetCategoriasAsync();
        }

        [HttpGet("FindEventoPorCategoria/{id}")]
        public async Task<ActionResult<Evento>> FindEventoPorCategoria(int id)
        {
            return await this.repo.FindEventoPorCategoriaAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Evento evento)
        {
            await this.repo.CreateEventoAsync
                (evento.Nombre, evento.Artista, evento.IdCategoria, evento.Imagen);
            return Ok();
        }
    }
}
