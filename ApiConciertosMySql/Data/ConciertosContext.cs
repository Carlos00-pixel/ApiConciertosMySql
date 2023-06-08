using ApiConciertosMySql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosMySql.Data
{
    public class ConciertosContext : DbContext
    {
        public ConciertosContext(DbContextOptions<ConciertosContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
