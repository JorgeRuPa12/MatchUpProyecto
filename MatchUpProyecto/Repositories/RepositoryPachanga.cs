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

        public async Task InsertPachangaAsync(Pachanga pachanga, int idequipo)
        {
            int maxIdPachanga = await this.context.Pachangas.AnyAsync()
                ? await this.context.Pachangas.MaxAsync(x => x.Id)
                : 0;

            int maxIPartido = await this.context.Partidos.AnyAsync()
                ? await this.context.Partidos.MaxAsync(x => x.Id)
                : 0;

            int iddeporte = await this.context.Equipos
                .Where(e => e.Id == idequipo)
                .Select(e => e.Deporte)
                .FirstOrDefaultAsync();

            if (iddeporte != 0)
            {
                Deporte deporte = await this.context.Deportes
                    .Where(z => z.Id == iddeporte)
                    .FirstOrDefaultAsync();

                Pachanga pachangaI = new Pachanga
                {
                    Id = maxIdPachanga + 1,
                    Nombre = pachanga.Nombre,
                    Ganador = pachanga.Ganador,
                    Deporte = iddeporte,
                    UbiLatitud = pachanga.UbiLatitud,
                    UbiLongitud = pachanga.UbiLongitud,
                    UbiProvincia = pachanga.UbiProvincia,
                    Inscripcion = pachanga.Inscripcion,
                    Estado = pachanga.Estado,
                    Acceso = pachanga.Acceso,
                    Fecha = pachanga.Fecha,
                };
                await this.context.Pachangas.AddAsync(pachangaI);

                Partido partido = new Partido
                {
                    Id = maxIPartido + 1,
                    Fecha = pachanga.Fecha,
                    EquipoLocal = idequipo,
                    EquipoVisitante = null,
                    Resultado = "0",
                    UbiLatitud = pachanga.UbiLatitud,
                    UbiLongitud = pachanga.UbiLongitud,
                    UbiProvincia = pachanga.UbiProvincia,
                    Tiempo = deporte.Tiempo
                };
                await this.context.Partidos.AddAsync(partido);
                await this.context.SaveChangesAsync();  // Guarda primero el partido

                PachangaPartido PP = new PachangaPartido
                {
                    IdPachanga = pachangaI.Id,
                    IdPartido = partido.Id,  // Ahora el Id es válido en la BD
                    Estado = "Pendiente"
                };

                await this.context.PachangaPartido.AddAsync(PP);
                await this.context.SaveChangesAsync();
            }
            await this.context.SaveChangesAsync();
        }

        public async Task<List<PartidoEquipos>> ObtenerPartidosPorPachanga()
        {

            List<Partido> partidos = await this.context.Partidos.ToListAsync();
            List<PartidoEquipos> partidosLista = new List<PartidoEquipos>();

            foreach(Partido par in partidos)
            {
                Equipo local = await this.context.Equipos.Where(z => z.Id == par.EquipoLocal).FirstOrDefaultAsync();
                Equipo visitante = await this.context.Equipos.Where(z => z.Id == par.EquipoVisitante).FirstOrDefaultAsync();
                PachangaPartido pp = await this.context.PachangaPartido.Where(z => z.IdPartido == par.Id).FirstOrDefaultAsync();
                Pachanga pachanga = await this.context.Pachangas.Where(z => z.Id == pp.IdPachanga).FirstOrDefaultAsync();
                PartidoEquipos pe = new PartidoEquipos
                {
                    Match = par,
                    Pacha = pachanga,
                    Local = local,
                    Visitante = visitante
                };
                partidosLista.Add(pe);
            }

            return partidosLista;
        }

    }
}
