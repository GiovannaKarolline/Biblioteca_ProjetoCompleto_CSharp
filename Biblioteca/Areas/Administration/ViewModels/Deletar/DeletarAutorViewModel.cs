using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarAutorViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Autor>? Autores { get; set; }
    }
}
