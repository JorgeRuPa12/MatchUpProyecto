using System.Diagnostics;
using MatchUpProyecto.Extensions;
using MatchUpProyecto.Models;
using MatchUpProyecto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchUpProyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RepositoryDeportes repo;

        public HomeController(ILogger<HomeController> logger, RepositoryDeportes repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Deporte> deportes = await this.repo.GetDeportes();
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
