using Microsoft.EntityFrameworkCore;
using WebApiFutbolistas.Entities;

namespace WebApiFutbolistas.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op):base(op)
        {

        }

    public DbSet<Jugador> Jugadores { get; set; }
    public DbSet<Posicion> Posiciones { get; set; }
    public DbSet<Continente> Continentes { get; set; }
    }
}
