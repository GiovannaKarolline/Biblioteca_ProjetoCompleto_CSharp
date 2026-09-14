using Biblioteca.Models;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarCategoriaViewModel : CategoriaViewModel
    {
        public Guid Id { get; set; }

        public IEnumerable<Categoria>? Categorias { get; set; }
    }
}
