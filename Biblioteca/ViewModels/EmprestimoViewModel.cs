using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class EmprestimoViewModel
    {
        [Required(ErrorMessage = "O empréstimo precisa ter um Id.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O empréstimo precisa estar atrelado ao usuário solicitante.")]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "A data em que o empréstimo foi realizado precisa ser preenchida.")]
        [DataType(DataType.Date)]
        public DateOnly DataRetirada { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DataDevolucao { get; set; }

        [Required(ErrorMessage = "A data prevista de devolução das cópias emprestadas precisa ser definida.")]
        [DataType(DataType.Date)]
        public DateOnly DataPrevistaDevolucao { get; set; }

        public Boolean Finalizado { get; set; } = false;

        public IEnumerable<Copia>? Copias = Enumerable.Empty<Copia>();
    }
}
