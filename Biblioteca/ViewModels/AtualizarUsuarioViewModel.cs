using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class AtualizarUsuarioViewModel : UsuarioViewModel
    {
        [Required(ErrorMessage = "O usuário a ser atualizado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}