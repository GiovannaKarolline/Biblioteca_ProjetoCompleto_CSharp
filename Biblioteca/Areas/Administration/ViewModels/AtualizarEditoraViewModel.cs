using Biblioteca.Models;
using Biblioteca.ViewModels;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class AtualizarEditoraViewModel : EditoraViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Editora>? Editoras { get; set; }
    }
}
