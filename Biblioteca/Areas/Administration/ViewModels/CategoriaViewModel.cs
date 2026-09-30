using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O título da categoria precisa ser preenchido.")]
        public string Titulo { get; set; }

        public IPagedList<Categoria>? Categorias { get; set; }

        public IEnumerable<Categoria>? CategoriasExistentes { get; set; }
    }
}
