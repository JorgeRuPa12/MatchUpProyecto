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
            equipo.Emblema = "emblema" + equipo.Emblema + ".jpg";
            await this.repo.InsertEquipoAsync(equipo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MisEquipos(int idusuario)
        {
            List<Equipo> misEquipos = await this.repo.GetEquiposUsuarioAysnc(idusuario);
            return View(misEquipos);
        }

        [AuthorizeUser]
        public async Task<IActionResult> Detalles(int idequipo)
        {
            EquipoDetalle equipoDetalle = await this.repo.GetEquipoDetalleAsync(idequipo);
            return View(equipoDetalle);
        }

        public async Task<IActionResult> Join(int idequipo)
        {
            string dato = HttpContext.User.FindFirst("Id").Value;
            int idusuario = int.Parse(dato);
            await this.repo.UnirseEquipoAsync(idequipo, idusuario);
            return RedirectToAction("MisEquipos");
        }
        
        public async Task<IActionResult> Leave(int idequipo)
        {
            string dato = HttpContext.User.FindFirst("Id").Value;
            int idusuario = int.Parse(dato);
            await this.repo.SalirseEquipoAsync(idequipo, idusuario);
            return RedirectToAction("MisEquipos");
        }
    }
}
