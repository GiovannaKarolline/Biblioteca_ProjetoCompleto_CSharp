using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels.Deletar
{
    public class DeletarCategoriaViewModel
    {
        [Required(ErrorMessage = "A categoria a ser deletada precisa ser definida.")]
        public Guid Id { get; set; }

        public IEnumerable<Categoria>? Categorias { get; set; }
    }
}
