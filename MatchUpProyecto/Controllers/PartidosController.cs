using MatchUpProyecto.Filters;
using MatchUpProyecto.Services;
using Microsoft.AspNetCore.Mvc;
using NugetMatchUp.Models;

namespace MatchUpProyecto.Controllers
{
    public class PartidosController : Controller
    {
        public ServiceMatchUp service;
        public PartidosController(ServiceMatchUp service)
        {
            this.service = service;
        }

        [AuthorizeUser]
        public async Task<IActionResult> MisPartidos()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            List<PartidoEquipos> partidos = await this.service.GetPartidosPachangaAsync();
            return View(partidos);
        }
    }
}
