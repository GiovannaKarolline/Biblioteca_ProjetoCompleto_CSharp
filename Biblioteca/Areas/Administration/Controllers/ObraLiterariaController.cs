using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ObraLiterariaController : Controller
    {
        private readonly IObraLiterariaService _obraLiterariaService;
        private readonly ICopiaService _copiaService;

        public ObraLiterariaController(IObraLiterariaService obraLiterariaService, ICopiaService copiaService)
        {
            _obraLiterariaService = obraLiterariaService;
            _copiaService = copiaService;
        }

        [HttpGet]
        public IActionResult CriarObra()
        {
            return View(new ObraLiterariaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarObra(ObraLiterariaViewModel obra)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a obra literária (modelo/dados inválidos).";

                return View(obra);
            }

            var resultadoCriacao = await _obraLiterariaService.CriarObraLiteraria(obra);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a obra literária (falha ao criar).";

                return View(obra);
            }

            ViewData["Sucesso"] = "Obra literária criada com sucesso!";

            obra.ObrasLiterarias = await _obraLiterariaService.GetObras();

            return View("Criarobra", obra);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarObra()
        {
            AtualizarObraViewModel obra = new AtualizarObraViewModel()
            {
                Obras = await _obraLiterariaService.GetObras()
            };

            return View(obra);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarObra(AtualizarObraViewModel obra)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a obra literária (modelo/dados inválidos).";

                return View(obra);
            }

            var resultadoCriacao = await _obraLiterariaService.AtualizarObraLiteraria(obra.Id, obra);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a obra literária (falha ao atualizar).";

                return View(obra);
            }

            ViewData["Sucesso"] = "Obra literária atualizada com sucesso!";

            obra.Obras = await _obraLiterariaService.GetObras();

            return View("AtualizarObra", obra);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarObra()
        {
            DeletarObraViewModel obra = new DeletarObraViewModel()
            {
                Obras = await _obraLiterariaService.GetObras()
            };

            return View(obra);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarObra(DeletarObraViewModel obra)
        {
            if (obra.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a obra literária (Guid inválido).";

                return View(obra);
            }

            var resultadoCriacao = await _obraLiterariaService.DeletarObraLiteraria(obra.Id);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a obra literária (falha ao deletar).";

                return View(obra);
            }

            ViewData["Sucesso"] = "Obra literária deletada com sucesso!";

            obra.Obras = await _obraLiterariaService.GetObras();

            return View("DeletarObra", obra);
        }

        [HttpGet]
        public async Task<IActionResult> ListarObras()
        {
            var listaObras = await _obraLiterariaService.GetObras();

            if (listaObras.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as obras literárias (lista vazia ou nula).";

                return View(listaObras);
            }

            ViewData["Sucesso"] = "Obras literárias listadas com sucesso!";

            return View("ListarObras", listaObras);
        }
    }
}
