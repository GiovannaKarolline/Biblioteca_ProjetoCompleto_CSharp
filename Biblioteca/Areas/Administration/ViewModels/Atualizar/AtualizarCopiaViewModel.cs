using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarCopiaViewModel : CopiaViewModel
    {
        [Required(ErrorMessage = "A cópia a ser atualizada precisa ser definida.")]
        public Guid Id { get; set; }

        public IEnumerable<Copia>? Copias { get; set; }

        public IEnumerable<ObraLiteraria> Obras { get; set; }
    }
}
