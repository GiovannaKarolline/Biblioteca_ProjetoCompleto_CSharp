using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Editoras")]
    public class Editora
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public bool Deletado { get; set; } = false;

        public List<ObraLiteraria> Obras { get; set; }

        public override string ToString()
        {
            return $"\nEditora:\n" +
                $"\n• Nome da editora: {Nome}\n" +
                $"\n• Id: {Id.ToString()}";
        }
    }
}
