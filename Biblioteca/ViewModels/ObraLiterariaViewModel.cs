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
        [Range(10, 17)]
        public string ISBN { get; set; }

        [Required]
        [MinLength(698)]
        public int AnoPublicacao { get; set; }

        [Required]
        [Url]
        public string FotoCapa { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }

        [Required]
        public Guid EditoraId { get; set; }

        public List<Autor> Autores { get; set; }

        public IEnumerable<ObraLiteraria> ObrasLiterarias { get; set; }
    }
}
