using Biblioteca.Areas.Administration.ViewModels.Atualizar;
using Biblioteca.Areas.Administration.ViewModels.Deletar;
using Biblioteca.Enums;
using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using X.PagedList;

namespace Biblioteca.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarUsuario(Guid id)
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
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(usuario);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar a Usuario literária (falha ao atualizar).";

                return View(usuario);
            }

            ViewData["Sucesso"] = "Usuario literária atualizada com sucesso!";

            usuario.Usuarios = await _usuarioService.GetUsuarios();

            return View("AtualizarUsuario", usuario);
        }

        //[HttpGet]
        //public async Task<IActionResult> DeletarUsuario()
        //{
        //    DeletarUsuarioViewModel usuario = new DeletarUsuarioViewModel()
        //    {
        //        Usuarios = await _usuarioService.GetUsuarios()
        //    };

        //    return View(usuario);
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeletarUsuario(DeletarUsuarioViewModel usuario)
        //{
        //    if (usuario.Id == Guid.Empty)
        //    {
        //        ViewData["Falha"] = "Não foi possível deletar o usuário (Guid inválido).";

        //        usuario.Usuarios = await _usuarioService.GetUsuarios();

        //        return View(usuario);
        //    }

        //    Usuario? resultadoDeletar;

        //    try
        //    {
        //        resultadoDeletar = await _usuarioService.DeletarUsuario(usuario.Id);
        //    }
        //    catch (Exception exception)
        //    {
        //        ViewData["Falha"] = exception.Message;

        //        return View(usuario);
        //    }

        //    if (resultadoDeletar == null)
        //    {
        //        ViewData["Falha"] = "Não foi possível deletar o usuário (falha ao deletar).";

        //        usuario.Usuarios = await _usuarioService.GetUsuarios();

        //        return View(usuario);
        //    }

        //    ViewData["Sucesso"] = "Usuário deletado com sucesso!";

        //    usuario.Usuarios = await _usuarioService.GetUsuarios();

        //    return View("DeletarUsuario", usuario);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ListarUsuarios()
        //{
        //    var listaUsuarios = await _usuarioService.GetUsuarios();

        //    if (listaUsuarios.IsNullOrEmpty())
        //    {
        //        ViewData["Falha"] = "Não foi possível listar as Usuarios  (lista vazia ou nula).";

        //        return View(listaUsuarios);
        //    }

        //    ViewData["Sucesso"] = "Usuários listados com sucesso!";

        //    return View("ListarUsuarios", listaUsuarios);
        //}

        [HttpGet]
        public async Task<IActionResult> Index(int? paginaAtual)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();

            usuario.Usuarios = (await _usuarioService.GetUsuarios()).ToPagedList(paginaAtual ?? 1 , 6);

            if (usuario.Usuarios.IsNullOrEmpty())
            {
                ViewData["Falha"] = "Não foi possível listar os usuários (lista vazia ou nula).";

                return View(usuario);
            }

            ViewData["Sucesso"] = "Usuários listados com sucesso!";

            return View("Index", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarAdministrador(UsuarioViewModel usuario)
        {

            //if (ModelState.IsValid)
            //{
            //    ViewData["Falha"] = "Não foi possível atualizar a Usuario literária (modelo/dados inválidos).";

            //    return View(new UsuarioViewModel());
            //}

            //usuario.Cargo = Cargo.Administrador;

            //Usuario? resultadoCriacao;

            //try
            //{
            //    resultadoCriacao = await _usuarioService.CadastrarUsuario(usuario);

            //}
            //catch (Exception exception)
            //{
            //    ViewData["Falha"] = exception.Message;

            //    return View(usuario);
            //}

            //if (resultadoCriacao is null)
            //{
            //    ViewData["Falha"] = "A criação falhou.";

            //    return View(usuario);
            //}

            return View(new UsuarioViewModel());

        }
    }
}
