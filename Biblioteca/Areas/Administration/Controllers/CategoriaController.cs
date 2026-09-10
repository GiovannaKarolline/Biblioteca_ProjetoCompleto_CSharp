using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService CategoriaService)
        {
            _categoriaService = CategoriaService;
        }

        [HttpGet]
        public IActionResult CriarCategoria()
        {
            return View(new CategoriaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria(CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var resultadoCriacao = await _categoriaService.CriarCategoria(categoria);

            if (resultadoCriacao == null)
            {
                return View(categoria);
            }

            ViewData["Sucesso"] = "Categoria criado com sucesso!";

            return View("CriarCategoria");
        }

        [HttpGet]
        public IActionResult AtualizarCategoria()
        {
            return View(new CategoriaViewModel());
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarCategoria(Guid id, CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            var resultadoCriacao = await _categoriaService.AtualizarCategoria(id, categoria);

            if (resultadoCriacao == null)
            {
                return View(categoria);
            }

            ViewData["Sucesso"] = "Categoria atualizado com sucesso!";

            return View("AtualizarCategoria");
        }

        [HttpGet]
        public IActionResult DeletarCategoria()
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarCategoria(Guid id)
        {
            if (id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a categoria (Guid inválido).";

                return View();
            }

            var resultadoCriacao = await _categoriaService.DeletarCategoria(id);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a categoria (falha ao deletar).";

                return View();
            }

            ViewData["Sucesso"] = "Categoria deletada com sucesso!";

            return View("DeletarCategoria");
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            var listaCategorias = await _categoriaService.GetCategorias();

            if (listaCategorias.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as categorias (lista vazia ou nula).";

                return View();
            }

            ViewData["Sucesso"] = "Categorias listadas com sucesso!";

            return RedirectToAction("ListarCategorias");
        }
    }
}
