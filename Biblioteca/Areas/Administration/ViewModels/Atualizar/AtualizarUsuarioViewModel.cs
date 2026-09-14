using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarUsuarioViewModel : UsuarioViewModel
    {

        public Guid Id { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
