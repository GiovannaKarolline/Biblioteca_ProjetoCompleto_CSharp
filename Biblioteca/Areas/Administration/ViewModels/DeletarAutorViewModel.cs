using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarAutorViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Autor>? Autores { get; set; }
    }
}
