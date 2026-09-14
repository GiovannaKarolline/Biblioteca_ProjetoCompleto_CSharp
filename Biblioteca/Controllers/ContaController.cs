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

        public ContaController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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

            var resultado = await _usuarioService.CadastrarUsuario(usuario);

            if(resultado is null)
            {
                return View(usuario);
            }

            return RedirectToAction("Login");
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

            var resultado = await _usuarioService.LogarUsuario(usuario);

            if (resultado is null)
            {
                return View(usuario);
            }

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

            var resultadoCriacao = await _usuarioService.AtualizarUsuario(usuario.Id, usuario);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a Usuario literária (falha ao atualizar).";

                return View(usuario);
            }

            ViewData["Sucesso"] = "Usuário atualizado com sucesso!";

            usuario.Usuarios = await _usuarioService.GetUsuarios();

            return View("AtualizarUsuario", usuario);
        }

        [HttpGet]
        public IActionResult VisualizarPerfil()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
