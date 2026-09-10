using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class EnderecoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
