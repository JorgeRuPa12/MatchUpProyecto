using MatchUpProyecto.Extensions;
using MatchUpProyecto.Models;
using MatchUpProyecto.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using MatchUpProyecto.Filters;

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

        [AuthorizeUser]
        public IActionResult Perfil()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string password)
        {
            user.Imagen = "defaultuser.jpg";
            await this.repo.InsertUser(user, password);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string correo, string pass)
        {
            User user = await this.repo.LogInEmpleadosAsync(correo, pass);
            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role
                    );
                Claim claimName = new Claim(ClaimTypes.Name, user.Nombre);
                identity.AddClaim(claimName);
                Claim claimId = new Claim(("Id"), user.Id.ToString());
                identity.AddClaim(claimId);
                Claim claimRol = new Claim(ClaimTypes.Role, user.Rol);
                identity.AddClaim(claimRol);
                Claim claimEmail = new Claim(ClaimTypes.Email, user.Email);
                identity.AddClaim(claimEmail);
                Claim claimImagen = new Claim("Imagen", user.Imagen);
                identity.AddClaim(claimImagen);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal
                    );
                return RedirectToAction("Perfil");
            }
            else
            {
                ViewData["MENSAJE"] = "Credenciales incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
