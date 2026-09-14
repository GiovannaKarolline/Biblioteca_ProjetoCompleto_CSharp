using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    [Table("ObrasLiterarias")]
    public class ObraLiteraria : IValidatableObject
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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

        public Editora Editora { get; set; }
        public Categoria Categoria { get; set; }

        public bool Deletado { get; set; } = false;

        public List<Autor> Autores { get; set; }
        public List<Copia> Copias { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //o livro legível mais antigo é do ano de 698
            if (AnoPublicacao < 698 || AnoPublicacao > DateTime.Now.Year)
                yield return new ValidationResult(
                    $"O ano deve estar entre 698 e {DateTime.Now.Year}.",
                    new[] { nameof(AnoPublicacao) });
        }

        public override string ToString()
        {
            return $"\nObra Literária" +
                $"\n* Id: {Id.ToString()}" +
                $"\n* Título: {Titulo}" +
                $"\n* Foto da Capa: {FotoCapa}" +
                $"\n* ISBN: {ISBN}" +
                $"\n* Ano de Publicação: {AnoPublicacao}" +
                $"\n* Id da Editora: {EditoraId.ToString()}" +
                $"\n* Id da Categoria: {CategoriaId.ToString()}\n";
        }
    }
}
