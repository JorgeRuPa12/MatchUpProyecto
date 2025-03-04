using MatchUpProyecto.Data;
using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchUpProyecto.Repositories
{
    public class RepositoryPachanga
    {
        private MatchUpContext context;
        public RepositoryPachanga(MatchUpContext context)
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
            int maxId = await this.context.Pachangas.AnyAsync()
                ? await this.context.Pachangas.MaxAsync(x => x.Id)
                : 0;

            Pachanga pachangaI = new Pachanga
            {
                Id = maxId + 1,
                Nombre = pachanga.Nombre,
                Ganador = pachanga.Ganador,
                Deporte = pachanga.Deporte,
                UbiLatitud = pachanga.UbiLatitud,
                UbiLongitud = pachanga.UbiLongitud,
                UbiProvincia = pachanga.UbiProvincia,
                Inscripcion = pachanga.Inscripcion,
                Estado = pachanga.Estado,
                Acceso = pachanga.Acceso,
                Fecha = pachanga.Fecha,
            };
            await this.context.Pachangas.AddAsync(pachangaI);
            await this.context.SaveChangesAsync();
        }
    }
}
