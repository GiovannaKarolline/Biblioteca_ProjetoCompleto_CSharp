using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Atualizar()
        {
            return View();
        }

        public IActionResult Deletar()
        {
            return View();
        }
    }
}
