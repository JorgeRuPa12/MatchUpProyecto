using MatchUpProyecto.Filters;
using MatchUpProyecto.Models;
using MatchUpProyecto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchUpProyecto.Controllers
{
    public class EquiposController : Controller
    {
        private RepositoryEquipos repo;
        public EquiposController(RepositoryEquipos repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Equipo> equipos = await this.repo.GetEquiposAsync();
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
            await this.repo.InsertEquipoAsync(equipo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MisEquipos(int idusuario)
        {
            List<Equipo> misEquipos = await this.repo.GetEquiposUsuarioAysnc(idusuario);
            return View(misEquipos);
        }
    }
}
