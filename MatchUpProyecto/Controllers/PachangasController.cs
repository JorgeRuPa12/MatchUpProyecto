using MatchUpProyecto.Extensions;
using MatchUpProyecto.Filters;
using NugetMatchUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using MatchUpProyecto.Services;

namespace MatchUpProyecto.Controllers
{
    public class PachangasController : Controller
    {
        public ServiceMatchUp service;
        public PachangasController (ServiceMatchUp service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            List<PartidoEquipos> partidos = await this.service.GetPartidosPachangaAsync();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                List<Equipo> equipos = await this.service.GetEquiposUserAsync(int.Parse(HttpContext.User.FindFirst("Id").Value), token);
                ViewData["Equipos"] = equipos;
            }
            return View(partidos);
        }

        [AuthorizeUser]
        [HttpPost]
        public async Task<IActionResult> Index(int idequipo, int idpartido)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            await this.service.UnirseAPartidoAsync(idequipo, idpartido, token);
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
                string token = HttpContext.Session.GetString("TOKEN");
                string dato = HttpContext.User.FindFirst("Id").Value;
                int id = int.Parse(dato);
                List<Equipo> misEquipos = await this.service.GetEquiposUserAsync(id, token);
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
                string token = HttpContext.Session.GetString("TOKEN");
                await this.service.CreatePachangaAsync(pachanga, idequipo, token);
                return RedirectToAction("Index");
            }
            else
            {
                return View(pachanga);
            }
        }

    }
}

