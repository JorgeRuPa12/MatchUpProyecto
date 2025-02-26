using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchUpProyecto.Data
{
    public class PachangaContext: DbContext
    {
        public PachangaContext(DbContextOptions<PachangaContext> options) : base(options) { }

        public DbSet<Pachanga> Pachangas { get; set; }
    }
}
