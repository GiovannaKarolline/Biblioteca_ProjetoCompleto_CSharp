using Biblioteca.Repositories.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class ObraLiterariaController : Controller
    {
        private readonly IObraLiterariaRepository _repository;

        public async Task<IActionResult> Index()
        {
            var obrasViewModel = new ObraLiterariaViewModel()
            {
                ObrasLiterarias = await _repository.GetObras()
            };

            return View();
        }
    }
}
