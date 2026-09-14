using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels.Criar
{
    public class CriarObraViewModel : ObraLiterariaViewModel
    {

        public IEnumerable<Categoria>? CategoriasExistentes { get; set; }

        public IEnumerable<Editora>? EditorasExistentes { get; set; }

        public IEnumerable<Autor>? AutoresExistentes { get; set; }

        public List<Guid> AutoresSelecionados { get; set; }
    }
}
