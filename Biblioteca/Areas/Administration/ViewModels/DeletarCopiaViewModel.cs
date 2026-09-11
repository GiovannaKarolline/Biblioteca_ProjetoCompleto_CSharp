using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarCopiaViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Copia>? Copias { get; set; }
    }
}
