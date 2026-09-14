using Biblioteca.Models;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private IObraLiterariaService _obraLiterariaService;

        public HomeController(IObraLiterariaService obraLiterariaService)
        {
            _obraLiterariaService = obraLiterariaService;
        }

        public async Task<IActionResult> Index()
        {
            var obras = new HomeViewModel
            {
                ObrasLiterarias = await _obraLiterariaService.GetObras() ?? new List<ObraLiteraria>()
            };

            return View(obras);
        }

        public async Task<IActionResult> PesquisarObra(string titulo)
        {
            IEnumerable<ObraLiteraria> obras = await _obraLiterariaService.GetObrasLiterariasByTitulo(titulo);

            return View(obras);
        }
    }
}
