using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class CopiaViewModel
    {
        [Required(ErrorMessage = "A obra representada pela cópia precisa ser definida.")]
        public Guid ObraId { get; set; }

        [Required(ErrorMessage = "O status de disponibilidade da cópia precisa ser definido.")]
        public Boolean StatusDisponibilidade { get; set; }
    }
}
