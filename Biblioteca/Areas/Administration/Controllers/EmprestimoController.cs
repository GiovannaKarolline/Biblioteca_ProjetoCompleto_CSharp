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
                ViewData["Falha"] = "Não foi possível criar o empréstimo (modelo/dados inválidos).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            List<Copia> listaCopiasSelecionadas = new List<Copia>();

            foreach (Guid id in emprestimo.CopiasSelecionadas)
            {
                Copia copiaRegistrada = await _copiaService.GetCopiaById(id);

                if (!emprestimo.Copias.Contains(copiaRegistrada))
                {
                    emprestimo.Copias.Add(copiaRegistrada);
                }

                listaCopiasSelecionadas.Add(copiaRegistrada);
            }

            List<Copia> listaCopias = emprestimo.Copias;

            foreach (var copia in listaCopias)
            {
                if (!listaCopiasSelecionadas.Contains(copia))
                {
                    emprestimo.Copias.Remove(copia);
                }
            }

            Emprestimo? resultadoCriacao;

            try
            {
                resultadoCriacao = await _emprestimoService.CriarEmprestimo(emprestimo);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o empréstimo (falha ao criar).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimo criado com sucesso!";

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
            emprestimo.Usuarios = await _usuarioService.GetUsuarios();
            emprestimo.CopiasExistentes = await _copiaService.GetCopias();

            return View("Index", emprestimo);
        }


        [HttpPost]
        public async Task<IActionResult> AtualizarEmprestimo(EmprestimoViewModel emprestimo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (modelo/dados inválidos).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

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
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (falha ao atualizar).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            ViewData["Sucesso"] = "Empréstimo atualizado com sucesso!";

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
            emprestimo.Usuarios = await _usuarioService.GetUsuarios();
            emprestimo.CopiasExistentes = await _copiaService.GetCopias();

            return View("Index", emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEmprestimo(EmprestimoViewModel emprestimo)
        {
            if (emprestimo.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o endereço (Guid inválido).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

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
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o endereço (falha ao deletar).";

                emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
                emprestimo.Usuarios = await _usuarioService.GetUsuarios();
                emprestimo.CopiasExistentes = await _copiaService.GetCopias();

                return View("Index", emprestimo);
            }

            ViewData["Sucesso"] = "Endereço deletado com sucesso!";

            emprestimo.Emprestimos = (await _emprestimoService.GetEmprestimos()).ToPagedList(1, 6);
            emprestimo.Usuarios = await _usuarioService.GetUsuarios();
            emprestimo.CopiasExistentes = await _copiaService.GetCopias();

            return View("Index", emprestimo);
        }

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
