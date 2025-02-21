using MatchUpProyecto.Data;
using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchUpProyecto.Repositories
{
    public class RepositoryDeportes
    {
        private DeporteContext context;
        public RepositoryDeportes(DeporteContext context)
        {
            this.context = context;
        }

        public async Task<List<Deporte>> GetDeportes()
        {
            var consulta = from datos in this.context.Deportes
                           select datos;

            return await consulta.ToListAsync();
        }
    }
}
