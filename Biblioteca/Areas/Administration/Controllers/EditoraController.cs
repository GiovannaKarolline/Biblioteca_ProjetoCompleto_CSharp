using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Models;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class EditoraController : Controller
    {
        private readonly IEditoraService _editoraService;
        public EditoraController(IEditoraService EditoraService, IObraLiterariaService obraLiterariaService)
        {
            _editoraService = EditoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            EditoraViewModel editora = new EditoraViewModel();

            editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(paginaAtual ?? 1, 6);

            if (editora.Editoras.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as editoras (lista vazia ou nula).";

                return View(editora);
            }

            ViewData["Sucesso"] = "Editoras listadas com sucesso!";

            return View(editora);
        }

        [HttpPost]
        public async Task<IActionResult> CriarEditora(EditoraViewModel editora)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a editora (modelo/dados inválidos).";

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            Editora? resultadoCriacao;

            try
            {
                resultadoCriacao = await _editoraService.CriarEditora(editora);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a editora (falha ao criar).";

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            ViewData["Sucesso"] = "Editora criada com sucesso!";

            editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

            return View("Index", editora);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEditora(EditoraViewModel editora)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a editora (modelo/dados inválidos).";

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            Editora? resultadoAtualizar;

            try
            {
                resultadoAtualizar = await _editoraService.AtualizarEditora(editora.Id, editora);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            if (resultadoAtualizar == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a editora (falha ao atualizar).";

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            ViewData["Sucesso"] = "Editora atualizada com sucesso!";

            editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

            return View("Index", editora);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEditora(EditoraViewModel editora)
        {
            if (editora.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a editora (Guid inválido).";

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            Editora? resultadoDeletar;

            try
            {
                resultadoDeletar = await _editoraService.DeletarEditora(editora.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View("Index", editora);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a editora (falha ao deletar).";

                editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

                return View(editora);
            }

            ViewData["Sucesso"] = "editora deletada com sucesso!";

            editora.Editoras = (await _editoraService.GetEditoras()).ToPagedList(1, 6);

            return View("Index", editora);
        }

    }
}
