using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Copias")]
    public class Copia
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid ObraId { get; set; }

        public ObraLiteraria? ObraLiteraria { get; set; }

        [Required]
        [Display(Name = "Status de Disponibilidade")]
        public Boolean StatusDisponibilidade{ get; set; }

        public bool Deletado { get; set; } = false;

        public List<Emprestimo>? Emprestimos { get; set; }

        public override string ToString()
        {
            return $"\nCópia" +
                $"\n* Id: {Id.ToString()}" +
                $"\n* Id da Obra Referenciada: {ObraId.ToString()}" +
                $"\n* Status de Disponibilidade (verdadeiro = disponível): {StatusDisponibilidade.ToString()}\n";
        }
    }
}
