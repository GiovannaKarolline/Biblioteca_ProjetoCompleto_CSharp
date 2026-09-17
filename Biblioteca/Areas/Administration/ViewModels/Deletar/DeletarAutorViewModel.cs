using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarAutorViewModel
    {
        [Required(ErrorMessage = "O autor a ser deletado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Autor>? Autores { get; set; }
    }
}
