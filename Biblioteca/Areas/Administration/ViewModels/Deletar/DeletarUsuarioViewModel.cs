using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarUsuarioViewModel
    {
        [Required(ErrorMessage = "O usuário a ser deletado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
