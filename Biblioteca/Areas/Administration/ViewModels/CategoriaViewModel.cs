using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Areas.Administration.ViewModels
{
    public class CategoriaViewModel
    {
        [Required(ErrorMessage = "O título da categoria precisa ser preenchido.")]
        public string Titulo { get; set; }
    }
}
