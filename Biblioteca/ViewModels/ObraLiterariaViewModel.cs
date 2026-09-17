using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class ObraLiterariaViewModel
    {
        [Required(ErrorMessage = "O título da obra literária precisa ser preenchido.")]
        [StringLength(50, ErrorMessage = "O título precisa ter entre 1 e 50 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O código ISBN da obra literária precisa ser preenchido.")]
        [Length(10, 17, ErrorMessage = "Códigos ISBN possuem entre 10 a 17 caracteres.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "O ano de publicação dessa edição de obra literária precisa ser preenchido.")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "O link para a foto de capa da obra literária precisa ser preenchido.")]
        [Url(ErrorMessage = "A foto de capa da obra literária precisa ser um link válido.")]
        public string FotoCapa { get; set; }

        [Required(ErrorMessage = "A categoria a qual a obra literária pertence precisa ser definida.")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "A editora que publicou a obra literária precisa ser posta.")]
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
