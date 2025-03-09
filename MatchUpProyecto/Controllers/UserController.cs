using MatchUpProyecto.Extensions;
using MatchUpProyecto.Models;
using MatchUpProyecto.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MatchUpProyecto.Controllers
{
    public class UserController : Controller
    {
        private RepositoryUsers repo;
        private RepositoryEquipos repoE;
        public UserController(RepositoryUsers repo, RepositoryEquipos repoE)
        {
            this.repo = repo;
            this.repoE = repoE;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            user.Imagen = "defaultuser.jpg";
            await this.repo.InsertUser(user);
            return View();
        }

        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string correo, string pass)
        {
            User user = await this.repo.GetUserAsync(correo, pass);
            if (user != null)
            {
                User userObj = new User
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Imagen = user.Imagen,
                    Pass = "0",
                    Rol = user.Rol,
                };
                HttpContext.Session.SetObject("USERINFO", user);
                List<Equipo> misequipos = await this.repoE.GetEquiposUsuarioAysnc(user.Id);
                HttpContext.Session.SetObject("MISEQUIPOS", misequipos);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            User user = HttpContext.Session.GetObject<User>("USERINFO");
            if (user != null)
            {
                HttpContext.Session.Remove("USERINFO");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
