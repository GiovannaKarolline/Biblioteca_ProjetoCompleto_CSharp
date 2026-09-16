using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Emprestimos")]
    public class Emprestimo
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DataRetirada { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DataDevolucao { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DataPrevistaDevolucao { get; set; }

        public bool Deletado { get; set; } = false;

        public bool Finalizado { get; set; } = false;

        [Required]
        public List<Copia> Copias { get; set; } = new List<Copia>();

        public override string ToString()
        {
            return $"\nEmpréstimo" +
                $"\n* Id: {Id.ToString()}" +
                $"\n* Id do Usuário: {UsuarioId.ToString()}" +
                $"\n* Data de Retirada: {DataRetirada.ToString()}" +
                $"\n* Data Prevista de Devolução: {DataPrevistaDevolucao.ToString()}" +
                $"\n* Data da Devolução: {DataDevolucao.ToString()}" +
                $"\n* Finalizado: {Finalizado.ToString()}\n";
        }
    }
}
