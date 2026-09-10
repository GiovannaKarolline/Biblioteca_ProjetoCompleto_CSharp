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
        public DateOnly DataDevolucao { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DataPrevistaDevolucao { get; set; }

        public bool Deletado { get; set; } = true;

        public bool Finalizado { get; set; } = false;

        [Required]
        public List<Copia> Copias { get; set; } = new List<Copia>();

        public override string ToString()
        {
            return $"\nEmpréstimo" +
                $"\nId: {Id.ToString()}" +
                $"\nId do Usuário: {UsuarioId.ToString()}" +
                $"\nData de Retirada: {DataRetirada.ToString()}" +
                $"\nData Prevista de Devolução: {DataPrevistaDevolucao.ToString()}" +
                $"\nData da Devolução: {DataDevolucao.ToString()}\n";
        }
    }
}
