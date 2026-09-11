using Biblioteca.Areas.Administration.ViewModels;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult CriarUsuario()
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar a Usuario literária (modelo/dados inválidos).";

                return View(usuario);
            }

            // var resultadoCriacao = await _usuarioService.CadastrarUsuario(usuario); ainda usa CadastroViewModel

            //if (resultadoCriacao == null)
            //{
            //    ViewData["Falha"] = "Não foi possível criar a Usuario literária (falha ao criar).";

            //    return View(usuario);
            //}

            ViewData["Sucesso"] = "Usuario literária criada com sucesso!";

            return View("CriarUsuario", usuario);
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

            ViewData["Sucesso"] = "Usuario literária atualizada com sucesso!";

            usuario.Usuarios = await _usuarioService.GetUsuarios();

            return View("AtualizarUsuario", usuario);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarUsuario()
        {
            DeletarUsuarioViewModel usuario = new DeletarUsuarioViewModel()
            {
                Usuarios = await _usuarioService.GetUsuarios()
            };

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> DeletarUsuario(DeletarUsuarioViewModel Usuario)
        {
            if (Usuario.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar a Usuario literária (Guid inválido).";

                return View(Usuario);
            }

            var resultadoCriacao = await _usuarioService.DeletarUsuario(Usuario.Id);

            if (resultadoCriacao == null)
            {
                ViewData["Falha"] = "Não foi possível deletar a Usuario literária (falha ao deletar).";

                return View(Usuario);
            }

            ViewData["Sucesso"] = "Usuario deletado com sucesso!";

            Usuario.Usuarios = await _usuarioService.GetUsuarios();

            return View("DeletarUsuario", Usuario);
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var listaUsuarios = await _usuarioService.GetUsuarios();

            if (listaUsuarios.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar as Usuarios  (lista vazia ou nula).";

                return View(listaUsuarios);
            }

            ViewData["Sucesso"] = "Usuários listados com sucesso!";

            return View("ListarUsuarios", listaUsuarios);
        }
    }
}
