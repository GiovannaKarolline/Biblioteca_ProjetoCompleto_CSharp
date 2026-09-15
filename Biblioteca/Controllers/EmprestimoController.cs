using Biblioteca.Models;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            Emprestimo? emprestimo = (await _emprestimoService.GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User)))).FirstOrDefault(emprestimo => emprestimo.Finalizado == false);

            EmprestimoViewModel emprestimoViewModel = new EmprestimoViewModel();

            if (emprestimo is not null)
            {
                emprestimoViewModel.UsuarioId = emprestimo.UsuarioId;
                emprestimoViewModel.DataRetirada = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                emprestimoViewModel.DataPrevistaDevolucao = new DateOnly(DateTime.Now.Year, (DateTime.Now.Month) + 3, DateTime.Now.Day);
                emprestimoViewModel.Copias = new List<Copia>();

                if(emprestimo.Copias is not null)
                {
                    emprestimoViewModel.Copias = emprestimo.Copias;
                }
            }
            else
            {
                emprestimoViewModel.DataRetirada = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                emprestimoViewModel.DataPrevistaDevolucao = new DateOnly(DateTime.Now.Year, (DateTime.Now.Month) + 3, DateTime.Now.Day);
                emprestimoViewModel.Copias = new List<Copia>();
                emprestimoViewModel.Finalizado = false;
                emprestimoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));
            }

            return View("Emprestimo", emprestimoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmprestimoViewModel emprestimo)
        {
            if (!ModelState.IsValid)
            {
                return View("Emprestimo", emprestimo);
            }

            var resultadoCriacao = await _emprestimoService.CriarEmprestimo(emprestimo);

            if(resultadoCriacao is null)
            {
                ModelState.AddModelError("_emprestimoService.CriarEmprestimo", "Falha ao criar empréstimo.");

                return View("Emprestimo", emprestimo);
            }

            return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAoEmprestimo(Guid id)
        {
            Emprestimo? emprestimo = (await _emprestimoService
                .GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User))))
                .FirstOrDefault(emprestimo => emprestimo.Finalizado == false);

            EmprestimoViewModel emprestimoViewModel = new EmprestimoViewModel();

            if(emprestimo is null)
            {
                emprestimoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

                Emprestimo novoEmprestimo = await _emprestimoService.CriarEmprestimo(emprestimoViewModel);

                emprestimoViewModel.Copias = novoEmprestimo.Copias;

                emprestimo = novoEmprestimo;
            }
            else
            {
                emprestimoViewModel.Copias = emprestimo.Copias;
            }

            emprestimoViewModel.Id = emprestimo.Id;
            emprestimoViewModel.DataRetirada = emprestimo.DataRetirada;
            emprestimoViewModel.Finalizado = emprestimo.Finalizado;
            emprestimoViewModel.DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao;

            Copia copia = await _copiaService.GetCopiaById(id);
            await _emprestimoService.AdicionarCopia(copia.Id, emprestimoViewModel.Id);

            return View("Emprestimo", emprestimoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoverDoEmprestimo(Guid idCopia)
        {
            Guid idUsuario = Guid.Parse(_userManager.GetUserId(User));

            Emprestimo? resultadoDeletar = await _emprestimoService.RemoverCopia(idCopia, idUsuario);

            if (resultadoDeletar is null)
            {
                ViewData["Falha"] = "Cópia não removida. Falha ao tentar remover.";
            }

            return RedirectToAction("Index"); //para ir para o get e atualizar a lista de empréstimos
        }

        [HttpPost]
        public async Task<IActionResult> RealizarDevolucao(Guid idEmprestimo)
        {
            Emprestimo? emprestimo = await _emprestimoService.GetEmprestimoById(idEmprestimo);

            if (emprestimo is null)
            {
                ViewData["Falha"] = "Não existe um empréstimo com este Id.";

                return View();
            }

            Emprestimo? resultadoAtualizacao = await _emprestimoService.RealizarDevolucao(idEmprestimo);

            if(resultadoAtualizacao is null)
            {
                ViewData["Falha"] = "Não foi possível realizar a devolução do empréstimo.";
            }

            return View();
        }
    }
}
