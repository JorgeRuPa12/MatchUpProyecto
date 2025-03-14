using MatchUpProyecto.Extensions;
using MatchUpProyecto.Filters;
using MatchUpProyecto.Models;
using MatchUpProyecto.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MatchUpProyecto.Controllers
{
    public class PachangasController : Controller
    {
        public RepositoryPachanga repo;
        public RepositoryEquipos repoE;
        public PachangasController (RepositoryPachanga repo, RepositoryEquipos repoE)
        {
            this.repo = repo;
            this.repoE = repoE;
        }
        public async Task<IActionResult> Index()
        {
            List<PartidoEquipos> partidos = await this.repo.ObtenerPartidosPorPachanga();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                List<Equipo> equipos = await this.repoE.GetEquiposUsuarioAysnc(int.Parse(HttpContext.User.FindFirst("Id").Value));
                ViewData["Equipos"] = equipos;
            }
            return View(partidos);
        }

        [AuthorizeUser]
        [HttpPost]
        public async Task<IActionResult> Index(int idequipo, int idpartido)
        {
            await this.repoE.UnirEquipoPartido(idequipo, idpartido);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> VerPachangas()
        //{
        //    List<Pachanga> pachangas = await this.repo.GetPachangasAsync();
        //    return View(pachangas);
        //}
        [AuthorizeUser]
        public async Task<IActionResult> Create()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string dato = HttpContext.User.FindFirst("Id").Value;
                int id = int.Parse(dato);
                List<Equipo> misEquipos = await this.repoE.GetEquiposUsuarioAysnc(id);
                HttpContext.Session.SetObject("MISEQUIPOS", misEquipos);
            }
            return View();
        }

        [AuthorizeUser]
        [HttpPost]
        public async Task<IActionResult> Create(Pachanga pachanga, int idequipo)
        {
            if(pachanga.UbiProvincia != null)
            {
                await this.repo.InsertPachangaAsync(pachanga, idequipo);
                return RedirectToAction("Index");
            }
            else
            {
                return View(pachanga);
            }
        }

    }
}

