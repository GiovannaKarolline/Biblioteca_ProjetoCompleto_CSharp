using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
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
