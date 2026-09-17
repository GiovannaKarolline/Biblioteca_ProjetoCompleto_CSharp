using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarCopiaViewModel
    {
        [Required(ErrorMessage = "A copia a ser deletada precisa ser definida.")]
        public Guid Id { get; set; }
        public IEnumerable<Copia>? Copias { get; set; }
    }
}
