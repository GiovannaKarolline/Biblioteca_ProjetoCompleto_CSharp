using Biblioteca.Models;
using Biblioteca.Services;
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
                ObrasLiterarias = (await _obraLiterariaService.GetObras()) ?? new List<ObraLiteraria>()
            };

            foreach (var obra in obras.ObrasLiterarias)
            {
                if(obra.Copias is not null)
                {
                    obra.Copias = obra.Copias.Where(copia => copia.Deletado == false).ToList(); //para que as cópias deletadas não sejam passadas para a view.
                    obra.Copias = obra.Copias.Where(copia => copia.StatusDisponibilidade == true).ToList(); //para que cópias emprestadas não sejam passadas para a view.
                }
            }

            return View(obras);
        }

        public async Task<IActionResult> PesquisarObra(string titulo)
        {
            IEnumerable<ObraLiteraria> obras = await _obraLiterariaService.GetObrasLiterariasByTitulo(titulo);

            return View(obras);
        }
    }
}
