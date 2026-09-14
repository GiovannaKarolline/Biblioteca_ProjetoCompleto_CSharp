using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        public bool Deletado { get; set; } = false;

        public List<ObraLiteraria> ObrasLiterarias { get; set; }

        public override string ToString()
        {
            return $"\nCategoria" +
                $"\n* Id: {Id.ToString()}" +
                $"\n* Título: {Titulo}\n";
        }
    }
}
