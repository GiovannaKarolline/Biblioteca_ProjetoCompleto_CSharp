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
                emprestimoViewModel.DataPrevistaDevolucao = new DateOnly();
                emprestimoViewModel.Copias.ToList().AddRange(emprestimo.Copias);
            }
            else
            {
                emprestimoViewModel.DataRetirada = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); //apenas para evitar erro de required
                emprestimoViewModel.DataPrevistaDevolucao = new DateOnly(DateTime.Now.Year, (DateTime.Now.Month) + 3, DateTime.Now.Day);
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

            emprestimo.Finalizado = true;

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
            Emprestimo? emprestimo = (await _emprestimoService.GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User)))).FirstOrDefault(emprestimo => emprestimo.Finalizado == false);

            if(emprestimo is not null)
            {
                Copia copia = await _copiaService.GetCopiaById(id);
                await _emprestimoService.AdicionarCopia(copia.Id, emprestimo.Id);
            }

            return View("Emprestimo");
        }
    }
}
