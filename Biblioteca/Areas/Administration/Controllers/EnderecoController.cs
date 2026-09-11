using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
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
                ViewData["Falha"] = "Não foi possível criar o empréstimo (modelo/dados inválidos).";

                return View(endereco);
            }

            endereco.UsuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var resultadoCriacao = await _enderecoService.CriarEndereco(endereco);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível criar o empréstimo (falha ao criar).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "Empréstimo criado com sucesso!";

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
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (modelo/dados inválidos).";

                return View(endereco);
            }

            var resultadoCriacao = await _enderecoService.AtualizarEndereco(endereco.Id, endereco);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o empréstimo (falha ao atualizar).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "Empréstimo atualizado com sucesso!";

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
                ViewData["Falha"] = "Não foi possível deletar o empréstimo (Guid inválido).";

                return View(endereco);
            }

            var resultadoCriacao = await _enderecoService.DeletarEndereco(endereco.Id);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o empréstimo (falha ao deletar).";

                return View(endereco);
            }

            ViewData["Sucesso"] = "empréstimo deletado com sucesso!";

            endereco.Enderecos = await _enderecoService.GetEnderecos();

            return View("DeletarEndereco", endereco);
        }

        [HttpGet]
        public async Task<IActionResult> ListarEnderecos()
        {
            var listaEnderecos = await _enderecoService.GetEnderecos();

            if (listaEnderecos.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os empréstimos (lista vazia ou nula).";

                return View(listaEnderecos);
            }

            ViewData["Sucesso"] = "Empréstimos listados com sucesso!";

            return View("ListarEnderecos", listaEnderecos);
        }
    }
}
