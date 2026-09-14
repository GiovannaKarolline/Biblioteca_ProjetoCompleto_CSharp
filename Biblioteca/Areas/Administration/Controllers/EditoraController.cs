using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
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
        public async Task<IActionResult> CriarEditora(EditoraViewModel Editora)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a editora (modelo/dados inválidos).";

                return View(Editora);
            }

            var resultadoCriacao = await _editoraService.CriarEditora(Editora);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a editora (falha ao criar).";

                return View(Editora);
            }

            ViewData["Sucesso"] = "Editora criada com sucesso!";

            return View("CriarEditora", Editora);
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
        public async Task<IActionResult> AtualizarEditora(AtualizarEditoraViewModel Editora)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (modelo/dados inválidos).";

                return View(Editora);
            }

            var resultadoCriacao = await _editoraService.AtualizarEditora(Editora.Id, Editora);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (falha ao atualizar).";

                return View(Editora);
            }

            ViewData["Sucesso"] = "Cópia atualizada com sucesso!";

            Editora.Editoras = await _editoraService.GetEditoras();

            return View("AtualizarEditora", Editora);
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

            var resultadoCriacao = await _editoraService.DeletarEditora(editora.Id);

            if (resultadoCriacao == null)
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
