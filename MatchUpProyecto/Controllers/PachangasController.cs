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
        public PachangasController (RepositoryPachanga repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Pachanga> pachangas = await this.repo.GetPachangasAsync();
            return View(pachangas);
        }

        //public async Task<IActionResult> VerPachangas()
        //{
        //    List<Pachanga> pachangas = await this.repo.GetPachangasAsync();
        //    return View(pachangas);
        //}

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pachanga pachanga)
        {
            if(pachanga.UbiProvincia != null)
            {
                await this.repo.InsertPachangaAsync(pachanga);
                return RedirectToActionPermanent("Index");
            }
            else
            {
                return View(pachanga);
            }
        }
    }
}

