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
                    Resultado = "Por Determinar",
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

        public async Task<List<PartidoEquipos>> GetPartidosDelMesActual(int idUsuario)
        {
            DateTime primerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            // Obtener los equipos del usuario
            var equiposUsuario = await this.context.UsuariosEquipo
                .Where(ue => ue.IdUsuario == idUsuario)
                .Select(ue => ue.IdEquipo)
                .ToListAsync();

            // Obtener las pachangas del mes donde el usuario tenga un equipo
            List<Pachanga> pachangas = await this.context.Pachangas
            .Where(p => p.Fecha >= primerDiaDelMes && p.Fecha <= ultimoDiaDelMes)
            .Join(this.context.PachangaPartido,
                  pach => pach.Id,
                  pp => pp.IdPachanga,
                  (pach, pp) => new { pach, pp })
            .Join(this.context.Partidos,
                  temp => temp.pp.IdPartido,
                  partido => partido.Id,
                  (temp, partido) => new { temp.pach, partido })
            .Where(temp => equiposUsuario.Contains(temp.partido.EquipoLocal)
                        || (temp.partido.EquipoVisitante.HasValue && equiposUsuario.Contains(temp.partido.EquipoVisitante.Value)))
            .OrderBy(temp => temp.pach.Fecha)
            .Select(temp => temp.pach)  // Solo seleccionamos Pachanga
            .ToListAsync();


            List<PartidoEquipos> partidosLista = new List<PartidoEquipos>();

            foreach (Pachanga pac in pachangas)
            {
                PachangaPartido pp = await this.context.PachangaPartido.Where(z => z.IdPachanga == pac.Id).FirstOrDefaultAsync();
                Partido partido = await this.context.Partidos.Where(z => z.Id == pp.IdPartido).FirstOrDefaultAsync();
                Equipo local = await this.context.Equipos.Where(z => z.Id == partido.EquipoLocal).FirstOrDefaultAsync();
                Equipo visitante = await this.context.Equipos.Where(z => z.Id == partido.EquipoVisitante).FirstOrDefaultAsync();
                PartidoEquipos pe = new PartidoEquipos
                {
                    Match = partido,
                    Pacha = pac,
                    Local = local,
                    Visitante = visitante
                };
                partidosLista.Add(pe);
            }

            return partidosLista;
        }

        public async Task ActualizarResultadoAsync(int local, int visitante, int idpartido)
        {
            Partido partido = await this.context.Partidos.Where(z => z.Id == idpartido).FirstOrDefaultAsync();

            partido.Resultado = local + " - " + visitante;

            await this.context.SaveChangesAsync();
        }


    }
}
