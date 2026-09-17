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

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _enderecoService;
        private readonly ICopiaService _copiaService;

        public EnderecoController(IEnderecoService EnderecoService, ICopiaService copiaService)
        {
            _enderecoService = EnderecoService;
            _copiaService = copiaService;
        }

        [HttpGet]
        public IActionResult CriarEndereco()
        {
            return View(new EnderecoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarEndereco(EnderecoViewModel endereco)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (modelo/dados inválidos).";

                return View(endereco);
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

                return View("CriarEndereco", endereco);
            }

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o endereço (falha ao criar).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "Endereço criado com sucesso!";

            return View("CriarEndereco", endereco);
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
        public async Task<IActionResult> DeletarEndereco(DeletarEnderecoViewModel endereco)
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

            endereco.Enderecos = await _enderecoService.GetEnderecos();

            return View("DeletarEndereco", endereco);
        }

        [HttpGet]
        public async Task<IActionResult> ListarEnderecos()
        {
            var listaEnderecos = await _enderecoService.GetEnderecos();

            if (listaEnderecos.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os endereços (lista vazia ou nula).";

                return View(listaEnderecos);
            }

            ViewData["Sucesso"] = "Endereços listados com sucesso!";

            return View("ListarEnderecos", listaEnderecos);
        }
    }
}
