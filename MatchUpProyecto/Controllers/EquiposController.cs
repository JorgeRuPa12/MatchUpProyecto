using MatchUpProyecto.Filters;
using NugetMatchUp.Models;
using Microsoft.AspNetCore.Mvc;
using MatchUpProyecto.Services;

namespace MatchUpProyecto.Controllers
{
    public class EquiposController : Controller
    {
        private ServiceMatchUp service;
        public EquiposController(ServiceMatchUp service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Equipo> equipos = await this.service.GetEquiposAsync();
            return View(equipos);
        }

        [AuthorizeUser]
        public IActionResult Insert()
        {

            return View();
        }

        [AuthorizeUser]
        [HttpPost]
        public async Task<IActionResult> Insert(Equipo equipo)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            equipo.Emblema = "emblema" + equipo.Emblema + ".jpg";
            await this.service.PostEquipoAsync(token, equipo);
            return RedirectToAction("Index");
        }

        [AuthorizeUser]
        public async Task<IActionResult> MisEquipos(int idusuario)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            List<Equipo> misEquipos = await this.service.GetEquiposUserAsync(idusuario, token);
            return View(misEquipos);
        }

        [AuthorizeUser]
        public async Task<IActionResult> Detalles(int idequipo)
        {
            EquipoDetalle equipoDetalle = await this.service.GetEquipoDetailsAsync(idequipo);
            return View(equipoDetalle);
        }

        public async Task<IActionResult> Join(int idequipo)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            string dato = HttpContext.User.FindFirst("Id").Value;
            int idusuario = int.Parse(dato);
            await this.service.UnirseEquipoAsync(idequipo, idusuario, token);
            return RedirectToAction("Perfil", "User");
        }
        
        public async Task<IActionResult> Leave(int idequipo)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            string dato = HttpContext.User.FindFirst("Id").Value;
            int idusuario = int.Parse(dato);
            await this.service.SalirseEquipoAsync(idequipo, idusuario, token);
            return RedirectToAction("Perfil", "User");
        }
    }
}
