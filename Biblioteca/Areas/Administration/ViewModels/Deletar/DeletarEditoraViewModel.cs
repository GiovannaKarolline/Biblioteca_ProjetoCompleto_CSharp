using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarEditoraViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Editora>? Editoras { get; set; }
    }
}
