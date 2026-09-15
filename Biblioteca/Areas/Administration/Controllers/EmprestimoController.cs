using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Criar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class EmprestimoController : Controller
    {
        private readonly IEmprestimoService _emprestimoService;
        private readonly ICopiaService _copiaService;

        public EmprestimoController(IEmprestimoService emprestimoService, ICopiaService copiaService)
        {
            _emprestimoService = emprestimoService;
            _copiaService = copiaService;
        }

        [HttpGet]
        public async Task<IActionResult> CriarEmprestimo()
        {
            CriarEmprestimoViewModel emprestimo = new CriarEmprestimoViewModel()
            {
                CopiasExistentes = await _copiaService.GetCopias()
            };

            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> CriarEmprestimo(CriarEmprestimoViewModel emprestimo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o empréstimo (modelo/dados inválidos).";

                return View(emprestimo);
            }

            emprestimo.UsuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var resultadoCriacao = await _emprestimoService.CriarEmprestimo(emprestimo);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o empréstimo (falha ao criar).";

                return View(emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimo criado com sucesso!";

            emprestimo.Copias = await _copiaService.GetCopias();

            return View("CriarEmprestimo", emprestimo);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarEmprestimo()
        {
            AtualizarEmprestimoViewModel emprestimo = new AtualizarEmprestimoViewModel()
            {
                CopiasExistentes = await _copiaService.GetCopias(),
                Emprestimos = await _emprestimoService.GetEmprestimos(),
                Copias = new List<Copia>()
            };

            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEmprestimo(AtualizarEmprestimoViewModel emprestimo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (modelo/dados inválidos).";

                return View(emprestimo);
            }

            var resultadoAtualizar = await _emprestimoService.AtualizarEmprestimo(emprestimo.Id, emprestimo);

            if (resultadoAtualizar == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (falha ao atualizar).";

                return View(emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimo atualizado com sucesso!";

            emprestimo.Emprestimos = await _emprestimoService.GetEmprestimos();

            return View("AtualizarEmprestimo", emprestimo);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarEmprestimo()
        {
            DeletarEmprestimoViewModel emprestimo = new DeletarEmprestimoViewModel()
            {
                Emprestimos = await _emprestimoService.GetEmprestimos()
            };

            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEmprestimo(DeletarEmprestimoViewModel emprestimo)
        {
            if (emprestimo.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o empréstimo (Guid inválido).";

                emprestimo.Emprestimos = await _emprestimoService.GetEmprestimos();

                return View(emprestimo);
            }

            var resultadoDeletar = await _emprestimoService.DeletarEmprestimo(emprestimo.Id);

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o empréstimo (falha ao deletar).";

                emprestimo.Emprestimos = await _emprestimoService.GetEmprestimos();

                return View(emprestimo);
            }

            ViewData["Sucesso"] = "empréstimo deletado com sucesso!";

            emprestimo.Emprestimos = await _emprestimoService.GetEmprestimos();

            return View("DeletarEmprestimo", emprestimo);
        }

        [HttpGet]
        public async Task<IActionResult> ListarEmprestimos()
        {
            var listaEmprestimos = await _emprestimoService.GetEmprestimos();

            if (listaEmprestimos.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os empréstimos (lista vazia ou nula).";

                return View(listaEmprestimos);
            }

            ViewData["Sucesso"] = "Empréstimos listados com sucesso!";

            return View("ListarEmprestimos", listaEmprestimos);
        }
    }
}
