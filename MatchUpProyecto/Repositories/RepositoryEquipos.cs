using MatchUpProyecto.Data;
using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchUpProyecto.Repositories
{
    public class RepositoryEquipos
    {
        private MatchUpContext context;
        public RepositoryEquipos(MatchUpContext context)
        {
            this.context = context;
        }

        public async Task<List<Equipo>> GetEquiposAsync()
        {
            return await this.context.Equipos.ToListAsync();
        }

        public async Task InsertEquipoAsync(Equipo equipo)
        {
            int maxId = await this.context.Equipos.AnyAsync()
                ? await this.context.Equipos.MaxAsync(x => x.Id)
                : 0;

            Equipo equipoObj = new Equipo
            {
                Id = maxId + 1,
                Nombre = equipo.Nombre,
                Color = equipo.Color,
                Deporte = equipo.Deporte,
                Emblema = equipo.Emblema,
                IdAdmin = equipo.IdAdmin,
            };

            UsuarioEquipo usuarioEquipoObj = new UsuarioEquipo
            {
                IdUsuario = equipoObj.IdAdmin,
                IdEquipo = equipoObj.Id
            };

            await this.context.AddAsync(equipoObj);
            await this.context.AddAsync(usuarioEquipoObj);

            await this.context.SaveChangesAsync();
        }

        public async Task<List<Equipo>> GetEquiposUsuarioAysnc(int idusuario)
        {
            return await (from ue in this.context.UsuariosEquipo
                          join e in this.context.Equipos on ue.IdEquipo equals e.Id
                          where ue.IdUsuario == idusuario
                          select e).ToListAsync();
        }

        public async Task<EquipoDetalle> GetEquipoDetalleAsync(int idequipo)
        {
            var jugadores = await (from ue in this.context.UsuariosEquipo
                           join u in this.context.Users on ue.IdUsuario equals u.Id
                           where ue.IdEquipo == idequipo
                           select u).ToListAsync();

            int numJugadores = jugadores.Count();
            Equipo team = await this.context.Equipos.Where(z => z.Id == idequipo).FirstOrDefaultAsync();

            EquipoDetalle equipoDetalle = new EquipoDetalle
            {
                Jugadores = jugadores,
                Detalles = team,
                NumJugadores = numJugadores
            };
            return equipoDetalle;
        }

        public async Task UnirseEquipoAsync(int idequipo, int idusuario)
        {
            UsuarioEquipo model = new UsuarioEquipo
            {
                IdEquipo = idequipo,
                IdUsuario = idusuario
            };

            await this.context.UsuariosEquipo.AddAsync(model);
            await this.context.SaveChangesAsync();
        }

        public async Task SalirseEquipoAsync(int idequipo, int idusuario)
        {
            UsuarioEquipo model = new UsuarioEquipo
            {
                IdEquipo = idequipo,
                IdUsuario = idusuario
            };

            this.context.UsuariosEquipo.Remove(model);
            await this.context.SaveChangesAsync();
        }
    }
}
