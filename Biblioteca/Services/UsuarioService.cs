using Biblioteca.Enums;
using Biblioteca.Models;
using Biblioteca.Repositories.Interfaces;
using Biblioteca.Services.Interfaces;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> CadastrarUsuario(CadastroViewModel usuario)
        {
            if(usuario.Email.Substring(usuario.Email.IndexOf("@")) == "@admin.com.br")
            {
                usuario.Cargo = Cargo.Administrador;
            }

            if ((await _usuarioRepository.GetUsuariosByNome(usuario.NomeUsuario))
                .FirstOrDefault(usuarioRegistrado => usuarioRegistrado.UserName == usuario.NomeUsuario) is not null)
            {
                throw new ArgumentException("O nome de usuário já existe, então o usuário não pôde ser cadastrado.");
            }

            Usuario novoUsuario = new Usuario() 
            { 
                UserName = usuario.NomeUsuario,
                Cargo = usuario.Cargo,
                Email = usuario.Email,
                Senha = usuario.Senha,
                DataNascimento = usuario.DataNascimento,
                PhoneNumber = usuario.NumeroTelefone
            };

            var resultado = await _usuarioRepository.CriarUsuario(novoUsuario);

            if (resultado is not null)
            {
                return resultado;
            }
            return null;
        }

        public async Task<Usuario> DeletarUsuario(Guid id)
        {
            Usuario? usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario is not null && usuario.Deletado == false)
            {
                await _usuarioRepository.DeletarUsuario(usuario);
            }
            else
            {
                throw new ArgumentException("Não é possível deletar este usuário porque ele não existe.");
            }

            return await Task.FromResult(usuario);
        }

        public async Task<Usuario> AtualizarUsuario(Guid id, UsuarioViewModel usuario)
        {
            Usuario? usuarioRegistrado = await _usuarioRepository.GetUsuarioById(id);

            if (usuarioRegistrado is not null)
            {
                usuarioRegistrado.PhoneNumber = usuario.NumeroTelefone;
                usuarioRegistrado.UserName = usuario.NomeUsuario;
                usuarioRegistrado.NormalizedUserName = usuario.NomeUsuario.ToLower();
                usuarioRegistrado.Cargo = usuario.Cargo;
                usuarioRegistrado.Email = usuario.Email;
                usuarioRegistrado.Senha = usuario.Senha;

                await _usuarioRepository.AtualizarUsuario(usuarioRegistrado);

                return usuarioRegistrado;
            }
            throw new ArgumentException("Não foi possível atualizar este usuário pois ele não existe no banco de dados ou não possui esse Id.");
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetUsuarios();

            usuarios = usuarios
                .Except(usuarios.Where(usuario => usuario.Deletado == true));

            return usuarios;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosByNome(string nome)
        {
            var usuarios = await _usuarioRepository.GetUsuariosByNome(nome);

            usuarios = usuarios
                .Except(usuarios.Where(usuario => usuario.Deletado == true));

            return usuarios;
        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);

            if(usuario is null || usuario.Deletado == true)
            {
                throw new ArgumentException("Não existe um usuário com este Id no banco de dados.");
            }
            
            return usuario;
        }

        public Task DeslogarUsuario()
        {
            _usuarioRepository.DeslogarUsuario();

            return Task.CompletedTask;
        }

        public async Task<Usuario?> LogarUsuario(LoginViewModel usuario)
        {
            Usuario? usuarioRegistrado = (await _usuarioRepository.GetUsuariosByNome(usuario.NomeUsuario)).FirstOrDefault(usuarioLista => usuarioLista.NormalizedUserName == usuario.NomeUsuario.ToUpper());

            if(usuarioRegistrado == null)
            {
                throw new ArgumentException("O usuário não existe ou o nome de usuário está incorreto.");
            }

            if(usuario.Senha != usuarioRegistrado.Senha)
            {
                throw new ArgumentException("A senha ou o usuário estão incorretos.");
            }

            var resultado = _usuarioRepository.LogarUsuario(usuarioRegistrado);

            if (resultado is null)
            {
                throw new InvalidOperationException("Login falhou.");
            }

            return await _usuarioRepository.LogarUsuario(usuarioRegistrado);
        }
    }
}   
