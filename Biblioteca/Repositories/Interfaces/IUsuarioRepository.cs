using Biblioteca.Models;

namespace Biblioteca.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<IEnumerable<Usuario>> GetUsuarios();

        public Task<IEnumerable<Usuario>> GetUsuariosByNome(string nome);

        public Task<Usuario?> GetUsuarioById(Guid id);

        public Task<Usuario?> LogarUsuario(Usuario usuario);

        public Task DeslogarUsuario();

        public Task<Usuario?> CriarUsuario(Usuario usuario);

        public Task<Usuario> DeletarUsuario(Usuario usuario);

        public Task<Usuario> AtualizarUsuario(Usuario usuario);

    }
}
