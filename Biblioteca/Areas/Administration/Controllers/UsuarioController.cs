using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
