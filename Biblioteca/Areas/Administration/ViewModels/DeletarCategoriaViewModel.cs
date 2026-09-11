using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class DeletarCategoriaViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Categoria>? Categorias { get; set; }
    }
}
