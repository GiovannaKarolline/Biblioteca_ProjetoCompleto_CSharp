using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public IActionResult CriarAutor()
        {
            return View(new AutorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarAutor(AutorViewModel autor)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o autor (modelo/dados inválidos).";

                return View(autor);
            }

            var resultadoCriacao = await _autorService.CriarAutor(autor);

            if(resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o autor (falha ao criar).";

                return View(autor);
            }

            ViewData["Sucesso"] = "Autor criado com sucesso!";

            return View("CriarAutor");
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarAutor()
        {
            AtualizarAutorViewModel autores = new AtualizarAutorViewModel()
            {
                Autores = await _autorService.GetAutores()
            };

            return View("AtualizarAutor", autores);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarAutor(AtualizarAutorViewModel autor)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o autor (modelo/dados inválidos).";

                return View(autor);
            }

            var resultadoCriacao = await _autorService.AtualizarAutor(autor.Id, autor);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o autor (falha ao atualizar).";

                return View(autor);
            }

            ViewData["Sucesso"] = "Autor atualizado com sucesso!";

            autor.Autores = await _autorService.GetAutores();

            return View("AtualizarAutor", autor);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarAutor()
        {
            DeletarAutorViewModel autores = new DeletarAutorViewModel()
            {
                Autores = await _autorService.GetAutores()
            };

            return View("DeletarAutor", autores);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarAutor(DeletarAutorViewModel autor)
        {
            if(autor.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o autor (Guid inválido).";

                return View(autor);
            }

            var resultadoCriacao = await _autorService.DeletarAutor(autor.Id);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o autor (falha ao deletar).";

                return View(autor);
            }

            ViewData["Sucesso"] = "Autor deletado com sucesso!";

            autor.Autores = await _autorService.GetAutores();

            return View("DeletarAutor", autor);
        }

        [HttpGet]
        public async Task<IActionResult> ListarAutores()
        {
            var listaAutores = await _autorService.GetAutores();

            if (listaAutores.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os autores (lista vazia ou nula).";

                return View(listaAutores);
            }

            ViewData["Sucesso"] = "Autores listados com sucesso!";

            return View("ListarAutores", listaAutores);
        }

    }
}
