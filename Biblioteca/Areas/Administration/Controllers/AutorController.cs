using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using X.PagedList;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            AutorViewModel autor = new AutorViewModel();

            autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(paginaAtual ?? 1, 6);

            if (autor.Autores.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os autores (lista vazia ou nula).";

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(paginaAtual ?? 1, 6);

                return View(autor);
            }

            ViewData["Sucesso"] = "Autores listados com sucesso!";

            autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(paginaAtual ?? 1, 6);

            return View("Index", autor);
        }

        [HttpPost]
        public async Task<IActionResult> CriarAutor(AutorViewModel autor)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o autor (modelo/dados inválidos).";

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            Autor? resultadoCriacao;

            try
            {

                resultadoCriacao = await _autorService.CriarAutor(autor);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }


            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o autor (falha ao criar).";

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            ViewData["Sucesso"] = "Autor criado com sucesso!";

            autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

            return View("Index", autor);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarAutor(AutorViewModel autor)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o autor (modelo/dados inválidos).";

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            Autor? resultadoAtualizacao;

            try
            {

                resultadoAtualizacao = await _autorService.AtualizarAutor(autor.Id, autor);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o autor (falha ao atualizar).";

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            ViewData["Sucesso"] = "Autor atualizado com sucesso!";

            autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

            return View("Index", autor);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarAutor(AutorViewModel autor)
        {
            if(autor.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o autor (Guid inválido).";

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            Autor? resultadoDeletar;

            try
            {

                resultadoDeletar = await _autorService.DeletarAutor(autor.Id);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

                return View("Index", autor);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o autor (falha ao deletar).";

                return View("Index", autor);
            }

            ViewData["Sucesso"] = "Autor deletado com sucesso!";

            autor.Autores = await (await _autorService.GetAutores()).ToPagedListAsync(1, 6);

            return View("Index", autor);
        }

    }
}
