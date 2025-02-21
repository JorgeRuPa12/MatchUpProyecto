using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchUpProyecto.Data
{
    public class DeporteContext: DbContext
    {
        public DeporteContext(DbContextOptions<DeporteContext> options): base(options) { }

        public DbSet<Deporte> Deportes { get; set; }
    }
}
