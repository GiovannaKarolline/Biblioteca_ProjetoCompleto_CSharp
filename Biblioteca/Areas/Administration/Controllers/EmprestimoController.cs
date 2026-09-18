using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Criar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class EmprestimoController : Controller
    {
        private readonly IEmprestimoService _emprestimoService;
        private readonly ICopiaService _copiaService;
        private readonly UserManager<Usuario> _userManager;

        public EmprestimoController(IEmprestimoService emprestimoService, ICopiaService copiaService, UserManager<Usuario> userManager)
        {
            _emprestimoService = emprestimoService;
            _copiaService = copiaService;
            _userManager = userManager;
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

            Emprestimo? resultadoCriacao;

            try
            {
                resultadoCriacao = await _emprestimoService.CriarEmprestimo(emprestimo);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("CriarEmprestimo", emprestimo);
            }

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
                Emprestimos = (await _emprestimoService.GetEmprestimos())
                .Where(emprestimo => emprestimo.Finalizado == true),
                Copias = new List<Copia>()
            };

            return View("AtualizarEmprestimo", emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEmprestimo(AtualizarEmprestimoViewModel emprestimo)
        {
            emprestimo.CopiasExistentes = await _copiaService.GetCopias(); //para trazer os dados de volta para o formulário numa próxima chamada de view

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos())
                .Where(emprestimo => emprestimo.Finalizado == true);

            emprestimo.Copias = new List<Copia>();

            emprestimo.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (modelo/dados inválidos).";

                return View("AtualizarEmprestimo", emprestimo);
            }

            Emprestimo? emprestimoRegistrado;

            try
            {
                emprestimoRegistrado = await _emprestimoService.GetEmprestimoById(emprestimo.Id);

                emprestimo.DataDevolucao = emprestimoRegistrado.DataDevolucao;

                emprestimo.DataPrevistaDevolucao = emprestimoRegistrado.DataPrevistaDevolucao;

                emprestimo.DataRetirada = emprestimoRegistrado.DataRetirada;

            }catch(Exception excecao)
            {
                ViewData["Falha"] = excecao.Message;

                return View("AtualizarEmprestimo", emprestimo);
            }

            foreach(Copia copia in emprestimoRegistrado.Copias)
            {
                if (emprestimo.DataDevolucao is null)
                {
                    copia.StatusDisponibilidade = false;
                }
            }

            await _emprestimoService.EditarCopiasEmprestimo(emprestimo.IdCopias, emprestimo.Id);

            Emprestimo? resultadoAtualizar;

            try
            {
                resultadoAtualizar = await _emprestimoService.AtualizarEmprestimo(emprestimo.Id, emprestimo);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("AtualizarEmprestimo", emprestimo);
            }

            if (resultadoAtualizar == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (falha ao atualizar).";

                return View("AtualizarEmprestimo", emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimo atualizado com sucesso!";

            emprestimo.CopiasExistentes = await _copiaService.GetCopias();

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos())
                .Where(emprestimo => emprestimo.Finalizado == true);

            emprestimo.Copias = new List<Copia>();

            return View("AtualizarEmprestimo", emprestimo);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarEmprestimo()
        {
            DeletarEmprestimoViewModel emprestimo = new DeletarEmprestimoViewModel()
            {
                Emprestimos = (await _emprestimoService
                .GetEmprestimosByUsuarioId(
                    Guid.Parse(_userManager.GetUserId(User))
                )
                ).Where(emprestimo => emprestimo.Finalizado == true)
                .ToList()
            };

            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEmprestimo(Guid id)
        {
            if (id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o empréstimo (Guid inválido).";

                return RedirectToAction("DeletarEmprestimo");
            }

            Emprestimo? resultadoDeletar;

            try
            {
                resultadoDeletar = await _emprestimoService.DeletarEmprestimo(id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return RedirectToAction("DeletarEmprestimo");
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o empréstimo (falha ao deletar).";

                return RedirectToAction("DeletarEmprestimo");
            }

            ViewData["Sucesso"] = "empréstimo deletado com sucesso!";

            return RedirectToAction("DeletarEmprestimo");
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
