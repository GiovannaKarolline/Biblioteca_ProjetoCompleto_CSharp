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
using X.PagedList;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class EmprestimoController : Controller
    {
        private readonly IEmprestimoService _emprestimoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICopiaService _copiaService;
        private readonly UserManager<Usuario> _userManager;

        public EmprestimoController(IEmprestimoService emprestimoService, IUsuarioService usuarioService, ICopiaService copiaService, UserManager<Usuario> userManager)
        {
            _emprestimoService = emprestimoService;
            _usuarioService = usuarioService;
            _copiaService = copiaService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            EmprestimoViewModel emprestimo = new EmprestimoViewModel();

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(paginaAtual ?? 1, 6);
            emprestimo.Usuarios = await _usuarioService.GetUsuarios();
            emprestimo.CopiasExistentes = await _copiaService.GetCopias();

            if (emprestimo.Emprestimos.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os empréstimos (lista vazia ou nula).";

                return View(emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimos listados com sucesso!";

            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> CriarEmprestimo(EmprestimoViewModel emprestimo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (modelo/dados inválidos).";

                return View("Index", emprestimo);
            }

            emprestimo.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

            Emprestimo? resultadoCriacao;

            try
            {
                resultadoCriacao = await _emprestimoService.CriarEmprestimo(emprestimo);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("Index", emprestimo);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o empréstimo (falha ao criar).";

                return View("Index", emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimo criado com sucesso!";

            return View("Index", emprestimo);
        }


        [HttpPost]
        public async Task<IActionResult> AtualizarEmprestimo(EmprestimoViewModel emprestimo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o endereço (modelo/dados inválidos).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

                return View("Index", emprestimo);
            }

            Emprestimo? resultadoAtualizacao;

            try
            {
                resultadoAtualizacao = await _emprestimoService.AtualizarEmprestimo(emprestimo.Id, emprestimo);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

                return View("Index", emprestimo);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o endereço (falha ao atualizar).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

                return View("Index", emprestimo);
            }

            ViewData["Sucesso"] = "Endereço atualizado com sucesso!";

            emprestimo.Emprestimos = await _emprestimoService.GetEmprestimos();

            return View("Index", emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEmprestimo(EmprestimoViewModel emprestimo)
        {
            if (emprestimo.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o endereço (Guid inválido).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

                return View("Index", emprestimo);
            }

            Emprestimo? resultadoDeletar;

            try
            {
                resultadoDeletar = await _emprestimoService.DeletarEmprestimo(emprestimo.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

                return View("Index", emprestimo);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o endereço (falha ao deletar).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

                return View("Index", emprestimo);
            }

            ViewData["Sucesso"] = "Endereço deletado com sucesso!";

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);

            return View("Index", emprestimo);
        }

        //        //[HttpGet]
        //        //public async Task<IActionResult> CriarEmprestimo()
        //        //{
        //        //    CriarEmprestimoViewModel emprestimo = new CriarEmprestimoViewModel()
        //        //    {
        //        //        CopiasExistentes = await _copiaService.GetCopias()
        //        //    };

        //        //    return View(emprestimo);
        //        //}

        //        //[HttpPost]
        //        //public async Task<IActionResult> CriarEmprestimo(CriarEmprestimoViewModel emprestimo)
        //        //{
        //        //    if (!ModelState.IsValid)
        //        //    {
        //        //        ViewData["Falha"] = "Não foi possível criar o empréstimo (modelo/dados inválidos).";

        //        //        return View(emprestimo);
        //        //    }

        //        //    emprestimo.UsuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        //        //    Emprestimo? resultadoCriacao;

        //        //    try
        //        //    {
        //        //        resultadoCriacao = await _emprestimoService.CriarEmprestimo(emprestimo);
        //        //    }
        //        //    catch (Exception exception)
        //        //    {
        //        //        ViewData["Falha"] = exception.Message;

        //        //        return View("CriarEmprestimo", emprestimo);
        //        //    }

        //        //    if (resultadoCriacao == null)
        //        //    {
        //        //        ViewData["Falha"] = "Não foi possível criar o empréstimo (falha ao criar).";

        //        //        return View(emprestimo);
        //        //    }

        //        //    ViewData["Sucesso"] = "Empréstimo criado com sucesso!";

        //        //    emprestimo.Copias = await _copiaService.GetCopias();

        //        //    return View("CriarEmprestimo", emprestimo);
        //        //}

        //        //[HttpGet]
        //        //public async Task<IActionResult> AtualizarEmprestimo()
        //        //{
        //        //    AtualizarEmprestimoViewModel emprestimo = new AtualizarEmprestimoViewModel()
        //        //    {
        //        //        CopiasExistentes = await _copiaService.GetCopias(),
        //        //        Emprestimos = (await _emprestimoService.GetEmprestimos())
        //        //        .Where(emprestimo => emprestimo.Finalizado == true),
        //        //        Copias = new List<Copia>()
        //        //    };

        //        //    return View("AtualizarEmprestimo", emprestimo);
        //        //}

        //        //[HttpPost]
        //        //public async Task<IActionResult> AtualizarEmprestimo(AtualizarEmprestimoViewModel emprestimo)
        //        //{
        //        //    emprestimo.CopiasExistentes = await _copiaService.GetCopias(); //para trazer os dados de volta para o formulário numa próxima chamada de view

        //        //    emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos())
        //        //        .Where(emprestimo => emprestimo.Finalizado == true);

        //        //    emprestimo.Copias = new List<Copia>();

        //        //    emprestimo.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

        //        //    if (!ModelState.IsValid)
        //        //    {
        //        //        ViewData["Falha"] = "Não foi possível atualizar o empréstimo (modelo/dados inválidos).";

        //        //        return View("AtualizarEmprestimo", emprestimo);
        //        //    }

        //        //    Emprestimo? emprestimoRegistrado;

        //        //    try
        //        //    {
        //        //        emprestimoRegistrado = await _emprestimoService.GetEmprestimoById(emprestimo.Id);

        //        //        emprestimo.DataDevolucao = emprestimoRegistrado.DataDevolucao;

        //        //        emprestimo.DataPrevistaDevolucao = emprestimoRegistrado.DataPrevistaDevolucao;

        //        //        emprestimo.DataRetirada = emprestimoRegistrado.DataRetirada;

        //        //    }catch(Exception excecao)
        //        //    {
        //        //        ViewData["Falha"] = excecao.Message;

        //        //        return View("AtualizarEmprestimo", emprestimo);
        //        //    }

        //        //    foreach(Copia copia in emprestimoRegistrado.Copias)
        //        //    {
        //        //        if (emprestimo.DataDevolucao is null)
        //        //        {
        //        //            copia.StatusDisponibilidade = false;
        //        //        }
        //        //    }

        //        //    await _emprestimoService.EditarCopiasEmprestimo(emprestimo.IdCopias, emprestimo.Id);

        //        //    Emprestimo? resultadoAtualizar;

        //        //    try
        //        //    {
        //        //        resultadoAtualizar = await _emprestimoService.AtualizarEmprestimo(emprestimo.Id, emprestimo);
        //        //    }
        //        //    catch (Exception exception)
        //        //    {
        //        //        ViewData["Falha"] = exception.Message;

        //        //        return View("AtualizarEmprestimo", emprestimo);
        //        //    }

        //        //    if (resultadoAtualizar == null)
        //        //    {
        //        //        ViewData["Falha"] = "Não foi possível atualizar o empréstimo (falha ao atualizar).";

        //        //        return View("AtualizarEmprestimo", emprestimo);
        //        //    }

        //        //    ViewData["Sucesso"] = "Empréstimo atualizado com sucesso!";

        //        //    emprestimo.CopiasExistentes = await _copiaService.GetCopias();

        //        //    emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos())
        //        //        .Where(emprestimo => emprestimo.Finalizado == true);

        //        //    emprestimo.Copias = new List<Copia>();

        //        //    return View("AtualizarEmprestimo", emprestimo);
        //        //}

        //        //[HttpGet]
        //        //public async Task<IActionResult> DeletarEmprestimo()
        //        //{
        //        //    DeletarEmprestimoViewModel emprestimo = new DeletarEmprestimoViewModel()
        //        //    {
        //        //        Emprestimos = (await _emprestimoService
        //        //        .GetEmprestimosByUsuarioId(
        //        //            Guid.Parse(_userManager.GetUserId(User))
        //        //        )
        //        //        ).Where(emprestimo => emprestimo.Finalizado == true)
        //        //        .ToList()
        //        //    };

        //        //    return View(emprestimo);
        //        //}

        [HttpGet]
        public async Task<IActionResult> GetQuantidadeCopias()
        {
            var emprestimos = await _emprestimoService
                .GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User)));

            var quantidadeCopias = emprestimos
                .Where(emprestimo => emprestimo.Finalizado == false)
                .SelectMany(emprestimo => emprestimo.Copias)
                .Count();

            return Ok(quantidadeCopias);
        }
    }
}
