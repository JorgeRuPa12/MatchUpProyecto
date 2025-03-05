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
        public UserController(RepositoryUsers repo)
        {
            this.repo = repo;
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
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
