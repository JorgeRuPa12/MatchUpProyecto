using MatchUpProyecto.Extensions;
using NugetMatchUp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using MatchUpProyecto.Filters;
using MatchUpProyecto.Services;

namespace MatchUpProyecto.Controllers
{
    public class UserController : Controller
    {
        private ServiceMatchUp service;
        public UserController(ServiceMatchUp service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeUser]
        public async Task<IActionResult> Perfil()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            int idusuario = int.Parse(HttpContext.User.FindFirst("Id").Value);
            List<Equipo> equipos = await this.service.GetEquiposUserAsync(idusuario, token);
            ViewData["Equipos"] = equipos;
            List <PartidoEquipos> partidos = await this.service.GetPartidosMesAsync(idusuario, token);
            ViewData["PartidosMes"] = partidos;
            return View();
        }
        [AuthorizeUser]
        [HttpPost]
        public async Task<IActionResult> Perfil(int local, int visitante, int idpartido)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            await this.service.UpdateResultPachanga(local, visitante, idpartido, token);
            return RedirectToAction("Perfil");
        }

        [AuthorizeUser]
        public async Task<IActionResult> CambiarImagen(string imagen)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            imagen = "https://storagematchup.blob.core.windows.net/imagenes/users/" + imagen;
            // Actualizar la imagen en la base de datos
            await this.service.UpdateImageUser(imagen, id, token);

            // Obtener la identidad del usuario actual
            var user = HttpContext.User;
            var identity = (ClaimsIdentity)user.Identity;

            // Remover el claim existente de "Imagen"
            var oldClaim = identity.FindFirst("Imagen");
            if (oldClaim != null)
            {
                identity.RemoveClaim(oldClaim);
            }

            // Agregar el nuevo claim con la imagen actualizada
            identity.AddClaim(new Claim("Imagen", imagen));

            // Crear una nueva identidad con los claims actualizados
            var newPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(newPrincipal);

            return RedirectToAction("Perfil");
        }


        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string password)
        {
            user.Imagen = "defaultuser.jpg";
            await this.service.RegisterUserAsync(user.Email, password, user.Nombre);
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string correo, string pass)
        {
            string token = await this.service.GetTokenAsync(correo, pass);
            if(token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("TOKEN", token); 
                User user = await this.service.LoginAsync(correo, pass);
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
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
