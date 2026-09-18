using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Enums;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEmprestimoService _emprestimoService;
        private readonly UserManager<Usuario> _userManager;

        public ContaController(IUsuarioService usuarioService, IEmprestimoService emprestimoService, UserManager<Usuario> userManager)
        {
            _usuarioService = usuarioService;
            _emprestimoService = emprestimoService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View("Cadastro", new UsuarioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            Usuario? resultadoCadastro;

            try
            {
                resultadoCadastro = await _usuarioService.CadastrarUsuario(usuario);

            }catch(Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(usuario);
            }
            
            if(resultadoCadastro is null)
            {
                return View(usuario);
            }

            await _usuarioService.LogarUsuario(new LoginViewModel()
            {
                NomeUsuario = usuario.NomeUsuario,
                Senha = usuario.Senha
            });

            return RedirectToAction("CadastrarEndereco", "Endereco");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            Usuario? resultadoLogin;

            try { 

                resultadoLogin = await _usuarioService.LogarUsuario(usuario);

            }catch(Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(usuario);
            }

            if (resultadoLogin is null)
            {
                return View(usuario);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Deslogar()
        {
            _usuarioService.DeslogarUsuario();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarUsuario()
        {
            AtualizarUsuarioViewModel Usuario = new AtualizarUsuarioViewModel()
            {
                Usuarios = await _usuarioService.GetUsuarios()
            };

            return View(Usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarUsuario(AtualizarUsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar a Usuario literária (modelo/dados inválidos).";

                return View(usuario);
            }

            Usuario? resultadoAtualizacao;

            try
            {
                resultadoAtualizacao = await _usuarioService.AtualizarUsuario(usuario.Id, usuario);
            }
            catch(Exception excecao)
            {
                ViewData["Falha"] = excecao.Message;

                return View(usuario);
            }
            

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o usuário (falha ao atualizar).";

                return View(usuario);
            }

            ViewData["Sucesso"] = "Usuário atualizado com sucesso!";

            usuario.Usuarios = await _usuarioService.GetUsuarios();

            return View("AtualizarUsuario", usuario);
        }

        [HttpGet]
        public async Task<IActionResult> VisualizarPerfil()
        {
            IEnumerable<Emprestimo> emprestimosUsuario = (await _emprestimoService.GetEmprestimosByUsuarioId(Guid.Parse(_userManager.GetUserId(User))))
                .Where(emprestimo => emprestimo.Finalizado == true);

            return View(emprestimosUsuario);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
