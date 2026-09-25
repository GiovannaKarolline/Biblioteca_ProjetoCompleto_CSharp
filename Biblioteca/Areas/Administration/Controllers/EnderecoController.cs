using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using X.PagedList;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IUsuarioService _usuarioService;

        public EnderecoController(IEnderecoService enderecoService, IUsuarioService usuarioService)
        {
            _enderecoService = enderecoService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            EnderecoViewModel endereco = new EnderecoViewModel();

            endereco.Enderecos = (await _enderecoService.GetEnderecos()).ToPagedList(paginaAtual ?? 1, 6);
            endereco.Usuarios = (await _usuarioService.GetUsuarios()).ToList();

            if (endereco.Enderecos.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os endereços (lista vazia ou nula).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "Endereços listados com sucesso!";

            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> CriarEndereco(EnderecoViewModel endereco)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (modelo/dados inválidos).";

                return View("Index", endereco);
            }

            endereco.UsuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Endereco? resultadoCriacao;

            try
            {
                resultadoCriacao = await _enderecoService.CriarEndereco(endereco);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("Index", endereco);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (falha ao criar).";

                return View("Index", endereco);
            }

            ViewData["Sucesso"] = "Endereço criado com sucesso!";

            return View("Index", endereco);
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

        [HttpGet]
        public async Task<IActionResult> DeletarEndereco()
        {
            DeletarEnderecoViewModel endereco = new DeletarEnderecoViewModel()
            {
                Enderecos = await _enderecoService.GetEnderecos()
            };

            return View(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEndereco(EnderecoViewModel endereco)
        {
            if (endereco.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o endereço (Guid inválido).";

                return View(endereco);
            }

            Endereco? resultadoDeletar;

            try
            {
                resultadoDeletar = await _enderecoService.DeletarEndereco(endereco.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("CriarEndereco", endereco);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o endereço (falha ao deletar).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "Endereço deletado com sucesso!";

            endereco.Enderecos = (await _enderecoService.GetEnderecos()).ToPagedList(1, 6);

            return View("DeletarEndereco", endereco);
        }

    }
}
