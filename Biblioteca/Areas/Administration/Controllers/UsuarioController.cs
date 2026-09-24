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
        private readonly IEnderecoService _enderecoService;

        public UsuarioController(IUsuarioService usuarioService, IEnderecoService enderecoService)
        {
            _usuarioService = usuarioService;
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível criar o usuário (modelo/dados inválidos).";

                return View("Index", usuario);
            }

            Usuario? resultadoCadastro;

            try
            {
                resultadoCadastro = await _usuarioService.CadastrarUsuario(usuario);

            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("Index", usuario);
            }

            if (resultadoCadastro is null)
            {
                ViewData["Falha"] = "A criação falhou.";

                return View("Index", usuario);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarUsuario(UsuarioViewModel usuario)
        {
            Endereco enderecoRegistrado = await _enderecoService.GetEnderecoByUsuarioId(usuario.Id);

            usuario.Endereco = new EnderecoViewModel(); //o endereço sempre vem nulo porque objetos não são passados diretamente por tag helpers

            if(enderecoRegistrado is not null)
            {
                usuario.Endereco.Logradouro = enderecoRegistrado.Logradouro;
                usuario.Endereco.TipoLogradouro = enderecoRegistrado.TipoLogradouro;
                usuario.Endereco.Numero = enderecoRegistrado.Numero;
                usuario.Endereco.UsuarioId = usuario.Id;
                usuario.Endereco.Complemento = enderecoRegistrado.Complemento;
                usuario.Endereco.Cep = enderecoRegistrado.Cep;
            }
            else
            {
                ViewData["Falha"] = "Não foi possível atualizar o usuário porque ele não possui um endereço.";

                return View("Index", usuario);
            }

            if (!ModelState.IsValid)
            {
                ViewData["Falha"] = "Não foi possível atualizar o usuário (modelo/dados inválidos).";

                return View("Index", usuario);
            }

            Usuario? resultadoAtualizacao;

            try
            {
                resultadoAtualizacao = await _usuarioService.AtualizarUsuario(usuario);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View("Index", usuario);
            }

            if (resultadoAtualizacao == null)
            {
                ViewData["Falha"] = "Não foi possível atualizar o usuário (falha ao atualizar).";

                return View("Index", usuario);
            }

            ViewData["Sucesso"] = "Usuário atualizado com sucesso!";

            usuario.Usuarios = (await _usuarioService.GetUsuarios()).ToPagedList(1, 6);

            return View("Index", usuario);
        }

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
        public async Task<IActionResult> DeletarUsuario(Guid id)
        {
            Usuario? usuario = await _usuarioService.GetUsuarioById(id);

            if (usuario is null)
            {
                ViewData["Falha"] = "Não foi possível deletar o usuário (Guid inválido).";

                return View("Index", new UsuarioViewModel() { Usuarios = (await _usuarioService.GetUsuarios()).ToPagedList(1, 6) });
            }

            Usuario? resultadoDeletar;

            try
            {
                resultadoDeletar = await _usuarioService.DeletarUsuario(usuario.Id);
            }
            catch (Exception exception)
            {
                ViewData["Falha"] = exception.Message;

                return View(usuario);
            }

            if (resultadoDeletar == null)
            {
                ViewData["Falha"] = "Não foi possível deletar o usuário (falha ao deletar).";

                return View("Index", new UsuarioViewModel() { Usuarios = (await _usuarioService.GetUsuarios()).ToPagedList(1, 6) });
            }

            ViewData["Sucesso"] = "Usuário deletado com sucesso!";

            return View("Index", new UsuarioViewModel() { Usuarios = (await _usuarioService.GetUsuarios()).ToPagedList(1, 6) });
        }
    }
}