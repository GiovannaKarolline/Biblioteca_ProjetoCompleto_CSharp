using System.ComponentModel.DataAnnotations;

namespace Biblioteca.ViewModels
{
    public class EditoraViewModel
    {
        [Required(ErrorMessage = "O nome da editora precisa ser preenchido.")]
        public string Nome { get; set; }
    }
}
