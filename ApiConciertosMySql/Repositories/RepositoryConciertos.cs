using ApiConciertosMySql.Data;
using ApiConciertosMySql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosMySql.Repositories
{
    public class RepositoryConciertos
    {
        private ConciertosContext context;

        public RepositoryConciertos(ConciertosContext context)
        {
            this.context = context;
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            return await this.context.Eventos.ToListAsync();
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }

        public async Task<Evento> FindEventoPorCategoriaAsync(int idcategoria)
        {
            return await this.context.Eventos
                .FirstOrDefaultAsync(x => x.IdCategoria == idcategoria);
        }

        private async Task<int> GetMaxIdEventoAsync()
        {
            return await this.context.Eventos.MaxAsync(x => x.IdEvento) + 1;
        }

        public async Task CreateEventoAsync(string nombre, string artista, int categoria, string imagen)
        {
            Evento evento = new Evento
            {
                IdEvento = await this.GetMaxIdEventoAsync(),
                Nombre = nombre,
                Artista = artista,
                IdCategoria = categoria,
                Imagen = imagen
            };
            this.context.Eventos.Add(evento);
            await this.context.SaveChangesAsync();
        }
    }
}
