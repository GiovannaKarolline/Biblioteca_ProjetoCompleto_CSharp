using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarUsuarioViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
