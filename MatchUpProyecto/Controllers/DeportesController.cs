using NugetMatchUp.Models;
using Microsoft.AspNetCore.Mvc;
using MatchUpProyecto.Services;

namespace MatchUpProyecto.Controllers
{
    public class DeportesController : Controller
    {
        private ServiceMatchUp service;
        public DeportesController(ServiceMatchUp service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Deporte> deportes = await this.service.GetDeportesAsync();
            return View(deportes);
        }
    }
}
