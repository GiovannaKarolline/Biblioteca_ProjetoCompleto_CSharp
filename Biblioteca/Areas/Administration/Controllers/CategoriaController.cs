using Biblioteca.Areas.Administration.Services;
using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
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
                ViewData["Falha"] = "Não foi possível criar a categoria (modelo/dados inválidos).";

                return View(categoria);
            }

            var resultadoCriacao = await _categoriaService.CriarCategoria(categoria);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a categoria (falha ao criar).";

                return View(categoria);
            }

            ViewData["Sucesso"] = "Categoria criada com sucesso!";

            return View("CriarCategoria");
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarCategoria()
        {
            AtualizarCategoriaViewModel categoria = new AtualizarCategoriaViewModel()
            {
                Categorias = await _categoriaService.GetCategorias()
            };

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarCategoria(AtualizarCategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a categoria (modelo/dados inválidos).";

                return View(categoria);
            }

            var resultadoCriacao = await _categoriaService.AtualizarCategoria(categoria.Id, categoria);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a categoria (falha ao atualizar).";

                return View(categoria);
            }

            ViewData["Sucesso"] = "Categoria atualizada com sucesso!";

            categoria.Categorias = await _categoriaService.GetCategorias();

            return View("AtualizarCategoria", categoria);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarCategoria()
        {
            DeletarCategoriaViewModel categoria = new DeletarCategoriaViewModel()
            {
                Categorias = await _categoriaService.GetCategorias()
            };

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarCategoria(DeletarCategoriaViewModel categoria)
        {
            if (categoria.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a categoria (Guid inválido).";

                return View(categoria);
            }

            var resultadoCriacao = await _categoriaService.DeletarCategoria(categoria.Id);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a categoria (falha ao deletar).";

                return View(categoria);
            }

            ViewData["Sucesso"] = "Categoria deletada com sucesso!";

            categoria.Categorias = await _categoriaService.GetCategorias();

            return View("DeletarCategoria", categoria);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            var listaCategorias = await _categoriaService.GetCategorias();

            if (listaCategorias.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as categorias (lista vazia ou nula).";

                return View(listaCategorias);
            }

            ViewData["Sucesso"] = "Categorias listadas com sucesso!";

            return View("ListarCategorias", listaCategorias);
        }
    }
}
