using Biblioteca.Areas.Administration.Services;
using Biblioteca.Areas.Administration.Services.Interfaces;
using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Criar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class ObraLiterariaController : Controller
    {
        private readonly IObraLiterariaService _obraLiterariaService;
        private readonly ICopiaService _copiaService;
        private readonly ICategoriaService _categoriaService;
        private readonly IEditoraService _editoraService;
        private readonly IAutorService _autorService;

        public ObraLiterariaController(IObraLiterariaService obraLiterariaService, ICopiaService copiaService, ICategoriaService categoriaService, IEditoraService editoraService, IAutorService autorService)
        {
            _obraLiterariaService = obraLiterariaService;
            _copiaService = copiaService;
            _editoraService = editoraService;
            _categoriaService = categoriaService;
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> CriarObra()
        {
            CriarObraViewModel obra = new CriarObraViewModel()
            {
                AutoresExistentes = await _autorService.GetAutores(),
                EditorasExistentes = await _editoraService.GetEditoras(),
                CategoriasExistentes = await _categoriaService.GetCategorias()
            };

            return View(obra);
        }

        [HttpPost]
        public async Task<IActionResult> CriarObra(CriarObraViewModel obra)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a obra literária (modelo/dados inválidos).";

                obra.AutoresExistentes = await _autorService.GetAutores();
                obra.EditorasExistentes = await _editoraService.GetEditoras();
                obra.CategoriasExistentes = await _categoriaService.GetCategorias();

                return View(obra);
            }

            ObraLiteraria? resultadoCriacao;

            try
            {
                resultadoCriacao = await _obraLiterariaService.CriarObraLiteraria(obra);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(obra);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a obra literária (falha ao criar).";

                obra.AutoresExistentes = await _autorService.GetAutores();
                obra.EditorasExistentes = await _editoraService.GetEditoras();
                obra.CategoriasExistentes = await _categoriaService.GetCategorias();

                return View(obra);
            }

            ViewData["Sucesso"] = "Obra literária criada com sucesso!";

            obra.AutoresExistentes = await _autorService.GetAutores();
            obra.EditorasExistentes = await _editoraService.GetEditoras();
            obra.CategoriasExistentes = await _categoriaService.GetCategorias();

            return View("CriarObra", obra);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarObra()
        {
            AtualizarObraViewModel obra = new AtualizarObraViewModel()
            {
                ObrasLiterarias = await _obraLiterariaService.GetObras(),
                AutoresExistentes = await _autorService.GetAutores(),
                CategoriasExistentes = await _categoriaService.GetCategorias(),
                EditorasExistentes = await _editoraService.GetEditoras()
            };

            return View(obra);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarObra(AtualizarObraViewModel obra)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a obra literária (modelo/dados inválidos).";

                obra.ObrasLiterarias = await _obraLiterariaService.GetObras();
                obra.AutoresExistentes = await _autorService.GetAutores();
                obra.CategoriasExistentes = await _categoriaService.GetCategorias();
                obra.EditorasExistentes = await _editoraService.GetEditoras();

                return View(obra);
            }

            ObraLiteraria? resultadoAtualizacao;

            try
            {
                resultadoAtualizacao = await _obraLiterariaService.AtualizarObraLiteraria(obra.Id, obra);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(obra);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a obra literária (falha ao atualizar).";

                obra.ObrasLiterarias = await _obraLiterariaService.GetObras();
                obra.AutoresExistentes = await _autorService.GetAutores();
                obra.CategoriasExistentes = await _categoriaService.GetCategorias();
                obra.EditorasExistentes = await _editoraService.GetEditoras();

                return View(obra);
            }

            ViewData["Sucesso"] = "Obra literária atualizada com sucesso!";

            obra.ObrasLiterarias = await _obraLiterariaService.GetObras();

            obra.AutoresExistentes = await _autorService.GetAutores();

            obra.CategoriasExistentes = await _categoriaService.GetCategorias();

            obra.EditorasExistentes = await _editoraService.GetEditoras();

            return View("AtualizarObra", obra);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarObra()
        {
            DeletarObraViewModel obra = new DeletarObraViewModel()
            {
                ObrasLiterarias = await _obraLiterariaService.GetObras()
            };

            return View(obra);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarObra(DeletarObraViewModel obra)
        {
            if (obra.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a obra literária (Guid inválido).";

                obra.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View(obra);
            }

            ObraLiteraria? resultadoDeletar;

            try
            {
                resultadoDeletar = await _obraLiterariaService.DeletarObraLiteraria(obra.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(obra);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a obra literária (falha ao deletar).";

                obra.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View(obra);
            }

            ViewData["Sucesso"] = "Obra literária deletada com sucesso!";

            obra.ObrasLiterarias = await _obraLiterariaService.GetObras();

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
