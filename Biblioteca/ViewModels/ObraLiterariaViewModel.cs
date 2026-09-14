using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class ObraLiterariaViewModel
    {
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [Length(10, 17)]
        public string ISBN { get; set; }

        [Required]
        public int AnoPublicacao { get; set; }

        [Required]
        [Url]
        public string FotoCapa { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }

        [Required]
        public Guid EditoraId { get; set; }

        public List<Autor> Autores { get; set; } = new List<Autor>();

        public IEnumerable<ObraLiteraria>? ObrasLiterarias { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //o livro legível mais antigo é do ano de 698
            if (AnoPublicacao < 698 || AnoPublicacao > DateTime.Now.Year)
                yield return new ValidationResult(
                    $"O ano deve estar entre 698 e {DateTime.Now.Year}.",
                    new[] { nameof(AnoPublicacao) });
        }
    }
}
