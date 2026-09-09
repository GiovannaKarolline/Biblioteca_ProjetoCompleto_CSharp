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
            return View("Cadastro", new CadastroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(CadastroViewModel usuario)
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

        public IActionResult Logout()
        {
            return View();
        }
    }
}
