using Microsoft.AspNetCore.Mvc;

namespace MatchUpProyecto.Controllers
{
    public class PartidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
