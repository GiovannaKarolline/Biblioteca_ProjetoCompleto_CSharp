using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Models;
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

            var resultadoAtualizacao = await _usuarioService.AtualizarUsuario(usuario.Id, usuario);

            if (resultadoAtualizacao == null)
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
        public async Task<IActionResult> DeletarUsuario(DeletarUsuarioViewModel usuario)
        {
            if (usuario.Id == Guid.Empty)
            {
                ViewData["Falha"] = "Não foi possível deletar o usuário (Guid inválido).";

                usuario.Usuarios = await _usuarioService.GetUsuarios();

                return View(usuario);
            }

            var resultadoDeletar = await _usuarioService.DeletarUsuario(usuario.Id);

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o usuário (falha ao deletar).";

                usuario.Usuarios = await _usuarioService.GetUsuarios();

                return View(usuario);
            }

            ViewData["Sucesso"] = "Usuário deletado com sucesso!";

            usuario.Usuarios = await _usuarioService.GetUsuarios();

            return View("DeletarUsuario", usuario);
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
