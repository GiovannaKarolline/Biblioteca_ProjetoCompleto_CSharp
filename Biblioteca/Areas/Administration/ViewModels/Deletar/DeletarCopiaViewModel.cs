using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarCopiaViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Copia>? Copias { get; set; }
    }
}
