using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Biblioteca.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        private readonly ICopiaService _copiaService;
        private readonly UserManager<Usuario> _userManager;

        public EnderecoController(IEnderecoService EnderecoService, ICopiaService copiaService, UserManager<Usuario> userManager)
        {
            _enderecoService = EnderecoService;
            _copiaService = copiaService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CadastrarEndereco()
        {
            return View("CadastrarEndereco", new EnderecoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarEndereco(EnderecoViewModel endereco)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (modelo/dados inválidos).";

                return View("CadastrarEndereco", endereco);
            }

            endereco.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

            Endereco? resultadoCadastro;

            try
            {
                resultadoCadastro = await _enderecoService.CriarEndereco(endereco);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("CadastrarEndereco", endereco);
            }

            if (resultadoCadastro == null)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (falha ao criar).";

                return View(endereco);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarEndereco()
        {
            AtualizarEnderecoViewModel endereco = new AtualizarEnderecoViewModel()
            {
                Enderecos = await _enderecoService.GetEnderecos()
            };

            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(AtualizarEnderecoViewModel endereco)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o endereço (modelo/dados inválidos).";

                return View(endereco);
            }

            Endereco? resultadoAtualizacao;

            try
            {
                resultadoAtualizacao = await _enderecoService.AtualizarEndereco(endereco.Id, endereco);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("CriarEndereco", endereco);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o endereço (falha ao atualizar).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "Endereço atualizado com sucesso!";

            endereco.Enderecos = await _enderecoService.GetEnderecos();

            return View("AtualizarEndereco", endereco);
        }
    }
}
