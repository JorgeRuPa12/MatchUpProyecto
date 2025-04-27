using System.Diagnostics;
using MatchUpProyecto.Extensions;
using NugetMatchUp.Models;
using Microsoft.AspNetCore.Mvc;
using MatchUpProyecto.Services;

namespace MatchUpProyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ServiceMatchUp service;

        public HomeController(ILogger<HomeController> logger, ServiceMatchUp service)
        {
            _logger = logger;
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Deporte> deportes = await this.service.GetDeportesAsync();
            HttpContext.Session.SetObject("DEPORTES", deportes);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
