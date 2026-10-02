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
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            CopiaViewModel copia = new CopiaViewModel();

            copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(paginaAtual ?? 1, 6);
            copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

            if (copia.Copias.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as cópias (lista vazia ou nula).";

                return View(copia);
            }

            ViewData["Sucesso"] = "Cópias listadas com sucesso!";

            return View("Index", copia);
        }

        [HttpPost]
        public async Task<IActionResult> CriarCopia(CopiaViewModel copia)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a cópia (modelo/dados inválidos).";

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            Copia? resultadoCriacao;

            try
            {

                resultadoCriacao = await _copiaService.CriarCopia(copia);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar a cópia (falha ao criar).";

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            ViewData["Sucesso"] = "Cópia criada com sucesso!";

            copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
            copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

            return View("Index", copia);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarCopia(CopiaViewModel copia)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (modelo/dados inválidos).";

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            

            Copia? resultadoAtualizacao;

            try
            {

                resultadoAtualizacao = await _copiaService.AtualizarCopia(copia.Id, copia);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a cópia (falha ao atualizar).";

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            ViewData["Sucesso"] = "Cópia atualizada com sucesso!";

            copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
            copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

            return View("Index", copia);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarCopia(CopiaViewModel copia)
        {
            if (copia.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a cópia (Guid inválido).";

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            Copia? resultadoDeletar;

            try
            {
                resultadoDeletar = await _copiaService.DeletarCopia(copia.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a cópia (falha ao deletar).";

                copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
                copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

                return View("Index", copia);
            }

            ViewData["Sucesso"] = "Cópia deletada com sucesso!";

            copia.Copias = await (await _copiaService.GetCopias()).ToPagedListAsync(1, 6);
            copia.ObrasLiterarias = await _obraLiterariaService.GetObras();

            return View("Index", copia);
        }

    }
}
