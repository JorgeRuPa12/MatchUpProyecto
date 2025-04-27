using MatchUpProyecto.Extensions;
using NugetMatchUp.Models;

namespace MvcNetCoreSession.Helpers
{
    public class HelperSessionContextAccesor
    {
        private IHttpContextAccessor contextAccessor;

        public HelperSessionContextAccesor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public User GetUserSession()
        {
            User user = this.contextAccessor.HttpContext.Session.GetObject<User>("USERINFO");
            return user;
        }
    }
}