using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarAutorViewModel : AutorViewModel
    {
        [Required(ErrorMessage = "O autor a ser atualizado precisa ser definido.")]
        public Guid Id { get; set; }

        public IEnumerable<Autor>? Autores { get; set; }
    }
}
