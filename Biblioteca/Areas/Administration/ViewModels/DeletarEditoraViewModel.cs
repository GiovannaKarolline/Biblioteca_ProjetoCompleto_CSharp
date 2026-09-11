using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarEditoraViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Editora>? Editoras { get; set; }
    }
}
