using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public IActionResult CriarEditora()
        {
            return View(new EditoraViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarEditora(EditoraViewModel editora)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a editora (modelo/dados inválidos).";

                return View(editora);
            }

            Editora? resultadoCriacao;

            try
            {
                resultadoCriacao = await _editoraService.CriarEditora(editora);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(editora);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a editora (falha ao criar).";

                return View(editora);
            }

            ViewData["Sucesso"] = "Editora criada com sucesso!";

            return View("CriarEditora", editora);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarEditora()
        {
            AtualizarEditoraViewModel editora = new AtualizarEditoraViewModel()
            {
                Editoras = await _editoraService.GetEditoras()
            };

            return View(editora);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEditora(AtualizarEditoraViewModel editora)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (modelo/dados inválidos).";

                return View(editora);
            }

            Editora? resultadoAtualizar;

            try
            {
                resultadoAtualizar = await _editoraService.AtualizarEditora(editora.Id, editora);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(editora);
            }

            if (resultadoAtualizar == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (falha ao atualizar).";

                return View(editora);
            }

            ViewData["Sucesso"] = "Cópia atualizada com sucesso!";

            editora.Editoras = await _editoraService.GetEditoras();

            return View("AtualizarEditora", editora);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarEditora()
        {
            DeletarEditoraViewModel editora = new DeletarEditoraViewModel()
            {
                Editoras = await _editoraService.GetEditoras()
            };

            return View(editora);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEditora(DeletarEditoraViewModel editora)
        {
            if (editora.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a editora (Guid inválido).";

                return View(editora);
            }

            Editora? resultadoDeletar;

            try
            {
                resultadoDeletar = await _editoraService.DeletarEditora(editora.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(editora);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a editora (falha ao deletar).";

                return View(editora);
            }

            ViewData["Sucesso"] = "editora deletada com sucesso!";

            editora.Editoras = await _editoraService.GetEditoras();

            return View("DeletarEditora", editora);
        }

        [HttpGet]
        public async Task<IActionResult> ListarEditoras()
        {
            var listaEditoras = await _editoraService.GetEditoras();

            if (listaEditoras.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as editoras (lista vazia ou nula).";

                return View(listaEditoras);
            }

            ViewData["Sucesso"] = "Editoras listadas com sucesso!";

            return View("ListarEditoras", listaEditoras);
        }
    }
}
