using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarCategoriaViewModel
    {
        public Guid Id { get; set; }
        public IEnumerable<Categoria>? Categorias { get; set; }
    }
}
