using Biblioteca.Models;
using Biblioteca.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Criar
{
    public class CriarObraViewModel : ObraLiterariaViewModel
    {

        public IEnumerable<Categoria>? CategoriasExistentes { get; set; }

        public IEnumerable<Editora>? EditorasExistentes { get; set; }

        public IEnumerable<Autor>? AutoresExistentes { get; set; }

        [Required(ErrorMessage = "O(s) autor(es) da obra precisa(m) ser definido(s).")]
        public List<Guid> AutoresSelecionados { get; set; }
    }
}
