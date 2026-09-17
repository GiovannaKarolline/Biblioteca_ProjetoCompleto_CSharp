using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Atualizar
{
    public class AtualizarCategoriaViewModel : CategoriaViewModel
    {
        [Required(ErrorMessage = "A categoria a ser atualizada precisa ser definida.")]
        public Guid Id { get; set; }

        public IEnumerable<Categoria>? Categorias { get; set; }
    }
}
