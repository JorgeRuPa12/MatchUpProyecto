using MatchUpProyecto.Data;
using MatchUpProyecto.Models;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreSession.Helpers;
using System;

namespace MatchUpProyecto.Repositories
{
    public class RepositoryUsers
    {
        private MatchUpContext context;
        private HelperSessionContextAccesor contextAccesor;
        public RepositoryUsers(MatchUpContext context, HelperSessionContextAccesor contextAccesor)
        {
            this.context = context;
            this.contextAccesor = contextAccesor;
        }

        public async Task InsertUser(User user)
        {
            int maxId = await this.context.Users.AnyAsync()
                ? await this.context.Users.MaxAsync(x => x.Id)
                : 0;

            User userObj = new User
            {
                Id = maxId + 1,
                Nombre = user.Nombre,
                Email = user.Email,
                Imagen = user.Imagen,
                Pass = user.Pass,
                Rol = user.Rol,

            };
            await this.context.Users.AddAsync(userObj);
            await this.context.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string correo, string password)
        {
            var consulta =  this.context.Users.Where(z => z.Email == correo && z.Pass == password).FirstOrDefaultAsync();

            return  await consulta;
        }
    }
}
