using Biblioteca.Models;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class AutorViewModel
    {
        [Required(ErrorMessage = "O autor a ser atualizado precisa ser definido.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O primeiro nome do autor precisa ser preenchido.")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O sobrenome do autor precisa ser preenchido")]
        public string Sobrenome { get; set; }

        public IPagedList<Autor>? Autores { get; set; }
    }
}
