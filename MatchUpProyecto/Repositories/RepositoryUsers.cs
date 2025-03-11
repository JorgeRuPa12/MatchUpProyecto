using MatchUpProyecto.Data;
using MatchUpProyecto.Helpers;
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

        public async Task InsertUser(User user,string password)
        {
            int maxId = await this.context.Users.AnyAsync()
                ? await this.context.Users.MaxAsync(x => x.Id)
                : 0;
            string salt = HelperCryptography.GenerateSalt();
            User userObj = new User
            {
                Id = maxId + 1,
                Nombre = user.Nombre,
                Email = user.Email,
                Imagen = user.Imagen,
                Salt = salt,
                Pass = HelperCryptography.EncryptPassword(password, salt),
                Rol = user.Rol,

            };
            await this.context.Users.AddAsync(userObj);
            await this.context.SaveChangesAsync();
        }

        public async Task<User> LogInEmpleadosAsync(string correo, string password)
        {
            User user = await this.context.Users.Where(x => x.Email == correo).FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                byte[] passBytes = user.Pass;
                bool response = HelperCryptography.CompararArrays(temp, passBytes);
                if(response == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
