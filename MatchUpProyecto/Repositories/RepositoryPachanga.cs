using MatchUpProyecto.Data;
using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchUpProyecto.Repositories
{
    public class RepositoryPachanga
    {
        private PachangaContext context;
        public RepositoryPachanga( PachangaContext context)
        {
            this.context = context;
        }

        public async Task<List<Pachanga>> GetPachangasAsync()
        {
            var consulta = from datos in this.context.Pachangas
                           select datos;

            return await consulta.ToListAsync();
        }

        public async Task InsertPachangaAsync(Pachanga pachanga)
        {
            var consulta = from datos in this.context.Pachangas
                           select datos;
            int maxId = 0;

            if(consulta.ToListAsync() != null)
            {
                maxId = consulta.Max(x => x.Id);
            }
            
            Pachanga pachangaI = new Pachanga
            {
                Id = maxId + 1,
                Ganador = pachanga.Ganador,
                Deporte = pachanga.Deporte,
                UbiLatitud = pachanga.UbiLatitud,
                UbiLongitud = pachanga.UbiLongitud,
                UbiProvincia = pachanga.UbiProvincia,
                Inscripcion = pachanga.Inscripcion,
                Acceso = pachanga.Acceso,
            };
            await this.context.Pachangas.AddAsync(pachangaI);
            await this.context.SaveChangesAsync();
        }
    }
}
