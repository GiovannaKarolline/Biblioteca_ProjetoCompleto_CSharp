using Biblioteca.Models;
using Biblioteca.Services;
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

                //atualizar emprestimo
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
        public async Task<IActionResult> AdicionarAoEmprestimo(Guid idCopia)
        {
            Emprestimo? emprestimo = (await _emprestimoService
                .GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User))))
                .FirstOrDefault(emprestimo => emprestimo.Finalizado == false);

            EmprestimoViewModel emprestimoViewModel = new EmprestimoViewModel();

            emprestimoViewModel.Copias = new List<Copia>();

            if (emprestimo is null)
            {
                emprestimoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

                Emprestimo novoEmprestimo = await _emprestimoService.CriarEmprestimo(emprestimoViewModel);

                if (novoEmprestimo is null)
                {
                    ViewData["Falha"] = "Falha ao tentar criar empréstimo. Não será possível finalizá-lo.";
                }

                emprestimo = novoEmprestimo;
            }

            emprestimoViewModel.Id = emprestimo.Id;
            emprestimoViewModel.DataRetirada = emprestimo.DataRetirada;
            emprestimoViewModel.Finalizado = emprestimo.Finalizado;
            emprestimoViewModel.DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao;

            Copia? copia = await _copiaService.GetCopiaById(idCopia);

            if(copia is null)
            {
                ViewData["Falha"] = "A cópia não pôde ser adicionada porque não existe.";

                return View("Emprestimo", emprestimoViewModel);
            }

            emprestimo = await _emprestimoService.AdicionarCopia(copia.Id, emprestimoViewModel.Id);

            emprestimoViewModel.Copias = emprestimo.Copias;

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
        public async Task<IActionResult> FinalizarEmprestimo()
        {
            Emprestimo? emprestimo = (await _emprestimoService.GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User))))
                .FirstOrDefault(emprestimo => emprestimo.Finalizado == false);

            if(emprestimo is null)
            {
                ViewData["Falha"] = "Empréstimo inexistente ou vazio. Não é possível finalizar.";

                return View("Emprestimo", new EmprestimoViewModel());
            }

            EmprestimoViewModel emprestimoViewModel = new EmprestimoViewModel()
            {
                Copias = emprestimo.Copias,
                DataDevolucao = emprestimo.DataDevolucao,
                DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao,
                DataRetirada = emprestimo.DataRetirada,
                Finalizado = emprestimo.Finalizado,
                UsuarioId = emprestimo.UsuarioId,
                Id = emprestimo.Id
            };

            if (emprestimo is null)
            {
                ViewData["Falha"] = "O empréstimo não pôde ser finalizado porque não existe.";

                return View("Emprestimo", emprestimoViewModel);
            }

            if (emprestimo.Copias is null || emprestimo.Copias.Count() < 1)
            {
                ViewData["Falha"] = "O empréstimo não pôde ser finalizado porque não possui cópias.";

                return View("Emprestimo", emprestimoViewModel);
            }

            emprestimo.Finalizado = true;
            await _emprestimoService.AtualizarEmprestimo(emprestimo.Id, emprestimoViewModel);

            try
            {
                await _copiaService.EmprestarCopias(emprestimo.Copias);
            }
            catch (Exception excecao)
            {
                ViewData["Falha"] = excecao.Message;

                return View("Emprestimo", emprestimoViewModel);
            }

            return RedirectToAction("Index", "Home");
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
