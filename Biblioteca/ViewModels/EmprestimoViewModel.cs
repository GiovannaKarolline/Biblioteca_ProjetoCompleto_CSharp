using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class EmprestimoViewModel
    {
        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly DataRetirada { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DataDevolucao { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DataPrevistaDevolucao { get; set; }

        public Boolean Finalizado { get; set; } = false;

        [Required]
        public List<Copia> Copias { get; set; }
    }
}
