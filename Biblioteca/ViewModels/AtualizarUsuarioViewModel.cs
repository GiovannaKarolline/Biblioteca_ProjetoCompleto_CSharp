using Biblioteca.Models;

namespace Biblioteca.ViewModels
{
    public class AtualizarUsuarioViewModel : UsuarioViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}