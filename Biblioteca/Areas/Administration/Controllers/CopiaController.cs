using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Areas.Administration.ViewModels.Criar;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class CopiaController : Controller
    {
        private readonly ICopiaService _copiaService;
        private readonly IObraLiterariaService _obraLiterariaService;

        public CopiaController(ICopiaService copiaService, IObraLiterariaService obraLiterariaService)
        {
            _copiaService = copiaService;
            _obraLiterariaService = obraLiterariaService;
        }

        [HttpGet]
        public async Task<IActionResult> CriarCopia()
        {
            CriarCopiaViewModel copia = new CriarCopiaViewModel()
            {
                ObrasPossiveis = await _obraLiterariaService.GetObras()
            };

            return View(copia);
        }

        [HttpPost]
        public async Task<IActionResult> CriarCopia(CriarCopiaViewModel copia)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a cópia (modelo/dados inválidos).";

                return View(copia);
            }

            var resultadoCriacao = await _copiaService.CriarCopia(copia);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a cópia (falha ao criar).";

                return View(copia);
            }

            ViewData["Sucesso"] = "Cópia criada com sucesso!";

            copia.ObrasPossiveis = await _obraLiterariaService.GetObras();

            return View("CriarCopia", copia);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarCopia()
        {
            AtualizarCopiaViewModel copia = new AtualizarCopiaViewModel()
            {
                Obras = await _obraLiterariaService.GetObras(),
                Copias = await _copiaService.GetCopias()
            };

            return View(copia);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarCopia(AtualizarCopiaViewModel copia)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (modelo/dados inválidos).";

                return View(copia);
            }

            var resultadoAtualizacao = await _copiaService.AtualizarCopia(copia.Id, copia);

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (falha ao atualizar).";

                return View(copia);
            }

            ViewData["Sucesso"] = "Cópia atualizada com sucesso!";

            copia.Copias = await _copiaService.GetCopias();

            return View("AtualizarCopia", copia);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarCopia()
        {
            DeletarCopiaViewModel copia = new DeletarCopiaViewModel()
            {
                Copias = await _copiaService.GetCopias()
            };

            return View(copia);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarCopia(DeletarCopiaViewModel copia)
        {
            if (copia.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a cópia (Guid inválido).";

                return View(copia);
            }

            var resultadoDeletar = await _copiaService.DeletarCopia(copia.Id);

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a cópia (falha ao deletar).";

                return View(copia);
            }

            ViewData["Sucesso"] = "Cópia deletada com sucesso!";

            copia.Copias = await _copiaService.GetCopias();

            return View("DeletarCopia", copia);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCopias()
        {
            var listaCopias = await _copiaService.GetCopias();

            if (listaCopias.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as cópias (lista vazia ou nula).";

                return View(listaCopias);
            }

            ViewData["Sucesso"] = "Cópias listadas com sucesso!";

            return View("ListarCopias", listaCopias);
        }
    }
}
