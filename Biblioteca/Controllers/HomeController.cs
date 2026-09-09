using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private IObraLiterariaRepository _obraLiterariaRepository;

        public HomeController(IObraLiterariaRepository obraLiterariaRepository)
        {
            _obraLiterariaRepository = obraLiterariaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var obras = new HomeViewModel
            {
                ObrasLiterarias = await _obraLiterariaRepository.GetObras() ?? new List<ObraLiteraria>()
            };

            return View(obras);
        }
    }
}
