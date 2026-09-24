using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<Usuario?> LogarUsuario(LoginViewModel usuario);

        public Task DeslogarUsuario();

        public Task<Usuario?> CadastrarUsuario(UsuarioViewModel usuario);

        public Task<Usuario> DeletarUsuario(Guid id);

        public Task<Usuario> AtualizarUsuario(UsuarioViewModel usuario);

        public Task<IEnumerable<Usuario>> GetUsuarios();

        public Task<IEnumerable<Usuario>> GetUsuariosByNome(string nome);

        public Task<Usuario?> GetUsuarioById(Guid id);
    }
}
