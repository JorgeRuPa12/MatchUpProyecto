using MatchUpProyecto.Models;
using MatchUpProyecto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchUpProyecto.Controllers
{
    public class DeportesController : Controller
    {
        private RepositoryDeportes repo;
        public DeportesController(RepositoryDeportes repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Deporte> deportes = await this.repo.GetDeportes();
            return View(deportes);
        }
    }
}
