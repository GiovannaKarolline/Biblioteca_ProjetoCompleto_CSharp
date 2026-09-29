using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Biblioteca.ViewModels
{
    public class EditoraViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da editora precisa ser preenchido.")]
        public string Nome { get; set; }

        public IPagedList<Editora>? Editoras { get; set; }
    }
}
