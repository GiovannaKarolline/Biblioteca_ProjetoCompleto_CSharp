using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Biblioteca.ViewModels
{
    public class CopiaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A obra representada pela cópia precisa ser definida.")]
        public Guid ObraId { get; set; }

        [Required(ErrorMessage = "O status de disponibilidade da cópia precisa ser definido.")]
        public Boolean StatusDisponibilidade { get; set; }

        public IPagedList<Copia> Copias { get; set; }

        public IEnumerable<ObraLiteraria> ObrasLiterarias { get; set; }
    }
}
