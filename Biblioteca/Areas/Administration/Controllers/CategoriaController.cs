using Biblioteca.Areas.Administration.Services;
using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService CategoriaService)
        {
            _categoriaService = CategoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            CategoriaViewModel categoria = new CategoriaViewModel();

            categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(paginaAtual ?? 1, 6);

            if (categoria.Categorias.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as categorias (lista vazia ou nula).";

                return View(categoria);
            }

            ViewData["Sucesso"] = "Categorias listadas com sucesso!";

            return View("Index", categoria);
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria(CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a categoria (modelo/dados inválidos).";

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            Categoria? resultadoCriacao;

            try
            {

                resultadoCriacao = await _categoriaService.CriarCategoria(categoria);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a categoria (falha ao criar).";

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            ViewData["Sucesso"] = "Categoria criada com sucesso!";

            categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

            return View("Index", categoria);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarCategoria(CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a categoria (modelo/dados inválidos).";

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            Categoria? resultadoAtualizacao;

            try
            {

                resultadoAtualizacao = await _categoriaService.AtualizarCategoria(categoria.Id, categoria);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a categoria (falha ao atualizar).";

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            ViewData["Sucesso"] = "Categoria atualizada com sucesso!";

            categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

            return View("Index", categoria);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarCategoria(CategoriaViewModel categoria)
        {
            if (categoria.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a categoria (Guid inválido).";

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            Categoria? resultadoDeletar;

            try
            {

                resultadoDeletar = await _categoriaService.DeletarCategoria(categoria.Id);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a categoria (falha ao deletar).";

                categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

                return View("Index", categoria);
            }

            ViewData["Sucesso"] = "Categoria deletada com sucesso!";

            categoria.Categorias = (await _categoriaService.GetCategorias()).ToPagedList(1, 6);

            return View("Index", categoria);
        }

    }
}
